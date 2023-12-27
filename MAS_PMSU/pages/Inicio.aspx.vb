Public Class Inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated = True Then
            Dim name As String = System.Web.HttpContext.Current.Session("Nombre_Detalle")
            Label1.Text = "¡Hola " & name & ", Bienvenido al Portal SAG-DICTA!"
        Else
            Response.Redirect(String.Format("~/pages/login.aspx"))
        End If
    End Sub

End Class