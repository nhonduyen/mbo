using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBO
{
    public class MBO_Plan
    {
        public int ID { get; set; }
        public string CONT { get; set; }
        public string ACTION_PLAN { get; set; }
        public string TARGET { get; set; }
        public int WEIGHT { get; set; }
        public string LVL { get; set; }
        public string ACTION { get; set; }
        public string RESULT { get; set; }
        public int RESULT_ID { get; set; }
        public int MBO_SELF_RATE { get; set; }
        public int MBO_M1_RATE { get; set; }

        public int MBO_M2_RATE { get; set; }
    }
}