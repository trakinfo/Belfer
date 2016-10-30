Imports System.Windows.Forms

Public Class dlgKopiujPlan

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Try
      If cbTarget.SelectedItem Is Nothing OrElse cbSource.SelectedItem Is Nothing Then
        MessageBox.Show("Nie wybrano planu docelowego lub(i) źródłowego!" & vbCr & "Spróbuj jeszcze raz.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Exit Sub
      ElseIf CType(cbTarget.SelectedItem, CbItem).ID = CType(cbSource.SelectedItem, CbItem).ID Then
        MessageBox.Show("Plan źródłowy i docelowy nie mogą być takie same!" & vbCr & "Spróbuj wybrać inny.")
        Exit Sub
      ElseIf CType(cbTarget.SelectedItem, CbItem).Kod = "1" Then
        MessageBox.Show("Wybrany plan docelowy jest zamknięty!" & vbCr & "Wybierz plan, którego status jest otwarty.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Exit Sub
      End If

      'If CType(cbTarget.SelectedItem, CbItem).ID <> CType(cbSource.SelectedItem, CbItem).ID Then
      'If CType(cbTarget.SelectedItem, CbItem).Kod = "1" Then
      '  MessageBox.Show("Wybrany plan docelowy jest zamknięty!" & vbCr & "Wybierz plan, którego status jest otwarty.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
      '  Exit Sub
      'End If
      Dim DBA As New DataBaseAction, P As New PlanSQL
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      If DBA.ComputeRecords(P.CountLekcja(CType(Me.cbTarget.SelectedItem, CbItem).ID.ToString)) > 0 Then
        If MessageBox.Show("Plan wybrany jako docelowy nie jest pusty." & vbCr & "Czy mimo to chcesz kontynuować?" & vbCr & "Jeśli tak, to wszystkie dane związane z tym planem zostaną usunięte!", "Ostrzeżenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
          Try
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim cmd As MySqlCommand = DBA.CreateCommand(P.DeleteLekcja(CType(cbTarget.SelectedItem, CbItem).ID.ToString))
            cmd.Transaction = MySQLTrans
            cmd.ExecuteNonQuery()
            CopyPlanContent(MySQLTrans, CType(cbSource.SelectedItem, CbItem).ID.ToString, CType(cbTarget.SelectedItem, CbItem).ID.ToString)
            MySQLTrans.Commit()
            Windows.Forms.Cursor.Current = Cursors.Default
            MessageBox.Show("Plan został skopiowany.")
          Catch mex As MySqlException
            MySQLTrans.Rollback()
            MessageBox.Show(mex.Message)
          End Try
        End If
      Else
        Try
          Windows.Forms.Cursor.Current = Cursors.WaitCursor
          CopyPlanContent(MySQLTrans, CType(cbSource.SelectedItem, CbItem).ID.ToString, CType(cbTarget.SelectedItem, CbItem).ID.ToString)
          MySQLTrans.Commit()
          Windows.Forms.Cursor.Current = Cursors.Default
          MessageBox.Show("Plan został skopiowany.")
        Catch mex As MySqlException
          MySQLTrans.Rollback()
          MessageBox.Show(mex.Message)
        End Try
      End If
      'Else
      '  MessageBox.Show("Plan źródłowy i docelowy nie mogą być takie same!" & vbCr & "Spróbuj wybrać inny.")
      'End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub CopyPlanContent(MyTran As MySqlTransaction, IdSourcePlan As String, IdTargetPlan As String)
    Dim DBA As New DataBaseAction, P As New PlanSQL, DT As DataTable

    DT = DBA.GetDataTable(P.SelectLekcja(IdSourcePlan))
    For Each R As DataRow In DT.Rows
      Dim cmd As MySqlCommand = DBA.CreateCommand(P.CopyLesson)
      cmd.Transaction = MyTran
      cmd.Parameters.AddWithValue("?IdPlan", IdTargetPlan)
      cmd.Parameters.AddWithValue("?IdObsada", R.Item("IdObsada"))
      cmd.Parameters.AddWithValue("?DzienTygodnia", R.Item("DzienTygodnia"))
      cmd.Parameters.AddWithValue("?IdGodzina", R.Item("IdGodzina"))
      cmd.Parameters.AddWithValue("?Sala", R.Item("Sala"))
      cmd.Parameters.AddWithValue("?Owner", R.Item("Owner"))
      cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
      '      cmd.Parameters.AddWithValue("?User", R.Item("User"))
      cmd.Parameters.AddWithValue("?IP", R.Item("ComputerIP"))
      cmd.ExecuteNonQuery()
    Next
  End Sub
  Public Sub FillPlan(cb As ComboBox)
    Dim FCB As New FillComboBox, P As New PlanSQL, CH As New CalcHelper
    FCB.AddComboBoxExtendedItems(cb, P.SelectPlan2(My.Settings.IdSchool, CH.StartDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString, CH.EndDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString))
    If cb.Items.Count > 0 Then
      'cb.SelectedIndex = 0
      cb.Enabled = True
    Else
      cb.Enabled = False
    End If
  End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

  Private Sub cbTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTarget.SelectedIndexChanged
    OK_Button.Enabled = True
  End Sub
End Class
