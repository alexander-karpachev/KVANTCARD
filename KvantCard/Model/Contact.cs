using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KvantCard.Model
{
    public class Contact : BaseIdEntity
    {
        public string Emails { get; set; }

        public string PhoneNumbers { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
