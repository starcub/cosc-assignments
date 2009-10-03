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
        public string CourseByUserCode(string userCode)
        {
            return getCourseByUserCode(userCode);
        }

        private string getCourseByUserCode(string userCode)
        {
            string a = "";
            string commandText = "select * from course c join Participation p on c.Code = p.CourseCode where p.UserCode = '" + userCode + "'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            a +=String.Format("{0}, {1}",  reader[0], reader[1]);
                        }
                    }
                }
            }
            return a;
        }

    }

}
