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
                llenagrid()
            End If
        End If
    End Sub
    Protected Sub vaciar(sender As Object, e As EventArgs)
        Response.Redirect("ActaRecepcionSemilla.aspx")
    End Sub

    Sub llenagrid()
        Dim cadena As String = "id, nombre, DNI, telefono, tipo, marca, color, no_placa, estado, CodVehi"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND nombre = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (DDL_SelCult.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND tipo = '" & DDL_SelCult.SelectedItem.Text & "' "
        End If

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `sag_registro_vehiculo_motorista` WHERE 1 = 1 AND estado = '1' " & c3 & c4

        GridDatos.DataBind()
    End Sub
    Protected Sub TxtProductorGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtProductorGrid.SelectedIndexChanged
        llenagrid()
    End Sub
    Protected Sub DDL_SelCult_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDL_SelCult.SelectedIndexChanged
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
        End If
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


    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM solicitud_inscripcion_delotes WHERE nombre_lote = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", txtProductor.Text)
        Dim dt As New DataTable
        adap.Fill(ds, "solicitud_inscripcion_delotes")
        Dim nombre As String

        nombre = "Solicitud Inscripcion de Lote o Campo _" + Today
        rptdocument.Load(Server.MapPath("~/pages/Solicitud Inscripcion de Lote.rpt"))
        rptdocument.SetDataSource(ds)
        Response.Buffer = False
        Response.ClearContent()
        Response.ClearHeaders()
        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub

    Protected Sub btnGuardarActa_Click(sender As Object, e As EventArgs)
        ' Verifica si los elementos no están vacíos
        If Not String.IsNullOrWhiteSpace(txtFechaSiembra.Text) AndAlso
            Not String.IsNullOrWhiteSpace(txtProductor.Text) AndAlso
            Not String.IsNullOrWhiteSpace(txtProductor.Text) Then

            validarflag = 0
            Verificar()
            If validarflag = 0 Then
                btnGuardarActa.Visible = False
                BtnImprimir.Visible = False
                BtnNuevo.Visible = True

                'Funcion para guardar en la BD
                GuardarActa()

            Else
                Label103.Text = "Debe ingresar toda la informacion primero"
                ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
            End If

        Else
            ' Si algún elemento está vacío, muestra mensajes en las Label correspondientes
            'If String.IsNullOrWhiteSpace(txtFechaSiembra.Text) Then
            '    Label14.Text = "Fecha de recepción es obligatoria"
            'Else
            '    Label14.Text = ""
            'End If

            'If String.IsNullOrWhiteSpace(txt_nombre_prod_new.Text) Then
            '    lb_nombre_new.Text = "Nombre del productor es obligatorio"
            'Else
            '    lb_nombre_new.Text = ""
            'End If

            'If String.IsNullOrWhiteSpace(TxtCeduIden.Text) Then
            '    Label1.Text = "Cédula de Identidad es obligatoria"
            'Else
            '    Label1.Text = ""
            'End If

            'If String.IsNullOrWhiteSpace(DDL_cultivo.SelectedValue) Then
            '    Label104.Text = "Tipo de cultivo es obligatorio"
            'Else
            '    Label104.Text = ""
            'End If
            Label103.Text = "Debe ingresar toda la informacion primero"
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If
    End Sub

    Protected Sub Verificar()

    End Sub

    Protected Sub GuardarActa()
        Dim conex As New MySqlConnection(conn)
        Dim fecha As Date

        If Date.TryParse(txtFechaSiembra.Text, fecha) Then
            fecha.ToString("dd-MM-yyyy")
        End If

        conex.Open()

        Dim cmd2 As New MySqlCommand()

        If btnGuardarActa.Text = "Guardar" Then



        End If

        If btnGuardarActa.Text = "Editar" Then



        End If
    End Sub

    Private Sub exportar()

        Dim query As String = ""
        Dim cadena As String = "id, nombre, DNI, telefono, tipo, marca, color, no_placa, estado, CodVehi"
        Dim c1 As String = ""
        Dim c4 As String = ""
        Dim c3 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND nombre = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (DDL_SelCult.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND tipo = '" & DDL_SelCult.SelectedItem.Text & "' "
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
                            Response.AddHeader("content-disposition", "attachment;filename=Registro de motorista  " & Today & " " & TxtProductorGrid.SelectedItem.Text & ".xlsx")
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

End Class