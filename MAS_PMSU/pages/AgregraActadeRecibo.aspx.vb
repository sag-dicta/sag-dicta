Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports ClosedXML.Excel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.[Shared].Json
Imports DocumentFormat.OpenXml.Office.Word
Imports MySql.Data.MySqlClient

Public Class AgregraActadeRecibo
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connSAG").ConnectionString
    Dim sentencia As String
    Dim validarflag As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If User.Identity.IsAuthenticated = True Then
            If IsPostBack Then

            Else
                txtFechaSiembra.Text = DateTime.Now.ToString("yyyy-MM-dd")
                llenarcomboProductor()
                llenarcomboDepto()
                llenarcomboCiclogrid()
                llenagrid()
            End If
        End If
    End Sub
    Protected Sub vaciar(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/AgregraActadeRecibo.aspx"))
    End Sub
    Private Sub llenarcomboDepto()
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
    Private Sub llenarcomboCiclo()
        Dim StrCombo As String = "SELECT * FROM sag_ciclo"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        DDL_Ciclo.DataSource = DtCombo
        DDL_Ciclo.DataValueField = DtCombo.Columns(0).ToString()
        DDL_Ciclo.DataTextField = DtCombo.Columns(1).ToString
        DDL_Ciclo.DataBind()
        Dim newitem As New ListItem(" ", " ")
        DDL_Ciclo.Items.Insert(0, newitem)
    End Sub

    Private Sub llenarcomboCiclogrid()
        Dim StrCombo As String = "SELECT * FROM sag_ciclo"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        txtciclo.DataSource = DtCombo
        txtciclo.DataValueField = DtCombo.Columns(0).ToString()
        txtciclo.DataTextField = DtCombo.Columns(1).ToString
        txtciclo.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        txtciclo.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboProductor()
        Dim StrCombo As String = "SELECT DISTINCT nombre_multiplicador FROM `sag_registro_multiplicador` WHERE 1 = 1 AND estado = '1' ORDER BY nombre_multiplicador ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        TxtProductorGrid.DataSource = DtCombo
        TxtProductorGrid.DataValueField = DtCombo.Columns(0).ToString()
        TxtProductorGrid.DataTextField = DtCombo.Columns(0).ToString
        TxtProductorGrid.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtProductorGrid.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboProductor2()
        Dim StrCombo As String = "SELECT DISTINCT nombre_multiplicador FROM `sag_registro_multiplicador` WHERE 1 = 1 AND estado = '1' AND departamento = '" & TxtDepto.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        TxtProductorGrid.DataSource = DtCombo
        TxtProductorGrid.DataValueField = DtCombo.Columns(0).ToString()
        TxtProductorGrid.DataTextField = DtCombo.Columns(0).ToString
        TxtProductorGrid.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtProductorGrid.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboLote()
        If TxtProductorGrid.SelectedItem.Text <> "Todos" Then
            Dim StrCombo As String = "SELECT DISTINCT no_lote FROM `sag_registro_lote` WHERE productor = '" & TxtProductorGrid.SelectedItem.Text & "' AND estado = '1' "
            Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
            Dim DtCombo As New DataTable
            adaptcombo.Fill(DtCombo)

            DDL_SelCLote.DataSource = DtCombo
            DDL_SelCLote.DataValueField = DtCombo.Columns(0).ToString()
            DDL_SelCLote.DataTextField = DtCombo.Columns(0).ToString
            DDL_SelCLote.DataBind()
            Dim newitem As New ListItem("Todos", "Todos")
            DDL_SelCLote.Items.Insert(0, newitem)
        Else
            DDL_SelCLote.Items.Clear()
            Dim newitem As New ListItem("Todos", "Todos")
            DDL_SelCLote.Items.Insert(0, newitem)
        End If
    End Sub
    Sub llenagrid()
        Dim cadena As String = "*"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c2 As String = ""
        Dim c4 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND  NOMBRE_MULTIPLICADOR = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c2 = " "
        Else
            c2 = "AND  departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        If (DDL_SelCLote.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND no_lote = '" & DDL_SelCLote.SelectedItem.Text & "' "
        End If

        If (txtciclo.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND ciclo_acta = '" & txtciclo.SelectedItem.Text & "' "
        End If

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND ciclo_acta IS NOT NULL " & c1 & c3 & c2 & c4

        GridDatos.DataBind()
    End Sub
    Protected Sub TxtProductorGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtProductorGrid.SelectedIndexChanged
        llenagrid()
        llenarcomboLote()

        If TxtProductorGrid.SelectedItem.Text = "Todos" Then
            TxtDepto.SelectedIndex = 0
            DDL_SelCLote.SelectedIndex = 0
            TxtProductorGrid.SelectedIndex = 0
            txtciclo.SelectedIndex = 0
            BAgregar.Visible = False
        End If
    End Sub
    Protected Sub TxtDepto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TxtDepto.SelectedIndexChanged
        If TxtDepto.SelectedItem.Text = "Todos" Then
            llenarcomboProductor()
            llenarcomboLote()
            BAgregar.Visible = False
        Else
            llenarcomboProductor2()
            llenarcomboLote()
            BAgregar.Visible = False
        End If
        llenagrid()
    End Sub

    Protected Sub txtciclo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtciclo.SelectedIndexChanged
        llenagrid()
    End Sub

    Protected Sub DDL_SelCLote_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_SelCLote.SelectedIndexChanged
        llenagrid()

        If TxtProductorGrid.SelectedItem.Text <> "Todos" Then
            BAgregar.Visible = True
        Else
            BAgregar.Visible = False
        End If

        If DDL_SelCLote.SelectedItem.Text = "Todos" Then
            TxtDepto.SelectedIndex = 0
            DDL_SelCLote.SelectedIndex = 0
            TxtProductorGrid.SelectedIndex = 0
            txtciclo.SelectedIndex = 0
            BAgregar.Visible = False
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
    Protected Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        lblTotalClientes.Text = e.AffectedRows.ToString()

    End Sub
    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand
        'Dim fecha2 As Date

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        If (e.CommandName = "Editar") Then
            DivGrid.Visible = False
            DivActa.Visible = True
            btnGuardarActa.Text = "Actualizar"
            btnGuardarActa.Visible = True
            BtnNuevo.Visible = True


            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "fecha_acta, nombre_multiplicador, departamento, municipio, aldea, caserio, no_lote, tipo_cultivo, variedad, categoria_origen, porcentaje_humedad, no_sacos, peso_humedo_QQ, ciclo_acta"
            Dim Str As String = "SELECT " & cadena & " FROM vista_acta_lote_multi WHERE  ID_ACTA='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            llenarcomboCiclo()
            SeleccionarItemEnDropDownList(DDL_Ciclo, dt.Rows(0)("ciclo_acta").ToString())
            txtFechaSiembra.Text = If(dt.Rows(0)("fecha_acta") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_acta"), DateTime).ToString("yyyy-MM-dd"))
            CrearIdentificador(dt.Rows(0)("departamento").ToString(), dt.Rows(0)("municipio").ToString(), dt.Rows(0)("aldea").ToString(), dt.Rows(0)("caserio").ToString())
            txtProcedencia.Text = Textrespaldo.Text
            txtProductor.Text = If(dt.Rows(0)("nombre_multiplicador") Is DBNull.Value, String.Empty, dt.Rows(0)("nombre_multiplicador").ToString())
            txtCultivo.Text = If(dt.Rows(0)("tipo_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_cultivo").ToString())
            txtVariedad.Text = If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString())
            txtCategoria.Text = If(dt.Rows(0)("categoria_origen") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_origen").ToString())
            txtLote.Text = If(dt.Rows(0)("no_lote") Is DBNull.Value, String.Empty, dt.Rows(0)("no_lote").ToString())
            txtHumedad.Text = If(dt.Rows(0)("porcentaje_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("porcentaje_humedad").ToString())
            txtSacos.Text = If(dt.Rows(0)("no_sacos") Is DBNull.Value, String.Empty, dt.Rows(0)("no_sacos").ToString())
            txtPesoH.Text = If(dt.Rows(0)("peso_humedo_QQ") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_humedo_QQ").ToString())

        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString


            Label103.Text = "¿Desea eliminar la informacion almacenada de esta acta de recibo de productos para multiplicadores de semilla de DICTA?"
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
            Dim Str As String = "SELECT * FROM vista_acta_lote_multi WHERE nombre_multiplicador = @valor AND ciclo_acta = @valor2 AND id_acta = @valor3"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            adap.SelectCommand.Parameters.AddWithValue("@valor2", HttpUtility.HtmlDecode(gvrow.Cells(10).Text).ToString)
            adap.SelectCommand.Parameters.AddWithValue("@valor3", Convert.ToInt32(HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString))
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "vista_acta_lote_multi")

            Dim nombre As String

            nombre = "Acta de Recepción de Semilla " + HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString + " " + Today

            rptdocument.Load(Server.MapPath("~/pages/ActaRecepcionReport.rpt"))

            rptdocument.SetDataSource(ds)
            Response.Buffer = False


            Response.ClearContent()
            Response.ClearHeaders()

            rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

            Response.End()
            'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

        End If
    End Sub

    Protected Sub elminar(sender As Object, e As EventArgs) Handles BBorrarsi.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_registro_senasa 
                    SET estado = @estado
                WHERE id = " & TxtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "0")
                cmd.ExecuteNonQuery()
                connection.Close()
                Response.Redirect(String.Format("~/pages/AgregraActadeRecibo.aspx"))
            End Using

        End Using

    End Sub
    Protected Sub PageDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs)
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

    Protected Sub btnGuardarActa_Click(sender As Object, e As EventArgs)
        'validarflag = 0
        Verificar()
        If validarflag = 1 Then
            'Funcion para guardar en la BD
            GuardarActa()
        Else
            Label103.Text = "Debe ingresar toda la informacion primero"
            BBorrarsi.Visible = False
            BBorrarno.Visible = False
            BConfirm.Visible = True
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If
    End Sub

    Protected Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

        DivActa.Visible = True
        DivGrid.Visible = False
        btnGuardarActa.Visible = True
        BtnNuevo.Visible = True
        btnGuardarActa.Text = "Guardar"

        Dim Str As String = "SELECT * FROM sag_registro_lote WHERE 1=1"

        If (DDL_SelCLote.SelectedItem.Text <> "Todos") Then
            Str &= " AND no_lote = '" & DDL_SelCLote.SelectedItem.Text & "'"

            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)
            llenarcomboCiclo()
            TextIdlote2.Text = If(dt.Rows(0)("id") Is DBNull.Value, String.Empty, dt.Rows(0)("id").ToString())
            SeleccionarItemEnDropDownList(DDL_Ciclo, txtciclo.SelectedItem.Text)
            CrearIdentificador(dt.Rows(0)("departamento").ToString(), dt.Rows(0)("municipio").ToString(), dt.Rows(0)("aldea").ToString(), dt.Rows(0)("caserio").ToString())
            txtProcedencia.Text = Textrespaldo.Text
            txtProductor.Text = If(dt.Rows(0)("nombre_productor") Is DBNull.Value, String.Empty, dt.Rows(0)("nombre_productor").ToString())
            txtCultivo.Text = If(dt.Rows(0)("tipo_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_cultivo").ToString())
            txtVariedad.Text = If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString())
            txtCategoria.Text = If(dt.Rows(0)("categoria_semilla") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_semilla").ToString())
            txtLote.Text = If(dt.Rows(0)("no_lote") Is DBNull.Value, String.Empty, dt.Rows(0)("no_lote").ToString())
            txtlega.Text = If(dt.Rows(0)("representante_legal") Is DBNull.Value, String.Empty, dt.Rows(0)("representante_legal").ToString())
            txtnum.Text = If(dt.Rows(0)("telefono_productor") Is DBNull.Value, String.Empty, dt.Rows(0)("telefono_productor").ToString())
            Verificar()
        End If
    End Sub

    Protected Sub GuardarActa()
        Dim fechaConvertida As DateTime
        If btnGuardarActa.Text = "Actualizar" Then
            LabelGuardar.Visible = False
            LabelGuardar.Text = ""
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "UPDATE sag_registro_senasa SET
                    fecha_acta = @fecha_acta,
                    porcentaje_humedad = @porcentaje_humedad,
                    no_sacos = @no_sacos,
                    peso_humedo_QQ = @peso_humedo_QQ,
                    ciclo_acta = @ciclo_acta
                WHERE id = " & TxtID.Text & ""


                Using cmd As New MySqlCommand(query, connection)


                    If DateTime.TryParse(txtFechaSiembra.Text, fechaConvertida) Then
                        cmd.Parameters.AddWithValue("@fecha_acta", fechaConvertida.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@porcentaje_humedad", Convert.ToDecimal(txtHumedad.Text))
                    cmd.Parameters.AddWithValue("@no_sacos", Convert.ToInt64(txtSacos.Text))
                    cmd.Parameters.AddWithValue("@peso_humedo_QQ", Convert.ToDecimal(txtPesoH.Text))
                    cmd.Parameters.AddWithValue("@ciclo_acta", DDL_Ciclo.SelectedItem.Text)

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label103.Text = "¡Se ha editado correctamente el acta de recibo de productos para multiplicadores de semilla de DICTA!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    BConfirm.Visible = True
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarActa.Visible = False
                    BtnImprimir.Visible = False
                    BtnNuevo.Visible = True

                End Using
            End Using
        Else
            LabelGuardar.Visible = False
            LabelGuardar.Text = ""
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO sag_registro_senasa (
                    id2,
                    estado,
                    fecha_acta,
                    porcentaje_humedad,
                    no_sacos,
                    peso_humedo_QQ,
                    ciclo_acta
                ) VALUES (
                    @id2, 
                    @estado,
                    @fecha_acta,
                    @porcentaje_humedad,
                    @no_sacos,
                    @peso_humedo_QQ,
                    @ciclo_acta
                );
                "
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@id2", TextIdlote2.Text)
                    cmd.Parameters.AddWithValue("@estado", "1")
                    If DateTime.TryParse(txtFechaSiembra.Text, fechaConvertida) Then
                        cmd.Parameters.AddWithValue("@fecha_acta", fechaConvertida.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@porcentaje_humedad", Convert.ToDecimal(txtHumedad.Text))
                    cmd.Parameters.AddWithValue("@no_sacos", Convert.ToInt64(txtSacos.Text))
                    cmd.Parameters.AddWithValue("@peso_humedo_QQ", Convert.ToDecimal(txtPesoH.Text))
                    cmd.Parameters.AddWithValue("@ciclo_acta", DDL_Ciclo.SelectedItem.Text)

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label103.Text = "¡Se ha registrado correctamente el acta de recibo de productos para multiplicadores de semilla de DICTA!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarActa.Visible = False
                    BtnImprimir.Visible = False
                    BtnNuevo.Visible = True

                End Using
            End Using
        End If
    End Sub
    Protected Sub Verificar()
        '1
        If String.IsNullOrEmpty(txtFechaSiembra.Text) Then
            lblFecha.Text = "*"
            validarflag = 0
        Else
            lblFecha.Text = ""
            validarflag += 1
        End If
        '2
        If String.IsNullOrEmpty(txtHumedad.Text) Then
            lblHumedad.Text = "*"
            validarflag = 0
        Else
            lblHumedad.Text = ""
            validarflag += 1
        End If
        '3
        If String.IsNullOrEmpty(txtSacos.Text) Then
            lblSacos.Text = "*"
            validarflag = 0
        Else
            lblSacos.Text = ""
            validarflag += 1
        End If
        '4
        If String.IsNullOrEmpty(txtPesoH.Text) Then
            lblPesoH.Text = "*"
            validarflag = 0
        Else
            lblPesoH.Text = ""
            validarflag += 1
        End If
        '5
        If (DDL_Ciclo.SelectedItem.Text = " ") Then
            Labelciclo.Text = "*"
            validarflag = 0
        Else
            Labelciclo.Text = ""
            validarflag += 1
        End If

        If validarflag = 5 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub

    Private Sub exportar()

        Dim cadena As String = "*"
        Dim query As String = ""
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""
        Dim c2 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND  NOMBRE_MULTIPLICADOR = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c2 = " "
        Else
            c2 = "AND  departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        If (DDL_SelCLote.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND no_lote = '" & DDL_SelCLote.SelectedItem.Text & "' "
        End If

        If (txtciclo.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND ciclo_acta = '" & txtciclo.SelectedItem.Text & "' "
        End If

        query = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND ciclo_acta IS NOT NULL " & c1 & c3 & c4 & c2

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        'Set Name of DataTables.
                        ds.Tables(0).TableName = "sag_registro_senasa"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Acta de Recibo de Multiplicadores " & Today & " " & TxtProductorGrid.SelectedItem.Text & ".xlsx")
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

    Protected Sub CrearIdentificador(d1 As String, m2 As String, a3 As String, c4 As String)
        Dim dep As String = d1
        Dim mun As String = m2
        Dim ald As String = a3
        Dim cas As String = c4

        Dim resultado As String = String.Format("{0}-{1}-{2}-{3}", dep, mun, ald, cas)

        Textrespaldo.Text = resultado
    End Sub
    Protected Sub separarIdentificador(procedencia As String)
        Dim cadena As String = procedencia

        Dim partes() As String = cadena.Split("-"c)

        If partes.Length >= 4 Then
            Dim depart As String = partes(0)
            Dim muni As String = partes(1)
            Dim alde As String = partes(2)
            Dim caser As String = partes(3)

            Textdepart.Text = depart
            Textmuni.Text = muni
            Textalde.Text = alde
            Textcase.Text = caser
        End If

    End Sub
    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtén los datos de la fila actual
            Dim estimadoProduccion As String = DataBinder.Eval(e.Row.DataItem, "no_sacos").ToString()

            ' Encuentra los botones en la fila por índice
            Dim btnEditar As Button = DirectCast(e.Row.Cells(11).Controls(0), Button) ' Ajusta el índice según la posición de tu botón en la fila
            Dim btnImprimir As Button = DirectCast(e.Row.Cells(13).Controls(0), Button)

            ' Modifica el texto y el color de los botones según la lógica que desees
            If Not String.IsNullOrEmpty(estimadoProduccion) Then
                btnEditar.Text = "Editar"
                btnEditar.CssClass = "btn btn-primary"
                btnEditar.Style("background-color") = "#007bff" ' Establece el color de fondo directamente
            Else
                btnEditar.Text = "Agregar"
                btnEditar.CssClass = "btn btn-success"
                btnEditar.Style("background-color") = "#28a745" ' Establece el color de fondo directamente
            End If

            If btnEditar.Text = "Editar" Then
                btnImprimir.Visible = True
            Else
                btnImprimir.Visible = False
            End If
        End If
    End Sub

    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE nombre_multiplicador = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", txtProductor.Text)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "sag_registro_senasa")

        Dim nombre As String

        nombre = " Datos del Multiplicador " + Today

        rptdocument.Load(Server.MapPath("~/pages/AgregarMultiplicadorReport2.rpt"))

        rptdocument.SetDataSource(ds)
        Response.Buffer = False


        Response.ClearContent()
        Response.ClearHeaders()

        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub
End Class