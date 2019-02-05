using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class MentorVm : HumanVm
    {
        private List<int> _kvantumId;

        public List<int> KvantumId
        {
            get => _kvantumId;
            set { SetProperty(ref _kvantumId, value, () => KvantumId); }
        }
    }
}
