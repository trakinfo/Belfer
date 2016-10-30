Imports System.Windows.Forms

Public Class dlgStudentPoprawka

  Public Typ As String
  Public Event NewAdded()
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If AddNew() Then
      OK_Button.Enabled = False
      cbStudent_SelectedIndexChanged(Nothing, Nothing)
    End If
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub


 
  Private Function AddNew() As Boolean
    AddNew = False
    Dim MySQLTran As MySqlTransaction
    Dim DBA As New DataBaseAction, P As New PoprawkaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertPoprawka)
    MySQLTran = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTran
    Try
      cmd.Parameters.AddWithValue("?IdPrzydzial", CType(cbStudent.SelectedItem, CbItem).ID)
      cmd.Parameters.AddWithValue("?IdObsada", CType(cbPrzedmiot.SelectedItem, CbItem).ID)
      cmd.Parameters.AddWithValue("?Typ", "R")
      cmd.ExecuteNonQuery()

      MySQLTran.Commit()
      RaiseEvent NewAdded()
      Return True

    Catch myex As MySqlException
      MySQLTran.Rollback()
      Select Case myex.Number
        Case 1062
          MessageBox.Show("Podana wartość już istnieje w bazie danych!", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Case Else
          MessageBox.Show(myex.Message & vbCr & myex.Number)
      End Select
    Catch ex As Exception
      MySQLTran.Rollback()
      MessageBox.Show(ex.Message)
      'Return False
    End Try
    'Return False
  End Function
  Private Sub FillComboBox(ByVal cb As ComboBox, SelectString As String)
    cb.Items.Clear()
    Dim FCB As New FillComboBox
    FCB.AddComboBoxComplexItems(cb, SelectString)
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    Dim P As New PoprawkaSQL
    FillComboBox(cbStudent, P.SelectStudent(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear, Typ, 4))
  End Sub

  Private Sub cbStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbStudent.SelectedIndexChanged
    Dim P As New PoprawkaSQL
    FillComboBox(cbPrzedmiot, P.SelectPrzedmiot(My.Settings.SchoolYear, Typ, CType(cbStudent.SelectedItem, CbItem).ID))

  End Sub

  Private Sub cbPrzedmiot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    OK_Button.Enabled = True
  End Sub
End Class
