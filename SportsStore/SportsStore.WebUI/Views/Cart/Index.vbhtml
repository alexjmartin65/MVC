@Imports SportsStore.Domain.Entities

@ModelType SportsStore.WebUI.Models.CartIndexViewModel

@Code
    ViewData("Title") = "Sports Store: Your Cart"
End Code

<h2>Your Cart</h2>

<table class="cartTable">
    <thead>
        <tr>
            <th>Quantity</th>
            <th>Item</th>
            <th>Price</th>
            <th>Subtotal</th>
        </tr>
    </thead>
    <tbody>
        @For Each line As CartLine In Model.Cart.Lines
            @<tr>
                <td>@line.Quantity</td>
                <td>@line.Product.Name</td>
                <td>@line.Product.Price.ToString("c")</td>
                <td>@((line.Quantity * line.Product.Price).ToString("c"))</td>
                <td>
                    @Using Html.BeginForm("RemoveFromCart", "Cart")
                        @Html.Hidden("ProductId", line.Product.ProductID)
                        @Html.HiddenFor(Function (x) x.ReturnUrl)
                        @<input class="actionButtons" type="submit" value="remove" />
                    End Using
                </td>
            </tr>
        Next
    </tbody>
    <tfoot>
        <tr>
            <td colspan="3">Total:</td>
            <td>@Model.Cart.ComputeTotalValues().ToString("c")</td>
        </tr>
    </tfoot>
</table>

<p class="actionButtons">
    <a href="@Model.ReturnUrl">Continue shopping</a>
    @Html.ActionLink("Checkout now", "Checkout")
</p>