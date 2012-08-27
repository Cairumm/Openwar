Public Class HumanPlayer
    Inherits Player

    Public Sub New()
        MyBase.New()
        Me.IsAIPlayer = False
    End Sub

    Public Overrides Sub BuildBase()
        MyBase.BuildBase()
    End Sub

    Public Overrides Sub PeacetimeTurn()


    End Sub

    Public Overrides Sub Spy()
        MyBase.Spy()
    End Sub

    Public Overrides Sub WartimeTurn()
        MyBase.WartimeTurn()
    End Sub
End Class
