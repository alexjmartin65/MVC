Imports SportsStore.Domain.Entities

Namespace Binders

    Public Class CartModelBinder
        Implements IModelBinder

        Private Const sessionKey As String = "Cart"

        Public Function BindModel(controllerContext As ControllerContext, bindingContext As ModelBindingContext) As Object Implements IModelBinder.BindModel

            Dim cart As Cart = CType(controllerContext.HttpContext.Session(sessionKey), Cart)

            If (cart Is Nothing) Then
                cart = New Cart()
                controllerContext.HttpContext.Session(sessionKey) = cart
            End If

            Return cart

        End Function

    End Class

End Namespace