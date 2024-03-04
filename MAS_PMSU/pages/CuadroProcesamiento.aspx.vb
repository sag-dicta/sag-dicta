Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports ClosedXML.Excel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.[Shared].Json
Imports DocumentFormat.OpenXml.Office.Word
Imports MySql.Data.MySqlClient

Public Class CuadroProcesamiento
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connSAG").ConnectionString
    Dim sentencia As String
    Dim validarflag As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If User.Identity.IsAuthenticated = True Then
            If IsPostBack Then

            Else
                llenarcomboProductor()
                llenarcomboDepto()
                llenarcomboCiclogrid()
                llenagrid()
            End If
        End If
    End Sub
    Protected Sub vaciar(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/CuadroProcesamiento.aspx"))
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
    Private Sub llenarcomboProductor()
        Dim StrCombo As String = "SELECT DISTINCT nombre_multiplicador FROM `vista_acta_lote_multi` WHERE 1 = 1 AND fecha_acta IS NOT NULL AND estado_sena = '1' ORDER BY nombre_multiplicador ASC"
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
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT nombre_multiplicador FROM vista_acta_lote_multi WHERE estado_sena = '1' AND fecha_acta IS NOT NULL AND departamento = '" & TxtDepto.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"

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
    Protected Sub TxtDepto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TxtDepto.SelectedIndexChanged
        If TxtDepto.SelectedItem.Text = "Todos" Then
            llenarcomboProductor()
        Else
            llenarcomboProductor2()
        End If
        llenagrid()
    End Sub
    Sub llenagrid()
        Dim cadena As String = "id_acta, nombre_multiplicador, departamento, tipo_cultivo, variedad, lote_registrado, categoria_registrado, ciclo_acta, peso_humedo_QQ, porcentaje_humedad, peso_materia_prima_QQ_porce_humedad, semilla_QQ_oro, semilla_QQ_consumo, semilla_QQ_basura, semilla_QQ_total, observaciones"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""
        Dim c2 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND  nombre_multiplicador = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c2 = " "
        Else
            c2 = "AND  departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        If (DDL_SelCult.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND tipo_cultivo = '" & DDL_SelCult.SelectedItem.Text & "' "
        End If

        If (txtciclo.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND ciclo_acta = '" & txtciclo.SelectedItem.Text & "' "
        End If


        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND fecha_acta IS NOT NULL AND estado_sena = '1' " & c1 & c3 & c4 & c2 & " ORDER BY id_acta DESC"

        GridDatos.DataBind()
    End Sub
    Protected Sub TxtProductorGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtProductorGrid.SelectedIndexChanged
        llenagrid()
    End Sub
    Protected Sub DDL_SelCult_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDL_SelCult.SelectedIndexChanged
        llenagrid()
    End Sub
    Protected Sub txtciclo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtciclo.SelectedIndexChanged
        llenagrid()
    End Sub

    Protected Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        lblTotalClientes.Text = e.AffectedRows.ToString()

    End Sub
    Protected Function SeleccionarItemEnDropDownList(ByVal Prodname As DropDownList, ByVal DtCombo As String)
        If DtCombo = "Frijol" Or DtCombo = "Maiz" Then
            For Each item As ListItem In Prodname.Items
                If item.Text = DtCombo Then
                    Prodname.SelectedValue = item.Value
                    Return True ' Se encontró una coincidencia, devolver verdadero
                End If
            Next
        Else
            For Each item As ListItem In Prodname.Items
                If item.Text = DtCombo Then
                    Prodname.SelectedValue = item.Value
                    Return True ' Se encontró una coincidencia, devolver verdadero
                End If
            Next
        End If
        ' No se encontró ninguna coincidencia
        Return 0
    End Function
    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand
        'Dim fecha2 As Date

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        If (e.CommandName = "Subir") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            div_nuevo_prod.Visible = True
            DivGrid.Visible = False
            DivActa.Visible = False
            DivActaInfo.Visible = False
        End If

        If (e.CommandName = "Editar") Then
            DivGrid.Visible = "false"
            DivActa.Visible = "true"
            DivActaInfo.Visible = "true"
            btnGuardarActa.Visible = True
            BtnNuevo.Visible = True

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "peso_materia_prima_QQ_porce_humedad, semilla_QQ_oro, semilla_QQ_consumo, semilla_QQ_basura, semilla_QQ_total, observaciones, RENDIMIETO_ORO_PESO"
            Dim Str As String = "SELECT " & cadena & " FROM vista_acta_lote_multi WHERE  ID_acta='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            llenarCampoLectura(TxtID.Text)
            txtPeso12Hum.Text = If(dt.Rows(0)("peso_materia_prima_QQ_porce_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_materia_prima_QQ_porce_humedad").ToString())
            'CrearIdentificador(dt.Rows(0)("departamento").ToString(), dt.Rows(0)("municipio").ToString(), dt.Rows(0)("aldea").ToString(), dt.Rows(0)("caserio").ToString())
            txtSemOro.Text = If(dt.Rows(0)("semilla_QQ_oro") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_QQ_oro").ToString())
            txtConsumo.Text = If(dt.Rows(0)("semilla_QQ_consumo") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_QQ_consumo").ToString())
            txtBasura.Text = If(dt.Rows(0)("semilla_QQ_basura") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_QQ_basura").ToString())
            txtTotal.Text = If(dt.Rows(0)("semilla_QQ_total") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_QQ_total").ToString())
            txtObserv.Text = If(dt.Rows(0)("observaciones") Is DBNull.Value, String.Empty, dt.Rows(0)("observaciones").ToString())
            txtrendimiento.Text = If(dt.Rows(0)("RENDIMIETO_ORO_PESO") Is DBNull.Value, String.Empty, dt.Rows(0)("RENDIMIETO_ORO_PESO").ToString())
            Verificar()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString


            Label103.Text = "¿Desea eliminar la informacion almacenada que contiene el cuadro de procesamiento (secado, limpieza y clasificación)?
                              
                            *NOTA: Solo se elimira la informacion que habia ingresado el usuario."
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
            Dim Str As String = "SELECT * FROM vista_acta_lote_multi WHERE ID_acta = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "vista_acta_lote_multi")

            Dim nombre As String

            nombre = "Cuadro de procesamiento " + HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString + " " + Today

            rptdocument.Load(Server.MapPath("~/pages/cuadro_procesamiento.rpt"))

            rptdocument.SetDataSource(ds)
            Response.Buffer = False


            Response.ClearContent()
            Response.ClearHeaders()

            rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

            Response.End()
            'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

        End If

        If (e.CommandName = "observacion") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "observaciones"
            Dim Str As String = "SELECT " & cadena & " FROM vista_acta_lote_multi WHERE  ID_acta='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            Label103.Text = If(dt.Rows(0)("observaciones") Is DBNull.Value, String.Empty, dt.Rows(0)("observaciones").ToString())
            BBorrarsi.Visible = False
            BBorrarno.Visible = False
            BConfirm.Visible = True
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If
    End Sub

    Protected Sub elminar(sender As Object, e As EventArgs) Handles BBorrarsi.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_registro_senasa 
                    SET peso_materia_prima_QQ_porce_humedad = @peso_materia_prima_QQ_porce_humedad,
                        semilla_QQ_oro = @semilla_QQ_oro,
                        semilla_QQ_consumo = @semilla_QQ_consumo,
                        semilla_QQ_basura = @semilla_QQ_basura,
                        semilla_QQ_total = @semilla_QQ_total,
                        observaciones = @observaciones,
                        rendimiento_oro_peso = @rendimiento_oro_peso
                WHERE id = " & TxtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@peso_materia_prima_QQ_porce_humedad", DBNull.Value)
                cmd.Parameters.AddWithValue("@semilla_QQ_oro", DBNull.Value)
                cmd.Parameters.AddWithValue("@semilla_QQ_consumo", DBNull.Value)
                cmd.Parameters.AddWithValue("@semilla_QQ_basura", DBNull.Value)
                cmd.Parameters.AddWithValue("@semilla_QQ_total", DBNull.Value)
                cmd.Parameters.AddWithValue("@observaciones", DBNull.Value)
                cmd.Parameters.AddWithValue("@rendimiento_oro_peso", DBNull.Value)
                cmd.ExecuteNonQuery()
                connection.Close()
                Response.Redirect(String.Format("~/pages/CuadroProcesamiento.aspx"))
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
            BConfirm.Visible = False
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If
    End Sub

    Protected Sub GuardarActa()
        Dim oro As Decimal = 0
        Dim prima As Decimal = 0

        If Decimal.TryParse(txtPeso12Hum.Text, prima) Then
            prima = Convert.ToDecimal(txtPeso12Hum.Text)
        End If

        If Decimal.TryParse(txtSemOro.Text, oro) Then
            oro = Convert.ToDecimal(txtSemOro.Text)
        End If

        If oro <= prima Then
            LabelGuardar.Visible = False
            LabelGuardar.Text = ""
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "UPDATE sag_registro_senasa SET
                peso_materia_prima_QQ_porce_humedad = @peso_materia_prima_QQ_porce_humedad,
                semilla_QQ_oro = @semilla_QQ_oro,
                semilla_QQ_consumo = @semilla_QQ_consumo,
                semilla_QQ_basura = @semilla_QQ_basura,
                semilla_QQ_total = @semilla_QQ_total,
                observaciones = @observaciones,
                rendimiento_oro_peso = @rendimiento_oro_peso
            WHERE id = " & TxtID.Text & ""

                Using cmd As New MySqlCommand(query, connection)


                    cmd.Parameters.AddWithValue("@peso_materia_prima_QQ_porce_humedad", Convert.ToDecimal(txtPeso12Hum.Text)) ' Aquí se formatea correctamente como yyyy-MM-dd
                    cmd.Parameters.AddWithValue("@rendimiento_oro_peso", Convert.ToDecimal(txtrendimiento.Text))
                    cmd.Parameters.AddWithValue("@semilla_QQ_oro", Convert.ToDecimal(txtSemOro.Text))
                    If txtConsumo.Text = "" Then
                        cmd.Parameters.AddWithValue("@semilla_QQ_consumo", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@semilla_QQ_consumo", Convert.ToDecimal(txtConsumo.Text))
                    End If
                    If txtBasura.Text = "" Then
                        cmd.Parameters.AddWithValue("@semilla_QQ_basura", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@semilla_QQ_basura", Convert.ToDecimal(txtBasura.Text))
                    End If
                    cmd.Parameters.AddWithValue("@semilla_QQ_total", Convert.ToDecimal(txtTotal.Text))
                    If txtObserv.Text = "" Then
                        cmd.Parameters.AddWithValue("@observaciones", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@observaciones", txtObserv.Text)
                    End If

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label103.Text = "¡Se ha registrado correctamente el cuadro de procesamiento (secado, limpieza y clasificación)!"
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
            Label1.Text = "¡No se puede guardar. El peso oro debe ser menor o igual al peso prima.!"
            Button3.Visible = False
            Button2.Visible = False
            Button1.Visible = True
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal2').modal('show'); });", True)
        End If
    End Sub
    Protected Sub Verificar()
        '1
        If String.IsNullOrEmpty(txtPeso12Hum.Text) Then
            lblPeso12Hum.Text = "*"
            validarflag = 0
        Else
            lblPeso12Hum.Text = ""
            validarflag += 1
        End If
        '2
        If String.IsNullOrEmpty(txtSemOro.Text) Then
            lblSemOro.Text = "*"
            validarflag = 0
        Else
            lblSemOro.Text = ""
            validarflag += 1
        End If
        ''3
        'If String.IsNullOrEmpty(txtConsumo.Text) Then
        '    lblConsumo.Text = "*"
        '    validarflag = 0
        'Else
        '    lblConsumo.Text = ""
        '    validarflag += 1
        'End If
        ''4
        'If String.IsNullOrEmpty(txtBasura.Text) Then
        '    lblBasura.Text = "*"
        '    validarflag = 0
        'Else
        '    lblBasura.Text = ""
        '    validarflag += 1
        'End If

        If validarflag = 2 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub
    Private Sub exportar()

        Dim cadena As String = "id_acta, nombre_multiplicador, departamento, tipo_cultivo, variedad, lote_registrado, categoria_registrado, DATE_FORMAT(fecha_acta, '%d-%m-%Y') AS fecha_acta, peso_humedo_QQ, porcentaje_humedad, peso_materia_prima_QQ_porce_humedad, semilla_QQ_oro, semilla_QQ_consumo, semilla_QQ_basura, semilla_QQ_total, observaciones, ciclo_acta"
        Dim query As String = ""
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""
        Dim c2 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND  nombre_multiplicador = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c2 = " "
        Else
            c2 = "AND  departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        If (DDL_SelCult.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND tipo_cultivo = '" & DDL_SelCult.SelectedItem.Text & "' "
        End If

        If (txtciclo.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND ciclo_acta = '" & txtciclo.SelectedItem.Text & "' "
        End If


        query = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND fecha_acta IS NOT NULL AND estado_sena = '1' " & c1 & c3 & c4 & c2

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        ' Set Name of DataTables.
                        ds.Tables(0).TableName = "CUADRO DE PROCESAMIENTO"

                        Using wb As New XLWorkbook()
                            ' Add DataTable as Worksheet.
                            wb.Worksheets.Add(ds.Tables(0), "sag_registro_senasa")

                            ' Set auto width for all columns based on content.
                            wb.Worksheet(1).Columns().AdjustToContents()

                            ' Export the Excel file.
                            Response.Clear()
                            Response.Buffer = True
                            Response.Charset = ""
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            Response.AddHeader("content-disposition", "attachment;filename=Cuadro_de_procesamiento.xlsx")
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

        'Textrespaldo.Text = resultado
    End Sub

    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtén los datos de la fila actual
            Dim estimadoProduccion As String = DataBinder.Eval(e.Row.DataItem, "semilla_QQ_total").ToString()

            ' Encuentra los botones en la fila por índice
            Dim btnEditar As Button = DirectCast(e.Row.Cells(17).Controls(0), Button) ' Ajusta el índice según la posición de tu botón en la fila
            Dim btnImprimir As Button = DirectCast(e.Row.Cells(19).Controls(0), Button)

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
        adap.SelectCommand.Parameters.AddWithValue("@valor", TxtID.Text)
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
    Protected Sub txtPeso12Hum_TextChanged(sender As Object, e As EventArgs) Handles txtPeso12Hum.TextChanged
        total()
    End Sub
    Protected Sub txtOro_TextChanged(sender As Object, e As EventArgs) Handles txtSemOro.TextChanged
        total()
    End Sub
    Protected Sub txtConsumo_TextChanged(sender As Object, e As EventArgs) Handles txtConsumo.TextChanged
        total()
    End Sub
    Protected Sub txtBasura_TextChanged(sender As Object, e As EventArgs) Handles txtBasura.TextChanged
        total()
    End Sub

    Protected Sub total()
        Dim valorOro As Decimal = 0
        Dim valorConsu As Decimal = 0
        Dim valorBasura As Decimal = 0
        Dim valorRendi As Decimal = 0
        Dim oro As Decimal = 0
        Dim prima As Decimal = 0
        lblrendimiento.Text = ""

        If Decimal.TryParse(txtSemOro.Text, valorOro) Then
            valorOro = Convert.ToDecimal(txtSemOro.Text)
        End If

        If Decimal.TryParse(txtConsumo.Text, valorConsu) Then
            valorConsu = Convert.ToDecimal(txtConsumo.Text)
        End If

        If Decimal.TryParse(txtBasura.Text, valorBasura) Then
            valorBasura = Convert.ToDecimal(txtBasura.Text)
        End If

        If Decimal.TryParse(txtPeso12Hum.Text, prima) Then
            prima = Convert.ToDecimal(txtPeso12Hum.Text)
        End If

        If Decimal.TryParse(txtSemOro.Text, oro) Then
            oro = Convert.ToDecimal(txtSemOro.Text)
        End If

        If Not String.IsNullOrEmpty(txtPeso12Hum.Text) AndAlso Not String.IsNullOrEmpty(txtSemOro.Text) AndAlso oro <= prima Then
            valorRendi = (oro / prima) * 100
            txtrendimiento.Text = valorRendi.ToString("0.00")
        Else
            txtrendimiento.Text = "0.00"
            lblrendimiento.Text = "El peso oro debe ser menor o igual al peso prima."
        End If

        ' Realizar la suma sin considerar los TextBox vacíos
        Dim sumaTotal As Decimal = valorOro + valorConsu + valorBasura

        If sumaTotal <> 0 Then
            txtTotal.Text = sumaTotal.ToString()
        Else
            txtTotal.Text = ""
        End If
    End Sub

    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/CuadroProcesamiento.aspx"))
    End Sub
    Private Sub llenarCampoLectura(ByVal id As String)
        Dim cadena As String = "fecha_acta, nombre_multiplicador, departamento, municipio, aldea, caserio, no_lote, tipo_cultivo, variedad, categoria_origen, porcentaje_humedad, no_sacos, peso_humedo_QQ, ciclo_acta, lote_registrado, categoria_registrado"
        Dim Str As String = "SELECT " & cadena & " FROM vista_acta_lote_multi WHERE  ID_ACTA=" & id & ""
        Dim adap As New MySqlDataAdapter(Str, conn)
        Dim dt As New DataTable
        adap.Fill(dt)

        Textciclo2.Text = If(dt.Rows(0)("ciclo_acta") Is DBNull.Value, String.Empty, dt.Rows(0)("ciclo_acta").ToString())
        txtFechaSiembra.Text = If(dt.Rows(0)("fecha_acta") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_acta"), DateTime).ToString("yyyy-MM-dd"))
        txtProductor.Text = If(dt.Rows(0)("nombre_multiplicador") Is DBNull.Value, String.Empty, dt.Rows(0)("nombre_multiplicador").ToString())
        txtCultivo.Text = If(dt.Rows(0)("tipo_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_cultivo").ToString())
        txtVariedad.Text = If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString())
        txtCategoria.Text = If(dt.Rows(0)("categoria_registrado") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_registrado").ToString())
        txtHumedad.Text = If(dt.Rows(0)("porcentaje_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("porcentaje_humedad").ToString())
        txtSacos.Text = If(dt.Rows(0)("no_sacos") Is DBNull.Value, String.Empty, dt.Rows(0)("no_sacos").ToString())
        txtPesoH.Text = If(dt.Rows(0)("peso_humedo_QQ") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_humedo_QQ").ToString())
        txtLoteRegi.Text = If(dt.Rows(0)("lote_registrado") Is DBNull.Value, String.Empty, dt.Rows(0)("lote_registrado").ToString())
    End Sub
    Private Function FileUploadToBytes(fileUpload As FileUpload) As Byte()
        Using stream As System.IO.Stream = fileUpload.PostedFile.InputStream
            Dim length As Integer = fileUpload.PostedFile.ContentLength
            Dim bytes As Byte() = New Byte(length - 1) {}
            stream.Read(bytes, 0, length)
            Return bytes
        End Using
    End Function
    Private Function EsExtensionValida(fileName As String) As Boolean
        Dim extension As String = Path.GetExtension(fileName)
        Dim esValida As Boolean = False
        If extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase) Then
            esValida = True
        End If
        Return esValida
    End Function
    Protected Function ValidarFormulario() As Boolean
        Dim esValido As Boolean = True
        LabelPDF.Visible = False

        If Not FileUploadPDF.HasFile OrElse Not EsExtensionValida(FileUploadPDF.FileName) Then
            LabelPDF.Visible = True
            esValido = False
        End If

        Return esValido
    End Function
    Protected Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click

        If ValidarFormulario() Then

            Dim connectionString As String = conn
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim bytesPDF As Byte() = FileUploadToBytes(FileUploadPDF)

                ' Actualizar bytes en la base de datos
                Dim query As String = "UPDATE sag_registro_senasa SET cuadro_firmado = @cuadro_firmado WHERE ID=" & TxtID.Text & " "
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cuadro_firmado", bytesPDF)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Label23.Visible = False
            Label25.Visible = True
            BtnUpload.Visible = False
        Else
            Label23.Visible = True
            Label25.Visible = False
            BtnUpload.Visible = True
        End If

    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Response.Redirect(String.Format("~/pages/CuadroProcesamiento.aspx"))
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Response.Redirect(String.Format("~/pages/Cuando_Procesamiento_DescArch.aspx"))
    End Sub
End Class