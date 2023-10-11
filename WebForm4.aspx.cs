using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Metadata;
using static System.Collections.Specialized.BitVector32;
using System.Web.UI.HtmlControls;
using NUnit.Framework;
using System.Xml.Linq;
using static Humanizer.On;
using Newtonsoft.Json.Linq;

namespace ExamManagement
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string SRno = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["SessionName"] != null)
                {
                    string sessionName = Request.QueryString["SessionName"].ToString();
                    string structurename = Request.QueryString["StructureName"].ToString();
                    string classNmae = Request.QueryString["ClassName"].ToString();
                    string sectionName = Request.QueryString["SectionName"].ToString();
                    string streamName = Request.QueryString["StreamName"].ToString();


                    ViewState["Session"] = sessionName;
                    ViewState["StructureName"] = structurename;
                    ViewState["className"] = classNmae;
                    ViewState["SectionName"] = sectionName;
                    ViewState["StreamName"] = streamName;


                }
            }
            catch { }



            // Load Students based on student's session, class, section and stream

            // For each srno generate marksheet





            load();
        }
        private void load()
        {
            try
            {

                getStudentdetails();




            }
            catch (Exception ex) { }
        }
        private void getStudentdetails()
        {
            try
            {
                string queryall = "select s.Sr_No, s.RollNo,s.Session,s.Section,s.Class,s.DOB,s.Father_FName + ' ' +s.Father_MName + ' ' +s.Father_LName as FatherName,s.Mother_FName + ' ' +s.Mother_MName + ' ' +s.Mother_LName as MotherName, s.FName + ' ' + s.MName + ' ' + s.Lname as StudentName from student_details as s " +
                    "where s.current_class='" + ViewState["className"] + "' and Stream='" + ViewState["StreamName"] + "' and Session='" + ViewState["Session"] + "' " +
                    "and current_section='" + ViewState["SectionName"] + "' order by RollNo";

                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(queryall, connection);

                DataTable dtall = new DataTable();



                connection.Open();
                adapter.Fill(dtall);
                connection.Close();

                rptMarksheets.DataSource = dtall;
                rptMarksheets.DataBind();

                /*for (global::System.Int32 i = 0; i < dtall.Rows.Count; i++)
                {
                    string StudentName = dtall.Rows[i]["StudentName"].ToString();
                }
*/

            }
            catch (Exception ex) { }
        }
        private void gettable()
        {
            string querytable = "select distinct Tables_name from marksheetdesigns";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapterall = new SqlDataAdapter(querytable, connection);

            DataTable dttable = new DataTable();
            connection.Open();
            adapterall.Fill(dttable);
            connection.Close();


            for (var i = 0; i < dttable.Rows.Count; i++)
            {
                var table = dttable.Rows[i][0].ToString();

                string queryColumns = "select Columns_names from marksheetdesigns where Structures_name = '" + ViewState["StructureName"] + "' and Tables_name = '" + table + "'";




                SqlDataAdapter adapter = new SqlDataAdapter(queryColumns, connection);

                DataTable dtcol = new DataTable();
                connection.Open();
                adapter.Fill(dtcol);
                connection.Close();
                DataTable dtfinal = new DataTable();
                for (global::System.Int32 j = 0; j < dtcol.Rows.Count; j++)
                {
                    dtfinal.Columns.Add();
                }
            }
        }

        protected void rptMarksheets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string structure = Request.QueryString["StructureName"].ToString();
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label structurename = e.Item.FindControl("lblStructurenm") as Label;
                Label Srno = e.Item.FindControl("lblsrno") as Label;
                Panel panelofgridview = e.Item.FindControl("panelGridViews") as Panel;
                structurename.Text = structure;
                SRno = Srno.Text;
                var a = getdatatable().Tables.Count;





                for (int i = 0; i < a; i++)
                {
                    panelofgridview.Controls.Add(getgrid(i));
                    panelofgridview.Controls.Add(new LiteralControl("&nbsp;"));
                    panelofgridview.Controls.Add(new LiteralControl("&nbsp;"));
                    panelofgridview.Controls.Add(new LiteralControl("&nbsp;"));
                }
            }
        }
        private GridView getgrid(int x)
        {
            GridView gridView = new GridView();
            gridView.AutoGenerateColumns = true;
            gridView.ID = "gridview";
            gridView.CssClass = "grid";




            gridView.DataSource = getdatatable().Tables[x];
            gridView.DataBind();



            return gridView;
        }

        private DataSet getdatatable()
        {
            string structure = Request.QueryString["StructureName"].ToString();

            string querystructureall = "select * from marksheetdesigns";
            string querystructuretab = "select distinct Tables_name from marksheetdesigns where Structures_name='" + structure + "'";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapterall = new SqlDataAdapter(querystructureall, connection);
            SqlDataAdapter adaptertables = new SqlDataAdapter(querystructuretab, connection);


            DataTable dtstructureall = new DataTable();
            DataTable dtstructuretables = new DataTable();

            connection.Open();
            adapterall.Fill(dtstructureall);
            adaptertables.Fill(dtstructuretables);





            DataSet ds = new DataSet();


            for (int i = 0; i < dtstructuretables.Rows.Count; i++)
            {

                DataTable dtmarksheet = new DataTable();
                dtmarksheet.Columns.Add("Subjects");

                string table = dtstructuretables.Rows[i][0].ToString();
                double total = 0;
                string querystructurecol = "select Columns_names from marksheetdesigns where Structures_name='" + structure + "' and Tables_name='" + table + "'";
                SqlDataAdapter adaptercolumns = new SqlDataAdapter(querystructurecol, connection);
                DataTable dtstructurecolumns = new DataTable();
                adaptercolumns.Fill(dtstructurecolumns);
                string exmnm = "";
                if (dtstructurecolumns.Rows.Count > 0)
                {

                    for (int j = 0; dtstructurecolumns.Rows.Count > j; j++)
                    {
                        exmnm = dtstructurecolumns.Rows[j][0].ToString();


                        dtmarksheet.Columns.Add(exmnm);


                    }



                    if (i != 1)
                    {
                        dtmarksheet.Columns.Add("Grand Total");
                        dtmarksheet.Columns.Add("Grade");
                        //in this row we have max marks
                        DataRow drmax = dtmarksheet.NewRow();
                        drmax[0] = " ";

                        for (int j = 1; j < dtmarksheet.Columns.Count - 2; j++)
                        {

                            if (getmaxmarks().Rows.Count > 0)
                            {
                                for (var m = 0; m < getmaxmarks().Rows.Count; m++)
                                {


                                    if (dtmarksheet.Columns[j].ColumnName.Trim().ToLower() == "term1")
                                    {
                                        double xyz = 0;
                                        double max = 0;
                                        try
                                        {
                                            double per = double.Parse(getcalsign(dtmarksheet.Columns[j].ColumnName.Trim()).Rows[0][1].ToString());
                                            double max1 = double.Parse(getmaxmarks("UT1").Rows[0]["Max_marks"].ToString());
                                            double max2 = double.Parse(getmaxmarks("UT2").Rows[0]["Max_marks"].ToString());
                                            max = max1 + max2;
                                            if (per != 0 && max != 0)
                                            {
                                                xyz = (per / 100) * max;
                                                drmax[j] = xyz;

                                            }
                                            else
                                            {
                                                drmax[j] = max;

                                            }
                                        }
                                        catch (Exception e)
                                        {

                                        }

                                    }
                                    else if (dtmarksheet.Columns[j].ColumnName.Trim().ToLower() == "term2")
                                    {
                                        double max = 0;
                                        try
                                        {
                                            double per = double.Parse(getcalsign(dtmarksheet.Columns[j].ColumnName.Trim()).Rows[0][1].ToString());
                                            double max1 = double.Parse(getmaxmarks("UT3").Rows[0]["Max_marks"].ToString());
                                            double max2 = double.Parse(getmaxmarks("UT4").Rows[0]["Max_marks"].ToString());
                                            max = max1 + max2;
                                            if (per != 0 && max != 0)
                                            {
                                                double xyz = Math.Round((per / 100) * max);
                                                drmax[j] = xyz;

                                            }
                                            else
                                            {
                                                drmax[j] = max;

                                            }
                                        }
                                        catch (Exception e)
                                        {

                                        }

                                        // drmax[j] = "10";
                                    }

                                    else
                                    {
                                        if (dtmarksheet.Columns[j].ColumnName.Trim().ToLower() == getmaxmarks().Rows[m]["Exam_name"].ToString().Trim().ToLower())
                                        {
                                            double per = double.Parse(getcalsign(dtmarksheet.Columns[j].ColumnName.Trim()).Rows[0][1].ToString());
                                            double max = double.Parse(getmaxmarks(dtmarksheet.Columns[j].ColumnName).Rows[0]["Max_marks"].ToString());
                                            if (per != 0 && max != 0)
                                            {
                                                double xyz = (per / 100) * max;
                                                drmax[j] = xyz;

                                            }
                                            else
                                            {
                                                drmax[j] = max;

                                            }
                                        }

                                    }


                                }


                            }
                            else
                            {

                                drmax[j] = " ";

                            }
                        }




                        dtmarksheet.Rows.Add(drmax);

                        //adding max marks total
                        try
                        {
                            DataRow maxMarksRow = dtmarksheet.Rows[0];

                            double value = 0;



                            for (int k = 1; k < maxMarksRow.ItemArray.Length - 2; k++)
                            {
                                var item = maxMarksRow.ItemArray[k];
                                if (item.ToString() != " ")
                                {
                                    double xx = double.Parse(item.ToString());
                                    value += xx;
                                }
                                else
                                {
                                    double xx = 0;
                                    value += xx;
                                }
                            }


                            for (int j1 = dtmarksheet.Columns.Count - 2; j1 < dtmarksheet.Columns.Count - 1; j1++)
                            {



                                drmax[j1] = value;



                            }
                        }
                        catch { }

                    }

                    int x = i + 1;
                    for (int s = 0; s < getsubjects(x).Rows.Count; s++)
                    {
                        DataRow dr = dtmarksheet.NewRow();
                        string subject = getsubjects(x).Rows[s][0].ToString();
                        string examid = "";
                        string srno = SRno;
                        dr[0] = subject;
                        //entering marks
                        for (int j = 1; j < dtmarksheet.Columns.Count; j++)
                        {

                            //here we add marks in cells
                            string exmname = dtmarksheet.Columns[j].ColumnName.ToLower().Trim();


                            for (int i1 = 0; i1 < getexamid().Rows.Count; i1++)
                            {

                                string exmnamedt = getexamid().Rows[i1]["Exam_name"].ToString().ToLower().Trim();

                                if (exmname == exmnamedt)
                                {


                                    try
                                    {
                                        examid = getexamid().Rows[i1]["Examid"].ToString();
                                        double max = double.Parse(getmaxmarks(getexamid().Rows[i1]["Exam_name"].ToString().Trim()).Rows[0]["Max_marks"].ToString());

                                        int mark = int.Parse(getmarks(subject, examid, srno).Rows[0][0].ToString().Trim());

                                        string grade = getmarks(subject, examid, srno).Rows[0][1].ToString().Trim();




                                        if (mark != 0)
                                        {
                                            string per = getcalsign(exmname).Rows[0][1].ToString();
                                            int maxmarkindt = int.Parse(dtmarksheet.Rows[0][j].ToString());

                                            double marksoutoftotal = (((mark / max) * 100) / 100) * maxmarkindt;
                                            string formattedNumber = marksoutoftotal.ToString("N1");
                                            if (marksoutoftotal != 0)
                                            {
                                                //here we will add formula

                                                dr[j] = formattedNumber;
                                                break;
                                            }
                                        }
                                        else if (grade != " ")
                                        {
                                            dr[j] = grade.Trim();
                                            break;
                                        }
                                        else
                                        {
                                            dr[j] = "a";
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                                else if (exmname == "term1")
                                {
                                    try
                                    {
                                        string ut1 = getexamid().Rows[0]["Examid"].ToString().Trim();
                                        string ut2 = getexamid().Rows[1]["Examid"].ToString().Trim();

                                        int mark1 = int.Parse(getmarks(subject, ut1, srno).Rows[0][0].ToString().Trim());

                                        int mark2 = int.Parse(getmarks(subject, ut2, srno).Rows[0][0].ToString().Trim());


                                        double max1 = double.Parse(getmaxmarks("UT1").Rows[0]["Max_marks"].ToString());
                                        double max2 = double.Parse(getmaxmarks("UT2").Rows[0]["Max_marks"].ToString());
                                        double max = max1 + max2;

                                        //here we will add formula
                                        string sign = getcalsign(exmname).Rows[0][0].ToString();
                                        double sum = 0;
                                        double marksoutoftotal = 0;
                                        int maxmarkindt = int.Parse(dtmarksheet.Rows[0][j].ToString());
                                        string formattedNumber = "";

                                        if (sign == "SUM")
                                        {
                                            sum = mark1 + mark2;
                                            marksoutoftotal = (((sum / max) * 100) / 100) * maxmarkindt;

                                            formattedNumber = marksoutoftotal.ToString("N1");
                                        }
                                        else if (sign == "AVG")
                                        {
                                            double sum1 = mark1 + mark2;
                                            sum = sum1 / 2;
                                            marksoutoftotal = (((sum / max) * 100) / 100) * maxmarkindt;
                                            formattedNumber = marksoutoftotal.ToString("N1");
                                        }
                                        else if (sign == "MAX")
                                        {
                                            if (mark1 > mark2)
                                            {
                                                formattedNumber = mark1.ToString();
                                            }
                                            else
                                            {
                                                formattedNumber = mark2.ToString();
                                            }

                                        }


                                        string grade = getmarks(subject, ut1, srno).Rows[0][1].ToString().Trim();


                                        if (marksoutoftotal != 0)
                                        {


                                            dr[j] = formattedNumber;
                                            break;
                                        }
                                        else if (grade != " ")
                                        {
                                            dr[j] = grade.Trim();
                                            break;
                                        }
                                        else
                                        {
                                            dr[j] = "a";
                                        }
                                    }
                                    catch (Exception e) { }




                                }
                                else if (exmname == "term2")
                                {

                                    try
                                    {
                                        string formattedNumber = "";
                                        double marksoutoftotal = 0;
                                        string ut3 = getexamid().Rows[2]["Examid"].ToString().Trim();
                                        string ut4 = getexamid().Rows[3]["Examid"].ToString().Trim();
                                        string ut5 = getexamid().Rows[4]["Examid"].ToString().Trim();

                                        int mark1 = int.Parse(getmarks(subject, ut3, srno).Rows[0][0].ToString().Trim());

                                        int mark2 = int.Parse(getmarks(subject, ut4, srno).Rows[0][0].ToString().Trim());

                                        int mark3 = int.Parse(getmarks(subject, ut5, srno).Rows[0][0].ToString().Trim());

                                        string sign = getcalsign(exmname).Rows[0][0].ToString();
                                        double sum = 0;
                                        int maxmarkindt = int.Parse(dtmarksheet.Rows[0][j].ToString());


                                        double max1 = double.Parse(getmaxmarks("UT3").Rows[0]["Max_marks"].ToString());
                                        double max2 = double.Parse(getmaxmarks("UT4").Rows[0]["Max_marks"].ToString());
                                        double max = max1 + max2;

                                        if (sign == "SUM")
                                        {
                                            sum = mark1 + mark2;
                                            marksoutoftotal = (((sum / max) * 100) / 100) * maxmarkindt;
                                            formattedNumber = marksoutoftotal.ToString("N1");
                                        }
                                        else if (sign == "AVG")
                                        {
                                            double sum1 = mark1 + mark2;
                                            sum = sum1 / 2;
                                            marksoutoftotal = (((sum / max) * 100) / 100) * maxmarkindt;
                                            formattedNumber = marksoutoftotal.ToString("N1");
                                        }
                                        else if (sign == "MAX")
                                        {
                                            if (mark1 > mark2 && mark1 > mark3)
                                            {
                                                formattedNumber = mark1.ToString();
                                            }
                                            else if (mark2 > mark3 && mark2 > mark1)
                                            {
                                                formattedNumber = mark2.ToString();
                                            }
                                            else
                                            {
                                                formattedNumber = mark3.ToString();
                                            }
                                        }
                                        string grade = getmarks(subject, ut3, srno).Rows[0][1].ToString().Trim();
                                        if (formattedNumber != "")
                                        {
                                            //here we will add formula

                                            dr[j] = formattedNumber;
                                            break;
                                        }
                                        else if (grade != " ")
                                        {
                                            dr[j] = grade.Trim();
                                            break;
                                        }
                                        else
                                        {
                                            dr[j] = "a";
                                        }
                                    }
                                    catch (Exception e) { }




                                }
                                else
                                {
                                    dr[j] = " ";
                                }


                            }




                        }





                        dtmarksheet.Rows.Add(dr);
                    }
                    //grand total marks
                    if (i != 1)
                    {
                        try
                        {
                            for (global::System.Int32 j1 = 1; j1 < dtmarksheet.Rows.Count - 1; j1++)
                            {
                                DataRow maxMarksRow2 = dtmarksheet.Rows[j1];

                                double value2 = 0;



                                for (int k = 1; k < maxMarksRow2.ItemArray.Length - 2; k++)
                                {
                                    var item = maxMarksRow2.ItemArray[k];
                                    if (item.ToString() != " ")
                                    {
                                        double xx = double.Parse(item.ToString());
                                        value2 += xx;
                                    }
                                    else
                                    {
                                        double xx = 0;
                                        value2 += xx;
                                    }
                                }


                                dtmarksheet.Rows[j1]["Grand Total"] = value2;
                                if (value2 > 0)
                                {
                                    double tm = (value2 / double.Parse(dtmarksheet.Rows[0]["Grand Total"].ToString())) * 100;
                                    for (int x1 = 0; x1 < gradeoftotalmarks().Rows.Count; x1++)
                                    {
                                        double rngstrt = double.Parse(gradeoftotalmarks().Rows[x1]["Range_start"].ToString().Trim());
                                        double rngend = double.Parse(gradeoftotalmarks().Rows[x1]["Range_end"].ToString().Trim());
                                        if (tm >= rngstrt && tm <= rngend)
                                        {
                                            dtmarksheet.Rows[j1]["Grade"] = gradeoftotalmarks().Rows[x1]["Grade_text"].ToString();
                                            break;
                                        }
                                       
                                        

                                    }

                                }

                            }
                        }
                        catch (Exception e) { }



                        DataRow dr1 = dtmarksheet.NewRow();
                        dr1[0] = "Total";

                        for (int j = 1; j < dtmarksheet.Columns.Count - 1; j++)
                        {

                            try
                            {
                                double value2 = 0;

                                for (int j1 = 1; j1 < dtmarksheet.Rows.Count - 1; j1++)
                                {
                                    double totalMarkCol = double.Parse(dtmarksheet.Rows[j1][j].ToString());

                                    var item = totalMarkCol;
                                    if (item.ToString() != " ")
                                    {
                                        double xx = double.Parse(item.ToString());
                                        value2 += xx;
                                    }
                                    else
                                    {
                                        double xx = double.Parse(item.ToString());
                                        value2 += xx;
                                    }

                                }


                                dr1[j] = value2;

                            }
                            catch (Exception e) { }


                        }

                        dtmarksheet.Rows.Add(dr1);
                    }

                }

                ds.Tables.Add(dtmarksheet);


            }

            connection.Close();

            return ds;

        }

        private DataTable getsubjects(int table)
        {
            string querySubject = "select distinct Subject from tblSubjectManagement where Class='" + ViewState["className"] + "' and Stream='" + ViewState["StreamName"] + "' and Category='Table" + table + "'";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querySubject, connection);

            DataTable dtSubject = new DataTable();
            connection.Open();
            adapter.Fill(dtSubject);
            connection.Close();
            return dtSubject;
        }
        private DataTable getmarks(string subjectname, string examid, string srno)
        {
            string querymarks = "select Marks_opt,Grade from Student_Marks_details where Subject_name='" + subjectname + "' and Examid='" + examid + "' and SR_No='" + srno + "'";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querymarks, connection);

            DataTable dtmarks = new DataTable();
            connection.Open();
            adapter.Fill(dtmarks);
            connection.Close();

            return dtmarks;
        }
        private DataTable getexamid()
        {
            string querySubjectid = "select Exam_name,ExamId from Student_Marks_id order by ExamId";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querySubjectid, connection);

            DataTable dtexamid = new DataTable();
            connection.Open();
            adapter.Fill(dtexamid);
            connection.Close();
            return dtexamid;
        }
        private DataTable getmaxmarks()
        {
            string querymaxmarks = "select distinct Exam_name,ExamId,Max_marks from Student_Marks_id order by ExamId";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querymaxmarks, connection);


            adapter = new SqlDataAdapter(querymaxmarks, connection);
            DataTable dtmaxmarks = new DataTable();




            connection.Open();
            adapter.Fill(dtmaxmarks);

            connection.Close();



            return dtmaxmarks;
        }
        private DataTable getmaxmarks(string exmname)
        {
            string querymaxmarks = "select distinct Exam_name,ExamId,Max_marks from Student_Marks_id  where Exam_name='" + exmname + "'";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querymaxmarks, connection);


            adapter = new SqlDataAdapter(querymaxmarks, connection);
            DataTable dtmaxmarks = new DataTable();




            connection.Open();
            adapter.Fill(dtmaxmarks);

            connection.Close();



            return dtmaxmarks;
        }
        private DataTable getcalsign(string printnm)
        {
            string querycal = "select distinct Cal_Operation,percentage_val from Student_Marks_operation where Print_name ='" + printnm + "'";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querycal, connection);


            adapter = new SqlDataAdapter(querycal, connection);
            DataTable dtcal = new DataTable();




            connection.Open();
            adapter.Fill(dtcal);

            connection.Close();



            return dtcal;
        }
        private DataTable gradeoftotalmarks()
        {
            string query = "select * from Student_MarkstoGrade";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


            adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();




            connection.Open();
            adapter.Fill(dt);

            connection.Close();



            return dt;
        }

    }



}
