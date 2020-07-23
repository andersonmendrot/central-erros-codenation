using System.Collections.Generic;

namespace CentralErros.Domain.Models
{
    public class ApplicationLayer : Repositories.IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Error> Errors { get; set; }
    }
}
