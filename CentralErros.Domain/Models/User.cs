using System;
using CentralErros.Domain.Repositories;

namespace CentralErros.Domain.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Byte[] Timestamp { get; set; }
    }
}
