namespace Domain
{
    public class AnswerChoice
    {
        public int AnswerChoiceId { get; set; }

        public int GameId { get; set; }

        public int QuestionId { get; set; }

        public int AnswerId { get; set; }
    }
}