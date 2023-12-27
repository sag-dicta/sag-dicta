Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System.IO
Imports ClosedXML.Excel
Public Class compromisoscafebi19_20
    Inherits System.Web.UI.Page

    Dim conn As String = ConfigurationManager.ConnectionStrings("ConnODK").ConnectionString
    Dim sentencia, identity As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If User.Identity.IsAuthenticated = True Then

        Else
            Response.Redirect(String.Format("~/pages/login.aspx"))
        End If

    End Sub


End Class