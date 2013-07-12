Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Entities
Imports SportsStore.WebUI.Controllers
Imports System.Linq
Imports System.Web.Mvc

<TestClass()>
Public Class ImageTests

    <TestMethod()>
    Public Sub Can_Retrieve_Image()
        Dim prod As New Product With {.ProductID = 2, .Name = "test", .ImageData = New Byte() {}, .ImageMimeType = "image/png"}

        Dim mock As New Mock(Of IProductRepository)()

        mock.Setup(Function(m) m.Products).Returns(New Product() _
                                                    {
                                                        New Product With {.ProductID = 1, .Name = "P1"},
                                                        prod,
                                                        New Product With {.ProductID = 3, .Name = "P3"}
                                                    }.AsQueryable())

        Dim target As New ProductController(mock.Object)
        Dim result As ActionResult = target.GetImage(2)

        Assert.IsNotNull(result)

        Assert.IsInstanceOfType(result, GetType(FileResult))
        Assert.AreEqual(prod.ImageMimeType, CType(result, FileResult).ContentType)

    End Sub

    <TestMethod()>
    Public Sub Cannot_Retrieve_Image()
        Dim mock As New Mock(Of IProductRepository)()

        mock.Setup(Function(m) m.Products).Returns(New Product() _
                                                    {
                                                        New Product With {.ProductID = 1, .Name = "P1"},
                                                        New Product With {.ProductID = 3, .Name = "P3"}
                                                    }.AsQueryable())

        Dim target As New ProductController(mock.Object)
        Dim result As ActionResult = target.GetImage(100)
        Assert.IsNull(result)
    End Sub

End Class