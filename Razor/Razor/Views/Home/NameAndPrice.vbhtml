@ModelType Razor.Models.Product

@Code
    ViewBag.Title = "NameAndPrice"
    Layout = "~/Views/_BasicLayout.vbhtml"
End Code

<h2>NameAndPrice</h2>
The product name is @Model.Name and it costs $@Model.Price
