using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class EmployeeVm : HumanVm
    {
        private int _positionId;

        public int PositionId
        {
            get => _positionId;
            set { SetProperty(ref _positionId, value, () => PositionId); }
        }
    }
}
