Public Class Knight
    Inherits Piece

    Public Sub New(ByVal row As Integer, ByVal col As Integer, ByVal color As Boolean)

        Me.row = row
        Me.col = col
        Me.color = color

        Me.eval = {
            {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
            {-4.0, -2.0, 0.0, 0.0, 0.0, 0.0, -2.0, -4.0},
            {-3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0},
            {-3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0},
            {-3.0, 0.0, 1.5, 2.0, 2.0, 1.5, 0.0, -3.0},
            {-3.0, 0.5, 1.0, 1.5, 1.5, 1.0, 0.5, -3.0},
            {-4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0},
            {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}
        }

        If Me.color Then

            Me.val = -30
            Me.img = Images.bKnight

        Else

            Me.val = 30
            Me.img = Images.wKnight

        End If

    End Sub

    Overrides Function generate_moves(ByVal board(,) As Piece)

        Dim moves As New List(Of Cell)

        Dim dx() As Integer = {1, 2, 2, 1, -1, -2, -2, -1}
        Dim dy() As Integer = {-2, -1, 1, 2, 2, 1, -1, -2}

        Dim move As Integer = 0

        Do While move < 8

            If Me.row + dy(move) >= 0 And Me.row + dy(move) <= 7 And Me.col + dx(move) >= 0 And Me.col + dx(move) <= 7 Then

                If TypeOf (board(Me.row + dy(move), Me.col + dx(move))) Is Null Or board(Me.row + dy(move), Me.col + dx(move)).color = Not Me.color Then

                    moves.Add(New Cell(Me.row + dy(move), Me.col + dx(move)))

                End If

            End If

            move += 1

        Loop

        Return moves

    End Function

End Class
