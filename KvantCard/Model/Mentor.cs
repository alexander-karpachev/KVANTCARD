using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public class Mentor : Human
    {
        private List<int> _kvantumId;

        public List<int> KvantumId
        {
            get => _kvantumId;
            set { SetProperty(ref _kvantumId, value, () => KvantumId); }
        }
    }
}
