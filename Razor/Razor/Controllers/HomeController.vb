Imports Razor.Models

Namespace Razor
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        Dim myProduct As New Product() With {.ProductID = 1,
                                              .Name = "Kayak",
                                              .Description = "A boat for one person",
                                              .Category = "Watersports",
                                              .Price = 275}

        Function Index() As ActionResult
            Return View(myProduct)
        End Function

        Function NameAndPrice() As ActionResult
            Return View(myProduct)
        End Function

        Function DemoExpression() As ActionResult
            ViewBag.ProductCount = 1
            ViewBag.ExpressShip = True
            ViewBag.ApplyDiscount = False
            ViewBag.Supplier = Nothing

            Return View(myProduct)
        End Function

        Function DemoArray() As ActionResult
            Dim array() As Product = {
                                        New Product() With {.Name = "Kayak", .Price = 275},
                                        New Product() With {.Name = "Lifejacket", .Price = 48.95},
                                        New Product() With {.Name = "Kayak", .Price = 275},
                                        New Product() With {.Name = "Kayak", .Price = 275}
                                     }
            Return View(array)
        End Function

    End Class
End Namespace