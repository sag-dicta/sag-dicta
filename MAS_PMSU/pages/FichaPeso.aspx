<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="FichaPeso.aspx.vb" Inherits="MAS_PMSU.FichaPeso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .label.label-warning {
            background-color: yellow;
            display: inline-block; /* Para que el fondo abarque toda la etiqueta */
            padding: 0px; /* Ajusta según sea necesario */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Ficha De Peso Al Recibo Lotes De Semilla (Pesaje Y Embolsado)</h1>
        </div>
    </div>
    <div id="DivGrid" runat="server" visible="true">
        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Datos Generales
                </div>

                <div class="panel-body">

                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Seleccione Departamento:</label>
                                <asp:DropDownList CssClass="form-control" ID="TxtDepto" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Seleccione Multiplicador:</label>
                                <asp:DropDownList CssClass="form-control" ID="TxtProductorGrid" runat="server" AutoPostBack="True">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Seleccione Cultivo:</label>
                                <asp:DropDownList CssClass="form-control" ID="DDL_SelCult" runat="server" AutoPostBack="True">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                    <asp:ListItem Text="Frijol"></asp:ListItem>
                                    <asp:ListItem Text="Maiz"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Seleccione Ciclo:</label>
                                <asp:DropDownList CssClass="form-control" ID="txtciclo" runat="server" AutoPostBack="True">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <h4>
                                    <span style="float: right;"><small># Fichas:</small>
                                        <asp:Label ID="lblTotalClientes" runat="server" CssClass="label label-warning" /></span>
                                </h4>
                                <p>&nbsp;</p>
                                <p>&nbsp;</p>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connSAG %>" ProviderName="<%$ ConnectionStrings:connSAG.ProviderName %>"></asp:SqlDataSource>
                                <asp:GridView ID="GridDatos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                                    GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataSourceID="SqlDataSource1" Font-Size="Small">
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    <EmptyDataTemplate>
                                        ¡No hay motoristas con esas caracteristicas!
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
                                        <asp:BoundField DataField="nombre_multiplicador" HeaderText="NOMBRE DEL PRODUCTOR" />
                                        <asp:BoundField DataField="departamento" HeaderText="DEPARTAMENTO" />
                                        <asp:BoundField DataField="representante_legal" HeaderText="RESPONSABLE LEGAL" />
                                        <asp:BoundField DataField="ciclo_acta" HeaderText="CICLO" />
                                        <asp:BoundField DataField="categoria_registrado" HeaderText="CATEGORÍA" />
                                        <asp:BoundField DataField="tipo_cultivo" HeaderText="CULTIVO" />
                                        <asp:BoundField DataField="variedad" HeaderText="VARIEDAD" />
                                        <asp:BoundField DataField="lote_registrado" HeaderText="N° DE LOTE" />
                                        <asp:BoundField DataField="porcentaje_humedad" HeaderText="% DE HUMEDAD DE INGRESO" />
                                        <asp:BoundField DataField="no_sacos" HeaderText="CANTIDAD DE SACOS" />
                                        <asp:BoundField DataField="semilla_QQ_oro" HeaderText="PESO BRUTO (QQ)" />
                                        <asp:BoundField DataField="tara" HeaderText="TARA (QQ)" />
                                        <asp:BoundField DataField="peso_neto" HeaderText="PESO NETO (QQ)" />

                                        <asp:ButtonField ButtonType="Button" Text="Editar" ControlStyle-CssClass="btn btn-warning" HeaderText="EDITAR" CommandName="Editar">
                                            <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Eliminar" ControlStyle-CssClass="btn btn-danger" HeaderText="ELIMINAR" CommandName="Eliminar">
                                            <ControlStyle CssClass="btn btn-danger"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Imprimir" ControlStyle-CssClass="btn btn-warning" HeaderText="IMPRIMIR ACTA" CommandName="Imprimir">
                                            <ControlStyle CssClass="btn btn-warning"></ControlStyle>
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
                    <div class="row">
                        <div class="col-lg-12">
                            <%--<asp:Button ID="Button1" runat="server" Text="Exportar Datos" CssClass="btn btn-success" />--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning" Text="Exportar Datos"><span class="glyphicon glyphicon-save"></span>&nbsp;Exportar Datos</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="DivActa" runat="server" visible="false">
        <div id="DivActa1" runat="server" visible="true">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Datos de la Ficha de Peso
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-4" runat="server" visible="false">
                                <div class="form-group">
                                    <label for="txt">ID:</label>
                                    <asp:TextBox CssClass="form-control" ID="TxtID" runat="server" AutoPostBack="false"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="TextRespaldo" runat="server" AutoPostBack="false"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="txtunion" runat="server" AutoPostBack="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Nombre Del Productor</label><asp:Label ID="lb_nombre_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txt_nombre_prod_new" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return lettersOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Lugar de Procedencia:</label><asp:Label ID="lblLugProc" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtLugProc" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Representante Legal</label><asp:Label ID="LB_RepresentanteLegal" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="Txt_Representante_Legal" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return lettersOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Telefono</label><asp:Label ID="LblTelefono" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="TxtTelefono" runat="server" Enabled="false" AutoPostBack="true" MaxLength="8" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Categoria:</label><asp:Label ID="lblCategoria" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCategoria" runat="server" OnTextChanged="Verificar" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Cultivo:</label><asp:Label ID="lblCultivo" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCultivo" runat="server" OnTextChanged="Verificar" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Variedad:</label><asp:Label ID="lblVariedad" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtVariedad" runat="server" OnTextChanged="Verificar" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Número de Lote:</label><asp:Label ID="LbLote" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="TxtLote" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Humedad (%):</label><asp:Label ID="lblHumedad" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtHumedad" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Total de Sacos:</label><asp:Label ID="lblCantSaco" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCantSaco" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Peso Bruto (QQ):</label><asp:Label ID="lblPesoBrut" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtPesoBrut" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div id="DivActa2" runat="server" visible="true">

            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Desglose de Peso Bruto y Cantidad de Sacos
                    </div>

                    <div class="panel-body">
                        <div class="row">

                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Cantidad Sacos:</label><asp:Label ID="lblCanSacMiniGrid" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCanSacMiniGrid" runat="server" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Peso en libras:</label><asp:Label ID="lblPesoLibMiniGrid" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtPesoLibMiniGrid" runat="server" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Cantidad en Quintales:</label><asp:Label ID="lblCanQuinMiniGrid" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCanQuinMiniGrid" runat="server" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label style="color: #FFFFFF">Agregar</label>
                                    <asp:Button CssClass="btn btn-primary" ID="btnAgregar" runat="server" AutoPostBack="True" Text="+" Font-Bold="True" Visible="false"></asp:Button>
                                </div>
                            </div>

                            <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtidminigrid" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="TextBanderita" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtRespaldito" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="TextminigridCambiarestado" runat="server" Visible="false"></asp:TextBox>
                        </div>

                        <div class="row">
                            <asp:Label ID="lblmas" class="label label-warning" runat="server" Text=""></asp:Label>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connSAG %>" ProviderName="<%$ ConnectionStrings:connSAG.ProviderName %>"></asp:SqlDataSource>
                            <asp:GridView ID="GridProductos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                                GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataSourceID="SqlDataSource1" Font-Size="Small">
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                    ¡No hay datos registrados!
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

                                    <asp:BoundField DataField="ID">
                                        <HeaderStyle CssClass="hide" />
                                        <ItemStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cantidad_sacos_ficha" HeaderText="CANTIDAD DE SACOS" />
                                    <asp:BoundField DataField="peso_lb_ficha" HeaderText="PESO EN LB" />
                                    <asp:BoundField DataField="cantidad_qq_ficha" HeaderText="CANTIDAD EN QQ" />

                                    <asp:ButtonField ButtonType="Button" Text="↺" ControlStyle-CssClass="btn btn-warning" HeaderText="Editar" CommandName="Editar">
                                        <ControlStyle CssClass="btn btn-warning"></ControlStyle>
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Button" Text="-" ControlStyle-CssClass="btn btn-danger" HeaderText="QUITAR" CommandName="Eliminar">
                                        <ControlStyle CssClass="btn btn-danger"></ControlStyle>
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
            </div>
        </div>

        <div id="DivActa3" runat="server" visible="true">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div class="row">
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Cantidad Sacos:</label><asp:Label ID="lblCantSacoC" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCantSacoC" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Peso en libras:</label><asp:Label ID="lblPesoLibr" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtPesoLibr" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Cantidad en Quintales:</label><asp:Label ID="lblCantQQ" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCantQQ" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Tara (QQ):</label><asp:Label ID="lblTara" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtTara" runat="server" Enabled="true" onkeypress="return numericOnly(this);" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Peso Neto (QQ):</label><asp:Label ID="lblPesoNeto" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtPesoNeto" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div>
        <div class="col-lg-2">
            <div class="form-group">
                <label></label>
                <asp:Label ID="LabelGuardar" class="label label-warning" runat="server" Text=""></asp:Label>
                <br />
                <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>
                <asp:Button CssClass="btn btn-primary" ID="btnGuardarActa" runat="server" Text="Guardar" Visible="false" OnClick="btnGuardarActa_Click" />
            </div>
        </div>
    </div>

    <div>
        <div class="col-lg-2">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary" ID="BtnImprimir" runat="server" Text="Descargar en PDF " OnClick="descargaPDF" Visible="false" />
            </div>
        </div>
        <div class="col-lg-2">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary" ID="BtnNuevo" runat="server" Text="Regresar" OnClick="vaciar" Visible="false" />
                <asp:Button CssClass="btn btn-primary" ID="btnRegresarConficha" runat="server" Text="Regresar" Visible="false" />
            </div>
        </div>
    </div>

    <div class="modal fade" id="DeleteModal" tabindex="-1" role="dialog" aria-labelledby="ModalTitle2" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalTitle2">SAG - DICTA</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label103" runat="server" Text="El Acta de Recepcion de semilla ha sido almacenada con exito"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="BConfirm" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" OnClick="BConfirm_Click" />
                    <asp:Button ID="BBorrarsi" Text="SI" Width="80px" runat="server" Class="btn btn-primary" />
                    <asp:Button ID="BBorrarno" Text="NO" Width="80px" runat="server" Class="btn btn-primary" />
                    <asp:Button ID="BConfirm2" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function numericOnly(elementRef) {
            var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

            // Un-comment to discover a key that I have forgotten to take into account...
            //alert(keyCodeEntered);

            if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
                return true;
            }
            // '+' sign...
            //else if (keyCodeEntered == 43) {
            //    // Allow only 1 plus sign ('+')...
            //    if ((elementRef.value) && (elementRef.value.indexOf('+') >= 0))
            //        return false;
            //    else
            //        return true;
            //}
            //    // '-' sign...
            //else if (keyCodeEntered == 45) {
            //    // Allow only 1 minus sign ('-')...
            //    if ((elementRef.value) && (elementRef.value.indexOf('-') >= 0))
            //        return false;
            //    else
            //        return true;
            //}
            // '.' decimal point...
            else if (keyCodeEntered == 46) {
                // Allow only 1 decimal point ('.')...
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
                else
                    return true;
            }

            return false;
        }
    </script>
</asp:Content>
