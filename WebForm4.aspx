<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="ExamManagement.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table {
            border-collapse: collapse;
            width: 50%;
        }

        .grid {
            font-family: Arial, sans-serif;
            width: 80%;
            border-collapse: collapse;
            border: 1px solid #ccc;
        }

            .grid th {
                background-color: #f2f2f2;
                border: 1px solid #ccc;
                padding: 8px;
                text-align: center;
            }

            .grid td {
                border: 1px solid #ccc;
                padding: 8px;
                text-align: center;
            }

            .grid tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .grid tr:nth-child(odd) {
                background-color: #ffffff;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Repeater ID="rptMarksheets" runat="server" OnItemDataBound="rptMarksheets_ItemDataBound">
            <ItemTemplate>
                <center>
                    <h1>SCHOOL NAME,AJMER</h1>
                    <h2>
                        <asp:Label ID="lblStructurenm" CssClass="marksheetname" runat="server"></asp:Label>

                    </h2>
                    <h3>
                        <%# Eval("Session") %>

                    </h3>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <h3>Student Name:</h3>
                                </td>
                                <td>

                                    <asp:Label ID="lblStudentname" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>

                                </td>
                                <td>
                                    <h3>SR No.</h3>
                                </td>
                                <td>
                                   
                                    <asp:Label ID="lblsrno" runat="server" Text=' <%# Eval("Sr_No") %>'></asp:Label>
                                </td>
                            </tr>

                            <tr>

                                <td>
                                    <h3>DOB:</h3>
                                </td>
                                <td>

                                    <%# Eval("DOB") %>

                                </td>
                                <td>
                                    <h3>Class & Section:</h3>
                                </td>
                                <td>

                                    <%# Eval("Class") %> <%# Eval("Section") %>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h3>Father's Name:</h3>
                                </td>
                                <td>
                                    <%# Eval("FatherName") %>

                                </td>
                                <td>
                                    <h3>Roll No.</h3>
                                </td>
                                <td>
                                    <%# Eval("RollNo") %>
                                    <%--<asp:Label ID="lblrollno" runat="server"></asp:Label></td>--%>
                            </tr>
                            <tr>
                                <td>
                                    <h3>Mother's Name:</h3>
                                </td>
                                <td>
                                    <%# Eval("MotherName") %>    
                                </td>

                            </tr>
                        </table>
                        <div style="height:10%"></div>
                        <asp:Panel ID="panelGridViews" runat="server" CssClass="gridview-panel">
                        </asp:Panel>
                    </div>
                </center>
            </ItemTemplate>
        </asp:Repeater>



    </form>
</body>
</html>
