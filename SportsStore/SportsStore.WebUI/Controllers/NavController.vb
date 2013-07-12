Imports SportsStore.Domain.Abstract
Imports System.Collections.Generic

Namespace Controllers

    Public Class NavController
        Inherits System.Web.Mvc.Controller

        Private repository As IProductRepository

        Public Sub New(repo As IProductRepository)
            repository = repo
        End Sub

        Public Function Menu(Optional category As String = Nothing) As PartialViewResult

            ViewBag.SelectedCategory = category

            Dim categories As IEnumerable(Of String) = repository.Products. _
                                                                  Select(Function(prod) prod.Category). _
                                                                  Distinct(). _
                                                                  OrderBy(Function(cat) cat)

            Return PartialView(categories)

        End Function

    End Class

End Namespace