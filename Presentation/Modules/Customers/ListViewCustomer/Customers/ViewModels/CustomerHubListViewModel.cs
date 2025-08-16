using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using Prism.Unity;
using Prism.Regions;
using Unity;

using Microsoft.Extensions.DependencyInjection;

using AutoMapper;

using Aksl.Toolkit.Services;

using Aksl.Modules.ListViewCustomer.Services;
using Aksl.Modules.ListViewCustomer.Models;

namespace Aksl.Modules.ListViewCustomer.ViewModels
{
    public class CustomerHubListViewModel : BindableBase, INavigationAware
    {
        #region Members
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogViewService _dialogViewService;

        private ICustomerService _customerService;
        #endregion

        #region Constructors
        public CustomerHubListViewModel(IUnityContainer container, IEventAggregator eventAggregator, IDialogViewService dialogViewService, ICustomerService customerService)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _dialogViewService = dialogViewService;
            _customerService = customerService;

            CreateCustomerListViewModelAsync().GetAwaiter().GetResult();

            //CreateNewCustomerCommand();
           CreateEditCustomerCommand();
            CreateDeleteCustomerCommand();
        }
        #endregion

        #region Properties
        public CustomerListViewModel CustomerList { get; private set; }

        private bool _isCompanyFilterChecked = false;
        public bool IsCompanyFilterChecked
        {
            get => _isCompanyFilterChecked;
            set
            {
                if (SetProperty(ref _isCompanyFilterChecked, value))
                {
                    CustomerList.IsCompanyFilterChecked= _isCompanyFilterChecked;
                }
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty<bool>(ref _isLoading, value);
        }

        #endregion

        #region Create CustomerListViewModel Method
        internal async Task CreateCustomerListViewModelAsync()
        {
            IsLoading = true;
            string errorMessage = string.Empty;

            try
            {
                CustomerList = new CustomerListViewModel(_customerService);
                AddPropertyChanged();
                await CustomerList.CreateCustomerItemViewModelsAsync();

                void AddPropertyChanged()
                {
                    CustomerList.PropertyChanged += (sender, e) =>
                    {
                        if (sender is CustomerListViewModel customersViewModel)
                        {
                            if (!customersViewModel.IsLoading)
                            {
                                IsLoading = false;
                            }

                            if (e.PropertyName == nameof(CustomerListViewModel.SelectedCustomerItem))
                            {
                                (EditCustomerCommand as DelegateCommand).RaiseCanExecuteChanged();
                                (DeleteCustomerCommand as DelegateCommand).RaiseCanExecuteChanged();
                            }
                        }
                    };
                }

                //RaisePropertyChanged(nameof(CustomerList));

                CompanyFilterCommand = CustomerList.CompanyFilterCommand;
                GroupCommand = CustomerList.GroupCommand;
                UnGroupCommand = CustomerList.UnGroupCommand;
            }
            catch (Exception ex)
            {
                errorMessage = $"The following error messages were {nameof(CustomerHubListViewModel)} : { Environment.NewLine}{ex.Message}";
            }
            finally
            {
                if (IsLoading)
                {
                    IsLoading = false;
                }
            }

            await _dialogViewService.AlertWhenAsync(errorMessage, "Error Messages:");
        }
        #endregion

        #region New Customer Command
        //public ICommand NewCustomerCommand { get; private set; }

        public ICommand NewCustomerCommand => CreateNewCustomerCommand();

        private ICommand CreateNewCustomerCommand()
        {
           var newCustomerCommand = new DelegateCommand(async  () =>
            {
                EditableCustomerViewModel editableCustomerViewModel = new EditableCustomerViewModel(new Models.CustomerDto());
                await ExecuteEditCustomer(editableCustomerViewModel);
                //await ExecuteNewCustomerCommand();
            },
            () =>
            {
                return true;
                //var canExecute = CanExecuteNewCustomerCommand();
                //return canExecute;
            });

            return newCustomerCommand;
        }

        //private async Task ExecuteNewCustomerCommand()
        //{
        //    EditableCustomerViewModel editableCustomerViewModel = new EditableCustomerViewModel(new Models.CustomerDto());
        //    await ExecuteEditCustomer(editableCustomerViewModel);    
        //}

        //private bool CanExecuteNewCustomerCommand()
        //{
        //    return true;
        //}
        #endregion

        #region Edit Customer Command
        public ICommand EditCustomerCommand { get; private set; }
        //public ICommand EditCustomerCommand => CreateEditCustomerCommand();

        //private ICommand CreateEditCustomerCommand()
        //{
        //    var editCustomerCommand = new DelegateCommand( async() =>
        //    {
        //        EditableCustomerViewModel editableCustomerViewModel = new EditableCustomerViewModel(CustomerList.SelectedCustomerItem.Customer);
        //        await ExecuteEditCustomer(editableCustomerViewModel);
        //        // await ExecuteEditCustomerCommand();
        //    },
        //    () =>
        //    {
        //        var canExecute = CustomerList.SelectedCustomerItem != null;
        //        return canExecute;
        //    });

        //    return editCustomerCommand;
        //}

        private void CreateEditCustomerCommand()
        {
            EditCustomerCommand = new DelegateCommand(async () =>
            {
                await ExecuteEditCustomerCommandAsync();
            },
            () =>
            {
                var canExecute = CustomerList.SelectedCustomerItem != null;
                return canExecute;
            });
        }

        private async Task ExecuteEditCustomerCommandAsync()
        {
            //IServiceProvider serviceProvider = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IServiceProvider>();
            //var mapper = serviceProvider.GetRequiredService<IMapper>();

            //var customerDto = mapper.Map<CustomerDto>(CustomerList.SelectedCustomerItem.Customer);
            //EditableCustomerViewModel editableCustomerViewModel = new EditableCustomerViewModel(new CustomerItemViewModel(customerDto));

            //EditableCustomerViewModel editableCustomerViewModel = new EditableCustomerViewModel(new CustomerItemViewModel(CustomerList.SelectedCustomerItem.Customer));
            //await ExecuteEditCustomer(editableCustomerViewModel);

            EditableCustomerViewModel editableCustomerViewModel = new EditableCustomerViewModel(CustomerList.SelectedCustomerItem.Customer);
            await ExecuteEditCustomer(editableCustomerViewModel);
        }

        private bool CanExecuteEditCustomerCommand()
        {
            return CustomerList.SelectedCustomerItem != null;
        }
        #endregion

        #region Add/Edit Customer Method
        private async Task ExecuteEditCustomer(EditableCustomerViewModel editableCustomerViewModel)
        {
            //IsLoading = true;
            string errorMessage = string.Empty;

            try
            {
                IServiceProvider serviceProvider = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IServiceProvider>();
                var mapper = serviceProvider.GetRequiredService<IMapper>();

                Views.EditableCustomerWindow editableCustomerWindow = new Views.EditableCustomerWindow();
                editableCustomerWindow.ViewModel = editableCustomerViewModel;

                editableCustomerViewModel.RequestSave += async (sender, e) =>
                {
                    var editableCustomerDto = mapper.Map<CustomerDto>(editableCustomerViewModel.EditableCustomerItem);

                    if (editableCustomerViewModel.IsNew)
                    {
                        editableCustomerViewModel.StatusMessage = "数据保存中...";

                        var customerId=await _customerService.AddAsync(editableCustomerDto);
                        editableCustomerDto.Id = customerId;
                        CustomerList.SyncAfetrAddCustomerItemViewModel(editableCustomerDto);

                        editableCustomerViewModel.StatusMessage = "数据保存成功...";
                    }
                    else
                    {
                        editableCustomerViewModel.StatusMessage = "数据更新中...";

                        var updateResult = await _customerService.UpdateAsync(editableCustomerDto);
                        if (updateResult)
                        {
                            CustomerList.SyncAfetrEditCustomerItemViewModel(editableCustomerDto);
                            editableCustomerViewModel.StatusMessage = "数据更新成功...";
                        }
                        else
                        {
                            editableCustomerViewModel.StatusMessage = "数据更新失败...";
                        }
                    }

                    editableCustomerWindow.Close();
                };

                editableCustomerWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                errorMessage = $"The following error messages were {nameof(CustomerHubListViewModel)} : { Environment.NewLine}{ex.Message}";
            }
            //finally
            //{
            //    if (IsLoading)
            //    {
            //        IsLoading = false;
            //    }
            //}

            await _dialogViewService.AlertWhenAsync(errorMessage, "Error Messages:");
        }
        #endregion

        #region Delete Customer Command
        public ICommand DeleteCustomerCommand { get; private set; }

        private void CreateDeleteCustomerCommand()
        {
            DeleteCustomerCommand = new DelegateCommand(() =>
            {
                ExecuteDeleteCustomerCommand();
            },
             () =>
             {
                 var canExecute = CustomerList.SelectedCustomerItem != null;
                 return canExecute;
             });
        }

        private void ExecuteDeleteCustomerCommand()
        {

        }

        private bool CanExecuteDeleteCustomerCommand()
        {
            return CustomerList.SelectedCustomerItem!=null;
        }
        #endregion

        #region  CheckBox Event Methods
        public ICommand CompanyFilterCommand { get; private set; }
        #endregion

        #region Group Commands
        public ICommand UnGroupCommand { get; private set; }

        public ICommand GroupCommand { get; private set; }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var parameters = navigationContext.Parameters;

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion
    }
}
