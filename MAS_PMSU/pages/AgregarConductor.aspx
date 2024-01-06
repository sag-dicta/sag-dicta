<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="AgregarConductor.aspx.vb" Inherits="MAS_PMSU.AgregarConductor" %>

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
            <h1 class="page-header">Registro de Motorista</h1>
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
                                    <label>Seleccione Nombre del Motorista:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLNombreGrid" runat="server" AutoPostBack="True">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Tipo Vehiculo:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLTipoGrid" runat="server" AutoPostBack="True">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Identificador del Vehiculo:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLIdenVehi" runat="server" AutoPostBack="True">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <%--<asp:Label ID="Label2" runat="server" CssClass="label label-warning" Text="Para crear un plan nuevo primero seleccione el departamento, el municipio y el multiplicador" />--%>
                                <asp:Button ID="BAgregar" runat="server" Text="Asignar Vehiculo a Motorista" CssClass="btn btn-success" Visible="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <h4>
                                        <span style="float: right;"><small># Motoristas:</small>
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

                                            <asp:BoundField DataField="ID">
                                                <HeaderStyle CssClass="hide" />
                                                <ItemStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre_multiplicador" HeaderText="MULTIPLICADOR" />
                                            <asp:BoundField DataField="cedula_multiplicador" HeaderText="CEDULA" />
                                            <asp:BoundField DataField="nombre_finca" HeaderText="NOMBRE DE LA FINCA" />
                                            <asp:BoundField DataField="nombre_productor" HeaderText="PRODUCTOR" />
                                            <asp:BoundField DataField="no_registro_productor" HeaderText="No. REGISTRO" />
                                            <asp:BoundField DataField="Departamento" HeaderText="DEPARTAMENTO" />
                                            <asp:BoundField DataField="municipio" HeaderText="MUNICIPIO" />

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
                    Datos del Motorista
                </div>

                <div class="panel-body">
                    <div class="row" style="align-items: flex-end;">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Nombre del Conductor:</label>
                                <asp:Label ID="LblNombCond" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtNombCond" runat="server" AutoPostBack="true" Visible="false"></asp:TextBox>
                                <asp:DropDownList CssClass="form-control" ID="DDLNombCond" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                    <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Button ID="BtnNewMoto" runat="server" Text="Nuevo Motorista" CssClass="btn btn-success" Visible="true" OnClick="BtnNewMoto_Click"/>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>DNI:</label>
                                <asp:Label ID="LblDNICond" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtDNICond" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Telefono:</label>
                                <asp:Label ID="LblTelfCond" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtTelfCond" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Datos del Vehículo
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Identificador del Vehiculo:</label>
                                <asp:Label ID="LblNombre" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtNombre" runat="server" AutoPostBack="true" ReadOnly="true" Visible="false"></asp:TextBox>
                                <asp:DropDownList CssClass="form-control" ID="DDLNombre" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                    <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Tipo:</label>
                                <asp:Label ID="lbTipo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtTipo" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Marca:</label>
                                <asp:Label ID="LblMarca" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtMarca" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Placa No.:</label>
                                <asp:Label ID="LblPlaca" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtPlaca" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Color:</label>
                                <asp:Label ID="LblColor" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtColor" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div>
            <label></label>
            <asp:Label ID="Label18" class="label label-warning" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Imprimir Hoja de Datos del Multiplicador" OnClick="descargaPDF" Visible="false" />
        </div>

        <div>
            <label></label>
            <asp:Label ID="Label23" class="label label-warning" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button CssClass="btn btn-success" ID="Button2" runat="server" Text="Nueva Asignacion" OnClick="vaciar" Visible="false" />
        </div>

        <div>
            <label></label>
            <asp:Label ID="LabelGuardar" class="label label-warning" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button CssClass="btn btn-primary" ID="btnAsignar" runat="server" Text="Asignar" OnClick="guardarSoli_lote" Visible="false" />
            <asp:Button CssClass="btn btn-primary" ID="btnRegresar" runat="server" Text="Regresar" OnClick="guardarSoli_lote" Visible="false" />
        </div>

    </div>

    <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>
    <div class="modal fade" id="DeleteModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalTitle2">SAG - DICTA</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label3" runat="server" Text="Mensaje Predeterminado - Label3"></asp:Label>
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
