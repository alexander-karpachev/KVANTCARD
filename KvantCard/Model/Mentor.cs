using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    class Mentor : _Human, INotifyPropertyChanged
    {
        private List<int> kvantumID;

        public List<int> KvantumID
        {
            get { return kvantumID; }
            set { kvantumID = value; OnPropertyChanged("statusID"); }
        }
    }
}
