using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public abstract class _Human : INotifyPropertyChanged
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value;}
        }

        protected String firstName;

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }

        protected String lastName;

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }

        protected String middleName;

        public String MiddleName
        {
            get { return middleName; }
            set { middleName = value; OnPropertyChanged("MiddleName"); }
        }

        private DateTime birthDate;

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; OnPropertyChanged("BirthDate"); }
        }

        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyChanged("Contact"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
