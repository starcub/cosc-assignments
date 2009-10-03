using System;
using System.Collections.Generic;

namespace Survey.DataEntities
{
    public class SurveyEntity
    {
        public string CourseCode;
        public DateTime EndDate;
        public DateTime StartDate;
        public List<QuestionEntity> Questions;
        public SurveyInstanceEntity SurveyInstance;
    }
}