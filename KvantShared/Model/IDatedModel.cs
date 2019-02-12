using System;

namespace KvantShared.Model
{
    public interface IDatedModel
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
        DateTime? Deleted { get; set; }
    }
}
