using System;

namespace CentralErros.Domain
{
    public class Error
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public char Status { get; set; }
        public int NumberEvents { get; set; }
        public Byte[] Timestamp { get; set; }
        public int ApplicationLayerId { get; set; }
        public int EnvironmentId { get; set; }
        public int LanguageId { get; set; }
        public int LevelId { get; set; }
        public virtual ApplicationLayer ApplicationLayer { get; set; }
        public virtual Environment Environment { get; set; }
        public virtual Language Language { get; set; }
        public virtual Level Level { get; set; }
    }
}
