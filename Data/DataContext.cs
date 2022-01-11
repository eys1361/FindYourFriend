using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourFriend.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindYourFriend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users  { get; set; }

    }
}
