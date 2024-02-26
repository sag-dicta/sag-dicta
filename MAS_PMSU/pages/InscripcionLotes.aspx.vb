Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class InscripcionLotes
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connSAG").ConnectionString

    Dim sentencia, identity As String
    Dim nuevo As Boolean
    Dim validarflag As Integer
    Dim id2 As String = "1"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If User.Identity.IsAuthenticated = True Then
            If IsPostBack Then

            Else
                llenarcomboDepto()
                llenarcomboDeptoGrid()
                VerificarTextBox()
                llenarcomboProductor3()
                llenagrid()
                btnGuardarLote.Visible = True
                btnRegresar.Visible = True
                eliminar_caducidad_lote()
            End If
        End If
    End Sub

    Protected Sub guardarSoli_lote(sender As Object, e As EventArgs)
        VerificarTextBox()
        If validarflag = 0 Then
            LabelGuardar.Visible = True
            LabelGuardar.Text = "Ingrese toda la información para poder guardarla"
        Else
            If btnGuardarLote.Text = "Actualizar" Then
                LabelGuardar.Visible = False
                LabelGuardar.Text = ""
                Dim connectionString As String = conn
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "UPDATE sag_registro_lote SET
                        categoria_origen = @categoria_origen,
                        tipo_cultivo = @tipo_cultivo,
                        variedad = @variedad,
                        productor = @productor,
                        no_lote = @no_lote,
                        fecha_analisis = @fecha_analisis,
                        ano_produ = @ano_produ,
                        categoria_semilla = @categoria_semilla,
                        tipo_semilla = @tipo_semilla,
                        cultivo_semilla = @cultivo_semilla,
                        variedad_frijol = @variedad_frijol,
                        variedad_maiz = @variedad_maiz,
                        superficie_hectarea = @superficie_hectarea,
                        fecha_aprox_siembra = @fecha_aprox_siembra,
                        fecha_aprox_cosecha = @fecha_aprox_cosecha,
                        produccion_est_hectareas = @produccion_est_hectareas,
                        destino = @destino,
                        caducidad_lote = @caducidad_lote
                    WHERE id = " & txtID.Text & ""

                    Dim fechaConvertida2 As DateTime
                    Dim fechaConvertida3 As DateTime
                    Dim fechaConvertida4 As DateTime
                    Dim fechaCadu As DateTime

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@categoria_origen", categoria_origen_ddl.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@tipo_cultivo", CmbTipoSemilla.SelectedItem.Text)
                        Dim selectedValue As String = CmbTipoSemilla.SelectedValue
                        If selectedValue = "Frijol" Then
                            cmd.Parameters.AddWithValue("@variedad", DropDownList5.SelectedItem.Text)
                        ElseIf selectedValue = "Maiz" Then
                            cmd.Parameters.AddWithValue("@variedad", DropDownList6.SelectedItem.Text)
                        End If
                        cmd.Parameters.AddWithValue("@productor", txtprodsem.Text)
                        cmd.Parameters.AddWithValue("@no_lote", TextBox3.Text)
                        If DateTime.TryParse(TextBox4.Text, fechaConvertida2) Then
                            cmd.Parameters.AddWithValue("@fecha_analisis", fechaConvertida2.ToString("yyyy-MM-dd")) ' Aquí se formatea correctamente como yyyy-MM-dd
                        End If
                        cmd.Parameters.AddWithValue("@ano_produ", TextBox6.Text)

                        cmd.Parameters.AddWithValue("@categoria_semilla", DdlCategoria.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@tipo_semilla", DdlTipo.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@cultivo_semilla", DropDownList3.SelectedItem.Text)
                        Dim selectedValue2 As String = DropDownList3.SelectedItem.Text
                        If selectedValue = "Frijol" Then
                            cmd.Parameters.AddWithValue("@variedad_frijol", DropDownList1.SelectedItem.Text)
                        Else
                            cmd.Parameters.AddWithValue("@variedad_frijol", DBNull.Value)
                        End If
                        If selectedValue = "Maiz" Then
                            cmd.Parameters.AddWithValue("@variedad_maiz", DropDownList2.SelectedItem.Text)
                        Else
                            cmd.Parameters.AddWithValue("@variedad_maiz", DBNull.Value)
                        End If
                        cmd.Parameters.AddWithValue("@superficie_hectarea", Convert.ToDouble(TxtHectareas.Text))
                        If DateTime.TryParse(TxtFechaSiembra.Text, fechaConvertida3) Then
                            cmd.Parameters.AddWithValue("@fecha_aprox_siembra", fechaConvertida3.ToString("yyyy-MM-dd")) ' Aquí se formatea correctamente como yyyy-MM-dd
                        End If
                        If DateTime.TryParse(TxtCosecha.Text, fechaConvertida4) Then
                            cmd.Parameters.AddWithValue("@fecha_aprox_cosecha", fechaConvertida4.ToString("yyyy-MM-dd")) ' Aquí se formatea correctamente como yyyy-MM-dd
                        End If
                        If DateTime.TryParse(txtFechaCad.Text, fechaCadu) Then
                            cmd.Parameters.AddWithValue("@caducidad_lote", fechaCadu.ToString("yyyy-MM-dd"))
                        End If
                        cmd.Parameters.AddWithValue("@produccion_est_hectareas", Convert.ToDouble(TxtProHectareas.Text))
                        cmd.Parameters.AddWithValue("@destino", DropDownList4.SelectedItem.Text)

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        Label3.Text = "¡Se ha editado correctamente el lote o inscripcion de SENASA!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        BConfirm.Visible = True
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                        btnGuardarLote.Visible = False

                    End Using
                End Using
            Else
                LabelGuardar.Visible = False
                LabelGuardar.Text = ""
                Dim connectionString As String = conn
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "INSERT INTO sag_registro_lote (
                    id2,
                    estado,
                    categoria_origen,
                    tipo_cultivo,
                    variedad,
                    productor,
                    no_lote,
                    fecha_analisis,
                    ano_produ,
                    categoria_semilla,
                    tipo_semilla,
                    cultivo_semilla,
                    variedad_frijol,
                    variedad_maiz,
                    superficie_hectarea,
                    fecha_aprox_siembra,
                    fecha_aprox_cosecha,
                    produccion_est_hectareas,
                    destino,
                    caducidad_lote
                ) VALUES (
                    @id2,
                    @estado,
                    @categoria_origen,
                    @tipo_cultivo,
                    @variedad,
                    @productor,
                    @no_lote,
                    @fecha_analisis,
                    @ano_produ,
                    @categoria_semilla,
                    @tipo_semilla,
                    @cultivo_semilla,
                    @variedad_frijol,
                    @variedad_maiz,
                    @superficie_hectarea,
                    @fecha_aprox_siembra,
                    @fecha_aprox_cosecha,
                    @produccion_est_hectareas,
                    @destino,
                    @caducidad_lote
                );
                "
                    Dim fechaConvertida2 As DateTime
                    Dim fechaConvertida3 As DateTime
                    Dim fechaConvertida4 As DateTime
                    Dim fechaCadu As DateTime

                    Using cmd As New MySqlCommand(query, connection)
                        cmd.Parameters.AddWithValue("@id2", TextIdMulti2.Text)
                        cmd.Parameters.AddWithValue("@categoria_origen", categoria_origen_ddl.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@tipo_cultivo", CmbTipoSemilla.SelectedItem.Text)
                        Dim selectedValue As String = CmbTipoSemilla.SelectedValue
                        If selectedValue = "Frijol" Then
                            cmd.Parameters.AddWithValue("@variedad", DropDownList5.SelectedItem.Text)
                        ElseIf selectedValue = "Maiz" Then
                            cmd.Parameters.AddWithValue("@variedad", DropDownList6.SelectedItem.Text)
                        End If
                        cmd.Parameters.AddWithValue("@productor", txtprodsem.Text)
                        cmd.Parameters.AddWithValue("@no_lote", TextBox3.Text)
                        If DateTime.TryParse(TextBox4.Text, fechaConvertida2) Then
                            cmd.Parameters.AddWithValue("@fecha_analisis", fechaConvertida2.ToString("yyyy-MM-dd")) ' Aquí se formatea correctamente como yyyy-MM-dd
                        End If
                        cmd.Parameters.AddWithValue("@ano_produ", TextBox6.Text)

                        cmd.Parameters.AddWithValue("@categoria_semilla", DdlCategoria.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@tipo_semilla", DdlTipo.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@cultivo_semilla", DropDownList3.SelectedItem.Text)
                        Dim selectedValue2 As String = DropDownList3.SelectedItem.Text
                        If selectedValue = "Frijol" Then
                            cmd.Parameters.AddWithValue("@variedad_frijol", DropDownList1.SelectedItem.Text)
                        Else
                            cmd.Parameters.AddWithValue("@variedad_frijol", DBNull.Value)
                        End If
                        If selectedValue = "Maiz" Then
                            cmd.Parameters.AddWithValue("@variedad_maiz", DropDownList2.SelectedItem.Text)
                        Else
                            cmd.Parameters.AddWithValue("@variedad_maiz", DBNull.Value)
                        End If
                        cmd.Parameters.AddWithValue("@superficie_hectarea", Convert.ToDouble(TxtHectareas.Text))
                        If DateTime.TryParse(TxtFechaSiembra.Text, fechaConvertida3) Then
                            cmd.Parameters.AddWithValue("@fecha_aprox_siembra", fechaConvertida3.ToString("yyyy-MM-dd"))
                        End If
                        If DateTime.TryParse(TxtCosecha.Text, fechaConvertida4) Then
                            cmd.Parameters.AddWithValue("@fecha_aprox_cosecha", fechaConvertida4.ToString("yyyy-MM-dd"))
                        End If
                        If DateTime.TryParse(txtFechaCad.Text, fechaCadu) Then
                            cmd.Parameters.AddWithValue("@caducidad_lote", fechaCadu.ToString("yyyy-MM-dd"))
                        End If
                        cmd.Parameters.AddWithValue("@produccion_est_hectareas", Convert.ToDouble(TxtProHectareas.Text))
                        cmd.Parameters.AddWithValue("@destino", DropDownList4.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@estado", "1")

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        Label3.Text = "¡Se ha registrado correctamente el lote o inscripcion de SENASA!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        BConfirm.Visible = True
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                        btnGuardarLote.Visible = False

                    End Using
                End Using
            End If
        End If

    End Sub

    Protected Sub vaciar(sender As Object, e As EventArgs)
        txt_nombre_prod_new.Text = " "
        Txt_Representante_Legal.Text = " "
        TxtIdentidad.Text = " "
        TextBox1.Text = " "
        TxtResidencia.Text = " "
        TxtTelefono.Text = " "
        txtNoRegistro.Text = " "
        txtNombreRe.Text = " "
        txtIdentidadRe.Text = " "
        TxtTelefonoRe.Text = " "
        TxtNombreFinca.Text = " "
        gb_departamento_new.SelectedIndex = 0

        gb_municipio_new.SelectedItem.Text = " "
        gb_municipio_new.Enabled = False

        gb_aldea_new.SelectedItem.Text = " "
        gb_aldea_new.Enabled = False

        gb_caserio_new.SelectedItem.Text = " "
        gb_caserio_new.Enabled = False

        TxtPersonaFinca.Text = " "
        TxtLote.Text = " "
        'FileUpload
        VerificarTextBox()
    End Sub
    Private Sub llenarcomboDepto()
        Dim StrCombo As String = "SELECT * FROM tb_departamentos"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        gb_departamento_new.DataSource = DtCombo
        gb_departamento_new.DataValueField = DtCombo.Columns(0).ToString()
        gb_departamento_new.DataTextField = DtCombo.Columns(2).ToString
        gb_departamento_new.DataBind()
        Dim newitem As New ListItem(" ", " ")
        gb_departamento_new.Items.Insert(0, newitem)
        VerificarTextBox()
    End Sub

    Private Function DevolverValorDepart(cadena As String)

        If TxtDepto.SelectedItem.Text <> " " Then
            Dim codigoDepartamento As String = ""
            Dim StrCombo As String = "SELECT CODIGO_DEPARTAMENTO FROM tb_departamentos WHERE NOMBRE = @nombre"
            Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
            adaptcombo.SelectCommand.Parameters.AddWithValue("@nombre", cadena)
            Dim DtCombo As New DataTable
            adaptcombo.Fill(DtCombo)
            txtCodDep.Text = DtCombo.Rows(0)("CODIGO_DEPARTAMENTO").ToString
            codigoDepartamento = DtCombo.Rows(0)("CODIGO_DEPARTAMENTO").ToString()
            Return codigoDepartamento
        End If

        Return 0
        VerificarTextBox()
    End Function

    Private Function DevolverValorMuni(cadena As String)
        If gb_municipio_new.SelectedItem.Text <> "" Then
            Dim codigoMunicipio As String = ""
            Dim StrCombo As String = "SELECT CODIGO_MUNICIPIO FROM tb_municipio WHERE NOMBRE = @nombre AND CODIGO_DEPARTAMENTO = '" & txtCodDep.Text & "'"
            Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
            adaptcombo.SelectCommand.Parameters.AddWithValue("@nombre", cadena)
            Dim DtCombo As New DataTable
            adaptcombo.Fill(DtCombo)
            TxtCodMun.Text = DtCombo.Rows(0)("CODIGO_MUNICIPIO").ToString
            codigoMunicipio = DtCombo.Rows(0)("CODIGO_MUNICIPIO").ToString()
            Return codigoMunicipio
        End If
        Return 0
        VerificarTextBox()
    End Function

    Private Function DevolverValorAlde(cadena As String)
        If gb_aldea_new.SelectedItem.Text <> "" Then
            Dim codigoCaserio As String = ""
            Dim StrCombo As String = "SELECT CODIGO_ALDEA FROM tb_aldea WHERE NOMBRE = @nombre AND CODIGO_MUNICIPIO = '" & TxtCodMun.Text & "'"
            Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
            adaptcombo.SelectCommand.Parameters.AddWithValue("@nombre", cadena)
            Dim DtCombo As New DataTable
            adaptcombo.Fill(DtCombo)

            codigoCaserio = DtCombo.Rows(0)("CODIGO_ALDEA").ToString()
            Return codigoCaserio
        End If
        Return 0
        VerificarTextBox()
    End Function
    Private Sub llenarmunicipio()
        gb_municipio_new.Enabled = True
        gb_aldea_new.SelectedItem.Text = " "
        gb_aldea_new.Enabled = False
        gb_caserio_new.SelectedItem.Text = " "
        gb_caserio_new.Enabled = False
        Dim departamento As String = DevolverValorDepart(gb_departamento_new.SelectedItem.Text)
        Dim StrCombo As String = "SELECT * FROM tb_municipio WHERE CODIGO_DEPARTAMENTO = " & departamento & ""
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        gb_municipio_new.DataSource = DtCombo
        gb_municipio_new.DataValueField = DtCombo.Columns(0).ToString()
        gb_municipio_new.DataTextField = DtCombo.Columns(3).ToString
        gb_municipio_new.DataBind()
        Dim newitem As New ListItem(" ", " ")
        gb_municipio_new.Items.Insert(0, newitem)
        VerificarTextBox()
    End Sub

    Private Sub llenarAldea()
        gb_aldea_new.Enabled = True
        gb_caserio_new.SelectedItem.Text = " "
        gb_caserio_new.Enabled = False
        Dim municipio As String = DevolverValorMuni(gb_municipio_new.SelectedItem.Text)
        Dim StrCombo As String = "SELECT * FROM tb_aldea WHERE CODIGO_MUNICIPIO = " & municipio & ""
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        gb_aldea_new.DataSource = DtCombo
        gb_aldea_new.DataValueField = DtCombo.Columns(0).ToString()
        gb_aldea_new.DataTextField = DtCombo.Columns(3).ToString
        gb_aldea_new.DataBind()
        Dim newitem As New ListItem(" ", " ")
        gb_aldea_new.Items.Insert(0, newitem)
        VerificarTextBox()
    End Sub

    Private Sub llenarCaserio()
        gb_caserio_new.Enabled = True
        Dim aldea As String = DevolverValorAlde(gb_aldea_new.SelectedItem.Text)
        Dim StrCombo As String = "SELECT * FROM tb_caserios WHERE CODIGO_ALDEA = " & aldea & ""
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        gb_caserio_new.DataSource = DtCombo
        gb_caserio_new.DataValueField = DtCombo.Columns(0).ToString()
        gb_caserio_new.DataTextField = DtCombo.Columns(5).ToString
        gb_caserio_new.DataBind()
        Dim newitem As New ListItem(" ", " ")
        gb_caserio_new.Items.Insert(0, newitem)
        VerificarTextBox()
    End Sub

    Protected Sub buscar_productor(sender As Object, e As EventArgs)
        VerificarTextBox()
    End Sub


    Protected Sub llenarProdutor()
        Dim StrCombo As String = "SELECT * FROM registros_bancos_semilla WHERE PROD_NOMBRE = @valor"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        adaptcombo.SelectCommand.Parameters.AddWithValue("@valor", txt_nombre_prod_new.Text)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        If DtCombo.Rows.Count > 0 Then
            txt_nombre_prod_new.Text = DtCombo.Rows(0)("PROD_NOMBRE").ToString
            TxtIdentidad.Text = DtCombo.Rows(0)("PROD_IDENTIDAD").ToString
            TxtTelefono.Text = DtCombo.Rows(0)("PROD_TELEFONO").ToString

            btnGuardarLote.Visible = True
        Else
            Response.Write("<script>window.alert('¡No existe productor en la base de datos!') </script>")
        End If
        VerificarTextBox()
    End Sub

    Protected Sub gb_departamento_new_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gb_departamento_new.SelectedIndexChanged
        llenarmunicipio()
        VerificarTextBox()
    End Sub

    Protected Sub gb_municipio_new_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gb_municipio_new.SelectedIndexChanged
        'gb_caserio_new.Enabled = False
        llenarAldea()
        VerificarTextBox()
    End Sub

    Protected Sub gb_aldea_new_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gb_aldea_new.SelectedIndexChanged
        llenarCaserio()
        VerificarTextBox()
    End Sub

    Protected Sub VerificarTextBox()
        '1
        If String.IsNullOrEmpty(CmbTipoSemilla.Text) Then
            Label2.Text = "*"
            validarflag = 0
        Else
            Label2.Text = ""
            validarflag += 1
        End If
        '2
        If CmbTipoSemilla.SelectedItem.Text = "Frijol" Then
            If String.IsNullOrEmpty(DropDownList5.Text) Then
                Label4.Text = "*"
                validarflag = 0
            Else
                Label4.Text = ""
                validarflag += 1
            End If
        Else
            If String.IsNullOrEmpty(DropDownList6.Text) Then
                Label6.Text = "*"
                validarflag = 0
            Else
                Label6.Text = ""
                validarflag += 1
            End If
        End If
        '3
        If String.IsNullOrEmpty(txtprodsem.Text) Then
            Label22.Text = "*"
            validarflag = 0
        Else
            Label22.Text = ""
            validarflag += 1
        End If
        '4
        If String.IsNullOrEmpty(TextBox3.Text) Then
            Label8.Text = "*"
            validarflag = 0
        Else
            Label8.Text = ""
            validarflag += 1
        End If
        '5
        If String.IsNullOrEmpty(TextBox4.Text) Then
            Label9.Text = "*"
            validarflag = 0
        Else
            Label9.Text = ""
            validarflag += 1
        End If
        '6
        If String.IsNullOrEmpty(TextBox6.Text) Then
            Label10.Text = "*"
            validarflag = 0
        Else
            Label10.Text = ""
            validarflag += 1
        End If
        '7
        If String.IsNullOrEmpty(DdlCategoria.Text) Then
            Label7.Text = "*"
            validarflag = 0
        Else
            Label7.Text = ""
            validarflag += 1
        End If
        '8
        If String.IsNullOrEmpty(DdlTipo.Text) Then
            Label11.Text = "*"
            validarflag = 0
        Else
            Label11.Text = ""
            validarflag += 1
        End If
        '9
        If String.IsNullOrEmpty(DropDownList3.Text) Then
            Label12.Text = "*"
            validarflag = 0
        Else
            Label12.Text = ""
            validarflag += 1
        End If
        '10
        If DropDownList3.SelectedItem.Text = "Frijol" Then
            If String.IsNullOrEmpty(DropDownList1.Text) Then
                Label15.Text = "*"
                validarflag = 0
            Else
                Label15.Text = ""
                validarflag += 1
            End If
        Else
            If String.IsNullOrEmpty(DropDownList2.Text) Then
                Label16.Text = "*"
                validarflag = 0
            Else
                validarflag += 1
                Label16.Text = ""
            End If
        End If
        '11
        If String.IsNullOrEmpty(TxtHectareas.Text) Then
            Label13.Text = "*"
            validarflag = 0
        Else
            Label13.Text = ""
            validarflag += 1
        End If
        '12
        If String.IsNullOrEmpty(TxtFechaSiembra.Text) Then
            Label17.Text = "*"
            validarflag = 0
        Else
            Label17.Text = ""
            validarflag += 1
        End If
        '13
        If String.IsNullOrEmpty(TxtCosecha.Text) Then
            Label19.Text = "*"
            validarflag = 0
        Else
            Label19.Text = ""
            validarflag += 1
        End If
        '14
        If String.IsNullOrEmpty(TxtProHectareas.Text) Then
            Label20.Text = "*"
            validarflag = 0
        Else
            Label20.Text = ""
            validarflag += 1
        End If
        '15
        If String.IsNullOrEmpty(DropDownList4.Text) Then
            Label24.Text = "*"
            validarflag = 0
        Else
            Label24.Text = ""
            validarflag += 1
        End If
        '16
        If String.IsNullOrEmpty(categoria_origen_ddl.Text) Then
            Label5.Text = "*"
            validarflag = 0
        Else
            Label5.Text = ""
            validarflag += 1
        End If

        If validarflag = 16 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub

    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM sag_registro_lote WHERE nombre_multiplicador = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", txtNombreRe.Text)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "sag_registro_lote")

        Dim nombre As String

        nombre = " Datos del Multiplicador " + Today

        rptdocument.Load(Server.MapPath("~/pages/AgregarMultiplicadorReport2.rpt"))

        rptdocument.SetDataSource(ds)
        Response.Buffer = False


        Response.ClearContent()
        Response.ClearHeaders()

        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub

    '********************************************************************************************************************

    Protected Sub PageDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Recupera la fila.
        Dim pagerRow As GridViewRow = GridDatos.BottomPagerRow
        ' Recupera el control DropDownList...
        Dim pageList As DropDownList = CType(pagerRow.Cells(0).FindControl("PageDropDownList"), DropDownList)
        ' Se Establece la propiedad PageIndex para visualizar la página seleccionada...
        GridDatos.PageIndex = pageList.SelectedIndex
        llenagrid()
        'Quita el mensaje de información si lo hubiera...
        'lblInfo.Text = ""
    End Sub

    Protected Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected
        lblTotalClientes.Text = e.AffectedRows.ToString()
    End Sub

    Sub llenagrid()
        Dim cadena As String = "id_lote, nombre_productor, nombre_finca, nombre_multiplicador, cedula_multiplicador, departamento, municipio, no_lote, certificado_origen_semilla, factura_comercio"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        If (TxtMultiplicador.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND nombre_multiplicador = '" & TxtMultiplicador.SelectedItem.Text & "' "
        End If

        If (TxtMunicipio.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND municipio = '" & TxtMunicipio.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `vista_multi_lote` WHERE 1 = 1 " & c1 & c3 & c4

        GridDatos.DataBind()
    End Sub
    Private Sub llenarcomboDeptoGrid()
        Dim StrCombo As String = "SELECT * FROM tb_departamentos"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        TxtDepto.DataSource = DtCombo
        TxtDepto.DataValueField = DtCombo.Columns(0).ToString()
        TxtDepto.DataTextField = DtCombo.Columns(2).ToString
        TxtDepto.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtDepto.Items.Insert(0, newitem)

    End Sub
    Private Function DevolverValorDepart2(cadena As String)

        If TxtDepto.SelectedItem.Text <> "Todos" Then
            Dim codigoDepartamento As String = ""
            Dim StrCombo As String = "SELECT CODIGO_DEPARTAMENTO FROM tb_departamentos WHERE NOMBRE = @nombre"
            Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
            adaptcombo.SelectCommand.Parameters.AddWithValue("@nombre", cadena)
            Dim DtCombo As New DataTable
            adaptcombo.Fill(DtCombo)
            txtCodDep.Text = DtCombo.Rows(0)("CODIGO_DEPARTAMENTO").ToString
            codigoDepartamento = DtCombo.Rows(0)("CODIGO_DEPARTAMENTO").ToString()
            Return codigoDepartamento
        End If

        Return 0
        VerificarTextBox()
    End Function
    Protected Sub TxtDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtDepto.SelectedIndexChanged
        llenarmunicipioGrid()
        If TxtDepto.SelectedItem.Text = "Todos" Then
            llenarcomboProductor3()
            BAgregar.Visible = False
        Else
            llenarcomboProductor2()
        End If
        llenagrid()
        'If TxtDepto.SelectedItem.Text = "Todos" Then
        '    TxtDepto.SelectedIndex = 0
        '    TxtMunicipio.SelectedIndex = 0
        '    TxtMultiplicador.SelectedIndex = 0
        'End If
    End Sub

    Private Sub llenarmunicipioGrid()
        Dim departamento As String = DevolverValorDepart2(TxtDepto.SelectedItem.Text)
        Dim StrCombo As String = "SELECT * FROM tb_municipio WHERE CODIGO_DEPARTAMENTO = " & departamento & ""
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        TxtMunicipio.DataSource = DtCombo
        TxtMunicipio.DataValueField = DtCombo.Columns(0).ToString()
        TxtMunicipio.DataTextField = DtCombo.Columns(3).ToString
        TxtMunicipio.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtMunicipio.Items.Insert(0, newitem)
    End Sub
    Protected Sub TxtMunicipio_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMunicipio.SelectedIndexChanged
        If TxtMunicipio.SelectedItem.Text = "Todos" Then
            llenarcomboProductor2()
        Else
            llenarcomboProductor()
        End If
        llenagrid()
    End Sub

    Private Sub llenarcomboProductor()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT nombre_multiplicador FROM sag_registro_Multiplicador WHERE estado = '1' AND municipio = '" & TxtMunicipio.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        TxtMultiplicador.DataSource = DtCombo
        TxtMultiplicador.DataValueField = DtCombo.Columns(0).ToString()
        TxtMultiplicador.DataTextField = DtCombo.Columns(0).ToString()
        TxtMultiplicador.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtMultiplicador.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboProductor2()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT * FROM sag_registro_Multiplicador WHERE estado = '1' AND departamento = '" & TxtDepto.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        TxtMultiplicador.DataSource = DtCombo
        TxtMultiplicador.DataValueField = DtCombo.Columns(0).ToString()
        TxtMultiplicador.DataTextField = DtCombo.Columns(8).ToString()
        TxtMultiplicador.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtMultiplicador.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboProductor3()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT * FROM sag_registro_Multiplicador WHERE estado = '1' ORDER BY nombre_multiplicador ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        TxtMultiplicador.DataSource = DtCombo
        TxtMultiplicador.DataValueField = DtCombo.Columns(0).ToString()
        TxtMultiplicador.DataTextField = DtCombo.Columns(8).ToString()
        TxtMultiplicador.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtMultiplicador.Items.Insert(0, newitem)
    End Sub
    Protected Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

        DivCrearNuevo.Visible = True
        DivGrid.Visible = False
        btnGuardarLote.Text = "Guardar"

        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        Dim Str As String = "SELECT * FROM sag_registro_multiplicador WHERE 1=1"

        If (TxtMultiplicador.SelectedItem.Text <> "Todos") Then
            Str &= " AND nombre_multiplicador = '" & TxtMultiplicador.SelectedItem.Text & "'"
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)
            TextIdMulti2.Text = dt.Rows(0)("id").ToString()
            txt_nombre_prod_new.Text = dt.Rows(0)("nombre_productor").ToString()
            Txt_Representante_Legal.Text = dt.Rows(0)("representante_legal").ToString()
            TxtIdentidad.Text = dt.Rows(0)("identidad_productor").ToString()
            TextBox1.Text = dt.Rows(0)("extendida").ToString()
            TxtResidencia.Text = dt.Rows(0)("residencia_productor").ToString()
            TxtTelefono.Text = dt.Rows(0)("telefono_productor").ToString()
            txtNoRegistro.Text = dt.Rows(0)("no_registro_productor").ToString()
            txtNombreRe.Text = dt.Rows(0)("nombre_multiplicador").ToString()
            txtIdentidadRe.Text = dt.Rows(0)("cedula_multiplicador").ToString()
            TxtTelefonoRe.Text = dt.Rows(0)("telefono_multiplicador").ToString()
            TxtNombreFinca.Text = dt.Rows(0)("nombre_finca").ToString()
            SeleccionarItemEnDropDownList(gb_departamento_new, dt.Rows(0)("departamento").ToString())
            llenarmunicipio()
            SeleccionarItemEnDropDownList(gb_municipio_new, dt.Rows(0)("municipio").ToString())
            llenarAldea()
            SeleccionarItemEnDropDownList(gb_aldea_new, dt.Rows(0)("aldea").ToString())
            llenarCaserio()
            SeleccionarItemEnDropDownList(gb_caserio_new, dt.Rows(0)("caserio").ToString())
            TxtPersonaFinca.Text = dt.Rows(0)("nombre_persona_finca").ToString()
            TxtLote.Text = dt.Rows(0)("nombre_lote").ToString()
            gb_aldea_new.Enabled = False
            gb_caserio_new.Enabled = False
            gb_municipio_new.Enabled = False
            VerificarTextBox()
        End If
    End Sub

    Protected Sub TxtMultiplicador_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
        If TxtMultiplicador.SelectedItem.Text <> "Todos" Then
            BAgregar.Visible = True
        Else
            BAgregar.Visible = False
        End If

        If TxtMultiplicador.SelectedItem.Text = "Todos" Then
            TxtDepto.SelectedIndex = 0
            TxtMunicipio.SelectedIndex = 0
            TxtMultiplicador.SelectedIndex = 0
            BAgregar.Visible = False
        End If
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/InscripcionLotes.aspx"))
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        exportar()
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Response.Redirect(String.Format("~/pages/InscSENASA_DescArch.aspx"))
    End Sub
    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Editar") Then
            DivCrearNuevo.Visible = True
            DivGrid.Visible = False
            div_nuevo_prod.Visible = False



            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "id_lote, nombre_productor,representante_legal,identidad_productor,extendida,residencia_productor,telefono_productor,no_registro_productor,nombre_multiplicador,cedula_multiplicador,telefono_multiplicador,nombre_finca,nombre_persona_finca,departamento,municipio,aldea,caserio,nombre_lote,tipo_cultivo,variedad,productor,no_lote,fecha_analisis,ano_produ,categoria_semilla,tipo_semilla,cultivo_semilla,variedad_maiz,variedad_frijol,superficie_hectarea,fecha_aprox_siembra,fecha_aprox_cosecha,produccion_est_hectareas,destino, categoria_origen, caducidad_lote"
            Dim Str As String = "SELECT " & cadena & " FROM `vista_multi_lote` WHERE  ID_lote='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)
            'Dim todosNulos As Boolean = True
            nuevo = False
            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            txt_nombre_prod_new.Text = dt.Rows(0)("nombre_productor").ToString()
            Txt_Representante_Legal.Text = dt.Rows(0)("representante_legal").ToString()
            TxtIdentidad.Text = dt.Rows(0)("identidad_productor").ToString()
            TextBox1.Text = dt.Rows(0)("extendida").ToString()
            TxtResidencia.Text = dt.Rows(0)("residencia_productor").ToString()
            TxtTelefono.Text = dt.Rows(0)("telefono_productor").ToString()
            txtNoRegistro.Text = dt.Rows(0)("no_registro_productor").ToString()
            txtNombreRe.Text = dt.Rows(0)("nombre_multiplicador").ToString()
            txtIdentidadRe.Text = dt.Rows(0)("cedula_multiplicador").ToString()
            TxtTelefonoRe.Text = dt.Rows(0)("telefono_multiplicador").ToString()
            TxtNombreFinca.Text = dt.Rows(0)("nombre_finca").ToString()
            SeleccionarItemEnDropDownList(gb_departamento_new, dt.Rows(0)("departamento").ToString())
            llenarmunicipio()
            SeleccionarItemEnDropDownList(gb_municipio_new, dt.Rows(0)("municipio").ToString())
            llenarAldea()
            SeleccionarItemEnDropDownList(gb_aldea_new, dt.Rows(0)("aldea").ToString())
            llenarCaserio()
            gb_aldea_new.Enabled = False
            gb_caserio_new.Enabled = False
            gb_municipio_new.Enabled = False
            SeleccionarItemEnDropDownList(gb_caserio_new, dt.Rows(0)("caserio").ToString())
            TxtPersonaFinca.Text = dt.Rows(0)("nombre_persona_finca").ToString()
            TxtLote.Text = dt.Rows(0)("nombre_lote").ToString()

            SeleccionarItemEnDropDownList(CmbTipoSemilla, If(dt.Rows(0)("tipo_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_cultivo").ToString()))

            If dt.Rows(0)("tipo_cultivo").ToString() = "Frijol" Then
                VariedadFrijol.Visible = True
                VariedadMaiz.Visible = False
                SeleccionarItemEnDropDownList(DropDownList5, If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString()))
            ElseIf dt.Rows(0)("tipo_cultivo").ToString() = "Maiz" Then
                VariedadFrijol.Visible = False
                VariedadMaiz.Visible = True
                SeleccionarItemEnDropDownList(DropDownList6, If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString()))
            Else
                VariedadFrijol.Visible = False
                VariedadMaiz.Visible = False
            End If

            txtprodsem.Text = If(dt.Rows(0)("productor") Is DBNull.Value, String.Empty, dt.Rows(0)("productor").ToString())
            SeleccionarItemEnDropDownList(categoria_origen_ddl, If(dt.Rows(0)("categoria_origen") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_origen").ToString()))
            TextBox3.Text = If(dt.Rows(0)("no_lote") Is DBNull.Value, String.Empty, dt.Rows(0)("no_lote").ToString())
            TextBox4.Text = If(dt.Rows(0)("fecha_analisis") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_analisis"), DateTime).ToString("yyyy-MM-dd"))
            txtFechaCad.Text = If(dt.Rows(0)("caducidad_lote") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("caducidad_lote"), DateTime).ToString("yyyy-MM-dd"))
            TextBox6.Text = If(dt.Rows(0)("ano_produ") Is DBNull.Value, String.Empty, dt.Rows(0)("ano_produ").ToString())


            SeleccionarItemEnDropDownList(DdlCategoria, If(dt.Rows(0)("categoria_semilla") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_semilla").ToString()))
            SeleccionarItemEnDropDownList(DdlTipo, If(dt.Rows(0)("tipo_semilla") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_semilla").ToString()))
            SeleccionarItemEnDropDownList(DropDownList3, If(dt.Rows(0)("cultivo_semilla") Is DBNull.Value, String.Empty, dt.Rows(0)("cultivo_semilla").ToString()))


            If dt.Rows(0)("cultivo_semilla").ToString() = "Frijol" Then
                variedadfrijol2.Visible = True
                variedadmaiz2.Visible = False
                SeleccionarItemEnDropDownList(DropDownList1, If(dt.Rows(0)("variedad_frijol") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad_frijol").ToString()))
            ElseIf dt.Rows(0)("cultivo_semilla").ToString() = "Maiz" Then
                variedadfrijol2.Visible = False
                variedadmaiz2.Visible = True
                SeleccionarItemEnDropDownList(DropDownList2, If(dt.Rows(0)("variedad_maiz") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad_maiz").ToString()))
            Else
                variedadfrijol2.Visible = False
                variedadmaiz2.Visible = False
            End If

            TxtHectareas.Text = If(dt.Rows(0)("superficie_hectarea") Is DBNull.Value, String.Empty, dt.Rows(0)("superficie_hectarea").ToString())

            TxtFechaSiembra.Text = If(dt.Rows(0)("fecha_aprox_siembra") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_aprox_siembra"), DateTime).ToString("yyyy-MM-dd"))
            TxtCosecha.Text = If(dt.Rows(0)("fecha_aprox_cosecha") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_aprox_cosecha"), DateTime).ToString("yyyy-MM-dd"))
            TxtProHectareas.Text = If(dt.Rows(0)("produccion_est_hectareas") Is DBNull.Value, String.Empty, dt.Rows(0)("produccion_est_hectareas").ToString())

            SeleccionarItemEnDropDownList(DropDownList4, If(dt.Rows(0)("destino") Is DBNull.Value, String.Empty, dt.Rows(0)("destino").ToString()))
            VerificarTextBox()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            Label3.Text = "¿Desea eliminar la inscripción de lote o SENASA?"
            BBorrarsi.Visible = True
            BBorrarno.Visible = True
            BConfirm.Visible = False
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If

        If (e.CommandName = "Subir") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            div_nuevo_prod.Visible = True
            DivGrid.Visible = False
            DivCrearNuevo.Visible = False

            'Label3.Text = "¿Desea eliminar la inscripción de lote o SENASA?"
            'BBorrarsi.Visible = True
            'BBorrarno.Visible = True
            'BConfirm.Visible = False
            'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If

        If (e.CommandName = "Imprimir") Then

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim rptdocument As New ReportDocument
            'nombre de dataset
            Dim ds As New DataSetMultiplicador
            Dim Str As String = "SELECT * FROM vista_multi_lote WHERE nombre_multiplicador = @valor AND id_lote = @valor2"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            adap.SelectCommand.Parameters.AddWithValue("@valor2", Convert.ToInt32(HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString))
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "vista_multi_lote")

            Dim nombre As String

            nombre = " Datos del Lote " + HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString + " " + Today

            rptdocument.Load(Server.MapPath("~/pages/AgregarMultiplicadorReport2.rpt"))

            rptdocument.SetDataSource(ds)
            Response.Buffer = False


            Response.ClearContent()
            Response.ClearHeaders()

            rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

            Response.End()
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

        End If
    End Sub

    Protected Sub GridDatos_DataBound(sender As Object, e As EventArgs) Handles GridDatos.DataBound
        If (GridDatos.Rows.Count > 0) Then
            ' Recupera la el PagerRow...
            Dim pagerRow As GridViewRow = GridDatos.BottomPagerRow
            ' Recupera los controles DropDownList y label...
            Dim pageList As DropDownList = CType(pagerRow.Cells(0).FindControl("PageDropDownList"), DropDownList)
            Dim pageLabel As Label = CType(pagerRow.Cells(0).FindControl("CurrentPageLabel"), Label)
            If Not pageList Is Nothing Then
                ' Se crean los valores del DropDownList tomando el número total de páginas...
                Dim i As Integer
                For i = 0 To GridDatos.PageCount - 1
                    ' Se crea un objeto ListItem para representar la �gina...
                    Dim pageNumber As Integer = i + 1
                    Dim item As ListItem = New ListItem(pageNumber.ToString())
                    If i = GridDatos.PageIndex Then
                        item.Selected = True
                    End If
                    ' Se añade el ListItem a la colección de Items del DropDownList...
                    pageList.Items.Add(item)
                Next i
            End If
            If Not pageLabel Is Nothing Then
                ' Calcula el nº de �gina actual...
                Dim currentPage As Integer = GridDatos.PageIndex + 1
                ' Actualiza el Label control con la �gina actual.
                pageLabel.Text = "Página " & currentPage.ToString() & " de " & GridDatos.PageCount.ToString()
            End If
        End If
    End Sub

    Protected Function SeleccionarItemEnDropDownList(ByVal Prodname As DropDownList, ByVal DtCombo As String)
        For Each item As ListItem In Prodname.Items
            If item.Text = DtCombo Then
                Prodname.SelectedValue = item.Value
                Return True ' Se encontró una coincidencia, devolver verdadero
            End If
        Next
        ' No se encontró ninguna coincidencia
        Return 0
    End Function

    Protected Sub elminar(sender As Object, e As EventArgs) Handles BBorrarsi.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_registro_lote 
                    SET estado = @estado
                WHERE id = " & txtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()

                Response.Redirect(String.Format("~/pages/InscripcionLotes.aspx"))
            End Using

        End Using

    End Sub


    '**************************************************************************************************************************************


    Protected Sub CmbTipoSemilla_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtiene el valor seleccionado en la DropDownList
        Dim selectedValue As String = CmbTipoSemilla.SelectedValue

        ' Si selecciona "Frijol," muestra la TextBox de Variedad; de lo contrario, ocúltala
        If selectedValue = "Frijol" Then
            DropDownList6.SelectedIndex = 0
            VariedadFrijol.Visible = True
            VariedadMaiz.Visible = False
        ElseIf selectedValue = "Maiz" Then
            VariedadMaiz.Visible = True
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
        Else
            VariedadMaiz.Visible = False
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
            DropDownList6.SelectedIndex = 0
        End If

        VerificarTextBox()
    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtiene el valor seleccionado en la DropDownList
        Dim selectedValue As String = DropDownList3.SelectedValue

        ' Si selecciona "Frijol," muestra la TextBox de Variedad; de lo contrario, ocúltala
        If selectedValue = "Frijol" Then
            DropDownList2.SelectedIndex = 0
            variedadfrijol2.Visible = True
            variedadmaiz2.Visible = False
        ElseIf selectedValue = "Maiz" Then
            variedadmaiz2.Visible = True
            variedadfrijol2.Visible = False
            DropDownList1.SelectedIndex = 0
        Else
            variedadmaiz2.Visible = False
            variedadfrijol2.Visible = False
            DropDownList1.SelectedIndex = 0
            DropDownList2.SelectedIndex = 0
        End If

        VerificarTextBox()
    End Sub

    Private Function FileUploadToBytes(fileUpload As FileUpload) As Byte()
        Using stream As System.IO.Stream = fileUpload.PostedFile.InputStream
            Dim length As Integer = fileUpload.PostedFile.ContentLength
            Dim bytes As Byte() = New Byte(length - 1) {}
            stream.Read(bytes, 0, length)
            Return bytes
        End Using
    End Function

    Protected Function ValidarFormulario() As Boolean
        Dim esValido As Boolean = True
        Label18.Visible = False
        Label21.Visible = False
        If Not FileUploadPagoTGR.HasFile OrElse Not EsExtensionValida(FileUploadPagoTGR.FileName) Then
            Label18.Visible = True
            esValido = False
        End If
        If Not FileUploadEtiquetaSemilla.HasFile OrElse Not EsExtensionValida(FileUploadEtiquetaSemilla.FileName) Then
            Label21.Visible = True
            esValido = False
        End If

        Return esValido
    End Function

    Protected Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click

        If ValidarFormulario() Then

            Dim connectionString As String = conn
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim bytesFichaSemilla As Byte() = FileUploadToBytes(FileUploadEtiquetaSemilla)
                Dim bytesPagoTGR As Byte() = FileUploadToBytes(FileUploadPagoTGR)

                ' Actualizar bytes en la base de datos
                Dim query As String = "UPDATE sag_registro_lote SET certificado_origen_semilla = @certificado_origen_semilla, factura_comercio = @factura_comercio WHERE ID=" & txtID.Text & " "
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@certificado_origen_semilla", bytesFichaSemilla)
                    cmd.Parameters.AddWithValue("@factura_comercio", bytesPagoTGR)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Label23.Visible = False
            Label25.Visible = True
            BtnUpload.Visible = False
        Else
            Label23.Visible = True
            Label25.Visible = False
            BtnUpload.Visible = True
        End If

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect(String.Format("~/pages/InscripcionLotes.aspx"))

    End Sub

    Private Function EsExtensionValida(fileName As String) As Boolean
        Dim extension As String = Path.GetExtension(fileName)
        Dim esValida As Boolean = False
        If extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) OrElse
           extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) OrElse
           extension.Equals(".png", StringComparison.OrdinalIgnoreCase) Then
            esValida = True
        End If
        Return esValida
    End Function

    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtén los datos de la fila actual
            Dim estimadoProduccion As String = DataBinder.Eval(e.Row.DataItem, "no_lote").ToString()
            Dim tipoSemilla As String = DataBinder.Eval(e.Row.DataItem, "certificado_origen_semilla").ToString()
            Dim tipoSemilla2 As String = DataBinder.Eval(e.Row.DataItem, "factura_comercio").ToString()

            ' Encuentra los botones en la fila por índice
            Dim btnEditar As Button = DirectCast(e.Row.Cells(8).Controls(0), Button) ' Ajusta el índice según la posición de tu botón en la fila
            Dim btnEliminar As Button = DirectCast(e.Row.Cells(9).Controls(0), Button) ' Ajusta el índice según la posición de tu botón en la fila
            Dim btnImprimir As Button = DirectCast(e.Row.Cells(10).Controls(0), Button) ' Ajusta el índice según la posición de tu botón en la fila

            ' Modifica el texto y el color de los botones según la lógica que desees
            If Not String.IsNullOrEmpty(estimadoProduccion) Then
                btnEditar.Text = "Editar Lote"
                btnEditar.CssClass = "btn btn-primary"
                btnEditar.Style("background-color") = "#007bff" ' Establece el color de fondo directamente
            Else
                btnEditar.Text = "Agregar Lote"
                btnEditar.CssClass = "btn btn-success"
                btnEditar.Style("background-color") = "#28a745" ' Establece el color de fondo directamente
            End If

            If Not String.IsNullOrEmpty(tipoSemilla) And Not String.IsNullOrEmpty(tipoSemilla2) Then
                btnEliminar.Text = "Editar Archivos"
                btnEliminar.CssClass = "btn btn-primary"
                btnEliminar.Style("background-color") = "#007bff" ' Establece el color de fondo directamente
            Else
                btnEliminar.Text = "Agregar Archivos"
                btnEliminar.CssClass = "btn btn-success"
                btnEliminar.Style("background-color") = "#28a745" ' Establece el color de fondo directamente
            End If

            If btnEditar.Text = "Editar Lote" And btnEliminar.Text = "Editar Archivos" Then
                btnImprimir.Visible = True
            Else
                btnImprimir.Visible = False
            End If
        End If
    End Sub

    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/InscripcionLotes.aspx"))
    End Sub

    Protected Sub TextBox4_TextChanged(sender As Object, e As EventArgs)
        ' Verificar si TextBox4 tiene una fecha válida
        Dim fechaAnalisis As Date
        If Date.TryParse(TextBox4.Text, fechaAnalisis) Then
            ' Calcular la fecha 6 meses después
            Dim fechaCaducidad As Date = fechaAnalisis.AddMonths(6)
            ' Asignar la fecha calculada a txtFechaCad
            txtFechaCad.Text = fechaCaducidad.ToString("yyyy-MM-dd")
        Else
            ' Manejar el caso donde TextBox4 no contiene una fecha válida
        End If
    End Sub

    Private Sub exportar()

        Dim query As String = ""
        Dim cadena As String = "*"
        Dim c1 As String = ""
        Dim c2 As String = ""
        Dim c3 As String = ""

        If (TxtMultiplicador.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND nombre_multiplicador = '" & TxtMultiplicador.SelectedItem.Text & "' "
        End If

        If (TxtMunicipio.SelectedItem.Text = "Todos") Then
            c2 = " "
        Else
            c2 = "AND municipio = '" & TxtMunicipio.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        query = "SELECT " & cadena & " FROM `vista_multi_lote` WHERE 1 = 1 " & c1 & c2 & c3

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "sag_registro_lote"

                        Using wb As New XLWorkbook()
                            For Each dt As DataTable In ds.Tables
                                ' Add DataTable as Worksheet.
                                Dim ws As IXLWorksheet = wb.Worksheets.Add(dt)

                                ' Set auto width for all columns based on content.
                                ws.Columns().AdjustToContents()
                            Next

                            ' Export the Excel file.
                            Response.Clear()
                            Response.Buffer = True
                            Response.Charset = ""
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            Response.AddHeader("content-disposition", "attachment;filename=Registro de Lote  " & Today & " " & TxtMultiplicador.SelectedItem.Text & " " & TxtDepto.SelectedItem.Text & ".xlsx")
                            Using MyMemoryStream As New MemoryStream()
                                wb.SaveAs(MyMemoryStream)
                                MyMemoryStream.WriteTo(Response.OutputStream)
                                Response.Flush()
                                Response.End()
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub eliminar_caducidad_lote()
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim fechaEspecifica As Date = Date.Today

            Dim query As String = "UPDATE sag_registro_lote SET estado = 4 WHERE caducidad_lote = @fechaEspecifica"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@fechaEspecifica", fechaEspecifica)
                cmd.ExecuteNonQuery()
            End Using

            connection.Close()
        End Using
    End Sub

End Class
