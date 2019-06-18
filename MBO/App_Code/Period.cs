using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBO
{
    public class Period
    {
        public string EVA_TIME { get; set; }
        public DateTime EVA_START { get; set; }
        public DateTime EVA_END { get; set; }
        public int STATUS { get; set; }
        public int SET_MBO { get; set; }
    }
}