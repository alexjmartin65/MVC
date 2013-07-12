Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

Namespace Entities

    Public Class Product

        <HiddenInput(DisplayValue:=False)>
        Public Property ProductID As Integer
        <Required(ErrorMessage:="Please enter a product name")>
        Public Property Name As String
        <DataType(DataType.MultilineText)>
        <Required(ErrorMessage:="Please enter a description")>
        Public Property Description As String
        <Required>
        <Range(0.01, Double.MaxValue, ErrorMessage:="Please enter a positive price")>
        Public Property Price As Decimal
        <Required(ErrorMessage:="Please enter a category")>
        Public Property Category As String

        Public Property ImageData As Byte()

        <HiddenInput(DisplayValue:=False)>
        Public Property ImageMimeType As String

    End Class

End Namespace
