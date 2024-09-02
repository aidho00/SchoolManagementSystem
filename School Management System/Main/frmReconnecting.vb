Imports MySql.Data.MySqlClient

Class frmReconnecting
    Private Sub frmReconnecting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Do
            Try
                cn.Close()
                cn.Open()
                e.Result = True
                Exit Do
            Catch ex As Exception
                Threading.Thread.Sleep(10000)
            End Try
        Loop
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If e.Result = True Then
            MsgBox("Connection to the database has been restored!", vbInformation)
            frmMain.Timer1.Start()
            Me.Close()
        End If
    End Sub
End Class