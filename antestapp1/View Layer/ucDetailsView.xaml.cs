using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using antestapp1.Data_Layer;
using antestapp1.ViewModel_Layer;
using antestapp1.Model_Layer;

namespace antestapp1.View_Layer
{
    /// <summary>
    /// Interaction logic for ucDetailsView.xaml
    /// </summary>
    public partial class ucDetailsView : UserControl, IDetailsView
    {
        public Contacts SelectedContact { get; set; }

        public string homePhone
        {
            get
            {
                return txtHomePhone.Text;
            }
        }

        public string workPhone
        {
            get
            {
                return txtWorkPhone.Text;
            }
        }

        public string mobilePhone
        {
            get
            {
                return txtMobilePhone.Text;
            }
        }

        public string homeAddress
        {
            get
            {
                return txtHomeAddress.Text;
            }
        }

        public string workAddress
        {
            get
            {
                return txtWorkPhone.Text;
            }
        }

        public string otherAddress
        {
            get
            {
                return txtOtherAddress.Text;
            }
        }

        public object dataContext
        {
            get
            {
                return this.DataContext;
            }

            set
            {
                this.DataContext = value;
            }
        }
        private IEditModel m;

        public ucDetailsView()
        {
            InitializeComponent();
            ucListView.DisplayContactDetailsArgs += new EventHandler(LoadEntityData);
            m = new EditModel(this);

        }
        protected void LoadEntityData(object sender, EventArgs e)
        {
            this.dataContext = SelectionManager.SelectedContact;
        }
        private void btnConfirmEdit_Click(object sender, RoutedEventArgs e)
        {
            ChangeBrushes(Brushes.Transparent, Brushes.Transparent, Visibility.Hidden, false);
            btnEdit.Visibility = Visibility.Visible;
            m.EditContact();
        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            ChangeBrushes(Brushes.Transparent, Brushes.Transparent, Visibility.Hidden, false);
            btnEdit.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ChangeBrushes(Brushes.WhiteSmoke, Brushes.Gray, Visibility.Visible, true);
            btnEdit.Visibility = Visibility.Hidden;
        }

        public void ChangeBrushes(Brush txtBrush, Brush borderBrush, Visibility visible, bool _boolValue)
        {
            btnConfirmEdit.Visibility = visible;
            btnCancelEdit.Visibility = visible;

            txtFullName.Focusable = _boolValue;
            txtFullName.Background = txtBrush;
            txtFullName.BorderBrush = borderBrush;

            txtMobilePhone.Focusable = _boolValue;
            txtMobilePhone.Background = txtBrush;
            txtMobilePhone.BorderBrush = borderBrush;

            txtWorkPhone.Focusable = _boolValue;
            txtWorkPhone.Background = txtBrush;
            txtWorkPhone.BorderBrush = borderBrush;

            txtHomePhone.Focusable = _boolValue;
            txtHomePhone.Background = txtBrush;
            txtHomePhone.BorderBrush = borderBrush;

            txtHomeAddress.Focusable = _boolValue;
            txtHomeAddress.Background = txtBrush;
            txtHomeAddress.BorderBrush = borderBrush;

            txtWorkAddress.Focusable = _boolValue;
            txtWorkAddress.Background = txtBrush;
            txtWorkAddress.BorderBrush = borderBrush;

            txtOtherAddress.Focusable = _boolValue;
            txtOtherAddress.Background = txtBrush;
            txtOtherAddress.BorderBrush = borderBrush;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Är du säker på att ta bort kontakten?", "Varning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                m.DeleteContact();
                UpdateListView();
            }
            else
            {
                return;
            }
        }
        public static event EventHandler UpdateListViewArgs;

        protected virtual void UpdateListView()
        {
            UpdateListViewArgs?.Invoke(this, EventArgs.Empty);
        }
    }
}
