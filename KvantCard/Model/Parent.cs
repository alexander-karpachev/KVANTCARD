using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model 
{
    public class Parent : _Human, INotifyPropertyChanged
    {
        // родство с учащимся
        private int statusID;

        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; OnPropertyChanged("statusID"); }
        }
    }
}
