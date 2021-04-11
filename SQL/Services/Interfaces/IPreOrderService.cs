#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using SQL.Models;

#endregion

namespace SQL.Services.Interfaces
{
    public interface IPreOrderService
    {
        Task<bool> AddPreOrderAsync(PreOrder customer);

        Task<bool> RemovePreOrderAsync(PreOrder customer);

        Task<bool> UpdatePreOrderAsync(PreOrder customer);

        Task<PreOrder> GetByIdAsync(int id);

        Task<IEnumerable<PreOrder>> GetAllPreOrdersAsync();
    }
}