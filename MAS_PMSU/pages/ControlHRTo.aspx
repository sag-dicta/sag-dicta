<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="ControlHRTo.aspx.vb" Inherits="MAS_PMSU.ControlHRTo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .label.label-warning {
            background-color: yellow;
            display: inline-block; /* Para que el fondo abarque toda la etiqueta */
            padding: 0px; /* Ajusta según sea necesario */
        }
    </style>
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Control de Humedad y Temperatura</h1>
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
                                    <label>Seleccione Fecha desde:</label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaDesde" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="txtFechaDesde_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Seleccione Fecha hasta:</label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaHasta" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="txtFechaHasta_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <%--<asp:Label ID="Label2" runat="server" CssClass="label label-warning" Text="Para crear un plan nuevo primero seleccione el departamento, el municipio y el multiplicador" />--%>
                                <asp:Button ID="BAgregar" runat="server" Text="Agregar Nuevo Control" CssClass="btn btn-success" Visible="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <h4>
                                        <span style="float: right;"><small># Control humedad y temperatura:</small>
                                            <asp:Label ID="lblTotalClientes" runat="server" CssClass="label label-warning" /></span>
                                    </h4>
                                    <p>&nbsp;</p>
                                    <p>&nbsp;</p>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connSAG %>" ProviderName="<%$ ConnectionStrings:connSAG.ProviderName %>"></asp:SqlDataSource>
                                    <asp:GridView ID="GridDatos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                                        GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataSourceID="SqlDataSource1" Font-Size="Small" OnRowCreated="grvMergeHeader_RowCreated">
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

                                            <asp:BoundField DataField="ID">
                                                <HeaderStyle CssClass="hide" />
                                                <ItemStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_monitoreo" HeaderText="FECHA DE MONITOREO" />
                                            <asp:BoundField DataField="camara1_hora" HeaderText="HORA 1" />
                                            <asp:BoundField DataField="camara1_temperatura" HeaderText="TEMPERATURA 1" />
                                            <asp:BoundField DataField="camara1_humedad" HeaderText="HUMEDAD 1" />
                                            <asp:BoundField DataField="camara2_hora" HeaderText="HORA 2" />
                                            <asp:BoundField DataField="camara2_temperatura" HeaderText="TEMPERATURA 2" />
                                            <asp:BoundField DataField="camara2_humedad" HeaderText="HUMEDAD 2" />
                                            <asp:BoundField DataField="camara3_hora" HeaderText="HORA 3" />
                                            <asp:BoundField DataField="camara3_temperatura" HeaderText="TEMPERATURA 3" />
                                            <asp:BoundField DataField="camara3_humedad" HeaderText="HUMEDAD 3" />
                                            <asp:BoundField DataField="camara4_hora" HeaderText="HORA 4" />
                                            <asp:BoundField DataField="camara4_temperatura" HeaderText="TEMPERATURA 4" />
                                            <asp:BoundField DataField="camara4_humedad" HeaderText="HUMEDAD 4" />
                                            <asp:BoundField DataField="camara5_hora" HeaderText="HORA 5" />
                                            <asp:BoundField DataField="camara5_temperatura" HeaderText="TEMPERATURA 5" />
                                            <asp:BoundField DataField="camara5_humedad" HeaderText="HUMEDAD 5" />
                                            <asp:BoundField DataField="camara6_hora" HeaderText="HORA 6" />
                                            <asp:BoundField DataField="camara6_temperatura" HeaderText="TEMPERATURA 6" />
                                            <asp:BoundField DataField="camara6_humedad" HeaderText="HUMEDAD 6" />
                                            <asp:BoundField DataField="externo_temperatura" HeaderText="TEMPERATURA EXTERNA" />
                                            <asp:BoundField DataField="externo_humedad" HeaderText="HUMEDAD EXTERNA" />

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

    <div id="DivCrearNuevo" runat="server" visible="true">
        <div class="row">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    Control segun Camara
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Fecha del Monitoreo:</label>
                                <asp:Label ID="lblfechamoni" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="TxtFechaMonitoreo" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="True"></asp:TextBox>
                                <asp:TextBox ID="Textid" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <asp:Label ID="lblmensaje" class="label label-warning" runat="server" Text=""></asp:Label>
                    <div class="row">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>Cámara 1:</label><br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Temperatura:</label>
                                <asp:TextBox ID="txtCam1Temp" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Humedad:</label>
                                <asp:TextBox ID="txtCam1Humd" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Hora:</label>
                                <asp:TextBox ID="txtCam1Hora" runat="server" CssClass="form-control" TextMode="Time" AutoPostBack="True" onBlur="formatTime"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label style="color: #FFFFFF">Borrar</label>
                                <asp:Button CssClass="btn btn-primary" ID="btnHora1" runat="server" AutoPostBack="True" Text="↺" Visible="false"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>Cámara 2:</label><br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Temperatura:</label>
                                <asp:TextBox ID="txtCam2Temp" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Humedad:</label>
                                <asp:TextBox ID="txtCam2Humd" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Hora:</label>
                                <asp:TextBox ID="txtCam2Hora" runat="server" CssClass="form-control" TextMode="Time" AutoPostBack="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label style="color: #FFFFFF">Borrar</label>
                                <asp:Button CssClass="btn btn-primary" ID="btnhora2" runat="server" AutoPostBack="True" Text="↺" Visible="false"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>Cámara 3:</label><br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Temperatura:</label>
                                <asp:TextBox ID="txtCam3Temp" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Humedad:</label>
                                <asp:TextBox ID="txtCam3Humd" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Hora:</label>
                                <asp:TextBox ID="txtCam3Hora" runat="server" CssClass="form-control" TextMode="Time" AutoPostBack="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label style="color: #FFFFFF">Borrar</label>
                                <asp:Button CssClass="btn btn-primary" ID="btnhora3" runat="server" AutoPostBack="True" Text="↺" Visible="false"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>Cámara 4:</label><br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Temperatura:</label>
                                <asp:TextBox ID="txtCam4Temp" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Humedad:</label>
                                <asp:TextBox ID="txtCam4Humd" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Hora:</label>
                                <asp:TextBox ID="txtCam4Hora" runat="server" CssClass="form-control" TextMode="Time" AutoPostBack="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label style="color: #FFFFFF">Borrar</label>
                                <asp:Button CssClass="btn btn-primary" ID="btnhora4" runat="server" AutoPostBack="True" Text="↺" Visible="false"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>Cámara 5:</label><br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Temperatura:</label>
                                <asp:TextBox ID="txtCam5Temp" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Humedad:</label>
                                <asp:TextBox ID="txtCam5Humd" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Hora:</label>
                                <asp:TextBox ID="txtCam5Hora" runat="server" CssClass="form-control" TextMode="Time" AutoPostBack="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label style="color: #FFFFFF">Borrar</label>
                                <asp:Button CssClass="btn btn-primary" ID="btnhora5" runat="server" AutoPostBack="True" Text="↺" Visible="false"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>Cámara 6:</label><br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Temperatura:</label>
                                <asp:TextBox ID="txtCam6Temp" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Humedad:</label>
                                <asp:TextBox ID="txtCam6Humd" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Hora:</label>
                                <asp:TextBox ID="txtCam6Hora" runat="server" CssClass="form-control" TextMode="Time" AutoPostBack="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label style="color: #FFFFFF">Borrar</label>
                                <asp:Button CssClass="btn btn-primary" ID="btnhora6" runat="server" AutoPostBack="True" Text="↺" Visible="false"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <br />
                    
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Datos Externos:</label><br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Temperatura:</label>
                                <asp:Label ID="Labeldatoexttemp" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtDatoExtTemp" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Humedad:</label>
                                <asp:Label ID="Labeldatoexthume" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtDatoExtHume" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <br />
                    
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <div>
                                    <asp:Label ID="LabelGuardar" class="label label-warning" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <div>
                                    <label></label>
                                    <asp:Button CssClass="btn btn-primary" ID="btnGuardarLote" runat="server" Text="" OnClick="guardarSoli_lote" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <div>
                                    <asp:Button CssClass="btn btn-primary" ID="btnRegresar" runat="server" Text="Regresar" OnClick="guardarSoli_lote" Visible="false" />
                                </div>
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
            <asp:Button CssClass="btn btn-success" ID="Button2" runat="server" Text="Nuevo Multiplicador" OnClick="vaciar" Visible="false" />
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
    <script>
        function formatTime() {
            var textBox = document.getElementById('<%= txtCam1Hora.ClientID %>');
            if (textBox.value.trim() === '') {
                textBox.value = '--:--';
            }
        }
    </script>

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
</asp:Content>
