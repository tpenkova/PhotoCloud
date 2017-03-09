using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoCloud
{
    public partial class Photo : System.Web.UI.Page
    {
        Photos photos = new Photos();
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = Request.QueryString["FileName"];
            photos.loadPhotos();

            DataRow dr = photos.DataTablePhotos.Select("FileName = '" + fileName + "'")[0];

            string strBase64 = Convert.ToBase64String((byte[])dr["Data"]);
       
            Image image = new Image();
            image.Width = 100;
            image.Height = 100;
            image.ImageUrl = "data:Image/png;base64," + strBase64;

        }
    }
}