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
        private void DisplayAnswers()
        {
            Panel1.Height = 1500;
            TrafficQuestionImage.ImageUrl = null;
            Panel1.HorizontalAlign = HorizontalAlign.Left;
            //Sets header over the wrong answers
            Label WrongAnswersHeaderLabel = new Label();
            WrongAnswersHeaderLabel.Text = "Du fikk feil svar på disse spørsmålene!";
            WrongAnswersHeaderLabel.Font.Size = 16;
            WrongAnswersHeaderLabel.Font.Bold = true;
            Panel1.Controls.Add(WrongAnswersHeaderLabel);
            Panel1.Controls.Add(new LiteralControl("<br />"));
            Panel1.Controls.Add(new LiteralControl("<br />"));
            //gets all the wrong answers
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
                NewQuestionLabel.Font.Bold = true;
                AnswerLabel1.Text = "1:" + " " + Answer1;
                AnswerLabel2.Text = "2:" + " " + Answer2;
                AnswerLabel3.Text = "3:" + " " + Answer3;
                CorrectAnswerLabel.Text = "Riktig svar:" + " " + CorrectAnswer;
                CorrectAnswerLabel.ForeColor = System.Drawing.Color.Green;

                Panel1.Controls.Add(NewQuestionLabel);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(AnswerLabel1);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(AnswerLabel2);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(AnswerLabel3);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(CorrectAnswerLabel);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(new LiteralControl("<br />"));

                con.Close();
            }
            //Sets header over the wrong answers
            Label RightAnswersHeaderLabel = new Label();
            RightAnswersHeaderLabel.Text = "Du svarte riktig på disse spørsmålene";
            RightAnswersHeaderLabel.Font.Size = 16;
            RightAnswersHeaderLabel.Font.Bold = true;
            Panel1.Controls.Add(RightAnswersHeaderLabel);
            Panel1.Controls.Add(new LiteralControl("<br />"));
            Panel1.Controls.Add(new LiteralControl("<br />"));
            //gets all the right answers
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
                NewQuestionLabel.Font.Bold = true;
                AnswerLabel1.Text = "1:" + " " + Answer1;
                AnswerLabel2.Text = "2:" + " " + Answer2;
                AnswerLabel3.Text = "3:" + " " + Answer3;
                CorrectAnswerLabel.Text = "Riktig svar:" + " " + CorrectAnswer;
                CorrectAnswerLabel.ForeColor = System.Drawing.Color.Green;

                Panel1.Controls.Add(NewQuestionLabel);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(AnswerLabel1);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(AnswerLabel2);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(AnswerLabel3);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(CorrectAnswerLabel);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(new LiteralControl("<br />"));

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

            //If correct answer
            if (sth == "Answer4")
            {
                RightAnswerList.Add(CurrentAskedQuestion);
            }
            //If correct answer
            if (sth != "Answer4")
            {
                WrongAnswerList.Add(CurrentAskedQuestion);
            }

            Answers.Items.Clear();
            QuestionText.Text = "";

            if (CurrentQuestion >= MaxAmountOfQuestions)
            {
                DisplayAnswers();
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




