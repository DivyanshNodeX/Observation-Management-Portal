using ObservationPortal.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObservationPortal.Pages
{
    public partial class PrintReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "Print",
                    "window.print();",
                    true);
            }
        }

        private void LoadReport()
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"
SELECT
    O.SerialNo,
    D.DepartmentName,
    OT.ObservationTypeName,
    O.Priority,
    O.Status,
    O.ObservationDate
FROM ObservationMaster O
INNER JOIN DepartmentMaster D
ON O.DepartmentID = D.DepartmentID
INNER JOIN ObservationTypeMaster OT
ON O.ObservationTypeID = OT.ObservationTypeID
ORDER BY O.ObservationID DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvPrint.DataSource = dt;
            gvPrint.DataBind();
        }
    }
}