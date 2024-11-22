<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminManager.aspx.cs" Inherits="EmailProject.AdminManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="AdminStyle.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnLogOut" runat="server" Text="Log Out" OnClick="btnLogOut_Click" />
        <div>
            <br>
            <asp:Button ID="btnEmailManager" runat="server" Text="Manage Email" OnClick="btnEmailManager_Click" />
            <br/>
            <br/>
            <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
            <asp:Panel ID="pnlMessage" runat="server" Visible="False" style="margin-top: 127px">
            <h3>Message Content </h3> <br/>
            <asp:Image ID="avatar" runat="server"  width="50" />
            <h4>From: </h4> 
            <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label><br/>
            <h4>To: </h4> 
            <asp:Label ID="lblTo" runat="server" Text=""></asp:Label><br/>
            <h4>Subject: </h4> 
            <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label><br/>
            <h4>Message: </h4> 
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><br/>
            <h4>Time: </h4> 
            <asp:Label ID="lblTime" runat="server" Text=""></asp:Label> <br/>
            <asp:Button ID="btnBack" runat="server" Text="Close" OnClick="btnBack_Click" />
        </asp:Panel>

            <asp:GridView ID="gvAdmin" runat="server" AutoGenerateColumns="False"  OnSelectedIndexChanged="gvAdmin_SelectedIndexChanged" AutoGenerateSelectButton="True" HorizontalAlign="Center" GridLines="Horizontal" CellPadding="10" CellSpacing="10">
            <Columns>
                <asp:BoundField DataField="SenderEmail" HeaderText="SenderEmail" />
                <asp:BoundField DataField="ReceiverEmail" HeaderText="ReceiverEmail" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                <asp:BoundField DataField="EmailBody" HeaderText="Content" />
                <asp:BoundField DataField="CreatedTime" HeaderText="CreatedTime" />
            </Columns>
            </asp:GridView>
                <br />
                <hr />
                <br />
             <asp:GridView ID="gvAccountInfo" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" GridLines="Horizontal" BorderStyle="None" CellSpacing="10" CellPadding="5">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAccountInfo" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Avatar">
                    <ItemTemplate>
                        <asp:Image ID="imgAvatar" runat="server" ImageUrl='<%# Bind("Avatar") %>' width="40"/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="UserName" HeaderText="UserName" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
                <asp:BoundField DataField="CreatedEmailAddress" HeaderText="Email" />
                <asp:BoundField DataField="Active" HeaderText="Active" />
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnBanned" runat="server" Text="Banned" OnClick="btnBanned_Click" />
        <asp:Button ID="btnUnBanned" runat="server" Text="Un-Banned" OnClick="btnUnBanned_Click" />

        </div>
    </form>
</body>
</html>
