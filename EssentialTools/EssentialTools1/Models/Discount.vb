Namespace Models

    Public Interface IDiscountHelper
        Function ApplyDiscount(totalParam As Decimal)
    End Interface

    Public Class DefaultDiscountHelper
        Implements IDiscountHelper

        Public discountSize As Decimal

        Public Sub New(discountParam As Decimal)
            discountSize = discountParam
        End Sub

        Public Function ApplyDiscount(totalParam As Decimal) As Object Implements IDiscountHelper.ApplyDiscount
            Return (totalParam - (discountSize / 100 * totalParam))
        End Function
    End Class

End Namespace