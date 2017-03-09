using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;

namespace PhotoCloud
{
    public partial class Photos1 : System.Web.UI.Page
    {
        Photos photo = new Photos();
        Categories categories = new Categories();
        Users users = new Users();
        Albums albums = new Albums();
        int albumId;
        string albumName;
        int userId;
        bool firstTime = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!firstTime)
            {
                categories.loadCategories();
                albums.loadAlbums();
                users.loadUsers();

                userId = Convert.ToInt32(Request.QueryString["Id"]);
                albumName = Request.QueryString["AlbumName"];
                DataRow drUser = users.DataTableUsers.Select("UserID = " + userId)[0];
                string firstName = drUser["FirstName"].ToString();
                string lastName = drUser["LastName"].ToString();

                lbWelcome.Text = "Welcome, " + firstName + " " + lastName;

                albumId = Convert.ToInt32(albums.DataTableAlbums.Select("Name = '" + albumName + "'")[0]["AlbumID"]);
                firstTime = true;
            }

            if(!IsPostBack)
            {
               
                loadPhotos();
            }
        }

        private void loadPhotos()
        {
            //photo.loadPhotos();
            categories.loadCategories();
            albums.loadAlbums();
            users.loadUsers();

            photo.loadPhotos(albumId);
            foreach (DataRow dr in photo.DataTablePhotos.Rows)
            {
                string strBase64 = Convert.ToBase64String((byte[])dr["Data"]);

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                Image image = new Image();
                image.Width = 50;
                image.Height = 50;
                image.ImageUrl = "data:Image/png;base64," + strBase64;
                cell1.Controls.Add(image);

                TableCell cell2 = new TableCell();
                LinkButton linkBtn = new LinkButton();
                linkBtn.Text = dr["FileName"].ToString();
                linkBtn.Click += new EventHandler((s, eArgs) => linkBtn_Click(s, eArgs, dr["FileName"].ToString()));
                cell2.Controls.Add(linkBtn);


                TableCell cell3 = new TableCell();
                if (dr["Date"].ToString() != "")
                {
                    DateTime date = Convert.ToDateTime(dr["Date"]);
                    cell3.Text = date.ToShortDateString();
                }
                else
                {
                    cell3.Text = "--";
                }
                //TableCell cell2 = new TableCell();
                //cell2.Text = dr["FileName"].ToString();
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                myTable.Rows.Add(row);
            }

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
        protected void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            photo.loadPhotos();
            HttpPostedFile postedFile = fileUpload.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;
            if (fileSize < 102400)
            {
                if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif" ||
                    fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".png")
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));


                    DataRow dr = photo.DataTablePhotos.NewRow();
                    dr["AlbumID"] = albumId;
                    dr["FileName"] = fileName;
                    dr["Data"] = bytes;
                    dr["Width"] = image.Width;
                    dr["Height"] = image.Height;
                    photo.DataTablePhotos.Rows.Add(dr);
                    stream.Close();
                    photo.updatePhotos();
                    photo.loadPhotos();
                    loadPhotos();

                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            
        }

        protected void SignOut(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }


        private void linkBtn_Click(object sender, EventArgs e, string fileName)
        {
            Response.Redirect(String.Format("Photo.aspx?FileName={0}&Id={1}", fileName, userId));
        }
    }
}