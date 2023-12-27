Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Logsistema.Focus()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Autenticacion.Autenticar(TxtUsuario.Text, TxtContrasena.Text) Then
            FormsAuthentication.RedirectFromLoginPage(TxtUsuario.Text, chkRememberMe.Checked)
        Else
            dvMessage.Visible = True
            lblMessage.Text = "Usuario o contraseña incorrecta"
        End If
    End Sub
End Class