
Namespace Models

    Public Class MinimumDiscountHelper
        Implements IDiscountHelper

        Public Function ApplyDiscount(totalParam As Decimal) As Object Implements IDiscountHelper.ApplyDiscount
            If (totalParam < 0) Then
                Throw New ArgumentOutOfRangeException
            ElseIf (totalParam > 100) Then
                Return totalParam * 0.9D
            ElseIf (totalParam >= 10 And totalParam <= 100) Then
                Return totalParam - 5
            Else
                Return totalParam
            End If
        End Function

    End Class

End Namespace
