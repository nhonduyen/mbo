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
using OfficeOpenXml.Drawing;

namespace MBO
{
    public partial class MBOResult : System.Web.UI.Page
    {
        protected List<Period> Periods = new List<Period>();
        protected string Duration;
        protected int CNT_MBO;
        protected DataTable Result1;
        protected List<Eva_Group> Groups = new List<Eva_Group>();
        protected List<WorkGroup> WGroups = new List<WorkGroup>();
        protected ResultMange RM = new ResultMange();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EMP_ID"] == null)
                    Response.Redirect("Login.aspx");
                UserManager um = new UserManager();
                Periods = um.GetPeriod();
                var period_id = string.IsNullOrWhiteSpace(Request.QueryString["eva"]) ? Periods[0].EVA_TIME : Request.QueryString["eva"].ToString().Trim();
                var group = Request.QueryString["group"] == null ? 0 : Convert.ToInt32(Request.QueryString["group"]);
                var wgroup = Request.QueryString["w"];
                var query = Request.QueryString["query"];
                var page = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"].ToString());


                ResultMange rm = new ResultMange();
                Groups = um.GetEvagroup();
                WGroups = um.GetWorkgroup();

                Duration = Periods[0].EVA_START.ToShortDateString() + "~" + Periods[0].EVA_END.ToShortDateString();

                Result1 = rm.GetMBOResult(period_id, group, query, wgroup, page);
                int countResult = rm.GetCountMBOResult(period_id, group, query, wgroup);
                CNT_MBO = (countResult + 20 - 1) / 20;

                for (int i = 0; i < Result1.Rows.Count; i++)
                {
                    Result1.Rows[i]["FINAL_GRADE"] = string.IsNullOrWhiteSpace(Result1.Rows[i]["FINAL_GRADE"].ToString()) ? Result1.Rows[i]["GRADE"].ToString() : Result1.Rows[i]["FINAL_GRADE"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["export"]))
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    var template = Server.MapPath("~/Template/MBO_RESULT.xlsx");

                    DataTable dtb = rm.GetMBOResultExport(period_id, group, query, wgroup);

