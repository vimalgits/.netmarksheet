<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ExamManagement.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif; /* Choose a readable font stack */
            font-size: 16px; /* Adjust the base font size for your content */
            line-height: 1.5; /* Improve readability with appropriate line height */
            color: #333;
        }

        h1, h2, h3, h4, h5, h6 {
            font-family: 'Helvetica Neue', sans-serif; /* Different font for headings */
            font-weight: bold; /* Vary the font weight to establish hierarchy */
            color: #222; /* Darker color than the body text */
            margin-bottom: 10px; /* Add spacing between headings and content */
        }

        .Background {
            background-color: Black;
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 1px;
            padding-bottom: 10px;
            padding-right: 10px;
            width: 300px;
            height: 200px;
            border-radius: 10px;
        }

        .button1 {
            display: inline-block;
            padding: 5px 20px;
            font-size: 16px;
            background-color: #3498db;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin: 2px;
        }

        .button2 {
            display: inline-block;
            padding: 1px 10px;
            font-size: 26px;
            background-color: #3498db;
            color: #fff;
            border: none;
            border-radius: 50px;
            cursor: pointer;
            margin: 2px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }


        .grid th {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: center;
            border-radius: 20px;
        }

        .grid td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: center;
        }



        .grid tr:nth-child(odd) {
            background-color: #f2f2f2;
        }

        .dropdown {
            padding: 10px; /* Vertical padding for some space */
            font-size: 16px; /* Font size of the text */
            border: 1px solid #ccc; /* Border for the dropdown */
            border-radius: 4px; /* Rounded corners */
            background-color: white; /* Background color of the dropdown */
            color: #333; /* Text color */
            width: 200px;
        }

            .dropdown option {
                font-size: 16px; /* Match the font size with the parent */
                padding: 8px; /* Add padding for consistent spacing */
                background-color: #fff; /* Option background color */
                color: #333; /* Option text color */
            }

        .textbox {
            width: 300px; /* Set the width of the textbox */
            padding: 10px; /* Add padding to the textbox */
            border: 1px solid #ccc; /* Add a border */
            border-radius: 5px; /* Add rounded corners */
            font-size: 16px; /* Set the font size */
            outline: none; /* Remove the default focus outline */
        }

        #gridmarkdiv {
            padding: 25px;
            margin-top: 10px;
            border-radius: 20px;
            border: 1px solid black;
            border-radius: 20px;
        }
    </style>
    <script type="text/javascript" language="javascript">

        /*function validation() {
            if (gridmarks.Text) { }
            lblvalidationmarks.visible = true;
            
        }*/

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <center>
                <asp:LinkButton ID="GotoMain" runat="server" OnClick="GotoMain_Click">GotoMain</asp:LinkButton>
                <h1>Student Marks Entry</h1>
                <h2>Enter Your Details</h2>


            </center>
            <table style="margin-top: 5px; width: 27%; margin-left: 40%;">
                <thead></thead>
                <tbody>
                    <tr>
                        <td>

                            <h3>Session:</h3>

                        </td>
                        <td>
                            <asp:DropDownList CssClass="dropdown" runat="server" ID="sessionlist" Width="150px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3>
                                <asp:Label ID="lblexam" runat="server" AutoPostback="true" Text="Exam Name:"></asp:Label></h3>


                        </td>
                        <td>


                            <asp:DropDownList CssClass="dropdown" runat="server" ID="examnamelist" Width="150px">
                            </asp:DropDownList>


                            <asp:Button ID="BtnSubjectadd" CssClass="button2" runat="server" Text="+" OnClick="BtnSubjectadd_Click" />

                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="ModalPanel" TargetControlID="BtnSubjectadd" CancelControlID="BackBtnpop" BackgroundCssClass="Background"></cc1:ModalPopupExtender>

                            <asp:Panel ID="ModalPanel" runat="server" Visible="false" CssClass="Popup" align="center">

                                <div>
                                    <div style="height: 10px"></div>
                                    <div>
                                    </div>
                                    Exam Name:<asp:TextBox ID="popuptxtbtnexmnm" runat="server" AutoPostBack="true" Width="70px" Style="align-items: center" OnTextChanged="popuptxtbtnexmnm_TextChanged"></asp:TextBox>

                                    <asp:Label ID="lblpopnm" Style="align-items: end; color: red" runat="server" Text=" Invalid value"></asp:Label>
                                </div>
                                <div style="height: 10px"></div>
                                <div>
                                    Maximum Marks:<asp:TextBox ID="popuptxtbtnexmmaxmarks" runat="server" AutoPostBack="true" Width="70px" Style="align-items: center" OnTextChanged="popuptxtbtnexmmaxmarks_TextChanged"></asp:TextBox>

                                    <asp:Label ID="lblpopmarks" runat="server" Style="align-items: end; color: red" Text=" Invalid value"></asp:Label>


                                </div>
                                <div style="height: 10px"></div>
                                <div>
                                    <asp:Label ID="lblpop" runat="server" Visible="false" Text=""></asp:Label>
                                </div>
                                <div style="height: 10px"></div>
                                <div>
                                    <asp:Button ID="addButton" CssClass="button1" runat="server" Text="Add" OnClick="OKButton_Click" />
                                    <asp:Button ID="BackBtnpop" CssClass="button1" runat="server" Text="Cancel" OnClick="BackBtnpop_Click" />
                                </div>

                            </asp:Panel>

                        </td>

                    </tr>
                    <tr>
                        <td>

                            <h3>Class:</h3>

                        </td>
                        <td>


                            <asp:DropDownList CssClass="dropdown" runat="server" ID="classlist" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="classlist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblclassvalidation" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>

                            <h3>Section:</h3>

                        </td>
                        <td>

                            <asp:DropDownList CssClass="dropdown" runat="server" ID="sectionnamelist" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="sectionnamelist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblsectionvalidation" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>

                            <h3>Stream:</h3>

                        </td>
                        <td>


                            <asp:DropDownList CssClass="dropdown" runat="server" ID="streamlist" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="streamlist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblstreamvalidation" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>

                            <h3>Subject:</h3>

                        </td>
                        <td>

                            <asp:DropDownList CssClass="dropdown" runat="server" ID="subjectnamelist" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="subjectnamelist_SelectedIndexChanged">
                            </asp:DropDownList>
                            <td>
                                <asp:Label ID="lblsubjectvalidation" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label></td>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>

                            <asp:RadioButtonList ID="rblMarksOrGrades" runat="server" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="rblMarksOrGrades_SelectedIndexChanged">
                                <asp:ListItem Text="Marks" Value="marks" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Grades" Value="grades"></asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                    </tr>
                    <tr></tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnShowStudents" CssClass="button1" runat="server" Text="Show" OnClick="btnShowStudents_Click" />

                        </td>
                    </tr>



                </tbody>



            </table>

        </div>
        <center>
            <div id="gridmarkdiv">
                <asp:GridView ID="gvStudentMarksAndGrades" CssClass="grid" AutoGenerateColumns="false" runat="server" Width="100%" OnRowDataBound="gvStudentMarksAndGrades_RowDataBound">
                    <Columns>

                        <asp:TemplateField HeaderText="S. No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RollNo">
                            <ItemTemplate>
                                <asp:Literal ID="ltrRollNo" runat="server" Text='<%# Eval("RollNo") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Sr_No">
                            <ItemTemplate>
                                <asp:Literal ID="ltrSrNo" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>
                                <asp:Literal ID="ltrStudentname" runat="server" Text='<%# Eval("StudentName") %>'></asp:Literal>

                            </ItemTemplate>

                        </asp:TemplateField>









                        <asp:TemplateField HeaderText="Marks" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>

                                <asp:TextBox ID="gridmarks" CssClass="textbox" runat="server" Width="50px" Style="align-content: center" OnLoad="gridmarks_Load"></asp:TextBox>

                                <asp:Literal ID="lblvalidationmarks" runat="server" Text="invalid output" Visible="false"></asp:Literal>

                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:TextBox ID="gridgrade" CssClass="textbox" runat="server" Width="50px" Style="align-content: center"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>



        </center>
        <div style="text-align: center">
            <asp:Button ID="btn_insert" runat="server" CssClass="button1" OnClick="btn_insert_Click"
                Text="Insert Records" />
            <asp:Button ID="btncancel" runat="server" CssClass="button1" OnClick="btncancel_Click"
                Text="Cancel" />


        </div>
        <div>
            <center>
                <h4>
                    <asp:Label ID="Label1" runat="server"></asp:Label></h4>
            </center>
        </div>





    </form>
</body>
</html>
