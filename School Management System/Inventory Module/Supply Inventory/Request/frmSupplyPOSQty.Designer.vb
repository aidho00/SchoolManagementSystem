<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSupplyPOSQty
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
        Me.lblQty = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtbox_code = New System.Windows.Forms.Label()
        Me.period_id = New System.Windows.Forms.Label()
        Me.yearid = New System.Windows.Forms.Label()
        Me.bcode_last_no = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblQty
        '
        Me.lblQty.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.Color.White
        Me.lblQty.Location = New System.Drawing.Point(12, 120)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(276, 21)
        Me.lblQty.TabIndex = 18
        Me.lblQty.Text = "QUANTITY TO RELEASE"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtQty
        '
        Me.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtQty.Font = New System.Drawing.Font("Century Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.Location = New System.Drawing.Point(12, 144)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(276, 50)
        Me.txtQty.TabIndex = 17
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Font = New System.Drawing.Font("Century Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 46)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(276, 50)
        Me.TextBox1.TabIndex = 17
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(276, 21)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "REQUESTED QUANTITY"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtbox_code
        '
        Me.txtbox_code.AutoSize = True
        Me.txtbox_code.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbox_code.ForeColor = System.Drawing.Color.Black
        Me.txtbox_code.Location = New System.Drawing.Point(9, 5)
        Me.txtbox_code.Name = "txtbox_code"
        Me.txtbox_code.Size = New System.Drawing.Size(15, 17)
        Me.txtbox_code.TabIndex = 446
        Me.txtbox_code.Text = "0"
        Me.txtbox_code.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtbox_code.Visible = False
        '
        'period_id
        '
        Me.period_id.AutoSize = True
        Me.period_id.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.period_id.ForeColor = System.Drawing.Color.Black
        Me.period_id.Location = New System.Drawing.Point(72, 5)
        Me.period_id.Name = "period_id"
        Me.period_id.Size = New System.Drawing.Size(15, 17)
        Me.period_id.TabIndex = 449
        Me.period_id.Text = "0"
        Me.period_id.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.period_id.Visible = False
        '
        'yearid
        '
        Me.yearid.AutoSize = True
        Me.yearid.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.yearid.ForeColor = System.Drawing.Color.Black
        Me.yearid.Location = New System.Drawing.Point(51, 5)
        Me.yearid.Name = "yearid"
        Me.yearid.Size = New System.Drawing.Size(15, 17)
        Me.yearid.TabIndex = 447
        Me.yearid.Text = "0"
        Me.yearid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.yearid.Visible = False
        '
        'bcode_last_no
        '
        Me.bcode_last_no.AutoSize = True
        Me.bcode_last_no.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bcode_last_no.ForeColor = System.Drawing.Color.Black
        Me.bcode_last_no.Location = New System.Drawing.Point(30, 5)
        Me.bcode_last_no.Name = "bcode_last_no"
        Me.bcode_last_no.Size = New System.Drawing.Size(15, 17)
        Me.bcode_last_no.TabIndex = 448
        Me.bcode_last_no.Text = "0"
        Me.bcode_last_no.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bcode_last_no.Visible = False
        '
        'btnClose
        '
        Me.btnClose.AutoSize = True
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(278, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(22, 23)
        Me.btnClose.TabIndex = 463
        Me.btnClose.Text = "✕"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.Visible = False
        '
        'frmSupplyPOSQty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(300, 206)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.period_id)
        Me.Controls.Add(Me.yearid)
        Me.Controls.Add(Me.bcode_last_no)
        Me.Controls.Add(Me.txtbox_code)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.txtQty)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSupplyPOSQty"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblQty As Label
    Friend WithEvents txtQty As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtbox_code As Label
    Friend WithEvents period_id As Label
    Friend WithEvents yearid As Label
    Friend WithEvents bcode_last_no As Label
    Friend WithEvents btnClose As Label
End Class
