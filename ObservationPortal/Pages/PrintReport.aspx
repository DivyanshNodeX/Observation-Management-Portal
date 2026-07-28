<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="PrintReport.aspx.cs"
    Inherits="ObservationPortal.Pages.PrintReport" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>ABC Bank Report</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
          rel="stylesheet" />

    <style>

        body{
            padding:30px;
        }

        h2{
            text-align:center;
            margin-bottom:5px;
        }

        h5{
            text-align:center;
            margin-bottom:30px;
            color:gray;
        }

    </style>

</head>

<body>

<form runat="server">

<h2>ABC Bank</h2>

<h5>Observation Report</h5>

<asp:GridView
    ID="gvPrint"
    runat="server"
    CssClass="table table-bordered table-striped"
    AutoGenerateColumns="true">
</asp:GridView>

</form>

</body>
</html>