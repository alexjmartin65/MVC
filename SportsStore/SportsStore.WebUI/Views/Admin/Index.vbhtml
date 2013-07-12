@ModelType IEnumerable(Of SportsStore.Domain.Entities.Product)

@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_AdminLayout.vbhtml"
End Code



<h1>All Products</h1>
<table class="Grid">
    <tr>
        <th>ID</th>
        <th>Name</th>
        <th class="NumericCol">Price</th>
        <th>Actions</th>
    </tr>

    @For Each item In Model
        Dim currentItem = item
        @<tr>
            <td>@item.ProductID</td>
            <td>@Html.ActionLink(item.Name, "Edit", New With {item.ProductID})</td>
            <td class="NumericCol">@item.Price.ToString("c")</td>
            <td>
                @Using (Html.BeginForm("Delete", "Admin"))
                    @Html.Hidden("ProductID", item.ProductID)
                    @<input type="submit" value="Delete" />
        End Using
            </td>
        </tr>
    Next
</table>
<p>
    @Html.ActionLink("Add a new product", "Create")
</p>
