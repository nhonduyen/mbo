using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MBO.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bundle")
                .Include("~/Content/bt/bootstrap.css", new CssRewriteUrlTransform()) // rewrite relative url to absolute url (show glyphicon)
                .Include("~/Content/fontawesome/font-awesome.css", new CssRewriteUrlTransform())
                .Include(
                "~/Content/bt/dataTables.bootstrap.css",
                "~/Content/bt/datepicker.css",
              "~/Content/site.css"
              ));
            bundles.Add(new ScriptBundle("~/Scripts/bundle").Include(
                "~/Scripts/Include/jquery-1.11.3.min.js", "~/Scripts/Include/bootstrap.min.js",
                "~/Scripts/jquery.xdomainrequest.min.js", "~/Scripts/Include/ie10-viewport-bug-workaround.js",
                "~/Scripts/bootbox.min.js", "~/Scripts/Include/jquery.backstretch.min.js",
                "~/Scripts/tableHeadFixer.js"
                ));
            BundleTable.EnableOptimizations = true;
        }
    }
}