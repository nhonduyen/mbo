<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="MBO.Plan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-list-alt"></i>&nbsp;PLAN RESULT</h2>
    <div class="row">
        <div class="col-md-8">
            <div class="table-responsive">
                <table class="table table-bordered">
                    <tbody>
                        <tr>
                            <td class="tdleft">Eva. time</td>
                            <td>
                                <select id="ddlPeriod" class="form-control" style="height: 30px;">
                                    <% foreach (var period in Periods)
                                       {%>
                                    <option value="<%=period.EVA_TIME.Trim() %>"><%=period.EVA_TIME.Trim() %></option>
                                    <% } %>
                                </select>
                            </td>

                            <td class="tdleft">Eva Group</td>
                            <td>
                                <select id="ddlGroup" class="form-control" style="height: 30px;">
                                    <option value="0"></option>
                                    <% foreach (var g in Groups)
                                       {%>
                                    <option value="<%=g.ID %>"><%=g.GROUP_NAME %></option>
                                    <%} %>
                                </select>
                            </td>

                            <td class="tdleft">EMP_ID/NAME</td>
                            <td>
                                <input type="text" id="txtQuery" class="form-control" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-inline">
                <button type="button" id="btnSearch" class="btn btn-primary"><span class="glyphicon glyphicon-search"></span>Search</button>
                <button type="button" id="btnExport" class="btn btn-primary"><span class="glyphicon glyphicon-export"></span>Export</button>
            </div>
        </div>
    </div>

    <hr />
    <div class="table-responsive" style="height: 500px; overflow: auto; font-size: 11px;">
        <table id="tbMainDefault" class="table table-striped table-bordered table-hover" style="border-collapse: separate;">
            <thead>
                <tr class="tbheader">

                    <th>Picture</th>
                    <th>Emp.ID</th>
                    <th>Full Name</th>
                    <th>Workgroup</th>
                    <th>Enter Date</th>
                    <th>Content</th>
                    <th>Action Plan</th>
                    <th>Target</th>
                    <th>Weight</th>
                    <th>Level</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                <% for (int i = 0; i < Result.Rows.Count; i++)
                   {

                       DateTime ENTER_DATE;
                       bool t = DateTime.TryParse(Result.Rows[i]["ENTER_DATE"].ToString(), out ENTER_DATE);
                       if (!t) ENTER_DATE = DateTime.MinValue;
                       int planCount = 0;

                       int statusCode = string.IsNullOrEmpty(Result.Rows[i]["PLAN_STATUS"].ToString()) ? 0 : Convert.ToInt32(Result.Rows[i]["PLAN_STATUS"].ToString());
                       var status = string.Empty;
                       int group = Convert.ToInt32(Result.Rows[i]["EVA_GROUP"].ToString());
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
                       for (int j = 0; j < Result.Rows.Count; j++)
                       {
                           if (Result.Rows[i]["EMP_ID"].ToString().Equals(Result.Rows[j]["EMP_ID"].ToString()))
                           {
                               planCount++;
                           }
                       }
                %>
                <tr class="cover" data-id="<%=Result.Rows[i]["RES_ID"].ToString()%>">

                    <td class="rp" rowspan="<%=planCount%>">
                        <img class='img-rounded' height='100' width='100' alt="X" src="<%=Result.Rows[i]["PICTURE"].ToString()%>" /></td>
                    <td class="rp" rowspan="<%=planCount%>"><%=Result.Rows[i]["EMP_ID"].ToString()%></td>
                    <td class="rp" rowspan="<%=planCount%>"><%=Result.Rows[i]["NAME"].ToString()%></td>
                    <td class="rp" rowspan="<%=planCount%>"><%=Result.Rows[i]["WORKGROUP"].ToString()%></td>
                    <td class="rp" rowspan="<%=planCount%>"><%=ENTER_DATE.ToShortDateString()%></td>
                    <td class="ap" data-click="0" data-id="<%=Result.Rows[i]["PID"].ToString()%>"><%=Result.Rows[i]["CONT"].ToString()%></td>
                    <td class="ap" data-click="0"><%=Result.Rows[i]["ACTION_PLAN"].ToString()%></td>
                    <td class="ap" data-click="0"><%=Result.Rows[i]["TARGET"].ToString()%></td>
                    <td class="ap" data-click="0"><%=Result.Rows[i]["WEIGHT"].ToString()%></td>
                    <td class="ap" data-click="0"><%=Result.Rows[i]["LVL"].ToString()%></td>
                    <td class="rp" data-status="<%=statusCode%>" rowspan="<%=planCount%>" style="border-left-width: 1px"><%=status%></td>
                </tr>
                <% for (int j = 1; j < planCount; j++)
                   {
                        
                %>
                <tr class="tr-content">
                    <td data-id="<%=Result.Rows[j+i]["PID"].ToString()%>"><%=Result.Rows[j+i]["CONT"].ToString()%></td>
                    <td><%=Result.Rows[j+i]["ACTION_PLAN"].ToString()%></td>
                    <td><%=Result.Rows[j+i]["TARGET"].ToString()%></td>
                    <td><%=Result.Rows[j+i]["WEIGHT"].ToString()%></td>
                    <td><%=Result.Rows[j+i]["LVL"].ToString()%></td>
                </tr>
                <%}
                   if (planCount > 1) i += planCount - 1;

                   } %>
            </tbody>
        </table>
    </div>
    <%
        int page = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["PAGE"].ToString());
        var period_id = Request.QueryString["eva"];
        var group1 = Request.QueryString["group"] == null ? 0 : Convert.ToInt32(Request.QueryString["group"]);

        var query = Request.QueryString["query"];
        var prevDisable = page <= 1 ? "disabled" : "";
        var nextDisable = page >= CNT_MBO ? "disabled" : "";
        var prev = "Plan.aspx?eva=" + period_id + "&group=" + group1 + "&query=" + query + "&page=" + (page - 1);
        var next = "Plan.aspx?eva=" + period_id + "&group=" + group1 + "&query=" + query + "&page=" + (page + 1);
         %>
    <span>Page <%=page %> of <%=CNT_MBO %></span>
    <ul class="pager">
        <li class="<%=prevDisable %>"><a href="<%=prev %>">Previous</a></li>
        <li class="<%=nextDisable %>"><a href="<%=next %>">Next</a></li>
    </ul>
    <script>
        $(document).ready(function () {
            var eva = getUrlParameter('eva');
            var group = getUrlParameter('group');
            var query = getUrlParameter('query');

            if (eva) {
                $('#ddlPeriod').val($.trim(eva));
            }
            if (group) {
                $('#ddlGroup').val($.trim(group));
            }
            if (query) {
                $('#txtQuery').val($.trim(query));
            }

            $('#plan').addClass('active');
            $('#tbMainDefault').tableHeadFixer({

                left: 3,
                'z-index': 50
            });
            $('#btnSearch').click(function () {
                var group = $('#ddlGroup').val();
                var time = $('#ddlPeriod').val();
                var query = $('#txtQuery').val();
                if (query)
                    window.location.replace("Plan.aspx?eva=" + time + "&group=" + group + "&query=" + query);
                else
                    window.location.replace("Plan.aspx?eva=" + time + "&group=" + group);
                return false;
            });
            $('#btnExport').click(function () {
                var group = $('#ddlGroup').val();
                var time = $('#ddlPeriod').val();
                var query = $('#txtQuery').val();
                var link = "Plan.aspx?export=1&eva=" + time + "&group=" + group + "&query=" + query;

                window.location.replace(link);
                return false;
            });

            function getUrlParameter(sParam) {
                var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                    sURLVariables = sPageURL.split('&'),
                    sParameterName,
                    i;

                for (i = 0; i < sURLVariables.length; i++) {
                    sParameterName = sURLVariables[i].split('=');

                    if (sParameterName[0] === sParam) {
                        return sParameterName[1] === undefined ? true : sParameterName[1];
                    }
                }
            };
        });
    </script>
</asp:Content>
