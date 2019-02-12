using System.Collections.Generic;

namespace KvantShared.Model
{
    public class Contact : BaseIdEntity
    {
        public string Emails { get; set; }

        public string PhoneNumbers { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
