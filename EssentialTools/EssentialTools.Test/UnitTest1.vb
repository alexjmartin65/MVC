Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports EssentialTools.Models

<TestClass()> 
Public Class UnitTest1

    Private Function getTestObject() As IDiscountHelper
        Return New MinimumDiscountHelper()
    End Function

    <TestMethod()>
    Public Sub Discount_Above_100()
        'Arrange
        Dim target As IDiscountHelper = getTestObject()
        Dim total As Decimal = 200D
        'Act
        Dim discountedTotal = target.ApplyDiscount(total)
        'Assert
        Assert.AreEqual(total * 0.9D, discountedTotal)
    End Sub

    <TestMethod()>
    Public Sub Discount_Between_10_And_100()
        'Arrange
        Dim target As IDiscountHelper = getTestObject()

        Dim TenDollarDiscount As Decimal = target.ApplyDiscount(10)
        Dim FiftyDollarDiscount As Decimal = target.ApplyDiscount(50)
        Dim HundredDollarDiscount As Decimal = target.ApplyDiscount(100)

        Assert.AreEqual(5D, TenDollarDiscount)
        Assert.AreEqual(45D, FiftyDollarDiscount)
        Assert.AreEqual(95D, HundredDollarDiscount)

    End Sub

    <TestMethod()>
    Public Sub Discount_Less_Than_10()
        'Arrange
        Dim target As IDiscountHelper = getTestObject()

        Dim discount5 As Decimal = target.ApplyDiscount(5)
        Dim discount0 As Decimal = target.ApplyDiscount(0)

        Assert.AreEqual(5D, discount5)
        Assert.AreEqual(0D, discount0)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentOutOfRangeException))>
    Public Sub Discount_NegativeTotal()
        Dim target As IDiscountHelper = getTestObject()
        target.ApplyDiscount(-1)
    End Sub

End Class