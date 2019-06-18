using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MBO
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string param = Request.QueryString["logout"];
                if (!string.IsNullOrWhiteSpace(param))
                {
                    Session.Abandon();
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmID.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Emp ID missing.');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Password missing.');", true);
                return;
            }
            UserManager um = new UserManager();
            bool isLoggedIn = um.Login(txtEmID.Text.Trim(), txtPassword.Text.Trim());
            if (isLoggedIn)
                Response.Redirect("Default.aspx");
            ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('UserID, Password incorrect.');", true);
        }
    }
}