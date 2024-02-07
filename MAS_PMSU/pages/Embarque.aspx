<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Embarque.aspx.vb" Inherits="MAS_PMSU.Embarque" %>

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
            <h1 class="page-header">Registro de Embarque</h1>
        </div>
    </div>

    <div id="DivGrid" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Departamento:</label>
                                    <asp:DropDownList CssClass="form-control" ID="TxtDepto" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Municipio:</label>
                                    <asp:DropDownList CssClass="form-control" ID="TxtMunicipio" runat="server" AutoPostBack="True">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Multiplicador:</label>
                                    <asp:DropDownList CssClass="form-control" ID="TxtMultiplicador" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TxtMultiplicador_SelectedIndexChanged">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <%--<asp:Label ID="Label2" runat="server" CssClass="label label-warning" Text="Para crear un plan nuevo primero seleccione el departamento, el municipio y el multiplicador" />--%>
                                <asp:Button ID="BAgregar" runat="server" Text="Agregar Embarque" CssClass="btn btn-success" Visible="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <h4>
                                        <span style="float: right;"><small># EMBARQUES:</small>
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
                                            ¡No hay multiplicadores inscritos!
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

                                            <asp:BoundField DataField="ID" ItemStyle-CssClass="hide">
                                                <HeaderStyle CssClass="hiding" />
                                                <ItemStyle CssClass="hiding" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="no_conocimiento" HeaderText="N° DE CONOCIMIENTO" />
                                            <asp:BoundField DataField="destinatario" HeaderText="DESTINATARIO" />
                                            <asp:BoundField DataField="fecha_elaboracion" HeaderText="FECHA DE ELABORACION" />
                                            <asp:BoundField DataField="cultivo_general" HeaderText="CULTIVO GENERAL" />
                                            <asp:BoundField DataField="lugar_destinatario" HeaderText="LUGAR DESTINATARIO" />
                                            <asp:BoundField DataField="remitente" HeaderText="REMITENTE" />
                                            <asp:BoundField DataField="conductor" HeaderText="TRANSPORTISTA" />

                                            <asp:ButtonField ButtonType="Button" Text="Editar" ControlStyle-CssClass="btn btn-warning" HeaderText="EDITAR" CommandName="Editar">
                                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Button" Text="Eliminar" ControlStyle-CssClass="btn btn-danger" HeaderText="ELIMINAR" CommandName="Eliminar">
                                                <ControlStyle CssClass="btn btn-danger"></ControlStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Button" Text="Imprimir" ControlStyle-CssClass="btn btn-success" HeaderText="HOJA DE DATOS" CommandName="Imprimir">
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
    </div>

    <div id="DivCrearNuevo" runat="server" visible="false">
        <div class="row">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    Información General
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Conocimiento No.:</label><asp:Label ID="lblConoNo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtConoNo" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Para:</label><asp:Label ID="lblPara" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtPara" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Fecha de Elaboración:</label><asp:Label ID="lblFecha" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtFecha" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Cultivo:</label><asp:Label ID="lblCultivo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:DropDownList CssClass="form-control" ID="DDLCultivo" runat="server" AutoPostBack="True">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                    <asp:ListItem Text="Frijol"></asp:ListItem>
                                    <asp:ListItem Text="Maiz"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Información de Envio
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Remitente:</label><asp:Label ID="lblremi" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtRemi" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Lugar (remitente):</label><asp:Label ID="lblLugarR" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtLugarR" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Destinatario:</label><asp:Label ID="lblDestin" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtDestin" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Lugar (Destinatario):</label><asp:Label ID="lblLugarD" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtLugarD" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Información del Producto
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3" id="VariedadFrijol" runat="server" visible="false">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Descripción del Producto</label>
                                    <asp:Label ID="Label4" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList CssClass="form-control" ID="DropDownList5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                        <asp:ListItem id="Amadeus77v1" Text="Amadeus-77"></asp:ListItem>
                                        <asp:ListItem id="Carrizalitov1" Text="Carrizalito"></asp:ListItem>
                                        <asp:ListItem id="Azabachev1" Text="Azabache"></asp:ListItem>
                                        <asp:ListItem id="Paraisitomejoradov1" Text="Paraisito mejorado PM-2"></asp:ListItem>
                                        <asp:ListItem id="Deorhov1" Text="Deorho"></asp:ListItem>
                                        <asp:ListItem id="IntaCardenasv1" Text="Inta Cárdenas"></asp:ListItem>
                                        <asp:ListItem id="Lencaprecozv1" Text="Lenca precoz"></asp:ListItem>
                                        <asp:ListItem id="Rojochortív1" Text="Rojo chortí"></asp:ListItem>
                                        <asp:ListItem id="Tolupanrojov1" Text="Tolupan rojo"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3" id="VariedadMaiz" runat="server" visible="false">
                            <div class="form-group">
                                <label>Descripción del Producto</label><asp:Label ID="Label6" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:DropDownList CssClass="form-control" ID="DropDownList6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                    <asp:ListItem id="DictaMayav1" Text="Dicta Maya"></asp:ListItem>
                                    <asp:ListItem id="DictaVictoriav1" Text="Dicta Victoria"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Categoria:</label>
                                <asp:DropDownList CssClass="form-control" ID="TxtCateogiraGrid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TxtCateogiraGrid_SelectedIndexChanged">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                    <asp:ListItem id="basica1" Text="Basica"></asp:ListItem>
                                    <asp:ListItem id="registrada1" Text="Registrada"></asp:ListItem>
                                    <asp:ListItem id="certificada1" Text="Certificada"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Unidad:</label><asp:Label ID="lblUnid" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtUnid" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Entregado:</label><asp:Label ID="lblEntreg" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtEntreg" runat="server" AutoPostBack="true" OnTextChanged="txtEntreg_TextChanged" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Precio Unitario (Lps):</label><asp:Label ID="lblPrecio" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtPrecio" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-11">
                            <div class="form-group">
                                <label>Observaciones:</label><asp:Label ID="lblObser" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtObser" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-1">
                            <div class="form-group">
                                <label style="color: #FFFFFF">Agregar</label>
                                <asp:Button CssClass="btn btn-primary" ID="btnAgregar" runat="server" AutoPostBack="True" Text="+" Font-Bold="True"></asp:Button>
                                <asp:Button CssClass="btn btn-warning" ID="btnEditar" runat="server" AutoPostBack="True" Text="↺" Font-Bold="True" Visible="false"></asp:Button>
                            </div>
                        </div>

                        <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtidminigrid" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TextBanderita" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtRespaldito" runat="server" Visible="false"></asp:TextBox>
                    </div>

                    <div class="row">

                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connSAG %>" ProviderName="<%$ ConnectionStrings:connSAG.ProviderName %>"></asp:SqlDataSource>
                        <asp:GridView ID="GridProductos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                            GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataSourceID="SqlDataSource1" Font-Size="Small">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            <EmptyDataTemplate>
                                ¡No hay productos inscritos!
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
                                <asp:BoundField DataField="variedad" HeaderText="DESCRIPCION" />
                                <asp:BoundField DataField="categoria_origen" HeaderText="CATEGORIA" />
                                <asp:BoundField DataField="unidad" HeaderText="UNIDAD" />
                                <asp:BoundField DataField="peso_neto" HeaderText="ENTREGADO" />
                                <asp:BoundField DataField="precio_uni" HeaderText="PRECIO" />
                                <asp:BoundField DataField="total" HeaderText="TOTAL" />
                                <asp:BoundField DataField="observaciones" HeaderText="OBSERVACIONES" />

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


        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Información del Conductor
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Seleccione Conductor:</label>
                                <asp:Label ID="Label1" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:DropDownList CssClass="form-control" ID="DDLConductor" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Vehiculo:</label><asp:Label ID="lblVehic" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtVehic" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div>
            <asp:Label ID="Label18" class="label label-warning" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Imprimir Hoja de Datos del Multiplicador" OnClick="descargaPDF" Visible="false" />
        </div>

        <div>
            <asp:Label ID="Label23" class="label label-warning" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Button CssClass="btn btn-success" ID="Button2" runat="server" Text="Nuevo Multiplicador" OnClick="vaciar" Visible="false" />
        </div>

        <div>
            <asp:Label ID="LabelGuardar" class="label label-warning" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button CssClass="btn btn-primary" ID="btnGuardarLote" runat="server" Text="Guardar" OnClick="guardarSoli_lote" Visible="false" />
            <asp:Button CssClass="btn btn-primary" ID="btnRegresar" runat="server" Text="Regresar" OnClick="guardarSoli_lote" Visible="false" />
            <asp:Button CssClass="btn btn-primary" ID="btnRegresarConEmbarque" runat="server" Text="Regresar" Visible="false" />
        </div>

    </div>

    <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>
    <div class="modal fade" id="DeleteModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalTitle2">SAG - DICTA</h4>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label3" runat="server" Text="Mensaje Predeterminado - Label3"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="BConfirm" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" OnClick="BConfirm_Click"/>
                    <asp:Button ID="BBorrarsi" Text="SI" Width="80px" runat="server" Class="btn btn-primary" />
                    <asp:Button ID="BBorrarno" Text="NO" Width="80px" runat="server" Class="btn btn-primary" />
                    <%--<asp:Button ID="Button2" Text="Salir" Width="80px" runat="server" Class="btn btn-primary" />--%>
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

    <script type="text/javascript">
        function lettersOnly(event) {
            var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

            // Un-comment to discover a key that I have forgotten to take into account...
            // alert(keyCodeEntered);

            // Check if the key code corresponds to a letter (a-z or A-Z), a space, an accent, a comma, a period, or parentheses
            if ((keyCodeEntered >= 65 && keyCodeEntered <= 90) || // A-Z
                (keyCodeEntered >= 97 && keyCodeEntered <= 122) || // a-z
                keyCodeEntered === 32 || // space
                (keyCodeEntered >= 192 && keyCodeEntered <= 255) || // accented characters
                keyCodeEntered === 44 || // comma
                keyCodeEntered === 46 || // period
                keyCodeEntered === 40 || // left parenthesis
                keyCodeEntered === 41) { // right parenthesis
                return true;
            }

            return false;
        }
    </script>


</asp:Content>
