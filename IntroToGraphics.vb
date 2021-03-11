Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim g As Graphics
        Dim sdBrush As New SolidBrush(Color.Red)
        Dim pBlue As New Pen(Color.Blue)
        Dim pRed As New Pen(Color.Red)

        g = PictureBox1.CreateGraphics

        g.DrawLine(pBlue, 23, 56, 200, 300)
        g.DrawRectangle(pRed, 23, 56, 20, 30)
        g.DrawEllipse(pRed, 150, 150, 250, 50)
        g.FillRectangle(sdBrush, 40, 60, 20, 30)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim g As Graphics
        Dim pRed As New Pen(Color.Red)
        Dim i, h, w, a, b As Integer
        h = PictureBox1.Height
        w = PictureBox1.Width
        g = PictureBox1.CreateGraphics
        For i = 0 To 100
            a = Int(w * Rnd())
            b = Int(h * Rnd())
            g.DrawRectangle(pRed, a, b, 1, 1)
        Next

    End Sub
End Class
