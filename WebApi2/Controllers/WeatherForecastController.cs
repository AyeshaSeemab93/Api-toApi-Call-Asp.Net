using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

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

        CallWebApi1().Wait();

        return new[] { "moiiiii2" };
    }

    [HttpGet("ReceiveFromApiA")] // Define a specific route for receiving the call from API-A
    public IActionResult ReceiveFromApiA()
    {
        // Your logic here to handle the API-A's call
        string responseMessage = "Received call from API-A in API-B";

        return Ok(responseMessage); // Return a response message
    }


    [HttpGet("CallWebApi1")]
    public async Task<string> CallWebApi1()
    {
        HttpClient client = new HttpClient();
        //for calling in pther api and taking data
        var response = await client.GetAsync("https://localhost:7199/WeatherForecast/ReceiveFromApiB");
        //for calling back and forth ,this call go to other api to call function
        //var response = await client.GetAsync("https://localhost:7199/WeatherForecast/CallWebApi2");
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return (" Success: Called WebApi1 from WebApi2 " + responseData);
        }
        else
        {
            return ("API-A returned an error: " + response.StatusCode);
        }
    }
}

