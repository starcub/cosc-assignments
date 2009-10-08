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

        public List<SurveyInstanceEntity> GetCourseByUserCode(string userCode)
        {
            List<Participation> participations = client.GetCourseByUserCode(userCode);
            List<SurveyInstanceEntity> results = new List<SurveyInstanceEntity>();
            SurveyInstanceEntity survey = new SurveyInstanceEntity();
            foreach (Participation participation in participations)
            {
                SurveyInstanceEntity surveyInstance = client.GetSurveyInstancesByParticipation(participation);
                surveyInstance.CourseCode = participation.CourseCode;
                surveyInstance.Role = participation.Role;
                results.Add(surveyInstance);
            }
            foreach (SurveyInstanceEntity surveyInstance in results)
            {
                if (surveyInstance.DateSubmitted == null)
                {
                    survey = client.GetSurveyInfoByCourseCode(surveyInstance.CourseCode);
                    surveyInstance.StartDate = survey.StartDate;
                    surveyInstance.EndDate = survey.EndDate;

                    if (DateTime.Now < surveyInstance.StartDate)
                    {
                        surveyInstance.Status = "Scheduled";
                    }
                    else if (DateTime.Now > surveyInstance.StartDate && DateTime.Now < surveyInstance.EndDate)
                    {
                        surveyInstance.Status = "Underway";
                    }
                    else if (DateTime.Now > surveyInstance.EndDate)
                    {
                        surveyInstance.Status = "Finished";
                    }
                }
                else
                {
                    surveyInstance.Status = "Submitted " + surveyInstance.DateSubmitted;
                }
            }
            return results;
        }

        public SurveyInstanceEntity GetSurveyByCourseCode(string courseCode)
        {
            SurveyInstanceEntity survey = client.GetSurveyByCourseCode(courseCode);
            return survey;
        }

        public int InsertSurveyInstance(SurveyInstanceEntity survey)
        {
            survey.SurveyInstanceID = client.InsertSurveyInstance(survey);
            
            foreach (QuestionInstanceEntity question in survey.Questions)
            {
                question.SurveyInstanceID = survey.SurveyInstanceID;
                client.InsertQuestionInstance(question);
            }
            return survey.SurveyInstanceID;
        }

        public int GetTotalNumberOfStudentsByCourseCode(string courseCode)
        {
            return client.GetTotalNumberOfStudentsByCourseCode(courseCode);
        }
    }
}
