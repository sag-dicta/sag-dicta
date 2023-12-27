Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Reflection.Emit
Imports System.Web.Services.Description
Imports System.Windows
Imports System.Windows.Forms
Imports Mysqlx.Cursor

Public Class Consulta_acciones_registros
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connIHMA").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True

        If (IsPostBack) Then

        Else
            Dim sql3 As String = "SELECT ID_producto AS ID, nom_Product AS Producto, descrip_Product AS Descripcion, sede AS Sede, Departamento, num_Factura AS Factura, num_Requisicion AS Requisicion, proveedor AS Proveedor, cantidad AS Cantidad, precio_Unitario AS Precio, cantidad*precio_Unitario AS Total, tipo AS Tipo, DATE_FORMAT(fecha, '%d/%m/%Y') AS Fecha FROM ihma_inventariado_proveduria WHERE visible = 1"
            Application("Sqlt") = sql3

            llenarConsulta(sql3)
            llenarDepartamento()
            llenarProducto()
            llenarSede()
            llenarTipo()
            calFecha1.SelectedDate = Date.Parse("01/01/2000")
            calFecha2.SelectedDate = DateTime.Today

            txtFecha1.Text = calFecha1.SelectedDate
            txtFecha2.Text = calFecha2.SelectedDate

        End If
    End Sub

    Private Sub llenarConsulta(sql As String)
        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(sql, con)
                cmd.CommandType = CommandType.Text
                Using sda As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()

                    Dim sql2 As String = sql
                    Dim sql3 As String = "SELECT COUNT(*) " & sql2.Substring(sql2.IndexOf("FROM") - 4)
                    lblcanreg.Text = "Cantidad de Registros: " & obtenerNumeroRegistros(sql3).ToString

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

    Private Sub llenarProducto()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_producto WHERE nombre_P != 'Producto nuevo' AND visible = 1 ORDER BY nombre_P ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        BsqProd.DataSource = DtCombo
        BsqProd.DataValueField = DtCombo.Columns(0).ToString()
        BsqProd.DataTextField = DtCombo.Columns(1).ToString()
        BsqProd.DataBind()
        Dim newItem As New ListItem("Todos los productos", "Todos los productos")
        BsqProd.Items.Insert(0, newItem)
    End Sub

    Private Sub llenarSede()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_sede WHERE Visible = 1 ORDER BY sede ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        BsqSede.DataSource = DtCombo
        BsqSede.DataValueField = DtCombo.Columns(0).ToString()
        BsqSede.DataTextField = DtCombo.Columns(1).ToString()
        BsqSede.DataBind()
        Dim newItem As New ListItem("Todas las sedes", "Todas las sedes")
        BsqSede.Items.Insert(0, newItem)
    End Sub

    Private Sub llenarDepartamento()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_departamento WHERE Visible = 1 ORDER BY departamento ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        BsqDepto.DataSource = DtCombo
        BsqDepto.DataValueField = DtCombo.Columns(0).ToString()
        BsqDepto.DataTextField = DtCombo.Columns(1).ToString()
        BsqDepto.DataBind()
        Dim newItem As New ListItem("Todos los departamentos", "Todos los departamentos")
        BsqDepto.Items.Insert(0, newItem)
    End Sub
    Private Sub llenarTipo()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_tipo WHERE Visible = 1 ORDER BY tipo ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        BsqTipo.DataSource = DtCombo
        BsqTipo.DataValueField = DtCombo.Columns(0).ToString()
        BsqTipo.DataTextField = DtCombo.Columns(1).ToString()
        BsqTipo.DataBind()
        Dim newItem As New ListItem("Todos", "Todos")
        BsqTipo.Items.Insert(0, newItem)
    End Sub

    Protected Sub Bsq_SelectedIndexChanged()
        Dim sql1 As String
        Dim depto As String
        Dim sede As String
        Dim prod As String
        Dim tipo As String
        Dim sql2 As String
        Dim fechaF As String

        Dim fecha1 As Date = calFecha1.SelectedDate
        Dim fecha2 As Date = calFecha2.SelectedDate

        Dim fechasql1 = fecha1.ToString("yyyy-MM-dd")
        Dim fechasql2 = fecha2.ToString("yyyy-MM-dd")

        sql1 = "SELECT ID_producto AS ID,
                    nom_Product AS Producto, 
                    descrip_Product AS Descripcion, 
                    sede AS Sede, 
                    Departamento, 
                    num_Factura AS Factura, 
                    num_Requisicion AS Requisicion, 
                    proveedor AS Proveedor, 
                    cantidad AS Cantidad, 
                    precio_Unitario AS Precio,
                    cantidad*precio_Unitario AS Total,
                    tipo AS Tipo, 
                    DATE_FORMAT(fecha, '%d/%m/%Y') AS Fecha
            FROM ihma_inventariado_proveduria 
            WHERE 1=1 AND visible = 1"

        If (BsqDepto.SelectedItem.Text <> "Todos los departamentos") Then
            depto = " AND departamento = '" & BsqDepto.SelectedItem.Text & "'"
        Else
            depto = ""
        End If

        If (BsqSede.SelectedItem.Text = "Todas las sedes") Then
            sede = ""
        Else
            sede = " AND sede = '" & BsqSede.SelectedItem.Text & "'"
        End If

        If (BsqProd.SelectedItem.Text = "Todos los productos") Then
            prod = ""
        Else
            prod = " AND nom_Product = '" & BsqProd.SelectedItem.Text & "'"
        End If

        If (BsqTipo.SelectedItem.Text = "Todos") Then
            tipo = ""
        Else
            tipo = " AND tipo = '" & BsqTipo.SelectedItem.Text & "'"
        End If

        If txtFecha1.Text <> "" And txtFecha2.Text <> "" Then
            fechaF = " AND fecha BETWEEN '" & fechasql1 & "' AND '" & fechasql2 & "'"
        Else
            fechaF = ""
        End If

        sql2 = sql1 & depto & sede & prod & tipo & fechaF

        llenarConsulta(sql2)
        Application("Sqlt") = sql2
    End Sub

    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        Dim sql As String

        sql = "SELECT ID_producto AS ID,
                    nom_Product AS Producto, 
                    descrip_Product AS Descripcion, 
                    sede AS Sede, 
                    Departamento, 
                    num_Factura AS Factura, 
                    num_Requisicion AS Requisicion, 
                    proveedor AS Proveedor, 
                    cantidad AS Cantidad, 
                    precio_Unitario AS Precio,
                    cantidad*precio_Unitario AS Total,
                    tipo AS Tipo, 
                    DATE_FORMAT(fecha, '%d/%m/%Y') AS Fecha
            FROM ihma_inventariado_proveduria 
            WHERE descrip_Product LIKE '%" & txtSearch.Text & "%' AND visible = 1"

        llenarConsulta(sql)
        Application("Sqlt") = sql
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnEditar As ImageButton = CType(sender, ImageButton)
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

        'If Windows.MessageBox.Show("Aviso: ¿Desea eliminar el producto?", "Eliminación", MessageBoxButton.YesNo) = DialogResult.Yes Then
        '    eliminarConsulta(ID, cantidad, nombre_P, tipo)
        '    Response.Redirect(Request.RawUrl)
        'Else
        '    Windows.MessageBox.Show("¡Se redirigira a la pagina de consulta!")
        '    Response.Redirect(Request.RawUrl)
        'End If

        'Response.Write("<script>
        '    if (confirm('Aviso: ¿Desea eliminar el producto?')){
        '        " & eliminarConsulta(ID, cantidad, nombre_P, tipo) & "
        '        window.location = '" & Request.RawUrl & "';
        '    } else {
        '        alert('¡No se cancelo el registro! Sera dirigido a la pagina de consulta');
        '        window.location = '" & Request.RawUrl & "';
        '    }
        '</script>")
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
                        ds.Tables(0).TableName = BsqSede.SelectedItem.Text.ToString()

                        Using wb As New XLWorkbook()
                            For i As Integer = 0 To ds.Tables.Count - 1
                                Dim dt As DataTable = ds.Tables(i)
                                Dim ws As IXLWorksheet = wb.Worksheets.Add(dt)
                                ws.Name = dt.TableName & " (" & (i + 1).ToString() & ")"
                                ' Agregar un título a la tabla
                                ws.Row(1).InsertRowsAbove(1)
                                ws.Cell(1, 1).Value = "Productos de " & BsqSede.SelectedItem.Text.ToString()
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

    Protected Sub CalFecha1_OnSelectionChanged(sender As Object, e As EventArgs) Handles calFecha1.SelectionChanged
        txtFecha1.Text = calFecha1.SelectedDate.ToShortDateString()
        calFecha1.Visible = False
        Bsq_SelectedIndexChanged()
    End Sub
    Protected Sub BtnCalend1_Click() Handles Btncalen1.Click
        If calFecha1.Visible = True Then
            calFecha1.Visible = False
            calFecha1.Enabled = False
            calFecha2.Visible = False
            calFecha2.Enabled = False
        Else
            calFecha1.Visible = True
            calFecha1.Enabled = True
            calFecha2.Visible = False
            calFecha2.Enabled = False
        End If
    End Sub
    Protected Sub CalFecha2_OnSelectionChanged(sender As Object, e As EventArgs) Handles calFecha2.SelectionChanged
        txtFecha2.Text = calFecha2.SelectedDate.ToShortDateString()
        calFecha2.Visible = False
        Bsq_SelectedIndexChanged()
    End Sub
    Protected Sub BtnCalend2_Click() Handles Btncalen2.Click
        If calFecha2.Visible = True Then
            calFecha2.Visible = False
            calFecha2.Enabled = False
            calFecha1.Visible = False
            calFecha1.Enabled = False
        Else
            calFecha2.Visible = True
            calFecha2.Enabled = True
            calFecha1.Visible = False
            calFecha1.Enabled = False
        End If
    End Sub

    Private Sub RestablecerFiltros() Handles btnRest.Click
        Dim sql3 As String = "SELECT ID_producto AS ID, nom_Product AS Producto, descrip_Product AS Descripcion, sede AS Sede, Departamento, num_Factura AS Factura, num_Requisicion AS Requisicion, proveedor AS Proveedor, cantidad AS Cantidad, precio_Unitario AS Precio, cantidad*precio_Unitario AS Total, tipo AS Tipo, DATE_FORMAT(fecha, '%d/%m/%Y') AS Fecha FROM ihma_inventariado_proveduria WHERE visible = 1"
        Application("Sqlt") = sql3

        llenarConsulta(sql3)
        llenarDepartamento()
        llenarProducto()
        llenarSede()
        llenarTipo()
        calFecha1.SelectedDate = Date.Parse("01/01/2000")
        calFecha2.SelectedDate = DateTime.Today

        txtFecha1.Text = calFecha1.SelectedDate
        txtFecha2.Text = calFecha2.SelectedDate
    End Sub

End Class




Public Class Proveeduria_Consultar_Inventario
    Inherits System.Web.UI.Page


End Class