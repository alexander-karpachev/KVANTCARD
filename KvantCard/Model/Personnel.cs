using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    class Personnel : _Human, INotifyPropertyChanged
    {
        private int positionID;

        public int PositionID
        {
            get { return positionID; }
            set { positionID = value; OnPropertyChanged("PositionID"); }
        }
    }
}
