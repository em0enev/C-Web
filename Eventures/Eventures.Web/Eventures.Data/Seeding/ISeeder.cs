using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventures.Data.Seeding
{
    public interface ISeeder
    {
        void Seed(EventuresDbContext context);
    }
}
