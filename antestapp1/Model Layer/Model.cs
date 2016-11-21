using antestapp1.Data_Layer;
using antestapp1.ViewModel_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace antestapp1.Model_Layer
{
    public class AddModel : IAddModel
    {
        private IAddView addView;
        public AddModel(IAddView _addView)
        {
            addView = _addView;
        }

        public void AddContact()
        {
            ContactBookDBDModel db = new ContactBookDBDModel();
            Contacts contact = new Contacts();

            contact.FirstName = addView.newFirstName;
            contact.LastName = addView.newLastName;
            contact.HomePhone = addView.newHomePhone;
            contact.WorkPhone = addView.newWorkPhone;
            contact.MobilePhone = addView.newMobilePhone;
            contact.HomeAddress = addView.newHomeAddress;
            contact.WorkPhone = addView.newWorkAddress;
            contact.OtherAddress = addView.newOtherAddress;

            db.Contacts.Add(contact);
            db.SaveChanges();

            // Update ListView Method
        }
    }

    public interface IAddModel
    {
        void AddContact();
    }
    public class EditModel : IEditModel
    {
        private IDetailsView detailsView;

        public EditModel(IDetailsView _detailsView)
        {
            detailsView = _detailsView;
        }

        public void EditContact()
        {
            try
            {
                ContactBookDBDModel db = new ContactBookDBDModel();

                if (SelectionManager.SelectedContact.ContactID <= 0)
                    return;
                var t = Task<Contacts>.Factory.StartNew(() =>
                {
                    return (from emp in db.Contacts
                            where emp.ContactID == SelectionManager.SelectedContact.ContactID
                            select emp).Single();
                });

                t.Wait();
                t.Result.HomePhone = detailsView.homePhone;
                t.Result.WorkPhone = detailsView.workPhone;
                t.Result.MobilePhone = detailsView.mobilePhone;

                t.Result.HomeAddress = detailsView.homeAddress;
                t.Result.WorkAddress = detailsView.workAddress;
                t.Result.OtherAddress = detailsView.otherAddress;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteContact()
        {
            try
            {
                ContactBookDBDModel db = new ContactBookDBDModel();

                Contacts contact = new Contacts();
                contact.ContactID = SelectionManager.SelectedContact.ContactID;
                var t = Task.Factory.StartNew(() => db.Contacts.Attach(contact));
                t.Wait();
                var t1 = Task.Factory.StartNew(() => db.Contacts.Remove(contact));
                t1.Wait();
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Något gick fel, kunde inte ta bort kontakten. " + ex.Message, "Felmeddelande");
            }
        }
    }

    public interface IEditModel
    {
        void EditContact();
        void DeleteContact();
    }

    public class ListViewModel : IListViewModel
    {
        private IListView listView;
        public ListViewModel(IListView _listView)
        {
            listView = _listView;
        }
        public List<Contacts> SearchResult
        {
            get
            {
                using (var db = new ContactBookDBDModel())
                {
                    try
                    {
                        SqlParameter p = new SqlParameter("@UserInput", listView.searchValue);
                        List<Contacts> result = db.Contacts.SqlQuery("SearchContactsSP @UserInput", p).ToList();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return null;
                    }
                }
            }
        }
    }

    public interface IListViewModel
    {
        List<Contacts> SearchResult { get; }
    }
}

