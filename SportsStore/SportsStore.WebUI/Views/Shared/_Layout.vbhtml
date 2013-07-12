<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewData("Title")</title>
    <link href="~/content/site.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <div id="header">
        @Code
            Html.RenderAction("Summary", "Cart")
        End Code
        <div class="title">SPORTS STORE</div>
    </div>
    <div id="categories">
        @Code
            Html.RenderAction("Menu", "Nav")
        End Code
    </div>
    <div id="content">
        @RenderBody()
    </div>
</body>
</html>
