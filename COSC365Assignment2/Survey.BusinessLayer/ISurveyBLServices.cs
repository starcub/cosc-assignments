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
        List<SurveyInstanceEntity> GetCourseByUserCode(string userCode);

        [OperationContract]
        SurveyInstanceEntity GetSurveyByCourseCode(string courseCode);

        [OperationContract]
        int InsertSurveyInstance(SurveyInstanceEntity survey);

        [OperationContract]
        int GetTotalNumberOfStudentsByCourseCode(string courseCode);

    }

}
