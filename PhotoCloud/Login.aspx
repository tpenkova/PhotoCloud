<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhotoCloud.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 76px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

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


        <div id="signIn">
                    <asp:Label runat="server" class="user">Email</asp:Label>
                    <div>
                      <asp:TextBox runat="server" ID="txtEmail" />
                    </div>
                    <asp:Label runat="server" class="user">Password</asp:Label>
                    <div>
                      <asp:TextBox runat="server" ID="txtPass" TextMode="Password" />
                    </div>
                
                    <div style="margin-bottom: 10px">
                       <div>
                          <asp:Button ID="btnSignIn" runat="server" OnClick="btnSignIn_Click" Text="Sign in" />
                       </div>
                    </div>
                </div>
    </div>
    </form>
</body>
</html>
