using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Obligatorisk3.Models;

namespace Obligatorisk3
{
    public partial class Login : System.Web.UI.Page
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
        private SqlDataReader sqlDataReader;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserData"] != null)
            {
                Response.Redirect("Default.aspx");
            }

            TextBoxUserName.Focus();
        }

        /*
         * When the user clicks the login button
         * Check if LogUserIn returns true
         * If it does, it means the user was authenticated and we can redirect to the quiz
         * 
         * Then do a check to see if the user is an admin
         * Redirect to the manager page
         * 
         */
        protected void Button_Login_Click(object sender, EventArgs e)
        {

            if (LogUserIn())
            {
                User user = (User)Session["UserData"];

                if (user != null && user.isAdmin)
                {
                    Response.Redirect("Manager.aspx");
                    return;
                }

                Response.Redirect("Users.aspx");
            }
            else
            {
                ShowUserNamePasswordErrorMessage();
            }
            
        }

        /*
         * Logs a user in (theoretically) based on username and password
         * Creates a UserData session which holds a User object with the properties UserId, UserName, IsAdmin
         * 
         */
        private bool LogUserIn()
        {
            connection.Open();

            SqlCommand command = new SqlCommand(null, connection);
            command.CommandText = 
                "SELECT UserId, UserName, IsAdmin " +
                "FROM UserData " +
                "WHERE UserName = @UserName " + 
                "AND Password = @Password";
            SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
            SqlParameter passWordParam = new SqlParameter("@Password", SqlDbType.VarChar, 20);
            userNameParam.Value = TextBoxUserName.Text;
            passWordParam.Value = TextBoxPassword.Text;

            command.Parameters.Add(userNameParam);
            command.Parameters.Add(passWordParam);
            command.Prepare();

            sqlDataReader = command.ExecuteReader();
            sqlDataReader.Read();

            if (sqlDataReader.HasRows)
            {
                User user = new User(sqlDataReader["UserName"].ToString(), int.Parse(sqlDataReader["UserId"].ToString()), bool.Parse(sqlDataReader["IsAdmin"].ToString() ));

                HttpContext.Current.Session.Add("UserData", user);

                return true;
            }

            connection.Close();

            return false;
        }

        /*
         * Responsible for displaying the error messages if the login is incorrect
         * 
         */
        private void ShowUserNamePasswordErrorMessage()
        {
            UsernamePasswordAlert.Style["display"] = "block";
        }
    }
}