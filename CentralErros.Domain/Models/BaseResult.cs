using System;
using System.Collections.Generic;
using System.Text;

namespace CentralErros.Domain.Models
{
    public class BaseResult<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
