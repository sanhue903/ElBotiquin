using System.Collections.Generic;
public class Score
    {
        private bool isCorrect;
        private string answer;
        private float time;
        public Score(bool isCorrect, string answer, float time)
        {
            this.isCorrect = isCorrect;
            this.answer = answer;
            this.time = time;
        }
    } 