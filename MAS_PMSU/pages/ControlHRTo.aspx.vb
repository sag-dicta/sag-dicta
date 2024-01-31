Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class ControlHRTo
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
                llenarcomboDeptoGrid()
                llenarcomboProductor3()
                VerificarTextBox()
                llenagrid()
                btnGuardarLote.Visible = False
                btnRegresar.Visible = False
                DivGrid.Visible = True
                DivCrearNuevo.Visible = False
            End If
        End If
    End Sub

    Protected Sub guardarSoli_lote(sender As Object, e As EventArgs)
        VerificarTextBox()
        If validarflag = 1 Then
            GuardarMonitoreo()
        Else
            LabelGuardar.Visible = True
            LabelGuardar.Text = "Ingrese toda la información para poder guardarla"
        End If

    End Sub

    Protected Sub vaciar(sender As Object, e As EventArgs)
        'FileUpload
        btnGuardarLote.Visible = True
        Button1.Visible = False
        Button2.Visible = False
        VerificarTextBox()
    End Sub


    Protected Sub buscar_productor(sender As Object, e As EventArgs)
        VerificarTextBox()
    End Sub
    Protected Sub VerificarTextBox()
        ' 1
        If String.IsNullOrEmpty(TxtFechaMonitoreo.Text) Then
            lblfechamoni.Text = "*"
            validarflag = 0
        Else
            lblfechamoni.Text = ""
            validarflag += 1
        End If
        ' 2
        If String.IsNullOrEmpty(txtDatoExtTemp.Text) Then
            Labeldatoexttemp.Text = "*"
            validarflag = 0
        Else
            Labeldatoexttemp.Text = ""
            validarflag += 1
        End If
        ' 3
        If String.IsNullOrEmpty(txtDatoExtHume.Text) Then
            Labeldatoexthume.Text = "*"
            validarflag = 0
        Else
            Labeldatoexthume.Text = ""
            validarflag += 1
        End If
        '4
        ' Verificar al menos una camara este seleccionado
        If (String.IsNullOrWhiteSpace(txtCam1Hora.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam1Temp.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam1Humd.Text)) AndAlso
           (String.IsNullOrWhiteSpace(txtCam2Hora.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam2Temp.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam2Humd.Text)) AndAlso
           (String.IsNullOrWhiteSpace(txtCam3Hora.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam3Temp.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam3Humd.Text)) AndAlso
           (String.IsNullOrWhiteSpace(txtCam4Hora.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam4Temp.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam4Humd.Text)) AndAlso
           (String.IsNullOrWhiteSpace(txtCam5Hora.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam5Temp.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam5Humd.Text)) AndAlso
           (String.IsNullOrWhiteSpace(txtCam6Hora.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam6Temp.Text) OrElse
            String.IsNullOrWhiteSpace(txtCam6Humd.Text)) Then
            lblmensaje.Text = "Registre al menos una camara."
            validarflag = 0
        Else
            lblmensaje.Text = ""
            validarflag += 1
        End If

        If validarflag >= 4 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub
    Protected Sub txtCam1Hora_TextChanged(sender As Object, e As EventArgs) Handles txtCam1Hora.TextChanged
        VerificarTextBox()
        horavaciaVerifica(txtCam1Hora.Text, btnHora1)
    End Sub
    Protected Sub txtCam1Humd_TextChanged(sender As Object, e As EventArgs) Handles txtCam1Humd.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam1Temp_TextChanged(sender As Object, e As EventArgs) Handles txtCam1Temp.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam2Hora_TextChanged(sender As Object, e As EventArgs) Handles txtCam2Hora.TextChanged
        VerificarTextBox()
        horavaciaVerifica(txtCam2Hora.Text, btnhora2)
    End Sub
    Protected Sub CtxtCam2Humd_TextChanged(sender As Object, e As EventArgs) Handles txtCam2Humd.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam2Temp_TextChanged(sender As Object, e As EventArgs) Handles txtCam2Temp.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam3Hora_TextChanged(sender As Object, e As EventArgs) Handles txtCam3Hora.TextChanged
        VerificarTextBox()
        horavaciaVerifica(txtCam3Hora.Text, btnhora3)
    End Sub
    Protected Sub txtCam3Humd_TextChanged(sender As Object, e As EventArgs) Handles txtCam3Humd.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam3Temp_TextChanged(sender As Object, e As EventArgs) Handles txtCam3Temp.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam4Hora_TextChanged(sender As Object, e As EventArgs) Handles txtCam4Hora.TextChanged
        VerificarTextBox()
        horavaciaVerifica(txtCam4Hora.Text, btnhora4)
    End Sub
    Protected Sub txtCam4Humd_TextChanged(sender As Object, e As EventArgs) Handles txtCam4Humd.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam4Temp_TextChanged(sender As Object, e As EventArgs) Handles txtCam4Temp.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam5Hora_TextChanged(sender As Object, e As EventArgs) Handles txtCam5Hora.TextChanged
        VerificarTextBox()
        horavaciaVerifica(txtCam5Hora.Text, btnhora5)
    End Sub
    Protected Sub txtCam5Humd_TextChanged(sender As Object, e As EventArgs) Handles txtCam5Humd.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam5Temp_TextChanged(sender As Object, e As EventArgs) Handles txtCam5Temp.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam6Hora_TextChanged(sender As Object, e As EventArgs) Handles txtCam6Hora.TextChanged
        VerificarTextBox()
        horavaciaVerifica(txtCam6Hora.Text, btnhora6)
    End Sub
    Protected Sub txtCam6Humd_TextChanged(sender As Object, e As EventArgs) Handles txtCam6Humd.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtCam6Temp_TextChanged(sender As Object, e As EventArgs) Handles txtCam6Temp.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub TxtFechaMonitoreo_TextChanged(sender As Object, e As EventArgs) Handles TxtFechaMonitoreo.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtDatoExtHume_TextChanged(sender As Object, e As EventArgs) Handles txtDatoExtHume.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub txtDatoExtTemp_TextChanged(sender As Object, e As EventArgs) Handles txtDatoExtTemp.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM sag_registro_multiplicador WHERE nombre_multiplicador = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", TxtFechaMonitoreo.Text)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "sag_registro_multiplicador")

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
        Dim cadena As String = "id,DATE_FORMAT(fecha_monitoreo, '%d-%m-%Y') AS fecha_monitoreo," &
                       "DATE_FORMAT(camara1_hora, '%H:%i') AS camara1_hora," &
                       "camara1_temperatura,camara1_humedad," &
                       "DATE_FORMAT(camara2_hora, '%H:%i') AS camara2_hora," &
                       "camara2_temperatura,camara2_humedad," &
                       "DATE_FORMAT(camara3_hora, '%H:%i') AS camara3_hora," &
                       "camara3_temperatura,camara3_humedad," &
                       "DATE_FORMAT(camara4_hora, '%H:%i') AS camara4_hora," &
                       "camara4_temperatura,camara4_humedad," &
                       "DATE_FORMAT(camara5_hora, '%H:%i') AS camara5_hora," &
                       "camara5_temperatura,camara5_humedad," &
                       "DATE_FORMAT(camara6_hora, '%H:%i') AS camara6_hora," &
                       "camara6_temperatura,camara6_humedad," &
                       "externo_temperatura,externo_humedad,estado"

        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        'If (TxtMultiplicador.SelectedItem.Text = "Todos") Then
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
        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_control_temperatura_humedad` WHERE 1 = 1 AND estado = '1' " & c1 & c3 & c4

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
        If TxtDepto.SelectedItem.Text = "Todos" Then
            llenarcomboProductor3()
        Else
            llenarcomboProductor2()
        End If
        llenagrid()
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

        StrCombo = "SELECT * FROM sag_registro_multiplicador WHERE estado = '1' AND municipio = '" & TxtMunicipio.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"

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

        StrCombo = "SELECT * FROM sag_registro_multiplicador WHERE estado = '1' AND departamento = '" & TxtDepto.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"

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

        StrCombo = "SELECT * FROM sag_registro_multiplicador WHERE estado = '1' ORDER BY nombre_multiplicador ASC"

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
        'DivActa.Visible = True
        DivGrid.Visible = False
        DivCrearNuevo.Visible = True
        btnGuardarLote.Visible = True
        btnRegresar.Visible = True
        btnGuardarLote.Text = "Guardar"
        'BtnNuevo.Visible = True
        'btnGuardarActa.Text = "Guardar"



        VerificarTextBox()
    End Sub

    Protected Sub TxtMultiplicador_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/ControlHRTo.aspx"))
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        exportar()
    End Sub
    Protected Sub horavaciaVerifica(data As String, btn As Button)
        If Not String.IsNullOrEmpty(data) Then
            btn.Visible = True
        End If
    End Sub
    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Editar") Then
            DivGrid.Visible = False
            DivCrearNuevo.Visible = True
            btnGuardarLote.Visible = True
            btnRegresar.Visible = True
            btnGuardarLote.Text = "Actualizar"

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "*"
            Dim Str As String = "SELECT " & cadena & " FROM sag_control_temperatura_humedad WHERE  ID='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            Textid.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            TxtFechaMonitoreo.Text = If(dt.Rows(0)("fecha_monitoreo") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_monitoreo"), DateTime).ToString("yyyy-MM-dd"))

            txtCam1Hora.Text = If(Not DBNull.Value.Equals(dt.Rows(0)("camara1_hora")) AndAlso TypeOf dt.Rows(0)("camara1_hora") Is TimeSpan,
                      DirectCast(dt.Rows(0)("camara1_hora"), TimeSpan).ToString("hh\:mm"),
                      String.Empty)
            horavaciaVerifica(dt.Rows(0)("camara1_hora").ToString, btnHora1)
            txtCam1Temp.Text = If(dt.Rows(0)("camara1_temperatura") Is DBNull.Value, String.Empty, dt.Rows(0)("camara1_temperatura").ToString())
            txtCam1Humd.Text = If(dt.Rows(0)("camara1_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("camara1_humedad").ToString())

            txtCam2Hora.Text = If(Not DBNull.Value.Equals(dt.Rows(0)("camara2_hora")) AndAlso TypeOf dt.Rows(0)("camara2_hora") Is TimeSpan,
                      DirectCast(dt.Rows(0)("camara2_hora"), TimeSpan).ToString("hh\:mm"),
                      String.Empty)
            horavaciaVerifica(dt.Rows(0)("camara2_hora").ToString, btnhora2)
            txtCam2Temp.Text = If(dt.Rows(0)("camara2_temperatura") Is DBNull.Value, String.Empty, dt.Rows(0)("camara2_temperatura").ToString())
            txtCam2Humd.Text = If(dt.Rows(0)("camara2_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("camara2_humedad").ToString())

            txtCam3Hora.Text = If(Not DBNull.Value.Equals(dt.Rows(0)("camara3_hora")) AndAlso TypeOf dt.Rows(0)("camara3_hora") Is TimeSpan,
                      DirectCast(dt.Rows(0)("camara3_hora"), TimeSpan).ToString("hh\:mm"),
                      String.Empty)
            horavaciaVerifica(dt.Rows(0)("camara3_hora").ToString, btnhora3)
            txtCam3Temp.Text = If(dt.Rows(0)("camara3_temperatura") Is DBNull.Value, String.Empty, dt.Rows(0)("camara3_temperatura").ToString())
            txtCam3Humd.Text = If(dt.Rows(0)("camara3_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("camara3_humedad").ToString())

            txtCam4Hora.Text = If(Not DBNull.Value.Equals(dt.Rows(0)("camara4_hora")) AndAlso TypeOf dt.Rows(0)("camara4_hora") Is TimeSpan,
                      DirectCast(dt.Rows(0)("camara4_hora"), TimeSpan).ToString("hh\:mm"),
                      String.Empty)
            horavaciaVerifica(dt.Rows(0)("camara4_hora").ToString, btnhora4)
            txtCam4Temp.Text = If(dt.Rows(0)("camara4_temperatura") Is DBNull.Value, String.Empty, dt.Rows(0)("camara4_temperatura").ToString())
            txtCam4Humd.Text = If(dt.Rows(0)("camara4_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("camara4_humedad").ToString())

            txtCam5Hora.Text = If(Not DBNull.Value.Equals(dt.Rows(0)("camara4_hora")) AndAlso TypeOf dt.Rows(0)("camara4_hora") Is TimeSpan,
                      DirectCast(dt.Rows(0)("camara4_hora"), TimeSpan).ToString("hh\:mm"),
                      String.Empty)
            horavaciaVerifica(dt.Rows(0)("camara5_hora").ToString, btnHora5)
            txtCam5Temp.Text = If(dt.Rows(0)("camara5_temperatura") Is DBNull.Value, String.Empty, dt.Rows(0)("camara5_temperatura").ToString())
            txtCam5Humd.Text = If(dt.Rows(0)("camara5_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("camara5_humedad").ToString())

            txtCam6Hora.Text = If(Not DBNull.Value.Equals(dt.Rows(0)("camara6_hora")) AndAlso TypeOf dt.Rows(0)("camara6_hora") Is TimeSpan,
                      DirectCast(dt.Rows(0)("camara6_hora"), TimeSpan).ToString("hh\:mm"),
                      String.Empty)
            horavaciaVerifica(dt.Rows(0)("camara6_hora").ToString, btnhora6)
            txtCam6Temp.Text = If(dt.Rows(0)("camara6_temperatura") Is DBNull.Value, String.Empty, dt.Rows(0)("camara6_temperatura").ToString())
            txtCam6Humd.Text = If(dt.Rows(0)("camara6_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("camara6_humedad").ToString())

            txtDatoExtTemp.Text = If(dt.Rows(0)("externo_temperatura") Is DBNull.Value, String.Empty, dt.Rows(0)("externo_temperatura").ToString())
            txtDatoExtHume.Text = If(dt.Rows(0)("externo_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("externo_humedad").ToString())
            VerificarTextBox()
        End If

        If (e.CommandName = "Eliminar") Then
        End If

        If (e.CommandName = "Imprimir") Then

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim rptdocument As New ReportDocument
            'nombre de dataset
            Dim ds As New DataSetMultiplicador
            Dim Str As String = "SELECT * FROM sag_registro_multiplicador WHERE nombre_multiplicador = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "sag_registro_multiplicador")

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

        query = "SELECT " & cadena & " FROM sag_registro_multiplicador WHERE 1 = 1 " & c1 & c2 & c3

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "sag_registro_multiplicador"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Información del Lote " & Today & " " & TxtMultiplicador.SelectedItem.Text & " " & TxtDepto.SelectedItem.Text & ".xlsx")
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
    Protected Sub GuardarMonitoreo()
        Dim fechaConvertida As DateTime
        Dim hora1 As DateTime
        Dim hora2 As DateTime
        Dim hora3 As DateTime
        Dim hora4 As DateTime
        Dim hora5 As DateTime
        Dim hora6 As DateTime
        If btnGuardarLote.Text = "Actualizar" Then
            LabelGuardar.Visible = False
            LabelGuardar.Text = ""
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim sql As String = "UPDATE sag_control_temperatura_humedad SET " &
                    "camara1_hora = @camara1_hora, camara1_temperatura = @camara1_temperatura, camara1_humedad = @camara1_humedad, " &
                    "camara2_hora = @camara2_hora, camara2_temperatura = @camara2_temperatura, camara2_humedad = @camara2_humedad, " &
                    "camara3_hora = @camara3_hora, camara3_temperatura = @camara3_temperatura, camara3_humedad = @camara3_humedad, " &
                    "camara4_hora = @camara4_hora, camara4_temperatura = @camara4_temperatura, camara4_humedad = @camara4_humedad, " &
                    "camara5_hora = @camara5_hora, camara5_temperatura = @camara5_temperatura, camara5_humedad = @camara5_humedad, " &
                    "camara6_hora = @camara6_hora, camara6_temperatura = @camara6_temperatura, camara6_humedad = @camara6_humedad, " &
                    "externo_temperatura = @externo_temperatura, externo_humedad = @externo_humedad " &
                    "WHERE id = " & Textid.Text & ""

                Using cmd As New MySqlCommand(sql, connection)


                    If DateTime.TryParse(TxtFechaMonitoreo.Text, fechaConvertida) Then
                        cmd.Parameters.AddWithValue("@fecha_monitoreo", fechaConvertida.ToString("yyyy-MM-dd"))
                    End If
                    '1
                    If txtCam1Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam1Hora.Text, hora1) Then
                            cmd.Parameters.AddWithValue("@camara1_hora", hora1.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara1_hora", DBNull.Value)
                    End If
                    If txtCam1Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara1_temperatura", Convert.ToDecimal(txtCam1Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara1_temperatura", DBNull.Value)
                    End If
                    If txtCam1Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara1_humedad", Convert.ToDecimal(txtCam1Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara1_humedad", DBNull.Value)
                    End If
                    '2
                    If txtCam2Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam2Hora.Text, hora2) Then
                            cmd.Parameters.AddWithValue("@camara2_hora", hora2.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara2_hora", DBNull.Value)
                    End If
                    If txtCam2Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara2_temperatura", Convert.ToDecimal(txtCam2Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara2_temperatura", DBNull.Value)
                    End If
                    If txtCam2Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara2_humedad", Convert.ToDecimal(txtCam2Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara2_humedad", DBNull.Value)
                    End If
                    '3
                    If txtCam3Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam3Hora.Text, hora3) Then
                            cmd.Parameters.AddWithValue("@camara3_hora", hora3.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara3_hora", DBNull.Value)
                    End If
                    If txtCam3Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara3_temperatura", Convert.ToDecimal(txtCam3Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara3_temperatura", DBNull.Value)
                    End If
                    If txtCam3Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara3_humedad", Convert.ToDecimal(txtCam3Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara3_humedad", DBNull.Value)
                    End If
                    '4
                    If txtCam4Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam4Hora.Text, hora4) Then
                            cmd.Parameters.AddWithValue("@camara4_hora", hora4.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara4_hora", DBNull.Value)
                    End If
                    If txtCam4Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara4_temperatura", Convert.ToDecimal(txtCam4Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara4_temperatura", DBNull.Value)
                    End If
                    If txtCam4Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara4_humedad", Convert.ToDecimal(txtCam4Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara4_humedad", DBNull.Value)
                    End If
                    '5
                    If txtCam5Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam5Hora.Text, hora5) Then
                            cmd.Parameters.AddWithValue("@camara5_hora", hora5.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara5_hora", DBNull.Value)
                    End If
                    If txtCam5Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara5_temperatura", Convert.ToDecimal(txtCam5Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara5_temperatura", DBNull.Value)
                    End If
                    If txtCam5Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara5_humedad", Convert.ToDecimal(txtCam5Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara5_humedad", DBNull.Value)
                    End If
                    '6
                    If txtCam6Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam6Hora.Text, hora6) Then
                            cmd.Parameters.AddWithValue("@camara6_hora", hora6.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara6_hora", DBNull.Value)
                    End If
                    If txtCam6Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara6_temperatura", Convert.ToDecimal(txtCam6Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara6_temperatura", DBNull.Value)
                    End If
                    If txtCam6Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara6_humedad", Convert.ToDecimal(txtCam6Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara6_humedad", DBNull.Value)
                    End If

                    cmd.Parameters.AddWithValue("@externo_temperatura", Convert.ToDecimal(txtDatoExtTemp.Text))
                    cmd.Parameters.AddWithValue("@externo_humedad", Convert.ToDecimal(txtDatoExtHume.Text))

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label3.Text = "¡Se ha editado correctamente el registro de Control de Humedad y Temperatura!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    BConfirm.Visible = True
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarLote.Visible = False
                    btnRegresar.Visible = True

                End Using
            End Using
        Else
            LabelGuardar.Visible = False
            LabelGuardar.Text = ""
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim sql As String = "INSERT INTO sag_control_temperatura_humedad (
                        fecha_monitoreo,
                        camara1_hora,
                        camara1_temperatura,
                        camara1_humedad,
                        camara2_hora,
                        camara2_temperatura,
                        camara2_humedad,
                        camara3_hora,
                        camara3_temperatura,
                        camara3_humedad,
                        camara4_hora,
                        camara4_temperatura,
                        camara4_humedad,
                        camara5_hora,
                        camara5_temperatura,
                        camara5_humedad,
                        camara6_hora,
                        camara6_temperatura,
                        camara6_humedad,
                        externo_temperatura,
                        externo_humedad,
                        estado
                    ) VALUES (
                        @fecha_monitoreo,
                        @camara1_hora,
                        @camara1_temperatura,
                        @camara1_humedad,
                        @camara2_hora,
                        @camara2_temperatura,
                        @camara2_humedad,
                        @camara3_hora,
                        @camara3_temperatura,
                        @camara3_humedad,
                        @camara4_hora,
                        @camara4_temperatura,
                        @camara4_humedad,
                        @camara5_hora,
                        @camara5_temperatura,
                        @camara5_humedad,
                        @camara6_hora,
                        @camara6_temperatura,
                        @camara6_humedad,
                        @externo_temperatura,
                        @externo_humedad,
                        @estado
                    );"
                Using cmd As New MySqlCommand(sql, connection)

                    If DateTime.TryParse(TxtFechaMonitoreo.Text, fechaConvertida) Then
                        cmd.Parameters.AddWithValue("@fecha_monitoreo", fechaConvertida.ToString("yyyy-MM-dd"))
                    End If
                    '1
                    If txtCam1Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam1Hora.Text, hora1) Then
                            cmd.Parameters.AddWithValue("@camara1_hora", hora1.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara1_hora", DBNull.Value)
                    End If
                    If txtCam1Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara1_temperatura", Convert.ToDecimal(txtCam1Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara1_temperatura", DBNull.Value)
                    End If
                    If txtCam1Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara1_humedad", Convert.ToDecimal(txtCam1Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara1_humedad", DBNull.Value)
                    End If
                    '2
                    If txtCam2Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam2Hora.Text, hora2) Then
                            cmd.Parameters.AddWithValue("@camara2_hora", hora2.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara2_hora", DBNull.Value)
                    End If
                    If txtCam2Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara2_temperatura", Convert.ToDecimal(txtCam2Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara2_temperatura", DBNull.Value)
                    End If
                    If txtCam2Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara2_humedad", Convert.ToDecimal(txtCam2Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara2_humedad", DBNull.Value)
                    End If
                    '3
                    If txtCam3Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam3Hora.Text, hora3) Then
                            cmd.Parameters.AddWithValue("@camara3_hora", hora3.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara3_hora", DBNull.Value)
                    End If
                    If txtCam3Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara3_temperatura", Convert.ToDecimal(txtCam3Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara3_temperatura", DBNull.Value)
                    End If
                    If txtCam3Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara3_humedad", Convert.ToDecimal(txtCam3Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara3_humedad", DBNull.Value)
                    End If
                    '4
                    If txtCam4Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam4Hora.Text, hora4) Then
                            cmd.Parameters.AddWithValue("@camara4_hora", hora4.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara4_hora", DBNull.Value)
                    End If
                    If txtCam4Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara4_temperatura", Convert.ToDecimal(txtCam4Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara4_temperatura", DBNull.Value)
                    End If
                    If txtCam4Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara4_humedad", Convert.ToDecimal(txtCam4Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara4_humedad", DBNull.Value)
                    End If
                    '5
                    If txtCam5Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam5Hora.Text, hora5) Then
                            cmd.Parameters.AddWithValue("@camara5_hora", hora5.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara5_hora", DBNull.Value)
                    End If
                    If txtCam5Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara5_temperatura", Convert.ToDecimal(txtCam5Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara5_temperatura", DBNull.Value)
                    End If
                    If txtCam5Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara5_humedad", Convert.ToDecimal(txtCam5Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara5_humedad", DBNull.Value)
                    End If
                    '6
                    If txtCam6Hora.Text <> "" Then
                        If DateTime.TryParse(txtCam6Hora.Text, hora6) Then
                            cmd.Parameters.AddWithValue("@camara6_hora", hora6.ToString("HH:mm"))
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@camara6_hora", DBNull.Value)
                    End If
                    If txtCam6Temp.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara6_temperatura", Convert.ToDecimal(txtCam6Temp.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara6_temperatura", DBNull.Value)
                    End If
                    If txtCam6Humd.Text <> "" Then
                        cmd.Parameters.AddWithValue("@camara6_humedad", Convert.ToDecimal(txtCam6Humd.Text))
                    Else
                        cmd.Parameters.AddWithValue("@camara6_humedad", DBNull.Value)
                    End If

                    cmd.Parameters.AddWithValue("@externo_temperatura", Convert.ToDecimal(txtDatoExtTemp.Text))
                    cmd.Parameters.AddWithValue("@externo_humedad", Convert.ToDecimal(txtDatoExtHume.Text))
                    cmd.Parameters.AddWithValue("@estado", "1")

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label3.Text = "¡Se ha registrado correctamente el registro de Control de Humedad y Temperatura!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarLote.Visible = False
                    btnRegresar.Visible = True

                End Using
            End Using
        End If
    End Sub
    Protected Sub formatear(textfecha As TextBox, btn As Button)
        textfecha.Text = ""
        btn.Visible = False
    End Sub
    Protected Sub btn1Hora_TextChanged(sender As Object, e As EventArgs) Handles btnHora1.Click
        formatear(txtCam1Hora, btnHora1)
    End Sub
    Protected Sub btn2Hora_TextChanged(sender As Object, e As EventArgs) Handles btnhora2.Click
        formatear(txtCam2Hora, btnhora2)
    End Sub
    Protected Sub btn3Hora_TextChanged(sender As Object, e As EventArgs) Handles btnhora3.Click
        formatear(txtCam3Hora, btnhora3)
    End Sub
    Protected Sub btn4Hora_TextChanged(sender As Object, e As EventArgs) Handles btnhora4.Click
        formatear(txtCam4Hora, btnhora4)
    End Sub
    Protected Sub btn5Hora_TextChanged(sender As Object, e As EventArgs) Handles btnhora5.Click
        formatear(txtCam5Hora, btnhora5)
    End Sub
    Protected Sub btn6Hora_TextChanged(sender As Object, e As EventArgs) Handles btnhora6.Click
        formatear(txtCam6Hora, btnhora6)
    End Sub
End Class
