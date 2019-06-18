using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MBO
{
    public class UserManager
    {
        public string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
        public bool Login(string EMP_ID, string PASSWORD)
        {
            PASSWORD = Encode(PASSWORD);
            Employee emp = DBManager<Employee>.ExecuteReader(StoredProcedure.Login, new { EMP_ID = EMP_ID, PASSWORD = PASSWORD }).FirstOrDefault();
            if (emp == null)
                return false;
            HttpContext.Current.Session["EMP_ID"] = emp.EMP_ID;
            HttpContext.Current.Session["Name"] = emp.NAME;

            return true;
        }

        public int ChangePassword(string EMP_ID, string PASSWORD, string NEWPASS)
        {
            PASSWORD = Encode(PASSWORD);
            NEWPASS = Encode(NEWPASS);
            return DBManager<Employee>.Execute(StoredProcedure.CHANGE_PASSWORD, new { EMP_ID = EMP_ID, OLDPASS = PASSWORD, NEWPASS = NEWPASS });
        }

        public int ResetPassword(string EMP_ID)
        {
            var PASSWORD = Encode("123456");
            return DBManager<Employee>.Execute(StoredProcedure.RESET_PASSWORD, new { EMP_ID = EMP_ID, PASSWORD=PASSWORD});
        }

        public List<Employee> GetAllUsers()
        {
            return DBManager<Employee>.ExecuteReader(StoredProcedure.GET_ALL_USERS);
        }

        public Employee GetEmpInfo(string EMP_ID)
        {
            return DBManager<Employee>.ExecuteReader(StoredProcedure.GET_USER_BY_ID, new { EMP_ID = EMP_ID }).FirstOrDefault();
        }

        public List<Employee> GetUserPaging(int START, int END)
        {
            return DBManager<Employee>.ExecuteReader(StoredProcedure.GET_USERS_PAGING, new { START = START, END = END });
        }

        public int CreateUser(string EMP_ID, string NAME, string WORKGROUP, DateTime ENTER_DATE, int EVA_GROUP)
        {
            if (ENTER_DATE == DateTime.MinValue)
                return DBManager<Employee>.Execute(StoredProcedure.CreateEmployee, new { EMP_ID = EMP_ID, NAME = NAME, WORKGROUP = WORKGROUP, ENTER_DATE = (DateTime?)null, EVA_GROUP =EVA_GROUP});
            return DBManager<Employee>.Execute(StoredProcedure.CreateEmployee, new { EMP_ID = EMP_ID, NAME = NAME, WORKGROUP = WORKGROUP, ENTER_DATE = ENTER_DATE, EVA_GROUP = EVA_GROUP });
        }

        public int UpdateUser(string EMP_ID, string NAME, string WORKGROUP, DateTime ENTER_DATE, int EVA_GROUP)
        {
            if (ENTER_DATE == DateTime.MinValue)
                return DBManager<Employee>.Execute(StoredProcedure.UPDATE_EMPLOYEE, new { EMP_ID = EMP_ID, NAME = NAME, WORKGROUP = WORKGROUP, ENTER_DATE = (DateTime?)null, EVA_GROUP = EVA_GROUP });
            return DBManager<Employee>.Execute(StoredProcedure.UPDATE_EMPLOYEE, new { EMP_ID = EMP_ID, NAME = NAME, WORKGROUP = WORKGROUP, ENTER_DATE = ENTER_DATE, EVA_GROUP=EVA_GROUP });
        }

        public int DeleteUser(string EMP_ID)
        {
            return DBManager<Employee>.Execute(StoredProcedure.DeleteEmployee, new { EMP_ID = EMP_ID });
        }

        public int AssignApprover(string EMP_ID, string APPROVER, int ROLE)
        {
            return DBManager<Employee_Approver>.Execute(StoredProcedure.ASSIGN_APPROVER, new { EMP_ID = EMP_ID, APPROVER = APPROVER, ROLE = ROLE });
        }

        public List<Employee_Approver> CheckExistApprover(string EMP_ID, string APPROVER, int ROLE)
        {
            return DBManager<Employee_Approver>.ExecuteReader(StoredProcedure.CHECK_ASSIGN_APPROVER, new { EMP_ID = EMP_ID, APPROVER = APPROVER, ROLE = ROLE });
        }

        public int ChangeApprover(string EMP_ID, string APPROVER, int ROLE)
        {
            return DBManager<Employee_Approver>.Execute(StoredProcedure.CHANGE_APPROVER, new { EMP_ID = EMP_ID, APPROVER = APPROVER, ROLE = ROLE});
        }

        public List<WorkGroup> GetWorkgroup()
        {
            return DBManager<WorkGroup>.ExecuteReader(StoredProcedure.GET_WORKGROUP);
        }
        public List<Eva_Group> GetEvagroup()
        {
            return DBManager<Eva_Group>.ExecuteReader(StoredProcedure.GET_EVA_GROUP);
        }

        public List<Period> GetPeriod()
        {
            return DBManager<Period>.ExecuteReader(StoredProcedure.GETALL_PERIOD);
        }

        public Period GetPeriod(string EVA_TIME)
        {
            return DBManager<Period>.ExecuteReader(StoredProcedure.GET_PERIOD, new { EVA_TIME = EVA_TIME }).FirstOrDefault();
        }

        public int InsertPeriod(Period period)
        {
            return DBManager<Period>.Execute(StoredProcedure.INSERT_PERIOD, new {
            EVA_TIME=period.EVA_TIME,
            EVA_START=period.EVA_START,
            EVA_END=period.EVA_END,
            STATUS=period.STATUS
        });
        }
        public int DeletePeriod(string EVA_TIME)
        {
            return DBManager<Period>.Execute(StoredProcedure.DELETE_PERIOD, new { EVA_TIME = EVA_TIME });
        }
        public int DisablePeriod(string EVA_TIME, int STATUS=1)
        {
            return DBManager<Period>.Execute(StoredProcedure.DISABLE_PERIOD, new { EVA_TIME = EVA_TIME, STATUS=STATUS });
        }
       
        public Employee GetApprover(string EMP_ID, int ROLE)
        {
            return DBManager<Employee>.ExecuteReader(StoredProcedure.GET_APPROVER_BY_EMP, new { EMP_ID = EMP_ID, ROLE = ROLE }).FirstOrDefault();
        }

        public int GetCountEmp(string query = null)
        {
            if (string.IsNullOrEmpty(query))
                return (int)DBManager<Employee>.ExecuteScalar(StoredProcedure.GET_COUNT_USERS);
            return (int)DBManager<Employee>.ExecuteScalar(StoredProcedure.GET_SEARCH_USERS_COUNT, new { KEY = query });
        }

        public int CheckRole(string EMP_ID, string APPROVER)
        {
            List<Employee_Approver> lst = DBManager<Employee_Approver>.ExecuteReader(StoredProcedure.CHECK_ROLE, new { EMP_ID = EMP_ID, APPROVER = APPROVER });
            if (lst == null)
                return 0;
            var role = 0;
            foreach (var r in lst)
            {
                if (r.ROLE > role)
                    role = r.ROLE;
            }
            return role;

        }
        public List<Employee> SearchUsers(string key, int START, int END)
        {
            return DBManager<Employee>.ExecuteReader(StoredProcedure.SEARCH_USERS_PAGING, new {KEY=key, START = START, END = END });
        }

        public int UpdatePicture(string EMP_ID, string PICTURE)
        {
            return DBManager<Employee>.Execute(StoredProcedure.UPDATE_IMG, new { EMP_ID = EMP_ID, PICTURE = PICTURE });
        }

        public List<Employee> GetNoMBOEMPS()
        {
            return DBManager<Employee>.ExecuteReader(StoredProcedure.GET_NO_MBO_EMP);
        }

        public int UnAssignApprover(string EMP_ID)
        {
            return DBManager<Employee_Approver>.Execute(StoredProcedure.UNASSIGN_APPROVER, new { EMP_ID=EMP_ID });
        }
    }
}