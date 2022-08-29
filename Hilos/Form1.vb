Imports System.Threading
Imports System.IO.StringWriter
Imports System.IO.StreamWriter
Imports System.ComponentModel
Public Class Form1
    Inherits System.Windows.Forms.Form
    Private WithEvents BackGroundWorker1 As New BackgroundWorker
    Private Sub Btn_Iniciar_Click(sender As Object, e As EventArgs) Handles Btn_Iniciar.Click
        Btn_Iniciar.Enabled = False

        BackGroundWorker1.WorkerReportsProgress = True
        BackGroundWorker1.WorkerSupportsCancellation = True
        BackGroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackGroundWorker1.DoWork
        For I = 1 To CInt(Txt_valor.Text)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Lineas.TXT", "Línea N° " & I & vbCrLf, True)
            If BackGroundWorker1.CancellationPending Then
                Exit Sub
            End If
            BackGroundWorker1.ReportProgress(CInt(100 * I / CInt(Txt_valor.Text)))
        Next
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackGroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label2.Text = ProgressBar1.Value & " %"
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackGroundWorker1.RunWorkerCompleted
        BackGroundWorker1.Dispose()
    End Sub

    Private Sub Btn_Cancelar_Click(sender As Object, e As EventArgs) Handles Btn_Cancelar.Click
        BackGroundWorker1.CancelAsync()
        Btn_Iniciar.Enabled = True
        Label2.Text = "0%"
        TXTBOX.Text = ""
        ProgressBar1.Value = 0
    End Sub
End Class
