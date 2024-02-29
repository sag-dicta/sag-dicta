<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Acta_DescArch.aspx.vb" Inherits="MAS_PMSU.Acta_DescArch" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header"></h1>
        </div>
    </div>
    <div id="panelmasiso" runat="server">

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Descargar de Acta de Recepción Firmados
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Productor:</label>
                                    <asp:DropDownList CssClass="form-control" ID="TxtProductor" runat="server" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label></label>
                                    <asp:Label ID="Label23" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:Button CssClass="btn btn-primary" ID="Button3" runat="server" Text="Reiniciar" OnClick="limpiarFiltros" Visible="true" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <h3>
                                        <span style="float: right;"><small># Lotes:</small>
                                            <asp:Label ID="lblTotalClientes" runat="server" CssClass="label label-warning" /></span>
                                    </h3>
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connSAG %>" ProviderName="<%$ ConnectionStrings:connSAG.ProviderName %>"></asp:SqlDataSource>
                                    <asp:GridView ID="GridDatos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                                        GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataSourceID="SqlDataSource1" Font-Size="Small">
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <EmptyDataTemplate>
                                            ¡No hay registros!
                                        </EmptyDataTemplate>
                                        <%--Paginador...--%>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <PagerTemplate>
                                            <div class="row" style="margin-top: 8px;">
                                                <div class="col-lg-1" style="text-align: right;">
                                                    <h5>
                                                        <asp:Label ID="MessageLabel" Text="Ir a la pág." runat="server" /></h5>
                                                </div>
                                                <div class="col-lg-1" style="text-align: left;">
                                                    <asp:DropDownList ID="PageDropDownList" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged" runat="server" CssClass="form-control" /></h3>
                                                </div>
                                                <div class="col-lg-10" style="text-align: right;">
                                                    <h3>
                                                        <asp:Label ID="CurrentPageLabel" runat="server" CssClass="label label-warning" /></h3>
                                                </div>
                                            </div>
                                        </PagerTemplate>
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>

                                            <asp:BoundField DataField="ID_acta">
                                                <HeaderStyle CssClass="hide" />
                                                <ItemStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre_multiplicador" HeaderText="MULTIPLICADOR" />
                                            <asp:BoundField DataField="tipo_cultivo" HeaderText="TIPO DE CULTIVO" />
                                            <asp:BoundField DataField="lote_registrado" HeaderText="NUMERO DE LOTE REGISTRADO" />

                                            <%--<asp:BoundField DataField="Habilitado" HeaderText="HABILITADO" />--%>

                                            <asp:ButtonField ButtonType="Button" Text="Descargar" ControlStyle-CssClass="btn btn-success" HeaderText="Acta Firmada" CommandName="FichaLote">
                                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                            </asp:ButtonField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" visible="false">
                            <div class="col-lg-2">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning" Text="Exportar Datos"><span class="glyphicon glyphicon-save" Visible="false"></span>&nbsp;Exportar Datos</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row" runat="server">
                            <div class="col-lg-2">
                                <asp:Button CssClass="btn btn-primary" ID="btnRegresar" runat="server" Text="Regresar" Visible="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>
                                    <asp:Button ID="BAgregar" runat="server" Text="Agregar Inscripcion" CssClass="btn btn-success" />
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>
                                    <asp:Button CssClass="btn btn-danger" ID="Button2" runat="server" Text="Exportar orden de compra a PDF" OnClick="descargaPDF" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <asp:TextBox ID="TxtID" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txt_habilitado" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>

                        <asp:TextBox ID="TxtTabla" runat="server" Visible="False"></asp:TextBox>

                        <div class="modal fade" id="DeleteModal" tabindex="-1" role="dialog" aria-labelledby="ModalTitle2"
                            aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                    &times;</button>--%>
                                        <h4 class="modal-title" id="ModalTitle2">MAS 2.0 - DICTA - MSU</h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="modal-footer" style="text-align: center">
                                        <asp:Button ID="BConfirm" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" />
                                        <asp:Button ID="BBorrarsi" Text="SI" Width="80px" runat="server" Class="btn btn-primary" />
                                        <asp:Button ID="BBorrarno" Text="NO" Width="80px" runat="server" Class="btn btn-primary" />
                                        <%--<asp:Button ID="Button2" Text="Salir" Width="80px" runat="server" Class="btn btn-primary" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade" id="DeleteModal2" tabindex="-1" role="dialog" aria-labelledby="ModalTitle2"
                            aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title" id="ModalTitle3">MAS 2.0 - TNS</h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="modal-footer" style="text-align: center">
                                        <asp:Button ID="BConfirm2" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>


    <contenttemplate>

    </contenttemplate>



</asp:Content>
