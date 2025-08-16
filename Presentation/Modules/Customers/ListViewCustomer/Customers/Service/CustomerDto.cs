
namespace Aksl.Modules.ListViewCustomer.Models
{
    public class CustomerDto
    {
        public CustomerDto()
        {
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        /// </summary>
        public bool IsCompany { get; set; }

        /// </summary>
        public string LastName { get; set; }

        public double TotalSales { get;  set; }

        //public CustomerDto Copy(CustomerDto customerDto)
        //{
        //    return new CustomerDto() 
        //    { 
        //        Id= customerDto.Id, 
        //        FirstName= customerDto.FirstName,
        //        LastName= customerDto.LastName, 
        //        Email= customerDto.Email, 
        //        IsCompany= customerDto.IsCompany, 
        //        TotalSales= customerDto.TotalSales
        //    };
        //}
    }
}
