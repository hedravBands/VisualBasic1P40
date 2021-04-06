Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports Newtonsoft.Json
Imports System.IO
Imports System.Text
Imports System.Security.Policy
Imports System.Security.Cryptography

Public Class Form1
    'Firebase Database config
    Private fbasecon As New FirebaseConfig() With
    {
        .AuthSecret = "N7sl3vI8xCuX3Z79icqPx6Sw5tge00guSNLQULuu",
        .BasePath = "https://brockcoin-551d6-default-rtdb.firebaseio.com/"
    }
    Private client As IFirebaseClient


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Connect to Firebase on LoadForm
        Try
            client = New FireSharp.FirebaseClient(fbasecon)
        Catch
            MessageBox.Show("Firebase Error: Check Connection.")

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles GenerateWalletBtn.Click
        'Get Mnemonic 24 words
        Dim RootKey As String

        'Compute RootKey with 512 bits = 128 Hex
        RootKey = ComputeRootKeyString(GenerateMnemonic())
        Console.WriteLine(RootKey)

        'Left half to be Master Private Key ("m") with 256 bits = 64 hex
        Dim MasterSK = RootKey.Substring(0, 64)
        Console.WriteLine(MasterSK)

        'compute MasterPK ("M") with digit validation 
        'With 256 + 8 = 564 bits = 64 + 2 = 66 hex 


        'Righ half to be Master Chain Code ("c") with 256 bits = 64 hex
        Dim MasterChainCode = RootKey.Substring(64, 64)
        Console.WriteLine(MasterChainCode)

        Dim wallet As New Wallet() With
        {
            .Nickname = "nickname",
            .PK = "123123123",
            .SK = "321321321"
        }
        client.Set("Wallets/" + RootKey + "/", wallet)
        'MessageBox.Show("Wallet Generated!")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ListWallet.Click
        Dim res = client.Get("Wallets/" + mnemonicTbox.Text)
        Dim wallet As New Wallet()
        Try
            wallet = res.ResultAs(Of Wallet)
        Catch ex As Exception
            MessageBox.Show("List Wallet Failed.")
        End Try
        If wallet IsNot Nothing Then
            Nickname.Text = wallet.Nickname
            PKLabel.Text = wallet.PK
            SKLabel.Text = wallet.SK
        Else
            MessageBox.Show("No Wallet found.")
        End If

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim wallet As New Wallet() With
        {
            .Nickname = mnemonicTbox.Text,
            .PK = "123123123",
            .SK = "321321321"
        }
        Dim setter = client.Update("Wallets/" + mnemonicTbox.Text, wallet)
        MessageBox.Show("Wallet Updated!")
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Try
            Dim res = client.Delete("Wallets/" + mnemonicTbox.Text)
            MessageBox.Show("Wallet Deleted!")
        Catch
            MessageBox.Show("Delete Wallet not possible.")
        End Try

    End Sub

    Private Sub WalletGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles WalletGridView.CellContentClick

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles RTBBtn.Click
        Dim res As FirebaseResponse = client.Get("Wallets")
        Dim data As Dictionary(Of String, Wallet) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Wallet))(res.Body.ToString())
        UpdateRTB(data)
    End Sub



    Private Sub GridBtn_Click(sender As Object, e As EventArgs) Handles GridBtn.Click
        Dim res As FirebaseResponse = client.Get("Wallets")
        Dim data As Dictionary(Of String, Wallet) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Wallet))(res.Body.ToString())
        UpdateDataGrid(data)
    End Sub



    Sub UpdateRTB(ByVal records As Dictionary(Of String, Wallet))
        WalletRTB.Clear()
        For Each item In records
            WalletRTB.Text += item.Key + vbLf
            WalletRTB.Text += "Nickname" + item.Value.Nickname + vbLf
            WalletRTB.Text += "PK" + item.Value.PK + vbLf
            WalletRTB.Text += "SK" + item.Value.SK + vbLf + vbLf
        Next

    End Sub
    Private Sub UpdateDataGrid(ByVal records As Dictionary(Of String, Wallet))
        WalletGridView.Rows.Clear()
        WalletGridView.Rows.Clear()


        WalletGridView.Columns.Add("nickname", "Nickname")
        WalletGridView.Columns.Add("pk", "PK")
        WalletGridView.Columns.Add("sk", "SK")


        For Each item In records
            WalletGridView.Rows.Add(item.Key, item.Value.PK, item.Value.SK)
        Next

    End Sub

    Public Function GenerateMnemonic() As String
        Dim PossibleWordList As String() = File.ReadAllLines("../../english.txt")
        Dim PickedList(23) As String
        Dim mnemonic As String = ""
        mnemonicTbox.Text = ""
        For i As Integer = 0 To 23
            PickedList(i) = PossibleWordList(Int(2047 * Rnd()))
            mnemonic += PickedList(i)
            If i <> 23 Then
                mnemonic += " "
            End If
        Next
        mnemonicTbox.Text = mnemonic
        Return mnemonic
    End Function


    Public Function ComputeRootKeyString(ByVal mnemonicS) As String
        Dim mySHARoot As SHA512 = SHA512Managed.Create()
        Dim mnemonicB As Byte() = Encoding.UTF8.GetBytes(mnemonicS)
        Dim hash As Byte() = mySHARoot.ComputeHash(mnemonicB)
        Dim rootKey As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            rootKey.Append(hash(i).ToString("X2")) '2 digits for each hexadecimal spot
        Next

        Return rootKey.ToString().ToLower()
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


End Class
