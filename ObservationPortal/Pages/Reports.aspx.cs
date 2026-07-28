using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObservationPortal.DAL;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ObservationPortal.Pages
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartments();
                if (Request.QueryString["status"] != null)
                {
                    ddlStatus.SelectedValue = Request.QueryString["status"];
                }

                if (Request.QueryString["priority"] != null)
                {
                    ddlPriority.SelectedValue = Request.QueryString["priority"];
                }
                LoadReport();
            }
        }

        private void LoadDepartments()
        {
            SqlConnection con = DBHelper.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT DepartmentID, DepartmentName FROM DepartmentMaster ORDER BY DepartmentName",
                con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", ""));
        }

        private void LoadReport()
        {
            SqlConnection con = DBHelper.GetConnection();

            string query = @"
SELECT
    O.SerialNo,
    D.DepartmentName,
    OT.ObservationTypeName,
    O.Observation,
    O.Priority,
    O.Status
FROM ObservationMaster O
INNER JOIN DepartmentMaster D
    ON O.DepartmentID = D.DepartmentID
INNER JOIN ObservationTypeMaster OT
    ON O.ObservationTypeID = OT.ObservationTypeID
WHERE 1=1";

            if (ddlDepartment.SelectedValue != "")
                query += " AND O.DepartmentID=@DepartmentID";

            if (ddlStatus.SelectedValue != "")
                query += " AND O.Status=@Status";

            if (ddlPriority.SelectedValue != "")
                query += " AND O.Priority=@Priority";

            if (txtFromDate.Text != "")
                query += " AND O.ObservationDate >= @FromDate";

            if (txtToDate.Text != "")
                query += " AND O.ObservationDate <= @ToDate";

            query += " ORDER BY O.ObservationID DESC";



            SqlCommand cmd = new SqlCommand(query, con);

            if (ddlDepartment.SelectedValue != "")
                cmd.Parameters.AddWithValue("@DepartmentID", ddlDepartment.SelectedValue);

            if (ddlStatus.SelectedValue != "")
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

            if (ddlPriority.SelectedValue != "")
                cmd.Parameters.AddWithValue("@Priority", ddlPriority.SelectedValue);

            if (txtFromDate.Text != "")
                cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);

            if (txtToDate.Text != "")
                cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);

            

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvReports.DataSource = dt;
            gvReports.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlDepartment.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlPriority.SelectedIndex = 0;

            gvReports.DataSource = null;
            gvReports.DataBind();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Pehle filtered data reload karo
            LoadReport();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ObservationReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvReports.AllowPaging = false;
            gvReports.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for Export to Excel
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            // Reload filtered data
            LoadReport();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ObservationReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvReports.AllowPaging = false;
            gvReports.DataBind();
            gvReports.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();

            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser =
                new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);

            htmlparser.Parse(sr);

            pdfDoc.Close();

            Response.Write(pdfDoc);
            Response.End();
        }
    }
}