using System;
using System.Collections.Generic;
using System.Text;

namespace KvantShared.Model
{
    public class Record : BaseIdEntity
    {
        public virtual Reference Reference { get; set; }

        public string Content { get; set; }

    }
}
