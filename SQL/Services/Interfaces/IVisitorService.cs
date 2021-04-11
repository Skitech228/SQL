using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQL.Models;

namespace SQL.Services.Interfaces
{
    interface IVisitorService
    {
        Task<bool> AddVisitorAsync(Visitor visitor);

        Task<bool> RemoveVisitorAsync(Visitor visitor);

        Task<bool> UpdateVisitorAsync(Visitor visitor);

        Task<Visitor> GetByIdAsync(int id);

        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
    }
}
