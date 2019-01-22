using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public class DictionaryItem : INotifyPropertyChanged
    {
        private int id;
        //// test
        public int ID
        {
            get { return id; }
            set { id = value;}
        }

        private String title;

        public event PropertyChangedEventHandler PropertyChanged;

        public String Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("DateTime"); }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
