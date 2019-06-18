using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace MBO.Services
{
    /// <summary>
    /// Summary description for PeriodServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PeriodServices : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int Insert(string EVA_TIME, DateTime EVA_START, DateTime EVA_END, int STATUS)
        {
            Period p = new Period();
            p.EVA_TIME = EVA_TIME;
            p.EVA_START = EVA_START;
            p.EVA_END = EVA_END;
            p.STATUS = STATUS;
            p.SET_MBO = 0;
            UserManager um = new UserManager();
            return um.InsertPeriod(p);
        }

        [WebMethod]
        public int Delete(string EVA_TIME)
        {
            UserManager um = new UserManager();
            return um.DeletePeriod(EVA_TIME);
        }

        [WebMethod]
        public int Disable(string EVA_TIME, int STATUS)
        {
            UserManager um = new UserManager();
            return um.DisablePeriod(EVA_TIME, STATUS);
        }

       
        [WebMethod]
        public int SetMbo(string EVA_TIME)
        {
            UserManager um = new UserManager();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@EVA_TIME", EVA_TIME);
            DataTable tbCheck = mgrDataSQL.ExecuteStoreReader(StoredProcedure.CHECK_NO_MBO, param);
            int t = 0;
            if (tbCheck.Rows.Count == 0)
            {
                List<Employee> emps = um.GetNoMBOEMPS();
                foreach (var emp in emps)
                {
                    Dictionary<string, object> param1 = new Dictionary<string, object>();
                    param1.Add("@EVA_TIME", EVA_TIME);
                    param1.Add("@EMP_ID", emp.EMP_ID);
                    t = mgrDataSQL.ExecuteStoreNonQuery(StoredProcedure.INSERT_NO_MBO_RESULT, param1);
                }
                Dictionary<string, object> param2 = new Dictionary<string, object>();
                param2.Add("@EVA_TIME", EVA_TIME);
                t = mgrDataSQL.ExecuteStoreNonQuery(StoredProcedure.UPDATE_PERIOD_SET_NO_MBO, param2);
            }
            return t;
        }
    }
}
