using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MBO.Services
{
    /// <summary>
    /// Summary description for DefaultService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DefaultService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public Period GetPeriod(string EVA_TIME)
        {
            UserManager um = new UserManager();
            Period period = um.GetPeriod(EVA_TIME);
            return period;
        }

        [WebMethod]
        public int SavePlans(string EVA_TIME, string EMP_ID, int RESULT_ID, List<MBO_Plan> PLANS, string DEL, List<MBO_Plan> EDIT_PLANS)
        {
            PlanManger pm = new PlanManger();
            return pm.SavePlan(EVA_TIME, EMP_ID, RESULT_ID, PLANS, DEL, EDIT_PLANS);
        }

        [WebMethod]
        public int Confirm(List<CONFIRM_DATA> CONFIRM_DATA)
        {
            var t = 0;
            foreach (var data in CONFIRM_DATA)
            {
                if (data.CURR_ST > 3)
                    return -1;
                PlanManger pm = new PlanManger();
                if (string.Compare(data.APPROVER, data.EMP_ID) == 0)
                {
                    if (data.ACTION == 1 && data.CURR_ST == 0)
                        t= pm.Confirm(data.ID, 1);
                    if (data.ACTION == -1 && data.CURR_ST == 1)
                        t= pm.Confirm(data.ID, 0);
                   
                }
                int role = 0;
                List<Employee_Approver> lstRoles = pm.GetAllRoles(data.EMP_ID, data.APPROVER);
                if (lstRoles != null)
                {
                    foreach (Employee_Approver e in lstRoles)
                    {
                        if (e.ROLE > role)
                            role = e.ROLE;
                    }
                }
                if (role == 1)
                {
                    if (data.ACTION == 1 && data.CURR_ST <= 1)
                        t= pm.Confirm(data.ID, 2);
                    if (data.ACTION == -1 && data.CURR_ST == 2)
                        t= pm.Confirm(data.ID, 1);

                }
                if (role == 2)
                {
                    
                    if (lstRoles.Count == 2)
                    {
                        if (data.ACTION == -1)
                            return pm.Confirm(data.ID, 1);
                    }
                    if (data.ACTION == 1)
                        t= pm.Confirm(data.ID, 3);

                    if (data.ACTION == -1)
                        t= pm.Confirm(data.ID, 2);

                }
               
            }
            return t;
        }
        [WebMethod]
        public int ConfirmPlan(List<CONFIRM_DATA> CONFIRM_DATA)
        {
            var t = 0;
            foreach (var data in CONFIRM_DATA)
            {
                if (data.CURR_ST > 3)
                    return -1;
                PlanManger pm = new PlanManger();
                if (string.Compare(data.APPROVER.Trim(), data.EMP_ID.Trim()) == 0)
                {
                    if (data.ACTION == 1 && data.CURR_ST == 0)
                        return pm.ConfirmPlan(data.ID, 1);
                    if (data.ACTION == -1 && data.CURR_ST == 1)
                        return pm.ConfirmPlan(data.ID, 0);
                  
                }
                int role = 0;
                List<Employee_Approver> lstRoles = pm.GetAllRoles(data.EMP_ID, data.APPROVER);
                if (lstRoles != null)
                {
                    foreach (Employee_Approver e in lstRoles)
                    {
                        if (e.ROLE > role)
                            role = e.ROLE;
                    }
                }
               
                if (role == 1)
                {
                    if (data.ACTION == 1 && data.CURR_ST <= 1)
                        t= pm.ConfirmPlan(data.ID, 2);
                    if (data.ACTION == -1 && data.CURR_ST == 2)
                        t= pm.ConfirmPlan(data.ID, 1);

                }
                if (role == 2)
                {
                    
                    if (lstRoles.Count == 2)
                    {
                        if (data.ACTION == -1)
                            return pm.ConfirmPlan(data.ID, 1);
                    }
                    if (data.ACTION == 1)
                        t= pm.ConfirmPlan(data.ID, 3);

                    if (data.ACTION == -1)
                        t= pm.ConfirmPlan(data.ID, 2);

                }

            }
            return t;
        }
    }

    public class CONFIRM_DATA
    {
        public int ID { get; set; }
        public string APPROVER { get; set; }
        public string EMP_ID { get; set; }
        public int ACTION { get; set; }
        public int CURR_ST { get; set; }
    }
}
