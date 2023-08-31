using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    //creating get request with the name GetWeatherForecast"
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<string> Get()
    {
        Console.WriteLine("Go get in Api1");
        //creating log1.txt file using AppendAllLines method.inside is file name , content
        
        System.IO.File.AppendAllLines("log1.txt", new[] { "Go get called at Api1" });
        System.Threading.Thread.Sleep(400); //400ms

        CallWebApi2().Wait();

        return new[] { "moiiiii" };
    }
    [HttpGet("CallWebApi2")]
    public async Task<string> CallWebApi2()
    {
       HttpClient client = new HttpClient();
       // Calling and taking responce from other api
        var response = await client.GetAsync("https://localhost:7178/WeatherForecast/ReceiveFromApiA");
        //for calling back and forth ,this call go to other api to call function
        //var response = await client.GetAsync("https://localhost:7178/WeatherForecast/CallWebApi1");
        if (response.IsSuccessStatusCode)
        {
        var responseData = await response.Content.ReadAsStringAsync();
        return (" Success: Called WebApi2 from WebApi1 " + responseData);
        }
        else
        {
        return("API-B returned an error: " + response.StatusCode);
        }
    }

    [HttpGet("ReceiveFromApiB")] // Define a specific route for receiving the call from API-B
    public IActionResult ReceiveFromApiB()
    {
        // Your logic here to handle the API-A's call
        string responseMessage = "Recieved in api A.Received call from API-B in API-A";

        return Ok(responseMessage); // Return a response message
    }
}



