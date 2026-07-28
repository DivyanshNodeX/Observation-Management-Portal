using ObservationPortal.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObservationPortal.Pages
{
    public partial class Observation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartment();
                LoadObservationType();
                LoadFinancialYear();
                LoadObservation();
            }
        }

        private void LoadDepartment()
        {
            SqlConnection con = DBHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT DepartmentID, DepartmentName FROM DepartmentMaster WHERE IsActive=1",
                con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, "--Select Department--");
        }

        private void LoadObservationType()
        {
            SqlConnection con = DBHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT ObservationTypeID, ObservationTypeName FROM ObservationTypeMaster WHERE IsActive=1",
                con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlObservationType.DataSource = dt;
            ddlObservationType.DataTextField = "ObservationTypeName";
            ddlObservationType.DataValueField = "ObservationTypeID";
            ddlObservationType.DataBind();

            ddlObservationType.Items.Insert(0, "--Select Observation Type--");
        }

        private void LoadFinancialYear()
        {
            ddlFinancialYear.Items.Clear();

            ddlFinancialYear.Items.Add(new ListItem("--Select Financial Year--", ""));

            for (int year = 2024; year <= 2035; year++)
            {
                string fy = year + "-" + (year + 1).ToString().Substring(2);
                ddlFinancialYear.Items.Add(new ListItem(fy, fy));
            }

            ddlFinancialYear.SelectedValue = "2026-27";
        }

        private void LoadObservation()
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"
    SELECT
        O.ObservationID,
        O.SerialNo,
        D.DepartmentName,
        OT.ObservationTypeName,
        O.Priority,
        O.Status
    FROM ObservationMaster O
    INNER JOIN DepartmentMaster D
        ON O.DepartmentID = D.DepartmentID
    INNER JOIN ObservationTypeMaster OT
        ON O.ObservationTypeID = OT.ObservationTypeID
    ORDER BY O.ObservationID DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvObservation.DataSource = dt;
            gvObservation.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfObservationID.Value != "")
            {
                UpdateObservation();
                return;
            }

            SqlConnection con = DBHelper.GetConnection();

            string query = @"INSERT INTO ObservationMaster
