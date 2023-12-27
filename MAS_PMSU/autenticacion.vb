Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.Configuration
Public NotInheritable Class Autenticacion
    Private Sub New()
    End Sub

    Public Shared Function Autenticar(ByVal usuario As String, ByVal password As String) As Boolean
        'consulta a la base de datos
        Dim sql As String = "SELECT * FROM Usuario WHERE Nombre = @user AND pass = @pass"

        'cadena conexion
        Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("connSAG").ToString())
            conn.Open()
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@user", usuario)
            cmd.Parameters.AddWithValue("@pass", password)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            'devuelve la fila afectada
            If count = 0 Then
                Return False
            Else
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                System.Web.HttpContext.Current.Session("saludo") = dt.Rows(0)("saludo").ToString
                System.Web.HttpContext.Current.Session("usutns") = dt.Rows(0)("Nombre").ToString

                System.Web.HttpContext.Current.Session("exportador") = dt.Rows(0)("exportador").ToString


                '*** llenar sesion
                Dim sql1 As String = "INSERT INTO sesiones2 (usuario, fecha) VALUES (@usuario,@fecha) "
                Dim cmd1 As New MySqlCommand(sql1, conn)
                cmd1.Parameters.AddWithValue("@usuario", dt.Rows(0)("Nombre").ToString)
                cmd1.Parameters.AddWithValue("@fecha", Date.Now)
                cmd1.ExecuteNonQuery()

                Return True
            End If
        End Using

    End Function
    Public Shared Function acceso(ByVal usuario As String) As Boolean
        'consulta a la base de datos
        Dim sql As String = "SELECT COUNT(*) FROM Usuario WHERE user_correo = @user AND user_tipo = 1"
        'cadena conexion
        Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("default").ToString())
            conn.Open()
            'abrimos conexion
            Dim cmd As New MySqlCommand(sql, conn)
            'ejecutamos la instruccion
            cmd.Parameters.AddWithValue("@user", usuario)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            'devuelve la fila afectada
            If count = 0 Then
                Return False
            Else
                Return True

            End If
        End Using
    End Function
    Public Shared Sub descarga(ByVal usuario As String, ByVal detalle As String)

        'cadena conexion
        Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("connSAG").ToString())
            conn.Open()

            '*** llenar descar
            Dim sql1 As String = "INSERT INTO sesiones2_descarga (usuario,detalle, fecha) VALUES (@usuario,@detalle,@fecha) "
            Dim cmd1 As New MySqlCommand(sql1, conn)
            cmd1.Parameters.AddWithValue("@usuario", usuario)
            cmd1.Parameters.AddWithValue("@detalle", detalle)
            cmd1.Parameters.AddWithValue("@fecha", Date.Now)
            cmd1.ExecuteNonQuery()

        End Using
    End Sub
End Class