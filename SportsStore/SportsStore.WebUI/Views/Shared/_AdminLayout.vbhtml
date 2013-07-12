<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <link href="~/Content/Admin.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="~/Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <script src="~/Scripts/jquery.validate.unobtrusive.min.js" type="text/javascript"></script>
    <title>@ViewData("Title")</title>
</head>
<body>
    <div>
        @Code
            If (TempData("message") IsNot Nothing) Then
                @<div class="message">@TempData("message")</div>
            End If
        End Code
        @RenderBody()
    </div>
</body>
</html>
