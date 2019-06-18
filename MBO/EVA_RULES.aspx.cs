using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MBO
{
    public partial class EVA_RULES : System.Web.UI.Page
    {
        protected List<EVA_RULE> RULES = new List<EVA_RULE>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMP_ID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                RULES = DBManager<EVA_RULE>.ExecuteReader(StoredProcedure.GET_EVA_RULE);
            }
        }
    }
}