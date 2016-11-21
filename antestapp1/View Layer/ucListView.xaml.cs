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
using antestapp1.ViewModel_Layer;
using antestapp1.Data_Layer;
using antestapp1.Model_Layer;
using System.Data.SqlClient;

namespace antestapp1.View_Layer
{
    /// <summary>
    /// Interaction logic for ucListView.xaml
    /// </summary>
    public partial class ucListView : UserControl, IListView
    {
        public static event EventHandler ShowAddViewArgs;
        public static event EventHandler DisplayContactDetailsArgs;
        public static event EventHandler ShowDetailViewFirstTimeArgs;
        public string searchValue
        {
            get
            {
                return this.txtSearch.Text;
            }
        }
        private IListViewModel m;

        public ucListView()
        {
            InitializeComponent();
            m = new ListViewModel(this);
            UpdateListView();
            ucAddView.UpdateListViewArgs += new EventHandler(UpdateListViewByAdd);
            ucDetailsView.UpdateListViewArgs += new EventHandler(UpdateListViewByAdd);

        }

        public bool IsFirstTime = true;
        private void GotoDetails(object sender, MouseButtonEventArgs e)
        {
            if (IsFirstTime == true)
            {
                ShowThisView();
                IsFirstTime = false;
            }
            var element = sender as FrameworkElement;
            var contact = element.DataContext as Contacts;

            if (contact == null)
                return;

            SelectionManager.SelectedContact = contact;
            DisplayContactDetails();
        }
        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            ShowAddView();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "".Trim())
            {
                try
                {
                    this.lbContacts.ItemsSource = m.SearchResult;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                UpdateListView();
        }

        public void UpdateListView()
        {
            var p = new ViewModel();
            this.lbContacts.ItemsSource = p.GetContacts();
        }

        protected void UpdateListViewByAdd(object sender, EventArgs e)
        {
            UpdateListView();
        }

        protected virtual void ShowAddView()
        {
            ShowAddViewArgs?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void DisplayContactDetails()
        {
            DisplayContactDetailsArgs?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void ShowThisView()
        {
            ShowDetailViewFirstTimeArgs?.Invoke(this, EventArgs.Empty);
        }
    }
}
