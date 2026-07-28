<%@ Page Title="Observation Type"
Language="C#"
MasterPageFile="~/Admin.Master"
AutoEventWireup="true"
CodeBehind="ObservationType.aspx.cs"
Inherits="ObservationPortal.Pages.ObservationType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="card p-4">

    <h3>Observation Type Master</h3>
    <hr />

    <div class="row">

        <div class="col-md-5">

            <label>Observation Type</label>

            <asp:TextBox ID="txtObservationType"
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
        CssClass="btn btn-primary px-5" Width="180px"
        OnClick="btnSave_Click"/>
    

</div>

<br />

<asp:GridView ID="gvObservationType"
    runat="server"
    CssClass="table table-bordered table-hover"
    AutoGenerateColumns="False"
    DataKeyNames="ObservationTypeID"
    OnRowEditing="gvObservationType_RowEditing"
    OnRowCancelingEdit="gvObservationType_RowCancelingEdit"
    OnRowUpdating="gvObservationType_RowUpdating"
    OnRowDeleting="gvObservationType_RowDeleting">
   

    <Columns>

        <asp:BoundField
    DataField="ObservationTypeID"
    HeaderText="ID" />

       <asp:TemplateField HeaderText="Observation Type">

    <ItemTemplate>
        <%# Eval("ObservationTypeName") %>
    </ItemTemplate>

    <EditItemTemplate>

        <asp:TextBox
            ID="txtEditObservationType"
            runat="server"
            CssClass="form-control"
            Text='<%# Bind("ObservationTypeName") %>'>
        </asp:TextBox>

    </EditItemTemplate>

</asp:TemplateField>

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