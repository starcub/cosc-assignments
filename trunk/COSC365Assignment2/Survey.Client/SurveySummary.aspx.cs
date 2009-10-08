using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Survey.Client.BusinessLayerServiceReference;

namespace Survey.Client
{
    public partial class SurveySummary : System.Web.UI.Page
    {

        #region Properties

        public string CourseCode
        {
            get
            {
                return Request.QueryString["CourseCode"];
            }
        }

        public int TotalNumberOfStudents
        {
            get;
            set;
        }

        public int NumberOfSubmitted
        {
            get;
            set;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(CourseCode))
                {
                    TotalNumberOfStudents = client.GetTotalNumberOfStudentsByCourseCode(CourseCode);
                    //NumberOfSubmitted = 
                }
            }
        }

        private readonly SurveyBLServicesClient client = new SurveyBLServicesClient();

        #endregion

        #region Methods



        #endregion
    }
}
