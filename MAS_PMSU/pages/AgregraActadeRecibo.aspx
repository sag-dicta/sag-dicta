﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="AgregraActadeRecibo.aspx.vb" Inherits="MAS_PMSU.AgregraActadeRecibo" %>

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
            <h1 class="page-header">Acta de Recepcion de Semilla</h1>
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
                                <label>Seleccione Multiplicador:</label>
                                <asp:DropDownList CssClass="form-control" ID="TxtProductorGrid" runat="server" AutoPostBack="True">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Seleccione Numero de lote:</label>
                                <asp:DropDownList CssClass="form-control" ID="DDL_SelCLote" runat="server" AutoPostBack="True">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Seleccione Variedad:</label>
                                <asp:DropDownList CssClass="form-control" ID="ddlvariedad" runat="server" AutoPostBack="True">
                                    <asp:ListItem Text="Todos"></asp:ListItem>
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
                            <asp:Label ID="Label2" runat="server" CssClass="label label-warning" Text="Para crear una nueva acta primero seleccione el multiplicador, el lote y variedad" />
                            <br />
                            <asp:Button ID="BAgregar" runat="server" Text="Agregar Acta" CssClass="btn btn-success" Visible="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <h4>
                                    <span style="float: right;"><small># Actas:</small>
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
                                        ¡No hay actas de recepción inscrita!
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
                                        <asp:BoundField DataField="nombre_multiplicador" HeaderText="NOMBRE DEL MULTIPLICADOR" />
                                        <asp:BoundField DataField="departamento" HeaderText="DEPARTAMENTO" />
                                        <asp:BoundField DataField="tipo_cultivo" HeaderText="TIPO DE CULTIVO" />
                                        <asp:BoundField DataField="variedad" HeaderText="VARIEDAD" />
                                        <asp:BoundField DataField="categoria_origen" HeaderText="CATEGORÍA" />
                                        <asp:BoundField DataField="no_lote" HeaderText="N° DE LOTE" />
                                        <asp:BoundField DataField="lote_registrado" HeaderText="N° LOTE REGISTRADO" />
                                        <asp:BoundField DataField="porcentaje_humedad" HeaderText="% DE HUMEDAD" />
                                        <asp:BoundField DataField="no_sacos" HeaderText="N° DE SACOS" />
                                        <asp:BoundField DataField="peso_humedo_QQ" HeaderText="PESO HUMEDO (QQ)" />
                                        <asp:BoundField DataField="ciclo_acta" HeaderText="CICLO" />
                                        
                                        <asp:ButtonField ButtonType="Button" Text="Subir" ControlStyle-CssClass="btn btn-dark" HeaderText="Acta Firmada" CommandName="Subir">
                                            <ControlStyle CssClass="btn btn-dark"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Editar" ControlStyle-CssClass="btn btn-warning" HeaderText="EDITAR" CommandName="Editar">
                                            <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Eliminar" ControlStyle-CssClass="btn btn-danger" HeaderText="ELIMINAR" CommandName="Eliminar">
                                            <ControlStyle CssClass="btn btn-danger"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Imprimir" ControlStyle-CssClass="btn btn-warning" HeaderText="IMPRIMIR" CommandName="Imprimir">
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
                    Datos del Acta de Recepción de Semilla
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txt">ID:</label>
                                <asp:TextBox CssClass="form-control" ID="TxtID" runat="server" AutoPostBack="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="txtlega" runat="server" AutoPostBack="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="txtnum" runat="server" AutoPostBack="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="Txtcount" runat="server" AutoPostBack="false"></asp:TextBox>
                                <asp:TextBox ID="TextIdlote2" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label for="txt">Fecha de recepción:</label>
                                <asp:Label ID="lblFecha" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtFechaSiembra" TextMode="date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txt">Procedencia:</label>
                                <asp:Label ID="lblProcedencia" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtProcedencia" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="Textrespaldo" runat="server" AutoPostBack="false" Visible="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="Textdepart" runat="server" AutoPostBack="false" Visible="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="Textmuni" runat="server" AutoPostBack="false" Visible="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="Textalde" runat="server" AutoPostBack="false" Visible="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="Textcase" runat="server" AutoPostBack="false" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txt">Productor:</label>
                                <asp:Label ID="lblProductor" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtProductor" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txt">Cultivo:</label>
                                <asp:Label ID="lblCultivo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtCultivo" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txt">Variedad:</label>
                                <asp:Label ID="lblVariedad" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtVariedad" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txt">Categoria:</label>
                                <asp:Label ID="lblCategoria" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:DropDownList CssClass="form-control" ID="categoria_origen_ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="verificar">
                                    <asp:ListItem Text=" "></asp:ListItem>
                                    <asp:ListItem id="basica1" Text="Basica"></asp:ListItem>
                                    <asp:ListItem id="registrada1" Text="Registrada"></asp:ListItem>
                                    <asp:ListItem id="certificada1" Text="Certificada"></asp:ListItem>
                                    <asp:ListItem id="comercial1" Text="Comercial"></asp:ListItem>
                                    <asp:ListItem id="Fito_Mejorador1" Text="Fito Mejorador"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txt">No. Lote:</label>
                                <asp:Label ID="lblLote" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtLote" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>% Humedad:</label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator42" runat="server" ControlToValidate="txtHumedad" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                <asp:Label ID="lblHumedad" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtHumedad" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>No. de Sacos:</label>
                                <asp:Label ID="lblSacos" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtSacos" CssClass="form-control" runat="server" TextMode="number" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Peso Humedo (QQ):</label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator43" runat="server" ControlToValidate="txtPesoH" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                <asp:Label ID="lblPesoH" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtPesoH" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Seleccione Ciclo:</label>
                                <asp:Label ID="Labelciclo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:DropDownList CssClass="form-control" ID="DDL_Ciclo" runat="server" AutoPostBack="True" OnTextChanged="Verificar">
                                    <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txt">No. Lote Semilla Registrada:</label>
                                <asp:Label ID="lblLoteRegi" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtLoteRegi" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="div_nuevo_prod" runat="server" visible="false">

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">

                                <div class="form-group">
                                    <h4>Subir archivo</h4>
                                    <div class="mb-3">
                                        <label for="FileUploadPDF" class="form-label">Acta de Recepción Firmada:</label>
                                        <asp:Label ID="LabelPDF" runat="server" Text="" BackColor="Red" ForeColor="White" Visible="false">Solo archivos PNG/JPG/JPEG se aceptan</asp:Label>
                                        <asp:FileUpload ID="FileUploadPDF" runat="server" class="form-control" accept=".pdf" />
                                    </div>
                                    <br />

                                    <asp:Label ID="Label23" runat="server" Text="" BackColor="Red" ForeColor="White" Visible="false">Antes debes ingresar toda la información</asp:Label>
                                    <asp:Label ID="Label25" runat="server" Text="" BackColor="Green" ForeColor="White" Visible="false">Archivos ingresados con exito</asp:Label>
                                    <br />
                                    <asp:Button ID="BtnUpload" runat="server" Text="Guardar" OnClick="BtnUpload_Click" AutoPostBack="false" class="btn btn-primary" />
                                    <asp:Button ID="Button1" runat="server" Text="Regresar" AutoPostBack="True" class="btn btn-primary" />
                                    <hr />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="DeleteModal4" tabindex="-1" role="dialog" aria-labelledby="ModalTitle5" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="ModalTitle5">SAG - DICTA</h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="Label26" runat="server" Text="El productor no tiene ningun lote registrado. ¿Desea agregarlo?"></asp:Label>
                    </div>
                    <div class="modal-footer" style="text-align: center">
                        <asp:Button ID="Button6" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" />
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
                <asp:Button CssClass="btn btn-primary" ID="btnGuardarActa" runat="server" Text="Guardar " Visible="false" OnClick="btnGuardarActa_Click" />
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
                    <h4 class="modal-title" id="ModalTitle2">SAG DICTA</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label103" runat="server" Text="El Acta de Recepcion de semilla ha sido almacenada con exito"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="BConfirm" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" OnClick="BConfirm_Click" />
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
