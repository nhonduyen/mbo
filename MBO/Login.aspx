<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MBO.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head id="Head1" runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <!-- Bootstrap -->

    <link rel="stylesheet" href="Content/bt/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/login.css" />
    <%:System.Web.Optimization.Scripts.Render("~/Scripts/bundle") %>
    <script>
        $(document).ready(function () {
            $.backstretch("Content/img/mkt5.jpg");
        });
    </script>
</head>
<body>
    <h1 style="text-align: center; color: white;">MBO SYSTEM MANGEMENT</h1>
    <form id="form1" runat="server" class="form-signin">
        <h2 class="form-signin-heading">Please sign in</h2>
        <label for="inputEmail" class="sr-only">Email address</label>
        <asp:TextBox ID="txtEmID" runat="server" class="form-control" placeholder="Employee Number" autofocus="autofocus" required="required"></asp:TextBox>
        <label for="inputPassword" class="sr-only">Password</label>
        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control" placeholder="Password" required="required"></asp:TextBox>
        <div class="checkbox">
        </div>
        <asp:Button ID="btnLogin" runat="server" Text="Sign In" class="btn btn-lg btn-primary btn-block" OnClick="btnLogin_Click" />
    </form>
</body>
</html>
