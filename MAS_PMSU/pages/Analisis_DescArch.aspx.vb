Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient

Public Class Analisis_DescArch
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connSAG").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If User.Identity.IsAuthenticated = True Then
            If IsPostBack Then

            Else
                ''llenarcomboDepto()
                llenarcomboProductor()
                'Dim newitem As New ListItem(" ", " ")
                'TxtProductor.Items.Insert(0, newitem)
                llenagrid()
                'TxtProductor.Enabled = False
            End If
        End If


    End Sub

    Private Sub llenarcomboProductor()
        'If TxtDepto.SelectedItem.Text <> " " Then
        Dim StrCombo As String = "SELECT DISTINCT nombre_multiplicador FROM vista_acta_lote_multi WHERE Estado_sena = 1 AND ciclo_acta IS NOT NULL ORDER BY nombre_multiplicador ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        'adaptcombo.SelectCommand.Parameters.AddWithValue("@nombre", TxtDepto.SelectedItem.Text)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        TxtProductor.DataSource = DtCombo
        TxtProductor.DataValueField = "nombre_multiplicador"
        TxtProductor.DataTextField = "nombre_multiplicador"
        TxtProductor.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtProductor.Items.Insert(0, newitem)
        'End If
        'If TxtDepto.SelectedItem.Text = " " Then
        '    TxtProductor.SelectedValue = " "
        'End If
    End Sub


    Sub llenagrid()
        BAgregar.Visible = False
        Dim cadena As String = "*"
        Dim c1 As String = ""
        Dim c2 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""
        Dim c5 As String = ""
        Dim c6 As String = ""
        Dim c7 As String = ""
        Dim c8 As String = ""

        If (TxtProductor.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND nombre_multiplicador = '" & TxtProductor.SelectedItem.Text & "' "
        End If

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM vista_acta_lote_multi WHERE Estado_sena = '1' AND ciclo_acta IS NOT NULL " & c1 & c4 & " ORDER BY id_acta DESC"
        GridDatos.DataBind()
    End Sub

    Protected Sub TxtProductor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtProductor.SelectedIndexChanged
        llenagrid()

    End Sub

    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand
        'Dim fecha2 As Date

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        If (e.CommandName = "FichaLote") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Dim Str As String = "SELECT GERMINACION_FIRMADA FROM `vista_acta_lote_multi` WHERE  ID_2_GERMI='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            Dim todosNulos As Boolean = True

            For Each row As DataRow In dt.Rows
                For Each column As DataColumn In dt.Columns
                    If Not IsDBNull(row(column)) Then
                        todosNulos = False
                        Exit For
                    End If
                Next

                If Not todosNulos Then
                    Using connection As New MySqlConnection(conn)
                        connection.Open()
                        Using command As New MySqlCommand(Str, connection)
                            Using reader As MySqlDataReader = command.ExecuteReader()
                                If reader.Read() Then
                                    Dim pdfData As Byte() = DirectCast(reader("GERMINACION_FIRMADA"), Byte())
                                    Dim nombre As String = HttpUtility.HtmlDecode(gvrow.Cells(2).Text).ToString
                                    Dim lote As String = HttpUtility.HtmlDecode(gvrow.Cells(3).Text).ToString
                                    ' Configura la respuesta HTTP para descargar la imagen
                                    HttpContext.Current.Response.Clear()
                                    HttpContext.Current.Response.ContentType = "application/pdf" ' Cambia el tipo de contenido a PDF
                                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Análisis_de_germinación_" & nombre & "_" & lote & "_Firmado.pdf") ' Cambia el nombre del archivo con la extensión PDF
                                    HttpContext.Current.Response.BinaryWrite(pdfData) ' Reemplaza pdfData con tus datos binarios del PDF
                                    HttpContext.Current.Response.Flush()
                                    HttpContext.Current.Response.End()

                                End If
                            End Using
                        End Using
                    End Using

                    TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
                Else
                    BBorrarsi.Visible = False
                    BConfirm.Visible = True
                    BBorrarno.Visible = False
                    Label1.Text = "¡No hay archivo para descarga!"
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
                End If
            Next
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
                    ' Se crea un objeto ListItem para representar la  gina...
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
                ' Calcula el nº de  gina actual...
                Dim currentPage As Integer = GridDatos.PageIndex + 1
                ' Actualiza el Label control con la  gina actual.
                pageLabel.Text = "Página " & currentPage.ToString() & " de " & GridDatos.PageCount.ToString()
            End If
        End If

    End Sub
    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        ' Verifica si es una fila de datos y no el encabezado o el pie de página
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtén los valores de los campos IMAGEN_FICHA, IMAGEN_PAGO_TGR, e IMAGEN_ETIQUETA_SEMILLA
            Dim estimadoProduccion As String = DataBinder.Eval(e.Row.DataItem, "lote_registrado").ToString()
            Dim imagenFicha As String = DataBinder.Eval(e.Row.DataItem, "GERMINACION_FIRMADA").ToString()
            Dim btnEditar As Button = DirectCast(e.Row.Cells(4).Controls(0), Button)

            If Not String.IsNullOrEmpty(estimadoProduccion) Then
                btnEditar.Visible = True
            End If
            ' Encuentra los botones en la fila por índice
            Dim indexFicha As Integer = 4

            ' Encuentra los botones en la fila
            Dim btnFicha As Button = CType(e.Row.Cells(indexFicha).Controls(0), Button)

            ' Oculta los botones según las condiciones que necesites
            btnFicha.Visible = Not String.IsNullOrEmpty(imagenFicha)
        End If
    End Sub


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

    Private Sub exportar()

        Dim query As String = ""

        query = "SELECT * FROM `bcs_inscripcion_senasa` where Estado = '1' ORDER BY Productor"
        'If (TxtDepto.SelectedValue = " Todos") Then
        '    query = "SELECT * FROM `bcs_inscripcion_senasa` WHERE Estado = '1' ORDER BY Departamento,Productor "
        'Else
        '    query = "SELECT * FROM `bcs_inscripcion_senasa` WHERE Departamento='" & TxtDepto.SelectedValue & "' AND Estado = '1' ORDER BY Departamento,Productor "
        'End If

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "bcs_inscripcion_senasa"

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
                            Response.AddHeader("content-disposition", "attachment;filename=PLAN PRODUCCIÓN DE SEMILLA DE FRIJOL  " & Today & ".xlsx")
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

    Private Sub llenarVAIDAD_CICLO()

    End Sub


    Protected Sub BBorrarsi_Click(sender As Object, e As EventArgs) Handles BBorrarsi.Click
        Dim conex As New MySqlConnection(conn)

        conex.Open()

        Dim Sql As String
        Dim cmd As New MySqlCommand()

        Sql = "UPDATE bcs_inscripcion_senasa SET Estado=0  WHERE ID=" & TxtID.Text & " "

        cmd.Connection = conex
        cmd.CommandText = Sql
        cmd.ExecuteNonQuery()

        llenagrid()

        Label1.Text = "El registro ha sido eliminado"

        BConfirm.Visible = True

        BBorrarsi.Visible = False
        BBorrarno.Visible = False

        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

    End Sub

    Protected Sub limpiarFiltros(sender As Object, e As EventArgs)
        Response.Redirect("Analisis_DescArch.aspx")
    End Sub

    Private Function FileUploadToBytes(fileUpload As FileUpload) As Byte()
        Using stream As System.IO.Stream = fileUpload.PostedFile.InputStream
            Dim length As Integer = fileUpload.PostedFile.ContentLength
            Dim bytes As Byte() = New Byte(length - 1) {}
            stream.Read(bytes, 0, length)
            Return bytes
        End Using
    End Function

    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        Dim productor As String = TxtProductor.SelectedItem.Text
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM vista_inscripcion_senasa_lote WHERE Productor = '" & productor & "' AND CICLO = '" & ciclo & "'"
        Dim adap As New MySqlDataAdapter(Str, conn)
        Dim dt As New DataTable

        ' Nombre de la vista del dataset
        adap.Fill(ds, "vista_inscripcion_senasa_lote")

        Dim nombre As String
        nombre = "Solicitud Inscripcion de Lote o Campo _" + Today
        rptdocument.Load(Server.MapPath("~/pages/CrystalReport3.rpt"))
        rptdocument.SetDataSource(ds)

        ' Usar un HashSet para almacenar municipios únicos
        Dim municipiosUnicos As New HashSet(Of String)()

        For Each row As DataRow In ds.Tables("vista_inscripcion_senasa_lote").Rows
            If Not IsDBNull(row("Municipio")) Then
                municipiosUnicos.Add(row("Municipio").ToString())
            End If
        Next

        ' Convertir el HashSet en una cadena separada por comas
        Dim MunicipiosConcatenados As String = String.Join(", ", municipiosUnicos)

        rptdocument.SetParameterValue("CadenaMunicipios", MunicipiosConcatenados)

        Response.Buffer = False
        Response.ClearContent()
        Response.ClearHeaders()

        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/AnalisisGerminacion.aspx"))
    End Sub
End Class