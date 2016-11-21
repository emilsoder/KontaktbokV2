using antestapp1.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antestapp1.ViewModel_Layer
{
    public class ViewModel
    {
        public IList<Contacts> _contacts;

        public ViewModel()
        {
            GetData();
        }

        public IEnumerable<Contacts> GetContacts()
        {
            return _contacts;
        }

        private void GetData()
        {
            var db = new ContactBookDBDModel();
            var result = from c in db.Contacts
                         select c;

            List<Contacts> lista = result.ToList();

            _contacts = lista;
        }
    }
}
