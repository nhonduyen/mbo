using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;


namespace MBO
{
    public class PlanManger
    {
        public DataTable GetPlan(string EMP_ID, string EVA_TIME)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@EMP_ID", EMP_ID);
            param.Add("@PERIOD_ID", EVA_TIME);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_PLAN_BY_EMP_PERIOD, param);
        }

        public DataTable GetPlanByRole(string EMP_ID, string EVA_TIME)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@EMP_ID", EMP_ID);
            param.Add("@PERIOD_ID", EVA_TIME);

            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_PLAN_BY_ROLE, param);
        }

        public List<MBO_Plan> GetPlanDetails(int RESULT_ID)
        {
            return DBManager<MBO_Plan>.ExecuteReader(StoredProcedure.GET_PLAN_DETAILS, new { RESULT_ID = RESULT_ID });
        }
        public List<MBO_Plan> GetActionPlan(int ID)
        {
            return DBManager<MBO_Plan>.ExecuteReader(StoredProcedure.GET_ACTION_PLAN_BY_ID, new { ID = ID });
        }
        public int SavePlan(string EVA_TIME, string EMP_ID, int RESULT_ID, List<MBO_Plan> PLANS, string DEL, List<MBO_Plan> EDIT_PLANS)
        {
            foreach (var p in PLANS)
            {
                if (p.RESULT != null)
                    p.RESULT = p.RESULT.Replace("\r\n", "<br/>");
                p.ACTION_PLAN = p.ACTION_PLAN.Replace("\r\n", "<br/>");
                p.CONT = p.CONT.Replace("\r\n", "<br/>");
                p.TARGET = p.TARGET.Replace("\r\n", "<br/>");
            }
            foreach (var p in EDIT_PLANS)
            {
                if (p.RESULT != null)
                    p.RESULT = p.RESULT.Replace("\r\n", "<br/>");
                p.ACTION_PLAN = p.ACTION_PLAN.Replace("\r\n", "<br/>");
                p.CONT = p.CONT.Replace("\r\n", "<br/>");
                p.TARGET = p.TARGET.Replace("\r\n", "<br/>");
            }
            int result = 0;
           
            MBO_Result ExistId = DBManager<MBO_Result>.ExecuteReader(StoredProcedure.GET_MBO_RESULTID, new { EMP_ID = EMP_ID, PERIOD_ID = EVA_TIME }).FirstOrDefault();
            if (ExistId == null)
            {
                int t = DBManager<MBO_Result>.Execute(StoredProcedure.INSERT_MBO_RESULT, new { EMP_ID = EMP_ID, PERIOD_ID = EVA_TIME });
                if (t > 0)
                {
                    MBO_Result NewId = DBManager<MBO_Result>.ExecuteReader(StoredProcedure.GET_MBO_RESULTID, new { EMP_ID = EMP_ID, PERIOD_ID = EVA_TIME }).FirstOrDefault();
                    foreach (var plan in PLANS)
                    {
                        if (NewId != null)
                        {
                            result = DBManager<MBO_Plan>.Execute(StoredProcedure.INSERT_MBO_PLAN, new
                            {
                                RESULT_ID = NewId.ID,
                                CONT = plan.CONT,
                                ACTION_PLAN = plan.ACTION_PLAN,
                                TARGET = plan.TARGET,
                                WEIGHT = plan.WEIGHT,
                                LVL = plan.LVL
                            });
                        }
                    }
                }
            }
            else
            {
                foreach (var plan in PLANS)
                {
                    result = DBManager<MBO_Plan>.Execute(StoredProcedure.INSERT_MBO_PLAN, new
                    {
                        RESULT_ID = ExistId.ID,
                        CONT = plan.CONT,
                        ACTION_PLAN = plan.ACTION_PLAN,
                        TARGET = plan.TARGET,
                        WEIGHT = plan.WEIGHT,
                        LVL = plan.LVL
                    });
                }
                foreach (var plan in EDIT_PLANS)
                {
                    result = DBManager<MBO_Plan>.Execute(StoredProcedure.UPDATE_PLAN_REGISTER, new
                    {
                        ID = plan.ID,
                        CONT = plan.CONT,
                        ACTION_PLAN = plan.ACTION_PLAN,
                        TARGET = plan.TARGET,
                        WEIGHT = plan.WEIGHT,
                        LVL = plan.LVL
                    });
                }
                if (!string.IsNullOrWhiteSpace(DEL))
                {
                    string[] ids = DEL.Split(',');
                    foreach (var id in ids)
                    {
                        int planId = Convert.ToInt32(id);
                        result = DBManager<MBO_Plan>.Execute(StoredProcedure.DELETE_MBO_PLAN, new { ID = planId });
                    }
                }
            }
            return result;
        }

        public int Confirm(int ID, int STATUS)
        {
            return DBManager<MBO_Plan>.Execute(StoredProcedure.CONFIRM_MBO_RESULT, new { ID = ID, STATUS = STATUS });
        }

        public int ConfirmPlan(int ID, int STATUS)
        {
            return DBManager<MBO_Plan>.Execute(StoredProcedure.CONFIRM_MBO_PLAN, new { ID = ID, PLAN_STATUS = STATUS });
        }

        public List<Employee_Approver> GetRoles(string EMP_ID)
        {
            return DBManager<Employee_Approver>.ExecuteReader(StoredProcedure.GET_ROLE, new { EMP_ID = EMP_ID });
        }
        public List<Employee_Approver> GetAllRoles(string EMP_ID, string APPROVER)
        {
            return DBManager<Employee_Approver>.ExecuteReader(StoredProcedure.CHECK_ROLE, new { EMP_ID = EMP_ID, APPROVER=APPROVER });
        }
        public int GetRoles(string EMP_ID, string APPROVER)
        {
            Dictionary<string, object> PARAM = new Dictionary<string, object>();
            PARAM.Add("@EMP_ID", EMP_ID);
            PARAM.Add("@APPROVER", APPROVER);
            DataTable DTB = mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_ROLE_MAX, PARAM);
            if (DTB.Rows.Count > 0)
            {
                var maxRole = DTB.Rows[0]["r"].ToString();
                if (maxRole.Length > 0)
                return Convert.ToInt32(maxRole);
            }
            return 0;
        }

        public int UpdatePlan(MBO_Plan plan, int role)
        {
            if (plan.ID.ToString() == "")
                return 0;
            if (role == 2)
            {
                return DBManager<MBO_Plan>.Execute(StoredProcedure.UPDATE_PLAN, new
                {
                    ID = plan.ID,
                    Action = plan.ACTION,
                    RESULT = plan.RESULT,
                    MBO_SELF_RATE = plan.MBO_SELF_RATE,
                    MBO_M1_RATE = plan.MBO_M1_RATE,
                    MBO_M2_RATE = plan.MBO_M2_RATE
                });
            }
            if (role == 1)
            {
                return DBManager<MBO_Plan>.Execute(StoredProcedure.UPDATE_PLAN_M1, new
                {
                    ID = plan.ID,
                    MBO_M1_RATE = plan.MBO_M1_RATE
                });
            }
            return DBManager<MBO_Plan>.Execute(StoredProcedure.UPDATE_PLAN_SELF, new
            {
                ID = plan.ID,
                Action = plan.ACTION,
                RESULT = plan.RESULT,
                MBO_SELF_RATE = plan.MBO_SELF_RATE
            });

        }

        public void UpdateListPlan(List<MBO_Plan> plans, string APPROVER)
        {
            ResultMange rm = new ResultMange();
            UserManager um = new UserManager();

            foreach (var plan in plans)
            {
                string emp_id = rm.GetEmpIdByResult(plan.RESULT_ID);
                int role = um.CheckRole(emp_id, APPROVER);
                int t = UpdatePlan(plan, role);
            }
        }
        public DataTable GetPlans(string PERIOD_ID, int group=0, string query = "")
        {
            Dictionary<string,object> param=new Dictionary<string,object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@GROUP",group);
            param.Add("@QUERY",query);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_PLAN_BY_PERIOD, param);
        }

        public DataTable GetMboPlans(string PERIOD_ID, int group = 0, string query = "", int page=1)
        {
            var start = (page - 1) * 20 + 1;
            var end = start + 20 - 1;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@GROUP", group);
            param.Add("@QUERY", query);
            param.Add("@start", start);
            param.Add("@end", end);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_PLAN_PAGING, param);
        }
        public DataTable GetPlanExport(string PERIOD_ID, int group = 0, string query = "")
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@GROUP", group);
            param.Add("@QUERY", query);

            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_PLAN_EXPORT, param);
        }
        public int CountPlans(string PERIOD_ID, int group = 0, string query = "")
        {
           
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@GROUP", group);
            param.Add("@QUERY", query);

            return (int)mgrDataSQL.ExecuteStoreScalar(StoredProcedure.COUNT_PLAN_PAGING, param);
        }
    }
}