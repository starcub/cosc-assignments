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
                return Request.QueryString["CourseCode"];
            }
        }

        public string Usercode
        {
            get
            {
                if (Session["UserCode"] != null)
                {
                    return Session["UserCode"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Session["UserCode"] = value;
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
            if (!IsPostBack)
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
        }

        /// <summary>
        /// Submit the survey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            // gets the data from the page
            SurveyInstanceEntity survey = PopulateDataFromControl();
            survey.SurveyInstanceID = client.InsertSurveyInstance(survey);


        }

        private SurveyInstanceEntity PopulateDataFromControl()
        {
            // instantiate a survey object and its questions
            SurveyInstanceEntity survey = new SurveyInstanceEntity();
            survey.Questions = new List<QuestionInstanceEntity>();
            survey.CourseCode = CourseCode;
            survey.UserCode = Usercode;
            survey.DateSubmitted = DateTime.Now;
            // loop through the repeater
            foreach (RepeaterItem item in rptQuestions.Items)
            {
                // if current item is actual data item
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    // Instantiate a new question object
                    QuestionInstanceEntity question = new QuestionInstanceEntity();
                    // find the radio button in side the repeater
                    RadioButtonList rbl = (RadioButtonList)item.FindControl("rblScore");
                    question.QuestionID = int.Parse(((HiddenField)item.FindControl("hidQuestionID")).Value);
                    question.Score = (int.Parse(rbl.SelectedValue));
                    question.Comment = (((TextBox)item.FindControl("txtComment")).Text);
                    // add question object into survey object;
                    survey.Questions.Add(question);
                }
            }
            return survey;
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
            SurveyInstanceEntity survey = client.GetSurveyByCourseCode(CourseCode);
            SurveyTitle = survey.CourseCode + " - " + survey.CourseName;
            rptQuestions.DataSource = survey.Questions;
            rptQuestions.DataBind();
        }

        #endregion
    }
}
