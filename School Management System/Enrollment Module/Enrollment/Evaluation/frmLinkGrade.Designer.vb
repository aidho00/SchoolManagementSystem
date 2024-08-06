<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLinkGrade
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.systemSign = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnLink = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCurrCode = New System.Windows.Forms.Label()
        Me.lblCurrDesc = New System.Windows.Forms.Label()
        Me.lblAcadStatus = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.lblSchool = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbGrade = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblGrade = New System.Windows.Forms.Label()
        Me.systemSign.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
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
        Me.systemSign.Size = New System.Drawing.Size(632, 39)
        Me.systemSign.TabIndex = 25
        '
        'btnClose
        '
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(606, 6)
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
        Me.Label1.Size = New System.Drawing.Size(449, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Confirm Link Grade"
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
        Me.Panel1.Size = New System.Drawing.Size(632, 10)
        Me.Panel1.TabIndex = 24
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.FlowLayoutPanel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 246)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(632, 39)
        Me.Panel4.TabIndex = 131
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.btnLink)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(217, 0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(415, 39)
        Me.FlowLayoutPanel2.TabIndex = 2
        '
        'btnLink
        '
        Me.btnLink.FlatAppearance.BorderSize = 0
        Me.btnLink.Location = New System.Drawing.Point(316, 3)
        Me.btnLink.Name = "btnLink"
        Me.btnLink.Size = New System.Drawing.Size(96, 34)
        Me.btnLink.TabIndex = 2
        Me.btnLink.Text = "LINK"
        Me.btnLink.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 16)
        Me.Label3.TabIndex = 132
        Me.Label3.Text = "Curriculum Subject:"
        '
        'lblCurrCode
        '
        Me.lblCurrCode.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblCurrCode.AutoSize = True
        Me.lblCurrCode.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrCode.Location = New System.Drawing.Point(24, 148)
        Me.lblCurrCode.Name = "lblCurrCode"
        Me.lblCurrCode.Size = New System.Drawing.Size(36, 15)
        Me.lblCurrCode.TabIndex = 132
        Me.lblCurrCode.Text = "Code"
        '
        'lblCurrDesc
        '
        Me.lblCurrDesc.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblCurrDesc.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrDesc.Location = New System.Drawing.Point(24, 164)
        Me.lblCurrDesc.Name = "lblCurrDesc"
        Me.lblCurrDesc.Size = New System.Drawing.Size(268, 35)
        Me.lblCurrDesc.TabIndex = 132
        Me.lblCurrDesc.Text = "Description"
        '
        'lblAcadStatus
        '
        Me.lblAcadStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblAcadStatus.AutoSize = True
        Me.lblAcadStatus.Location = New System.Drawing.Point(346, 95)
        Me.lblAcadStatus.Name = "lblAcadStatus"
        Me.lblAcadStatus.Size = New System.Drawing.Size(92, 16)
        Me.lblAcadStatus.TabIndex = 132
        Me.lblAcadStatus.Text = "Academic Year"
        '
        'lblDesc
        '
        Me.lblDesc.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblDesc.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.Location = New System.Drawing.Point(346, 164)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(268, 35)
        Me.lblDesc.TabIndex = 132
        Me.lblDesc.Text = "Description"
        '
        'lblSchool
        '
        Me.lblSchool.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblSchool.Location = New System.Drawing.Point(346, 113)
        Me.lblSchool.Name = "lblSchool"
        Me.lblSchool.Size = New System.Drawing.Size(268, 35)
        Me.lblSchool.TabIndex = 132
        Me.lblSchool.Text = "School"
        '
        'lblCode
        '
        Me.lblCode.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblCode.AutoSize = True
        Me.lblCode.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(346, 148)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(36, 15)
        Me.lblCode.TabIndex = 132
        Me.lblCode.Text = "Code"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(350, 217)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 16)
        Me.Label8.TabIndex = 132
        Me.Label8.Text = "Grade"
        '
        'cbGrade
        '
        Me.cbGrade.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGrade.FormattingEnabled = True
        Me.cbGrade.Items.AddRange(New Object() {"1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0", "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "3.0", "5.0", "D", "W"})
        Me.cbGrade.Location = New System.Drawing.Point(409, 210)
        Me.cbGrade.Name = "cbGrade"
        Me.cbGrade.Size = New System.Drawing.Size(95, 29)
        Me.cbGrade.TabIndex = 133
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(346, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 16)
        Me.Label2.TabIndex = 132
        Me.Label2.Text = "Link To:"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(316, 56)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1, 225)
        Me.Panel2.TabIndex = 134
        '
        'lblGrade
        '
        Me.lblGrade.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblGrade.AutoSize = True
        Me.lblGrade.Location = New System.Drawing.Point(411, 217)
        Me.lblGrade.Name = "lblGrade"
        Me.lblGrade.Size = New System.Drawing.Size(44, 16)
        Me.lblGrade.TabIndex = 132
        Me.lblGrade.Text = "Grade"
        '
        'frmLinkGrade
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(632, 285)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.cbGrade)
        Me.Controls.Add(Me.lblSchool)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.lblCurrDesc)
        Me.Controls.Add(Me.lblAcadStatus)
        Me.Controls.Add(Me.lblCode)
        Me.Controls.Add(Me.lblCurrCode)
        Me.Controls.Add(Me.lblGrade)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.systemSign)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmLinkGrade"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.systemSign.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents systemSign As Panel
    Friend WithEvents btnClose As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents lblCurrCode As Label
    Friend WithEvents lblCurrDesc As Label
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents btnLink As Button
    Friend WithEvents lblAcadStatus As Label
    Friend WithEvents lblDesc As Label
    Friend WithEvents lblSchool As Label
    Friend WithEvents lblCode As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cbGrade As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblGrade As Label
End Class
