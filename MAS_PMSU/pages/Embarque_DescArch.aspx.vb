Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class Embarque_DescArch
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
                llenarcomboConocimiento()
                llenagrid()
            End If
        End If
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
        Dim cadena As String = "Id, estado, no_conocimiento, para_general, DATE_FORMAT(fecha_elaboracion, '%d-%m-%Y') AS fecha_elaboracion, cultivo_general, remitente, destinatario, lugar_remitente, lugar_destinatario, conductor, vehiculo, observacion2, tipo_salida, no_convenio, salida_firmada"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        If (TxtMultiplicador.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND para_general = '" & TxtMultiplicador.SelectedItem.Text & "' "
        End If

        If (DDLConoc.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND no_conocimiento = '" & DDLConoc.SelectedItem.Text & "' "
        End If

        If (DDLTipoSalida.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND tipo_salida = '" & DDLTipoSalida.SelectedItem.Text & "' "
        End If

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_embarque_info` WHERE 1 = 1 AND estado = '1' " & c1 & c3 & c4 & " AND fecha_elaboracion >= '" & txtFechaDesde.Text & "' AND fecha_elaboracion <= '" & txtFechaHasta.Text & "'" & " ORDER BY id DESC"

        GridDatos.DataBind()
    End Sub

    Protected Sub txtFechaDesde_TextChanged(sender As Object, e As EventArgs)
        llenarcomboProductor()
        llenarcomboConocimiento()
        llenagrid()
    End Sub

    Protected Sub txtFechaHasta_TextChanged(sender As Object, e As EventArgs)
        llenarcomboProductor()
        llenarcomboConocimiento()
        llenagrid()
    End Sub

    Private Sub llenarcomboProductor()
        Dim StrCombo As String

        If DDLTipoSalida.SelectedItem.Text = "Convenio" Then
            StrCombo = "SELECT para_general FROM sag_embarque_info WHERE estado = '1' AND no_convenio IS NOT NULL AND no_convenio <> '' ORDER BY para_general ASC"
        ElseIf DDLTipoSalida.SelectedItem.Text = "Todos" Then
            StrCombo = "SELECT para_general FROM sag_embarque_info WHERE estado = '1' ORDER BY para_general ASC"
        Else
            StrCombo = "SELECT para_general FROM sag_embarque_info WHERE estado = '1' AND no_conocimiento IS NOT NULL AND no_conocimiento <> '' ORDER BY para_general ASC"
        End If


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
        If TxtMultiplicador.SelectedItem.Text = "Todos" Then
            llenarcomboConocimiento()
        Else
            llenarcomboConocimiento2()
        End If

        llenagrid()
    End Sub
    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        If (e.CommandName = "FichaLote") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Dim Str As String = "SELECT salida_firmada FROM `sag_embarque_info` WHERE id='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
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
                                    Dim pdfData As Byte() = DirectCast(reader("salida_firmada"), Byte())
                                    Dim tiposalida As String = HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString
                                    Dim var As String = HttpUtility.HtmlDecode(gvrow.Cells(2).Text).ToString
                                    Dim var2 As String = HttpUtility.HtmlDecode(gvrow.Cells(3).Text).ToString
                                    ' Configura la respuesta HTTP para descargar la imagen
                                    HttpContext.Current.Response.Clear()
                                    HttpContext.Current.Response.ContentType = "application/pdf" ' Cambia el tipo de contenido a PDF

                                    Dim nombre As String
                                    If tiposalida = "Convenio" Then
                                        nombre = "CONVENIO DE COINVERSION PARA LA PRODUCCION DE SEMILLA MEJORADA - " + var + " - " + Today
                                    Else
                                        nombre = "CONOCIMIENTO DE EMBARQUE No " + var2 + " " + Today
                                    End If

                                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" & nombre & "_Firmado.pdf") ' Cambia el nombre del archivo con la extensión PDF
                                    HttpContext.Current.Response.BinaryWrite(pdfData) ' Reemplaza pdfData con tus datos binarios del PDF
                                    HttpContext.Current.Response.Flush()
                                    HttpContext.Current.Response.End()

                                End If
                            End Using
                        End Using
                    End Using

                    'TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
                Else
                    BBorrarsi.Visible = False
                    BConfirm.Visible = True
                    BBorrarno.Visible = False
                    'Label1.Text = "¡No hay archivo para descarga!"
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
                End If
            Next
        End If

    End Sub

    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        ' Verifica si es una fila de datos y no el encabezado o el pie de página
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtén los valores de los campos IMAGEN_FICHA, IMAGEN_PAGO_TGR, e IMAGEN_ETIQUETA_SEMILLA
            Dim hayimagen As String = DataBinder.Eval(e.Row.DataItem, "salida_firmada").ToString()
            Dim btnEditar As Button = DirectCast(e.Row.Cells(10).Controls(0), Button)

            If Not String.IsNullOrEmpty(hayimagen) Then
                btnEditar.Visible = True
            Else
                btnEditar.Visible = False
            End If
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
    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/Embarque.aspx"))
    End Sub

    Protected Sub DDLConoc_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub
    Private Sub llenarcomboConocimiento()
        Dim StrCombo As String

        If DDLTipoSalida.SelectedItem.Text = "Convenio" Then
            StrCombo = "SELECT no_convenio FROM sag_embarque_info WHERE estado = '1' AND no_convenio IS NOT NULL AND no_convenio <> '' ORDER BY no_convenio ASC"
        ElseIf DDLTipoSalida.SelectedItem.Text = "Todos" Then
            StrCombo = "SELECT no_convenio FROM sag_embarque_info WHERE estado = '1' AND no_convenio IS NOT NULL AND no_convenio <> '' " &
                       "UNION " &
                       "SELECT no_conocimiento FROM sag_embarque_info WHERE estado = '1' AND no_conocimiento IS NOT NULL AND no_conocimiento <> '' " &
                       "ORDER BY 1 ASC"
        Else
            StrCombo = "SELECT no_conocimiento FROM sag_embarque_info WHERE estado = '1' AND no_conocimiento IS NOT NULL AND no_conocimiento <> '' ORDER BY no_conocimiento ASC"
        End If

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        DDLConoc.DataSource = DtCombo
        DDLConoc.DataValueField = DtCombo.Columns(0).ToString()
        DDLConoc.DataTextField = DtCombo.Columns(0).ToString()
        DDLConoc.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        DDLConoc.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboConocimiento2()
        Dim StrCombo As String

        If DDLTipoSalida.SelectedItem.Text = "Convenio" Then
            StrCombo = "SELECT no_convenio FROM sag_embarque_info WHERE estado = '1' AND no_convenio IS NOT NULL AND no_convenio <> '' AND para_general= '" & TxtMultiplicador.SelectedItem.Text & "' ORDER BY no_convenio ASC"
        ElseIf DDLTipoSalida.SelectedItem.Text = "Todos" Then
            StrCombo = "SELECT no_convenio FROM sag_embarque_info WHERE estado = '1' AND no_convenio IS NOT NULL AND no_convenio <> '' " &
                   "UNION " &
                   "SELECT no_conocimiento FROM sag_embarque_info WHERE estado = '1' AND no_conocimiento IS NOT NULL AND no_conocimiento <> '' " &
                   "ORDER BY 1 ASC"
        Else
            StrCombo = "SELECT no_conocimiento FROM sag_embarque_info WHERE estado = '1' AND no_conocimiento IS NOT NULL AND no_conocimiento <> '' AND para_general= '" & TxtMultiplicador.SelectedItem.Text & "' ORDER BY no_conocimiento ASC"
        End If

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        DDLConoc.DataSource = DtCombo
        DDLConoc.DataValueField = DtCombo.Columns(0).ToString()
        DDLConoc.DataTextField = DtCombo.Columns(0).ToString()
        DDLConoc.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        DDLConoc.Items.Insert(0, newitem)
    End Sub

    Protected Sub cambiarEstadoProducto(conocimiento As String)
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_embarque SET
                estado = @estado
                WHERE no_conocimiento = '" & conocimiento & "'"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@estado", "1")
                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
    End Sub
    Protected Sub ImprimirConvenio(ByVal var As String)
        Dim nombre As String
        Dim años As String = ObtenerAños()
        Dim ciclo As String = ObtenerCiclo()

        Dim rptdocument As New ReportDocument
        Dim ds As New DataSetMultiplicador
        Dim Str As String
        Str = "SELECT * FROM sag_embarque_info WHERE no_convenio = '" & var & "'"
        Dim adap As New MySqlDataAdapter(Str, conn)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "vista_inventario_informe")

        nombre = "CONVENIO DE COINVERSION PARA LA PRODUCCION DE SEMILLA MEJORADA DE FRIJOL  - " + var + " - " + Today

        rptdocument.Load(Server.MapPath("~/pages/Convenio.rpt"))

        rptdocument.SetDataSource(ds)

        rptdocument.SetParameterValue("NombreDirector", "AquiVaElNombreDelDirector")
        rptdocument.SetParameterValue("IdentidadDirector", "AquiVaLaIdentidadDelDirector")
        rptdocument.SetParameterValue("NoConvenio", var)
        rptdocument.SetParameterValue("Años", años)
        rptdocument.SetParameterValue("Ciclo", ciclo)

        Response.Buffer = False

        Response.ClearContent()
        Response.ClearHeaders()

        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub

    Protected Sub DDLTipoSalida_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenarcomboProductor()
        llenarcomboConocimiento()
        llenagrid()
    End Sub

    Private Function ObtenerAños() As String
        ' Obtener el año actual
        Dim añoActual As Integer = DateTime.Now.Year

        ' Obtener el año siguiente
        Dim añoSiguiente As Integer = añoActual + 1

        ' Almacenar ambos años en una variable de tipo String
        Dim años As String = añoActual.ToString() & "-" & añoSiguiente.ToString()

        Return años
    End Function

    Private Function ObtenerCiclo() As String

        Dim mesActual As Integer = DateTime.Now.Month
        Dim añoActual As Integer = DateTime.Now.Year

        Dim ciclo As String = ""
        Select Case mesActual
            Case 3 To 6
                ciclo = "Ciclo A-" & añoActual.ToString()
            Case 7 To 11
                ciclo = "Ciclo B-" & añoActual.ToString()
            Case Else
                ciclo = "Ciclo C-" & añoActual.ToString()
        End Select

        Return ciclo
    End Function
End Class
