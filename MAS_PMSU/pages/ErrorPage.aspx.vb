Imports System.Web
Imports System.Web.UI


Public Class ErrorPage
    Inherits Page
    Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
        'Response.StatusCode = 500
        'MyBase.Render(writer)
        'writer.WriteLine("<script>")
        'writer.WriteLine("if (window.performance) {")
        'writer.WriteLine("   if (window.performance.navigation.type == 1) {")
        'writer.WriteLine("       window.location.href = 'Login.aspx';")
        'writer.WriteLine("   }")
        'writer.WriteLine("}")
        'writer.WriteLine("</script>")
    End Sub
End Class
