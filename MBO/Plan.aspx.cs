using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace MBO
{
    public partial class Plan : System.Web.UI.Page
    {
        protected List<Period> Periods = new List<Period>();
        protected string Duration;
        protected int CNT_MBO;
        protected DataTable Result;
        protected List<Eva_Group> Groups = new List<Eva_Group>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMP_ID"] == null)
                    Response.Redirect("Login.aspx");
                var period_id = Request.QueryString["eva"];
                var group = Convert.ToInt32(Request.QueryString["group"]);
                var query = string.IsNullOrEmpty(Request.QueryString["query"]) ? "" : Request.QueryString["query"].ToString();
                var page = string.IsNullOrWhiteSpace(Request.QueryString["page"]) ? 1 : Convert.ToInt32(Request.QueryString["page"].ToString());

                UserManager um = new UserManager();
                PlanManger pm = new PlanManger();
                Groups = um.GetEvagroup();
                Periods = um.GetPeriod();
                Duration = Periods[0].EVA_START.ToShortDateString() + "~" + Periods[0].EVA_END.ToShortDateString();
                if (string.IsNullOrWhiteSpace(period_id))
                    period_id = Periods[0].EVA_TIME;
                Result = pm.GetMboPlans(period_id, group, query, page);
                var count = pm.CountPlans(period_id, group, query);
                CNT_MBO = (count + 20 - 1) / 20;

                if (!string.IsNullOrEmpty(Request.QueryString["export"]))
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    var template = Server.MapPath("~/Template/MBO_PLAN.xlsx");
                    DataTable dtb = pm.GetPlanExport(period_id, group, query);

                    using (ExcelPackage package = new ExcelPackage(new FileInfo(template)))
                    {
                        ExcelWorksheet ws = package.Workbook.Worksheets.FirstOrDefault();

                        for (int i = 0; i < dtb.Rows.Count; i++)
                        {
                            var startIndex = i + 3;
                            DataRow r = dtb.Rows[i];
                            int statusCode = string.IsNullOrWhiteSpace(r["PLAN_STATUS"].ToString()) ? 0 : Convert.ToInt32(r["PLAN_STATUS"].ToString());
                            var status = "";
                            switch (statusCode)
                            {
                                case 0:
                                    status = "Unconfirmed";
                                    if (group == 2 || group == 4)
                                        status = "Self confirm";
                                    break;
                                case 1:
                                    status = "Self confirm";
                                    break;
                                case 2:
                                    status = "1st confirm";
                                    break;
                                case 3:
                                    status = "2nd confirm";
                                    break;
                                default:
                                    status = "Unconfirmed";
                                    break;
                            }

                            ws.Cells["B" + startIndex].Value = r["EMP_ID"];
                            ws.Cells["C" + startIndex].Value = r["NAME"];
                            ws.Cells["D" + startIndex].Value = r["WORKGROUP"];
                            ws.Cells["E" + startIndex].Value = r["ENTER_DATE"] is DBNull ? "" : Convert.ToDateTime(r["ENTER_DATE"]).ToString("yyyy-MM-dd");
                            ws.Cells["F" + startIndex].Value = r["CONT"].ToString().Replace("<br>", Environment.NewLine).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<");
                            ws.Cells["G" + startIndex].Value = r["ACTION_PLAN"].ToString().Replace("<br>", Environment.NewLine).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<");
                            ws.Cells["G" + startIndex].Style.WrapText = true;
                            ws.Cells["H" + startIndex].Value = r["TARGET"].ToString().Replace("<br>", Environment.NewLine).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<");
                            ws.Cells["H" + startIndex].Style.WrapText = true;
                            ws.Cells["I" + startIndex].Value = r["WEIGHT"];
                            ws.Cells["J" + startIndex].Value = r["LVL"];
                            ws.Cells["K" + startIndex].Value = status;

                        }
                        for (int i = 0; i < dtb.Rows.Count; i++)
                        {
                            var countPlan = 0;
                            var startIndex = i + 3;
                            
                            int statusCode = string.IsNullOrWhiteSpace(dtb.Rows[i]["PLAN_STATUS"].ToString()) ? 0 : Convert.ToInt32(dtb.Rows[i]["PLAN_STATUS"].ToString());
                            var status = "";
                            switch (statusCode)
                            {
                                case 0:
                                    status = "Unconfirmed";
                                    if (group == 2 || group == 4)
                                        status = "Self confirm";
                                    break;
                                case 1:
                                    status = "Self confirm";
                                    break;
                                case 2:
                                    status = "1st confirm";
                                    break;
                                case 3:
                                    status = "2nd confirm";
                                    break;
                                default:
                                    status = "Unconfirmed";
                                    break;
                            }
                            for (int j = 0; j < dtb.Rows.Count; j++)
                            {
                                if (dtb.Rows[i]["EMP_ID"].ToString().Equals(dtb.Rows[j]["EMP_ID"].ToString()))
                                {
                                    countPlan++;
                                }
                            }
                            ws.Cells["B" + startIndex + ":B" + (startIndex + countPlan - 1).ToString()].Merge = true;
                            //ws.Cells["B" + startIndex].Value = dtb.Rows[i]["EMP_ID"];
                            if (dtb.Rows[i]["EMP_ID"].ToString().Contains("quantri"))
                            {
                                ws.Cells["B" + startIndex].Value = dtb.Rows[i]["EMP_ID"].ToString();
                            }
                            else
                            {
                                ws.Cells["B" + startIndex].Value = Convert.ToInt32(dtb.Rows[i]["EMP_ID"].ToString());
                                ws.Cells["B" + startIndex].Style.Numberformat.Format = "#";
                            }
                            ws.Cells["C" + startIndex + ":C" + (startIndex + countPlan - 1).ToString()].Merge = true;
                            ws.Cells["C" + startIndex].Value = dtb.Rows[i]["NAME"];
                            ws.Cells["D" + startIndex + ":D" + (startIndex + countPlan - 1).ToString()].Merge = true;
                            ws.Cells["D" + startIndex].Value = dtb.Rows[i]["WORKGROUP"];
                            ws.Cells["E" + startIndex + ":E" + (startIndex + countPlan - 1).ToString()].Merge = true;
                            ws.Cells["E" + startIndex].Value = dtb.Rows[i]["ENTER_DATE"] is DBNull ? "" : Convert.ToDateTime(dtb.Rows[i]["ENTER_DATE"]).ToString("yyyy-MM-dd");
                            ws.Cells["K" + startIndex + ":K" + (startIndex + countPlan - 1).ToString()].Merge = true;
                            ws.Cells["K" + startIndex].Value = status;
                            i += countPlan - 1;
                        }
                        for (int i = 2; i <= 12; i++)
                        {
                            ws.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            ws.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                            //wrap text in the cells
                            ws.Column(i).Style.WrapText = true;
                        }
                    
                        Byte[] fileBytes = package.GetAsByteArray();
                        Response.ClearContent();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;  filename=" + fileName);
                        Response.BinaryWrite(fileBytes);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }
}