@Imports Razor.Models

@ModelType Razor.Models.Product()



@Code
    ViewData("Title") = "DemoArray"
End Code

@If (Model.Length > 0) Then
    @<table>
        <thead>
            <tr>
                <th>
                    Product
                </th>
                <th>
                    Price
                </th>
            </tr>
        </thead>
        <tbody>
            @For Each p As Product In Model
                @<tr>
                    <td>
                        @p.Name
                    </td>
                    <td>
                        @p.Price
                    </td>
                </tr>
    Next
        </tbody>
    </table>
Else
    @<h2>No product data</h2>
End If