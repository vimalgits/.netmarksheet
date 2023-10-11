using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamManagement
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MarksEntery_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:44314/WebForm1.aspx");
        }

        protected void CreateStructure_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:44314/WebForm2.aspx");
        }

        protected void GenerateMarksheet_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:44314/WebForm3.aspx");
        }
    }
}