//Tina Mooon 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EmailLibrary;
using Utilities;
using System.Data;
using System.Collections;
using System.Globalization;
using EmailLibrary.Model;

namespace EmailProject
{
    public partial class AccountCreation : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        Validation validate = new Validation();
        Utility utility = new EmailLibrary.Utility();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{ 
            
            //}
        }

        protected void ddlSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAvatar.SelectedValue.Equals("Princess"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar1.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Soldier"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar2.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("BountyHunter"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar3.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Loyal"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar4.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Evil"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar5.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Server"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar6.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Fixer"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar7.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Wise"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar8.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Gangster"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar9.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Cuddly"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar10.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Deadly"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar11.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Jeti"))
            {
                imgAvatar.ImageUrl = "~/imgs/avatar12.PNG";
            }
            else if (ddlAvatar.SelectedValue.Equals("Admin"))
            {
                imgAvatar.ImageUrl = "~/imgs/admin.PNG";
            }
            else
            {
                imgAvatar.ImageUrl = "~/imgs/default.PNG";
            }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (validate.ValBlankBoxes(txtUserName.Text, txtAddress.Text, txtPhoneNumber.Text, txtUserEmail.Text, txtRecoverEmail.Text, txtPassword.Text, txtConfirmPassword.Text) == true)
            {
                lblAlert.Text = "";
                if ((validate.CheckUserExist(txtUserName.Text) == false))
                {
                    lblAlert.Text = "";
                    if (validate.CheckEmail(txtUserEmail.Text) == true && validate.CheckEmail(txtRecoverEmail.Text))
                    {
                        lblAlert.Text = "";
                        if (validate.CheckEmailExist(txtUserEmail.Text) == false)
                        {
                            lblAlert.Text = "";
                            if (validate.SamePasswords(txtPassword.Text, txtConfirmPassword.Text) == true)
                            {
                                lblAlert.Text = "";
                                User user = new User();
                                user.UserName = txtUserName.Text;
                                user.Address = txtAddress.Text;
                                user.PhoneNumber = txtPhoneNumber.Text;
                                user.CreatedEmailAddress = txtUserEmail.Text;
                                user.RecoveryEmailAddress = txtRecoverEmail.Text;
                                user.Avatar = imgAvatar.ImageUrl.ToString();
                                user.Password = txtPassword.Text;
                                user.Active = "1";//1 is Active -- 0 is Banned
                                user.Type = rblUserType.SelectedValue;

                                if (utility.AddUserDataBase(user) == true)
                                {
                                    lblAlert.Text = "Thank you for your submission";
                                    int userID = utility.GetUserIdByEmail(user.CreatedEmailAddress);

                                    //creating tags for new user
                                    utility.InsertTag("Inbox", userID);
                                    utility.InsertTag("Trash", userID);
                                    utility.InsertTag("Flag", userID);
                                }
                                else
                                {
                                    lblAlert.Text = "ERROR";
                                }
                            }
                            else
                            {
                                lblAlert.Text = "Your Passwords do not match. Please Try again";
                            }
                        }
                        else
                        {
                            lblAlert.Text = "That Email already exist. Please Make a different Email";
                        }
                    }
                    else 
                    {
                        lblAlert.Text = "You email address is not valid, please add @. For Example: MyEmail@farfaraway"; 
                    }
                }
                else
                {
                    lblAlert.Text = "That User already exist. Please Make a different UserName";
                }
            }
            else
            {
                lblAlert.Text = "Please fill out your Personal Information";
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.Aspx");
        }
    }
}