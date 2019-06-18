using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MBO
{
    public partial class Eva_Info : System.Web.UI.Page
    {
        protected List<Period> Periods = new List<Period>();
        protected string Duration;
        protected DataTable Result1;
        protected List<Eva_Group> Groups = new List<Eva_Group>();
        protected List<WorkGroup> WGroups = new List<WorkGroup>();
        protected ResultMange RM = new ResultMange();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMP_ID"] == null)
                    Response.Redirect("Login.aspx");
                var period_id = Request.QueryString["eva"];
                var group = Request.QueryString["group"];
                var wgroup = Request.QueryString["w"];
                var query = Request.QueryString["query"];
                var plan = Request.QueryString["plan"];

                UserManager um = new UserManager();
                ResultMange rm = new ResultMange();
                Groups = um.GetEvagroup();
                WGroups = um.GetWorkgroup();
                Periods = um.GetPeriod();
                Duration = Periods[0].EVA_START.ToShortDateString() + "~" + Periods[0].EVA_END.ToShortDateString();
                if (group == null)
                {
                    Result1 = rm.GetMBOResult(Periods[0].EVA_TIME, 1, "", "");
                }
                else
                {
                    if (query == null && wgroup == null)
                        Result1 = rm.GetMBOResult(period_id, Convert.ToInt32(group));
                    else
                        Result1 = rm.GetMBOResult(period_id, Convert.ToInt32(group), query, wgroup);
                }
            }
        }
    }
}