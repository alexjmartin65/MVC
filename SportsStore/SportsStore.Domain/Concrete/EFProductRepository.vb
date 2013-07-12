Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Entities
Imports System.Linq

Namespace Concrete

    Public Class EFProductRepository
        Implements IProductRepository

        Private context As EFDbContext = New EFDbContext()

        Public ReadOnly Property Products As IQueryable(Of Entities.Product) Implements IProductRepository.Products
            Get
                Return context.Products
            End Get
        End Property

        Public Sub SaveProduct(product As Product) Implements IProductRepository.SaveProduct
            If (product.ProductID = 0) Then
                context.Products.Add(product)
            Else
                Dim dbEntry As Product = context.Products.Find(product.ProductID)
                If (dbEntry IsNot Nothing) Then
                    dbEntry.Name = product.Name
                    dbEntry.Description = product.Description
                    dbEntry.Price = product.Price
                    dbEntry.Category = product.Category
                    dbEntry.ImageData = product.ImageData
                    dbEntry.ImageMimeType = product.ImageMimeType
                End If
            End If
            context.SaveChanges()
        End Sub

        Public Function DeleteProduct(productId As Integer) As Product Implements IProductRepository.Delete
            Dim dbEntry As Product = context.Products.Find(productId)
            If (dbEntry IsNot Nothing) Then
                context.Products.Remove(dbEntry)
                context.SaveChanges()
            End If
            Return dbEntry
        End Function

    End Class

End Namespace
