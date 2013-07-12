Imports System.Collections.Generic

Namespace Models

    Public Class ShoppingCart
        Private calc As LinqValueCalculator

        Public Sub New(ByVal calcParam As IValueCalculator)
            calc = calcParam
        End Sub

        Public Property Products As IEnumerable(Of Product)

        Public Function CalculateProductsTotal() As Decimal
            Return calc.ValueProducts(Products)
        End Function

    End Class

End Namespace
