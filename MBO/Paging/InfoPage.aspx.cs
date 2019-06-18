using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;

namespace MBO.Paging
{
    public partial class InfoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod(Description = "Server Side DataTables support", EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void Data(object parameters, string PERIOD_ID, int GROUP, string QUERY)
        {
            var req = DataTableParameters.Get(parameters);
            var resultSet = new DataTableResultSet();
            resultSet.draw = req.Draw;
            ResultMange rm = new ResultMange();

            var result = rm.GetResultInfo(PERIOD_ID, GROUP, QUERY, req.Start + 1, req.Start + req.Length + 1);
            resultSet.recordsTotal = resultSet.recordsFiltered = rm.GetCountResultInfo(PERIOD_ID, GROUP, QUERY);

            foreach (DataRow r in result.Rows)
            {
                var F1 = "";
                var F2 = "";
                var F1Value="";
                var F2Value="";
                DataTable dtb1 = rm.GetFactorId(r["RID"].ToString(), "Toeic Speaking/Topik");
                DataTable dtb2 = rm.GetFactorId(r["RID"].ToString(), "professional certificate/ diploma");
                DataTable dtb3 = rm.GetFactorId(r["RID"].ToString(), "Chứng chỉ chuyên môn/ Bằng cấp");
                DataTable eltb = rm.GetElementHr(GROUP, "Other certificate");
                foreach (DataRow r1 in eltb.Rows)
                {
                    var factor_name = r1["FACTOR_NAME"].ToString().Trim();
                    if (factor_name.Contains("certificate"))
                    {
                        F2 = r1["FID"].ToString();
                    }
                    if (factor_name.Contains("Toeic"))
                    {
                        F1 = r1["FID"].ToString();
                    }
                    if (factor_name.Trim().Contains("Chứng chỉ"))
                    {
                        F2 = r1["FID"].ToString();
                        F2Value = dtb3.Rows.Count > 0 ? dtb3.Rows[0]["M1_SCORE"].ToString() : "";
                    }
                }
                if (dtb1.Rows.Count > 0)
                {
                    F1Value = dtb1.Rows[0]["M1_SCORE"].ToString();
                }
                if (dtb2.Rows.Count > 0)
                {
                    F2Value = dtb2.Rows[0]["M1_SCORE"].ToString();
                }
               
                var emp_id = r["EMP_ID"].ToString().Trim();
                var reason = r["REASON"] == null ? "" : r["REASON"].ToString().Trim();
                var columns = new List<string>();
                columns.Add(emp_id);
                columns.Add(r["NAME"].ToString());
                columns.Add(r["GROUP_NAME"].ToString());
                columns.Add("<input type='text' class='toeic' data-id='" + r["RID"].ToString() + "' data-fid='" + F1+ "' value='" + F1Value+ "'/>");
                columns.Add("<input type='text' class='cer' data-id='" + r["RID"].ToString() + "' data-fid='" + F2 + "' value='" + F2Value + "'/>");
                columns.Add(string.Format("<input id='checkBox' type='checkbox' data-value='{0}' data-id='{1}' {2}>", "C", r["RID"].ToString(), reason == "Absent 1 day" ? "checked='checked'" : ""));
                columns.Add(string.Format("<input id='checkBox' type='checkbox' data-value='{0}' data-id='{1}' {2}>", "D", r["RID"].ToString(), reason == "Absent 2 day" ? "checked='checked'" : ""));
                columns.Add(string.Format("<input id='checkBox' type='checkbox' data-value='{0}' data-id='{1}' {2}>", "C", r["RID"].ToString(), reason == "Unpaid 10~14 days" ? "checked='checked'" : ""));
                columns.Add(string.Format("<input id='checkBox' type='checkbox' data-value='{0}' data-id='{1}' {2}>", "D", r["RID"].ToString(), reason == "Unpaid >15 days" ? "checked='checked'" : ""));
                columns.Add(string.Format("<input id='checkBox' type='checkbox' data-value='{0}' data-id='{1}' {2}>", "C", r["RID"].ToString(), reason == "Warning 1" ? "checked='checked'" : ""));
                columns.Add(string.Format("<input id='checkBox' type='checkbox' data-value='{0}' data-id='{1}' {2}>", "D", r["RID"].ToString(), reason == "Warning 2" ? "checked='checked'" : ""));
                columns.Add(string.Format("<input id='checkBox' type='checkbox' data-value='{0}' data-id='{1}' {2}>", "B", r["RID"].ToString(), reason == "Having explaination letter" ? "checked='checked'" : ""));
                columns.Add("<textarea data-id='" + r["RID"].ToString() + "'>" + r["REMARK"].ToString() + "</textarea>");
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