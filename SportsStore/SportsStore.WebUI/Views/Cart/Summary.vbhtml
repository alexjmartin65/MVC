@ModelType SportsStore.Domain.Entities.Cart

<div id="cart">
    <span class="caption">
        <strong>Your Cart:</strong>
        @Model.Lines.Sum(Function (item) item.Quantity) item(s)
        @Model.ComputeTotalValues().ToString("c")
    </span>

    @Html.ActionLink("Checkout", "Index", "Cart", New With { .returnUrl = Request.Url.PathAndQuery }, Nothing)

</div>