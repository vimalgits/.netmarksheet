<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ExamManagement.WebForm2" %>

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

        div.maindiv {
            height: 100%;
            display: inline-table;
            position: sticky;
            font-family: sans-serif;
            font-weight: bold;
            align-content: start;
            /* background: blue;
            white-space: nowrap;
            border: 1px solid #DFA612;
            color: black;
            font-family: sans-serif;
            font-weight: bold; 
            display:inline-block;*/
        }


        div.maindiv {
            display: inline-flex;
            width: 100%;
            align-content: space-evenly;
            position: center;
        }



        .exampledraggable {
            align-content: center;
            text-align: center;
            color: black;
            border: 0px;
            border-radius: 5px;
            font-size: 15px;
            /* background-color: cornflowerblue;
            font-weight: normal;
            padding: 10px;
            width: 10%;
            */
        }



        .example-dropzone {
            width: 20%;
            margin: 0%;
            border: 1px dashed white !important;
            font-family: calibri;
            color: black;
            text-align: center;
            /* background-color: aliceblue;
            
            padding: 10px;
            width: 90%;
            
            position: relative;*/
        }

        p {
            background-color: white;
            text-align: center;
        }

        div#Printable {
            display: inline-block;
            text-align: center;
            padding-top: 0px;
            margin-top: 1px;
            margin-left: 380px;
        }

        div.example-dropzone {
            width: 100%;
        }

        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
            text-align: center;
        }

        td {
            background-color: #96D4D4;
            width: 30px;
            height: 5px;
            text-align: center;
            padding-bottom: 12px;
        }

        div#container {
            text-align: center;
            width: 160px;
            border: 1px solid black;
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
    </style>


    <script lang="javascript">
        /* draggable element */
        var arr = [];

        let sumstringcol = [];
        let sumstringtbl = [];
        let sumstringcells = [];
        function onDragStart(event) {
            event.dataTransfer.setData('text/plain', event.target.id);

            event.currentTarget.style;
        }
        function onDragOver(event) {
            event.preventDefault();

        }

        function onDrop(event) {

            const id = event.dataTransfer.getData('text');
            const draggableElement = document.getElementById(id);
            const dropzone = event.target;
            dropzone.appendChild(draggableElement);
            event.dataTransfer.clearData();


        }


        function gettableid() {


            let tables = document.getElementsByTagName('table');

            for (var tab = 0; tab < tables.length; tab++) {

                var table = tables[tab];


                sumstringtbl += tab + ",";


                var rows = table.getElementsByTagName('tr');
                for (var i = 0; i < rows.length; i++) {
                    var cells = rows[i].getElementsByTagName('td');
                    sumstringcells += cells + ",";
                    for (var j = 1; j < cells.length; j++) {
                        var cellData = cells[j].textContent;

                        sumstringcol += cellData + ",";

                        console.log('table' + tab + 'Cell ' + j + ': ' + cellData);

                    }

                }

            }


            if (cellData != "") {

                document.getElementById('hidetxttab').value = sumstringtbl;
                console.log("tables:" + sumstringtbl);
                document.getElementById('hidetxtcell').value = sumstringcells;
                console.log("cell no." + sumstringcells);
                document.getElementById('hidetxtcol').value = sumstringcol;
                console.log("text are" + sumstringcol);
                document.getElementById("<%=btnsave.ClientID %>").style.visibility = "visible";
                document.getElementById("btnconfirm").style.visibility = "Hidden";



            }
            else {
                alert("Table Can't be Blank ,kindly Drag Exam Name");
            }







        }




        function divcreate(sublen, exmid, subnm) {

            var container = document.getElementById('container');

            function block(Id, Txt) {

                return '<div id = "' + Id + '" class="exampledraggable" draggable = "true" ondragstart = "onDragStart(event) ">' + Txt + '</div >';

            }


            for (var i = 0; i < sublen; i++) {

                container.innerHTML += block(exmid, subnm);
            }
            document.getElementById("<%=btnsbmt.ClientID %>").style.visibility = "Hidden";
            document.getElementById("<%=btnsave.ClientID %>").style.visibility = "Hidden";
        }

        function hidebtn() {
            document.getElementById("<%=btnsave.ClientID %>").style.visibility = "Hidden";
            document.getElementById("btnconfirm").style.visibility = "Hidden";
        }

       function numeric(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57)) {
                return true;
            }
            else {
                alert("Can't be Character");
                return false;
            }
        }
        function lettersOnly(event) {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8)
                return true;
            else {
                alert("Can't be Number");
                return false;
            }

        }
        function checkblank() {
            var structuretxt = document.getElementById('<%= txtboxStructuresname.ClientID %>').value;
            var tableCount = document.getElementById('<%= txtbxtblcount.ClientID %>').value;

            if (structuretxt == "" && tableCount == "")
            {
                alert("Form Can't be Blank");
               
            }
            else {
                
            }
        }



    </script>

</head>
<body>


    <form id="form1" runat="server">
        <center>
             <asp:LinkButton ID="GotoMain" runat="server" OnClick="GotoMain_Click">GotoMain</asp:LinkButton>

            <h3>Enter Structure Name:</h3>
            <asp:TextBox CssClass="txtbox" ID="txtboxStructuresname" runat="server" onkeypress="return lettersOnly(event)" Placeholder="Enter MarkSheet Design Name"></asp:TextBox>
            <br />
            
            <br />
            <h3>How Many Tables  Do You Want ?</h3>
            <asp:TextBox CssClass="txtbox" ID="txtbxtblcount" runat="server" onkeypress="return numeric(event)" AutoPostBack="true" OnTextChanged="txtbxtblcount_TextChanged"></asp:TextBox>
            <br />
            <asp:Label ID="lblcols" runat="server" Visible="false"><h3>How Many Coloums Do You Want In Tables?</h3></asp:Label>
            
           
            <br />
            <asp:Panel ID="pantxtbox" runat="server"></asp:Panel>
            <br />
            <asp:Button ID="btnsbmt" CssClass="button1" runat="server" Text="Submit" OnClientClick="checkblank()" OnClick="btnsbmt_Click" />
            <br />
            <br />
            <asp:HiddenField ID="hidetxtcol" runat="server" />
            <asp:HiddenField ID="hidetxttab" runat="server" />
            <asp:HiddenField ID="hidetxtcell" runat="server" />

            <br />

        </center>


        <div class="maindiv">

            <div id="container" ondragover='onDragOver(event);' ondrop='onDrop(event);'>
            </div>


            <center>


                <div id="Printable">
                    <div>

                        <asp:Panel ID="panTest" runat="server">
                            <asp:Literal ID="ltrMyHtmlmarksTable" runat="server"></asp:Literal>

                        </asp:Panel>



                    </div>

                    <br />
                    <div>
                    </div>
                    <br />
                    <br />
                    <p id="Info"></p>
                    <br />

                </div>

            </center>
        </div>
        <center>
            <p>
                <input type="button" class="button1" id="btnconfirm" value="Confirm" onclick="gettableid()" />
            </p>
            <asp:Button ID="btnsave" CssClass="button1" runat="server" Text="Save" OnClick="btnsave_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
        </center>







    </form>

</body>
</html>
