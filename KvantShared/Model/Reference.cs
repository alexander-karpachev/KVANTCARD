using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace KvantShared.Model
{
    public class Reference : BaseIdEntity
    {
        public string Code { get; set; }

        public string Title { get; set; }

        public bool Complex { get; set; }

        public string ItemClass { get; set; }

        public virtual ICollection<Record> Records { get; set; }


    }
}
