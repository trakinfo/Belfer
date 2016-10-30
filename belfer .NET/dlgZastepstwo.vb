Imports System.Windows.Forms

Public Class dlgZastepstwo
  Public IsNewMode, IsRefreshMode As Boolean, IdLekcja As String
  Public Event NewAdded(ByVal InsertedID As String)
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()
    If IsNewMode Then
      If AddNew() Then
        FillLekcja(cbLekcja)
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub
  Private Function AddNew() As Boolean
    AddNew = False
    Dim MySQLTran As MySqlTransaction
    Dim DBA As New DataBaseAction, Z As New ZastepstwoSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(Z.InsertZastepstwo)
    MySQLTran = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTran
    Try
      cmd.Parameters.AddWithValue("?Idlekcja", CType(cbLekcja.SelectedItem, CbItem).ID)
      cmd.Parameters.AddWithValue("?Data", dtpData.Value.ToShortDateString)
      cmd.Parameters.AddWithValue("?Sala", txtSala.Text.Trim)
      cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
      cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
      If chkStatusLekcji.Checked Then
        cmd.Parameters.AddWithValue("?IdNauczyciel", DBNull.Value)
        cmd.Parameters.AddWithValue("?IdPrzedmiot", DBNull.Value)
        cmd.Parameters.AddWithValue("?Status", chkStatusLekcji.CheckState)
      Else
        cmd.Parameters.AddWithValue("?Status", chkStatus.CheckState)
        cmd.Parameters.AddWithValue("?IdNauczyciel", CType(cbNauczyciel2.SelectedItem, CbItem).ID)
        cmd.Parameters.AddWithValue("?IdPrzedmiot", CType(cbPrzedmiot.SelectedItem, CbItem).ID)
      End If
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
  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cbNauczyciel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbNauczyciel.SelectedIndexChanged
    cbNauczyciel2.SelectedItem = Nothing
    cbNauczyciel2.Enabled = False
    cbPrzedmiot.Items.Clear()
    cbPrzedmiot.Enabled = False
    chkStatus.Enabled = False
    chkStatusLekcji.Enabled = False
    OK_Button.Enabled = False
    txtSala.Enabled = False
    FillLekcja(cbLekcja)
  End Sub
  Private Sub FillLekcja(ByVal cb As ComboBox)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, Z As New ZastepstwoSQL
    FCB.AddComboBoxComplexItems(cb, Z.SelectLekcja(My.Settings.IdSchool, CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString, dtpData.Value.ToShortDateString, Weekday(dtpData.Value, FirstDayOfWeek.Monday).ToString))
    'FCB.AddComboBoxComplexItems(cb, Z.SelectLekcja(My.Settings.IdSchool, CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString, dtpData.Value.ToShortDateString, CType(dtpData.Value.DayOfWeek, Integer).ToString))
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
  End Sub
  
  Private Sub dtpData_ValueChanged(sender As Object, e As EventArgs) Handles dtpData.ValueChanged
    If Not IsNewMode Then Exit Sub
    If IsRefreshMode Then Exit Sub
    If cbNauczyciel.SelectedItem IsNot Nothing Then cbNauczyciel_SelectedIndexChanged(Nothing, Nothing)

  End Sub

  Private Sub cbLekcja_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLekcja.SelectedIndexChanged
    If chkStatusLekcji.Checked = False Then
      cbNauczyciel2.Enabled = True
      OK_Button.Enabled = CType(IIf(cbPrzedmiot.SelectedItem IsNot Nothing, True, False), Boolean)
    Else
      cbNauczyciel2.SelectedItem = Nothing
      cbNauczyciel2.Enabled = False
      cbPrzedmiot.Enabled = False
      chkStatus.Enabled = False
      OK_Button.Enabled = True 'CType(IIf(cbPrzedmiot.SelectedItem IsNot Nothing, True, False), Boolean)
    End If
    chkStatusLekcji.Enabled = True
    txtSala.Enabled = True
  End Sub

  Private Sub cbNauczyciel2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbNauczyciel2.SelectedIndexChanged
    If cbNauczyciel2.SelectedItem Is Nothing Then
      cbPrzedmiot.SelectedItem = Nothing
      Exit Sub
    End If
    chkStatus.Checked = False
    chkStatus.Enabled = False
    OK_Button.Enabled = False
    Dim Z As New ZastepstwoSQL, FCB As New FillComboBox, Lekcja As String
    If IsNewMode Then
      Lekcja = CType(cbLekcja.SelectedItem, CbItem).ID.ToString
    Else
      Lekcja = IdLekcja
    End If
    If CType(cbNauczyciel.SelectedItem, CbItem).ID = CType(cbNauczyciel2.SelectedItem, CbItem).ID Then
      FCB.AddComboBoxComplexItems(cbPrzedmiot, Z.SelectPrzedmiot(My.Settings.IdSchool, Lekcja))
    Else
      FCB.AddComboBoxComplexItems(cbPrzedmiot, Z.SelectPrzedmiot(My.Settings.IdSchool))
    End If
    cbPrzedmiot.Enabled = CType(IIf(cbPrzedmiot.Items.Count > 0, True, False), Boolean)
  End Sub

  Private Sub cbPrzedmiot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    'If cbNauczyciel2.SelectedItem IsNot Nothing Then
    chkStatus.Enabled = True
    OK_Button.Enabled = True
    'End If
  End Sub

  'Private Sub txtSala_TextChanged(sender As Object, e As EventArgs) Handles txtSala.TextChanged
  '  cbLekcja_SelectedIndexChanged(sender, e)
  'End Sub

  Private Sub chkStatusLekcji_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatusLekcji.CheckedChanged
    cbLekcja_SelectedIndexChanged(sender, e)
  End Sub
End Class
