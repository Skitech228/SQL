using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQL.Models;

namespace SQL.Services.Interfaces
{
    interface IWaiterService
    {
        Task<bool> AddWaiterAsync(Waiter customer);

        Task<bool> RemoveWaiterAsync(Waiter customer);

        Task<bool> UpdateWaiterAsync(Waiter customer);

        Task<Waiter> GetByIdAsync(int id);

        Task<IEnumerable<Waiter>> GetAllWaitersAsync();
    }
}
