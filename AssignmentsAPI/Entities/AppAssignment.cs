using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppAssignment
    {
            public int Id { get; set; }

            [StringLength(20)]
            [Required]
            public string TaskName { get; set; } = string.Empty;

            [StringLength(200)]
            public string Description { get; set; } = string.Empty;

            [StringLength(20)]
            [Required]
            public string TaskStatus { get; set; } = string.Empty;

            [StringLength(20)]
            public string RepetitionTask { get; set; } = string.Empty;

            public int TaskTypeId { get; set; }
            public TaskType? TaskType { get; set; }

            public int TaskArchiveId { get; set; }
            public TaskArchive? TaskArchive { get; set; }

            [Required]
            public DateTime Start { get; set; }

            public DateTime End { get; set; }
    }
}