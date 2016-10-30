Public Class frmBlokada
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.LockResultColumnToolStripMenuItem.Enabled = True

  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.LockResultColumnToolStripMenuItem.Enabled = True

  End Sub
  Private Sub rbSemestr_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemestr.CheckedChanged, rbAllType.CheckedChanged, rbRokSzkolny.CheckedChanged, rbLock.CheckedChanged, rbUnlock.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    txtOutcome.Text = "Liczba rekordów spełniających kryteria: "
    Dim Zakres, Typ As RadioButton
    Typ = gbTypOperacji.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = False)
    Zakres = gbZakresOperacji.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    Dim Outcome As Integer = GetOutcome(Zakres.Tag.ToString, Typ.Tag.ToString)
    txtOutcome.Text += Outcome.ToString
    cmdExecute.Enabled = CType(Outcome, Boolean)
  End Sub
  Private Function GetOutcome(Zakres As String, StatusBlokady As String) As Integer
    Dim A As New AdminSQL, DBA As New DataBaseAction
    Return DBA.ComputeRecords(A.CountColumnLock(StatusBlokady, Zakres, My.Settings.SchoolYear))
  End Function

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub frmBlokada_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    rbSemestr.Checked = True
  End Sub

  Private Sub cmdExecute_Click(sender As Object, e As EventArgs) Handles cmdExecute.Click
    cmdExecute.Enabled = False
    Dim A As New AdminSQL, DBA As New DataBaseAction, cmd As MySqlCommand, T As MySqlTransaction = GlobalValues.gblConn.BeginTransaction
    Try
      Dim Zakres, Typ As RadioButton
      Typ = gbTypOperacji.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
      Zakres = gbZakresOperacji.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
      cmd = DBA.CreateCommand(A.UpdateColumnLock(Typ.Tag.ToString, Zakres.Tag.ToString, My.Settings.SchoolYear))
      cmd.Transaction = T
      Dim Outcome As Integer
      Outcome = cmd.ExecuteNonQuery()
      T.Commit()
      txtOutcome.Text += vbNewLine & "Liczba zmodyfikowanych rekordów: " & Outcome.ToString
    Catch mex As MySqlException
      T.Rollback()
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      T.Rollback()
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

End Class