using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        public int GameId { get; set; }

        public int QuizId { get; set; }

        public int Points { get; set; }

        public string PlayerName { get; set; } = null!;

        public string DateCreated { get; set; } = DateTime.Now.ToLongDateString();

        public ICollection<AnswerChoice> AnswerChoices { get; set; } = new List<AnswerChoice>();
    }
}