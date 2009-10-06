using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survey.DataLayer.CourseInfoServiceReference;
using Survey.DataEntities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

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

        public SurveyInstanceEntity GetSurveyInstancesByCourseCodeUsercode(string courseCode, string usercode)
        {
            SurveyInstanceEntity surveyInstance = new SurveyInstanceEntity();
            string commandText = "SELECT * FROM SurveyInstance si JOIN Survey s ON si.SurveyID = s.ID WHERE si.CourseCode = @CourseCode AND Usercode = @Usercode";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@CourseCode", SqlDbType.NVarChar);
                command.Parameters["@CourseCode"].Value = courseCode;
                command.Parameters.Add("@UserCode", SqlDbType.NVarChar);
                command.Parameters["@UserCode"].Value = usercode;
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // should return max count of 1
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                surveyInstance.SurveyInstanceID = (int)reader["ID"];
                                surveyInstance.SurveyID = (int)reader["SurveyID"];
                                surveyInstance.CourseCode = (string)reader["si.CourseCode"];
                                surveyInstance.CourseName = (string)reader["CourseName"];
                                surveyInstance.DateSubmitted = (DateTime)reader["DateSubmitted"];
                                surveyInstance.StartDate = (DateTime)reader["StartDate"];
                                surveyInstance.EndDate = (DateTime)reader["EndDate"];
                            }
                        }
                        else
                        {
                            surveyInstance = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return surveyInstance;
        }

        public SurveyInstanceEntity GetSurveyByCourseCode(string courseCode)
        {
            // gets course info from the course info webservice
            Course course = new CourseInfoServiceReference.CourseInfoServiceSoapClient().GetCourseByCourseCode(courseCode);

            // instantiates a survey object and sets its course info properties
            SurveyInstanceEntity results = new DataEntities.SurveyInstanceEntity();
            results.CourseCode = course.Code;
            results.CourseName = course.Name;
            results.Questions = getQuestionsBySurveyID(1);
            
            return results;
        }

        public int InsertSurveyInstance(SurveyInstanceEntity survey)
        {
            int id = 0;
            string commandText = "INSERT INTO SurveyInstance (CourseCode,Usercode,DateSubmitted) VALUES (@CourseCode,@UserCode,@DateSubmitted)" +
                                 "SELECT CAST(scope_identity() AS int)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@CourseCode",SqlDbType.NVarChar);
                command.Parameters["@CourseCode"].Value = survey.CourseCode;

                command.Parameters.Add("@Usercode", SqlDbType.NVarChar);
                command.Parameters["@Usercode"].Value = survey.UserCode;
                command.Parameters.Add("@DateSubmitted", SqlDbType.DateTime);
                command.Parameters["@DateSubmitted"].Value = survey.DateSubmitted;

                try
                {
                    connection.Open();
                    id = (Int32)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public int InsertQuestionInstance(QuestionInstanceEntity question)
        {
            int id = 0;
            string commandText = "INSERT INTO QuestionInstance (Score,Comment,SurveyInstanceID,QuestionID) VALUES (@Score,@Comment,@SurveyInstanceID,@QuestionID)" +
                                 "SELECT CAST(scope_identity() AS int)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@Score", SqlDbType.Int);
                command.Parameters["@Score"].Value = question.Score;
                command.Parameters.Add("@Comment", SqlDbType.Text);
                command.Parameters["@Comment"].Value = question.Comment;
                command.Parameters.Add("@SurveyInstanceID", SqlDbType.Int);
                command.Parameters["@SurveyInstanceID"].Value = question.SurveyInstanceID;
                command.Parameters.Add("@QuestionID", SqlDbType.Int);
                command.Parameters["@QuestionID"].Value = question.QuestionID;

                try
                {
                    connection.Open();
                    id = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        private List<QuestionInstanceEntity> getQuestionsBySurveyID(int surveyID)
        {
            List<QuestionInstanceEntity> results = new List<QuestionInstanceEntity>();
            string commandText = "select * from Question WHERE surveyID = @surveyID";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@surveyID", SqlDbType.Int);
                command.Parameters["@surveyID"].Value = surveyID;
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuestionInstanceEntity question = new QuestionInstanceEntity();
                            question.QuestionID = (int)reader["ID"];
                            question.Text = (string)reader["Text"];
                            question.SurveyID = (int)reader["SurveyID"];
                            results.Add(question);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return results;
        }

    }
}
