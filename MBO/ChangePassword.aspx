<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="MBO.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="panel panel-primary">
            <div class="panel-heading">Change Password</div>

            <div class="panel-body">
                <form runat="server" class="form-horizontal" role="form" id="frmChangePassword" method="post">
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="id">Employee ID:</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtEmpNo" runat="server" class="form-control" placeholder="EmID" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="id">Name:</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Name" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="name">Password:</label>
                        <div class="col-sm-10">
                            <asp:TextBox TextMode="Password" ID="txtPassword" MaxLength="30" runat="server" class="form-control" placeholder="Password" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="dept">New Password:</label>
                        <div class="col-sm-10">
                            <asp:TextBox TextMode="Password" ID="txtNewPassword" MaxLength="30" runat="server" class="form-control" placeholder="New Password" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="approver">Confirm Password:</label>
                        <div class="col-sm-10">
                            <asp:TextBox TextMode="Password" ID="txtConfirm" MaxLength="30" runat="server" class="form-control" placeholder="Confirm Password" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnChangePassword" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnChangePassword_Click"/>
                            <button type="reset" class="btn btn-default">Cancel</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
