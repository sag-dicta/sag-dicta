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
                txtFechaDesde.Text = New DateTime(2024, 1, 1).ToString("yyyy-MM-dd")
                txtFechaHasta.Text = DateTime.Today.ToString("yyyy-MM-dd")

                llenarcomboProductor()
                VerificarTextBox()
                llenagrid()
                btnGuardarLote.Visible = False
                DivCrearNuevo.Visible = False
                btnRegresar.Visible = False
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
        If String.IsNullOrEmpty(txtRespo.Text) Then
            lblrespo.Text = "*"
            validarflag = 0
        Else
            lblrespo.Text = ""
            validarflag += 1
        End If

        ' 3
        ' Verificar al menos un CheckBox seleccionado
        If Not (Camara1MaizCheckbox.Checked Or Camara1FrijolCheckbox.Checked Or Camara1ArrozCheckbox.Checked Or Camara1SorgoCheckbox.Checked Or Camara1PapaCheckbox.Checked Or Camara1AjonjoliCheckbox.Checked Or
                Camara2MaizCheckbox.Checked Or Camara2FrijolCheckbox.Checked Or Camara2ArrozCheckbox.Checked Or Camara2SorgoCheckbox.Checked Or Camara2PapaCheckbox.Checked Or Camara2AjonjoliCheckbox.Checked Or
                Camara3MaizCheckbox.Checked Or Camara3FrijolCheckbox.Checked Or Camara3ArrozCheckbox.Checked Or Camara3SorgoCheckbox.Checked Or Camara3PapaCheckbox.Checked Or Camara3AjonjoliCheckbox.Checked Or
                Camara4MaizCheckbox.Checked Or Camara4FrijolCheckbox.Checked Or Camara4ArrozCheckbox.Checked Or Camara4SorgoCheckbox.Checked Or Camara4PapaCheckbox.Checked Or Camara4AjonjoliCheckbox.Checked Or
                Camara5MaizCheckbox.Checked Or Camara5FrijolCheckbox.Checked Or Camara5ArrozCheckbox.Checked Or Camara5SorgoCheckbox.Checked Or Camara5PapaCheckbox.Checked Or Camara5AjonjoliCheckbox.Checked Or
                Camara6MaizCheckbox.Checked Or Camara6FrijolCheckbox.Checked Or Camara6ArrozCheckbox.Checked Or Camara6SorgoCheckbox.Checked Or Camara6PapaCheckbox.Checked Or Camara6AjonjoliCheckbox.Checked) Then
            lblmensaje.Text = "Seleccione al menos una semilla."
            validarflag = 0
        Else
            lblmensaje.Text = ""
            validarflag += 1
        End If

        ' Validar si todos los campos están completos
        If validarflag >= 3 Then
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
        Dim cadena As String = "id, DATE_FORMAT(fecha_monitoreo, '%d-%m-%Y') AS fecha_monitoreo, responsable, camara1_maiz, camara1_frijol, camara1_arroz, camara1_sorgo, camara1_papa, camara1_ajonjoli, camara2_maiz, camara2_frijol, camara2_arroz, camara2_sorgo, camara2_papa, camara2_ajonjoli, camara3_maiz, camara3_frijol, camara3_arroz, camara3_sorgo, camara3_papa, camara3_ajonjoli, camara4_maiz, camara4_frijol, camara4_arroz, camara4_sorgo, camara4_papa, camara4_ajonjoli, camara5_maiz, camara5_frijol, camara5_arroz, camara5_sorgo, camara5_papa, camara5_ajonjoli, camara6_maiz, camara6_frijol, camara6_arroz, camara6_sorgo, camara6_papa, camara6_ajonjoli, total_incidencias"
        Dim c1 As String = ""

        If (TxtMultiplicador.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND responsable = '" & TxtMultiplicador.SelectedItem.Text & "' "
        End If

        BAgregar.Visible = True
        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_monitoreo_plagas_semilla` WHERE 1 = 1 AND estado = '1' " & c1 & " AND fecha_monitoreo >= '" & txtFechaDesde.Text & "' AND fecha_monitoreo <= '" & txtFechaHasta.Text & "' ORDER BY id DESC"

        GridDatos.DataBind()
    End Sub
    Protected Sub grvMergeHeader_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            HeaderCell.Text = ""
            HeaderCell.ColumnSpan = 3
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Camara 1"
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Camara 2"
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Camara 3"
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Camara 4"
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Camara 5"
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Camara 6"
            HeaderCell.ColumnSpan = 6
            HeaderGridRow.Cells.Add(HeaderCell)

            GridDatos.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If
    End Sub

    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Empezar desde la segunda celda (índice 1)
            For i As Integer = 3 To e.Row.Cells.Count - 5
                Dim cell As TableCell = e.Row.Cells(i)

                If cell.Text = "1" Then
                    cell.Text = "X"
                ElseIf cell.Text = "0" Then
                    cell.Text = ""
                End If

                If cell.Text = "X" OrElse cell.Text = "" Then
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.VerticalAlign = VerticalAlign.Middle
                End If
            Next
        End If
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
    Private Sub llenarcomboProductor()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT responsable FROM sag_monitoreo_plagas_semilla WHERE estado = '1' ORDER BY responsable ASC"

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

    Protected Sub TxtMultiplicador_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/MonitoreoPlagas.aspx"))
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        exportar()
    End Sub
    Sub ConvertirVarbinaryABooleano(data As String, checkbox As CheckBox)

        If data = "1" Then
            checkbox.Checked = True
        Else
            checkbox.Checked = False
        End If
    End Sub
    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Editar") Then

            DivGrid.Visible = False
            DivCrearNuevo.Visible = True
            btnGuardarLote.Text = "Actualizar"
            btnGuardarLote.Visible = True
            btnRegresar.Visible = True

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "fecha_monitoreo, responsable, camara1_maiz, camara1_frijol, camara1_arroz, camara1_sorgo, camara1_papa, camara1_ajonjoli, camara2_maiz, camara2_frijol, camara2_arroz, camara2_sorgo, camara2_papa, camara2_ajonjoli, camara3_maiz, camara3_frijol, camara3_arroz, camara3_sorgo, camara3_papa, camara3_ajonjoli, camara4_maiz, camara4_frijol, camara4_arroz, camara4_sorgo, camara4_papa, camara4_ajonjoli, camara5_maiz, camara5_frijol, camara5_arroz, camara5_sorgo, camara5_papa, camara5_ajonjoli, camara6_maiz, camara6_frijol, camara6_arroz, camara6_sorgo, camara6_papa, camara6_ajonjoli, total_incidencias"
            Dim Str As String = "SELECT " & cadena & " FROM sag_monitoreo_plagas_semilla WHERE  ID ='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            Textid.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            TxtFechaMonitoreo.Text = If(dt.Rows(0)("fecha_monitoreo") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_monitoreo"), DateTime).ToString("yyyy-MM-dd"))
            txtRespo.Text = If(dt.Rows(0)("responsable") Is DBNull.Value, String.Empty, dt.Rows(0)("responsable").ToString())

            '1
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara1_maiz").ToString, Camara1MaizCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara1_frijol").ToString, Camara1FrijolCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara1_arroz").ToString, Camara1ArrozCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara1_sorgo").ToString, Camara1SorgoCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara1_papa").ToString, Camara1PapaCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara1_ajonjoli").ToString, Camara1AjonjoliCheckbox)

            '2
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara2_maiz").ToString, Camara2MaizCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara2_frijol").ToString, Camara2FrijolCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara2_arroz").ToString, Camara2ArrozCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara2_sorgo").ToString, Camara2SorgoCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara2_papa").ToString, Camara2PapaCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara2_ajonjoli").ToString, Camara2AjonjoliCheckbox)

            '3
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara3_maiz").ToString, Camara3MaizCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara3_frijol").ToString, Camara3FrijolCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara3_arroz").ToString, Camara3ArrozCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara3_sorgo").ToString, Camara3SorgoCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara3_papa").ToString, Camara3PapaCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara3_ajonjoli").ToString, Camara3AjonjoliCheckbox)

            '4
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara4_maiz").ToString, Camara4MaizCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara4_frijol").ToString, Camara4FrijolCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara4_arroz").ToString, Camara4ArrozCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara4_sorgo").ToString, Camara4SorgoCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara4_papa").ToString, Camara4PapaCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara4_ajonjoli").ToString, Camara4AjonjoliCheckbox)

            '5
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara5_maiz").ToString, Camara5MaizCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara5_frijol").ToString, Camara5FrijolCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara5_arroz").ToString, Camara5ArrozCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara5_sorgo").ToString, Camara5SorgoCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara5_papa").ToString, Camara5PapaCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara5_ajonjoli").ToString, Camara5AjonjoliCheckbox)

            '6
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara6_maiz").ToString, Camara6MaizCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara6_frijol").ToString, Camara6FrijolCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara6_arroz").ToString, Camara6ArrozCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara6_sorgo").ToString, Camara6SorgoCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara6_papa").ToString, Camara6PapaCheckbox)
            ConvertirVarbinaryABooleano(dt.Rows(0)("camara6_ajonjoli").ToString, Camara6AjonjoliCheckbox)

            txtTotalInc.Text = dt.Rows(0)("total_incidencias").ToString
            VerificarTextBox()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Textid.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString


            Label3.Text = "¿Desea eliminar el registro de monitoreo de plaga en camara de semillas?"
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
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_monitoreo_plagas_semilla 
                    SET estado = @estado
                WHERE id = " & Textid.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()
                Response.Redirect(String.Format("~/pages/MonitoreoPlagas.aspx"))
            End Using

        End Using
    End Sub

    Private Sub exportar()

        Dim query As String = ""
        Dim cadena As String = "*"
        Dim c1 As String = ""

        If (TxtMultiplicador.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND TxtMultiplicador = '" & TxtMultiplicador.SelectedItem.Text & "' "
        End If

        query = "SELECT " & cadena & " FROM `sag_monitoreo_plagas_semilla` WHERE 1 = 1 AND estado = '1' " & c1 & " AND fecha_monitoreo >= '" & txtFechaDesde.Text & "' AND fecha_monitoreo <= '" & txtFechaHasta.Text & "' ORDER BY fecha_monitoreo DESC"

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "sag_monitoreo_plagas_semilla"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Monitoreo de Plagas en las Camaras " & Today & ".xlsx")
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
                                                                            Camara1PapaCheckbox.CheckedChanged,
                                                                            Camara1AjonjoliCheckbox.CheckedChanged,
                                                                            Camara2MaizCheckbox.CheckedChanged,
                                                                            Camara2FrijolCheckbox.CheckedChanged,
                                                                            Camara2ArrozCheckbox.CheckedChanged,
                                                                            Camara2SorgoCheckbox.CheckedChanged,
                                                                            Camara2PapaCheckbox.CheckedChanged,
                                                                            Camara2AjonjoliCheckbox.CheckedChanged,
                                                                            Camara3MaizCheckbox.CheckedChanged,
                                                                            Camara3FrijolCheckbox.CheckedChanged,
                                                                            Camara3ArrozCheckbox.CheckedChanged,
                                                                            Camara3SorgoCheckbox.CheckedChanged,
                                                                            Camara3PapaCheckbox.CheckedChanged,
                                                                            Camara3AjonjoliCheckbox.CheckedChanged,
                                                                            Camara4MaizCheckbox.CheckedChanged,
                                                                            Camara4FrijolCheckbox.CheckedChanged,
                                                                            Camara4ArrozCheckbox.CheckedChanged,
                                                                            Camara4SorgoCheckbox.CheckedChanged,
                                                                            Camara4PapaCheckbox.CheckedChanged,
                                                                            Camara4AjonjoliCheckbox.CheckedChanged,
                                                                            Camara5MaizCheckbox.CheckedChanged,
                                                                            Camara5FrijolCheckbox.CheckedChanged,
                                                                            Camara5ArrozCheckbox.CheckedChanged,
                                                                            Camara5SorgoCheckbox.CheckedChanged,
                                                                            Camara5PapaCheckbox.CheckedChanged,
                                                                            Camara5AjonjoliCheckbox.CheckedChanged,
                                                                            Camara6MaizCheckbox.CheckedChanged,
                                                                            Camara6FrijolCheckbox.CheckedChanged,
                                                                            Camara6ArrozCheckbox.CheckedChanged,
                                                                            Camara6SorgoCheckbox.CheckedChanged,
                                                                            Camara6PapaCheckbox.CheckedChanged,
                                                                            Camara6AjonjoliCheckbox.CheckedChanged

        Dim totalSeleccionadas As Integer = 0
        btnGuardarLote.Visible = False

        ' Lista de todos los CheckBoxes
        Dim checkBoxes As New List(Of CheckBox) From {
        Camara1MaizCheckbox, Camara1FrijolCheckbox, Camara1ArrozCheckbox, Camara1SorgoCheckbox, Camara1PapaCheckbox, Camara1AjonjoliCheckbox,
        Camara2MaizCheckbox, Camara2FrijolCheckbox, Camara2ArrozCheckbox, Camara2SorgoCheckbox, Camara2PapaCheckbox, Camara2AjonjoliCheckbox,
        Camara3MaizCheckbox, Camara3FrijolCheckbox, Camara3ArrozCheckbox, Camara3SorgoCheckbox, Camara3PapaCheckbox, Camara3AjonjoliCheckbox,
        Camara4MaizCheckbox, Camara4FrijolCheckbox, Camara4ArrozCheckbox, Camara4SorgoCheckbox, Camara4PapaCheckbox, Camara4AjonjoliCheckbox,
        Camara5MaizCheckbox, Camara5FrijolCheckbox, Camara5ArrozCheckbox, Camara5SorgoCheckbox, Camara5PapaCheckbox, Camara5AjonjoliCheckbox,
        Camara6MaizCheckbox, Camara6FrijolCheckbox, Camara6ArrozCheckbox, Camara6SorgoCheckbox, Camara6PapaCheckbox, Camara6AjonjoliCheckbox
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

    Protected Sub Camara1MaizCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara1MaizCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara1frijolCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara1FrijolCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara1arrozCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara1ArrozCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara1sorgoCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara1SorgoCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara1papaCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara1PapaCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara1ajonjoliCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara1AjonjoliCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara2maizCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara2MaizCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara2frijolCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara2FrijolCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara2arrozCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara2ArrozCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara2sorgoCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara2SorgoCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara2papaCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara2PapaCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara2ajonjoliCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara2AjonjoliCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara3frijolCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara3FrijolCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara3MaizCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara3MaizCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara3arrozCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara3ArrozCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara3SorgoCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara3SorgoCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara3papaCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara3PapaCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara3ajonjoliCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara3AjonjoliCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara4frijolCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara4FrijolCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara4MaizCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara4MaizCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara4arrozCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara4ArrozCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara4SorgoCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara4SorgoCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara4papaCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara4PapaCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara4ajonjoliCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara4AjonjoliCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara5frijolCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara5FrijolCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara5MaizCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara5MaizCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara5arrozCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara5ArrozCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara5SorgoCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara5SorgoCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara5papaCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara5PapaCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara5ajonjoliCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara5AjonjoliCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara6frijolCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara6FrijolCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara6MaizCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara6MaizCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara6arrozCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara6ArrozCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara6SorgoCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara6SorgoCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara6papaCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara6PapaCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub
    Protected Sub Camara6ajonjoliCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles Camara6AjonjoliCheckbox.CheckedChanged
        VerificarTextBox()
    End Sub

    Protected Sub txtRespo_TextChanged(sender As Object, e As EventArgs) Handles txtRespo.TextChanged
        VerificarTextBox()
    End Sub
    Protected Sub TxtFechaMonitoreo_TextChanged(sender As Object, e As EventArgs) Handles TxtFechaMonitoreo.TextChanged
        VerificarTextBox()
    End Sub

    Protected Sub txtFechaDesde_TextChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub txtFechaHasta_TextChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/MonitoreoPlagas.aspx"))
    End Sub

    Protected Sub GuardarMonitoreo()
        Dim fechaConvertida As DateTime
        If btnGuardarLote.Text = "Actualizar" Then
            LabelGuardar.Visible = False
            LabelGuardar.Text = ""
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim sql As String = "UPDATE sag_monitoreo_plagas_semilla SET
                        fecha_monitoreo = @fecha_monitoreo,
                        responsable = @responsable,
                        camara1_maiz = @camara1_maiz,
                        camara1_frijol = @camara1_frijol, 
                        camara1_arroz = @camara1_arroz,
                        camara1_sorgo = @camara1_sorgo,
                        camara1_papa = @camara1_papa,
                        camara1_ajonjoli = @camara1_ajonjoli,
                        camara2_maiz = @camara2_maiz,
                        camara2_frijol = @camara2_frijol, 
                        camara2_arroz = @camara2_arroz, 
                        camara2_sorgo = @camara2_sorgo,
                        camara2_papa = @camara2_papa,
                        camara2_ajonjoli = @camara2_ajonjoli,
                        camara3_maiz = @camara3_maiz,
                        camara3_frijol = @camara3_frijol, 
                        camara3_arroz = @camara3_arroz, 
                        camara3_sorgo = @camara3_sorgo,
                        camara3_papa = @camara3_papa,
                        camara3_ajonjoli = @camara3_ajonjoli,
                        camara4_maiz = @camara4_maiz,
                        camara4_frijol = @camara4_frijol, 
                        camara4_arroz = @camara4_arroz, 
                        camara4_sorgo = @camara4_sorgo,
                        camara4_papa = @camara4_papa,
                        camara4_ajonjoli = @camara4_ajonjoli,
                        camara5_maiz = @camara5_maiz,
                        camara5_frijol = @camara5_frijol, 
                        camara5_arroz = @camara5_arroz,
                        camara5_sorgo = @camara5_sorgo,
                        camara5_papa = @camara5_papa,
                        camara5_ajonjoli = @camara5_ajonjoli,
                        camara6_maiz = @camara6_maiz,
                        camara6_frijol = @camara6_frijol, 
                        camara6_arroz = @camara6_arroz, 
                        camara6_sorgo = @camara6_sorgo,
                        camara6_papa = @camara6_papa,
                        camara6_ajonjoli = @camara6_ajonjoli,
                        total_incidencias = @total_incidencias
                        WHERE ID = " & Textid.Text & ""


                Using cmd As New MySqlCommand(sql, connection)


                    If DateTime.TryParse(TxtFechaMonitoreo.Text, fechaConvertida) Then
                        cmd.Parameters.AddWithValue("@fecha_monitoreo", fechaConvertida.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@responsable", txtRespo.Text)

                    '1
                    If Camara1MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_maiz", "0")
                    End If
                    If Camara1FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_frijol", "0")
                    End If
                    If Camara1ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_arroz", "0")
                    End If
                    If Camara1SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_sorgo", "0")
                    End If
                    If Camara1PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_papa", "0")
                    End If
                    If Camara1AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_ajonjoli", "0")
                    End If
                    '2
                    If Camara2MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_maiz", "0")
                    End If
                    If Camara2FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_frijol", "0")
                    End If
                    If Camara2ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_arroz", "0")
                    End If
                    If Camara2SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_sorgo", "0")
                    End If
                    If Camara2PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_papa", "0")
                    End If
                    If Camara2AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_ajonjoli", "0")
                    End If
                    '3
                    If Camara3MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_maiz", "0")
                    End If
                    If Camara3FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_frijol", "0")
                    End If
                    If Camara3ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_arroz", "0")
                    End If
                    If Camara3SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_sorgo", "0")
                    End If
                    If Camara3PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_papa", "0")
                    End If
                    If Camara3AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_ajonjoli", "0")
                    End If
                    '4
                    If Camara4MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_maiz", "0")
                    End If
                    If Camara4FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_frijol", "0")
                    End If
                    If Camara4ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_arroz", "0")
                    End If
                    If Camara4SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_sorgo", "0")
                    End If
                    If Camara4PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_papa", "0")
                    End If
                    If Camara4AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_ajonjoli", "0")
                    End If
                    '5
                    If Camara5MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_maiz", "0")
                    End If
                    If Camara5FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_frijol", "0")
                    End If
                    If Camara5ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_arroz", "0")
                    End If
                    If Camara5SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_sorgo", "0")
                    End If
                    If Camara5PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_papa", "0")
                    End If
                    If Camara5AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_ajonjoli", "0")
                    End If
                    '6
                    If Camara6MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_maiz", "0")
                    End If
                    If Camara6FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_frijol", "0")
                    End If
                    If Camara6ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_arroz", "0")
                    End If
                    If Camara6SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_sorgo", "0")
                    End If
                    If Camara6PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_papa", "0")
                    End If
                    If Camara6AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_ajonjoli", "0")
                    End If

                    cmd.Parameters.AddWithValue("@total_incidencias", Convert.ToInt64(txtTotalInc.Text))

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label3.Text = "¡Se ha editado correctamente el registro de monitoreo de plaga!"
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

                Dim sql As String = "INSERT INTO sag_monitoreo_plagas_semilla (fecha_monitoreo, responsable, camara1_maiz, camara1_frijol, camara1_arroz, camara1_sorgo, camara1_papa, camara1_ajonjoli,
                        camara2_maiz, camara2_frijol, camara2_arroz, camara2_sorgo, camara2_papa, camara2_ajonjoli, camara3_maiz, camara3_frijol, camara3_arroz,
                        camara3_sorgo, camara3_papa, camara3_ajonjoli, camara4_maiz, camara4_frijol, camara4_arroz, camara4_sorgo, camara4_papa, camara4_ajonjoli, camara5_maiz, camara5_frijol,
                        camara5_arroz, camara5_sorgo, camara5_papa, camara5_ajonjoli, camara6_maiz, camara6_frijol, camara6_arroz, camara6_sorgo, camara6_papa, camara6_ajonjoli, total_incidencias, estado)
                        VALUES (@fecha_monitoreo, @responsable, @camara1_maiz, @camara1_frijol, @camara1_arroz, @camara1_sorgo, @camara1_papa, @camara1_ajonjoli,
                        @camara2_maiz, @camara2_frijol, @camara2_arroz, @camara2_sorgo, @camara2_papa, @camara2_ajonjoli, @camara3_maiz, @camara3_frijol, @camara3_arroz,
                        @camara3_sorgo, @camara3_papa, @camara3_ajonjoli, @camara4_maiz, @camara4_frijol, @camara4_arroz, @camara4_sorgo, @camara4_papa, @camara4_ajonjoli, @camara5_maiz, @camara5_frijol,
                        @camara5_arroz, @camara5_sorgo, @camara5_papa, @camara5_ajonjoli, @camara6_maiz, @camara6_frijol, @camara6_arroz, @camara6_sorgo, @camara6_papa, @camara6_ajonjoli, @total_incidencias, @estado)"
                Using cmd As New MySqlCommand(sql, connection)

                    If DateTime.TryParse(TxtFechaMonitoreo.Text, fechaConvertida) Then
                        cmd.Parameters.AddWithValue("@fecha_monitoreo", fechaConvertida.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@responsable", txtRespo.Text)
                    '1
                    If Camara1MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_maiz", "0")
                    End If
                    If Camara1FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_frijol", "0")
                    End If
                    If Camara1ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_arroz", "0")
                    End If
                    If Camara1SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_sorgo", "0")
                    End If
                    If Camara1PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_papa", "0")
                    End If
                    If Camara1AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara1_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara1_ajonjoli", "0")
                    End If
                    '2
                    If Camara2MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_maiz", "0")
                    End If
                    If Camara2FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_frijol", "0")
                    End If
                    If Camara2ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_arroz", "0")
                    End If
                    If Camara2SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_sorgo", "0")
                    End If
                    If Camara2PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_papa", "0")
                    End If
                    If Camara2AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara2_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara2_ajonjoli", "0")
                    End If
                    '3
                    If Camara3MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_maiz", "0")
                    End If
                    If Camara3FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_frijol", "0")
                    End If
                    If Camara3ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_arroz", "0")
                    End If
                    If Camara3SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_sorgo", "0")
                    End If
                    If Camara3PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_papa", "0")
                    End If
                    If Camara3AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara3_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara3_ajonjoli", "0")
                    End If
                    '4
                    If Camara4MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_maiz", "0")
                    End If
                    If Camara4FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_frijol", "0")
                    End If
                    If Camara4ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_arroz", "0")
                    End If
                    If Camara4SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_sorgo", "0")
                    End If
                    If Camara4PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_papa", "0")
                    End If
                    If Camara4AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara4_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara4_ajonjoli", "0")
                    End If
                    '5
                    If Camara5MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_maiz", "0")
                    End If
                    If Camara5FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_frijol", "0")
                    End If
                    If Camara5ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_arroz", "0")
                    End If
                    If Camara5SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_sorgo", "0")
                    End If
                    If Camara5PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_papa", "0")
                    End If
                    If Camara5AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara5_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara5_ajonjoli", "0")
                    End If
                    '6
                    If Camara6MaizCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_maiz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_maiz", "0")
                    End If
                    If Camara6FrijolCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_frijol", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_frijol", "0")
                    End If
                    If Camara6ArrozCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_arroz", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_arroz", "0")
                    End If
                    If Camara6SorgoCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_sorgo", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_sorgo", "0")
                    End If
                    If Camara6PapaCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_papa", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_papa", "0")
                    End If
                    If Camara6AjonjoliCheckbox.Checked = True Then
                        cmd.Parameters.AddWithValue("@camara6_ajonjoli", "1")
                    Else
                        cmd.Parameters.AddWithValue("@camara6_ajonjoli", "0")
                    End If

                    cmd.Parameters.AddWithValue("@total_incidencias", Convert.ToInt64(txtTotalInc.Text))
                    cmd.Parameters.AddWithValue("@estado", "1")

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label3.Text = "¡Se ha registrado correctamente el registro de monitoreo de plaga!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarLote.Visible = False
                    btnRegresar.Visible = True

                End Using
            End Using
        End If
    End Sub
End Class
