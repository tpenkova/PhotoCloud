using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PhotoCloud
{
    public partial class NewAlbum : System.Web.UI.Page
    {
        Categories categories = new Categories();
        Albums albums = new Albums();
        Users users = new Users();
        private int userId;
        private string email;

        protected void Page_Load(object sender, EventArgs e)
        {
            users.loadUsers();
            users.loadUsersAlbums(userId);
            categories.loadCategories();

           userId = Convert.ToInt32(Request.QueryString["Id"]);
           DataRow dr = users.DataTableUsers.Select("UserID = " + userId)[0];
           string userName = dr["FirstName"].ToString() + " " + dr["LastName"];
           email = dr["Email"].ToString();
            
           lbWelcome.Text = "Welcome, " +  userName;

           foreach (DataRow drCat in categories.DataTableCategories.Rows)
           {
               HtmlGenericControl li = new HtmlGenericControl("li");
               menu.Controls.Add(li);

               HtmlGenericControl anchor = new HtmlGenericControl("a");
               anchor.Attributes.Add("href", "");
               anchor.InnerText = drCat["Name"].ToString();

               li.Controls.Add(anchor);
           }
        }

        protected void SignOut(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnNewAlbum_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewAlbum.aspx");
        }

        private List<UsersAlbums> listUA;
        protected void create_Click(object sender, EventArgs e)
        {
            listUA = new List<UsersAlbums>();
            DataRow dr = albums.DataTableAlbums.NewRow();
            dr["AlbumID"] = -1;
            dr["Name"] = txtName.Text;
            dr["Description"] = txtDescription.Text;
            dr["DateModified"] = DateTime.Now;
            albums.DataTableAlbums.Rows.Add(dr);

            UsersAlbums ua = new UsersAlbums();
            ua.UserId = userId;
            listUA.Add(ua);

            albums.updateAlbums(listUA);

            Response.Redirect("Albums.aspx?Email="+ email);
        }

    }
}