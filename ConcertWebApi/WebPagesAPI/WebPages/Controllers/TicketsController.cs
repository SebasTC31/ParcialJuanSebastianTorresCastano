using ConcertWebApi.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public TicketsController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7225/api/Tickets/Get";
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
            return View(tickets);
        }
    }
}
