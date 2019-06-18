<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="MBO.UploadImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="page-header" style="margin: 10px;"><i class="glyphicon glyphicon-upload"></i>Upload Image</h2>
    <div class="container">
        <div class="panel panel-primary">
            <div class="panel-heading">Upload Image</div>

            <div class="panel-body">
                <form class="form-horizontal" runat="server" enctype="multipart/form-data">
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="email">Emp. Id</label>
                        <div class="col-sm-10">
                            <asp:Label ID="lblId" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="pwd">Name</label>
                        <div class="col-sm-10">
                            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="pwd">Browse</label>
                        <div class="col-sm-10">
                            <asp:FileUpload ID="UploadImg" runat="server" AllowMultiple="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-default" OnClick="btnUpload_Click" />
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
