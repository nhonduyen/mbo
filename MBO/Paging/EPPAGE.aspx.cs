using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace MBO.Paging
{
    public partial class EPPAGE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(Description = "Server Side DataTables support", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void Data(object parameters)
        {
            var req = DataTableParameters.Get(parameters);
            var resultSet = new DataTableResultSet();
            resultSet.draw = req.Draw;
            UserManager UM = new UserManager();
            List<Employee> emps = new List<Employee>();
            if (!string.IsNullOrWhiteSpace(req.SearchValue))
            {
                emps = UM.SearchUsers(req.SearchValue, req.Start + 1, req.Start + req.Length + 1);
                resultSet.recordsTotal = resultSet.recordsFiltered = UM.GetCountEmp(req.SearchValue);
            }
            else
            {
                emps = UM.GetUserPaging(req.Start + 1, req.Start + req.Length + 1);
                resultSet.recordsTotal = resultSet.recordsFiltered = UM.GetCountEmp();
            }


            foreach (var emp in emps)
            {
                Employee app1 = UM.GetApprover(emp.EMP_ID, 1);
                Employee app2 = UM.GetApprover(emp.EMP_ID, 2);
                string name1 = (app1 == null) ? "" : app1.NAME;
                string name2 = (app2 == null) ? "" : app2.NAME;
                string enter_date = string.Empty;
                string role = string.Empty;

                if (emp.ENTER_DATE != DateTime.MinValue) enter_date = emp.ENTER_DATE.ToString("yyyy-MM-dd");
               
                var columns = new List<string>();
                columns.Add("<input type='checkbox' class='ckb' data-emp-id='" + emp.EMP_ID + "' data-group='" + emp.EVA_GROUP + "'/>");
                columns.Add("<img src='" + emp.PICTURE + "' class='img-rounded' height='100' width='100' alt='X' />");
                columns.Add(emp.EMP_ID);
                columns.Add(emp.NAME);
                columns.Add(emp.WORKGROUP);
                columns.Add(enter_date);
               
                columns.Add(name1);
                columns.Add(name2);

                resultSet.data.Add(columns);
            }


            SendResponse(HttpContext.Current.Response, resultSet);
        }

       
        private static void SendResponse(HttpResponse response, DataTableResultSet result)
        {
            response.Clear();
            response.Headers.Add("X-Content-Type-Options", "nosniff");
            response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            response.ContentType = "application/json; charset=utf-8";
            response.Write(result.ToJSON());
            response.Flush();
            response.End();
        }
    }
}