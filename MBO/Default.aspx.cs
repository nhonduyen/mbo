using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MBO
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<Period> Periods = new List<Period>();
     
        protected DataTable Result;
        protected Employee EMP;
        protected int STATUS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMP_ID"] == null)
                    Response.Redirect("Login.aspx");
                string eva = Request.QueryString["eva"];
                string action = Request.QueryString["action"];
                UserManager um = new UserManager();
                PlanManger pm = new PlanManger();
                Periods = um.GetPeriod();
  
                STATUS = Periods[0].STATUS;
                if (string.IsNullOrEmpty(action))
                {
                    if (!string.IsNullOrEmpty(eva))
                    {
                        Result = pm.GetPlan(Session["EMP_ID"].ToString(), eva);
                    }
                    else
                    {
                        Result = pm.GetPlan(Session["EMP_ID"].ToString(), Periods[0].EVA_TIME);
                       
                    }
                    if (Result.Rows.Count == 0)
                    {
                        EMP = um.GetEmpInfo(Session["EMP_ID"].ToString());
                    }
                    
                }
                else
                {
                    var t = Session["EMP_ID"].ToString();
                    
                    Result = pm.GetPlanByRole(Session["EMP_ID"].ToString(), eva);
                    Period p = um.GetPeriod(eva);
                    STATUS = p.STATUS;
                 
                }
               
            }
        }
    }
}