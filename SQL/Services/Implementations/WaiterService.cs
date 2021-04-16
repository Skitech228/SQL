#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQL.Models;
using SQL.Services.Interfaces;

#endregion

namespace SQL.Services.Implementations
{
    public class WaiterService : IWaiterService
    {
        private readonly HotelContext _context;

        public WaiterService(HotelContext context) => _context = context;

        #region Implementation of IPreOrderService

        /// <inheritdoc />
        public async Task<bool> AddWaiterAsync(Waiter visitor)
        {
            _context.Waiters.Add(visitor);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveWaiterAsync(Waiter visitor)
        {
            _context.Waiters.Attach(visitor);
            _context.Waiters.Remove(visitor);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateWaiterAsync(Waiter visitor)
        {
            _context.Waiters.Attach(visitor);
            _context.Entry(visitor).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Waiter> GetByIdAsync(int id) => await _context.Waiters.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Waiter>> GetAllWaitersAsync() => await _context.Waiters
                                                                               .ToListAsync();

        #endregion
    }
}