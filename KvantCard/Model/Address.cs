namespace KvantCard.Model
{
    public class Address : BaseIdEntity
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
    }
}
