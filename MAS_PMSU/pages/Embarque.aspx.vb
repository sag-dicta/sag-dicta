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
                txtFechaDesde.Text = New DateTime(2024, 1, 1).ToString("yyyy-MM-dd")
                txtFechaHasta.Text = DateTime.Today.ToString("yyyy-MM-dd")

                llenarcomboProductor()
                llenarcomboConocimiento()
                VerificarTextBox()
                llenagrid()
                eliminarMiniGrid2()
                btnGuardarLote.Visible = True
            End If
        End If
    End Sub

    Protected Sub guardarSoli_lote(sender As Object, e As EventArgs)
        verificar_Produc()
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

                        cmd.Parameters.AddWithValue("@observacion2", txtObser2.Text)
                        cmd.Parameters.AddWithValue("@duplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@triplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@estado", "1")

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        'Response.Write("<script>window.alert('¡Se ha registrado correctamente la solicitud del Multiplicador o Estación!') </script>")
                        cambiarEstadoProducto(txtConoNo.Text)

                        Label3.Text = "¡Se ha registrado correctamente el conocimiento de embarque!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

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
                    WHERE id = " & txtID.Text & ""


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

                        cmd.Parameters.AddWithValue("@observacion2", txtObser2.Text)
                        cmd.Parameters.AddWithValue("@duplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@triplicado", DBNull.Value)
                        cmd.Parameters.AddWithValue("@estado", "1")

                        cmd.ExecuteNonQuery()
                        connection.Close()

                        'Response.Write("<script>window.alert('¡Se ha editado correctamente la solicitud del Multiplicador o Estación!') </script>")
                        cambiarEstadoProducto(txtConoNo.Text)

                        Label3.Text = "¡Se ha editado correctamente el conocimiento de embarque!"
                        BBorrarsi.Visible = False
                        BBorrarno.Visible = False
                        ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
                        btnGuardarLote.Visible = False
                        btnRegresar.Visible = True
                        btnRegresarConEmbarque.Visible = False
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
        verificardatosproductos()
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
            If String.IsNullOrEmpty(txtObser2.Text) Then
                lblObser2.Text = "*"
                validarflag = 0
            Else
                lblObser2.Text = ""
                validarflag += 1
            End If
            '10
            If verificar_Produc() = 0 Then
                lblmas.Text = "Debe ingresar al menos un producto de semilla."
                validarflag = 0
            Else
                lblmas.Text = ""
                validarflag += 1
            End If

            If validarflag = 10 Then
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
        Dim cadena As String = "Id, estado, no_conocimiento, para_general, DATE_FORMAT(fecha_elaboracion, '%d-%m-%Y') AS fecha_elaboracion, cultivo_general, remitente, destinatario, lugar_remitente, lugar_destinatario, conductor, vehiculo, observacion2"
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

        BAgregar.Visible = True
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

        StrCombo = "SELECT para_general FROM sag_embarque_info WHERE estado = '1' ORDER BY para_general ASC"

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
    Protected Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

        DivCrearNuevo.Visible = True
        DivGrid.Visible = False
        btnRegresar.Visible = True
        btnRegresarConEmbarque.Visible = False
        TextBanderita.Text = "Guardar"
        Llenar_conocimiento()
        llenarcomboConductor()
        verificar_Produc()
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
        vaciarCamposProductos()
        verificar_Produc()
        VerificarTextBox()
    End Sub
    Protected Sub vaciarCamposProductos()
        DropDownList5.SelectedIndex = 0
        DropDownList6.SelectedIndex = 0
        TxtCateogiraGrid.SelectedIndex = 0
        txtUnid.Text = "QQ"
        txtEntreg.Text = ""
        txtPrecio.Text = ""
        txtObser.Text = ""
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
        If TxtMultiplicador.SelectedItem.Text = "Todos" Then
            llenarcomboConocimiento()
        Else
            llenarcomboConocimiento2()
        End If

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

        If (e.CommandName = "Detalles") Then

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            txtConoNo.Text = ""
            txtConoNo.Text = HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(1).Text).ToString
            ModalTitle3.InnerText = "Información del Embarque " & HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(1).Text).ToString
            Dim cadena As String = "*"

            Me.SqlDataSource3.SelectCommand = "SELECT " & cadena & " FROM `sag_embarque` WHERE no_conocimiento = '" & txtConoNo.Text & "'"

            GridDetalles.DataBind()

            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal3').modal('show'); });", True)

        End If

        If (e.CommandName = "Editar") Then
            llenarcomboConductor()
            btnGuardarLote.Text = "Actualizar"

            DivCrearNuevo.Visible = True
            DivGrid.Visible = False

            btnRegresar.Visible = True
            btnRegresarConEmbarque.Visible = False

            TextBanderita.Text = "Guardar"

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            txtID.Text = ""
            txtID.Text = HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(0).Text).ToString
            Dim Str As String = "SELECT * FROM vista_embarque_general WHERE  ID_EMBARQUE_INFO = " & txtID.Text & ""
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            txtConoNo.Text = dt.Rows(0)("NO_CONOCIMIENTO_EMBARQUE_INFO").ToString()
            txtPara.Text = dt.Rows(0)("PARA_GENERAL").ToString()
            txtFecha.Text = If(dt.Rows(0)("FECHA_ELABORACION") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("FECHA_ELABORACION"), DateTime).ToString("yyyy-MM-dd"))
            SeleccionarItemEnDropDownList(DDLCultivo, dt.Rows(0)("CULTIVO_GENERAL").ToString())
            DDLCultivo.Enabled = False
            If dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Frijol" Then
                VariedadFrijol.Visible = True
            End If
            If dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Maiz" Then
                VariedadMaiz.Visible = True
            End If
            txtRemi.Text = dt.Rows(0)("REMITENTE").ToString()
            txtDestin.Text = dt.Rows(0)("DESTINATARIO").ToString()
            txtLugarR.Text = dt.Rows(0)("LUGAR_REMITENTE").ToString()
            txtLugarD.Text = dt.Rows(0)("LUGAR_DESTINATARIO").ToString()
            SeleccionarItemEnDropDownList(DDLConductor, dt.Rows(0)("CONDUCTOR").ToString())
            txtVehic.Text = dt.Rows(0)("VEHICULO").ToString()
            llenaMinigrid()
            verificar_Produc()
            VerificarTextBox()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            TextminigridCambiarestado.Text = HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString

            Label3.Text = "¿Desea eliminar el conocimiento de embarque?"
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
            Dim Str As String = "SELECT * FROM vista_embarque_informe WHERE NO_CONOCIMIENTO_EMBARQUE_INFO = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "vista_embarque_informe")

            Dim nombre As String

            nombre = "Conocimiento de Embarque No " + HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString + " " + Today

            rptdocument.Load(Server.MapPath("~/pages/EmbarqueReport.rpt"))

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
    Protected Function SeleccionarItemEnDropDownListFrijolOMaiz(ByVal DtCombo As String, ByVal DtCombo2 As String)
        If DtCombo2 = "Frijol" Then
            For Each item As ListItem In DropDownList5.Items
                If item.Text = DtCombo Then
                    DropDownList5.SelectedValue = item.Value
                    DropDownList5.Visible = True
                    DropDownList6.Visible = False
                    Return True ' Se encontró una coincidencia, devolver verdadero
                End If
            Next
            ' No se encontró ninguna coincidencia
            Return 0
        Else
            For Each item As ListItem In DropDownList6.Items
                If item.Text = DtCombo Then
                    DropDownList6.SelectedValue = item.Value
                    DropDownList6.Visible = True
                    DropDownList5.Visible = False
                    Return True ' Se encontró una coincidencia, devolver verdadero
                End If
            Next
            Return 0
        End If
        Return 0
    End Function
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

            Dim query As String = "UPDATE sag_embarque_info 
                    SET estado = @estado
                WHERE id = " & txtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()

                elminarProductos()
                Response.Redirect(String.Format("~/pages/Embarque.aspx"))
            End Using

        End Using
    End Sub
    Protected Sub elminarProductos()
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_embarque 
                    SET estado = @estado
                WHERE no_conocimiento = '" & TextminigridCambiarestado.Text & "'"

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "3")
                cmd.ExecuteNonQuery()
                connection.Close()
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
            c1 = "AND PARA_GENERAL = '" & TxtMultiplicador.SelectedItem.Text & "' "
        End If

        If (DDLConoc.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND NO_CONOCIMIENTO_EMBARQUE_INFO = '" & DDLConoc.SelectedItem.Text & "' "
        End If

        query = "SELECT " & cadena & " FROM `vista_embarque_general` WHERE 1 = 1 " & c1 & c3 & " AND FECHA_ELABORACION >= '" & txtFechaDesde.Text & "' AND FECHA_ELABORACION <= '" & txtFechaHasta.Text & "'"

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "vista_embarque_general"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Información del Embarque " & Today & " " & TxtMultiplicador.SelectedItem.Text & ".xlsx")
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

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_embarque` WHERE no_conocimiento = '" & txtConoNo.Text & "'"

        GridProductos.DataBind()
        VerificarTextBox()
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
    Protected Sub eliminarMiniGrid3(id As String)
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_embarque WHERE id = " & id & ""

            Using cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
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
        verificar_Produc()
        VerificarTextBox()
    End Sub
    Protected Sub GridProductos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridProductos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridProductos.Rows(index)
            txtidminigrid.Text = ""
            txtidminigrid.Text = HttpUtility.HtmlDecode(GridProductos.Rows(index).Cells(0).Text).ToString
            eliminarMiniGridEspecifico(txtidminigrid.Text)

            DropDownList5.SelectedIndex = 0
            DropDownList6.SelectedIndex = 0
            TxtCateogiraGrid.SelectedIndex = 0
            txtEntreg.Text = ""
            txtPrecio.Text = ""
            txtObser.Text = ""

        End If

        If (e.CommandName = "Editar") Then
            btnAgregar.Visible = False
            Dim gvrow As GridViewRow = GridProductos.Rows(index)
            txtidminigrid.Text = ""
            txtidminigrid.Text = HttpUtility.HtmlDecode(GridProductos.Rows(index).Cells(0).Text).ToString
            Dim Str As String = "SELECT * FROM sag_embarque WHERE  ID= " & txtidminigrid.Text & ""
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            SeleccionarItemEnDropDownListFrijolOMaiz(dt.Rows(0)("variedad").ToString(), dt.Rows(0)("tipo_cultivo").ToString())
            SeleccionarItemEnDropDownList(TxtCateogiraGrid, dt.Rows(0)("categoria_origen").ToString())
            txtUnid.Text = dt.Rows(0)("unidad").ToString()
            txtEntreg.Text = dt.Rows(0)("peso_neto").ToString()
            txtPrecio.Text = dt.Rows(0)("precio_uni").ToString()
            txtObser.Text = dt.Rows(0)("observaciones").ToString()
            verificardatosproductos()
            eliminarMiniGrid3(txtidminigrid.Text)
            llenaMinigrid()
        End If
    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtEntreg.Text = ""
        llenarcomboCategoriaFrijol()
        verificardatosproductos()
        VerificarTextBox()
    End Sub
    Private Sub llenarcomboCategoriaFrijol()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT categoria_registrado FROM vista_suma_tabla_a WHERE variedad = '" & DropDownList5.SelectedItem.Text & "' ORDER BY categoria_registrado ASC"

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
        verificardatosproductos()
        VerificarTextBox()
    End Sub
    Private Sub llenarcomboCategoriaMaiz()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT categoria_registrado FROM vista_suma_tabla_a WHERE variedad = '" & DropDownList6.SelectedItem.Text & "' ORDER BY categoria_registrado ASC"

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

        ' Obtener el valor ingresado en txtEntreg
        Dim entregado As Integer = 0
        ' Construir la consulta SQL dinámica
        Dim c1 As String = "SELECT peso_neto_resta FROM vista_inventario2 WHERE 1=1 "
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
            c3 = " AND categoria_registrad = '" & TxtCateogiraGrid.SelectedItem.Text & "' "
        End If

        ' Agregar condiciones a la consulta SQL
        Dim query As String = c1 & c2 & c3

        Dim strCombo As String = query
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        If DtCombo.Rows.Count > 0 AndAlso Not IsDBNull(DtCombo.Rows(0)("peso_neto_resta")) Then
            pesoTotal = Convert.ToInt32(DtCombo.Rows(0)("peso_neto_resta"))
            txtEntreg.Text = pesoTotal
        End If

        'txtEntreg.Text = ""
        verificardatosproductos()
        VerificarTextBox()
    End Sub
    Protected Sub txtEntreg_TextChanged(sender As Object, e As EventArgs) Handles txtEntreg.TextChanged
        ' Obtener el valor ingresado en txtEntreg
        Dim entregado As Integer = 0
        If Integer.TryParse(txtEntreg.Text, entregado) Then
            ' Construir la consulta SQL dinámica
            Dim c1 As String = "SELECT peso_neto_resta FROM vista_inventario2 WHERE 1=1 "
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
                c3 = " AND categoria_registrado = '" & TxtCateogiraGrid.SelectedItem.Text & "' "
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
                    Label2.Text = "El valor de entrega ingresado (" & txtEntreg.Text & ") supera al valor en inventario que es: (" & DtCombo.Rows(0)("peso_neto_resta").ToString & ")"
                    txtEntreg.Text = ""
                    BConfirm.Visible = False
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal2').modal('show'); });", True)

                End If

            End If

        Else
            ' Mostrar mensaje de error si el valor ingresado no es un número válido
            lblEntreg.Text = "*"
        End If

        verificardatosproductos()
        VerificarTextBox()

    End Sub
    Protected Sub DDLConductor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLConductor.SelectedIndexChanged
        llenarcomboinfoAuto()
        VerificarTextBox()
    End Sub
    Private Sub llenarcomboConductor()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT nombre FROM sag_registro_vehiculo_motorista WHERE estado='1' ORDER BY nombre ASC"

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
    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/Embarque.aspx"))
    End Sub

    Protected Sub DDLConoc_SelectedIndexChanged(sender As Object, e As EventArgs)
        llenagrid()
    End Sub
    Private Sub llenarcomboConocimiento()
        Dim StrCombo As String

        StrCombo = "SELECT no_conocimiento FROM sag_embarque_info WHERE estado = '1' ORDER BY no_conocimiento ASC"

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

        StrCombo = "SELECT no_conocimiento FROM sag_embarque_info WHERE estado = '1' AND para_general= '" & TxtMultiplicador.SelectedItem.Text & "' ORDER BY no_conocimiento ASC"

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
    Private Function verificar_Produc()
        Dim strCombo As String = "SELECT * FROM sag_embarque WHERE no_conocimiento = '" & txtConoNo.Text & "'"
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        Return DtCombo.Rows.Count
    End Function

    Sub verificardatosproductos()
        Dim validar As Integer = 0

        If String.IsNullOrEmpty(txtEntreg.Text) Then
            lblLugarD.Text = "*"
            validar = 0
        Else
            lblLugarD.Text = ""
            validar += 1
        End If

        If String.IsNullOrEmpty(txtPrecio.Text) Then
            lblLugarD.Text = "*"
            validar = 0
        Else
            lblLugarD.Text = ""
            validar += 1
        End If

        If String.IsNullOrEmpty(txtObser.Text) Then
            lblLugarD.Text = "*"
            validar = 0
        Else
            lblLugarD.Text = ""
            validar += 1
        End If


        If DDLCultivo.SelectedItem.Text = "Frijol" Then
            If DropDownList5.SelectedItem.Text <> " " Then
                Label1.Text = ""
                validar += 1

            Else
                Label1.Text = "*"
                validar = 0
            End If
        Else
            If DropDownList6.SelectedItem.Text <> " " Then
                Label1.Text = ""
                validar += 1

            Else
                Label1.Text = "*"
                validar = 0
            End If
        End If

        If TxtCateogiraGrid.SelectedItem.Text <> " " Then
            Label1.Text = ""
            validar += 1

        Else
            Label1.Text = "*"
            validar = 0
        End If

        If validar = 5 Then
            btnAgregar.Visible = True
        Else
            btnAgregar.Visible = False
        End If
    End Sub
End Class