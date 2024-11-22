//Tina Mooon THM@gmail.com 12345
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EmailLibrary;
using EmailLibrary.Model;
using Utilities;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Data.SqlClient;

namespace EmailProject
{
    public partial class EmailManager : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        Validation validate = new Validation();
        Utility utility = new Utility();
        String createdTime = DateTime.Now.ToShortDateString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlCompost.Visible = false;
                gvEmail.Visible = true;
                Session["Tag"] = "Inbox";
                showInbox(Session["Tag"].ToString());
                ShowUserContent();
                populateTags();
            }
        }

        protected void btnCompose_Click(object sender, EventArgs e)
        {
            pnlCompost.Visible = true; 
            gvEmail.Visible = false;
            gvSent.Visible = false;
            pnlMessage.Visible = false;
            pnlSent.Visible = false;
            txtTo.Focus();
            txtFrom.Text = (String)Session["CreatedEmailAddress"];
            ddlMoveTag.Visible = false;
            btnFlagMessage.Visible = false;
            btnUnFlaMessage.Visible = false;
            txtNewTagName.Visible = false;
            btnAddNewTag.Visible = false;
        }

        protected void btnInbox_Click(object sender, EventArgs e)
        {
            pnlCompost.Visible = false;
            gvEmail.Visible = true;
            gvSent.Visible = false;
            pnlMessage.Visible = false;
            pnlSent.Visible = false;
            Session["Tag"] = "Inbox";
            showInbox(Session["Tag"].ToString());
            btnBack.Visible = false;
            ddlMoveTag.Visible = true;
            btnFlagMessage.Visible = true;
            btnUnFlaMessage.Visible = true;
            txtNewTagName.Visible = true;
            btnAddNewTag.Visible = true;
        }
        protected void btnSent_Click(object sender, EventArgs e)
        {
            pnlCompost.Visible = false;
            gvEmail.Visible = false;
            gvSent.Visible = true;
            pnlMessage.Visible = false;
            pnlSent.Visible = false;
            showSent();
            btnBack.Visible = false;
            ddlMoveTag.Visible = false;
            btnFlagMessage.Visible = false;
            btnUnFlaMessage.Visible = false;
            txtNewTagName.Visible = false;
            btnAddNewTag.Visible = false;
        }

        protected void btnFlag_Click(object sender, EventArgs e)
        {
            pnlCompost.Visible = false;
            gvEmail.Visible = true;
            pnlSent.Visible = false;
            pnlMessage.Visible = false;
            gvSent.Visible = false;
            Session["Tag"] = "Flag";
            showFlag(Session["Tag"].ToString());
            btnBack.Visible = false; ddlMoveTag.Visible = true;
            btnFlagMessage.Visible = true;
            btnUnFlaMessage.Visible = true;
            txtNewTagName.Visible = true;
            btnAddNewTag.Visible = true;
        }

        protected void btnTrash_Click(object sender, EventArgs e)
        {
            pnlCompost.Visible = false;
            gvEmail.Visible = true;
            gvSent.Visible = false;
            pnlMessage.Visible = false;
            pnlSent.Visible = false;
            Session["Tag"] = "Trash";
            showTrash(Session["Tag"].ToString());
            btnBack.Visible = false;
            ddlMoveTag.Visible = true;
            btnFlagMessage.Visible = true;
            btnUnFlaMessage.Visible = true;
            txtNewTagName.Visible = true;
            btnAddNewTag.Visible = true;
        }

        protected void ddlSelectedIndexChanged(object sender, EventArgs e)
        {
            gvEmail.Visible = true;
            gvSent.Visible = false;
            String tag = ddlTags.SelectedValue;
            showInbox(tag);
            ddlMoveTag.Visible = true;
            btnFlagMessage.Visible = true;
            btnUnFlaMessage.Visible = true;
            txtNewTagName.Visible = true;
            btnAddNewTag.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pnlCompost.Visible = false;
            gvEmail.Visible = true;
            gvSent.Visible = false;
            pnlMessage.Visible = false;
            pnlSent.Visible = false;
            showSearch();
            ddlMoveTag.Visible = true;
            btnFlagMessage.Visible = true;
            btnUnFlaMessage.Visible = true;
            txtNewTagName.Visible = true;
            btnAddNewTag.Visible = true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlMessage.Visible = false;
            gvEmail.Visible = true;
            gvSent.Visible = false;
            Session["Tag"] = "Inbox";
            showInbox(Session["Tag"].ToString());
            ddlMoveTag.Visible = true;
            btnFlagMessage.Visible = true;
            btnUnFlaMessage.Visible = true;
            txtNewTagName.Visible = true;
            btnAddNewTag.Visible = true;
        }

        protected void gvSent_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSent.Visible = true;
            btnBack2.Visible = true;
            String to = gvSent.SelectedRow.Cells[1].Text;
            String subject = gvSent.SelectedRow.Cells[2].Text;
            String message = gvSent.SelectedRow.Cells[3].Text;
            String time = gvSent.SelectedRow.Cells[4].Text;

            lblTo.Text = to;
            lblSubject2.Text = subject;
            lblMessage2.Text = message;
            lblTime2.Text = time;
            string imgPath = utility.GetAvatar(to);
            avatarTo.ImageUrl = imgPath;
            gvSent.Visible = false;
        }

        protected void btnBack2_Click(object sender, EventArgs e)
        {
            pnlSent.Visible = false;
            gvSent.Visible = true;
            showSent();
            ddlMoveTag.Visible = true;
            btnFlagMessage.Visible = true;
            btnUnFlaMessage.Visible = true;
            txtNewTagName.Visible = true;
            btnAddNewTag.Visible = true;
        }

        protected void btnFlagMessage_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(Session["UserID"].ToString());
            CheckBox Cbox;
            for (int i = 0; i < gvEmail.Rows.Count; i++)
            {
                Cbox = (CheckBox)gvEmail.Rows[i].FindControl("cbSelectInbox");
                if (Cbox.Checked == true)
                {
                    int emailId = int.Parse(gvEmail.Rows[i].Cells[1].Text);

                    int tagID = utility.GetTagID(userID, "Flag");
                    utility.UpdateTagID(tagID, emailId);
                    utility.FlagEmail(userID, emailId);
                }
            }
            showFlag("Inbox");
        }

        protected void btnUnFlaMessage_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(Session["UserID"].ToString());
            CheckBox Cbox;
            for (int i = 0; i < gvEmail.Rows.Count; i++)
            {
                Cbox = (CheckBox)gvEmail.Rows[i].FindControl("cbSelectInbox");
                if (Cbox.Checked == true)
                {
                    int emailId = int.Parse(gvEmail.Rows[i].Cells[1].Text);
                    String senderUserName = gvEmail.Rows[i].Cells[3].Text;

                    int tagID = utility.GetTagID(userID, "Inbox");
                    utility.UpdateTagID(tagID, emailId);
                    utility.UnFlagEmail(userID, emailId);
                }
            }
            showFlag("Inbox");
        }
        protected void gvEmail_SelectedIndexChanged(object sender, EventArgs e)
        {//changed
            pnlMessage.Visible = true;
            btnBack.Visible = true;
            String from = gvEmail.SelectedRow.Cells[3].Text;
            String subject = gvEmail.SelectedRow.Cells[4].Text;
            String message = gvEmail.SelectedRow.Cells[5].Text;
            String time = gvEmail.SelectedRow.Cells[6].Text;

            lblFrom.Text = from;
            lblSubject.Text = subject;
            lblMessage.Text = message;
            lblTime.Text = time;
            string imgPath = utility.GetAvatar(from);
            avatarFrom.ImageUrl = imgPath;
            gvEmail.Visible = false;
        }

        protected void ddlSelectedIndexChangedMove(object sender, EventArgs e)
        {
            Session["Tag"] = ddlMoveTag.SelectedValue;
            gridViewLookUp();
        }

        public void gridViewLookUp()
        {
            String tagName = Session["Tag"].ToString();
            moveGVemail(tagName);
            showInbox("Inbox");
        }

        public void moveGVemail(String tagName)
        {
            int userID = int.Parse(Session["UserID"].ToString());
            CheckBox Cbox;
            for (int i = 0; i < gvEmail.Rows.Count; i++)
            {
                Cbox = (CheckBox)gvEmail.Rows[i].FindControl("cbSelectInbox");
                if (Cbox.Checked == true)
                {
                    int emailId = int.Parse(gvEmail.Rows[i].Cells[1].Text);

                    int tagID = utility.GetTagID(userID, tagName);
                    utility.UpdateTagID(tagID, emailId);
                }
            }
            populateTags();
        }

        public void ShowUserContent() 
        {
            lblUserName.Text = (string)Session["UserName"];
            imgAvatar.ImageUrl = (string)Session["Avatar"];
            lblUserEmail.Text = (string)Session["CreatedEmailAddress"];
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (validate.BlankNewEmail(txtTo.Text, txtSubject.Text, txtMessageBox.Text) == false)
            {
                lblAlert.Text = "Please do not leave any boxes blank";
            }
            else
            {
                lblAlert.Text = "";
                int receiverID = utility.GetUserIdByEmail(txtTo.Text);
                String userEmail = (String)Session["CreatedEmailAddress"];

                if (receiverID == -1)
                {
                    Response.Write("<script>alert('This user does not exist. Please make sure you are sending to a user that exist in the system')</script>");
                    txtTo.Text = "";
                    txtTo.Focus();
                }
                else
                {
                    int userID = int.Parse(Session["UserID"].ToString());
                    int tagId = utility.GetTagID(receiverID, "Inbox");
                    int emailID = utility.InsertNewEmail(userID, receiverID, txtSubject.Text, txtMessageBox.Text, createdTime);

                    utility.InsertEmailReceipt(receiverID, emailID, tagId, 0);
                    Response.Write("<script>alert('Your Message has been sent')</script>");
                    ClearCompose();
                }
            } 
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClearCompose();
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.Aspx");
        }

        public void ClearCompose()
        {
            txtTo.Text = "";
            txtTo.Focus();
            txtSubject.Text = "";
            txtMessageBox.Text = "";
        }

        public void showInbox(String tagName)
        {
            int userID = int.Parse(Session["UserID"].ToString());
            int tagID = utility.GetTagID(userID, tagName);

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ShowInboxGV";
            objCommand.Parameters.AddWithValue("@theTagId", tagID);
            objCommand.Parameters.AddWithValue("@theUserId", userID);

            DataSet myInboxData;
            myInboxData = dBConnect.GetDataSetUsingCmdObj(objCommand);
            int dataSize = myInboxData.Tables[0].Rows.Count;
            if (dataSize > 0)
            {
                lblAlert.Text = "";
                ArrayList emailClassList = new ArrayList();
                for (int i = 0; i < dataSize; i++)
                {
                    String senderID = myInboxData.Tables[0].Rows[i]["SenderID"].ToString();

                    objCommand.Parameters.Clear();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "EmailInfoByUserID";
                    objCommand.Parameters.AddWithValue("@theUserId", senderID);
                    DataSet mySenderData;
                    mySenderData = dBConnect.GetDataSetUsingCmdObj(objCommand);
                 
                    EmailClass email = new EmailClass();
                    email.EmailId = int.Parse(myInboxData.Tables[0].Rows[i]["EmailID"].ToString());
                    email.SenderName = mySenderData.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = myInboxData.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = myInboxData.Tables[0].Rows[i]["EmailBody"].ToString();
                    String createdTime = myInboxData.Tables[0].Rows[i]["CreatedTime"].ToString();
                    email.CreatedTime = DateTime.Parse(createdTime).ToShortDateString();

                    emailClassList.Add(email);
                }
                gvEmail.DataSource = emailClassList;
                gvEmail.DataBind();
            }
            else
            {
                lblAlert.Text = "Your " + tagName + " is Empty";
                gvEmail.Visible = false;
            }
        }

        public void showSent()
        {
            int userID = int.Parse(Session["UserID"].ToString());

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ShowSentGV";
            objCommand.Parameters.AddWithValue("@theUserId", userID);
            DataSet mySentData;
            mySentData = dBConnect.GetDataSetUsingCmdObj(objCommand);

            int dataSize = mySentData.Tables[0].Rows.Count;
            if (dataSize > 0)
            {
                lblAlert.Text = "";
                ArrayList emailClassList = new ArrayList();
                for (int i = 0; i < dataSize; i++)
                {
                    String receiverID = mySentData.Tables[0].Rows[i]["ReceiverID"].ToString();
                    objCommand.Parameters.Clear();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "EmailInfoByUserID";
                    objCommand.Parameters.AddWithValue("@theUserId", receiverID);
                    DataSet myRecieverData;
                    myRecieverData = dBConnect.GetDataSetUsingCmdObj(objCommand);

                    EmailClass email = new EmailClass();
                    email.ReceiverName = myRecieverData.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = mySentData.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = mySentData.Tables[0].Rows[i]["EmailBody"].ToString();
                    String createdTime = mySentData.Tables[0].Rows[i]["CreatedTime"].ToString();
                    email.CreatedTime = DateTime.Parse(createdTime).ToShortDateString();

                    emailClassList.Add(email);
                }
                gvSent.DataSource = emailClassList;
                gvSent.DataBind();
            }
            else
            {
                lblAlert.Text = "Your Sent is Empty";
                gvSent.Visible = false;
            }
        }

        public void showFlag(String tagName) 
        {
            int userID = int.Parse(Session["UserID"].ToString());
            int emailTagID = utility.GetTagID(userID, tagName);

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ShowFlagGV";
            objCommand.Parameters.AddWithValue("@theUserId", userID);
            DataSet myFlagData;
            myFlagData = dBConnect.GetDataSetUsingCmdObj(objCommand);
            int dataSize = myFlagData.Tables[0].Rows.Count;

            if (dataSize > 0)
            {
                lblAlert.Text = "";
                ArrayList emailClassList = new ArrayList();
                for (int i = 0; i < dataSize; i++)
                {
                    int senderID = int.Parse(myFlagData.Tables[0].Rows[i]["SenderID"].ToString());
                    objCommand.Parameters.Clear();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "EmailInfoByUserID";
                    objCommand.Parameters.AddWithValue("@theUserId", senderID);
                    DataSet mySenderData;
                    mySenderData = dBConnect.GetDataSetUsingCmdObj(objCommand);

                    EmailClass email = new EmailClass();
                    email.EmailId = int.Parse(myFlagData.Tables[0].Rows[i]["EmailID"].ToString());
                    email.SenderName = mySenderData.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = myFlagData.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = myFlagData.Tables[0].Rows[i]["EmailBody"].ToString();
                    String createdTime = myFlagData.Tables[0].Rows[i]["CreatedTime"].ToString();
                    email.CreatedTime = DateTime.Parse(createdTime).ToShortDateString();

                    emailClassList.Add(email);
                }
                gvEmail.DataSource = emailClassList;
                gvEmail.DataBind();
            }
            else
            {
                lblAlert.Text = "Your " + tagName + " is Empty";
                gvEmail.Visible = false;
            }
        }

        public void showTrash(String tagName)
        {
            int userID = int.Parse(Session["UserID"].ToString());
            int emailTagID = utility.GetTagID(userID, tagName);

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ShowTrashGV";
            objCommand.Parameters.AddWithValue("@theUserId", userID);
            objCommand.Parameters.AddWithValue("@theEmailTagId", emailTagID);
      
            DataSet myTrashData;
            myTrashData = dBConnect.GetDataSetUsingCmdObj(objCommand);

            int dataSize = myTrashData.Tables[0].Rows.Count;
            if (dataSize > 0)
            {
                lblAlert.Text = "";
                ArrayList emailClassList = new ArrayList();
                for (int i = 0; i < dataSize; i++)
                {
                    String senderID = myTrashData.Tables[0].Rows[i]["SenderID"].ToString();

                    objCommand.Parameters.Clear();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "EmailInfoByUserID";
                    objCommand.Parameters.AddWithValue("@theUserId", senderID);
                    DataSet mySenderData;
                    mySenderData = dBConnect.GetDataSetUsingCmdObj(objCommand);

                    EmailClass email = new EmailClass();
                    email.EmailId = int.Parse(myTrashData.Tables[0].Rows[i]["EmailID"].ToString());
                    email.SenderName = mySenderData.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = myTrashData.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = myTrashData.Tables[0].Rows[i]["EmailBody"].ToString();
                    String createdTime = myTrashData.Tables[0].Rows[i]["CreatedTime"].ToString();
                    email.CreatedTime = DateTime.Parse(createdTime).ToShortDateString();

                    emailClassList.Add(email);
                }
                gvEmail.DataSource = emailClassList;
                gvEmail.DataBind();
            }
            else
            {
                lblAlert.Text = "Your " + tagName + " is Empty";
                gvEmail.Visible = false;
            }
        }

        protected void btnAddNewTag_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(Session["UserID"].ToString());

            String newFolderName = txtNewTagName.Text;
            if (utility.InsertFolder(userID, newFolderName) == true)
            {
                showInbox("Inbox");
                populateTags();
                txtNewTagName.Text = "";
            }
            else
            {
                Response.Write("<script>alert('Failed to add new Tag folder due to an Error')</script>");
            }
        }

        public void populateTags()
        {
            int userID = int.Parse(Session["UserID"].ToString());

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "PopulateTagByUserId";
            objCommand.Parameters.AddWithValue("@theUserId", userID);
            DataSet myTagsData;
            myTagsData = dBConnect.GetDataSetUsingCmdObj(objCommand);

            ArrayList moveTo = new ArrayList();
            ArrayList otherFolders = new ArrayList();

            int numTags = myTagsData.Tables[0].Rows.Count;
            if (numTags > 0)
            {
                for (int i = 0; i < numTags; i++)
                {
                    Tag moveToTag = new Tag();
                    Tag otherFolderTag = new Tag();
                    String currentTagName = myTagsData.Tables[0].Rows[i]["TagName"].ToString();
                    if (currentTagName.CompareTo("Inbox") == 0 || currentTagName.CompareTo("Sent") == 0 || currentTagName.CompareTo("Flag") == 0 || currentTagName.CompareTo("Trash") == 0)
                    {
                       //do nothing
                    }
                    else
                    {
                        otherFolderTag.TagName = currentTagName;
                        otherFolders.Add(otherFolderTag);
                    }
                    if (currentTagName.CompareTo("Flag") == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        moveToTag.TagName = currentTagName;
                        moveTo.Add(moveToTag);
                    }
                }
                ddlMoveTag.DataSource = moveTo;
                ddlMoveTag.DataTextField = "TagName";
                ddlMoveTag.DataValueField = "TagName";
                ddlMoveTag.DataBind();
                ddlMoveTag.Items.Insert(0, new ListItem(" Move To ", "0"));

                ddlTags.DataSource = otherFolders;
                ddlTags.DataTextField = "TagName";
                ddlTags.DataValueField = "TagName";
                ddlTags.DataBind();
                ddlTags.Items.Insert(0, new ListItem(" Other Folders ", "0"));
            }
        }

        public void showSearch()
        {
            int userId = int.Parse(Session["UserID"].ToString());

            String searchTxt = "";
            searchTxt = txtSearch.Text;

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "SearchByEmail";
            objCommand.Parameters.AddWithValue("@theUserId", userId);
            objCommand.Parameters.AddWithValue("@theSearch", searchTxt);

            DataSet mySearchData;
            mySearchData = dBConnect.GetDataSetUsingCmdObj(objCommand);

            int searchSize = mySearchData.Tables[0].Rows.Count;
            if (searchSize > 0)
            {
                ArrayList searchList = new ArrayList();
                for (int i = 0; i < searchSize; i++)
                {
                    String senderID = mySearchData.Tables[0].Rows[i]["SenderID"].ToString();
                    objCommand.Parameters.Clear();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "EmailInfoByUserID";
                    objCommand.Parameters.AddWithValue("@theUserId", senderID);
                    DataSet mySenderData;
                    mySenderData = dBConnect.GetDataSetUsingCmdObj(objCommand);

                    EmailClass email = new EmailClass();
                    email.SenderName = mySenderData.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = mySearchData.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = mySearchData.Tables[0].Rows[i]["EmailBody"].ToString();
                    String createdTime = mySearchData.Tables[0].Rows[i]["CreatedTime"].ToString();
                    email.CreatedTime = DateTime.Parse(createdTime).ToShortDateString();

                    searchList.Add(email);
                }
                gvEmail.DataSource = searchList;
                gvEmail.DataBind();
            }
            else 
            {
                lblAlert.Text = "There was nothing found in the search";
                gvEmail.Visible = false;
            }
        }
    }
}