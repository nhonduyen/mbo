using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MBO
{
    public partial class UploadImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMP_ID"] == null)
                    Response.Redirect("Login.aspx");
                var emp_id = Request.QueryString["emp_id"];
                if (emp_id != null)
                {
                    UserManager um = new UserManager();
                    Employee EMP = um.GetEmpInfo(emp_id.ToString());
                    lblId.Text = EMP.EMP_ID;
                    lblName.Text = EMP.NAME;
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["emp_id"] != null)
            {
                if ((UploadImg.PostedFile != null) && (UploadImg.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(UploadImg.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Images") + "\\" + fn;
                    try
                    {
                        UploadImg.PostedFile.SaveAs(SaveLocation);

                        UserManager um = new UserManager();
                        int result = um.UpdatePicture(lblId.Text, "Images\\" + fn);
                        if (result > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Upload success.');", true);
                            return;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Upload failed');", true);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Info", "Error " + ex.Message, true);
                        return;
                        //Note: Exception.Message returns a detailed message that describes the current exception. 
                        //For security reasons, we do not recommend that you return Exception.Message to end users in 
                        //production environments. It would be better to put a generic error message. 
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Choose file to upload.');", true);
                    return;
                }
            }
            else
            {
                if (UploadImg.HasFiles)
                {
                    int result = 0;
                    foreach (HttpPostedFile uploadedFile in UploadImg.PostedFiles)
                    {
                        string filename = System.IO.Path.GetFileName(uploadedFile.FileName);
                        string SaveLocation = Server.MapPath("Images") + "\\" + filename;
                        uploadedFile.SaveAs(SaveLocation);
                        string[] emp_id = filename.Split('-');
                        UserManager um = new UserManager();
                        result = um.UpdatePicture(emp_id[0], "Images\\" + filename);
                        
                    }
                    if (result > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Upload success.');", true);
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Info", "bootbox.alert('Upload failed.');", true);
                        return;
                    }
                }
            }
        }
    }
}