using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Quiz
    {
        public int QuizId { get; set; }

        public int CategoryId { get; set; }

        public int TimesPlayed { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; } = null!;

        public string DateCreated { get; set; } = DateTime.Now.ToLongDateString();

        [MaxLength(128)]
        public string QuizName { get; set; } = null!;

        [MaxLength(32)]
        public string CreatorName { get; set; } = null!;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}