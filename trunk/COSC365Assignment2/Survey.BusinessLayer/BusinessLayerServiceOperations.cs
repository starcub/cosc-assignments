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
            foreach (Participation participation in participations)
            {
                SurveyInstanceEntity surveyInstance = client.GetSurveyInstancesByCourseCodeUsercode(participation.CourseCode, participation.UserCode);
                if (surveyInstance == null)
                {
                    surveyInstance = new SurveyInstanceEntity();
                }
                surveyInstance.CourseCode = participation.CourseCode;
                surveyInstance.Role = participation.Role;
                results.Add(surveyInstance);
            }
            foreach (SurveyInstanceEntity surveyInstance in results)
            {
                if (surveyInstance.DateSubmitted != null)
                {
                    surveyInstance.Status = "Submitted " + surveyInstance.DateSubmitted;
                }
                else
                {
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
    }
}
