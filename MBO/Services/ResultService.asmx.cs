using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace MBO.Services
{
    /// <summary>
    /// Summary description for ResultService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ResultService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<ElementTable> GetElementTable(int GROUP_ID, int RESULT_ID, string APPROVER)
        {
            UserManager um = new UserManager();
            ResultMange rm = new ResultMange();
            var emp_id = rm.GetEmpIdByResult(RESULT_ID);
            var role = um.CheckRole(emp_id, APPROVER);
            DataTable dtb = rm.GetElementTable(GROUP_ID, RESULT_ID);
            List<ElementTable> elements = new List<ElementTable>();
            foreach (DataRow r in dtb.Rows)
            {
               
                double m1 = 0;
                double m2=0;
                if (!string.IsNullOrEmpty(r["M1_SCORE"].ToString()))
                    m1 = Convert.ToDouble(r["M1_SCORE"].ToString());
                if (!string.IsNullOrEmpty(r["M2_SCORE"].ToString()))
                    m2 = Convert.ToDouble(r["M2_SCORE"].ToString());
                ElementTable e = new ElementTable();
                e.Factor_ID = Convert.ToInt32(r["ID"].ToString());
                e.M1_Score = m1;
                e.M2_Score = m2;
                e.Weight = Convert.ToInt32(r["WEIGHT"].ToString());
                e.Element = r["ELEMENT_NAME"].ToString().Trim();
                e.Factor = r["FACTOR_NAME"].ToString();
                if (role == 0)
                {
                    e.M1_Score = 0;
                    e.M2_Score = 0;
                }
                if (role == 1)
                {
                    e.M2_Score = 0;
                }
                if (e.Element.Equals("Other certificate"))
                {
                    // get custom 
                    e.M1_Score = m1;
                    e.M2_Score = m2;
                }
                elements.Add(e);
            }
            return elements;
        }

        [WebMethod]
        public List<ElementTable> GetElementTable1(int GROUP_ID, int RESULT_ID)
        {
            UserManager um = new UserManager();
            ResultMange rm = new ResultMange();
            var emp_id = rm.GetEmpIdByResult(RESULT_ID);
           
            DataTable dtb = rm.GetElementTable(GROUP_ID, RESULT_ID);
            List<ElementTable> elements = new List<ElementTable>();
            foreach (DataRow r in dtb.Rows)
            {

                double m1 = 0;
                double m2 = 0;
                if (!string.IsNullOrEmpty(r["M1_SCORE"].ToString()))
                    m1 = Convert.ToDouble(r["M1_SCORE"].ToString());
                if (!string.IsNullOrEmpty(r["M2_SCORE"].ToString()))
                    m2 = Convert.ToDouble(r["M2_SCORE"].ToString());
                ElementTable e = new ElementTable();
                e.Factor_ID = Convert.ToInt32(r["ID"].ToString());
                e.M1_Score = m1;
                e.M2_Score = m2;
                e.Weight = Convert.ToInt32(r["WEIGHT"].ToString());
                e.Element = r["ELEMENT_NAME"].ToString();
                e.Factor = r["FACTOR_NAME"].ToString();
               
                elements.Add(e);
            }
            return elements;
        }

        [WebMethod]
        public List<Review> GetResultReview(int GROUP, string EMP_ID, string PERIOD_ID)
        {
            ResultMange rm = new ResultMange();

            DataTable dtb = rm.GetResultReview(EMP_ID, PERIOD_ID, GROUP);
            List<Review> reviews = new List<Review>();
            foreach (DataRow r in dtb.Rows)
            {
                Review rv = new Review();
                rv.NAME = r["NAME"].ToString();
                rv.SCORE = r["SCORE"] == DBNull.Value ? 0 : Convert.ToInt32( r["SCORE"].ToString());
                rv.GROUP = r["WORKGROUP"].ToString();
                rv.GRADE = r["GRADE"].ToString();
                reviews.Add(rv);
            }
            return reviews;
        }

        [WebMethod]
        public int UpdateResult(List<MBO_Result> RESULTS, List<MBO_Plan> PLANS, string APPROVER)
        {
            ResultMange rm = new ResultMange();
            PlanManger p = new PlanManger();
           p.UpdateListPlan(PLANS, APPROVER);
            return rm.UpdateListResult(RESULTS, APPROVER);
        }

        [WebMethod]
        public int CheckRole(string EMP_ID, string APPROVER)
        {
            UserManager um = new UserManager();
            return um.CheckRole(EMP_ID,APPROVER);
        }

        [WebMethod]
        public int SaveScore(List<Score_Detail> SCORES, int ROLE)
        {
            ResultMange rm = new ResultMange();
            return rm.SaveListScore(SCORES, ROLE);
        }

        [WebMethod]
        public List<MBO_Plan> GetActionPlanById( int ID)
        {
            PlanManger pm = new PlanManger();
            return pm.GetActionPlan(ID);
        }
    }

    public class ElementTable
    {
        public string Element { get; set; }
        public string Factor { get; set; }
        public int Factor_ID { get; set; }
        public double M1_Score { get; set; }
        public double M2_Score { get; set; }
        public int Weight { get; set; }
    }

    public class Review
    {
        public string NAME { get; set; }
        public string GROUP { get; set; }
        public int SCORE { get; set; }
        public string GRADE { get; set; }
   
    }
}
