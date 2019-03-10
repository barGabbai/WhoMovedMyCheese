using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void enterPlatform(object sender, EventArgs e)
    {

        if(TextBox1.Text == "editor")
        {
            if(TextBox2.Text == "1234")
            {
                Response.Redirect("Default.aspx");
            }
        }

        else
        {
            Fb.Visible = true;
        }
 


    }
}