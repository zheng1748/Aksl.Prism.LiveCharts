using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;

using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Ioc;
using Prism.Unity;

using Unity;
using AutoMapper;

using Aksl.Modules.ListViewCustomer.Models;

namespace Aksl.Modules.ListViewCustomer.ViewModels
{
    public class EditableCustomerViewModel : BindableBase
    {
        #region Members
        private IMapper _mapper;
        #endregion

        #region Constructors
        public EditableCustomerViewModel(CustomerDto customerDto)
        {
            InitializeEditableCustomerViewModel(customerDto);
        }

        //public EditableCustomerViewModel(CustomerItemViewModel customerItemViewModel ) 
        //{
        //    EditingCustomerItemViewModel = customerItemViewModel;
           
        //    InitializeEditableCustomerViewModel(customerItemViewModel.Customer);
        //}

        private void InitializeEditableCustomerViewModel(CustomerDto customerDto)
        {
            var serviceProvider = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IServiceProvider>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

            Customer = customerDto;

            CreateSaveCommand();

            EditableCustomerItem = _mapper.Map<EditableCustomerItemViewModel>(Customer);
            if (!IsNew)
            {
                EditableCustomerItem.CustomerType = EditableCustomerItem.IsCompany ? "Company" : "Person";
            }
            RegisterPropertyChanged();
        }

        private void RegisterPropertyChanged()
        {
            EditableCustomerItem.PropertyChanged += (sender, e) =>
            {
                if (sender is EditableCustomerItemViewModel ecivm)
                {
                    (SaveCommand as DelegateCommand)?.RaiseCanExecuteChanged();
                }
            };
        }
        #endregion

        #region Event Handler
        public event EventHandler RequestSave;

        public event EventHandler RequestClose;
        #endregion

        #region Editable Properties
        // public CustomerItemViewModel EditingCustomerItemViewModel { get; private set; }
    
        private EditableCustomerItemViewModel _editableCustomerItemViewModel = null;
        public EditableCustomerItemViewModel EditableCustomerItem
        {
            get => _editableCustomerItemViewModel;
            set => SetProperty(ref _editableCustomerItemViewModel, value);
        }
        #endregion

        #region Properties
        public CustomerDto Customer { get; private set; }

        public bool IsNew => Customer.Id == 0;

        public string DisplayName
        {
            get
            {
                if (this.IsNew)
                {
                    return "New Customer";
                }
                else if (Customer.IsCompany)
                {
                    //return Customer.FirstName;
                    return $"Edit:{Customer.FirstName}";
                }
                else
                {
                    //return $"{Customer.LastName}, {Customer.FirstName}";
                    return $"Edit:{Customer.LastName} {Customer.FirstName}";
                }
            }
        }

        public double TotalSales => Customer.TotalSales;

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty<bool>(ref _isLoading, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }
        #endregion

        #region Save Command
        public ICommand SaveCommand { get; private set; }

        private void CreateSaveCommand()
        {
            SaveCommand = new DelegateCommand(() =>
            {
                ExecuteSaveCommand();
            },
            () =>
            {
                var canExecute = CanExecuteSaveCommand();
                return canExecute;
            });
        }

        private void ExecuteSaveCommand()
        {
            IsLoading = true;

            string errorMessage = null;
            StatusMessage = "开始保存...";

            try
            {
                //if (IsNew)
                //{
                //    //var customerDto = _mapper.Map<CustomerDto>(EditableCustomerItem);
                //    //EditingCustomerItemViewModel = new CustomerItemViewModel(customerDto);
                //}
                //else
                //{
                //    //var customerDto = _mapper.Map<CustomerDto>(EditableCustomerItem);
                //    //EditingCustomerItemViewModel.Customer = customerDto;
                //}

                RequestSave?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                //result = Result.Error(ex);
                errorMessage = ex.Message;
            }
            finally
            {
                StatusMessage = string.IsNullOrEmpty(errorMessage) ? "保存完毕..." : errorMessage;

                IsLoading = false;
            }
        }

        private bool CanExecuteSaveCommand()
        {
            return !IsLoading && !EditableCustomerItem.HasErrors/* && EditableCustomerItem.ValidateResult*/;
        }
        #endregion

        #region Cancel Command
        public ICommand CancelCommand { get; private set; }

        protected virtual void CreateCancelCommand()
        {
            CancelCommand = new DelegateCommand(() =>
            {
                ExecuteCancelCommand();
            },
            () =>
            {
                var canExecute = CanExecutCancelCommand();
                return canExecute;
            });
        }

        protected virtual void ExecuteCancelCommand()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        protected virtual bool CanExecutCancelCommand()
        {
            return true;
        }
        #endregion
    }
}
