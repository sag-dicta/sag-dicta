Imports System.IO
Imports System.Reflection.Emit
Imports System.Web.Services.Description
Imports System.Windows
Imports System.Windows.Forms
Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient
Imports Mysqlx.Cursor

Public Class Solicitudes_Productos_Individuales
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connIHMA").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True

        If (IsPostBack) Then

        Else
            Dim referrer As Uri = Request.UrlReferrer
            Dim rowIndex As String = Request.QueryString("RowIndex")
            Dim sql3 As String = "SELECT num_solicitud AS Numero_Solicitud, firma_respomsable AS Firma_Responsable, departamento AS Departamento, descripcion_bien AS Descripcion_Bien, num_catalago AS Numero_Catalago, unidad_medida AS Unidad_Medida, cantidad_solicitada AS Cantidad, sede AS Sede, DATE_FORMAT(fecha, '%d/%m/%Y') AS Fecha FROM ihma_requisicion_bienes WHERE ID = " & rowIndex.ToString
            Application("Sqlt") = sql3

            llenarConsulta(sql3)

        End If
    End Sub

    Private Sub llenarConsulta(sql As String)
        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(sql, con)
                cmd.CommandType = CommandType.Text
                Using sda As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    Dim dtSeparado As DataTable
                    dtSeparado = New DataTable()
                    dtSeparado.Columns.Add("descripcion_bien")

                    Dim descripcion As String = dt.Rows("descripcion_bien").ToString
                    Dim descripcionSeparado As String() = descripcion.Split(", ")

                    For i As Integer = 0 To descripcionSeparado.Length - 1
                        dtSeparado.Rows(0)("descripcion_bien").Add(descripcionSeparado(i))
                    Next
                    dt = dtSeparado.Copy()
                    GridView1.DataSource = dt
                    GridView1.DataBind()

                End Using
            End Using
        End Using
    End Sub

    Private Sub mostrarRequisicionUnica(sql As String)
        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(sql, con)
                cmd.CommandType = CommandType.Text
                Using sda As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    ' Separar los valores de las columnas
                    Dim dtSeparado As New DataTable()
                    dtSeparado.Columns.Add("Descripcion_bien")

                    Dim descripcion As String = dt.Rows("descripcion_bien").ToString()

                    Dim descripcionSeparado As String() = descripcion.Split(", ")

                    For i As Integer = 0 To descripcionSeparado.Length - 1
                        dtSeparado.Rows.Add(descripcionSeparado(i))
                    Next

                    GridView1.DataSource = dtSeparado
                    GridView1.DataBind()
                End Using
            End Using
        End Using

    End Sub
    Private Function obtenerNumeroRegistros(sql As String) As Integer
        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(sql, con)
                cmd.CommandType = CommandType.Text
                con.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Protected Sub OnPaging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Me.llenarConsulta(Application("Sqlt"))
    End Sub
    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnEditar As LinkButton = CType(sender, LinkButton)
        Dim rowIndex As Integer = Integer.Parse(btnEditar.CommandArgument)
        Response.Redirect("~/pages/EditarProducto.aspx?RowIndex=" & rowIndex)
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btnEliminar As ImageButton = CType(sender, ImageButton)
        Dim argumento As String = btnEliminar.CommandArgument
        Dim valores As String() = argumento.Split("-") ' Desconcatenar los valores
        Dim ID As Integer = Integer.Parse(valores(0))
        Dim cantidad As Integer = Integer.Parse(valores(1))
        Dim nombre_P As String = valores(2)
        Dim tipo As String = valores(3)

        eliminarConsulta(ID, cantidad, nombre_P, tipo)
        Response.Write("<script>window.alert('¡El producto: " & nombre_P & ", con la cantidad: " & cantidad & " unidades y con el registro ID: " & ID & ", ha sido eliminado de la lista de consulta de producto!');
        window.location = '" & Request.RawUrl & "';
        </script>")
    End Sub

    Protected Function eliminarConsulta(ByVal ID As Integer, ByVal Cantidad As Integer, ByVal nombre_P As String, ByVal tipo As String)
        Dim conex As New MySqlConnection(conn)
        Dim cmd As New MySqlCommand()
        Dim cmd2 As New MySqlCommand()

        Dim fecha2 As DateTime = DateTime.Now
        Dim detalle As String = "se elimino un producto con el ID: " & ID.ToString & ", por el usuario con nombre: '" & System.Web.HttpContext.Current.Session("Nombre").ToString & "' con ID: '" & System.Web.HttpContext.Current.Session("id").ToString & "'"
        Dim usuario As String = System.Web.HttpContext.Current.Session("Nombre").ToString
        Dim departmento As String = System.Web.HttpContext.Current.Session("departamento").ToString

        conex.Open()

        cmd.Connection = conex
        cmd2.Connection = conex

        cmd.CommandText = "UPDATE ihma_inventariado_proveduria SET visible = 0
                            WHERE ID = '" & ID & "'"

        If (tipo = "Entradas (Compras)") Then
            cmd2.CommandText = "UPDATE ihma_inventario_producto SET cantidad = IFNULL(cantidad, 0) - " & Cantidad & "
                            WHERE nombre_P = '" & nombre_P & "'"
            cmd2.ExecuteNonQuery()
        End If

        If (tipo = "Salidas (Requisiciones)") Then
            cmd2.CommandText = "UPDATE ihma_inventario_producto SET cantidad = IFNULL(cantidad, 0) + " & Cantidad & "
                            WHERE nombre_P = '" & nombre_P & "'"
            cmd2.ExecuteNonQuery()
        End If

        Autenticacion.accionRegistro(fecha2, detalle, usuario, departmento, "Eliminar")



        cmd.ExecuteNonQuery()

        conex.Close()
        Return ""
    End Function

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs)
        Dim query As String = Application("Sqlt")
        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()

                        ds.Tables.Add(New DataTable())
                        sda.Fill(ds.Tables(0))

                        'Set Name of DataTables.
                        'ds.Tables(0).TableName = BsqSede.SelectedItem.Text.ToString()

                        Using wb As New XLWorkbook()
                            For i As Integer = 0 To ds.Tables.Count - 1
                                Dim dt As DataTable = ds.Tables(i)
                                Dim ws As IXLWorksheet = wb.Worksheets.Add(dt)
                                ws.Name = dt.TableName & " (" & (i + 1).ToString() & ")"
                                ' Agregar un título a la tabla
                                ws.Row(1).InsertRowsAbove(1)
                                'ws.Cell(1, 1).Value = "Productos de " & BsqSede.SelectedItem.Text.ToString()
                                ws.Range("A1:" & ExcelColumnFromNumber(dt.Columns.Count) & "1").Merge()
                                ws.Cell(1, 1).Style.Font.FontSize = 16
                                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                ws.Cell(1, 1).Style.Font.Bold = True

                            Next

                            'Export the Excel file.
                            Response.Clear()
                            Response.Buffer = True
                            Response.Charset = ""
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            Response.AddHeader("content-disposition", "attachment;filename=Inventario_IHMA " & Now().ToString("yyyy-MM-dd HH-mm-ss") & ".xlsx")
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

    Private Function ExcelColumnFromNumber(column As Integer) As String
        Dim columnString As String = ""
        Dim columnNumber As Integer = column
        While columnNumber > 0
            Dim currentLetterNumber As Integer = (columnNumber - 1) Mod 26
            Dim currentLetter As Char = Chr(currentLetterNumber + 65)
            columnString = currentLetter + columnString
            columnNumber = (columnNumber - currentLetterNumber) \ 26
        End While
        Return columnString
    End Function

End Class