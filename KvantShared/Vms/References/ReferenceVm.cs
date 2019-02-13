using System;

namespace KvantShared.Vms.References
{
    public class ReferenceVm : BaseIdVm
    {
        private string _code;
        public string Code
        {
            get => _code;
            set { SetProperty(ref _code, value, () => Code); }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set { SetProperty(ref _title, value, () => Title); }
        }


        private bool _complex;
        public bool Complex
        {
            get => _complex;
            set { SetProperty(ref _complex, value, () => Complex); }
        }

        private Type _itemClass;
        public Type ItemClass
        {
            get => _itemClass;
            set { SetProperty(ref _itemClass, value, () => ItemClass); }
        }


    }
}
