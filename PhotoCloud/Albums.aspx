<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Albums.aspx.cs" Inherits="PhotoCloud.Albums1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body id="albums">
    <form id="form1" runat="server">
    <div>
        <asp:ImageButton ID="logo" runat="server" ImageUrl="~/Pictures/photoCloud.png" />
        <asp:LinkButton ID="logOut" runat="server" OnClick="SignOut">Sign out</asp:LinkButton>
        <asp:Label ID="lbWelcome" runat="server" Text="Label"></asp:Label>
        <div class="clear">
        </div>
        <div id="navAlbums">
            <asp:Button ID="btnNewAlbum" class= "button" runat="server" Text="New album" OnClick="btnNewAlbum_Click" />
            <asp:Button ID="btnShare" class= "button" runat="server" Text="Share" />
        </div>
        <div class="clear"></div>
        <div id="categories"> 
            Categories
            <ul id="menu" runat="server">
            </ul>
        </div>
        <div id="table">
            <asp:Table ID="myTable" runat="server" Width="70%" style="margin-left: 47px; margin-top: 0px"> 
                <asp:TableRow>
                    <asp:TableCell> </asp:TableCell>
                    <asp:TableCell CssClass="headerCell">Name</asp:TableCell>
                    <asp:TableCell CssClass="headerCell">Modified</asp:TableCell>
                    <asp:TableCell CssClass="headerCell">Members</asp:TableCell>
                </asp:TableRow>
            </asp:Table>  
        </div>
    </div>
    </form>
</body>
</html>
