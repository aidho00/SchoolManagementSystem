<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSupplyPOSStudID
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
        Me.txtStudentID = New System.Windows.Forms.TextBox()
        Me.college = New System.Windows.Forms.CheckBox()
        Me.highschool = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblQty
        '
        Me.lblQty.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.Color.White
        Me.lblQty.Location = New System.Drawing.Point(12, 16)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(276, 21)
        Me.lblQty.TabIndex = 20
        Me.lblQty.Text = "SCAN/INPUT STUDENT ID"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStudentID
        '
        Me.txtStudentID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStudentID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStudentID.Font = New System.Drawing.Font("Century Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudentID.Location = New System.Drawing.Point(12, 52)
        Me.txtStudentID.Name = "txtStudentID"
        Me.txtStudentID.Size = New System.Drawing.Size(276, 50)
        Me.txtStudentID.TabIndex = 19
        Me.txtStudentID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'college
        '
        Me.college.AutoSize = True
        Me.college.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.college.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.college.ForeColor = System.Drawing.Color.White
        Me.college.Location = New System.Drawing.Point(12, 111)
        Me.college.Name = "college"
        Me.college.Size = New System.Drawing.Size(98, 20)
        Me.college.TabIndex = 140
        Me.college.Text = "          College"
        Me.college.UseVisualStyleBackColor = False
        '
        'highschool
        '
        Me.highschool.AutoSize = True
        Me.highschool.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.highschool.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.highschool.ForeColor = System.Drawing.Color.White
        Me.highschool.Location = New System.Drawing.Point(12, 137)
        Me.highschool.Name = "highschool"
        Me.highschool.Size = New System.Drawing.Size(143, 20)
        Me.highschool.TabIndex = 141
        Me.highschool.Text = "          Basic Education"
        Me.highschool.UseVisualStyleBackColor = False
        '
        'frmSupplyPOSStudID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(300, 168)
        Me.ControlBox = False
        Me.Controls.Add(Me.college)
        Me.Controls.Add(Me.highschool)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.txtStudentID)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSupplyPOSStudID"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblQty As Label
    Friend WithEvents txtStudentID As TextBox
    Friend WithEvents college As CheckBox
    Friend WithEvents highschool As CheckBox
End Class
