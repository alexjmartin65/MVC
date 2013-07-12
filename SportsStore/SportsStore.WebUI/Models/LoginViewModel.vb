Imports System.ComponentModel.DataAnnotations


Namespace Models

    Public Class LoginViewModel

        <Required>
        Public Property UserName As String

        <Required>
        <DataType(DataType.Password)>
        Public Property Password As String

    End Class

End Namespace