using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    class Document : INotifyPropertyChanged
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private String title;

        public String Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }
        
        //TODO: Добавить данные о документе

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
