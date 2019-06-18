using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MBO
{
    public class ResultMange
    {
        public DataTable GetResult(string PERIOD_ID, string EMP_ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@EMP_ID", EMP_ID);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_RESULT_BY_PERIOD, param);
        }
        public DataTable GetResult(string PERIOD_ID, string EMP_ID, int GROUP)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@EMP_ID", EMP_ID);
            param.Add("@GROUP", GROUP);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_RESULT_BY_PERIOD_GROUP, param);
        }
        public DataTable GetSelfResult(string PERIOD_ID, string EMP_ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@EMP_ID", EMP_ID);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_RESULT_SELF, param);
        }
        public DataTable GetElementTable(int GROUP_ID, int RESULT_ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@GROUP_ID", GROUP_ID);
            param.Add("@RESULT_ID", RESULT_ID);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_ELEMENT_TABLE, param);
        }

        public int UpdateResult(MBO_Result res, int role)
        {
            if (role == 2)
            {
                return DBManager<MBO_Result>.Execute(StoredProcedure.UPDATE_RESULT, new
                {
                    ID = res.ID,
                    MBO_SELF_SCORE = res.MBO_SELF_SCORE,
                    MBO_M1_SCORE = res.MBO_M1_SCORE,
                    MBO_M2_SCORE = res.MBO_M2_SCORE,
                    MBO_FINAL_SCORE = res.MBO_FINAL_SCORE,
                    CAP_M1 = res.CAP_M1,
                    CAP_M2 = res.CAP_M2,
                    CAP_FINAL_SCORE = res.CAP_FINAL_SCORE,
                    TOTAL_SCORE = res.TOTAL_SCORE,
                    GRADE = res.GRADE,
                    M1_FINAL_SCORE = res.M1_FINAL_SCORE,
                    M1_GRADE = res.M1_GRADE
                }
                );
            }
            if (role == 1)
            {
                return DBManager<MBO_Result>.Execute(StoredProcedure.UPDATE_RESULT_M1, new
                {
                    ID = res.ID,

                    MBO_M1_SCORE = res.MBO_M1_SCORE,
                    M1_FINAL_SCORE = res.M1_FINAL_SCORE,
                    M1_GRADE = @res.M1_GRADE,
                    CAP_M1 = res.CAP_M1

                }
               );
            }
            return DBManager<MBO_Result>.Execute(StoredProcedure.UPDATE_RESULT_SELF, new
            {
                ID = res.ID,
                MBO_SELF_SCORE = res.MBO_SELF_SCORE
            }
               );
        }

        public int UpdateListResult(List<MBO_Result> results, string APPROVER)
        {
            int res = 0;
            UserManager um = new UserManager();
            foreach (var result in results)
            {
                string emp_id = GetEmpIdByResult(result.ID);
                int role = um.CheckRole(emp_id, APPROVER);
                res = UpdateResult(result, role);

            }
            return res;
        }

        public string GetEmpIdByResult(int ID)
        {
           var emp = DBManager<dynamic>.ExecuteDynamic(StoredProcedure.GET_EMPID_BY_RESULT, new { ID = ID }).FirstOrDefault();
            if (emp == null)
                return string.Empty;
            return emp.EMP_ID;
           
        }
        public int SaveScore(Score_Detail score, int ROLE)
        {
            return DBManager<Score_Detail>.Execute(StoredProcedure.INSERT_UPDATE_SCORE, new
            {
                RESULT_ID = score.RESULT_ID,
                FACTOR_ID = score.FACTOR_ID,
                M1_SCORE = score.M1_SCORE,
                M2_SCORE = score.M2_SCORE,
                ROLE = ROLE
            });
        }
        public int SaveListScore(List<Score_Detail> scores, int ROLE)
        {
            int t = 0;
            foreach (var score in scores)
            {
                t = SaveScore(score, ROLE);
            }
            return t;
        }

        public DataTable GetMBOResult(string eva_time, int group)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", eva_time);
            param.Add("@GROUP", group);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_RESULT_HR, param);
        }
        public DataTable GetMBOResult(string eva_time, int group=0, string query=null, string workgroup=null)
        {
            if (!string.IsNullOrEmpty(query)) query = query.ToString();
            if (!string.IsNullOrEmpty(workgroup)) workgroup = workgroup.ToString();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", eva_time);
            param.Add("@GROUP", group);
            param.Add("@QUERY", query);
            param.Add("@WORKGROUP", workgroup);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_RESULT_HR_BY_EMP, param);
        }
        public DataTable GetMBOResult(string eva_time, int group = 0, string query = null, string workgroup = null, int page=1)
        {
            var start = (page - 1) * 20 + 1;
            var end = start + 20 - 1;
            if (!string.IsNullOrEmpty(query)) query = query.ToString();
            if (!string.IsNullOrEmpty(workgroup)) workgroup = workgroup.ToString();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", eva_time);
            param.Add("@GROUP", group);
            param.Add("@QUERY", query);
            param.Add("@WORKGROUP", workgroup);
            param.Add("@start", start);
            param.Add("@end", end);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_MBO_RESULT, param);
        }
        public DataTable GetMBOResultExport(string eva_time, int group = 0, string query = null, string workgroup = null)
        {
            if (!string.IsNullOrEmpty(query)) query = query.ToString();
            if (!string.IsNullOrEmpty(workgroup)) workgroup = workgroup.ToString();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", eva_time);
            param.Add("@GROUP", group);
            param.Add("@QUERY", query);
            param.Add("@WORKGROUP", workgroup);

            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_MBO_RESULT_EXPORT, param);
        }
        public int GetCountMBOResult(string eva_time, int group = 0, string query = null, string workgroup = null)
        {
            if (!string.IsNullOrEmpty(query)) query = query.ToString();
            if (!string.IsNullOrEmpty(workgroup)) workgroup = workgroup.ToString();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", eva_time);
            param.Add("@GROUP", group);
            param.Add("@QUERY", query);
            param.Add("@WORKGROUP", workgroup);

            return (int)mgrDataSQL.ExecuteStoreScalar(StoredProcedure.COUNT_MBO_RESULT, param);
        }
        public DataTable GetEmployeeByApprover(string EMP_ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@EMP_ID", EMP_ID);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_EMP_BY_APPROVER, param);
        }

        public DataTable GetResultReview(string EMP_ID, string PERIOD_ID, int GROUP)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@EMP_ID", EMP_ID);
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@GROUP", GROUP);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_RESULT_REVIEW, param);
        }

        public bool IsOneApprover(string EMP_ID)
        {
           List<Employee_Approver> lst = DBManager<Employee_Approver>.ExecuteReader(StoredProcedure.CHECK_ONE_APPROVER, new
            {
                EMP_ID = EMP_ID
            });
           if (lst == null)
               return false;
           if (lst.Count == 1)
               return true;
           return false;
        }

        public DataTable GetResultInfo(string PERIOD_ID, int GROUP = 0, string QUERY="", int START=0, int END=10)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@GROUP", GROUP);
            param.Add("@QUERY", QUERY);
            param.Add("@START", START);
            param.Add("@END", END);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_EVA_INFO, param);
        }

        public int GetCountResultInfo(string PERIOD_ID, int GROUP = 0, string QUERY = "")
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@GROUP", GROUP);
            param.Add("@QUERY", QUERY);
            return (int) mgrDataSQL.ExecuteStoreScalar(StoredProcedure.GET_COUNT_EVA_INFO, param);
        }

        public DataTable GetFactorId(string RESULT_ID, string FACTOR_NAME)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@RESULT_ID", RESULT_ID);
            param.Add("@FACTOR_NAME", FACTOR_NAME);
            DataTable dtb =  mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_FACTOR_ID, param);
            return dtb;
        }

        public DataTable GetElementHr(int GROUP_ID, string ELNAME)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@GROUP_ID", GROUP_ID);
            param.Add("@ELNAME", ELNAME);
            DataTable dtb = mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_ELEMENT_HR, param);
            return dtb;
        }
    }
}