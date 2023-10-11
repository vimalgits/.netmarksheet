<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="ExamManagement.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


        <center>
            <br />
            <asp:LinkButton ID="MarksEntery" runat="server" OnClick="MarksEntery_Click">Marks Entery</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="CreateStructure" runat="server" OnClick="CreateStructure_Click">Create Structure</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="GenerateMarksheet" runat="server" OnClick="GenerateMarksheet_Click">Generate Marksheet</asp:LinkButton>
        </center>
        <br />

    </form>
</body>
</html>
