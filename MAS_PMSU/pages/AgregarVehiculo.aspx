<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="AgregarVehiculo.aspx.vb" Inherits="MAS_PMSU.AgregarVehiculo" %>

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
            <h1 class="page-header">Registro de Vehiculo</h1>
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
                                    <label>Seleccione Tipo:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLTipoGrid" runat="server" AutoPostBack="True">
                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Camion" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Trailer" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Marca:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLMarcaGrid" runat="server" AutoPostBack="True">
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
                                <asp:Button ID="BAgregar" runat="server" Text="Agregar Vehiculo" CssClass="btn btn-success" Visible="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <h5>
                                        <span style="float: right;"><small># Vehiculos:</small>
                                        <asp:Label ID="lblTotalClientes" runat="server" CssClass="label"/></span>
                                    </h5>
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connSAG %>" ProviderName="<%$ ConnectionStrings:connSAG.ProviderName %>"></asp:SqlDataSource>
                                    <asp:GridView ID="GridDatos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                                        GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataSourceID="SqlDataSource1" Font-Size="Small">
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <EmptyDataTemplate>
                                            ¡No hay vehiculos con esas caracteristicas!
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
                                            <asp:BoundField DataField="tipo" HeaderText="TIPO" />
                                            <asp:BoundField DataField="marca" HeaderText="MARCA" />
                                            <asp:BoundField DataField="color" HeaderText="COLOR" />
                                            <asp:BoundField DataField="no_placa" HeaderText="N° DE PLACA" />
                                            <asp:BoundField DataField="CodVehi" HeaderText="IDENTIFICADOR" />

                                            <asp:ButtonField ButtonType="Button" Text="Editar" ControlStyle-CssClass="btn btn-warning" HeaderText="EDITAR" CommandName="Editar">
                                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Button" Text="Eliminar" ControlStyle-CssClass="btn btn-danger" HeaderText="ELIMINAR" CommandName="Eliminar">
                                                <ControlStyle CssClass="btn btn-danger"></ControlStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Button" Text="Imprimir" ControlStyle-CssClass="btn btn-success" HeaderText="HOJA DE DATOS" CommandName="Imprimir" Visible="false">
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
                    Datos del Vehículo
                </div>

                <div class="panel-body">
                    <div class="row">

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Tipo:</label>
                                <asp:Label ID="lbTipo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtTipo" runat="server" AutoPostBack="true" ReadOnly="true" Visible="false"></asp:TextBox>
                                <asp:DropDownList CssClass="form-control" ID="DDLTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Camion" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Trailer" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Marca:</label>
                                <asp:Label ID="LblMarca" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtMarca" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Placa No.:</label>
                                <asp:Label ID="LblPlaca" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtPlaca" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Color:</label>
                                <asp:Label ID="LblColor" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtColor" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return lettersOnly(this);"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Identificador del Vehiculo:</label>
                                <asp:Label ID="LblIdenVehi" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtIdenVehi" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
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
            <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Imprimir Hoja de Datos del Vehiculo" OnClick="descargaPDF" Visible="false" />
        </div>

        <div>
            <label></label>
            <asp:Label ID="Label23" class="label label-warning" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button CssClass="btn btn-success" ID="Button2" runat="server" Text="Nuevo Vehiculo" OnClick="vaciar" Visible="false" />
        </div>

        <div>
            <label></label>
            <asp:Label ID="LabelGuardar" class="label label-warning" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button CssClass="btn btn-primary" ID="btnGuardarLote" runat="server" Text="Guardar" OnClick="guardarSoli_lote" Visible="false" />
            <asp:Button CssClass="btn btn-primary" ID="btnRegresar" runat="server" Text="Regresar" OnClick="guardarSoli_lote" Visible="false" />
        </div>

    </div>

    <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>
    <div class="modal fade" id="DeleteModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalTitle2">SAG - DICTA</h4>
                    <button type="button" class="close" data-dismiss="DeleteModal" aria-hidden="true">&times;</button>
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

            // Check if the key code corresponds to a letter (a-z or A-Z), a space, or an accent
            if ((keyCodeEntered >= 65 && keyCodeEntered <= 90) || // A-Z
                (keyCodeEntered >= 97 && keyCodeEntered <= 122) || // a-z
                keyCodeEntered === 32 || // space
                (keyCodeEntered >= 192 && keyCodeEntered <= 255)) { // accented characters
                return true;
            }

            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#DeleteModal').on('hidden.bs.modal', function () {
                // Cuando la modal se cierra, redirige a la página deseada
                window.location.href = 'AgregarVehiculo.aspx';
            });
        });
    </script>
</asp:Content>
