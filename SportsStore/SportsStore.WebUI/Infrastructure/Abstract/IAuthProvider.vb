
Namespace Infrastructure.Abstract

    Public Interface IAuthProvider

        Function Authenticate(username As String, password As String) As Boolean

    End Interface

End Namespace