#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using SQL.Models;

#endregion

namespace SQL.Services.Interfaces
{
    public interface IWaiterService
    {
        Task<bool> AddWaiterAsync(Waiter customer);

        Task<bool> RemoveWaiterAsync(Waiter customer);

        Task<bool> UpdateWaiterAsync(Waiter customer);

        Task<Waiter> GetByIdAsync(int id);

        Task<IEnumerable<Waiter>> GetAllWaitersAsync();
    }
}