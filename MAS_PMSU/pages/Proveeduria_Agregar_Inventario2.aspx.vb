Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Web.Configuration
Imports System.Windows.Controls
Imports MySql.Data.MySqlClient

Public Class Proveeduria_Agregar_Inventario2
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connIHMA").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True

        If (IsPostBack) Then

        Else
            llenarProducto()
            llenarTipo()
            llenarSede()
            llenarDepartamento()
            Prodname_SelectedIndexChanged()
            llenarProveedor()
            Prodprov_SelectedIndexChanged()
            Llenar_ID_Prod()
            txtFecha.Text = DateTime.Today.ToShortDateString()
            calFecha.SelectedDate = DateTime.Today
        End If
    End Sub

    Protected Sub Prodname_SelectedIndexChanged() Handles Prodname.SelectedIndexChanged
        If (Prodname.SelectedItem.Text = "Producto nuevo") Then
            txtInputProdname.Enabled = True
            txtInputProdname.Visible = True
            lblInputProdname.Visible = True
            validadorProdname.Enabled = True
        Else
            txtInputProdname.Enabled = False
            txtInputProdname.Visible = False
            lblInputProdname.Visible = False
            validadorProdname.Enabled = False
            txtInputProdname.Text = ""
        End If
        lbladver1.Visible = False
    End Sub

    Protected Function GuardarProductos()
        Dim conex As New MySqlConnection(conn)
        Dim cmd As New MySqlCommand()
        Dim cmd5 As New MySqlCommand()


        Dim fecha As Date = calFecha.SelectedDate
        Dim fechasql = fecha.ToString("yyyy-MM-dd")

        Dim fecha2 As DateTime = DateTime.Now

        Dim detalle As String = "se agrego un producto a la lista de consulta con numero de ID: " & ProdID.Text & ", por el usuario con nombre: '" & System.Web.HttpContext.Current.Session("Nombre").ToString & "' con ID: '" & System.Web.HttpContext.Current.Session("id").ToString & "'"
        Dim detalleNewProd As String = "se agrego un producto nuevo con nombre de: " & txtInputProdname.Text & ", por el usuario con nombre: '" & System.Web.HttpContext.Current.Session("Nombre").ToString & "' con ID: '" & System.Web.HttpContext.Current.Session("id").ToString & "'"
        Dim detalleNewProv As String = "se agrego un proveedor: " & txtInputProdprov.Text & ", por el usuario con nombre: '" & System.Web.HttpContext.Current.Session("Nombre").ToString & "' con ID: '" & System.Web.HttpContext.Current.Session("id").ToString & "'"
        Dim usuario As String = System.Web.HttpContext.Current.Session("Nombre").ToString
        Dim departmento As String = System.Web.HttpContext.Current.Session("departamento").ToString

        conex.Open()

        Dim prodnomb
        Dim provnomb

        Dim band1 As Short
        Dim band2 As Short

        If (txtInputProdname.Enabled = True) Then
            If (existeProduc(txtInputProdname.Text) = 0) Then
                prodnomb = txtInputProdname.Text
                cmd.Connection = conex

                cmd.CommandText = "INSERT INTO ihma_inventario_producto (nombre_P, visible, cantidad) VALUES (@nombre_P, 1, @cantidadProd)"
                cmd.Parameters.AddWithValue("nombre_P", prodnomb)
                cmd.Parameters.AddWithValue("@cantidadProd", Convert.ToInt32(Prodcant.Text))

                Autenticacion.accionRegistro(fecha2, detalleNewProd, usuario, departmento, "Agregar")

                cmd.ExecuteNonQuery()
                band1 = 1
            Else
                lbladver1.Visible = True
                band1 = 0
                Response.Write("<script>window.alert('El producto ingresado ya esta en la lista de productos!');</script>")
            End If
        Else
            prodnomb = Prodname.SelectedItem.Text
            band1 = 1
        End If

        If existeProduc(Prodname.SelectedItem.Text) <> 0 And DropDownList1.SelectedItem.Text = "Entradas (Compras)" Then
            cmd5.Connection = conex

            cmd5.CommandText = "UPDATE ihma_inventario_producto SET cantidad = IFNULL(cantidad, 0) + @cantidadProducto WHERE nombre_P = @nombreProd"

            cmd5.Parameters.AddWithValue("@cantidadProducto", Convert.ToInt32(Prodcant.Text))
            cmd5.Parameters.AddWithValue("@nombreProd", Prodname.SelectedItem.Text)

            cmd5.ExecuteNonQuery()

        End If

        If existeProduc(Prodname.SelectedItem.Text) <> 0 And DropDownList1.SelectedItem.Text = "Salidas (Requisiciones)" Then
            cmd5.Connection = conex



            Dim StrCOmbo As String = "SELECT IFNULL(cantidad, 0) FROM ihma_inventario_producto WHERE nombre_P = @nombre_Producto"
            Dim adaptcombo As New MySqlDataAdapter(StrCOmbo, conn)
            adaptcombo.SelectCommand.Parameters.AddWithValue("@nombre_Producto", Prodname.SelectedItem.Text)
            Dim resultado As New DataTable()
            adaptcombo.Fill(resultado)

            If Convert.ToInt32(resultado.Rows(0)(0)) > 0 Then

                If Convert.ToInt32(resultado.Rows(0)(0)) >= Convert.ToInt32(Prodcant.Text) Then
                    cmd5.CommandText = "UPDATE ihma_inventario_producto SET cantidad = IFNULL(cantidad, 0) - @cantidadProducto WHERE nombre_P = @nombreProd"

                    cmd5.Parameters.AddWithValue("@cantidadProducto", Convert.ToInt32(Prodcant.Text))
                    cmd5.Parameters.AddWithValue("@nombreProd", Prodname.SelectedItem.Text)

                    cmd5.ExecuteNonQuery()
                End If

                If Convert.ToInt32(resultado.Rows(0)(0)) < Convert.ToInt32(Prodcant.Text) Then
                    cmd5.CommandText = "UPDATE ihma_inventario_producto SET cantidad = IFNULL(cantidad, 0) - @cantidadProducto1 WHERE nombre_P = @nombreProd1"

                    cmd5.Parameters.AddWithValue("@cantidadProducto1", Convert.ToInt32(resultado.Rows(0)(0)))
                    cmd5.Parameters.AddWithValue("@nombreProd1", Prodname.SelectedItem.Text)

                    cmd5.ExecuteNonQuery()

                    Response.Write("<script>window.alert('¡La cantidad solicitada del producto es de: " & Prodcant.Text & " unidades, se excede con la disponibilidad del inventario que es de: " & resultado.Rows(0)(0).ToString & " unidades! Se le proveera solo lo existente en inventario.');
                    </script>")

                    Prodcant.Text = resultado.Rows(0)(0).ToString
                End If
            Else
                Response.Write("<script>window.alert('¡No hay existencia del productos " & Prodname.SelectedItem.Text & ": " & resultado.Rows(0)(0).ToString & " unidades!');
                 </script>")
                VaciarCasillas()
                Return 0
            End If
        End If

        If (txtInputProdprov.Enabled = True) Then
            If (existeProv(txtInputProdprov.Text) = 0) Then
                provnomb = txtInputProdprov.Text
                cmd.Connection = conex

                cmd.CommandText = "INSERT INTO ihma_inventario_proveedor (proveedor, visible) VALUES (@Proveedor, 1)"
                cmd.Parameters.AddWithValue("@proveedor", provnomb)

                Autenticacion.accionRegistro(fecha2, detalleNewProv, usuario, departmento, "Agregar")

                cmd.ExecuteNonQuery()
                band2 = 1
            Else
                lbladver2.Visible = True
                band2 = 0
                Response.Write("<script>window.alert('El proveedor ingresado ya esta en la lista de proveedores!');</script>")
            End If
        Else
            provnomb = Prodprov.SelectedItem.Text
            band2 = 1
        End If


        If (band1 = 1 And band2 = 1) Then
            cmd.Connection = conex

            cmd.CommandText = "INSERT INTO ihma_inventariado_proveduria 
                            (ID_producto, nom_Product, descrip_Product, sede, Departamento, num_Factura, num_Requisicion, proveedor, cantidad, precio_Unitario, tipo, fecha) VALUES (@ID_producto, @nom_Product, @descrip_Product, @sede, @Departamento, @num_Factura, @num_Requisicion, @prov, @cantidad, @precio_Unitario, @tipo, '" & fechasql & "')"

            cmd.Parameters.AddWithValue("@ID_producto", Convert.ToInt32(ProdID.Text))
            cmd.Parameters.AddWithValue("@nom_Product", prodnomb)
            cmd.Parameters.AddWithValue("@descrip_Product", Proddesc.Text)
            cmd.Parameters.AddWithValue("@sede", DropDownList2.SelectedItem.Text)
            cmd.Parameters.AddWithValue("@Departamento", Proddepto.SelectedItem.Text)
            cmd.Parameters.AddWithValue("@num_Factura", Prodfact.Text)
            cmd.Parameters.AddWithValue("@num_Requisicion", Prodrequi.Text)
            cmd.Parameters.AddWithValue("@prov", provnomb)
            cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(Prodcant.Text))
            cmd.Parameters.AddWithValue("@precio_Unitario", Convert.ToInt32(Prodpreuni.Text))
            cmd.Parameters.AddWithValue("@tipo", DropDownList1.SelectedItem.Text)
            cmd.Parameters.AddWithValue("@fecha", fechasql)

            Autenticacion.accionRegistro(fecha2, detalle, usuario, departmento, "Agregar")

            cmd.ExecuteNonQuery()

            conex.Close()

            VaciarCasillas()
            Response.Write("<script>alert('Registro almacenado con éxito!'); window.location='" + Request.RawUrl + "';</script>")
            band1 = 0
            band2 = 0

        End If
        Return 0
    End Function
    Protected Sub Btn_cancel_Click(sender As Object, e As EventArgs)
        VaciarCasillas()
    End Sub

    Protected Sub Prodprov_SelectedIndexChanged() Handles Prodprov.SelectedIndexChanged
        If (Prodprov.SelectedItem.Text = "Nuevo Proveedor") Then
            txtInputProdprov.Enabled = True
            txtInputProdprov.Visible = True
            lblInputProdprov.Visible = True
            validatorProdprov.Enabled = True
        Else
            txtInputProdprov.Enabled = False
            txtInputProdprov.Visible = False
            lblInputProdprov.Visible = False
            validatorProdprov.Enabled = False
            txtInputProdprov.Text = ""
        End If
        lbladver2.Visible = False
    End Sub

    Protected Sub Validar(sender As Object, e As EventArgs) Handles Btn_save.Click

        If (txtInputProdname.Visible And validadorProdname.Enabled = False) Then
            validadorProdname.Enabled = True
            validadorProdname.Validate()
        End If

        If (txtInputProdprov.Visible And validatorProdprov.Enabled = False) Then
            validatorProdprov.Enabled = True
            validatorProdprov.Validate()
        End If

        If (Page.IsValid) Then
            GuardarProductos()
        Else
            Response.Write("<script>window.alert('Algunos campos tienen valores invalidos!');</script>")
        End If
    End Sub
    Protected Sub BtnCalend_Click() Handles Btncalen.Click
        If calFecha.Visible = True Then
            calFecha.Visible = False
            calFecha.Enabled = False
        Else
            calFecha.Visible = True
            calFecha.Enabled = True
        End If
    End Sub

    Protected Sub CalFecha_OnSelectionChanged(sender As Object, e As EventArgs) Handles calFecha.SelectionChanged
        txtFecha.Text = calFecha.SelectedDate.ToShortDateString()
        calFecha.Visible = False
    End Sub

    Private Sub llenarProducto()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_producto WHERE visible = 1 ORDER BY nombre_P"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        Prodname.DataSource = DtCombo
        Prodname.DataValueField = DtCombo.Columns(0).ToString()
        Prodname.DataTextField = DtCombo.Columns(1).ToString()
        Prodname.DataBind()
        Dim newItem As New ListItem("Producto nuevo", "Producto nuevo")
        Prodname.Items.Insert(0, newItem)
    End Sub

    Private Sub llenarSede()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_sede WHERE Visible = 1 ORDER BY sede ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        DropDownList2.DataSource = DtCombo
        DropDownList2.DataValueField = DtCombo.Columns(0).ToString()
        DropDownList2.DataTextField = DtCombo.Columns(1).ToString()
        DropDownList2.DataBind()
        Dim newItem As New ListItem("Todas las sedes", "Todas las sedes")
        DropDownList2.Items.Insert(0, newItem)
    End Sub

    Private Sub llenarDepartamento()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_departamento WHERE Visible = 1 ORDER BY departamento ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        Proddepto.DataSource = DtCombo
        Proddepto.DataValueField = DtCombo.Columns(0).ToString()
        Proddepto.DataTextField = DtCombo.Columns(1).ToString()
        Proddepto.DataBind()
        Dim newItem As New ListItem("Todos los departamentos", "Todos los departamentos")
        Proddepto.Items.Insert(0, newItem)
    End Sub

    Private Sub llenarTipo()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_tipo"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        DropDownList1.DataSource = DtCombo
        DropDownList1.DataValueField = DtCombo.Columns(0).ToString()
        DropDownList1.DataTextField = DtCombo.Columns(1).ToString()
        DropDownList1.DataBind()
        Dim newItem As New ListItem("Seleccione el tipo", "Seleccione el tipo")
        DropDownList1.Items.Insert(0, newItem)
    End Sub

    Private Function existeProduc(ByVal valor As String)
        Dim StrCOmbo As String = "SELECT COUNT(*) FROM ihma_inventario_producto WHERE nombre_P = @valor"
        Dim adaptcombo As New MySqlDataAdapter(StrCOmbo, conn)
        adaptcombo.SelectCommand.Parameters.AddWithValue("@valor", valor)
        Dim resultado As New DataTable()
        adaptcombo.Fill(resultado)

        Return Convert.ToInt32(resultado.Rows(0)(0)) > 0
    End Function

    Private Function existeProv(ByVal valor As String)
        Dim StrCOmbo As String = "SELECT COUNT(*) FROM ihma_inventario_proveedor WHERE proveedor = @valor"
        Dim adaptcombo As New MySqlDataAdapter(StrCOmbo, conn)
        adaptcombo.SelectCommand.Parameters.AddWithValue("@valor", valor)
        Dim resultado As New DataTable()
        adaptcombo.Fill(resultado)

        Return Convert.ToInt32(resultado.Rows(0)(0)) > 0
    End Function
    Private Sub llenarProveedor()
        Dim StrCombo As String = "SELECT * FROM ihma_inventario_proveedor WHERE visible = 1 ORDER BY proveedor ASC "
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)
        Prodprov.DataSource = DtCombo
        Prodprov.DataValueField = DtCombo.Columns(0).ToString()
        Prodprov.DataTextField = DtCombo.Columns(1).ToString()
        Prodprov.DataBind()
        Dim newItem As New ListItem("Nuevo Proveedor", "Nuevo Proveedor")
        Prodprov.Items.Insert(0, newItem)
    End Sub

    Private Sub Llenar_ID_Prod()
        Dim strCombo As String = "SELECT COUNT(ID_producto) FROM ihma_inventariado_proveduria"
        Dim adaptcombo As New MySqlDataAdapter(strCombo, conn)
        Dim DtCombo As New DataTable()
        adaptcombo.Fill(DtCombo)

        If DtCombo.Rows.Count > 0 AndAlso DtCombo.Columns.Count > 0 Then
            Dim total As Integer = Convert.ToInt32(DtCombo.Rows(0)(0))
            total += 1
            ProdID.Text = total.ToString()
        Else
            Dim total1 As Integer = 1
            ProdID.Text = total1.ToString()
        End If
    End Sub

    Private Sub VaciarCasillas()
        txtInputProdname.Text = ""
        Proddesc.Text = ""
        Prodfact.Text = ""
        Prodrequi.Text = ""
        txtInputProdprov.Text = ""
        Prodcant.Text = ""
        Prodpreuni.Text = ""
        txtFecha.Text = DateTime.Today.ToShortDateString()
        lbladver1.Visible = False
        lbladver2.Visible = False
    End Sub

End Class