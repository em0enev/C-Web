using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Domain;
using WebApi_ChatInc.Models;

namespace WebApi_ChatInc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessagesDbContext context;

        public MessageController(MessagesDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name ="All")]
        [Route("all")]
        public ActionResult<IEnumerable<Message>> AllOrderedByCreatedOnAscending()
        {
            return this.context.Messages
                .OrderBy(x => x.CreatedOn)
                .ToList();
        }

        [HttpPost(Name = "Create")]
        [Route("create")]
        public async Task<ActionResult> Create(MessageCreateBindingModel model)
        {
            Message message = new Message()
            {
                Content = model.Content,
                User = model.User,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();
            return this.Ok();
        }
    }
}
