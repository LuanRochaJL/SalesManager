using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManager.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public int UserId { get; private set; }
        [JsonIgnore]
        public virtual User? User { get; private set; }
        [Required]
        public List<OrderProduct> OrderProducts { get; set; }
        [Required]
        public DateTime CreationDate { get; private set; }

        public Order(int userId)
        {
            this.UserId = userId;
        }

        public Order SetCreationDateNow()
        {
            this.CreationDate = DateTime.Now;
            return this;
        }
    }

    // Classes intermediarias para prevenir exposição da entidade.
    public class OrderInput
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public List<OrderProductInput> OrderProducts { get; set; }
    }

    public class OrderFilter
    {
        public string UserName { get; set; } = String.Empty;
        public DateTime? FromCreationDate { get; set; } = null;
        public DateTime? ToCreationDate { get; set; } = null;
    }
}