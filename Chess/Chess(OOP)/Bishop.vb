Public Class Bishop
    Inherits Piece

    Public Sub New(ByVal row As Integer, ByVal col As Integer, ByVal color As Boolean)

        Me.row = row
        Me.col = col
        Me.color = color

        Dim bishopEval(,) As Integer = {
            {-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
            {-1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0},
            {-1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0},
            {-1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0},
            {-1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0},
            {-1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0},
            {-1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0},
            {-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0}
        }

        If Me.color Then

            Me.val = -30
            Me.eval = reverse(bishopEval)
            Me.img = Images.bBishop

        Else

            Me.val = 30
            Me.eval = bishopEval
            Me.img = Images.wBishop

        End If

    End Sub

    Overrides Function generate_moves(ByVal board(,) As Piece)

        Dim moves As New List(Of Cell)

        Dim i As Integer = row - 1
        Dim j As Integer = col + 1

        Do While i >= 0 And j <= 7

            If TypeOf (board(i, j)) Is Null Then

                moves.Add(New Cell(i, j))

            Else

                If board(i, j).color = Not Me.color Then

                    moves.Add(New Cell(i, j))

                End If

                Exit Do

            End If

            i -= 1
            j += 1

        Loop

        i = row + 1
        j = col + 1

        Do While i <= 7 And j <= 7

            If TypeOf (board(i, j)) Is Null Then

                moves.Add(New Cell(i, j))

            Else

                If board(i, j).color = Not Me.color Then

                    moves.Add(New Cell(i, j))

                End If

                Exit Do

            End If

            i += 1
            j += 1

        Loop

        i = row + 1
        j = col - 1

        Do While i <= 7 And j >= 0

            If TypeOf (board(i, j)) Is Null Then

                moves.Add(New Cell(i, j))

            Else

                If board(i, j).color = Not Me.color Then

                    moves.Add(New Cell(i, j))

                End If

                Exit Do

            End If

            i += 1
            j -= 1

        Loop

        i = row - 1
        j = col - 1

        Do While i >= 0 And j >= 0

            If TypeOf (board(i, j)) Is Null Then

                moves.Add(New Cell(i, j))

            Else

                If board(i, j).color = Not Me.color Then

                    moves.Add(New Cell(i, j))

                End If

                Exit Do

            End If

            i -= 1
            j -= 1

        Loop

        Return moves

    End Function

End Class
