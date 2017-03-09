using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoCloud
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignIn(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Register2(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}