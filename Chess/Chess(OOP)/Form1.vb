Imports System.Math

Public Class Chess

    Dim Button(8, 8) As Button
    Dim board(8, 8) As Piece

    Dim over, turn As Boolean

    Dim wRmoved(2), bRmoved(2) As Boolean

    Dim wKmoved, bKmoved As Boolean

    Dim row1, col1, row2, col2 As Integer

    Dim bestRow1, bestCol1, bestRow2, bestCol2 As Integer

    Function ElemExists(ByVal list As List(Of Cell), ByVal elem As Cell)

        Dim exists As Boolean = False

        For Each c As Cell In list

            If c.row = elem.row And c.col = elem.col Then

                exists = True

                Exit For

            End If

        Next

        Return exists

    End Function

    Function PossibleMoveExists(ByVal turn As Boolean)

        Dim exists As Boolean = False

        For i = 0 To 7

            For j = 0 To 7

                If board(i, j).color = turn Then

                    Dim cnt As Integer = 0

                    For Each c As Cell In board(i, j).generate_moves(board)

                        If Not PutKingInCheck(i, j, c.row, c.col, turn) And MoveValid(i, j, c.row, c.col, turn) Then

                            cnt += 1

                        End If

                    Next

                    If cnt > 0 Then

                        exists = True

                        GoTo out

                    End If

                End If

            Next

        Next

