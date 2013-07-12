Imports Moq
Imports Ninject
Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Concrete
Imports SportsStore.Domain.Entities
Imports SportsStore.WebUI.Infrastructure.Abstract
Imports SportsStore.WebUI.Infrastructure.Concrete
Imports System.Linq

Namespace Infrastructure

    Public Class NinjectControllerFactory
        Inherits DefaultControllerFactory

        Private ninjectKernel As IKernel

        Public Sub New()
            ninjectKernel = New StandardKernel()
            AddBindings()
        End Sub

        Protected Overrides Function GetControllerInstance(requestContext As RequestContext, controllerType As Type) As IController
            If (controllerType Is Nothing) Then
                Return Nothing
            Else
                Return CType(ninjectKernel.Get(controllerType), IController)
            End If
        End Function

        Private Sub AddBindings()
            ninjectKernel.Bind(Of IProductRepository)().To(Of EFProductRepository)()

            Dim emailSettings As EmailSettings = _
                New EmailSettings With {.WriteAsFile = Boolean.Parse(If(ConfigurationManager.AppSettings("Email.WriteAsFile"), "false"))}

            ninjectKernel.Bind(Of IOrderProcessor).To(Of EmailOrderProcessor)().WithConstructorArgument("settings", emailSettings)
            ninjectKernel.Bind(Of IAuthProvider).To(Of FormsAuthProvider)()


        End Sub

    End Class

End Namespace
