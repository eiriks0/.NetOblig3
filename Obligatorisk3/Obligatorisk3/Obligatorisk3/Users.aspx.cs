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
        protected int CurrentAskedQuestion;
        protected double CurrentQuestion;
        protected double MaxAmountOfQuestions = 10;
        public static List<int> Questions = new List<int>();
        public static List<int> RightAnswerList = new List<int>();
        public static List<int> WrongAnswerList = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["CurrentPage"] == null)
            {
                Session["CurrentPage"] = 1.0;
            }

            CurrentQuestion = Convert.ToDouble(Session["CurrentPage"]);
            PanelProgressbar.Style["width"] = (CurrentQuestion / MaxAmountOfQuestions) * 100 + "%";

            DrawQuestion();
        }

        /** 
        * Generates labels based on the ints saved in the WrongAnswerList
        * 
         */
        private void DisplayWrongAnswers()
        {
            //Gets all the wrong answers
            foreach (int Answer in RightAnswerList)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "SELECT * FROM Quiz WHERE QuestionId=" + Answer;
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                reader.Read();
                string[] sqlDataReaderKeys = new string[4] { "Answer", "Anwer2", "Anwer3", "CorrectAns" };

                string question = reader["Question"].ToString();
                string Answer1 = reader["Answer"].ToString();
                string Answer2 = reader["Anwer2"].ToString();
                string Answer3 = reader["Anwer3"].ToString();
                string CorrectAnswer = reader["CorrectAns"].ToString();

                Label NewQuestionLabel = new Label();
                Label AnswerLabel1 = new Label();
                Label AnswerLabel2 = new Label();
                Label AnswerLabel3 = new Label();
                Label CorrectAnswerLabel = new Label();

                NewQuestionLabel.Text = question;
                NewQuestionLabel.Font.Size = 12;
                AnswerLabel1.Text = Answer1;
                AnswerLabel2.Text = Answer2;
                AnswerLabel3.Text = Answer3;
                CorrectAnswerLabel.Text = CorrectAnswer;
                CorrectAnswerLabel.ForeColor = System.Drawing.Color.Green;

                //Debug{
                System.Diagnostics.Debug.WriteLine(question);

                System.Diagnostics.Debug.WriteLine(CorrectAnswer);
                //}
                con.Close();
            }

            foreach (int Answer in WrongAnswerList)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "SELECT * FROM Quiz WHERE QuestionId=" + Answer;
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                reader.Read();
                string[] sqlDataReaderKeys = new string[4] { "Answer", "Anwer2", "Anwer3", "CorrectAns" };

                string question = reader["Question"].ToString();
                string answer = reader["CorrectAns"].ToString();

                Label NewQuestionLabel = new Label();
                Label NewAnswerLabel = new Label();

                NewQuestionLabel.Text = question;
                NewAnswerLabel.Text = answer;

                //Debug{
                System.Diagnostics.Debug.WriteLine(question);
                System.Diagnostics.Debug.WriteLine(answer);
                //}
                con.Close();
            }



        }

        /** 
         * DrawQuestion is responsible for drawing radiobuttons on the screen and changing some text around
         * 
          */
        private void DrawQuestion()
        {
            Random rnd = new Random();
            Random rndQuestionOrder = new Random();
            CurrentAskedQuestion = rnd.Next(1, 18); //Generere random int mellom 1 og 17 (18 er ikke med).

            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "SELECT * FROM Quiz WHERE QuestionId=" + CurrentAskedQuestion;
            com = new SqlCommand(str, con);
            SqlDataReader reader = com.ExecuteReader();
            reader.Read();

            RadioButton[] rbuttons = new RadioButton[4];

            string[] sqlDataReaderKeys = new string[4] { "Answer", "Anwer2", "Anwer3", "CorrectAns" };
            sqlDataReaderKeys = sqlDataReaderKeys.OrderBy(x => rnd.Next()).ToArray();

            for (int i = 0; i < sqlDataReaderKeys.Length; i++)
            {
                ListItem li = new ListItem();
                li.Text = reader[sqlDataReaderKeys[i]].ToString();
                li.Value = (sqlDataReaderKeys[i] == "CorrectAns") ? "Answer4" : sqlDataReaderKeys[i];
                li.Selected = (i == 0) ? true : false;

                Answers.Items.Add(li);
            }

            if (reader["Picture"] != null)
            {
                TrafficQuestionImage.ImageUrl = reader["Picture"].ToString();
            }

            QuestionText.Text = reader["Question"].ToString();
            QuestionCounter.Text = CurrentQuestion.ToString() + "/" + MaxAmountOfQuestions.ToString();

            con.Close();
        }

        protected void B_Logout_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sth = Answers.SelectedValue;

            //IF correct answer
            if (sth == "Answer4")
            {
                // System.Diagnostics.Debug.WriteLine(sth);
                RightAnswerList.Add(CurrentAskedQuestion);
            }
            //IF correct answer
            if (sth != "Answer4")
            {
                //  System.Diagnostics.Debug.WriteLine(sth);
                WrongAnswerList.Add(CurrentAskedQuestion);
            }

            Answers.Items.Clear();
            QuestionText.Text = "";

            if (CurrentQuestion >= MaxAmountOfQuestions)
            {
                DisplayWrongAnswers();
                PanelProgressbar.Style["background-color"] = "#0094ff";
                Button1.Enabled = false;
                return;
            }

            CurrentQuestion++;
            Session["CurrentPage"] = CurrentQuestion;





            DrawQuestion();
            PanelProgressbar.Style["width"] = (CurrentQuestion / MaxAmountOfQuestions) * 100 + "%";
            QuestionText.Text += sth;
        }
    }
}



