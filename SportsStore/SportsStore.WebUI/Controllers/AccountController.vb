Imports SportsStore.WebUI.Infrastructure.Abstract
Imports SportsStore.WebUI.Models

Namespace Controllers
    Public Class AccountController
        Inherits System.Web.Mvc.Controller

        Private authProvider As IAuthProvider

        Public Sub New(auth As IAuthProvider)
            authProvider = auth
        End Sub

        Public Function Login() As ViewResult
            Return View()
        End Function

        <HttpPost>
        Public Function Login(model As LoginViewModel, returnUrl As String) As ActionResult
            If (ModelState.IsValid) Then
                If (authProvider.Authenticate(model.UserName, model.Password)) Then
                    Dim redirectUrl As String

                    If (Not String.IsNullOrEmpty(returnUrl)) Then
                        redirectUrl = returnUrl
                    Else
                        redirectUrl = Url.Action("Index", "Admin")
                    End If

                    Return Redirect(redirectUrl)
                Else
                    ModelState.AddModelError("", "Incorrect username or password")
                    Return View()
                End If
            Else
                Return View()
            End If
        End Function

    End Class
End Namespace