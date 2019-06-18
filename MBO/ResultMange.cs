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
        public DataTable GetSelfResult(string PERIOD_ID, string EMP_ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@PERIOD_ID", PERIOD_ID);
            param.Add("@EMP_ID", EMP_ID);
            return mgrDataSQL.ExecuteStoreReader(StoredProcedure.GET_RESULT_SELF, param);
        }
    }
}