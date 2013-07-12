@ModelType SportsStore.WebUI.Models.LoginViewModel

@Code
    ViewData("Title") = "Login"
    Layout = "~/Views/Shared/_AdminLayout.vbhtml"
End Code

<h1>Log In</h1>


<p>Please login to access the administrative area:</p>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @Html.EditorForModel()
    @<p><input type="submit" value="Log In" /></p>
End Using
