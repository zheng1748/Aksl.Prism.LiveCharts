using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Ioc;
using Prism.Unity;

using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using Aksl.Modules.ListViewCustomer.Services;
using Aksl.Modules.ListViewCustomer.Models;

namespace Aksl.Modules.ListViewCustomer.ViewModels
{
    public class CustomerListViewModel : BindableBase
    {
        #region Members
        private ICustomerService _customerService;
        #endregion

        #region Constructors
        public CustomerListViewModel(ICustomerService customerService)
        {
            _customerService = customerService;

            AllCustomers = new ObservableCollection<CustomerItemViewModel>();

            CreateCompanyFilterCommand();
            CreateUnGroupCommand();
            CreateGroupCommand();
        }
        #endregion

        #region Properties
        public string Title => $"Customers:({AllCustomers.Count})";

        public ObservableCollection<CustomerItemViewModel> AllCustomers { get; private set; }

        private CustomerItemViewModel _selectedCustomerItem;
        public CustomerItemViewModel SelectedCustomerItem
        {
            get => _selectedCustomerItem;
            set
            {
                if (SetProperty(ref _selectedCustomerItem, value))
                {
                    if (_selectedCustomerItem != null)
                    {
                        _selectedCustomerItem.IsSelected = true;
                        //  OnPropertyChanged(() => TotalSelectedSales);
                        RaisePropertyChanged(nameof(TotalSelectedSales));
                    }
                }
            }
        }

        // public double TotalSelectedSales => this.AllCustomers.Sum(custVM => custVM.IsSelected ? custVM.TotalSales : 0.0);
        public double TotalSelectedSales
        {
            get
            {
                if (_selectedCustomerItem != null)
                {
                    return _selectedCustomerItem.TotalSales;
                }
                return 0.0d;
            }
        }

        private bool _isCompanyFilterChecked = false;
        public bool IsCompanyFilterChecked
        {
            get => _isCompanyFilterChecked;
            set => SetProperty(ref _isCompanyFilterChecked, value);
        }

