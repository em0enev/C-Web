using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Domain;

namespace WebApi.Data
{
    public class MessagesDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessagesDbContext(DbContextOptions options)
            :base(options)
        {

        }
    }
}
