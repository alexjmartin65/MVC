Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports EssentialTools.Models
Imports Moq

<TestClass()> Public Class UnitTest2

    Private products() As Product = {
                                        New Product() With {.Name = "Kayak", .Category = "Water Sports", .Price = 275D},
                                        New Product() With {.Name = "Life Jacket", .Category = "Water Sports", .Price = 48.95D},
                                        New Product() With {.Name = "Soccer Ball", .Category = "Soccer", .Price = 19.5D},
                                        New Product() With {.Name = "Corner Flag", .Category = "Soccer", .Price = 34.95D}
                                    }

    <TestMethod()>
    Public Sub Sum_Products_Correctly()
        Dim mock As New Mock(Of IDiscountHelper)
        mock.Setup(Function(m) m.ApplyDiscount(It.IsAny(Of Decimal)())).Returns(Of Decimal)(Function(total) total)

        Dim target = New LinqValueCalculator(mock.Object)

        Dim result = target.ValueProducts(products)

        Assert.AreEqual(products.Sum(Function(prod) prod.Price), result)
    End Sub

    Private Function createProduct(value As Decimal) As Product()
        Return New Product() {New Product() With {.Price = value}}
    End Function

    <TestMethod>
    <ExpectedException(GetType(ArgumentOutOfRangeException))>
    Public Sub Pass_Through_Varaiable_Discounts()
        Dim mock As New Mock(Of IDiscountHelper)
        mock.Setup(Function(m) m.ApplyDiscount(It.IsAny(Of Decimal)())).Returns(Of Decimal)(Function(total) total)
        mock.Setup(Function(m) m.ApplyDiscount(It.Is(Of Decimal)(Function(v) v = 0))).Throws(Of ArgumentOutOfRangeException)()
        mock.Setup(Function(m) m.ApplyDiscount(It.Is(Of Decimal)(Function(v) v > 100))).Returns(Of Decimal)(Function(total) total * 0.9D)
        mock.Setup(Function(m) m.ApplyDiscount(It.IsInRange(Of Decimal)(10, 100, Range.Inclusive))).Returns(Of Decimal)(Function(total) total - 5)
        Dim target = New LinqValueCalculator(mock.Object)

        Dim FiveDollarDiscount = target.ValueProducts(createProduct(5))
        Dim TenDollarDiscount = target.ValueProducts(createProduct(10))
        Dim FiftyeDollarDiscount = target.ValueProducts(createProduct(50))
        Dim HundredDollarDiscount = target.ValueProducts(createProduct(100))
        Dim FiveHunderDollarDiscount = target.ValueProducts(createProduct(500))


        Assert.AreEqual(5D, FiveDollarDiscount)
        Assert.AreEqual(5D, TenDollarDiscount)
        Assert.AreEqual(45D, FiftyeDollarDiscount)
        Assert.AreEqual(95D, HundredDollarDiscount)
        Assert.AreEqual(450D, FiveHunderDollarDiscount)

        target.ValueProducts(createProduct(0))
    End Sub

End Class