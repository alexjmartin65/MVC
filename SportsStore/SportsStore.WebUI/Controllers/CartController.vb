Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Entities
Imports SportsStore.WebUI.Models

Namespace Controllers
    Public Class CartController
        Inherits System.Web.Mvc.Controller

        Private repository As IProductRepository
        Private orderProcessor As IOrderProcessor

        Public Sub New(repo As IProductRepository, processor As IOrderProcessor)
            repository = repo
            orderProcessor = processor
        End Sub

        Public Function AddToCart(cart As Cart, productId As Integer, returnUrl As String) As RedirectToRouteResult
            Dim product As Product = repository.Products.FirstOrDefault(Function(prod) prod.ProductID = productId)

            If (product IsNot Nothing) Then
                cart.AddItem(product, 1)
            End If

            Return RedirectToAction("Index", New With {returnUrl})
        End Function

        Public Function RemoveFromCart(cart As Cart, productId As Integer, returnUrl As String) As RedirectToRouteResult
            Dim product As Product = repository.Products.FirstOrDefault(Function(prod) prod.ProductID = productId)

            If (product IsNot Nothing) Then
                cart.RemoveLine(product)
            End If

            Return RedirectToAction("Index", New With {returnUrl})
        End Function

        Public Function Index(cart As Cart, returnUrl As String) As ViewResult
            Return View(New CartIndexViewModel With {.Cart = cart, .ReturnUrl = returnUrl})
        End Function

        Public Function Summary(cart As Cart) As PartialViewResult
            Return PartialView(cart)
        End Function

        Public Function Checkout() As ViewResult
            Return View(New ShippingDetails())
        End Function

        <HttpPost>
        Public Function Checkout(cart As Cart, shippingDetails As ShippingDetails) As ViewResult
            If (cart.Lines.Count() = 0) Then
                ModelState.AddModelError("", "Sorry, your cart is empty")
            End If

            If (ModelState.IsValid) Then
                Try
                    orderProcessor.ProcessOrder(cart, shippingDetails)
                Catch ex As Exception
                    'TODO: Error handling 
                End Try
                cart.Clear()
                Return View("Completed")
            Else
                Return View(shippingDetails)
            End If

        End Function

    End Class
End Namespace