out:

        Return exists

    End Function

    Sub MoveType(ByVal piece1 As Piece, ByRef piece2 As Piece, ByVal cell As Cell)

        If TypeOf (piece1) Is wPawn Then

            piece2 = New wPawn(cell.row, cell.col)

        ElseIf TypeOf (piece1) Is bPawn Then

            piece2 = New bPawn(cell.row, cell.col)

        ElseIf TypeOf (piece1) Is Rook Then

            piece2 = New Rook(cell.row, cell.col, piece1.color)

        ElseIf TypeOf (piece1) Is Knight Then

            piece2 = New Knight(cell.row, cell.col, piece1.color)

        ElseIf TypeOf (piece1) Is Bishop Then

            piece2 = New Bishop(cell.row, cell.col, piece1.color)

        ElseIf TypeOf (piece1) Is Queen Then

            piece2 = New Queen(cell.row, cell.col, piece1.color)

        ElseIf TypeOf (piece1) Is King Then

            piece2 = New King(cell.row, cell.col, piece1.color)

        ElseIf TypeOf (piece1) Is Null Then

            piece2 = New Null()

        End If

    End Sub

    Sub PieceMove(ByVal row1 As Integer, ByVal col1 As Integer, ByVal row2 As Integer, ByVal col2 As Integer)

        If TypeOf (board(row1, col1)) Is wPawn Then

            If row2 = 0 Then

                board(row2, col2) = New Queen(row2, col2, False)

            Else

                MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

            End If

            board(row1, col1) = New Null()

        ElseIf TypeOf (board(row1, col1)) Is bPawn Then

            If row2 = 7 Then

                board(row2, col2) = New Queen(row2, col2, True)

            Else

                MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

            End If

            board(row1, col1) = New Null()

        ElseIf TypeOf (board(row1, col1)) Is Rook Then

            If Not board(row1, col1).color Then

                If row1 = 7 Then

                    If col1 = 0 Then

                        wRmoved(0) = True

                    ElseIf col1 = 7 Then

                        wRmoved(1) = True

                    End If

                End If

            Else

                If row1 = 0 Then

                    If col1 = 0 Then

                        bRmoved(0) = True

                    ElseIf col1 = 7 Then

                        bRmoved(1) = True

                    End If

                End If

            End If

            MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

            board(row1, col1) = New Null()

        ElseIf TypeOf (board(row1, col1)) Is King Then

            If Not board(row1, col1).color Then

                If Not wKmoved Then

                    If col2 = 2 Then

                        If TypeOf (board(7, 1)) Is Null Then

                            If Not wRmoved(0) Then

                                If Not Checked(False) Then

                                    If IsSafe(7, 3, False) Then

                                        MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                                        board(row1, col1) = New Null()

                                        MoveType(board(7, 0), board(7, 3), New Cell(7, 3))

                                        board(7, 0) = New Null()

                                        wRmoved(0) = True

                                        wKmoved = True

                                    End If

                                End If

                            End If

                        End If

                    ElseIf col2 = 6 Then

                        If Not wRmoved(1) Then

                            If Not Checked(False) Then

                                If IsSafe(7, 5, False) Then

                                    MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                                    board(row1, col1) = New Null()

                                    MoveType(board(7, 7), board(7, 5), New Cell(7, 5))

                                    board(7, 7) = New Null()

                                    wRmoved(1) = True

                                    wKmoved = True

                                End If

                            End If

                        End If

                    Else

                        MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                        board(row1, col1) = New Null()

                        wKmoved = True

                    End If

                Else

                    If Abs(row1 - row2) <= 1 And Abs(col1 - col2) <= 1 Then

                        MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                        board(row1, col1) = New Null()

                    End If

                End If

            Else

                If Not bKmoved Then

                    If col2 = 2 Then

                        If TypeOf (board(0, 1)) Is Null Then

                            If Not bRmoved(0) Then

                                If Not Checked(True) Then

                                    If IsSafe(0, 3, True) Then

                                        MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                                        board(row1, col1) = New Null()

                                        MoveType(board(0, 0), board(0, 3), New Cell(0, 3))

                                        board(0, 0) = New Null()

                                        bRmoved(0) = True

                                        bKmoved = True

                                    End If

                                End If

                            End If

                        End If

                    ElseIf col2 = 6 Then

                        If Not bRmoved(1) Then

                            If Not Checked(True) Then

                                If IsSafe(0, 5, True) Then

                                    MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                                    board(row1, col1) = New Null()

                                    MoveType(board(0, 7), board(0, 5), New Cell(0, 5))

                                    board(0, 7) = New Null()

                                    bRmoved(1) = True

                                    bKmoved = True

                                End If

                            End If

                        End If

                    Else

                        MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                        board(row1, col1) = New Null()

                        bKmoved = True

                    End If

                Else

                    If Abs(row1 - row2) <= 1 And Abs(col1 - col2) <= 1 Then

                        MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

                        board(row1, col1) = New Null()

                    End If

                End If

            End If

        Else

            MoveType(board(row1, col1), board(row2, col2), New Cell(row2, col2))

            board(row1, col1) = New Null()

        End If



    End Sub

    Sub PieceUndo(ByVal temp(,) As Piece, ByVal wRmovedTemp() As Boolean, ByVal bRmovedTemp() As Boolean, ByVal wKmovedTemp As Boolean, ByVal bKmovedTemp As Boolean)

        For i = 0 To 7

            For j = 0 To 7

                MoveType(temp(i, j), board(i, j), New Cell(i, j))

            Next

        Next

        wRmoved(0) = wRmovedTemp(0)
        wRmoved(1) = wRmovedTemp(1)

        bRmoved(0) = bRmovedTemp(0)
        bRmoved(1) = bRmovedTemp(1)

        wKmoved = wKmovedTemp

        bKmoved = bKmovedTemp

    End Sub

    Function Checked(ByVal color As Boolean)

        Dim kRow, kCol As Integer
        Dim danger(8, 8) As Boolean

        For i = 0 To 7

            For j = 0 To 7

                If TypeOf (board(i, j)) Is King And board(i, j).color = color Then

                    kRow = i
                    kCol = j

                    GoTo out

                End If

            Next

        Next

