Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class MonitoreoPlagas
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
            End If
        End If
    End Sub

    Protected Sub guardarSoli_lote(sender As Object, e As EventArgs)
        VerificarTextBox()
        If validarflag = 0 Then
            LabelGuardar.Visible = True
            LabelGuardar.Text = "Ingrese toda la información para poder guardarla"
        Else

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

        If validarflag = 17 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
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
        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_registro_multiplicador` WHERE 1 = 1 AND estado = '1' " & c1 & c3 & c4

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
        VerificarTextBox()
        'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

    End Sub

    Protected Sub TxtMultiplicador_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/agregarMultiplicador.aspx"))
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        exportar()
    End Sub

    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Editar") Then

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

    Protected Sub CalcularTotalInc(sender As Object, e As EventArgs) Handles Camara1MaizCheckbox.CheckedChanged,
                                                                            Camara1FrijolCheckbox.CheckedChanged,
                                                                            Camara1ArrozCheckbox.CheckedChanged,
                                                                            Camara1SorgoCheckbox.CheckedChanged,
                                                                            Camara2MaizCheckbox.CheckedChanged,
                                                                            Camara2FrijolCheckbox.CheckedChanged,
                                                                            Camara2ArrozCheckbox.CheckedChanged,
                                                                            Camara2SorgoCheckbox.CheckedChanged,
                                                                            Camara3MaizCheckbox.CheckedChanged,
                                                                            Camara3FrijolCheckbox.CheckedChanged,
                                                                            Camara3ArrozCheckbox.CheckedChanged,
                                                                            Camara3SorgoCheckbox.CheckedChanged,
                                                                            Camara4MaizCheckbox.CheckedChanged,
                                                                            Camara4FrijolCheckbox.CheckedChanged,
                                                                            Camara4ArrozCheckbox.CheckedChanged,
                                                                            Camara4SorgoCheckbox.CheckedChanged,
                                                                            Camara5MaizCheckbox.CheckedChanged,
                                                                            Camara5FrijolCheckbox.CheckedChanged,
                                                                            Camara5ArrozCheckbox.CheckedChanged,
                                                                            Camara5SorgoCheckbox.CheckedChanged,
                                                                            Camara6MaizCheckbox.CheckedChanged,
                                                                            Camara6FrijolCheckbox.CheckedChanged,
                                                                            Camara6ArrozCheckbox.CheckedChanged,
                                                                            Camara6SorgoCheckbox.CheckedChanged

        Dim totalSeleccionadas As Integer = 0
        btnGuardarLote.Visible = False

        ' Lista de todos los CheckBoxes
        Dim checkBoxes As New List(Of CheckBox) From {
        Camara1MaizCheckbox, Camara1FrijolCheckbox, Camara1ArrozCheckbox, Camara1SorgoCheckbox,
        Camara2MaizCheckbox, Camara2FrijolCheckbox, Camara2ArrozCheckbox, Camara2SorgoCheckbox,
        Camara3MaizCheckbox, Camara3FrijolCheckbox, Camara3ArrozCheckbox, Camara3SorgoCheckbox,
        Camara4MaizCheckbox, Camara4FrijolCheckbox, Camara4ArrozCheckbox, Camara4SorgoCheckbox,
        Camara5MaizCheckbox, Camara5FrijolCheckbox, Camara5ArrozCheckbox, Camara5SorgoCheckbox,
        Camara6MaizCheckbox, Camara6FrijolCheckbox, Camara6ArrozCheckbox, Camara6SorgoCheckbox
    }

        ' Contar las CheckBoxes seleccionadas
        For Each checkBox As CheckBox In checkBoxes
            If checkBox.Checked Then
                totalSeleccionadas += 1
            End If
        Next

        ' Mostrar el total en la TextBox
        txtTotalInc.Text = totalSeleccionadas.ToString()
        If totalSeleccionadas <> 0 Then
            btnGuardarLote.Visible = True
        End If

        ' Actualizar el UpdatePanel
        UpdatePanel1.Update()
    End Sub


End Class
