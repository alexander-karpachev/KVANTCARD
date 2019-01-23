using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public class Address : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private String city;

        public String City
        {
            get { return city; }
            set { city = value; OnPropertyChanged("City"); }
        }

        private String street;

        public String Street
        {
            get { return street; }
            set { street = value; OnPropertyChanged("Street"); }
        }

        private String appartment;

        public String Appartment
        {
            get { return appartment; }
            set { appartment = value; OnPropertyChanged("Appartment"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
