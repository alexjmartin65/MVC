Imports System.Collections.Generic
Imports SportsStore.Domain.Entities

Namespace Models

    Public Class ProductsListViewModel

        Public Property Products As IEnumerable(Of Product)
        Public Property PagingInfo As PagingInfo
        Public CurrentCategory As String

    End Class

End Namespace