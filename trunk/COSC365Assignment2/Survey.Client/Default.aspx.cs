using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Survey.Client.BusinessLayerServiceReference;


namespace Survey.Client
{
    public partial class _Default : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            //SurveyEntity survey = new SurveyEntity();
            //survey.CourseCode = "cosc121";
            //survey.StartDate = DateTime.Now.AddDays(-1);
            //survey.EndDate = DateTime.Now.AddMonths(6);
            //survey.Questions = new List<QuestionEntity>();
            //survey.Questions.Add(new QuestionEntity()
            //                    {
            //                        Text = "is this good?",
            //                        QuestionInstance = new QuestionInstanceEntity()
            //                        {
            //                            Score = 1,
            //                            Comment = "bad"
            //                        }
            //                    });
            //survey.SurveyInstance = new SurveyInstanceEntity()
            //{
            //    CourseCode = survey.CourseCode,
            //    UserCode = "xji25",
            //    DateSubmitted = DateTime.Now
            //};

            
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            Usercode = txtUsercode.Text;
            List<SurveyInstanceEntity> p = new BusinessLayerServiceReference.SurveyBLServicesClient().GetCourseByUserCode(Usercode);
            if (p.Count == 0)
            {
                lblMessage.Text = "User " + Usercode + " does not exist.";
            }
            else
            {
                lblMessage.Visible = false;
                gvCourses.DataSource = p;
                gvCourses.DataBind();
            }
        }

        protected void gvCourses_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row != null) && e.Row.RowType == DataControlRowType.DataRow)
            {
                SurveyInstanceEntity surveyInstance = e.Row.DataItem as SurveyInstanceEntity;
                // TODO: comment out this
                surveyInstance.Role = surveyInstance.Role.Trim();
                if (surveyInstance != null)
                {
                    if (surveyInstance.Status == "Scheduled")
                    {
                        // set link to be unusable
                        ((HyperLink)e.Row.FindControl("lnkCourse")).Enabled = false;
                        return;
                    }
                    if (surveyInstance.Role == "Staff")
                    {
                        // go to survey summary page
                        ((HyperLink)e.Row.FindControl("lnkCourse")).NavigateUrl = "~/SurveySummary.aspx?CourseCode=" + surveyInstance.CourseCode;
                    }
                    else if (surveyInstance.Role == "Student")
                    {
                        // if the survey is open or the user has submitted the survey for the course
                        if (surveyInstance.Status == "Underway" || surveyInstance.Status == "Submitted")
                        {
                            // don't do anything, leave it as default
                        }
                        else if (surveyInstance.Status == "Finished")
                        {
                            // redirect to view summary page
                            ((HyperLink)e.Row.FindControl("lnkCourse")).NavigateUrl = "~/SurveySummary.aspx?CourseCode=" + surveyInstance.CourseCode;
                        }
                    }
                }
            }
        }

    }
}
