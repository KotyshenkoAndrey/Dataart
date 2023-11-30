using Microsoft.AspNetCore.Mvc;
using DataArt.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DataArt.Database;

[ApiController]
[Route("api/[controller]")]
public class ProcessorController : ControllerBase
{
    private readonly MyDbContext _context;
    public ProcessorController(MyDbContext context)
    {
        _context = context;
    } 

    [HttpPost("/getEvent")]
    public async Task<IActionResult> getEvent([FromBody] Event EventJson)
    {
        if (ModelState.IsValid)
        {
            if( EventJson.Type == EventTypeEnum.Event1) 
            {
                _context.IncidentDb.Add(new Incident { Id = EventJson.Id, Type = IncidentTypeEnum.Incident1, Time = EventJson.Time.ToUniversalTime() });
            }
            

            await _context.SaveChangesAsync();

            return Ok("Event added successfully");
        }

        return BadRequest("Invalid model state");
    }
    [HttpGet]
    public async Task<IActionResult> GetIncidents([FromQuery] string sortBy = "Time", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.IncidentDb.AsQueryable();
        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy)
            {
                case "Type":
                    query = query.OrderBy(t => t.Type);
                    break;
                case "Time":
                    query = query.OrderBy(t => t.Time);
                    break; 
                default:
                {
                    query = query.OrderBy(t => t.Time);
                    break;  
                }
            }
        }

        int totalItems = await query.CountAsync();
        var pagedIncidents = await query.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();
        return Ok(new { TotalItems = totalItems, Incidents = pagedIncidents });
    }
    //public void ReceiveMessage(Event newEvent)
    //{
    //    _context.IncidentDb.Add(new Incident { Id = newEvent.Id, Type = IncidentTypeEnum.Incident1, Time = newEvent.Time }); ;

    //    // Сохраняем изменения в базе данных
    //    _context.SaveChanges();
    //}
}
