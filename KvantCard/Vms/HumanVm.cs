using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class HumanVm : BaseIdVm
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { SetProperty(ref _firstName, value, () => FirstName); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { SetProperty(ref _lastName, value, () => LastName); }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set { SetProperty(ref _middleName, value, () => MiddleName); }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                SetProperty(ref _birthDate, value, () => BirthDate);
                RaisePropertyChanged(() => Age);
            }
        }

        public int Age => (int)((DateTime.Now - BirthDate).Days / 365.25);

        private ContactVm _contact;
        public ContactVm Contact
        {
            get => _contact;
            set { SetProperty(ref _contact, value, () => Contact); }
        }
    }
}
