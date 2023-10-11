using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Reflection.Metadata;
using HtmlElement = System.Web.UI.HtmlControls.HtmlElement;
using Microsoft.CodeAnalysis;
using Document = Microsoft.CodeAnalysis.Document;
using System.Text;
using static Humanizer.On;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;


using System.IO;
using NUnit.Framework;
using TextBox = System.Web.UI.WebControls.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using Control = System.Web.UI.Control;
using Panel = System.Web.UI.WebControls.Panel;

namespace ExamManagement
{
    public partial class WebForm2 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loadtable();
            }

        }

        public void Loadtable()
        {
            string querystdnm = "select * from Student_Marks_details";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlDataAdapter adapterstudnm = new SqlDataAdapter(querystdnm, connection);




            DataTable dtheaderdata = new DataTable();


            connection.Open();


            adapterstudnm.Fill(dtheaderdata);

            connection.Close();

        }

        private void GetCol()
        {

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string queryprintnm = "select distinct Print_name from Student_Marks_Operation";
            string querysubnm = "select distinct Subject,SubjectOrder from tblSubjectManagement where Class='I' and Stream='N/A' and SubjectOrder is not null order by SubjectOrder";
            string queryMaxmarks = "select * from Student_Marks_id order by ExamId";



            SqlDataAdapter adapter2 = new SqlDataAdapter(queryprintnm, connection);
            SqlDataAdapter adapter3 = new SqlDataAdapter(querysubnm, connection);
            SqlDataAdapter adapter4 = new SqlDataAdapter(queryMaxmarks, connection);

            DataTable dtPrint = new DataTable();
            DataTable dtSubjects = new DataTable();
            DataTable dtFinal1 = new DataTable();
            DataTable dtFinal2 = new DataTable();
            DataTable dtMaxmarks = new DataTable();

            connection.Open();

            adapter2.Fill(dtPrint);
            adapter3.Fill(dtSubjects);
            adapter4.Fill(dtMaxmarks);







            //ADDING COLUMNS in table 
            if (txtbxtblcount.Text != "")
            {







               /* for (int t = 0; t < int.Parse(txtbxtblcount.Text); t++)
                {*/

                    foreach (TextBox textBox in pantxtbox.Controls.OfType<TextBox>())
                    {

                        string htmlTable1 = "";
                        htmlTable1 += "<table id='table" + textBox + "' class ='table' style ='width:400px;  height: 10%; border:1px solid black;'>";
                        htmlTable1 += "<tr>";
                        htmlTable1 += "<td>";
                        htmlTable1 += "<h4>Subjects</h4>";
                        htmlTable1 += "</td>";
                        int tdid = 0;


                        ViewState["columncount"]=textBox.Text;
                        for (int i = 0; i < int.Parse(textBox.Text); i++)
                        {
                            tdid++;
                            htmlTable1 += "<td id='Col" + tdid + "'>";

                            htmlTable1 += "<div class='example-dropzone' ondragover='onDragOver(event);' ondrop='onDrop(event);'><p></p></div>";
                            htmlTable1 += "</td>";


                        }

                        htmlTable1 += "</tr>";

                    try
                    {

                        if (dtSubjects.Rows.Count > 0)
                        {

                            string exmpara = "";
                            string exmid = "";

                            for (var sub = 0; sub < dtPrint.Rows.Count; sub++)
                            {


                                exmpara = dtPrint.Rows[sub]["Print_name"].ToString();
                                exmid = dtMaxmarks.Rows[sub]["ExamId"].ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "divcreate(1,'" + exmid + "','" + exmpara + "');", true);


                            }
                        }
                    }
                    catch { }
                        htmlTable1 += "</table>";
                        htmlTable1 += "<div style='height:50px'></div>";
                        ltrMyHtmlmarksTable.Text += htmlTable1;


                    }
               // }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Table Count Can't be Blank')", true);
            }

        }


        protected void btnsbmt_Click(object sender, EventArgs e)
        {
           
            if (txtboxStructuresname.Text != "" && txtbxtblcount.Text != "")
            {
                GetCol();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Form Count Can't be Blank')", true);
            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "gettableid();", true);

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);

            string hiddentxtcol = hidetxtcol.Value;
            string hiddentxttbl = hidetxttab.Value;
            string hiddentxtcell = hidetxtcell.Value;
            Response.Write(hiddentxttbl);
            string equery = "";


            List<string> colval = hiddentxtcol.Split(',').ToList();
            List<string> tableidval = hiddentxttbl.Split(',').ToList();
            List<string> cellval = hiddentxtcell.Split(',').ToList();







            string checkduplicate = "select * from marksheetdesigns";
            SqlDataAdapter adapter = new SqlDataAdapter(checkduplicate, con);
            DataTable dtDuplicate = new DataTable();
            con.Open();
            adapter.Fill(dtDuplicate);
            con.Close();

            try
            {
                for (int d = 0; d < dtDuplicate.Rows.Count + 1; d++)
                {
                    /*if (dtDuplicate.Rows[d]["Structures_name"].ToString() != "")
                    {}*/
                    try
                    {
                        if (txtboxStructuresname.Text.ToLower() == dtDuplicate.Rows[d]["Structures_name"].ToString().ToLower())
                        {

                            updateStructure();
                            break;


                        }
                        else
                        {

                            insertStructure();

                            break;

                        }
                    }
                    catch
                    {
                        insertStructure();
                        break;
                    }

                }
            }
            catch
            {
                lblMessage.Text = "Some Error Occured";
            }

            Response.Redirect("WebForm3.aspx");
        }

        private void insertStructure()
        {
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);

            string hiddentxtcol = hidetxtcol.Value.Trim();
            string hiddentxttbl = hidetxttab.Value.Trim();
            string hiddentxtcell = hidetxtcell.Value.Trim();
            Response.Write(hiddentxttbl);
            string equery = "";


            List<string> colval = hiddentxtcol.Split(',').ToList();
            List<string> tableidval = hiddentxttbl.Split(',').ToList();
            List<string> cellval = hiddentxtcell.Split(',').ToList();


            int k = 0;
            int t = 0;
            equery = "insert into marksheetdesigns (Structures_name,Tables_name,Columns_names)values(@Structures_name,@Tables_name,@Columns_names)";

            try
            {
                if (txtboxStructuresname.Text != "")
                {
                    for (int j = 0; j < tableidval.Count+1; j++)
                    {

                        for (int i = 0; i < colval.Count; i++)
                        {

                            if (int.Parse(ViewState["columncount"].ToString())+1 == i)
                            {
                                break;
                            }
                            if (tableidval[j] != "" && colval[k] != "")
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand(equery, con);

                                cmd.Parameters.AddWithValue("@Structures_name", txtboxStructuresname.Text);
                                cmd.Parameters.AddWithValue("@Tables_name", tableidval[j]);
                                cmd.Parameters.AddWithValue("@Columns_names", colval[k]);
                                cmd.ExecuteNonQuery();
                                con.Close();
                                k++;
                            }
                            else
                            {
                                lblMessage.Text = "ok.";
                            }

                        }
                        t++;

                    }




                    lblMessage.Text = "Structure Inserted.";
                }
            }
            catch
            {
                lblMessage.Text = "Some Error";
            }
        }

        private void updateStructure()
        {
            string hiddentxtcol = hidetxtcol.Value;
            string hiddentxttbl = hidetxttab.Value;
            Response.Write(hiddentxttbl);



            List<string> colval = hiddentxtcol.Split(',').ToList();
            List<string> tableidval = hiddentxttbl.Split(',').ToList();

            string queryupdate = "";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);

            queryupdate = "update marksheetdesigns set Tables_name = @Tables_name , Columns_names= @Columns_names where Structures_name='" + txtboxStructuresname.Text + "'";




            if (txtboxStructuresname.Text != "")
            {

                for (int j = 0; j < tableidval.Count - 1; j++)
                {

                    for (int i = 0; i < colval.Count - 1; i++)
                    {

                        if (int.Parse(ViewState["columncount"].ToString()) == i)
                        {
                            break;
                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand(queryupdate, con);

                        cmd.Parameters.AddWithValue("@Tables_name", tableidval[j]);
                        cmd.Parameters.AddWithValue("@Columns_names", colval[i]);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //}

                    }


                }
                lblMessage.Text = "Structure updated";
            }

        }
        protected void txtbxtblcount_TextChanged(object sender, EventArgs e)
        {
            lblcols.Visible = true;
            //getcoltextbox();
            for (int i = 1; i < int.Parse(txtbxtblcount.Text) + 1; i++)
            {

                this.getcoltextbox("table" + i);
            }
        }
        private void getcoltextbox(string id)
        {
            try
            {
                TextBox textBox = new TextBox();
                // Set properties for the TextBox
                textBox.ID = id;
                textBox.CssClass = "txtbox";
                textBox.TextChanged += dynamicTextBox_TextChanged;


                // Add the TextBox to the form's Controls collection

                pantxtbox.Controls.Add(new LiteralControl(" " + id + " :"));

                pantxtbox.Controls.Add(textBox);
                pantxtbox.Controls.Add(new LiteralControl("\n"));



            }
            catch
            {

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("table")).ToList();
            int i = 1;
            foreach (string key in keys)
            {
                this.getcoltextbox("table" + i);
                i++;
            }
        }
        private void dynamicTextBox_TextChanged(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "return numeric(event);", true);





        }

        protected void GotoMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:44314/main.aspx");
        }




        /*
if (item != "")
{

addcolquery = "ALTER TABLE " + txtboxmarksheetdesignnm.Text.ToUpper() + " ADD " + item + " varchar(255);";

SqlCommand cmd2 = new SqlCommand(addcolquery, con);
con.Open();
cmd2.ExecuteNonQuery();
con.Close();
}
else
{
Response.Write("no values left");
}
}
//ADDING ROWS
/*
  DataRow dr1 = dtFinal.NewRow();

  foreach (var mark in dtMaxmarks.Rows)
  {


      for (int k = 0; k < dtMaxmarks.Rows.Count; k++)
      {
          maxmarks = dtMaxmarks.Rows[k]["Max_marks"].ToString();
          dr1[k] = maxmarks;
      }

  }
  dtFinal.Rows.Add(dr1);



for (int Y = 0; Y < dtSubjects.Rows.Count; Y++)
{
htmlTable1 += "<tr>";
subjectsdt = dtSubjects.Rows[Y]["Subject"].ToString();
htmlTable1 += "<p>" + subjectsdt + "</p>";
htmlTable1 += "</tr>";
}*/







        //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ondropvalue();", true);



    }



}

/* for (int k = 0; k < 5; k++)
            {
    dtFinal1.Columns.Add("" + k);
}
DataRow dr1 = dtFinal1.NewRow();
for (int k = 0; k < dtFinal1.Columns.Count; k++)
{
    dr1[k] = "abc";
}

dtFinal1.Rows.Add(dr1);



gvmarkstab1.DataSource = dtFinal1;
gvmarkstab1.DataBind();

foreach (GridViewRow row in gvmarkstab1.Rows)
{
    Literal ltrMyHtmlmarksTable = row.FindControl("ltrMyHtmlmarksTable") as Literal;


private string GetMarks(string sub, string exmid)
        {

            string marks = "";
            string query = "select Marks_opt from Student_Marks_details where SR_No='6203' and Subject_name='" + sub + "' and Examid='" + exmid + "' and Session_s='2020-2021'";


            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataTable dtall = new DataTable();

            connection.Open();
            adapter.Fill(dtall);
            connection.Close();

            if (dtall.Rows.Count > 0)
            {
                marks = dtall.Rows[0]["Marks_opt"].ToString();
            }

            return marks;
        }
*/