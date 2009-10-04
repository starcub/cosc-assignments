using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Survey.Client.BusinessLayerServiceReference;

namespace Survey.Client
{
    public partial class CourseSurvey : System.Web.UI.Page
    {
        #region Properties
        public string CourseCode
        {
            get
            {
                return Request.QueryString["CourseCode"] ?? "";
            }
        }

        public string SurveyTitle
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        #endregion

        #region Fields

        private readonly SurveyBLServicesClient client = new SurveyBLServicesClient();

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CourseCode))
            {
                Response.Redirect("~/");
            }
            else
            {
                InitSurvey();
            }
        }

        /// <summary>
        /// Submit the survey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Cancel to fill out the survey and return to the homepage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the survey page. This will load the questions from the database
        /// and display them.
        /// </summary>
        private void InitSurvey()
        {
            SurveyEntity survey = client.GetSurveyByCourseCode(CourseCode);
            SurveyTitle = survey.CourseCode + " - " + survey.CourseName;
            rptQuestions.DataSource = survey.Questions;
            rptQuestions.DataBind();
        }

        #endregion
    }
}
