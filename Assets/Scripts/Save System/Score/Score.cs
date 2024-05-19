using System.Collections.Generic;
public class Score
    {
        private string idQuestion;
        private bool isCorrect;
        private string answer;
        private float time;
        public Score(string idQuestion, bool isCorrect, string answer, float time)
        {
            this.idQuestion = idQuestion;
            this.isCorrect = isCorrect;
            this.answer = answer;
            this.time = time;
        }
    } 