Imports Moq
Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Concrete
Imports SportsStore.Domain.Entities
Imports SportsStore.WebUI.Controllers
Imports SportsStore.WebUI.HtmlHelpers
Imports SportsStore.WebUI.Models
Imports System
Imports System.Text
Imports System.Linq
Imports System.Collections.Generic
Imports System.Web.Mvc
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class ProductTest

    <TestMethod()>
    Public Sub Can_Paginate()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1"},
                                                                    New Product With {.ProductID = 2, .Name = "P2"},
                                                                    New Product With {.ProductID = 3, .Name = "P3"},
                                                                    New Product With {.ProductID = 4, .Name = "P4"},
                                                                    New Product With {.ProductID = 5, .Name = "P5"}
                                                                 }.AsQueryable())

        Dim controller As New ProductController(mock.Object)
        controller.PageSize = 3

        Dim result = CType(controller.List(Nothing, 2).Model, ProductsListViewModel)

        Dim prodArray() As Product = result.Products.ToArray()
        Assert.IsTrue(prodArray.Length = 2)
        Assert.AreEqual(prodArray(0).Name, "P4")
        Assert.AreEqual(prodArray(1).Name, "P5")
    End Sub

    <TestMethod()>
    Public Sub Can_Generate_Page_Links()
        Dim myHelper As HtmlHelper = Nothing

        Dim pagingInfo As New PagingInfo() With {.CurrentPage = 2, .TotalItems = 28, .ItemsPerPage = 10}

        Dim pageUrlDelegate As Func(Of Integer, String) = Function(i) String.Concat("Page", i)

        Dim result As MvcHtmlString = myHelper.PageLinks(pagingInfo, pageUrlDelegate)

        Assert.AreEqual(result.ToString(), "<a href=""Page1"">1</a><a class=""selected"" href=""Page2"">2</a><a href=""Page3"">3</a>")

    End Sub

    <TestMethod>
    Public Sub Can_Send_Pagination_View_Model()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1"},
                                                                    New Product With {.ProductID = 2, .Name = "P2"},
                                                                    New Product With {.ProductID = 3, .Name = "P3"},
                                                                    New Product With {.ProductID = 4, .Name = "P4"},
                                                                    New Product With {.ProductID = 5, .Name = "P5"}
                                                                 }.AsQueryable())

        Dim controller As New ProductController(mock.Object)
        controller.PageSize = 3

        Dim result = CType(controller.List(Nothing, 2).Model, ProductsListViewModel)
        Dim pageInfo As PagingInfo = result.PagingInfo
        Assert.AreEqual(2, pageInfo.CurrentPage)
        Assert.AreEqual(3, pageInfo.ItemsPerPage)
        Assert.AreEqual(5, pageInfo.TotalItems)
        Assert.AreEqual(2, pageInfo.TotalPages)

    End Sub

    <TestMethod>
    Public Sub Can_Filter_Products()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1", .Category = "Cat1"},
                                                                    New Product With {.ProductID = 2, .Name = "P2", .Category = "Cat2"},
                                                                    New Product With {.ProductID = 3, .Name = "P3", .Category = "Cat1"},
                                                                    New Product With {.ProductID = 4, .Name = "P4", .Category = "Cat2"},
                                                                    New Product With {.ProductID = 5, .Name = "P5", .Category = "Cat3"}
                                                                 }.AsQueryable())

        Dim controller As New ProductController(mock.Object)
        controller.PageSize = 3

        Dim result() As Product = (CType(controller.List("Cat2", 1).Model, ProductsListViewModel)).Products.ToArray()

        Assert.AreEqual(2, result.Length)
        Assert.IsTrue(result(0).Name = "P2" And result(0).Category = "Cat2")
        Assert.IsTrue(result(1).Name = "P4" And result(1).Category = "Cat2")
    End Sub

    <TestMethod>
    Public Sub Can_Create_Categories()

        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1", .Category = "Apples"},
                                                                    New Product With {.ProductID = 2, .Name = "P2", .Category = "Apples"},
                                                                    New Product With {.ProductID = 3, .Name = "P3", .Category = "Plums"},
                                                                    New Product With {.ProductID = 4, .Name = "P4", .Category = "Oranges"}
                                                                 }.AsQueryable())

        Dim target As New NavController(mock.Object)
        Dim results() As String = (CType(target.Menu().Model, IEnumerable(Of String))).ToArray()

        Assert.AreEqual(3, results.Length)
        Assert.AreEqual("Apples", results(0))
        Assert.AreEqual("Oranges", results(1))
        Assert.AreEqual("Plums", results(2))

    End Sub

    <TestMethod>
    Public Sub IndicatesSelectedCategory()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1", .Category = "Apples"},
                                                                    New Product With {.ProductID = 2, .Name = "P2", .Category = "Oranges"}
                                                                 }.AsQueryable())
        Dim target As New NavController(mock.Object)

        Dim categoryToSelect As String = "Apples"

        Dim result As String = target.Menu(categoryToSelect).ViewBag.SelectedCategory

        Assert.AreEqual(categoryToSelect, result)
    End Sub

    <TestMethod>
    Public Sub Generate_Category_Specific_Product_Count()
        Dim mock As New Mock(Of IProductRepository)

        mock.Setup(Function(m) m.Products).Returns(New Product() {
                                                                    New Product With {.ProductID = 1, .Name = "P1", .Category = "Cat1"},
                                                                    New Product With {.ProductID = 2, .Name = "P2", .Category = "Cat2"},
                                                                    New Product With {.ProductID = 3, .Name = "P3", .Category = "Cat1"},
                                                                    New Product With {.ProductID = 4, .Name = "P4", .Category = "Cat2"},
                                                                    New Product With {.ProductID = 5, .Name = "P5", .Category = "Cat3"}
                                                                 }.AsQueryable())

        Dim target As New ProductController(mock.Object)
        target.PageSize = 3

        Dim res1 As Integer = CType(target.List("Cat1").Model, ProductsListViewModel).PagingInfo.TotalItems
        Dim res2 As Integer = CType(target.List("Cat2").Model, ProductsListViewModel).PagingInfo.TotalItems
        Dim res3 As Integer = CType(target.List("Cat3").Model, ProductsListViewModel).PagingInfo.TotalItems
        Dim resAll As Integer = CType(target.List(Nothing).Model, ProductsListViewModel).PagingInfo.TotalItems

        Assert.AreEqual(2, res1)
        Assert.AreEqual(2, res2)
        Assert.AreEqual(1, res3)
        Assert.AreEqual(5, resAll)

    End Sub

End Class