out:

        For i = 0 To 7

            For j = 0 To 7

                If board(i, j).color = Not color Then

                    For Each c As Cell In board(i, j).generate_moves(board)

                        danger(c.row, c.col) = True

                    Next

                End If

            Next

        Next

        Return danger(kRow, kCol)

    End Function

    Function IsSafe(ByVal row As Integer, ByVal col As Integer, ByVal color As Boolean)

        Dim danger(8, 8) As Boolean

        For i = 0 To 7

            For j = 0 To 7

                If board(i, j).color = Not color Then

                    For Each c As Cell In board(i, j).generate_moves(board)

                        danger(c.row, c.col) = True

                    Next

                End If

            Next

        Next

        Return danger(row, col) = False

    End Function

    Function PutKingInCheck(ByVal row1 As Integer, ByVal col1 As Integer, ByVal row2 As Integer, ByVal col2 As Integer, ByVal color As Boolean)

        Dim temp(8, 8) As Piece

        For i = 0 To 7

            For j = 0 To 7

                MoveType(board(i, j), temp(i, j), New Cell(i, j))

            Next

        Next

        Dim wRmovedTemp(2), bRMovedTemp(2) As Boolean
        Dim wKmovedTemp, bKmovedTemp As Boolean

        wRmovedTemp(0) = wRmoved(0)
        wRmovedTemp(1) = wRmoved(1)

        bRMovedTemp(0) = bRmoved(0)
        bRMovedTemp(1) = bRmoved(1)

        wKmovedTemp = wKmoved

        bKmovedTemp = bKmoved

        PieceMove(row1, col1, row2, col2)

        Dim inCheck As Boolean = Checked(color)

        PieceUndo(temp, wRmovedTemp, bRMovedTemp, wKmovedTemp, bKmovedTemp)

        Return inCheck

        Return True

    End Function

    Function MoveValid(ByVal row1 As Integer, ByVal col1 As Integer, ByVal row2 As Integer, ByVal col2 As Integer, ByVal color As Boolean)

        Dim valid As Boolean = True

        Dim temp(8, 8) As Piece

        For i = 0 To 7

            For j = 0 To 7

                MoveType(board(i, j), temp(i, j), New Cell(i, j))

            Next

        Next

        Dim wRmovedTemp(2), bRMovedTemp(2) As Boolean
        Dim wKmovedTemp, bKmovedTemp As Boolean

        wRmovedTemp(0) = wRmoved(0)
        wRmovedTemp(1) = wRmoved(1)

        bRMovedTemp(0) = bRmoved(0)
        bRMovedTemp(1) = bRmoved(1)

        wKmovedTemp = wKmoved

        bKmovedTemp = bKmoved

        PieceMove(row1, col1, row2, col2)

        If board(row1, col1).val <> 0 Then

            valid = False

        End If

        PieceUndo(temp, wRmovedTemp, bRMovedTemp, wKmovedTemp, bKmovedTemp)

        Return valid

    End Function

    Function Winner(ByVal color As Boolean)

        Dim win As Boolean = False

        If Checked(Not color) Then

            Dim canEsc As Boolean = False

            For i = 0 To 7

                For j = 0 To 7

                    If board(i, j).color = Not color Then

                        For Each c As Cell In board(i, j).generate_moves(board)

                            If Not PutKingInCheck(i, j, c.row, c.col, Not color) And MoveValid(i, j, c.row, c.col, Not color) Then

                                canEsc = True

                                GoTo esc

                            End If

                        Next

                    End If

                Next

            Next

