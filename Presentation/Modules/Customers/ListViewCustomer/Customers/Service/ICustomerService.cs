using System.Collections.Generic;
using System.Threading.Tasks;

using Aksl.Modules.ListViewCustomer.Models;

namespace Aksl.Modules.ListViewCustomer.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();

        Task<int> AddAsync(CustomerDto customer);

        Task<bool> UpdateAsync(CustomerDto  customerDto);
    }
}
 