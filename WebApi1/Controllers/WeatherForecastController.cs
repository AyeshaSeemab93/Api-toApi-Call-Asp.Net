using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers;

[ApiController] //tell this class is API CONTROLLER and enable automatic validation os requests
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    //creating get request with the name GetWeatherForecast"
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<string> Get() //return a list of strings.

    {
        Console.WriteLine("Go get in Api1");// helpful for debugging purposes

        //creating log1.txt file using AppendAllLines method.inside is file name , content
        System.IO.File.AppendAllLines("log1.txt", new[] { "Go get called at Api1" });

        System.Threading.Thread.Sleep(400); //400millisec delay in the method's execution.

        CallWebApi2().Wait();

        return new[] { "moiiiii" };
    }


    //Method to make a call(GET)
    [HttpGet("CallWebApi2")]

    public async Task<string> CallWebApi2() //this will eventually give a string(task)
    {
        //1. create instance/obj of HTTPClient class.Is heandle HTTP Requests(call/Get/POST)
       HttpClient client = new HttpClient(); //For sending request

        // Calling(Send a GET request and get the response)
        //GetAsync: sends an asynchronous HTTP GET request to the specified URL
        //await: wait for the reponse but let main method do next tasks
        var response = await client.GetAsync("https://localhost:7178/WeatherForecast/ReceiveFromApiA");

        //for calling back and forth in loops ,this call go to other api call function
        //var response = await client.GetAsync("https://localhost:7178/WeatherForecast/CallWebApi1");


        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            //response: obtained from above GET request. . Content is content of the response
            //ReadAsStringAsync(): Method of Content class that reads the content of the HTTP response as a string.

            return (" Success: Called WebApi2 from WebApi1 " + responseData);
        }
        else
        {
        return("API-B returned an error: " + response.StatusCode);
        }
    }





    //Method to handle the call request(from other Api) and provide response
    [HttpGet("ReceiveFromApiB")] // Define a specific route for receiving the call from API-B

    public IActionResult ReceiveFromApiB()
    {
        // response message to the caller
        string responseMessage = "Received call from API-B in API-A";

        return Ok(responseMessage); // Return a response message
    }
}


//IActionResult:  represents an HTTP response that will be sent back to the caller.
//ok() : Return a 200 OK/success
// return BadRequest("Some error occurred."); // Return a 400 Bad Request
//return NotFound("Resource not found."); // Return a 404 Not Found
//return Ok(responseMessage); // Return a 200 OK


