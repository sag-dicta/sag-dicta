Public Class TC_INVENTARIOS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated = True Then

        Else
            Response.Redirect(String.Format("~/pages/login.aspx"))
        End If


    End Sub

End Class