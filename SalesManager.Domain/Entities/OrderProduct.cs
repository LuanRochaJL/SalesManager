using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManager.Domain.Entities
{
    public class OrderProduct : BaseEntity
    {
        [Required]
        public int OrderId { get; private set; }
        [JsonIgnore]
        public virtual Order? Order { get; private set; }
        [Required]
        public int ProductId { get; private set; }
        public virtual Product? Product { get; private set; }
        [Required]
        public int Quantity { get; private set; }

        public OrderProduct(int orderId, int productId, int quantity)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }

    // Classes intermediarias para prevenir exposição da entidade.
    public class OrderProductInput
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}