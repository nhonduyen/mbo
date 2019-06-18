using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBO
{
    public class Employee
    {
        public string EMP_ID { get; set; }
        public string NAME { get; set; }
        public string WORKGROUP { get; set; }
        public DateTime ENTER_DATE { get; set; }
        public string PASSWORD { get; set; }
        public int ROLE { get; set; }

        public Employee()
        {
        }
    }
}