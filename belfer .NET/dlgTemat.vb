Imports System.Windows.Forms

Public Class dlgTemat
  Public IsNewMode, IsRefreshMode, IsVirtual As Boolean, Klasa As String, IdLekcja, Lp As Integer, DataLekcji As Date, DtStudent, dtFrekwencja, dtObsada, dtGrupa, dtZastepstwo, dtNrTemat, dtIndividualStaff As DataTable
  Public Event NewAdded(ByVal InsertedID As String)
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()
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
  Public Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      '.Enabled = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Items.Clear()
      '.Enabled = False
    End With
  End Sub
  Public Sub SetColumns(lv As ListView, HeaderText As String)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nr", 30, HorizontalAlignment.Center)
      .Columns.Add(HeaderText, 225, HorizontalAlignment.Left)
      .Columns.Add("IdFrekwencja", 0, HorizontalAlignment.Left)
      .Columns.Add("Typ", 0, HorizontalAlignment.Left)
    End With
  End Sub
  Public Sub GetData(IdLekcja As Integer)
    'Dim DBA As New DataBaseAction
    Try
      ClearLV(False)
      GetPresent(IdLekcja)
      GetAbsent(IdLekcja)
      GetLate(IdLekcja)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub GetPresent(IdLekcja As Integer)
    Dim dtPresent As DataTable
    'dtPresent = CreateDataTable(New String() {"IdStudent", "Nr", "Student"}, New String() {"System.Int32", "System.UInt16", "System.String"})
    dtPresent = CreateDataTable(New String() {"IdStudent", "Nr", "Student", "IdFrekwencja", "Typ"}, New String() {"System.Int32", "System.UInt16", "System.String", "System.Int32", "System.String"})

    Dim IdPrzedmiot As Integer = CType(dtObsada.Select("ID=" & IdLekcja)(0).Item("Przedmiot"), Integer)
    Dim Gr As DataRow()
    If IsVirtual Then
      Dim tmpStudent As New DataTable
      tmpStudent = DtStudent.Copy
      For Each Student As DataRow In DtStudent.Select("StatusAktywacji=false AND Przedmiot=" & IdPrzedmiot)
        If CType(Student.Item("DataDeaktywacji"), Date) <= DataLekcji Then tmpStudent.Select("ID=" & Student.Item("ID").ToString)(0).Delete()
      Next
      Gr = tmpStudent.Select("Przedmiot=" & IdPrzedmiot)
    Else
      If dtGrupa.Select("IdSzkolaPrzedmiot=" & IdPrzedmiot).GetLength(0) > 0 Then
        Dim IdPrzydzial As String = ""
        For Each Student As DataRow In dtGrupa.Select("IdSzkolaPrzedmiot=" & IdPrzedmiot)
          If CType(Student.Item("StatusAktywacji"), Boolean) = True Then
            IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
          Else
            If CType(Student.Item("Deaktywacja"), Date) > DataLekcji Then
              IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
            End If
          End If
        Next
        Gr = DtStudent.Select("IdPrzydzial IN (" & IdPrzydzial.TrimEnd(",".ToCharArray) & ")")
      Else
        Dim tmpStudent As New DataTable
        tmpStudent = DtStudent.Copy
        For Each Student As DataRow In dtIndividualStaff.Select("Przedmiot=" & IdPrzedmiot & " AND DataAktywacji<=#" & DataLekcji.ToShortDateString & "# AND (DataDeaktywacji>#" & DataLekcji.ToShortDateString & "# OR DataDeaktywacji is null)")
          tmpStudent.Select("IdPrzydzial=" & Student.Item("IdPrzydzial").ToString)(0).Delete()
        Next
        For Each Student As DataRow In DtStudent.Select("StatusAktywacji=false")
          If CType(Student.Item("DataDeaktywacji"), Date) <= DataLekcji Then tmpStudent.Select("ID=" & Student.Item("ID").ToString)(0).Delete()
        Next
        Gr = tmpStudent.Select()
      End If
    End If

    For Each R As DataRow In Gr 'DtStudent.Rows
      dtPresent.Rows.Add(R.Item(0), R.Item(1), R.Item(2))
    Next
    For Each R As DataRow In dtFrekwencja.Select("IdLekcja=" & IdLekcja & " AND Data=#" & DataLekcji.ToShortDateString & "#")
      dtPresent.Select("IdStudent=" & R.Item("IdUczen").ToString)(0).Delete()
    Next
    LvNewItem(lvPresent, dtPresent)

    lvPresent.Columns(2).Width = CInt(IIf(lvPresent.Items.Count > 17, 206, 225))
    lvPresent.Enabled = CType(IIf(lvPresent.Items.Count > 0, True, False), Boolean)
  End Sub


  Private Sub GetAbsent(IdLekcja As Integer)
    Dim Student As String()
    Dim ItemColor As Color
    For Each R As DataRow In dtFrekwencja.Select("IdLekcja=" & IdLekcja & " AND Data=#" & DataLekcji.ToShortDateString & "# AND Typ IN ('u','n')")
      Student = New String() {DtStudent.Select("ID=" & R.Item("IdUczen").ToString)(0).Item("ID").ToString, DtStudent.Select("ID=" & R.Item("IdUczen").ToString)(0).Item("NrwDzienniku").ToString, DtStudent.Select("ID=" & R.Item("IdUczen").ToString)(0).Item("Student").ToString, R.Item("ID").ToString, R.Item("Typ").ToString}
      ItemColor = CType(IIf(R.Item("Typ").ToString = "n", Color.Red, Color.Green), Color)
      LvNewItem(lvAbsent, Student, ItemColor)
    Next
    lvAbsent.Columns(2).Width = CInt(IIf(lvAbsent.Items.Count > 17, 206, 225))
    lvAbsent.Enabled = CType(IIf(lvAbsent.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub GetLate(IdLekcja As Integer)
    Dim Student As String()
    Dim ItemColor As Color = Color.Black
    For Each R As DataRow In dtFrekwencja.Select("IdLekcja=" & IdLekcja & " AND Data=#" & DataLekcji.ToShortDateString & "# AND Typ='s'")
      Student = New String() {DtStudent.Select("ID=" & R.Item("IdUczen").ToString)(0).Item("ID").ToString, DtStudent.Select("ID=" & R.Item("IdUczen").ToString)(0).Item("NrwDzienniku").ToString, DtStudent.Select("ID=" & R.Item("IdUczen").ToString)(0).Item("Student").ToString, R.Item("ID").ToString, R.Item("Typ").ToString}
      LvNewItem(lvLate, Student, ItemColor)
    Next
    lvLate.Columns(2).Width = CInt(IIf(lvLate.Items.Count > 17, 206, 225))
    lvLate.Enabled = CType(IIf(lvLate.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Function CreateDataTable(ColName As String(), ColType As String()) As DataTable
    Dim DT As DataTable = New DataTable()
    For i As Integer = 0 To ColName.GetLength(0) - 1
      Dim Col As New DataColumn(ColName(i), System.Type.GetType(ColType(i)))
      DT.Columns.Add(Col)
    Next
    Return DT
  End Function
  Private Overloads Sub LvNewItem(ByVal LV As ListView, DT As DataTable)
    For Each row As DataRow In DT.Select("", "Nr")
      Dim NewItem As New ListViewItem(row.Item(0).ToString)
      NewItem.UseItemStyleForSubItems = True
      For i As Integer = 1 To DT.Columns.Count - 1
        NewItem.SubItems.Add(row.Item(i).ToString)
      Next
      LV.Items.Add(NewItem)
    Next
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView, Student As String(), ItemColor As Color)
    Dim NewItem As New ListViewItem(Student(0))
    NewItem.UseItemStyleForSubItems = True
    For i As Integer = 1 To Student.GetLength(0) - 1
      NewItem.SubItems.Add(Student(i))
    Next
    NewItem.ForeColor = ItemColor
    LV.Items.Add(NewItem)
  End Sub
  Private Overloads Sub LvNewItem(ByVal SourceLV As ListView, TargetLV As ListView)
    For Each item As ListViewItem In SourceLV.SelectedItems
      Dim NewItem As New ListViewItem(item.Text)
      NewItem.UseItemStyleForSubItems = True
      For i As Integer = 1 To SourceLV.Columns.Count - 1
        NewItem.SubItems.Add(item.SubItems(i).Text)
      Next
      SourceLV.Items(item.Index).Remove()
      TargetLV.Items.Add(NewItem)
      TargetLV.Sorting = SortOrder.Ascending
      TargetLV.Sort()
    Next
    SourceLV.Enabled = CType(IIf(SourceLV.Items.Count > 0, True, False), Boolean)
    TargetLV.Enabled = CType(IIf(TargetLV.Items.Count > 0, True, False), Boolean)
    OK_Button.Enabled = True
  End Sub
  Private Overloads Sub LvNewItem(ByVal SourceLV As ListView, TargetLV As ListView, ItemColor As Color)
    For Each item As ListViewItem In SourceLV.SelectedItems
      Dim NewItem As New ListViewItem(item.Text)
      NewItem.UseItemStyleForSubItems = True
      NewItem.ForeColor = ItemColor
      For i As Integer = 1 To SourceLV.Columns.Count - 1
        NewItem.SubItems.Add(item.SubItems(i).Text)
      Next
      'NewItem.SubItems.Add(item.SubItems(1).Text)
      SourceLV.Items(item.Index).Remove()
      TargetLV.Items.Add(NewItem)
      TargetLV.Sorting = SortOrder.Ascending
      TargetLV.Sort()
    Next
    SourceLV.Enabled = CType(IIf(SourceLV.Items.Count > 0, True, False), Boolean)
    TargetLV.Enabled = CType(IIf(TargetLV.Items.Count > 0, True, False), Boolean)
    OK_Button.Enabled = True
  End Sub
  Private Sub ClearLV(Value As Boolean)
    lvPresent.Items.Clear()
    lvAbsent.Items.Clear()
    lvLate.Items.Clear()
    lvPresent.Enabled = Value
    lvAbsent.Enabled = Value
    lvLate.Enabled = Value
  End Sub
  Private Sub ClearData()
    txtTemat.Text = ""
    nudNr.Value = 1
    lblZastepstwo.Text = ""
    chkZastepstwo.Checked = False
    lblZastepstwo.Enabled = False
    chkZastepstwo.Enabled = False
    txtTemat.Enabled = False
    nudNr.Enabled = False
    ClearLV(False)
  End Sub

  Public Sub FillGodzina()
    ClearData()
    With cbGodzina
      .Items.Clear()
      Dim FCB As New FillComboBox, T As New TematSQL
      FCB.AddComboBoxComplexItems(cbGodzina, T.SelectLekcja(My.Settings.SchoolYear, My.Settings.IdSchool, Klasa, DataLekcji))
      'cb.Enabled = False
      .Enabled = CType(IIf(.Items.Count > 0, True, False), Boolean)
    End With
  End Sub

  Private Sub chkZastepstwo_CheckedChanged(sender As Object, e As EventArgs) Handles chkZastepstwo.CheckedChanged
    If IsRefreshMode Then Exit Sub
    lblZastepstwo.Enabled = CType(IIf(chkZastepstwo.Checked, True, False), Boolean)
    OK_Button.Enabled = True
  End Sub

  Private Sub lvPresent_DoubleClick(sender As Object, e As EventArgs) Handles lvPresent.DoubleClick
    LvNewItem(lvPresent, lvAbsent, Color.Red)
  End Sub

  Private Sub lvPresent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvPresent.ItemSelectionChanged
    If e.IsSelected Then
      cmdMove.Enabled = True
    Else
      cmdMove.Enabled = False
    End If
  End Sub

  Private Sub lvAbsent_DoubleClick(sender As Object, e As EventArgs) Handles lvAbsent.DoubleClick
    LvNewItem(lvAbsent, lvLate)
  End Sub

  Private Sub lvAbsent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvAbsent.ItemSelectionChanged
    If e.IsSelected Then
      cmdMoveBack.Enabled = True
      cmdMoveDown.Enabled = True
    Else
      cmdMoveBack.Enabled = False
      cmdMoveDown.Enabled = False
    End If
  End Sub

  Private Sub lvLate_DoubleClick(sender As Object, e As EventArgs) Handles lvLate.DoubleClick
    LvNewItem(lvLate, lvPresent)
  End Sub

  Private Sub lvLate_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvLate.ItemSelectionChanged
    If e.IsSelected Then
      cmdMoveOff.Enabled = True
    Else
      cmdMoveOff.Enabled = False
    End If
  End Sub

  Private Sub cmdMove_Click(sender As Object, e As EventArgs) Handles cmdMove.Click
    'For Each Item As ListViewItem In lvPresent.SelectedItems
    '  If lvLate.FindItemWithText(Item.Text) Then MessageBox.Show("Nie można dodać ucznia do listy nieobecnych, ponieważ jest na liście spóźnionych")
    'Next
    LvNewItem(lvPresent, lvAbsent, Color.Red)
  End Sub

  Private Sub cmdMoveBack_Click(sender As Object, e As EventArgs) Handles cmdMoveBack.Click
    LvNewItem(lvAbsent, lvPresent)
  End Sub

  Private Sub cmdMoveDown_Click(sender As Object, e As EventArgs) Handles cmdMoveDown.Click
    LvNewItem(lvAbsent, lvLate)
  End Sub

  Private Sub cmdMoveUp_Click(sender As Object, e As EventArgs) Handles cmdMoveOff.Click

    'For Each item As ListViewItem In lvLate.SelectedItems
    '  lvPresent.Items(lvPresent.FindItemWithText(item.Text).Index).Remove()
    'Next
    LvNewItem(lvLate, lvPresent)    'lvLate.Enabled = CType(IIf(lvLate.Items.Count > 0, True, False), Boolean)
  End Sub

  Private Sub cbGodzina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGodzina.SelectedIndexChanged
    IdLekcja = CType(cbGodzina.SelectedItem, CbItem).ID
    chkStatus.Enabled = True
    chkStatus.Checked = False
    Lp = CType(dtNrTemat.Select("IdObsada=" & CType(dtObsada.Select("ID=" & IdLekcja)(0).Item("IdObsada"), Integer))(0).Item("Nr"), Integer) + 1
    nudNr.Enabled = True
    nudNr.Value = Lp
    txtTemat.Enabled = True
    GetSubstitute(IdLekcja)
    GetData(IdLekcja)
    OK_Button.Enabled = True
  End Sub
  Private Sub GetSubstitute(IdLekcja As Integer)
    If dtZastepstwo.Select("IdLekcja=" & IdLekcja).Length > 0 Then
      lblZastepstwo.Text = dtZastepstwo.Select("IdLekcja=" & IdLekcja)(0).Item("Zastepca").ToString
      chkZastepstwo.Enabled = True
      chkZastepstwo.Checked = CType(dtZastepstwo.Select("IdLekcja=" & IdLekcja)(0).Item("Status"), Boolean)
      lblZastepstwo.Enabled = chkZastepstwo.Checked
      lblZastepstwo.BackColor = SystemColors.Info
      'Return CType(dtZastepstwo.Select("IdLekcja=" & IdLekcja)(0).Item("IdZastepstwo"), Integer)
    Else
      lblZastepstwo.Enabled = False
      lblZastepstwo.BackColor = Color.Transparent
      chkZastepstwo.Enabled = False
      chkZastepstwo.Checked = False
      'Return 0
    End If
  End Sub
  Private Function AddNew() As Boolean
    AddNew = False
    Dim MySQLTran As MySqlTransaction
    Dim DBA As New DataBaseAction, T As New TematSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(T.InsertTopic)
    MySQLTran = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTran
    Try
      cmd.Parameters.AddWithValue("?Tresc", txtTemat.Text.Trim)
      cmd.Parameters.AddWithValue("?Idlekcja", CType(cbGodzina.SelectedItem, CbItem).ID)
      cmd.Parameters.AddWithValue("?Data", DataLekcji.ToShortDateString)
      If chkStatus.Checked = False Then
        cmd.Parameters.AddWithValue("?Nr", nudNr.Value)
        cmd.Parameters.AddWithValue("?StatusLekcji", 1)
        cmd.ExecuteNonQuery()
        If chkZastepstwo.Enabled Then
          cmd.CommandText = T.UpdateSubstituteStatus
          cmd.Parameters.AddWithValue("?IdZastepstwo", dtZastepstwo.Select("IdLekcja=" & CType(cbGodzina.SelectedItem, CbItem).ID)(0).Item("IdZastepstwo"))
          cmd.Parameters.AddWithValue("?Status", chkZastepstwo.Checked)
          cmd.ExecuteNonQuery()
        End If
        For Each Student As ListViewItem In lvPresent.Items
          If Student.SubItems(3).Text.Length > 0 Then
            Dim cmdPresent As MySqlCommand = DBA.CreateCommand(T.DeleteAbsence)
            cmdPresent.Transaction = MySQLTran
            cmdPresent.Parameters.AddWithValue("?ID", Student.SubItems(3).Text)
            cmdPresent.ExecuteNonQuery()
          End If
        Next
        For Each Student As ListViewItem In lvAbsent.Items
          If Student.SubItems(3).Text.Length = 0 Then
            Dim cmdAbsent As MySqlCommand = DBA.CreateCommand(T.InsertAbsence)
            cmdAbsent.Transaction = MySQLTran
            cmdAbsent.Parameters.AddWithValue("?IdUczen", Student.Text)
            cmdAbsent.Parameters.AddWithValue("?IdLekcja", CType(cbGodzina.SelectedItem, CbItem).ID)
            cmdAbsent.Parameters.AddWithValue("?Typ", "n")
            cmdAbsent.Parameters.AddWithValue("?Data", DataLekcji.ToShortDateString)
            cmdAbsent.ExecuteNonQuery()
          ElseIf Student.SubItems(3).Text.Length > 0 AndAlso Student.SubItems(4).Text = "s" Then
            Dim cmdAbsent As MySqlCommand = DBA.CreateCommand(T.UpdateAbsence)
            cmdAbsent.Transaction = MySQLTran
            cmdAbsent.Parameters.AddWithValue("?ID", Student.SubItems(3).Text)
            cmdAbsent.Parameters.AddWithValue("?Typ", "n")
            cmdAbsent.ExecuteNonQuery()
          End If

        Next
        For Each Student As ListViewItem In lvLate.Items
          If Student.SubItems(3).Text.Length = 0 Then
            Dim cmdLate As MySqlCommand = DBA.CreateCommand(T.InsertAbsence)
            cmdLate.Transaction = MySQLTran
            cmdLate.Parameters.AddWithValue("?IdUczen", Student.Text)
            cmdLate.Parameters.AddWithValue("?IdLekcja", CType(cbGodzina.SelectedItem, CbItem).ID)
            cmdLate.Parameters.AddWithValue("?Typ", "s")
            cmdLate.Parameters.AddWithValue("?Data", DataLekcji.ToShortDateString)
            cmdLate.ExecuteNonQuery()
          ElseIf Student.SubItems(3).Text.Length > 0 AndAlso Student.SubItems(4).Text <> "s" Then
            Dim cmdAbsent As MySqlCommand = DBA.CreateCommand(T.UpdateAbsence)
            cmdAbsent.Transaction = MySQLTran
            cmdAbsent.Parameters.AddWithValue("?ID", Student.SubItems(3).Text)
            cmdAbsent.Parameters.AddWithValue("?Typ", "s")
            cmdAbsent.ExecuteNonQuery()
          End If
        Next
      Else
        cmd.Parameters.AddWithValue("?Nr", 0)
        cmd.Parameters.AddWithValue("?StatusLekcji", 0)
        cmd.ExecuteNonQuery()
      End If

      MySQLTran.Commit()
      RaiseEvent NewAdded(cmd.LastInsertedId.ToString)
      Return True

    Catch myex As MySqlException
      MySQLTran.Rollback()
      MessageBox.Show(myex.Message & vbCr & myex.Number)
    Catch ex As Exception
      MySQLTran.Rollback()
      MessageBox.Show(ex.Message)
      'Return False
    End Try
    'Return False
  End Function

  'Private Sub cbZastepca_SelectedIndexChanged(sender As Object, e As EventArgs)
  '  If Not IsRefreshMode Then OK_Button.Enabled = True
  'End Sub

  Private Sub txtTemat_TextChanged(sender As Object, e As EventArgs) Handles txtTemat.TextChanged
    If Not IsRefreshMode Then OK_Button.Enabled = True
  End Sub

  Private Sub nudNr_ValueChanged(sender As Object, e As EventArgs) Handles nudNr.ValueChanged
    If Not Me.Created Then Exit Sub
    If Not IsRefreshMode Then OK_Button.Enabled = True
    Lp = CType(nudNr.Value, Integer)
  End Sub

  Public Sub chkStatus_CheckedChanged(sender As Object, e As EventArgs) Handles chkStatus.CheckedChanged
    If Not IsRefreshMode Then OK_Button.Enabled = True

    If Not chkStatus.Checked Then
      nudNr.Enabled = True
      nudNr.Value = Lp
      GetSubstitute(IdLekcja)
      GetData(IdLekcja)
    Else
      chkZastepstwo.Checked = False
      chkZastepstwo.Enabled = False
      lblZastepstwo.Enabled = False
      lblZastepstwo.Text = ""
      lblZastepstwo.BackColor = CType(IIf(chkZastepstwo.Checked, SystemColors.Info, SystemColors.Control), Color)
      nudNr.Enabled = False
      'nudNr.Value = 0
      cmdMoveBack.Enabled = False
      cmdMoveDown.Enabled = False
      cmdMove.Enabled = False
      cmdMoveOff.Enabled = False
      ClearLV(Not chkStatus.Checked)
    End If
  End Sub

End Class
