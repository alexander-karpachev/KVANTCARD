using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public class DictionaryItem : BaseEntity
    {
        private string title;
        public string Title
        {
            get => title;
            set { SetProperty(ref title, value, () => Title); }
        }

    }
}
