using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.DataEntities
{
    public abstract class SurveyEntity
    {
        public int SurveyID;
        public DateTime StartDate;
        public DateTime EndDate;
    }
}
