using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KvantCard.Model
{
    public class Contact : BaseIdEntity
    {
        private List<string> _email;
        [NotMapped]
        public List<string> Email
        {
            get => _email;
            set { SetProperty(ref _email, value, () => Email); }
        }

        private List<string> _phoneNumber;
        [NotMapped]
        public List<string> PhoneNumber
        {
            get => _phoneNumber;
            set { SetProperty(ref _phoneNumber, value, () => PhoneNumber); }
        }

        private List<Address> _address;

        public List<Address> Address
        {
            get => _address;
            set { SetProperty(ref _address, value, () => Address); }
        }
    }
}
