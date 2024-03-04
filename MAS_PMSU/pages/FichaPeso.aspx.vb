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
                eliminarMiniGrid2()
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
        StrCombo = "SELECT DISTINCT nombre_multiplicador FROM vista_acta_lote_multi WHERE estado_sena = '1' AND semilla_QQ_oro IS NOT NULL AND departamento = '" & TxtDepto.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"
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
        Dim cadena As String = "id_acta, nombre_multiplicador, departamento, representante_legal, ciclo_acta, categoria_registrado, tipo_cultivo, variedad, lote_registrado, porcentaje_humedad, no_sacos, peso_humedo_QQ, semilla_QQ_oro, tara, peso_neto"
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


        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND semilla_QQ_oro IS NOT NULL AND estado_sena = '1' " & c1 & c3 & c4 & c2 & "ORDER BY id_acta DESC"

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

        If (e.CommandName = "Subir") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            div_nuevo_prod.Visible = True
            DivGrid.Visible = False
            DivActa.Visible = False
        End If

        If (e.CommandName = "Editar") Then

            DivGrid.Visible = "false"
            DivActa.Visible = "true"
            btnGuardarActa.Visible = True
            BtnNuevo.Visible = True
            btnRegresarConficha.Visible = False

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim cadena As String = "nombre_multiplicador, departamento, municipio, aldea, caserio, representante_legal, telefono_multiplicador, categoria_registrado, tipo_cultivo, variedad, lote_registrado, porcentaje_humedad, no_sacos, semilla_QQ_oro, peso_neto, tara, peso_lb"
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
            txtCategoria.Text = If(dt.Rows(0)("categoria_registrado") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_registrado").ToString())
            txtCultivo.Text = If(dt.Rows(0)("tipo_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_cultivo").ToString())
            txtVariedad.Text = If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString())
            TxtLote.Text = If(dt.Rows(0)("lote_registrado") Is DBNull.Value, String.Empty, dt.Rows(0)("lote_registrado").ToString())
            txtHumedad.Text = If(dt.Rows(0)("porcentaje_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("porcentaje_humedad").ToString())
            txtCantSaco.Text = If(dt.Rows(0)("no_sacos") Is DBNull.Value, String.Empty, dt.Rows(0)("no_sacos").ToString())
            txtPesoBrut.Text = If(dt.Rows(0)("semilla_QQ_oro") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_QQ_oro").ToString())
            txtPesoNeto.Text = If(dt.Rows(0)("peso_neto") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_neto").ToString())
            txtTara.Text = If(dt.Rows(0)("tara") Is DBNull.Value, String.Empty, dt.Rows(0)("tara").ToString())
            txtPesoLibr.Text = If(dt.Rows(0)("peso_lb") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_lb").ToString())

            txtCantSacoC.Text = If(dt.Rows(0)("no_sacos") Is DBNull.Value, String.Empty, dt.Rows(0)("no_sacos").ToString())
            txtCantQQ.Text = If(dt.Rows(0)("semilla_QQ_oro") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_QQ_oro").ToString())

            total()
            llenaMinigrid()
            verificar_Produc()
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
            BConfirm2.Visible = False
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If

        If (e.CommandName = "Imprimir") Then

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim rptdocument As New ReportDocument
            'nombre de dataset
            Dim ds As New DataSetMultiplicador
            Dim Str As String = "SELECT * FROM vista_ficha_informe WHERE nombre_multiplicador = @valor AND ciclo_acta = @valor2 AND id_acta = @valor3"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString)
            adap.SelectCommand.Parameters.AddWithValue("@valor2", HttpUtility.HtmlDecode(gvrow.Cells(4).Text).ToString)
            adap.SelectCommand.Parameters.AddWithValue("@valor3", Convert.ToInt32(HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString))
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "vista_ficha_informe")

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
            BConfirm2.Visible = False
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

                elminarProductos()
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
        verificar_Produc()
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
        LabelGuardar.Visible = False
        LabelGuardar.Text = ""

        If Convert.ToDecimal(txtPesoBrut.Text) = Convert.ToDecimal(txtCantQQ.Text) And Convert.ToDecimal(txtCantSaco.Text) = Convert.ToDecimal(txtCantSacoC.Text) Then

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

                    cambiarEstadoProducto(TxtID.Text)

                    Label103.Text = "¡Se ha registrado correctamente la ficha de peso al recibo lotes de semilla (pesaje y embolsado)!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    BConfirm2.Visible = False
                    BConfirm.Visible = True
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarActa.Visible = False
                    BtnImprimir.Visible = False
                    BtnNuevo.Visible = True
                    btnRegresarConficha.Visible = False
                End Using
            End Using

        Else
            Label103.Text = "¡Las cantidades de Peso Bruto y Cantidades de Quintales deben coincidir! \n ¡Las cantidades de sacos y total de sacos deben coincidir!"
            BBorrarsi.Visible = False
            BBorrarno.Visible = False
            BConfirm.Visible = False
            BConfirm2.Visible = True
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If
    End Sub
    Protected Sub Verificar()
        verificardatosproductos()
        '1
        If String.IsNullOrEmpty(txtTara.Text) Then
            lblTara.Text = "*"
            validarflag = 0
        Else
            lblTara.Text = ""
            validarflag += 1
        End If
        '2
        If verificar_Produc() = 0 Then
            lblmas.Text = "Debe ingresar la distribución de sacos y quintales correcta"
            validarflag = 0
        Else
            lblmas.Text = ""
            validarflag += 1
        End If

        If validarflag = 2 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub
    Private Sub exportar()

        Dim cadena As String = "id_acta, nombre_multiplicador, departamento, tipo_cultivo, variedad, categoria_registrado, lote_registrado, DATE_FORMAT(fecha_acta, '%d-%m-%Y') AS fecha_acta, peso_humedo_QQ, porcentaje_humedad, peso_materia_prima_QQ_porce_humedad, semilla_QQ_oro, semilla_QQ_consumo, semilla_QQ_basura, semilla_QQ_total, observaciones, ciclo_acta, peso_lb"
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
            Dim btnEditar As Button = DirectCast(e.Row.Cells(15).Controls(0), Button) ' Ajusta el índice según la posición de tu botón en la fila
            Dim btnImprimir As Button = DirectCast(e.Row.Cells(17).Controls(0), Button)

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

        If Decimal.TryParse(txtCantQQ.Text, valorOro) Then
            valorOro = Convert.ToDecimal(txtCantQQ.Text)
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
        Else
            txtPesoNeto.Text = "0.00"
        End If
    End Sub

    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/FichaPeso.aspx"))
    End Sub
    Protected Sub cambiarEstadoProducto(conocimiento As String)
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_ficha_cantidad SET
                estado = @estado
                WHERE id_ficha = " & conocimiento & ""

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@estado", "1")
                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
    End Sub
    Private Function verificar_Produc()
        Dim strCombo As String = "SELECT * FROM sag_ficha_cantidad WHERE id_ficha = " & TxtID.Text & ""
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        Return DtCombo.Rows.Count
    End Function
    Protected Sub elminarProductos()
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "UPDATE sag_ficha_cantidad 
                    SET estado = @estado
                WHERE id_ficha = " & TxtID.Text & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@estado", "3")
                cmd.ExecuteNonQuery()
                connection.Close()
            End Using

        End Using

    End Sub
    Protected Sub eliminarMiniGrid3(id As String)
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_ficha_cantidad WHERE id = " & id & ""

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

            Dim query As String = "DELETE FROM sag_ficha_cantidad WHERE estado = 0"

            Using cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
    End Sub
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim valor As Decimal
        valor = CalcularSumatoriaPesoNeto()
        valor += Convert.ToDecimal(txtCanQuinMiniGrid.Text)

        Dim valor1 As Decimal
        valor1 = CalcularSumatoriaCantSaco()
        valor1 += Convert.ToDecimal(txtCanSacMiniGrid.Text)

        Dim valor2 As Decimal
        valor2 = CalcularSumatoriaPesoLB()
        valor2 += Convert.ToDecimal(txtPesoLibMiniGrid.Text)

        If valor <= Convert.ToDecimal(txtPesoBrut.Text) Then
            txtCantQQ.Text = valor.ToString
            txtCantSacoC.Text = valor1.ToString
            txtPesoLibr.Text = valor2.ToString

            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO sag_ficha_cantidad (
                    cantidad_qq_ficha,
                    id_ficha,
                    estado,
                    peso_lb_ficha,
                    cantidad_sacos_ficha
                    ) VALUES (@cantidad_qq_ficha,
                    @id_ficha,
                    @estado,
                    @peso_lb_ficha,
                    @cantidad_sacos_ficha
                    )"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@cantidad_qq_ficha", txtCanQuinMiniGrid.Text)
                    cmd.Parameters.AddWithValue("@id_ficha", Convert.ToInt64(TxtID.Text))
                    cmd.Parameters.AddWithValue("@peso_lb_ficha", txtPesoLibMiniGrid.Text)
                    cmd.Parameters.AddWithValue("@cantidad_sacos_ficha", txtCanSacMiniGrid.Text)
                    cmd.Parameters.AddWithValue("@estado", "0")

                    cmd.ExecuteNonQuery()
                    connection.Close()
                End Using
            End Using
            llenaMinigrid()
            BtnNuevo.Visible = False
            btnRegresarConficha.Visible = True
            vaciarCamposProductos()
            verificar_Produc()
            Verificar()
        Else
            Label103.Text = "¡Las Cantidad en Quintales se excede de las de Peso Bruto!"
            BBorrarsi.Visible = False
            BBorrarno.Visible = False
            BConfirm.Visible = False
            BConfirm2.Visible = True
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If


    End Sub
    Protected Sub eliminarMiniGrid(sender As Object, e As EventArgs) Handles btnRegresarConficha.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_ficha_cantidad WHERE estado = 0"

            Using cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
                connection.Close()
                Response.Redirect(String.Format("~/pages/FichaPeso.aspx"))
            End Using
        End Using
    End Sub
    Protected Sub eliminarMiniGridEspecifico(id As String)
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_ficha_cantidad 
                WHERE id = " & id & ""

            Using cmd As New MySqlCommand(query, connection)

                cmd.ExecuteNonQuery()
                connection.Close()
            End Using
            llenaMinigrid()
        End Using
        verificar_Produc()
        Verificar()
    End Sub
    Sub verificardatosproductos()
        Dim validar As Integer = 0
        '1
        If String.IsNullOrEmpty(txtCanSacMiniGrid.Text) Then
            lblCanSacMiniGrid.Text = "*"
            validar = 0
        Else
            lblCanSacMiniGrid.Text = ""
            validar += 1
        End If
        '2
        If String.IsNullOrEmpty(txtPesoLibMiniGrid.Text) Then
            lblPesoLibMiniGrid.Text = "*"
            validar = 0
        Else
            lblPesoLibMiniGrid.Text = ""
            validar += 1
        End If
        '3
        If String.IsNullOrEmpty(txtCanQuinMiniGrid.Text) Then
            lblCanQuinMiniGrid.Text = "*"
            validar = 0
        Else
            lblCanQuinMiniGrid.Text = ""
            validar += 1
        End If

        If validar = 3 Then
            btnAgregar.Visible = True
        Else
            btnAgregar.Visible = False
        End If
    End Sub
    Sub llenaMinigrid()
        Dim cadena As String = "*"

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_ficha_cantidad` WHERE id_ficha = '" & TxtID.Text & "'"

        GridProductos.DataBind()
        Verificar()
    End Sub
    Protected Sub vaciarCamposProductos()
        txtCanSacMiniGrid.Text = ""
        txtPesoLibMiniGrid.Text = ""
        txtCanQuinMiniGrid.Text = ""
    End Sub

    Protected Sub GridProductos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridProductos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridProductos.Rows(index)
            txtidminigrid.Text = ""
            txtidminigrid.Text = HttpUtility.HtmlDecode(GridProductos.Rows(index).Cells(0).Text).ToString
            eliminarMiniGridEspecifico(txtidminigrid.Text)

            txtCanSacMiniGrid.Text = ""
            txtPesoLibMiniGrid.Text = ""
            txtCanQuinMiniGrid.Text = ""

            Dim valor As Decimal
            valor = CalcularSumatoriaPesoNeto()

            Dim valor1 As Decimal
            valor1 = CalcularSumatoriaCantSaco()

            Dim valor2 As Decimal
            valor2 = CalcularSumatoriaPesoLB()

            txtCantQQ.Text = valor.ToString
            txtCantSacoC.Text = valor1.ToString
            txtPesoLibr.Text = valor2.ToString

        End If

        If (e.CommandName = "Editar") Then
            btnAgregar.Visible = False
            Dim gvrow As GridViewRow = GridProductos.Rows(index)
            txtidminigrid.Text = ""
            txtidminigrid.Text = HttpUtility.HtmlDecode(GridProductos.Rows(index).Cells(0).Text).ToString
            Dim Str As String = "SELECT * FROM sag_ficha_cantidad WHERE  ID= " & txtidminigrid.Text & ""
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            txtCanSacMiniGrid.Text = dt.Rows(0)("cantidad_sacos_ficha").ToString()
            txtPesoLibMiniGrid.Text = dt.Rows(0)("peso_lb_ficha").ToString()
            txtCanQuinMiniGrid.Text = dt.Rows(0)("cantidad_qq_ficha").ToString()

            verificardatosproductos()
            eliminarMiniGrid3(txtidminigrid.Text)
            llenaMinigrid()

            Dim valor As Decimal
            valor = CalcularSumatoriaPesoNeto()
            Dim valor1 As Decimal
            valor1 = CalcularSumatoriaCantSaco()
            Dim valor2 As Decimal
            valor2 = CalcularSumatoriaPesoLB()

            txtCantQQ.Text = valor.ToString
            txtCantSacoC.Text = valor1.ToString
            txtPesoLibr.Text = valor2.ToString

        End If
    End Sub

    Protected Function CalcularSumatoriaPesoNeto() As Decimal
        Dim sumatoria As Decimal = 0

        ' Verificar si GridDatos tiene filas
        If GridProductos.Rows.Count > 0 Then
            ' Iterar a través de las filas del GridView
            For Each row As GridViewRow In GridProductos.Rows
                ' Encontrar el control que contiene el valor de la columna "cantidad_qq_ficha"
                Dim cantQQ As String = row.Cells(GridProductos.Columns.IndexOf(GridProductos.Columns.OfType(Of BoundField)().FirstOrDefault(Function(f) f.DataField = "cantidad_qq_ficha"))).Text

                ' Verificar si el control se encontró y el valor no está vacío
                If Not String.IsNullOrEmpty(cantQQ) Then
                    ' Convertir el valor a Decimal y sumarlo a la sumatoria
                    sumatoria += Convert.ToDecimal(cantQQ)
                Else
                    ' Si el valor está vacío, asignar 0 a la sumatoria
                    sumatoria += 0
                End If
            Next
        Else
            ' Manejar el caso en que GridDatos no tiene filas
            ' Puedes mostrar un mensaje, lanzar una excepción, o realizar alguna otra acción según tus necesidades.
        End If

        Return sumatoria
    End Function
    Protected Function CalcularSumatoriaCantSaco() As Decimal
        Dim sumatoria As Decimal = 0

        ' Verificar si GridDatos tiene filas
        If GridProductos.Rows.Count > 0 Then
            ' Iterar a través de las filas del GridView
            For Each row As GridViewRow In GridProductos.Rows
                ' Encontrar el control que contiene el valor de la columna "cantidad_qq_ficha"
                Dim cansaco As String = row.Cells(GridProductos.Columns.IndexOf(GridProductos.Columns.OfType(Of BoundField)().FirstOrDefault(Function(f) f.DataField = "cantidad_sacos_ficha"))).Text

                If Not String.IsNullOrEmpty(cansaco) Then
                    ' Convertir el valor a Decimal y sumarlo a la sumatoria
                    sumatoria += Convert.ToDecimal(cansaco)
                Else
                    ' Si el valor está vacío, asignar 0 a la sumatoria
                    sumatoria += 0
                End If

            Next
        End If

        Return sumatoria
    End Function
    Protected Function CalcularSumatoriaPesoLB() As Decimal
        Dim sumatoria As Decimal = 0

        ' Verificar si GridDatos tiene filas
        If GridProductos.Rows.Count > 0 Then
            ' Iterar a través de las filas del GridView
            For Each row As GridViewRow In GridProductos.Rows
                ' Encontrar el control que contiene el valor de la columna "cantidad_qq_ficha"
                Dim pesolb As String = row.Cells(GridProductos.Columns.IndexOf(GridProductos.Columns.OfType(Of BoundField)().FirstOrDefault(Function(f) f.DataField = "peso_lb_ficha"))).Text

                ' Verificar si el control se encontró y el valor no está vacío
                If Not String.IsNullOrEmpty(pesolb) Then
                    ' Convertir el valor a Decimal y sumarlo a la sumatoria
                    sumatoria += Convert.ToDecimal(pesolb)
                Else
                    ' Si el valor está vacío, asignar 0 a la sumatoria
                    sumatoria += 0
                End If
            Next
        End If

        Return sumatoria
    End Function

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
                Dim query As String = "UPDATE sag_registro_senasa SET acta_firmada = @acta_firmada WHERE ID=" & TxtID.Text & " "
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@acta_firmada", bytesPDF)
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
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect(String.Format("~/pages/AgregraActadeRecibo.aspx"))
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Response.Redirect(String.Format("~/pages/Acta_DescArch.aspx"))
    End Sub
End Class