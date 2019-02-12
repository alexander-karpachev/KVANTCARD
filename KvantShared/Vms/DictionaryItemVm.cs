namespace KvantShared.Vms
{
    public class DictionaryItemVm : BaseIdVm
    {
        private string title;
        public string Title
        {
            get => title;
            set { SetProperty(ref title, value, () => Title); }
        }
    }
}
