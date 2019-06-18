using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI;

namespace MBO
{
    public static class Ultilities
    {
        public static void Export(DataTable dtb, string fileTitle = "Export")
        {
            try
            {
                string style = @"<style> .text { mso-number-format:\@; } </style>";
                GridView gv = new GridView();
                gv.DataSource = dtb;
                gv.DataBind();

                
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    for (int j = 0; j < gv.Columns.Count; j++)
                    {
                       
                        gv.Rows[i].Cells[j].Attributes.Add("class", "text");
                     
                    }
                }

                string fname = fileTitle + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fname);
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
                HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
           
                
               
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                gv.RenderControl(htw);
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}