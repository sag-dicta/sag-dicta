<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Cambiopassword.aspx.vb" Inherits="MAS_PMSU.Cambiopassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 50px; padding: 1rem;">
        <div class="col-lg-12">
            <h1 class="page-header"></h1>
        </div>
    </div>
   <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    CAMBIAR CONTRASEÑA
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>1. Ingrese la contraseña actual:</label>
                                <asp:TextBox CssClass="form-control" ID="Txtpassact" runat="server" placeholder="Contraseña Actual" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>2. Ingrese la contraseña nueva:</label>
                                <asp:TextBox CssClass="form-control" ID="Txtpass" runat="server" placeholder="Contraseña Nueva" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>3. Confirme la contraseña nueva:</label>
                                <asp:TextBox CssClass="form-control" ID="Txtpass1" runat="server" placeholder="Confirme Contraseña" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-md-6 col-md-offset-2">
                            <%--<asp:Button ID="Button1" runat="server" Text="Exportar Datos" CssClass="btn btn-success" />--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" Text="Guardar Cambios">&nbsp;Guardar</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-warning" Text="Cancelar">&nbsp;Cancelar</asp:LinkButton>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
