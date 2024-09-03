Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging
Public Class frmCamera



    Private cropStart As Point ' Starting point of the cropping rectangle
    Private cropRect As Rectangle ' Cropping rectangle
    Private cropPen As Pen = New Pen(Color.Red, 2) ' Pen for drawing the cropping rectangle
    Private cropRatio As Double = 1.0 ' Aspect ratio (width/height) for the fixed cropping box

    Private cropRectangle As Rectangle
    Private cropping As Boolean = False

    Private Const WM_CAP_START As Integer = &H400S
    Private Const WM_CAP_DRIVER_CONNECT As Integer = WM_CAP_START + 10
    Private Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_CAP_START + 11
    Private Const WM_CAP_EDIT_COPY As Integer = WM_CAP_START + 30
    Private Const WM_CAP_SET_PREVIEW As Integer = WM_CAP_START + 50
    Private Const WM_CAP_SET_PREVIEWRATE As Integer = WM_CAP_START + 52
    Private Const WM_CAP_SET_SCALE As Integer = WM_CAP_START + 53
    Private Const WM_CAP_SET_VIDEOFORMAT As Integer = WM_CAP_START + 45
    Private Const WS_CHILD As Integer = &H40000000
    Private Const WS_VISIBLE As Integer = &H10000000
    Private Const HWND_BOTTOM As Integer = 1

    <StructLayout(LayoutKind.Sequential)>
    Private Structure BITMAPINFOHEADER
        Public biSize As Integer
        Public biWidth As Integer
        Public biHeight As Integer
        Public biPlanes As Short
        Public biBitCount As Short
        Public biCompression As Integer
        Public biSizeImage As Integer
        Public biXPelsPerMeter As Integer
        Public biYPelsPerMeter As Integer
        Public biClrUsed As Integer
        Public biClrImportant As Integer
    End Structure

    <DllImport("user32", EntryPoint:="SendMessageA")>
    Private Shared Function SendMessage(ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As BITMAPINFOHEADER) As Integer
    End Function

    <DllImport("user32", EntryPoint:="SendMessageA")>
    Private Shared Function SendMessage(ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    <DllImport("user32", EntryPoint:="SetWindowPos")>
    Private Shared Function SetWindowPos(ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Boolean
    End Function

    <DllImport("user32", SetLastError:=True)>
    Private Shared Function DestroyWindow(ByVal hWnd As IntPtr) As Boolean
    End Function

    <DllImport("avicap32.dll", EntryPoint:="capCreateCaptureWindowA")>
    Private Shared Function capCreateCaptureWindow(ByVal lpszWindowName As String, ByVal dwStyle As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hWndParent As Integer, ByVal nID As Integer) As Integer
    End Function

    Private iDevice As Integer = 0
    Private hHwnd As Integer

#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles systemSign.MouseDown, Panel1.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove, Panel1.MouseMove   ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnStartCamera.Click
        CameraFeed.BringToFront()
        CroppedImg.Image = Nothing
        hHwnd = capCreateCaptureWindow(iDevice.ToString, WS_VISIBLE Or WS_CHILD, 0, 0, CameraFeed.Width, CameraFeed.Height, CameraFeed.Handle.ToInt32, 0)

        If SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) Then
            Dim bmpInfoHeader As New BITMAPINFOHEADER
            bmpInfoHeader.biSize = Marshal.SizeOf(bmpInfoHeader)
            bmpInfoHeader.biWidth = 640 ' Set width to a reasonable resolution
            bmpInfoHeader.biHeight = 480 ' Set height to a reasonable resolution
            bmpInfoHeader.biPlanes = 1
            bmpInfoHeader.biBitCount = 24
            bmpInfoHeader.biCompression = 0 ' BI_RGB
            bmpInfoHeader.biSizeImage = 0
            bmpInfoHeader.biXPelsPerMeter = 0
            bmpInfoHeader.biYPelsPerMeter = 0
            bmpInfoHeader.biClrUsed = 0
            bmpInfoHeader.biClrImportant = 0

            SendMessage(hHwnd, WM_CAP_SET_VIDEOFORMAT, Marshal.SizeOf(bmpInfoHeader), bmpInfoHeader)
            SendMessage(hHwnd, WM_CAP_SET_SCALE, True, 0)
            SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0)
            SendMessage(hHwnd, WM_CAP_SET_PREVIEW, True, 0)
            SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, CameraFeed.Width, CameraFeed.Height, &H1 Or &H4 Or &H20)
        Else
            DestroyWindow(hHwnd)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Dim Data As IDataObject
        Dim Bmap As Image
        SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)
        Data = Clipboard.GetDataObject()
        If Data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            Bmap = CType(Data.GetData(GetType(System.Drawing.Bitmap)), Image)
            CameraFeed.Image = Bmap
            CroppedImg.Image = CameraFeed.Image
            PicPreview.Image = CameraFeed.Image
            MsgBox("Captured!", MsgBoxStyle.Information)
            PicPreview.BringToFront()
        Else
            MsgBox("Capturing Image failed!", MsgBoxStyle.Critical)
        End If
        SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0)
        DestroyWindow(hHwnd)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CameraFeed.BringToFront()
        PicPreview.Image = Nothing
        CroppedImg.Image = Nothing
        Me.Close()
    End Sub





    Private Sub PicPreview_MouseDown(sender As Object, e As MouseEventArgs) Handles PicPreview.MouseDown
        If cbBox.Checked = True Then
            If e.Button = MouseButtons.Left Then
                cropStart = e.Location
                cropRect = New Rectangle(cropStart, New Size(0, 0))
            End If

        Else
            If PicPreview.Image IsNot Nothing AndAlso e.Button = MouseButtons.Left Then
                ' Start selecting crop area
                cropRectangle = New Rectangle(e.X, e.Y, 0, 0)
                cropping = True
            End If
        End If
    End Sub

    Private Sub PicPreview_MouseMove(sender As Object, e As MouseEventArgs) Handles PicPreview.MouseMove
        If cbBox.Checked = True Then
            If e.Button = MouseButtons.Left Then
                Dim deltaX As Integer = e.X - cropStart.X
                Dim deltaY As Integer = e.Y - cropStart.Y

                ' Maintain fixed aspect ratio
                If Math.Abs(deltaX) > Math.Abs(deltaY) Then
                    deltaY = CInt(deltaX / cropRatio)
                Else
                    deltaX = CInt(deltaY * cropRatio)
                End If

                cropRect = New Rectangle(cropStart.X, cropStart.Y, deltaX, deltaY)
                PicPreview.Invalidate()
            End If
        Else
            If cropping AndAlso PicPreview.Image IsNot Nothing Then
                ' Update crop area size
                cropRectangle.Width = e.X - cropRectangle.X
                cropRectangle.Height = e.Y - cropRectangle.Y

                ' Redraw PictureBox to show the selection
                PicPreview.Refresh()
            End If
        End If
    End Sub

    Private Sub PicPreview_MouseUp(sender As Object, e As MouseEventArgs) Handles PicPreview.MouseUp
        If cbBox.Checked = True Then
            If e.Button = MouseButtons.Left AndAlso cropRect.Width > 0 AndAlso cropRect.Height > 0 Then
                ' Perform cropping operation
                CropImage()
            End If
        Else

            If cropping Then
                ' Finish selecting crop area
                cropping = False

                ' Ensure width and height are positive
                If cropRectangle.Width < 0 Then
                    cropRectangle.X += cropRectangle.Width
                    cropRectangle.Width = Math.Abs(cropRectangle.Width)
                End If
                If cropRectangle.Height < 0 Then
                    cropRectangle.Y += cropRectangle.Height
                    cropRectangle.Height = Math.Abs(cropRectangle.Height)
                End If

                ' Draw the final selection
                PicPreview.Refresh()
                CropImage()
            End If
        End If

    End Sub

    Private Sub PicPreview_Paint(sender As Object, e As PaintEventArgs) Handles PicPreview.Paint
        If cbBox.Checked = True Then
            If PicPreview.Image IsNot Nothing Then
                e.Graphics.DrawImage(PicPreview.Image, 0, 0, PicPreview.Width, PicPreview.Height)

                ' Draw the cropping rectangle
                e.Graphics.DrawRectangle(cropPen, cropRect)
            End If
        Else
            ' Draw the selection rectangle
            Using pen As New Pen(Color.Red, 2)
                e.Graphics.DrawRectangle(pen, cropRectangle)
            End Using
        End If
    End Sub

    Private Sub CropImage()
        If cbBox.Checked = True Then
            If PicPreview.Image IsNot Nothing Then
                Dim originalImage As Bitmap = CType(PicPreview.Image, Bitmap)

                ' Calculate scaling factors
                Dim scaleX As Double = originalImage.Width / PicPreview.ClientSize.Width
                Dim scaleY As Double = originalImage.Height / PicPreview.ClientSize.Height

                ' Scale crop rectangle to original image's dimensions
                Dim cropX As Integer = CInt(cropRect.X * scaleX)
                Dim cropY As Integer = CInt(cropRect.Y * scaleY)
                Dim cropWidth As Integer = CInt(cropRect.Width * scaleX)
                Dim cropHeight As Integer = CInt(cropRect.Height * scaleY)

                ' Ensure crop rectangle is within bounds of original image
                cropX = Math.Max(0, Math.Min(cropX, originalImage.Width))
                cropY = Math.Max(0, Math.Min(cropY, originalImage.Height))
                cropWidth = Math.Max(1, Math.Min(cropWidth, originalImage.Width - cropX))
                cropHeight = Math.Max(1, Math.Min(cropHeight, originalImage.Height - cropY))

                ' Create cropped image
                Dim croppedImage As New Bitmap(cropWidth, cropHeight)
                Using g As Graphics = Graphics.FromImage(croppedImage)
                    g.DrawImage(originalImage, New Rectangle(0, 0, cropWidth, cropHeight), New Rectangle(cropX, cropY, cropWidth, cropHeight), GraphicsUnit.Pixel)
                End Using

                ' Display cropped image in pictureBox2
                CroppedImg.Image = croppedImage
            End If
        Else
            If PicPreview.Image IsNot Nothing AndAlso Not cropRectangle.IsEmpty Then
                ' Create a new bitmap with the crop size
                Dim originalImage As Bitmap = CType(PicPreview.Image, Bitmap)

                ' Scale cropRectangle to the original image's dimensions
                Dim scaleX As Double = originalImage.Width / PicPreview.ClientSize.Width
                Dim scaleY As Double = originalImage.Height / PicPreview.ClientSize.Height

                Dim cropX As Integer = CInt(cropRectangle.X * scaleX)
                Dim cropY As Integer = CInt(cropRectangle.Y * scaleY)
                Dim cropWidth As Integer = CInt(cropRectangle.Width * scaleX)
                Dim cropHeight As Integer = CInt(cropRectangle.Height * scaleY)

                ' Ensure the crop rectangle is within the bounds of the original image
                cropX = Math.Max(0, Math.Min(cropX, originalImage.Width))
                cropY = Math.Max(0, Math.Min(cropY, originalImage.Height))
                cropWidth = Math.Max(1, Math.Min(cropWidth, originalImage.Width - cropX))
                cropHeight = Math.Max(1, Math.Min(cropHeight, originalImage.Height - cropY))

                Dim croppedImage As New Bitmap(cropWidth, cropHeight)

                ' Crop the selected region
                Using g As Graphics = Graphics.FromImage(croppedImage)
                    g.DrawImage(originalImage, New Rectangle(0, 0, cropWidth, cropHeight), New Rectangle(cropX, cropY, cropWidth, cropHeight), GraphicsUnit.Pixel)
                End Using

                ' Display the cropped image in the new PictureBox
                CroppedImg.Image = croppedImage
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        If lblCameraSubject.Text = "Employee" Then
            frmEmployee.empPhoto.Image = CroppedImg.Image
            MsgBox("Cropped Image copied!", MsgBoxStyle.Information)
        ElseIf lblCameraSubject.Text = "User" Then
            frmUser.userPhoto.Image = CroppedImg.Image
            MsgBox("Cropped Image copied!", MsgBoxStyle.Information)
        ElseIf lblCameraSubject.Text = "Student" Then
            frmStudentInfo.studentPhoto.Image = CroppedImg.Image
            MsgBox("Cropped Image copied!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        DestroyWindow(hHwnd)
        PicPreview.BringToFront()

        Dim openDlg As New System.Windows.Forms.OpenFileDialog
        openDlg.Filter = "JPEG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|Bitmap Files (*.bmp)|*.bmp"
        If openDlg.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        If Not openDlg.FileName Is Nothing Then
            CroppedImg.Image = System.Drawing.Bitmap.FromFile(openDlg.FileName)
            PicPreview.Image = System.Drawing.Bitmap.FromFile(openDlg.FileName)
        End If
    End Sub

    Private Sub frmCamera_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
    End Sub
End Class