<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="AnalisisGerminacion.aspx.vb" Inherits="MAS_PMSU.AnalisisGerminacion" %>

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
            <h1 class="page-header">Análisis de Germinación de Muestras de Semillas</h1>
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
                                    <span style="float: right;"><small># Análisis de germinación:</small>
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
                                        ¡No hay análisis de germinación con esas caracteristicas!
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
                                        <asp:BoundField DataField="categoria_registrado" HeaderText="CATEGORÍA" />
                                        <asp:BoundField DataField="lote_registrado" HeaderText="N° DE LOTE" />
                                        <asp:BoundField DataField="ciclo_acta" HeaderText="CICLO" />
                                        
                                        <asp:BoundField DataField="porcentaje_humedad" HeaderText="% DE HUMEDAD DE INGRESO" />
                                        <asp:BoundField DataField="humedad_final" HeaderText="% DE HUMEDAD DE FINAL" />
                                        <asp:BoundField DataField="peso_inicial_g" HeaderText="PESO INICIAL EN PLANTA (QQ)" />
                                        <asp:BoundField DataField="cantidad_existente" HeaderText="CANTIDAD EXISTENTE" />                                       
                                        <asp:BoundField DataField="PORCENTAJE_GERMINACION" HeaderText="% DE GERMINACIÓN" />
                                        <asp:BoundField DataField="decision" HeaderText="DECISION" />

                                        
                                        <asp:ButtonField ButtonType="Button" Text="Subir" ControlStyle-CssClass="btn btn-dark" HeaderText="ANÁLISIS FIRMADO" CommandName="Subir">
                                            <ControlStyle CssClass="btn btn-dark"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" Text="Editar" ControlStyle-CssClass="btn btn-success" HeaderText="EDITAR" CommandName="Editar">
                                            <ControlStyle CssClass="btn btn-success"></ControlStyle>
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
                        <div class="col-lg-2">
                            <%--<asp:Button ID="Button1" runat="server" Text="Exportar Datos" CssClass="btn btn-success" />--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning" Text="Exportar Datos"><span class="glyphicon glyphicon-save"></span>&nbsp;Exportar Datos</asp:LinkButton>
                        </div>
                        <div class="col-lg-4">
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary" Text="Ver los Archivos Subidos"><span class="glyphicon glyphicon-save"></span>&nbsp;Ver los Archivos Subidos</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="DivActaInfo" runat="server" visible="false">
        <div id="DivActaInfo1" runat="server">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Información del Lote
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-4" runat="server" visible="false">
                                <div class="form-group">
                                    <label for="txt">ID:</label>
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" AutoPostBack="false"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="txtlega" runat="server" AutoPostBack="false"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="txtnum" runat="server" AutoPostBack="false"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="Txtcount" runat="server" AutoPostBack="false"></asp:TextBox>
                                    <asp:TextBox ID="TextIdlote2" runat="server" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Fecha de elaboración:</label>
                                    <asp:Label ID="lblFechaElab" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaElab" TextMode="date" runat="server" Enabled="true" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Cultivo:</label>
                                    <asp:Label ID="lblCultivo" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCultivo" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Variedad:</label>
                                    <asp:Label ID="lblVariedad" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtVariedad" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Hibrido:</label>
                                    <asp:Label ID="lblHibrido" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtHibrido" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Categoria:</label>
                                    <asp:Label ID="lblCategoria" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtCategoria" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Año:</label>
                                    <asp:Label ID="lblaño" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtaño" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label for="txt">Productor:</label>
                                    <asp:Label ID="lblProductor" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtProductor" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label for="txt">Procedencia:</label>
                                    <asp:Label ID="lblProcedencia" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtProcedencia" runat="server" AutoPostBack="false" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label for="txt">Lote No.:</label>
                                    <asp:Label ID="lblLoteRegi" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtLoteRegi" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Ciclo:</label>
                                    <asp:Label ID="lblciclo2" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="Textciclo2" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Departamento:</label>
                                    <asp:Label ID="lblDepartamento" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtDepartamento" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Municipio:</label>
                                    <asp:Label ID="lblMunicipio" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtMunicipio" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Locallidad/Aldea:</label>
                                    <asp:Label ID="lblLocallidad" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtLocallidad" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="DivActaInfo2" runat="server">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Peso Inicial Estacion Experimental
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Peso Humedo (QQ):</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator43" runat="server" ControlToValidate="txtPesoH" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblPesoH" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtPesoH" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>No. de Sacos:</label>
                                    <asp:Label ID="lblSacos" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtSacos" CssClass="form-control" runat="server" TextMode="number" OnTextChanged="Verificar" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>No. Envase:</label>
                                    <asp:Label ID="lblEnvase" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtEnvase" CssClass="form-control" runat="server" TextMode="number" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="DivActaInfo3" runat="server">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Peso Inicial Planta
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>Peso Inicial (QQ):</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPesoInicialPlanta" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblPesoInicialPlanta" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtPesoInicialPlanta" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Tipo:</label>
                                    <asp:Label ID="lblGranel" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLGranel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Verificar">
                                        <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Granel en Sacos" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Granel en Bolsas" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="En Mazorca" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="DivActaInfo4" runat="server">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Otra Información
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Fecha de Recibo:</label>
                                    <asp:Label ID="lblFechaRecibo" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaRecibo" TextMode="date" runat="server" Enabled="true" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Fecha de Muestreo:</label>
                                    <asp:Label ID="lblFechaMuestreo" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaMuestreo" TextMode="date" runat="server" Enabled="true" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>% Humedad Entrada:</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator42" runat="server" ControlToValidate="txtHumedad" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblHumedad" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtHumedad" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>% Humedad Final:</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtHumedadF" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblHumedadF" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtHumedadF" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Fecha de Siembra:</label>
                                    <asp:Label ID="lblFecha" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaSiembra" TextMode="date" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Fecha de Evaluación:</label>
                                    <asp:Label ID="lblFechaEval" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaEval" TextMode="date" runat="server" Enabled="true" OnTextChanged="Verificar" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="DivActaInfo5" runat="server">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Envasado, Fase, Tamaño y otros
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Tipo de Envase:</label>
                                    <asp:Label ID="lblEnvasado" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLEnvasado" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Verificar">
                                        <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        <asp:ListItem Text="En Sacos" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="En Bolsas" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Fase:</label>
                                    <asp:Label ID="lblFase" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLFase" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Verificar">
                                        <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Al Recibo" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Al Secado" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Al Desgrane" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Sin Procesar" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Procesado" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Tratado" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="En Almacen" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Tamaño Maiz:</label>
                                    <asp:Label ID="lblTamañoMaiz" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList CssClass="form-control" ID="DDLTamañoMaiz" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Verificar" Enabled="false">
                                        <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        <asp:ListItem Text="GP" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="MP" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="PP" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="GR" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="MR" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="PR" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>Cantidad Inicial (QQ):</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtCantInicial" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblCantInicial" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtCantInicial" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>Cantidad Existente/Final (QQ):</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtCantExistente" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblCantExistente" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtCantExistente" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Camara No.:</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtCamaraNo" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblCamaraNo" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtCamaraNo" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Perimetro:</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtPerimetro" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblPerimetro" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtPerimetro" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="DivActaInfo6" runat="server">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Último Analisis
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>CERTISEM %:</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtCERTISEM" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblCERTISEM" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtCERTISEM" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Fecha CERTISEM:</label>
                                    <asp:Label ID="lblFechaCERTISEM" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaCERTISEM" TextMode="date" runat="server" Enabled="true" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Planta:</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtPlanta" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    <asp:Label ID="lblPlanta" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtPlanta" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label for="txt">Fecha Planta %:</label>
                                    <asp:Label ID="lblFechaPlanta" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtFechaPlanta" TextMode="date" runat="server" Enabled="true" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                                </div>
                            </div>
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
                    Pureza y Germinación
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txt">ID:</label>
                                <asp:TextBox CssClass="form-control" ID="TxtID" runat="server" AutoPostBack="false"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="txtrespaldito" runat="server" AutoPostBack="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Semilla Pura %:</label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtSemillaPura" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                <asp:Label ID="lblSemillaPura" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtSemillaPura" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Semilla Otro Cultivo %:</label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtSemillaOtroCult" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                <asp:Label ID="lblSemillaOtroCult" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtSemillaOtroCult" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Semilla Malezas %:</label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtSemillaMalezas" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                <asp:Label ID="lblSemillaMalezas" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtSemillaMalezas" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Materia Inerte %:</label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtSemillaInerte" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                <asp:Label ID="lblSemillaInerte" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtSemillaInerte" CssClass="form-control" runat="server" onkeypress="return numericOnly(this);" OnTextChanged="Verificar" AutoPostBack="true" Enabled="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <asp:Label ID="lblmensaje" class="label label-warning" runat="server" Text=""></asp:Label>
                        
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Repetición 1:</label><br />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Normales:</label>
                                    <asp:TextBox ID="txtCam1PlanNorm" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Anormales:</label>
                                    <asp:TextBox ID="txtCam1PlanAnor" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Muertas:</label>
                                    <asp:TextBox ID="txtCam1SemiMuer" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semillas Duras:</label>
                                    <asp:TextBox ID="txtCam1SemiDura" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Debiles:</label>
                                    <asp:TextBox ID="txtCam1Debiles" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Mezcla:</label>
                                    <asp:TextBox ID="txtCam1Mezcla" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>N° de Dias:</label>
                                    <asp:TextBox ID="txtCam1NoDias" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Repetición 2:</label><br />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Normales:</label>
                                    <asp:TextBox ID="txtCam2PlanNorm" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Anormales:</label>
                                    <asp:TextBox ID="txtCam2PlanAnor" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Muertas:</label>
                                    <asp:TextBox ID="txtCam2SemiMuer" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semillas Duras:</label>
                                    <asp:TextBox ID="txtCam2SemiDura" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Debiles:</label>
                                    <asp:TextBox ID="txtCam2Debiles" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Mezcla:</label>
                                    <asp:TextBox ID="txtCam2Mezcla" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>N° de Dias:</label>
                                    <asp:TextBox ID="txtCam2NoDias" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />       
                        
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Repetición 3:</label><br />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Normales:</label>
                                    <asp:TextBox ID="txtCam3PlanNorm" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Anormales:</label>
                                    <asp:TextBox ID="txtCam3PlanAnor" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Muertas:</label>
                                    <asp:TextBox ID="txtCam3SemiMuer" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semillas Duras:</label>
                                    <asp:TextBox ID="txtCam3SemiDura" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Debiles:</label>
                                    <asp:TextBox ID="txtCam3Debiles" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Mezcla:</label>
                                    <asp:TextBox ID="txtCam3Mezcla" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>N° de Dias:</label>
                                    <asp:TextBox ID="txtCam3NoDias" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />       
                        
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Repetición 4:</label><br />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Normales:</label>
                                    <asp:TextBox ID="txtCam4PlanNorm" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Anormales:</label>
                                    <asp:TextBox ID="txtCam4PlanAnor" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Muertas:</label>
                                    <asp:TextBox ID="txtCam4SemiMuer" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semillas Duras:</label>
                                    <asp:TextBox ID="txtCam4SemiDura" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Debiles:</label>
                                    <asp:TextBox ID="txtCam4Debiles" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Mezcla:</label>
                                    <asp:TextBox ID="txtCam4Mezcla" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>N° de Dias:</label>
                                    <asp:TextBox ID="txtCam4NoDias" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Verificar" onkeypress="return numericOnly(this);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />  
                        <br />
                        
                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Totales:</label><br />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Normales:</label>
                                    <asp:TextBox ID="txtTotalPlanNorm" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Plantulas Anormales:</label>
                                    <asp:TextBox ID="txtTotalPlanAnor" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Muertas:</label>
                                    <asp:TextBox ID="txtTotalSemiMuer" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semillas Duras:</label>
                                    <asp:TextBox ID="txtTotalSemiDura" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Debiles:</label>
                                    <asp:TextBox ID="txtTotalDebiles" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>Semilla Mezcla:</label>
                                    <asp:TextBox ID="txtTotalMezcla" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>N° de Dias:</label>
                                    <asp:TextBox ID="txtTotalNoDias" runat="server" CssClass="form-control" AutoPostBack="True" onkeypress="return numericOnly(this);" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>    
                    </div>

                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtObserv">Porcentaje (%) de Germinación:</label>
                                <asp:Label ID="lblPorcGerm" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtPorcGerm" CssClass="form-control" runat="server" AutoPostBack="false" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label for="txtObserv">Observaciones:</label>
                                <asp:Label ID="lblObserv" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtObserv" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label for="txtObserv">Responsable del Muestreo:</label>
                                <asp:Label ID="lblRespMuestreo" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtRespMuestreo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label for="txtObserv">Responsable del Análisis:</label>
                                <asp:Label ID="lblRespAnalisis" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtRespAnalisis" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Verificar"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                                <div class="form-group">
                                    <label>Decisión:</label>
                                    <asp:Label ID="lbldecision" class="label label-warning" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList CssClass="form-control" ID="DDL_decision" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Verificar">
                                        <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        <asp:ListItem Text="APROBADO" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="RECHAZADO" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="PENDIENTE" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
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
                                        <label for="FileUploadPDF" class="form-label">Análisis De Germinación Firmado:</label>
                                        <asp:Label ID="LabelPDF" runat="server" Text="" BackColor="Red" ForeColor="White" Visible="false">Solo archivos PDF se aceptan</asp:Label>
                                        <asp:FileUpload ID="FileUploadPDF" runat="server" class="form-control" accept=".pdf" />
                                    </div>
                                    <br />

                                    <asp:Label ID="Label23" runat="server" Text="" BackColor="Red" ForeColor="White" Visible="false">Antes debes ingresar toda la información</asp:Label>
                                    <asp:Label ID="Label25" runat="server" Text="" BackColor="Green" ForeColor="White" Visible="false">Archivos ingresados con exito</asp:Label>
                                    <br />
                                    <asp:Button ID="BtnUpload" runat="server" Text="Guardar" OnClick="BtnUpload_Click" AutoPostBack="false" class="btn btn-primary" />
                                    <asp:Button ID="Button4" runat="server" Text="Regresar" AutoPostBack="True" class="btn btn-primary" />
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
                    <asp:Button ID="BConfirm" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" OnClick="BConfirm_Click" />
                    <asp:Button ID="BBorrarsi" Text="SI" Width="80px" runat="server" Class="btn btn-primary" />
                    <asp:Button ID="BBorrarno" Text="NO" Width="80px" runat="server" Class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="DeleteModal2" tabindex="-1" role="dialog" aria-labelledby="ModalTitle2" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalTitle3">SAG - DICTA</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label1" runat="server" Text="El Acta de Recepcion de semilla ha sido almacenada con exito"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="Button1" Text="Aceptar" Width="80px" runat="server" Class="btn btn-primary" />
                    <asp:Button ID="Button2" Text="SI" Width="80px" runat="server" Class="btn btn-primary" />
                    <asp:Button ID="Button3" Text="NO" Width="80px" runat="server" Class="btn btn-primary" />
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
