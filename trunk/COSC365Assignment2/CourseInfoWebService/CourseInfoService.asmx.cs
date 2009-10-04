using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using SurveyManageApp.DataEntities;
using System.Data.SqlClient;
using System.Data;

namespace SurveyManageApp.CourseInfoWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CourseInfoService : System.Web.Services.WebService
    {

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CourseInfoConnectionString"].ConnectionString;
            }
        }

        [WebMethod]
        public List<Participation> GetCourseByUserCode(string userCode)
        {
            return getParticipationByUserCode(userCode);
        }

        [WebMethod]
        public Course GetCourseByCourseCode(string courseCode)
        {
            return getCourseByCourseCode(courseCode);
        }

        private List<Participation> getParticipationByUserCode(string userCode)
        {
            List<Participation> results = new List<Participation>();
            
            string commandText = "select * from courses c join Participation p on c.Code = p.CourseCode where p.UserCode = '" + userCode + "'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Participation participation = new Participation();
                            participation.CourseCode = (string)reader["CourseCode"];
                            participation.UserCode=userCode;
                            participation.Role = (string)reader["Role"];
                            results.Add(participation);
                        }
                    }
                }
            }
            return results;
        }

        private Course getCourseByCourseCode(string CourseCode)
        {
            Course results = new Course();

            string commandText = "select * from courses where Code = '" + CourseCode+ "'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Code = (string)reader["Code"];
                            results.Name= (string)reader["Name"];
                        }
                    }
                }
            }
            return results;
        }

    }

}
