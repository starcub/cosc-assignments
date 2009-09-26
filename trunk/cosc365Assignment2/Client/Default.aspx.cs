using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Client.BusinessLayerServiceReference;

namespace Client
{
    public partial class _Default : System.Web.UI.Page
    {
        BusinessLayerServiceClient client = new BusinessLayerServiceClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            lblName.Text = client.GetData(int.Parse(txtName.Text));
        }
    }
}
