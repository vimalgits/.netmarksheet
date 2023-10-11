<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2a.aspx.cs" Inherits="ExamManagement.WebForm2a" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .txtbox {
            box-sizing: border-box;
            width: 200px; /* Adjust this value as needed */
            height: 30px; /* Adjust this value as needed */
            padding: 5px; /* Adjust this value as needed */
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
            color: #333;
        }

            .txtbox:hover {
                border-color: #66afe9;
            }

            .txtbox:focus {
                outline: none;
                border-color: #5cb3fd;
                box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
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

        /*#GridView1 {
            display: flex;
            flex-direction: column;
            align-items: center;*/ /* Adjust alignment as needed */
            /*gap: 20px;*/ /* Add spacing between tables */
            /*width: 100%;*/ /* Set the width of the container */
            /*padding: 20px;*/ /* Add padding around the container */
            /*box-sizing: border-box;
        }*/


    </style>
    <script>
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div>
                <h3>Enter Structure Name:</h3>
                <asp:TextBox CssClass="txtbox" ID="txtboxStructuresname" runat="server" Placeholder="Enter MarkSheet Design Name"></asp:TextBox>
                <br />
                <h3>How Many Tables  Do You Want ?</h3>
                <asp:TextBox CssClass="txtbox" ID="txtbxtblcount" runat="server"></asp:TextBox>
                <br />
                <h3>How Many Coloums Do You Want In Tables?</h3>
                <asp:TextBox CssClass="txtbox" ID="txtbxcolcount" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnsbmt" CssClass="button1" runat="server" Text="Submit" OnClick="btnsbmt_Click" />
                <br />
                <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound">
                 
                </asp:GridView>

                <asp:Panel ID="panMyControlArea" runat="server" EnableViewState="true"></asp:Panel>


                <asp:Button ID="btnsave" CssClass="button1" runat="server" Text="Save to DataBase" OnClick="btnsave_Click" />
                <asp:Label ID="Label1" runat="server" ></asp:Label>
            </div>

        </center>
    </form>
</body>
</html>
