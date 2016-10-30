Imports System.Windows.Forms

Public Class dlgHarmonogram
  Public Event NewAdded(ByVal InsertedID As String)
  Public IsNewMode As Boolean
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()

    If dtStartTime.Value > dtEndTime.Value Then
      MessageBox.Show("Wartość początkowa nie może być większa od wartości końcowej!")
      Exit Sub
    End If
    If IsNewMode Then
      If AddNew() Then
        dtStartTime.Value = dtEndTime.Value
        dtEndTime.Value = dtEndTime.Value.AddMinutes(45)
        dtStartTime.Focus()
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
  Private Function AddNew() As Boolean
    AddNew = False
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, H As New HarmonogramSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(H.InsertActivityTime)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?NrLekcji", nudNrLekcji.Value)
      cmd.Parameters.AddWithValue("?StartTime", dtStartTime.Value.ToShortTimeString)
      cmd.Parameters.AddWithValue("?EndTime", dtEndTime.Value.ToShortTimeString)
      cmd.Parameters.AddWithValue("IdSzkola", My.Settings.IdSchool)
      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()
      RaiseEvent NewAdded(cmd.LastInsertedId.ToString)
      Return True
      'dtStartTime.Focus()

    Catch myex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(myex.Message & vbCr & myex.Number)
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      'Return False
    End Try
    'Return False
  End Function
  Private Sub dlgHarmonogram_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'If IsNewMode Then AddHandler SharedHarmonogram.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub dtEndTime_ValueChanged(sender As Object, e As EventArgs) Handles dtEndTime.ValueChanged, dtStartTime.ValueChanged
    OK_Button.Enabled = True
  End Sub
End Class
