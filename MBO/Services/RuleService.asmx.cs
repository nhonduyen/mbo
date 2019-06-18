using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MBO.Services
{
    /// <summary>
    /// Summary description for RuleService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RuleService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int Insert(int NUM_EMPS, int S, int A, int BCD)
        {
            return DBManager<EVA_RULE>.Execute(StoredProcedure.INSERT_EVA_RULE, new { NUM_EMPS = NUM_EMPS, S=S, A=A,BCD=BCD });
        }
        [WebMethod]
        public int Update(int NUM_EMPS, int S, int A, int BCD)
        {
            return DBManager<EVA_RULE>.Execute(StoredProcedure.UPDATE_EVA_RULE, new { NUM_EMPS = NUM_EMPS, S=S, A=A,BCD=BCD });
        }
        [WebMethod]
        public int Delete(int NUM_EMPS)
        {
            return DBManager<EVA_RULE>.Execute(StoredProcedure.DELETE_EVA_RULE, new { NUM_EMPS = NUM_EMPS });
        }
    }
}
