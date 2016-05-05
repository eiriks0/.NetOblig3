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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserData"] != null)
            {
                User user = (User)Session["UserData"];
                if (user.isAdmin)
                {
                    ManagerAdmin.Style["display"] = "block";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}