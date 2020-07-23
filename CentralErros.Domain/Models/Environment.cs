using System.Collections.Generic;
using CentralErros.Domain.Repositories;

namespace CentralErros.Domain.Models
{
    public class Environment : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Error> Errors { get; set; }
    }
}
