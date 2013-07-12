@ModelType SportsStore.Domain.Entities.Product

<div class="item">
    @If (Model.ImageData IsNot Nothing) Then
        @<div style="float:left; margin-right: 20px">
            <img style="width: 75px; height: 75px" src="@Url.Action("GetImage", "Product", New With {Model.ProductID})" />
         </div>
    End If
    <h3>@Model.Name</h3>
    @Model.Description

    @Using Html.BeginForm("AddToCart", "Cart")
        @Html.HiddenFor(Function(prod) prod.ProductID)
        @Html.Hidden("returnUrl", Request.Url.PathAndQuery)
        @<input type="submit" value="+ Add to cart" />
    End Using

    <h4>@Model.Price.ToString("c")</h4>
</div>