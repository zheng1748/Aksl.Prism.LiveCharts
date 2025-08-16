using System.Windows;

namespace Aksl.Modules.DataGridCustomer.Views
{
    public partial class EditableCustomerWindow : Window
    {
        public EditableCustomerWindow()
        {
            this.InitializeComponent();
        }

        private ViewModels.EditableCustomerViewModel _editableCustomerViewModel;
        public ViewModels.EditableCustomerViewModel ViewModel
        {
            get => _editableCustomerViewModel;
            set
            {
                _editableCustomerViewModel = value;
                DataContext = _editableCustomerViewModel;
            }
        }
    }
}
