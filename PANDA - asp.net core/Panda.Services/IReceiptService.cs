using Panda.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public interface IReceiptService
    {
        Receipt CreateReceipt( Package package, PandaUser user);

        IEnumerable<Receipt> GetReceiptsForCurrentUserById(string id);

        IQueryable<Receipt> GetReceitForDetailsById(string id);
    }
}
