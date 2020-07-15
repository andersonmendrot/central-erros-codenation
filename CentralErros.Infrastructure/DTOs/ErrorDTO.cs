using System;
using System.Collections.Generic;
using System.Text;

namespace CentralErros.Infrastructure.DTOs
{
    public class ErrorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public char Status { get; set; }
        public int NumberEvents { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ApplicationLayerId { get; set; }
        public int EnvironmentId { get; set; }
        public int LanguageId { get; set; }
        public int LevelId { get; set; }
    }
}
