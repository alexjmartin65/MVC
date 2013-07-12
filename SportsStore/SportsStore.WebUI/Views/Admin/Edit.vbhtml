@ModelType SportsStore.Domain.Entities.Product

@Code
    ViewData("Title") = "Edit"
    Layout = "~/Views/Shared/_AdminLayout.vbhtml"
End Code

<h2>Edit @Model.Name</h2>

@Using Html.BeginForm("Edit", "Admin", FormMethod.Post, New With {.enctype = "multipart/form-data" })
    @Html.EditorForModel()

    @<div class="editor-label">
        Image
     </div>
    @<div class="editor-field">
        @If (Model.ImageData Is Nothing) Then
            @:None 
        Else
            @<img style="width: 150px; height: 150px" src="@Url.Action("GetImage", "Product", New With {Model.ProductID})" />
        End If
        <div>Upload new image: <input type="file" name="Image" /></div>
     </div>    
    
    @<input type="submit" value="save" />
    @Html.ActionLink("Cancel and return to list", "Index")
End Using
