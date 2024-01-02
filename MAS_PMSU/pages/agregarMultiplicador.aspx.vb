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
                'llenarcomboDepto()
                llenarcomboDeptoGrid()
                VerificarTextBox()
                llenatxtproductor()
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
            LabelGuardar.Visible = False
            LabelGuardar.Text = ""
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO sag_registro_senasa (nombre_productor, representante_legar, identidad_productor, extendida, residencia_productor, telefono_productor, no_registro_productor, nombre_multiplicador, 
                cedula_multiplicador, telefono_multiplicador, nombre_finca, departamento, municipio, aldea, caserio, nombre_persona_finca, nombre_lote, croquis) VALUES (@nombre_productor, @representante_legal, @identidad_productor, 
                @extendida, @residencia_productor, @telefono_productor, @no_registro_productor, @nombre_multiplicador, @cedula_multiplicador, @telefono_multiplicador, @nombre_finca, @departamento,
                @municipio, @aldea, @caserio, @nombre_persona_finca, @nombre_lote, @croquis)"

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


                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Response.Write("<script>window.alert('¡Se ha registrado correctamente la solicitud del Multiplicador o Estación!') </script>")

                    Button1.Visible = True
                    Button2.Visible = True
                    btnGuardarLote.Visible = False

                End Using
            End Using
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
        gb_departamento_new.SelectedItem.Text = " "
        gb_municipio_new.SelectedItem.Text = " "
        gb_aldea_new.SelectedItem.Text = " "
        gb_caserio_new.SelectedItem.Text = " "
        TxtPersonaFinca.Text = " "
        TxtLote.Text = " "
        'FileUpload
        VerificarTextBox()
        Response.Redirect("agregarMultiplicador.aspx")
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
        llenarAldea()
        VerificarTextBox()
    End Sub

    Protected Sub gb_aldea_new_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gb_aldea_new.SelectedIndexChanged
        llenarCaserio()
        VerificarTextBox()
    End Sub

    Protected Sub VerificarTextBox()

        If String.IsNullOrEmpty(txt_nombre_prod_new.Text) Then
            lb_nombre_new.Text = "*"
            validarflag = 0
        Else
            lb_nombre_new.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(Txt_Representante_Legal.Text) Then
            LB_RepresentanteLegal.Text = "*"
            validarflag = 0
        Else
            LB_RepresentanteLegal.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtIdentidad.Text) Then
            Lb_CedulaIdentidad.Text = "*"
            validarflag = 0
        Else
            Lb_CedulaIdentidad.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TextBox1.Text) Then
            Label1.Text = "*"
            validarflag = 0
        Else
            Label1.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtResidencia.Text) Then
            LbResidencia.Text = "*"
            validarflag = 0
        Else
            LbResidencia.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtTelefono.Text) Then
            LblTelefono.Text = "*"
            validarflag = 0
        Else
            LblTelefono.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(txtNoRegistro.Text) Then
            LbNoRegistro.Text = "*"
            validarflag = 0
        Else
            LbNoRegistro.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(txtNombreRe.Text) Then
            lbNombreRe.Text = "*"
            validarflag = 0
        Else
            lbNombreRe.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(txtIdentidadRe.Text) Then
            lbIdentidadRe.Text = "*"
            validarflag = 0
        Else
            lbIdentidadRe.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtTelefonoRe.Text) Then
            LbTelefonoRe.Text = "*"
            validarflag = 0
        Else
            LbTelefonoRe.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtNombreFinca.Text) Then
            LblNombreFinca.Text = "*"
            validarflag = 0
        Else
            LblNombreFinca.Text = ""
            validarflag = 1
        End If

        If (gb_departamento_new.SelectedItem.Text = " ") Then
            lb_dept_new.Text = "*"
            validarflag = 0
        Else
            lb_dept_new.Text = ""
            validarflag = 1
        End If

        If (gb_municipio_new.SelectedItem.Text = " ") Then
            lb_mun_new.Text = "*"
            validarflag = 0
        Else
            lb_mun_new.Text = ""
            validarflag = 1
        End If

        If (gb_aldea_new.SelectedItem.Text = " ") Then
            lb_aldea_new.Text = "*"
            validarflag = 0
        Else
            lb_aldea_new.Text = ""
            validarflag = 1
        End If

        If (gb_caserio_new.SelectedItem.Text = " ") Then
            lb_caserio_new.Text = "*"
            validarflag = 0
        Else
            lb_caserio_new.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtPersonaFinca.Text) Then
            LblPersonaFinca.Text = "*"
            validarflag = 0
        Else
            LblPersonaFinca.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtLote.Text) Then
            LbLote.Text = "*"
            validarflag = 0
        Else
            LbLote.Text = ""
            validarflag = 1
        End If

        If fileUpload.HasFile AndAlso EsExtensionValida(fileUpload.FileName) Then
            validarflag = 1
        Else
            validarflag = 0
            Label25.Visible = True
        End If
    End Sub

    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        'Dim ds As New DataSetLotes
        Dim Str As String = "SELECT * FROM  WHERE nombre_lote = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", TxtLote.Text)
        Dim dt As New DataTable


        'nombre de la vista del data set

        adap.Fill(ds, "")

        Dim nombre As String

        nombre = " _" + Today

        rptdocument.Load(Server.MapPath("~/pages/.rpt"))

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

    Sub llenagrid()
        Dim cadena As String = "id, nombre_productor, nombre_finca, no_registro_productor, nombre_multiplicador, cedula_multiplicador, departamento, municipio"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        'If (TxtMultiplicador.SelectedItem.Text = "") Then
        '    c1 = " "
        'Else
        '    c1 = "AND nombre_multiplicador = '" & TxtMultiplicador.SelectedItem.Text & "' "
        'End If
        '
        'If (TxtMunicipio.SelectedItem.Text = "Todos") Then
        '    c3 = " "
        'Else
        '    c3 = "AND municipio = '" & TxtMunicipio.SelectedItem.Text & "' "
        'End If
        '
        'If (TxtDepto.SelectedItem.Text = "Todos") Then
        '    c4 = " "
        'Else
        '    c4 = "AND departamento = '" & TxtDepto.SelectedItem.Text & "' "
        'End If

        BAgregar.Visible = True
        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_registro_senasa` WHERE 1 = 1 " & c1 & c3 & c4
        'Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `registro_multiplicadores` WHERE 1 = 1 "

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

    Protected Sub TxtDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtDepto.SelectedIndexChanged
        llenarmunicipioGrid()
    End Sub

    Private Sub llenarmunicipioGrid()
        Dim departamento As String = DevolverValorDepart(TxtDepto.SelectedItem.Text)
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
    End Sub

    Private Sub llenarcomboProductor()
        Dim StrCombo As String

        StrCombo = "SELECT * FROM sag_registro_senasa WHERE municipio = '" & TxtMunicipio.SelectedItem.text & "' "

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        TxtMultiplicador.DataSource = DtCombo
        TxtMultiplicador.DataValueField = DtCombo.Columns(0).ToString()
        TxtMultiplicador.DataTextField = DtCombo.Columns(1).ToString()
        TxtMultiplicador.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtMultiplicador.Items.Insert(0, newitem)
    End Sub
    Protected Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

        DivCrearNuevo.Visible = True
        DivGrid.Visible = False

        If (TxtMultiplicador.SelectedIndex <> 0) Then
            txtNombreRe.Text = TxtMultiplicador.SelectedValue
        End If

        'If (TxtMunicipio.SelectedIndex <> 0) Then
        'gb_municipio_new.SelectedValue = TxtMunicipio.SelectedValue
        'End If

        'If (TxtDepto.SelectedIndex <> 0) Then
        'gb_departamento_new.SelectedValue = TxtDepto.SelectedValue
        'End If

        'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/agregarMultiplicador.aspx"))
    End Sub
End Class