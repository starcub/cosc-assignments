using System;
using System.Collections.Generic;

namespace Survey.DataEntities
{
    public class SurveyEntity
    {
        public string CourseCode;
        public string CourseName;
        public DateTime StartDate;
        public DateTime EndDate;
        public List<QuestionEntity> Questions;
    }
}