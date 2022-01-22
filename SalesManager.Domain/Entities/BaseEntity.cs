using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesManager.Domain.Entities
{
    public class BaseEntity
    {
        [JsonPropertyOrder(-1)]
        public virtual int Id { get; set; }
    }
}