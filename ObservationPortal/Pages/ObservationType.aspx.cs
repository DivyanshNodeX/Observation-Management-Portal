using ObservationPortal.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObservationPortal.Pages
{
    public partial class ObservationType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadObservationType();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"INSERT INTO ObservationTypeMaster
                            (ObservationTypeName, IsActive)
                            VALUES
                            (@ObservationTypeName, @IsActive)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ObservationTypeName", txtObservationType.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", chkActive.Checked);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            txtObservationType.Text = "";
            chkActive.Checked = true;

            LoadObservationType();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('Observation Type Saved Successfully');",
                true);
        }

        

        private void LoadObservationType()
        {
            SqlConnection con = DBHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM ObservationTypeMaster ORDER BY ObservationTypeID DESC",
                con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvObservationType.DataSource = dt;
            gvObservationType.DataBind();
        }

        protected void gvObservationType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvObservationType.EditIndex = e.NewEditIndex;
            LoadObservationType();
        }

        protected void gvObservationType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvObservationType.EditIndex = -1;
            LoadObservationType();
        }

        protected void gvObservationType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            int ObservationTypeID = Convert.ToInt32(gvObservationType.DataKeys[e.RowIndex].Value);

            TextBox txtObservationType =
    (TextBox)gvObservationType.Rows[e.RowIndex]
    .FindControl("txtEditObservationType");

            CheckBox chkActive =
                (CheckBox)gvObservationType.Rows[e.RowIndex].Cells[2].Controls[0];

            SqlConnection con = DBHelper.GetConnection();

            string query = @"UPDATE ObservationTypeMaster
                             SET ObservationTypeName=@ObservationTypeName,
                                 IsActive=@IsActive
                             WHERE ObservationTypeID=@ObservationTypeID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ObservationTypeName", txtObservationType.Text);
            cmd.Parameters.AddWithValue("@IsActive", chkActive.Checked);
            cmd.Parameters.AddWithValue("@ObservationTypeID", ObservationTypeID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(
    this,
    GetType(),
    "msg",
    "alert('Observation Type Updated Successfully');",
    true);

            gvObservationType.EditIndex = -1;
            LoadObservationType();
        }

        protected void gvObservationType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ObservationTypeID = Convert.ToInt32(gvObservationType.DataKeys[e.RowIndex].Value);

            SqlConnection con = DBHelper.GetConnection();

            string query = "DELETE FROM ObservationTypeMaster WHERE ObservationTypeID=@ObservationTypeID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ObservationTypeID", ObservationTypeID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadObservationType();
        }
    }
}