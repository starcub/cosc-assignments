using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Survey.BusinessLayer.DataLayerServiceReference;

namespace Survey.BusinessLayer
{
    [ServiceContract]
    interface ISurveyBLServices
    {
        [OperationContract]
        List<Participation> GetCourseByUserCode(string userCode);

        [OperationContract]
        SurveyEntity GetSurveyByCourseCode(string courseCode);
    }

}
