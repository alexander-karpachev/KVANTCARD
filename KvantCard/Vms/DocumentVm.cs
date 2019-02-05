using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class DocumentVm : BaseIdVm
    {
        private string _title;

        public string Title
        {
            get => _title;
            set { SetProperty(ref _title, value, () => Title); }
        }
    }
}
