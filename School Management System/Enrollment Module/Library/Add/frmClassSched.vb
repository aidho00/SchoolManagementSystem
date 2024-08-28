Public Class frmClassSched

    Dim slidePanels As New List(Of Panel)()

    Public Shared currentSlideIndex As Integer = 1

    Public Shared ClassID As Integer = 0
    Public Shared ClassSubjectID As Integer = 0
    Public Shared ClassSectionID As Integer = 0
    Public Shared ClassInstructorID As Integer = 0
    Public Shared ClassRoomID As Integer = 0
    Public Shared ClassDaySchedID As Integer = 0
    Public Shared ClassAcadID As Integer = 0
    Public Shared ClassCurID As Integer = 0

    Public Shared ClassPreviousSubjectID As Integer = 0

    Dim classLoadStatus As Double = 0



#Region "Drag Form"

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles systemSign.MouseDown, Panel1.MouseDown  ' Add more handles here (Example: PictureBox1.MouseDown)
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.Default
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles systemSign.MouseMove, Panel1.MouseMove  ' Add more handles here (Example: PictureBox1.MouseMove)
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles systemSign.MouseUp, Panel1.MouseUp   ' Add more handles here (Example: PictureBox1.MouseUp)
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

#End Region
    Private Sub toNormalForm()
        SearchPanel.Visible = False
        Panel8.Visible = True
        Me.Size = New Size(830, 559)
        dgSubjectList.Rows.Clear()
        dgAcadList.Rows.Clear()
        dgEmployeeList.Rows.Clear()
        dgDaySchedList.Rows.Clear()
        dgCurList.Rows.Clear()
        dgRoomList.Rows.Clear()
        dgSectionList.Rows.Clear()
        txtSearch.Text = String.Empty
        centerForm(Me)
    End Sub

    Private Sub frmClassSched_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon(Me)
        ApplyHoverEffectToControls(Me)
        slidePanels.Add(slide1)
        slidePanels.Add(slide2)
        slidePanels.Add(slide3)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If SearchPanel.Visible = True Then
            toNormalForm()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel?", vbYesNo + vbQuestion) = vbYes Then
            If btnSave.Visible = True Then
                ResetControls(Me)
            ElseIf btnUpdate.Visible = True Then
                Me.Close()
            End If
        Else
        End If
    End Sub

    Private Sub frmClassSched_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ResetControls(Me)

        ClassID = 0
        ClassSubjectID = 0
        ClassSectionID = 0
        ClassInstructorID = 0
        ClassRoomID = 0
        ClassDaySchedID = 0
        ClassAcadID = 0
        ClassCurID = 0
        ClassPreviousSubjectID = 0

        classLoadStatus = 0

        currentSlideIndex = 1
    End Sub


    Private Sub cbCur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCur.SelectedIndexChanged
        Dim curID As Integer = 0
        Try
            curID = CInt(cbCur.SelectedValue)
        Catch ex As Exception
            curID = 0
        End Try
        fillCombo("select Subject, Subject_ID from subjectspercurriculum where curr_ID = " & curID & "", cbSubject, "subjectspercurriculum", "Subject", "Subject_ID")
    End Sub

    Private Sub CbPetition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbPetition.SelectedIndexChanged
        If CbPetition.Text = "Yes" Then
            txtAmount.Enabled = True
        Else
            txtAmount.Enabled = False
            txtAmount.Text = "0.00"
        End If
    End Sub

    Private Sub cbLoadStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLoadStatus.SelectedIndexChanged
        If cbLoadStatus.Text = "Normal" Then
            classLoadStatus = 0
        ElseIf cbLoadStatus.Text = "Overload" Then
            classLoadStatus = 1
        ElseIf cbLoadStatus.Text = "Half Overload" Then
            classLoadStatus = 0.5
        ElseIf cbLoadStatus.Text = "Honorarium" Then
            classLoadStatus = 0.2
        End If
    End Sub

    Private Sub btnSearchSection_Click(sender As Object, e As EventArgs) Handles btnSearchSection.Click
        frmTitle.Text = "Search Section"
        SearchPanel.Visible = True
        Panel8.Visible = False
        LibraryClassSectionList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSectionList)
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ClassScheduleSearch()
    End Sub

    Private Sub btnSearchCur_Click(sender As Object, e As EventArgs) Handles btnSearchCur.Click
        frmTitle.Text = "Search Curriculum"
        SearchPanel.Visible = True
        Panel8.Visible = False
        LibraryClassCurList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgCurList)
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub btnSearchSubject_Click(sender As Object, e As EventArgs) Handles btnSearchSubject.Click
        frmTitle.Text = "Search Subject"
        SearchPanel.Visible = True
        Panel8.Visible = False
        LibraryClassSubjectList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgSubjectList)
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub btnSearchRoom_Click(sender As Object, e As EventArgs) Handles btnSearchRoom.Click
        frmTitle.Text = "Search Room"
        SearchPanel.Visible = True
        Panel8.Visible = False
        LibraryClassRoomList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgRoomList)
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub btnSearchDaySched_Click(sender As Object, e As EventArgs) Handles btnSearchDaySched.Click
        frmTitle.Text = "Search Day Schedule"
        SearchPanel.Visible = True
        Panel8.Visible = False
        LibraryClassDaySchedList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgDaySchedList)
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub btnSearchInstructor_Click(sender As Object, e As EventArgs) Handles btnSearchInstructor.Click
        frmTitle.Text = "Search Instructor"
        SearchPanel.Visible = True
        Panel8.Visible = False
        LibraryClassEmployeeList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgEmployeeList)
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub btnSearchAcad_Click(sender As Object, e As EventArgs) Handles btnSearchAcad.Click
        frmTitle.Text = "Search Academic Year"
        SearchPanel.Visible = True
        Panel8.Visible = False
        LibraryClassAcadList()
        HideAllDatagridViewInPanelExcept(dgPanel, dgAcadList)
        Me.Size = New Size(1038, 682)
        centerForm(Me)
    End Sub

    Private Sub cbCur_TextChanged(sender As Object, e As EventArgs) Handles cbCur.TextChanged
        If cbCur.Text = String.Empty Then
            cbSubject.Text = String.Empty
            cbSubject.DataSource = Nothing
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Select Case frmTitle.Text
            Case "Search Section"
                ClassSectionID = dgSectionList.CurrentRow.Cells(1).Value
                cbSection.Text = dgSectionList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Curriculum"
                ClassCurID = dgCurList.CurrentRow.Cells(1).Value
                cbCur.Text = dgCurList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Subject"
                ClassSubjectID = dgSubjectList.CurrentRow.Cells(1).Value
                cbSubject.Text = dgSubjectList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Day Schedule"
                ClassDaySchedID = dgDaySchedList.CurrentRow.Cells(1).Value
                cbDaySched.Text = dgDaySchedList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Instructor"
                ClassInstructorID = dgEmployeeList.CurrentRow.Cells(1).Value
                cbInstructor.Text = dgEmployeeList.CurrentRow.Cells(3).Value & ", " & dgEmployeeList.CurrentRow.Cells(4).Value & " " & dgEmployeeList.CurrentRow.Cells(5).Value
                toNormalForm()
            Case "Search Room"
                ClassRoomID = dgRoomList.CurrentRow.Cells(1).Value
                cbRoom.Text = dgRoomList.CurrentRow.Cells(2).Value
                toNormalForm()
            Case "Search Academic Year"
                ClassAcadID = dgAcadList.CurrentRow.Cells(1).Value
                cbAcademicYear.Text = dgAcadList.CurrentRow.Cells(2).Value
                toNormalForm()
        End Select

    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ClassAcadID = 0 Or cbAcademicYear.SelectedValue Is Nothing Then
                MsgBox("Warning: Invalid Academic Year.", vbExclamation)
                Return
            End If
            If ClassSubjectID = 0 Or cbSubject.SelectedValue Is Nothing Then
                MsgBox("Warning: Invalid Subject.", vbExclamation)
                Return
            End If
            If ClassDaySchedID = 0 Or cbDaySched.SelectedValue Is Nothing Then
                MsgBox("Warning: Invalid Day Schedule.", vbExclamation)
                Return
            End If
            If ClassSectionID = 0 Or cbSection.SelectedValue Is Nothing Then
                MsgBox("Warning: Invalid Section.", vbExclamation)
                Return
            End If
            If IS_EMPTY(txtPopulation) = True Then Return
            If IS_EMPTY(txtAmount) = True Then Return
            If CHECK_EXISTING("SELECT * FROM tbl_class_schedule WHERE cssubject_id = " & ClassSubjectID & " and class_block_id = " & ClassSectionID & " and csperiod_id = " & ClassAcadID & "") Then Return
            If MsgBox("Are you sure you want to save this record?", vbYesNo + vbQuestion) = vbYes Then
                AddSubjectSchedule()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'Try
        If ClassAcadID = 0 Then
            MsgBox("Warning: Invalid Academic Year.", vbExclamation)
            Return
        End If
        If ClassSubjectID = 0 Then
            MsgBox("Warning: Invalid Subject.", vbExclamation)
            Return
        End If
        If ClassDaySchedID = 0 Then
            MsgBox("Warning: Invalid Day Schedule.", vbExclamation)
            Return
        End If
        If ClassSectionID = 0 Then
            MsgBox("Warning: Invalid Section.", vbExclamation)
            Return
        End If
        If IS_EMPTY(txtPopulation) = True Then Return
        If IS_EMPTY(txtAmount) = True Then Return
        If CHECK_EXISTING("SELECT * FROM tbl_class_schedule WHERE cssubject_id = " & ClassSubjectID & " and class_block_id = " & ClassSectionID & " and csperiod_id = " & ClassAcadID & " and class_schedule_id NOT IN (" & ClassID & ")") Then Return
        If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
            UpdateSubjectSchedule()
        End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, vbCritical, "")
        'End Try
    End Sub

    Private Sub btnOverrideUpdate_Click(sender As Object, e As EventArgs) Handles btnOverrideUpdate.Click
        Try
            If ClassAcadID = 0 Or cbAcademicYear.SelectedValue = Nothing Then
                MsgBox("Warning: Invalid Academic Year.", vbExclamation)
                Return
            End If
            If ClassSubjectID = 0 Or cbSubject.SelectedValue = Nothing Then
                MsgBox("Warning: Invalid Subject.", vbExclamation)
                Return
            End If
            If ClassDaySchedID = 0 Or cbDaySched.SelectedValue = Nothing Then
                MsgBox("Warning: Invalid Day Schedule.", vbExclamation)
                Return
            End If
            If ClassSectionID = 0 Or cbSection.SelectedValue = Nothing Then
                MsgBox("Warning: Invalid Section.", vbExclamation)
                Return
            End If
            If IS_EMPTY(txtPopulation) = True Then Return
            If IS_EMPTY(txtAmount) = True Then Return
            'If CHECK_EXISTING("SELECT * FROM tbl_class_schedule WHERE cssubject_id = " & ClassSubjectID & " and class_block_id = " & ClassSectionID & " and csperiod_id = " & ClassAcadID & "") Then Return
            If MsgBox("Are you sure you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                UpdateClassSchedule()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "")
        End Try
    End Sub
End Class