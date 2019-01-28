using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public interface IDatedModel
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
        DateTime? Deleted { get; set; }
    }
}
