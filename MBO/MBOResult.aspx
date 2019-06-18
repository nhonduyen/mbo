<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MBOResult.aspx.cs" Inherits="MBO.MBOResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-list-alt"></i>&nbsp;HR MBO RESULT &amp; CAPACITY EVALUATION</h2>
    <div class="row">
        <div class="col-sm-12 col-md-12 col-lg-12">
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
                            <td class="tdleft">Workgroup</td>
                            <td>
                                <select id="ddlWork" class="form-control">
                                    <option></option>
                                    <% foreach (var g in WGroups)
                                       {%>
                                    <option value="<%=g.WORKGROUP.Trim() %>"><%=g.WORKGROUP %></option>
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
    </div>
    <div class="row">
        <div class="col-md-8"></div>
        <div class="col-md-4">
            <button type="button" id="btnSearch" class="btn btn-primary"><span class="glyphicon glyphicon-search"></span>Search</button>
            <button type="button" id="btnExport" class="btn btn-primary"><span class="glyphicon glyphicon-export"></span>Export</button>
        </div>
    </div>
    <hr />
    <div style="height: 500px; overflow: auto;">
        <table id="tbMainDefault" class="table table-striped table-bordered table-hover" style="border-collapse: separate; font-size: 11px;">
            <thead style="min-height: 50px">
                <tr class="tbheader">
                    <th rowspan="3" style="vertical-align: middle">
                        <input type="checkbox" id="ckAll" /></th>
                    <th rowspan="3" style="vertical-align: middle; padding-right: 50px;">Picture</th>
                    <th rowspan="3" style="vertical-align: middle">Emp.ID</th>
                    <th rowspan="3" style="vertical-align: middle; white-space: nowrap; padding-right: 50px;">Full Name</th>
                    <th rowspan="3" style="vertical-align: middle">Workgroup</th>
                    <th rowspan="3" style="vertical-align: middle; white-space: nowrap">Enter Date</th>
                    <th colspan="11" style="text-align: center">MBO</th>

                    <th colspan="2" style="text-align: center">Capacity</th>
                    <th colspan="4" style="text-align: center">Eva Result</th>

                    <th rowspan="3" style="vertical-align: middle">Final Grade</th>
                    <th rowspan="3" style="vertical-align: middle">Remark</th>
                    <th rowspan="3" style="vertical-align: middle">Status</th>
                </tr>
                <tr>
                    <th rowspan="2" style="vertical-align: middle; white-space: nowrap; padding-right: 200px;">Action Plan</th>
                    <th rowspan="2" style="vertical-align: middle; white-space: nowrap; padding-right: 100px;">Target</th>
                    <th rowspan="2" style="vertical-align: middle; padding-right: 100px;">Result</th>
                    <th rowspan="2" style="vertical-align: middle">Weight</th>
                    <th colspan="2" style="text-align: center">Self</th>
                    <th colspan="2" style="text-align: center">M1</th>
                    <th colspan="2" style="text-align: center">M2</th>
                    <th rowspan="2" style="vertical-align: middle; border-left-width: 1px">Final Score</th>
                    <th rowspan="2" style="vertical-align: middle">M1</th>
                    <th rowspan="2" style="vertical-align: middle">M2</th>
                    <th rowspan="2" style="vertical-align: middle">M1 Score</th>
                    <th rowspan="2" style="vertical-align: middle">M1 Grade</th>
                    <th rowspan="2" style="vertical-align: middle">M2 Score</th>
                    <th rowspan="2" style="vertical-align: middle">M2 Grade</th>

                </tr>
                <tr>
                    <th>Rate</th>
                    <th>Score</th>
                    <th>Rate</th>
                    <th>Score</th>
                    <th>Rate</th>
                    <th>Score</th>

                </tr>
            </thead>
            <tbody>
                <% for (int i = 0; i < Result1.Rows.Count; i++)
                   {
                       int planCount = 0;
                       for (int j = 0; j < Result1.Rows.Count; j++)
                       {
                           if (Result1.Rows[i]["EMP_ID"].ToString().Equals(Result1.Rows[j]["EMP_ID"].ToString()))
                           {
                               planCount++;
                           }
                       }
                       DateTime ENTER_DATE;
                       bool t = DateTime.TryParse(Result1.Rows[i]["ENTER_DATE"].ToString(), out ENTER_DATE);
                       if (!t) ENTER_DATE = DateTime.MinValue;
                       //int planCount = Convert.ToInt32(Result1.Rows[i]["num"].ToString());
                       if (planCount == 0) planCount = 1;
                       var GRAGE = Result1.Rows[i]["GRADE"].ToString();

                       var isOneApprover = RM.IsOneApprover(Result1.Rows[i]["EMP_ID"].ToString());

                       int statusCode = string.IsNullOrWhiteSpace(Result1.Rows[i]["STATUS"].ToString()) ? 0 : Convert.ToInt32(Result1.Rows[i]["STATUS"].ToString());
                       var status = "Unconfirmed";
                       int group = Convert.ToInt32(Result1.Rows[i]["EVA_GROUP"].ToString());
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
                %>
                <tr class="cover" id="<%=Result1.Rows[i]["RES_ID"].ToString() %>" data-group="<%=Result1.Rows[i]["EVA_GROUP"].ToString() %>">
                    <td class="rp" rowspan="<%=planCount %>">
                        <input type="checkbox" class="ckb" /></td>
                    <td class="rp" rowspan="<%=planCount %>">
                        <img class='img-rounded' alt="X" height='100' width='100' src="<%=Result1.Rows[i]["PICTURE"].ToString() %>" /></td>
                    <td class="rp" rowspan="<%=planCount %>"><%=Result1.Rows[i]["EMP_ID"].ToString() %></td>
                    <td class="rp" rowspan="<%=planCount %>"><%=Result1.Rows[i]["NAME"].ToString() %></td>
                    <td class="rp" rowspan="<%=planCount %>"><%=Result1.Rows[i]["WORKGROUP"].ToString() %></td>
                    <td class="rp" rowspan="<%=planCount %>"><%=ENTER_DATE.ToShortDateString() %></td>
                    <td class="action"><%=Result1.Rows[i]["ACTION_PLAN"].ToString() %></td>
                    <td class="pid" data-id="<%=Result1.Rows[i]["PID"].ToString() %>"><%=Result1.Rows[i]["TARGET"].ToString() %></td>

                    <td class="result"><%=Result1.Rows[i]["RESULT"].ToString() %></td>
                    <td class="weight"><%=Result1.Rows[i]["WEIGHT"].ToString() %></td>
                    <td class="self_rate"><%=Result1.Rows[i]["MBO_SELF_RATE"].ToString() %></td>
                    <td class="self_score" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["MBO_SELF_SCORE"].ToString() %></td>
                    <td class="m1rate"><%=(isOneApprover) ? "" : Result1.Rows[i]["MBO_M1_RATE"].ToString() %></td>
                    <td class="m1score" rowspan="<%=planCount %>" style="font-weight: bold"><%=(isOneApprover) ? "" : Result1.Rows[i]["MBO_M1_SCORE"].ToString() %></td>

                    <td class="m2rate"><%=Result1.Rows[i]["MBO_M2_RATE"].ToString() %></td>
                    <td class="m2score" rowspan="<%=planCount %>" style="border-left-width: 1px; font-weight: bold;"><%=Result1.Rows[i]["MBO_M2_SCORE"].ToString() %></td>
                    <td class="mbo_final" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["MBO_FINAL_SCORE"].ToString() %></td>
                    <td class="cap1" rowspan="<%=planCount %>" style="font-weight: bold"><%=(isOneApprover) ? "" : Result1.Rows[i]["CAP_M1"].ToString() %></td>
                    <td class="cap2" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["CAP_M2"].ToString() %></td>
                    <td class="m1_score" rowspan="<%=planCount %>" style="font-weight: bold"><%=(isOneApprover) ? "" :Result1.Rows[i]["M1_FINAL_SCORE"].ToString() %></td>
                    <td class="m1_grade" rowspan="<%=planCount %>"><%=(isOneApprover) ? "" :Result1.Rows[i]["M1_GRADE"].ToString() %></td>
                    <td class="total" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["TOTAL_SCORE"].ToString() %></td>
                    <td class="grade" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["GRADE"].ToString() %></td>

                    <td class="rp" rowspan="<%=planCount %>"><%=Result1.Rows[i]["FINAL_GRADE"].ToString() %></td>
                    <td class="rp" rowspan="<%=planCount %>"><%=Result1.Rows[i]["REASON"].ToString()+" - "+Result1.Rows[i]["REMARK"].ToString() %></td>
                    <td class="rp" data-status="<%=statusCode %>" rowspan="<%=planCount %>"><%=status %></td>
                </tr>
                <% var step = 0;
                   for (int j = 1; j < planCount; j++)
                   {
                       step = i + j;%>
                <tr class="tr-content">
                    <td class="action"><%=Result1.Rows[step]["ACTION_PLAN"].ToString() %></td>
                    <td class="pid" data-id="<%=Result1.Rows[step]["PID"].ToString() %>"><%=Result1.Rows[step]["TARGET"].ToString() %></td>

                    <td class="result"><%=Result1.Rows[step]["RESULT"].ToString() %></td>
                    <td class="weight"><%=Result1.Rows[step]["WEIGHT"].ToString() %></td>
                    <td class="self_rate"><%=Result1.Rows[step]["MBO_SELF_RATE"].ToString() %></td>
                    <td class="m1rate"><%=(isOneApprover) ? "" : Result1.Rows[step]["MBO_M1_RATE"].ToString() %></td>
                    <td class="m2rate"><%=Result1.Rows[step]["MBO_M2_RATE"].ToString() %></td>
                </tr>

                <%} if (planCount > 0) i += planCount - 1;
                   }%>
            </tbody>
        </table>
    </div>
    <%
        int page = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["PAGE"].ToString());
        var period_id = Request.QueryString["eva"];
        var group1 = Request.QueryString["group"] == null ? 0 : Convert.ToInt32(Request.QueryString["group"]);
        var wgroup = Request.QueryString["w"];
        var query = Request.QueryString["query"];
        var prevDisable = page <= 1 ? "disabled" : "";
        var nextDisable = page >= CNT_MBO ? "disabled" : "";
        var prev = "MBOResult.aspx?eva=" + period_id + "&group=" + group1 + "&w=" + wgroup + "&query=" + query + "&page=" + (page - 1);
        var next = "MBOResult.aspx?eva=" + period_id + "&group=" + group1 + "&w=" + wgroup + "&query=" + query + "&page=" + (page + 1);
    %>
    <%-- <ul class="pagination" style="margin: 5px 0px;">
        <% 
            for (int i = 1; i <= CNT_MBO; i++)
            {
                var liclass = page == i ? "active" : "";
                var href = "MBOResult.aspx?eva=" + period_id + "&group=" + group1 + "&w=" + wgroup + "&query=" + query + "&page=" + i;
        %>
        <li class="<%=liclass %>"><a href="<%=href %>"><%=i %></a></li>
        <%} %>
    </ul>--%>
    <span>Page <%=page %> of <%=CNT_MBO %></span>
    <ul class="pager">
        <li class="<%=prevDisable %>"><a href="<%=prev %>">Previous</a></li>
        <li class="<%=nextDisable %>"><a href="<%=next %>">Next</a></li>
    </ul>

    <div class="modal fade" id="mdElement" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>Evaluable Element Table</h4>
                </div>
                <div class="modal-body">
                    <button type="button" data-id="" style="float: right; padding-bottom: 5px;" id="btnSaveScore" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span>Save</button>
                    <table id="tbElement" class="table table-striped table-bordered table-hover" data-role="">
                        <thead>
                            <tr class="tbheader">
                                <th>Element</th>
                                <th>Factor</th>
                                <th>Weight</th>
                                <th>M1-Score</th>
                                <th>M2-Score</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2"><span>Total Score</span></td>
                                <td><span id="sum"></span></td>
                                <td><span id="sumM1"></span></td>
                                <td><span id="sumM2"></span></td>

                            </tr>

                        </tfoot>
                    </table>

                </div>

            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var eva = getUrlParameter('eva');
            var group = getUrlParameter('group');
            var query = getUrlParameter('query');
            var w = getUrlParameter('w');
            if (eva) {
                $('#ddlPeriod').val($.trim(eva));
            }
            if (group) {
                $('#ddlGroup').val($.trim(group));
            }
            if (query) {
                $('#txtQuery').val($.trim(query));
            }
            if (w) {
                $('#ddlWork').val($.trim(w));
            }
            $('#rs').addClass('active');
            $('#tbMainDefault').tableHeadFixer({

                left: 5,
                'z-index': 50
            });
            $('#btnSearch').click(function () {
                var group = $('#ddlGroup').val();
                var wgroup = $('#ddlWork').val();
                var time = $('#ddlPeriod').val();
                var query = $('#txtQuery').val();
                var link = "MBOResult.aspx?eva=" + time + "&group=" + group + "&w" + wgroup;
                if (query)
                    link += "&query=" + query;
                if (wgroup)
                    link += "&w=" + wgroup;

                window.location.replace(link);
                return false;
            });
            $('.cap1, .cap2').click(function () {
                var ROLE = $(this).parent().attr('data-role');
                var STATUS = $(this).siblings(":last").attr('data-status');
                var RESULT_ID = $(this).parent().attr('id');
                var GROUP_ID = $(this).parent().attr('data-group');
                var APPROVER = $('#btnConfirm').attr('data-empid');

                if (ROLE == 0) return false;
                if (parseInt(GROUP_ID) == 5) GROUP_ID = 3;
                $('#btnSaveScore').attr('data-id', RESULT_ID);
                $('#tbElement').attr('data-role', ROLE);

                $.ajax({
                    url: 'Services/ResultService.asmx/GetElementTable1',
                    data: JSON.stringify(
                       { RESULT_ID: RESULT_ID, GROUP_ID: GROUP_ID, APPROVER: APPROVER }
                    ),
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    crossBrowser: true,
                    success: function (data, status) {
                        var rs = data.d;
                        if (rs) {
                            var sumM1 = 0;
                            var sumM2 = 0;
                            var sum = 0;
                            $('#tbElement tr').not(':last').not(':first').remove();
                            for (var i = 0; i < rs.length; i++) {
                                sumM1 += rs[i].M1_Score;
                                sumM2 += rs[i].M2_Score;
                                sum += rs[i].Weight;
                                $('#tbElement tbody').append('<tr id="' + rs[i].Factor_ID + '"><td>' + rs[i].Element + '</td><td>' + rs[i].Factor + '</td><td class="weight">' + rs[i].Weight + '</td><td class="m1edit">' + rs[i].M1_Score + '</td><td class="m2edit">' + rs[i].M2_Score + '</td></tr>');
                            }
                            $('#sum').text(sum);
                            $('#sumM1').text(sumM1);
                            $('#sumM2').text(sumM2);
                        }
                        return false;
                    },
                    error: function (xhr, status, error) {
                        alert("Error! " + xhr.status);
                    },
                });

                $('#mdElement').modal();
                return false;
            });
            $('#btnExport').click(function () {
                var group = $('#ddlGroup').val();
                var wgroup = $('#ddlWork').val();
                var time = $('#ddlPeriod').val();
                var query = $('#txtQuery').val();
                var link = "MBOResult.aspx?export=1&eva=" + time + "&group=" + group + "&w" + wgroup;
                if (query)
                    link += "&query=" + query;
                if (wgroup)
                    link += "&w=" + wgroup;

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
