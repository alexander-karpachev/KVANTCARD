using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KvantShared.Services;
using KvantShared.Utils;
using KvantShared.Vms.References;

namespace KvantCard.View.SimpleRef
{
    public class SimpleRefVm : NotifiableBase
    {
        private readonly ReferenceService _refSertvice;

        public SimpleRefVm()
        {

        }

        public SimpleRefVm(ReferenceService refSertvice)
        {
            _refSertvice = refSertvice;
        }

        private string _refCode;

        public string RefCode
        {
            get => _refCode ?? "<Design Time Code>";
            set
            {
                SetProperty(ref _refCode, value, () => RefCode);

                if (_refSertvice == null || string.IsNullOrWhiteSpace(_refCode)) return;

                _refTitle = _refSertvice.GetByCode(_refCode).Title;
                RaisePropertyChanged(() => RefTitle);
                _records = null;
                RaisePropertyChanged(() => Records);
            }
        }

        private string _refTitle;

        public string RefTitle => _refTitle ?? "<Design Time Title>";

        private ObservableCollection<SimpleRecordVm> _records;

        public ObservableCollection<SimpleRecordVm> Records
        {
            get
            {
                if (_records != null) return _records;
                if (_refSertvice == null || string.IsNullOrWhiteSpace(_refCode))
                    _records = new ObservableCollection<SimpleRecordVm>();
                else
                    _records = _refSertvice.GetRecords<SimpleRecordVm>(_refCode);
                return _records;
            }

            set
            {
                SetProperty(ref _records, value, () => Records);
            }
        }

    }
}
