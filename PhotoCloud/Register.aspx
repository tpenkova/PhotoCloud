<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="PhotoCloud.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
    <style type="text/css">
        .auto-style1 {
            width: 201px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="nav">
                    <ul>
                        <li><a runat="server" href="Home.aspx">Home</a></li>
                        <li class="right">
                            <asp:Button ID="Button1" class="button" runat="server"  Text="Register" OnClikck ="Register2"/>
                        </li>
                        <li class="right">
                            <asp:Button ID="Button2" class="button" runat="server"  Text="Sign in"  OnClick="SignIn" />
                        </li>
                        
                    </ul>
                </div>

        <div id="register">
            <table>
                <tr>
                    <td class="auto-style1">
                        <asp:Label runat="server" class="user" ID="lbFirstName">First name</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFirstName" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1">
                        <asp:Label runat="server" class="user" ID="lbLastName">Last name</asp:Label>
                    
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtLastName" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1">
                        <asp:Label runat="server" class="user" ID="lbEmail">Email</asp:Label>
                    
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1">
                       <asp:Label runat="server" class="user" ID="lbPass">Password</asp:Label>
                    </td>
                    <td>
                       <asp:TextBox runat="server" ID="txtPass" TextMode="Password" />
                    </td>
                </tr>


                <tr>
                    <td class="auto-style1">
                       <asp:Label runat="server" class="user" ID="lbPass2">Confirm password</asp:Label>
                    
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPass2" TextMode="Password" />
                    </td>
                </tr>
            </table>
                    
              
                    
                    <div style="margin-bottom: 10px">
                        <div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button id="btnRegister" class="button btnLogin" runat="server" Text="Register" OnClick="btnRegister_Click" />
                        </div>
                    </div>
                </div>
    </form>
</body>
</html>
