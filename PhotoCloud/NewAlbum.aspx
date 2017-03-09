<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAlbum.aspx.cs" Inherits="PhotoCloud.NewAlbum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
    <style type="text/css">
        .auto-style1 {
            width: 91px;
        }
        .auto-style2 {
            width: 91px;
            height: 26px;
        }
        .auto-style3 {
            height: 26px;
        }
    </style>
</head>
<body id="newAlbum">
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
        <div id="tableNew">
        <table style="width: 48%; margin-left: 0px;">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lbName" runat="server" Text="Name"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtName" runat="server" Width="176px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lbDescription" runat="server" Text="Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>
                     <asp:Button ID="create" class="button save" runat="server" Text="Save" OnClick="create_Click" />
                </td>
            </tr>
        </table>
       
            </div>
    </div>
    </form>
</body>
</html>
