Public Class Form1
    '   Brock University
    'Faculty of Mathematics & Science
    'Mathematics & Statistics Department

    'Course Title: Math Integrated With Computers & Application I
    'Course Code 1P40 

    'Instructor: Jesse Larone
    'Lab Instructors: Jesse Larone And Neil Marshall

    'Student: Heduin R. B. de Morais
    'Brock_ID: 6967483
    'Campus_ID: hr19ut
    'Lab_ID: 02 

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles LabelTotalPrimesTitle.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        LabelStatus.Text = "Generating Primes"
        Dim maxNumber As Long = Val(TextBox1.Text)
        Dim maxPrime As Long = 2
        Dim primeList(maxNumber + 1) As Boolean '+1 to avoid outofbounds pointer
        Dim remainderList(10) As Integer  '0 to 9 is the last unit algarism
        Dim percentualList(10) As Double  '0 to 9 is the last unit algarism
        Dim p, i, primeCounter As Integer

        'primes start at 2 
        primeList(0) = False
        primeList(1) = False
        For p = 2 To maxNumber
            primeList(p) = True
        Next

        'Sieve Algorithm
        'If list.item is true then it is a prime
        p = 2
        While (p * p <= maxNumber)
            If (primeList(p) = True) Then
                i = p * p
                While (i <= maxNumber)
                    primeList(i) = False
                    i += p
                End While
            End If
            p += 1
        End While



        'add to the list
        LabelStatus.Text = "Generating Chart"
        primeCounter = 0
        ListBox1.Items.Clear()
        For p = 2 To maxNumber
            If primeList(p) = True Then
                maxPrime = p
                primeCounter += 1
                ListBox1.Items.Add(p)
                'compute distribution of this prime
                'get the last digit
                Dim pString = p.ToString()
                Dim remainder As Integer = Val(pString.Chars(Len(pString) - 1))
                remainderList(remainder) += 1
            End If
        Next


        LabelStatus.Text = "Generating Report"

        'build the histogram chart
        For p = 1 To 9
            Chart1.Series("Residual").Points.AddXY(p, remainderList(p))
            percentualList(p) = Math.Round(remainderList(p) / primeCounter, 5, MidpointRounding.AwayFromZero)
        Next


        'generating report
        LabelTotalPrimes.Text = primeCounter
        LabelMaxPrime.Text = maxPrime
        End1.Text = percentualList(1) & "   Error: " & Math.Round((percentualList(1) - 0.25) / 0.25, 5, MidpointRounding.AwayFromZero) & " %"
        End3.Text = percentualList(3) & "   Error: " & Math.Round((percentualList(3) - 0.25) / 0.25, 5, MidpointRounding.AwayFromZero) & " %"
        End7.Text = percentualList(7) & "   Error: " & Math.Round((percentualList(7) - 0.25) / 0.25, 5, MidpointRounding.AwayFromZero) & " %"
        End9.Text = percentualList(9) & "   Error: " & Math.Round((percentualList(9) - 0.25) / 0.25, 5, MidpointRounding.AwayFromZero) & " %"
        Expected.Text = "0.25000"


        'final status
        LabelStatus.Text = "All Done!"
        Beep()

    End Sub


    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles LabelStatusTitle.Click

    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub LabelMaxPrimeTitle_Click(sender As Object, e As EventArgs) Handles LabelMaxPrimeTitle.Click

    End Sub

    Private Sub Label9_Click_1(sender As Object, e As EventArgs) Handles End1Title.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles End1.Click

    End Sub

    Private Sub Label9_Click_2(sender As Object, e As EventArgs) Handles ExpectedPercentualTitle.Click

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs) Handles Expected.Click

    End Sub

    Private Sub End3_Click(sender As Object, e As EventArgs) Handles End3.Click

    End Sub
End Class
