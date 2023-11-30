using Microsoft.AspNetCore.Mvc;
using DataArt.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

[ApiController]
[Route("api/generator")]
public class GeneratorController : ControllerBase
{
    private readonly IHttpClientFactory _client;
    private readonly Random _random;

    public GeneratorController(IHttpClientFactory client)
    {
        _client = client;
        _random = new Random();
        StartEventGeneration();
    }
    private async Task StartEventGeneration()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            var newEvent = GenerateRandomEvent();
            await SendEventToProcessor(newEvent);
        }
    }

    private Event GenerateRandomEvent()
    {
        return new Event
        {
            Id = Guid.NewGuid(),
            Type = (EventTypeEnum)_random.Next(1, 5),
            Time = DateTime.Now
        };
    }


    private async Task SendEventToProcessor(Event newEvent)
    {
        var client = _client.CreateClient("ProcessorClient");
        await client.PostAsJsonAsync("/api/processor/getEvent", newEvent);
    }

    [HttpPost("/sendManual")]
    public async Task<IActionResult> GenerateManualEvent()
    {
        var newEvent = GenerateRandomEvent();
        await SendEventToProcessor(newEvent);
        return Ok(newEvent);
    }
}