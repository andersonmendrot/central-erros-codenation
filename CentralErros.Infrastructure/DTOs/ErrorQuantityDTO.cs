using System;
using System.Collections.Generic;
using System.Text;

namespace CentralErros.Infrastructure.DTOs
{
    public class ErrorQuantityDTO
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public char Status { get; set; }
        public int ApplicationLayerId { get; set; }
        public int EnvironmentId { get; set; }
        public int LanguageId { get; set; }
        public int LevelId { get; set; }
        public int Ocurrences { get; set; }
    }
}
