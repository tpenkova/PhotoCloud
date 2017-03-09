<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="PhotoCloud.Photo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body id="photo">
    <form id="form1" runat="server">
    <div >
        <asp:ImageButton ID="logo" runat="server" ImageUrl="~/Pictures/photoCloud.png" />
        <asp:LinkButton ID="logOut" runat="server" OnClick="SignOut">Sign out</asp:LinkButton>
        <asp:Label ID="lbWelcome" runat="server" Text="Label"></asp:Label>
        <div class="clear">
        </div>
      
        <div id="categories"> 
            Categories
            <ul id="menu" runat="server">
            </ul>
        </div>
        
        <asp:Image ID="Image1" runat="server" />
   
    </div>
    </form>
</body>
</html>
