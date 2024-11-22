<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountCreation.aspx.cs" Inherits="EmailProject.AccountCreation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" href="AccountCreateStyle.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnLogOut" runat="server" Text="Log Out" OnClick="btnLogOut_Click" />

        <div>
            <asp:Label ID="lblAlert" runat="server" Text="" Width="200"></asp:Label><br />
            <asp:Label ID="lblUserName" runat="server" Text="User Name:" Width="200"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" Width="200" placeholder="EX: StarWarsFreak"></asp:TextBox><br />
            <asp:Label ID="lblAddress" runat="server" Text="Address:" Width="200"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" Width="200" placeholder="EX: 1010 Tatooine" OnTextChanged="txtAddress_TextChanged"></asp:TextBox><br />
            <asp:Label ID="lblPhoneNumber" runat="server" Text="PhoneNumber:" Width="200" ></asp:Label>
            <asp:TextBox ID="txtPhoneNumber" runat="server" Width="200" placeholder="EX: 0000000000"></asp:TextBox><br />
            <asp:Label ID="lblUserEmail" runat="server" Text="Create Your Email:" Width="200"></asp:Label>
            <asp:TextBox ID="txtUserEmail" runat="server" Width="200" placeholder="EX: ...@farfaraway"></asp:TextBox><br />
            <asp:Label ID="lblRecoverEmail" runat="server" Text="Recover Email:" Width="200"></asp:Label>
            <asp:TextBox ID="txtRecoverEmail" runat="server" Width="200" placeholder="Just in case"></asp:TextBox><br />
            <asp:Label ID="lblPassword" runat="server" Text="Password:" Width="200"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" Width="200" placeholder="Don't Share!"></asp:TextBox><br />
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" Width="200"></asp:Label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" Width="200" placeholder="Make sure it matches"></asp:TextBox><br />
            
            <hr />

            <label for="avatar" >Select an avatar:</label><br />
            <asp:Image ID="imgAvatar" runat="server" ImageUrl ="~/imgs/default.PNG" width="70" /><br />
            <asp:DropDownList ID="ddlAvatar" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectedIndexChanged">
                <asp:ListItem>Princess</asp:ListItem>
                <asp:ListItem>Soldier</asp:ListItem>
                <asp:ListItem>BountyHunter</asp:ListItem>
                <asp:ListItem>Loyal</asp:ListItem>
                <asp:ListItem>Evil</asp:ListItem>
                <asp:ListItem>Server</asp:ListItem>
                <asp:ListItem>Fixer</asp:ListItem>
                <asp:ListItem>Wise</asp:ListItem>
                <asp:ListItem>Gangster</asp:ListItem>
                <asp:ListItem>Cuddly</asp:ListItem>
                <asp:ListItem>Deadly</asp:ListItem>
                <asp:ListItem>Jeti</asp:ListItem>
                <asp:ListItem>Admin</asp:ListItem>
                <asp:ListItem Selected = "True">Default</asp:ListItem>
            </asp:DropDownList>

            <hr />

            <asp:Label ID="lblUserType" runat="server" Text="Please Select User Type: "/><br />
            <asp:RadioButtonList ID="rblUserType" runat="server">
            <asp:ListItem Selected="True" Value ="User" >User</asp:ListItem>
            <asp:ListItem Value="Admin" >Admin</asp:ListItem>
            </asp:RadioButtonList>

            <asp:Button ID="btnCreateAccount" runat="server" Text="CreateAccount" OnClick="btnCreateAccount_Click" />

        </div>
    </form>
</body>
</html>