        private bool _isChoice = false;
        public bool IsChoice
        {
            get => _isChoice;
            set
            {
                if (SetProperty(ref _isChoice, value))
                {
                    foreach (var customer in AllCustomers)
                    {
                        customer.IsChoice = _isChoice;
                    }
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

        #region Create CustomerItem ViewModel Method
        internal async Task CreateCustomerItemViewModelsAsync()
        {
            IsLoading = true;

            var customers = await _customerService.GetAllAsync();
            foreach (var customer in customers)
            {
                CustomerItemViewModel customerItemViewModel = new (customer);

                AddCustomerItemViewModel(customerItemViewModel);
                //AddPropertyChanged();

                //void AddPropertyChanged()
                //{
                //    customerItemViewModel.PropertyChanged += (sender, e) =>
                //    {
                //        if (sender is CustomerItemViewModel civm)
                //        {
                //            if (e.PropertyName == nameof(CustomerItemViewModel.IsChoice))
                //            {
                //                if (civm.IsChoice)
                //                {
                //                    SelectedCustomerItem = civm;

                //                    if (!IsChoice && AllCustomers.All(c => c.IsChoice))
                //                    {
                //                        IsChoice = true;
                //                    }
                //                }
                //                else
                //                {
                //                    //SelectedCustomerItem = null;

                //                    if (IsChoice && !AllCustomers.Any(c => c.IsChoice))
                //                    {
                //                        IsChoice = false;
                //                    }
                //                }
                //            }
                //        }
                //    };
                //}

                //  AllCustomers.Add(customerItemViewModel);
            }

            // SetActiveWorkspace(this);

            RaisePropertyChanged(nameof(AllCustomers));

            IsLoading = false;
        }

        internal void AddCustomerItemViewModel(CustomerItemViewModel customerItemViewModel)
        {
            AddPropertyChanged();

            void AddPropertyChanged()
            {
                customerItemViewModel.PropertyChanged += (sender, e) =>
                {
                    if (sender is CustomerItemViewModel civm)
                    {
                        if (e.PropertyName == nameof(CustomerItemViewModel.IsChoice))
                        {
                            if (civm.IsChoice)
                            {
                                SelectedCustomerItem = civm;

                                if (!IsChoice && AllCustomers.All(c => c.IsChoice))
                                {
                                    IsChoice = true;
                                }
                            }
                            else
                            {
                                //SelectedCustomerItem = null;

                                if (IsChoice && !AllCustomers.Any(c => c.IsChoice))
                                {
                                    IsChoice = false;
                                }
                            }
                        }
                    }
                };
            }

            AllCustomers.Add(customerItemViewModel);
        }

        //internal void SyncAfetrAddCustomerItemViewModel(CustomerItemViewModel editableCustomerItemViewModel)
        //{
        //    IsLoading = true;

        //    AddCustomerItemViewModel(editableCustomerItemViewModel);

        //    RaisePropertyChanged(nameof(AllCustomers));

        //    IsLoading = false;
        //}

        internal void SyncAfetrAddCustomerItemViewModel(CustomerDto newCustomerDto)
        {
            var customerItemViewModel = new CustomerItemViewModel(newCustomerDto);

            AddCustomerItemViewModel(customerItemViewModel);

            RaisePropertyChanged(nameof(AllCustomers));
        }

        internal void SyncAfetrEditCustomerItemViewModel(CustomerDto editableCustomerDto)
        {
            //if (editableCustomerItemViewModel == SelectedCustomerItem)
            //{
            //IServiceProvider serviceProvider = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IServiceProvider>();
            //var mapper = serviceProvider.GetRequiredService<IMapper>();

            // var currentCustomerItemViewModel = AllCustomers.FirstOrDefault(c => c.Id == editableCustomerDto.Id);
            //var customerDto = mapper.Map<CustomerDto>(editableCustomerItemViewModel.Customer);
            //currentCustomerItemViewModel.Customer.Email = editableCustomerItemViewModel.Customer.Email;
            //AllCustomers.Remove(currentCustomerItemViewModel);
            var selectedCustomerItem = SelectedCustomerItem;
            selectedCustomerItem.Customer = editableCustomerDto;
            //AllCustomers.Add(currentCustomerItemViewModel);
            //  currentCustomerItemViewModel.RegisterPropertyChanged();

            _selectedCustomerItem.IsSelected = false;
            SelectedCustomerItem = null;
            // SelectedCustomerItem.Customer = editableCustomerItemViewModel.Customer;
            SelectedCustomerItem = selectedCustomerItem;
            //RaisePropertyChanged(nameof(SelectedCustomerItem));
            //RaisePropertyChanged(nameof(AllCustomers));

            ICollectionView customeCollectionView = CollectionViewSource.GetDefaultView(AllCustomers);
            if (customeCollectionView != null)
            {
                customeCollectionView.Refresh();
            }
        }

        //internal void SyncAfetrEditCustomerItemViewModel(CustomerItemViewModel editableCustomerItemViewModel)
        //{
        //    //if (editableCustomerItemViewModel == SelectedCustomerItem)
        //    //{
        //    //IServiceProvider serviceProvider = (PrismApplication.Current as PrismApplicationBase).Container.Resolve<IServiceProvider>();
        //    //var mapper = serviceProvider.GetRequiredService<IMapper>();

        //    //var currentCustomerItemViewModel = AllCustomers.FirstOrDefault(c => c.Id == editableCustomerItemViewModel.Id);
        //    //var customerDto = mapper.Map<CustomerDto>(editableCustomerItemViewModel.Customer);
        //    //currentCustomerItemViewModel.Customer.Email = editableCustomerItemViewModel.Customer.Email;
        //    //AllCustomers.Remove(currentCustomerItemViewModel);

        //    SelectedCustomerItem.Customer = editableCustomerItemViewModel.Customer;
        //    //AllCustomers.Add(currentCustomerItemViewModel);
        //    //  currentCustomerItemViewModel.RegisterPropertyChanged();

        //    _selectedCustomerItem.IsSelected = false;
        //    SelectedCustomerItem = null;
        //    // SelectedCustomerItem.Customer = editableCustomerItemViewModel.Customer;
        //    SelectedCustomerItem = editableCustomerItemViewModel;
        //    //RaisePropertyChanged(nameof(SelectedCustomerItem));
        //    //RaisePropertyChanged(nameof(AllCustomers));

        //    ICollectionView customeCollectionView = CollectionViewSource.GetDefaultView(AllCustomers);
        //    if (customeCollectionView != null)
        //    {
        //        customeCollectionView.Refresh();
        //    }
        //}
        #endregion

        #region ListView Event Methods
        //public void ListViewLoaded(object sender, RoutedEventArgs e)
        //{
        //    var itemsSource = (sender as ListView).ItemsSource;
        //    var customeCollectionView = CollectionViewSource.GetDefaultView(itemsSource);
        //    if (customeCollectionView != null)
        //    {
        //        customeCollectionView.Filter += (e) =>
        //        {
        //            if ((e is CustomerItemViewModel civm) && IsChecked)
        //            {
        //                return civm.IsCompany;
        //            }

        //            return true;
        //        };
        //    }
        //}
        #endregion

        #region  CheckBox Event Methods
        public ICommand CompanyFilterCommand { get; private set; }

        private void CreateCompanyFilterCommand()
        {
            CompanyFilterCommand = new DelegateCommand<IEnumerable>((items) =>
            {
                ExecuteCompanyFilterCommand(items);
            },
            (items) =>
            {
                return true;
            });
        }

        private void ExecuteCompanyFilterCommand(IEnumerable itemsSource)
        {
            ICollectionView customeCollectionView = CollectionViewSource.GetDefaultView(AllCustomers);
            if (customeCollectionView != null)
            {
                customeCollectionView.Filter += (e) =>
                {
                    if ((e is CustomerItemViewModel civm) && IsCompanyFilterChecked)
                    {
                        return civm.IsCompany;
                    }

                    return true;
                };

                customeCollectionView.Refresh();
            }
        }
        #endregion

        #region Group Command Method
        public ICommand GroupCommand { get; private set; }

        private void CreateGroupCommand()
        {
            GroupCommand = new DelegateCommand<IEnumerable>((items) =>
            {
                ExecuteGroupCommand(items);
            },
            (items) =>
            {
                return true;
            });
        }

        private void ExecuteGroupCommand(IEnumerable itemsSource)
        {
            ICollectionView customeCollectionView = CollectionViewSource.GetDefaultView(AllCustomers);
            if (customeCollectionView != null && customeCollectionView.CanGroup == true)
            {
                customeCollectionView.GroupDescriptions.Clear();
                customeCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("IsCompany"));

                customeCollectionView.MoveCurrentTo(this);
            }
        }

        public ICommand UnGroupCommand { get; private set; }

        private void CreateUnGroupCommand()
        {
            UnGroupCommand = new DelegateCommand<IEnumerable>((items) =>
            {
                ExecuteUnGroupCommand(items);
            },
            (items) =>
            {
                return true;
            });
        }

        private void ExecuteUnGroupCommand(IEnumerable itemsSource)
        {
            ICollectionView customeCollectionView = CollectionViewSource.GetDefaultView(AllCustomers);
            if (customeCollectionView != null)
            {
                customeCollectionView.GroupDescriptions.Clear();
            }
        }
        #endregion

        #region Active Workspac Method
        private void SetActiveWorkspace(CustomerListViewModel customerListViewModel)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.AllCustomers);
            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(customerListViewModel);
            }
        }
        #endregion
    }
}
