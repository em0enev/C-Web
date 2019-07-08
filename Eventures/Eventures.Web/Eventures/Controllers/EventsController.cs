using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.Domain;
using Eventures.Models.BindingModels;
using Eventures.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventuresDbContext context;

        public EventsController(EventuresDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(EventCreateBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                Event eventForDb = new Event()
                {
                    Name = model.Name,
                    Place = model.Place,
                    Start = model.Start,
                    End = model.End,
                    PricePerTicket = model.PricePerTicket,
                    TotalTickets = model.TotalTickets
                };

                this.context.Events.Add(eventForDb);
                this.context.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View();
        }

        public IActionResult All()
        {
            var events = this.context.Events
                .Select(x => new EventAllViewModel()
                {
                    Name = x.Name,
                    Place = x.Place,
                    Start = x.Start.ToString("dd-MMM-yyy HH:mm", CultureInfo.InvariantCulture),
                    End = x.End.ToString("dd-MMM-yyy HH:mm", CultureInfo.InvariantCulture)
                })
                .ToList();

            return View(events);
        }
    }
}