                    using (ExcelPackage package = new ExcelPackage(new FileInfo(template)))
                    {
                        ExcelWorksheet ws = package.Workbook.Worksheets.FirstOrDefault();

                        for (int i = 0; i < dtb.Rows.Count; i++)
                        {
                            var startIndex = i + 5;
                            dtb.Rows[i]["FINAL_GRADE"] = string.IsNullOrWhiteSpace(dtb.Rows[i]["FINAL_GRADE"].ToString()) ? dtb.Rows[i]["GRADE"].ToString() : dtb.Rows[i]["FINAL_GRADE"].ToString();
                            DataRow r = dtb.Rows[i];
                            int statusCode = string.IsNullOrWhiteSpace(r["STATUS"].ToString()) ? 0 : Convert.ToInt32(r["STATUS"].ToString());
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

                            ws.Cells["C" + startIndex].Value = r["NAME"];
                            ws.Cells["D" + startIndex].Value = r["WORKGROUP"];
                            ws.Cells["E" + startIndex].Value = r["ENTER_DATE"] is DBNull ? "" : Convert.ToDateTime(r["ENTER_DATE"]).ToString("yyyy-MM-dd");
                            ws.Cells["F" + startIndex].Value = r["ACTION_PLAN"].ToString().Replace("<br>", Environment.NewLine).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<");
                            ws.Cells["F" + startIndex].Style.WrapText = true;
                            ws.Cells["G" + startIndex].Value = r["TARGET"].ToString().Replace("<br>", Environment.NewLine).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<");
                            ws.Cells["G" + startIndex].Style.WrapText = true;
                            ws.Cells["H" + startIndex].Value = r["RESULT"];
                            ws.Cells["I" + startIndex].Value = r["WEIGHT"];
                            ws.Cells["J" + startIndex].Value = r["MBO_SELF_RATE"];
                            ws.Cells["K" + startIndex].Value = r["MBO_SELF_SCORE"];
                            ws.Cells["L" + startIndex].Value = r["MBO_M1_RATE"];
                            ws.Cells["M" + startIndex].Value = r["MBO_M1_SCORE"];
                            ws.Cells["N" + startIndex].Value = r["MBO_M2_RATE"];
                            ws.Cells["O" + startIndex].Value = r["MBO_M2_SCORE"];
                            ws.Cells["P" + startIndex].Value = r["MBO_FINAL_SCORE"];
                            ws.Cells["Q" + startIndex].Value = r["CAP_M1"];
                            ws.Cells["R" + startIndex].Value = r["CAP_M2"];
                            ws.Cells["S" + startIndex].Value = r["M1_FINAL_SCORE"];
                            ws.Cells["T" + startIndex].Value = r["M1_GRADE"];
                            ws.Cells["U" + startIndex].Value = r["TOTAL_SCORE"];
                            ws.Cells["V" + startIndex].Value = r["GRADE"];
                            ws.Cells["W" + startIndex].Value = r["FINAL_GRADE"];
                            ws.Cells["X" + startIndex].Value = r["REMARK"];
                            ws.Cells["Y" + startIndex].Value = status;

                        }
                        for (int i = 0; i < dtb.Rows.Count; i++)
                        {
                            var count = 0;
                            var startIndex = i + 5;
                            DataRow r = dtb.Rows[i];
                            int statusCode = string.IsNullOrWhiteSpace(r["STATUS"].ToString()) ? 0 : Convert.ToInt32(r["STATUS"].ToString());
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
                                if (r["EMP_ID"].ToString().Equals(dtb.Rows[j]["EMP_ID"].ToString()))
                                {
                                    count++;
                                }
                            }
                            ws.Cells["B" + startIndex + ":B" + (startIndex + count - 1).ToString()].Merge = true;

                            if (r["EMP_ID"].ToString().Contains("quantri"))
                            {
                                ws.Cells["B" + startIndex].Value = r["EMP_ID"].ToString();
                            }
                            else
                            {
                                ws.Cells["B" + startIndex].Value = Convert.ToInt32(r["EMP_ID"].ToString());
                                ws.Cells["B" + startIndex].Style.Numberformat.Format = "#";
                            }
                            ws.Cells["C" + startIndex + ":C" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["C" + startIndex].Value = r["NAME"];
                            ws.Cells["D" + startIndex + ":D" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["D" + startIndex].Value = r["WORKGROUP"];
                            ws.Cells["E" + startIndex + ":E" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["E" + startIndex].Value = r["ENTER_DATE"] is DBNull ? "" : Convert.ToDateTime(r["ENTER_DATE"]).ToString("yyyy-MM-dd");
                            ws.Cells["K" + startIndex + ":K" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["K" + startIndex].Value = r["MBO_SELF_SCORE"];
                            ws.Cells["M" + startIndex + ":M" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["M" + startIndex].Value = r["MBO_M1_SCORE"];
                            ws.Cells["O" + startIndex + ":O" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["O" + startIndex].Value = r["MBO_M2_SCORE"];
                            ws.Cells["P" + startIndex + ":P" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["P" + startIndex].Value = r["MBO_FINAL_SCORE"];
                            ws.Cells["Q" + startIndex + ":Q" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["Q" + startIndex].Value = r["CAP_M1"];
                            ws.Cells["R" + startIndex + ":R" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["R" + startIndex].Value = r["CAP_M2"];
                            ws.Cells["S" + startIndex + ":S" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["S" + startIndex].Value = r["M1_FINAL_SCORE"];
                            ws.Cells["T" + startIndex + ":T" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["T" + startIndex].Value = r["M1_GRADE"];
                            ws.Cells["U" + startIndex + ":U" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["U" + startIndex].Value = r["TOTAL_SCORE"];
                            ws.Cells["V" + startIndex + ":V" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["V" + startIndex].Value = r["GRADE"];
                            ws.Cells["W" + startIndex + ":W" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["W" + startIndex].Value = r["FINAL_GRADE"];
                            ws.Cells["X" + startIndex + ":X" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["X" + startIndex].Value =r["REASON"] + " - " + r["REMARK"];
                            ws.Cells["Y" + startIndex + ":Y" + (startIndex + count - 1).ToString()].Merge = true;
                            ws.Cells["Y" + startIndex].Value = status;


                            i += count - 1;
                        }
                        for (int i = 2; i <= 25; i++)
                        {
                            ws.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            ws.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                            //wrap text in the cells
                            ws.Column(i).Style.WrapText = true;
                        }
                        ws.Cells["F2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells["Q2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells["S2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells["J3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells["L3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells["N3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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