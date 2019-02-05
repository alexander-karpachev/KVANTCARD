using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class ParentVm : HumanVm
    {
        private int _statusId;

        public int StatusId
        {
            get => _statusId;
            set { SetProperty(ref _statusId, value, () => StatusId); }
        }
    }
}
