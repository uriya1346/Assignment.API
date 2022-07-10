using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class TaskType
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string TaskTypeName { get; set; } = string.Empty;
  
    }
}