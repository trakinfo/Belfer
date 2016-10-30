Imports System.Drawing.Printing
Imports System.Xml
Public Class frmWykazNdst
  Public Filter, Typ As String
  Private DS As DataSet ', dtIndividualStaff As DataTable
  Public Event NewRow()
  Private Offset(3), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    If Typ = "P" Then
      MainForm.WykazNdstToolStripMenuItem.Enabled = True
    Else
      MainForm.WykazNdpToolStripMenuItem.Enabled = True
    End If
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    'MainForm.WykazNdstToolStripMenuItem.Enabled = True
    If Typ = "P" Then
      MainForm.WykazNdstToolStripMenuItem.Enabled = True
    Else
      MainForm.WykazNdpToolStripMenuItem.Enabled = True
    End If
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData(IdNauczyciel As String)
    Dim S As New StatystykaSQL, DBA As New DataBaseAction, CH As New CalcHelper, U As New UzasadnienieSQL
    Dim Semestr As RadioButton
    Semestr = Me.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    Dim IdSchool As String = My.Settings.IdSchool
    Dim SchoolYear As String = My.Settings.SchoolYear
    Dim StartDate, EndDate As Date
    StartDate = CH.StartDateOfSchoolYear(SchoolYear)
    'EndDate = CType(IIf(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(SchoolYear)), Date)
    EndDate = CType(IIf(Semestr.Tag.ToString = "S", CH.StartDateOfSemester2(CType(SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(SchoolYear)), Date)
    Dim ScoreListContent As List(Of DataTableContent) = Nothing
    If Typ = "P" Then
      'DS = DBA.GetDataSet(S.SelectPrzedmiot(IdSchool, SchoolYear, IdNauczyciel) & S.SelectStudentNdst(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel) & S.SelectResult(IdSchool, SchoolYear, Semestr.Tag.ToString, IIf(Semestr.Tag.ToString = "S", "C1", "C2").ToString, IdNauczyciel) & S.SelectAbsence(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel) & S.SelectLekcjaBezZastepstw(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel) & S.SelectZastepstwo(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel) & S.SelectAbsenceZastepstwo(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel) & U.SelectUzasadnienie(IdNauczyciel, SchoolYear, Semestr.Tag.ToString))

      'DS.Tables(0).TableName = "Przedmiot"
      'DS.Tables(1).TableName = "Student"
      'DS.Tables(2).TableName = "Wyniki"
      'DS.Tables(3).TableName = "Absence"
      'DS.Tables(4).TableName = "LekcjaBezZastepstw"
      'DS.Tables(5).TableName = "Zastepstwo"
      'DS.Tables(6).TableName = "AbsenceZastepstwo"
      'DS.Tables(7).TableName = "Uzasadnienie"

      ScoreListContent = New List(Of DataTableContent) From
{
  New DataTableContent With {.TableName = "Przedmiot", .SelectString = S.SelectPrzedmiot(IdSchool, SchoolYear, IdNauczyciel)},
  New DataTableContent With {.TableName = "Student", .SelectString = S.SelectStudentNdst(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel)},
  New DataTableContent With {.TableName = "Wyniki", .SelectString = S.SelectResult(IdSchool, SchoolYear, Semestr.Tag.ToString, IIf(Semestr.Tag.ToString = "S", "C1", "C2").ToString, IdNauczyciel)},
  New DataTableContent With {.TableName = "Absence", .SelectString = S.SelectAbsence(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel)},
  New DataTableContent With {.TableName = "LekcjaBezZastepstw", .SelectString = S.SelectLekcjaBezZastepstw(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel)},
  New DataTableContent With {.TableName = "Zastepstwo", .SelectString = S.SelectZastepstwo(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel)},
  New DataTableContent With {.TableName = "AbsenceZastepstwo", .SelectString = S.SelectAbsenceZastepstwo(IdSchool, SchoolYear, Semestr.Tag.ToString, StartDate, EndDate, IdNauczyciel)},
    New DataTableContent With {.TableName = "Uzasadnienie", .SelectString = U.SelectUzasadnienie(IdNauczyciel, SchoolYear, Semestr.Tag.ToString)}
}
    Else
      'DS = DBA.GetDataSet(S.SelectStudentZ(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel) & S.SelectAbsenceZ(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel, StartDate, EndDate) & S.SelectLekcjaZ(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel, StartDate, EndDate) & S.SelectLekcjaZG(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel, StartDate, EndDate) & U.SelectUzasadnienieZ(IdNauczyciel, SchoolYear, Semestr.Tag.ToString) & S.SelectGrupaPrzedmiot(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel))
      'DS.Tables(0).TableName = "Student"
      'DS.Tables(1).TableName = "Absence"
      'DS.Tables(2).TableName = "Lekcja"
      'DS.Tables(3).TableName = "LekcjaByGrupa"
      'DS.Tables(4).TableName = "Uzasadnienie"
      'DS.Tables(5).TableName = "GrupaPrzedmiotowa"

      ScoreListContent = New List(Of DataTableContent) From
{
New DataTableContent With {.TableName = "Student", .SelectString = S.SelectStudentZ(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel)},
New DataTableContent With {.TableName = "Absence", .SelectString = S.SelectAbsenceZ(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel, StartDate, EndDate)},
New DataTableContent With {.TableName = "Lekcja", .SelectString = S.SelectLekcjaZ(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel, StartDate, EndDate)},
New DataTableContent With {.TableName = "LekcjaByGrupa", .SelectString = S.SelectLekcjaZG(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel, StartDate, EndDate)},
New DataTableContent With {.TableName = "Uzasadnienie", .SelectString = U.SelectUzasadnienieZ(IdNauczyciel, SchoolYear, Semestr.Tag.ToString)},
New DataTableContent With {.TableName = "GrupaPrzedmiotowa", .SelectString = S.SelectGrupaPrzedmiot(IdSchool, SchoolYear, Semestr.Tag.ToString, IdNauczyciel)}
}

    End If
    DS = New DataSet
    For Each T In ScoreListContent
      Dim DT As DataTable = DBA.GetDataTable(T.SelectString)
      DT.TableName = T.TableName
      DS.Tables.Add(DT)
    Next
  End Sub
  Private Sub frmWykazNdst_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Dim SeekCriteria() As String = If(Typ = "P", New String() {"Nazwisko i imię ucznia", "Przedmiot", "Klasa"}, New String() {"Nazwisko i imię ucznia", "Klasa"})
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    ListViewConfig(lvStudent)
    AddListViewColumn(lvStudent)
    If Typ = "Z" Then Text = "Wykaz uczniów z obniżonymi ocenami zachowania"
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    lblRecord.Text = ""
    Dim CH As New CalcHelper
    'Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    If Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year) Then
      rbSemestr.Checked = True
    Else
      rbRokSzkolny.Checked = True
    End If
    'FillBelfer(cbNauczyciel)
    'FetchData()
  End Sub
  Private Sub FillBelfer(ByVal cb As ComboBox, Semestr As String)
    cb.Items.Clear()
    lvStudent.Items.Clear()
    Dim FCB As New FillComboBox, S As New StatystykaSQL, SelectString As String
    SelectString = If(Typ = "P", S.SelectBelfer(My.Settings.IdSchool, My.Settings.SchoolYear, Semestr), S.SelectBelferZ(My.Settings.IdSchool, My.Settings.SchoolYear, Semestr))
    FCB.AddComboBoxComplexItems(cb, SelectString)
    Dim SH As New SeekHelper
    If My.Settings.IdBelfer.Length > 0 Then SH.FindComboItem(Me.cbNauczyciel, CType(My.Settings.IdBelfer, Integer))
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub ListViewConfig(LV As ListView)
    With LV
      .View = View.Details
      .FullRowSelect = True
      .GridLines = True
      '.ColorGridLines = Color.Beige
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False

      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddListViewColumn(lv As ListView)
    With lv
      If Typ = "P" Then
        .Columns.Add("IdStudent", 0, HorizontalAlignment.Center)
        .Columns.Add("Nazwisko i imię", 200, HorizontalAlignment.Left)
        .Columns.Add("Klasa", 150, HorizontalAlignment.Center)
        .Columns.Add("Ocena", 150, HorizontalAlignment.Left)
        .Columns.Add("Średnia", 100, HorizontalAlignment.Center)
        .Columns.Add("Absencja", 100, HorizontalAlignment.Center)
        '.Columns.Add("Uzasadnienie", 120, HorizontalAlignment.Center)
        .Columns.Add(New ColumnHeader With {.Name = "Uz", .Text = "Uzasadnienie", .Width = 120, .TextAlign = HorizontalAlignment.Center})
        .Columns.Add("IdPrzedmiot", 0)
        '.Columns.Add("IdWynik", 0)
      Else
        .Columns.Add("IdStudent", 0, HorizontalAlignment.Center)
        .Columns.Add("Nazwisko i imię", 200, HorizontalAlignment.Left)
        .Columns.Add("Klasa", 150, HorizontalAlignment.Center)
        .Columns.Add("Ocena", 150, HorizontalAlignment.Left)
        '.Columns.Add("Średnia", 100, HorizontalAlignment.Center)
        .Columns.Add("Absencja", 100, HorizontalAlignment.Center)
        '.Columns.Add("Uzasadnienie", 120, HorizontalAlignment.Center)
        .Columns.Add(New ColumnHeader With {.Name = "Uz", .Text = "Uzasadnienie", .Width = 120, .TextAlign = HorizontalAlignment.Center})

      End If
    End With

  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub GetData(lv As ListView)
    'Dim DBA As New DataBaseAction
    Try
      lv.Items.Clear()
      lv.Groups.Clear()
      'EnableButton(False)
      lv.BeginUpdate()
      If Typ = "P" Then
        For Each Przedmiot As DataRow In DS.Tables("Przedmiot").Rows 'DS.Tables("Przedmiot").Select("Nauczyciel=" & Belfer)
          LvNewItem(lv, Przedmiot.Item("ID").ToString, Przedmiot.Item("Nazwa").ToString)
        Next
      Else
        Dim IdPrzedmiot As String = DS.Tables("Student").DefaultView.ToTable(True, "IdPrzedmiot").Rows(0).Item(0).ToString
        LvNewItem(lv, IdPrzedmiot)
      End If
      lv.EndUpdate()
      lv.Columns("Uz").Width = CInt(IIf(lv.Items.Count > 10, 100, 120))
      If lv.Items.Count > 0 Then
        lv.Enabled = True
        'cmdPrint.Enabled = True
      Else
        lv.Enabled = False
        'cmdPrint.Enabled = False
      End If
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub RefreshData()
    Dim Wait As New dlgWait
    Wait.lblInfo.Text = "Trwa pobieranie danych ..."
    Cursor = Cursors.WaitCursor
    Wait.Show()
    Application.DoEvents()
    EnableButton(False)
    FetchData(CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString)
    GetData(lvStudent)
    Wait.Hide()
    Cursor = Cursors.Default
  End Sub
  Private Sub LvNewItem(ByVal LV As ListView, ByVal Przedmiot As String, ByVal Grupa As String)
    Dim NG As New ListViewGroup(Grupa, Grupa)
    NG.HeaderAlignment = HorizontalAlignment.Left
    LV.Groups.Add(NG)
    LV.Groups(Grupa).Tag = Przedmiot
    For Each Student As DataRow In DS.Tables("Student").Select("IdPrzedmiot=" & Przedmiot & Filter)
      Dim NewItem As New ListViewItem(Student.Item("IdStudent").ToString, NG)
      NewItem.Tag = Student.Item("Owner").ToString
      NewItem.UseItemStyleForSubItems = False
      NewItem.SubItems.Add(Student.Item("Student").ToString)
      NewItem.SubItems.Add(Student.Item("Nazwa_Klasy").ToString).Tag = Student.Item("Klasa").ToString
      NewItem.SubItems.Add(Student.Item("Ocena").ToString).Tag = Student.Item("IdWynik").ToString
      NewItem.SubItems.Add(ComputeAvg(Przedmiot, Student.Item("IdStudent").ToString))
      NewItem.SubItems.Add(ComputeAbsence(Student.Item("IdObsada").ToString, Student.Item("IdStudent").ToString, Student.Item("Klasa").ToString, Student.Item("Przedmiot").ToString, CType(Student.Item("DataAktywacji"), Date)).ProcentNieobecnosci)
      Dim ReasonContents As ReasonContents = GetReasonContents(Student.Item("IdWynik").ToString)
      Dim StatusColor As Color = Color.Black
      If CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Odrzucone Then
        StatusColor = Color.Red
      ElseIf CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Zatwierdzone Then
        StatusColor = Color.Green
      ElseIf CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Przekazane Then
        StatusColor = Color.Blue
      End If
      NewItem.SubItems.Add(ReasonContents.Status.ToString).Tag = ReasonContents
      NewItem.SubItems(6).Name = "Uz"
      NewItem.SubItems(6).ForeColor = StatusColor
      NewItem.SubItems.Add(Przedmiot)
      'NewItem.SubItems.Add(Student.Item("IdWynik").ToString)

      LV.Items.Add(NewItem)
    Next
    lblRecord.Text = "0 z " & LV.Items.Count
    If LV.Groups(Grupa).Items.Count = 0 Then LV.Groups.Remove(NG)
  End Sub

  Private Sub LvNewItem(ByVal LV As ListView, Przedmiot As String)
    Dim NG As New ListViewGroup("Zachowanie", "Zachowanie")
    NG.HeaderAlignment = HorizontalAlignment.Left
    LV.Groups.Add(NG)
    LV.Groups("Zachowanie").Tag = Przedmiot
    LV.ShowGroups = False
    For Each Student As DataRow In DS.Tables("Student").Select(Filter)
      Dim NewItem As New ListViewItem(Student.Item("IdStudent").ToString, NG)
      NewItem.Tag = Student.Item("Owner").ToString
      NewItem.UseItemStyleForSubItems = False
      NewItem.SubItems.Add(Student.Item("Student").ToString)
      NewItem.SubItems.Add(Student.Item("Nazwa_Klasy").ToString).Tag = Student.Item("Klasa").ToString
      NewItem.SubItems.Add(Student.Item("Ocena").ToString).Tag = Student.Item("IdWynik").ToString
      'NewItem.SubItems.Add(ComputeAvg(Przedmiot, Student.Item("IdStudent").ToString))
      NewItem.SubItems.Add(ComputeAbsence(Student.Item("IdStudent").ToString, Student.Item("Klasa").ToString, CType(Student.Item("DataAktywacji"), Date)).ProcentNieobecnosci)
      Dim ReasonContents As ReasonContents = GetReasonContents(Student.Item("IdWynik").ToString)
      Dim StatusColor As Color = Color.Black
      If CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Odrzucone Then
        StatusColor = Color.Red
      ElseIf CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Zatwierdzone Then
        StatusColor = Color.Green
      ElseIf CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Przekazane Then
        StatusColor = Color.Blue
      End If
      NewItem.SubItems.Add(ReasonContents.Status.ToString).Tag = ReasonContents
      NewItem.SubItems(5).Name = "Uz"
      NewItem.SubItems(5).ForeColor = StatusColor
      'NewItem.SubItems.Add(Przedmiot)
      'NewItem.SubItems.Add(Student.Item("IdWynik").ToString)

      LV.Items.Add(NewItem)
    Next
    lblRecord.Text = "0 z " & LV.Items.Count
    'If LV.Groups(Grupa).Items.Count = 0 Then LV.Groups.Remove(NG)
  End Sub

  Private Function GetReasonContents(IdWynik As String) As ReasonContents
    Dim DR As DataRow() = DS.Tables("Uzasadnienie").Select("IdWynik=" & IdWynik)
    If DR.GetLength(0) > 0 Then
      Return New ReasonContents With {.Content = DR(0).Item("Tresc").ToString, .Status = CType(DR(0).Item("Status"), GlobalValues.ReasonStatus), .Owner = DR(0).Item("Owner").ToString, .ID = CType(DR(0).Item("ID"), Integer)}
    Else
      Return New ReasonContents With {.Content = "", .Status = GlobalValues.ReasonStatus.Brak}
    End If
  End Function
  Private Sub cbNauczyciel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbNauczyciel.SelectedIndexChanged
    'Dim Wait As New dlgWait
    'Wait.lblInfo.Text = "Trwa pobieranie danych ..."
    'Cursor = Cursors.WaitCursor
    'Wait.Show()
    'Application.DoEvents()
    'lvWyniki.Items.Clear()
    'lvWyniki.Enabled = False
    RefreshData()
    'Wait.Hide()
    'Cursor = Cursors.Default
  End Sub

  Private Sub cbNauczyciel_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbNauczyciel.SelectionChangeCommitted
    My.Settings.IdBelfer = CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub EnableButton(Value As Boolean)
    cmdAddNew.Enabled = Value
    cmdDelete.Enabled = Value
    cmdEdit.Enabled = Value
    cmdExport.Enabled = Value
    cmdSend.Enabled = Value
    cmdPrint.Enabled = Value
  End Sub

  Private Sub rbSemestr_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemestr.CheckedChanged, rbRokSzkolny.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    'If cbNauczyciel.SelectedItem IsNot Nothing Then cbNauczyciel_SelectedIndexChanged(Nothing, Nothing)
    FillBelfer(cbNauczyciel, CType(sender, RadioButton).Tag.ToString)
  End Sub

  Private Sub lvStudent_DoubleClick(sender As Object, e As EventArgs) Handles lvStudent.DoubleClick
    If GlobalValues.AppUser.Login = lvStudent.SelectedItems(0).Tag.ToString AndAlso CType(lvStudent.SelectedItems(0).SubItems("Uz").Tag, ReasonContents).Status <> GlobalValues.ReasonStatus.Brak Then
      Dim dlg As New dlgUzasadnienie
      With dlg
        .lblStudent.Text = "Uczeń: " & lvStudent.SelectedItems(0).SubItems(1).Text
        .txtUzasadnienie.Text = CType(lvStudent.SelectedItems(0).SubItems("Uz").Tag, ReasonContents).Content
        .txtUzasadnienie.ReadOnly = True
        If Typ = "P" Then
          .Text = "Uzasadnienie oceny przedmiotowej - podgląd (tylko do odczytu)"
          .lblPrzedmiot.Text = "Przedmiot: " & lvStudent.SelectedItems(0).Group.Name
          ListViewConfig(.lvWyniki)
          With .lvWyniki
            .Columns.Add("IdWynik", 0)
            .Columns.Add("Ocena", 70, HorizontalAlignment.Center)
            .Columns.Add("Opis oceny", 630, HorizontalAlignment.Left)
          End With
          GetResult(.lvWyniki, lvStudent.SelectedItems(0).SubItems(7).Text, lvStudent.SelectedItems(0).Text)
        Else
          .Text = "Uzasadnienie oceny zachowania - podgląd (tylko do odczytu)"
          .lblPrzedmiot.Visible = False
          .lvWyniki.Visible = False
          .Height -= .lvWyniki.Height
        End If
        .OK_Button.Enabled = False
        .OK_Button.Visible = False
        '.Cancel_Button.Enabled = False
        '.Cancel_Button.Visible = False
        .Cancel_Button.Text = "Zamknij"
        .ShowDialog()
      End With
    End If
  End Sub
  Private Sub lvStudent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvStudent.ItemSelectionChanged
    'lvWyniki.Items.Clear()
    'lvWyniki.Enabled = False
    If e.IsSelected Then
      'GetResult(lvWyniki, e.Item.SubItems(7).Text, e.Item.Text)
      'cmdPrint.Enabled = True
      If GlobalValues.AppUser.Login = e.Item.Tag.ToString Then
        cmdAddNew.Enabled = If(CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Brak, True, False)
        cmdEdit.Enabled = If(CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Wprowadzone OrElse CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Odrzucone, True, False)
        cmdDelete.Enabled = If(CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Wprowadzone OrElse CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Odrzucone, True, False)
        cmdSend.Enabled = If(CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Wprowadzone OrElse CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Odrzucone, True, False)
        cmdExport.Enabled = If(CType(e.Item.SubItems("Uz").Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Brak, False, True)
        cmdPrint.Enabled = True
      Else
        EnableButton(False)
      End If
      lblRecord.Text = e.ItemIndex + 1 & " z " & e.Item.ListView.Items.Count
    Else
      'cmdPrint.Enabled = False
      lblRecord.Text = "0 z " & e.Item.ListView.Items.Count
      EnableButton(False)
    End If

  End Sub
  Private Sub GetResult(lv As ListView, IdPrzedmiot As String, IdUczen As String)
    Try
      lv.Items.Clear()
      For Each Wynik As DataRow In DS.Tables("Wyniki").Select("IdPrzedmiot=" & IdPrzedmiot & " AND IdUczen=" & IdUczen)
        Dim NewItem As New ListViewItem(Wynik.Item("IdWynik").ToString)
        NewItem.UseItemStyleForSubItems = True
        NewItem.SubItems.Add(Wynik.Item("Ocena").ToString)
        NewItem.SubItems.Add(Wynik.Item("OpisOceny").ToString)
        lv.Items.Add(NewItem)
      Next
      lv.Columns(2).Width = CInt(IIf(lv.Items.Count > 10, 611, 630))
      If lv.Items.Count > 0 Then
        lv.Enabled = True
        'cmdPrint.Enabled = True
      Else
        lv.Enabled = False
        'cmdPrint.Enabled = False
      End If
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try

  End Sub
  Private Function ComputeAvg(IdPrzedmiot As String, IdUczen As String) As String
    Dim Suma, SumaWag As Single
    For Each Wynik As DataRow In DS.Tables("Wyniki").Select("IdPrzedmiot=" & IdPrzedmiot & " AND IdUczen=" & IdUczen)
      If CType(Wynik.Item("WagaOceny"), Single) > 0 Then
        Suma += CType(Wynik.Item("WagaOceny"), Single) * CType(Wynik.Item("WagaKolumny"), Single)
        SumaWag += CType(Wynik.Item("WagaKolumny"), Single)
      End If
    Next
    Return IIf(Suma > 0, Math.Round(Suma / SumaWag, 2), 0).ToString
  End Function
  Private Overloads Function ComputeAbsence(IdObsada As String, IdUczen As String, Klasa As String, Przedmiot As String, DataAktywacji As Date) As Absencja
    Dim Frekwencja As Single ', CH As New CalcHelper
    Dim LiczbaNb, LiczbaLekcji As Integer

    LiczbaLekcji = CType(DS.Tables("LekcjaBezZastepstw").Compute("COUNT(IdLekcja)", "Data>=#" & DataAktywacji & "# AND IdObsada=" & IdObsada), Integer)
    LiczbaLekcji += DS.Tables("Zastepstwo").Select("Data>=#" & DataAktywacji & "# AND Klasa=" & Klasa & " AND Przedmiot=" & Przedmiot).GetLength(0)
    If LiczbaLekcji = 0 Then Return New Absencja With {.ProcentNieobecnosci = "-", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = "?"}
    LiczbaNb = CType(DS.Tables("Absence").Compute("COUNT(IdUczen)", "Data>=#" & DataAktywacji & "# AND IdObsada=" & IdObsada & " AND IdUczen=" & IdUczen), Integer)

    For Each R As DataRow In DS.Tables("Zastepstwo").DefaultView.ToTable(True).Select("Data>=#" & DataAktywacji & "# AND Klasa=" & Klasa & " AND Przedmiot=" & Przedmiot) ' 
      LiczbaNb += CType(DS.Tables("AbsenceZastepstwo").Compute("COUNT(IdUczen)", "Data>=#" & DataAktywacji & "# AND IdObsada=" & R.Item("IdObsada").ToString & " AND IdUczen=" & IdUczen), Integer)
    Next
    Frekwencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
    Frekwencja = CType(IIf(Frekwencja > 100, 100, Frekwencja), Single)
    'Return Frekwencja.ToString & "%"
    Return New Absencja With {.ProcentNieobecnosci = Frekwencja.ToString & "%", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = LiczbaNb.ToString}
  End Function
  Private Overloads Function ComputeAbsence(IdUczen As String, Klasa As String, DataAktywacji As Date) As Absencja
    Dim Frekwencja As Single ', CH As New CalcHelper
    Dim LiczbaNb, LiczbaLekcji As Integer
    LiczbaLekcji = CType(DS.Tables("Lekcja").Compute("Count(ID)", "Data>=#" & DataAktywacji & "# AND Klasa=" & Klasa), Integer)
    For Each G As DataRow In DS.Tables("GrupaPrzedmiotowa").Select("IdUczen=" & IdUczen)
      LiczbaLekcji += CType(DS.Tables("LekcjaByGrupa").Compute("Count(ID)", "Data>=#" & DataAktywacji & "# AND Klasa=" & Klasa & " AND Przedmiot=" & G.Item("Przedmiot").ToString), Integer)
    Next
    If LiczbaLekcji = 0 Then Return New Absencja With {.ProcentNieobecnosci = "-", .LiczbaLekcji = "0", .LiczbaNieobecnosci = "?"}

    LiczbaNb = CType(DS.Tables("Absence").Compute("Count(ID)", "Data>=#" & DataAktywacji & "# AND IdUczen=" & IdUczen), Integer)
    Frekwencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
    Frekwencja = CType(IIf(Frekwencja > 100, 100, Frekwencja), Single)
    Return New Absencja With {.ProcentNieobecnosci = Frekwencja.ToString & "%", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = LiczbaNb.ToString}
  End Function

  'Private Function ComputeAbsence(Przydzial As StudentAllocation) As Absencja
  '  Try
  '    Dim IdUczen As Integer = Przydzial.ID
  '    Dim DataAktywacji, DataDeaktywacji As Date
  '    DataAktywacji = Przydzial.DataAktywacji
  '    DataDeaktywacji = Przydzial.DataDeaktywacji
  '    If Przydzial.NauczanieIndywidualne IsNot Nothing Then
  '      'Dim IdPrzedmiot As Integer = CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ObjectID
  '      For Each P As IndividualStaff In Przydzial.NauczanieIndywidualne.SchoolObject
  '        'If P.ObjectID = IdPrzedmiot Then DataDeaktywacji = P.StartDate.AddDays(-1)
  '      Next
  '    End If
  '    Dim Frekwencja As Single, LiczbaLekcji, LiczbaNb As Integer
  '    LiczbaLekcji = CType(DS.Tables("LekcjaBezZastepstw").Compute("COUNT(ID)", "Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# is null)"), Integer)
  '    LiczbaLekcji += CType(DS.Tables("Zastepstwo").Compute("COUNT(ID)", "Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# IS NULL)"), Integer)
  '    If LiczbaLekcji = 0 Then Return New Absencja With {.ProcentNieobecnosci = "-", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = "?"}
  '    LiczbaNb = CType(DS.Tables("Absence").Compute("COUNT(IdUczen)", "IdUczen=" & IdUczen & " AND Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# is null)"), Integer)
  '    LiczbaNb += CType(DS.Tables("AbsenceZastepstwo").Compute("COUNT(IdUczen)", "IdUczen=" & IdUczen & " AND Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# IS NULL)"), Integer)
  '    Frekwencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
  '    Frekwencja = CType(IIf(Frekwencja > 100, 100, Frekwencja), Single)
  '    Return New Absencja With {.ProcentNieobecnosci = Frekwencja.ToString & "%", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = LiczbaNb.ToString}
  '  Catch ex As Exception
  '    MessageBox.Show(ex.Message)
  '    Return Nothing
  '  End Try
  'End Function


  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Dim dlg As New dlgUzasadnienie
    With dlg
      .lblStudent.Text = "Uczeń: " & lvStudent.SelectedItems(0).SubItems(1).Text
      If Typ = "P" Then
        .Text = "Uzasadnienie oceny przedmiotowej"
        .lblPrzedmiot.Text = "Przedmiot: " & lvStudent.SelectedItems(0).Group.Name
        ListViewConfig(.lvWyniki)
        With .lvWyniki
          .Columns.Add("IdWynik", 0)
          .Columns.Add("Ocena", 70, HorizontalAlignment.Center)
          .Columns.Add("Opis oceny", 630, HorizontalAlignment.Left)
        End With
        GetResult(.lvWyniki, lvStudent.SelectedItems(0).SubItems(7).Text, lvStudent.SelectedItems(0).Text)
      Else
        .Text = "Uzasadnienie oceny zachowania"
        .lblPrzedmiot.Visible = False
        .lvWyniki.Visible = False
        .Height -= .lvWyniki.Height
      End If

      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        Try
          Dim U As New UzasadnienieSQL, DBA As New DataBaseAction, cmd As MySqlCommand, IdStudent As String = lvStudent.SelectedItems(0).Text
          cmd = DBA.CreateCommand(U.InsertUzasadnienie)
          cmd.Parameters.AddWithValue("?IdWynik", lvStudent.SelectedItems(0).SubItems(3).Tag.ToString)
          cmd.Parameters.AddWithValue("?Tresc", .txtUzasadnienie.Text.Trim)
          cmd.Parameters.AddWithValue("?Status", 0)
          cmd.ExecuteNonQuery()
          RefreshData()
          Dim SH As New SeekHelper
          SH.FindListViewItem(lvStudent, IdStudent)
        Catch mex As MySqlException
          MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
          MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

      End If
    End With

  End Sub

  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    Dim dlg As New dlgUzasadnienie
    With dlg
      .lblStudent.Text = "Uczeń: " & lvStudent.SelectedItems(0).SubItems(1).Text
      .txtUzasadnienie.Text = CType(lvStudent.SelectedItems(0).SubItems("Uz").Tag, ReasonContents).Content
      If Typ = "P" Then
        .Text = "Uzasadnienie oceny przedmiotowej"
        .lblPrzedmiot.Text = "Przedmiot: " & lvStudent.SelectedItems(0).Group.Name
        ListViewConfig(.lvWyniki)
        With .lvWyniki
          .Columns.Add("IdWynik", 0)
          .Columns.Add("Ocena", 70, HorizontalAlignment.Center)
          .Columns.Add("Opis oceny", 630, HorizontalAlignment.Left)
        End With
        GetResult(.lvWyniki, lvStudent.SelectedItems(0).SubItems(7).Text, lvStudent.SelectedItems(0).Text)
      Else
        .Text = "Uzasadnienie oceny zachowania"
        .lblPrzedmiot.Visible = False
        .lvWyniki.Visible = False
        .Height -= .lvWyniki.Height
      End If

      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        Try
          Dim U As New UzasadnienieSQL, DBA As New DataBaseAction, cmd As MySqlCommand, IdStudent As String = lvStudent.SelectedItems(0).Text
          cmd = DBA.CreateCommand(U.UpdateUzasadnienie)
          cmd.Parameters.AddWithValue("?IdWynik", lvStudent.SelectedItems(0).SubItems(3).Tag.ToString)
          cmd.Parameters.AddWithValue("?Tresc", .txtUzasadnienie.Text.Trim)
          cmd.ExecuteNonQuery()
          RefreshData()
          Dim SH As New SeekHelper
          SH.FindListViewItem(lvStudent, IdStudent)
        Catch mex As MySqlException
          MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
          MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

      End If
    End With
  End Sub

  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć uzasadnienie oceny dla wskazanego ucznia?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, U As New UzasadnienieSQL
      Try
        Dim cmd As MySqlCommand = DBA.CreateCommand(U.DeleteUzasadnienie)
        cmd.Parameters.AddWithValue("?IdWynik", lvStudent.SelectedItems(0).SubItems(3).Tag.ToString)
        cmd.ExecuteNonQuery()
        RefreshData()
      Catch mex As MySqlException
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub

  Private Sub cmdSend_Click(sender As Object, e As EventArgs) Handles cmdSend.Click
    If MessageBox.Show("Po przekazaniu uzasadnienia nie będzie możliwości jego edycji ani usunięcia. Czy na pewno chcesz przekazać uzasadnienie oceny?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, U As New UzasadnienieSQL
      Try
        Dim cmd As MySqlCommand = DBA.CreateCommand(U.ChangeStatus)
        cmd.Parameters.AddWithValue("?IdWynik", lvStudent.SelectedItems(0).SubItems(3).Tag.ToString)
        cmd.Parameters.AddWithValue("?Status", GlobalValues.ReasonStatus.Przekazane)
        cmd.ExecuteNonQuery()
        RefreshData()
      Catch mex As MySqlException
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub

  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię ucznia"
        Filter = "Student LIKE '" & Me.txtSeek.Text + "%'"
      Case "Przedmiot"
        Filter = "Nazwa LIKE '" & Me.txtSeek.Text + "%'"
      Case "Klasa"
        Filter = "Nazwa_Klasy LIKE '" & Me.txtSeek.Text + "%'"
    End Select
    If Typ = "P" Then Filter = " AND " + Filter
    If cbNauczyciel.SelectedItem IsNot Nothing Then Me.GetData(lvStudent)
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub
  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Dim PP As New dlgPrintPreview, dlg As New dlgPrintOption
    PP.Doc = New PrintReport(Nothing)
    PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
    PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
    PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin
    PP.Width = 1000
    RemoveHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
    AddHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
    RemoveHandler NewRow, AddressOf PP.NewRow
    AddHandler NewRow, AddressOf PP.NewRow
    RemoveHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    AddHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    Dim Semestr As RadioButton
    Semestr = Me.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    Dim Klasyfikacja As String = "Klasyfikacja " & If(Semestr.Tag.ToString = "S", "śródroczna", "roczna")
    If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
      If dlg.rbStudentList.Checked Then
        RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_StudentList_PrintPage
        AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_StudentList_PrintPage
        If Typ = "P" Then
          PP.Doc.ReportHeader = New String() {"Wykaz uczniów z ocenami niedostatecznymi (nieklasyfikowanych)", "Nauczyciel: " & cbNauczyciel.Text, Klasyfikacja}
        Else
          PP.Doc.ReportHeader = New String() {"Wykaz uczniów z obniżonymi ocenami zachowania (nieklasyfikowanych)", "Wychowawca: " & cbNauczyciel.Text, Klasyfikacja}

        End If
      Else
        RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_ReasonContents_PrintPage
        AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_ReasonContents_PrintPage
        If Typ = "P" Then
          PP.Doc.ReportHeader = New String() {"Uzasadnienia ocen niedostatecznych (nieklasyfikowania)", "Nauczyciel: " & cbNauczyciel.Text, Klasyfikacja}
        Else
          PP.Doc.ReportHeader = New String() {"Uzasadnienia obniżonych ocen zachowania (nieklasyfikowania)", "Wychowawca: " & cbNauczyciel.Text, Klasyfikacja}
        End If
      End If
      PP.ShowDialog()
    End If
  End Sub
  Private Sub PrnDoc_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
    PH = New PrintHelper()
    If e.PrintAction = PrintAction.PrintToPrinter Then
      IsPreview = False
    Else
      IsPreview = True
    End If
  End Sub
  Private Sub PreviewModeChanged(PreviewMode As Boolean)
    IsPreview = PreviewMode
  End Sub
  Public Sub PrnDoc_StudentList_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    Dim Doc As PrintReport = CType(sender, PrintReport)
    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)
    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    Dim PrintHeight As Integer = e.MarginBounds.Bottom

    '---------------------------------------- Nagłówek i stopka ----------------------------
    PH.DrawHeader(x, y, PrintWidth)
    PH.DrawFooter(x, PrintHeight, PrintWidth)
    PageNumber += 1
    PH.DrawPageNumber("- " & PageNumber.ToString & " -", x, y, PrintWidth)
    If PageNumber = 1 Then
      y += LineHeight
      PH.DrawText(Doc.ReportHeader(0), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      y += HeaderLineHeight * 2
      PH.DrawText(Doc.ReportHeader(1), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      PH.DrawText(Doc.ReportHeader(2), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
      y += LineHeight * 2
    End If
    Dim ColSize As Integer = 100, Ocena As Integer = 150

    Dim StudentName As Integer = CInt((PrintWidth - Ocena - ColSize * 3)) '500

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Student", .Label = "Nazwisko i imię", .Size = StudentName},
        New Pole With {.Name = "Klasa", .Label = "Klasa", .Size = ColSize},
        New Pole With {.Name = "Ocena", .Label = "Ocena", .Size = Ocena},
        New Pole With {.Name = "Avg", .Label = "Średnia ocen", .Size = ColSize},
        New Pole With {.Name = "Absencja", .Label = "Absencja", .Size = ColSize}
    }

    Do Until (y + LineHeight * CSng(5)) > PrintHeight Or Offset(0) > lvStudent.Groups.Count - 1
      'If lvStudent.Groups(Offset(0)).Items.Count > 0 Then
      If Offset(1) = 0 Then
        If Typ = "P" Then
          PH.DrawText(lvStudent.Groups(Offset(0)).Name.ToUpper, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight * 2, 0, Brushes.Black, False)
          y += LineHeight * CSng(2)
        End If

        Dim ColOffset As Integer = 0
        For Each Col In Kolumna
          PH.DrawText(Col.Label, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, Col.Size, LineHeight * CSng(2), 1, Brushes.Black)
          ColOffset += Col.Size
        Next
        y += LineHeight * CSng(2)

      End If
      Do Until (y + LineHeight) > PrintHeight Or Offset(1) > lvStudent.Groups(Offset(0)).Items.Count - 1
        ColSize = 0
        Dim i As Integer = 1 '0
        For Each Col In Kolumna
          PH.DrawText(lvStudent.Groups(Offset(0)).Items(Offset(1)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, CType(If(Col.Name = "Student", 0, 1), Byte), Brushes.Black)
          ColSize += Col.Size
          i += 1
        Next
        y += LineHeight
        Offset(1) += 1
      Loop
      If Offset(1) < lvStudent.Groups(Offset(0)).Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      Else
        Offset(1) = 0
      End If
      y += LineHeight
      'End If
      Offset(0) += 1

    Loop
    If Offset(0) < lvStudent.Groups.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
      PageNumber = 0
    End If
  End Sub

  Public Sub PrnDoc_ReasonContents_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    Dim Doc As PrintReport = CType(sender, PrintReport)
    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)
    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    Dim PrintHeight As Integer = e.MarginBounds.Bottom

    '---------------------------------------- Nagłówek i stopka ----------------------------
    PH.DrawHeader(x, y, PrintWidth)
    PH.DrawFooter(x, PrintHeight, PrintWidth)
    PageNumber += 1
    PH.DrawPageNumber("- " & PageNumber.ToString & " -", x, y, PrintWidth)
    If PageNumber = 1 Then
      y += LineHeight
      PH.DrawText(Doc.ReportHeader(0), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      y += HeaderLineHeight * 2
      PH.DrawText(Doc.ReportHeader(1), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      PH.DrawText(Doc.ReportHeader(2), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
      y += LineHeight * 2
    End If

    Do Until (y + LineHeight * CSng(8)) > PrintHeight Or Offset(0) > lvStudent.Groups.Count - 1
      If Offset(1) = 0 AndAlso Offset(2) = 0 AndAlso Offset(3) = 0 Then
        If Typ = "P" Then PH.DrawText(lvStudent.Groups(Offset(0)).Name.ToUpper, HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False, False, True)
      End If

      Dim Klasa As DataTable = New DataView(DS.Tables("Student"), "IdPrzedmiot=" & lvStudent.Groups(Offset(0)).Tag.ToString, "Nazwa_Klasy ASC", DataViewRowState.CurrentRows).ToTable.DefaultView.ToTable(True, "Nazwa_Klasy", "Klasa")
      Do Until (y + LineHeight * CSng(6)) > PrintHeight Or Offset(1) > Klasa.Rows.Count - 1
        If Offset(2) = 0 AndAlso Offset(3) = 0 Then
          y += LineHeight * CSng(2)
          PH.DrawText("Klasa " & Klasa.Rows(Offset(1)).Item("Nazwa_Klasy").ToString, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          y += LineHeight
          PH.DrawLine(x, y, x + PrintWidth, y)
          y += LineHeight * CSng(0.5)
        End If


        Dim Student As DataTable = New DataView(DS.Tables("Student"), "IdPrzedmiot=" & lvStudent.Groups(Offset(0)).Tag.ToString & " AND Klasa=" & Klasa.Rows(Offset(1)).Item("Klasa").ToString, "Student ASC", DataViewRowState.CurrentRows).ToTable.DefaultView.ToTable(False, "Student", "Ocena", "IdWynik")
        'Dim i As Integer = 1

        Do Until (y + LineHeight * CSng(3)) > PrintHeight Or Offset(2) > Student.Rows.Count - 1
          Dim StringLength As Single
          If Offset(3) = 0 Then
            Doc.Lp += 1
            PH.DrawText(String.Concat(Doc.Lp, ") ", Student.Rows(Offset(2)).Item("Student").ToString), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
            StringLength = e.Graphics.MeasureString(String.Concat(Doc.Lp, ")  ", Student.Rows(Offset(2)).Item("Student").ToString), TextFont).Width
            PH.DrawText(String.Concat(" ", Chr(151), " ", Student.Rows(Offset(2)).Item("Ocena").ToString), TextFont, x + StringLength, y, PrintWidth - StringLength, LineHeight, 0, Brushes.Black, False)
            y += LineHeight
          End If

          If DS.Tables("Uzasadnienie").Select("IdWynik=" & Student.Rows(Offset(2)).Item("IdWynik").ToString).GetLength(0) > 0 Then
            Dim Uzasadnienie As String() = DS.Tables("Uzasadnienie").Select("IdWynik=" & Student.Rows(Offset(2)).Item("IdWynik").ToString)(0).Item("Tresc").ToString.Split(" ".ToCharArray)
            StringLength = e.Graphics.MeasureString(String.Concat(Doc.Lp, ") "), TextFont).Width
            If PH.DrawWrappedText(Uzasadnienie, TextFont, Offset(3), x + StringLength, y, PrintWidth - StringLength, PrintHeight, LineHeight) = False Then
              e.HasMorePages = True
              RaiseEvent NewRow()
              Exit Sub
            Else
              Offset(3) = 0
              y += LineHeight
            End If
          End If
          Offset(2) += 1
        Loop
        If Offset(2) < Student.Rows.Count Then
          e.HasMorePages = True
          RaiseEvent NewRow()
          Exit Sub
        Else
          y -= LineHeight
          Offset(2) = 0
          Doc.Lp = 0
        End If
        Offset(1) += 1
      Loop

      y += LineHeight * CSng(2)
      If Offset(1) < Klasa.Rows.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      Else
        Offset(1) = 0
      End If
      'y += LineHeight
      'End If
      Offset(0) += 1

    Loop
    If Offset(0) < lvStudent.Groups.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
      PageNumber = 0
    End If
  End Sub

  Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
    Dim dlgSave As New SaveFileDialog

    With dlgSave
      .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

      .AddExtension = True
      .CheckFileExists = False
      .DefaultExt = "odt"
      .Filter = "Dokument tekstowy OpenDocument (*.odt)|*.odt|Pliki xml (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*"

      If .ShowDialog() = Windows.Forms.DialogResult.OK Then
        If ExportToOpenDocumentText(.FileName) Then MessageBox.Show("Export dokumentu zakończony powodzeniem.")

      End If
    End With
  End Sub


  Public Function ExportToOpenDocumentText(FileName As String) As Boolean
    Dim HeaderText As String = If(Typ = "P", "Uzasadnienia ocen niedostatecznych", "Uzasadnienia obniżonych ocen zachowania")
    Dim DocumentHeader() As String = New String() {HeaderText, "za " & If(rbSemestr.Checked, "I semestr roku szkolnego ", "rok szkolny ") & "2015/2016"}
    Try
      Dim XmlDoc As New Xml.XmlDocument()
      XmlDoc.Load(Application.StartupPath & "\Szablony\JustifyTemplate.xml")
      Dim Root As XmlElement = XmlDoc.DocumentElement
      Dim OfficeText As XmlNode = Root.LastChild.FirstChild

      For Each Line As String In DocumentHeader
        InsertParagraph(XmlDoc, OfficeText, "text:p", "P2", Line)
      Next
      InsertEmptyParagraph(XmlDoc, OfficeText)
      InsertParagraph(XmlDoc, OfficeText, "text:p", "P6", If(Typ = "P", "Nauczyciel: ", "Wychowawca: ") & cbNauczyciel.Text.ToUpper)
      InsertEmptyParagraph(XmlDoc, OfficeText)
      For Each G As ListViewGroup In lvStudent.Groups
        If Typ = "P" Then
          InsertParagraph(XmlDoc, OfficeText, "text:p", "P3", G.Name.ToUpper)
          InsertEmptyParagraph(XmlDoc, OfficeText)
        End If

        Dim Klasa As DataTable = New DataView(DS.Tables("Student"), "IdPrzedmiot=" & G.Tag.ToString, "Nazwa_Klasy ASC", DataViewRowState.CurrentRows).ToTable.DefaultView.ToTable(True, "Nazwa_Klasy", "Klasa")
        For Each K As DataRow In Klasa.Rows
          InsertParagraph(XmlDoc, OfficeText, "text:p", "P4", "Klasa: " & K.Item("Nazwa_Klasy").ToString)
          InsertParagraph(XmlDoc, OfficeText, "text:list", "L1", "")
          Dim Student As DataTable = New DataView(DS.Tables("Student"), "IdPrzedmiot=" & G.Tag.ToString & " AND Klasa=" & K.Item("Klasa").ToString, "Student ASC", DataViewRowState.CurrentRows).ToTable.DefaultView.ToTable(False, "Student", "Ocena", "IdWynik")
          Dim ListEntry As XmlNode = OfficeText.LastChild
          For Each S As DataRow In Student.Rows
            InsertListItem(XmlDoc, ListEntry)
            Dim ListItem As XmlNode = ListEntry.LastChild
            InsertParagraph(XmlDoc, ListItem, "text:p", "P5", String.Concat(S.Item("Student").ToString, " ", Chr(151), " ", S.Item("Ocena")))
            Dim ReasonContent() As DataRow = DS.Tables("Uzasadnienie").Select("IdWynik=" & S.Item("IdWynik").ToString)
            If ReasonContent.GetLength(0) > 0 Then
              InsertParagraph(XmlDoc, ListItem, "text:p", "Standard", ReasonContent(0).Item("Tresc").ToString())
              InsertEmptyParagraph(XmlDoc, ListItem)
            End If
          Next
          InsertEmptyParagraph(XmlDoc, OfficeText)
        Next
      Next

      Dim writer As XmlTextWriter = New XmlTextWriter(FileName, System.Text.Encoding.GetEncoding("UTF-8"))
      writer.Formatting = Formatting.Indented
      XmlDoc.Save(writer)
      writer.Close()
      Return True
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return False
    Finally
    End Try
  End Function
  Private Function AddAttrib(ByVal Nazwa As String, ByVal Value As String, ByVal XmlDoc As XmlDocument) As XmlAttribute
    Dim Attr As XmlAttribute
    Attr = XmlDoc.CreateAttribute(Nazwa, "urn:oasis:names:tc:opendocument:xmlns:text:1.0")
    Attr.Value = Value
    Return Attr
  End Function
  Private Sub InsertEmptyParagraph(Doc As XmlDocument, DocNode As XmlNode)
    Dim P As XmlElement
    P = Doc.CreateElement("text:p", "urn:oasis:names:tc:opendocument:xmlns:text:1.0")
    P.Attributes.Append(AddAttrib("text:style-name", "Standard", Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertParagraph(Doc As XmlDocument, DocNode As XmlNode, NodeName As String, StyleName As String, ParagraphText As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName, "urn:oasis:names:tc:opendocument:xmlns:text:1.0")
    P.InnerText = ParagraphText
    P.Attributes.Append(AddAttrib("text:style-name", StyleName, Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertListItem(Doc As XmlDocument, DocNode As XmlNode)
    Dim P As XmlElement
    P = Doc.CreateElement("text:list-item", "urn:oasis:names:tc:opendocument:xmlns:text:1.0")
    DocNode.AppendChild(P)
  End Sub

End Class
Public Class ReasonContents
  Public Property ID As Integer
  Public Property Content As String
  Public Property Status As GlobalValues.ReasonStatus
  Public Property Owner As String
End Class

Public Class RootAttribute
  Public Property Name As String
  Public Property Value As String
End Class