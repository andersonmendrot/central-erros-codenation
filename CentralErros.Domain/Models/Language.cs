using System.Collections.Generic;

namespace CentralErros.Domain.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Error> Errors { get; set; }
    }
}
