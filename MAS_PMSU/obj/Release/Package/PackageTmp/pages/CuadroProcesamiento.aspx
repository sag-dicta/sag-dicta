﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="CuadroProcesamiento.aspx.vb" Inherits="MAS_PMSU.CuadroProcesamiento" %>

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
            <h1 class="page-header">Cuadro de Procesamiento (Secado, limpieza y clasificación)</h1>
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
                                    <span style="float: right;"><small># Cuadros Procesamientos:</small>
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
                                        <asp:BoundField DataField="tipo_cultivo" HeaderText="TIPO DE CULTIVO" />
                                        <asp:BoundField DataField="variedad" HeaderText="VARIEDAD" />
                                        <asp:BoundField DataField="categoria_origen" HeaderText="CATEGORÍA" />
                                        <asp:BoundField DataField="no_lote" HeaderText="N° DE LOTE" />
                                        <asp:BoundField DataField="ciclo_acta" HeaderText="CICLO" />
                                        <asp:BoundField DataField="peso_humedo_QQ" HeaderText="PESO PRIMA EN LA PLANTA" />
                                        <asp:BoundField DataField="porcentaje_humedad" HeaderText="% DE HUMEDAD DE INGRESO" />
                                        <asp:BoundField DataField="peso_materia_prima_QQ_porce_humedad" HeaderText="PESO PRIMA AL 12% DE HUMEDAD (QQ)" />
                                        <asp:BoundField DataField="semilla_QQ_oro" HeaderText="SEMILLA ORO (QQ)" />
                                        <asp:BoundField DataField="semilla_QQ_consumo" HeaderText="SEMILLA CONSUMO (QQ)" />
                                        <asp:BoundField DataField="semilla_QQ_basura" HeaderText="SEMILLA BASURA (QQ)" />
                                        <asp:BoundField DataField="semilla_QQ_total" HeaderText="SEMILLA TOTAL (QQ)" />

                                        <asp:ButtonField ButtonType="Button" Text="observacion" ControlStyle-CssClass="btn btn-warning" HeaderText="Observaciones" CommandName="observacion">
                                            <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Editar" ControlStyle-CssClass="btn btn-warning" HeaderText="EDITAR" CommandName="Editar">
                                            <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Eliminar" ControlStyle-CssClass="btn btn-danger" HeaderText="ELIMINAR" CommandName="Eliminar">
                                            <ControlStyle CssClass="btn btn-danger"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Imprimir" ControlStyle-CssClass="btn btn-warning" HeaderText="IMPRIMIR" CommandName="Imprimir" >
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
        <div class="row">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Datos del Cuadro de Procesamiento
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txt">ID:</label>
                                <asp:TextBox CssClass="form-control" ID="TxtID" runat="server" AutoPostBack="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtPeso12Hum">Peso de Materia Prima al 12% de Humedad(QQ):</label>
                                <asp:Label ID="lblPeso12Hum" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtPeso12Hum" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtSemOro">Semilla Oro(QQ):</label>
                                <asp:Label ID="lblSemOro" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtSemOro" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtConsumo">Consumo(QQ):</label>
                                <asp:Label ID="lblConsumo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtConsumo" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtBasura">Basura(QQ):</label>
                                <asp:Label ID="lblBasura" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtBasura" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtTotal">Total(QQ):</label>
                                <asp:Label ID="lblTotal" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtTotal" CssClass="form-control" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label for="txtObserv">Observaciones</label>
                                <asp:Label ID="lblObserv" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtObserv" CssClass="form-control" runat="server" AutoPostBack="false"></asp:TextBox>
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
                    <asp:Button ID="BConfirm" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" OnClick="BConfirm_Click"/>
                    <asp:Button ID="BBorrarsi" Text="SI" Width="80px" runat="server" Class="btn btn-primary" />
                    <asp:Button ID="BBorrarno" Text="NO" Width="80px" runat="server" Class="btn btn-primary" />
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