using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralErros.Domain
{
    [Table("application_layer")]
    public class ApplicationLayer
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        
        [Column("name")]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public ICollection<Error> Errors { get; set; }
    }
}
