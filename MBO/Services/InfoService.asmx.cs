using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MBO.Services
{
    /// <summary>
    /// Summary description for InfoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class InfoService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int SaveScore(Score_Detail SCORE, int ROLE = 2)
        {
            ResultMange rm = new ResultMange();
            return rm.SaveScore(SCORE, ROLE);
        }

        [WebMethod]
        public int SaveRemark(int ID, string REMARK)
        {
            return DBManager<MBO_Result>.Execute(StoredProcedure.UPDATE_RESULT_REMARK, new { ID = ID, REMARK = REMARK });
        }

        [WebMethod]
        public int SaveFinal(int ID, string FINAL_GRADE, string REASON)
        {
            return DBManager<MBO_Result>.Execute(StoredProcedure.UPDATE_RESULT_HR_FINAL, new
            {
                ID = ID,
                FINAL_GRADE = FINAL_GRADE,
                REASON = REASON
            });
        }
    }
}
