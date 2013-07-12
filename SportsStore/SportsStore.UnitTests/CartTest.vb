Imports System.Text
Imports SportsStore.Domain.Entities
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports SportsStore.Domain.Abstract
Imports SportsStore.WebUI.Controllers
Imports SportsStore.WebUI.Models
Imports System.Web.Mvc

<TestClass()> Public Class CartTest

    <TestMethod()>
    Public Sub Can_Add_New_Lines()

        Dim p1 As New Product() With {.ProductID = 1, .Name = "P1"}
        Dim p2 As New Product() With {.ProductID = 2, .Name = "P2"}

        Dim target As New Cart()

        target.AddItem(p1, 1)
        target.AddItem(p2, 1)

        Dim results() As CartLine = target.Lines.ToArray()

        Assert.AreEqual(2, results.Length)
        Assert.AreEqual(p1, results(0).Product)
        Assert.AreEqual(p2, results(1).Product)

    End Sub

    <TestMethod>
    Public Sub Can_Add_Multiple()
        Dim p1 As New Product() With {.ProductID = 1, .Name = "P1"}
        Dim p2 As New Product() With {.ProductID = 2, .Name = "P2"}

        Dim target As New Cart()

        target.AddItem(p1, 1)
        target.AddItem(p2, 1)
        target.AddItem(p1, 10)

        Dim results() As CartLine = target.Lines.OrderBy(Function(item) item.Product.ProductID).ToArray()

        Assert.AreEqual(2, results.Length)
        Assert.AreEqual(11, results(0).Quantity)
        Assert.AreEqual(1, results(1).Quantity)

    End Sub

    <TestMethod>
    Public Sub Can_Remove()
        Dim p1 As New Product() With {.ProductID = 1, .Name = "P1"}
        Dim p2 As New Product() With {.ProductID = 2, .Name = "P2"}
        Dim p3 As New Product() With {.ProductID = 3, .Name = "P3"}

        Dim target As New Cart()

        target.AddItem(p1, 1)
        target.AddItem(p2, 3)
        target.AddItem(p3, 5)
        target.AddItem(p2, 1)

        target.RemoveLine(p2)

        Assert.AreEqual(0, target.Lines.Where(Function(item) item.Product.Equals(p2)).Count())
        Assert.AreEqual(2, target.Lines.Count())

    End Sub

    <TestMethod>
    Public Sub Calculate_Total()
        Dim p1 As New Product() With {.ProductID = 1, .Name = "P1", .Price = 100D}
        Dim p2 As New Product() With {.ProductID = 2, .Name = "P2", .Price = 50D}

        Dim target As New Cart()

        target.AddItem(p1, 1)
        target.AddItem(p2, 1)
        target.AddItem(p1, 3)

        Dim result As Decimal = target.ComputeTotalValues()

        Assert.AreEqual(450D, result)

    End Sub

    <TestMethod>
    Public Sub Can_Clear()
        Dim p1 As New Product() With {.ProductID = 1, .Name = "P1", .Price = 100D}
        Dim p2 As New Product() With {.ProductID = 2, .Name = "P2", .Price = 50D}

        Dim target As New Cart()

        target.AddItem(p1, 1)
        target.AddItem(p2, 1)

        target.Clear()

        Assert.AreEqual(0, target.Lines.Count())

    End Sub

    <TestMethod>
    Public Sub Can_Add_To_Cart()
        Dim mock As New Mock(Of IProductRepository)
        mock.Setup(Function(m) m.Products).Returns(New Product() _
                                                                        {
                                                                            New Product() With {.ProductID = 1, .Name = "P1", .Category = "Apples"}
                                                                        }.AsQueryable())

        Dim cart As New Cart()
        Dim target As New CartController(mock.Object, Nothing)

        target.AddToCart(cart, 1, Nothing)

        Assert.AreEqual(1, cart.Lines.Count())
        Assert.AreEqual(1, cart.Lines.ToArray()(0).Product.ProductID)
    End Sub

    <TestMethod>
    Public Sub Adding_Product_To_Cart_Goes_To_Cart_Screen()
        Dim mock As New Mock(Of IProductRepository)
        mock.Setup(Function(m) m.Products).Returns(New Product() _
                                                                        {
                                                                            New Product() With {.ProductID = 1, .Name = "P1", .Category = "Apples"}
                                                                        }.AsQueryable())

        Dim cart As New Cart()
        Dim target As New CartController(mock.Object, Nothing)

        Dim result As RedirectToRouteResult = target.AddToCart(cart, 1, "myUrl")

        Assert.AreEqual("Index", result.RouteValues("action"))
        Assert.AreEqual("myUrl", result.RouteValues("returnUrl"))

    End Sub

    <TestMethod>
    Public Sub Can_View_Cart_Contents()
        Dim cart As New Cart()

        Dim target As New CartController(Nothing, Nothing)

        Dim result As CartIndexViewModel = CType(target.Index(cart, "myUrl").ViewData.Model, CartIndexViewModel)

        Assert.AreSame(cart, result.Cart)
        Assert.AreEqual("myUrl", result.ReturnUrl)

    End Sub

    <TestMethod>
    Public Sub Cannot_Checkout_Empty_Cart()

        Dim mock As New Mock(Of IOrderProcessor)

        Dim cart As New Cart()

        Dim shippingDetails As New ShippingDetails()

        Dim target As New CartController(Nothing, mock.Object)

        Dim result As ViewResult = target.Checkout(cart, shippingDetails)
        mock.Verify(Sub(m) m.ProcessOrder(It.IsAny(Of Cart)(), It.IsAny(Of ShippingDetails)()), Times.Never())
        Assert.AreEqual("", result.ViewName)
        Assert.AreEqual(False, result.ViewData.ModelState.IsValid)
    End Sub

    <TestMethod>
    Public Sub Cannot_Checkout_Invalid_Shippping()
        Dim mock As New Mock(Of IOrderProcessor)

        Dim cart As New Cart()
        cart.AddItem(New Product(), 1)

        Dim target As New CartController(Nothing, mock.Object)
        target.ModelState.AddModelError("error", "error")

        Dim result As ViewResult = target.Checkout(cart, New ShippingDetails())

        mock.Verify(Sub(m) m.ProcessOrder(It.IsAny(Of Cart)(), It.IsAny(Of ShippingDetails)()), Times.Never())

        Assert.AreEqual(String.Empty, result.ViewName)
        Assert.AreEqual(False, result.ViewData.ModelState.IsValid)

    End Sub

    <TestMethod>
    Public Sub Can_Checkout_And_Submit_Order()
        Dim mock As New Mock(Of IOrderProcessor)

        Dim cart As New Cart()
        cart.AddItem(New Product(), 1)
        Dim target As New CartController(Nothing, mock.Object)

        Dim result As ViewResult = target.Checkout(cart, New ShippingDetails)

        mock.Verify(Sub(m) m.ProcessOrder(It.IsAny(Of Cart)(), It.IsAny(Of ShippingDetails)()), Times.Once())

        Assert.AreEqual("Completed", result.ViewName)
        Assert.AreEqual(True, result.ViewData.ModelState.IsValid)

    End Sub

End Class