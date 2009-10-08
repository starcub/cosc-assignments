﻿using System;
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
        SurveyInstanceEntity GetSurveyByCourseCode(string courseCode);

        [OperationContract]
        int InsertSurveyInstance(SurveyInstanceEntity survey);

        [OperationContract]
        int InsertQuestionInstance(QuestionInstanceEntity question);

        [OperationContract]
        SurveyInstanceEntity GetSurveyInstancesByParticipation(Participation participation);

        // TODO: rename this to GetSurveyByCourseCode
        [OperationContract]
        SurveyInstanceEntity GetSurveyInfoByCourseCode(string courseCode);

        [OperationContract]
        int GetTotalNumberOfStudentsByCourseCode(string courseCode);
    }
}
