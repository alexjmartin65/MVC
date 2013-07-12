Imports SportsStore.Domain.Entities


Namespace Abstract

    Public Interface IProductRepository

        ReadOnly Property Products As IQueryable(Of Product)

        Sub SaveProduct(product As Product)

        Function Delete(productId As Integer) As Product

    End Interface

End Namespace
