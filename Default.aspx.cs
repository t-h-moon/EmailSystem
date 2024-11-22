//Tina Mooon 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using EmailLibrary;
using EmailLibrary.Model;
using Utilities;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Data.SqlClient;

namespace EmailProject
{
    public partial class Default : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        Validation validate = new Validation();
        SqlCommand objCommand = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountCreation.Aspx");
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            if (validate.ValSignIn(txtEmailAddress.Text, txtPassword.Text) == true)
            {
                lblAlert.Text = "";
                if (validate.CheckEmailExist(txtEmailAddress.Text) == true)
                {
                    lblAlert.Text = "";
                    if (validate.ValidatePassword(txtEmailAddress.Text, txtPassword.Text))
                    {
                        if (validate.IsActive(txtEmailAddress.Text) == false)//checking if user is banned
                        {
                            lblAlert.Text = "";
                            if (validate.IsTypeUser(txtEmailAddress.Text) == true)//is Type user
                            {
                                lblAlert.Text = "Current VIEW is USER";
                                GetUserInfo(txtEmailAddress.Text);
                                Response.Redirect("EmailManager.aspx");
                            }
                            else//is Type admin
                            {
                                lblAlert.Text = "Current VIEW ADMIN";
                                GetUserInfo(txtEmailAddress.Text);
                                Response.Redirect("AdminManager.Aspx");
                            }
                        }
                        else
                        {
                            lblAlert.Text = "Please contact admin. Your account has been banned";
                        }
                    }
                    else  
                    {
                        lblAlert.Text = "Your Email Address and Password does not match. Please try again";
                    }
                }
                else 
                {
                    lblAlert.Text = "Your Email Address was not found. Please try again";
                }
            }
            else
            {
                lblAlert.Text = "Please do not leave Email Address or Password Blank";
            }
        }

        public void GetUserInfo(String userEmail)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserInfoByEmail";
            objCommand.Parameters.AddWithValue("@theEmail", userEmail);
            DataSet myData;
            myData = dBConnect.GetDataSetUsingCmdObj(objCommand);
            
            User user = new User();
            Session["UserID"] = myData.Tables[0].Rows[0]["UserID"].ToString();
            Session["UserName"] = user.UserName = myData.Tables[0].Rows[0]["UserName"].ToString();
            Session["Avatar"] = user.Avatar = myData.Tables[0].Rows[0]["Avatar"].ToString();
            Session["CreatedEmailAddress"] = txtEmailAddress.Text;
        }
    }
}