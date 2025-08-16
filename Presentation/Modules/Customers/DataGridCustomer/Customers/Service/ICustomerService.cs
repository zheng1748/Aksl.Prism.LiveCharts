using System.Collections.Generic;
using System.Threading.Tasks;

using Aksl.Modules.DataGridCustomer.Models;

namespace Aksl.Modules.DataGridCustomer.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();

        Task<int> AddAsync(CustomerDto customer);

        Task<bool> UpdateAsync(CustomerDto customerDto);
    }
}
 