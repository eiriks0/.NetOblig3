using System;
using System.Collections.Generic;
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

        protected static int MaxQuestions = 5;
        protected static int QuestionsAnswered = 0;
        protected static int CurrentQuestion;
        public static List<int> Questions = new List<int>();


        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["New"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            GetQuestion();

        }

        public static void AddQuestion(int Number)
        {
            Questions.Add(Number);
        }

        protected void GetQuestion()
        {

            Random rnd = new Random();

            //Generates random int 1 to 17 (last number excluded).
            CurrentQuestion = rnd.Next(1, 18);

            // Checks if int (QuestionID) is already in list. If true, we generate a new question until we find one that is not in the list.
            if (Questions.Contains(CurrentQuestion))
            {
                while (Questions.Contains(CurrentQuestion))
                {
                    CurrentQuestion = rnd.Next(1, 18);
                    return;
                }
            }

            //Adds current questions id to int list.
            AddQuestion(CurrentQuestion);
            //Adds one to questions answered.
            QuestionsAnswered = QuestionsAnswered + 1;

            //If QuestionsAnswered is equal to MaxQuestions + 1
            if (QuestionsAnswered == MaxQuestions + 1)
            {
                RadioButton1.Text = "Done!";

                RadioButton2.Text = "Done!";

                RadioButton3.Text = "Done!";

                RadioButton4.Text = "Done!";

            }

            //If QuestionsAnswered is less than MaxQuestions we get the new question based on the generated int "CurrentQuestion"
            if (QuestionsAnswered < MaxQuestions)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from Quiz where QuestionId=" + CurrentQuestion;
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
