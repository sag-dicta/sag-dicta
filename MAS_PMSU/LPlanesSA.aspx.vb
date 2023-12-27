Imports System.Data
Imports System.IO
Imports MySql.Data.MySqlClient
Public Class LPlanesSA
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("ConnODK").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindGridData()
    End Sub
    Protected Sub BindGridData()
        'GridView1.DataSource
        Dim con As New MySqlConnection(conn)

        con.Open()

        Dim query As String

        query = "SELECT DEPARTAMENTO, MUNICIPIO, ALDEA, CASERIO, G_JH_JH_CODIGO, G_JH_JH_NOMBRE, G_JH_JH_APELLIDO, G_JH_JH_SEXO FROM planes_sa_hogares2 "
        '" WHERE MUNICIPIO LIKE '%" & TxtBuscar.Text & "%' OR ALDEA LIKE '%" & TxtBuscar.Text & "%' OR CASERIO LIKE '%" & TxtBuscar.Text & "%' OR G_JH_JH_CODIGO LIKE '%" & TxtBuscar.Text & "%' " +
        '"OR G_JH_JH_NOMBRE LIKE '%" & TxtBuscar.Text & "%' OR G_JH_JH_APELLIDO LIKE '%" & TxtBuscar.Text & "%' "
        Dim cmd As New MySqlCommand(query, con)
        Dim da As New MySqlDataAdapter(cmd)
        Dim dt As New DataTable()

        da.Fill(dt)
        GridDatos.DataSource = dt
        GridDatos.DataBind()

        con.Close()

        'If GridDatos.Rows.Count = 0 Then
        '    Label1.Visible = True
        '    Button3.Visible = False
        '    'Agregar.Visible = True
        'Else
        '    Button3.Visible = True
        '    Label1.Visible = False
        '    'Agregar.Visible = False
        'End If
    End Sub
    Protected Sub GridDatos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridDatos.PageIndexChanging
        GridDatos.PageIndex = e.NewPageIndex
        GridDatos.DataBind()
    End Sub
End Class