using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.App.Models.Receipts;
using Panda.Services;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace Panda.App.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly IReceiptService receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            this.receiptService = receiptService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var myReceipts = this.receiptService.GetReceiptsForCurrentUserById(userId)
                 .Select(x => new ReceiptIndexViewModel()
                 {
                     Id = x.Id,
                     Fee = x.Fee,
                     IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                     Recipient = x.Recipient.UserName
                 })
                 .ToList();

            return this.View(myReceipts);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {

            var receipt = this.receiptService.GetReceitForDetailsById(id)
                .Select(x => new ReceiptDetailsViewModel()
                {
                    Id = x.Id,
                    DeliveryAddress = x.Package.ShippingAddress,
                    Description = x.Package.Description,
                    IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Recipient = x.Recipient.UserName,
                    TotalPrice = x.Fee,
                    Weight = x.Package.Weight
                })
                .SingleOrDefault();

            return this.View(receipt);
        }
    }
}
