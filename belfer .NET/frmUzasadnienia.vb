Imports System.Drawing.Printing
'Imports System.Xml
Public Class frmUzasadnienia
  Public Filter, Status As String
  Private DS, dsBrakuj As DataSet ', dtIndividualStaff As DataTable
  Public Event NewRow()
  Private Offset(2), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean ', FilterID As Integer

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.UzasadnieniaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.UzasadnieniaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData(Semestr As String)
    Dim DBA As New DataBaseAction, CH As New CalcHelper, U As New UzasadnienieSQL
    Dim Typ As RadioButton
    Typ = gbTyp.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    Dim IdSchool As String = My.Settings.IdSchool
    Dim SchoolYear As String = My.Settings.SchoolYear
    Dim StartDate, EndDate As Date
    StartDate = CH.StartDateOfSchoolYear(SchoolYear)
    EndDate = CType(IIf(Semestr = "S", CH.StartDateOfSemester2(CType(SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(SchoolYear)), Date)
    Select Case Typ.Name
      Case "rbZachowanie"
        DS = DBA.GetDataSet(U.SelectGrupaZ(IdSchool, SchoolYear, Semestr) & U.SelectStudentZ(IdSchool, SchoolYear, Semestr) & U.SelectUzasadnienieZ(SchoolYear, Semestr))
        DS.Tables(0).TableName = "Grupa"
        DS.Tables(1).TableName = "Student"
        DS.Tables(2).TableName = "Uzasadnienie"
        dsBrakuj = DBA.GetDataSet(U.SelectGrupaBZ(IdSchool, SchoolYear, Semestr) & U.SelectStudentBZ(IdSchool, SchoolYear, Semestr))
        dsBrakuj.Tables(0).TableName = "Grupa"
        dsBrakuj.Tables(1).TableName = "Student"
      Case Else
        DS = DBA.GetDataSet(U.SelectGrupa(IdSchool, SchoolYear, Semestr) & U.SelectStudent(IdSchool, SchoolYear, Semestr) & U.SelectUzasadnienie(SchoolYear, Semestr))
        DS.Tables(0).TableName = "Grupa"
        DS.Tables(1).TableName = "Student"
        DS.Tables(2).TableName = "Uzasadnienie"
        dsBrakuj = DBA.GetDataSet(U.SelectGrupaB(IdSchool, SchoolYear, Semestr) & U.SelectStudentB(IdSchool, SchoolYear, Semestr))
        dsBrakuj.Tables(0).TableName = "Grupa"
        dsBrakuj.Tables(1).TableName = "Student"
    End Select
  End Sub
  Private Sub frmWykazNdst_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Dim SeekCriteria() As String = New String() {"Nazwisko i imię nauczyciela", "Nazwisko i imię ucznia", "Przedmiot", "Klasa"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    ListViewConfig(lvStudent)

    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    lblRecord.Text = ""
    rbPrzedmiot.Checked = True

  End Sub

  Private Sub ListViewConfig(LV As ListView)
    With LV
      .View = View.Details
      .FullRowSelect = True
      .GridLines = True
      '.ColorGridLines = Color.Beige
      .MultiSelect = True
      .AllowColumnReorder = False
      '.ColumnReorderMode = BetterListViewColumnReorderMode.Disabled
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)

      .HideSelection = False
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .ShowGroups = True
      .Items.Clear()
      .Enabled = False

      'AddListViewColumn(LV, Cols)
    End With
  End Sub
  Private Sub AddListViewColumn(lv As ListView, Cols As List(Of ReasonColumn))
    With lv
      .Items.Clear()
      .Columns.Clear()
      For Each Col As ReasonColumn In Cols
        .Columns.Add(Col.Label, Col.Width, HorizontalAlignment.Center).Name = Col.Name
        '  .Columns(Col.Name).AlignHorizontal()
      Next
    End With
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub GetData(lv As ListView)
    Try
      EnableButton(False)
      txtUzasadnienie.Text = ""
      lv.BeginUpdate()
      lv.Items.Clear()
      lv.Groups.Clear()
      Dim dtGrupa As DataTable = Nothing
      If Status = "0" Then
        dtGrupa = dsBrakuj.Tables("Grupa") 'New DataView(dsBrakuj.Tables("Student"), "Nauczyciel=" & IdGrupa & Filter, "Student ASC", DataViewRowState.CurrentRows).ToTable
      Else
        dtGrupa = DS.Tables("Grupa") 'New DataView(DS.Tables("Student"), "Nauczyciel=" & IdGrupa & Filter & " AND " & Status, "Alias ASC,Nazwa_Klasy ASC,Student ASC", DataViewRowState.CurrentRows).ToTable
      End If
      For Each G As DataRow In dtGrupa.Rows  'DS.Tables("Grupa").Rows
        LvNewItem(lv, G.Item("ID").ToString, G.Item("Nazwa").ToString)
      Next

      If lv.Items.Count > 0 Then
        lv.Enabled = True
      Else
        lv.Enabled = False
      End If
      lv.EndUpdate()
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub LvNewItem(ByVal LV As ListView, ByVal IdGrupa As String, ByVal Grupa As String)
    Dim NG As New ListViewGroup(Grupa, Grupa)
    NG.HeaderAlignment = HorizontalAlignment.Center
    LV.Groups.Add(NG)
    LV.Groups(Grupa).Tag = IdGrupa
    Dim dtStudent As DataTable = Nothing
    If Status = "0" Then
      dtStudent = New DataView(dsBrakuj.Tables("Student"), "Nauczyciel=" & IdGrupa & Filter, "Student ASC", DataViewRowState.CurrentRows).ToTable
    Else
      dtStudent = New DataView(DS.Tables("Student"), "Nauczyciel=" & IdGrupa & Filter & " AND " & Status, "Alias ASC,Nazwa_Klasy ASC,Student ASC", DataViewRowState.CurrentRows).ToTable
    End If

    For Each Student As DataRow In dtStudent.Rows  'DS.Tables("Student").Select("Nauczyciel=" & IdGrupa & Filter & " AND " & Status)
      Dim NewItem As New ListViewItem(Student.Item("Student").ToString, NG)
      NewItem.UseItemStyleForSubItems = False
      NewItem.Tag = Student.Item("IdStudent").ToString
      NewItem.Name = "Student"
      'NewItem.SubItems.Add()
      If rbPrzedmiot.Checked Then
        NewItem.SubItems.Add(Student.Item("Alias").ToString).Tag = Student.Item("Przedmiot").ToString
        NewItem.SubItems(NewItem.SubItems.Count - 1).Name = "Przedmiot"

      End If
      NewItem.SubItems.Add(Student.Item("Ocena").ToString).Tag = Student.Item("IdWynik").ToString
      NewItem.SubItems(NewItem.SubItems.Count - 1).Name = "Ocena"
      If Status <> "0" Then
        'If rbPrzedmiot.Checked Then NewItem.SubItems.Add(ComputeAvg(Student.Item("Przedmiot").ToString, Student.Item("IdStudent").ToString))
        'If rbPrzedmiot.Checked Then
        '  NewItem.SubItems.Add(ComputeAbsence(Student.Item("IdStudent").ToString, Student.Item("Klasa").ToString, Student.Item("Przedmiot").ToString).ProcentNieobecnosci)
        'Else
        '  NewItem.SubItems.Add(ComputeAbsence(Student.Item("IdStudent").ToString, Student.Item("Klasa").ToString).ProcentNieobecnosci)
        'End If

        Dim ReasonContents As ReasonContents = GetReasonContents(Student.Item("IdWynik").ToString)
        Dim StatusColor As Color = Color.Black
        If CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Odrzucone Then
          StatusColor = Color.Red
        ElseIf CType(ReasonContents.Status, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Zatwierdzone Then
          StatusColor = Color.Green
        End If
        NewItem.SubItems.Add(ReasonContents.Status.ToString).Tag = ReasonContents
        NewItem.SubItems(NewItem.SubItems.Count - 1).Name = "Status"
        NewItem.SubItems("Status").ForeColor = StatusColor
      Else
        NewItem.SubItems.Add(GlobalValues.ReasonStatus.Brak.ToString)

      End If

      'For i As Integer = 1 To NewItem.SubItems.Count - 1
      '  NewItem.SubItems(i).AlignHorizontal = TextAlignmentHorizontal.Center
      'Next
      LV.Items.Add(NewItem)
    Next
    'LV.Columns(LV.Columns.Count - 1).Width = CInt(IIf(LV.Items.Count > 9, 100, 120))
    lblRecord.Text = "0 z " & LV.Items.Count
    If LV.Groups(Grupa).Items.Count = 0 Then LV.Groups.Remove(NG)
  End Sub
  Private Function GetReasonContents(IdWynik As String) As ReasonContents
    Dim DR As DataRow() = DS.Tables("Uzasadnienie").Select("IdWynik=" & IdWynik)
    If DR.GetLength(0) > 0 Then
      Return New ReasonContents With {.Content = DR(0).Item("Tresc").ToString, .Status = CType(DR(0).Item("Status"), GlobalValues.ReasonStatus), .Owner = DR(0).Item("Owner").ToString, .ID = CType(DR(0).Item("ID"), Integer)}
    Else
      Return New ReasonContents With {.Content = "", .Status = GlobalValues.ReasonStatus.Brak}
    End If
  End Function

  Private Sub EnableButton(Value As Boolean)
    cmdAccept.Enabled = Value
    cmdReject.Enabled = Value
    cmdPrint.Enabled = Value
  End Sub

  Private Sub rbSemestr_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemestr.CheckedChanged, rbRokSzkolny.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    Cursor = Cursors.WaitCursor
    Dim Wait As New dlgWait
    Wait.lblInfo.Text = "Trwa pobieranie danych ..."
    Wait.Show()
    Application.DoEvents()
    FetchData(CType(sender, RadioButton).Tag.ToString)
    'If Not BackgroundWorker1.IsBusy Then
    '  BackgroundWorker1.RunWorkerAsync()

    'End If
    Dim Status As RadioButton
    Status = gbStatus.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    If Status Is Nothing Then
      rbAll.Checked = True
    Else
      rbStatus_CheckedChanged(Status, Nothing)
    End If
    Wait.Hide()
    Cursor = Cursors.Default
  End Sub


  'Private Sub lvStudent_DrawColumnHeaderBackground(sender As Object, e As BetterListViewDrawColumnHeaderBackgroundEventArgs)
  '  'e.Graphics.FillRectangle(Brushes.LightGray, e.ColumnHeaderBounds.BoundsOuter)
  '  e.Graphics.FillRectangle(New SolidBrush(SystemColors.ButtonFace), e.ColumnHeaderBounds.BoundsOuter)
  '  e.Graphics.DrawLine(Pens.White, e.ColumnHeaderBounds.BoundsSpacing.Left, e.ColumnHeaderBounds.BoundsSpacing.Top, e.ColumnHeaderBounds.BoundsSpacing.Left, e.ColumnHeaderBounds.BoundsSpacing.Bottom)
  'End Sub

  Private Sub lvStudent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvStudent.ItemSelectionChanged
    If e.IsSelected Then
      If Status <> "0" Then 'AndAlso CType(e.Item.SubItems(e.Item.ListView.Columns.Count - 1).Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Przekazane Then
        If CType(e.Item.SubItems(e.Item.ListView.Columns.Count - 1).Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Przekazane Then 'CType(lvStudent.SelectedItems(0).SubItems(lvStudent.Columns.Count - 1).Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Przekazane Then
          cmdAccept.Enabled = True
          cmdReject.Enabled = True
        Else
          cmdAccept.Enabled = False
          cmdReject.Enabled = False
        End If
        'txtUzasadnienie.Text = CType(lvStudent.SelectedItems(0).SubItems(lvStudent.Columns.Count - 1).Tag, ReasonContents).Content
        'cmdAccept.Enabled = True
        'cmdReject.Enabled = True
        txtUzasadnienie.Text = CType(e.Item.SubItems(e.Item.ListView.Columns.Count - 1).Tag, ReasonContents).Content
      End If
      cmdPrint.Enabled = True
      lblRecord.Text = e.Item.ListView.SelectedItems(0).Index + 1 & " z " & e.Item.ListView.Items.Count
      Else
        'cmdPrint.Enabled = False
        txtUzasadnienie.Text = ""
        EnableButton(False)
        lblRecord.Text = "0 z " & e.Item.ListView.Items.Count

      End If
  End Sub
  'Private Sub lvStudent_SelectedIndexChanged(sender As Object, e As EventArgs)
  '  If lvStudent.SelectedItems.Count > 0 Then
  '    If Status <> "0" Then
  '      If CType(lvStudent.SelectedItems(0).SubItems(lvStudent.Columns.Count - 1).Tag, ReasonContents).Status = GlobalValues.ReasonStatus.Przekazane Then
  '        cmdAccept.Enabled = True
  '        cmdReject.Enabled = True
  '      Else
  '        cmdAccept.Enabled = False
  '        cmdReject.Enabled = False
  '      End If
  '      txtUzasadnienie.Text = CType(lvStudent.SelectedItems(0).SubItems(lvStudent.Columns.Count - 1).Tag, ReasonContents).Content
  '    End If
  '    cmdPrint.Enabled = True
  '    lblRecord.Text = lvStudent.SelectedItems(0).Index + 1 & " z " & lvStudent.Items.Count
  '  Else
  '    txtUzasadnienie.Text = ""
  '    EnableButton(False)
  '    lblRecord.Text = "0 z " & lvStudent.Items.Count
  '  End If
  'End Sub

  'Private Function ComputeAvg(IdPrzedmiot As String, IdUczen As String) As String
  '  Dim Suma, SumaWag As Single
  '  Dim dsPrzedmiot As DataSet = Nothing
  '  If Status = "0" Then
  '    dsPrzedmiot = dsBrakuj
  '  Else
  '    dsPrzedmiot = DS
  '  End If
  '  For Each Wynik As DataRow In dsPrzedmiot.Tables("Wyniki").Select("Przedmiot=" & IdPrzedmiot & " AND IdUczen=" & IdUczen)
  '    If CType(Wynik.Item("WagaOceny"), Single) > 0 Then
  '      Suma += CType(Wynik.Item("WagaOceny"), Single) * CType(Wynik.Item("WagaKolumny"), Single)
  '      SumaWag += CType(Wynik.Item("WagaKolumny"), Single)
  '    End If
  '  Next
  '  Return IIf(Suma > 0, Math.Round(Suma / SumaWag, 2), 0).ToString
  'End Function
  'Private Function ComputeAvg(IdUczen As String) As String
  '  Dim Suma, SumaWag As Single
  '  For Each Wynik As DataRow In DS.Tables("Wyniki").Select("IdUczen=" & IdUczen)
  '    If CType(Wynik.Item("WagaOceny"), Single) > 0 Then
  '      Suma += CType(Wynik.Item("WagaOceny"), Single) * CType(Wynik.Item("WagaKolumny"), Single)
  '      SumaWag += CType(Wynik.Item("WagaKolumny"), Single)
  '    End If
  '  Next
  '  Return IIf(Suma > 0, Math.Round(Suma / SumaWag, 2), 0).ToString
  'End Function
  'Private Function ComputeAbsence(IdUczen As String, Klasa As String, Przedmiot As String) As Absencja
  '  Dim Frekwencja As Single ', CH As New CalcHelper
  '  Dim LiczbaNb, LiczbaLekcji As Integer
  '  Dim dsFrekwencja As DataSet = Nothing
  '  If Status = "0" Then
  '    dsFrekwencja = dsBrakuj
  '  Else
  '    dsFrekwencja = DS
  '  End If
  '  If dsFrekwencja.Tables("LekcjaBezZastepstw").Select("Klasa=" & Klasa & " AND Przedmiot=" & Przedmiot).GetLength(0) > 0 Then LiczbaLekcji = CType(dsFrekwencja.Tables("LekcjaBezZastepstw").Select("Klasa=" & Klasa & " AND Przedmiot=" & Przedmiot)(0).Item("LL"), Integer)

  '  LiczbaLekcji += dsFrekwencja.Tables("Zastepstwo").Select("Klasa=" & Klasa & " AND Przedmiot=" & Przedmiot).GetLength(0)

  '  If LiczbaLekcji = 0 Then Return New Absencja With {.ProcentNieobecnosci = "-", .LiczbaLekcji = "0", .LiczbaNieobecnosci = "?"}

  '  If dsFrekwencja.Tables("Absence").Select("Przedmiot=" & Przedmiot & " AND IdUczen=" & IdUczen).GetLength(0) > 0 Then LiczbaNb = CType(dsFrekwencja.Tables("Absence").Select("Przedmiot=" & Przedmiot & " AND IdUczen=" & IdUczen)(0).Item("Abc"), Integer)
  '  If dsFrekwencja.Tables("AbsenceZastepstwo").Select("IdUczen=" & IdUczen & " AND Przedmiot=" & Przedmiot).GetLength(0) > 0 Then LiczbaNb += CType(dsFrekwencja.Tables("AbsenceZastepstwo").Select("IdUczen=" & IdUczen & " AND Przedmiot=" & Przedmiot)(0).Item("LNb"), Integer)
  '  Frekwencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
  '  Frekwencja = CType(IIf(Frekwencja > 100, 100, Frekwencja), Single)
  '  Return New Absencja With {.ProcentNieobecnosci = Frekwencja.ToString & "%", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = LiczbaNb.ToString}
  'End Function
  'Private Function ComputeAbsence(IdUczen As String, Klasa As String) As Absencja
  '  Dim Frekwencja As Single ', CH As New CalcHelper
  '  Dim LiczbaNb, LiczbaLekcji As Integer
  '  Dim dsFrekwencja As DataSet = Nothing
  '  If Status = "0" Then
  '    dsFrekwencja = dsBrakuj
  '  Else
  '    dsFrekwencja = DS
  '  End If
  '  If dsFrekwencja.Tables("Lekcja").Select("Klasa=" & Klasa).GetLength(0) > 0 Then LiczbaLekcji = CType(dsFrekwencja.Tables("Lekcja").Select("Klasa=" & Klasa)(0).Item("LL"), Integer)
  '  For Each G As DataRow In dsFrekwencja.Tables("GrupaPrzedmiotowa").Select("IdUczen=" & IdUczen)
  '    If dsFrekwencja.Tables("LekcjaByGrupa").Select("Klasa=" & Klasa & " AND Przedmiot=" & G.Item("Przedmiot").ToString).GetLength(0) > 0 Then LiczbaLekcji += CType(dsFrekwencja.Tables("LekcjaByGrupa").Select("Klasa=" & Klasa & " AND Przedmiot=" & G.Item("Przedmiot").ToString)(0).Item("LL"), Integer)

  '  Next
  '  If LiczbaLekcji = 0 Then Return New Absencja With {.ProcentNieobecnosci = "-", .LiczbaLekcji = "0", .LiczbaNieobecnosci = "?"}

  '  If dsFrekwencja.Tables("Absence").Select("IdUczen=" & IdUczen).GetLength(0) > 0 Then LiczbaNb = CType(dsFrekwencja.Tables("Absence").Select("IdUczen=" & IdUczen)(0).Item("Abc"), Integer)
  '  Frekwencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
  '  Frekwencja = CType(IIf(Frekwencja > 100, 100, Frekwencja), Single)
  '  Return New Absencja With {.ProcentNieobecnosci = Frekwencja.ToString & "%", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = LiczbaNb.ToString}
  'End Function
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


  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAccept.Click, cmdReject.Click
    Dim DBA As New DataBaseAction, U As New UzasadnienieSQL, MyTran As MySqlTransaction
    MyTran = GlobalValues.gblConn.BeginTransaction
    Try
      For Each Item As ListViewItem In lvStudent.SelectedItems
        Dim cmd As MySqlCommand = DBA.CreateCommand(U.ChangeStatus)
        cmd.Transaction = MyTran
        cmd.Parameters.AddWithValue("?IdWynik", Item.SubItems("Ocena").Tag.ToString)
        cmd.Parameters.AddWithValue("?Status", CType(sender, Button).Tag.ToString)
        cmd.ExecuteNonQuery()
      Next
      MyTran.Commit()
      RefreshData()
    Catch mex As MySqlException
      MyTran.Rollback()
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
    Catch ex As Exception
      MyTran.Rollback()
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
    End Try
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
    RemoveHandler NewRow, AddressOf PP.NewRow
    RemoveHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    AddHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
    AddHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    AddHandler NewRow, AddressOf PP.NewRow
    Dim Semestr As RadioButton
    Semestr = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    Dim Klasyfikacja As String = "Klasyfikacja " & If(Semestr.Tag.ToString = "S", "śródroczna", "roczna")
    dlg.rbReasonContents.Enabled = If(rbMissing.Checked, False, True)
    If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
      If dlg.rbStudentList.Checked Then
        RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_StudentList_PrintPage
        AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_StudentList_PrintPage
        If Status = "0" Then
          PP.Doc.ReportHeader = New String() {If(rbPrzedmiot.Checked, "Wykaz uczniów z brakującymi uzasadnieniami ocen niedostatecznych", "Wykaz uczniów z brakującymi uzasadnieniami obniżonych ocen zachowania"), Klasyfikacja}
        Else
          PP.Doc.ReportHeader = New String() {If(rbPrzedmiot.Checked, "Wykaz uczniów z uzasadnionymi ocenami niedostatecznymi", "Wykaz uczniów z uzasadnionymi, obniżonymi ocenami zachowania"), Klasyfikacja}
        End If
      Else
        RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_ReasonContents_PrintPage
        AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_ReasonContents_PrintPage
        PP.Doc.ReportHeader = New String() {If(rbPrzedmiot.Checked, "Uzasadnienia ocen niedostatecznych (nieklasyfikowania)", "Uzasadnienia obniżonych ocen zachowania"), Klasyfikacja}
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
      PH.DrawText(Doc.ReportHeader(1), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
      'PH.DrawText(Doc.ReportHeader(2), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
      y += LineHeight * 2
    End If
    Dim ColSize As Integer = 100

    Dim StudentName As Integer = CInt((PrintWidth - ColSize - ColSize * CSng(1.5)))

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Student", .Label = "Nazwisko i imię {klasa}", .Size = StudentName},
        New Pole With {.Name = "Ocena", .Label = "Ocena", .Size = CInt(ColSize * CSng(1.5))},
        New Pole With {.Name = "Status", .Label = "Status", .Size = ColSize}
    }
    If rbPrzedmiot.Checked Then
      Kolumna.Insert(1, New Pole With {.Name = "Przedmiot", .Label = "Przedmiot", .Size = CInt(ColSize * CSng(1.5))})
      Kolumna.Item(0).Size -= Kolumna.Item(1).Size
    End If

    Do Until (y + LineHeight * CSng(5)) > PrintHeight Or Offset(0) > lvStudent.Groups.Count - 1
      Dim ColOffset As Single = 0
      If Offset(1) = 0 Then
        PH.DrawText(lvStudent.Groups(Offset(0)).Name.ToUpper, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight * 2, 0, Brushes.Black, False)
        y += LineHeight * CSng(2)
        For Each Col In Kolumna
          PH.DrawText(Col.Label, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, Col.Size, LineHeight * CSng(2), 1, Brushes.Black)
          ColOffset += Col.Size
        Next
        y += LineHeight * CSng(2)

      End If
      Do Until (y + LineHeight) > PrintHeight Or Offset(1) > lvStudent.Groups(Offset(0)).Items.Count - 1
        ColOffset = x
        For i As Integer = 0 To Kolumna.Count - 1
          PH.DrawText(lvStudent.Groups(Offset(0)).Items(Offset(1)).SubItems(i).Text, TextFont, ColOffset, y, Kolumna(i).Size, LineHeight, CType(If(Kolumna(i).Name = "Student", 0, 1), Byte), Brushes.Black)
          ColOffset += Kolumna(i).Size
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
      PH.DrawText(Doc.ReportHeader(1), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
      y += LineHeight * 2
    End If

    Do Until (y + LineHeight * CSng(4.5)) > PrintHeight Or Offset(0) > lvStudent.Groups.Count - 1
      If Offset(1) = 0 AndAlso Offset(2) = 0 Then
        PH.DrawText(lvStudent.Groups(Offset(0)).Name.ToUpper, HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False, False, False)
        y += LineHeight * CSng(1.5)
      End If

      Do Until (y + LineHeight * CSng(3.5)) > PrintHeight Or Offset(1) > lvStudent.Groups(Offset(0)).Items.Count - 1

        If Offset(2) = 0 Then
          PH.DrawText(lvStudent.Groups(Offset(0)).Items(Offset(1)).Text, New Font(TextFont, FontStyle.Bold), x, y, CSng(PrintWidth / 3), LineHeight * CSng(1.5), 1, Brushes.Black, True)
          If rbPrzedmiot.Checked Then
            PH.DrawText(lvStudent.Groups(Offset(0)).Items(Offset(1)).SubItems("Przedmiot").Text, New Font(TextFont, FontStyle.Bold), x + CSng(PrintWidth / 3), y, CSng(PrintWidth / 3), LineHeight * CSng(1.5), 1, Brushes.Black, True)
            PH.DrawText(lvStudent.Groups(Offset(0)).Items(Offset(1)).SubItems("Ocena").Text, New Font(TextFont, FontStyle.Bold), x + CSng(PrintWidth * 2 / 3), y, CSng(PrintWidth / 3), LineHeight * CSng(1.5), 1, Brushes.Black, True)
          Else
            PH.DrawText(lvStudent.Groups(Offset(0)).Items(Offset(1)).SubItems("Ocena").Text, New Font(TextFont, FontStyle.Bold), x + CSng(PrintWidth / 3), y, CSng(PrintWidth / 3), LineHeight * CSng(1.5), 1, Brushes.Black, True)

          End If
          y += LineHeight * CSng(2)
        End If

        If DS.Tables("Uzasadnienie").Select("IdWynik=" & lvStudent.Groups(Offset(0)).Items(Offset(1)).SubItems("Ocena").Tag.ToString).GetLength(0) > 0 Then
          Dim Uzasadnienie As String() = DS.Tables("Uzasadnienie").Select("IdWynik=" & lvStudent.Groups(Offset(0)).Items(Offset(1)).SubItems("Ocena").Tag.ToString)(0).Item("Tresc").ToString.Split(" ".ToCharArray)
          If PH.DrawWrappedText(Uzasadnienie, TextFont, Offset(2), x, y, PrintWidth, PrintHeight, LineHeight) = False Then
            e.HasMorePages = True
            RaiseEvent NewRow()
            Exit Sub
          Else
            Offset(2) = 0
            y += LineHeight
          End If
        End If
        Offset(1) += 1
      Loop

      y += LineHeight
      If Offset(1) < lvStudent.Groups(Offset(0)).Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      Else
        Offset(1) = 0
        y -= LineHeight
        Doc.Lp = 0
      End If

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

  Private Sub rbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rbRejected.CheckedChanged, rbSubmitted.CheckedChanged, rbAccepted.CheckedChanged, rbAll.CheckedChanged, rbMissing.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    Status = CType(sender, RadioButton).Tag.ToString
    GetData(lvStudent)
  End Sub

  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię nauczyciela"
        Filter = " AND Belfer LIKE '" & Me.txtSeek.Text + "%'"
      Case "Nazwisko i imię ucznia"
        Filter = " AND Student LIKE '" & Me.txtSeek.Text + "%'"
      Case "Przedmiot"
        Filter = " AND Alias LIKE '" & Me.txtSeek.Text + "%'"
      Case "Klasa"
        Filter = " AND Nazwa_Klasy LIKE '" & Me.txtSeek.Text + "%'"
    End Select
    Me.GetData(lvStudent)
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub

  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    RefreshData()
  End Sub

  Private Sub rbPrzedmiot_CheckedChanged(sender As Object, e As EventArgs) Handles rbPrzedmiot.CheckedChanged, rbZachowanie.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub

    Select Case CType(sender, RadioButton).Name
      Case "rbPrzedmiot"
        Dim Cols As New List(Of ReasonColumn) From {New ReasonColumn With {.Name = "Student", .Label = "Uczeń {klasa}", .Width = 350}, New ReasonColumn With {.Name = "Przedmiot", .Label = "Przedmiot", .Width = 150}, New ReasonColumn With {.Name = "Ocena", .Label = "Ocena", .Width = 150}, New ReasonColumn With {.Name = "Status", .Label = "Status", .Width = 150}}
       
        AddListViewColumn(lvStudent, Cols)
      Case Else
        Dim Cols As New List(Of ReasonColumn) From {New ReasonColumn With {.Name = "Student", .Label = "Uczeń {klasa}", .Width = 400}, New ReasonColumn With {.Name = "Ocena", .Label = "Ocena", .Width = 250}, New ReasonColumn With {.Name = "Status", .Label = "Status", .Width = 150}}

        AddListViewColumn(lvStudent, Cols)
    End Select

    RefreshData()
  End Sub


  Sub RefreshData()
    Dim Semestr As RadioButton
    Semestr = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    If Semestr Is Nothing Then
      Dim CH As New CalcHelper
      If Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year) Then
        rbSemestr.Checked = True
      Else
        rbRokSzkolny.Checked = True
      End If
    Else
      rbSemestr_CheckedChanged(Semestr, Nothing)
    End If
  End Sub


  'Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
  '  Dim Wait As New dlgWait
  '  Wait.lblInfo.Text = "Trwa pobieranie danych ..."
  '  Wait.Show()
  '  Application.DoEvents()
  '  Dim Semestr As RadioButton
  '  Semestr = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
  '  FetchData(Semestr.Tag.ToString)

  '  Wait.Hide()
  'End Sub

  'Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
  '  Dim Status As RadioButton
  '  Status = gbStatus.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
  '  If Status Is Nothing Then
  '    rbAll.Checked = True
  '  Else
  '    rbStatus_CheckedChanged(Status, Nothing)
  '  End If
  'End Sub


  Private Sub lvStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvStudent.SelectedIndexChanged

  End Sub
End Class
Public Class ReasonColumn
  Public Property Name As String
  Public Property Label As String
  Public Property Width As Integer
End Class