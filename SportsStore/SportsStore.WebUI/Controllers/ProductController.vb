Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Entities
Imports SportsStore.WebUI.Models

Namespace Controllers
    Public Class ProductController
        Inherits System.Web.Mvc.Controller

        Private repository As IProductRepository
        Public PageSize As Integer = 4

        Public Sub New(productRepository As IProductRepository)
            repository = productRepository
        End Sub

        Public Function List(category As String, Optional page As Integer = 1) As ViewResult

            Dim model As New ProductsListViewModel() With {.Products =
                                                                repository.Products. _
                                                                Where(Function(p) category Is Nothing Or p.Category = category). _
                                                                OrderBy(Function(p) p.ProductID). _
                                                                Skip((page - 1) * PageSize). _
                                                                Take(PageSize),
                                                            .PagingInfo = New PagingInfo With
                                                                        {
                                                                            .CurrentPage = page,
                                                                            .ItemsPerPage = PageSize,
                                                                            .TotalItems = If(category Is Nothing,
                                                                                             repository.Products.Count(),
                                                                                             repository.Products. _
                                                                                                Where(Function(prod) prod.Category = category).Count())
                                                                        },
                                                            .CurrentCategory = category
                                                          }

            Return View(model)

        End Function

        Public Function GetImage(productId As Integer) As FileContentResult
            Dim prod As Product = repository.Products.FirstOrDefault(Function(p) p.ProductID = productId)
            If (prod IsNot Nothing) Then
                Return File(prod.ImageData, prod.ImageMimeType)
            Else
                Return Nothing
            End If
        End Function

    End Class
End Namespace