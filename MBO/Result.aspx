<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="MBO.Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Result.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #tbMainDefault {
            border: 1px solid black !important;
        }

            #tbMainDefault > thead > tr > th {
                padding: 0px;
                text-align: center;
            }

        .bd-r {
            border-right: 1px solid black !important;
        }

        tr.bd-tb th {
            border-bottom: 1px solid black !important;
        }
    </style>
    <h4 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-list-alt"></i>&nbsp;MBO RESULT &amp; CAPACITY EVALUATION</h4>
    <div class="row">
        <div class="col-sm-8 col-md-8 col-lg-8">
            <div class="table-responsive">
                <table class="table table-bordered">
                    <tbody>
                        <tr>
                            <td class="tdleft">Eva. time</td>
                            <td>
                                <select id="ddlPeriod" class="form-control" data-status="<%=STATUS %>" style="height: 30px;">
                                    <% foreach (var period in Periods)
                                       {%>
                                    <option value="<%=period.EVA_TIME.Trim() %>"><%=period.EVA_TIME.Trim() %></option>
                                    <% } %>
                                </select>
                            </td>
                            <td class="tdleft">Emp.type</td>
                            <td>
                                <select id="selMbo" class="form-control" style="height: 30px;">
                                    <option value="register">Self Eva</option>
                                    <option value="approve">MBO & capacity Eva</option>
                                </select>
                            </td>
                            <td class="tdleft">Eva Group</td>
                            <td>
                                <select id="ddlEva" class="form-control" style="height: 30px;">
                                    <option value=""></option>
                                    <% foreach (var group in Groups)
                                       {%>
                                    <option value="<%=group.ID %>"><%=group.GROUP_NAME.Trim() %></option>
                                    <%} %>
                                </select>
                            </td>
                        </tr>


                    </tbody>
                </table>
            </div>
        </div>
        <%--   <div class="col-md-5">
                <dl  style="font-size:11px;">
                    <dt><u>Remark</u></dt>
                    <dd><b>- Eva rate:</b> 10% <b>S</b>, 15% <b>A</b>, 75% <b>B, C, D</b></dd>
                    <dt>- Eva result</dt>
                    <dd>+ Engineer/Staff, Leader & upper: 60% (MBO result) + 40% (Capacity evaluation)</dd>
                    <dd>+ Shift Leader, Supervisor & Lower: 100% (Capacity evaluation)</dd>
                    <dt><b>- Eva grade</b></dt>
                    <dd>+ Engineer/Staff, Leader & upper: <b>D</b> (0~64),<b>C</b> (65~74),<b>B</b> (75~84),<b>A</b> (85~89),<b>S</b> (90~100)</dd>
                    <dd>+ Shift Leader, Supervisor & Lower: <b>D</b> (0~59),<b>C</b> (60~69),<b>B</b> (70~79),<b>A</b> (80~89),<b>S</b> (90~100)</dd>
                </dl>
            </div>--%>
        <div class="col-md-4">
            <% if (Request.QueryString["action"] != null && SUM.Rows.Count > 0)
               { %>
            <table id="tbSum" class="table table-bordered" style="margin-bottom: 5px; font-size: 10px;">
                <thead>
                    <tr>
                        <th colspan="2"></th>
                        <th style="white-space: nowrap; font-size: 10px;">Num Emp</th>
                        <th style="white-space: nowrap; font-size: 10px;">S 10%</th>
                        <th style="white-space: nowrap; font-size: 10px;">A 15%</th>
                        <th style="white-space: nowrap; font-size: 10px;">B,C,D 75%</th>
                    </tr>
                </thead>
                <tbody>
                    <% 
                   var group = Request.QueryString["group"];
                   for (int i = 0; i < SUM.Rows.Count; i++)
                   {
                       string style = "";
                       if (group != null && SUM.Rows[i]["EVA_GROUP"].ToString().Trim() != group.ToString().Trim())
                       {
                           style = "style='display:none;'";
                       }
                       int s = 0;
                       int a = 0;
                       int b = 0;
                       string errorA = string.Empty;
                       string errorS = string.Empty;
                       string errorB = string.Empty;
                       var sumEva = Convert.ToInt32(SUM.Rows[i]["EVA_GROUP"].ToString());
                       for (int j = 0; j < Result1.Rows.Count; j++)
                       {
                           var num = 0;
                           for (int k = 0; k < Result1.Rows.Count; k++)
                           {
                               if (Result1.Rows[k]["EMP_ID"].ToString().Equals(Result1.Rows[j]["EMP_ID"].ToString()))
                               {
                                   num++;
                               }
                           }
                           var resEva = Convert.ToInt32(Result1.Rows[j]["EVA_GROUP"].ToString());
                           if (sumEva == resEva)
                           {
                               //num = Convert.ToInt32(Result1.Rows[j]["NUM"].ToString());
                               var grade = string.IsNullOrWhiteSpace(Result1.Rows[j]["FINAL_GRADE"].ToString()) ? Result1.Rows[j]["GRADE"].ToString() : Result1.Rows[j]["FINAL_GRADE"].ToString();
                               if (grade == "S") s++;
                               if (grade == "A") a++;
                               if (grade != "A" && grade != "S" && grade != "") b++;
                               if (num > 0)
                                   j += num - 1;
                           }
                       }
                       int sumA = Convert.ToInt32(SUM.Rows[i]["A"].ToString());
                       int sumS = Convert.ToInt32(SUM.Rows[i]["S"].ToString());
                       int sumB = Convert.ToInt32(SUM.Rows[i]["BCD"].ToString());
                       if (a != sumA)
                           errorA = " style='background-color:red'";
                       if (s != sumS)
                           errorS = " style='background-color:red'";
                       if (b != sumB)
                           errorB = " style='background-color:red'";
                    %>
                    <tr <%=style %>>
                        <td rowspan="2">
                            <%=SUM.Rows[i]["GROUP_NAME"].ToString() %>
                        </td>
                        <td style="white-space: nowrap;">Eva. Standard
                        </td>
                        <td>
                            <%=SUM.Rows[i]["NUM_EMPS"].ToString() %>
                        </td>
                        <td>
                            <%=SUM.Rows[i]["S"].ToString() %>
                        </td>
                        <td>
                            <%=SUM.Rows[i]["A"].ToString() %>
                        </td>
                        <td>
                            <%=SUM.Rows[i]["BCD"].ToString() %>
                        </td>
                    </tr>

                    <tr <%=style %> id="r<%=SUM.Rows[i]["EVA_GROUP"].ToString() %>">
                        <td style="white-space: nowrap;">Eva. Result
                        </td>
                        <td>
                            <%=SUM.Rows[i]["COUNT_RES"].ToString() %>
                        </td>
                        <td class="s" <%=errorS %>>
                            <%=s %>
                        </td>
                        <td class="a" <%=errorA %>>
                            <%=a %>
                        </td>
                        <td class="b" <%=errorB %>>
                            <%=b %>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
        <%} %>
    </div>
    <div class="row">
        <div class="col-md-7">
            <div id="alert" class="alert alert-success alert-dismissable" style="display: none">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>

            </div>
        </div>
        <div class="col-md-5">
            <div class="form-inline">
                <button type="button" id="btnSearch" class="btn btn-primary"><span class="glyphicon glyphicon-search"></span>Search</button>
                <button type="button" id="btnSaveResult" class="btn btn-success"><span class="glyphicon glyphicon-floppy-saved"></span>Save</button>
                <button type="button" id="btnReview" class="btn btn-info"><span class="glyphicon glyphicon-eye-open"></span>Result Review</button>
                <button type="button" data-empid="<%=Session["EMP_ID"].ToString()%>" id="btnConfirm" class="btn btn-info"><span class="glyphicon glyphicon-ok"></span>Confirm</button>
                <button type="button" id="btnUnconfirm" class="btn btn-warning"><span class="glyphicon glyphicon-remove"></span>Unconfirm</button>

            </div>
        </div>
    </div>

    <div style="height: 500px; overflow: auto;">
        <table id="tbMainDefault" class="table table-bordered" style="border-collapse: separate;">
            <thead>
                <tr class="tbheader bd-tb">
                    <th rowspan="3" style="vertical-align: middle; width: 5px; padding: 5px 5px 8px 5px;">
                        <input type="checkbox" id="ckAll" /></th>
                    <th rowspan="3" style="vertical-align: middle;">Picture</th>

                    <th rowspan="3" style="vertical-align: middle;">Name</th>
                    <th id="group_header" rowspan="3" class="bd-r" style="vertical-align: middle">Group</th>

                    <th colspan="10" class="bd-r">MBO</th>

                    <th colspan="2" class="bd-r">Capacity</th>
                    <th colspan="4" class="bd-r">Eva Result</th>

                    <%--<th rowspan="3" style="vertical-align: middle">Grade</th>--%>
                    <th rowspan="3" style="vertical-align: middle">Final Grade</th>
                    <th rowspan="3" style="vertical-align: middle">Remark</th>
                    <th rowspan="3" style="vertical-align: middle">Status</th>
                </tr>
                <tr class="bd-tb">
                    <th rowspan="2" style="vertical-align: middle; white-space: nowrap; min-width: 120px;">Content</th>
                    <%--<th rowspan="2" style="vertical-align: middle; white-space: nowrap; padding-right: 200px;">Action Plan</th>--%>
                    <th rowspan="2" style="vertical-align: middle; white-space: nowrap; min-width: 120px;">Target</th>
                    <th rowspan="2" style="vertical-align: middle; min-width: 120px;">Result</th>
                    <th rowspan="2" style="min-width: 50px;" class="bd-r">Wt</th>
                    <th colspan="2">Self</th>
                    <th colspan="2">M1</th>
                    <th colspan="2" class="bd-r">M2</th>
                    <%--<th rowspan="2" style="vertical-align: middle; border-left-width: 1px">Final Score</th>--%>
                    <th rowspan="2" style="border-left-width: 1px; min-width: 50px;">M1</th>
                    <th rowspan="2" style="min-width: 50px;" class="bd-r">M2</th>
                    <th rowspan="2" style="min-width: 50px;">M1<br />
                        Score</th>
                    <th rowspan="2" style="min-width: 50px;">M1<br />
                        Grade</th>
                    <th rowspan="2" style="min-width: 50px;">M2<br />
                        Score</th>
                    <%--<th rowspan="2" style="vertical-align: middle">M2 Grade</th>--%>
                    <th rowspan="2" class="bd-r" style="min-width: 50px;">M2<br />
                        Grade</th>
                </tr>
                <tr class="bd-tb">
                    <th style="min-width: 50px;">Rate</th>
                    <th style="min-width: 50px;">Score</th>
                    <th style="min-width: 50px;">Rate</th>
                    <th style="min-width: 50px;">Score</th>
                    <th style="min-width: 50px;">Rate</th>
                    <th style="min-width: 50px;" class="bd-r">Score</th>

                </tr>
            </thead>
            <tbody>
                <% 
                    int seq = 0;
                    for (int i = 0; i < Result1.Rows.Count; i++)
                    {
                        int planCount =0;

                        var GRAGE = Result1.Rows[i]["GRADE"].ToString();
                        var role = Result1.Rows[i]["ROLE"].ToString();
                        var final_grade = string.IsNullOrWhiteSpace(Result1.Rows[i]["FINAL_GRADE"].ToString()) ? GRAGE : Result1.Rows[i]["FINAL_GRADE"].ToString();
                        if (string.IsNullOrEmpty(role) || role != "2")
                        {
                            final_grade = string.Empty;
                        }
                        
                        for (int j = 0; j < Result1.Rows.Count; j++)
                        {
                            if (Result1.Rows[i]["EMP_ID"].ToString().Equals(Result1.Rows[j]["EMP_ID"].ToString()))
                            {
                                planCount++;
                            }
                        }
                        string active = (seq % 2 == 0) ? "info" : string.Empty;
                        seq++;
                        int statusCode = string.IsNullOrWhiteSpace(Result1.Rows[i]["STATUS"].ToString()) ? 0 : Convert.ToInt32(Result1.Rows[i]["STATUS"].ToString());
                        var status = "Un confirm";
                        switch (statusCode)
                        {
                            case 0:
                                status = "Un confirm";
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
                                status = "Un confirm";
                                break;
                        }
                          
                %>
                <tr class="cover <%=active %>" id="<%=Result1.Rows[i]["RES_ID"].ToString() %>" data-group="<%=Result1.Rows[i]["EVA_GROUP"].ToString() %>" data-role="<%=Result1.Rows[i]["ROLE"].ToString() %>" data-emid="<%=Result1.Rows[i]["EMP_ID"].ToString() %>" style="font-size: 11px;">
                    <td class="rp" rowspan="<%=planCount %>">
                        <input type="checkbox" class="ckb" /></td>
                    <td class="rp" rowspan="<%=planCount %>">
                        <img class='img-rounded' alt="X" height='100' width='70' src="<%=Result1.Rows[i]["PICTURE"].ToString() %>" /></td>

                    <td class="rp" rowspan="<%=planCount %>"><%=Result1.Rows[i]["NAME"].ToString() %></td>
                    <td class="rp bd-r" rowspan="<%=planCount %>"><%=Result1.Rows[i]["WORKGROUP"].ToString() %></td>
                    <td class="cont" data-id="<%=Result1.Rows[i]["PID"].ToString() %>"><%=Result1.Rows[i]["CONT"].ToString() %></td>
                    <%--<td class="action"><%=Result1.Rows[i]["ACTION_PLAN"].ToString() %></td>--%>
                    <td class="pid" data-id="<%=Result1.Rows[i]["PID"].ToString() %>"><%=Result1.Rows[i]["TARGET"].ToString() %></td>

                    <td class="result"><%=Result1.Rows[i]["RESULT"].ToString() %></td>
                    <td class="weight bd-r"><%=Result1.Rows[i]["WEIGHT"].ToString() %></td>
                    <td class="self_rate"><%=Result1.Rows[i]["MBO_SELF_RATE"].ToString() %></td>
                    <td class="self_score" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["MBO_SELF_SCORE"].ToString() %></td>
                    <td class="m1rate"><%=Result1.Rows[i]["MBO_M1_RATE"].ToString() %></td>
                    <td class="m1score" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["MBO_M1_SCORE"].ToString() %></td>

                    <td class="m2rate"><%=Result1.Rows[i]["MBO_M2_RATE"].ToString() %></td>
                    <td class="m2score bd-r" rowspan="<%=planCount %>" style="border-left-width: 1px; font-weight: bold;"><%=Result1.Rows[i]["MBO_M2_SCORE"].ToString() %></td>
                    <%--<td class="mbo_final" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["MBO_FINAL_SCORE"].ToString() %></td>--%>
                    <td class="cap1" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["CAP_M1"].ToString() %></td>
                    <td class="cap2 bd-r" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["CAP_M2"].ToString() %></td>
                    <td class="m1_score" data-grade="<%=Result1.Rows[i]["M1_GRADE"].ToString().Trim() %>" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["M1_FINAL_SCORE"].ToString() %></td>
                    <td class="m1_grade" rowspan="<%=planCount %>"><%=Result1.Rows[i]["M1_GRADE"].ToString() %></td>
                    <td class="total" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["TOTAL_SCORE"].ToString() %></td>
                    <%--<td class="grade" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["GRADE"].ToString() %></td>--%>

                    <td class="grade bd-r" rowspan="<%=planCount %>" style="font-weight: bold"><%=Result1.Rows[i]["GRADE"].ToString() %></td>
                    <td class="final_grade" rowspan="<%=planCount %>" data-value="<%=Result1.Rows[i]["FINAL_GRADE"].ToString() %>"><%=final_grade %></td>
                    <td class="rp" rowspan="<%=planCount %>"><%=Result1.Rows[i]["REASON"].ToString()+" - "+Result1.Rows[i]["REMARK"].ToString() %></td>
                    <td class="rp" data-status="<%=statusCode %>" rowspan="<%=planCount %>"><%=status %></td>
                </tr>
                <% var step = 0;
                   for (int j = 1; j < planCount; j++)
                   {
                       step = i + j;%>
                <tr class="tr-content <%=active %>" data-role="<%=Result1.Rows[step]["ROLE"].ToString() %>" data-emid="<%=Result1.Rows[i]["EMP_ID"].ToString() %>" style="font-size: 11px;">
                    <td class="cont" data-id="<%=Result1.Rows[step]["PID"].ToString() %>"><%=Result1.Rows[step]["CONT"].ToString() %></td>
                    <%--<td class="action"><%=Result1.Rows[step]["ACTION_PLAN"].ToString() %></td>--%>
                    <td class="pid" data-id="<%=Result1.Rows[step]["PID"].ToString() %>"><%=Result1.Rows[step]["TARGET"].ToString() %></td>

                    <td class="result"><%=Result1.Rows[step]["RESULT"].ToString() %></td>
                    <td class="weight bd-r"><%=Result1.Rows[step]["WEIGHT"].ToString() %></td>
                    <td class="self_rate"><%=Result1.Rows[step]["MBO_SELF_RATE"].ToString() %></td>
                    <td class="m1rate"><%=Result1.Rows[step]["MBO_M1_RATE"].ToString() %></td>
                    <td class="m2rate"><%=Result1.Rows[step]["MBO_M2_RATE"].ToString() %></td>
                </tr>

                <%} if (planCount > 0) i += planCount - 1;
                    }%>
            </tbody>
        </table>
    </div>


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
    <div class="modal fade" id="mdShowActionPlan" role="dialog">
        <div class="modal-dialog modal-md">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>Action Plan</h4>
                </div>
                <div id="plan_detail" class="modal-body">
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="mdReview" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>Result Review</h4>
                </div>
                <div class="modal-body">
                    <button type="button" data-id="" style="float: right; padding-bottom: 5px;" id="btnExportReview" class="btn btn-primary"><span class="glyphicon glyphicon-export"></span>Export</button>
                    <table id="tbReview" class="table table-striped table-bordered table-hover" data-role="">
                        <thead>
                            <tr class="tbheader">
                                <th>#</th>
                                <th>Name</th>
                                <th>Group</th>
                                <th>Score</th>
                                <th>Grade</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>

                    </table>

                </div>

            </div>
        </div>
    </div>
    <iframe id="txtArea1" style="display: none;"></iframe>

</asp:Content>
