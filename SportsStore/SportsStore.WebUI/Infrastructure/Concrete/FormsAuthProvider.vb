Imports SportsStore.WebUI.Infrastructure.Abstract
Imports System.Web.Security

Namespace Infrastructure.Concrete

    Public Class FormsAuthProvider
        Implements IAuthProvider

        Public Function Authenticate(username As String, password As String) As Boolean Implements IAuthProvider.Authenticate
            Dim result As Boolean = FormsAuthentication.Authenticate(username, password)
            If (result) Then
                FormsAuthentication.SetAuthCookie(username, False)
            End If
            Return result
        End Function

    End Class

End Namespace