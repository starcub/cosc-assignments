using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.DataEntities
{
    public abstract class QuestionEntity
    {
        public int QuestionID;
        public string Text;
        public int SurveyID;

    }
}
