Imports EssentialTools.Models

Namespace Models

    Public Interface IValueCalculator

        Function ValueProducts(products As IEnumerable(Of Product)) As Decimal

    End Interface

End Namespace