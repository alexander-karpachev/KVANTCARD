namespace KvantShared.Vms
{
    public class EmployeeVm : HumanVm
    {
        private int _positionId;

        public int PositionId
        {
            get => _positionId;
            set { SetProperty(ref _positionId, value, () => PositionId); }
        }
    }
}
