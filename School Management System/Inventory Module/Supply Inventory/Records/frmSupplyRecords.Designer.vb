<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSupplyRecords
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.studentInfoPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.btn_return = New System.Windows.Forms.Button()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblStudentID = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblStudentName = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.cb_aps = New System.Windows.Forms.CheckBox()
        Me.cb_as = New System.Windows.Forms.CheckBox()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.cb_all = New System.Windows.Forms.CheckBox()
        Me.cbRequests = New System.Windows.Forms.ComboBox()
        Me.txtboxlocation = New System.Windows.Forms.ComboBox()
        Me.lbl_status = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dfrom = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dto = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnPrinterSetup = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.dgprintall = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Label()
        Me.CrystalReportViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.dgdeployrecords = New System.Windows.Forms.DataGridView()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblReportRequestID = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.studentInfoPanel.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        CType(Me.dgprintall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgdeployrecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(235, 739)
        Me.Panel1.TabIndex = 460
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.studentInfoPanel)
        Me.Panel6.Controls.Add(Me.btn_return)
        Me.Panel6.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel6.Controls.Add(Me.Panel9)
        Me.Panel6.Controls.Add(Me.cb_all)
        Me.Panel6.Controls.Add(Me.cbRequests)
        Me.Panel6.Controls.Add(Me.txtboxlocation)
        Me.Panel6.Controls.Add(Me.lbl_status)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Controls.Add(Me.Label1)
        Me.Panel6.Controls.Add(Me.dfrom)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Controls.Add(Me.Label17)
        Me.Panel6.Controls.Add(Me.dto)
        Me.Panel6.Controls.Add(Me.Label18)
        Me.Panel6.Controls.Add(Me.btnPrinterSetup)
        Me.Panel6.Controls.Add(Me.btnPrint)
        Me.Panel6.Controls.Add(Me.btnPreview)
        Me.Panel6.Controls.Add(Me.btnGenerate)
        Me.Panel6.Controls.Add(Me.dtFrom)
        Me.Panel6.Controls.Add(Me.dtTo)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(235, 737)
        Me.Panel6.TabIndex = 433
        '
        'studentInfoPanel
        '
        Me.studentInfoPanel.Controls.Add(Me.Panel13)
        Me.studentInfoPanel.Controls.Add(Me.Label6)
        Me.studentInfoPanel.Controls.Add(Me.lblStudentID)
        Me.studentInfoPanel.Controls.Add(Me.Panel12)
        Me.studentInfoPanel.Controls.Add(Me.Label9)
        Me.studentInfoPanel.Controls.Add(Me.lblStudentName)
        Me.studentInfoPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.studentInfoPanel.Location = New System.Drawing.Point(10, 248)
        Me.studentInfoPanel.Name = "studentInfoPanel"
        Me.studentInfoPanel.Size = New System.Drawing.Size(218, 116)
        Me.studentInfoPanel.TabIndex = 474
        '
        'btn_return
        '
        Me.btn_return.BackColor = System.Drawing.Color.White
        Me.btn_return.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_return.ForeColor = System.Drawing.Color.Black
        Me.btn_return.Location = New System.Drawing.Point(13, 214)
        Me.btn_return.Name = "btn_return"
        Me.btn_return.Size = New System.Drawing.Size(212, 29)
        Me.btn_return.TabIndex = 432
        Me.btn_return.Text = "RETURN"
        Me.btn_return.UseVisualStyleBackColor = True
        Me.btn_return.Visible = False
        '
        'Panel13
        '
        Me.Panel13.Location = New System.Drawing.Point(3, 3)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(19, 1)
        Me.Panel13.TabIndex = 433
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(3, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Student ID:"
        '
        'lblStudentID
        '
        Me.lblStudentID.AutoSize = True
        Me.lblStudentID.BackColor = System.Drawing.Color.White
        Me.lblStudentID.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStudentID.ForeColor = System.Drawing.Color.Black
        Me.lblStudentID.Location = New System.Drawing.Point(3, 23)
        Me.lblStudentID.Name = "lblStudentID"
        Me.lblStudentID.Size = New System.Drawing.Size(12, 16)
        Me.lblStudentID.TabIndex = 5
        Me.lblStudentID.Text = "-"
        '
        'Panel12
        '
        Me.Panel12.Location = New System.Drawing.Point(3, 42)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(19, 1)
        Me.Panel12.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(3, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 16)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Name:"
        '
        'lblStudentName
        '
        Me.lblStudentName.BackColor = System.Drawing.Color.White
        Me.lblStudentName.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStudentName.ForeColor = System.Drawing.Color.Black
        Me.lblStudentName.Location = New System.Drawing.Point(3, 62)
        Me.lblStudentName.Name = "lblStudentName"
        Me.lblStudentName.Size = New System.Drawing.Size(211, 46)
        Me.lblStudentName.TabIndex = 8
        Me.lblStudentName.Text = "-"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.Controls.Add(Me.Panel10)
        Me.FlowLayoutPanel1.Controls.Add(Me.Panel11)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 408)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(229, 131)
        Me.FlowLayoutPanel1.TabIndex = 467
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.cb_aps)
        Me.Panel10.Controls.Add(Me.cb_as)
        Me.Panel10.Location = New System.Drawing.Point(3, 85)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(200, 43)
        Me.Panel10.TabIndex = 467
        '
        'cb_aps
        '
        Me.cb_aps.AutoSize = True
        Me.cb_aps.BackColor = System.Drawing.Color.White
        Me.cb_aps.ForeColor = System.Drawing.Color.Black
        Me.cb_aps.Location = New System.Drawing.Point(8, 21)
        Me.cb_aps.Name = "cb_aps"
        Me.cb_aps.Size = New System.Drawing.Size(155, 20)
        Me.cb_aps.TabIndex = 472
        Me.cb_aps.Text = "Generate All (All Offices)"
        Me.cb_aps.UseVisualStyleBackColor = False
        Me.cb_aps.Visible = False
        '
        'cb_as
        '
        Me.cb_as.AutoSize = True
        Me.cb_as.BackColor = System.Drawing.Color.White
        Me.cb_as.ForeColor = System.Drawing.Color.Black
        Me.cb_as.Location = New System.Drawing.Point(8, 2)
        Me.cb_as.Name = "cb_as"
        Me.cb_as.Size = New System.Drawing.Size(188, 20)
        Me.cb_as.TabIndex = 472
        Me.cb_as.Text = "Generate All (Selected Office)"
        Me.cb_as.UseVisualStyleBackColor = False
        Me.cb_as.Visible = False
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.ComboBox1)
        Me.Panel11.Controls.Add(Me.Label5)
        Me.Panel11.Location = New System.Drawing.Point(3, 19)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(221, 60)
        Me.Panel11.TabIndex = 467
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Both College - High School", "College", "High School"})
        Me.ComboBox1.Location = New System.Drawing.Point(8, 32)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(209, 24)
        Me.ComboBox1.TabIndex = 471
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(5, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(135, 16)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Generate Summary For:"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Black
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel9.Location = New System.Drawing.Point(234, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1, 737)
        Me.Panel9.TabIndex = 473
        '
        'cb_all
        '
        Me.cb_all.AutoSize = True
        Me.cb_all.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_all.ForeColor = System.Drawing.Color.Black
        Me.cb_all.Location = New System.Drawing.Point(192, 4)
        Me.cb_all.Name = "cb_all"
        Me.cb_all.Size = New System.Drawing.Size(38, 20)
        Me.cb_all.TabIndex = 0
        Me.cb_all.Text = "All"
        Me.cb_all.UseVisualStyleBackColor = True
        Me.cb_all.Visible = False
        '
        'cbRequests
        '
        Me.cbRequests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRequests.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRequests.FormattingEnabled = True
        Me.cbRequests.Location = New System.Drawing.Point(13, 168)
        Me.cbRequests.Name = "cbRequests"
        Me.cbRequests.Size = New System.Drawing.Size(209, 24)
        Me.cbRequests.TabIndex = 471
        '
        'txtboxlocation
        '
        Me.txtboxlocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtboxlocation.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtboxlocation.FormattingEnabled = True
        Me.txtboxlocation.Location = New System.Drawing.Point(14, 24)
        Me.txtboxlocation.Name = "txtboxlocation"
        Me.txtboxlocation.Size = New System.Drawing.Size(209, 24)
        Me.txtboxlocation.TabIndex = 471
        '
        'lbl_status
        '
        Me.lbl_status.AutoSize = True
        Me.lbl_status.BackColor = System.Drawing.Color.White
        Me.lbl_status.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_status.ForeColor = System.Drawing.Color.Black
        Me.lbl_status.Location = New System.Drawing.Point(59, 195)
        Me.lbl_status.Name = "lbl_status"
        Me.lbl_status.Size = New System.Drawing.Size(0, 16)
        Me.lbl_status.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(11, 195)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Status:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Request ID"
        '
        'dfrom
        '
        Me.dfrom.AutoSize = True
        Me.dfrom.ForeColor = System.Drawing.Color.Black
        Me.dfrom.Location = New System.Drawing.Point(167, 58)
        Me.dfrom.Name = "dfrom"
        Me.dfrom.Size = New System.Drawing.Size(63, 16)
        Me.dfrom.TabIndex = 6
        Me.dfrom.Text = "Date From"
        Me.dfrom.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(11, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Location/Office"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(11, 55)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 16)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Date From"
        '
        'dto
        '
        Me.dto.AutoSize = True
        Me.dto.ForeColor = System.Drawing.Color.Black
        Me.dto.Location = New System.Drawing.Point(177, 104)
        Me.dto.Name = "dto"
        Me.dto.Size = New System.Drawing.Size(50, 16)
        Me.dto.TabIndex = 5
        Me.dto.Text = "Date To"
        Me.dto.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.White
        Me.Label18.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(11, 101)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 16)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Date To"
        '
        'btnPrinterSetup
        '
        Me.btnPrinterSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrinterSetup.BackColor = System.Drawing.Color.White
        Me.btnPrinterSetup.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrinterSetup.ForeColor = System.Drawing.Color.Black
        Me.btnPrinterSetup.Location = New System.Drawing.Point(12, 688)
        Me.btnPrinterSetup.Name = "btnPrinterSetup"
        Me.btnPrinterSetup.Size = New System.Drawing.Size(210, 42)
        Me.btnPrinterSetup.TabIndex = 432
        Me.btnPrinterSetup.Text = "PRINTER SETUP"
        Me.btnPrinterSetup.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.Color.White
        Me.btnPrint.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.Black
        Me.btnPrint.Location = New System.Drawing.Point(12, 640)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(210, 42)
        Me.btnPrint.TabIndex = 432
        Me.btnPrint.Text = "PRINT"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.White
        Me.btnPreview.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.Black
        Me.btnPreview.Location = New System.Drawing.Point(12, 592)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(210, 42)
        Me.btnPreview.TabIndex = 432
        Me.btnPreview.Text = "PRINT PREVIEW"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnGenerate
        '
        Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenerate.BackColor = System.Drawing.Color.White
        Me.btnGenerate.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.ForeColor = System.Drawing.Color.Black
        Me.btnGenerate.Location = New System.Drawing.Point(12, 544)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(210, 42)
        Me.btnGenerate.TabIndex = 432
        Me.btnGenerate.Text = "GENERATE"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'dtFrom
        '
        Me.dtFrom.CustomFormat = "yyyy/MM/dd"
        Me.dtFrom.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(13, 74)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(210, 21)
        Me.dtFrom.TabIndex = 2
        '
        'dtTo
        '
        Me.dtTo.CustomFormat = "yyyy/MM/dd"
        Me.dtTo.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(13, 120)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(210, 21)
        Me.dtTo.TabIndex = 2
        '
        'dgprintall
        '
        Me.dgprintall.AllowUserToAddRows = False
        Me.dgprintall.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgprintall.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgprintall.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgprintall.BackgroundColor = System.Drawing.Color.White
        Me.dgprintall.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgprintall.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgprintall.ColumnHeadersHeight = 30
        Me.dgprintall.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgprintall.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgprintall.EnableHeadersVisualStyles = False
        Me.dgprintall.GridColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.dgprintall.Location = New System.Drawing.Point(243, 642)
        Me.dgprintall.MultiSelect = False
        Me.dgprintall.Name = "dgprintall"
        Me.dgprintall.ReadOnly = True
        Me.dgprintall.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgprintall.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgprintall.RowHeadersWidth = 25
        Me.dgprintall.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        Me.dgprintall.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgprintall.RowTemplate.Height = 20
        Me.dgprintall.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgprintall.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgprintall.Size = New System.Drawing.Size(25, 26)
        Me.dgprintall.TabIndex = 464
        Me.dgprintall.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Controls.Add(Me.dgprintall)
        Me.Panel4.Controls.Add(Me.Panel1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1166, 739)
        Me.Panel4.TabIndex = 464
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(235, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(931, 739)
        Me.Panel2.TabIndex = 469
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnClose)
        Me.Panel5.Controls.Add(Me.CrystalReportViewer)
        Me.Panel5.Controls.Add(Me.dgdeployrecords)
        Me.Panel5.Controls.Add(Me.Panel8)
        Me.Panel5.Controls.Add(Me.Panel3)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(10, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(921, 739)
        Me.Panel5.TabIndex = 471
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.AutoSize = True
        Me.btnClose.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Font = New System.Drawing.Font("Corbel", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(895, 69)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(19, 19)
        Me.btnClose.TabIndex = 471
        Me.btnClose.Text = "✕"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Visible = False
        '
        'CrystalReportViewer
        '
        Me.CrystalReportViewer.ActiveViewIndex = -1
        Me.CrystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer.Location = New System.Drawing.Point(0, 65)
        Me.CrystalReportViewer.Name = "CrystalReportViewer"
        Me.CrystalReportViewer.Size = New System.Drawing.Size(921, 674)
        Me.CrystalReportViewer.TabIndex = 470
        Me.CrystalReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.CrystalReportViewer.Visible = False
        '
        'dgdeployrecords
        '
        Me.dgdeployrecords.AllowUserToAddRows = False
        Me.dgdeployrecords.AllowUserToDeleteRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        Me.dgdeployrecords.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgdeployrecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgdeployrecords.BackgroundColor = System.Drawing.Color.White
        Me.dgdeployrecords.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgdeployrecords.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgdeployrecords.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgdeployrecords.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgdeployrecords.ColumnHeadersHeight = 40
        Me.dgdeployrecords.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dgdeployrecords.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgdeployrecords.EnableHeadersVisualStyles = False
        Me.dgdeployrecords.GridColor = System.Drawing.Color.DimGray
        Me.dgdeployrecords.Location = New System.Drawing.Point(0, 65)
        Me.dgdeployrecords.MultiSelect = False
        Me.dgdeployrecords.Name = "dgdeployrecords"
        Me.dgdeployrecords.ReadOnly = True
        Me.dgdeployrecords.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Gray
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgdeployrecords.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgdeployrecords.RowHeadersVisible = False
        Me.dgdeployrecords.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.dgdeployrecords.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.dgdeployrecords.RowTemplate.Height = 26
        Me.dgdeployrecords.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgdeployrecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgdeployrecords.Size = New System.Drawing.Size(921, 674)
        Me.dgdeployrecords.TabIndex = 466
        '
        'Panel8
        '
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 55)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(921, 10)
        Me.Panel8.TabIndex = 465
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(921, 55)
        Me.Panel3.TabIndex = 464
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Controls.Add(Me.lblReportRequestID)
        Me.Panel7.Controls.Add(Me.Label4)
        Me.Panel7.Controls.Add(Me.lblTotal)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(921, 50)
        Me.Panel7.TabIndex = 434
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(268, 16)
        Me.Label7.TabIndex = 485
        Me.Label7.Text = "REQUEST ID"
        '
        'lblReportRequestID
        '
        Me.lblReportRequestID.BackColor = System.Drawing.Color.White
        Me.lblReportRequestID.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblReportRequestID.Font = New System.Drawing.Font("Century Gothic", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportRequestID.ForeColor = System.Drawing.Color.Black
        Me.lblReportRequestID.Location = New System.Drawing.Point(0, 0)
        Me.lblReportRequestID.Name = "lblReportRequestID"
        Me.lblReportRequestID.Size = New System.Drawing.Size(444, 50)
        Me.lblReportRequestID.TabIndex = 484
        Me.lblReportRequestID.Text = "-"
        Me.lblReportRequestID.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(654, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(268, 16)
        Me.Label4.TabIndex = 483
        Me.Label4.Text = "TOTAL                                                                            " &
    "                                                                  "
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotal.Font = New System.Drawing.Font("Century Gothic", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Black
        Me.lblTotal.Location = New System.Drawing.Point(0, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(921, 50)
        Me.lblTotal.TabIndex = 482
        Me.lblTotal.Text = "0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'frmSupplyRecords
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1166, 739)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSupplyRecords"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.studentInfoPanel.ResumeLayout(False)
        Me.studentInfoPanel.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        CType(Me.dgprintall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgdeployrecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents cb_as As CheckBox
    Friend WithEvents cb_aps As CheckBox
    Friend WithEvents cb_all As CheckBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents cbRequests As ComboBox
    Friend WithEvents txtboxlocation As ComboBox
    Friend WithEvents lbl_status As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dfrom As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents dto As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents btnGenerate As Button
    Friend WithEvents dtFrom As DateTimePicker
    Friend WithEvents dtTo As DateTimePicker
    Friend WithEvents dgprintall As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents dgdeployrecords As DataGridView
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblReportRequestID As Label
    Friend WithEvents btnPrinterSetup As Button
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel11 As Panel
    Public WithEvents CrystalReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btnClose As Label
    Friend WithEvents studentInfoPanel As FlowLayoutPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents lblStudentID As Label
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents lblStudentName As Label
    Friend WithEvents btn_return As Button
    Friend WithEvents Panel13 As Panel
End Class
