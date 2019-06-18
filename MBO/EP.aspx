<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EP.aspx.cs" Inherits="MBO.EP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/bt/datepicker.css" />
    <link rel="stylesheet" href="Content/bt/dataTables.bootstrap.css" />
    <script src="Scripts/Include/bootstrap-datepicker.js"></script>
    <script src="Scripts/Include/jquery.dataTables.min.js"></script>
    <script src="Scripts/Include/dataTables.bootstrap.js"></script>
    <script src="Scripts/Include/datetime.js"></script>

    <script src="Scripts/EP.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-user"></i>&nbsp;PERIOD &amp; EMPLOYEE</h2>

    <div class="panel panel-primary">
        <div class="panel-heading">Period</div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-6"></div>
                <div class="col-md-6">
                    <div class="form-inline">
                        <button type="button" id="btnNew" class="btn btn-primary"><span class="glyphicon glyphicon-file"></span>New</button>
                        <button type="button" id="btnEnable" class="btn btn-success"><span class="glyphicon glyphicon-check"></span>Enable</button>
                        <button type="button" id="btnDisable" class="btn btn-warning"><span class="glyphicon glyphicon-remove"></span>Disable</button>
                        <button type="button" id="btnDel" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span>Delete</button>
                        <button type="button" id="btnSet" class="btn btn-info"><span class="glyphicon glyphicon-ok"></span>Set MBO</button>
                    </div>
                </div>
            </div>
            <table class="table table-bordered" id="tbPeriod">
                <thead>
                    <tr class="tbheader">
                        <th>SEL</th>
                        <th>Eva. Time</th>
                        <th>From</th>
                        <th>To</th>
                        <th>STATUS</th>
                        <th>SET MBO</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var period in Periods)
                       {
                           var status = "Enable";
                           var set = "NO";
                           if (period.SET_MBO == 1)
                               set = "YES";
                           if (period.STATUS == 1)
                               status = "Disable";
                    %>
                    <tr>
                        <td>
                            <input type="checkbox" class="ckb" data-eva="<%=period.EVA_TIME %>" data-set="<%=period.SET_MBO %>" /></td>
                        <td><%=period.EVA_TIME %></td>
                        <td><%=period.EVA_START.ToString("yyyy-MM-dd") %></td>
                        <td><%=period.EVA_END.ToString("yyyy-MM-dd") %></td>
                        <td><%=status %></td>
                        <td><%=set %></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">Employee</div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-1">
                    <form id="Form1" class="form-inline" runat="server" role="form">
                        <asp:LinkButton ID="btnExport"
                            runat="server"
                            CssClass="btn btn-primary" OnClick="btnExport_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-export"></span> Export
                        </asp:LinkButton>
                    </form>
                </div>
                <div class="col-md-11">
                    <div class="form-inline">
                        <button type="button" id="btnAdd" class="btn btn-primary"><span class="glyphicon glyphicon-file"></span>Add</button>
                        <button type="button" id="btnEdit" class="btn btn-default"><span class="glyphicon glyphicon-pencil"></span>Edit</button>
                        <button type="button" id="btnUpload" class="btn btn-info"><span class="glyphicon glyphicon-upload"></span>Upload IMG</button>
                        <button type="button" id="btnDelete" class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span>Delete</button>
                        <button type="button" id="btnApprover1" class="btn btn-info"><span class="glyphicon glyphicon-ok"></span>Assign M1</button>
                        <button type="button" id="btnApprover2" class="btn btn-warning"><span class="glyphicon glyphicon-ok"></span>Assign M2</button>
                        <button type="button" id="btnUnAssign" class="btn btn-warning"><span class="glyphicon glyphicon-remove"></span>UnAssign</button>
                        <button type="button" id="btnResetPass" class="btn btn-warning"><span class="glyphicon glyphicon-ok"></span>Reset Password</button>
                    </div>
                </div>
            </div>
            <div id="tbData" class="table-responsive">
                <table id="tbMainDefault" class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr class="tbheader">
                            <th style="width: 5px; padding: 5px 5px 8px 5px;">SEL</th>
                            <th>Picture</th>
                            <th>Emp. ID</th>
                            <th>Full Name</th>
                            <th>Workgroup</th>
                            <th>Enter date</th>

                            <th>Approver 1</th>
                            <th>Approver 2</th>

                        </tr>
                    </thead>
                    <tbody>
                    </tbody>

                </table>
            </div>
        </div>
    </div>


    <div class="modal fade" id="mdPeriod" role="dialog">
        <div class="modal-dialog modal-md">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 id="H1"><span class="glyphicon glyphicon-user"></span>Add Period</h4>
                    <input type="hidden" id="action" />
                </div>
                <div class="modal-body">
                    <form id="frmPeriod" role="form">
                        <table class="table table-bordered">
                            <tbody>
                                <tr>
                                    <td class="tdleft">
                                        <label>Eva. Time</label></td>
                                    <td>
                                        <input type="text" id="txtEvaTime" class="form-control" required="required" /></td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>From</label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtFrom" class="form-control" required="required" /></td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>To</label></td>
                                    <td>
                                        <input type="text" id="txtTo" class="form-control" required="required" /></td>
                                </tr>

                            </tbody>
                        </table>
                        <button type="submit" id="btnSavePeriod" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span>Save</button>
                        <button class="btn btn-danger" data-dismiss="modal" id="Button2" type="reset"><span class="glyphicon glyphicon-remove"></span>Close</button>
                    </form>
                </div>

            </div>

        </div>
    </div>

    <div class="modal fade" id="mdEmp" role="dialog">
        <div class="modal-dialog modal-md">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 id="H2"><span class="glyphicon glyphicon-user"></span>Add/Edit Employee</h4>
                </div>
                <div class="modal-body">
                    <form id="frmModify" role="form">
                        <table class="table table-bordered">
                            <tbody>
                                <tr>
                                    <td class="tdleft">
                                        <label>Emp. Id</label></td>
                                    <td>
                                        <input type="text" id="txtId" class="form-control" required="required" /></td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>Full Name</label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtName" class="form-control" required="required" /></td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>Workgroup</label></td>
                                    <td>
                                        <select id="ddlGroup" class="form-control">

                                            <% foreach (var group in workgroups)
                                               {%>
                                            <option value="<%=group.WORKGROUP.Trim() %>"><%=group.WORKGROUP %></option>
                                            <% } %>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>Workgroup</label></td>
                                    <td>
                                        <select id="ddlEva" class="form-control">

                                            <% foreach (var group in GROUPS)
                                               {%>
                                            <option value="<%=group.ID %>"><%=group.GROUP_NAME %></option>
                                            <% } %>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>Enter date</label>
                                    </td>
                                    <td>
                                        <input type="date" id="txtDate" class="form-control" /></td>
                                </tr>

                            </tbody>
                        </table>
                        <button type="submit" id="Button1" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span>Save</button>
                        <button class="btn btn-danger" data-dismiss="modal" id="Button3" type="reset"><span class="glyphicon glyphicon-remove"></span>Close</button>
                    </form>
                </div>

            </div>

        </div>
    </div>
    <div class="modal fade" id="mdUser" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>Employee list</h4>
                </div>
                <div class="modal-body">
                    <button type="button" data-assign="" data-empid="" style="float: right; padding-bottom: 5px;" id="btnAssign" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span>Assign</button>
                    <button type="button" data-assign="" data-empid="" style="float: right; padding-bottom: 5px;" id="btnChange" class="btn btn-warning"><span class="glyphicon glyphicon-floppy-disk"></span>Change Approver</button>
                    <table id="tbUser" class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr class="tbheader">
                                <th></th>
                                <th>Picture</th>
                                <th>EMP_ID</th>
                                <th>NAME</th>
                                <th>WORKGROUP</th>
                                <th>ENTER DATE</th>

                            </tr>
                        </thead>
                        <tbody>
                        </tbody>

                    </table>

                </div>

            </div>
        </div>
    </div>
</asp:Content>
