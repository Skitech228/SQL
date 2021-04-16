#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQL.Models;
using SQL.Services.Interfaces;

#endregion

namespace SQL.Services.Implementations
{
    public class VisitorService : IVisitorService
    {
        private readonly HotelContext _context;

        public VisitorService(HotelContext context) => _context = context;

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
                                                                                 .Include(x => x.Waiter)
                                                                                 .ToListAsync();

        #endregion
    }
}