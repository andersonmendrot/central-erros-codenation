using System.Collections.Generic;

namespace CentralErros.Domain
{
    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Error> Errors { get; set; }
    }
}
