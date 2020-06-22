using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralErros.Models
{
    [Table("error")]
    public class Error
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("title", TypeName = "text")]
        [Required]
        public string Title { get; set; }

        [Column("details", TypeName = "text")]
        [Required]
        public string Details { get; set; }
        
        [Column("origin")]
        [StringLength(200)]
        [Required]
        public string Origin { get; set; }

        [Column("status")]
        [Required]
        public char Status { get; set; }

        [Column("number_events")]
        [Required]
        public int NumberEvents { get; set; }
        
        [Column("timestamp")]
        [Required]
        public Byte[] TimeStamp { get; set; }

        [Column("environment_id")]
        [Required]
        public int EnvironmentId { get; set; }

        [ForeignKey("EnvironmentId")]
        public virtual Environment Environment { get; set; }

        [Column("application_layer_id")]
        [Required]
        public int ApplicationLayerId { get; set; }

        [ForeignKey("ApplicationLayerId")]
        public virtual ApplicationLayer ApplicationLayer { get; set; }

        [Column("level_id")]
        [Required]
        public int LevelId { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }
    }
}
