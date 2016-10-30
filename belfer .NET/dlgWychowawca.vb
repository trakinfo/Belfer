Imports System.Windows.Forms

Public Class dlgWychowawca
  Public IdPrzedmiot As String
  Public IsNewMode As Boolean
  Public Event NewWychowawcaAdded(ByVal InsertedKlasa As String)

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If IsNewMode Then
      If AddNewWychowawca() Then
        FillKlasa()
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
  Private Function ActiveObsadaExist(Klasa As String, Przedmiot As String, RokSzkolny As String) As Boolean
    Dim DBA As New DataBaseAction, O As New ObsadaSQL, R As MySqlDataReader = Nothing
    Try
      R = DBA.GetReader(O.CountActiveStaff(Klasa, Przedmiot, RokSzkolny))
      If R.HasRows Then Return True
      Return False
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      Return False
    Finally
      R.Close()
    End Try
  End Function
  'Private Function ActiveObsadaExist(Klasa As String, Przedmiot As String) As Boolean
  '  Dim DBA As New DataBaseAction, O As New ObsadaSQL, R As MySqlDataReader = Nothing
  '  Try
  '    R = DBA.GetReader(O.CountActiveStaff(Klasa, Przedmiot))
  '    If R.HasRows Then Return True
  '    Return False
  '  Catch mex As MySqlException
  '    MessageBox.Show(mex.Message)
  '    Return False
  '  Finally
  '    R.Close()
  '  End Try
  'End Function
  Private Function GetNotActiveStaff(Klasa As String, Przedmiot As String) As ObjectStaff
    Dim DBA As New DataBaseAction, O As New ObsadaSQL, R As MySqlDataReader = Nothing
    Try
      R = DBA.GetReader(O.SelectClosingNotActiveStaff(Klasa, Przedmiot))
      If R.HasRows Then
        Dim ClosingNotActiveStaff As New ObjectStaff
        R.Read()
        ClosingNotActiveStaff.ID = R.GetInt32("ID")
        ClosingNotActiveStaff.DataDeaktywacji = R.GetDateTime("DataDeaktywacji")
        Return ClosingNotActiveStaff
      End If
      Return Nothing
      'Return DBA.GetSingleValue(O.SelectNotActiveStaffID(Klasa, Przedmiot))
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      Return Nothing
    Finally
      R.Close()
    End Try
  End Function
  Private Function AddNewWychowawca() As Boolean
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, W As New WychowawcaSQL, K As New KolumnaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(W.InsertWychowawca)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      If ActiveObsadaExist(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, IdPrzedmiot, My.Settings.SchoolYear) Then
        Throw New Exception("W podanej klasie istnieje aktywna obsada wskazanego przedmiotu. Deaktywuj istniejącą obsadę i spróbuj jeszcze raz.")
      End If
      With Me
        'Dim OldObjectStaff As ObjectStaff = GetNotActiveStaff(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, IdPrzedmiot)
        'If dtDataAktywacji.Value <= OldObjectStaff.DataDeaktywacji Then Throw New Exception("Data aktywacji musi być większa od daty deaktywacji poprzedniego wychowawstwa.")
        cmd.Parameters.AddWithValue("?Klasa", CType(.cbKlasa.SelectedItem, CbItem).ID)
        cmd.Parameters.AddWithValue("?IdPrzedmiot", IdPrzedmiot)
        cmd.Parameters.AddWithValue("?IdNauczyciel", CType(.cbBelfer.SelectedItem, CbItem).ID)
        cmd.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
        cmd.Parameters.AddWithValue("?DataAktywacji", .dtDataAktywacji.Value)
        cmd.Parameters.AddWithValue("?LiczbaGodzin", .nudLiczbaGodzin.Value)
        cmd.ExecuteNonQuery()
        Dim OldObjectStaff As ObjectStaff = GetNotActiveStaff(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, IdPrzedmiot)
        If OldObjectStaff Is Nothing Then
          For Each Typ As String In New String() {"S", "R"}
            Dim cmdEndCol As MySqlCommand = DBA.CreateCommand(K.InsertKolumna)
            With cmdEndCol
              .Transaction = MySQLTrans
              .Parameters.AddWithValue("?NrKolumny", 0)
              .Parameters.AddWithValue("?IdObsada", cmd.LastInsertedId)
              .Parameters.AddWithValue("?IdOpis", DBNull.Value)
              .Parameters.AddWithValue("?Typ", Typ)
              .Parameters.AddWithValue("?Waga", 1)
              .Parameters.AddWithValue("?Poprawa", 0)
              .ExecuteNonQuery()
            End With
          Next
        Else
          If dtDataAktywacji.Value > OldObjectStaff.DataDeaktywacji Then
            Dim O As New ObsadaSQL
            Dim cmdUpdateKolumna As MySqlCommand = DBA.CreateCommand(O.UpdateKolumna)
            With cmdUpdateKolumna
              .Transaction = MySQLTrans
              .Parameters.AddWithValue("?IdObsadaOld", OldObjectStaff.ID)
              .Parameters.AddWithValue("?IdObsadaNew", cmd.LastInsertedId)
              .ExecuteNonQuery()
            End With
          Else
            Throw New Exception("Data aktywacji musi być większa od daty deaktywacji poprzedniego wychowawstwa.")
          End If
        End If

        MySQLTrans.Commit()
        RaiseEvent NewWychowawcaAdded(CType(.cbKlasa.SelectedItem, CbItem).ID.ToString)
      End With
      Return True
    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wybrana klasa ma już przydzielone wychowawstwo!")
        Case Else
          MessageBox.Show(ex.Message & vbCr & ex.Number)
      End Select
      Return False
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      Return False
    End Try
  End Function


  Private Sub cbBelfer_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbBelfer.SelectionChangeCommitted, cbKlasa.SelectionChangeCommitted
    If CType(sender, ComboBox).Name = "cbKlasa" Then
      If cbBelfer.Text.Length > 0 Then OK_Button.Enabled = True
    Else
      If cbKlasa.Text.Length > 0 Then OK_Button.Enabled = True
    End If
  End Sub

  Public Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, W As New WychowawcaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, W.SelectClass(My.Settings.IdSchool, My.Settings.SchoolYear.Substring(0, 4)))
    cbKlasa.AutoCompleteSource = AutoCompleteSource.ListItems
    cbKlasa.AutoCompleteMode = AutoCompleteMode.Append
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Public Sub FillBelfer()
    cbBelfer.Items.Clear()
    Dim FCB As New FillComboBox, W As New WychowawcaSQL
    FCB.AddComboBoxComplexItems(cbBelfer, W.SelectBelfer(My.Settings.IdSchool))
    cbBelfer.AutoCompleteSource = AutoCompleteSource.ListItems
    cbBelfer.AutoCompleteMode = AutoCompleteMode.Append
    cbBelfer.Enabled = CType(IIf(cbBelfer.Items.Count > 0, True, False), Boolean)
  End Sub

  Private Sub nudLiczbaGodzin_ValueChanged(sender As Object, e As EventArgs) Handles nudLiczbaGodzin.ValueChanged
    If Not nudLiczbaGodzin.Created Then Exit Sub
    cbBelfer_SelectionChangeCommitted(cbKlasa, Nothing)
  End Sub

  Private Sub dtDataAktywacji_ValueChanged(sender As Object, e As EventArgs) Handles dtDataAktywacji.ValueChanged
    If Not dtDataAktywacji.Created Then Exit Sub
    cbBelfer_SelectionChangeCommitted(cbKlasa, Nothing)
  End Sub
End Class
