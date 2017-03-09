using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoCloud
{
    public partial class Login : System.Web.UI.Page
    {
        private Users users = new Users();
        Password ps = new Password();

        protected void Page_Load(object sender, EventArgs e)
        {
            users.loadUsers();

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            DataRow dr = users.DataTableUsers.Select("Email = '" + txtEmail.Text + "'")[0];
            string hash = dr["Password"].ToString();
            string salt = dr["Salt"].ToString();
            if(ps.Verify(salt, hash, txtPass.Text))
            {
                Response.Redirect("Albums.aspx?Email="+ txtEmail.Text);
            }
            else
            {

            }
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