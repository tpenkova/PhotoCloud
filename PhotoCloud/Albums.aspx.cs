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
    
    public partial class Albums1 : System.Web.UI.Page
    {
        private int userId;
        Users users = new Users();
        Photos photos = new Photos();
        Categories categories = new Categories();

        protected void Page_Load(object sender, EventArgs e)
        {
            users.loadUsers();
            categories.loadCategories();

            string email = Request.QueryString["Email"];
            DataRow drUser = users.DataTableUsers.Select("Email = '" + email + "'")[0];
            string firstName = drUser["FirstName"].ToString();
            string lastName = drUser["LastName"].ToString();
            userId = Convert.ToInt32(drUser["UserID"]);

            lbWelcome.Text = "Welcome, " + firstName + " " + lastName;

            users.loadUsersAlbums(userId);
            foreach(DataRow dr in users.DataTableAlbums.Rows)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                Image img = new Image();
                img.ImageUrl = "Pictures/folder_images_blue.png";
                img.Width = 40;
                img.Height = 40;
                cell.Controls.Add(img);

                TableCell cell1 = new TableCell();
                LinkButton linkBtn = new LinkButton();
                linkBtn.Text = dr["Name"].ToString();
                linkBtn.Click += new EventHandler((s, eArgs) => linkBtn_Click(s, eArgs, dr["Name"].ToString()));
                cell1.Controls.Add(linkBtn);

                TableCell cell2 = new TableCell();
                if(dr["DateModified"].ToString() != "")
                {
                    DateTime date = Convert.ToDateTime(dr["DateModified"]);
                    cell2.Text = date.ToShortDateString();
                }
                else
                {
                    cell2.Text = "--";
                }
                row.Cells.Add(cell);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                myTable.Rows.Add(row);
            }


            foreach(DataRow drCat in categories.DataTableCategories.Rows)
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
            Response.Redirect("NewAlbum.aspx?Id=" + userId);
        }

        private void linkBtn_Click(object sender, EventArgs e,string albumName)
        {
            Response.Redirect(String.Format("Photos.aspx?AlbumName={0}&Id={1}", albumName, userId));
        }
    }
}