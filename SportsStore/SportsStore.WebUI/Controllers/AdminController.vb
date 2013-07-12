Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Entities

Namespace Controllers
    <Authorize>
    Public Class AdminController
        Inherits System.Web.Mvc.Controller

        Private repository As IProductRepository

        Public Sub New(repo As IProductRepository)
            Me.repository = repo
        End Sub

        Function Index() As ViewResult
            Return View(repository.Products)
        End Function

        Public Function Edit(productId As Integer) As ViewResult
            Dim product As Product = repository.Products.FirstOrDefault(Function(prod) prod.ProductID = productId)

            Return View(product)
        End Function

        <HttpPost>
        Public Function Edit(product As Product, image As HttpPostedFileBase) As ActionResult
            If (ModelState.IsValid) Then
                If (image IsNot Nothing) Then
                    product.ImageMimeType = image.ContentType
                    product.ImageData = New Byte(image.ContentLength) {}
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength)
                End If
                repository.SaveProduct(product)
                TempData("message") = String.Format("{0} has been saved", product.Name)
                Return RedirectToAction("Index")
            Else
                Return View(product)
            End If
        End Function

        Public Function Create() As ViewResult
            Return View("Edit", New Product())
        End Function

        Public Function Delete(productId As Integer) As ActionResult
            Dim deletedProduct As Product = repository.Delete(productId)
            If (deletedProduct IsNot Nothing) Then
                TempData("message") = String.Format("{0} was deleted", deletedProduct.Name)
            End If

            Return RedirectToAction("Index")
        End Function

    End Class
End Namespace