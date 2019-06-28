using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly PandaDbContext context;

        public ReceiptService(PandaDbContext context)
        {
            this.context = context;
        }

        public Receipt CreateReceipt(Package package, PandaUser user)
        {
            var receipt = new Receipt()
            {
                Fee = package.Weight * 2.67m,
                IssuedOn = DateTime.UtcNow,
                Package = package,
                Recipient = user
            };

            return receipt;
        }

        public IEnumerable<Receipt> GetReceiptsForCurrentUserById(string id)
        {
            var myReceipts = this.context.Receipts
                .Include(receipt => receipt.Recipient)
                .Where(x => x.Recipient.Id == id)
                .ToList();

            return myReceipts;
        }

        public IQueryable<Receipt> GetReceitForDetailsById(string id)
        {
            var receipt = this.context.Receipts
                .Include(x => x.Recipient)
                .Include(x => x.Package)
                .Where(x => x.Id == id)
                .AsNoTracking();

            return receipt;
        }
    }
}
