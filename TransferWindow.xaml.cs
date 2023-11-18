using System.Collections.ObjectModel;
using System.Windows;

namespace DepartmentManagementApp
{
    public partial class TransferWindow : Window
    {
        public Department SelectedTarget { get; private set; }

        public TransferWindow(Department company, object selectedItem)
        {
            InitializeComponent();

            TreeViewTransfer.ItemsSource = new ObservableCollection<Department> { company };

        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void TreeViewTransfer_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is Department department)
            {
                SelectedTarget = department;
            }
        }
    }
}