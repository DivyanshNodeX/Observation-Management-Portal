using ObservationPortal.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObservationPortal.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadDepartmentFilter();
                LoadDashboard();
                LoadRecentObservations();
            }

            if (!IsPostBack)
            {
                SetGreeting();

                // Existing code...
            }
        }

        private void LoadDashboard()
        {
            SqlConnection con = DBHelper.GetConnection();

            con.Open();

            string where = " WHERE 1=1 ";

            if (ddlFilterDepartment.SelectedValue != "")
            {
                where += " AND DepartmentID=@DepartmentID";
            }

            SqlCommand cmdTotal = new SqlCommand(
     "SELECT COUNT(*) FROM ObservationMaster" + where, con);
           

            SqlCommand cmdOpen = new SqlCommand(
    "SELECT COUNT(*) FROM ObservationMaster" +
    where + " AND Status='Open'", con);
            

            SqlCommand cmdWIP = new SqlCommand(
    "SELECT COUNT(*) FROM ObservationMaster" +
    where + " AND Status='WIP'", con);
            

            SqlCommand cmdClosed = new SqlCommand(
    "SELECT COUNT(*) FROM ObservationMaster" +
    where + " AND Status='Closed'", con);

            

            if (ddlFilterDepartment.SelectedValue != "")
            {
                cmdTotal.Parameters.AddWithValue("@DepartmentID", ddlFilterDepartment.SelectedValue);
                cmdOpen.Parameters.AddWithValue("@DepartmentID", ddlFilterDepartment.SelectedValue);
                cmdWIP.Parameters.AddWithValue("@DepartmentID", ddlFilterDepartment.SelectedValue);
                cmdClosed.Parameters.AddWithValue("@DepartmentID", ddlFilterDepartment.SelectedValue);
            }

            lblTotal.Text = cmdTotal.ExecuteScalar().ToString();
            lblOpen.Text = cmdOpen.ExecuteScalar().ToString();
            lblWIP.Text = cmdWIP.ExecuteScalar().ToString();
            lblClosed.Text = cmdClosed.ExecuteScalar().ToString();

            SqlCommand cmdDept = new SqlCommand(
                "SELECT COUNT(*) FROM DepartmentMaster", con);
            lblDepartmentCount.Text = cmdDept.ExecuteScalar().ToString();

            SqlCommand cmdObsType = new SqlCommand(
                "SELECT COUNT(*) FROM ObservationTypeMaster", con);
            lblObservationTypeCount.Text = cmdObsType.ExecuteScalar().ToString();

            SqlCommand cmdHigh = new SqlCommand(
                "SELECT COUNT(*) FROM ObservationMaster WHERE Priority='High'", con);
            lblHighPriority.Text = cmdHigh.ExecuteScalar().ToString();

            lblOpenChart.Text = lblOpen.Text;
            lblWIPChart.Text = lblWIP.Text;
            lblClosedChart.Text = lblClosed.Text;

            SqlCommand cmdBar = new SqlCommand(@"
SELECT
    D.DepartmentName,
    COUNT(O.ObservationID) AS Total
FROM DepartmentMaster D
LEFT JOIN ObservationMaster O
ON D.DepartmentID = O.DepartmentID
GROUP BY D.DepartmentName", con);

            SqlDataReader dr = cmdBar.ExecuteReader();

            string labels = "";
            string values = "";

            while (dr.Read())
            {
                labels += "'" + dr["DepartmentName"].ToString() + "',";
                values += dr["Total"].ToString() + ",";
            }

            dr.Close();

            hfDepartmentLabels.Value = labels.TrimEnd(',');
            hfDepartmentCounts.Value = values.TrimEnd(',');

            

            con.Close();
        }

        private void LoadDepartmentFilter()
        {
            SqlConnection con = DBHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT DepartmentID, DepartmentName FROM DepartmentMaster ORDER BY DepartmentName",
                con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlFilterDepartment.DataSource = dt;
            ddlFilterDepartment.DataTextField = "DepartmentName";
            ddlFilterDepartment.DataValueField = "DepartmentID";
            ddlFilterDepartment.DataBind();

            ddlFilterDepartment.Items.Insert(0, new ListItem("All", ""));
        }

        private void LoadRecentObservations()
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"
SELECT TOP 5
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
WHERE 1=1 ";

            if (ddlFilterDepartment.SelectedValue != "")
                query += " AND O.DepartmentID=@DepartmentID";

            if (ddlFilterStatus.SelectedValue != "")
                //query += " AND O.Status=@Status";

            if (ddlFilterPriority.SelectedValue != "")
                query += " AND O.Priority=@Priority";

            query += " ORDER BY O.ObservationID DESC";

            SqlCommand cmd = new SqlCommand(query, con);

            if (ddlFilterDepartment.SelectedValue != "")
                cmd.Parameters.AddWithValue("@DepartmentID", ddlFilterDepartment.SelectedValue);

            if (ddlFilterStatus.SelectedValue != "")
                cmd.Parameters.AddWithValue("@Status", ddlFilterStatus.SelectedValue);

            if (ddlFilterPriority.SelectedValue != "")
                cmd.Parameters.AddWithValue("@Priority", ddlFilterPriority.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvRecentObservations.DataSource = dt;
            gvRecentObservations.DataBind();
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            LoadDashboard();
            LoadRecentObservations();
        }

        private void SetGreeting()
        {
            string greeting;

            int hour = DateTime.Now.Hour;

            if (hour < 12)
                greeting = "Good Morning";
            else if (hour < 17)
                greeting = "Good Afternoon";
            else
                greeting = "Good Evening";

            string username = Session["UserName"] != null
                ? Session["UserName"].ToString()
                : "Admin";

            username = char.ToUpper(username[0]) + username.Substring(1);

            lblGreeting.Text = greeting + ", " + username;

            lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }
    }
}