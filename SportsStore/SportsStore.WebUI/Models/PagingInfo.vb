Namespace Models

    Public Class PagingInfo
        Public Property TotalItems As Integer
        Public Property ItemsPerPage As Integer
        Public Property CurrentPage As Integer

        Public ReadOnly Property TotalPages() As Integer
            Get
                Return CType(Math.Ceiling(TotalItems / ItemsPerPage), Integer)
            End Get
        End Property


    End Class

End Namespace