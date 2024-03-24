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
                llenarcomboCultivo()
                llenarcomboCultivo2()
                VerificarTextBox()
                llenagrid()
                eliminarMiniGrid2()
                btnGuardarLote.Visible = True
            End If
        End If
    End Sub

    Protected Sub guardarSoli_lote(sender As Object, e As EventArgs)
        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
            verificar_Produc_convenio()
        Else
            verificar_Produc()
        End If
        VerificarTextBox()
        Dim fechaConvertida As Date
        Dim fechaconvenio As Date
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
                        tipo_salida,
                        fecha_final_convenio,
                        identidad,
                        mz_sembrar_qq,
                        variedad_conve,
                        categoria_conve,
                        produ_apro_qq_mz,
                        precio_minimo,
                        compensacion,
                        precio_final,
                        no_convenio
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
                        @tipo_salida,
                        @fecha_final_convenio,
                        @identidad,
                        @mz_sembrar_qq,
                        @variedad_conve,
                        @categoria_conve,
                        @produ_apro_qq_mz,
                        @precio_minimo,
                        @compensacion,
                        @precio_final,
                        @no_convenio)"

                    Using cmd As New MySqlCommand(query, connection)

                        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
                            cmd.Parameters.AddWithValue("@tipo_salida", ddl_tiposalida.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@no_convenio", txtConoNo.Text)
                            cmd.Parameters.AddWithValue("@no_conocimiento", DBNull.Value)
                            If Date.TryParse(txtFecha.Text, fechaConvertida) Then
                                cmd.Parameters.AddWithValue("@fecha_elaboracion", fechaConvertida.ToString("yyyy-MM-dd"))
                            End If
                            If Date.TryParse(txtFecha2.Text, fechaconvenio) Then
                                cmd.Parameters.AddWithValue("@fecha_final_convenio", fechaconvenio.ToString("yyyy-MM-dd"))
                            End If
                            cmd.Parameters.AddWithValue("@para_general", txtParaConv.Text)
                            cmd.Parameters.AddWithValue("@identidad", txtParaIdent.Text)
                            cmd.Parameters.AddWithValue("@mz_sembrar_qq", txtMzSembrar.Text)
                            cmd.Parameters.AddWithValue("@cultivo_general", txtCultiConv.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@variedad_conve", txtVariedadConv.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@categoria_conve", txtCategConv.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@produ_apro_qq_mz", Convert.ToDecimal(txtProducAprox.Text))
                            cmd.Parameters.AddWithValue("@precio_minimo", Convert.ToDecimal(txtPrecioMinimoCompra.Text))
                            cmd.Parameters.AddWithValue("@compensacion", Convert.ToDecimal(txtCompPerd.Text))
                            cmd.Parameters.AddWithValue("@precio_final", Convert.ToDecimal(txtPrecioFinal.Text))

                            cmd.Parameters.AddWithValue("@remitente", DBNull.Value)
                            cmd.Parameters.AddWithValue("@destinatario", DBNull.Value)
                            cmd.Parameters.AddWithValue("@lugar_remitente", DBNull.Value)
                            cmd.Parameters.AddWithValue("@lugar_destinatario", DBNull.Value)

                            cmd.Parameters.AddWithValue("@conductor", DBNull.Value)
                            cmd.Parameters.AddWithValue("@vehiculo", DBNull.Value)

                            cmd.Parameters.AddWithValue("@observacion2", DBNull.Value)

                            cmd.Parameters.AddWithValue("@estado", "1")
                        Else
                            cmd.Parameters.AddWithValue("@tipo_salida", ddl_tiposalida.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@no_conocimiento", txtConoNo.Text)
                            cmd.Parameters.AddWithValue("@no_convenio", DBNull.Value)
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

                            cmd.Parameters.AddWithValue("@variedad_conve", DBNull.Value)
                            cmd.Parameters.AddWithValue("@categoria_conve", DBNull.Value)
                            cmd.Parameters.AddWithValue("@produ_apro_qq_mz", DBNull.Value)
                            cmd.Parameters.AddWithValue("@precio_minimo", DBNull.Value)
                            cmd.Parameters.AddWithValue("@compensacion", DBNull.Value)
                            cmd.Parameters.AddWithValue("@precio_final", DBNull.Value)
                            cmd.Parameters.AddWithValue("@fecha_final_convenio", DBNull.Value)
                            cmd.Parameters.AddWithValue("@identidad", DBNull.Value)
                            cmd.Parameters.AddWithValue("@mz_sembrar_qq", DBNull.Value)

                            cmd.Parameters.AddWithValue("@estado", "1")
                        End If
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
                            tipo_salida = @tipo_salida,
                            fecha_final_convenio = @fecha_final_convenio,
                            identidad = @identidad,
                            mz_sembrar_qq= @mz_sembrar_qq,
                            variedad_conve = @variedad_conve,
                            categoria_conve = @categoria_conve,
                            produ_apro_qq_mz = @produ_apro_qq_mz,
                            precio_minimo = @precio_minimo,
                            compensacion = @compensacion,
                            precio_final = @precio_final,
                            no_convenio = @no_convenio
                            
                        WHERE id = " & txtID.Text & ""


                    Using cmd As New MySqlCommand(query, connection)

                        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
                            cmd.Parameters.AddWithValue("@tipo_salida", ddl_tiposalida.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@no_convenio", txtConoNo.Text)
                            cmd.Parameters.AddWithValue("@no_conocimiento", DBNull.Value)
                            If Date.TryParse(txtFecha.Text, fechaConvertida) Then
                                cmd.Parameters.AddWithValue("@fecha_elaboracion", fechaConvertida.ToString("yyyy-MM-dd"))
                            End If
                            If Date.TryParse(txtFecha2.Text, fechaconvenio) Then
                                cmd.Parameters.AddWithValue("@fecha_final_convenio", fechaconvenio.ToString("yyyy-MM-dd"))
                            End If
                            cmd.Parameters.AddWithValue("@para_general", txtParaConv.Text)
                            cmd.Parameters.AddWithValue("@identidad", txtParaIdent.Text)
                            cmd.Parameters.AddWithValue("@mz_sembrar_qq", txtMzSembrar.Text)
                            cmd.Parameters.AddWithValue("@cultivo_general", txtCultiConv.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@variedad_conve", txtVariedadConv.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@categoria_conve", txtCategConv.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@produ_apro_qq_mz", Convert.ToDecimal(txtProducAprox.Text))
                            cmd.Parameters.AddWithValue("@precio_minimo", Convert.ToDecimal(txtPrecioMinimoCompra.Text))
                            cmd.Parameters.AddWithValue("@compensacion", Convert.ToDecimal(txtCompPerd.Text))
                            cmd.Parameters.AddWithValue("@precio_final", Convert.ToDecimal(txtPrecioFinal.Text))

                            cmd.Parameters.AddWithValue("@remitente", DBNull.Value)
                            cmd.Parameters.AddWithValue("@destinatario", DBNull.Value)
                            cmd.Parameters.AddWithValue("@lugar_remitente", DBNull.Value)
                            cmd.Parameters.AddWithValue("@lugar_destinatario", DBNull.Value)

                            cmd.Parameters.AddWithValue("@conductor", DBNull.Value)
                            cmd.Parameters.AddWithValue("@vehiculo", DBNull.Value)

                            cmd.Parameters.AddWithValue("@observacion2", DBNull.Value)

                            cmd.Parameters.AddWithValue("@estado", "1")
                        Else
                            cmd.Parameters.AddWithValue("@tipo_salida", ddl_tiposalida.SelectedItem.Text)
                            cmd.Parameters.AddWithValue("@no_conocimiento", txtConoNo.Text)
                            cmd.Parameters.AddWithValue("@no_convenio", DBNull.Value)
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

                            cmd.Parameters.AddWithValue("@variedad_conve", DBNull.Value)
                            cmd.Parameters.AddWithValue("@categoria_conve", DBNull.Value)
                            cmd.Parameters.AddWithValue("@produ_apro_qq_mz", DBNull.Value)
                            cmd.Parameters.AddWithValue("@precio_minimo", DBNull.Value)
                            cmd.Parameters.AddWithValue("@compensacion", DBNull.Value)
                            cmd.Parameters.AddWithValue("@precio_final", DBNull.Value)
                            cmd.Parameters.AddWithValue("@fecha_final_convenio", DBNull.Value)
                            cmd.Parameters.AddWithValue("@identidad", DBNull.Value)
                            cmd.Parameters.AddWithValue("@mz_sembrar_qq", DBNull.Value)

                            cmd.Parameters.AddWithValue("@estado", "1")
                        End If

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
        TextBanderita.Text = ddl_tiposalida.SelectedItem.Text
        'Aqui van las verificaciones
        If TextBanderita.Text = " " Or TextBanderita.Text = "Distribución y embarque" Or TextBanderita.Text = "Actas" Then
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
            If DDLCultivo.SelectedItem.Text = " " Then
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
            If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
                If verificar_Produc_convenio() = 0 Then
                    lblmas.Text = "Debe ingresar al menos un producto de semilla."
                    validarflag = 0
                Else
                    lblmas.Text = ""
                    validarflag += 1
                End If
            Else
                If verificar_Produc() = 0 Then
                    lblmas.Text = "Debe ingresar al menos un producto de semilla."
                    validarflag = 0
                Else
                    lblmas.Text = ""
                    validarflag += 1
                End If
            End If
            '11
            If ddl_tiposalida.SelectedItem.Text <> " " Then
                lbltiposalida.Text = ""
                validarflag += 1

            Else
                lbltiposalida.Text = "*"
                validarflag = 0
            End If

            If validarflag = 11 Then
                validarflag = 1
            Else
                validarflag = 0
            End If
        Else
            '1
            If String.IsNullOrEmpty(txtParaConv.Text) Then
                lblParaConv.Text = "*"
                validarflag = 0
            Else
                lblParaConv.Text = ""
                validarflag += 1
            End If
            '2
            If String.IsNullOrEmpty(txtCompPerd.Text) Then
                lblCompPerd.Text = "*"
                validarflag = 0
            Else
                lblCompPerd.Text = ""
                validarflag += 1
            End If
            '3
            If txtCultiConv.SelectedItem.Text = " " Then
                lblCultiConv.Text = "*"
                validarflag = 0
            Else
                lblCultiConv.Text = ""
                validarflag += 1
            End If
            '4
            If String.IsNullOrEmpty(txtParaIdent.Text) Then
                lblParaIdent.Text = "*"
                validarflag = 0
            Else
                lblParaIdent.Text = ""
                validarflag += 1
            End If
            '5
            If String.IsNullOrEmpty(txtMzSembrar.Text) Then
                lblMzSembrar.Text = "*"
                validarflag = 0
            Else
                lblMzSembrar.Text = ""
                validarflag += 1
            End If
            '6
            If txtVariedadConv.SelectedItem.Text = " " Then
                lblVariedadConv.Text = "*"
                validarflag = 0
            Else
                lblVariedadConv.Text = ""
                validarflag += 1
            End If
            '7
            If txtCategConv.SelectedItem.Text = " " Then
                lblCategConv.Text = "*"
                validarflag = 0
            Else
                lblCategConv.Text = ""
                validarflag += 1
            End If
            '8
            If String.IsNullOrEmpty(txtProducAprox.Text) Then
                lblProducAprox.Text = "*"
                validarflag = 0
            Else
                lblProducAprox.Text = ""
                validarflag += 1
            End If
            '9
            If String.IsNullOrEmpty(txtPrecioMinimoCompra.Text) Then
                lblPrecioMinimoCompra.Text = "*"
                validarflag = 0
            Else
                lblPrecioMinimoCompra.Text = ""
                validarflag += 1
            End If
            '10
            If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
                If verificar_Produc_convenio() = 0 Then
                    lblmas.Text = "Debe ingresar al menos un producto de semilla."
                    validarflag = 0
                Else
                    lblmas.Text = ""
                    validarflag += 1
                End If
            Else
                If verificar_Produc() = 0 Then
                    lblmas.Text = "Debe ingresar al menos un producto de semilla."
                    validarflag = 0
                Else
                    lblmas.Text = ""
                    validarflag += 1
                End If
            End If
            '11
            If ddl_tiposalida.SelectedItem.Text <> " " Then
                lbltiposalida.Text = ""
                validarflag += 1

            Else
                lbltiposalida.Text = "*"
                validarflag = 0
            End If
            '12
            If String.IsNullOrEmpty(txtFecha.Text) Then
                lblFecha.Text = "*"
                validarflag = 0
            Else
                lblFecha.Text = ""
                validarflag += 1
            End If

            If validarflag = 12 Then
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
        Dim cadena As String = "Id, estado, no_conocimiento, para_general, DATE_FORMAT(fecha_elaboracion, '%d-%m-%Y') AS fecha_elaboracion, cultivo_general, remitente, destinatario, lugar_remitente, lugar_destinatario, conductor, vehiculo, observacion2, tipo_salida, no_convenio"
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
    Protected Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

        DivCrearNuevo.Visible = True
        DivGrid.Visible = False
        btnRegresar.Visible = True
        btnRegresarConEmbarque.Visible = False
        TextBanderita.Text = " "
        llenarcomboCultivo()
        llenarcomboCultivo2()
        If ddl_tiposalida.SelectedItem.Text = "Distribución y embarque" Or ddl_tiposalida.SelectedItem.Text = "Actas" Or ddl_tiposalida.SelectedItem.Text = " " Then
            Llenar_conocimiento()
        End If
        llenarcomboConductor()
        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
            verificar_Produc_convenio()
        Else
            verificar_Produc()
        End If
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
                    unidad,
                    lote_registrado
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
                    @unidad,
                    @lote_registrado
                    )"

            Using cmd As New MySqlCommand(query, connection)
                CrearIdentificador()
                cmd.Parameters.AddWithValue("@variedad_categoria", txtRespaldito.Text)
                cmd.Parameters.AddWithValue("@observaciones", txtObser.Text)
                cmd.Parameters.AddWithValue("@unidad", txtUnid.Text)
                cmd.Parameters.AddWithValue("@categoria_origen", TxtCateogiraGrid.SelectedItem.Text)

                If DDLCultivo.SelectedItem.Text <> " " Then
                    cmd.Parameters.AddWithValue("@tipo_cultivo", DDLCultivo.SelectedItem.Text)
                ElseIf txtCultiConv.SelectedItem.Text <> " " Then
                    cmd.Parameters.AddWithValue("@tipo_cultivo", txtCultiConv.SelectedItem.Text)
                End If

                If DDLCultivo.SelectedItem.Text = "Frijol" Or DDLCultivo.SelectedItem.Text = "Sorgo" Or DDLCultivo.SelectedItem.Text = "Arroz" Or DDLCultivo.SelectedItem.Text = "Ajonjoli" Or DDLCultivo.SelectedItem.Text = "Papa" Then
                    cmd.Parameters.AddWithValue("@variedad", DropDownList5.SelectedItem.Text)
                ElseIf txtCultiConv.SelectedItem.Text = "Frijol" Or txtCultiConv.SelectedItem.Text = "Sorgo" Or txtCultiConv.SelectedItem.Text = "Arroz" Or txtCultiConv.SelectedItem.Text = "Ajonjoli" Or txtCultiConv.SelectedItem.Text = "Papa" Then
                    cmd.Parameters.AddWithValue("@variedad", DropDownList5.SelectedItem.Text)
                End If

                If DDLCultivo.SelectedItem.Text = "Maiz" Then
                    cmd.Parameters.AddWithValue("@variedad", DropDownList6.SelectedItem.Text)
                ElseIf txtCultiConv.SelectedItem.Text = "Maiz" Then
                    cmd.Parameters.AddWithValue("@variedad", DropDownList6.SelectedItem.Text)
                End If

                cmd.Parameters.AddWithValue("@peso_neto", Convert.ToDecimal(txtEntreg.Text))
                cmd.Parameters.AddWithValue("@precio_uni", Convert.ToDecimal(txtPrecio.Text))
                cmd.Parameters.AddWithValue("@total", Convert.ToDecimal(Convert.ToDecimal(txtEntreg.Text) * Convert.ToDecimal(txtPrecio.Text)))
                cmd.Parameters.AddWithValue("@no_conocimiento", txtConoNo.Text)
                cmd.Parameters.AddWithValue("@lote_registrado", Ddl_nolote.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@estado", "0")

                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
        llenaMinigrid()
        btnRegresar.Visible = False
        btnRegresarConEmbarque.Visible = True
        vaciarCamposProductos()
        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
            verificar_Produc_convenio()
        Else
            verificar_Produc()
        End If
        VerificarTextBox()
    End Sub
    Protected Sub vaciarCamposProductos()
        DropDownList5.SelectedIndex = 0
        DropDownList6.SelectedIndex = 0
        TxtCateogiraGrid.SelectedIndex = 0
        Ddl_nolote.SelectedIndex = 0
        txtUnid.Text = "QQ"
        txtEntreg.Text = ""
        txtPrecio.Text = "0"
        txtObser.Text = "Ninguno"
    End Sub
    Protected Sub CrearIdentificador()
        Dim variedad As String

        If DDLCultivo.SelectedItem.Text = "Frijol" Or DDLCultivo.SelectedItem.Text = "Sorgo" Or DDLCultivo.SelectedItem.Text = "Arroz" Or DDLCultivo.SelectedItem.Text = "Ajonjoli" Or DDLCultivo.SelectedItem.Text = "Papa" Or
            txtCultiConv.SelectedItem.Text = "Frijol" Or txtCultiConv.SelectedItem.Text = "Sorgo" Or txtCultiConv.SelectedItem.Text = "Arroz" Or txtCultiConv.SelectedItem.Text = "Ajonjoli" Or txtCultiConv.SelectedItem.Text = "Papa" Then
            variedad = DropDownList5.SelectedItem.Text
        ElseIf DDLCultivo.SelectedItem.Text = "Maiz" Or txtCultiConv.SelectedItem.Text = "Maiz" Then
            variedad = DropDownList6.SelectedItem.Text
        End If

        Dim categoria As String = TxtCateogiraGrid.SelectedItem.Text

        Dim resultado As String = String.Format("{0}-{1}", variedad, categoria)

        txtRespaldito.Text = resultado
    End Sub
    Private Sub Llenar_conocimiento()
        Dim strCombo As String = "SELECT COUNT(no_conocimiento) AS no_conocimiento FROM sag_embarque_info"
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        Label7.Text = "Conocimiento No.:"
        If DtCombo.Rows.Count > 0 AndAlso DtCombo.Columns.Count > 0 Then
            Dim total As Integer = DtCombo.Rows(0)("no_conocimiento")
            total += 1

            Dim year As String = DateTime.Now.Year.ToString()

            Dim resultadoFormateado As String
            If total > 999 Then
                resultadoFormateado = total.ToString("D4") & " - " & year
            Else
                resultadoFormateado = total.ToString("D3") & " - " & year
            End If
            txtConoNo.Text = resultadoFormateado
        Else
            Dim total1 As Integer = 1
            txtConoNo.Text = total1.ToString("D3") & " - " & DateTime.Now.Year.ToString()
        End If
    End Sub
    Private Sub Llenar_convenio()
        Dim strCombo As String = "SELECT COUNT(no_convenio) AS no_convenio FROM sag_embarque_info"
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        Label7.Text = "Convenio- PNS:"
        If DtCombo.Rows.Count > 0 AndAlso DtCombo.Columns.Count > 0 Then
            Dim total As Integer = CInt(DtCombo.Rows(0)("no_convenio")) + 1

            ' Obtener el mes actual en formato de tres dígitos
            Dim mesActual As String = DateTime.Now.Month.ToString("D3")

            Dim year As String = DateTime.Now.Year.ToString()

            ' Formatear el número de convenio
            Dim resultadoFormateado As String = total.ToString("D3") & "-" & mesActual & "-" & year
            txtConoNo.Text = resultadoFormateado
        Else
            ' Si no hay ningún convenio en la base de datos, comenzar desde 001
            Dim total1 As Integer = 1
            Dim mesActual As String = DateTime.Now.Month.ToString("D3")
            txtConoNo.Text = total1.ToString("D3") & "-" & DateTime.Now.Month.ToString("D3") & "-" & DateTime.Now.Year.ToString()
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
            txtsalida.Text = HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString

            If txtsalida.Text = "Convenio" Then
                txtConoNo.Text = ""
                txtConoNo.Text = HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(2).Text).ToString
                ModalTitle3.InnerText = "Información del Embarque " & HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(2).Text).ToString
                Dim cadena As String = "*"

                Me.SqlDataSource3.SelectCommand = "SELECT " & cadena & " FROM `sag_embarque` WHERE no_conocimiento = '" & txtConoNo.Text & "'"

                GridDetalles.DataBind()

                ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal3').modal('show'); });", True)

            Else
                txtConoNo.Text = ""
                txtConoNo.Text = HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(3).Text).ToString
                ModalTitle3.InnerText = "Información del Embarque " & HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(3).Text).ToString
                Dim cadena As String = "*"

                Me.SqlDataSource3.SelectCommand = "SELECT " & cadena & " FROM `sag_embarque` WHERE no_conocimiento = '" & txtConoNo.Text & "'"

                GridDetalles.DataBind()

                ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal3').modal('show'); });", True)
            End If

        End If

        If (e.CommandName = "Editar") Then
            llenarcomboConductor()
            btnGuardarLote.Text = "Actualizar"

            DivCrearNuevo.Visible = True
            DivGrid.Visible = False

            btnRegresar.Visible = True
            btnRegresarConEmbarque.Visible = False
            ddl_tiposalida.Enabled = False
            'TextBanderita.Text = "Guardar"

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            txtID.Text = ""
            txtID.Text = HttpUtility.HtmlDecode(GridDatos.Rows(index).Cells(0).Text).ToString
            Dim Str As String = "SELECT * FROM vista_embarque_general WHERE  ID_EMBARQUE_INFO = " & txtID.Text & ""
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)
            If dt.Rows(0)("tipo_salida").ToString() = "Convenio" Then

                divconvenio.Visible = True
                idcultivo.Visible = False
                idpara.Visible = False
                divInfoEnvio.Visible = False
                divInfoConduc.Visible = False
                divInfoObser.Visible = False
                divPrecio.Visible = False
                DDLCultivo.SelectedIndex = 0
                txtCultiConv.Enabled = False

                SeleccionarItemEnDropDownList(ddl_tiposalida, dt.Rows(0)("tipo_salida").ToString())
                txtConoNo.Text = dt.Rows(0)("no_convenio").ToString()
                txtFecha.Text = If(dt.Rows(0)("FECHA_ELABORACION") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("FECHA_ELABORACION"), DateTime).ToString("yyyy-MM-dd"))
                txtFecha2.Text = If(dt.Rows(0)("fecha_final_convenio") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_final_convenio"), DateTime).ToString("yyyy-MM-dd"))
                txtParaConv.Text = dt.Rows(0)("PARA_GENERAL").ToString()
                txtParaIdent.Text = dt.Rows(0)("identidad").ToString()
                txtMzSembrar.Text = dt.Rows(0)("mz_sembrar_qq").ToString()
                SeleccionarItemEnDropDownList(txtCultiConv, dt.Rows(0)("CULTIVO_GENERAL").ToString())
                If txtCultiConv.SelectedItem.Text = "Frijol" Or txtCultiConv.SelectedItem.Text = "Sorgo" Or txtCultiConv.SelectedItem.Text = "Arroz" Or txtCultiConv.SelectedItem.Text = "Ajonjoli" Or txtCultiConv.SelectedItem.Text = "Papa" Then
                    DropDownList6.SelectedIndex = 0
                    VariedadFrijol.Visible = True
                    VariedadMaiz.Visible = False
                    llenarcomboFrijol()
                Else
                    VariedadMaiz.Visible = True
                    VariedadFrijol.Visible = False
                    DropDownList5.SelectedIndex = 0
                    llenarcomboMaiz()
                End If
                llenarcombovariedad()
                SeleccionarItemEnDropDownList(txtVariedadConv, dt.Rows(0)("variedad_conve").ToString())
                llenarcomboCategoria()
                SeleccionarItemEnDropDownList(txtCategConv, dt.Rows(0)("categoria_conve").ToString())
                txtProducAprox.Text = dt.Rows(0)("produ_apro_qq_mz").ToString()
                txtPrecioMinimoCompra.Text = dt.Rows(0)("precio_minimo").ToString()
                txtCompPerd.Text = dt.Rows(0)("compensacion").ToString()
                txtPrecioFinal.Text = dt.Rows(0)("precio_final").ToString()
                VerificarTextBox()
            ElseIf ddl_tiposalida.SelectedItem.Text = "Actas" Then
                divconvenio.Visible = False
                idcultivo.Visible = True
                idpara.Visible = True
                divInfoEnvio.Visible = True
                divInfoConduc.Visible = True
                divInfoObser.Visible = True
                txtFecha2.Text = ""
                divPrecio.Visible = False
                txtCultiConv.SelectedIndex = 0

                SeleccionarItemEnDropDownList(ddl_tiposalida, dt.Rows(0)("tipo_salida").ToString())
                txtConoNo.Text = dt.Rows(0)("NO_CONOCIMIENTO_EMBARQUE_INFO").ToString()
                txtPara.Text = dt.Rows(0)("PARA_GENERAL").ToString()
                txtFecha.Text = If(dt.Rows(0)("FECHA_ELABORACION") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("FECHA_ELABORACION"), DateTime).ToString("yyyy-MM-dd"))
                SeleccionarItemEnDropDownList(DDLCultivo, dt.Rows(0)("CULTIVO_GENERAL").ToString())
                DDLCultivo.Enabled = False
                If dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Frijol" Or dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Arroz" Or dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Ajonjoli" Or dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Papa" Then
                    VariedadFrijol.Visible = True
                End If
                If dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Maiz" Then
                    VariedadMaiz.Visible = True
                End If
                If DDLCultivo.SelectedItem.Text = "Frijol" Or DDLCultivo.SelectedItem.Text = "Sorgo" Or DDLCultivo.SelectedItem.Text = "Arroz" Or DDLCultivo.SelectedItem.Text = "Ajonjoli" Or DDLCultivo.SelectedItem.Text = "Papa" Then
                    DropDownList6.SelectedIndex = 0
                    VariedadFrijol.Visible = True
                    VariedadMaiz.Visible = False
                    llenarcomboFrijol()
                Else
                    VariedadMaiz.Visible = True
                    VariedadFrijol.Visible = False
                    DropDownList5.SelectedIndex = 0
                    llenarcomboMaiz()
                End If
                txtRemi.Text = dt.Rows(0)("REMITENTE").ToString()
                txtDestin.Text = dt.Rows(0)("DESTINATARIO").ToString()
                txtLugarR.Text = dt.Rows(0)("LUGAR_REMITENTE").ToString()
                txtLugarD.Text = dt.Rows(0)("LUGAR_DESTINATARIO").ToString()
                SeleccionarItemEnDropDownList(DDLConductor, dt.Rows(0)("CONDUCTOR").ToString())
                txtVehic.Text = dt.Rows(0)("VEHICULO").ToString()
                VerificarTextBox()
            Else
                divconvenio.Visible = False
                idcultivo.Visible = True
                idpara.Visible = True
                divInfoEnvio.Visible = True
                divInfoConduc.Visible = True
                divInfoObser.Visible = True
                txtFecha2.Text = ""
                divPrecio.Visible = True
                txtCultiConv.SelectedIndex = 0

                SeleccionarItemEnDropDownList(ddl_tiposalida, dt.Rows(0)("tipo_salida").ToString())
                txtConoNo.Text = dt.Rows(0)("NO_CONOCIMIENTO_EMBARQUE_INFO").ToString()
                txtPara.Text = dt.Rows(0)("PARA_GENERAL").ToString()
                txtFecha.Text = If(dt.Rows(0)("FECHA_ELABORACION") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("FECHA_ELABORACION"), DateTime).ToString("yyyy-MM-dd"))
                SeleccionarItemEnDropDownList(DDLCultivo, dt.Rows(0)("CULTIVO_GENERAL").ToString())
                DDLCultivo.Enabled = False
                If dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Frijol" Or dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Arroz" Or dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Ajonjoli" Or dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Papa" Then
                    VariedadFrijol.Visible = True
                End If
                If dt.Rows(0)("CULTIVO_GENERAL").ToString() = "Maiz" Then
                    VariedadMaiz.Visible = True
                End If
                If DDLCultivo.SelectedItem.Text = "Frijol" Or DDLCultivo.SelectedItem.Text = "Sorgo" Or DDLCultivo.SelectedItem.Text = "Arroz" Or DDLCultivo.SelectedItem.Text = "Ajonjoli" Or DDLCultivo.SelectedItem.Text = "Papa" Then
                    DropDownList6.SelectedIndex = 0
                    VariedadFrijol.Visible = True
                    VariedadMaiz.Visible = False
                    llenarcomboFrijol()
                Else
                    VariedadMaiz.Visible = True
                    VariedadFrijol.Visible = False
                    DropDownList5.SelectedIndex = 0
                    llenarcomboMaiz()
                End If
                txtRemi.Text = dt.Rows(0)("REMITENTE").ToString()
                txtDestin.Text = dt.Rows(0)("DESTINATARIO").ToString()
                txtLugarR.Text = dt.Rows(0)("LUGAR_REMITENTE").ToString()
                txtLugarD.Text = dt.Rows(0)("LUGAR_DESTINATARIO").ToString()
                SeleccionarItemEnDropDownList(DDLConductor, dt.Rows(0)("CONDUCTOR").ToString())
                txtVehic.Text = dt.Rows(0)("VEHICULO").ToString()
                VerificarTextBox()
            End If

            llenaMinigrid()
            If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
                verificar_Produc_convenio()
            Else
                verificar_Produc()
            End If
            VerificarTextBox()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            txtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            txtsalida.Text = HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString

            If txtsalida.Text = "Convenio" Then
                TextminigridCambiarestado.Text = HttpUtility.HtmlDecode(gvrow.Cells(2).Text).ToString
            Else
                TextminigridCambiarestado.Text = HttpUtility.HtmlDecode(gvrow.Cells(3).Text).ToString
            End If

            Label3.Text = "¿Desea eliminar el conocimiento de embarque?"
            BBorrarsi.Visible = True
            BBorrarno.Visible = True
            BConfirm.Visible = False
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If

        If (e.CommandName = "Imprimir") Then

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            txtsalida.Text = HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString

            If txtsalida.Text = "Convenio" Then
                Dim var As String = HttpUtility.HtmlDecode(gvrow.Cells(2).Text).ToString
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

                adap.Fill(ds, "sag_embarque_info")

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
            Else
                Dim rptdocument As New ReportDocument
                'nombre de dataset
                Dim ds As New DataSetMultiplicador
                Dim Str As String = "SELECT * FROM vista_embarque_informe WHERE NO_CONOCIMIENTO_EMBARQUE_INFO = @valor"
                Dim adap As New MySqlDataAdapter(Str, conn)
                adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(3).Text).ToString)
                Dim dt As New DataTable

                'nombre de la vista del data set

                adap.Fill(ds, "vista_embarque_informe")

                Dim nombre As String

                nombre = "Conocimiento de Embarque No " + HttpUtility.HtmlDecode(gvrow.Cells(3).Text).ToString + " " + Today

                rptdocument.Load(Server.MapPath("~/pages/EmbarqueReport.rpt"))

                rptdocument.SetDataSource(ds)
                Response.Buffer = False

                Response.ClearContent()
                Response.ClearHeaders()

                rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

                Response.End()
                ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)
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
    Protected Function SeleccionarItemEnDropDownListFrijolOMaiz(ByVal DtCombo As String, ByVal DtCombo2 As String)
        If DtCombo2 = "Frijol" Or DtCombo2 = "Sorgo" Or DtCombo2 = "Ajonjoli" Or DtCombo2 = "Arroz" Or DtCombo2 = "Papa" Then
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

        If (DDLTipoSalida.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND tipo_salida = '" & DDLTipoSalida.SelectedItem.Text & "' "
        End If

        query = "SELECT " & cadena & " FROM `vista_embarque_general` WHERE 1 = 1 " & c1 & c3 & c4 & " AND FECHA_ELABORACION >= '" & txtFechaDesde.Text & "' AND FECHA_ELABORACION <= '" & txtFechaHasta.Text & "'"

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
        If selectedValue = "Frijol" Or
           selectedValue = "Sorgo" Or
           selectedValue = "Arroz" Or
           selectedValue = "Ajonjoli" Or
           selectedValue = "Papa" Then
            DropDownList6.SelectedIndex = 0
            DropDownList5.Visible = True
            VariedadFrijol.Visible = True
            VariedadMaiz.Visible = False
            llenarcomboFrijol()
        ElseIf selectedValue = "Maiz" Then
            VariedadMaiz.Visible = True
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
            DropDownList6.Visible = True
            llenarcomboMaiz()
        Else
            VariedadMaiz.Visible = False
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
            DropDownList6.SelectedIndex = 0
            DropDownList6.Visible = False
            DropDownList5.Visible = False
        End If

        VerificarTextBox()
    End Sub
    Private Sub llenarcomboFrijol()
        Dim StrCombo, cultivo As String

        If DDLCultivo.SelectedItem.Text <> " " Then
            cultivo = DDLCultivo.SelectedItem.Text
        ElseIf txtCultiConv.SelectedItem.Text <> " " Then
            cultivo = txtCultiConv.SelectedItem.Text
        End If
        StrCombo = "SELECT DISTINCT variedad FROM vista_suma_tabla_a WHERE tipo_cultivo = '" & cultivo & "' ORDER BY variedad ASC"

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

        StrCombo = "SELECT DISTINCT variedad FROM vista_suma_tabla_a WHERE tipo_cultivo = 'Maiz' ORDER BY variedad ASC"

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
        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
            verificar_Produc_convenio()
        Else
            verificar_Produc()
        End If
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
            txtPrecio.Text = "0"
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
            If dt.Rows(0)("tipo_cultivo").ToString() = "Frijol" Or dt.Rows(0)("tipo_cultivo").ToString() = "Sorgo" Or dt.Rows(0)("tipo_cultivo").ToString() = "Arroz" Or dt.Rows(0)("tipo_cultivo").ToString() = "Ajonjoli" Or dt.Rows(0)("tipo_cultivo").ToString() = "Papa" Then
                llenarcomboCategoriaFrijol()
            Else
                llenarcomboCategoriaMaiz()
            End If
            SeleccionarItemEnDropDownList(TxtCateogiraGrid, dt.Rows(0)("categoria_origen").ToString())
            SeleccionarItemEnDropDownList(Ddl_nolote, dt.Rows(0)("lote_registrado").ToString())
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
        DropDownList6.SelectedIndex = 0
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
        DropDownList5.SelectedIndex = 0
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
        Dim selectedValue As String
        txtEntreg.Text = 0
        If DDLCultivo.SelectedItem.Text <> " " Then
            selectedValue = DDLCultivo.SelectedItem.Text
        ElseIf txtCultiConv.SelectedItem.Text <> " " Then
            selectedValue = txtCultiConv.SelectedItem.Text
        End If

        ' Si selecciona "Frijol," muestra la TextBox de Variedad; de lo contrario, ocúltala
        If ((selectedValue = "Frijol" Or
           selectedValue = "Sorgo" Or
           selectedValue = "Arroz" Or
           selectedValue = "Ajonjoli" Or
           selectedValue = "Papa") And DropDownList5.SelectedItem.Text <> " ") Then
            llenarcombolote()
            verificardatosproductos()
        ElseIf selectedValue = "Maiz" And DropDownList6.SelectedItem.Text <> " " Then
            llenarcombolotemaiz()
            verificardatosproductos()
        End If
        verificardatosproductos()
    End Sub
    Protected Sub txtEntreg_TextChanged(sender As Object, e As EventArgs) Handles txtEntreg.TextChanged
        ' Obtener el valor ingresado en txtEntreg

        If Integer.TryParse(txtEntreg.Text, entregado) Then
            ' Construir la consulta SQL dinámica
            Dim c1 As String = "SELECT peso_neto_resta FROM vista_inventario2 WHERE 1=1 "
            Dim c2 As String
            Dim c3 As String

            ' Obtener las selecciones de los DropDownList
            If DropDownList5.SelectedItem.Text = " " And DropDownList6.SelectedItem.Text <> " " Then
                c2 = " AND variedad = '" & DropDownList6.SelectedItem.Text & "' "
            Else
                c2 = " "
            End If

            If DropDownList6.SelectedItem.Text = " " And DropDownList5.SelectedItem.Text <> " " Then
                c2 = " AND variedad = '" & DropDownList5.SelectedItem.Text & "' "
            Else
                c2 = " "
            End If

            If (TxtCateogiraGrid.SelectedItem.Text = " ") Then
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
    Private Function verificar_Produc()
        Dim strCombo As String = "SELECT * FROM sag_embarque WHERE no_conocimiento = '" & txtConoNo.Text & "'"
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        Return DtCombo.Rows.Count
    End Function
    Private Function verificar_Produc_convenio()
        Dim strCombo As String = "SELECT * FROM sag_embarque WHERE no_conocimiento = '" & txtConoNo.Text & "'"
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        Return DtCombo.Rows.Count
    End Function
    Sub verificardatosproductos()
        Dim validar As Integer = 0
        If TextBanderita.Text = " " Or TextBanderita.Text = "Distribución y embarque" Or TextBanderita.Text = "Actas" Then
            '1
            If String.IsNullOrEmpty(txtEntreg.Text) Then
                lblLugarD.Text = "*"
                validar = 0
            Else
                lblLugarD.Text = ""
                validar += 1
            End If
            '2
            If String.IsNullOrEmpty(txtPrecio.Text) Then
                lblLugarD.Text = "*"
                validar = 0
            Else
                lblLugarD.Text = ""
                validar += 1
            End If
            '3
            If String.IsNullOrEmpty(txtObser.Text) Then
                lblLugarD.Text = "*"
                validar = 0
            Else
                lblLugarD.Text = ""
                validar += 1
            End If
            '4
            If DDLCultivo.SelectedItem.Text = "Frijol" Or DDLCultivo.SelectedItem.Text = "Sorgo" Or DDLCultivo.SelectedItem.Text = "Arroz" Or DDLCultivo.SelectedItem.Text = "Ajonjoli" Or DDLCultivo.SelectedItem.Text = "Papa" Then
                If DropDownList5.SelectedItem.Text <> " " Then
                    Label1.Text = ""
                    validar += 1

                Else
                    Label1.Text = "*"
                    validar = 0
                End If
            ElseIf DDLCultivo.SelectedItem.Text = "Maiz" Then
                If DropDownList6.SelectedItem.Text <> " " Then
                    Label1.Text = ""
                    validar += 1

                Else
                    Label1.Text = "*"
                    validar = 0
                End If
            End If
            '5
            If TxtCateogiraGrid.SelectedItem.Text <> " " Then
                Label1.Text = ""
                validar += 1

            Else
                Label1.Text = "*"
                validar = 0
            End If
            '6
            If Ddl_nolote.SelectedItem.Text <> " " Then
                Label1.Text = ""
                validar += 1

            Else
                Label1.Text = "*"
                validar = 0
            End If
            If validar = 6 Then
                btnAgregar.Visible = True
            Else
                btnAgregar.Visible = False
            End If
        Else
            '1
            If String.IsNullOrEmpty(txtEntreg.Text) Then
                lblLugarD.Text = "*"
                validar = 0
            Else
                lblLugarD.Text = ""
                validar += 1
            End If
            '2
            If String.IsNullOrEmpty(txtPrecio.Text) Then
                lblLugarD.Text = "*"
                validar = 0
            Else
                lblLugarD.Text = ""
                validar += 1
            End If
            '3
            If String.IsNullOrEmpty(txtObser.Text) Then
                lblLugarD.Text = "*"
                validar = 0
            Else
                lblLugarD.Text = ""
                validar += 1
            End If
            '4
            If txtCultiConv.SelectedItem.Text = "Frijol" Or txtCultiConv.SelectedItem.Text = "Sorgo" Or txtCultiConv.SelectedItem.Text = "Arroz" Or txtCultiConv.SelectedItem.Text = "Ajonjoli" Or txtCultiConv.SelectedItem.Text = "Papa" Then
                If DropDownList5.SelectedItem.Text <> " " Then
                    Label1.Text = ""
                    validar += 1

                Else
                    Label1.Text = "*"
                    validar = 0
                End If
            ElseIf txtCultiConv.SelectedItem.Text = "Maiz" Then
                If DropDownList6.SelectedItem.Text <> " " Then
                    Label1.Text = ""
                    validar += 1

                Else
                    Label1.Text = "*"
                    validar = 0
                End If
            End If
            '5
            If TxtCateogiraGrid.SelectedItem.Text <> " " Then
                Label1.Text = ""
                validar += 1

            Else
                Label1.Text = "*"
                validar = 0
            End If
            '6
            If Ddl_nolote.SelectedItem.Text <> " " Then
                Label1.Text = ""
                validar += 1

            Else
                Label1.Text = "*"
                validar = 0
            End If
            If validar = 6 Then
                If verificar_Produc_convenio() >= 1 Then
                    btnAgregar.Visible = False
                ElseIf verificar_Produc_convenio() = 0 Then
                    btnAgregar.Visible = True
                End If
            Else
                btnAgregar.Visible = False
            End If
        End If
    End Sub
    Protected Sub txtFecha2_TextChanged()
        ' Obtener la fecha seleccionada en el primer TextBox

        Dim fechaSeleccionada As Date = DateTime.Parse(txtFecha.Text)

        ' Sumar 129 días a la fecha seleccionada
        Dim fechaCalculada As Date = fechaSeleccionada.AddDays(129)

        ' Establecer el valor del segundo TextBox con la fecha calculada
        txtFecha2.Text = fechaCalculada.ToString("yyyy-MM-dd")
    End Sub
    Protected Sub txtFecha_TextChanged(sender As Object, e As EventArgs) Handles txtFecha.TextChanged
        If ddl_tiposalida.SelectedItem.Text = "Convenio" And txtFecha.Text <> "" Then
            txtFecha2_TextChanged()
        Else
            txtFecha2.Text = ""
        End If
    End Sub
    Protected Sub ddl_tiposalida_TextChanged(sender As Object, e As EventArgs) Handles ddl_tiposalida.SelectedIndexChanged
        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
            divconvenio.Visible = True
            idcultivo.Visible = False
            idpara.Visible = False
            divInfoEnvio.Visible = False
            divInfoConduc.Visible = False
            divInfoObser.Visible = False
            txtFecha.Text = ""
            divPrecio.Visible = False
            txtPrecio.Text = "0"
            DDLCultivo.SelectedIndex = 0
            Llenar_convenio()
            vaciarCamposSalida()
        ElseIf ddl_tiposalida.SelectedItem.Text = "Actas" Then
            divconvenio.Visible = False
            idcultivo.Visible = True
            idpara.Visible = True
            divInfoEnvio.Visible = True
            divInfoConduc.Visible = True
            divInfoObser.Visible = True
            txtFecha2.Text = ""
            txtFecha.Text = ""
            divPrecio.Visible = False
            txtPrecio.Text = "0"
            txtCultiConv.SelectedIndex = 0
            Llenar_conocimiento()
            vaciarCamposSalida()
        Else
            divconvenio.Visible = False
            idcultivo.Visible = True
            idpara.Visible = True
            divInfoEnvio.Visible = True
            divInfoConduc.Visible = True
            divInfoObser.Visible = True
            txtFecha2.Text = ""
            txtFecha.Text = ""
            divPrecio.Visible = True
            txtPrecio.Text = "0"
            txtCultiConv.SelectedIndex = 0
            Llenar_conocimiento()
            vaciarCamposSalida()
        End If
    End Sub
    Private Sub llenarcomboCultivo()
        Dim StrCombo As String = "SELECT DISTINCT tipo_cultivo FROM vista_inventario"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        DDLCultivo.DataSource = DtCombo
        DDLCultivo.DataValueField = DtCombo.Columns(0).ToString()
        DDLCultivo.DataTextField = DtCombo.Columns(0).ToString
        DDLCultivo.DataBind()
        Dim newitem As New ListItem(" ", " ")
        DDLCultivo.Items.Insert(0, newitem)
    End Sub

    Private Sub llenarcomboCultivo2()
        Dim StrCombo As String = "SELECT DISTINCT tipo_cultivo FROM vista_inventario"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        txtCultiConv.DataSource = DtCombo
        txtCultiConv.DataValueField = DtCombo.Columns(0).ToString()
        txtCultiConv.DataTextField = DtCombo.Columns(0).ToString
        txtCultiConv.DataBind()
        Dim newitem As New ListItem(" ", " ")
        txtCultiConv.Items.Insert(0, newitem)
    End Sub
    Protected Sub llenarcombolote()
        Dim variedad, categoria, tipo As String

        If DDLCultivo.SelectedItem.Text <> " " Then
            tipo = DDLCultivo.SelectedItem.Text
        ElseIf txtCultiConv.SelectedItem.Text <> " " Then
            tipo = txtCultiConv.SelectedItem.Text
        End If
        variedad = DropDownList5.SelectedItem.Text
        categoria = TxtCateogiraGrid.SelectedItem.Text
        Dim StrCombo As String = "SELECT DISTINCT lote_registrado FROM vista_inventario WHERE tipo_cultivo = '" & tipo & "' AND variedad = '" & variedad & "' AND categoria_registrado = '" & categoria & "'"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        Ddl_nolote.DataSource = DtCombo
        Ddl_nolote.DataValueField = DtCombo.Columns(0).ToString()
        Ddl_nolote.DataTextField = DtCombo.Columns(0).ToString
        Ddl_nolote.DataBind()
        Dim newitem As New ListItem(" ", " ")
        Ddl_nolote.Items.Insert(0, newitem)
    End Sub
    Protected Sub llenarcombolotemaiz()
        Dim variedad, categoria As String

        variedad = DropDownList6.SelectedItem.Text
        categoria = TxtCateogiraGrid.SelectedItem.Text

        Dim StrCombo As String = "SELECT DISTINCT lote_registrado FROM vista_inventario WHERE tipo_cultivo = 'Maiz' AND variedad = '" & variedad & "' AND categoria_registrado = '" & categoria & "'"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        Ddl_nolote.DataSource = DtCombo
        Ddl_nolote.DataValueField = DtCombo.Columns(0).ToString()
        Ddl_nolote.DataTextField = DtCombo.Columns(0).ToString
        Ddl_nolote.DataBind()
        Dim newitem As New ListItem(" ", " ")
        Ddl_nolote.Items.Insert(0, newitem)
    End Sub
    Protected Sub Ddl_nolote_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Ddl_nolote.SelectedIndexChanged
        ' Obtener el valor ingresado en txtEntreg
        Dim entregado As Integer = 0
        ' Construir la consulta SQL dinámica
        Dim c1 As String = "SELECT peso_neto_resta FROM vista_inventario2 WHERE 1=1 "
        Dim c2 As String
        Dim c3 As String
        Dim c4 As String

        ' Obtener las selecciones de los DropDownList
        If DropDownList5.SelectedItem.Text = " " And DropDownList6.SelectedItem.Text <> " " Then
            c2 = " AND variedad = '" & DropDownList6.SelectedItem.Text & "' "
        Else
            c2 = " "
        End If

        If DropDownList6.SelectedItem.Text = " " And DropDownList5.SelectedItem.Text <> " " Then
            c2 = " AND variedad = '" & DropDownList5.SelectedItem.Text & "' "
        Else
            c2 = " "
        End If

        If (TxtCateogiraGrid.SelectedItem.Text = " ") Then
            c3 = " "
        Else
            c3 = " AND categoria_registrado = '" & TxtCateogiraGrid.SelectedItem.Text & "' "
        End If

        If (Ddl_nolote.SelectedItem.Text = " ") Then
            c3 = " "
        Else
            c3 = " AND lote_registrado = '" & Ddl_nolote.SelectedItem.Text & "' "
        End If

        ' Agregar condiciones a la consulta SQL
        Dim query As String = c1 & c2 & c3 & c4

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
    Private Sub llenarcombovariedad()
        Dim StrCombo As String
        Dim cultivo = txtCultiConv.SelectedItem.Text
        StrCombo = "SELECT DISTINCT variedad FROM vista_suma_tabla_a WHERE tipo_cultivo = '" & cultivo & "' ORDER BY variedad ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        txtVariedadConv.DataSource = DtCombo
        txtVariedadConv.DataValueField = DtCombo.Columns(0).ToString()
        txtVariedadConv.DataTextField = DtCombo.Columns(0).ToString()
        txtVariedadConv.DataBind()
        Dim newitem As New ListItem(" ", " ")
        txtVariedadConv.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboCategoria()
        Dim StrCombo As String
        Dim variedad = txtVariedadConv.SelectedItem.Text
        StrCombo = "SELECT DISTINCT categoria_registrado FROM vista_suma_tabla_a WHERE variedad = '" & variedad & "' ORDER BY categoria_registrado ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        txtCategConv.DataSource = DtCombo
        txtCategConv.DataValueField = DtCombo.Columns(0).ToString()
        txtCategConv.DataTextField = DtCombo.Columns(0).ToString()
        txtCategConv.DataBind()
        Dim newitem As New ListItem(" ", " ")
        txtCategConv.Items.Insert(0, newitem)
    End Sub
    Protected Sub txtCultiConv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtCultiConv.SelectedIndexChanged
        If txtCultiConv.SelectedItem.Text <> " " Then
            llenarcombovariedad()
            verificardatosproductos()
            VerificarTextBox()
        Else
            verificardatosproductos()
            VerificarTextBox()
        End If

        Dim selectedValue As String = txtCultiConv.SelectedItem.Text

        ' Si selecciona "Frijol," muestra la TextBox de Variedad; de lo contrario, ocúltala
        If selectedValue = "Frijol" Or
           selectedValue = "Sorgo" Or
           selectedValue = "Arroz" Or
           selectedValue = "Ajonjoli" Or
           selectedValue = "Papa" Then
            DropDownList6.SelectedIndex = 0
            DropDownList5.Visible = True
            VariedadFrijol.Visible = True
            VariedadMaiz.Visible = False
            llenarcomboFrijol()
        ElseIf selectedValue = "Maiz" Then
            VariedadMaiz.Visible = True
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
            DropDownList6.Visible = True
            llenarcomboMaiz()
        Else
            VariedadMaiz.Visible = False
            VariedadFrijol.Visible = False
            DropDownList5.SelectedIndex = 0
            DropDownList6.SelectedIndex = 0
            DropDownList6.Visible = False
            DropDownList5.Visible = False
        End If

        VerificarTextBox()
    End Sub
    Protected Sub txtVariedadConv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtVariedadConv.SelectedIndexChanged
        If txtVariedadConv.SelectedItem.Text <> " " Then
            llenarcomboCategoria()
            verificardatosproductos()
            VerificarTextBox()
        Else
            verificardatosproductos()
            VerificarTextBox()
        End If
    End Sub
    Protected Sub sumaprecioFinal()
        Dim preciomin, compensacion, preciofinal As Decimal


        If Decimal.TryParse(txtPrecioMinimoCompra.Text, preciomin) Then
            preciomin = txtPrecioMinimoCompra.Text
        End If
        If Decimal.TryParse(txtCompPerd.Text, compensacion) Then
            compensacion = txtCompPerd.Text
        End If
        preciofinal = preciomin + compensacion
        txtPrecioFinal.Text = preciofinal.ToString
    End Sub
    Protected Sub txtPrecioMinimoCompra_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtPrecioMinimoCompra.TextChanged
        If txtPrecioMinimoCompra.Text <> " " Then
            sumaprecioFinal()
            verificardatosproductos()
            VerificarTextBox()
        Else
            verificardatosproductos()
            VerificarTextBox()
        End If
    End Sub

    Protected Sub txtCompPerd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtCompPerd.TextChanged
        If txtCompPerd.Text <> " " Then
            sumaprecioFinal()
            verificardatosproductos()
            VerificarTextBox()
        Else
            verificardatosproductos()
            VerificarTextBox()
        End If
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

    Protected Sub vaciarCamposSalida()
        If ddl_tiposalida.SelectedItem.Text = "Convenio" Then
            '1
            txtParaConv.Text = ""
            '2
            txtCompPerd.Text = ""
            '3
            txtCultiConv.SelectedIndex = 0
            '4
            txtParaIdent.Text = ""
            '5
            txtMzSembrar.Text = ""
            '6
            txtVariedadConv.SelectedIndex = 0
            '7
            txtCategConv.SelectedIndex = 0
            '8
            txtProducAprox.Text = ""
            '9
            txtPrecioMinimoCompra.Text = ""
            '10
            txtFecha.Text = ""
            '11
            txtFecha2.Text = ""
            '12
            txtPrecioFinal.Text = ""
            vaciarCamposProductos()
        Else
            '1
            txtPara.Text = ""
            '2
            txtFecha.Text = ""
            '3
            DDLCultivo.SelectedIndex = 0
            '4
            txtRemi.Text = ""
            '5
            txtDestin.Text = ""
            '6
            txtLugarR.Text = ""
            '7
            txtLugarD.Text = ""
            '8
            DDLConductor.SelectedIndex = 0
            '9
            txtObser2.Text = ""
            '10
            txtVehic.Text = ""
            vaciarCamposProductos()
        End If
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
