﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="InscripcionLotes.aspx.vb" Inherits="MAS_PMSU.InscripcionLotes" %>

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
            <h1 class="page-header">Registro de Multiplicador o Estación</h1>
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
                                <asp:Button ID="BAgregar" runat="server" Text="Agregar Inscripcion" CssClass="btn btn-success" Visible="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <h4>
                                        <span style="float: right;"><small># Multiplicadores:</small>
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
                    A. DATOS PERSONALES
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Nombre Del Productor</label><asp:Label ID="lb_nombre_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txt_nombre_prod_new" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Representante Legal</label><asp:Label ID="LB_RepresentanteLegal" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="Txt_Representante_Legal" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Cedula de Identidad</label><asp:Label ID="Lb_CedulaIdentidad" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtIdentidad" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Extendida En :</label><asp:Label ID="Label1" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TextBox1" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Residencia</label><asp:Label ID="LbResidencia" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtResidencia" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Telefono</label><asp:Label ID="LblTelefono" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtTelefono" runat="server" AutoPostBack="true" MaxLength="9" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>No. De Registro de Productor</label><asp:Label ID="LbNoRegistro" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtNoRegistro" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Nombre del Multiplicador o Reproductor:</label><asp:Label ID="lbNombreRe" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtNombreRe" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Identidad del Multiplicador:</label><asp:Label ID="lbIdentidadRe" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtIdentidadRe" runat="server" AutoPostBack="true" MaxLength="13" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Telefono del Multiplicador:</label><asp:Label ID="LbTelefonoRe" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtTelefonoRe" runat="server" AutoPostBack="true" MaxLength="8" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="row">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    B. UBICACION GEOGRAFICA
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Nombre De la finca </label>
                                <asp:Label ID="LblNombreFinca" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtNombreFinca" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>

                        <section id="todoDeptos" runat="server">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Departamento</label>
                                        <asp:Label ID="lb_dept_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtCodDep" runat="server" AutoPostBack="true" ReadOnly="true" Visible="false"></asp:TextBox>
                                        <asp:DropDownList CssClass="form-control" ID="gb_departamento_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                            <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Municipio</label><asp:Label ID="lb_mun_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TxtCodMun" runat="server" AutoPostBack="true" ReadOnly="true" Visible="false"></asp:TextBox>
                                        <asp:DropDownList CssClass="form-control" ID="gb_municipio_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox" Enabled="false">
                                            <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Aldea</label>
                                        <asp:Label ID="lb_aldea_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList CssClass="form-control" ID="gb_aldea_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox" Enabled="false">
                                            <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Caserio</label>
                                        <asp:Label ID="lb_caserio_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList CssClass="form-control" ID="gb_caserio_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox" Enabled="false">
                                            <asp:ListItem Text=" " Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </section>

                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Encargado de la finca</label><asp:Label ID="LblPersonaFinca" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtPersonaFinca" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Nombre o numero de Lote</label><asp:Label ID="LbLote" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtLote" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Croquis del Lote:</label>
                                <asp:Label ID="Label5" class="label label-warning" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="Label25" runat="server" Text="" class="label label-warning" Visible="false">Subir archivo PNG/JPG/JPEG</asp:Label>
                                <!-- Agrega el control FileUpload para cargar una imagen -->
                                <asp:FileUpload ID="fileUpload" runat="server" PostBackUrl="SolicitudInscripcionDeLotes.aspx" accept=".png,.jpg,.jpeg" />
                                <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>

        <asp:UpdatePanel runat="server" ID="Updatepanel666">
            <ContentTemplate>
                <div class="row" id="PanelC" runat="server" visible="true">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            C. ORIGEN DE LA SEMILLA A SEMBRAR
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Cultivo</label><asp:Label ID="Label2" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList CssClass="form-control" ID="CmbTipoSemilla" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CmbTipoSemilla_SelectedIndexChanged">
                                            <asp:ListItem Text=""></asp:ListItem>
                                            <asp:ListItem id="frijol" Text="Frijol"></asp:ListItem>
                                            <asp:ListItem id="maiz" Text="Maiz"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <section id="Section1" runat="server">
                                    <div class="col-lg-3" id="VariedadFrijol" runat="server" visible="false">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label>Variedad Frijol</label>
                                                <asp:Label ID="Label4" class="label label-warning" runat="server" Text=""></asp:Label>
                                                <asp:DropDownList CssClass="form-control" ID="DropDownList5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VerificarTextBox">
                                                    <asp:ListItem Text=""></asp:ListItem>
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
                                            <label>Variedades Maiz</label><asp:Label ID="Label6" class="label label-warning" runat="server" Text=""></asp:Label>
                                            <asp:DropDownList CssClass="form-control" ID="DropDownList6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VerificarTextBox">
                                                <asp:ListItem Text=""></asp:ListItem>
                                                <asp:ListItem id="DictaMayav1" Text="Dicta Maya"></asp:ListItem>
                                                <asp:ListItem id="DictaVictoriav1" Text="Dicta Victoria"></asp:ListItem>
                                                <asp:ListItem id="OtroMaizv1" Text="Otro"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </section>

                                <div class="col-lg-3">
                                    <div class="form-group">

                                        <label>Productor</label><asp:Label ID="Label22" class="label label-warning" runat="server" Text=""></asp:Label>

                                        <asp:TextBox CssClass="form-control" ID="txtprodsem" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">

                                        <label>Lote No.</label><asp:Label ID="Label8" class="label label-warning" runat="server" Text=""></asp:Label>

                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Fecha de Analisis</label><asp:Label ID="Label9" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Año de Producción </label>
                                        <asp:Label ID="Label10" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="Updatepanel1">
            <ContentTemplate>
                <div class="row" id="PanelD" runat="server" visible="true">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            D. SEMILLA A PRODUCIR
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Categoria</label><asp:Label ID="Label7" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList CssClass="form-control" ID="DdlCategoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VerificarTextBox">
                                            <asp:ListItem Text=""></asp:ListItem>
                                            <asp:ListItem id="basica" Text="Basica"></asp:ListItem>
                                            <asp:ListItem id="registrada" Text="Registrada"></asp:ListItem>
                                            <asp:ListItem id="certificada" Text="Certificada"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <section id="Section3" runat="server">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label>Tipo</label>
                                                <asp:Label ID="Label11" class="label label-warning" runat="server" Text=""></asp:Label>
                                                <asp:DropDownList CssClass="form-control" ID="DdlTipo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VerificarTextBox">
                                                    <asp:ListItem Text=""></asp:ListItem>
                                                    <asp:ListItem id="linea" Text="Linea"></asp:ListItem>
                                                    <asp:ListItem id="variedad" Text="Variedad"></asp:ListItem>
                                                    <asp:ListItem id="hibrido" Text="Hibrido"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </section>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Cultivo</label><asp:Label ID="Label12" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList CssClass="form-control" ID="DropDownList3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                            <asp:ListItem Text=""></asp:ListItem>
                                            <asp:ListItem id="frijolcultivo" Text="Frijol"></asp:ListItem>
                                            <asp:ListItem id="maizcultivo" Text="Maiz"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <section id="Section4" runat="server">
                                    <div class="col-lg-3" visible="false" id="variedadfrijol2" runat="server">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label>Variedad Frijol</label>
                                                <asp:Label ID="Label15" class="label label-warning" runat="server" Text=""></asp:Label>
                                                <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VerificarTextBox">
                                                    <asp:ListItem Text=""></asp:ListItem>
                                                    <asp:ListItem id="Amadeus77v" Text="Amadeus-77"></asp:ListItem>
                                                    <asp:ListItem id="Carrizalitov" Text="Carrizalito"></asp:ListItem>
                                                    <asp:ListItem id="Azabachev" Text="Azabache"></asp:ListItem>
                                                    <asp:ListItem id="Paraisitomejoradov" Text="Paraisito mejorado PM-2"></asp:ListItem>
                                                    <asp:ListItem id="Deorhov" Text="Deorho"></asp:ListItem>
                                                    <asp:ListItem id="IntaCardenasv" Text="Inta Cárdenas"></asp:ListItem>
                                                    <asp:ListItem id="Lencaprecozv" Text="Lenca precoz"></asp:ListItem>
                                                    <asp:ListItem id="Rojochortív" Text="Rojo chortí"></asp:ListItem>
                                                    <asp:ListItem id="Tolupanrojov" Text="Tolupan rojo"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </section>

                                <section id="Section5" runat="server">
                                    <div class="col-lg-3" visible="false" id="variedadmaiz2" runat="server">
                                        <div class="form-group">
                                            <label>Variedades Maiz</label><asp:Label ID="Label16" class="label label-warning" runat="server" Text=""></asp:Label>
                                            <asp:DropDownList CssClass="form-control" ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VerificarTextBox">
                                                <asp:ListItem Text=""></asp:ListItem>
                                                <asp:ListItem id="DictaMayav" Text="Dicta Maya"></asp:ListItem>
                                                <asp:ListItem id="DictaVictoriav" Text="Dicta Victoria"></asp:ListItem>
                                                <asp:ListItem id="OtroMaizv" Text="Otro"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </section>
                            </div>


                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Superficie a Sembrar Ha:</label><asp:Label ID="Label13" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TxtHectareas" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" CausesValidation="false" onkeypress="return numericOnly(this);"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtHectareas" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Superficie en MZ</label><asp:Label ID="Label14" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TxtSuperficieMZ" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtSuperficieMZ" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Fecha Aproximada de Siembra </label>
                                        <asp:Label ID="Label17" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TxtFechaSiembra" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Fecha Aproximada de Cosecha</label>
                                        <asp:Label ID="Label19" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TxtCosecha" TextMode="date" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Produccion Estimada por Ha</label>
                                        <asp:Label ID="Label20" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TxtProHectareas" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtProHectareas" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Produccion Estimada por Mz</label>
                                        <asp:Label ID="Label21" class="label label-warning" runat="server" Text=""></asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" Enabled="false"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegexValidator" runat="server" ControlToValidate="TextBox7" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="Ingresa un número válido." Display="Dynamic" Style="color: red;" />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">

                                        <label>Destino</label><asp:Label ID="Label24" class="label label-warning" runat="server" Text=""></asp:Label>

                                        <asp:DropDownList CssClass="form-control" ID="DropDownList4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VerificarTextBox">
                                            <asp:ListItem Text=""></asp:ListItem>
                                            <asp:ListItem id="mercado" Text="Mercado Local"></asp:ListItem>
                                            <asp:ListItem id="exportacion" Text="Exportación"></asp:ListItem>
                                            <asp:ListItem id="ambas" Text="Ambas"></asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

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
