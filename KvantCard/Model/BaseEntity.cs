using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KvantCard.Utils;

namespace KvantCard.Model
{
    public abstract class BaseEntity : NotifiableBase, IDatedModel
    {
        protected BaseEntity()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }

        private DateTime _created;
        /// <summary>
        /// Record creation date
        /// </summary>
        [Column(Order = 100)]
        public DateTime Created {
            get => _created;
            set { SetProperty(ref _created, value, () => Created); }
        }

        private DateTime _updated;
        /// <summary>
        /// Record last update date (including creation and deletion)
        /// </summary>
        [Column(Order = 101)]
        public DateTime Updated {
            get => _updated;
            set { SetProperty(ref _updated, value, () => Updated); }
        }

        private DateTime? _deleted;
        /// <summary>
        /// Record deleting date
        /// </summary>
        [Column(Order = 102)]
        public DateTime? Deleted {
            get => _deleted;
            set { SetProperty(ref _deleted, value, () => Deleted); }
        }
    }
}
