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
    public partial class Results : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            // Label1.Text = "Du klarte " + Session["poeng"].ToString() + " av 20 spørsmål";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
            Session.Clear();
        }
    }
}