Public Class frmWait
    Public seconds As Byte
    Dim counter As Integer
    Private Sub frmWait_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' CenterForm(Me)

            Timer1.Interval = seconds * 1000
            Timer2.Interval = 1000
            counter = 1

            Timer1.Enabled = True
            Timer2.Enabled = True

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer2.Enabled = False
        Me.Dispose()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        On Error Resume Next
        counter = counter + 1
    End Sub
End Class