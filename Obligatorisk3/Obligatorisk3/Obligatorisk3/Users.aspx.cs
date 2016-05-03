﻿using System;
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
using System.Diagnostics;

namespace Obligatorisk3 {
    public partial class Users : System.Web.UI.Page {
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
        public static string _Answered;

        public static List<string> Answered = new List<string>();

        public static List<KeyValuePair<int, bool>> AnswerList = new List<KeyValuePair<int, bool>>();

        private const int NUM_QUESTIONS_IN_DB = 20;

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["New"] == null) {
                Response.Redirect("Login.aspx");
                return;
            }

            // Fill the list with question IDs and set it in the user's session.
            if (Session["questionIDs"] == null) {
                List<int> questionIDs = new List<int>(NUM_QUESTIONS_IN_DB);

                for (int i = 1; i < NUM_QUESTIONS_IN_DB + 1; i++) {
                    questionIDs.Add(i);
                }

                // Set the list to the user's session.
                Session["questionIDs"] = questionIDs;
            }

            if (Session["CurrentPage"] == null) {
                Session["CurrentPage"] = 1.0;
            }

            CurrentQuestion = Convert.ToDouble(Session["CurrentPage"]);
            PanelProgressbar.Style["width"] = (CurrentQuestion / MaxAmountOfQuestions) * 100 + "%";

