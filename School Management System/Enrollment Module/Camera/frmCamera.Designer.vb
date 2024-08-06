<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCamera
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CameraFeed = New System.Windows.Forms.PictureBox()
        Me.btnStartCamera = New System.Windows.Forms.Button()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.systemSign = New System.Windows.Forms.Panel()
        Me.lblCameraSubject = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PicPreview = New System.Windows.Forms.PictureBox()
        Me.CroppedImg = New System.Windows.Forms.PictureBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.cbBox = New System.Windows.Forms.CheckBox()
        CType(Me.CameraFeed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.systemSign.SuspendLayout()
        CType(Me.PicPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CroppedImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CameraFeed
        '
        Me.CameraFeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CameraFeed.Location = New System.Drawing.Point(10, 57)
        Me.CameraFeed.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CameraFeed.Name = "CameraFeed"
        Me.CameraFeed.Size = New System.Drawing.Size(640, 480)
        Me.CameraFeed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.CameraFeed.TabIndex = 0
        Me.CameraFeed.TabStop = False
        '
        'btnStartCamera
        '
        Me.btnStartCamera.Location = New System.Drawing.Point(10, 555)
        Me.btnStartCamera.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnStartCamera.Name = "btnStartCamera"
        Me.btnStartCamera.Size = New System.Drawing.Size(182, 28)
        Me.btnStartCamera.TabIndex = 1
        Me.btnStartCamera.Text = "START CAMERA"
        Me.btnStartCamera.UseVisualStyleBackColor = True
        '
        'btnCapture
        '
        Me.btnCapture.Location = New System.Drawing.Point(198, 555)
        Me.btnCapture.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(180, 28)
        Me.btnCapture.TabIndex = 1
        Me.btnCapture.Text = "CAPTURE"
        Me.btnCapture.UseVisualStyleBackColor = True
        '
        'systemSign
        '
        Me.systemSign.BackColor = System.Drawing.Color.White
        Me.systemSign.Controls.Add(Me.lblCameraSubject)
        Me.systemSign.Controls.Add(Me.Label2)
        Me.systemSign.Controls.Add(Me.btnClose)
        Me.systemSign.Controls.Add(Me.Label1)
        Me.systemSign.Dock = System.Windows.Forms.DockStyle.Top
        Me.systemSign.Location = New System.Drawing.Point(0, 10)
        Me.systemSign.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.systemSign.Name = "systemSign"
        Me.systemSign.Padding = New System.Windows.Forms.Padding(6)
        Me.systemSign.Size = New System.Drawing.Size(994, 39)
        Me.systemSign.TabIndex = 23
        '
        'lblCameraSubject
        '
        Me.lblCameraSubject.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblCameraSubject.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCameraSubject.ForeColor = System.Drawing.Color.Black
        Me.lblCameraSubject.Location = New System.Drawing.Point(287, 6)
        Me.lblCameraSubject.Name = "lblCameraSubject"
        Me.lblCameraSubject.Size = New System.Drawing.Size(437, 27)
        Me.lblCameraSubject.TabIndex = 5
        Me.lblCameraSubject.Text = "-"
        Me.lblCameraSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(118, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(169, 27)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Capturing photo for"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(968, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(20, 27)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "✕"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "CAMERA   |"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(6)
        Me.Panel1.Size = New System.Drawing.Size(994, 10)
        Me.Panel1.TabIndex = 22
        '
        'PicPreview
        '
        Me.PicPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicPreview.Location = New System.Drawing.Point(10, 57)
        Me.PicPreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PicPreview.Name = "PicPreview"
        Me.PicPreview.Size = New System.Drawing.Size(640, 480)
        Me.PicPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicPreview.TabIndex = 24
        Me.PicPreview.TabStop = False
        '
        'CroppedImg
        '
        Me.CroppedImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CroppedImg.Location = New System.Drawing.Point(664, 57)
        Me.CroppedImg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CroppedImg.Name = "CroppedImg"
        Me.CroppedImg.Size = New System.Drawing.Size(320, 291)
        Me.CroppedImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.CroppedImg.TabIndex = 0
        Me.CroppedImg.TabStop = False
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(802, 366)
        Me.btnCopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(180, 28)
        Me.btnCopy.TabIndex = 1
        Me.btnCopy.Text = "COPY"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnUpload
        '
        Me.btnUpload.Location = New System.Drawing.Point(470, 555)
        Me.btnUpload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(180, 28)
        Me.btnUpload.TabIndex = 1
        Me.btnUpload.Text = "BROWSE"
        Me.btnUpload.UseVisualStyleBackColor = True
        '
        'cbBox
        '
        Me.cbBox.AutoSize = True
        Me.cbBox.ForeColor = System.Drawing.Color.Black
        Me.cbBox.Location = New System.Drawing.Point(664, 371)
        Me.cbBox.Name = "cbBox"
        Me.cbBox.Size = New System.Drawing.Size(109, 20)
        Me.cbBox.TabIndex = 264
        Me.cbBox.Text = "Fixed Cropping"
        Me.cbBox.UseVisualStyleBackColor = True
        '
        'frmCamera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(994, 602)
        Me.ControlBox = False
        Me.Controls.Add(Me.cbBox)
        Me.Controls.Add(Me.systemSign)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.btnUpload)
        Me.Controls.Add(Me.btnCapture)
        Me.Controls.Add(Me.btnStartCamera)
        Me.Controls.Add(Me.CroppedImg)
        Me.Controls.Add(Me.CameraFeed)
        Me.Controls.Add(Me.PicPreview)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmCamera"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.CameraFeed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.systemSign.ResumeLayout(False)
        CType(Me.PicPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CroppedImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CameraFeed As PictureBox
    Friend WithEvents btnStartCamera As Button
    Friend WithEvents btnCapture As Button
    Friend WithEvents systemSign As Panel
    Friend WithEvents btnClose As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PicPreview As PictureBox
    Friend WithEvents CroppedImg As PictureBox
    Friend WithEvents lblCameraSubject As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnCopy As Button
    Friend WithEvents btnUpload As Button
    Friend WithEvents cbBox As CheckBox
End Class
