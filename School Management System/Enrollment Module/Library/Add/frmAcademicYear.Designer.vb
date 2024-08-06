<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAcademicYear
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
        Me.btnClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.systemSign = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.settingsPanel = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbBalance = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbAD = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbEnroll = New System.Windows.Forms.ComboBox()
        Me.dtEnd = New System.Windows.Forms.DateTimePicker()
        Me.dtStart = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.cbSemester = New System.Windows.Forms.ComboBox()
        Me.dtGrad = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblGrad = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEnd = New System.Windows.Forms.TextBox()
        Me.txtStart = New System.Windows.Forms.TextBox()
        Me.systemSign.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.settingsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(802, 6)
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
        Me.Label1.Size = New System.Drawing.Size(266, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Academic Year Entry"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'systemSign
        '
        Me.systemSign.BackColor = System.Drawing.Color.White
        Me.systemSign.Controls.Add(Me.btnClose)
        Me.systemSign.Controls.Add(Me.Label1)
        Me.systemSign.Dock = System.Windows.Forms.DockStyle.Top
        Me.systemSign.Location = New System.Drawing.Point(0, 10)
        Me.systemSign.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.systemSign.Name = "systemSign"
        Me.systemSign.Padding = New System.Windows.Forms.Padding(6)
        Me.systemSign.Size = New System.Drawing.Size(828, 39)
        Me.systemSign.TabIndex = 12
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(6)
        Me.Panel1.Size = New System.Drawing.Size(828, 10)
        Me.Panel1.TabIndex = 11
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.settingsPanel)
        Me.Panel2.Controls.Add(Me.cbStatus)
        Me.Panel2.Controls.Add(Me.cbSemester)
        Me.Panel2.Controls.Add(Me.dtGrad)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.lblGrad)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtEnd)
        Me.Panel2.Controls.Add(Me.txtStart)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 49)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(828, 430)
        Me.Panel2.TabIndex = 14
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.FlowLayoutPanel2)
        Me.Panel3.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 391)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(828, 39)
        Me.Panel3.TabIndex = 93
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(380, 39)
        Me.FlowLayoutPanel2.TabIndex = 2
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCancel)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnSave)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnUpdate)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(413, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(415, 39)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.Location = New System.Drawing.Point(316, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 34)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Location = New System.Drawing.Point(214, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 34)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "SAVE"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.Location = New System.Drawing.Point(112, 3)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(96, 34)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "UPDATE"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'settingsPanel
        '
        Me.settingsPanel.Controls.Add(Me.Label5)
        Me.settingsPanel.Controls.Add(Me.cbBalance)
        Me.settingsPanel.Controls.Add(Me.Label10)
        Me.settingsPanel.Controls.Add(Me.cbAD)
        Me.settingsPanel.Controls.Add(Me.Label9)
        Me.settingsPanel.Controls.Add(Me.cbEnroll)
        Me.settingsPanel.Controls.Add(Me.dtEnd)
        Me.settingsPanel.Controls.Add(Me.dtStart)
        Me.settingsPanel.Controls.Add(Me.Label11)
        Me.settingsPanel.Controls.Add(Me.Label8)
        Me.settingsPanel.Controls.Add(Me.Label6)
        Me.settingsPanel.Location = New System.Drawing.Point(71, 221)
        Me.settingsPanel.Name = "settingsPanel"
        Me.settingsPanel.Size = New System.Drawing.Size(677, 130)
        Me.settingsPanel.TabIndex = 90
        Me.settingsPanel.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 16)
        Me.Label5.TabIndex = 83
        Me.Label5.Text = "Enrollment Settings"
        '
        'cbBalance
        '
        Me.cbBalance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBalance.FormattingEnabled = True
        Me.cbBalance.Items.AddRange(New Object() {"ON", "OFF"})
        Me.cbBalance.Location = New System.Drawing.Point(160, 88)
        Me.cbBalance.Name = "cbBalance"
        Me.cbBalance.Size = New System.Drawing.Size(96, 24)
        Me.cbBalance.TabIndex = 88
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 91)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 16)
        Me.Label10.TabIndex = 84
        Me.Label10.Text = "Balance Checking"
        '
        'cbAD
        '
        Me.cbAD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAD.FormattingEnabled = True
        Me.cbAD.Items.AddRange(New Object() {"CLOSE", "OPEN"})
        Me.cbAD.Location = New System.Drawing.Point(160, 58)
        Me.cbAD.Name = "cbAD"
        Me.cbAD.Size = New System.Drawing.Size(96, 24)
        Me.cbAD.TabIndex = 88
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 61)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(144, 16)
        Me.Label9.TabIndex = 84
        Me.Label9.Text = "Updating Class Schedule"
        '
        'cbEnroll
        '
        Me.cbEnroll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEnroll.FormattingEnabled = True
        Me.cbEnroll.Items.AddRange(New Object() {"CLOSE", "OPEN"})
        Me.cbEnroll.Location = New System.Drawing.Point(160, 28)
        Me.cbEnroll.Name = "cbEnroll"
        Me.cbEnroll.Size = New System.Drawing.Size(96, 24)
        Me.cbEnroll.TabIndex = 88
        '
        'dtEnd
        '
        Me.dtEnd.CustomFormat = "yyyy/MM/dd"
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEnd.Location = New System.Drawing.Point(428, 56)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.Size = New System.Drawing.Size(238, 21)
        Me.dtEnd.TabIndex = 87
        '
        'dtStart
        '
        Me.dtStart.CustomFormat = "yyyy/MM/dd"
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStart.Location = New System.Drawing.Point(428, 28)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.Size = New System.Drawing.Size(238, 21)
        Me.dtStart.TabIndex = 87
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(302, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 16)
        Me.Label11.TabIndex = 82
        Me.Label11.Text = "End of Enrollment"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(135, 16)
        Me.Label8.TabIndex = 84
        Me.Label8.Text = "Enrolling Class Schedule"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(302, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 16)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "Start of Enrollment"
        '
        'cbStatus
        '
        Me.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Items.AddRange(New Object() {"Inactive", "Active"})
        Me.cbStatus.Location = New System.Drawing.Point(231, 143)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(506, 24)
        Me.cbStatus.TabIndex = 88
        '
        'cbSemester
        '
        Me.cbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSemester.FormattingEnabled = True
        Me.cbSemester.Items.AddRange(New Object() {"1st Semester", "2nd Semester", "Summer", "Trimester", "1st Trimester", "2nd Trimester", "3rd Trimester", "Review"})
        Me.cbSemester.Location = New System.Drawing.Point(231, 59)
        Me.cbSemester.Name = "cbSemester"
        Me.cbSemester.Size = New System.Drawing.Size(506, 24)
        Me.cbSemester.TabIndex = 88
        '
        'dtGrad
        '
        Me.dtGrad.CustomFormat = "yyyy/MM/dd"
        Me.dtGrad.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtGrad.Location = New System.Drawing.Point(231, 173)
        Me.dtGrad.Name = "dtGrad"
        Me.dtGrad.Size = New System.Drawing.Size(506, 21)
        Me.dtGrad.TabIndex = 87
        Me.dtGrad.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(81, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 16)
        Me.Label7.TabIndex = 81
        Me.Label7.Text = "Semester"
        '
        'lblGrad
        '
        Me.lblGrad.AutoSize = True
        Me.lblGrad.Location = New System.Drawing.Point(81, 179)
        Me.lblGrad.Name = "lblGrad"
        Me.lblGrad.Size = New System.Drawing.Size(102, 16)
        Me.lblGrad.TabIndex = 82
        Me.lblGrad.Text = "Graduation Date"
        Me.lblGrad.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(82, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 16)
        Me.Label4.TabIndex = 84
        Me.Label4.Text = "Status"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(81, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 16)
        Me.Label3.TabIndex = 85
        Me.Label3.Text = "End Year"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(81, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 16)
        Me.Label2.TabIndex = 86
        Me.Label2.Text = "Start Year"
        '
        'txtEnd
        '
        Me.txtEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEnd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEnd.Location = New System.Drawing.Point(231, 116)
        Me.txtEnd.Name = "txtEnd"
        Me.txtEnd.Size = New System.Drawing.Size(506, 21)
        Me.txtEnd.TabIndex = 79
        Me.txtEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStart
        '
        Me.txtStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStart.Location = New System.Drawing.Point(231, 89)
        Me.txtStart.Name = "txtStart"
        Me.txtStart.Size = New System.Drawing.Size(506, 21)
        Me.txtStart.TabIndex = 80
        Me.txtStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmAcademicYear
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(828, 479)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.systemSign)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmAcademicYear"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.systemSign.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.settingsPanel.ResumeLayout(False)
        Me.settingsPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnClose As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents systemSign As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents settingsPanel As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents cbBalance As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents cbAD As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cbEnroll As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents cbStatus As ComboBox
    Friend WithEvents cbSemester As ComboBox
    Friend WithEvents dtGrad As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents lblGrad As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtEnd As TextBox
    Friend WithEvents txtStart As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents dtEnd As DateTimePicker
    Friend WithEvents dtStart As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents Label6 As Label
End Class
