using System.ComponentModel.DataAnnotations;
using KvantCard.Utils;

namespace KvantCard.Model
{
    public class BaseEntity : NotifiableBase
    {
        private int _id;
        [Key]
        public int Id { get => _id;
            set { SetProperty(ref _id, value, () => Id); } }

    }
}
