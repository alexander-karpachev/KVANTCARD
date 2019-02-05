using System;
using KvantCard.Model;
using KvantCard.Utils;

namespace KvantCard.Vms
{
    public class BaseVm : NotifiableBase, IDatedModel
    {
        protected BaseVm()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }

        private DateTime _created;
        /// <summary>
        /// Record creation date
        /// </summary>
        public DateTime Created
        {
            get => _created;
            set { SetProperty(ref _created, value, () => Created); }
        }

        private DateTime _updated;
        /// <summary>
        /// Record last update date (including creation and deletion)
        /// </summary>
        public DateTime Updated
        {
            get => _updated;
            set { SetProperty(ref _updated, value, () => Updated); }
        }

        private DateTime? _deleted;
        /// <summary>
        /// Record deleting date
        /// </summary>
        public DateTime? Deleted
        {
            get => _deleted;
            set { SetProperty(ref _deleted, value, () => Deleted); }
        }
    }
}
