using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Survey.DataLayer.CourseInfoServiceReference;
using Survey.DataEntities;

namespace Survey.DataLayer
{
    [ServiceContract]
    interface ISurveyDLServices
    {
        [OperationContract]
        List<Participation> GetCourseByUserCode(string userCode);

        [OperationContract]
        SurveyEntity GetSurveyByCourseCode(string courseCode);
    }
}
