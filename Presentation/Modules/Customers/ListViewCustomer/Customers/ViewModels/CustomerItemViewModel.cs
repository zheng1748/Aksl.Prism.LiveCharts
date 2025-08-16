using System.Windows.Input;

using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using Prism.Unity;
using Unity;

using Aksl.Modules.ListViewCustomer.Models;

namespace Aksl.Modules.ListViewCustomer.ViewModels
{
    public class CustomerItemViewModel : BindableBase
    {
        #region Members  
      

        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Constructors
        public CustomerItemViewModel(CustomerDto customer)
        {
            Customer = customer;

          

            _eventAggregator = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IEventAggregator>();

          
        }
        #endregion

        #region Properties
        private bool _isEditable = false;
        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                if (SetProperty(ref _isEditable, value))
                {
               
                }
            }
        }
        #endregion

        #region Properties
        public CustomerDto Customer { get; set; }

        public int Id => Customer.Id;

        public string DisplayName
        {
            get
            {
                if (Customer.IsCompany)
                {
                    return Customer.FirstName;
                }
                else
                {
                    return $"{Customer.LastName},{Customer.FirstName}";
                }
            }
        }

        public bool IsCompany => Customer.IsCompany;

        public string FirstName => Customer.FirstName;

        public string LastName => Customer.LastName;

        public string Email => Customer.Email;

        public double TotalSales => Customer.TotalSales;

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (SetProperty<bool>(ref _isSelected, value))
                {
                    if (_isSelected)
                    {
                        // _eventAggregator.GetEvent<OnEditableEvent<CustomerItemViewModel>>().Publish(new OnEditableEvent<CustomerItemViewModel> { EditablObject = this });
                    }

                    //(EditCustomerCommand as DelegateCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isChoice = false;
        public bool IsChoice
        {
            get => _isChoice;
            set => SetProperty(ref _isChoice, value);
        }
        #endregion
    }
}
