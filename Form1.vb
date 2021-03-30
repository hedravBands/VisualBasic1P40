Public Class Form1
    Dim a, b As Double
    Dim gMax, gMin As Double
    Dim sequence() As Double
    Function G(ByVal x As Double) As Double
        a = CDbl(aValue.Text)
        b = CDbl(bValue.Text)
        Return -2 * x * x * x + 3 * (a + b) * x * x - 6 * a * b * x + 4
    End Function
    Sub ComputeMaxMin()
        gMax = Math.Max(G(0), G(1))
        gMin = Math.Min(G(0), G(1))
        'for g'(x)=0 => a=0 or b=0, if a or b E [0,1] then test them
        a = CDbl(aValue.Text)
        b = CDbl(bValue.Text)
        If (0 <= a And a <= 1) Then
            gMax = Math.Max(G(a), gMax)
            gMin = Math.Min(G(a), gMin)
        End If
        If (0 <= b And b <= 1) Then
            gMax = Math.Max(G(b), gMax)
            gMin = Math.Min(G(b), gMin)
        End If
    End Sub

    Function f(ByVal x As Double) As Double
        Return (G(x) - gMin) / (gMax - gMin)
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
        a = CDbl(aValue.Text)
        b = CDbl(bValue.Text)
        Dim x0 As Double = CDbl(startField.Text)
        Dim iter As Integer = Val(iField.Text)

        'Origin and End of the Diagonal
        Dim pYellow As New Pen(Color.Yellow)
        g.DrawRectangle(pYellow, 0, 400, 2, 2)
        g.DrawRectangle(pYellow, 400, 0, 2, 2)

        'Plot Function f(x)
        Dim x As Double
        For x = 0 To 1 Step 0.0005
            g.DrawRectangle(pRed, XC(x), YC(f(x)), 1, 1)
        Next

    End Sub

    Sub DrawDiagonal()
        Dim g As Graphics
        Dim pBlack As New Pen(Color.Black)
        Dim sdRed As New SolidBrush(Color.Red)
        g = PictureBox1.CreateGraphics
        a = CDbl(aValue.Text)
        b = CDbl(bValue.Text)

        'Axis
        g.DrawLine(pBlack, 0, 0, 0, 400) 'y-axis
        g.DrawLine(pBlack, 0, 400, 400, 400) 'x-axis

        'Highlight the initial point
        Dim x0 As Double
        x0 = CDbl(startField.Text)
        'Keep x0 E [0,1]
        If (x0 < 0 Or x0 > 1) Then x0 = 0.1
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
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ButtonGraphs.Click
        PictureBox1.Refresh()
        ComputeMaxMin()
        DrawDiagonal()
        DrawFunctionGraph()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        For a = -5 To 5 Step 0.1
            For b = 5 To 5 Step 0.1

            Next
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonEnd.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonDynamics.Click
        Dim M As Integer
        M = CInt(iField.Text)
        ComputeSequence(M)
        Cobweb(M)
    End Sub


End Class
