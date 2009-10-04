using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survey.BusinessLayer.DataLayerServiceReference;

namespace Survey.BusinessLayer
{
    public class BusinessLayerServiceOperations : ISurveyBLServices
    {
        SurveyDLServicesClient client = new SurveyDLServicesClient();

        public List<Participation> GetCourseByUserCode(string userCode)
        {
            return client.GetCourseByUserCode(userCode);
        }

        public SurveyEntity GetSurveyByCourseCode(string courseCode)
        {
            SurveyEntity survey = client.GetSurveyByCourseCode(courseCode);
            return survey;
        }
    }
}
