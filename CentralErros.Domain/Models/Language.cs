using System.Collections.Generic;
using CentralErros.Domain.Repositories;

namespace CentralErros.Domain.Models
{
    public class Language : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Error> Errors { get; set; }
    }
}