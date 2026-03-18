Public Class bPawn
    Inherits Piece

    Public Sub New(ByVal row, ByVal col)

        Me.row = row
        Me.col = col
        Me.color = True
        Me.val = -10
        Dim pawnEval(,) As Integer = {
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            {5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0},
            {1.0, 1.0, 2.0, 3.0, 3.0, 2.0, 1.0, 1.0},
            {0.5, 0.5, 1.0, 2.5, 2.5, 1.0, 0.5, 0.5},
            {0.0, 0.0, 0.0, 2.0, 2.0, 0.0, 0.0, 0.0},
            {0.5, -0.5, -1.0, 0.0, 0.0, -1.0, -0.5, 0.5},
            {0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5},
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0}
        }
        Me.eval = reverse(pawnEval)
        Me.img = Images.bPawn

    End Sub

    Overrides Function generate_moves(ByVal board(,) As Piece)

        Dim moves As New List(Of Cell)

        If Me.row + 1 <= 7 Then

            If TypeOf (board(Me.row + 1, Me.col)) Is Null Then

                moves.Add(New Cell(Me.row + 1, Me.col))

            End If

            If Me.col - 1 >= 0 Then

                If Not board(Me.row + 1, Me.col - 1).color Then

                    moves.Add(New Cell(Me.row + 1, Me.col - 1))

                End If

            End If

            If Me.col + 1 <= 7 Then

                If Not board(Me.row + 1, Me.col + 1).color Then

                    moves.Add(New Cell(Me.row + 1, Me.col + 1))

                End If

            End If

        End If

        If Me.row = 1 Then

            If TypeOf (board(Me.row + 1, Me.col)) Is Null And TypeOf (board(Me.row + 2, Me.col)) Is Null Then

                moves.Add(New Cell(Me.row + 2, Me.col))

            End If

        End If

        Return moves

    End Function

End Class
