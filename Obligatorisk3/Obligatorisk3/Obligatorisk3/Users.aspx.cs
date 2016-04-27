using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Obligatorisk3
{
    public partial class Users : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        string str;
        SqlCommand com;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                Label_welcome.Text += Session["New"].ToString();
            }
            else
                Response.Redirect("Login.aspx");

            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from Quiz where QuestionId=2";
            com = new SqlCommand(str, con);
            SqlDataReader reader = com.ExecuteReader();
            reader.Read();
            Label1.Text = reader["Question"].ToString();
            
            RadioButton1.Text = reader["Answer"].ToString();
           
            RadioButton2.Text = reader["Anwer2"].ToString();
          
            RadioButton3.Text = reader["Anwer3"].ToString();
        
            RadioButton4.Text = reader["CorrectAns"].ToString();

            Image1.ImageUrl = reader["Picture"].ToString();

            con.Close();


        }

        protected void B_Logout_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}