namespace KvantCard.Model
{
    public class Parent : Human
    {
        private int _statusId;

        public int StatusId
        {
            get => _statusId;
            set { SetProperty(ref _statusId, value, () => StatusId); }
        }
    }
}
