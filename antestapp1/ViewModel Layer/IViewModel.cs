using antestapp1.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace antestapp1.ViewModel_Layer
{
    interface IViewModel
    {
    }

    public interface IAddView
    {
        string newFirstName { get; }
        string newLastName { get; }
        string newHomePhone { get; }
        string newWorkPhone { get; }
        string newMobilePhone { get; }

        string newHomeAddress { get; }
        string newWorkAddress { get; }
        string newOtherAddress { get; }

        Brush txtFirstNameBorderBrush { get; set; }
        Brush txtLastNameBorderBrush { get; set; }
        Visibility errorLabelVisibility { get; set; }
    }

    public interface IDetailsView
    {
        Contacts SelectedContact { get; set; }
        string homePhone { get; }
        string workPhone { get; }
        string mobilePhone { get; }
        string homeAddress { get; }
        string workAddress { get; }
        string otherAddress { get; }
        object dataContext { get; set; }

    }

    public interface IListView
    {
        string searchValue { get; }
        void UpdateListView();
    }

    public interface IMainView
    {

    }
}
