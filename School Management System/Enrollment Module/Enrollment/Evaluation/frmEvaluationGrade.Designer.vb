<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEvaluationGrade
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEvaluationGrade))
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnLink = New System.Windows.Forms.Button()
        Me.systemSign = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblCurrDesc = New System.Windows.Forms.Label()
        Me.lblCurrCode = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbGrade = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.PictureBox()
        Me.btnLinkGrade = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.systemSign.SuspendLayout()
        CType(Me.btnRemove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnLinkGrade, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.FlowLayoutPanel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 180)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(352, 39)
        Me.Panel4.TabIndex = 132
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.btnLink)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(-63, 0)
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
        Me.btnLink.Text = "SAVE"
        Me.btnLink.UseVisualStyleBackColor = True
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
        Me.systemSign.Size = New System.Drawing.Size(352, 39)
        Me.systemSign.TabIndex = 134
        '
        'btnClose
        '
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(326, 6)
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
        Me.Label1.Size = New System.Drawing.Size(132, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Encode Grade"
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
        Me.Panel1.Size = New System.Drawing.Size(352, 10)
        Me.Panel1.TabIndex = 133
        '
        'lblCurrDesc
        '
        Me.lblCurrDesc.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblCurrDesc.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrDesc.Location = New System.Drawing.Point(7, 104)
        Me.lblCurrDesc.Name = "lblCurrDesc"
        Me.lblCurrDesc.Size = New System.Drawing.Size(335, 35)
        Me.lblCurrDesc.TabIndex = 135
        Me.lblCurrDesc.Text = "Description"
        '
        'lblCurrCode
        '
        Me.lblCurrCode.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblCurrCode.AutoSize = True
        Me.lblCurrCode.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrCode.Location = New System.Drawing.Point(7, 88)
        Me.lblCurrCode.Name = "lblCurrCode"
        Me.lblCurrCode.Size = New System.Drawing.Size(36, 15)
        Me.lblCurrCode.TabIndex = 136
        Me.lblCurrCode.Text = "Code"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 16)
        Me.Label3.TabIndex = 137
        Me.Label3.Text = "Curriculum Subject:"
        '
        'cbGrade
        '
        Me.cbGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGrade.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGrade.FormattingEnabled = True
        Me.cbGrade.Items.AddRange(New Object() {"1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0", "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "3.0", "5.0", "D", "W"})
        Me.cbGrade.Location = New System.Drawing.Point(67, 141)
        Me.cbGrade.Name = "cbGrade"
        Me.cbGrade.Size = New System.Drawing.Size(95, 29)
        Me.cbGrade.TabIndex = 139
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 149)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 16)
        Me.Label8.TabIndex = 138
        Me.Label8.Text = "Grade"
        '
        'btnRemove
        '
        Me.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.Location = New System.Drawing.Point(253, 82)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(17, 19)
        Me.btnRemove.TabIndex = 348
        Me.btnRemove.TabStop = False
        '
        'btnLinkGrade
        '
        Me.btnLinkGrade.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLinkGrade.Image = CType(resources.GetObject("btnLinkGrade.Image"), System.Drawing.Image)
        Me.btnLinkGrade.Location = New System.Drawing.Point(253, 57)
        Me.btnLinkGrade.Name = "btnLinkGrade"
        Me.btnLinkGrade.Size = New System.Drawing.Size(17, 19)
        Me.btnLinkGrade.TabIndex = 348
        Me.btnLinkGrade.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(273, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 349
        Me.Label2.Text = "Link"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(273, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 349
        Me.Label4.Text = "Remove"
        '
        'frmEvaluationGrade
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 219)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnLinkGrade)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.cbGrade)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblCurrDesc)
        Me.Controls.Add(Me.lblCurrCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.systemSign)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmEvaluationGrade"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel4.ResumeLayout(False)
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.systemSign.ResumeLayout(False)
        CType(Me.btnRemove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnLinkGrade, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents btnLink As Button
    Friend WithEvents systemSign As Panel
    Friend WithEvents btnClose As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblCurrDesc As Label
    Friend WithEvents lblCurrCode As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbGrade As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents btnRemove As PictureBox
    Friend WithEvents btnLinkGrade As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
End Class
