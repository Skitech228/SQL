#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQL.Models;
using SQL.Services.Interfaces;

#endregion

namespace SQL.Services.Implementations
{
    internal class PreOrderService : IPreOrderService
    {
        private readonly HotelContext _context;

        private PreOrderService(HotelContext context) => _context = context;

        #region Implementation of IPreOrderService

        /// <inheritdoc />
        public async Task<bool> AddPreOrderAsync(PreOrder preOrder)
        {
            _context.PreOrders.Add(preOrder);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemovePreOrderAsync(PreOrder preOrder)
        {
            _context.PreOrders.Attach(preOrder);
            _context.PreOrders.Remove(preOrder);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdatePreOrderAsync(PreOrder preOrder)
        {
            _context.PreOrders.Attach(preOrder);
            _context.Entry(preOrder).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<PreOrder> GetByIdAsync(int id) => await _context.PreOrders.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<PreOrder>> GetAllPreOrdersAsync() => await _context.PreOrders
                                                                                   .Include(x => _context.PreOrders)
                                                                                   .ToListAsync();

        #endregion
    }
}