<%@ Page Title="Department"
    Language="C#"
    MasterPageFile="~/Admin.Master"
    AutoEventWireup="true"
    CodeBehind="Department.aspx.cs"
    Inherits="ObservationPortal.Pages.Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="card p-4">

    <h3>Department Master</h3>
    <hr />

    <div class="row">

        <div class="col-md-5">

            <label>Department Name</label>

            <asp:TextBox ID="txtDepartment"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>

        </div>

        <div class="col-md-3">

            <label>Active</label>

            <br />

            <asp:CheckBox ID="chkActive"
                runat="server"
                Checked="true" />

        </div>

    </div>

    <br />

    <asp:Button ID="btnSave"
        runat="server"
        Text="Save"
        CssClass="btn btn-primary px-5"
Width="180px"
        OnClick="btnSave_Click"/>
    

</div>

<br />

<asp:GridView ID="gvDepartment"
    runat="server"
    CssClass="table table-bordered table-hover"
    AutoGenerateColumns="False"
    DataKeyNames="DepartmentID"
    OnRowEditing="gvDepartment_RowEditing"
    OnRowCancelingEdit="gvDepartment_RowCancelingEdit"
    OnRowUpdating="gvDepartment_RowUpdating"
    OnRowDeleting="gvDepartment_RowDeleting">
   

    <Columns>

        <asp:BoundField
            DataField="DepartmentID"
            HeaderText="ID" />

        <asp:BoundField
            DataField="DepartmentName"
            HeaderText="Department" />

        <asp:CheckBoxField
            DataField="IsActive"
            HeaderText="Active" />

        <asp:CommandField
    ShowEditButton="True"
    ShowDeleteButton="True"
    EditText="Edit"
    UpdateText="Update"
    CancelText="Cancel"
    DeleteText="Delete"
    ControlStyle-CssClass="btn btn-sm btn-link" />

    </Columns>

</asp:GridView>

</asp:Content>