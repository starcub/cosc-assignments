using System;
using System.Collections.Generic;


namespace Survey.DataEntities
{
    public class SurveyInstanceEntity
    {
        public string CourseCode;
        public string UserCode;
        public DateTime DateSubmitted;
        public List<QuestionInstanceEntity> QuestionInstances;
    }
}