esc:

            If Not canEsc Then

                win = True

            End If

        End If

        Return win

    End Function

    Function SBE(ByVal isMax As Boolean)

        If Winner(True) Then

            Return Integer.MaxValue - 10

        ElseIf Winner(False) Then

            Return Integer.MinValue + 10

        ElseIf Not PossibleMoveExists(isMax) Then

            Return 0

        Else

            Dim totEval As Integer = 0

            'Pieces score

            For i = 0 To 7

                For j = 0 To 7

                    Dim absVal As Integer = Math.Abs(board(i, j).val) + board(i, j).eval(i, j)

                    If board(i, j).color Then

                        totEval += -absVal

                    Else

                        totEval += absVal

                    End If

                Next

            Next

            'Safe spots

            For i = 0 To 7

                For j = 0 To 7

                    If IsSafe(i, j, True) Then

                        totEval += 1

                    ElseIf IsSafe(i, j, False) Then

                        totEval -= 10

                    End If

                Next

            Next

            'Total Evaluation

            Return totEval

        End If

    End Function

    Function Minimax(ByVal depth As Integer, ByVal isMax As Boolean, ByVal alpha As Integer, ByVal beta As Integer)

        If depth = 1 Or Winner(True) Or Winner(False) Or Not PossibleMoveExists(isMax) Then

            Dim score As Integer = -SBE(isMax)

            Return score - depth

        Else

            If isMax Then

                Dim best As Integer = Integer.MinValue + 5

                For i = 0 To 7

                    For j = 0 To 7

                        If board(i, j).color Then

                            For Each c As Cell In board(i, j).generate_moves(board)

                                If Not PutKingInCheck(i, j, c.row, c.col, isMax) And MoveValid(i, j, c.row, c.col, isMax) Then

                                    Dim temp(8, 8) As Piece

                                    For y = 0 To 7

                                        For x = 0 To 7

                                            MoveType(board(y, x), temp(y, x), New Cell(y, x))

                                        Next

                                    Next

                                    Dim wRmovedTemp(2), bRMovedTemp(2) As Boolean
                                    Dim wKmovedTemp, bKmovedTemp As Boolean

                                    wRmovedTemp(0) = wRmoved(0)
                                    wRmovedTemp(1) = wRmoved(1)

                                    bRMovedTemp(0) = bRmoved(0)
                                    bRMovedTemp(1) = bRmoved(1)

                                    wKmovedTemp = wKmoved

                                    bKmovedTemp = bKmoved

                                    PieceMove(i, j, c.row, c.col)
                                    best = Max(best, Minimax(depth + 1, Not isMax, alpha, beta))
                                    PieceUndo(temp, wRmovedTemp, bRMovedTemp, wKmovedTemp, bKmovedTemp)

                                    alpha = Max(alpha, best)

                                    If alpha >= beta Then

                                        GoTo out1

                                    End If

                                End If

                            Next

                        End If

                    Next

                Next

out1:

                Return best

            Else

                Dim best As Integer = Integer.MaxValue - 5

                For i = 0 To 7

                    For j = 0 To 7

                        If Not board(i, j).color Then

                            For Each c As Cell In board(i, j).generate_moves(board)

                                If Not PutKingInCheck(i, j, c.row, c.col, isMax) And MoveValid(i, j, c.row, c.col, isMax) Then

                                    Dim temp(8, 8) As Piece

                                    For y = 0 To 7

                                        For x = 0 To 7

                                            MoveType(board(y, x), temp(y, x), New Cell(y, x))

                                        Next

                                    Next

                                    Dim wRmovedTemp(2), bRMovedTemp(2) As Boolean
                                    Dim wKmovedTemp, bKmovedTemp As Boolean

                                    wRmovedTemp(0) = wRmoved(0)
                                    wRmovedTemp(1) = wRmoved(1)

                                    bRMovedTemp(0) = bRmoved(0)
                                    bRMovedTemp(1) = bRmoved(1)

                                    wKmovedTemp = wKmoved

                                    bKmovedTemp = bKmoved

                                    PieceMove(i, j, c.row, c.col)
                                    best = Min(best, Minimax(depth + 1, Not isMax, alpha, beta))
                                    PieceUndo(temp, wRmovedTemp, bRMovedTemp, wKmovedTemp, bKmovedTemp)

                                    beta = Min(beta, best)

                                    If alpha >= beta Then

                                        GoTo out2

                                    End If

                                End If

                            Next

                        End If

                    Next

                Next

