Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class AgregarVehiculo
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
                llenarDDLIdenVehi()
                llenarDDLMarcaGrid()
                'llenarcomboDeptoGrid()
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
            If btnGuardarLote.Text = "Guardar" Then
                LabelGuardar.Visible = False
                LabelGuardar.Text = ""
                Dim connectionString As String = conn
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "INSERT INTO sag_vehiculo (tipo, marca, color, no_placa, estado, CodVehi) VALUES (@tipo, @marca, @color, @no_placa, @estado, @CodVehi)"

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@tipo", DDLTipo.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@marca", TxtMarca.Text)
                        cmd.Parameters.AddWithValue("@color", TxtColor.Text)
                        cmd.Parameters.AddWithValue("@no_placa", TxtPlaca.Text)
                        cmd.Parameters.AddWithValue("@estado", "1")
                        cmd.Parameters.AddWithValue("@CodVehi", TxtIdenVehi.Text)

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        Label3.Text = "¡Se ha registrado correctamente el vehiculo!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                        Button1.Visible = False
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

                    Dim query As String = "UPDATE sag_vehiculo
                    SET tipo = @tipo, 
                        marca = @marca, 
                        color = @color, 
                        no_placa = @no_placa,
                        CodVehi= @CodVehi
                    WHERE id = " & txtID.Text & ""

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@tipo", DDLTipo.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@marca", TxtMarca.Text)
                        cmd.Parameters.AddWithValue("@color", TxtColor.Text)
                        cmd.Parameters.AddWithValue("@no_placa", TxtPlaca.Text)
                        cmd.Parameters.AddWithValue("@CodVehi", TxtIdenVehi.Text)

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        'Response.Write("<script>window.alert('¡Se ha editado correctamente la solicitud del Multiplicador o Estación!') </script>")
                        Label3.Text = "¡Se ha editado correctamente el registro del vehiculo!"
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
        DDLTipo.SelectedValue = 0
        TxtMarca.Text = ""
        TxtColor.Text = ""
        TxtPlaca.Text = ""
        Button2.Visible = False
        btnGuardarLote.Visible = True
        VerificarTextBox()
    End Sub

    Protected Sub buscar_productor(sender As Object, e As EventArgs)
        VerificarTextBox()
    End Sub

    Protected Sub VerificarTextBox()

        If String.IsNullOrEmpty(DDLTipo.SelectedItem.Text) Then
            lbTipo.Text = "*"
            validarflag = 0
        Else
            lbTipo.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtMarca.Text) Then
            LblMarca.Text = "*"
            validarflag = 0
        Else
            LblMarca.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtPlaca.Text) Then
            LblPlaca.Text = "*"
            validarflag = 0
        Else
            LblPlaca.Text = ""
            validarflag = 1
        End If

        If String.IsNullOrEmpty(TxtColor.Text) Then
            LblColor.Text = "*"
            validarflag = 0
        Else
            LblColor.Text = ""
            validarflag = 1
        End If

        CrearIdentificador()

    End Sub

    Protected Sub CrearIdentificador()
        Dim marca As String = TxtMarca.Text
        Dim tipo As String = DDLTipo.SelectedItem.Text
        Dim color As String = TxtColor.Text
        Dim placa As String = TxtPlaca.Text

        Dim resultado As String = String.Format("{0}-{1}-{2}-{3}", marca, tipo, color, placa)

        TxtIdenVehi.Text = resultado
    End Sub
    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE nombre_multiplicador = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", TxtPlaca.Text)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "sag_registro_senasa")

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

        'If Not String.IsNullOrEmpty(id2) Then
        '    txt_nombre_prod_new.Text = id2
        'Else
        '    txt_nombre_prod_new.Text = " "
        'End If
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
        Dim cadena As String = "id, tipo, marca, color, no_placa, CodVehi"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        If (DDLMarcaGrid.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND marca = '" & DDLMarcaGrid.SelectedItem.Text & "' "
        End If

        If (DDLTipoGrid.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND tipo = '" & DDLTipoGrid.SelectedItem.Text & "' "
        End If

        If (DDLIdenVehi.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND CodVehi = '" & DDLIdenVehi.SelectedItem.Text & "' "
        End If

        BAgregar.Visible = True
        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_vehiculo` WHERE 1 = 1 AND estado = '1' " & c3 & c4 & c1

        GridDatos.DataBind()
    End Sub
    'Private Sub llenarcomboDeptoGrid()
    '    Dim StrCombo As String = "SELECT * FROM sag_vehiculo"
    '    Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
    '    Dim DtCombo As New DataTable
    '    adaptcombo.Fill(DtCombo)
    '
    '    DDLTipoGrid.DataSource = DtCombo
    '    DDLTipoGrid.DataValueField = DtCombo.Columns(0).ToString()
    '    DDLTipoGrid.DataTextField = DtCombo.Columns(2).ToString
    '    DDLTipoGrid.DataBind()
    '    Dim newitem As New ListItem("Todos", "Todos")
    '    DDLTipoGrid.Items.Insert(0, newitem)
    '
    'End Sub
    'Private Function DevolverValorDepart2(cadena As String)
    '
    '    If DDLTipoGrid.SelectedItem.Text <> "Todos" Then
    '        Dim codigoDepartamento As String = ""
    '        Dim StrCombo As String = "SELECT * FROM sag_vehiculo WHERE tipo = @nombre"
    '        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
    '        adaptcombo.SelectCommand.Parameters.AddWithValue("@nombre", cadena)
    '        Dim DtCombo As New DataTable
    '        adaptcombo.Fill(DtCombo)
    '        'txtCodDep.Text = DtCombo.Rows(0)("CODIGO_DEPARTAMENTO").ToString
    '        codigoDepartamento = DtCombo.Rows(0)("tipo").ToString()
    '        Return codigoDepartamento
    '    End If
    '
    '    Return 0
    '    VerificarTextBox()
    'End Function
    Protected Sub DDLTipoGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLTipoGrid.SelectedIndexChanged
        llenarDDLMarcaGrid()
        llenarDDLIdenVehi()
        llenagrid()
    End Sub

    'Private Sub llenarmunicipioGrid()
    '    Dim departamento As String = DDLTipoGrid.SelectedItem.Text
    '    Dim newitem As New ListItem("Todos", "Todos")
    '    If DDLTipoGrid.SelectedItem.Text <> "Todos" Then
    '        Dim StrCombo As String = "SELECT DISTINCT marca FROM sag_vehiculo WHERE tipo = '" & departamento & "'"
    '        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
    '        Dim DtCombo As New DataTable
    '        adaptcombo.Fill(DtCombo)

    '        Dim todosNulos As Boolean = True

    '        For Each row As DataRow In DtCombo.Rows
    '            For Each column As DataColumn In DtCombo.Columns
    '                If Not IsDBNull(row(column)) Then
    '                    todosNulos = False
    '                    Exit For
    '                End If
    '            Next

    '            If Not todosNulos Then
    '                DDLMarcaGrid.DataSource = DtCombo
    '                DDLMarcaGrid.DataValueField = DtCombo.Columns(0).ToString()
    '                DDLMarcaGrid.DataTextField = DtCombo.Columns(0).ToString()
    '                DDLMarcaGrid.DataBind()
    '                DDLMarcaGrid.Items.Insert(0, newitem)
    '            Else
    '                DDLMarcaGrid.Items.Insert(0, newitem)
    '            End If
    '        Next
    '    Else
    '        DDLMarcaGrid.Items.Insert(0, newitem)
    '    End If
    'End Sub

    Protected Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

        DivCrearNuevo.Visible = True
        DivGrid.Visible = False

        If DDLTipoGrid.SelectedIndex = 0 Then
            DDLTipo.SelectedValue = 0

        Else
            SeleccionarItemEnDropDownList(DDLTipo, DDLTipoGrid.SelectedItem.Text)
            If DDLMarcaGrid.SelectedItem.Text <> "Todos" Then
                TxtMarca.Text = DDLMarcaGrid.SelectedItem.Text
            End If
        End If

        VerificarTextBox()
        'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

    End Sub

    Protected Sub TxtMultiplicador_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/AgregarVehiculo.aspx"))
    End Sub

    Private Sub exportar()

        Dim query As String = ""
        Dim cadena As String = "id, tipo, marca, color, no_placa"
        Dim c1 As String = ""
        Dim c4 As String = ""
        Dim c3 As String = ""

        If (DDLMarcaGrid.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND marca = '" & DDLMarcaGrid.SelectedItem.Text & "' "
        End If

        If (DDLTipoGrid.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND tipo = '" & DDLTipoGrid.SelectedItem.Text & "' "
        End If

        query = "SELECT " & cadena & " FROM `sag_vehiculo` WHERE 1 = 1 AND estado = '1' " & c3 & c4

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "sag_vehiculo"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Registro de vehiculo  " & Today & " " & DDLTipoGrid.SelectedItem.Text & ".xlsx")
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
            Button1.Visible = False
            Button2.Visible = False
            DivCrearNuevo.Visible = True
            DivGrid.Visible = False

            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Dim Str As String = "SELECT * FROM sag_vehiculo WHERE  ID='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            nuevo = False
            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            SeleccionarItemEnDropDownList(DDLTipo, dt.Rows(0)("tipo").ToString())
            TxtMarca.Text = dt.Rows(0)("marca").ToString()
            TxtPlaca.Text = dt.Rows(0)("no_placa").ToString()
            TxtColor.Text = dt.Rows(0)("color").ToString()

            CrearIdentificador()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            Label3.Text = "¿Desea eliminar el registro del vehiculo?"
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

            adap.Fill(ds, "sag_registro_senasa")

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

            Dim query As String = "UPDATE sag_vehiculo 
                    SET estado = @estado
                WHERE id = " & txtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()

                Response.Redirect(String.Format("~/pages/AgregarVehiculo.aspx"))
            End Using

        End Using

    End Sub
    Private Sub llenarDDLIdenVehi()
        Dim StrCombo As String = "SELECT DISTINCT CodVehi FROM sag_vehiculo WHERE estado = 1"

        If DDLTipoGrid.SelectedItem.Text <> "Todos" Then
            StrCombo += " AND tipo = '" & DDLTipoGrid.SelectedItem.Text & "' "
        End If

        If DDLMarcaGrid.SelectedItem.Text <> "Todos" Then
            StrCombo += " And marca = '" & DDLMarcaGrid.SelectedItem.Text & "' "
        End If

        StrCombo += " ORDER BY marca ASC;"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        DDLIdenVehi.DataSource = DtCombo
        DDLIdenVehi.DataValueField = DtCombo.Columns(0).ToString()
        DDLIdenVehi.DataTextField = DtCombo.Columns(0).ToString
        DDLIdenVehi.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        DDLIdenVehi.Items.Insert(0, newitem)
    End Sub
    Protected Sub DDLIdenVehi_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLIdenVehi.SelectedIndexChanged
        llenagrid()
    End Sub
    Private Sub llenarDDLMarcaGrid()
        Dim StrCombo As String
        If DDLTipoGrid.SelectedItem.Text = "Todos" Then
            StrCombo = "SELECT DISTINCT marca FROM sag_vehiculo WHERE estado = 1 ORDER BY marca ASC;"
        Else
            StrCombo = "SELECT DISTINCT marca FROM sag_vehiculo WHERE estado = 1 AND tipo = '" & DDLTipoGrid.SelectedItem.Text & "' ORDER BY marca ASC;"
        End If

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        DDLMarcaGrid.DataSource = DtCombo
        DDLMarcaGrid.DataValueField = DtCombo.Columns(0).ToString()
        DDLMarcaGrid.DataTextField = DtCombo.Columns(0).ToString
        DDLMarcaGrid.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        DDLMarcaGrid.Items.Insert(0, newitem)
    End Sub
    Protected Sub DDLMarcaGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLMarcaGrid.SelectedIndexChanged
        llenarDDLIdenVehi()
        llenagrid()
    End Sub
End Class
