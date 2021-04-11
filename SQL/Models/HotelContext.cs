#region Using derectives

using Microsoft.EntityFrameworkCore;

#endregion

namespace SQL.Models
{
    public class HotelContext : DbContext
    {
        public HotelContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<PreOrder> PreOrders { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=Hotel.db");
        }

        #region Overrides of DbContext

        #endregion
    }
}