using System;
using System.Collections.Generic;
using System.Text;

namespace CentralErros.Domain.Interfaces
{
    public interface IUser
    {
        string Id { get; set; }
        string Name { get; set; }
    }
}
