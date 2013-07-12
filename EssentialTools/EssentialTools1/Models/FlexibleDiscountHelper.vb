
Namespace Models

    Public Class FlexibleDiscountHelper
        Implements IDiscountHelper

        Public Function ApplyDiscount(totalParam As Decimal) As Object Implements IDiscountHelper.ApplyDiscount
            Dim discount As Decimal = If(totalParam > 100, 70, 25)
            Return totalParam - (discount / 100D * totalParam)
        End Function

    End Class

End Namespace
