Imports SportsStore.Domain.Entities

Namespace Abstract

    Public Interface IOrderProcessor

        Sub ProcessOrder(cart As Cart, shippingDetails As ShippingDetails)

    End Interface

End Namespace