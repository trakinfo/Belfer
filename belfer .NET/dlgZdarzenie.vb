Imports System.Windows.Forms

Public Class dlgZdarzenie
  Public IsNewMode, IsRefreshMode As Boolean, Klasa As String ', IdLekcja As Integer
  Public Event NewAdded(ByVal InsertedID As String)
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

    If IsNewMode Then
      If AddNew() Then
        FillGodzina()
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
  
  Private Sub ClearData()
    txtZdarzenie.Text = ""
    txtZdarzenie.Enabled = False
    chkStatus.Checked = False
    chkStatus.Enabled = False
  End Sub

  Public Sub FillGodzina()
    ClearData()
    With cbGodzina
      .Items.Clear()
      Dim FCB As New FillComboBox, T As New TerminarzSQL
      'nie wybiera lekcji
      FCB.AddComboBoxComplexItems(cbGodzina, T.SelectLekcja(My.Settings.SchoolYear, My.Settings.IdSchool, Klasa, GlobalValues.AppUser.SchoolTeacherID, dtpDataZajec.Value))
      'cb.Enabled = False
      .Enabled = CType(IIf(.Items.Count > 0, True, False), Boolean)
    End With
  End Sub

  Private Sub cbGodzina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGodzina.SelectedIndexChanged
    'IdLekcja = CType(cbGodzina.SelectedItem, CbItem).ID
    txtZdarzenie.Enabled = True
    chkStatus.Enabled = True
  End Sub
 
  Private Function AddNew() As Boolean
    AddNew = False
    Dim MySQLTran As MySqlTransaction
    Dim DBA As New DataBaseAction, T As New TerminarzSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(T.InsertEvent)
    MySQLTran = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTran
    Try
      cmd.Parameters.AddWithValue("?Info", txtZdarzenie.Text.Trim)
      cmd.Parameters.AddWithValue("?Idlekcja", CType(cbGodzina.SelectedItem, CbItem).ID)
      cmd.Parameters.AddWithValue("?Data", dtpDataZajec.Value.ToShortDateString)
      cmd.Parameters.AddWithValue("?Status", chkStatus.Checked)
      cmd.ExecuteNonQuery()
      MySQLTran.Commit()
      RaiseEvent NewAdded(cmd.LastInsertedId.ToString)
      Return True
    Catch myex As MySqlException
      MySQLTran.Rollback()
      MessageBox.Show(myex.Message & vbCr & myex.Number)
    Catch ex As Exception
      MySQLTran.Rollback()
      MessageBox.Show(ex.Message)
    End Try
  End Function

  Private Sub txtTemat_TextChanged(sender As Object, e As EventArgs) Handles txtZdarzenie.TextChanged
    If IsRefreshMode Then Exit Sub
    OK_Button.Enabled = CType(IIf(txtZdarzenie.Text.Length > 0, True, False), Boolean)
  End Sub

  Private Sub dtpDataZajec_ValueChanged(sender As Object, e As EventArgs) Handles dtpDataZajec.ValueChanged
    If IsNewMode Then FillGodzina()
  End Sub

  Private Sub chkStatus_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatus.CheckedChanged
    'If Not IsRefreshMode Then OK_Button.Enabled = True
    txtTemat_TextChanged(Nothing, Nothing)
  End Sub

End Class