out2:

                Return best

            End If

        End If

    End Function

    Sub FindBestMove()

        Dim bestScore As Integer = Integer.MinValue

        For i = 0 To 7

            For j = 0 To 7

                If board(i, j).color Then

                    For Each c As Cell In board(i, j).generate_moves(board)

                        If Not PutKingInCheck(i, j, c.row, c.col, True) And MoveValid(i, j, c.row, c.col, True) Then

                            Dim temp(8, 8) As Piece

                            For y = 0 To 7

                                For x = 0 To 7

                                    MoveType(board(y, x), temp(y, x), New Cell(y, x))

                                Next

                            Next

                            Dim wRmovedTemp(2), bRMovedTemp(2) As Boolean
                            Dim wKmovedTemp, bKmovedTemp As Boolean

                            wRmovedTemp(0) = wRmoved(0)
                            wRmovedTemp(1) = wRmoved(1)

                            bRMovedTemp(0) = bRmoved(0)
                            bRMovedTemp(1) = bRmoved(1)

                            wKmovedTemp = wKmoved

                            bKmovedTemp = bKmoved

                            PieceMove(i, j, c.row, c.col)
                            Dim moveScore As Integer = Minimax(0, False, Integer.MinValue + 5, Integer.MaxValue - 5)
                            PieceUndo(temp, wRmovedTemp, bRMovedTemp, wKmovedTemp, bKmovedTemp)

                            If moveScore > bestScore Then

                                bestScore = moveScore

                                bestRow1 = i
                                bestCol1 = j

                                bestRow2 = c.row
                                bestCol2 = c.col

                            End If

                        End If

                    Next

                End If

            Next

        Next

    End Sub

    Private Sub Chess_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        board(0, 0) = New Rook(0, 0, True)
        board(0, 1) = New Knight(0, 1, True)
        board(0, 2) = New Bishop(0, 2, True)
        board(0, 3) = New Queen(0, 3, True)
        board(0, 4) = New King(0, 4, True)
        board(0, 5) = New Bishop(0, 5, True)
        board(0, 6) = New Knight(0, 6, True)
        board(0, 7) = New Rook(0, 7, True)

        For j = 0 To 7

            board(1, j) = New bPawn(1, j)

        Next

        board(7, 0) = New Rook(7, 0, False)
        board(7, 1) = New Knight(7, 1, False)
        board(7, 2) = New Bishop(7, 2, False)
        board(7, 3) = New Queen(7, 3, False)
        board(7, 4) = New King(7, 4, False)
        board(7, 5) = New Bishop(7, 5, False)
        board(7, 6) = New Knight(7, 6, False)
        board(7, 7) = New Rook(7, 7, False)

        For j = 0 To 7

            board(6, j) = New wPawn(6, j)

        Next

        For i = 2 To 5

            For j = 0 To 7

                board(i, j) = New Null()

            Next

        Next

        Dim cnt As Integer = 1

        For i = 0 To 7

            For j = 0 To 7

                Button(i, j) = DirectCast(Me.Controls("Button" & cnt), Button)

                Button(i, j).ForeColor = Nothing

                If i = 0 Then
                    If j = 0 Or j = 7 Then
                        Button(i, j).Image = Images.bRook
                    ElseIf j = 1 Or j = 6 Then
                        Button(i, j).Image = Images.bKnight
                    ElseIf j = 2 Or j = 5 Then
                        Button(i, j).Image = Images.bBishop
                    ElseIf j = 3 Then
                        Button(i, j).Image = Images.bQueen
                    Else
                        Button(i, j).Image = Images.bKing
                    End If
                ElseIf i = 1 Then
                    Button(i, j).Image = Images.bPawn
                ElseIf i = 6 Then
                    Button(i, j).Image = Images.wPawn
                ElseIf i = 7 Then
                    If j = 0 Or j = 7 Then
                        Button(i, j).Image = Images.wRook
                    ElseIf j = 1 Or j = 6 Then
                        Button(i, j).Image = Images.wKnight
                    ElseIf j = 2 Or j = 5 Then
                        Button(i, j).Image = Images.wBishop
                    ElseIf j = 3 Then
                        Button(i, j).Image = Images.wQueen
                    Else
                        Button(i, j).Image = Images.wKing
                    End If
                Else
                    Button(i, j).Image = Nothing
                End If

                cnt += 1

            Next

        Next

        over = False

        wRmoved(0) = False
        wRmoved(1) = False

        bRmoved(0) = False
        bRmoved(1) = False

        wKmoved = False

        bKmoved = False

        row1 = -1
        col1 = -1

        row2 = -1
        col2 = -1

        turn = False

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        bestRow1 = -1
        bestCol1 = -1
        bestRow2 = -1
        bestCol2 = -1

        Me.Text = "Thinking..."

        FindBestMove()

        Me.Text = "Chess"

        PieceMove(bestRow1, bestCol1, bestRow2, bestCol2)

        turn = False

        For k = 1 To 64

            Dim b As Button = DirectCast(Me.Controls("Button" & k), Button)
            Dim row As Integer = (k - 1) \ 8
            Dim col As Integer = (k - 1) Mod 8

            b.Image = board(row, col).img
            b.ForeColor = Nothing

            If row = bestRow1 And col = bestCol1 Then

                b.ForeColor = Color.Blue

            End If

            If row = bestRow2 And col = bestCol2 Then

                b.ForeColor = Color.Blue

            End If

        Next

        Timer1.Enabled = False

        If Winner(True) Then

            over = True

            MsgBox("White checkmate!")

        ElseIf Winner(False) Then

            over = True

            MsgBox("Black checkmate!")

        ElseIf PossibleMoveExists(turn) = False Then

            over = True

            MsgBox("Stalemate!")

        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click, Button8.Click, Button7.Click, Button64.Click, Button63.Click, Button62.Click, Button61.Click, Button60.Click, Button6.Click, Button59.Click, Button58.Click, Button57.Click, Button56.Click, Button55.Click, Button54.Click, Button53.Click, Button52.Click, Button51.Click, Button50.Click, Button5.Click, Button49.Click, Button48.Click, Button47.Click, Button46.Click, Button45.Click, Button44.Click, Button43.Click, Button42.Click, Button41.Click, Button40.Click, Button4.Click, Button39.Click, Button38.Click, Button37.Click, Button36.Click, Button35.Click, Button34.Click, Button33.Click, Button32.Click, Button31.Click, Button30.Click, Button3.Click, Button29.Click, Button28.Click, Button27.Click, Button26.Click, Button25.Click, Button24.Click, Button23.Click, Button22.Click, Button21.Click, Button20.Click, Button2.Click, Button19.Click, Button18.Click, Button17.Click, Button16.Click, Button15.Click, Button14.Click, Button13.Click, Button12.Click, Button11.Click, Button10.Click, Button1.Click

        If over = False Then

            For i = 0 To 7

                For j = 0 To 7

                    If Button(i, j) Is sender Then

                        If row1 = -1 And col1 = -1 Then

                            row1 = i
                            col1 = j

                        ElseIf row2 = -1 And col2 = -1 Then

                            row2 = i
                            col2 = j

                        End If

                        GoTo out

                    End If

                Next

            Next

