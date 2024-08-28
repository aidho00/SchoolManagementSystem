<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGradesRevisionPortalUpload
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGradesRevisionPortalUpload))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.loading = New System.Windows.Forms.PictureBox()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgDummyList = New System.Windows.Forms.DataGridView()
        Me.cbAcademicYear = New System.Windows.Forms.ComboBox()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgPanel = New System.Windows.Forms.Panel()
        Me.dgStudentList = New System.Windows.Forms.DataGridView()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.systemSign = New System.Windows.Forms.Panel()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.studentID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.studentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.loading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgDummyList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.dgPanel.SuspendLayout()
        CType(Me.dgStudentList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.systemSign.SuspendLayout()
        Me.SuspendLayout()
        '
        'BackgroundWorker2
        '
        Me.BackgroundWorker2.WorkerReportsProgress = True
        Me.BackgroundWorker2.WorkerSupportsCancellation = True
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.Location = New System.Drawing.Point(185, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 34)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Location = New System.Drawing.Point(83, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 34)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "UPLOAD"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCancel)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnSave)
        Me.FlowLayoutPanel1.Controls.Add(Me.loading)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(987, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(284, 39)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'loading
        '
        Me.loading.Image = CType(resources.GetObject("loading.Image"), System.Drawing.Image)
        Me.loading.Location = New System.Drawing.Point(48, 3)
        Me.loading.Name = "loading"
        Me.loading.Size = New System.Drawing.Size(29, 33)
        Me.loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.loading.TabIndex = 129
        Me.loading.TabStop = False
        Me.loading.Visible = False
        '
        'btnImport
        '
        Me.btnImport.FlatAppearance.BorderSize = 0
        Me.btnImport.Location = New System.Drawing.Point(3, 3)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(96, 34)
        Me.btnImport.TabIndex = 5
        Me.btnImport.Text = "IMPORT"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(411, 11)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(73, 16)
        Me.lblProgress.TabIndex = 90
        Me.lblProgress.Text = "Progress: 0/0"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(111, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 16)
        Me.Label5.TabIndex = 90
        Me.Label5.Text = "Academic Year"
        '
        'dgDummyList
        '
        Me.dgDummyList.AllowUserToAddRows = False
        Me.dgDummyList.AllowUserToDeleteRows = False
        Me.dgDummyList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgDummyList.BackgroundColor = System.Drawing.Color.White
        Me.dgDummyList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgDummyList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgDummyList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgDummyList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgDummyList.ColumnHeadersHeight = 40
        Me.dgDummyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgDummyList.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgDummyList.EnableHeadersVisualStyles = False
        Me.dgDummyList.GridColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.dgDummyList.Location = New System.Drawing.Point(538, 8)
        Me.dgDummyList.Name = "dgDummyList"
        Me.dgDummyList.ReadOnly = True
        Me.dgDummyList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgDummyList.RowHeadersVisible = False
        Me.dgDummyList.RowTemplate.Height = 26
        Me.dgDummyList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgDummyList.Size = New System.Drawing.Size(45, 25)
        Me.dgDummyList.TabIndex = 87
        Me.dgDummyList.Visible = False
        '
        'cbAcademicYear
        '
        Me.cbAcademicYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbAcademicYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbAcademicYear.FormattingEnabled = True
        Me.cbAcademicYear.Items.AddRange(New Object() {"Inactive", "Active"})
        Me.cbAcademicYear.Location = New System.Drawing.Point(209, 8)
        Me.cbAcademicYear.Name = "cbAcademicYear"
        Me.cbAcademicYear.Size = New System.Drawing.Size(196, 24)
        Me.cbAcademicYear.TabIndex = 89
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.btnImport)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(105, 39)
        Me.FlowLayoutPanel2.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblProgress)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.dgDummyList)
        Me.Panel1.Controls.Add(Me.cbAcademicYear)
        Me.Panel1.Controls.Add(Me.FlowLayoutPanel2)
        Me.Panel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(7, 643)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1271, 39)
        Me.Panel1.TabIndex = 0
        '
        'dgPanel
        '
        Me.dgPanel.Controls.Add(Me.dgStudentList)
        Me.dgPanel.Controls.Add(Me.Panel1)
        Me.dgPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPanel.Location = New System.Drawing.Point(0, 36)
        Me.dgPanel.Name = "dgPanel"
        Me.dgPanel.Padding = New System.Windows.Forms.Padding(7, 0, 0, 0)
        Me.dgPanel.Size = New System.Drawing.Size(1278, 682)
        Me.dgPanel.TabIndex = 100
        '
        'dgStudentList
        '
        Me.dgStudentList.AllowUserToAddRows = False
        Me.dgStudentList.BackgroundColor = System.Drawing.Color.White
        Me.dgStudentList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgStudentList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgStudentList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgStudentList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgStudentList.ColumnHeadersHeight = 40
        Me.dgStudentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgStudentList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.DataGridViewTextBoxColumn1, Me.Column2, Me.Column10, Me.studentID, Me.studentName, Me.Column8, Me.Column9})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgStudentList.DefaultCellStyle = DataGridViewCellStyle8
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
        Me.dgStudentList.Size = New System.Drawing.Size(1271, 643)
        Me.dgStudentList.TabIndex = 86
        '
        'btnClose
        '
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(1253, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(20, 26)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "✕"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(5, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(296, 26)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Upload Grades Revision from Portal"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'systemSign
        '
        Me.systemSign.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.systemSign.Controls.Add(Me.btnClose)
        Me.systemSign.Controls.Add(Me.Label1)
        Me.systemSign.Dock = System.Windows.Forms.DockStyle.Top
        Me.systemSign.Location = New System.Drawing.Point(0, 0)
        Me.systemSign.Name = "systemSign"
        Me.systemSign.Padding = New System.Windows.Forms.Padding(5)
        Me.systemSign.Size = New System.Drawing.Size(1278, 36)
        Me.systemSign.TabIndex = 99
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Column4
        '
        Me.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column4.HeaderText = "#"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 39
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Class Schedule ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column2.HeaderText = "Academic Year ID"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        '
        'Column10
        '
        Me.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column10.HeaderText = "Subject"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 72
        '
        'studentID
        '
        Me.studentID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.studentID.HeaderText = "Student ID"
        Me.studentID.Name = "studentID"
        Me.studentID.ReadOnly = True
        '
        'studentName
        '
        Me.studentName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.studentName.HeaderText = "Name"
        Me.studentName.Name = "studentName"
        Me.studentName.ReadOnly = True
        Me.studentName.Visible = False
        '
        'Column8
        '
        Me.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column8.HeaderText = "Current Grade"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 101
        '
        'Column9
        '
        Me.Column9.HeaderText = "Revised Grade"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'frmGradesRevisionPortalUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1278, 718)
        Me.ControlBox = False
        Me.Controls.Add(Me.dgPanel)
        Me.Controls.Add(Me.systemSign)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmGradesRevisionPortalUpload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.FlowLayoutPanel1.ResumeLayout(False)
        CType(Me.loading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgDummyList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.dgPanel.ResumeLayout(False)
        CType(Me.dgStudentList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.systemSign.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents loading As PictureBox
    Friend WithEvents btnImport As Button
    Friend WithEvents lblProgress As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents dgDummyList As DataGridView
    Friend WithEvents cbAcademicYear As ComboBox
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents dgPanel As Panel
    Friend WithEvents dgStudentList As DataGridView
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents studentID As DataGridViewTextBoxColumn
    Friend WithEvents studentName As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents btnClose As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents systemSign As Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
