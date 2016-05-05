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

        private void ShowUserNamePasswordErrorMessage()
        {

        }
    }
}