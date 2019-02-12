namespace KvantShared.Vms
{
    public class ParentVm : HumanVm
    {
        private int _statusId;

        public int StatusId
        {
            get => _statusId;
            set { SetProperty(ref _statusId, value, () => StatusId); }
        }
    }
}