            // Draw question if button is NOT clicked
            if(Session["isButtonClicked"] == null || (bool)Session["isButtonClicked"] == false) {
                DrawQuestion();
            }
            else if((bool)Session["isButtonClicked"] == true) {
                // Buttonclick will draw question, we do not need to draw one on page load.
                // Set isButtonClicked to false.
                Session["isButtonClicked"] = false;
            }
        }

        /** 
        * Generates labels based on the ints saved in the WrongAnswerList
        * 
         */
        private void DisplayAnswers()
        {
            TrafficQuestionImage.ImageUrl = null;

            TheScore.InnerText = RightAnswerList.Count.ToString();

            int _Int = new int();
            foreach (KeyValuePair<int, bool> Answers in AnswerList)
            {

                _Int = _Int + 1;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "SELECT * FROM Quiz WHERE QuestionId=" + Answers.Key;
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
                AnswerLabel1.Text = "1:" + " " + Answer1 + _Answered;
                AnswerLabel2.Text = "2:" + " " + Answer2;
                AnswerLabel3.Text = "3:" + " " + Answer3;
                CorrectAnswerLabel.Text = "Riktig svar:" + " " + CorrectAnswer;
                CorrectAnswerLabel.ForeColor = System.Drawing.Color.Green;
                CorrectAnswerLabel.Style["font-weight"] = "900";

                //_Answered = Answered[AnswerList.IndexOf(Answers)];

                _Answered = Answered[_Int];
                string WrongAnswer = reader[_Answered].ToString();

                if (!Answers.Value)
                {
                    AnswerLabel1.Text = "Du svarte:" + " " + WrongAnswer;
                    AnswerLabel1.ForeColor = System.Drawing.Color.Red;
                    WrongResults.Controls.Add(NewQuestionLabel);
                    WrongResults.Controls.Add(new LiteralControl("<br />"));
                    WrongResults.Controls.Add(AnswerLabel1);
                    WrongResults.Controls.Add(new LiteralControl("<br />"));
                    WrongResults.Controls.Add(CorrectAnswerLabel);
                    WrongResults.Controls.Add(new LiteralControl("<br />"));
                    WrongResults.Controls.Add(new LiteralControl("<br />"));
                }

                if (Answers.Value)
                {
                    CorrectResults.Controls.Add(NewQuestionLabel);
                    CorrectResults.Controls.Add(new LiteralControl("<br />"));
                    CorrectResults.Controls.Add(AnswerLabel1);
                    CorrectResults.Controls.Add(new LiteralControl("<br />"));
                    CorrectResults.Controls.Add(AnswerLabel2);
                    CorrectResults.Controls.Add(new LiteralControl("<br />"));
                    CorrectResults.Controls.Add(AnswerLabel3);
                    CorrectResults.Controls.Add(new LiteralControl("<br />"));
                    CorrectResults.Controls.Add(CorrectAnswerLabel);
                    CorrectResults.Controls.Add(new LiteralControl("<br />"));
                }

                con.Close();
            }

            ResultsWrapper.Style["display"] = "block";
        }


        /**
         *  Returns a random, but not already used, question ID.
         */
        private int getRandomQuestionID() {
            int questionIdToReturn;

            List<int> questionIDs = (List<int>)Session["questionIDs"];

            // Generate random index.
            int index = new Random().Next(0, questionIDs.Count());

            // Set the int at this index as the ID to return.
            questionIdToReturn = questionIDs[index];

            // Now that the question ID at this index has been used, remove it form the list.
            Debug.WriteLine("Question ID used & removed: " + questionIDs[index]);
            questionIDs.RemoveAt(index);

            return questionIdToReturn;
        }

        /** 
         * DrawQuestion is responsible for drawing radiobuttons on the screen and changing some text around
         */
        private void DrawQuestion() {
            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>> DrawQuestion() called! <<<<<<<<<<<<<<<<<<<<<<<<<");

            CurrentAskedQuestion = getRandomQuestionID();

            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "SELECT * FROM Quiz WHERE QuestionId=" + CurrentAskedQuestion;
            com = new SqlCommand(str, con);
            SqlDataReader reader = com.ExecuteReader();
            reader.Read();

            RadioButton[] rbuttons = new RadioButton[4];

            Random rnd = new Random();
            string[] sqlDataReaderKeys = new string[4] { "Answer", "Anwer2", "Anwer3", "CorrectAns" };
            sqlDataReaderKeys = sqlDataReaderKeys.OrderBy(x => rnd.Next()).ToArray();

            for (int i = 0; i < sqlDataReaderKeys.Length; i++) {
                ListItem li = new ListItem();
                li.Text = reader[sqlDataReaderKeys[i]].ToString();
                li.Value = (sqlDataReaderKeys[i] == "CorrectAns") ? "Answer4" : sqlDataReaderKeys[i];
                li.Selected = (i == 0) ? true : false;

                Answers.Items.Add(li);
            }

            if (reader["Picture"] != null) {
                TrafficQuestionImage.ImageUrl = reader["Picture"].ToString();
            }

            QuestionText.Text = reader["Question"].ToString();
            QuestionCounter.Text = CurrentQuestion.ToString() + "/" + MaxAmountOfQuestions.ToString();

            con.Close();
        }

        protected void B_Logout_Click(object sender, EventArgs e) {
            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e) {
            Session["isButtonClicked"] = true;

            string sth = Answers.SelectedValue; // get the current selected value from radio button list
            if (sth == "Answer4") // we know that the value "answer4" contains the correct question text
            {
               // RightAnswerList.Add(CurrentAskedQuestion); // Add the id of the question to the array of correct answers
                Answered.Add("CorrectAns");
            } else {
                //WrongAnswerList.Add(CurrentAskedQuestion); // Add the id of the question to the array of wrong answers
                Answered.Add(sth);
            }

            Answers.Items.Clear();
            QuestionText.Text = "";

            if (CurrentQuestion >= MaxAmountOfQuestions) {
                DisplayAnswers();
                PanelProgressbar.Style["background-color"] = "#0094ff";
                Button1.Visible = false;
                Button2.Visible = true;
                Button3.Visible = true;
                return;
            }

            CurrentQuestion++;

            if (CurrentQuestion == MaxAmountOfQuestions) {
                Button1.Text = "Se resultater";
            }

            Session["CurrentPage"] = CurrentQuestion;

            PanelProgressbar.Style["width"] = (CurrentQuestion / MaxAmountOfQuestions) * 100 + "%";

            DrawQuestion();
        }

        // Handling "new quiz" button
        protected void Button2_Click(object sender, EventArgs e) {
            Session["CurrentPage"] = null;
            Button1.Visible = true;
            Button2.Visible = false;
            Button3.Visible = false;
            Response.Redirect(Request.RawUrl); // reloads the page
        }

        protected void Button3_Click(object sender, EventArgs e) {
            // save score to highscore table
        }
    }
}