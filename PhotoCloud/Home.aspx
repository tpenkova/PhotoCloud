<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PhotoCloud.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhotoCloud</title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="holder">
            
                <div id="nav">
                    <ul>
                        <li><a runat="server" href="Home.aspx">Home</a></li>
                        <li class="right">
                            <asp:Button ID="Button1" class="button" runat="server"  Text="Register" OnClick="Register2"/>
                        </li>
                        <li class="right">
                            <asp:Button ID="Button2" class="button" runat="server"  Text="Sign in"  OnClick="SignIn" />
                        </li>
                        
                    </ul>
                </div>
                <div class="clear"></div>

                <div id="welcome">
                    <div id="big">Welcome to</div>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Pictures/photoCloud.png" />
                    <div>PhotoCloud is free cloud platform that offers storage of photos. There's no limit of photos and is absolutely free. You can upload your photos from anywhere and you can be sure that they won't be lost.</div>
                </div>
        </div>
    </form>
</body>
</html>
