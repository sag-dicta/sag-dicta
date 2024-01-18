Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class agregarMultiplicador
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
                'llenatxtproductor()
                llenagrid()
                btnGuardarLote.Visible = True
                btnRegresar.Visible = True
            End If
        End If
    End Sub

    Protected Sub guardarSoli_lote(sender As Object, e As EventArgs)
        VerificarTextBox()
        If validarflag = 0 Then
            LabelGuardar.Visible = True
            LabelGuardar.Text = "Ingrese toda la información para poder guardarla"
        Else
            If btnGuardarLote.Text = "Guardar" Then
                LabelGuardar.Visible = False
                LabelGuardar.Text = ""
                Dim connectionString As String = conn
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "INSERT INTO sag_registro_senasa (nombre_productor, representante_legal, identidad_productor, extendida, residencia_productor, telefono_productor, no_registro_productor, nombre_multiplicador, 
                cedula_multiplicador, telefono_multiplicador, nombre_finca, departamento, municipio, aldea, caserio, nombre_persona_finca, nombre_lote, croquis, estado) VALUES (@nombre_productor, @representante_legal, @identidad_productor, 
                @extendida, @residencia_productor, @telefono_productor, @no_registro_productor, @nombre_multiplicador, @cedula_multiplicador, @telefono_multiplicador, @nombre_finca, @departamento,
                @municipio, @aldea, @caserio, @nombre_persona_finca, @nombre_lote, @croquis, @estado)"

                    Dim fechaConvertida As DateTime


                    If DateTime.TryParse(TextBox1.Text, fechaConvertida) Then
                        fechaConvertida.ToString("dd-MM-yyyy")
                    End If

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@nombre_productor", txt_nombre_prod_new.Text)
                        cmd.Parameters.AddWithValue("@representante_legal", Txt_Representante_Legal.Text)
                        cmd.Parameters.AddWithValue("@identidad_productor", TxtIdentidad.Text)
                        If DateTime.TryParse(TextBox1.Text, fechaConvertida) Then
                            cmd.Parameters.AddWithValue("@extendida", fechaConvertida.ToString("yyyy-MM-dd")) ' Aquí se formatea correctamente como yyyy-MM-dd
                        End If
                        cmd.Parameters.AddWithValue("@residencia_productor", TxtResidencia.Text)
                        cmd.Parameters.AddWithValue("@telefono_productor", TxtTelefono.Text)
                        cmd.Parameters.AddWithValue("@no_registro_productor", txtNoRegistro.Text)
                        cmd.Parameters.AddWithValue("@nombre_multiplicador", txtNombreRe.Text)
                        cmd.Parameters.AddWithValue("@cedula_multiplicador", txtIdentidadRe.Text)
                        cmd.Parameters.AddWithValue("@telefono_multiplicador", TxtTelefonoRe.Text)
                        cmd.Parameters.AddWithValue("@nombre_finca", TxtNombreFinca.Text)
                        cmd.Parameters.AddWithValue("@departamento", gb_departamento_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@municipio", gb_municipio_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@aldea", gb_aldea_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@caserio", gb_caserio_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@nombre_persona_finca", TxtPersonaFinca.Text)
                        cmd.Parameters.AddWithValue("@nombre_lote", TxtLote.Text)
                        If fileUpload.HasFile Then
                            ' Obtener el contenido del archivo
                            Dim fileBytes As Byte() = fileUpload.FileBytes


                            cmd.Parameters.AddWithValue("@croquis", fileBytes)
                        End If
                        cmd.Parameters.AddWithValue("@estado", "1")

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        'Response.Write("<script>window.alert('¡Se ha registrado correctamente la solicitud del Multiplicador o Estación!') </script>")

                        Label3.Text = "¡Se ha registrado correctamente la solicitud del Multiplicador o Estación!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                        Button1.Visible = True
                        Button2.Visible = True
                        btnGuardarLote.Visible = False

                    End Using
                End Using
            Else
                LabelGuardar.Visible = False
                LabelGuardar.Text = ""
                Dim connectionString As String = conn
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "UPDATE sag_registro_senasa 
                    SET nombre_productor = @nombre_productor,
                        representante_legal = @representante_legal,
                        identidad_productor = @identidad_productor,
                        extendida = @extendida,
                        residencia_productor = @residencia_productor,
                        telefono_productor = @telefono_productor,
                        no_registro_productor = @no_registro_productor,
                        nombre_multiplicador = @nombre_multiplicador,
                        cedula_multiplicador = @cedula_multiplicador,
                        telefono_multiplicador = @telefono_multiplicador,
                        nombre_finca = @nombre_finca,
                        departamento = @departamento,
                        municipio = @municipio,
                        aldea = @aldea,
                        caserio = @caserio,
                        nombre_persona_finca = @nombre_persona_finca,
                        nombre_lote = @nombre_lote
                    WHERE id = " & txtID.Text & ""

                    Dim fechaConvertida As DateTime

                    If DateTime.TryParse(TextBox1.Text, fechaConvertida) Then
                        fechaConvertida.ToString("dd-MM-yyyy")
                    End If

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@nombre_productor", txt_nombre_prod_new.Text)
                        cmd.Parameters.AddWithValue("@representante_legal", Txt_Representante_Legal.Text)
                        cmd.Parameters.AddWithValue("@identidad_productor", TxtIdentidad.Text)
                        If DateTime.TryParse(TextBox1.Text, fechaConvertida) Then
                            cmd.Parameters.AddWithValue("@extendida", fechaConvertida.ToString("yyyy-MM-dd")) ' Aquí se formatea correctamente como yyyy-MM-dd
                        End If
                        cmd.Parameters.AddWithValue("@residencia_productor", TxtResidencia.Text)
                        cmd.Parameters.AddWithValue("@telefono_productor", TxtTelefono.Text)
                        cmd.Parameters.AddWithValue("@no_registro_productor", txtNoRegistro.Text)
                        cmd.Parameters.AddWithValue("@nombre_multiplicador", txtNombreRe.Text)
                        cmd.Parameters.AddWithValue("@cedula_multiplicador", txtIdentidadRe.Text)
                        cmd.Parameters.AddWithValue("@telefono_multiplicador", TxtTelefonoRe.Text)
                        cmd.Parameters.AddWithValue("@nombre_finca", TxtNombreFinca.Text)
                        cmd.Parameters.AddWithValue("@departamento", gb_departamento_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@municipio", gb_municipio_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@aldea", gb_aldea_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@caserio", gb_caserio_new.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@nombre_persona_finca", TxtPersonaFinca.Text)
                        cmd.Parameters.AddWithValue("@nombre_lote", TxtLote.Text)

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        'Response.Write("<script>window.alert('¡Se ha editado correctamente la solicitud del Multiplicador o Estación!') </script>")
                        Label3.Text = "¡Se ha editado correctamente la solicitud del Multiplicador o Estación!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
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
        btnGuardarLote.Visible = True
        Button1.Visible = False
        Button2.Visible = False
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


    'Protected Sub llenarProdutor()
    '    Dim StrCombo As String = "SELECT * FROM registros_bancos_semilla WHERE PROD_NOMBRE = @valor"
    '    Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
    '    adaptcombo.SelectCommand.Parameters.AddWithValue("@valor", txt_nombre_prod_new.Text)
    '    Dim DtCombo As New DataTable
    '    adaptcombo.Fill(DtCombo)
    '
    '    If DtCombo.Rows.Count > 0 Then
    '        txt_nombre_prod_new.Text = DtCombo.Rows(0)("PROD_NOMBRE").ToString
    '        TxtIdentidad.Text = DtCombo.Rows(0)("PROD_IDENTIDAD").ToString
    '        TxtTelefono.Text = DtCombo.Rows(0)("PROD_TELEFONO").ToString
    '
    '        btnGuardarLote.Visible = True
    '    Else
    '        Response.Write("<script>window.alert('¡No existe productor en la base de datos!') </script>")
    '    End If
    '    VerificarTextBox()
    'End Sub

    Protected Sub gb_departamento_new_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gb_departamento_new.SelectedIndexChanged
        llenarmunicipio()
        VerificarTextBox()
    End Sub

    Protected Sub gb_municipio_new_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gb_municipio_new.SelectedIndexChanged
        gb_caserio_new.Enabled = False
        llenarAldea()
        VerificarTextBox()
    End Sub

    Protected Sub gb_aldea_new_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gb_aldea_new.SelectedIndexChanged
        llenarCaserio()
        VerificarTextBox()
    End Sub

    Protected Sub VerificarTextBox()
        If TextBanderita.Text = "Guardar" Then
            '1
            If String.IsNullOrEmpty(txt_nombre_prod_new.Text) Then
                lb_nombre_new.Text = "*"
                validarflag = 0
            Else
                lb_nombre_new.Text = ""
                validarflag += 1
            End If
            '2
            If String.IsNullOrEmpty(Txt_Representante_Legal.Text) Then
                LB_RepresentanteLegal.Text = "*"
                validarflag = 0
            Else
                LB_RepresentanteLegal.Text = ""
                validarflag += 1
            End If
            '3
            If String.IsNullOrEmpty(TxtIdentidad.Text) Then
                Lb_CedulaIdentidad.Text = "*"
                validarflag = 0
            Else
                Lb_CedulaIdentidad.Text = ""
                validarflag += 1
            End If
            '4
            If String.IsNullOrEmpty(TextBox1.Text) Then
                Label1.Text = "*"
                validarflag = 0
            Else
                Label1.Text = ""
                validarflag += 1
            End If
            '5
            If String.IsNullOrEmpty(TxtResidencia.Text) Then
                LbResidencia.Text = "*"
                validarflag = 0
            Else
                LbResidencia.Text = ""
                validarflag += 1
            End If
            '6
            If String.IsNullOrEmpty(TxtTelefono.Text) Then
                LblTelefono.Text = "*"
                validarflag = 0
            Else
                LblTelefono.Text = ""
                validarflag += 1
            End If
            '7
            If String.IsNullOrEmpty(txtNoRegistro.Text) Then
                LbNoRegistro.Text = "*"
                validarflag = 0
            Else
                LbNoRegistro.Text = ""
                validarflag += 1
            End If
            '8
            If String.IsNullOrEmpty(txtNombreRe.Text) Then
                lbNombreRe.Text = "*"
                validarflag = 0
            Else
                lbNombreRe.Text = ""
                validarflag += 1
            End If
            '8
            If String.IsNullOrEmpty(txtIdentidadRe.Text) Then
                lbIdentidadRe.Text = "*"
                validarflag = 0
            Else
                lbIdentidadRe.Text = ""
                validarflag += 1
            End If
            '10
            If String.IsNullOrEmpty(TxtTelefonoRe.Text) Then
                LbTelefonoRe.Text = "*"
                validarflag = 0
            Else
                LbTelefonoRe.Text = ""
                validarflag += 1
            End If
            '11
            If String.IsNullOrEmpty(TxtNombreFinca.Text) Then
                LblNombreFinca.Text = "*"
                validarflag = 0
            Else
                LblNombreFinca.Text = ""
                validarflag += 1
            End If
            '12
            If (gb_departamento_new.SelectedItem.Text = " ") Then
                lb_dept_new.Text = "*"
                validarflag = 0
            Else
                lb_dept_new.Text = ""
                validarflag += 1
            End If
            '13
            If (gb_municipio_new.SelectedItem.Text = " ") Then
                lb_mun_new.Text = "*"
                validarflag = 0
            Else
                lb_mun_new.Text = ""
                validarflag += 1
            End If
            '14
            If (gb_aldea_new.SelectedItem.Text = " ") Then
                lb_aldea_new.Text = "*"
                validarflag = 0
            Else
                lb_aldea_new.Text = ""
                validarflag += 1
            End If
            '15
            If (gb_caserio_new.SelectedItem.Text = " ") Then
                lb_caserio_new.Text = "*"
                validarflag = 0
            Else
                lb_caserio_new.Text = ""
                validarflag += 1
            End If
            '16
            If String.IsNullOrEmpty(TxtPersonaFinca.Text) Then
                LblPersonaFinca.Text = "*"
                validarflag = 0
            Else
                LblPersonaFinca.Text = ""
                validarflag += 1
            End If
            '17
            If String.IsNullOrEmpty(TxtLote.Text) Then
                LbLote.Text = "*"
                validarflag = 0
            Else
                LbLote.Text = ""
                validarflag += 1
            End If
            '18
            If fileUpload.HasFile AndAlso EsExtensionValida(fileUpload.FileName) Then
                validarflag += 1
            Else
                validarflag = 0
                Label25.Visible = True
            End If

            If validarflag = 18 Then
                validarflag = 1
            Else
                validarflag = 0
            End If
        Else
            '1
            If String.IsNullOrEmpty(txt_nombre_prod_new.Text) Then
                lb_nombre_new.Text = "*"
                validarflag = 0
            Else
                lb_nombre_new.Text = ""
                validarflag += 1
            End If
            '2
            If String.IsNullOrEmpty(Txt_Representante_Legal.Text) Then
                LB_RepresentanteLegal.Text = "*"
                validarflag = 0
            Else
                LB_RepresentanteLegal.Text = ""
                validarflag += 1
            End If
            '3
            If String.IsNullOrEmpty(TxtIdentidad.Text) Then
                Lb_CedulaIdentidad.Text = "*"
                validarflag = 0
            Else
                Lb_CedulaIdentidad.Text = ""
                validarflag += 1
            End If
            '4
            If String.IsNullOrEmpty(TextBox1.Text) Then
                Label1.Text = "*"
                validarflag = 0
            Else
                Label1.Text = ""
                validarflag += 1
            End If
            '5
            If String.IsNullOrEmpty(TxtResidencia.Text) Then
                LbResidencia.Text = "*"
                validarflag = 0
            Else
                LbResidencia.Text = ""
                validarflag += 1
            End If
            '6
            If String.IsNullOrEmpty(TxtTelefono.Text) Then
                LblTelefono.Text = "*"
                validarflag = 0
            Else
                LblTelefono.Text = ""
                validarflag += 1
            End If
            '7
            If String.IsNullOrEmpty(txtNoRegistro.Text) Then
                LbNoRegistro.Text = "*"
                validarflag = 0
            Else
                LbNoRegistro.Text = ""
                validarflag += 1
            End If
            '8
            If String.IsNullOrEmpty(txtNombreRe.Text) Then
                lbNombreRe.Text = "*"
                validarflag = 0
            Else
                lbNombreRe.Text = ""
                validarflag += 1
            End If
            '8
            If String.IsNullOrEmpty(txtIdentidadRe.Text) Then
                lbIdentidadRe.Text = "*"
                validarflag = 0
            Else
                lbIdentidadRe.Text = ""
                validarflag += 1
            End If
            '10
            If String.IsNullOrEmpty(TxtTelefonoRe.Text) Then
                LbTelefonoRe.Text = "*"
                validarflag = 0
            Else
                LbTelefonoRe.Text = ""
                validarflag += 1
            End If
            '11
            If String.IsNullOrEmpty(TxtNombreFinca.Text) Then
                LblNombreFinca.Text = "*"
                validarflag = 0
            Else
                LblNombreFinca.Text = ""
                validarflag += 1
            End If
            '12
            If (gb_departamento_new.SelectedItem.Text = " ") Then
                lb_dept_new.Text = "*"
                validarflag = 0
            Else
                lb_dept_new.Text = ""
                validarflag += 1
            End If
            '13
            If (gb_municipio_new.SelectedItem.Text = " ") Then
                lb_mun_new.Text = "*"
                validarflag = 0
            Else
                lb_mun_new.Text = ""
                validarflag += 1
            End If
            '14
            If (gb_aldea_new.SelectedItem.Text = " ") Then
                lb_aldea_new.Text = "*"
                validarflag = 0
            Else
                lb_aldea_new.Text = ""
                validarflag += 1
            End If
            '15
            If (gb_caserio_new.SelectedItem.Text = " ") Then
                lb_caserio_new.Text = "*"
                validarflag = 0
            Else
                lb_caserio_new.Text = ""
                validarflag += 1
            End If
            '16
            If String.IsNullOrEmpty(TxtPersonaFinca.Text) Then
                LblPersonaFinca.Text = "*"
                validarflag = 0
            Else
                LblPersonaFinca.Text = ""
                validarflag += 1
            End If
            '17
            If String.IsNullOrEmpty(TxtLote.Text) Then
                LbLote.Text = "*"
                validarflag = 0
            Else
                LbLote.Text = ""
                validarflag += 1
            End If

            If validarflag = 17 Then
                validarflag = 1
            Else
                validarflag = 0
            End If
        End If
    End Sub

    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE nombre_multiplicador = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", txtNombreRe.Text)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "sag_registro_senasa1")

        Dim nombre As String

        nombre = " Datos del Multiplicador " + Today

        rptdocument.Load(Server.MapPath("~/pages/AgregarMultiplicadorReport.rpt"))

        rptdocument.SetDataSource(ds)
        Response.Buffer = False


        Response.ClearContent()
        Response.ClearHeaders()

        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub
    Sub llenatxtproductor()
        Dim id2 As String = Request.QueryString("id")

        If Not String.IsNullOrEmpty(id2) Then
            txt_nombre_prod_new.Text = id2
        Else
            txt_nombre_prod_new.Text = " "
        End If
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
        Dim cadena As String = "id, nombre_productor, nombre_finca, no_registro_productor, nombre_multiplicador, cedula_multiplicador, departamento, municipio"
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

        BAgregar.Visible = True
        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_registro_senasa` WHERE 1 = 1 AND estado = '1' " & c1 & c3 & c4

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
        llenarcomboProductor2()
        llenagrid()
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
        llenarcomboProductor()
        llenagrid()
    End Sub

    Private Sub llenarcomboProductor()
        Dim StrCombo As String

        StrCombo = "SELECT * FROM sag_registro_senasa WHERE municipio = '" & TxtMunicipio.SelectedItem.Text & "' "

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
    Private Sub llenarcomboProductor2()
        Dim StrCombo As String

        StrCombo = "SELECT * FROM sag_registro_senasa WHERE departamento = '" & TxtDepto.SelectedItem.Text & "' "

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
        TextBanderita.Text = "Guardar"

        If (TxtMultiplicador.SelectedIndex <> 0) Then
            txtNombreRe.Text = TxtMultiplicador.SelectedItem.Text
        End If

        If TxtDepto.SelectedIndex <> 0 Then
            gb_departamento_new.SelectedIndex = TxtDepto.SelectedIndex
            llenarmunicipio()

            If TxtMunicipio.SelectedIndex = 0 Then
                gb_municipio_new.SelectedIndex = 0
            Else
                gb_municipio_new.SelectedIndex = TxtMunicipio.SelectedIndex
                llenarAldea()
            End If
        End If
        VerificarTextBox()
        'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

    End Sub

    Protected Sub TxtMultiplicador_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/agregarMultiplicador.aspx"))
    End Sub

    Private Sub exportar()

        Dim query As String = ""
        Dim cadena As String = "id, nombre_productor, nombre_finca, no_registro_productor, nombre_multiplicador, cedula_multiplicador, departamento, municipio"
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

        query = "SELECT " & cadena & " FROM sag_registro_senasa WHERE 1 = 1 " & c1 & c2 & c3

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "sag_registro_senasa"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Registro de Multiplicador  " & Today & " " & TxtMultiplicador.SelectedItem.Text & " " & TxtDepto.SelectedItem.Text & ".xlsx")
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

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        exportar()
    End Sub

    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Editar") Then
            btnGuardarLote.Text = "Editar"
            TextBanderita.Text = "Editar"
            Button1.Visible = False
            Button2.Visible = False
            DivCrearNuevo.Visible = True
            DivGrid.Visible = False
            fileUP.Visible = False
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE  ID='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            nuevo = False
            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            txt_nombre_prod_new.Text = dt.Rows(0)("nombre_productor").ToString()
            Txt_Representante_Legal.Text = dt.Rows(0)("representante_legal").ToString()
            TxtIdentidad.Text = dt.Rows(0)("identidad_productor").ToString()
            TextBox1.Text = DirectCast(dt.Rows(0)("extendida"), DateTime).ToString("yyyy-MM-dd")
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
            VerificarTextBox()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            Label3.Text = "¿Desea eliminar la solicitud del Multiplicador o Estación?"
            BBorrarsi.Visible = True
            BBorrarno.Visible = True
            BConfirm.Visible = False
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If

        If (e.CommandName = "Imprimir") Then

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim rptdocument As New ReportDocument
            'nombre de dataset
            Dim ds As New DataSetMultiplicador
            Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE nombre_multiplicador = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "sag_registro_senasa1")

            Dim nombre As String

            nombre = " Datos del Multiplicador " + HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString + " " + Today

            rptdocument.Load(Server.MapPath("~/pages/AgregarMultiplicadorReport.rpt"))

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

            Dim query As String = "UPDATE sag_registro_senasa 
                    SET estado = @estado
                WHERE id = " & txtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()

                Response.Redirect(String.Format("~/pages/agregarMultiplicador.aspx"))
            End Using

        End Using

    End Sub

End Class
