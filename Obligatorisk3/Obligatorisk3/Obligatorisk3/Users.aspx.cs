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
        SqlDataReader Question;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            GetQuestion();

        
        }

        private SqlDataReader InitQuestion()
        {
            Random rnd = new Random();
            int RandQuestionId = rnd.Next(1, 18); //Generere random int mellom 1 og 17 (18 er ikke med).

            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "SELECT * FROM Quiz WHERE QuestionId=" + RandQuestionId;
            com = new SqlCommand(str, con);
            SqlDataReader reader = com.ExecuteReader();
            return reader;

        }

        protected void GetQuestion()
        {

            Random rnd = new Random();
            int QuetsionNumber = rnd.Next(1, 18); //Generere random int mellom 1 og 17 (18 er ikke med).
            
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from Quiz where QuestionId=" + QuetsionNumber;
            com = new SqlCommand(str, con);
            SqlDataReader reader = com.ExecuteReader();
            reader.Read();
            QuestionText.Text = reader["Question"].ToString();
            Random rnd1 = new Random();
            int questionorder = rnd.Next(1, 4); //Generere random int mellom 1 og 4 (4 er ikke med).

            if (questionorder == 1)
            {
                RadioButton1.Text = reader["Answer"].ToString();

                RadioButton2.Text = reader["CorrectAns"].ToString();

                RadioButton3.Text = reader["Anwer3"].ToString();

                RadioButton4.Text = reader["Anwer2"].ToString();

                Image1.ImageUrl = reader["Picture"].ToString();
            }

            if (questionorder == 2)
            {
                RadioButton1.Text = reader["Anwer3"].ToString();

                RadioButton2.Text = reader["Answer"].ToString();

                RadioButton3.Text = reader["CorrectAns"].ToString();

                RadioButton4.Text = reader["Anwer2"].ToString();

                Image1.ImageUrl = reader["Picture"].ToString();
            }

            if (questionorder == 3)
            {
                RadioButton1.Text = reader["Anwer2"].ToString();

                RadioButton2.Text = reader["Anwer3"].ToString();

                RadioButton3.Text = reader["Answer"].ToString();

                RadioButton4.Text = reader["CorrectAns"].ToString();

                Image1.ImageUrl = reader["Picture"].ToString();
            }


            con.Close();


        }

        protected void B_Logout_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetQuestion();
        }
    }
}