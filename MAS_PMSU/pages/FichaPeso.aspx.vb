Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports ClosedXML.Excel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.[Shared].Json
Imports DocumentFormat.OpenXml.Office.Word
Imports MySql.Data.MySqlClient

Public Class FichaPeso
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
                llenarcomboCiclogrid()
                llenarcomboDepto()
                llenagrid()
            End If
        End If
    End Sub
    Protected Sub vaciar(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/FichaPeso.aspx"))
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
        Dim StrCombo As String = "SELECT DISTINCT nombre_multiplicador FROM `vista_acta_lote_multi` WHERE 1 = 1 AND semilla_QQ_oro IS NOT NULL AND estado_sena = '1' ORDER BY nombre_multiplicador ASC"
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
        StrCombo = "SELECT DISTINCT nombre_multiplicador FROM vista_acta_lote_multi WHERE estado_sena = '1' AND semilla_QQ_oro IS NOT NULL AND departamento = '" & TxtDepto.SelectedItem.Text & "' ORDER BY nombre_productor ASC"
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
        Dim cadena As String = "id_acta, nombre_multiplicador, departamento, representante_legal, ciclo_acta, categoria_origen, tipo_cultivo, variedad, no_lote, porcentaje_humedad, no_sacos, peso_humedo_QQ, semilla_QQ_oro, tara, peso_neto"
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


        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND semilla_QQ_oro IS NOT NULL AND estado_sena = '1' " & c1 & c3 & c4 & c2

        GridDatos.DataBind()
    End Sub
    Protected Sub TxtProductorGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtProductorGrid.SelectedIndexChanged
        llenagrid()
    End Sub

    Protected Sub DDL_SelCult_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDL_SelCult.SelectedIndexChanged
        llenagrid()
    End Sub

    Protected Sub txtciclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtciclo.SelectedIndexChanged
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

        If (e.CommandName = "Editar") Then
            DivGrid.Visible = "false"
            DivActa.Visible = "true"
            btnGuardarActa.Visible = True
            BtnNuevo.Visible = True

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "nombre_multiplicador, departamento, municipio, aldea, caserio, representante_legal, telefono_multiplicador, categoria_origen, tipo_cultivo, variedad, no_lote, porcentaje_humedad, no_sacos, semilla_QQ_oro, peso_neto, tara, peso_lb"
            Dim Str As String = "SELECT " & cadena & " FROM vista_acta_lote_multi WHERE  ID_acta='" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & "' "
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString
            txt_nombre_prod_new.Text = If(dt.Rows(0)("nombre_multiplicador") Is DBNull.Value, String.Empty, dt.Rows(0)("nombre_multiplicador").ToString())
            CrearIdentificador(dt.Rows(0)("departamento").ToString(), dt.Rows(0)("municipio").ToString(), dt.Rows(0)("aldea").ToString(), dt.Rows(0)("caserio").ToString())
            txtLugProc.Text = TextRespaldo.Text
            Txt_Representante_Legal.Text = If(dt.Rows(0)("representante_legal") Is DBNull.Value, String.Empty, dt.Rows(0)("representante_legal").ToString())
            TxtTelefono.Text = If(dt.Rows(0)("telefono_multiplicador") Is DBNull.Value, String.Empty, dt.Rows(0)("telefono_multiplicador").ToString())
            txtCategoria.Text = If(dt.Rows(0)("categoria_origen") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_origen").ToString())
            txtCultivo.Text = If(dt.Rows(0)("tipo_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_cultivo").ToString())
            txtVariedad.Text = If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString())
            TxtLote.Text = If(dt.Rows(0)("no_lote") Is DBNull.Value, String.Empty, dt.Rows(0)("no_lote").ToString())
            txtHumedad.Text = If(dt.Rows(0)("porcentaje_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("porcentaje_humedad").ToString())
            txtCantSaco.Text = If(dt.Rows(0)("no_sacos") Is DBNull.Value, String.Empty, dt.Rows(0)("no_sacos").ToString())
            txtPesoBrut.Text = If(dt.Rows(0)("semilla_QQ_oro") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_QQ_oro").ToString())
            txtPesoNeto.Text = If(dt.Rows(0)("peso_neto") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_neto").ToString())
            txtTara.Text = If(dt.Rows(0)("tara") Is DBNull.Value, String.Empty, dt.Rows(0)("tara").ToString())
            txtPesoLibr.Text = If(dt.Rows(0)("peso_lb") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_lb").ToString())
            total()
            Verificar()
        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString


            Label103.Text = "¿Desea eliminar la informacion almacenada que contiene el cuadro de procesamiento (secado, limpieza y clasificación)?
                              
                            *NOTA: Solo se elimira la informacion que habia ingresado el usuario, de la tabla no se eliminara."
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
            Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE nombre_productor = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "sag_registro_senasa1")

            Dim nombre As String

            nombre = "Ficha De Peso Al Recibo Lotes De Semilla " + HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString + " " + Today

            rptdocument.Load(Server.MapPath("~/pages/FichaReport.rpt"))

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
    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE nombre_productor = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", TxtID.Text)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "sag_registro_senasa1")

        Dim nombre As String

        nombre = "Ficha De Peso Al Recibo Lotes De Semilla " + Today

        rptdocument.Load(Server.MapPath("~/pages/FichaReport.rpt"))

        rptdocument.SetDataSource(ds)
        Response.Buffer = False

        Response.ClearContent()
        Response.ClearHeaders()

        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub

    Protected Sub elminar(sender As Object, e As EventArgs) Handles BBorrarsi.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_registro_senasa 
                    SET tara = @tara,
                        peso_lb = @peso_lb,
                        peso_neto = @peso_neto
                WHERE id = " & TxtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@tara", DBNull.Value)
                cmd.Parameters.AddWithValue("@peso_lb", DBNull.Value)
                cmd.Parameters.AddWithValue("@peso_neto", DBNull.Value)

                cmd.ExecuteNonQuery()
                connection.Close()
                Response.Redirect(String.Format("~/pages/FichaPeso.aspx"))
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

    Protected Sub GuardarActa()
        LabelGuardar.Visible = False
        LabelGuardar.Text = ""
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_registro_senasa SET
                tara = @tara,
                peso_neto = @peso_neto,
                peso_lb = @peso_lb
            WHERE id = " & TxtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)


                cmd.Parameters.AddWithValue("@tara", Convert.ToDecimal(txtTara.Text))
                cmd.Parameters.AddWithValue("@peso_neto", Convert.ToDecimal(txtPesoNeto.Text))
                cmd.Parameters.AddWithValue("@peso_lb", Convert.ToDecimal(txtPesoLibr.Text))

                cmd.ExecuteNonQuery()
                connection.Close()

                Label103.Text = "¡Se ha registrado correctamente la ficha de peso al recibo lotes de semilla (pesaje y embolsado)!"
                BBorrarsi.Visible = False
                BBorrarno.Visible = False
                BConfirm.Visible = True
                ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                btnGuardarActa.Visible = False
                BtnImprimir.Visible = False
                BtnNuevo.Visible = True
            End Using
        End Using
    End Sub
    Protected Sub Verificar()
        '1
        If String.IsNullOrEmpty(txtTara.Text) Then
            lblTara.Text = "*"
            validarflag = 0
        Else
            lblTara.Text = ""
            validarflag += 1
        End If
        ''2
        'If String.IsNullOrEmpty(txtSemOro.Text) Then
        '    lblSemOro.Text = "*"
        '    validarflag = 0
        'Else
        '    lblSemOro.Text = ""
        '    validarflag += 1
        'End If
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

        If validarflag = 1 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub
    Private Sub exportar()

        Dim cadena As String = "id_acta, nombre_multiplicador, departamento, tipo_cultivo, variedad, categoria_origen, no_lote, DATE_FORMAT(fecha_acta, '%d-%m-%Y') AS fecha_acta, peso_humedo_QQ, porcentaje_humedad, peso_materia_prima_QQ_porce_humedad, semilla_QQ_oro, semilla_QQ_consumo, semilla_QQ_basura, semilla_QQ_total, observaciones, ciclo_acta, peso_lb"
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

        query = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND semilla_QQ_oro IS NOT NULL AND estado_sena = '1' " & c1 & c3 & c4 & c2

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        ' Set Name of DataTables.
                        ds.Tables(0).TableName = "Ficha De Peso Al Recibo Lotes De Semilla"

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
                            Response.AddHeader("content-disposition", "attachment;filename=Ficha De Peso Al Recibo Lotes De Semilla.xlsx")
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

        TextRespaldo.Text = resultado
    End Sub

    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtén los datos de la fila actual
            Dim estimadoProduccion As String = DataBinder.Eval(e.Row.DataItem, "tara").ToString()

            ' Encuentra los botones en la fila por índice
            Dim btnEditar As Button = DirectCast(e.Row.Cells(14).Controls(0), Button) ' Ajusta el índice según la posición de tu botón en la fila
            Dim btnImprimir As Button = DirectCast(e.Row.Cells(16).Controls(0), Button)

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

    Protected Sub txtPesoBrut_TextChanged(sender As Object, e As EventArgs) Handles txtPesoBrut.TextChanged
        total()
    End Sub
    Protected Sub txtTara_TextChanged(sender As Object, e As EventArgs) Handles txtTara.TextChanged
        total()
    End Sub
    Protected Sub txtPesoNeto_TextChanged(sender As Object, e As EventArgs) Handles txtPesoNeto.TextChanged
        total()
    End Sub

    Protected Sub total()
        Dim valorOro As Decimal = 0
        Dim valortara As Decimal = 0
        Dim pesolb As Decimal = 0

        If Decimal.TryParse(txtPesoBrut.Text, valorOro) Then
            valorOro = Convert.ToDecimal(txtPesoBrut.Text)
        End If

        If Decimal.TryParse(txtTara.Text, valortara) Then
            valortara = Convert.ToDecimal(txtTara.Text)
        End If

        Dim sumaTotal As Decimal

        If txtTara.Text = "" Then
            sumaTotal = 0
        Else
            sumaTotal = valorOro - valortara
        End If
        If sumaTotal <> 0 Then
            txtPesoNeto.Text = sumaTotal.ToString()
            pesolb = Convert.ToDecimal(txtPesoNeto.Text) * 100
            txtPesoLibr.Text = pesolb.ToString
            txtCantSacoC.Text = txtCantSaco.Text
            txtCantQQ.Text = txtPesoNeto.Text
        Else
            txtPesoNeto.Text = "0.00"
            txtPesoLibr.Text = "0.00"
            txtCantSacoC.Text = "0.00"
            txtCantQQ.Text = "0.00"
        End If
    End Sub
    'Protected Sub buscarYguardar(palabra As String)
    '    Dim Str As String = "SELECT variedad_categoria FROM `sag_registro_inventario` WHERE variedad_categoria = '" & palabra & "'"
    '    Dim adap As New MySqlDataAdapter(Str, conn)
    '    Dim dt As New DataTable
    '    adap.Fill(dt)
    '
    '    Dim connectionString As String = conn
    '    Using connection As New MySqlConnection(connectionString)
    '        connection.Open()
    '
    '        For Each row As DataRow In dt.Rows
    '            For Each column As DataColumn In dt.Columns
    '                If row(column).ToString() = palabra Then
    '
    '                    Dim query As String = "UPDATE sag_registro_inventario SET
    '                        peso_neto = @peso_neto
    '                    WHERE variedad_categoria = " & palabra & ""
    '
    '                    Using cmd As New MySqlCommand(query, connection)
    '
    '                        cmd.Parameters.AddWithValue("@peso_neto", Convert.ToDecimal(txtPesoNeto.Text))
    '
    '                        cmd.ExecuteNonQuery()
    '                        connection.Close()
    '                    End Using
    '                Else
    '                    Dim query As String = "INSERT INTO sag_registro_inventario (variedad_categoria, categoria_origen, tipo_cultivo, variedad, estado, peso_neto) VALUES (@variedad_categoria, @categoria_origen, @tipo_cultivo, @variedad, @estado, @peso_neto)"
    '
    '                    Using cmd As New MySqlCommand(query, connection)
    '
    '                        cmd.Parameters.AddWithValue("@variedad_categoria", palabra)
    '                        cmd.Parameters.AddWithValue("@categoria_origen", txtCategoria.Text)
    '                        cmd.Parameters.AddWithValue("@tipo_cultivo", txtCultivo.Text)
    '                        cmd.Parameters.AddWithValue("@variedad", txtVariedad.Text)
    '                        cmd.Parameters.AddWithValue("@estado", "1")
    '                        cmd.Parameters.AddWithValue("@peso_neto", Convert.ToDecimal(txtPesoNeto.Text))
    '
    '                        cmd.ExecuteNonQuery()
    '                        connection.Close()
    '                    End Using
    '                End If
    '            Next
    '        Next
    '    End Using
    'End Sub
    'Protected Sub identificador(c1 As String, v2 As String)
    '    Dim cat As String = c1
    '    Dim vari As String = v2
    '
    '
    '    Dim resultado As String = String.Format("{0}-{1}", cat, vari)
    '
    '    txtunion.Text = resultado
    'End Sub
End Class