(
    SerialNo,
    Observation,
    Remedy,
    Reference,
    Priority,
    Status,
    Remarks,
    DepartmentID,
    ObservationTypeID,
    FinancialYear,
    Quarter,
    ObservationDate
)
VALUES
(
    @SerialNo,
    @Observation,
    @Remedy,
    @Reference,
    @Priority,
    @Status,
    @Remarks,
    @DepartmentID,
    @ObservationTypeID,
    @FinancialYear,
    @Quarter,
    @ObservationDate
)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@SerialNo", txtSerialNo.Text.Trim());
            cmd.Parameters.AddWithValue("@Observation", txtObservation.Text.Trim());
            cmd.Parameters.AddWithValue("@Remedy", txtRemedy.Text.Trim());
            cmd.Parameters.AddWithValue("@Reference", txtReference.Text.Trim());
            cmd.Parameters.AddWithValue("@Priority", ddlPriority.SelectedValue);
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());
            cmd.Parameters.AddWithValue("@DepartmentID", ddlDepartment.SelectedValue);
            cmd.Parameters.AddWithValue("@ObservationTypeID", ddlObservationType.SelectedValue);
            cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@Quarter", ddlQuarter.SelectedValue);
            cmd.Parameters.AddWithValue("@ObservationDate", DateTime.Today);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DBHelper.LogAction(
            Session["UserName"].ToString(),
            "Observation", "Added");

            ClearForm();
            LoadObservation();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('Observation Saved Successfully');",
                true);
        }
        private void ClearForm()
        {
            txtSerialNo.Text = "";
            txtObservation.Text = "";
            txtRemedy.Text = "";
            txtReference.Text = "";
            txtRemarks.Text = "";
            ddlFinancialYear.SelectedValue = "";

            ddlDepartment.SelectedIndex = 0;
            ddlObservationType.SelectedIndex = 0;
            ddlPriority.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlQuarter.SelectedIndex = 0;
        }

        private void UpdateObservation()
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"
    UPDATE ObservationMaster
    SET
        SerialNo = @SerialNo,
        Observation = @Observation,
        Remedy = @Remedy,
        Reference = @Reference,
        Priority = @Priority,
        Status = @Status,
        Remarks = @Remarks,
        DepartmentID = @DepartmentID,
        ObservationTypeID = @ObservationTypeID,
        FinancialYear = @FinancialYear,
        Quarter = @Quarter
    WHERE ObservationID = @ObservationID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@SerialNo", txtSerialNo.Text.Trim());
            cmd.Parameters.AddWithValue("@Observation", txtObservation.Text.Trim());
            cmd.Parameters.AddWithValue("@Remedy", txtRemedy.Text.Trim());
            cmd.Parameters.AddWithValue("@Reference", txtReference.Text.Trim());
            cmd.Parameters.AddWithValue("@Priority", ddlPriority.SelectedValue);
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());
            cmd.Parameters.AddWithValue("@DepartmentID", ddlDepartment.SelectedValue);
            cmd.Parameters.AddWithValue("@ObservationTypeID", ddlObservationType.SelectedValue);
            cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@Quarter", ddlQuarter.SelectedValue);

            cmd.Parameters.AddWithValue("@ObservationID", hfObservationID.Value);
            cmd.Parameters.AddWithValue("@ObservationDate", DateTime.Today);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            DBHelper.LogAction(
            Session["UserName"].ToString(),
            "Observation", "Updated");

            hfObservationID.Value = "";

            btnSave.Text = "Save Observation";

            ClearForm();

            LoadObservation();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('Observation Updated Successfully');",
                true);
        }

        protected void gvObservation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName == "View")
                
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                int observationID = Convert.ToInt32(gvObservation.DataKeys[rowIndex].Value);

                SqlConnection con = DBHelper.GetConnection();

                string query = @"
        SELECT
            O.SerialNo,
            D.DepartmentName,
            OT.ObservationTypeName,
            O.Observation,
            O.Remedy,
            O.Reference,
            O.Priority,
            O.Status,
            O.Remarks
        FROM ObservationMaster O
        INNER JOIN DepartmentMaster D
            ON O.DepartmentID = D.DepartmentID
        INNER JOIN ObservationTypeMaster OT
            ON O.ObservationTypeID = OT.ObservationTypeID
        WHERE O.ObservationID=@ObservationID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ObservationID", observationID);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    lblSerialNo.Text = dr["SerialNo"].ToString();
                    lblDepartment.Text = dr["DepartmentName"].ToString();
                    lblObservationType.Text = dr["ObservationTypeName"].ToString();
                    lblObservation.Text = dr["Observation"].ToString();
                    lblRemedy.Text = dr["Remedy"].ToString();
                    lblReference.Text = dr["Reference"].ToString();
                    lblPriority.Text = dr["Priority"].ToString();
                    lblStatus.Text = dr["Status"].ToString();
                    lblRemarks.Text = dr["Remarks"].ToString();
                }

                dr.Close();
                con.Close();

                ScriptManager.RegisterStartupScript(
    this,
    this.GetType(),
    "ShowModal",
    @"
window.onload = function () {
    var modal = new bootstrap.Modal(document.getElementById('viewModal'));
    modal.show();
};
",
    true);
            }
            if (e.CommandName == "View")

            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                int observationID = Convert.ToInt32(gvObservation.DataKeys[rowIndex].Value);

                SqlConnection con = DBHelper.GetConnection();

                string query = @"
        SELECT
            O.SerialNo,
            D.DepartmentName,
            OT.ObservationTypeName,
            O.Observation,
            O.Remedy,
            O.Reference,
            O.Priority,
            O.Status,
            O.Remarks
        FROM ObservationMaster O
        INNER JOIN DepartmentMaster D
            ON O.DepartmentID = D.DepartmentID
        INNER JOIN ObservationTypeMaster OT
            ON O.ObservationTypeID = OT.ObservationTypeID
        WHERE O.ObservationID=@ObservationID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ObservationID", observationID);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    lblSerialNo.Text = dr["SerialNo"].ToString();
                    lblDepartment.Text = dr["DepartmentName"].ToString();
                    lblObservationType.Text = dr["ObservationTypeName"].ToString();
                    lblObservation.Text = dr["Observation"].ToString();
                    lblRemedy.Text = dr["Remedy"].ToString();
                    lblReference.Text = dr["Reference"].ToString();
                    lblPriority.Text = dr["Priority"].ToString();
                    lblStatus.Text = dr["Status"].ToString();
                    lblRemarks.Text = dr["Remarks"].ToString();
                }

                dr.Close();
                con.Close();

                ScriptManager.RegisterStartupScript(
    this,
    this.GetType(),
    "ShowModal",
    @"
window.onload = function () {
    var modal = new bootstrap.Modal(document.getElementById('viewModal'));
    modal.show();
};
",
    true);
            }
            if (e.CommandName == "LoadEdit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                int observationID = Convert.ToInt32(gvObservation.DataKeys[rowIndex].Value);

                LoadObservationForEdit(observationID);
            }
        }
        protected void gvObservation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvObservation.EditIndex = e.NewEditIndex;
            LoadObservation();
        }

        protected void gvObservation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvObservation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvObservation.EditIndex = -1;
            LoadObservation();
        }

        protected void gvObservation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int observationID = Convert.ToInt32(gvObservation.DataKeys[e.RowIndex].Value);

            SqlConnection con = DBHelper.GetConnection();

            string query = "DELETE FROM ObservationMaster WHERE ObservationID=@ObservationID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ObservationID", observationID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            DBHelper.LogAction(
            Session["UserName"].ToString(),
            "Observation",  "Deleted");

            LoadObservation();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('Observation Deleted Successfully');",
                true);
        }
        private void LoadObservationForEdit(int observationID)
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"SELECT * FROM ObservationMaster
                     WHERE ObservationID=@ObservationID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ObservationID", observationID);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                hfObservationID.Value = dr["ObservationID"].ToString();

                txtSerialNo.Text = dr["SerialNo"].ToString();
                txtObservation.Text = dr["Observation"].ToString();
                txtRemedy.Text = dr["Remedy"].ToString();
                txtReference.Text = dr["Reference"].ToString();
                txtRemarks.Text = dr["Remarks"].ToString();
                ddlFinancialYear.SelectedValue = dr["FinancialYear"].ToString();

                ddlDepartment.SelectedValue = dr["DepartmentID"].ToString();
                ddlObservationType.SelectedValue = dr["ObservationTypeID"].ToString();
                ddlPriority.SelectedValue = dr["Priority"].ToString();
                ddlStatus.SelectedValue = dr["Status"].ToString();
                ddlQuarter.SelectedValue = dr["Quarter"].ToString();

                btnSave.Text = "Update Observation";
            }

            dr.Close();
            con.Close();
        }
    }
}