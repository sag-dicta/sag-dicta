Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel

Public Class Embarque
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
                eliminarMiniGrid2()
                btnGuardarLote.Visible = True
        End If
        End If
    End Sub

    Protected Sub guardarSoli_lote(sender As Object, e As EventArgs)
        VerificarTextBox()
        Dim fechaConvertida As Date
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

                    Dim query As String = "INSERT INTO sag_embarque_info (
                    estado,
                    no_conocimiento,
                    para_general,
                    fecha_elaboracion,
                    cultivo_general,
                    remitente,
                    destinatario,
                    lugar_remitente,
                    lugar_destinatario,
                    conductor,
                    vehiculo,
                    observacion2,
                    duplicado,
                    triplicado
                    ) VALUES (@estado,
                    @no_conocimiento,
                    @para_general,
                    @fecha_elaboracion,
                    @cultivo_general,
                    @remitente,
                    @destinatario,
                    @lugar_remitente,
                    @lugar_destinatario,
                    @conductor,
                    @vehiculo,
                    @observacion2,
                    @duplicado,
                    @triplicado)"

                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@no_conocimiento", txtConoNo.Text)
                        cmd.Parameters.AddWithValue("@para_general", txtPara.Text)
                        If Date.TryParse(txtFecha.Text, fechaConvertida) Then
                            cmd.Parameters.AddWithValue("@fecha_elaboracion", fechaConvertida.ToString("yyyy-MM-dd"))
                        End If
                        cmd.Parameters.AddWithValue("@cultivo_general", DDLCultivo.SelectedItem.Text)

                        cmd.Parameters.AddWithValue("@remitente", txtRemi.Text)
                        cmd.Parameters.AddWithValue("@destinatario", txtDestin.Text)
                        cmd.Parameters.AddWithValue("@lugar_remitente", txtLugarR.Text)
                        cmd.Parameters.AddWithValue("@lugar_destinatario", txtLugarD.Text)

                        cmd.Parameters.AddWithValue("@conductor", DDLConductor.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@vehiculo", txtVehic.Text)

                        cmd.Parameters.AddWithValue("@observacion2", DBNull.Value)
                        cmd.Parameters.AddWithValue("@duplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@triplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@estado", "1")

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        'Response.Write("<script>window.alert('¡Se ha registrado correctamente la solicitud del Multiplicador o Estación!') </script>")
                        cambiarEstadoProducto(txtConoNo.Text)

                        Label3.Text = "¡Se ha registrado correctamente la solicitud del Multiplicador o Estación!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                        Button1.Visible = True
                        Button2.Visible = True
                        btnGuardarLote.Visible = False
                        btnRegresar.Visible = True
                        btnRegresarConEmbarque.Visible = False
                    End Using
                End Using
            Else
                LabelGuardar.Visible = False
                LabelGuardar.Text = ""
                Dim connectionString As String = conn
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "UPDATE sag_embarque_info SET
                        para_general = @para_general,
                        fecha_elaboracion = @fecha_elaboracion,
                        cultivo_general = @cultivo_general,
                        remitente = @remitente,
                        destinatario = @destinatario,
                        lugar_remitente = @lugar_remitente,
                        lugar_destinatario = @lugar_destinatario,
                        conductor = @conductor,
                        vehiculo = @vehiculo,
                        observacion2 = @observacion2,
                        duplicado = @duplicado,
                        triplicado = @triplicado
                    WHERE no_conocimiento = " & txtID.Text & ""


                    Using cmd As New MySqlCommand(query, connection)

                        cmd.Parameters.AddWithValue("@para_general", txtPara.Text)
                        If Date.TryParse(txtFecha.Text, fechaConvertida) Then
                            cmd.Parameters.AddWithValue("@fecha_elaboracion", fechaConvertida.ToString("yyyy-MM-dd"))
                        End If
                        cmd.Parameters.AddWithValue("@cultivo_general", DDLCultivo.SelectedItem.Text)

                        cmd.Parameters.AddWithValue("@remitente", txtRemi.Text)
                        cmd.Parameters.AddWithValue("@destinatario", txtDestin.Text)
                        cmd.Parameters.AddWithValue("@lugar_remitente", txtLugarR.Text)
                        cmd.Parameters.AddWithValue("@lugar_destinatario", txtLugarD.Text)

                        cmd.Parameters.AddWithValue("@conductor", DDLConductor.SelectedItem.Text)
                        cmd.Parameters.AddWithValue("@vehiculo", txtVehic.Text)

                        cmd.Parameters.AddWithValue("@observacion2", DBNull.Value)
                        cmd.Parameters.AddWithValue("@duplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@triplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@estado", "1")

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
        'Aqui va lo que se va a vaciar

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
        'Aqui van las verificaciones
        If TextBanderita.Text = "Guardar" Then
            '1
            If String.IsNullOrEmpty(txtPara.Text) Then
                lblPara.Text = "*"
                validarflag = 0
            Else
                lblPara.Text = ""
                validarflag += 1
            End If
            '2
            If String.IsNullOrEmpty(txtFecha.Text) Then
                lblFecha.Text = "*"
                validarflag = 0
            Else
                lblFecha.Text = ""
                validarflag += 1
            End If
            '3
            If DDLCultivo.SelectedItem.Text = "Todos" Then
                lblCultivo.Text = "*"
                validarflag = 0
            Else
                lblCultivo.Text = ""
                validarflag += 1
            End If
            '4
            If String.IsNullOrEmpty(txtRemi.Text) Then
                lblremi.Text = "*"
                validarflag = 0
            Else
                lblremi.Text = ""
                validarflag += 1
            End If
            '5
            If String.IsNullOrEmpty(txtDestin.Text) Then
                lblDestin.Text = "*"
                validarflag = 0
            Else
                lblDestin.Text = ""
                validarflag += 1
            End If
            '6
            If String.IsNullOrEmpty(txtLugarR.Text) Then
                lblLugarR.Text = "*"
                validarflag = 0
            Else
                lblLugarR.Text = ""
                validarflag += 1
            End If
            '7
            If String.IsNullOrEmpty(txtLugarD.Text) Then
                lblLugarD.Text = "*"
                validarflag = 0
            Else
                lblLugarD.Text = ""
                validarflag += 1
            End If
            '8
            If DDLConductor.SelectedItem.Text <> "Todos" Then
                Label1.Text = ""
                validarflag += 1

            Else
                Label1.Text = "*"
                validarflag = 0
            End If
            '9
            'If String.IsNullOrEmpty(txtVehic.Text) Then
            '    lblVehic.Text = "*"
            '    validarflag = 0
            'Else
            '    lblVehic.Text = ""
            '    validarflag += 1
            'End If

            If validarflag = 8 Then
                validarflag = 1
            Else
                validarflag = 0
            End If
        Else

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
        Dim Str As String = "SELECT * FROM sag_registro_multiplicador WHERE nombre_multiplicador = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", txtID.Text)
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

        DivCrearNuevo.Visible = True
        DivGrid.Visible = False
        btnRegresar.Visible = True
        btnRegresarConEmbarque.Visible = False
        TextBanderita.Text = "Guardar"
        Llenar_conocimiento()
        llenarcomboConductor()
        VerificarTextBox()
        'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

    End Sub
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "INSERT INTO sag_embarque (
                    variedad_categoria,
                    categoria_origen,
                    tipo_cultivo,
                    variedad,
                    estado,
                    peso_neto,
                    no_conocimiento,
                    precio_uni,
                    total,
                    observaciones,
                    unidad
                    ) VALUES (@variedad_categoria,
                    @categoria_origen,
                    @tipo_cultivo,
                    @variedad,
                    @estado,
                    @peso_neto,
                    @no_conocimiento,
                    @precio_uni,
                    @total,
                    @observaciones,
                    @unidad
                    )"

            Using cmd As New MySqlCommand(query, connection)
                CrearIdentificador()
                cmd.Parameters.AddWithValue("@variedad_categoria", txtRespaldito.Text)
                cmd.Parameters.AddWithValue("@observaciones", txtObser.Text)
                cmd.Parameters.AddWithValue("@unidad", txtUnid.Text)
                cmd.Parameters.AddWithValue("@categoria_origen", TxtCateogiraGrid.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@tipo_cultivo", DDLCultivo.SelectedItem.Text)
                If DDLCultivo.SelectedItem.Text = "Frijol" Then
                    cmd.Parameters.AddWithValue("@variedad", DropDownList5.SelectedItem.Text)
                End If
                If DDLCultivo.SelectedItem.Text = "Maiz" Then
                    cmd.Parameters.AddWithValue("@variedad", DropDownList6.SelectedItem.Text)
                End If
                cmd.Parameters.AddWithValue("@peso_neto", Convert.ToDecimal(txtEntreg.Text))
                cmd.Parameters.AddWithValue("@precio_uni", Convert.ToDecimal(txtPrecio.Text))
                cmd.Parameters.AddWithValue("@total", Convert.ToDecimal(Convert.ToDecimal(txtEntreg.Text) * Convert.ToDecimal(txtPrecio.Text)))
                cmd.Parameters.AddWithValue("@no_conocimiento", txtConoNo.Text)
                cmd.Parameters.AddWithValue("@estado", "0")

                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
        llenaMinigrid()
        btnRegresar.Visible = False
        btnRegresarConEmbarque.Visible = True
    End Sub
    Protected Sub CrearIdentificador()
        Dim variedad As String

        If DDLCultivo.SelectedItem.Text = "Frijol" Then
            variedad = DropDownList5.SelectedItem.Text
        Else
            variedad = DropDownList6.SelectedItem.Text
        End If

        Dim categoria As String = TxtCateogiraGrid.SelectedItem.Text

        Dim resultado As String = String.Format("{0}-{1}", variedad, categoria)

        txtRespaldito.Text = resultado
    End Sub
    Private Sub Llenar_conocimiento()
        Dim strCombo As String = "SELECT COUNT(*) AS no_conocimiento FROM sag_embarque_info"
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        If DtCombo.Rows.Count > 0 AndAlso DtCombo.Columns.Count > 0 Then
            Dim total As Integer = DtCombo.Rows(0)("no_conocimiento")
            total += 1

            Dim year As String = DateTime.Now.Year.ToString()

            Dim resultadoFormateado As String = total.ToString("D3") & " - " & year

            txtConoNo.Text = resultadoFormateado
        Else
            Dim total1 As Integer = 1
            txtConoNo.Text = total1.ToString("D3") & " - " & DateTime.Now.Year.ToString()
        End If
    End Sub

    Protected Sub TxtMultiplicador_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(String.Format("~/pages/Embarque.aspx"))
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
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Dim Str As String = "SELECT * FROM sag_registro_multiplicador WHERE  ID='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            nuevo = False
            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString


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
            Dim Str As String = "SELECT * FROM vista_multi_lote WHERE nombre_multiplicador = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "vista_multi_lote")

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

            Dim query As String = "UPDATE sag_registro_multiplicador 
                    SET estado = @estado
                WHERE id = " & txtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()

                Response.Redirect(String.Format("~/pages/Embarque.aspx"))
            End Using

        End Using

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

    Sub llenaMinigrid()
        Dim cadena As String = "*"

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_embarque` WHERE no_conocimiento = '" & txtConoNo.Text & "' AND estado = '0' "

        GridProductos.DataBind()
    End Sub
    Protected Sub DDLCultivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLCultivo.SelectedIndexChanged
        ' Obtiene el valor seleccionado en la DropDownList
        Dim selectedValue As String = DDLCultivo.SelectedItem.Text

        ' Si selecciona "Frijol," muestra la TextBox de Variedad; de lo contrario, ocúltala
        If selectedValue = "Frijol" Then
            DropDownList6.SelectedIndex = 0
            VariedadFrijol.Visible = True
            VariedadMaiz.Visible = False
            llenarcomboFrijol()
        ElseIf selectedValue = "Maiz" Then
            VariedadMaiz.Visible = True
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
            llenarcomboMaiz()
        Else
            VariedadMaiz.Visible = False
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
            DropDownList6.SelectedIndex = 0
        End If

        VerificarTextBox()
    End Sub
    Private Sub llenarcomboFrijol()
        Dim StrCombo As String

        StrCombo = "SELECT variedad FROM vista_suma_tabla_a WHERE tipo_cultivo = 'Frijol' ORDER BY variedad ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        DropDownList5.DataSource = DtCombo
        DropDownList5.DataValueField = DtCombo.Columns(0).ToString()
        DropDownList5.DataTextField = DtCombo.Columns(0).ToString()
        DropDownList5.DataBind()
        Dim newitem As New ListItem(" ", " ")
        DropDownList5.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboMaiz()
        Dim StrCombo As String

        StrCombo = "SELECT variedad FROM vista_suma_tabla_a WHERE tipo_cultivo = 'Maiz' ORDER BY variedad ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        DropDownList6.DataSource = DtCombo
        DropDownList6.DataValueField = DtCombo.Columns(0).ToString()
        DropDownList6.DataTextField = DtCombo.Columns(0).ToString()
        DropDownList6.DataBind()
        Dim newitem As New ListItem(" ", " ")
        DropDownList6.Items.Insert(0, newitem)
    End Sub
    Protected Sub eliminarMiniGrid2()
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_embarque WHERE estado = 0"

            Using cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
    End Sub
    Protected Sub eliminarMiniGrid(sender As Object, e As EventArgs) Handles btnRegresarConEmbarque.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_embarque WHERE estado = 0"

            Using cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
                connection.Close()
                Response.Redirect(String.Format("~/pages/Embarque.aspx"))
            End Using
        End Using
    End Sub

    Protected Sub eliminarMiniGridEspecifico(id As String)
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_embarque 
                WHERE id = " & id & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
            llenaMinigrid()
        End Using
    End Sub
    Protected Sub GridProductos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridProductos.RowCommand

        If (e.CommandName = "Eliminar") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            txtidminigrid.Text = ""
            txtidminigrid.Text = HttpUtility.HtmlDecode(GridProductos.Rows(index).Cells(0).Text).ToString
            eliminarMiniGridEspecifico(txtidminigrid.Text)
        End If
    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtEntreg.Text = ""
        llenarcomboCategoriaFrijol()
        VerificarTextBox()
    End Sub
    Private Sub llenarcomboCategoriaFrijol()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT categoria_origen FROM vista_suma_tabla_a WHERE variedad = '" & DropDownList5.SelectedItem.Text & "' ORDER BY categoria_origen ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        TxtCateogiraGrid.DataSource = DtCombo
        TxtCateogiraGrid.DataValueField = DtCombo.Columns(0).ToString()
        TxtCateogiraGrid.DataTextField = DtCombo.Columns(0).ToString()
        TxtCateogiraGrid.DataBind()
        Dim newitem As New ListItem(" ", " ")
        TxtCateogiraGrid.Items.Insert(0, newitem)
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtEntreg.Text = ""
        llenarcomboCategoriaMaiz()
        VerificarTextBox()
    End Sub
    Private Sub llenarcomboCategoriaMaiz()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT categoria_origen FROM vista_suma_tabla_a WHERE variedad = '" & DropDownList6.SelectedItem.Text & "' ORDER BY categoria_origen ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        TxtCateogiraGrid.DataSource = DtCombo
        TxtCateogiraGrid.DataValueField = DtCombo.Columns(0).ToString()
        TxtCateogiraGrid.DataTextField = DtCombo.Columns(0).ToString()
        TxtCateogiraGrid.DataBind()
        Dim newitem As New ListItem(" ", " ")
        TxtCateogiraGrid.Items.Insert(0, newitem)
    End Sub

    Protected Sub TxtCateogiraGrid_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtEntreg.Text = ""
        VerificarTextBox()
    End Sub

    Protected Sub txtEntreg_TextChanged(sender As Object, e As EventArgs) Handles txtEntreg.TextChanged
        ' Obtener el valor ingresado en txtEntreg
        Dim entregado As Integer = 0
        If Integer.TryParse(txtEntreg.Text, entregado) Then
            ' Construir la consulta SQL dinámica
            Dim c1 As String = "SELECT peso_neto_resta FROM vista_inventario WHERE 1=1 "
            Dim c2 As String
            Dim c3 As String

            ' Obtener las selecciones de los DropDownList
            If DropDownList5.SelectedItem.Text = "Todos" And DropDownList6.SelectedItem.Text <> "Todos" Then
                c2 = " AND variedad = '" & DropDownList6.SelectedItem.Text & "' "
            Else
                c2 = " "
            End If

            If DropDownList6.SelectedItem.Text = "Todos" And DropDownList5.SelectedItem.Text <> "Todos" Then
                c2 = " AND variedad = '" & DropDownList5.SelectedItem.Text & "' "
            Else
                c2 = " "
            End If

            If (TxtCateogiraGrid.SelectedItem.Text = "Todos") Then
                c3 = " "
            Else
                c3 = " AND categoria_origen = '" & TxtCateogiraGrid.SelectedItem.Text & "' "
            End If

            ' Agregar condiciones a la consulta SQL
            Dim query As String = c1 & c2 & c3

            Dim strCombo As String = query
            Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
            Dim DtCombo As New DataTable()
            adaptcombo.Fill(DtCombo)

            If DtCombo.Rows.Count > 0 AndAlso Not IsDBNull(DtCombo.Rows(0)("peso_neto_resta")) Then
                pesoTotal = Convert.ToInt32(DtCombo.Rows(0)("peso_neto_resta"))

                If entregado <= pesoTotal Then
                    ' El valor ingresado es menor o igual al peso total
                    lblEntreg.Text = ""
                Else
                    ' El valor ingresado es mayor al peso total
                    lblEntreg.Text = "*"
                    Label3.Text = "El valor de entrega ingresado (" & txtEntreg.Text & ") supera al valor en inventario que es: (" & DtCombo.Rows(0)("peso_neto_resta").ToString & ")"
                    txtEntreg.Text = ""
                    BConfirm.Visible = False
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                End If

            End If

        Else
            ' Mostrar mensaje de error si el valor ingresado no es un número válido
            lblEntreg.Text = "*"
        End If

        VerificarTextBox()

    End Sub
    Protected Sub DDLConductor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLConductor.SelectedIndexChanged
        llenarcomboinfoAuto()
    End Sub
    Private Sub llenarcomboConductor()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT nombre FROM sag_registro_vehiculo_motorista ORDER BY nombre ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        DDLConductor.DataSource = DtCombo
        DDLConductor.DataValueField = DtCombo.Columns(0).ToString()
        DDLConductor.DataTextField = DtCombo.Columns(0).ToString()
        DDLConductor.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        DDLConductor.Items.Insert(0, newitem)
    End Sub

    Private Sub llenarcomboinfoAuto()
        If DDLConductor.SelectedItem.Text <> "Todos" Then
            Dim StrCombo As String

            StrCombo = "SELECT codvehi FROM sag_registro_vehiculo_motorista WHERE nombre = '" & DDLConductor.SelectedItem.Text & "' ORDER BY nombre ASC"

            Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
            Dim DtCombo As New DataTable
            adaptcombo.Fill(DtCombo)

            If DtCombo.Rows.Count > 0 Then
                ' Asigna el valor de codvehi al TextBox
                txtVehic.Text = DtCombo.Rows(0)("codvehi").ToString()
            Else
                ' Maneja el caso en que no se encuentre ningún registro
                txtVehic.Text = "No se encontró codvehi para el conductor seleccionado."
            End If
        End If
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
End Class