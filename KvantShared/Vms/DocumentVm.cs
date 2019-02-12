namespace KvantShared.Vms
{
    public class DocumentVm : BaseIdVm
    {
        private string _title;

        public string Title
        {
            get => _title;
            set { SetProperty(ref _title, value, () => Title); }
        }
    }
}
