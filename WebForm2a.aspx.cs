using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ExamManagement
{

    public partial class WebForm2a : System.Web.UI.Page
    {
        DataSet dsTables = new DataSet();

        string gvid = "";


        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {


                if (Session["numberOfTables"] != null)
                {
                    //loadgv();
                    GridView1.Visible = true;
                    btnsbmt.Visible = false;
                    int numberOfTables = int.Parse(Session["numberOfTables"].ToString());


                    for (int m = 0; m < numberOfTables; m++)
                    {
                        GridView gv = new GridView();
                        gv.ID = "gv" + m.ToString();

                        ViewState["gridid"] = gv.ID;

                        gvid = gv.ID;
                        DataTable dtMyTable = new DataTable();
                        int numberofColumns = int.Parse(Session["numberofColumns"].ToString());

                        for (int k = 0; k < numberofColumns; k++)
                        {
                            dtMyTable.Columns.Add("col" + k.ToString());
                        }

                        dtMyTable.Rows.Add();
                        gv.DataSource = dtMyTable;
                        gv.DataBind();

                        foreach (GridViewRow row in gv.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                int countOfRowCells = row.Cells.Count;
                                for (int j = 0; j < countOfRowCells; j++)
                                {
                                    DropDownList ddl = GenerateDropdown(j);
                                    row.Cells[j].Controls.Add(ddl);
                                }
                            }
                        }


                        panMyControlArea.Controls.Add(gv);
                    }




                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsbmt_Click(object sender, EventArgs e)
        {
            //loadgv();
            GridView1.Visible = true;
            btnsbmt.Visible = false;
            int numberOfTables = int.Parse(txtbxtblcount.Text);
            Session["numberOfTables"] = numberOfTables;

            for (int m = 0; m < numberOfTables; m++)
            {
                GridView gv = new GridView();
                gv.ID = "gv" + m.ToString();
                gv.EnableViewState = true;

                Session["gridid"] = gv.ID;

                gvid = gv.ID;
                DataTable dtMyTable = new DataTable();
                int numberofColumns = int.Parse(txtbxcolcount.Text);
                Session["numberofColumns"] = numberofColumns;

                for (int k = 0; k < numberofColumns; k++)
                {
                    dtMyTable.Columns.Add("col" + k.ToString());
                }

                dtMyTable.Rows.Add();
                gv.DataSource = dtMyTable;
                gv.DataBind();

                foreach (GridViewRow row in gv.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int countOfRowCells = row.Cells.Count;
                        for (int j = 0; j < countOfRowCells; j++)
                        {
                            DropDownList ddl = GenerateDropdown(j);
                            row.Cells[j].Controls.Add(ddl);
                        }
                    }
                }


                panMyControlArea.Controls.Add(gv);

            }


        }
        private void loadgv()
        {


            int numberOfTables = int.Parse(txtbxtblcount.Text);



            //dt.Columns.Add("tables");
            for (int i = 0; i < numberOfTables - 1; i++)
            {

                dsTables.Tables.Add(CreateDynamicTable($"Table_{i}"));

                //dt.Rows.Add(dsTables.Tables[i]);


            }
            foreach (DataTable table in dsTables.Tables)
            {




                foreach (DataColumn column in table.Columns)
                {



                    BoundField boundField = new BoundField();
                    boundField.DataField = column.ColumnName;
                    boundField.HeaderText = column.ColumnName;
                    GridView1.Columns.Add(boundField);




                }




            }
            for (int j = 0; j < numberOfTables - 1; j++)
            {

                GridView1.DataSource = dsTables.Tables[j];
                GridView1.DataBind();
            }

        }

        private DataTable CreateDynamicTable(string tableName)
        {

            DataTable table = new DataTable(tableName);

            for (int i = 1; i < int.Parse(txtbxcolcount.Text) + 1; i++)
            {
                table.Columns.Add("ExamName" + i);
            }
            DataRow row = table.NewRow();
            for (int i = 0; i < table.Columns.Count; i++)
            {

                row[i] = "";// Ddlist();

            }
            table.Rows.Add(row);


            return table;
        }

        private DropDownList GenerateDropdown(int id)
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = "DD" + id;
            Session["dropdownid"] = ddl.ID;
            string queryExamName = "select distinct Exam_name from Student_Marks_id";
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryExamName, connection);


            adapter = new SqlDataAdapter(queryExamName, connection);
            DataTable dtExamNames = new DataTable();




            connection.Open();
            adapter.Fill(dtExamNames);

            string exm = "";

            for (var sub = 0; sub < dtExamNames.Rows.Count; sub++)
            {

                exm = dtExamNames.Rows[sub]["Exam_name"].ToString();
                ddl.Items.Add(new ListItem(exm));
            }
            connection.Close();

            return ddl;
        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*GridView1.Rows[0].Cells[0].Controls.Add(GenerateDropdown(1));*/
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Controls.Add(GenerateDropdown(i));
                }


            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string gridid = Session["gridid"].ToString();
            string dropdownid = Session["dropdownid"].ToString();




            /* GridView GvMyGrid = panMyControlArea.FindControl("gv0") as GridView;*/
            /*DropDownList ddl = GvMyGrid.FindControl(dropdownid) as DropDownList;*/


            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mycs"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);
            string Queryinsert = "insert into marksheetdesigns (Structures_name,Tables_name,Columns_names)values(@Structures_name,@Tables_name,@Columns_names)";
            string Queryupdate = "update marksheetdesigns set Tables_name = @Tables_name , Columns_names= @Columns_names where Structures_name='" + txtboxStructuresname.Text + "'";
            SqlCommand cmdinsert = new SqlCommand(Queryinsert);
            SqlCommand cmdupdate = new SqlCommand(Queryupdate);


            try
            {
                con.Open();
                cmdinsert.Parameters.AddWithValue("@Structures_name", txtboxStructuresname.Text);
                foreach (System.Web.UI.Control c in panMyControlArea.Controls)
                {
                    for (global::System.Int32 i = 0; i < int.Parse(txtbxtblcount.Text); i++)
                    {

                        if (c.ID == "gv" + i.ToString())
                        {

                            GridView gvMy = c as GridView;
                            cmdinsert.Parameters.AddWithValue("@Tables_name", c.ToString());
                            foreach (GridViewRow row in gvMy.Rows)
                            {
                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    DropDownList ddl = row.FindControl(dropdownid) as DropDownList;
                                    cmdinsert.Parameters.AddWithValue("@Columns_names", ddl.SelectedValue.ToString());
                                    cmdinsert.ExecuteNonQuery();

                                }
                            }
                        }

                    }
                }
                con.Close();

            }
            catch
            {
                Label1.Text = "Records not inserted ";
            }


        }
    }
}










//<asp:PlaceHolder ID="YourPlaceholder" runat="server"></asp:PlaceHolder>