using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Reflection.Emit;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using AjaxControlToolkit;
using System.Drawing;
using System.Linq.Expressions;

namespace ExamManagement
{
    public partial class WebForm1 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loadpage();
            }

        }


        private void Loadpage()
        {
            btncancel.Visible = false;
            // Load Session
            string querySession = "select distinct Session from student_details order by session desc";



            // Load Classes
            string queryClasses = "select distinct Current_Class from student_details order by Current_Class";


            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            string ConnectionString2 = System.Configuration.ConfigurationManager.ConnectionStrings["mycs2"].ConnectionString;


            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlDataAdapter adapter = new SqlDataAdapter(querySession, connection);


            DataTable dtSession = new DataTable();

            connection.Open();
            adapter.Fill(dtSession);
            connection.Close();



            ExamnameList();

            adapter = new SqlDataAdapter(queryClasses, connection);
            DataTable dtClasses = new DataTable();

            connection.Open();
            adapter.Fill(dtClasses);
            connection.Close();





            sessionlist.DataSource = dtSession;
            sessionlist.DataTextField = "Session";
            sessionlist.DataValueField = "Session";
            sessionlist.DataBind();





            classlist.DataSource = dtClasses;
            classlist.DataTextField = "Current_Class";
            classlist.DataValueField = "Current_Class";
            classlist.DataBind();



        }
        //load exam name in dropdown
        private void ExamnameList()
        {
            // Load Exam Names

            string queryExamName = "select distinct ExamId,Exam_name,Max_marks from Student_Marks_id";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryExamName, connection);


            adapter = new SqlDataAdapter(queryExamName, connection);
            DataTable dtExamNames = new DataTable();




            connection.Open();
            adapter.Fill(dtExamNames);

            connection.Close();

            examnamelist.DataSource = dtExamNames;
            examnamelist.DataTextField = "Exam_name";
            examnamelist.DataValueField = "ExamId";
            examnamelist.DataBind();

            for (var i = 0; i < dtExamNames.Rows.Count; i++)
            {
                dtExamNames.Rows[i]["Max_marks"].ToString();
            }




        }
        //load class name in dropdown
        protected void classlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblclassvalidation.Visible = false;
            string selectedClass = classlist.SelectedItem.Text;

            string querySection = "select distinct current_section from student_details where Current_Class='" + selectedClass + "' order by Current_Section";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;


            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querySection, connection);

            DataTable dtSection = new DataTable();

            connection.Open();
            adapter.Fill(dtSection);
            connection.Close();


            sectionnamelist.DataSource = dtSection;
            sectionnamelist.DataTextField = "current_section";
            sectionnamelist.DataValueField = "current_section";
            sectionnamelist.DataBind();

            sectionnamelist.Items.Add(new ListItem { Text = "Please select", Selected = true });

        }
        //load section name in dropdown
        protected void sectionnamelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblsectionvalidation.Visible = false;
            string selectedClass = classlist.SelectedItem.Text;
            string selectedSection = sectionnamelist.SelectedItem.Text;
            string queryStream = "select distinct Stream from student_details where current_class='" + selectedClass + "' AND current_section='" + selectedSection + "'";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;


            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryStream, connection);

            DataTable dtStream = new DataTable();
            connection.Open();
            adapter.Fill(dtStream);
            connection.Close();

            streamlist.DataSource = dtStream;
            streamlist.DataTextField = "Stream";
            streamlist.DataValueField = "Stream";
            streamlist.DataBind();

            streamlist.Items.Add(new ListItem { Text = " select", Selected = true });


        }
        //load Subject name in dropdown
        protected void streamlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblstreamvalidation.Visible = false;
            lblsubjectvalidation.Visible = false;
            string selectedStream = streamlist.SelectedItem.Text;
            string selectedClass = classlist.SelectedItem.Text;
            string querySubject = "select * from tblSubjectManagement where Class='" + selectedClass + "' and Stream='" + selectedStream + "'";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(querySubject, connection);

            DataTable dtSubject = new DataTable();
            connection.Open();
            adapter.Fill(dtSubject);
            connection.Close();

            subjectnamelist.DataSource = dtSubject;
            subjectnamelist.DataTextField = "Subject";
            subjectnamelist.DataValueField = "Subject";
            subjectnamelist.DataBind();








        }

        //load grid
        private void LoadGridView()
        {

            try
            {
                gvStudentMarksAndGrades.Columns[5].Visible = false;

                string selectedStream = streamlist.SelectedItem.Text;
                string selectedClass = classlist.SelectedItem.Text;
                string selectedSession = sessionlist.SelectedItem.Text;
                string selectedSection = sectionnamelist.SelectedItem.Text;
                string selectSubject = subjectnamelist.SelectedItem.Text;









                string queryall = "select s.Sr_No, s.RollNo, s.FName + ' ' + s.MName + ' ' + s.Lname as StudentName from student_details as s " +
                    "where s.current_class='" + selectedClass + "' and Stream='" + selectedStream + "' and Session='" + selectedSession + "' " +
                    "and current_section='" + selectedSection + "' order by RollNo";

                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(queryall, connection);

                DataTable dtall = new DataTable();



                connection.Open();
                adapter.Fill(dtall);
                connection.Close();

                gvStudentMarksAndGrades.DataSource = dtall;
                gvStudentMarksAndGrades.DataBind();


                foreach (GridViewRow row in gvStudentMarksAndGrades.Rows)
                {
                    TextBox gridmarks = row.FindControl("gridmarks") as TextBox;
                    TextBox gridgrade = row.FindControl("gridgrade") as TextBox;
                    Literal ltrSrNo = row.FindControl("ltrSrNo") as Literal;

                    gridmarks.Text = GetMarks(ltrSrNo.Text, sessionlist.SelectedItem.Text, subjectnamelist.SelectedItem.Text, examnamelist.SelectedItem.Value);
                    gridgrade.Text = Getgrade(ltrSrNo.Text, sessionlist.SelectedItem.Text, subjectnamelist.SelectedItem.Text, examnamelist.SelectedItem.Value);


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
                }




            }
            catch { }

        }
        //mark select
        private string GetMarks(string srNo, string session, string subjectName, string examId)
        {

            string marks = "";
            string query = "select * from Student_Marks_details where SR_No='" + srNo + "' and Subject_name='" + subjectName + "' and Examid='" + examId + "' and Session_s='" + session + "'";

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
        //GET GRADE
        private string Getgrade(string srNo, string session, string subjectName, string examId)
        {

            string Grade = "";
            string query = "select * from Student_Marks_details where SR_No='" + srNo + "' and Subject_name='" + subjectName + "' and Examid='" + examId + "' and Session_s='" + session + "'";

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataTable dtall = new DataTable();

            connection.Open();
            adapter.Fill(dtall);
            connection.Close();

            if (dtall.Rows.Count > 0)
            {
                Grade = dtall.Rows[0]["Grade"].ToString();
            }

            return Grade;
        }
        //insert button
        protected void btn_insert_Click(object sender, EventArgs e)
        {



            btncancel.Visible = true;


            foreach (GridViewRow row in gvStudentMarksAndGrades.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Literal ltrRollNo = row.FindControl("ltrRollNo") as Literal;
                    Literal ltrSrNo = row.FindControl("ltrSrNo") as Literal;
                    Literal ltrStudentname = row.FindControl("ltrStudentname") as Literal;
                    TextBox gridmarks = row.FindControl("gridmarks") as TextBox;
                    TextBox gridgrade = row.FindControl("gridgrade") as TextBox;


                    string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
                    SqlConnection con = new SqlConnection(ConnectionString);
                    string queryMax = "select ExamId,Max_marks from Student_Marks_id where ExamId = '" + examnamelist.SelectedItem.Value + "'";
                    string student_marks_query = "insert into " +
                        "Student_Marks_details (Examid,Subject_name,Marks_opt,Sr_No,Session_s,Class_Name,Section,Grade,Rollno,ExamName,Stream_student,Student_Name) " +
                        "values(@Examid,@Subject_name,@Marks_opt,@Sr_No,@Session_s,@Class_Name,@Section,@Grade,@Rollno,@ExamName,@Stream_student,@Student_Name)";

                    string queryDuplicatecheck = "select *  from Student_Marks_details  where SR_No='" + ltrSrNo.Text + "'";




                    SqlDataAdapter adapter = new SqlDataAdapter(queryMax, con);
                    SqlDataAdapter adapterDup = new SqlDataAdapter(queryDuplicatecheck, con);




                    SqlCommand student_marks_cmd = new SqlCommand(student_marks_query, con);

                    DataTable dtMax = new DataTable();
                    DataTable dtDuplicatecheck = new DataTable();


                    adapter.Fill(dtMax);
                    adapterDup.Fill(dtDuplicatecheck);


                    student_marks_cmd.Parameters.AddWithValue("@Session_s", sessionlist.SelectedItem.Value);
                    student_marks_cmd.Parameters.AddWithValue("@Examid", examnamelist.SelectedItem.Value);
                    student_marks_cmd.Parameters.AddWithValue("@Subject_name", subjectnamelist.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Marks_opt", gridmarks.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Sr_No", ltrSrNo.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Class_Name", classlist.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Section", sectionnamelist.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Grade", gridgrade.Text);
                    student_marks_cmd.Parameters.AddWithValue("@RollNo", ltrRollNo.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Examname", examnamelist.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Stream_student", streamlist.Text);
                    student_marks_cmd.Parameters.AddWithValue("@Student_Name", ltrStudentname.Text);






                    try
                    {
                        if (dtDuplicatecheck.Rows.Count > 0)
                        {
                            for (global::System.Int32 j = 0; j < dtDuplicatecheck.Rows.Count; j++)
                            {

                                if (gridmarks.Text != "" && dtDuplicatecheck.Rows[j]["SR_No"].ToString().Trim().ToLower() == ltrSrNo.Text.Trim().ToLower())
                                {

                                    if (int.Parse(dtMax.Rows[0]["Max_marks"].ToString()) >= int.Parse(gridmarks.Text) && gridmarks.Text != "" && int.Parse(gridmarks.Text) != 0)
                                    {
                                        UpdateDb();
                                        break;
                                    }
                                    else
                                    {
                                        Response.Write("Invalid Input");
                                        break;
                                    }

                                }
                                else
                                {
                                    Label1.Text = "Invalid Input ";


                                }
                            }

                        }
                        else if (dtDuplicatecheck.Rows.Count == 0)
                        {
                            if (gridmarks.Text.ToLower().Trim()=="ab"&& gridmarks.Text.ToLower().Trim() == "ab"&& gridmarks.Text.ToLower().Trim() == "ab")
                            {
                                
                            }

                            if (int.Parse(dtMax.Rows[0]["Max_marks"].ToString()) >= int.Parse(gridmarks.Text) && gridmarks.Text!= "" && int.Parse(gridmarks.Text) != 0)
                            {
                                con.Open();
                                student_marks_cmd.ExecuteNonQuery();
                                con.Close();
                                Response.Write(gridmarks.Text);
                                Label1.Text = "Records inserted successfully";
                                break;
                            }
                            else
                            {
                                Response.Write("marks should't more then maximum");
                            }


                        }
                        else
                        {
                            Response.Write(gridmarks.Text);
                        }

                    }
                    catch (Exception ex) { }
                }


            }



        }
        //mark update button
        private void UpdateDb()
        {
            try
            {
                foreach (GridViewRow row in gvStudentMarksAndGrades.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {


                        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
                        SqlConnection con = new SqlConnection(ConnectionString);

                        Literal ltrStudentname = row.FindControl("ltrStudentname") as Literal;
                        TextBox gridmarks = row.FindControl("gridmarks") as TextBox;
                        TextBox gridgrade = row.FindControl("gridgrade") as TextBox;
                        CheckBox chkboxupdate = row.FindControl("chkboxupdate") as CheckBox;
                        Literal lblvalidationmarks = row.FindControl("lblvalidationmarks") as Literal;

                        string student_marks_query = "update Student_Marks_details set Marks_opt='" + gridmarks.Text + "' WHERE Subject_name='" + subjectnamelist.Text + "' and ExamName='" + examnamelist.SelectedValue + "' and Student_Name='" + ltrStudentname.Text + "'";
                        SqlCommand student_marks_cmd = new SqlCommand(student_marks_query, con);








                        if (true)
                        {
                            con.Open();

                            student_marks_cmd.ExecuteNonQuery();
                            Label1.Text = "Records Updated successfully";


                            con.Close();
                            break;


                        }
                        else
                        {


                            lblvalidationmarks.Visible = true;
                            break;

                        }




                    }
                    break;

                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private bool GetMarkValidation()
        {
            bool dat = false;
            foreach (GridViewRow row in gvStudentMarksAndGrades.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
                    SqlConnection con = new SqlConnection(ConnectionString);
                    string queryMax = "select ExamId,Max_marks from Student_Marks_id order by ExamId";
                    SqlDataAdapter adapter = new SqlDataAdapter(queryMax, con);
                    DataTable dtMax = new DataTable();
                    adapter.Fill(dtMax);
                    TextBox gridmarks = row.FindControl("gridmarks") as TextBox;
                    TextBox gridgrade = row.FindControl("gridgrade") as TextBox;
                    Literal lblvalidationmarks = row.FindControl("lblvalidationmarks") as Literal;


                    for (var i = 0; i < gvStudentMarksAndGrades.Rows.Count; i++)
                    {
                        if ((int.Parse(dtMax.Rows[i]["ExamId"].ToString()) == int.Parse(examnamelist.SelectedValue.ToString())))
                        {

                            if ((gridmarks.Text != "" || gridgrade.Text != ""))
                            {
                                if ((int.Parse(dtMax.Rows[i]["Max_marks"].ToString()) >= int.Parse(gridmarks.Text)))
                                {
                                    lblvalidationmarks.Text = "";
                                    Response.Write(int.Parse(dtMax.Rows[i]["Max_marks"].ToString()));
                                    dat = true;
                                    break;
                                }
                                else
                                {
                                    lblvalidationmarks.Text = "\n" + "Marks Must be less than or Equal to " + int.Parse(dtMax.Rows[i]["Max_marks"].ToString()) + " ";
                                    dat = false;
                                    break;

                                }
                            }
                            else
                            {
                                lblvalidationmarks.Text = "Marks Must Have some value";
                                dat = false;
                                break;
                            }



                        }
                    }
                }
            }
            return dat;

        }



        protected void btnShowStudents_Click(object sender, EventArgs e)
        {


            LoadGridView();

        }




        //radio button index change
        protected void rblMarksOrGrades_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rblMarksOrGrades.SelectedItem.Value.ToLower() == "marks")
            {
                gvStudentMarksAndGrades.Columns[5].Visible = false;
                gvStudentMarksAndGrades.Columns[4].Visible = true;



            }
            else
            {
                gvStudentMarksAndGrades.Columns[4].Visible = false;
                gvStudentMarksAndGrades.Columns[5].Visible = true;
            }
        }

        protected void gvStudentMarksAndGrades_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }



        protected void btncancel_Click(object sender, EventArgs e)
        {
            Label1.Text = " ";

        }

        protected void subjectnamelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblsubjectvalidation.Visible = false;
        }



        protected void gridmarks_Load(object sender, EventArgs e)
        {
            // GetMaskValidation();
        }
        //pop up
        protected void popuptxtbtnexmnm_TextChanged(object sender, EventArgs e)
        {
            if (lblpopnm.Text != "")
            {
                lblpopnm.Visible = false;
            }

        }


        protected void popuptxtbtnexmmaxmarks_TextChanged(object sender, EventArgs e)
        {

            if (lblpopmarks.Text != "")
            {
                lblpopmarks.Visible = false;
            }
        }
        protected void BtnSubjectadd_Click(object sender, EventArgs e)
        {
            ModalPanel.Visible = true;
            examnamelist.Visible = false;
            BtnSubjectadd.Visible = false;
            lblexam.Visible = false;


        }

        private bool IsExamNameExists(string examName)
        {
            bool result = false;
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);
            string checkduplicate = "select * from Student_Marks_id where exam_name='" + examName + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(checkduplicate, con);
            DataTable dtDuplicate = new DataTable();
            con.Open();
            adapter.Fill(dtDuplicate);
            con.Close();


            if (dtDuplicate.Rows.Count > 0)
                result = true;

            return result;
        }

        //pop ok button
        protected void OKButton_Click(object sender, EventArgs e)
        {

            examnamelist.Visible = true;

            BtnSubjectadd.Visible = false;
            lblexam.Visible = true;
            lblpop.Visible = true;



            try
            {
                if (popuptxtbtnexmnm.Text != "" && popuptxtbtnexmmaxmarks.Text != "" && popuptxtbtnexmmaxmarks.Text != "0")
                {
                    lblpopnm.Visible = false;
                    lblpopmarks.Visible = false;
                    string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
                    SqlConnection con = new SqlConnection(ConnectionString);

                    string Student_Marks_id_query = "insert into Student_Marks_id (Exam_name,Max_marks)values(@Exam_name,@Max_marks)";
                    SqlCommand cmd = new SqlCommand(Student_Marks_id_query, con);

                    if (!(IsExamNameExists(popuptxtbtnexmnm.Text)))
                    {
                        con.Open();

                        cmd.Parameters.AddWithValue("@Exam_name", popuptxtbtnexmnm.Text);

                        cmd.Parameters.AddWithValue("@Max_marks", popuptxtbtnexmmaxmarks.Text);

                        cmd.ExecuteNonQuery();

                        con.Close();

                        lblpop.Text = "Exam name Created";
                    }
                    else
                    {
                        Popupdate();
                        lblpop.Text = "Exam name already exists. Existing max marks updated";
                    }


                    ExamnameList();

                }
                else
                {
                    lblpop.Text = "Name and Max Marks can't be Blank";
                }

            }
            catch (Exception ex)
            {
                Response.Write("some error in popup");
            }

        }
        //pop up back
        protected void BackBtnpop_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx");

        }





        //function popup update

        private void Popupdate()
        {
            try
            {
                if (popuptxtbtnexmnm.Text != "" && popuptxtbtnexmmaxmarks.Text != "")
                {

                    string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
                    SqlConnection con = new SqlConnection(ConnectionString);

                    string Student_Marks_id_query_update = "update Student_Marks_id set Max_marks = '" + popuptxtbtnexmmaxmarks.Text + "' where Exam_name ='" + popuptxtbtnexmnm.Text + "'";
                    SqlCommand Student_Marks_id_update_cmd = new SqlCommand(Student_Marks_id_query_update, con);

                    string checkduplicate = "select Exam_name,Max_marks from Student_Marks_id order by ExamId";
                    SqlDataAdapter adapter = new SqlDataAdapter(checkduplicate, con);
                    DataTable dtDuplicate = new DataTable();
                    adapter.Fill(dtDuplicate);


                    for (int i = 0; i < dtDuplicate.Rows.Count; i++)
                    {
                        if (popuptxtbtnexmnm.Text.ToLower() == dtDuplicate.Rows[i]["Exam_name"].ToString().ToLower())
                        {
                            con.Open();

                            Student_Marks_id_update_cmd.ExecuteNonQuery();

                            con.Close();
                            Response.Write("popup updated");
                        }
                    }


                    ExamnameList();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }
        }

        protected void GotoMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:44314/main.aspx");
        }
    }
}