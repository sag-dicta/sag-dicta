<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Registro_multiplicadores.aspx.vb" Inherits="MAS_PMSU.Registro_multiplicadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divdatos" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header"></h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        REGISTRO DE MULTIPLICADORES
                    </div>
                    <div class="panel-body">
                        <%--<form role="form" runat="server">--%>
                        <ul class="nav nav-pills">
                            <li class="active"><a href="#Datos" data-toggle="tab">Datos</a>
                            </li>
                            <%--<li><a href="#Graficos" data-toggle="tab">Graficos</a>
</li>--%>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="Datos">
                                <div class="panel-body">
                                    <div class="row">
                                        <asp:TextBox ID="txt_admin" runat="server" Visible="false"></asp:TextBox>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>Seleccione Departamento:</label>
                                                <asp:DropDownList CssClass="form-control" ID="TxtDepto" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>Seleccione Entrenador:</label>
                                                <asp:DropDownList CssClass="form-control" ID="TxtEntrenador" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>Seleccione Organización:</label>
                                                <asp:DropDownList CssClass="form-control" ID="cmborganizacion" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <div id="div_admin" runat="server">
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-warning" Text="Exportar Datos"><span class="glyphicon glyphicon-plus"></span>&nbsp; Agregar nueva organización</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.row (nested) -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <h3>
                                                    <span style="float: right;"><small># Organizaciones:</small>
                                                        <asp:Label ID="LabelTot" runat="server" CssClass="label label-primary" /></span>
                                                </h3>
                                                <p>&nbsp;</p>
                                                <p>&nbsp;</p>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connSAG %>" ProviderName="<%$ ConnectionStrings:connSAG.ProviderName %>"></asp:SqlDataSource>
                                                <asp:GridView ID="GridDatos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                                                    GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataSourceID="SqlDataSource1" Font-Size="Small">
                                                    <FooterStyle BackColor="#40E0D0" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#40E0D0" Font-Bold="True" ForeColor="White" />
                                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                    <EmptyDataTemplate>
                                                        ¡No hay organizaciones registradas!
                                                    </EmptyDataTemplate>
                                                    <%--Paginador...--%>
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    <PagerTemplate>
                                                        <div class="row" style="margin-top: 8px;">
                                                            <div class="col-lg-1" style="text-align: right;">
                                                                <h5>
                                                                    <asp:Label ID="MsgL" Text="Ir a la pág." runat="server" /></h5>
                                                            </div>
                                                            <div class="col-lg-1" style="text-align: left;">
                                                                <asp:DropDownList ID="CmbPage" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="CmbPage_SelectedIndexChanged" runat="server" CssClass="form-control" /></h3>
                                                            </div>
                                                            <div class="col-lg-10" style="text-align: right;">
                                                                <h3>
                                                                    <asp:Label ID="PagActual" runat="server" CssClass="label label-primary" /></h3>
                                                            </div>
                                                        </div>
                                                    </PagerTemplate>
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="id">
                                                            <HeaderStyle CssClass="hide" />
                                                            <ItemStyle CssClass="hide" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Depto_Descripcion" HeaderText="DEPARTAMENTO" />
                                                        <asp:BoundField DataField="ec_nombre" HeaderText="ENTRENADOR" />
                                                        <asp:BoundField DataField="COD_ORGANIZACION" HeaderText="COD_ORGANIZACION" />
                                                        <asp:BoundField DataField="OP_NOMBRE" HeaderText="OP_NOMBRE" />
                                                        <asp:BoundField DataField="REPRESENTANTE_NOMBRE" HeaderText="REPRESENTANTE" />
                                                        <asp:BoundField DataField="REPRESENTANTE_JUNTA_DIRECTIVA" HeaderText="CARGO EN JUNTA" />
                                                        <asp:BoundField DataField="SOCIOS_ACTUAL_HOMBRES" HeaderText="SOCIOS HOMBRES" />
                                                        <asp:BoundField DataField="SOCIOS_ACTUAL_MUJERES" HeaderText="SOCIOS MUJERES" />
                                                        <asp:ButtonField ButtonType="Button" Text="Actualizar" ControlStyle-CssClass="btn btn-info" HeaderText="Actualizar" CommandName="Actualizar">
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
                                            <asp:TextBox ID="tns" runat="server" Visible="false"></asp:TextBox>
                                            <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Exportar Datos" />--%>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" Visible="true" Text="Exportar Datos"><span class="glyphicon glyphicon-save"></span>&nbsp;Exportar Datos</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="Graficos">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="embed-responsive embed-responsive-16by9">
                                            <%--<iframe class="embed-responsive-item" src="https://app.powerbi.com/view?r=eyJrIjoiZWM4MWI1YTEtMjE0NC00MDBmLTk2NTItNDlkZjI1YjJhNDgyIiwidCI6ImM5NzU0NTExLTliODMtNGZmMi1iZmM4LTlkZmY2NzI1NTBmNSIsImMiOjR9" allowfullscreen="true"></iframe>--%>
                                            <iframe width="800" height="600" src="https://app.powerbi.com/view?r=eyJrIjoiMThlMjc3M2EtNzY1Ny00ZjVkLTk2ZWItNTk1NDBhMmFhZjU3IiwidCI6ImVhOGEzNmMwLTczOGItNGNiNC05MzhjLTY5YTUwNWJiNjg5OCIsImMiOjF9" frameborder="0" allowfullscreen="true"></iframe>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--</form>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txt_id" Visible="false" runat="server"></asp:TextBox>
    <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>
    <asp:UpdatePanel ID="UpdatePanel2"
        runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div id="divedit" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header"></h1>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                DETALLE DEL REGISTRO DE ORGANIZACIONES
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <label>Datos generales</label>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>1.Nombre completo del representante:</label>
                                                            <asp:TextBox CssClass="form-control" ID="TXT_nombre_joven" runat="server" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>2. DNI:</label>
                                                            <asp:TextBox CssClass="form-control" ID="TXT_DNI" runat="server" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>3. # Telefono:</label>
                                                            <asp:TextBox CssClass="form-control" ID="TXT_telefono" runat="server" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <label>Constitución legal</label>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>Personeria juridica</label>
                                                            <asp:DropDownList CssClass="form-control" ID="Txttrabaja" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>Si</asp:ListItem>
                                                                <asp:ListItem>No</asp:ListItem>
                                                                <asp:ListItem>En_Tramite</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label>Número de personeria juridica</label>
                                                            <asp:TextBox CssClass="form-control" ID="txt_det_personeria" runat="server" ReadOnly="true" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>RTN</label>
                                                            <asp:DropDownList CssClass="form-control" ID="gb_rtn" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>Si</asp:ListItem>
                                                                <asp:ListItem>No</asp:ListItem>
                                                                <asp:ListItem>En_Tramite</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label>Número de RTN</label>
                                                            <asp:TextBox CssClass="form-control" ID="txt_det_rtn" runat="server" ReadOnly="true" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>CAI</label>
                                                            <asp:DropDownList CssClass="form-control" ID="gb_cai" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>Si</asp:ListItem>
                                                                <asp:ListItem>No</asp:ListItem>
                                                                <asp:ListItem>En_Tramite</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label>Número de CAI</label>
                                                            <asp:TextBox CssClass="form-control" ID="txt_det_cai" runat="server" ReadOnly="true" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <label>Detalle de socios (as)</label>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>Número socios Hombres</label>
                                                            <asp:TextBox CssClass="form-control" ID="txt_socio_H" runat="server" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>Número socios Mujeres</label>
                                                            <asp:TextBox CssClass="form-control" ID="txt_socio_m" runat="server" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>Total socios</label>
                                                            <asp:TextBox CssClass="form-control" ID="txt_socio_total" runat="server" ReadOnly="true" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="col-lg-12">
                            <asp:Label ID="lberror" class="btn-warning" runat="server" Text=""></asp:Label>

                        </div>




                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Button ID="Guardar" OnClientClick="$('#exampleModal2').modal('hide');" Text="Guardar" Width="80px" data-toggle="modal" data-target="#exampleModal2" runat="server" Class="btn btn-Primary" />

                                <asp:Button ID="BSalir" Text="Regresar" Width="80px" runat="server" Class="btn btn-danger" />


                            </div>
                        </div>
                    </div>
                </div>
            </div>




            <%--DESDE AQUI NUEVO FORMULARIO--%>



            <div id="div_nuevo_prod" runat="server">





                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header"></h1>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-12">



                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <asp:Label ID="laBEL2" runat="server" Text=""></asp:Label>REGISTRAR NUEVA ORGANIZACIÓN

                                <asp:TextBox ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" Visible="false" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox3" Visible="false" runat="server"></asp:TextBox>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="row">

                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    Identificacion

                                                </div>


                                                <div class="panel-body">
                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Departamento</label>
                                                            <asp:Label ID="lb_dept_new" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_departamento_new" runat="server" AutoPostBack="True"></asp:DropDownList>

                                                        </div>
                                                    </div>


                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Municipio</label><asp:Label ID="lb_mun_new" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_municipio_new" runat="server" AutoPostBack="True"></asp:DropDownList>

                                                        </div>
                                                    </div>

                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Aldea</label>
                                                            <asp:Label ID="lb_aldea_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:DropDownList CssClass="form-control" ID="gb_aldea_new" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Caserio</label>
                                                            <asp:Label ID="lb_caserio_new" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_caserio_new" runat="server" AutoPostBack="True"></asp:DropDownList>

                                                        </div>
                                                    </div>



                                                </div>
                                            </div>






                                        </div>



                                        <div class="row">

                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    Datos generales

                                                </div>


                                                <div class="panel-body">
                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Nombre Representante</label><asp:Label ID="lb_nombre_new" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="txt_nombre_prod_new" runat="server" AutoPostBack="True"></asp:TextBox>

                                                        </div>
                                                    </div>


                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Sexo representante</label>
                                                            <asp:Label ID="Label14" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_sexo_new" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>Hombre</asp:ListItem>
                                                                <asp:ListItem>Mujer</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>




                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>DNI representante</label><asp:Label ID="lb_dni_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:TextBox CssClass="form-control" ID="txt_dni_new" TextMode="number" runat="server" AutoPostBack="True" MaxLength="13"></asp:TextBox>


                                                        </div>
                                                    </div>


                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label># Telefono</label><asp:Label ID="lb_telefono_new" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="txt_telefono_new" runat="server" TextMode="number" AutoPostBack="True" MaxLength="8"></asp:TextBox>

                                                        </div>
                                                    </div>




                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Cargo</label>
                                                            <asp:Label ID="Label12" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_cargo_nuevo" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>Presidente/a</asp:ListItem>
                                                                <asp:ListItem>Vicepresidente/a</asp:ListItem>
                                                                <asp:ListItem>Secretario/a</asp:ListItem>
                                                                <asp:ListItem>Tesorero/a</asp:ListItem>
                                                                <asp:ListItem>Vocal 1</asp:ListItem>
                                                                <asp:ListItem>Vocal 2</asp:ListItem>
                                                                <asp:ListItem>Vocal 3</asp:ListItem>
                                                                <asp:ListItem>socio</asp:ListItem>









                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>


                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label># Socios Hombres</label><asp:Label ID="LB_SOCIO_H_NEW" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="TXT_SOCIO_H_NEW" runat="server" TextMode="number" AutoPostBack="True" MaxLength="8"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label># Socios MUjeres</label><asp:Label ID="LB_SOCIO_M_NEW" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="TXT_SOCIO_M_NEW" runat="server" TextMode="number" AutoPostBack="True" MaxLength="8"></asp:TextBox>

                                                        </div>
                                                    </div>



                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Cadena</label>
                                                            <asp:Label ID="Label16" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_cadena_new" runat="server" AutoPostBack="True">

                                                                <asp:ListItem>ARROZ</asp:ListItem>
                                                                <asp:ListItem>FRIJOL</asp:ListItem>
                                                                <asp:ListItem>MAIZ</asp:ListItem>

                                                                <asp:ListItem>FRIJOL-MAIZ</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>


                                                </div>
                                            </div>






                                        </div>

                                        <div class="row">

                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    DATOS GENERALES DE LA ORGANIZACIÓN

                                                </div>


                                                <div class="panel-body">




                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Nombre de la organización</label><asp:Label ID="LB_OP_NOMBRE_NEW" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="TXT_OP_NOMBRE_NEW" runat="server" AutoPostBack="True"></asp:TextBox>


                                                        </div>
                                                    </div>

                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Direccion de Organizcion</label><asp:Label ID="LB_DIRECCION_NEW" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="TXT_OPDIRECCION_NEW" runat="server" AutoPostBack="True"></asp:TextBox>


                                                        </div>
                                                    </div>


                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Tipo organizacion</label><asp:Label ID="Label3" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_tipo_new" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>Asociacion_Productores</asp:ListItem>
                                                                <asp:ListItem>Caja_Rural</asp:ListItem>

                                                                <asp:ListItem>Empresa_servicios_multiples</asp:ListItem>
                                                                <asp:ListItem>Organizacion_Indigena</asp:ListItem>
                                                                <asp:ListItem>Organizacion_Mujeres</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>



                                                    <div class="col-lg-3">
                                                        <div class="form-group">


                                                            <label>Fecha de creacion organizacion</label><asp:Label ID="Label5" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:TextBox CssClass="form-control" ID="TXT_FECHA_CREATE_OP" runat="server" TextMode="date" AutoPostBack="True"></asp:TextBox>



                                                        </div>
                                                    </div>



                                                </div>
                                            </div>






                                        </div>




                                        <div class="row">

                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    CONSTITUCIÓN LEGAL DE LA ORGANIZACIÓN

                                                </div>


                                                <div class="panel-body">




                                                    <div class="col-lg-4">
                                                        <div class="form-group">

                                                            <label>Tiene RTN</label><asp:Label ID="Label6" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:DropDownList CssClass="form-control" ID="GB_RTN_new" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>No</asp:ListItem>
                                                                <asp:ListItem>Si</asp:ListItem>

                                                                <asp:ListItem>En_Tramite</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <br />
                                                            <label>Digitar el numero de RTN</label><asp:Label ID="LB_RTN" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:TextBox CssClass="form-control" ID="TXT_RTN_new" runat="server" ReadOnly="true" AutoPostBack="True"></asp:TextBox>

                                                        </div>
                                                    </div>


                                                    <div class="col-lg-4">
                                                        <div class="form-group">

                                                            <label>Tiene personeria </label>
                                                            <asp:Label ID="Label8" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:DropDownList CssClass="form-control" ID="GB_PERSONERIA_new" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>No</asp:ListItem>
                                                                <asp:ListItem>Si</asp:ListItem>

                                                                <asp:ListItem>En_Tramite</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <br />
                                                            <label>Digitar el numero de personeria</label><asp:Label ID="LB_PERSONERIA" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:TextBox CssClass="form-control" ID="TXT_PERSONERIA_new" runat="server" ReadOnly="true" AutoPostBack="True"></asp:TextBox>

                                                        </div>
                                                    </div>



                                                    <div class="col-lg-4">
                                                        <div class="form-group">

                                                            <label>Tiene CAI </label>
                                                            <asp:Label ID="Label10" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:DropDownList CssClass="form-control" ID="GB_CAI_new" runat="server" AutoPostBack="True">
                                                                <asp:ListItem>No</asp:ListItem>
                                                                <asp:ListItem>Si</asp:ListItem>

                                                                <asp:ListItem>En_Tramite</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <br />
                                                            <label>Digitar el numero de CAI</label><asp:Label ID="LB_CAI" class="label label-warning" runat="server" Text=""></asp:Label>
                                                            <asp:TextBox CssClass="form-control" ID="TXT_CAI_new" runat="server" ReadOnly="true" AutoPostBack="True"></asp:TextBox>

                                                        </div>
                                                    </div>





                                                </div>
                                            </div>






                                        </div>



                                        <div class="row">

                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    ASIGNAR ORGANIZACIÓN Y ASESOR TECNICO
                                                </div>


                                                <div class="panel-body">




                                                    <div class="col-lg-6">
                                                        <div class="form-group">


                                                            <label>Seleccionar asesor técnico</label><asp:Label ID="lb_asesor_new" class="label label-warning" runat="server" Text=""></asp:Label>

                                                            <asp:DropDownList CssClass="form-control" ID="gb_ec_new" runat="server" AutoPostBack="True">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>




                                                </div>
                                            </div>






                                        </div>














                                        <%--<label>.</label>--%>







                                        <script type="text/javascript">
                                            function numericOnly(elementRef) {
                                                var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

                                                // Un-comment to discover a key that I have forgotten to take into account...
                                                //alert(keyCodeEntered);

                                                if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
                                                    return true;
                                                }
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
                                    </div>

                                    <div id="div2" runat="server">
                                        <%-- <asp:Button ID="Guardar_registro" class="btn btn-success" runat="server" Text="Guardar" data-toggle="modal" data-target="#exampleModal2" />--%>
                                        <button type="button" id="btn_nuevo_prod" runat="server" class="btn btn-success" data-toggle="modal" data-target="#exampleModal222">
                                            Guardar
                                        </button>

                                        <asp:Button ID="Button3" class="btn btn-danger" runat="server" Text="Regresar" />

                                    </div>




                                </div>










                                <div class="row">
                                    <div class="auto-style3">
                                        <script type="text/javascript" src='../vendor/jquery/jquery-1.8.3.min.js'></script>









                                        <div class="modal fade" id="exampleModal222" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                            <div class="modal-dialog" role="document">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title" id="exampleModalLabela22">IHMA</h5>

                                                    </div>
                                                    <div class="modal-body">
                                                        ¿Está seguro que sea registrar una nueva organización?
                                                    </div>
                                                    <div class="modal-footer">

                                                        <asp:Button ID="btn_si_nuevo" Text="SI" Width="80px" runat="server" Class="btn btn-Success" />
                                                        <button type="button" id="Button5" runat="server" class="btn btn-danger" data-dismiss="modal">NO</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>












                                        <br />
                                        <br />

                                        <asp:Label ID="Label13" class="badge badge-pill badge-success" runat="server" Text=""></asp:Label>




                                        <!-- Modal -->






                                    </div>

















                                </div>
                            </div>
                        </div>
                    </div>





                </div>



            </div>




            <%-- HASTA AQUI NUEVO FORM--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">MAS 2.0 - IHMA</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    ¿Está seguro que desea actualizar el registro de las Organización?
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnsi" class="btn btn-primary" runat="server" Text="SI" />

                    <button type="button" class="btn btn-danger" data-dismiss="modal">NO</button>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
