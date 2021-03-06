﻿using System;
using CentralErros.Domain.Repositories;

namespace CentralErros.Domain.Models
{
    public class Error : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }
        public char Status { get; set; }
        public DateTime CreatedAt { get; set; }
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
