<%@ Page Title="Observation"
Language="C#"
MasterPageFile="~/Admin.Master"
AutoEventWireup="true"
CodeBehind="Observation.aspx.cs"
Inherits="ObservationPortal.Pages.Observation" %>

<asp:Content ID="Content1"
ContentPlaceHolderID="MainContent"
runat="server">

    <div class="card p-4">

<h3>Observation Entry</h3>
<hr />

<div class="row">

<div class="col-md-3">
<label>Serial No</label>
<asp:TextBox ID="txtSerialNo" runat="server"
CssClass="form-control"></asp:TextBox>
</div>

<div class="col-md-3">
<label>Department</label>
<asp:DropDownList ID="ddlDepartment"
runat="server"
CssClass="form-control">
</asp:DropDownList>
</div>

<div class="col-md-3">
<label>Observation Type</label>
<asp:DropDownList ID="ddlObservationType"
runat="server"
CssClass="form-control">
</asp:DropDownList>
</div>

<div class="col-md-3">
<label>Priority</label>
<asp:DropDownList ID="ddlPriority"
runat="server"
CssClass="form-control">

<asp:ListItem>High</asp:ListItem>
<asp:ListItem>Medium</asp:ListItem>
<asp:ListItem>Low</asp:ListItem>

</asp:DropDownList>
</div>

</div>

<br />

<div class="row">

<div class="col-md-6">

<label>Observation</label>

<asp:TextBox
ID="txtObservation"
runat="server"
TextMode="MultiLine"
Rows="4"
CssClass="form-control">
</asp:TextBox>

</div>

<div class="col-md-6">

<label>Remedy</label>

<asp:TextBox
ID="txtRemedy"
runat="server"
TextMode="MultiLine"
Rows="4"
CssClass="form-control">
</asp:TextBox>

</div>

</div>

<br />

<div class="row">

<div class="col-md-3">

<label>Reference</label>

<asp:TextBox
ID="txtReference"
runat="server"
CssClass="form-control">
</asp:TextBox>

</div>

<div class="col-md-3">

<label>Status</label>

<asp:DropDownList
ID="ddlStatus"
runat="server"
CssClass="form-control">

<asp:ListItem>Open</asp:ListItem>
<asp:ListItem>WIP</asp:ListItem>
<asp:ListItem>Closed</asp:ListItem>

</asp:DropDownList>

</div>

<div class="col-md-3">

<label>Financial Year</label>

<asp:DropDownList
    ID="ddlFinancialYear"
    runat="server"
    CssClass="form-select">
</asp:DropDownList>

</div>

<div class="col-md-3">

<label>Quarter</label>

<asp:DropDownList
ID="ddlQuarter"
runat="server"
CssClass="form-control">

<asp:ListItem>Q1</asp:ListItem>
<asp:ListItem>Q2</asp:ListItem>
<asp:ListItem>Q3</asp:ListItem>
<asp:ListItem>Q4</asp:ListItem>

</asp:DropDownList>

</div>

</div>

<br />

<div class="row">

<div class="col-md-12">

<label>Remarks</label>

<asp:TextBox
ID="txtRemarks"
runat="server"
TextMode="MultiLine"
Rows="3"
CssClass="form-control">
</asp:TextBox>

</div>

</div>

<br />

<asp:HiddenField
    ID="hfObservationID"
    runat="server" />

<asp:Button
ID="btnSave"
runat="server"
Text="Save Observation"
CssClass="btn btn-primary"
Width="220px"
OnClick="btnSave_Click"/>
        <br />
<br />

<div class="card p-3">

<h4>Observation List</h4>

<asp:GridView
ID="gvObservation"
runat="server"
CssClass="table table-bordered table-hover"
AutoGenerateColumns="False"
DataKeyNames="ObservationID"
OnRowCommand="gvObservation_RowCommand"
OnRowEditing="gvObservation_RowEditing"
OnRowUpdating="gvObservation_RowUpdating"
OnRowCancelingEdit="gvObservation_RowCancelingEdit"
OnRowDeleting="gvObservation_RowDeleting">

<Columns>

<asp:BoundField
DataField="ObservationID"
HeaderText="ID" />

<asp:BoundField
DataField="SerialNo"
HeaderText="Serial No" />

<asp:BoundField
DataField="DepartmentName"
HeaderText="Department" />

<asp:BoundField
DataField="ObservationTypeName"
HeaderText="Observation Type" />

<asp:BoundField
DataField="Priority"
HeaderText="Priority" />

<asp:BoundField
DataField="Status"
HeaderText="Status" />

<asp:TemplateField HeaderText="Actions">

    <ItemTemplate>

 <asp:LinkButton
    ID="lnkView"
    runat="server"
    Text="View"
    CommandName="View"
    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
    CssClass="btn btn-info btn-sm" />

&nbsp;
    <asp:LinkButton
    ID="lnkEdit"
    runat="server"
    Text="Edit"
    CommandName="LoadEdit"
    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
    CssClass="btn btn-primary btn-sm" />

    &nbsp;

    <asp:LinkButton
        ID="lnkDelete"
        runat="server"
        Text="Delete"
        CommandName="Delete"
        CssClass="btn btn-danger btn-sm"
        OnClientClick="return confirm('Are you sure you want to delete this observation?');" />

</ItemTemplate>

    <EditItemTemplate>

        <asp:LinkButton
            ID="lnkUpdate"
            runat="server"
            Text="Update"
            CommandName="Update"
            CssClass="btn btn-success btn-sm" />

        &nbsp;

        <asp:LinkButton
            ID="lnkCancel"
            runat="server"
            Text="Cancel"
            CommandName="Cancel"
            CssClass="btn btn-secondary btn-sm" />

    </EditItemTemplate>

</asp:TemplateField>
</Columns>

</asp:GridView>

</div>

</div>

<br />
       <!-- View Modal -->

<div class="modal fade"
     id="viewModal"
     tabindex="-1"
     aria-hidden="true">

    <div class="modal-dialog modal-lg">

        <div class="modal-content">

            <div class="modal-header bg-primary text-white">

                <h5 class="modal-title">
                    Observation Details
                </h5>

                <button type="button"
                        class="btn-close"
                        data-bs-dismiss="modal">
                </button>

            </div>

            <div class="modal-body">

                <table class="table table-bordered">

                    <tr>
                        <th>Serial No</th>
                        <td>
                            <asp:Label ID="lblSerialNo"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Department</th>
                        <td>
                            <asp:Label ID="lblDepartment"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Observation Type</th>
                        <td>
                            <asp:Label ID="lblObservationType"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Observation</th>
                        <td>
                            <asp:Label ID="lblObservation"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Remedy</th>
                        <td>
                            <asp:Label ID="lblRemedy"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Reference</th>
                        <td>
                            <asp:Label ID="lblReference"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Priority</th>
                        <td>
                            <asp:Label ID="lblPriority"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Status</th>
                        <td>
                            <asp:Label ID="lblStatus"
                                runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <th>Remarks</th>
                        <td>
                            <asp:Label ID="lblRemarks"
                                runat="server" />
                        </td>
                    </tr>

                </table>

            </div>

        </div>

    </div>

</div>

    </asp:Content>