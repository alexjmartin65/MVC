Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Class RouteConfig
    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(name:=Nothing,
                url:="",
                defaults:=New With {.controller = "Product", .action = "List", .category = CType(Nothing, String), .page = 1})

        routes.MapRoute(Nothing,
                        "Page{page}",
                        New With {.controller = "Product", .action = "List", .category = CType(Nothing, String)},
                        New With {.page = "\d+"})

        routes.MapRoute(Nothing,
                "{category}",
                New With {.controller = "Product", .action = "List", .page = 1})

        routes.MapRoute(Nothing,
                "{category}/Page{page}",
                New With {.controller = "Product", .action = "List"},
                New With {.page = "\d+"})


        routes.MapRoute( _
            name:="Default", _
            url:="{controller}/{action}/{id}", _
            defaults:=New With {.controller = "Product", .action = "List", .id = UrlParameter.Optional} _
        )
    End Sub
End Class