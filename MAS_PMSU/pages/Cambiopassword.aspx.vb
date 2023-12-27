Imports MySql.Data.MySqlClient
Public Class Cambiopassword
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connSAG").ConnectionString
    Dim LValidacion As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated = True Then
            Using sqlCon As New MySqlConnection(conn)
                Using cmd As New MySqlCommand()
                    cmd.CommandText = "SELECT * FROM usuario Where Nombre='" & User.Identity.Name & "' "
                    cmd.Connection = sqlCon
                    sqlCon.Open()
                    Dim da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    sqlCon.Close()
                    LValidacion = dt.Rows(0)("pass").ToString
                End Using
            End Using
        Else
            Response.Redirect(String.Format("~/pages/login.aspx"))
        End If
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If Txtpass.Text = Txtpass1.Text Then
            If Txtpassact.Text = LValidacion Then
                Using sqlCon As New MySqlConnection(conn)
                    sqlCon.Open()
                    Dim query As String = "UPDATE usuario SET pass=@pass Where Nombre ='" & User.Identity.Name & "'"

                    Dim cmd As New MySqlCommand(query, sqlCon)

                    cmd.Parameters.AddWithValue("@pass", Txtpass.Text)

                    cmd.ExecuteNonQuery()

                    sqlCon.Close()
                End Using

                Response.Write("<script>window.alert('La contraseña fue cambiada con exito!');</script>" + "<script>window.setTimeout(location.href='login.aspx', 2000);</script>")
                FormsAuthentication.SignOut()
                'Session.Abandon()

            Else
                Response.Write("<script>window.alert('La contraseña anterior es incorrecta, favor revise y vuelva a intentarlo');</script>")
                Txtpass.Text = ""
                Txtpass1.Text = ""
                Txtpassact.Text = ""
            End If

        Else
            Response.Write("<script>window.alert('Las Nuevas contraseñas no coinciden, favor revise las contraseñas');</script>")
            Txtpass.Text = ""
            Txtpass1.Text = ""
            Txtpassact.Text = ""
        End If
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Response.Redirect(String.Format("~/pages/Inicio.aspx"))
    End Sub
End Class