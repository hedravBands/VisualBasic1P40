Imports System.IO 'Reading from file
Imports System.Security.Cryptography 'Compute sha256
Imports System.Text 'Encoding and StringBuilder
Imports FireSharp.Config 'Set up Firebase permitions
Imports FireSharp.Interfaces 'Convertion from Data to Classes
Imports FireSharp.Response 'http response handling
Imports System.Numerics 'BigInteger

Public Class GenerateWalletForm
    'Firebase Database config
    Private fbasecon As New FirebaseConfig() With
    {
        .AuthSecret = "N7sl3vI8xCuX3Z79icqPx6Sw5tge00guSNLQULuu",
        .BasePath = "https://brockcoin-551d6-default-rtdb.firebaseio.com/"
    }
    Private client As IFirebaseClient

    '# largest prime less than 2^128 modulus
    'Dim bigPrime As BigInteger = 2 ^ 128 - 159
    Dim bigPrime As BigInteger = 2 ^ 256 - 2 ^ 32 - 2 ^ 9 - 2 ^ 8 - 2 ^ 7 - 2 ^ 6 - 2 ^ 4 - 1

    'Initil Point G in the Elipse
    'Dim Gx As BigInteger = BigInteger.Parse("212498110140165357794002386415204704134")
    'Dim Gy As BigInteger = BigInteger.Parse("289301238143048640206148905425988571395")
    Dim Gx As BigInteger = BigInteger.Parse("55066263022277343669578718895168534326250603453777594175500187360389116729240")
    Dim Gy As BigInteger = BigInteger.Parse("32670510020758816978083085130507043184471273380659243275938904335757337482424")
    Dim initialPoint As New List(Of BigInteger) From {gx, gy}

    Private Sub GenerateWalletForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Connect to Firebase on LoadForm
        Try
            client = New FireSharp.FirebaseClient(fbasecon)
        Catch
            MessageBox.Show("Firebase Error: Check Connection.")
        End Try
    End Sub
    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click_1(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub GenerateWalletBtn_Click(sender As Object, e As EventArgs) Handles GenerateWalletBtn.Click
        If handleTB.Text = "" Then
            MessageBox.Show("Please inform your BrockU email Address")
        Else
            'Get Mnemonic 12 words + brockU handle from email as an extra protection
            Dim RootKey As String

            'Compute RootKey with 256 bits = 64 Hex
            RootKey = ComputeRootKeyString(GenerateMnemonic + handleTB.Text)
            Console.WriteLine("RootKey: " + RootKey)

            'Left half to be Master Secret Key ("m") with 128 bits = 32 hex
            Dim MasterSK = RootKey.Substring(0, 32)
            Console.WriteLine("MasterSK: " + MasterSK)

            'Right half to be Master Chain Code ("c") with 128 bits = 32 hex
            Dim MasterChainCode = RootKey.Substring(32, 32)
            Console.WriteLine("MasterCC: " + MasterChainCode)

            'Master Public Key from Master Secret Key
            Dim MasterPK = ComputeMasterPK(MasterSK)
            Console.WriteLine("MasterPK: " + MasterPK)
            'compute MasterPK ("M") with digit validation = first digit
            'With 128 + 4 = 132 bits = 32 + 1 = 65 hex 
            'MasterSK.


        End If


    End Sub



    'This function generates a sequence of random 12 words from the 2048  options
    'Returns a String with 12 words separated by " " in between them
    Public ReadOnly Property GenerateMnemonic As String
        Get
            Dim PossibleWordList As String() = File.ReadAllLines("../../english.txt")
            Dim PickedList(11) As String
            Dim mnemonic As String = ""
            mnemonicTbox.Text = ""
            For i As Integer = 0 To 11
                PickedList(i) = PossibleWordList(Int(2047 * Rnd()))
                mnemonic += PickedList(i) + " "
            Next
            mnemonicTbox.Text = mnemonic
            Return mnemonic
        End Get
    End Property

    Public Function ComputeRootKeyString(ByVal mnemonicS) As String
        Dim mySHARoot As SHA256 = SHA256Managed.Create()
        Dim mnemonicB As Byte() = Encoding.UTF8.GetBytes(mnemonicS)
        Dim hash As Byte() = mySHARoot.ComputeHash(mnemonicB)
        Dim rootKey As New StringBuilder()
        For h As Integer = 0 To hash.Length - 1
            rootKey.Append(hash(h).ToString("X2")) '2 digits for each hexadecimal spot
        Next
        Return rootKey.ToString()
    End Function

    Public Function ComputeSHA256String(ByVal bytes) As String
        Dim mySHA256 As SHA256 = SHA256Managed.Create()
        Dim hash As Byte() = mySHA256.ComputeHash(bytes)
        Dim mySHA256String As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            mySHA256String.Append(hash(i).ToString("X2")) '2 digits for each hexadecimal spot
        Next

        Return mySHA256String.ToString().ToLower()
    End Function

    Private Function ComputeMasterPK(ByVal MasterSK As String) As String
        Dim actualPoint = initialPoint
        'MasterSK = "ef235aacf90d9f4aadd8c92e4b2562e1d9eb97f0df9ba3b508258739cb013db2"
        'MasterSK = "ef235aacf90d9f4aadd8c92e"
        'convert string to binary
        Dim binarySK As New StringBuilder
        For Each hex As Char In MasterSK
            binarySK.Append(Convert.ToString(Convert.ToInt32(hex, 16), 2).PadLeft(4, "0"c))
        Next
        Dim SK2 As Byte() = Encoding.UTF8.GetBytes(binarySK.ToString())

        For d As Integer = 0 To binarySK.Length - 1
            actualPoint = DoublePoint(actualPoint)
            If (d = 1) Then
                actualPoint = AddPoint(actualPoint, initialPoint)
            End If
        Next

        Return "0X" + actualPoint(0).ToString '+ "  " + actualPoint(1).ToString
    End Function


    Private Function ModularInverse(ByVal input As BigInteger) As BigInteger
        Dim p = bigPrime
        If input < 0 Then
            input = input Mod p
        End If
        Dim oldY As BigInteger = 0
        Dim newY As BigInteger = 1
        While (input > 1)
            newY = oldY - (p / input) * newY
            oldY = newY
            input = p Mod input
            p = input
        End While
        Return newY
    End Function

    Private Function DoublePoint(ByVal Point As List(Of BigInteger)) As List(Of BigInteger)
        'Add a point On the curve To itself.
        Dim thisSlope As BigInteger
        Dim newX, newY As BigInteger

        'slope = (3x^2 + a) / 2y  from implicit diff on Maple
        thisSlope = ((3 * Point(0) * Point(0)) * ModularInverse((2 * Point(1)))) Mod bigPrime

        'New x = slope^2 - 2x
        newX = (thisSlope * thisSlope - (2 * Point(0))) Mod bigPrime

        'New y = slope * (Old x - New x) * y
        newY = (thisSlope * (Point(0) - newX) - Point(1)) Mod bigPrime

        Dim doubledPoint As New List(Of BigInteger) From {newX, newY}
        Return doubledPoint

    End Function


    Private Function AddPoint(ByVal PointA As List(Of BigInteger), ByVal PointB As List(Of BigInteger)) As List(Of BigInteger)
        '# Double If both points are the same
        If (PointA(0) = PointB(0) And PointA(1) = PointB(1)) Then
            Return DoublePoint(PointA)
        End If

        'slope = (deltaY) / (deltaX)
        Dim slope = ((PointA(1) - PointB(1)) * ModularInverse(PointA(0) - PointB(0))) Mod bigPrime

        'x = slope^2 - x1 - x2
        Dim x = (slope * slope - PointA(0) - PointB(0)) Mod bigPrime

        'y = slope * (x1 - New x) - y1
        Dim y = ((slope * (PointA(0) - x)) - PointA(1)) Mod bigPrime

        Dim addedPoint As New List(Of BigInteger) From {x, y}
        Return addedPoint

    End Function



End Class
