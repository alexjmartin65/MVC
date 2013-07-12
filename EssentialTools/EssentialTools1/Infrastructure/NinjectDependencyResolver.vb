Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Web.Mvc
Imports Ninject
Imports Ninject.Parameters
Imports Ninject.Syntax
Imports EssentialTools.Models

Namespace Infrastructure

    Public Class NinjectDependencyResolver
        Implements IDependencyResolver

        Private kernal As IKernel

        Public Sub New()
            kernal = New StandardKernel()
            AddBindings()

        End Sub

        Public Function GetService(serviceType As Type) As Object Implements IDependencyResolver.GetService
            Return kernal.TryGet(serviceType)
        End Function

        Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyResolver.GetServices
            Return kernal.GetAll(serviceType)
        End Function

        Private Sub AddBindings()
            kernal.Bind(Of IValueCalculator)().To(Of LinqValueCalculator)()
            kernal.Bind(Of IDiscountHelper)().To(Of DefaultDiscountHelper)().WithConstructorArgument("discountParam", 50D)
            kernal.Bind(Of IDiscountHelper)().To(Of FlexibleDiscountHelper)().WhenInjectedInto(Of LinqValueCalculator)()
        End Sub

    End Class

End Namespace
