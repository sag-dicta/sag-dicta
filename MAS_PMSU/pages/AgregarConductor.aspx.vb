Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class AgregarConductor
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
                llenarDDLTipoGrid()
                llenarNombre()
                VerificarTextBox()
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

                    Dim query As String = "INSERT INTO sag_registro_vehiculo_motorista (nombre, DNI, telefono, tipo, marca, color, no_placa, estado, CodVehi) VALUES (@nombre, @DNI, @telefono, @tipo, @marca, @color, @no_placa, @estado, @CodVehi)"

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@nombre", TxtNombCond.Text)
                        cmd.Parameters.AddWithValue("@DNI", TxtDNICond.Text)
                        cmd.Parameters.AddWithValue("@telefono", TxtTelfCond.Text)
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text)
                        cmd.Parameters.AddWithValue("@marca", TxtMarca.Text)
                        cmd.Parameters.AddWithValue("@color", TxtColor.Text)
                        cmd.Parameters.AddWithValue("@no_placa", TxtPlaca.Text)
                        cmd.Parameters.AddWithValue("@estado", "1")
                        cmd.Parameters.AddWithValue("@CodVehi", DDLNombre.SelectedItem.Text)

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        Label3.Text = "¡Se ha registrado correctamente la asignación de un motorista a un vehiculo!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                        Button1.Visible = False
                        Button2.Visible = True
                        btnGuardarLote.Visible = False

                        vehiculo_Ocupado(DDLNombre.SelectedItem.Text)
                    End Using
                End Using
            Else
                LabelGuardar.Visible = False
                LabelGuardar.Text = ""
                Dim connectionString As String = conn
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "UPDATE sag_registro_vehiculo_motorista
                    SET nombre = @nombre, 
                        DNI = @DNI, 
                        telefono = @telefono, 
                        tipo = @tipo, 
                        marca = @marca, 
                        color = @color, 
                        no_placa = @no_placa, 
                        CodVehi = @CodVehi
                    WHERE id = " & txtID.Text & ""

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@nombre", TxtNombCond.Text)
                        cmd.Parameters.AddWithValue("@DNI", TxtDNICond.Text)
                        cmd.Parameters.AddWithValue("@telefono", TxtTelfCond.Text)
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text)
                        cmd.Parameters.AddWithValue("@marca", TxtMarca.Text)
                        cmd.Parameters.AddWithValue("@color", TxtColor.Text)
                        cmd.Parameters.AddWithValue("@no_placa", TxtPlaca.Text)
                        cmd.Parameters.AddWithValue("@CodVehi", DDLNombre.SelectedItem.Text)

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        'Response.Write("<script>window.alert('¡Se ha editado correctamente la solicitud del Multiplicador o Estación!') </script>")
                        Label3.Text = "¡Se ha editado correctamente los datos del motorista!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
                        btnGuardarLote.Visible = False

                        If DDLNombre.SelectedItem.Text <> TxtNombre.Text Then
                            vehiculo_Ocupado(DDLNombre.SelectedItem.Text)
                            editar_vehiculo_Ocupado(TxtNombre.Text)
                        End If
                    End Using

                End Using
            End If
        End If

    End Sub

    Protected Sub vaciar(sender As Object, e As EventArgs)
        TxtNombCond.Text = ""
        TxtDNICond.Text = ""
        TxtTelfCond.Text = ""
        SeleccionarItemEnDropDownList(DDLNombre, " ")
        txtTipo.Text = ""
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
        If String.IsNullOrEmpty(TxtNombCond.Text) Then
            LblNombCond.Text = "*"
            validarflag = 0
        Else
            LblNombCond.Text = ""
            validarflag = +1
        End If

        If String.IsNullOrEmpty(TxtDNICond.Text) Then
            LblDNICond.Text = "*"
            validarflag = 0
        Else
            LblDNICond.Text = ""
            validarflag += 1
        End If

        If String.IsNullOrEmpty(TxtTelfCond.Text) Then
            LblTelfCond.Text = "*"
            validarflag = 0
        Else
            LblTelfCond.Text = ""
            validarflag += 1
        End If

        If String.IsNullOrEmpty(txtTipo.Text) Then
            lbTipo.Text = "*"
            validarflag = 0
        Else
            lbTipo.Text = ""
            validarflag += 1
        End If

        If String.IsNullOrEmpty(TxtMarca.Text) Then
            LblMarca.Text = "*"
            validarflag = 0
        Else
            LblMarca.Text = ""
            validarflag += 1
        End If

        If String.IsNullOrEmpty(TxtPlaca.Text) Then
            LblPlaca.Text = "*"
            validarflag = 0
        Else
            LblPlaca.Text = ""
            validarflag += 1
        End If

        If String.IsNullOrEmpty(TxtColor.Text) Then
            LblColor.Text = "*"
            validarflag = 0
        Else
            LblColor.Text = ""
            validarflag += 1
        End If

        If validarflag = 7 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub

    Private Sub llenarDDLNombre()
        Dim StrCombo As String = "SELECT DISTINCT CodVehi FROM sag_vehiculo WHERE estado = 1 ORDER BY marca ASC;"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        DDLNombre.DataSource = DtCombo
        DDLNombre.DataValueField = DtCombo.Columns(0).ToString()
        DDLNombre.DataTextField = DtCombo.Columns(0).ToString
        DDLNombre.DataBind()
        Dim newitem As New ListItem(" ", " ")
        DDLNombre.Items.Insert(0, newitem)
    End Sub

    Protected Sub separarIdentificador()
        Dim cadena As String = DDLNombre.SelectedItem.Text

        Dim partes() As String = cadena.Split("-"c)

        If partes.Length >= 4 Then
            Dim marca As String = partes(0)
            Dim tipo As String = partes(1)
            Dim color As String = partes(2)
            Dim placa As String = partes(3)

            TxtMarca.Text = marca
            txtTipo.Text = tipo
            TxtColor.Text = color
            TxtPlaca.Text = placa
        End If

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
        Dim cadena As String = "id, nombre, DNI, telefono, tipo, marca, color, no_placa, estado, CodVehi"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        If (DDLNombreGrid.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND nombre = '" & DDLNombreGrid.SelectedItem.Text & "' "
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
        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_registro_vehiculo_motorista` WHERE 1 = 1 AND estado = '1' " & c3 & c4 & c1

        GridDatos.DataBind()
    End Sub
    Private Sub llenarNombre()
        Dim StrCombo As String = "SELECT nombre FROM sag_registro_vehiculo_motorista WHERE estado = '1' ORDER BY marca ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        DDLNombreGrid.DataSource = DtCombo
        DDLNombreGrid.DataValueField = DtCombo.Columns(0).ToString()
        DDLNombreGrid.DataTextField = DtCombo.Columns(0).ToString
        DDLNombreGrid.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        DDLNombreGrid.Items.Insert(0, newitem)

    End Sub

    Protected Sub DDLNombreGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLNombreGrid.SelectedIndexChanged
        llenarDDLTipoGrid()
        llenarDDLIdenVehi()
        llenagrid()
    End Sub

    Protected Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

        DivCrearNuevo.Visible = True
        DivGrid.Visible = False
        llenarDDLNombre()
        'If DDLTipoGrid.SelectedIndex = 0 Then
        '    DDLTipo.SelectedValue = 0
        '
        'Else
        '    SeleccionarItemEnDropDownList(DDLTipo, DDLTipoGrid.SelectedItem.Text)
        '    If DDLMarcaGrid.SelectedItem.Text <> "Todos" Then
        '        TxtMarca.Text = DDLMarcaGrid.SelectedItem.Text
        '    End If
        'End If

        VerificarTextBox()

    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/AgregarConductor.aspx"))
    End Sub

    Private Sub exportar()

        Dim query As String = ""
        Dim cadena As String = "id, nombre, DNI, telefono, tipo, marca, color, no_placa, estado, CodVehi"
        Dim c1 As String = ""
        Dim c4 As String = ""
        Dim c3 As String = ""

        If (DDLNombreGrid.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND nombre = '" & DDLNombreGrid.SelectedItem.Text & "' "
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

        query = "SELECT " & cadena & " FROM `sag_registro_vehiculo_motorista` WHERE 1 = 1 AND estado = '1' " & c3 & c4 & c1

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "sag_registro_vehiculo_motorista"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Registro de motorista  " & Today & " " & DDLNombreGrid.SelectedItem.Text & ".xlsx")
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
            llenarDDLNombre()
            btnGuardarLote.Text = "Editar"
            Button1.Visible = False
            Button2.Visible = False
            DivCrearNuevo.Visible = True
            DivGrid.Visible = False
            TxtNombre.Visible = True


            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Dim Str As String = "SELECT * FROM sag_registro_vehiculo_motorista WHERE ID='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            nuevo = False

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            TxtNombre.Text = dt.Rows(0)("CodVehi").ToString()
            Dim cantidadElementos As Integer = DDLNombre.Items.Count
            Dim newitem2 As New ListItem(TxtNombre.Text, TxtNombre.Text)
            DDLNombre.Items.Insert(cantidadElementos, newitem2)

            TxtNombCond.Text = dt.Rows(0)("nombre").ToString()
            TxtDNICond.Text = dt.Rows(0)("DNI").ToString()
            TxtTelfCond.Text = dt.Rows(0)("telefono").ToString()
            SeleccionarItemEnDropDownList(DDLNombre, dt.Rows(0)("CodVehi").ToString())
            txtTipo.Text = dt.Rows(0)("tipo").ToString()
            TxtMarca.Text = dt.Rows(0)("marca").ToString()
            TxtPlaca.Text = dt.Rows(0)("no_placa").ToString()
            TxtColor.Text = dt.Rows(0)("color").ToString()
            VerificarTextBox()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            Txtidvehiculoelimi.Text = HttpUtility.HtmlDecode(gvrow.Cells(8).Text).ToString

            Label3.Text = "¿Desea eliminar el registro de motorista?"
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
            Dim Str As String = "SELECT * FROM sag_registro_vehiculo_motorista WHERE nombre_multiplicador = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "sag_registro_vehiculo_motorista")

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

    Protected Sub elminar(sender As Object, e As EventArgs) Handles BBorrarsi.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_registro_vehiculo_motorista 
                    SET estado = @estado
                WHERE id = " & txtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()

                editar_vehiculo_Ocupado(Txtidvehiculoelimi.Text)
                Response.Redirect(String.Format("~/pages/AgregarConductor.aspx"))
            End Using

        End Using

    End Sub
    Private Sub llenarDDLIdenVehi()
        Dim StrCombo As String = "SELECT DISTINCT CodVehi FROM sag_registro_vehiculo_motorista WHERE estado = 1"

        If DDLTipoGrid.SelectedItem.Text <> "Todos" Then
            StrCombo += " AND tipo = '" & DDLTipoGrid.SelectedItem.Text & "' "
        End If

        If DDLNombreGrid.SelectedItem.Text <> "Todos" Then
            StrCombo += " And nombre = '" & DDLNombreGrid.SelectedItem.Text & "' "
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
    Private Sub llenarDDLTipoGrid()
        Dim StrCombo As String
        If DDLNombreGrid.SelectedItem.Text = "Todos" Then
            StrCombo = "SELECT DISTINCT tipo FROM sag_registro_vehiculo_motorista WHERE estado = 1 ORDER BY marca ASC;"
        Else
            StrCombo = "SELECT DISTINCT tipo FROM sag_registro_vehiculo_motorista WHERE estado = 1 AND nombre = '" & DDLNombreGrid.SelectedItem.Text & "' ORDER BY marca ASC;"
        End If

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        DDLTipoGrid.DataSource = DtCombo
        DDLTipoGrid.DataValueField = DtCombo.Columns(0).ToString()
        DDLTipoGrid.DataTextField = DtCombo.Columns(0).ToString
        DDLTipoGrid.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        DDLTipoGrid.Items.Insert(0, newitem)
    End Sub

    Protected Sub DDLNombre_SelectedIndexChanged(sender As Object, e As EventArgs)
        If DDLNombre.SelectedItem.Text = " " Then
            TxtMarca.Text = ""
            txtTipo.Text = ""
            TxtColor.Text = ""
            TxtPlaca.Text = ""
            VerificarTextBox()
        Else
            separarIdentificador()
            VerificarTextBox()
        End If
    End Sub

    Protected Sub DDLTipoGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLTipoGrid.SelectedIndexChanged
        llenarDDLIdenVehi()
        llenagrid()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/AgregarVehiculo.aspx"))
    End Sub

    Protected Sub vehiculo_Ocupado(valor1 As String)
        Dim conex As New MySqlConnection(conn)

        conex.Open()
        Dim cmd2 As New MySqlCommand()
        Dim Sql As String
        Sql = "UPDATE sag_vehiculo
        SET estado = 0
        WHERE CodVehi = '" & valor1 & "'"

        cmd2.Connection = conex
        cmd2.CommandText = Sql

        cmd2.ExecuteNonQuery()
        conex.Close()
    End Sub

    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/AgregarConductor.aspx"))
    End Sub

    Protected Sub editar_vehiculo_Ocupado(valor1 As String)
        Dim conex As New MySqlConnection(conn)

        conex.Open()
        Dim cmd2 As New MySqlCommand()
        Dim Sql As String
        Sql = "UPDATE sag_vehiculo
        SET estado = 1
        WHERE CodVehi = '" & valor1 & "'"

        cmd2.Connection = conex
        cmd2.CommandText = Sql

        cmd2.ExecuteNonQuery()
        conex.Close()
    End Sub
End Class