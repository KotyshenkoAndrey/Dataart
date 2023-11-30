using Microsoft.AspNetCore.Mvc;
using DataArt.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;

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
        //using (var client = new HttpClient())
        //{
        //    var response = await client.PostAsync("https://localhost:10001/getEvent", data);

        //    string result = response.Content.ReadAsStringAsync().Result;
        //    Console.WriteLine(result);
        //}
        var client = _client.CreateClient();

        string apiUrl = "http://localhost:10001/getEvent";  // Адрес второго контроллера

        var json = JsonConvert.SerializeObject(newEvent);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpClient httpClient = new HttpClient();
        using var response = await httpClient.PostAsync(apiUrl, content);
    }

    [HttpPost("/sendManual")]
    public async Task<IActionResult> GenerateManualEvent()
    {
        var newEvent = GenerateRandomEvent();
        await SendEventToProcessor(newEvent);
        return Ok(newEvent);
    }
}