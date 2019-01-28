using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Model
{
    public interface IIdModel : IDatedModel
    {
        int Id { get; set; }
    }
}
