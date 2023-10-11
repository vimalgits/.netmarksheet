using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamManagement
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getsessions();
            }
            
        }

        private void getsessions()
        {
            string querySession = "select distinct Session from student_details order by session desc";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querySession, connection);


            DataTable dtSession = new DataTable();




            connection.Open();
            adapter.Fill(dtSession);
            connection.Close();

            ddlsessionlist.DataSource = dtSession;
            ddlsessionlist.DataTextField = "Session";
            ddlsessionlist.DataValueField = "Session";
            ddlsessionlist.DataBind();
            /*ddlsessionlist.Items.Add(new ListItem { Text = "Please select", Selected = true });*/
        }

        protected void ddlsessionlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblsessionselet.Visible = false;
            string queryStructureName = "select distinct Structures_name from marksheetdesigns";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryStructureName, connection);



            DataTable dtStructureName = new DataTable();




            connection.Open();
            adapter.Fill(dtStructureName);

            connection.Close();

            ddlstructurenames.DataSource = dtStructureName;
            ddlstructurenames.DataTextField = "Structures_name";
            ddlstructurenames.DataValueField = "Structures_name";
            ddlstructurenames.DataBind();
            
        }
        protected void ddlstructurenames_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load Classes
            lblstructureselet.Visible = false;
            string queryClasses = "select distinct Current_Class from student_details order by Current_Class";


            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);

            var adapter = new SqlDataAdapter(queryClasses, connection);
            DataTable dtClasses = new DataTable();

            connection.Open();
            adapter.Fill(dtClasses);
            connection.Close();


            ddlclasslist.DataSource = dtClasses;
            ddlclasslist.DataTextField = "Current_Class";
            ddlclasslist.DataValueField = "Current_Class";
            ddlclasslist.DataBind();

        }

        protected void ddlclasslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblclassvalidation.Visible = false;

            var selectedClass = ddlclasslist.SelectedValue as string;

            string querySection = "select distinct current_section from student_details where Current_Class='" + selectedClass + "' order by Current_Section";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querySection, connection);

            DataTable dtSection = new DataTable();

            connection.Open();
            adapter.Fill(dtSection);
            connection.Close();


            ddlsectionnamelist.DataSource = dtSection;
            ddlsectionnamelist.DataTextField = "current_section";
            ddlsectionnamelist.DataValueField = "current_section";
            ddlsectionnamelist.DataBind();

        }

        protected void ddlsectionnamelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblsectionvalidation.Visible = false;


            string selectedClass = ddlclasslist.SelectedItem.Text;
            string selectedSection = ddlsectionnamelist.SelectedItem.Text;
            string queryStream = "select distinct Stream from student_details where current_class='" + selectedClass + "' AND current_section='" + selectedSection + "'";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;


            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryStream, connection);

            DataTable dtStream = new DataTable();
            connection.Open();
            adapter.Fill(dtStream);
            connection.Close();

            ddlstreamlist.DataSource = dtStream;
            ddlstreamlist.DataTextField = "Stream";
            ddlstreamlist.DataValueField = "Stream";
            ddlstreamlist.DataBind();

            ddlstreamlist.Items.Add(new ListItem { Text = "Please select", Selected = true });

        }
        protected void ddlstreamlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblstreamvalidation.Visible = false;
            btnsubmit.Visible = true;
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            string responseUrl = "WebForm4.aspx?";

            string sessionName = ddlsessionlist.SelectedItem.Text;
            string structurename = ddlstructurenames.SelectedItem.Text;
            string classNmae = ddlclasslist.SelectedItem.Text;
            string sectionName = ddlsectionnamelist.SelectedItem.Text;
            string streamName = ddlstreamlist.SelectedItem.Text;


            responseUrl += "SessionName=" + sessionName + "&StructureName=" + structurename + "&Classname=" + classNmae + "&sectionName=" + sectionName + "&streamName=" + streamName;


            Response.Redirect(responseUrl);

            //getgridview();
        }

        protected void GotoMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:44314/main.aspx");
        }
        /*
       private void getgridview()
       {

           try
           {


               string selectedStream = ddlstreamlist.SelectedItem.Text;
               string selectedClass = ddlclasslist.SelectedItem.Text;
               string selectedSession = ddlsessionlist.SelectedItem.Text;
               string selectedSection = ddlsectionnamelist.SelectedItem.Text;





               gvstudents.DataSource = dtall;
               gvstudents.DataBind();


               *//* foreach (GridViewRow row in gvStudentMarksAndGrades.Rows)
                {
                    TextBox gridmarks = row.FindControl("gridmarks") as TextBox;
                    Literal ltrSrNo = row.FindControl("ltrSrNo") as Literal;

                    gridmarks.Text = GetMarks(ltrSrNo.Text, sessionlist.SelectedItem.Text, subjectnamelist.SelectedItem.Text, examnamelist.SelectedItem.Value);

                }

               ;
                if (rblMarksOrGrades.SelectedItem.Value.ToLower() == "marks")
                {
                    gvStudentMarksAndGrades.Columns[5].Visible = false;
                    gvStudentMarksAndGrades.Columns[4].Visible = true;



                }
                else
                {
                    gvStudentMarksAndGrades.Columns[4].Visible = false;
                    gvStudentMarksAndGrades.Columns[5].Visible = true;
                }*//*




           }
           catch { }
       }

       protected void gvstudents_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           if (e.CommandName == "Select")
           {
               //Determine the RowIndex of the Row whose Button was clicked.
               int rowIndex = Convert.ToInt32(e.CommandArgument);

               //Reference the GridView Row.
               GridViewRow row = gvstudents.Rows[rowIndex];

               //Fetch value of Name.
               string studentname = (row.FindControl("ltrStudentname") as Literal).Text.ToString();
               string rollno = (row.FindControl("ltrRollNo") as Literal).Text.ToString();
               string srno = (row.FindControl("ltrSrNo") as Literal).Text.ToString();
               string structurename = ddlstructurenames.SelectedItem.ToString();

               string classnm =ddlclasslist.SelectedItem.ToString();
               string sectionnm =ddlsectionnamelist.SelectedItem.ToString();


               Session["studentname"] = studentname;
               Session["Rollno"] = rollno;
               Session["SRNo"] = srno;
               Session["StructureName"] = structurename;

               Session["Sectionnm"] = sectionnm;
               Session["classnm"] = classnm;
               Server.Transfer("WebForm4.aspx");

           }
       }*/
    }
}