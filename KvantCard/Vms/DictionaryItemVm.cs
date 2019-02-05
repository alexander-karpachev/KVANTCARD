using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class DictionaryItemVm : BaseIdVm
    {
        private string title;
        public string Title
        {
            get => title;
            set { SetProperty(ref title, value, () => Title); }
        }
    }
}
