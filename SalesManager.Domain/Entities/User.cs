using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManager.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string UserName { get; private set; } = String.Empty;
        [Required]
        [MaxLength(32)]
        [JsonIgnore]
        public string Password { get; private set; } = String.Empty;
        [Required]
        [MaxLength(100)]  
        public string Name { get; private set; } = String.Empty;
        [Required]
        [MaxLength(100)]  
        public string Email { get; private set; } = String.Empty;
        [JsonIgnore]
        public List<Order>? Orders { get; private set; }
        public DateTime CreationDate { get; private set; }

        public User(string userName, string password, string name, string email)
        {
            this.UserName = userName;
            this.Password = password;
            this.Name = name;
            this.Email = email;
        }

        public User SetCreationDateNow()
        {
            this.CreationDate = DateTime.Now;
            return this;
        }
    }

    // Classes intermediarias para prevenir exposição da entidade.
    public class UserInput
    {
        [Required]
        [MaxLength(10)]
        public string UserName { get; set; } = String.Empty;
        [Required]
        [MaxLength(32)]
        public string Password { get; set; } = String.Empty;
        [Required]
        [MaxLength(100)]  
        public string Name { get; set; } = String.Empty;
        [Required]
        [MaxLength(100)]  
        public string Email { get; set; } = String.Empty;
    }

    public class UserAuthenticator
    {
        [Required]
        [MaxLength(10)]
        public string UserName { get; set; } = String.Empty;
        [Required]
        [MaxLength(32)]
        public string Password { get; set; } = String.Empty;   
    }

    public class UserFilter
    {
        public string UserName { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public bool OrderbyDescUserName { get; set; } = false;
        public bool OrderbyDescName { get; set; } = false;
        public bool OrderbyDescEmail { get; set; } = false;
        public bool OrderbyDescCreationDate { get; set; } = false;
        public DateTime? FromCreationDate { get; set; } = null;
        public DateTime? ToCreationDate { get; set; } = null;
    }
}
