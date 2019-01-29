namespace KvantCard.Model
{
    public class Address : BaseIdEntity
    {
        private string _city;
        public string City
        {
            get => _city;
            set { SetProperty(ref _city, value, () => City); }
        }

        private string _street;
        public string Street
        {
            get => _street;
            set { SetProperty(ref _street, value, () => Street); }
        }

        private string _apartment;
        public string Apartment
        {
            get => _apartment;
            set { SetProperty(ref _apartment, value, () => Apartment); }
        }
    }
}
