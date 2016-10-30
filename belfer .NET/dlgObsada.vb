Imports System.Windows.Forms

Public Class dlgObsada
  Public IsNewMode, Virtual As Boolean
  Public IdObsadaFilter, ObsadaFilter As String, ColNumber As Byte ', Virtual As String

  Public Event NewObsadaAdded(ByVal IdObsada As String)

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If IsNewMode Then
      If AddNew() Then
        'cbGodzina.SelectedIndex = CType(IIf(cbGodzina.SelectedIndex < cbGodzina.Items.Count - 1, cbGodzina.SelectedIndex + 1, 0), Integer)
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    'Me.Dispose(True)
    Close()
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
  Private Function AddNew() As Boolean
    Dim Klasa, Przedmiot As String
    If ObsadaFilter = "Klasa" Then
      Klasa = IdObsadaFilter
      Przedmiot = CType(cb1.SelectedItem, CbItem).ID.ToString
    ElseIf ObsadaFilter = "Belfer" Then
      Klasa = CType(cb2.SelectedItem, CbItem).ID.ToString
      Przedmiot = CType(cb1.SelectedItem, CbItem).ID.ToString
    Else
      Klasa = CType(cb1.SelectedItem, CbItem).ID.ToString
      Przedmiot = IdObsadaFilter
    End If
    Dim MySQLTrans As MySqlTransaction = Nothing
    Dim DBA As New DataBaseAction, O As New ObsadaSQL, OK As New KolumnaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(O.InsertObsada)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      If ActiveObsadaExist(Klasa, Przedmiot, My.Settings.SchoolYear) Then
        Throw New Exception("W podanej klasie istnieje aktywna obsada wskazanego przedmiotu. Deaktywuj istniejącą obsadę i spróbuj jeszcze raz.")
      End If

      With Me
        'dodać nauczanie indywidualne
        If ObsadaFilter = "Klasa" Then
          cmd.Parameters.AddWithValue("?Klasa", IdObsadaFilter)
          cmd.Parameters.AddWithValue("?Przedmiot", CType(.cb1.SelectedItem, CbItem).ID)
          cmd.Parameters.AddWithValue("?Nauczyciel", CType(.cb2.SelectedItem, CbItem).ID)
        ElseIf ObsadaFilter = "Belfer" Then
          cmd.Parameters.AddWithValue("?Klasa", CType(.cb2.SelectedItem, CbItem).ID)
          cmd.Parameters.AddWithValue("?Przedmiot", CType(.cb1.SelectedItem, CbItem).ID)
          cmd.Parameters.AddWithValue("?Nauczyciel", IdObsadaFilter)
        Else
          cmd.Parameters.AddWithValue("?Nauczyciel", CType(.cb2.SelectedItem, CbItem).ID)
          cmd.Parameters.AddWithValue("?Klasa", CType(.cb1.SelectedItem, CbItem).ID)
          cmd.Parameters.AddWithValue("?Przedmiot", IdObsadaFilter)
        End If
        cmd.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
        cmd.Parameters.AddWithValue("?Kategoria", CType(.cbKategoria.SelectedItem, CbItem).Kod)
        cmd.Parameters.AddWithValue("?GetToAverage", CType(.chkGetToAvg.CheckState, Integer))
        'cmd.Parameters.AddWithValue("?Status", CType(.chkStatus.CheckState, Integer))
        cmd.Parameters.AddWithValue("?DataAktywacji", dtDataAktywacji.Value)
        cmd.Parameters.AddWithValue("?LiczbaGodzin", nudLiczbaGodzin.Value)
        cmd.ExecuteNonQuery()
        If Virtual Then
          Dim cmdVirtual As MySqlCommand = DBA.CreateCommand(O.InsertVirtualAllocation)
          With cmdVirtual
            .Transaction = MySQLTrans
            .Parameters.AddWithValue("?Przydzial", CType(cmdStudent.Tag, CbItem).ID)
            .Parameters.AddWithValue("?Obsada", cmd.LastInsertedId)
            .ExecuteNonQuery()
          End With
        End If

        Dim OldObjectStaff As ObjectStaff = GetNotActiveStaff(Klasa, Przedmiot)
        If OldObjectStaff Is Nothing Then
          For Each Typ As String In New String() {"C1", "C2"}
            For i As Byte = 1 To ColNumber
              Dim cmdCols As MySqlCommand = DBA.CreateCommand(OK.InsertKolumna)
              With cmdCols
                .Transaction = MySQLTrans
                .Parameters.AddWithValue("?NrKolumny", i)
                .Parameters.AddWithValue("?IdObsada", cmd.LastInsertedId)
                .Parameters.AddWithValue("?IdOpis", DBNull.Value)
                .Parameters.AddWithValue("?Typ", Typ)
                .Parameters.AddWithValue("?Waga", 1)
                .Parameters.AddWithValue("?Poprawa", 0)
                .ExecuteNonQuery()
              End With
            Next
          Next
          For Each Typ As String In New String() {"S", "R"}
            Dim cmdEndCol As MySqlCommand = DBA.CreateCommand(OK.InsertKolumna)
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
            Dim cmdUpdateKolumna As MySqlCommand = DBA.CreateCommand(O.UpdateKolumna)
            With cmdUpdateKolumna
              .Transaction = MySQLTrans
              .Parameters.AddWithValue("?IdObsadaOld", OldObjectStaff.ID)
              .Parameters.AddWithValue("?IdObsadaNew", cmd.LastInsertedId)
              .ExecuteNonQuery()
            End With
          Else
            Throw New Exception("Data aktywacji musi być większa od daty deaktywacji poprzedniej obsady.")
          End If
        End If
        MySQLTrans.Commit()
        RaiseEvent NewObsadaAdded(cmd.LastInsertedId.ToString)
      End With
      Return True
    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Zdefiniowana obsada już istnieje w danej klasie!")
        Case Else
          MessageBox.Show(ex.Message & vbCr & ex.Number)
      End Select
      Return False
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Return False
    End Try
  End Function

  Private Sub cbBelfer_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb1.SelectionChangeCommitted, cb2.SelectionChangeCommitted
    If CType(sender, ComboBox).Name = "cbPrzedmiot" Then
      If cb2.Text.Length > 0 Then OK_Button.Enabled = True
    Else
      If cb1.Text.Length > 0 Then OK_Button.Enabled = True
    End If
  End Sub
  Public Sub FillCombo(cb As ComboBox, Filter As String)
    Dim FCB As New FillComboBox, Content As String = ""  ', PN As New PrzydzialNauczycieliSQL
    If Filter = "Klasa" Then
      Dim O As New ObsadaSQL
      Content = O.SelectClasses(My.Settings.IdSchool, Convert.ToInt32(Virtual).ToString)
      'Content = O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, Virtual)
    ElseIf Filter = "Belfer" Then
      Dim PN As New PrzydzialNauczycieliSQL
      Content = PN.SelectSchoolTeachers(My.Settings.IdSchool)
    Else
      Dim O As New ObsadaSQL
      Content = O.SelectPrzedmiot(My.Settings.IdSchool)
    End If
    FCB.AddComboBoxComplexItems(cb, content)
    With cb
      .AutoCompleteSource = AutoCompleteSource.ListItems
      .AutoCompleteMode = AutoCompleteMode.Append
      .Enabled = CType(IIf(.Items.Count > 0, True, False), Boolean)
    End With
  End Sub

  Public Sub FillKategoria()
    With cbKategoria
      .Items.Add(New CbItem("o", "Obowiązkowy"))
      .Items.Add(New CbItem("n", "Nadobowiązkowy"))
      .SelectedIndex = 0
      .AutoCompleteSource = AutoCompleteSource.ListItems
      .AutoCompleteMode = AutoCompleteMode.Append
      .Enabled = CType(IIf(.Items.Count > 0, True, False), Boolean)

    End With
  End Sub

  Private Sub cbKategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKategoria.SelectedIndexChanged
    If cb1.SelectedItem Is Nothing OrElse cb2.SelectedItem Is Nothing Then Exit Sub
    OK_Button.Enabled = True
  End Sub

  Private Sub chkGetToAvg_CheckedChanged(sender As Object, e As EventArgs) Handles chkGetToAvg.CheckedChanged
    If cb1.SelectedItem Is Nothing OrElse cb2.SelectedItem Is Nothing Then Exit Sub
    OK_Button.Enabled = True
  End Sub

  Private Sub cmdStudent_Click(sender As Object, e As EventArgs) Handles cmdStudent.Click
    Dim Student As New dlgNauczanieIndywidualne
    cmdStudent.Enabled = False
    With Student
      .StartPosition = FormStartPosition.Manual
      .Location = New Point(Me.Location.X + Me.cmdStudent.Location.X + 10, Me.Location.Y + Me.cmdStudent.Location.Y + 23 + 30)
      .ListViewConfig(.lvStudent)
      .FillKlasa()
      If .ShowDialog() = Windows.Forms.DialogResult.OK Then
        cmdStudent.Tag = New CbItem(CType(.lvStudent.SelectedItems(0).Text, Integer), .lvStudent.SelectedItems(0).SubItems(1).Text)
        cmdStudent.Text = CType(cmdStudent.Tag, CbItem).Nazwa
        pnlObsada.Enabled = True
      End If
      cmdStudent.Enabled = True
    End With
  End Sub


End Class

Public Class ObjectStaff
  Public Property ID As Integer
  Public Property DataDeaktywacji As Date
End Class