<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Embarque_DescArch.aspx.vb" Inherits="MAS_PMSU.Embarque_DescArch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .label.label-warning {
            background-color: yellow;
            display: inline-block; /* Para que el fondo abarque toda la etiqueta */
            padding: 0px; /* Ajusta según sea necesario */
        }

        .modal-dialog {
            max-width: 90%; /* Establecer un ancho máximo para la modal */
            width: auto;
        }

        .modal-content {
            width: 100%; /* Ajustar el contenido al ancho máximo de la modal */
            height: auto; /* Permitir que la altura se ajuste automáticamente */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Archivos de Salidas Firmados</h1>
        </div>
    </div>

    <div id="DivGrid" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Tipo de Salida:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLTipoSalida" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLTipoSalida_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Convenio"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Distribución y embarque"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Actas"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Fecha desde:</label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaDesde" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="txtFechaDesde_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Fecha hasta:</label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaHasta" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="txtFechaHasta_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Destinatario:</label>
                                    <asp:DropDownList CssClass="form-control" ID="TxtMultiplicador" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TxtMultiplicador_SelectedIndexChanged">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>No. Conocimiento:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLConoc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLConoc_SelectedIndexChanged">
                                        <asp:ListItem Text="Todos"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
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
                                            <asp:BoundField DataField="tipo_salida" HeaderText="TIPO DE SALIDA" />
                                            <asp:BoundField DataField="no_convenio" HeaderText="N° DE CONVENIO" />
                                            <asp:BoundField DataField="no_conocimiento" HeaderText="N° DE CONOCIMIENTO" />
                                            <asp:BoundField DataField="para_general" HeaderText="DIRIGIDO A" />
                                            <asp:BoundField DataField="fecha_elaboracion" HeaderText="FECHA DE ELABORACION" />
                                            <asp:BoundField DataField="cultivo_general" HeaderText="CULTIVO GENERAL" />
                                            <asp:BoundField DataField="lugar_destinatario" HeaderText="LUGAR DESTINATARIO" />
                                            <asp:BoundField DataField="remitente" HeaderText="REMITENTE" />
                                            <asp:BoundField DataField="conductor" HeaderText="TRANSPORTISTA" />

                                            <asp:ButtonField ButtonType="Button" Text="Descargar" ControlStyle-CssClass="btn btn-success" HeaderText="SALIDA FIRMADA" CommandName="FichaLote">
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

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>

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

    <script type="text/javascript">
        $(document).ready(function () {
            $('#DeleteModal').on('hidden.bs.modal', function () {
                // Cuando la modal se cierra, redirige a la página deseada
                window.location.href = 'Embarque.aspx';
            });
        });
    </script>
</asp:Content>
