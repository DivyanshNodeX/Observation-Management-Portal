using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using ObservationPortal.DAL;
using System.Web.UI.WebControls;

namespace ObservationPortal.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUserName.Focus();
            }

            if (!IsPostBack)
            {
                GenerateCaptcha();
            }

        }

        private void GenerateCaptcha()
        {
            Random rnd = new Random();

            int num1 = rnd.Next(2, 10);
            int num2 = rnd.Next(1, num1);   

            if (rnd.Next(2) == 0)
            {
                lblCaptcha.Text = num1 + " + " + num2 + " =";
                Session["CaptchaAnswer"] = (num1 + num2).ToString();
            }
            else
            {
                lblCaptcha.Text = num1 + " - " + num2 + " =";
                Session["CaptchaAnswer"] = (num1 - num2).ToString();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtCaptcha.Text.Trim() != Session["CaptchaAnswer"].ToString())
            {
                lblMessage.Text = "Incorrect verification answer.";

                GenerateCaptcha();
                txtCaptcha.Text = "";

                return;
            }
            
            SqlConnection con = DBHelper.GetConnection();

            string query = "SELECT COUNT(*) FROM LoginMaster WHERE UserName=@UserName AND Password=@Password AND IsActive=1";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();

            if (count > 0)
            {
                Session["UserName"] = txtUserName.Text;
                GenerateCaptcha();
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid Username or Password.";

                GenerateCaptcha();

                txtCaptcha.Text = "";
            }
        }
    }
}