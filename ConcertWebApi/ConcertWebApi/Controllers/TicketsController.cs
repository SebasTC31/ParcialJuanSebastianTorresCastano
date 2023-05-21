using ConcertWebApi.DAL;
using ConcertWebApi.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ConcertWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public TicketsController(DataBaseContext context)
        {
            _context = context;
        }

        //all tickets
        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();

            if (tickets == null) return NotFound();

            return tickets;
        }

        //ticket by id
        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult> ValidateTicket(Guid? id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound("Boleta no válida");
            }
            else if (ticket.IsUsed)
            {
                return BadRequest($"Boleta ya usada. Fecha: {ticket.UseDate}, portería {ticket.EntranceGate}");
            }
            else
            {
                ticket.IsUsed = true;
                ticket.UseDate = DateTime.Now;
                ticket.EntranceGate = "Sur";
                await _context.SaveChangesAsync();
                return Ok("Boleta válida, puede ingresar al concierto");
            }
        }
    }
}
