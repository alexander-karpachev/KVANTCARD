using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.ComponentModel;

namespace KvantCard.Model
{
    public class Contact : INotifyPropertyChanged
    {
        private List<String> email;

        public List<String> Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email");  }
        }

        private List<String> phoneNumber;

        public List<String> PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }

        private List<Address> address;

        public List<Address> Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
