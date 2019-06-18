<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Eva_Info.aspx.cs" Inherits="MBO.Eva_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/bt/dataTables.bootstrap.css" />
    <script src="Scripts/Include/jquery.dataTables.min.js"></script>
    <script src="Scripts/Include/dataTables.bootstrap.js"></script>
    <script src="Scripts/eva_info.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-list-alt"></i>&nbsp;Eva Info</h4>
    <div class="row">
        <div class="col-sm-8 col-md-8 col-lg-8">
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
                                <select id="ddlEva" class="form-control" style="height: 30px;">
                                    <% foreach (var group in Groups)
                                       {%>
                                    <option value="<%=group.ID %>"><%=group.GROUP_NAME.Trim() %></option>
                                    <%} %>
                                </select>
                            </td>
                            <td class="tdleft">EMP_ID/NAME</td>
                            <td>
                                <input type="text" id="txtQuery" class="form-control" />
                            </td>
                            <td>
                                <button type="button" id="btnSearch" class="btn btn-primary"><span class="glyphicon glyphicon-search"></span>Search</button>
                            </td>
                        </tr>


                    </tbody>
                </table>
            </div>
        </div>

        <div class="col-md-4">
        </div>
    </div>

    <div class="table-responsive">
        <table id="tbMainDefault" class="table table-bordered" style="border-collapse: separate;">
            <thead>
                <tr class="tbheader">
                    <th>EMP ID</th>
                    <th>FULL NAME</th>
                    <th>GROUP</th>
                    <th>TOEIC/TOPIK</th>
                    <th>Certification</th>
                    <th>Absent 1 day</th>
                    <th>Absent 2 day</th>
                    <th>Unpaid 10~14 days</th>
                    <th>Unpaid >15 days</th>
                    <th>Warning 1</th>
                    <th>Warning 2</th>
                    <th>Having explaination letter</th>
                    <th>Remark</th>
                </tr>

            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
</asp:Content>
