Imports SportsStore.Domain.Abstract
Imports SportsStore.Domain.Entities
Imports System.Net.Mail
Imports System.Net
Imports System.Text

Namespace Concrete

    Public Class EmailSettings

        Public MailToAddress As String = "orders@example.com"
        Public MailFromAddress As String = "orders@example.com"
        Public UseSSL As Boolean = True
        Public UserName As String = "My User Name"
        Public Password As String = "Password"
        Public ServerName As String = "smtp.example.com"
        Public ServerPort As Integer = 587
        Public WriteAsFile As Boolean = False
        Public FileLocation As String = "C:\Sports_Store_Emails"

    End Class

    Public Class EmailOrderProcessor
        Implements IOrderProcessor

        Private emailSettings As EmailSettings

        Public Sub New(settings As EmailSettings)
            emailSettings = settings
        End Sub

        Public Sub ProcessOrder(cart As Cart, shippingInfo As ShippingDetails) Implements IOrderProcessor.ProcessOrder
            Using smtpClient As New SmtpClient
                smtpClient.EnableSsl = emailSettings.UseSSL
                smtpClient.Host = emailSettings.ServerName
                smtpClient.Port = emailSettings.ServerPort
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = New NetworkCredential(emailSettings.UserName, emailSettings.Password)

                If (emailSettings.WriteAsFile) Then
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation
                    smtpClient.EnableSsl = False
                End If

                Dim body As New StringBuilder()
                For Each line As CartLine In cart.Lines
                    Dim subTotal = line.Product.Price * line.Quantity
                    body.AppendFormat("{0} x {1} (subtotal {2:c})", line.Quantity, line.Product.Name, subTotal)
                Next

                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValues()).AppendLine("---"). _
                    AppendLine("Ship to:"). _
                    AppendLine(shippingInfo.Name). _
                    AppendLine(shippingInfo.Line1). _
                    AppendLine(If(IsNothing(shippingInfo.Line2), shippingInfo.Line2, String.Empty)). _
                    AppendLine(If(IsNothing(shippingInfo.Line3), shippingInfo.Line3, String.Empty)). _
                    AppendLine(shippingInfo.City). _
                    AppendLine(If(IsNothing(shippingInfo.State), shippingInfo.State, String.Empty)). _
                    AppendLine(shippingInfo.Country). _
                    AppendLine(shippingInfo.Zip). _
                    AppendLine("---"). _
                    AppendFormat("Gift wrap: {0}",
                        If(shippingInfo.GiftWrap, "Yes", "No"))

                Dim mailMessage As New MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, "New order submitted", body.ToString())

                If (emailSettings.WriteAsFile) Then
                    mailMessage.BodyEncoding = Encoding.ASCII
                End If

                smtpClient.Send(mailMessage)

            End Using
        End Sub

    End Class

End Namespace