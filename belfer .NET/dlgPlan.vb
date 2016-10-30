Imports System.Windows.Forms

Public Class dlgPlan
  Public IsNewMode As Boolean
  Public Event NewAdded(ByVal InsertedID As String)

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If dtpStartDate.Value > dtpEndDate.Value Then
      MessageBox.Show("Data początkowa nie może być większa od daty końcowej.")
      Exit Sub
    ElseIf IsNewMode AndAlso IsDuplicate(dtpStartDate.Value) Then
      MessageBox.Show("W bazie danych istnieje plan, którego data obowiązywania wykracza poza datę początkową tego planu." & vbNewLine & "Ustaw datę początkową większą niż data końcowa istniejącego planu lub ogranicz czas obowiązywania istniejącego planu.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
      Exit Sub
    End If
    If IsNewMode Then
      If AddNew() Then
        txtPlan.Text = ""
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
    Dim DBA As New DataBaseAction, P As New PlanSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertPlan)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Nazwa", txtPlan.Text.Trim)
      cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
      cmd.Parameters.AddWithValue("?StartDate", dtpStartDate.Value.ToShortDateString)
      cmd.Parameters.AddWithValue("?EndDate", dtpEndDate.Value.ToShortDateString)
      cmd.Parameters.AddWithValue("?Lock", chkLock.CheckState)
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
  Private Function IsDuplicate(StartDate As Date) As Boolean
    Dim DBA As New DataBaseAction, P As New PlanSQL
    If DBA.ComputeRecords(P.CountPlan(StartDate, My.Settings.IdSchool)) > 0 Then Return True
    Return False
  End Function

  Private Sub txtPlan_TextChanged(sender As Object, e As EventArgs) Handles txtPlan.TextChanged
    OK_Button.Enabled = CType(IIf(txtPlan.Text.Trim.Length > 0, True, False), Boolean)
  End Sub

  Private Sub dtpStartDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpStartDate.ValueChanged, dtpEndDate.ValueChanged
    OK_Button.Enabled = CType(IIf(txtPlan.Text.Trim.Length > 0, True, False), Boolean) 'True
  End Sub

  Private Sub chkLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkLock.CheckedChanged
    OK_Button.Enabled = CType(IIf(txtPlan.Text.Trim.Length > 0, True, False), Boolean)
  End Sub
End Class
