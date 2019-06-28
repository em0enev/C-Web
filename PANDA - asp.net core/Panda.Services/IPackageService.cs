using Panda.Domain;
using System.Collections.Generic;

namespace Panda.Services
{
    public interface IPackageService
    {
        Package CreatePackage(string recipientName, decimal weight, string shippingAddress, string description);

        Package GetPackage(string id);

        bool ShipPackage(string id);

        bool DeliverPackage(string id);

        bool AcquirePackage(string id);

        IEnumerable<Package> GetPackagesByStatus(PackageStatus status);

        IEnumerable<Package> GetAllPackages();
    }
}
