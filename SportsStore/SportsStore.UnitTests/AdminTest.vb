Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Entities
Imports SportsStore.WebUI.Controllers
Imports System.Text
Imports System.Web.Mvc



<TestClass()>
Public Class AdminTest

    <TestMethod()>
    Public Sub TestMethod1()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1"},
                                                                    New Product With {.ProductID = 2, .Name = "P2"},
                                                                    New Product With {.ProductID = 3, .Name = "P3"}
                                                                 }.AsQueryable())

        Dim target As New AdminController(mock.Object)
        Dim result() As Product = CType(target.Index().ViewData.Model, IEnumerable(Of Product)).ToArray()

        Assert.AreEqual(3, result.Length)
        Assert.AreEqual("P1", result(0).Name)

    End Sub

    <TestMethod>
    Public Sub Can_Edit_Product()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1"},
                                                                    New Product With {.ProductID = 2, .Name = "P2"},
                                                                    New Product With {.ProductID = 3, .Name = "P3"}
                                                                 }.AsQueryable())
        Dim target As New AdminController(mock.Object)

        Dim p1 As Product = target.Edit(1).ViewData.Model
        Dim p2 As Product = target.Edit(2).ViewData.Model
        Dim p3 As Product = target.Edit(3).ViewData.Model

        Assert.AreEqual(1, p1.ProductID)
        Assert.AreEqual(2, p2.ProductID)
        Assert.AreEqual(3, p3.ProductID)
    End Sub

    <TestMethod>
    Public Sub Cannot_Edit_NonExistent()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1"},
                                                                    New Product With {.ProductID = 2, .Name = "P2"},
                                                                    New Product With {.ProductID = 3, .Name = "P3"}
                                                                 }.AsQueryable())
        Dim target As New AdminController(mock.Object)

        Dim result As Product = target.Edit(4).ViewData.Model

        Assert.IsNull(result)

    End Sub

    <TestMethod>
    Public Sub Can_Save_Valid_Changes()
        Dim mock As New Mock(Of IProductRepository)
        Dim target As New AdminController(mock.Object)
        Dim product As New Product With {.Name = "Test"}
        Dim result As ActionResult = target.Edit(product, Nothing)

        mock.Verify(Sub(m) m.SaveProduct(product))

        Assert.IsNotInstanceOfType(result, GetType(ViewResult))
    End Sub

    <TestMethod>
    Public Sub Cannot_Save_Invalid_Changes()
        Dim mock As New Mock(Of IProductRepository)
        Dim target As New AdminController(mock.Object)
        Dim product As New Product With {.Name = "Test"}
        target.ModelState.AddModelError("error", "error")
        Dim result As ActionResult = target.Edit(product, Nothing)
        mock.Verify(Sub(m) m.SaveProduct(It.IsAny(Of Product)), Times.Never())

        Assert.IsInstanceOfType(result, GetType(ViewResult))
    End Sub

    Public Sub Can_Delete_Valid()
        Dim prod As New Product With {.ProductID = 2, .Name = "Test"}

        Dim mock As New Mock(Of IProductRepository)
        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                            New Product With {.ProductID = 1, .Name = "P1", .Category = "Cat1"},
                                                            prod,
                                                            New Product With {.ProductID = 3, .Name = "P3", .Category = "Cat1"}
                                                         }.AsQueryable())

        Dim target As New AdminController(mock.Object)

        target.Delete(prod.ProductID)

        mock.Verify(Sub(m) m.Delete(prod.ProductID))
    End Sub

End Class