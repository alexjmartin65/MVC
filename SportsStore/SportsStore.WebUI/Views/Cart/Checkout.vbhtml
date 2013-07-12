@ModelType SportsStore.Domain.Entities.ShippingDetails

@Code
    ViewData("Title") = "Sports Store: Checkout"
End Code

<h2>Check out now</h2>

Please enter your details, and we'll ship your goods right away!

@Using Html.BeginForm()
    @<div class="checkoutContainer">

        @Html.ValidationSummary()

        <h3>Ship to</h3>
        <div><label>Name:</label> @Html.EditorFor(Function(x) x.Name)</div>
    
        <h3>Address</h3>
        <div><label>Line 1:</label> @Html.EditorFor(Function(x) x.Line1)</div>
        <div><label>Line 2:</label> @Html.EditorFor(Function(x) x.Line2)</div>
        <div><label>Line 3:</label> @Html.EditorFor(Function(x) x.Line3)</div>
        <div><label>City:</label> @Html.EditorFor(Function(x) x.City)</div>
        <div><label>State:</label> @Html.EditorFor(Function(x) x.State)</div>
        <div><label>Zip:</label> @Html.EditorFor(Function(x) x.Zip)</div>
        <div><label>Country:</label> @Html.EditorFor(Function(x) x.Country)</div>    
    
        <h3>Options</h3>
        <label>
            @Html.EditorFor(Function(x) x.GiftWrap)
            Gift wrap these items
         </label>
    
        <p>
            <input class="actionButtons" type="submit" value="Complete order" />
         </p>
     </div>
    
End Using
