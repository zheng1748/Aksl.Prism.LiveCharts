using System;
using System.Linq;

using AutoMapper;

using Aksl.Modules.ListViewCustomer.ViewModels;

namespace Aksl.Modules.ListViewCustomer.Models
{
    public class CustomerAutoMapper : Profile
    {
        public CustomerAutoMapper()
        {
            // CustomerDto => EditableCustomerItemViewModel
            _ = CreateMap<CustomerDto, EditableCustomerItemViewModel>()
                  .ForMember(vm => vm.CustomerId, (map) => map.MapFrom(m => m.Id))
                  .ForMember(vm => vm.FirstName, (map) => map.MapFrom(m => m.FirstName))
                  .ForMember(vm => vm.LastName, (map) => map.MapFrom(m => m.LastName))
                  .ForMember(vm => vm.Email, (map) => map.MapFrom(m => m.Email))
                  .ForMember(vm => vm.IsCompany, (map) => map.MapFrom(m => m.IsCompany))
                  .ForMember(vm => vm.TotalSales, (map) => map.MapFrom(m => m.TotalSales));

            // EditableCustomerItemViewModel => CustomerDto
            _ = CreateMap<EditableCustomerItemViewModel, CustomerDto>()
                  .ForMember(m => m.Id, (map) => map.MapFrom(vm => vm.CustomerId))
                  .ForMember(m => m.FirstName, (map) => map.MapFrom(vm => vm.FirstName))
                  .ForMember(m => m.LastName, (map) => map.MapFrom(vm => vm.LastName))
                  .ForMember(m => m.Email, (map) => map.MapFrom(vm => vm.Email))
                  .ForMember(m => m.IsCompany, (map) => map.MapFrom(vm => vm.IsCompany))
                  .ForMember(m => m.TotalSales, (map) => map.MapFrom(vm => vm.TotalSales));
        }
    }
}
