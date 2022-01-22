using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManager.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; private set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; private set; }    
        public float Price { get; private set; } = 0;
        [JsonIgnore]
        public virtual List<OrderProduct>? OrderProducts { get; }
        [Required]
        public DateTime CreationDate { get; private set; }

        public Product(string name, string description, float price)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        public Product SetCreationDateNow()
        {
            this.CreationDate = DateTime.Now;
            return this;
        }

        public Product SetProduct(ProductUpdate prod)
        {
            this.Name = prod.Name;
            this.Description = prod.Description;
            this.Price = prod.Price;
            return this;
        }
    }

    // Classes intermediarias para prevenir exposição da entidade.
    public class ProductInput
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = String.Empty;
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = String.Empty;   
        public float Price { get; set; } = 0;
    }

    public class ProductUpdate : ProductInput
    {
        [Required]
        public int Id { get; set; }
    }


    public class ProductFilter
    {
        public string Name { get; set; } = String.Empty;
        public bool OrderbyDescName { get; set; } = false;
        public DateTime? FromCreationDate { get; set; } = null;
        public DateTime? ToCreationDate { get; set; } = null;
    }
}