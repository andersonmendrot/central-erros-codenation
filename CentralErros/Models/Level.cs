using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralErros.Models
{
    [Table("level")]
    public class Level
    {
        [Column("id")]
        [Key]   
        public int Id { get; set; }

        [Column("name")]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
