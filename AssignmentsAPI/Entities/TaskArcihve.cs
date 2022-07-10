using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class TaskArchive
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string ArchiveStatus { get; set; } = string.Empty;
    }
}