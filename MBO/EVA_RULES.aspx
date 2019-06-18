<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EVA_RULES.aspx.cs" Inherits="MBO.EVA_RULES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/bt/dataTables.bootstrap.css" />
    <script src="Scripts/Include/jquery.dataTables.min.js"></script>
    <script src="Scripts/Include/dataTables.bootstrap.js"></script>
    <script src="Scripts/eva_rule.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-user"></i>&nbsp;EVA RULES</h2>
    <div class="container-fluid">
        <div class="panel panel-primary">
            <div class="panel-heading">Period</div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-md-8"></div>
                    <div class="col-md-4">
                        <div class="form-inline">
                            <button type="button" id="btnNew" class="btn btn-primary"><span class="glyphicon glyphicon-file"></span>New</button>
                            <button type="button" id="btnEdit" class="btn btn-info"><span class="glyphicon glyphicon-pencil"></span>Edit</button>
                            <button type="button" id="btnDelete" class="btn btn-warning"><span class="glyphicon glyphicon-remove"></span>Delete</button>
                        </div>
                    </div>
                </div>
                <table class="table table-bordered" id="tbMainDefault">
                    <thead>
                        <tr class="tbheader">
                            <th>SEL</th>
                            <th>Num Emps</th>
                            <th>S</th>
                            <th>A</th>
                            <th>B-C-D</th>

                        </tr>
                    </thead>
                    <tbody>
                        <% foreach (var rule in RULES)
                           {
                              
                        %>
                        <tr>
                            <td>
                                <input type="checkbox" class="ckb" data-num="<%=rule.NUM_EMPS %>" /></td>
                            <td><%=rule.NUM_EMPS %></td>
                            <td><%=rule.S %></td>
                            <td><%=rule.A %></td>
                            <td><%=rule.BCD %></td>

                        </tr>
                        <% } %>
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
                    <h4 id="H1"><span class="glyphicon glyphicon-user"></span>Add Rules</h4>
                    <input type="hidden" id="action" />
                </div>
                <div class="modal-body">
                    <form id="frmRule" role="form">
                        <table class="table table-bordered">
                            <tbody>
                                <tr>
                                    <td class="tdleft">
                                        <label>Num emps</label></td>
                                    <td>
                                        <input type="number" id="txtNum" class="form-control" min="0" required="required" /></td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>S</label>
                                    </td>
                                    <td>
                                        <input type="number" id="txtS" min="0" class="form-control" required="required" /></td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>A</label></td>
                                    <td>
                                        <input type="number" id="txtA" min="0" class="form-control" required="required" /></td>
                                </tr>
                                <tr>
                                    <td class="tdleft">
                                        <label>B-C-D</label></td>
                                    <td>
                                        <input type="number" id="txtB" min="0" class="form-control" required="required" /></td>
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

</asp:Content>
