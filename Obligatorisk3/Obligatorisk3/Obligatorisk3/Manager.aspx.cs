using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obligatorisk3.Models;

namespace Obligatorisk3
{
    public partial class Manager : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserData"] != null)
            {
                User user = (User)Session["UserData"];
                if (user.isAdmin)
                {
                    ManagerAdmin.Style["display"] = "block";
                }

                HighscoresDataSource.SelectCommand =
                    "SELECT Highscores.Score, Highscores.CreatedDate FROM UserData INNER JOIN Highscores ON ( Highscores.UserId = UserData.UserId AND UserData.UserId = " + user.userId.ToString() + ")";

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}