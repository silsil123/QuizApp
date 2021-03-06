using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }

        [MaxLength(256)]
        public string AnswerString { get; set; } = null!;

        public bool CorrectAnswer { get; set; }
    }
}