Imports SportsStore.Domain.Entities
Imports System.Data.Entity

Namespace Concrete

    Public Class EFDbContext
        Inherits DbContext

        Public Property Products As DbSet(Of Product)

    End Class

End Namespace
