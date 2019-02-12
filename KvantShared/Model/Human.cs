using System;

namespace KvantShared.Model
{
    public class Human : BaseIdEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public Contact Contact { get; set; }
    }
}
