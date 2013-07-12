@ModelType IEnumerable(Of String)


@Html.ActionLink("Home", "List", "Product")

@For Each link As String In Model
    @Html.RouteLink(link, 
                     New With { .controller = "Product", .action = "List", .category = link, .page = 1 },
                     New With { .class = If(link = ViewBag.SelectedCategory, "selected", Nothing)}
                    )
Next
