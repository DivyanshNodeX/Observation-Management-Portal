<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs"
    Inherits="ObservationPortal.Pages.Reports"
    MasterPageFile="~/Admin.Master" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2 class="mb-4">
        Reports
    </h2>

    <div class="card shadow-sm p-4 mb-4">

    <div class="row">

        <div class="col-md-3">
            <label><b>Department</b></label>
            <asp:DropDownList ID="ddlDepartment"
                runat="server"
                CssClass="form-select">
            </asp:DropDownList>
        </div>

        <div class="col-md-3">
            <label><b>Status</b></label>
            <asp:DropDownList ID="ddlStatus"
                runat="server"
                CssClass="form-select">

                <asp:ListItem Text="All" Value=""></asp:ListItem>
                <asp:ListItem>Open</asp:ListItem>
                <asp:ListItem>WIP</asp:ListItem>
                <asp:ListItem>Closed</asp:ListItem>

            </asp:DropDownList>
        </div>

        <div class="col-md-3">
            <label><b>Priority</b></label>
            <asp:DropDownList ID="ddlPriority"
                runat="server"
                CssClass="form-select">

                <asp:ListItem Text="All" Value=""></asp:ListItem>
                <asp:ListItem>High</asp:ListItem>
                <asp:ListItem>Medium</asp:ListItem>
                <asp:ListItem>Low</asp:ListItem>

            </asp:DropDownList>
        </div>

        <div class="col-md-2">
    <label><b>From Date</b></label>

    <asp:TextBox ID="txtFromDate"
        runat="server"
        TextMode="Date"
        CssClass="form-control">
    </asp:TextBox>

</div>

<div class="col-md-2">
    <label><b>To Date</b></label>

    <asp:TextBox ID="txtToDate"
        runat="server"
        TextMode="Date"
        CssClass="form-control">
    </asp:TextBox>

</div>

       <div class="col-md-2">

    <label><b>&nbsp;</b></label>

    <div class="d-flex gap-2">

        <asp:Button ID="btnSearch"
            runat="server"
            Text="Search"
            CssClass="btn btn-primary flex-fill"
            OnClick="btnSearch_Click" />

        <asp:Button ID="btnReset"
            runat="server"
            Text="Reset"
            CssClass="btn btn-secondary"
            OnClick="btnReset_Click" />

    </div>

</div>

    </div>

</div>

    <div class="mb-3">

    <asp:Button ID="btnExportExcel"
        runat="server"
        Text="Export to Excel"
        CssClass="btn btn-success"
        OnClick="btnExportExcel_Click" />

    <asp:Button ID="btnExportPDF"
    runat="server"
    Text="Export PDF"
    CssClass="btn btn-danger"
    OnClick="btnExportPDF_Click" />

    <asp:Button ID="btnPrint"
    runat="server"
    Text="Print"
    CssClass="btn btn-dark"
    PostBackUrl="~/Pages/PrintReport.aspx" />

</div>

<asp:GridView ID="gvReports"
    runat="server"
    CssClass="table table-bordered table-hover"
    AutoGenerateColumns="true">
</asp:GridView>



</asp:Content>