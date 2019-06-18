using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MBO
{
    public partial class EP : System.Web.UI.Page
    {
        protected List<Period> Periods = new List<Period>();
        protected List<WorkGroup> workgroups = new List<WorkGroup>();
        protected List<Eva_Group> GROUPS = new List<Eva_Group>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserManager um = new UserManager();
                Periods = um.GetPeriod();
                workgroups = um.GetWorkgroup();
                GROUPS = um.GetEvagroup();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable tbEmps = mgrDataSQL.ExecuteStoreReader(StoredProcedure.EXPORT_USERS);
           
            Ultilities.Export(tbEmps);
        }

       
    }
}