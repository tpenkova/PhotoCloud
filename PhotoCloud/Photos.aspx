<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" MaintainScrollPositionOnPostBack="true" Inherits="PhotoCloud.Photos1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body id="photos">
    <form id="form1" runat="server">
    <div >
        <asp:ImageButton ID="logo" runat="server" ImageUrl="~/Pictures/photoCloud.png" />
        <asp:LinkButton ID="logOut" runat="server" OnClick="SignOut">Sign out</asp:LinkButton>
        <asp:Label ID="lbWelcome" runat="server" Text="Label"></asp:Label>
        <div class="clear">
        </div>
        <div id="navPhotos">
            <asp:FileUpload ID="fileUpload" runat="server" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="btnUploadPhoto_Click" style="margin-left: 0px" Text="Upload photo" />
        </div>
        <div class="clear"></div>
        <div id="categories"> 
            Categories
            <ul id="menu" runat="server">
            </ul>
        </div>
        <div id="table">
            <asp:Table ID="myTable" runat="server" Width="70%"> 
        <asp:TableRow>
            <asp:TableCell> </asp:TableCell>
            <asp:TableCell>Name</asp:TableCell>
            <asp:TableCell>Modified</asp:TableCell>
            <asp:TableCell>Members</asp:TableCell>
        </asp:TableRow>
        </asp:Table>  
        </div>
    </div>

  
    </form>
</body>
</html>
