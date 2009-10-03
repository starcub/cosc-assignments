using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Survey.DataEntities;


namespace Survey.Client
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SurveyEntity survey = new SurveyEntity();
            survey.CourseCode = "cosc121";
            survey.StartDate = DateTime.Now.AddDays(-1);
            survey.EndDate = DateTime.Now.AddMonths(6);
            survey.Questions = new List<QuestionEntity>();
            survey.Questions.Add(new QuestionEntity()
                                {
                                    Text = "is this good?",
                                    QuestionInstance = new QuestionInstanceEntity()
                                    {
                                        Score = 1,
                                        Comment = "bad"
                                    }
                                });
            survey.SurveyInstance = new SurveyInstanceEntity()
            {
                CourseCode = survey.CourseCode,
                UserCode = "xji25",
                DateSubmitted = DateTime.Now
            };

        }
    }
}
