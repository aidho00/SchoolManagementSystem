<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentMonitoringUpdate
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymentMonitoringUpdate))
        Me.systemSign = New System.Windows.Forms.Panel()
        Me.lblPaymentStatus = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt4 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt1 = New System.Windows.Forms.Label()
        Me.txt2 = New System.Windows.Forms.Label()
        Me.txt3 = New System.Windows.Forms.TextBox()
        Me.txt8 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt5 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Label()
        Me.cbCashier = New System.Windows.Forms.ComboBox()
        Me.cbAcademicYear = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAmountChange = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtOR = New System.Windows.Forms.TextBox()
        Me.StudentName = New System.Windows.Forms.Label()
        Me.StudentID = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAmountReceived = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearchStudent = New System.Windows.Forms.Label()
        Me.txt6 = New System.Windows.Forms.TextBox()
        Me.txt7 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.SearchPanel = New System.Windows.Forms.Panel()
        Me.SearchBasePanel = New System.Windows.Forms.Panel()
        Me.dgPanel = New System.Windows.Forms.Panel()
        Me.dgStudentList = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUpdate = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.frmTitle = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.PanelReason = New System.Windows.Forms.Panel()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.closeReasonPanel = New System.Windows.Forms.Label()
        Me.btnUpdate2 = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.systemSign.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SearchPanel.SuspendLayout()
        Me.SearchBasePanel.SuspendLayout()
        Me.dgPanel.SuspendLayout()
        CType(Me.dgStudentList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.PanelReason.SuspendLayout()
        Me.SuspendLayout()
        '
        'systemSign
        '
        Me.systemSign.BackColor = System.Drawing.Color.White
        Me.systemSign.Controls.Add(Me.lblPaymentStatus)
        Me.systemSign.Controls.Add(Me.btnClose)
        Me.systemSign.Controls.Add(Me.Label1)
        Me.systemSign.Dock = System.Windows.Forms.DockStyle.Top
        Me.systemSign.Location = New System.Drawing.Point(0, 10)
        Me.systemSign.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.systemSign.Name = "systemSign"
        Me.systemSign.Padding = New System.Windows.Forms.Padding(6)
        Me.systemSign.Size = New System.Drawing.Size(881, 39)
        Me.systemSign.TabIndex = 14
        '
        'lblPaymentStatus
        '
        Me.lblPaymentStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPaymentStatus.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentStatus.ForeColor = System.Drawing.Color.Black
        Me.lblPaymentStatus.Location = New System.Drawing.Point(80, 6)
        Me.lblPaymentStatus.Name = "lblPaymentStatus"
        Me.lblPaymentStatus.Size = New System.Drawing.Size(266, 27)
        Me.lblPaymentStatus.TabIndex = 4
        Me.lblPaymentStatus.Text = "Payment"
        Me.lblPaymentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(855, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(20, 27)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "✕"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Visible = False
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Update"
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
        Me.Panel1.Size = New System.Drawing.Size(881, 10)
        Me.Panel1.TabIndex = 13
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 427)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(881, 39)
        Me.Panel3.TabIndex = 97
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCancel)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnUpdate)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(484, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(397, 39)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.Location = New System.Drawing.Point(298, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 34)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.Location = New System.Drawing.Point(196, 3)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(96, 34)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "UPDATE"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 173)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 94
        Me.Label9.Text = "Amount Received"
        '
        'txt4
        '
        Me.txt4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt4.Location = New System.Drawing.Point(115, 171)
        Me.txt4.Name = "txt4"
        Me.txt4.ReadOnly = True
        Me.txt4.Size = New System.Drawing.Size(284, 20)
        Me.txt4.TabIndex = 95
        Me.txt4.Text = "0.00"
        Me.txt4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 129)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 94
        Me.Label10.Text = "OR Number"
        '
        'txt1
        '
        Me.txt1.Location = New System.Drawing.Point(115, 67)
        Me.txt1.Name = "txt1"
        Me.txt1.Size = New System.Drawing.Size(284, 21)
        Me.txt1.TabIndex = 94
        Me.txt1.Text = "ID"
        Me.txt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt2
        '
        Me.txt2.Location = New System.Drawing.Point(115, 91)
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(284, 21)
        Me.txt2.TabIndex = 94
        Me.txt2.Text = "Name"
        Me.txt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt3
        '
        Me.txt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt3.Location = New System.Drawing.Point(115, 127)
        Me.txt3.Name = "txt3"
        Me.txt3.ReadOnly = True
        Me.txt3.Size = New System.Drawing.Size(284, 20)
        Me.txt3.TabIndex = 95
        '
        'txt8
        '
        Me.txt8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt8.Location = New System.Drawing.Point(115, 333)
        Me.txt8.Multiline = True
        Me.txt8.Name = "txt8"
        Me.txt8.ReadOnly = True
        Me.txt8.Size = New System.Drawing.Size(284, 80)
        Me.txt8.TabIndex = 95
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(17, 199)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 13)
        Me.Label13.TabIndex = 94
        Me.Label13.Text = "Amount Paid"
        '
        'txt5
        '
        Me.txt5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt5.Location = New System.Drawing.Point(115, 197)
        Me.txt5.Name = "txt5"
        Me.txt5.ReadOnly = True
        Me.txt5.Size = New System.Drawing.Size(284, 20)
        Me.txt5.TabIndex = 95
        Me.txt5.Text = "0.00"
        Me.txt5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(17, 225)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 13)
        Me.Label14.TabIndex = 94
        Me.Label14.Text = "Amount Change"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(17, 275)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(79, 13)
        Me.Label15.TabIndex = 94
        Me.Label15.Text = "Academic Year"
        '
        'TextBox5
        '
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox5.Location = New System.Drawing.Point(115, 223)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(284, 20)
        Me.TextBox5.TabIndex = 95
        Me.TextBox5.Text = "0.00"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(17, 302)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 13)
        Me.Label16.TabIndex = 94
        Me.Label16.Text = "Cashier"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(17, 340)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(35, 13)
        Me.Label17.TabIndex = 94
        Me.Label17.Text = "Notes"
        '
        'btnNext
        '
        Me.btnNext.Font = New System.Drawing.Font("Century Gothic", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.btnNext.Location = New System.Drawing.Point(416, 60)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(57, 346)
        Me.btnNext.TabIndex = 168
        Me.btnNext.Text = "﹥"
        Me.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbCashier
        '
        Me.cbCashier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCashier.FormattingEnabled = True
        Me.cbCashier.Items.AddRange(New Object() {"OR Number", "Student ID", "Student Name", "Cashier", "Date"})
        Me.cbCashier.Location = New System.Drawing.Point(577, 297)
        Me.cbCashier.Name = "cbCashier"
        Me.cbCashier.Size = New System.Drawing.Size(284, 21)
        Me.cbCashier.TabIndex = 183
        '
        'cbAcademicYear
        '
        Me.cbAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAcademicYear.FormattingEnabled = True
        Me.cbAcademicYear.Items.AddRange(New Object() {"OR Number", "Student ID", "Student Name", "Cashier", "Date"})
        Me.cbAcademicYear.Location = New System.Drawing.Point(577, 270)
        Me.cbAcademicYear.Name = "cbAcademicYear"
        Me.cbAcademicYear.Size = New System.Drawing.Size(284, 21)
        Me.cbAcademicYear.TabIndex = 184
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(479, 340)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 13)
        Me.Label8.TabIndex = 169
        Me.Label8.Text = "Notes"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(479, 302)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 170
        Me.Label6.Text = "Cashier"
        '
        'txtAmountChange
        '
        Me.txtAmountChange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountChange.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAmountChange.Location = New System.Drawing.Point(577, 223)
        Me.txtAmountChange.Name = "txtAmountChange"
        Me.txtAmountChange.ReadOnly = True
        Me.txtAmountChange.Size = New System.Drawing.Size(284, 20)
        Me.txtAmountChange.TabIndex = 178
        Me.txtAmountChange.Text = "0.00"
        Me.txtAmountChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(479, 275)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 171
        Me.Label5.Text = "Academic Year"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(479, 225)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 172
        Me.Label4.Text = "Amount Change"
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAmountPaid.Location = New System.Drawing.Point(577, 197)
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.Size = New System.Drawing.Size(284, 20)
        Me.txtAmountPaid.TabIndex = 179
        Me.txtAmountPaid.Text = "0.00"
        Me.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(479, 199)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 173
        Me.Label2.Text = "Amount Paid"
        '
        'txtNotes
        '
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Location = New System.Drawing.Point(577, 333)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ReadOnly = True
        Me.txtNotes.Size = New System.Drawing.Size(284, 80)
        Me.txtNotes.TabIndex = 180
        '
        'txtOR
        '
        Me.txtOR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOR.Location = New System.Drawing.Point(577, 127)
        Me.txtOR.Name = "txtOR"
        Me.txtOR.Size = New System.Drawing.Size(284, 20)
        Me.txtOR.TabIndex = 181
        '
        'StudentName
        '
        Me.StudentName.Location = New System.Drawing.Point(577, 91)
        Me.StudentName.Name = "StudentName"
        Me.StudentName.Size = New System.Drawing.Size(284, 21)
        Me.StudentName.TabIndex = 174
        Me.StudentName.Text = "Name"
        Me.StudentName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StudentID
        '
        Me.StudentID.Location = New System.Drawing.Point(577, 67)
        Me.StudentID.Name = "StudentID"
        Me.StudentID.Size = New System.Drawing.Size(284, 21)
        Me.StudentID.TabIndex = 175
        Me.StudentID.Text = "ID"
        Me.StudentID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(479, 129)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 13)
        Me.Label7.TabIndex = 176
        Me.Label7.Text = "OR Number"
        '
        'txtAmountReceived
        '
        Me.txtAmountReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountReceived.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAmountReceived.Location = New System.Drawing.Point(577, 171)
        Me.txtAmountReceived.Name = "txtAmountReceived"
        Me.txtAmountReceived.Size = New System.Drawing.Size(284, 20)
        Me.txtAmountReceived.TabIndex = 182
        Me.txtAmountReceived.Text = "0.00"
        Me.txtAmountReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(479, 173)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 177
        Me.Label3.Text = "Amount Received"
        '
        'btnSearchStudent
        '
        Me.btnSearchStudent.AutoSize = True
        Me.btnSearchStudent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchStudent.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchStudent.Location = New System.Drawing.Point(533, 67)
        Me.btnSearchStudent.Name = "btnSearchStudent"
        Me.btnSearchStudent.Size = New System.Drawing.Size(25, 20)
        Me.btnSearchStudent.TabIndex = 185
        Me.btnSearchStudent.Text = "🔍"
        '
        'txt6
        '
        Me.txt6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt6.Location = New System.Drawing.Point(115, 273)
        Me.txt6.Name = "txt6"
        Me.txt6.ReadOnly = True
        Me.txt6.Size = New System.Drawing.Size(284, 20)
        Me.txt6.TabIndex = 95
        '
        'txt7
        '
        Me.txt7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt7.Location = New System.Drawing.Point(115, 300)
        Me.txt7.Name = "txt7"
        Me.txt7.ReadOnly = True
        Me.txt7.Size = New System.Drawing.Size(284, 20)
        Me.txt7.TabIndex = 95
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 13)
        Me.Label11.TabIndex = 94
        Me.Label11.Text = "Student ID"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(17, 95)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 13)
        Me.Label12.TabIndex = 94
        Me.Label12.Text = "Student Name"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(479, 71)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 13)
        Me.Label18.TabIndex = 94
        Me.Label18.Text = "Student ID"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(479, 95)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(75, 13)
        Me.Label19.TabIndex = 94
        Me.Label19.Text = "Student Name"
        '
        'SearchPanel
        '
        Me.SearchPanel.Controls.Add(Me.SearchBasePanel)
        Me.SearchPanel.Controls.Add(Me.Panel7)
        Me.SearchPanel.Controls.Add(Me.Panel5)
        Me.SearchPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SearchPanel.Location = New System.Drawing.Point(0, 49)
        Me.SearchPanel.Name = "SearchPanel"
        Me.SearchPanel.Size = New System.Drawing.Size(881, 378)
        Me.SearchPanel.TabIndex = 186
        Me.SearchPanel.Visible = False
        '
        'SearchBasePanel
        '
        Me.SearchBasePanel.Controls.Add(Me.dgPanel)
        Me.SearchBasePanel.Controls.Add(Me.Panel6)
        Me.SearchBasePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SearchBasePanel.Location = New System.Drawing.Point(0, 1)
        Me.SearchBasePanel.Name = "SearchBasePanel"
        Me.SearchBasePanel.Size = New System.Drawing.Size(881, 327)
        Me.SearchBasePanel.TabIndex = 101
        '
        'dgPanel
        '
        Me.dgPanel.Controls.Add(Me.dgStudentList)
        Me.dgPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPanel.Location = New System.Drawing.Point(0, 35)
        Me.dgPanel.Name = "dgPanel"
        Me.dgPanel.Padding = New System.Windows.Forms.Padding(7, 0, 0, 0)
        Me.dgPanel.Size = New System.Drawing.Size(881, 292)
        Me.dgPanel.TabIndex = 104
        '
        'dgStudentList
        '
        Me.dgStudentList.AllowUserToAddRows = False
        Me.dgStudentList.AllowUserToDeleteRows = False
        Me.dgStudentList.BackgroundColor = System.Drawing.Color.White
        Me.dgStudentList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgStudentList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgStudentList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgStudentList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgStudentList.ColumnHeadersHeight = 40
        Me.dgStudentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgStudentList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.Column1, Me.Column3, Me.Column2, Me.Column5, Me.Column6, Me.colUpdate})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgStudentList.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgStudentList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgStudentList.EnableHeadersVisualStyles = False
        Me.dgStudentList.GridColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.dgStudentList.Location = New System.Drawing.Point(7, 0)
        Me.dgStudentList.Name = "dgStudentList"
        Me.dgStudentList.ReadOnly = True
        Me.dgStudentList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgStudentList.RowHeadersVisible = False
        Me.dgStudentList.RowTemplate.Height = 26
        Me.dgStudentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgStudentList.Size = New System.Drawing.Size(874, 292)
        Me.dgStudentList.TabIndex = 86
        '
        'Column4
        '
        Me.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column4.HeaderText = "#"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 37
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 41
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "Last Name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "First Name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column1.HeaderText = "Middle Name"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column3.HeaderText = "Suffix"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 59
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column2.HeaderText = "Gender"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 68
        '
        'Column5
        '
        Me.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column5.HeaderText = "Year Level"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 72
        '
        'Column6
        '
        Me.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column6.HeaderText = "Course/Strand/Grade"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 139
        '
        'colUpdate
        '
        Me.colUpdate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colUpdate.HeaderText = ""
        Me.colUpdate.Image = CType(resources.GetObject("colUpdate.Image"), System.Drawing.Image)
        Me.colUpdate.Name = "colUpdate"
        Me.colUpdate.ReadOnly = True
        Me.colUpdate.Visible = False
        Me.colUpdate.Width = 5
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.Panel4)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel6.Size = New System.Drawing.Size(881, 35)
        Me.Panel6.TabIndex = 103
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.txtSearch)
        Me.Panel4.Controls.Add(Me.frmTitle)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(875, 29)
        Me.Panel4.TabIndex = 11
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(97, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(287, 27)
        Me.txtSearch.TabIndex = 25
        '
        'frmTitle
        '
        Me.frmTitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.frmTitle.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmTitle.ForeColor = System.Drawing.Color.Black
        Me.frmTitle.Location = New System.Drawing.Point(0, 0)
        Me.frmTitle.Name = "frmTitle"
        Me.frmTitle.Size = New System.Drawing.Size(97, 29)
        Me.frmTitle.TabIndex = 24
        Me.frmTitle.Text = " Search"
        Me.frmTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel7.Size = New System.Drawing.Size(881, 1)
        Me.Panel7.TabIndex = 100
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnSelect)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 328)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(8)
        Me.Panel5.Size = New System.Drawing.Size(881, 50)
        Me.Panel5.TabIndex = 0
        '
        'btnSelect
        '
        Me.btnSelect.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelect.FlatAppearance.BorderSize = 0
        Me.btnSelect.Location = New System.Drawing.Point(777, 8)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(96, 34)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "SELECT"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'PanelReason
        '
        Me.PanelReason.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PanelReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelReason.Controls.Add(Me.txtReason)
        Me.PanelReason.Controls.Add(Me.closeReasonPanel)
        Me.PanelReason.Controls.Add(Me.btnUpdate2)
        Me.PanelReason.Controls.Add(Me.Label20)
        Me.PanelReason.Location = New System.Drawing.Point(252, 119)
        Me.PanelReason.Name = "PanelReason"
        Me.PanelReason.Size = New System.Drawing.Size(400, 210)
        Me.PanelReason.TabIndex = 188
        Me.PanelReason.Visible = False
        '
        'txtReason
        '
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Location = New System.Drawing.Point(12, 33)
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(375, 128)
        Me.txtReason.TabIndex = 184
        '
        'closeReasonPanel
        '
        Me.closeReasonPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.closeReasonPanel.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.closeReasonPanel.ForeColor = System.Drawing.Color.Black
        Me.closeReasonPanel.Location = New System.Drawing.Point(375, 2)
        Me.closeReasonPanel.Name = "closeReasonPanel"
        Me.closeReasonPanel.Size = New System.Drawing.Size(20, 27)
        Me.closeReasonPanel.TabIndex = 183
        Me.closeReasonPanel.Text = "✕"
        Me.closeReasonPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUpdate2
        '
        Me.btnUpdate2.FlatAppearance.BorderSize = 0
        Me.btnUpdate2.Location = New System.Drawing.Point(88, 167)
        Me.btnUpdate2.Name = "btnUpdate2"
        Me.btnUpdate2.Size = New System.Drawing.Size(227, 34)
        Me.btnUpdate2.TabIndex = 182
        Me.btnUpdate2.Text = "UPDATE"
        Me.btnUpdate2.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(149, 13)
        Me.Label20.TabIndex = 95
        Me.Label20.Text = "Reason on Updating Payment"
        '
        'frmPaymentMonitoringUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(881, 466)
        Me.ControlBox = False
        Me.Controls.Add(Me.SearchPanel)
        Me.Controls.Add(Me.PanelReason)
        Me.Controls.Add(Me.cbCashier)
        Me.Controls.Add(Me.cbAcademicYear)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtAmountChange)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtAmountPaid)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.txtOR)
        Me.Controls.Add(Me.StudentName)
        Me.Controls.Add(Me.StudentID)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtAmountReceived)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txt5)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txt8)
        Me.Controls.Add(Me.txt7)
        Me.Controls.Add(Me.txt6)
        Me.Controls.Add(Me.txt3)
        Me.Controls.Add(Me.txt2)
        Me.Controls.Add(Me.txt1)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.systemSign)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnSearchStudent)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmPaymentMonitoringUpdate"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.systemSign.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.SearchPanel.ResumeLayout(False)
        Me.SearchBasePanel.ResumeLayout(False)
        Me.dgPanel.ResumeLayout(False)
        CType(Me.dgStudentList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.PanelReason.ResumeLayout(False)
        Me.PanelReason.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents systemSign As Panel
    Friend WithEvents btnClose As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents txt4 As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txt1 As Label
    Friend WithEvents txt2 As Label
    Friend WithEvents txt3 As TextBox
    Friend WithEvents txt8 As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txt5 As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents btnNext As Label
    Friend WithEvents cbCashier As ComboBox
    Friend WithEvents cbAcademicYear As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtAmountChange As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtAmountPaid As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents txtOR As TextBox
    Friend WithEvents StudentName As Label
    Friend WithEvents StudentID As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtAmountReceived As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnSearchStudent As Label
    Friend WithEvents txt6 As TextBox
    Friend WithEvents txt7 As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents SearchPanel As Panel
    Friend WithEvents SearchBasePanel As Panel
    Friend WithEvents dgPanel As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents frmTitle As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents btnSelect As Button
    Friend WithEvents dgStudentList As DataGridView
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents colUpdate As DataGridViewImageColumn
    Friend WithEvents PanelReason As Panel
    Friend WithEvents btnUpdate2 As Button
    Friend WithEvents Label20 As Label
    Friend WithEvents txtReason As TextBox
    Friend WithEvents closeReasonPanel As Label
    Friend WithEvents lblPaymentStatus As Label
End Class
