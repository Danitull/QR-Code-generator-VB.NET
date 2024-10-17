Imports QRCoder
Imports System.Drawing
Imports System.IO
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Data As String = TextBox1.Text
        Label1.Font = New Font(Label1.Font.FontFamily, 14)
        Label1.TextAlign = HorizontalAlignment.Center
        If Data <> "" Then
            ' Creating the QR Code
            Dim qrGenerator As New QRCodeGenerator()
            Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(Data, QRCodeGenerator.ECCLevel.Q)
            Dim qrCode As New QRCode(qrCodeData)

            ' Creating the QR Code's Image
            Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20)

            ' Get the application's startup path
            Dim startupPath As String = Application.StartupPath

            ' Create the "QR Codes" folder 3 levels up from the startup path
            Dim qrCodePath As String = Directory.GetParent(Directory.GetParent(Directory.GetParent(startupPath).FullName).FullName).FullName & "\QR Codes\"
            If Not Directory.Exists(qrCodePath) Then
                Directory.CreateDirectory(qrCodePath)
            End If

            ' Initialize the file name and increment until it's unique
            Dim i As Integer = 1
            Dim qrCodeFile As String
            Do 
                qrCodeFile = String.Format("QRCode{0}", i)

                ' Check if a file exists that starts with the first 6 characters of qrCodeFile
                Dim fileExists As Boolean = Directory.GetFiles(qrCodePath).Any(Function(file) Path.GetFileName(file).StartsWith(qrCodeFile.Substring(0, 7)))

                ' If a file exists, increment i and continue the loop
                If fileExists Then
                    i += 1
                Else
                    ' If no file exists, exit the loop
                    Exit Do
                End If
            Loop

            ' Save the QR Code image in the QR Codes Directory
            qrCodeImage.Save(qrCodePath & (qrCodeFile & "_" & Data & ".png"), System.Drawing.Imaging.ImageFormat.Png)

            Label1.Text = "QR Code Generated"
            Label1.ForeColor = Color.Black
            TextBox1.Text = ""
        Else
            Label1.Text = "Insert a String in the Text Box if you want to generate the QR Code"
            Label1.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "QR Code Generator"
    End Sub
End Class
