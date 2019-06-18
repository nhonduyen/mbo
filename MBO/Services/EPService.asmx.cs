using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace MBO.Services
{
    /// <summary>
    /// Summary description for EPService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EPService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int InsertEmployee(string EMP_ID, string NAME, string WORKGROUP, DateTime ENTER_DATE, int EVA_GROUP)
        {
            UserManager um = new UserManager();
            return um.CreateUser(EMP_ID, NAME, WORKGROUP, ENTER_DATE, EVA_GROUP);            
        }

        [WebMethod]
        public int UpdateEmployee(string EMP_ID, string NAME, string WORKGROUP, DateTime ENTER_DATE, int EVA_GROUP)
        {
            if (ENTER_DATE == DateTime.MinValue) ENTER_DATE = default(DateTime);
            UserManager um = new UserManager();
            return um.UpdateUser(EMP_ID, NAME, WORKGROUP, ENTER_DATE, EVA_GROUP);
        }

        [WebMethod]
        public int Unassign(string EMP_ID)
        {

            UserManager um = new UserManager();
            return um.UnAssignApprover(EMP_ID);
        }

        [WebMethod]
        public int DeleteEmployee(string EMP_ID)
        {

            UserManager um = new UserManager();
            return um.DeleteUser(EMP_ID);
        }
        [WebMethod]
        public int ResetPass(string EMP_ID)
        {

            UserManager um = new UserManager();
            return um.ResetPassword(EMP_ID);
        }

        [WebMethod]
        public int Assign(string EMP_ID, string APPROVER, int ROLE)
        {

            UserManager um = new UserManager();
            var exist = um.CheckExistApprover(EMP_ID, APPROVER, ROLE);
            if (exist != null && exist.Count > 0)
                return -1;
            return um.AssignApprover(EMP_ID, APPROVER, ROLE);
        }

        [WebMethod]
        public int ChangeApprover(string EMP_ID, string APPROVER, int ROLE)
        {

            UserManager um = new UserManager();
            return um.ChangeApprover(EMP_ID, APPROVER, ROLE);
        }
       
    }
}
