using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBO
{
    public class MBO_Result
    {
        public Employee EMPLOYEE { get; set; }
        public Period PERIOD { get; set; }
        public List<MBO_Plan> MBO_PLAN {get;set;}
        public int ID { get; set; }
        public string RESULT { get; set; }
        public double MBO_SELF_SCORE { get; set; }
        public double MBO_M1_SCORE { get; set; }
        public double MBO_M2_SCORE { get; set; }
        public double MBO_FINAL_SCORE { get; set; }
        public double CAP_M1 { get; set; }
        public double CAP_M2 { get; set; }
        public double CAP_FINAL_SCORE { get; set; }
        public double TOTAL_SCORE { get; set; }
        public string GRADE { get; set; }
        public int STATUS { get; set; }
        public int PLAN_STATUS { get; set; }
        public int M1_FINAL_SCORE { get; set; }
        public string M1_GRADE { get; set; }
        public string FINAL_GRADE { get; set; }
        public string REASON { get; set; }
        public string REMARK { get; set; }
    }
}