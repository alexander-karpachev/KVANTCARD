using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvantCard.Vms
{
    public class UserVm : BaseIdVm
    {
        private string _name;
        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value, () => Name); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { SetProperty(ref _lastName, value, () => LastName); }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set { SetProperty(ref _userName, value, () => UserName); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { SetProperty(ref _email, value, () => Email); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { SetProperty(ref _password, value, () => Password); }
        }

    }
}
