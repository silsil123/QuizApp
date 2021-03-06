using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Question
    {
        public int QuestionId { get; set; }

        public int QuizId { get; set; }

        [MaxLength(512)]
        public string QuestionString { get; set; } = null!;
        
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}