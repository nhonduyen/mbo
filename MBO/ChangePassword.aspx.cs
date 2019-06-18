using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MBO
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Name"] == null)
                    Response.Redirect("Login.aspx");
                txtName.Text = Session["Name"].ToString();
                txtEmpNo.Text = Session["EMP_ID"].ToString();
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirm.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Please fill out all password field.');", true);
                return;
            }
            if (string.Compare(txtNewPassword.Text, txtConfirm.Text) != 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Password confirm does not match.');", true);
                return;
            }
            UserManager UM = new UserManager();
            int result = UM.ChangePassword(txtEmpNo.Text, txtPassword.Text.Trim(), txtNewPassword.Text.Trim());
            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Success.');", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Fail.');", true);
                return;
            }
        }
    }
}