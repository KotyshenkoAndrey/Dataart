using Microsoft.AspNetCore.Mvc;
using DataArt.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/processor")]
public class ProcessorController : ControllerBase
{
    int Type = 0;
    DateTime date = DateTime.MinValue;
    public ProcessorController()
    {

    } 

    [HttpPost("/api/processor/getEvent")]
    public void getEvent([FromBody] Event EventJson)
    {
        Console.WriteLine("Я в докере");
        Console.WriteLine(EventJson.Type + " " + EventJson.Time);
        Type = Convert.ToInt32(EventJson.Type);
        date = EventJson.Time.ToUniversalTime();
    }

    //[HttpGet("/api/processor/getTest")]
    //public string TestEvent()
    //{
    //    return Type.ToString() + "    " + date.ToString();
    //}

}
