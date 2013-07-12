@Imports SportsStore.Domain.Entities
@Imports SportsStore.WebUI.Models
@Imports SportsStore.WebUI.HtmlHelpers

@ModelType ProductsListViewModel

@Code
    ViewData("Title") = "List"
End Code

@For Each product As Product In Model.Products
    Html.RenderPartial("ProductSummary", product)
Next

<div class="pager">
    @Html.PageLinks(Model.PagingInfo, Function(x) Url.Action("List", New With { .Page = x, .Category = Model.CurrentCategory}))
 </div>