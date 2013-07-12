Imports EssentialTools.Models
Imports Ninject

Namespace EssentialTools

    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        Private products() As Product = {
                                        New Product() With {.Name = "Kayak", .Price = 275},
                                        New Product() With {.Name = "Lifejacket", .Price = 48.95},
                                        New Product() With {.Name = "Kayak", .Price = 275},
                                        New Product() With {.Name = "Kayak", .Price = 275}
                                     }

        Private calc As IValueCalculator
        Public Sub New(calcParam As IValueCalculator)
            calc = calcParam
        End Sub

        Function Index() As ActionResult
            Dim cart As New ShoppingCart(calc)
            cart.Products = products

            Dim totalValue As Decimal = cart.CalculateProductsTotal()
            Return View(totalValue)
        End Function

    End Class
End Namespace