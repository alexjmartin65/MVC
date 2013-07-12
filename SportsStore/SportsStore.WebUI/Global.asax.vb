' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization
Imports SportsStore.Domain.Entities
Imports SportsStore.WebUI.Binders
Imports SportsStore.WebUI.Infrastructure

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)


        ControllerBuilder.Current.SetControllerFactory(New NinjectControllerFactory())
        ModelBinders.Binders.Add(GetType(Cart), New CartModelBinder())
    End Sub
End Class
