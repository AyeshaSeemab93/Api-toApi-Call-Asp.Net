using Microsoft.AspNetCore.Mvc;


[ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
	{
    [HttpGet(Name = "GetHello")]
    public string Get()
    {

        //To run this on browser,type in browser https://localhost:7198/Test
        return "Hello ryhmä";
	}
	}


