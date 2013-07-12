Imports SportsStore.WebUI.Models
Imports System.Text
Imports System.Web.Mvc
Imports System.Runtime.CompilerServices

Namespace HtmlHelpers

    Public Module PagingHelpers

        <Extension()>
        Public Function PageLinks(html As HtmlHelper, pagingInfo As PagingInfo, pageUrl As Func(Of Integer, String)) As MvcHtmlString
            Dim result As New StringBuilder()

            For index As Integer = 1 To pagingInfo.TotalPages
                Dim tag As New TagBuilder("a")
                tag.MergeAttribute("href", pageUrl(index))
                tag.InnerHtml = index.ToString()
                If (index = pagingInfo.CurrentPage) Then
                    tag.AddCssClass("selected")
                End If
                result.Append(tag.ToString())

            Next
            Return MvcHtmlString.Create(result.ToString())

        End Function

    End Module

End Namespace
