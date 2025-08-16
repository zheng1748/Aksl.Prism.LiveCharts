using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;

using Unity;

using Aksl.Modules.DataGridCustomer.Views;
using Aksl.Modules.DataGridCustomer.ViewModels;
using Aksl.Modules.DataGridCustomer.Services;

namespace Aksl.Modules.DataGridCustomer
{
    public class DataGridCustomerModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public DataGridCustomerModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICustomerService, CustomerService>();
 
            containerRegistry.RegisterForNavigation<CustomerHubDataGridView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(CustomerHubDataGridView).ToString(),
                                         () => this._container.Resolve<CustomerHubDataGridViewModel>());
        }
        #endregion
    }
}
