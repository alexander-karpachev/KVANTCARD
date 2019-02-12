using System.Collections.Generic;

namespace KvantShared.Vms
{
    public class MentorVm : HumanVm
    {
        private List<int> _kvantumId;

        public List<int> KvantumId
        {
            get => _kvantumId;
            set { SetProperty(ref _kvantumId, value, () => KvantumId); }
        }
    }
}
