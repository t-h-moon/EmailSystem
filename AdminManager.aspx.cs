//Tina Mooon 
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
    public partial class AdminManager : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        //Validation validate = new Validation();
        Utility utility = new Utility();
        SqlCommand objCommand = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showAdminFlag();
                showAllAccount();
                pnlMessage.Visible = false;
                btnBack.Visible = true;
            }
        }

        protected void btnEmailManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmailManager.aspx");
        }
        
        //display the flagged emails
        public void showAdminFlag()
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ShowAllFlagEmail";
            DataSet myData;
            myData = dBConnect.GetDataSetUsingCmdObj(objCommand);
            int dataSize = myData.Tables[0].Rows.Count;

            if (dataSize > 0)
            {
                ArrayList adminClassList = new ArrayList();
                for (int i = 0; i < dataSize; i++)
                {
                    String senderID = myData.Tables[0].Rows[i]["SenderID"].ToString();
                    String receiverID = myData.Tables[0].Rows[i]["ReceiverID"].ToString();

                    objCommand.Parameters.Clear();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "GetEmailbyUserId";
                    objCommand.Parameters.AddWithValue("@theUserId", senderID);
                    DataSet mySenderIDdata;
                    mySenderIDdata = dBConnect.GetDataSetUsingCmdObj(objCommand);

                    objCommand.Parameters.Clear();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "GetEmailbyUserId";
                    objCommand.Parameters.AddWithValue("@theUserId", receiverID);
                    DataSet myReceiverIDdata;
                    myReceiverIDdata = dBConnect.GetDataSetUsingCmdObj(objCommand);

                    AdminFlag_Info flag = new AdminFlag_Info();
                    flag.SenderEmail = mySenderIDdata.Tables[0].Rows[0]["CreatedEmailAddress"].ToString();
                    flag.ReceiverEmail = myReceiverIDdata.Tables[0].Rows[0]["CreatedEmailAddress"].ToString();
                    flag.Subject = myData.Tables[0].Rows[i]["Subject"].ToString();
                    flag.EmailBody = myData.Tables[0].Rows[i]["EmailBody"].ToString();
                    String createdTime = myData.Tables[0].Rows[i]["CreatedTime"].ToString();
                    flag.CreatedTime = DateTime.Parse(createdTime).ToShortDateString();

                    adminClassList.Add(flag);
                }
                gvAdmin.DataSource = adminClassList;
                gvAdmin.DataBind();
            }
            else
            {
                lblAlert.Text = "Flag Inbox is Empty";
            }
        }

        //getting information for each user 
        public void showAllAccount()
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ShowAllUsers";
            DataSet myData;
            myData = dBConnect.GetDataSetUsingCmdObj(objCommand);
            int dataSize = myData.Tables[0].Rows.Count;

            if (dataSize > 0)
            {
                ArrayList accountClassList = new ArrayList();
                for (int i = 0; i < dataSize; i++)
                {
                    User userAccount = new User();
                    userAccount.Avatar = myData.Tables[0].Rows[i]["Avatar"].ToString();
                    userAccount.UserName = myData.Tables[0].Rows[i]["UserName"].ToString();
                    userAccount.PhoneNumber = myData.Tables[0].Rows[i]["PhoneNumber"].ToString();
                    userAccount.Address = myData.Tables[0].Rows[i]["Address"].ToString();
                    userAccount.CreatedEmailAddress = myData.Tables[0].Rows[i]["CreatedEmailAddress"].ToString();
                    int activeNum = int.Parse(myData.Tables[0].Rows[i]["Active"].ToString());
                    String userStatus = "";
                    if (activeNum == 1)
                    {
                        userStatus = "Active";
                    }
                    else
                    {
                        userStatus = "Inactive";
                    }
                    userAccount.Active = userStatus;
                    accountClassList.Add(userAccount);
                }
                gvAccountInfo.DataSource = accountClassList;
                gvAccountInfo.DataBind();
            }
            else
            {
                lblAlert.Text = "Flag Inbox is Empty";
            }
        }


        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.Aspx");
        }

        //display all users on a grid view
        protected void gvAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMessage.Visible = true;
            btnBack.Visible = true;

            String from = gvAdmin.SelectedRow.Cells[1].Text;
            String to = gvAdmin.SelectedRow.Cells[2].Text;
            String subject = gvAdmin.SelectedRow.Cells[3].Text;
            String message = gvAdmin.SelectedRow.Cells[4].Text;
            String time = gvAdmin.SelectedRow.Cells[5].Text;

            lblFrom.Text = from;
            lblTo.Text = to;
            lblSubject.Text = subject;
            lblMessage.Text = message;
            lblTime.Text = time;
            String avatarPath = utility.GetAvatarByEmail(from);
            avatar.ImageUrl = avatarPath;
            gvAdmin.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlMessage.Visible = false;
            gvAdmin.Visible = true;
        }

        protected void btnBanned_Click(object sender, EventArgs e)
        {
            unActive();
        }

        protected void btnUnBanned_Click(object sender, EventArgs e)
        {
            reActive();
        }

        public void unActive()
        {
            int userId = int.Parse(Session["UserId"].ToString());
            for (int i = 0; i < gvAccountInfo.Rows.Count; i++)
            {
                CheckBox Cbox = (CheckBox)gvAccountInfo.Rows[i].FindControl("cbSelectAccountInfo");
                if (Cbox.Checked == true)
                {
                    String senderUserName = gvAccountInfo.Rows[i].Cells[5].Text;
                    int userById = utility.GetUserIdByEmail(senderUserName);
                    utility.UpdateUserActiveStatus(0, userById);
                }
            }
            gvAccountInfo.DataBind();
            showAllAccount();
        }

        public void reActive()
        {
            int userId = int.Parse(Session["UserId"].ToString());
            for (int i = 0; i < gvAccountInfo.Rows.Count; i++)
            {
                CheckBox Cbox = (CheckBox)gvAccountInfo.Rows[i].FindControl("cbSelectAccountInfo");
                if (Cbox.Checked == true)
                {
                    String senderUserName = gvAccountInfo.Rows[i].Cells[5].Text;
                    int userById = utility.GetUserIdByEmail(senderUserName);
                    utility.UpdateUserActiveStatus(1, userById);
                }
            }
            gvAccountInfo.DataBind();
            showAllAccount();
        }
    }
}