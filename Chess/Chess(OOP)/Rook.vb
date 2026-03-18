Public Class Rook
    Inherits Piece

    Public Sub New(ByVal row As Integer, ByVal col As Integer, ByVal color As Boolean)

        Me.row = row
        Me.col = col
        Me.color = color

        Dim rookEval(,) As Integer = {
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            {0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5},
            {0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0}
        }

        If Me.color Then

            Me.val = -50
            Me.eval = reverse(rookEval)
            Me.img = Images.bRook

        Else

            Me.val = 50
            Me.eval = rookEval
            Me.img = Images.wRook

        End If

    End Sub

    Overrides Function generate_moves(ByVal board(,) As Piece)

        Dim moves As New List(Of Cell)

        For i = Me.row - 1 To 0 Step -1

            If TypeOf (board(i, Me.col)) Is Null Then

                moves.Add(New Cell(i, Me.col))

            Else

                If board(i, Me.col).color = Not Me.color Then

                    moves.Add(New Cell(i, Me.col))

                End If

                Exit For

            End If

        Next

        For j = Me.col + 1 To 7

            If TypeOf (board(Me.row, j)) Is Null Then

                moves.Add(New Cell(Me.row, j))

            Else

                If board(Me.row, j).color = Not Me.color Then

                    moves.Add(New Cell(Me.row, j))

                End If

                Exit For

            End If

        Next

        For i = Me.row + 1 To 7

            If TypeOf (board(i, Me.col)) Is Null Then

                moves.Add(New Cell(i, Me.col))

            Else

                If board(i, Me.col).color = Not Me.color Then

                    moves.Add(New Cell(i, Me.col))

                End If

                Exit For

            End If

        Next

        For j = Me.col - 1 To 0 Step -1

            If TypeOf (board(Me.row, j)) Is Null Then

                moves.Add(New Cell(Me.row, j))

            Else

                If board(Me.row, j).color = Not Me.color Then

                    moves.Add(New Cell(Me.row, j))

                End If

                Exit For

            End If

        Next

        Return moves

    End Function

End Class
