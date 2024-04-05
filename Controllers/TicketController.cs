using Microsoft.AspNetCore.Mvc;
using CusSupApp.Models;
using System.Text.Json;

namespace CusSupApp.Controllers {

    public class TicketController : Controller {

        public static List<Ticket> tickets = new List<Ticket>();

        public IActionResult Index() {
            var json = HttpContext.Session.GetString("TicketList");
            if (!string.IsNullOrEmpty(json)) {
                tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
            }

            // Sort tickets by deadline in descending order
            var sortedTickets = tickets.OrderByDescending(t => t.Deadline).ToList();
            return View(sortedTickets);
        }

        public IActionResult Resolve(int id) {
            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null) { ticket.IsResolved = true; }

            HttpContext.Session.SetString("TicketList", JsonSerializer.Serialize(tickets));

            return RedirectToAction("Index");
        }

        public IActionResult CreateTicket() {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTicket(Ticket ticket) {
            if (!tickets.Any()) {
                ticket.Id = 1;
            } else {
                ticket.Id = tickets.Last().Id + 1;
            }
            
            ticket.EntryTime = DateTime.Now;
            tickets.Add(ticket);

            HttpContext.Session.SetString("TicketList", JsonSerializer.Serialize(tickets));

            return RedirectToAction("Index");
        }
    }
}