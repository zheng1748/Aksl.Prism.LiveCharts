using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;

using Unity;

using Aksl.Modules.ListViewCustomer.Views;
using Aksl.Modules.ListViewCustomer.ViewModels;
using Aksl.Modules.ListViewCustomer.Services;

namespace Aksl.Modules.ListViewCustomer
{
    public class ListViewCustomerModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public ListViewCustomerModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule 成员
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICustomerService, CustomerService>();

            containerRegistry.RegisterForNavigation<CustomerHubListView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ViewModelLocationProvider.Register(typeof(CustomerHubListView).ToString(),
                                              () => this._container.Resolve<CustomerHubListViewModel>());
        }
        #endregion
    }
}
