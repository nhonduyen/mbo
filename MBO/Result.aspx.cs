using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MBO
{
    public partial class Result : System.Web.UI.Page
    {
        protected List<Period> Periods = new List<Period>();
        protected int STATUS;
        protected DataTable Result1;
        protected List<Eva_Group> Groups;
        protected DataTable SUM;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMP_ID"] == null)
                    Response.Redirect("Login.aspx");
                var period_id = Request.QueryString["eva"];
                var action = Request.QueryString["action"];
                var group = Request.QueryString["group"];
                var emp_id = Session["EMP_ID"].ToString();

                UserManager um = new UserManager();
                ResultMange rm = new ResultMange();
                Groups = um.GetEvagroup();
                Periods = um.GetPeriod();
                STATUS = Periods[0].STATUS;
                if (string.IsNullOrEmpty(action))
                {
                    if (string.IsNullOrEmpty(period_id))
                        Result1 = rm.GetSelfResult(Periods[0].EVA_TIME, emp_id);
                    else
                        Result1 = rm.GetSelfResult(period_id.ToString(), emp_id);
                    Result1.Columns.Add("ROLE", typeof(System.Int32));
                    foreach (DataRow r in Result1.Rows)
                    {
                        r["MBO_M1_RATE"] = 0;
                        r["MBO_M1_SCORE"] = 0;
                        r["M1_FINAL_SCORE"] = 0;
                        r["M1_GRADE"] = 0;
                        r["MBO_M2_RATE"] = 0;
                        r["MBO_M2_SCORE"] = 0;
                        r["MBO_FINAL_SCORE"] = 0;
                        r["CAP_M1"] = 0;
                        r["CAP_M2"] = 0;
                        r["CAP_FINAL_SCORE"] = 0;
                        r["TOTAL_SCORE"] = 0;
                        r["GRADE"] = string.Empty;
                        r["FINAL_GRADE"] = string.Empty;
                        r["ROLE"] = 0;
                    }
                }
                else
                {
                    Period p = um.GetPeriod(period_id);
                    STATUS = p.STATUS;
                    if (group == null)
                        Result1 = rm.GetResult(period_id.ToString(), emp_id);
                    else
                    {
                        Result1 = rm.GetResult(period_id.ToString(), emp_id, Convert.ToInt32(group.ToString()));
                    }
                    Result1.Columns.Add("ROLE", typeof(System.Int32));
                   
                  
                    for (int i=0 ; i< Result1.Rows.Count; i++)
                    {
                        Result1.Rows[i]["FINAL_GRADE"] = string.IsNullOrWhiteSpace(Result1.Rows[i]["FINAL_GRADE"].ToString()) ? Result1.Rows[i]["GRADE"].ToString() : Result1.Rows[i]["FINAL_GRADE"].ToString();
                        var e_id = Result1.Rows[i]["EMP_ID"].ToString();
                        int role = um.CheckRole(e_id, emp_id);
                        if (role == 1)
                        {
                            Result1.Rows[i]["MBO_M2_RATE"] = 0;
                            Result1.Rows[i]["MBO_M2_SCORE"] = 0;
                            Result1.Rows[i]["MBO_FINAL_SCORE"] = 0;
                            Result1.Rows[i]["CAP_M2"] = 0;
                            Result1.Rows[i]["CAP_FINAL_SCORE"] = 0;
                            Result1.Rows[i]["TOTAL_SCORE"] = 0;
                            Result1.Rows[i]["GRADE"] = "";
                            //Result1.Rows[i]["FINAL_GRADE"] = "";
                        }
                        Result1.Rows[i]["ROLE"] = role;
                       
                    }
                    Dictionary<string, object> sum = new Dictionary<string, object>();
                    sum.Add("@EVA_TIME", period_id);
                    sum.Add("@APPROVER", emp_id);
                    SUM = mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_EVA_SUMMARY, sum);
                   
                }
               
            }
        }   
    }
   
}