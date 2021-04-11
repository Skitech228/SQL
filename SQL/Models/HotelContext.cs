using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Metadata.Internal;

namespace SQL.Models
{
    public class HotelContext:DbContext
    {
        public HotelContext() : base()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=Hotel.db");
        }

        #region Overrides of DbContext

        #endregion

        public DbSet<PreOrder> PreOrders { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

    }
}
