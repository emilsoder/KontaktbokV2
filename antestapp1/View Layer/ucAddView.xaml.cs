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
using antestapp1.Model_Layer;

namespace antestapp1
{
    /// <summary>
    /// Interaction logic for ucAddView.xaml
    /// </summary>
    public partial class ucAddView : UserControl, IAddView
    {
        #region Interface Implementation
        public string newHomePhone
        {
            get
            {
                return txtHomePhoneAdd.Text;
            }
        }

        public string newWorkPhone
        {
            get
            {
                return txtWorkPhoneAdd.Text;
            }
        }

        public string newMobilePhone
        {
            get
            {
                return txtMobilePhoneAdd.Text;
            }
        }

        public string newHomeAddress
        {
            get
            {
                return txtHomeAddressAdd.Text;
            }
        }

        public string newWorkAddress
        {
            get
            {
                return txtWorkAddressAdd.Text;
            }
        }

        public string newOtherAddress
        {
            get
            {
                return txtOtherAddressAdd.Text;
            }
        }

        public string newFirstName
        {
            get
            {
                return txtFirstNameAdd.Text;
            }
        }

        public string newLastName
        {
            get
            {
                return txtLastNameAdd.Text;
            }
        }
        public Brush txtFirstNameBorderBrush
        {
            get
            {
                return txtFirstNameAdd.BorderBrush;
            }
            set
            {
                txtFirstNameAdd.BorderBrush = value;
                if (value == Brushes.Red)
                {
                    txtFirstNameAdd.BorderThickness = new Thickness(4);
                    errorLabelVisibility = Visibility.Visible;
                }
                else if (value == Brushes.Green)
                {
                    errorLabelVisibility = Visibility.Hidden;
                }
                else
                {
                    txtFirstNameAdd.BorderThickness = new Thickness(1);
                    errorLabelVisibility = Visibility.Hidden;
                }
            }
        }
        public Brush txtLastNameBorderBrush
        {
            get
            {
                return txtLastNameAdd.BorderBrush;
            }
            set
            {
                txtLastNameAdd.BorderBrush = value;
                if (value == Brushes.Red)
                {
                    txtLastNameAdd.BorderThickness = new Thickness(4);
                    errorLabelVisibility = Visibility.Visible;
                }
                else if (value == Brushes.Green)
                {
                    errorLabelVisibility = Visibility.Hidden;
                }
                else
                {
                    txtLastNameAdd.BorderThickness = new Thickness(1);
                    errorLabelVisibility = Visibility.Hidden;
                }
            }
        }
        public Visibility errorLabelVisibility
        {
            get
            {
                return ErrorLabel.Visibility;
            }
            set
            {
                ErrorLabel.Visibility = value;
            }
        }
        #endregion

        private IAddModel addModel;

        public ucAddView()
        {
            InitializeComponent();
            addModel = new AddModel(this);
        }

        private void txtLastNameAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((newLastName != "" && newFirstName != "") || (newLastName != "" && newFirstName == ""))
            {
                txtLastNameBorderBrush = Brushes.Green;
                txtFirstNameBorderBrush = Brushes.LightGray;
            }
            else
            {
                txtLastNameBorderBrush = Brushes.Red;
                txtFirstNameBorderBrush = Brushes.Red;
            }
        }

        private void txtFirstNameAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((newFirstName != "" && newLastName != "") || (newFirstName != "" && newLastName == ""))
            {
                txtFirstNameBorderBrush = Brushes.Green;
                txtLastNameBorderBrush = Brushes.LightGray;
            }
            else
            {
                txtLastNameBorderBrush = Brushes.Red;
                txtFirstNameBorderBrush = Brushes.Red;
            }
        }

        private void btnConfirmAdd_Click(object sender, RoutedEventArgs e)
        {
            if (newFirstName != "".Trim() || newLastName != "".Trim())
            {
                txtLastNameBorderBrush = Brushes.LightGray;
                txtFirstNameBorderBrush = Brushes.LightGray;

                HideAddView();
                addModel.AddContact();
                UpdateListView();
                ClearTextBoxes();
            }
            else
            {
                txtFirstNameBorderBrush = Brushes.Red;
                txtLastNameBorderBrush = Brushes.Red;
            }
        }

        private void btnCancelAdd_Click(object sender, RoutedEventArgs e)
        {
            HideAddView();
            ClearTextBoxes();
        }

        public static event EventHandler UpdateListViewArgs;

        protected virtual void UpdateListView()
        {
            UpdateListViewArgs?.Invoke(this, EventArgs.Empty);
        }

        public static event EventHandler HideAddViewArgs;

        protected virtual void HideAddView()
        {
            HideAddViewArgs?.Invoke(this, EventArgs.Empty);
        }

        public void ClearTextBoxes()
        {
            txtFirstNameAdd.Text = "";
            txtLastNameAdd.Text = "";

            txtHomePhoneAdd.Text = "";
            txtWorkPhoneAdd.Text = "";
            txtMobilePhoneAdd.Text = "";

            txtHomeAddressAdd.Text = "";
            txtWorkAddressAdd.Text = "";
            txtOtherAddressAdd.Text = "";

            txtFirstNameBorderBrush = Brushes.LightGray;
            txtLastNameBorderBrush = Brushes.LightGray;
        }
    }
}
