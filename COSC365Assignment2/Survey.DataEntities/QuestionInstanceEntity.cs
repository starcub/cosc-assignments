using System;


namespace Survey.DataEntities
{
    public class QuestionInstanceEntity : QuestionEntity
    {
        public int QuestionInstanceID;
        public int Score;
        public string Comment;
        public int SurveyInstanceID;
    }
}