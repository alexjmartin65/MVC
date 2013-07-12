Imports System.ComponentModel.DataAnnotations

Namespace Entities

    Public Class ShippingDetails

        <Required(ErrorMessage:="Please enter a name")>
        Public Property Name As String

        <Required(ErrorMessage:="Please enter the first address line")>
        Public Property Line1 As String
        Public Property Line2 As String
        Public Property Line3 As String

        <Required(ErrorMessage:="Please enter a city name")>
        Public Property City As String

        <Required(ErrorMessage:="Please enter a state name")>
        Public Property State As String

        Public Property Zip As String

        <Required(ErrorMessage:="Please enter a country name")>
        Public Property Country As String

        Public Property GiftWrap As Boolean

    End Class

End Namespace
