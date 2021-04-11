using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQL.Models;

namespace SQL.Services.Interfaces
{
    interface IPreOrderService
    {
        Task<bool> AddPreOrderAsync(PreOrder customer);

        Task<bool> RemovePreOrderAsync(PreOrder customer);

        Task<bool> UpdatePreOrderAsync(PreOrder customer);

        Task<PreOrder> GetByIdAsync(int id);

        Task<IEnumerable<PreOrder>> GetAllPreOrdersAsync();      
    }
}
