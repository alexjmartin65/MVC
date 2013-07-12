Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports SportsStore.WebUI.Controllers
Imports SportsStore.WebUI.Infrastructure.Abstract
Imports SportsStore.WebUI.Models
Imports System.Web.Mvc

<TestClass()>
Public Class AdminSecurityTest

    <TestMethod()>
    Public Sub Can_Login_With_Valid_Credentials()

        Dim mock As New Mock(Of IAuthProvider)
        mock.Setup(Function(m) m.Authenticate("admin", "secret")).Returns(True)

        Dim model As New LoginViewModel() With {.UserName = "admin", .Password = "secret"}

        Dim target As New AccountController(mock.Object)

        Dim result As ActionResult = target.Login(model, "/MyURL")

        Assert.IsInstanceOfType(result, GetType(RedirectResult))
        Assert.AreEqual("/MyURL", CType(result, RedirectResult).Url)

    End Sub

    <TestMethod>
    Public Sub Cannot_Login()
        Dim mock As New Mock(Of IAuthProvider)
        mock.Setup(Function(m) m.Authenticate("badUser", "badPass")).Returns(False)

        Dim model As New LoginViewModel() With {.UserName = "badUser", .Password = "badPass"}
        Dim target As New AccountController(mock.Object)
        Dim result As ActionResult = target.Login(model, "/MyURL")

        Assert.IsInstanceOfType(result, GetType(ViewResult))
        Assert.IsFalse(CType(result, ViewResult).ViewData.ModelState.IsValid)
    End Sub

End Class