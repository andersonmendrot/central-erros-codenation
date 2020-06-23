using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralErros.Domain
{
    public class User
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name", TypeName = "text")]
        [Required]
        public string Name { get; set; }

        [Column("email")]
        [StringLength(254)]
        [Required]
        public string Email { get; set; }

        [Column("password")]
        [StringLength(15)]
        [Required]
        public string Password { get; set; }

        [Column("timestamp")]
        [Required]
        public Byte[] TimeStamp { get; set; }
    }
}
