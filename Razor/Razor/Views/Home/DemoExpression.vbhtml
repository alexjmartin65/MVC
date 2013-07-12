@ModelType Razor.Models.Product

@Code
    ViewData("Title") = "DemoExpression"
End Code

<table>
    <thead>
        <tr>
            <th>
                Property
            </th>
            <th>
                Value
            </th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                Name
            </td>
            <td>
                @Model.Name
            </td>
        </tr>
                <tr>
            <td>
                Price
            </td>
            <td>
                @Model.Price
            </td>
        </tr>
                <tr>
            <td>
                Stock Level
            </td>
            <td>
                @Select Case(CType(ViewBag.ProductCount, Integer))
                    Case 0
                        @:Out of Stock
                    Case 1
                        @<strong>Low Stock (@ViewBag.ProductCount)</strong>
                    Case Else
                        @ViewBag.ProductCount
                End Select

                @*@ViewBag.ProductCount*@
            </td>
        </tr>
    </tbody>
</table>

<div data-discount="@ViewBag.ApplyDiscount" data-express="@ViewBag.ExpressShip" data-supplier="@ViewBag.Supplier">
    The containing element has data attributes.
</div>

Discount: <input type="checkbox" checked="@ViewBag.ApplyDiscount" />
Express: <input type="checkbox" checked="@ViewBag.ExpressShip" />
Supplier: <input type="checkbox" checked="@ViewBag.Supplier" />