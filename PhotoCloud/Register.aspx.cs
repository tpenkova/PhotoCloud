using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoCloud
{
    public partial class Register : System.Web.UI.Page
    {
        private Users users = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            users.loadUsers();

            DataTable dt = users.DataTableUsers;
            Password ps = new Password();
            ps.SaltedHash(txtPass.Text);

            //row["Salt"] = ps.Salt;
           // row["Pass"] = ps.Hash;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            DataRow dr = users.DataTableUsers.NewRow();
            dr["FirstName"] = txtFirstName.Text;
            dr["LastName"] = txtLastName.Text;
            dr["Email"] = txtEmail.Text;
            
            if(txtPass.Text == txtPass2.Text)
            {
                Password ps = new Password();
                ps.SaltedHash(txtPass.Text);
                dr["Salt"] = ps.Salt;
                dr["Password"] = ps.Hash;
            }
            else
            {
                
            }

            users.DataTableUsers.Rows.Add(dr);

            users.updateUsers();

            Response.Redirect("Albums.aspx?Email="+txtEmail.Text);
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