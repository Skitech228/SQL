using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQL.Models;
using SQL.Services.Interfaces;

namespace SQL.Services.Implementations
{
    class VisitorService:IVisitorService
    {
        private HotelContext _context;

        private VisitorService(HotelContext context) => _context = context;

        #region Implementation of IPreOrderService

        /// <inheritdoc />
        public async Task<bool> AddVisitorAsync(Visitor visitor)
        {
            _context.Visitors.Add(visitor);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveVisitorAsync(Visitor visitor)
        {
            _context.Visitors.Attach(visitor);
            _context.Visitors.Remove(visitor);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateVisitorAsync(Visitor visitor)
        {
            _context.Visitors.Attach(visitor);
            _context.Entry(visitor).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Visitor> GetByIdAsync(int id) => await _context.Visitors.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync() => await _context.Visitors
                                                                                  .Include(x => _context.PreOrders)
                                                                                  .ToListAsync();

        #endregion
    }
}
