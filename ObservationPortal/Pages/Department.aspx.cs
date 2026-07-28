using ObservationPortal.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObservationPortal.Pages
{
    public partial class Department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartment();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"INSERT INTO DepartmentMaster
                             (DepartmentName, IsActive)
                             VALUES
                             (@DepartmentName, @IsActive)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DepartmentName", txtDepartment.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", chkActive.Checked);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            txtDepartment.Text = "";
            chkActive.Checked = true;

            LoadDepartment();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('Department Saved Successfully');",
                true);
        }

        private void LoadDepartment()
        {
            SqlConnection con = DBHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM DepartmentMaster ORDER BY DepartmentID DESC",
                con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvDepartment.DataSource = dt;
            gvDepartment.DataBind();
        }
        protected void gvDepartment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDepartment.EditIndex = e.NewEditIndex;
            LoadDepartment();
        }

        protected void gvDepartment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDepartment.EditIndex = -1;
            LoadDepartment();
        }

        protected void gvDepartment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int DepartmentID = Convert.ToInt32(gvDepartment.DataKeys[e.RowIndex].Value);

            TextBox txtDepartment =
                (TextBox)gvDepartment.Rows[e.RowIndex].Cells[1].Controls[0];

            CheckBox chkActive =
                (CheckBox)gvDepartment.Rows[e.RowIndex].Cells[2].Controls[0];

            SqlConnection con = DBHelper.GetConnection();

            string query = @"UPDATE DepartmentMaster
                     SET DepartmentName=@DepartmentName,
                         IsActive=@IsActive
                     WHERE DepartmentID=@DepartmentID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DepartmentName", txtDepartment.Text);
            cmd.Parameters.AddWithValue("@IsActive", chkActive.Checked);
            cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);

            con.Open();
            int rows = cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('Rows Updated: " + rows + "');",
                true);
            cmd.ExecuteNonQuery();
            con.Close();

            gvDepartment.EditIndex = -1;
            LoadDepartment();
        }

        protected void gvDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int DepartmentID = Convert.ToInt32(gvDepartment.DataKeys[e.RowIndex].Value);

            SqlConnection con = DBHelper.GetConnection();

            string query = "DELETE FROM DepartmentMaster WHERE DepartmentID=@DepartmentID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadDepartment();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('Department Deleted Successfully');",
                true);
        }
    }
}