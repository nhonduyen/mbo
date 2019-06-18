using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBO
{
    public class UserManager
    {
        public bool Login(string EMP_ID, string PASSWORD)
        {
            Employee emp = DBManager<Employee>.ExecuteReader(StoredProcedure.Login, new { EMP_ID = EMP_ID, PASSWORD = PASSWORD }).FirstOrDefault();
            if (emp.EMP_ID != null)
            {
                HttpContext.Current.Session["EMP_ID"] = emp.EMP_ID;
                HttpContext.Current.Session["Name"] = emp.NAME;
                HttpContext.Current.Session["ROLE"] = emp.ROLE;
                return true;
            }
            return false;
        }

        public int ChangePassword(string EMP_ID, string PASSWORD, string NEWPASS)
        {
            return DBManager<Employee>.Execute(StoredProcedure.CHANGE_PASSWORD, new { EMP_ID = EMP_ID, OLDPASS = PASSWORD, NEWPASS = NEWPASS });
        }

        public List<Employee> GetAllUsers()
        {
            return DBManager<Employee>.ExecuteReader(StoredProcedure.GET_ALL_USERS);
        }

        public int CreateUser(string EMP_ID, string NAME, string PASSWORD, string WORKGROUP, string ROLE, DateTime ENTER_DATE)
        {
            return DBManager<Employee>.Execute(StoredProcedure.CreateEmployee, new { EMP_ID = EMP_ID, NAME = NAME, PASSWORD = PASSWORD, 
                WORKGROUP = WORKGROUP, ROLE = ROLE, ENTER_DATE = ENTER_DATE });
        }

        public int AssignApprover(string EMP_ID, string APPROVER, int ROLE)
        {
            return DBManager<Employee_Approver>.Execute(StoredProcedure.ASSIGN_APPROVER, new { EMP_ID = EMP_ID, APPROVER = APPROVER, ROLE = ROLE });
        }

        public List<WorkGroup> GetWorkgroup()
        {
            return DBManager<WorkGroup>.ExecuteReader(StoredProcedure.GET_WORKGROUP);
        }
    }
}