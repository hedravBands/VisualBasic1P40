Public Class Form1

    'Brock University
    'Faculty of Mathematics & Science
    'Mathematics & Statistics Department

    'Course Title: Math Integrated With Computers & Application I
    'Course Code 1P40
    'Assignment 02

    'Instructor: Jesse Larone
    'Lab Instructors: Jesse Larone And Neil Marshall

    'Student: Heduin R. B. de Morais
    'Brock_ID: 6967483
    'Campus_ID: hr19ut
    'Lab_ID: 02 
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SecretBtn.Click
        Dim Msg, Style, Title
        Msg = "No Key?! No Problemo! Leave >> d <<  blank..."
        Style = vbYes
        Title = "CryptoCracker says:"
        MsgBox(Msg, Style, Title)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles DecodeBtn.Click
        Dim rd, n, d, nn As Long
        n = CLng(decoden.Text)


        nn = CLng(decodem.Text)

        'Secret Feature here
        If decoded.Text = "" Then
            d = CrackD(n)
            decoded.Text = d.ToString
        Else
            d = CLng(decoded.Text)
        End If

        rd = ComputeCrypto(nn, d, n)
        decodemLabel.Text = rd.ToString
    End Sub
    Public Function CrackD(n As Long) As Long
        Dim p, q, d, c As Long
        Dim possiblePrimes

        possiblePrimes = BuildPrimeStack(2, Math.Ceiling(Math.Sqrt(n)))

        Do
            p = possiblePrimes.pop()
        Loop While (n Mod p <> 0) 'Or (p = 2)

        'p is cracked, q is easy
        q = n / p


        d = 1
        c = CLng(encodee.Text)
        While (c * d) Mod LCM((p - 1), (q - 1)) <> 1
            d += 1
        End While

        Return d


    End Function



    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub EncodeBtn_Click(sender As Object, e As EventArgs) Handles EncodeBtn.Click
        Dim re, n, c, m As Long
        n = CLng(encoden.Text)
        c = CLng(encodee.Text)
        m = CLng(encodem.Text)
        re = ComputeCrypto(m, c, n)
        encodemLabel.Text = re.ToString
        decodem.Text = re.ToString
        decoden.Text = encoden.Text
    End Sub

    Public Function ComputeCrypto(ByVal base As Long, ByVal exponent As Long, ByVal modulus As Long) As Long
        Dim i As Integer
        Dim output As Long
        output = base
        For i = 2 To exponent
            output = output * base Mod modulus
        Next
        Return output
    End Function

    Private Sub GenBtn_Click(sender As Object, e As EventArgs) Handles GenBtn.Click
        Dim n, c, d, p1, p2, phiOfn
        Dim myPrimeList
        'Clean empty fields
        encodem.Text = ""
        decodem.Text = ""

        myPrimeList = BuildPrimeStack(100, 999).ToArray
        'Let's pick 2 random prime numbers p and q by randomizing the array's index
        p1 = 2
        p2 = 2
        While p1 = p2
            p1 = myPrimeList(Int((myPrimeList.Length) * Rnd()))
            p2 = myPrimeList(Int((myPrimeList.Length) * Rnd()))
        End While

        'Define n as p1*p2 and assign boxes and set labels for primes
        n = p1 * p2
        encoden.Text = n.ToString
        decoden.Text = n.ToString
        PrimeOneLabel.Text = p1.ToString
        PrimeTwoLabel.Text = p2.ToString

        'Calculate phiOfn as (p1 - 1) times (p2 - 1), each factor is (p^1 - p^0)
        phiOfn = (p1 - 1) * (p2 - 1)

        'Calculate c, part of the public key (n, c)
        c = 2
        While (GCD(c, phiOfn) <> 1) And (c < phiOfn)  'And to avoid infinite loop
            c += 1
        End While
        encodee.Text = c.ToString


        'Calculate d, part of the private key(n, d)
        d = 1
        While (c * d) Mod phiOfn <> 1 'LCM((p1 - 1), (p2 - 1)) <> 1 
            d += 1
        End While
        decoded.Text = d.ToString


    End Sub

    'This functions returns a stack with prime numbers from minNumber to maxNumber
    Public Function BuildPrimeStack(minNumber As Long, maxNumber As Long) As Stack
        Dim primeList(maxNumber + 1) As Boolean '+1 to avoid outofbounds pointer
        Dim p, i As Long

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

        'Create a Stack of prime numbers with 3 digits then convert to Array
        Dim primeStack As Stack = New Stack()
        For p = minNumber To maxNumber
            If primeList(p) = True Then
                primeStack.Push(p)
            End If
        Next
        Return primeStack
    End Function


    'This function returns the gcd(a,b) using the recursive Euclidean Algorithm

    Public Function GCD(numberA As Integer, numberB As Integer) As Integer
        If numberA = 0 Then Return numberB
        Return GCD(numberB Mod numberA, numberA)
    End Function

    'This function returns the lcm(a,b) using lcm(a,b)*gcd(a,b) = abs(a*b)
    Public Function LCM(numberA As Integer, numberB As Integer) As Integer
        Return (numberA * numberB) / GCD(numberA, numberB)
    End Function

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Operation_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
    End Sub

    Private Sub TextBox3_TextChanged_1(sender As Object, e As EventArgs) Handles decoden.TextChanged

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class
