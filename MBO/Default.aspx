<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MBO.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Default.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h4 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-list-alt"></i>&nbsp;REGISTER &amp; CONFIRM MBO PLAN</h4>
    <div class="row">
        <div class="col-sm-6 col-md-6 col-lg-6">
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
                                    <option value="register">Self Plan</option>
                                    <option value="approve">Confirm Plan</option>
                                </select>
                            </td>

                        </tr>


                    </tbody>
                </table>
            </div>
        </div>

    </div>
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-8">
            <div class="form-inline">
                <button type="button" id="btnSearch" class="btn btn-primary"><span class="glyphicon glyphicon-search"></span>Search</button>
                <button type="button" id="btnAdd" class="btn btn-default"><span class="glyphicon glyphicon-file"></span>Add row</button>

                <button type="button" id="btnDel" class="btn btn-danger" data-delete=""><span class="glyphicon glyphicon-trash"></span>Delete row</button>
                <button type="button" id="btnSave" class="btn btn-success"><span class="glyphicon glyphicon-floppy-saved"></span>Save</button>
                <button type="button" id="btnConfirm" class="btn btn-info" data-empid="<%=Session["EMP_ID"].ToString()%>"><span class="glyphicon glyphicon-ok"></span>Confirm</button>
                <button type="button" id="btnUnconfirm" class="btn btn-warning"><span class="glyphicon glyphicon-remove"></span>Unconfirm</button>
            </div>
        </div>
    </div>

    <div id="mytable" style="height: 500px; overflow: auto;">
        <table id="tbMainDefault" data-rowspan="1" class="table table-bordered" style="border-collapse: separate !important;">
            <thead>
                <tr class="tbheader">
                    <th style="width: 5px; padding: 5px 5px 8px 5px;">
                        <input type="checkbox" id="ckAll" /></th>
                    <th style="width: 100px">Picture</th>

                    <th>Full Name</th>
                    <th>Workgroup</th>

                    <th style="padding-right: 200px;">Content</th>
                    <th style="padding-right: 200px;">Action Plan</th>
                    <th style="padding-right: 200px;">Target</th>
                    <th>Weight</th>
                    <th>Level</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                <% if (EMP != null)
                   {
                       var rId = (Result.Rows.Count == 0) ? "0" : Result.Rows[0]["RES_ID"].ToString();
                %>
                <tr class="cover" data-id="<%=rId %>" data-emid="<%=EMP.EMP_ID %>" style="font-size: 11px;">
                    <td class="rp" rowspan="1">
                        <input type="checkbox" class="ckb" />
                    </td>
                    <td class="rp" rowspan="1">
                        <img class='img-rounded' height='100' width='100' alt="X" src="<%=EMP.PICTURE %>" /></td>

                    <td class="rp" rowspan="1"><%=EMP.NAME %></td>
                    <td class="rp" rowspan="1"><%=EMP.WORKGROUP %></td>

                    <td class="ap" data-click="0" data-id="0"></td>
                    <td class="ap" data-click="0"></td>
                    <td class="ap" data-click="0"></td>
                    <td class="ap weight" data-click="0"></td>
                    <td class="ap lvl" data-click="0"></td>
                    <td class="rp" rowspan="1" data-status="0" style="border-left-width: 1px">Unconfirmed</td>
                </tr>
                <% }
                   else
                   {
                       int seq = 0;
                       for (int i = 0; i < Result.Rows.Count; i++)
                       {
                           string active = (seq % 2 == 0) ? "info" : string.Empty;
                           seq++;
                           var span = 0;
                           int statusCode = 0;
                           if (!string.IsNullOrEmpty(Result.Rows[i]["PLAN_STATUS"].ToString()))
                               statusCode = Convert.ToInt32(Result.Rows[i]["PLAN_STATUS"].ToString());
                           var status = string.Empty;
                           switch (statusCode)
                           {
                               case 0:
                                   status = "Unconfirmed";
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
                                   span++;
                               }
                           }
                %>
                <tr class="cover <%=active %>" data-id="<%=Result.Rows[i]["RES_ID"].ToString()%>" data-emid="<%=Result.Rows[i]["EMP_ID"].ToString()%>" style="font-size: 11px;">
                    <td class="rp" rowspan="<%=span%>">
                        <input type="checkbox" class="ckb" />
                    </td>
                    <td class="rp" rowspan="<%=span%>">
                        <img class='img-rounded' height='100' width='100' alt="X" src="<%=Result.Rows[i]["PICTURE"].ToString()%>" /></td>

                    <td class="rp" rowspan="<%=span%>"><%=Result.Rows[i]["NAME"].ToString()%></td>
                    <td class="rp" rowspan="<%=span%>"><%=Result.Rows[i]["WORKGROUP"].ToString()%></td>

                    <td class="ap" data-click="0" data-id="<%=Result.Rows[i]["PID"].ToString()%>"><%=Result.Rows[i]["CONT"].ToString()%></td>
                    <td class="ap" data-click="0"><%=Result.Rows[i]["ACTION_PLAN"].ToString()%></td>
                    <td class="ap" data-click="0"><%=Result.Rows[i]["TARGET"].ToString()%></td>
                    <td class="ap weight" data-click="0"><%=Result.Rows[i]["WEIGHT"].ToString()%></td>
                    <td class="ap lvl" data-click="0"><%=Result.Rows[i]["LVL"].ToString()%></td>
                    <td class="rp" data-status="<%=statusCode%>" rowspan="<%=span%>" style="border-left-width: 1px"><%=status%></td>
                </tr>
                <% 
                       for (int j = 1; j < span; j++)
                       {
                         
   
                %>
                <tr class="tr-content <%=active %>" style="font-size: 11px;" data-status="<%=statusCode %>" data-emid="<%=Result.Rows[i]["EMP_ID"].ToString()%>">
                    <td data-click="" data-id="<%=Result.Rows[i+j]["PID"].ToString()%>"><%=Result.Rows[i+j]["CONT"].ToString()%></td>
                    <td><%=Result.Rows[i+j]["ACTION_PLAN"].ToString()%></td>
                    <td><%=Result.Rows[i+j]["TARGET"].ToString()%></td>
                    <td class="weight"><%=Result.Rows[i+j]["WEIGHT"].ToString()%></td>
                    <td class="lvl"><%=Result.Rows[i+j]["LVL"].ToString()%></td>
                </tr>
                <%}
                       if (span > 0) i += span - 1;
                   }
                   } %>
            </tbody>
        </table>
    </div>


</asp:Content>
