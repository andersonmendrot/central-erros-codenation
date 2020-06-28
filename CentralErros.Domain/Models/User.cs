using System;

namespace CentralErros.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Byte[] Timestamp { get; set; }
    }
}
