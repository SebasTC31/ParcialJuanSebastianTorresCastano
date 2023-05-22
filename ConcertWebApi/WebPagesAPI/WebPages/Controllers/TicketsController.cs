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

        [HttpGet]
        public IActionResult Validate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(Guid? Id)
        {
            try
            {
                var url = "https://localhost:7225/api/Tickets";
                await _httpClient.CreateClient().PostAsJsonAsync(url, Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Validate2(Guid? id)
        {
            try
            {
                return View(await GetTicketById(id));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        //edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate2(Guid? id, Ticket ticket)
        {
            try
            {
                var url = String.Format("https://localhost:7225/api/Tickets/Put/{0}", id);
                await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }

        }

        private async Task<Ticket> GetTicketById(Guid? id)
        {
            var url = String.Format("https://localhost:7225/api/Tickets/Get/{0}", id);
            return JsonConvert.DeserializeObject<Ticket>(await _httpClient.CreateClient().GetStringAsync(url));
        }
    }
}
