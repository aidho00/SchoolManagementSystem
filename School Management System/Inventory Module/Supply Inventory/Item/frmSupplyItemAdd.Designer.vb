<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSupplyItemAdd
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSupplyItemAdd))
        Me.systemSign = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BasePanel = New System.Windows.Forms.Panel()
        Me.SearchPanel = New System.Windows.Forms.Panel()
        Me.SearchBasePanel = New System.Windows.Forms.Panel()
        Me.dgPanel = New System.Windows.Forms.Panel()
        Me.dgSupplySize = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgSupplyCategory = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.frmTitle = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.PictureBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnCancelSearch = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cbSupplySize = New System.Windows.Forms.TextBox()
        Me.cbSupplyCategory = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.barcodeIMG = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbSupplyStatus = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbSupplyType = New System.Windows.Forms.ComboBox()
        Me.btnSearchSize = New System.Windows.Forms.Label()
        Me.btnSearchCategory = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.barcodeID = New System.Windows.Forms.Label()
        Me.txtReOrderPoint = New System.Windows.Forms.TextBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtOpeningStock = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSupplyDesc = New System.Windows.Forms.TextBox()
        Me.systemSign.SuspendLayout()
        Me.BasePanel.SuspendLayout()
        Me.SearchPanel.SuspendLayout()
        Me.SearchBasePanel.SuspendLayout()
        Me.dgPanel.SuspendLayout()
        CType(Me.dgSupplySize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgSupplyCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.barcodeIMG, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.systemSign.Size = New System.Drawing.Size(828, 39)
        Me.systemSign.TabIndex = 26
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
        Me.Label1.Size = New System.Drawing.Size(449, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Supply Item Entry"
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
        Me.Panel1.Size = New System.Drawing.Size(828, 10)
        Me.Panel1.TabIndex = 25
        '
        'BasePanel
        '
        Me.BasePanel.Controls.Add(Me.SearchPanel)
        Me.BasePanel.Controls.Add(Me.Panel2)
        Me.BasePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BasePanel.Location = New System.Drawing.Point(0, 49)
        Me.BasePanel.Name = "BasePanel"
        Me.BasePanel.Size = New System.Drawing.Size(828, 430)
        Me.BasePanel.TabIndex = 419
        '
        'SearchPanel
        '
        Me.SearchPanel.Controls.Add(Me.SearchBasePanel)
        Me.SearchPanel.Controls.Add(Me.Panel10)
        Me.SearchPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SearchPanel.Location = New System.Drawing.Point(0, 0)
        Me.SearchPanel.Name = "SearchPanel"
        Me.SearchPanel.Size = New System.Drawing.Size(828, 430)
        Me.SearchPanel.TabIndex = 171
        Me.SearchPanel.Visible = False
        '
        'SearchBasePanel
        '
        Me.SearchBasePanel.Controls.Add(Me.dgPanel)
        Me.SearchBasePanel.Controls.Add(Me.Panel7)
        Me.SearchBasePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SearchBasePanel.Location = New System.Drawing.Point(0, 0)
        Me.SearchBasePanel.Name = "SearchBasePanel"
        Me.SearchBasePanel.Size = New System.Drawing.Size(828, 380)
        Me.SearchBasePanel.TabIndex = 101
        '
        'dgPanel
        '
        Me.dgPanel.Controls.Add(Me.dgSupplySize)
        Me.dgPanel.Controls.Add(Me.dgSupplyCategory)
        Me.dgPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPanel.Location = New System.Drawing.Point(0, 35)
        Me.dgPanel.Name = "dgPanel"
        Me.dgPanel.Padding = New System.Windows.Forms.Padding(7, 0, 0, 0)
        Me.dgPanel.Size = New System.Drawing.Size(828, 345)
        Me.dgPanel.TabIndex = 104
        '
        'dgSupplySize
        '
        Me.dgSupplySize.AllowUserToAddRows = False
        Me.dgSupplySize.AllowUserToDeleteRows = False
        Me.dgSupplySize.BackgroundColor = System.Drawing.Color.White
        Me.dgSupplySize.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgSupplySize.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgSupplySize.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgSupplySize.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgSupplySize.ColumnHeadersHeight = 40
        Me.dgSupplySize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgSupplySize.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgSupplySize.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgSupplySize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgSupplySize.EnableHeadersVisualStyles = False
        Me.dgSupplySize.GridColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.dgSupplySize.Location = New System.Drawing.Point(7, 0)
        Me.dgSupplySize.Name = "dgSupplySize"
        Me.dgSupplySize.ReadOnly = True
        Me.dgSupplySize.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgSupplySize.RowHeadersVisible = False
        Me.dgSupplySize.RowTemplate.Height = 26
        Me.dgSupplySize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSupplySize.Size = New System.Drawing.Size(821, 345)
        Me.dgSupplySize.TabIndex = 96
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn2.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 44
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "Size Description"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'dgSupplyCategory
        '
        Me.dgSupplyCategory.AllowUserToAddRows = False
        Me.dgSupplyCategory.AllowUserToDeleteRows = False
        Me.dgSupplyCategory.BackgroundColor = System.Drawing.Color.White
        Me.dgSupplyCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgSupplyCategory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgSupplyCategory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgSupplyCategory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgSupplyCategory.ColumnHeadersHeight = 40
        Me.dgSupplyCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgSupplyCategory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn17, Me.DataGridViewTextBoxColumn18})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgSupplyCategory.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgSupplyCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgSupplyCategory.EnableHeadersVisualStyles = False
        Me.dgSupplyCategory.GridColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.dgSupplyCategory.Location = New System.Drawing.Point(7, 0)
        Me.dgSupplyCategory.Name = "dgSupplyCategory"
        Me.dgSupplyCategory.ReadOnly = True
        Me.dgSupplyCategory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgSupplyCategory.RowHeadersVisible = False
        Me.dgSupplyCategory.RowTemplate.Height = 26
        Me.dgSupplyCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSupplyCategory.Size = New System.Drawing.Size(821, 345)
        Me.dgSupplyCategory.TabIndex = 95
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn17.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Visible = False
        Me.DataGridViewTextBoxColumn17.Width = 44
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn18.HeaderText = "Category Description"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel7.Size = New System.Drawing.Size(828, 35)
        Me.Panel7.TabIndex = 103
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.frmTitle)
        Me.Panel8.Controls.Add(Me.btnAdd)
        Me.Panel8.Controls.Add(Me.txtSearch)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(3, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(822, 29)
        Me.Panel8.TabIndex = 11
        '
        'frmTitle
        '
        Me.frmTitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.frmTitle.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmTitle.ForeColor = System.Drawing.Color.Black
        Me.frmTitle.Location = New System.Drawing.Point(27, 0)
        Me.frmTitle.Name = "frmTitle"
        Me.frmTitle.Size = New System.Drawing.Size(141, 29)
        Me.frmTitle.TabIndex = 352
        Me.frmTitle.Text = " Search"
        Me.frmTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAdd
        '
        Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(0, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(27, 29)
        Me.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnAdd.TabIndex = 351
        Me.btnAdd.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(175, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(297, 27)
        Me.txtSearch.TabIndex = 25
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.btnSelect)
        Me.Panel10.Controls.Add(Me.btnCancelSearch)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(0, 380)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(8)
        Me.Panel10.Size = New System.Drawing.Size(828, 50)
        Me.Panel10.TabIndex = 0
        '
        'btnSelect
        '
        Me.btnSelect.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelect.FlatAppearance.BorderSize = 0
        Me.btnSelect.Location = New System.Drawing.Point(628, 8)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(96, 34)
        Me.btnSelect.TabIndex = 5
        Me.btnSelect.Text = "SELECT"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnCancelSearch
        '
        Me.btnCancelSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancelSearch.FlatAppearance.BorderSize = 0
        Me.btnCancelSearch.Location = New System.Drawing.Point(724, 8)
        Me.btnCancelSearch.Name = "btnCancelSearch"
        Me.btnCancelSearch.Size = New System.Drawing.Size(96, 34)
        Me.btnCancelSearch.TabIndex = 4
        Me.btnCancelSearch.Text = "CANCEL"
        Me.btnCancelSearch.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cbSupplySize)
        Me.Panel2.Controls.Add(Me.cbSupplyCategory)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.barcodeIMG)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.cbSupplyStatus)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.cbSupplyType)
        Me.Panel2.Controls.Add(Me.btnSearchSize)
        Me.Panel2.Controls.Add(Me.btnSearchCategory)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.barcodeID)
        Me.Panel2.Controls.Add(Me.txtReOrderPoint)
        Me.Panel2.Controls.Add(Me.txtPrice)
        Me.Panel2.Controls.Add(Me.txtOpeningStock)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtSupplyDesc)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(828, 430)
        Me.Panel2.TabIndex = 0
        '
        'cbSupplySize
        '
        Me.cbSupplySize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cbSupplySize.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cbSupplySize.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSupplySize.Location = New System.Drawing.Point(206, 163)
        Me.cbSupplySize.Name = "cbSupplySize"
        Me.cbSupplySize.Size = New System.Drawing.Size(280, 21)
        Me.cbSupplySize.TabIndex = 442
        '
        'cbSupplyCategory
        '
        Me.cbSupplyCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cbSupplyCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.cbSupplyCategory.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSupplyCategory.Location = New System.Drawing.Point(206, 135)
        Me.cbSupplyCategory.Name = "cbSupplyCategory"
        Me.cbSupplyCategory.Size = New System.Drawing.Size(280, 21)
        Me.cbSupplyCategory.TabIndex = 442
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 391)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(828, 39)
        Me.Panel3.TabIndex = 441
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCancel)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnSave)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnUpdate)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(451, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(377, 39)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.Location = New System.Drawing.Point(278, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 34)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Location = New System.Drawing.Point(176, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 34)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "SAVE"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.Location = New System.Drawing.Point(74, 3)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(96, 34)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "UPDATE"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'barcodeIMG
        '
        Me.barcodeIMG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.barcodeIMG.Location = New System.Drawing.Point(531, 100)
        Me.barcodeIMG.Name = "barcodeIMG"
        Me.barcodeIMG.Size = New System.Drawing.Size(229, 88)
        Me.barcodeIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.barcodeIMG.TabIndex = 440
        Me.barcodeIMG.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(528, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 16)
        Me.Label8.TabIndex = 432
        Me.Label8.Text = "Barcode"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(75, 275)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 16)
        Me.Label10.TabIndex = 431
        Me.Label10.Text = "Price"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(75, 304)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 16)
        Me.Label4.TabIndex = 430
        Me.Label4.Text = "Reorder Point"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(75, 333)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 16)
        Me.Label6.TabIndex = 429
        Me.Label6.Text = "Opening Stock"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(75, 196)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(110, 16)
        Me.Label9.TabIndex = 428
        Me.Label9.Text = "Description (Brand)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(75, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 16)
        Me.Label5.TabIndex = 427
        Me.Label5.Text = "Size"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(75, 137)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 16)
        Me.Label3.TabIndex = 426
        Me.Label3.Text = "Category"
        '
        'cbSupplyStatus
        '
        Me.cbSupplyStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSupplyStatus.FormattingEnabled = True
        Me.cbSupplyStatus.Items.AddRange(New Object() {"Available", "Unavailable"})
        Me.cbSupplyStatus.Location = New System.Drawing.Point(616, 38)
        Me.cbSupplyStatus.Name = "cbSupplyStatus"
        Me.cbSupplyStatus.Size = New System.Drawing.Size(144, 24)
        Me.cbSupplyStatus.TabIndex = 436
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(528, 41)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 16)
        Me.Label11.TabIndex = 425
        Me.Label11.Text = "Status"
        '
        'cbSupplyType
        '
        Me.cbSupplyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSupplyType.FormattingEnabled = True
        Me.cbSupplyType.Items.AddRange(New Object() {"", "Office Supply", "School Consumable"})
        Me.cbSupplyType.Location = New System.Drawing.Point(206, 104)
        Me.cbSupplyType.Name = "cbSupplyType"
        Me.cbSupplyType.Size = New System.Drawing.Size(280, 24)
        Me.cbSupplyType.TabIndex = 439
        '
        'btnSearchSize
        '
        Me.btnSearchSize.AutoSize = True
        Me.btnSearchSize.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchSize.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchSize.Location = New System.Drawing.Point(492, 164)
        Me.btnSearchSize.Name = "btnSearchSize"
        Me.btnSearchSize.Size = New System.Drawing.Size(24, 17)
        Me.btnSearchSize.TabIndex = 424
        Me.btnSearchSize.Text = "🔍"
        '
        'btnSearchCategory
        '
        Me.btnSearchCategory.AutoSize = True
        Me.btnSearchCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchCategory.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchCategory.Location = New System.Drawing.Point(492, 135)
        Me.btnSearchCategory.Name = "btnSearchCategory"
        Me.btnSearchCategory.Size = New System.Drawing.Size(24, 17)
        Me.btnSearchCategory.TabIndex = 423
        Me.btnSearchCategory.Text = "🔍"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(75, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 16)
        Me.Label7.TabIndex = 433
        Me.Label7.Text = "Type"
        '
        'barcodeID
        '
        Me.barcodeID.AutoSize = True
        Me.barcodeID.Location = New System.Drawing.Point(203, 77)
        Me.barcodeID.Name = "barcodeID"
        Me.barcodeID.Size = New System.Drawing.Size(12, 16)
        Me.barcodeID.TabIndex = 435
        Me.barcodeID.Text = "-"
        '
        'txtReOrderPoint
        '
        Me.txtReOrderPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReOrderPoint.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReOrderPoint.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReOrderPoint.Location = New System.Drawing.Point(206, 301)
        Me.txtReOrderPoint.Name = "txtReOrderPoint"
        Me.txtReOrderPoint.Size = New System.Drawing.Size(280, 23)
        Me.txtReOrderPoint.TabIndex = 422
        Me.txtReOrderPoint.Text = "0"
        Me.txtReOrderPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPrice
        '
        Me.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrice.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.Location = New System.Drawing.Point(206, 272)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(280, 23)
        Me.txtPrice.TabIndex = 421
        Me.txtPrice.Text = "0.00"
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOpeningStock
        '
        Me.txtOpeningStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpeningStock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOpeningStock.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpeningStock.Location = New System.Drawing.Point(206, 330)
        Me.txtOpeningStock.Name = "txtOpeningStock"
        Me.txtOpeningStock.Size = New System.Drawing.Size(280, 23)
        Me.txtOpeningStock.TabIndex = 420
        Me.txtOpeningStock.Text = "0"
        Me.txtOpeningStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(75, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 16)
        Me.Label2.TabIndex = 434
        Me.Label2.Text = "Item No."
        '
        'txtSupplyDesc
        '
        Me.txtSupplyDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplyDesc.Location = New System.Drawing.Point(206, 194)
        Me.txtSupplyDesc.Multiline = True
        Me.txtSupplyDesc.Name = "txtSupplyDesc"
        Me.txtSupplyDesc.Size = New System.Drawing.Size(554, 48)
        Me.txtSupplyDesc.TabIndex = 419
        '
        'frmSupplyItemAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(828, 479)
        Me.ControlBox = False
        Me.Controls.Add(Me.BasePanel)
        Me.Controls.Add(Me.systemSign)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSupplyItemAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.systemSign.ResumeLayout(False)
        Me.BasePanel.ResumeLayout(False)
        Me.SearchPanel.ResumeLayout(False)
        Me.SearchBasePanel.ResumeLayout(False)
        Me.dgPanel.ResumeLayout(False)
        CType(Me.dgSupplySize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgSupplyCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        CType(Me.barcodeIMG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents systemSign As Panel
    Friend WithEvents btnClose As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BasePanel As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents barcodeIMG As PictureBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbSupplyStatus As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cbSupplyType As ComboBox
    Friend WithEvents btnSearchSize As Label
    Friend WithEvents btnSearchCategory As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents barcodeID As Label
    Friend WithEvents txtReOrderPoint As TextBox
    Friend WithEvents txtPrice As TextBox
    Friend WithEvents txtOpeningStock As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtSupplyDesc As TextBox
    Friend WithEvents SearchPanel As Panel
    Friend WithEvents SearchBasePanel As Panel
    Friend WithEvents dgPanel As Panel
    Friend WithEvents dgSupplyCategory As DataGridView
    Friend WithEvents dgSupplySize As DataGridView
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents frmTitle As Label
    Friend WithEvents btnAdd As PictureBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Panel10 As Panel
    Friend WithEvents btnSelect As Button
    Friend WithEvents btnCancelSearch As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As DataGridViewTextBoxColumn
    Friend WithEvents Label8 As Label
    Friend WithEvents cbSupplySize As TextBox
    Friend WithEvents cbSupplyCategory As TextBox
End Class
