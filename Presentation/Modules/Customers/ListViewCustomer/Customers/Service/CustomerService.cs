using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Aksl.Modules.ListViewCustomer.Models;

namespace Aksl.Modules.ListViewCustomer.Services
{
    public class CustomerService : ICustomerService
    {
        private List<CustomerDto> _memoryCustomers;

        public CustomerService()
        {
            _memoryCustomers = new List<CustomerDto>()
            {
                new CustomerDto{ Id=1, FirstName="Josh", LastName="Smith", IsCompany=false, Email="josh@contoso.com", TotalSales=32132.9d },
                new CustomerDto{ Id=2,FirstName="Greg", LastName="Bujak", IsCompany=false, Email="greg@contoso.com", TotalSales=9848.06d },

                new CustomerDto{Id=3, FirstName="Alfreds Futterkiste", LastName="", IsCompany=true, Email="maria@contoso.com", TotalSales=8832.16d },
                new CustomerDto{ Id=4,FirstName="Eastern Connection", LastName="", IsCompany=true, Email="ann@contoso.com", TotalSales=12831.73d },
            };
        }

        public Task<int> AddAsync(CustomerDto customer)
        {
            int maxId = _memoryCustomers.Max(c=>c.Id);
            customer.Id= maxId+1;

            _memoryCustomers.Add(customer);

            return Task.FromResult(customer.Id);
        }

        public Task<List<CustomerDto>> GetAllAsync()
        {
            return Task.FromResult(_memoryCustomers);
        }

        public Task<bool> UpdateAsync(CustomerDto customerDto)
        {
            try
            {
                var storedCCustomer = _memoryCustomers.FirstOrDefault(c => c.Id== customerDto.Id);
                if (storedCCustomer != null)
                {
                    _memoryCustomers.Remove(storedCCustomer);
                }

                _memoryCustomers.Add(customerDto);
                _memoryCustomers.OrderByDescending(c => c.Id);

                return Task.FromResult(true);
            }
            catch 
            {

            }

            return Task.FromResult(false);
        }
    }
}
 