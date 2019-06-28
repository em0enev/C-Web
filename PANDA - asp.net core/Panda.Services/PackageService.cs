using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public class PackageService : IPackageService
    {
        private readonly PandaDbContext context;
        private readonly IReceiptService receiptService;

        public PackageService(PandaDbContext context, IReceiptService receiptService)
        {
            this.context = context;
            this.receiptService = receiptService;
        }

        public Package CreatePackage(string recipientName, decimal weight, string shippingAddress, string description)
        {
            var recipient = this.context.PandaUsers.SingleOrDefault(x => x.UserName == recipientName);
            var package = new Package()
            {
                Description = description,
                Status = PackageStatus.Pending,
                Weight = weight,
                ShippingAddress = shippingAddress,
                Recipient = recipient,
            };

            this.context.Packages.Add(package);
            this.context.SaveChanges();

            return package;
        }


        public Package GetPackage(string id)
        {
            var package = this.context.Packages
                .Include(x => x.Recipient)
                .Include(x => x.Receipt)
                .SingleOrDefault(x => x.Id == id);

            return package;
        }

        public IEnumerable<Package> GetPackagesByStatus(PackageStatus status)
        {
            var packages = this.context.Packages
                .Include(x => x.Recipient)
                .Where(x => x.Status == status)
                .ToList();

            return packages;
        }

        public bool ShipPackage(string id)
        {
            var package = GetPackage(id);

            package.Status = PackageStatus.Shipped;
            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(new Random().Next(20, 41));

            this.context.Update(package);
            this.context.SaveChanges();

            return true;
        }

        public bool DeliverPackage(string id)
        {
            var package = GetPackage(id);

            package.Status = PackageStatus.Delivered;

            this.context.Update(package);
            this.context.SaveChanges();

            return true;
        }

        public bool AcquirePackage(string id)
        {
            var package = this.context.Packages
                .Include(x => x.Recipient)
                .Where(x => x.Id == id)
                .SingleOrDefault();

            package.Status = PackageStatus.Acquired;

            this.context.Update(package);
            this.context.SaveChanges();

            var user = this.context.Users
                .SingleOrDefault(x => x.Id == package.Recipient.Id);

            var receipt = this.receiptService.CreateReceipt(package, user);

            this.context.Receipts.Add(receipt);
            this.context.SaveChanges();

            return true;
        }

        public IEnumerable<Package> GetAllPackages()
        {
            var packages = this.context.Packages
                .Include(x => x.Recipient)
                .ToList();

            return packages;
        }
    }
}
