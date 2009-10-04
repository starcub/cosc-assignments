using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survey.DataLayer.CourseInfoServiceReference;
using Survey.DataEntities;
using System.Configuration;
using System.Data.SqlClient;

namespace Survey.DataLayer
{
    public class DataLayerServiceOperations : ISurveyDLServices
    {

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SurveyConnectionString"].ConnectionString;
            }
        }

        public List<Participation> GetCourseByUserCode(string userCode)
        {
            return new CourseInfoServiceReference.CourseInfoServiceSoapClient().GetCourseByUserCode(userCode);
        }

        public SurveyEntity GetSurveyByCourseCode(string courseCode)
        {
            // gets course info from the course info webservice
            Course course = new CourseInfoServiceReference.CourseInfoServiceSoapClient().GetCourseByCourseCode(courseCode);

            // instantiates a survey object and sets its course info properties
            SurveyEntity results = new DataEntities.SurveyEntity();
            results.CourseCode = course.Code;
            results.CourseName = course.Name;
            results.Questions = getQuestions();
            
            return results;
        }


        private List<QuestionEntity> getQuestions()
        {
            List<QuestionEntity> results = new List<QuestionEntity>();
            string commandText = "select * from Question";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuestionEntity question = new QuestionEntity();
                            question.Text = (string)reader["text"];
                            results.Add(question);
                        }
                    }
                }
            }
            return results;
        }

    }
}
