<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmailProject.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="LogInPageStyle.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
           <%-- <link rel="stylesheet" href="SignInStyle.css"/>--%>
            <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblSignIn" runat="server" Text="Please Sign In"></asp:Label><br />
            <asp:Label ID="lblUserEmail" runat="server" Text="User Email Address: " ></asp:Label>
            <asp:TextBox ID="txtEmailAddress" runat="server" ></asp:TextBox><br />
            <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
            <asp:TextBox ID="txtPassword" type="Password" runat="server" ></asp:TextBox><br />

            <asp:Button ID="btnSignIn" runat="server" Text="Sign In" OnClick="btnSignIn_Click"  Width="120"/>
            <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" OnClick="btnCreateAccount_Click" Width="120"/>
        </div>
    </form>
</body>
</html>
