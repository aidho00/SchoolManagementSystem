Imports MySql.Data.MySqlClient

Public Class frmLogin

#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp   ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region


    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
        appVersion(applicationVersion)
        Me.KeyPreview = True
        txtUsername.Text = My.Settings.Username
        txtHost.Text = My.Settings.Host
        txtUser.Text = My.Settings.User
        txtPass.Text = My.Settings.Pass

        systemdbhost = txtHost.Text
        systemdbuser = txtUser.Text
        systemdbpass = txtPass.Text

        'CheckConnection()
        cmbAcad.SelectedIndex = 0
    End Sub
    Private Sub CheckConnection()
        Try
            CloseConnection()
            CloseConnection2()
            Connection1()
            Connection2()
            lblConnection.Text = "System Connected!"
            lblConnection.ForeColor = Color.Green
            MsgBox("Database Connection Success!", vbInformation)

            My.Settings.Host = txtHost.Text
            My.Settings.User = txtUser.Text
            My.Settings.Pass = txtPass.Text

            CloseConnection()
            CloseConnection2()
            btnLogin.Enabled = True
            PanelLogo.Visible = True
        Catch ex As Exception
            CloseConnection()
            CloseConnection2()
            lblConnection.Text = "System Disonnected!"
            lblConnection.ForeColor = Color.Red
            btnLogin.Enabled = False
            MsgBox("System Connection Failed!", vbCritical)
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim found As Boolean
            If IS_EMPTY(txtUsername) = True Then Return
            If IS_EMPTY(txtPassword) = True Then Return
            cn.Close()
            cn.Open()
            cm = New MySqlCommand("select * from tbl_user_account where ua_user_name = @1 and ua_password = @2", cn)
            With cm
                .Parameters.AddWithValue("@1", txtUsername.Text)
                .Parameters.AddWithValue("@2", txtPassword.Text)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                found = True
                str_userid = dr.Item("ua_id").ToString
                str_user = dr.Item("ua_user_name").ToString
                str_name = dr.Item("ua_first_name").ToString & " " & dr.Item("ua_middle_name").ToString & " " & dr.Item("ua_last_name").ToString
                str_role = dr.Item("ua_account_type").ToString
            Else
                found = False
            End If
            dr.Close()
            cn.Close()
            If found = True Then

                If str_role = "Administrator" Or str_role = "Registrar" Then

                    If cmbAcad.Text = "College Management System" Then
                        frmMain.systemModule.Text = "College Module"
                        frmMain.btnGrading.Visible = True
                        frmMain.btnTOR.Visible = True
                        frmMain.btnTOR2.Visible = True
                        frmMain.btnOTR.Visible = True
                        frmMain.btnForm9.Visible = True
                        frmMain.btnEnrollList.Visible = True
                        frmMain.btnPromoList.Visible = True
                        frmMain.btnNSTP.Visible = True
                        frmMain.btnCredentials.Visible = True
                        frmMain.btnMonitoring.Visible = True
                        frmMain.btnOthers.Visible = True

                        frmMain.btnRegistrar.Visible = True

                        frmMain.btnCourseList.Text = "Course"
                    ElseIf cmbAcad.Text = "Basic Education Management System" Then
                        frmMain.systemModule.Text = "Basic Education Module"
                        frmMain.btnGrading.Visible = False
                        frmMain.btnTOR.Visible = False
                        frmMain.btnTOR2.Visible = False
                        frmMain.btnOTR.Visible = False
                        frmMain.btnForm9.Visible = False
                        frmMain.btnEnrollList.Visible = False
                        frmMain.btnPromoList.Visible = False
                        frmMain.btnNSTP.Visible = False
                        frmMain.btnCredentials.Visible = False
                        frmMain.btnMonitoring.Visible = False
                        frmMain.btnOthers.Visible = False

                        frmMain.btnRegistrar.Visible = False

                        frmMain.btnCourseList.Text = "Strand"
                    End If

                    UserActivity("Logged-in.", "LOGIN")
                    My.Settings.Username = txtUsername.Text
                    txtPassword.Text = ""

                    frmWait.seconds = 1
                    frmWait.ShowDialog()
                    frmMain.Show()
                    MsgBox("Welcome " & str_name & "!", vbInformation)
                    Me.Hide()
                Else
                    MsgBox("Access denied! Only Administrator or Registrar accounts are authorized.", vbExclamation)
                End If

            Else
                MsgBox("Invalid username or password!", vbExclamation)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox("System Failed. " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to exit?", vbYesNo + vbQuestion) = vbYes Then
            MsgBox("System Exit!", vbExclamation)
            Application.Exit()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub frmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(sender, e)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = True
    End Sub

    Private Sub btnModule_MouseDown(sender As Object, e As MouseEventArgs) Handles btnModule.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim position As Point = Control.MousePosition
            ModuleMenu.Show(position)
        End If
    End Sub

    Private Sub btnCheckConnection_Click(sender As Object, e As EventArgs) Handles btnCheckConnection.Click
        CheckConnection()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbAcad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAcad.SelectedIndexChanged
        cbModule.Text = cmbAcad.Text
        If cbModule.Text = "College Management System" Then
            SystemDataBase.Text = "cfcissmsdb"
        ElseIf cbModule.Text = "Basic Education Management System" Then
            SystemDataBase.Text = "cfcissmsdbhighschool"
        End If
        CheckConnection()
        txtUsername.Focus()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        PanelLogo.Visible = True
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        systemdbhost = txtHost.Text
        systemdbuser = txtUser.Text
        systemdbpass = txtPass.Text
        CheckConnection()
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        If txtPassword.Text = "systemconnection" Then
            PanelLogo.Visible = False
            txtPassword.Text = String.Empty
            txtHost.Focus()
        End If
    End Sub
End Class