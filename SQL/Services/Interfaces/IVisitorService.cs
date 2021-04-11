#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using SQL.Models;

#endregion

namespace SQL.Services.Interfaces
{
    public interface IVisitorService
    {
        Task<bool> AddVisitorAsync(Visitor visitor);

        Task<bool> RemoveVisitorAsync(Visitor visitor);

        Task<bool> UpdateVisitorAsync(Visitor visitor);

        Task<Visitor> GetByIdAsync(int id);

        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
    }
}