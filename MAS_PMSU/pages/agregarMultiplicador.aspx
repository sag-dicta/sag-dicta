<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="agregarMultiplicador.aspx.vb" Inherits="MAS_PMSU.agregarMultiplicador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Registro de Multiplicador o Estación</h1>
        </div>
    </div>

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

                    <div class="col-lg-3">
                        <div class="form-group">
                            <label>Cedula de Identidad</label><asp:Label ID="Lb_CedulaIdentidad" class="label label-warning" runat="server" Text=""></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="TxtIdentidad" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-3">
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
                    <div class="col-lg-3">
                        <div class="form-group">
                            <label>Telefono</label><asp:Label ID="LblTelefono" class="label label-warning" runat="server" Text=""></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="TxtTelefono" runat="server" AutoPostBack="true" MaxLength="9" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-3">
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
                    <div class="col-lg-3">
                        <div class="form-group">
                            <label>Identidad del Multiplicador:</label><asp:Label ID="lbIdentidadRe" class="label label-warning" runat="server" Text=""></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtIdentidadRe" runat="server" AutoPostBack="true" MaxLength="13" OnTextChanged="VerificarTextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-3">
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
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Departamento</label>
                                <asp:Label ID="lb_dept_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtCodDep" runat="server" AutoPostBack="true" ReadOnly="true" Visible="false"></asp:TextBox>
                                <asp:DropDownList CssClass="form-control" ID="gb_departamento_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                    <asp:ListItem Text=" "></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Municipio</label><asp:Label ID="lb_mun_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="TxtCodMun" runat="server" AutoPostBack="true" ReadOnly="true" Visible="false"></asp:TextBox>
                                <asp:DropDownList CssClass="form-control" ID="gb_municipio_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                    <asp:ListItem Text=" "></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Aldea</label>
                                <asp:Label ID="lb_aldea_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:DropDownList CssClass="form-control" ID="gb_aldea_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                    <asp:ListItem Text=" "></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Caserio</label>
                                <asp:Label ID="lb_caserio_new" class="label label-warning" runat="server" Text=""></asp:Label>
                                <asp:DropDownList CssClass="form-control" ID="gb_caserio_new" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VerificarTextBox">
                                    <asp:ListItem Text=" "></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </section>
                    <div class="col-lg-3">
                        <div class="form-group">
                            <label>Encargado de la finca</label><asp:Label ID="LblPersonaFinca" class="label label-warning" runat="server" Text=""></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="TxtPersonaFinca" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="form-group">
                            <label>Nombre o numero de Lote</label><asp:Label ID="LbLote" class="label label-warning" runat="server" Text=""></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="TxtLote" runat="server" AutoPostBack="true" OnTextChanged="VerificarTextBox"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-3">
                        <div class="form-group">
                            <label>Croquis del Lote</label>
                            <asp:Label ID="Label5" class="label label-warning" runat="server" Text=""></asp:Label>
                            <asp:Label ID="Label25" runat="server" Text="" class="label label-warning" Visible="false">Solo archivos PNG/JPG/JPEG se aceptan</asp:Label>
                            <!-- Agrega el control FileUpload para cargar una imagen -->
                            <asp:FileUpload ID="fileUpload" runat="server" PostBackUrl="SolicitudInscripcionDeLotes.aspx" accept=".png,.jpg,.jpeg"/>
                        </div>
                    </div>
                </div>
                

            </div>
        </div>
    </div>

    <div>
        <label></label><asp:Label ID="LabelGuardar" class="label label-warning" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button CssClass="btn btn-primary" ID="btnGuardarLote" runat="server" Text="Guardar" OnClick="guardarSoli_lote" visible="false"/>
    </div>

    <div>
        <label></label><asp:Label ID="Label18" class="label label-warning" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Imprimir Hoja de Datos del Lote Registrado" onclick="descargaPDF" visible="false"/>
    </div>
    
    <div>
        <label></label><asp:Label ID="Label23" class="label label-warning" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button CssClass="btn btn-primary" ID="Button2" runat="server" Text="Nuevo" OnClick="vaciar" visible="false"/>
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
