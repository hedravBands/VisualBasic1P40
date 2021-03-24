Public Class Form1
    Dim k As Double
    Dim sequence() As Double
    Function f(ByVal x As Double) As Double
        Return k * x * (1 - x)
    End Function

    Function XC(ByVal x As Double) As Integer
        Return 400 * x
    End Function

    Function YC(ByVal y As Double) As Integer
        Return 400 * (1 - y)
    End Function

    Sub DrawFunctionGraph()
        Dim g As Graphics
        g = PictureBox1.CreateGraphics
        Dim pBlack As New Pen(Color.Black)
        Dim pRed As New Pen(Color.Red)

        'Params
        k = CDbl(kField.Text)
        Dim x0 As Double = CDbl(startField.Text)
        Dim iter As Integer = Val(iField.Text)

        'Origin
        Dim pYellow As New Pen(Color.Yellow)
        g.DrawRectangle(pYellow, 0, 400, 2, 2)

        'Plot Parabola
        Dim x, y As Double
        For x = 0 To 1 Step 0.01
            g.DrawRectangle(pRed, XC(x), YC(f(x)), 1, 1)
        Next

        'PictureBox1.Refresh()
        ComputeSequence(Val(iField.Text))
        For x = 0 To 5


        Next
    End Sub

    Sub DrawDiagonal()
        Dim g As Graphics
        Dim pBlack As New Pen(Color.Black)
        Dim sdRed As New SolidBrush(Color.Red)
        g = PictureBox1.CreateGraphics
        k = CDbl(kField.Text)
        'Axis
        g.DrawLine(pBlack, 0, 0, 0, 400) 'y-axis
        g.DrawLine(pBlack, 0, 400, 400, 400) 'x-axis

        Dim x0 As Double
        x0 = CDbl(startField.Text)
        g.DrawLine(pBlack, XC(0), YC(0), XC(400), YC(400))
        g.FillRectangle(sdRed, XC(x0), YC(x0), 2, 2)

    End Sub

    Sub ComputeSequence(ByVal M As Integer)
        ReDim sequence(M)
        Dim i As Integer
        Dim output As String = ""

        'First line
        sequence(0) = CDbl(startField.Text)

        output &= 0 & vbTab & sequence(0) & vbCrLf
        'M-1 lines
        For i = 1 To M
            sequence(i) = f(sequence(i - 1))
            output &= i & vbTab & sequence(i) & vbCrLf
        Next
        TextOutput.Text = output
    End Sub


    Sub Cobweb(ByVal M As Integer)
        Dim g As Graphics
        Dim pBlue As New Pen(Color.Blue)
        g = PictureBox1.CreateGraphics


        Dim i As Integer
        For i = 0 To M - 1
            g.DrawLine(pBlue, XC(sequence(i)), YC(sequence(i)), XC(sequence(i)), YC(sequence(i + 1)))
            g.DrawLine(pBlue, XC(sequence(i)), YC(sequence(i + 1)), XC(sequence(i + 1)), YC(sequence(i + 1)))
        Next
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        k = CDbl(kField.Text)
        PictureBox1.Refresh()
        DrawFunctionGraph()
        DrawDiagonal()

        Dim M As Integer
        M = CInt(iField.Text)
        ComputeSequence(M)
        Cobweb(M)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        DrawDiagonal()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class
