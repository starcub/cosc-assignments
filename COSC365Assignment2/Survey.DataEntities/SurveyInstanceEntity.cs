using System;
using System.Collections.Generic;


namespace Survey.DataEntities
{
    public class SurveyInstanceEntity : SurveyEntity
    {
        public int SurveyInstanceID;
        public string CourseCode;
        public string CourseName;
        public string UserCode;
        public DateTime DateSubmitted;
        public string Role;
        public string Status;
        public List<QuestionInstanceEntity> Questions;
    }
}