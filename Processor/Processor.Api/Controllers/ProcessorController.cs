using Microsoft.AspNetCore.Mvc;
using DataArt.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DataArt.Database;

[ApiController]
[Route("api/processor")]
public class ProcessorController : ControllerBase
{
    int Type = 0;
    DateTime date = DateTime.MinValue;
    private readonly MyDbContext _context;
    public ProcessorController(MyDbContext context)
    {
        _context = context;
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
    //public async Task<IEnumerable<Incident>> TestEvent()
    //{
    //    var tasks = await _context.Incident.Include(t => t.Id).ToListAsync();
    //    return tasks;
    //}

}
