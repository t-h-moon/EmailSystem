<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailManager.aspx.cs" Inherits="EmailProject.EmailManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EmailManager</title>

     <link rel="stylesheet" href="EmailManagerStyle.css"/>
     <meta charset="utf-8" />
     <meta name="viewport" content="width=device-width, initial-scale=1" />
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="topnav">
            <asp:Panel ID="pnlContent" runat="server">
            <asp:DropDownList ID="ddlMoveTag" runat="server" AutoPostBack ="True" OnSelectedIndexChanged="ddlSelectedIndexChangedMove">
                 <asp:ListItem Selected = "True">Move To --</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnFlagMessage" runat="server" Text="Flag" OnClick="btnFlagMessage_Click" />
            <asp:Button ID="btnUnFlaMessage" runat="server" Text="UnFlag" OnClick="btnUnFlaMessage_Click" />
            <asp:Button ID="btnLogOut" runat="server" Text="Log Out" OnClick="btnLogOut_Click" />
 
            <div class="input-group">
                <span class="input-group-btn">
            <asp:TextBox ID="txtNewTagName" runat="server" class="form-control" placeholder="Add New Folder Name.." Width ="200"></asp:TextBox>
            
            <asp:Button ID="btnAddNewTag" class="btn btn-default" runat="server" Text="Add" OnClick="btnAddNewTag_Click" />
            </span>
            </div>
                
        </asp:Panel>
        </div>
<div class="container-fluid">
  <div class="row content">
    <div class="col-sm-3 sidenav">

      <h2>Welcome</h2>
        <asp:Image ID="imgAvatar" runat="server"  width="80" /> <br />
        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblUserEmail" runat="server" Text=""></asp:Label>
      <ul class="nav nav-pills nav-stacked">
        <asp:Button ID="btnCompose" runat="server" Text="Compose" OnClick="btnCompose_Click" Width="100"/> <br />
        <asp:Button ID="btnInbox" runat="server" Text="Inbox" OnClick="btnInbox_Click" Width="100"/> <br />
        <asp:Button ID="btnSent" runat="server" Text="Sent" OnClick="btnSent_Click" Width="100"/> <br />
        <asp:Button ID="btnFlag" runat="server" Text="Flag" OnClick="btnFlag_Click" Width="100"/> <br />
        <asp:Button ID="btnTrash" runat="server" Text="Trash" OnClick="btnTrash_Click" Width="100"/> <br />
        <asp:DropDownList ID="ddlTags" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectedIndexChanged">
        <asp:ListItem Selected = "True">Other Folders</asp:ListItem>
        </asp:DropDownList>
   
      </ul><br /><hr />

      <div class="input-group">
        <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Search Mail.." ></asp:TextBox>
        <span class="input-group-btn">
          <asp:Button ID="btnSearch" class="btn btn-default" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </span>
      </div>
         <br>
          <br>
    </div>
      
    <div class="col-sm-9">
        <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
   


        <asp:Panel ID="pnlMessage" runat="server" Visible="False" style="margin-top: 127px">
            <h3>Message Content </h3> <br/>
            <asp:Image ID="avatarFrom" runat="server"  width="50" />
            <h4>From: </h4> 
            <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label><br/>
            <h4>Subject: </h4> 
            <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label><br/>
            <h4>Message: </h4> 
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><br/>
            <h4>Time: </h4> 
            <asp:Label ID="lblTime" runat="server" Text=""></asp:Label> <br/>
            <asp:Button ID="btnBack" runat="server" Text="Close" OnClick="btnBack_Click" />
        </asp:Panel>

        <asp:Panel ID="pnlSent" runat="server" Visible="False">
            <h3>Sent Message Content </h3> <br/>
             <asp:Image ID="avatarTo" runat="server"  width="50" />
            <h4>To: </h4> 
            <asp:Label ID="lblTo" runat="server" Text=""></asp:Label> <br/>
            <h4>Subject: </h4> 
            <asp:Label ID="lblSubject2" runat="server" Text=""></asp:Label> <br/>
            <h4>Message: </h4> 
            <asp:Label ID="lblMessage2" runat="server" Text="Message: "></asp:Label> <br/>
            <h4>Time: </h4> 
            <asp:Label ID="lblTime2" runat="server" Text="Time: "></asp:Label> <br/>
            <asp:Button ID="btnBack2" runat="server" Text="Close" OnClick="btnBack2_Click" />
        </asp:Panel>


        <asp:Panel ID="pnlCompost" runat="server" Visible="False">
        <h2>New Message</h2>
        <br/>
        <h4>From: </h4>
        <asp:TextBox ID="txtFrom" runat="server" Width="195px" ReadOnly="True" BackColor="#666666"></asp:TextBox>
        <h4>To: </h4>
        <asp:TextBox ID="txtTo" runat="server" Width="195px" BackColor="#666666"></asp:TextBox>
        <hr />
        <h4>Subject: </h4>
        <asp:TextBox ID="txtSubject" runat="server" Width="776px" BackColor="#666666"></asp:TextBox>
        <h4>Email Content: </h4>
        <asp:TextBox ID="txtMessageBox" runat="server" TextMode="MultiLine" Rows="5" Width="780px" BackColor="#666666"></asp:TextBox><br />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /><%--clears message--%>
            <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
        </asp:Panel>
        
        <div class ="gridView">
        <asp:GridView ID="gvEmail" runat="server" AutoGenerateColumns="False"  AutoPostBack ="True" OnSelectedIndexChanged="gvEmail_SelectedIndexChanged" AutoGenerateSelectButton="True" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="EmailID" HeaderText="" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectInbox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SenderName" HeaderText="From" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                <asp:BoundField DataField="EmailBody" HeaderText="Message" />
                <asp:BoundField DataField="CreatedTime" HeaderText="Time " />
            </Columns>
        </asp:GridView>

         <asp:GridView ID="gvSent" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvSent_SelectedIndexChanged" AutoGenerateSelectButton="True" HorizontalAlign="Center" CellSpacing="20" CellPadding="15">
            <Columns>
               
                <asp:BoundField DataField="ReceiverName" HeaderText="To" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                <asp:BoundField DataField="EmailBody" HeaderText="Message" />
                <asp:BoundField DataField="CreatedTime" HeaderText="Time " />
            </Columns>
        </asp:GridView>
        </ div>


    </div>
  </div>
</div>


</form>
    <div class="footer">
        <p>@farfaraway.com</p>
    </div>

</body>
</html>
