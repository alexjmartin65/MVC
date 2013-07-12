@ModelType Decimal

@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div>
        Total value is @Model.ToString("c")
    </div>
</body>
</html>
