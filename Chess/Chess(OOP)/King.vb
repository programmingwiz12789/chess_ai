Public Class King
    Inherits Piece

    Public Sub New(ByVal row As Integer, ByVal col As Integer, ByVal color As Boolean)

        Me.row = row
        Me.col = col
        Me.color = color

        Dim kingEval(,) As Integer = {
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
            {-1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
            {2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0},
            {2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0}
        }

        If Me.color Then

            Me.val = -900
            Me.eval = reverse(kingEval)
            Me.img = Images.bKing

        Else

            Me.val = 900
            Me.eval = kingEval
            Me.img = Images.wKing

        End If

    End Sub

    Overrides Function generate_moves(ByVal board(,) As Piece)

        Dim moves As New List(Of Cell)

        Dim dx() As Integer = {1, 1, 1, 0, -1, -1, -1, 0}
        Dim dy() As Integer = {-1, 0, 1, 1, 1, 0, -1, -1}

        Dim move As Integer = 0

        Do While move < 8

            If Me.row + dy(move) >= 0 And Me.row + dy(move) <= 7 And Me.col + dx(move) >= 0 And Me.col + dx(move) <= 7 Then

                If TypeOf (board(Me.row + dy(move), Me.col + dx(move))) Is Null Or board(Me.row + dy(move), Me.col + dx(move)).color = Not Me.color Then

                    moves.Add(New Cell(Me.row + dy(move), Me.col + dx(move)))

                End If

            End If

            move += 1

        Loop

        If Me.col - 2 >= 0 Then

            If TypeOf (board(Me.row, Me.col - 1)) Is Null And TypeOf (board(Me.row, Me.col - 2)) Is Null Then

                moves.Add(New Cell(Me.row, Me.col - 2))

            End If

        End If

        If Me.col + 2 <= 7 Then

            If TypeOf (board(Me.row, Me.col + 1)) Is Null And TypeOf (board(Me.row, Me.col + 2)) Is Null Then

                moves.Add(New Cell(Me.row, Me.col + 2))

            End If

        End If

        Return moves

    End Function

End Class
