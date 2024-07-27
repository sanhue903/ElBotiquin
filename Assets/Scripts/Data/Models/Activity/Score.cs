using Newtonsoft.Json;

public class Score
    {
        public string question_id;
        public bool is_correct;
        public string answer;
        public float seconds;
        public Score(string idQuestion, bool isCorrect, string answer, float time)
        {
            this.question_id = idQuestion;
            this.is_correct = isCorrect;
            this.answer = answer;
            this.seconds = time;
        }

        public string GetInfo()
        {
            return $"Question: {question_id}\nCorrect: {is_correct}\nAnswer: {answer}\nTime: {seconds}";
        }
    } 