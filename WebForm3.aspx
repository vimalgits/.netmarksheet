<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="ExamManagement.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table {
            border-collapse: collapse;
            width: 40%;
            margin: auto;
        }

        th, td {
           
            text-align: center;
        }

        th {
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

        .button1 {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            background-color: #3498db;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin: 5px;
        }

            .button1:hover {
                background-color: #2980b9;
            }

        .grid {
            display: grid;
            grid-template-columns: auto auto;
            width: 100%;
            margin: auto;
        }
    </style>
    <script></script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <center>
                 <asp:LinkButton ID="GotoMain" runat="server" OnClick="GotoMain_Click">GotoMain</asp:LinkButton>
                <table>
                    <tr>
                        <td colspan="2">
                            <h2>ABC SCHOOL,AJMER</h2>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <h3>Session:</h3>

                        </td>
                        <td>
                            <asp:DropDownList CssClass="dropdown" runat="server" ID="ddlsessionlist" AutoPostBack="true" OnSelectedIndexChanged="ddlsessionlist_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblsessionselet" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3>Choose Sructure</h3>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlstructurenames" CssClass="dropdown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstructurenames_SelectedIndexChanged"></asp:DropDownList></td>
                        <td>
                            <asp:Label ID="lblstructureselet" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>

                            <h3>Class:</h3>

                        </td>
                        <td>


                            <asp:DropDownList CssClass="dropdown" runat="server" ID="ddlclasslist" AutoPostBack="true" OnSelectedIndexChanged="ddlclasslist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblclassvalidation" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>

                            <h3>Section:</h3>

                        </td>
                        <td>

                            <asp:DropDownList CssClass="dropdown" runat="server" ID="ddlsectionnamelist" AutoPostBack="true" OnSelectedIndexChanged="ddlsectionnamelist_SelectedIndexChanged">
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


                            <asp:DropDownList CssClass="dropdown" runat="server" ID="ddlstreamlist" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlstreamlist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblstreamvalidation" runat="server" Visible="true" Style="color: red" Text="please select"></asp:Label></td>
                    </tr>
                    <tr>
                    </tr>
                </table>
                <div>
                    <asp:Button ID="btnsubmit" runat="server" Text="submit" CssClass="button1" Visible="false" OnClick="btnsubmit_Click" />
                </div>
                <div style="height: 30px">
                </div>
                <div>
                  <%--  <asp:GridView ID="gvstudents" Style="width: 90%" runat="server" AutoGenerateColumns="false" OnRowCommand="gvstudents_RowCommand">
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
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Button CssClass="button1" ID="btngrid" runat="server" Text="create marksheet" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />

                                </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>--%>
                </div>
            </center>

        </div>
    </form>
</body>
</html>
