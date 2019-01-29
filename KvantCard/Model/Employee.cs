using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public class Employee : Human
    {
        private int _positionId;

        public int PositionId
        {
            get => _positionId;
            set { SetProperty(ref _positionId, value, () => PositionId); }
        }
    }
}