out:

            If Not board(row1, col1).color Then

                If row1 <> -1 And col1 <> -1 And row2 <> -1 And col2 <> -1 Then

                    If Not Winner(True) And Not Winner(False) And PossibleMoveExists(turn) Then

                        If ElemExists(board(row1, col1).generate_moves(board), New Cell(row2, col2)) Then

                            If Not PutKingInCheck(row1, col1, row2, col2, False) And MoveValid(row1, col1, row2, col2, False) Then

                                PieceMove(row1, col1, row2, col2)

                                turn = True

                                For k = 1 To 64

                                    Dim b As Button = DirectCast(Me.Controls("Button" & k), Button)
                                    Dim row As Integer = (k - 1) \ 8
                                    Dim col As Integer = (k - 1) Mod 8

                                    b.Image = board(row, col).img

                                Next

                                If Not Winner(True) And Not Winner(False) And PossibleMoveExists(turn) Then

                                    Timer1.Enabled = True

                                End If

                                row1 = -1
                                col1 = -1

                            Else

                                If Not board(row2, col2).color Then

                                    row1 = row2
                                    col1 = col2

                                Else

                                    row1 = -1
                                    col1 = -1

                                End If

                            End If

                        Else

                            If Not board(row2, col2).color Then

                                row1 = row2
                                col1 = col2

                            Else

                                row1 = -1
                                col1 = -1

                            End If

                        End If

                        row2 = -1
                        col2 = -1

                    End If

                    If Winner(True) Then

                        over = True

                        MsgBox("White checkmate!")

                    ElseIf Winner(False) Then

                        over = True

                        MsgBox("Black checkmate!")

                    ElseIf PossibleMoveExists(turn) = False Then

                        over = True

                        MsgBox("Stalemate!")

                    End If

                End If

            Else

                row1 = -1
                col1 = -1

                row2 = -1
                col2 = -1

            End If

        End If

    End Sub

    Private Sub RestartBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestartBtn.Click

        Chess_Load(sender, e)

    End Sub

End Class
