using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KvantShared.Model
{
    public abstract class BaseEntity : IDatedModel
    {
        protected BaseEntity()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }

        /// <summary>
        /// Record creation date
        /// </summary>
        [Column(Order = 100)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Record last update date (including creation and deletion)
        /// </summary>
        [Column(Order = 101)]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Record deleting date
        /// </summary>
        [Column(Order = 102)]
        public DateTime? Deleted { get; set; }
    }
}
