Imports System.Collections.Generic
Imports System.Linq

Namespace Models

    Public Class LinqValueCalculator
        Implements IValueCalculator

        Private discounter As IDiscountHelper

        Public Sub New(discountParam As IDiscountHelper)
            discounter = discountParam
        End Sub

        Public Function ValueProducts(ByVal products As IEnumerable(Of Product)) As Decimal Implements IValueCalculator.ValueProducts
            Return discounter.ApplyDiscount(products.Sum(Function(prod) prod.Price))
        End Function

    End Class

End Namespace
