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
using System.Diagnostics;

namespace Obligatorisk3 {
    public partial class Users : System.Web.UI.Page {
        static string sqlConnectionString = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
        SqlCommand sqlCommand;
        SqlDataReader sqlReader;
        string queryString;
        
        protected static int currentQuestionID; // ID of the currently asked question.

        protected static double numOfAskedQuestions = 0.0; // Number of questions asked thus far.

        protected double numOfQuestionsToAsk = 5; // The amount of questions to ask.

        // List for storing the user's answers to the questions.
        public static List<KeyValuePair<int, bool>> userAnswers = new List<KeyValuePair<int, bool>>();

        public static string _Answered; // Not sure what this does

        public static List<string> Answered = new List<string>(); // Not sure what this does

        private static bool isFirstLoad = true;

        private static int NUM_QUESTIONS_IN_DB = 30;

        private static List<int> questionIDs = new List<int>(NUM_QUESTIONS_IN_DB);

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["New"] == null) {
                Response.Redirect("Login.aspx");
                return;
            }

            if(isFirstLoad) {
                // Populate list with question IDs
                questionIDs.Clear();
                for (int i = 1; i < NUM_QUESTIONS_IN_DB + 1; i++) {
                    questionIDs.Add(i);
                }

                // Draw the initial question
                DrawQuestion();

                isFirstLoad = false;
            }  
        }

        /**
        * Method for updating the progressbar, progress-counter-text and the button text.
        */
        private void updateProgress() {
            PanelProgressbar.Style["width"] = (numOfAskedQuestions / numOfQuestionsToAsk) * 100 + "%";

            QuestionCounter.Text = numOfAskedQuestions.ToString() + "/" + numOfQuestionsToAsk.ToString();

            if(numOfAskedQuestions == numOfQuestionsToAsk) {
                Button1.Text = "Se resultater";
            }
        }

        /**
        * Method for resetting the data on this page.
        */
        private void resetData() {
            isFirstLoad = true;
            numOfAskedQuestions = 0.0;
        }

        /**
        * Returns a random question ID from the questionIDs list,
        * then removes the returned ID from the list.
        * This ensures that all IDs returned from this method is unique.
        */
        private int getUniqueQuestionID() {
            int index = new Random().Next(0, questionIDs.Count());
            int questionIdToReturn = questionIDs[index];
            questionIDs.RemoveAt(index);

            Debug.WriteLine("getUniqueQuestionID() >> Returning ID: " + questionIdToReturn);

            return questionIdToReturn;
        }

        /** 
        * Generates labels based on the ints saved in the WrongAnswerList
        */
        private void DisplayAnswers() {
            TrafficQuestionImage.ImageUrl = null;

            sqlConnection.Open();

            foreach (KeyValuePair<int, bool> userAnswer in userAnswers) {
                Debug.WriteLine("DisplayAnswers() >> in foreach userAnswers >> userAnswer.Key: " + userAnswer.Key + " usrAnswrBool: " + userAnswer.Value.ToString());

                queryString = "SELECT * FROM Quiz WHERE QuestionId=" + userAnswer.Key;
                sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlReader = sqlCommand.ExecuteReader();
                sqlReader.Read();

                string question = sqlReader["Question"].ToString();
                string Answer1 = sqlReader["Answer"].ToString();
                string Answer2 = sqlReader["Anwer2"].ToString();
                string Answer3 = sqlReader["Anwer3"].ToString();
                string CorrectAnswer = sqlReader["CorrectAns"].ToString();

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

                _Answered = Answered[userAnswers.IndexOf(userAnswer)];
                string WrongAnswer = sqlReader[_Answered].ToString();

                if (!userAnswer.Value) {
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

                if (userAnswer.Value) {
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

                sqlReader.Close();
            }//-end foreach

            sqlConnection.Close();

            ResultsWrapper.Style["display"] = "block";
        }

        /** 
         * DrawQuestion is responsible for drawing radiobuttons on the screen and changing some text around
         */
        private void DrawQuestion() {
            numOfAskedQuestions++;
            updateProgress();

            currentQuestionID = getUniqueQuestionID();

            sqlConnection.Open();
            queryString = "SELECT * FROM Quiz WHERE QuestionId=" + currentQuestionID;
            sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();

            RadioButton[] rbuttons = new RadioButton[4];

            string[] sqlDataReaderKeys = new string[4] { "Answer", "Anwer2", "Anwer3", "CorrectAns" };
            sqlDataReaderKeys = sqlDataReaderKeys.OrderBy(x => new Random().Next()).ToArray();

            for (int i = 0; i < sqlDataReaderKeys.Length; i++) {
                ListItem li = new ListItem();
                li.Text = sqlReader[sqlDataReaderKeys[i]].ToString();
                li.Value = (sqlDataReaderKeys[i] == "CorrectAns") ? "Answer4" : sqlDataReaderKeys[i];
                li.Selected = (i == 0) ? true : false;

                Answers.Items.Add(li);
            }

            if (sqlReader["Picture"] != null) {
                TrafficQuestionImage.ImageUrl = sqlReader["Picture"].ToString();
            }

            QuestionText.Text = sqlReader["Question"].ToString();

            sqlConnection.Close();
        }

        protected void B_Logout_Click(object sender, EventArgs e) {
            resetData();

            Session["New"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e) {
            Debug.WriteLine("Button1_Click() >> currentQuestionID: " + currentQuestionID);

            // Handle user answer
            string currSelectedRadioValue = Answers.SelectedValue; // get the current selected value from radio button list

            if (currSelectedRadioValue == "Answer4") // we know that the value "answer4" contains the correct question text
            {
                userAnswers.Insert(0, new KeyValuePair<int, bool>(currentQuestionID, true));
                Answered.Add("CorrectAns");
            } else {
                userAnswers.Insert(0, new KeyValuePair<int, bool>(currentQuestionID, false));
                Answered.Add(currSelectedRadioValue);
            }

            Answers.Items.Clear();
            QuestionText.Text = "";

            // If the user has answered the final question, show results and return.
            if (numOfAskedQuestions >= numOfQuestionsToAsk) {
                resetData();

                DisplayAnswers();
                PanelProgressbar.Style["background-color"] = "#0094ff";
                Button1.Visible = false;
                Button2.Visible = true;
                Button3.Visible = true;
                return;
            }

            updateProgress();
            DrawQuestion();
        }

        // Handling "new quiz" button
        protected void Button2_Click(object sender, EventArgs e) {
            //Session["CurrentPage"] = null;
            isFirstLoad = true;
            numOfAskedQuestions = 0.0;

            Answered.Clear();
            userAnswers.Clear();
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