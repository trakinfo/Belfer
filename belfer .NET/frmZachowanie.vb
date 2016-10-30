Public Class frmZachowanie
  Private InRefresh As Boolean = True, DTWyniki, dtGrupa As DataTable, DS As DataSet ', NewList, EditList As New ArrayList
  Dim EndMarks As List(Of EndMark)
  Private IntRowIndex As Integer = 0, IntColumnIndex As Integer = 2, CurrentDate As Date
  'Private LiczbaLekcjiBezGrup As Integer = 0

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.OcenyZachowanieToolStripMenuItem.Enabled = True
    MainForm.cmdOcenyZachowania.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.OcenyZachowanieToolStripMenuItem.Enabled = True
    MainForm.cmdOcenyZachowania.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  'Private IdPrzedmiot As Integer
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub

  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    lblWychowawca.Text = "Wychowawca: "
    GetEndMarks()
    DataGridConfig(dgvZachowanie)
    'Dim CH As New CalcHelper
    'Dim Semester2 As Date = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
    'Me.nudSemestr.Value = CType(IIf(CurrentDate < Semester2, 1, 2), Integer)

    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    'lblOpis.Text = ""
    CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)

    'Dim CH As New CalcHelper
    'InRefresh = True
    'Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    'InRefresh = False
    Dim CH As New CalcHelper
    Dim Semester2 As Date = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
    Me.nudSemestr.Value = CType(IIf(CurrentDate < Semester2, 1, 2), Integer)
    nudSemestr.Minimum = 1
    'nudSemestr_ValueChanged(Nothing, Nothing)
    FillKlasa()
  End Sub

  Private Sub FetchData(Klasa As String)
    Dim DBA As New DataBaseAction, W As New WynikiSQL, S As New StatystykaSQL, CH As New CalcHelper
    DS = DBA.GetDataSet(S.CountNotes(Klasa, My.Settings.SchoolYear, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)) & S.CountAbsence(Klasa, My.Settings.SchoolYear, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)) & S.SelectLekcja(Klasa, My.Settings.SchoolYear, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)))

    '& S.SelectLekcjaGrupa(Klasa, My.Settings.SchoolYear, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date))

    DS.Tables(0).TableName = "Notes"
    DS.Tables(1).TableName = "Absence"
    DS.Tables(2).TableName = "Lesson"
    'DS.Tables(3).TableName = "LessonNoGroup"
  End Sub
  Private Sub FetchResult()
    Dim DBA As New DataBaseAction, W As New WynikiSQL
    DTWyniki = DBA.GetDataTable(W.SelectBehaviorResult(CType(dgvZachowanie.Columns("Zachowanie").Tag, BehaviourColProperty).ID.ToString))
    DTWyniki.TableName = "Wyniki"
  End Sub

  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, O As New ObsadaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, "0"))
    Dim SH As New SeekHelper
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    Cursor = Cursors.WaitCursor
    dgvZachowanie.Rows.Clear()
    dgvZachowanie.Columns.Clear()
    nudSemestr.Enabled = True
    InRefresh = True
    GetWychowawca(CType(cbKlasa.SelectedItem, CbItem).ID.ToString)
    GetGrupa(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)
    'GetKolumnaZachowaniaID(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)

    SetColumns(dgvZachowanie)
    SetRows(dgvZachowanie)
    InRefresh = False
    Cursor = Cursors.Default
    RefreshData()
  End Sub
  Private Function GetKolumnaZachowania(Klasa As String, RokSzkolny As String) As BehaviourColProperty
    Dim DBA As New DataBaseAction, W As New WynikiSQL, R As MySqlDataReader = Nothing ', BCL As BehaviourColProperty = Nothing
    R = DBA.GetReader(W.SelectKolumnaZachowanie(Klasa, RokSzkolny, IIf(nudSemestr.Value = 1, "S", "R").ToString))
    Try
      If R.HasRows Then
        R.Read()
        Dim BCL As New BehaviourColProperty With {.ID = R.GetInt32("ID"), .Lock = R.GetBoolean("Lock")}
        Return BCL
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try
    Return Nothing
  End Function
  Private Sub GetGrupa(Klasa As String, RokSzkolny As String)
    Dim DBA As New DataBaseAction, W As New WynikiSQL
    dtGrupa = DBA.GetDataTable(W.SelectGrupa(Klasa, RokSzkolny))
  End Sub
  Private Sub GetWychowawca(ByVal Klasa As String)
    Dim R As MySqlDataReader = Nothing, W As New WynikiSQL, DBA As New DataBaseAction, CurrentDate As Date
    Try
      lblWychowawca.Text = "Wychowawca: "
      CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)

      R = DBA.GetReader(W.SelectWychowawca(Klasa, My.Settings.SchoolYear, CurrentDate))
      If R.HasRows Then
        R.Read()
        lblWychowawca.Text += R.Item("Wychowawca").ToString
        'TutorLogin = R.Item("Login").ToString
      Else
        lblWychowawca.Text = "Nie uda³o siê ustaliæ wychowawstwa"
        'TutorLogin = ""
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch err As Exception
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub RefreshData()
    Try
      'InRefresh = True
      Cursor = Cursors.WaitCursor
      cmdDelete.Enabled = False
      'ClearDetails()
      IntRowIndex = 0
      IntColumnIndex = dgvZachowanie.Columns("Zachowanie").Index
      'GetKolumnaZachowaniaID(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)
      FetchData(CType(cbKlasa.SelectedItem, CbItem).ID.ToString)
      GetNotes()
      GetAbsence()
      ComputeAbsenceByPercent()
      Me.GetData()
      Me.dgvZachowanie.Focus()
      Me.dgvZachowanie.Enabled = True
      Cursor = Cursors.Default
      'InRefresh = False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub ClearDetails()
    Me.lblUser.Text = ""
    Me.lblIP.Text = ""
    Me.lblData.Text = ""
  End Sub
  Private Sub DataGridConfig(ByVal dgvName As DataGridView)
    With dgvName
      .SelectionMode = DataGridViewSelectionMode.CellSelect
      .AutoGenerateColumns = False
      '.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 40
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
      AddHandler .CellPainting, AddressOf dgvZestawienieOcen_CellPainting
      AddHandler .Paint, AddressOf dgvZestawienieOcen_Paint
      AddHandler .Scroll, AddressOf dgvZestawienieOcen_Scroll
      AddHandler .ColumnWidthChanged, AddressOf dgvZestawienieOcen_ColumnWidthChanged
    End With
  End Sub

  Private Overloads Sub SetColumns(ByVal dgvName As DataGridView)
    Dim DBA As New DataBaseAction, Reader As MySqlDataReader = Nothing
    Try
      With dgvName
        'While Reader.Read
        .Columns.Clear()
        Dim NameCol, NrCol As New DataGridViewColumn
        NrCol.Name = "Nr"
        NrCol.HeaderText = ""
        NrCol.Width = 30
        NrCol.CellTemplate = New DataGridViewTextBoxCell()
        NrCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        NrCol.ToolTipText = "Numer ucznia w dzienniku"
        NrCol.ReadOnly = True
        NrCol.Frozen = True
        NrCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NrCol)

        NameCol.Name = "Nazwisko"
        NameCol.HeaderText = ""
        NameCol.Width = 200
        NameCol.CellTemplate = New DataGridViewTextBoxCell()
        NameCol.ToolTipText = "Nazwisko i imiê ucznia"
        NameCol.ReadOnly = True
        NameCol.Frozen = True
        NameCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NameCol)

        For Each Uwaga As String In "PN"
          Dim NoteColumn As New DataGridViewTextBoxColumn      'DataGridViewComboBoxColumn
          With NoteColumn
            .HeaderText = "   " + Uwaga
            .Width = 50 '65
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate = New DataGridViewTextBoxCell
            .Name = "U" + Uwaga
            '.Frozen = True
            .ReadOnly = True
            '.HeaderCell.Style.BackColor = Color.AliceBlue
            .CellTemplate.Style.BackColor = Color.AliceBlue
            If Uwaga = "P" Then
              .CellTemplate.Style.ForeColor = Color.Blue
              .ToolTipText = "Liczba uwag pozytywnych"
            Else
              .CellTemplate.Style.ForeColor = Color.Red
              .ToolTipText = "Liczba uwag negatywnych"
            End If
            .SortMode = DataGridViewColumnSortMode.Programmatic
          End With
          .Columns.Add(NoteColumn)
        Next

        For Each Abs As String In "UNS%"
          Dim FrekwencjaColumn As New DataGridViewTextBoxColumn      'DataGridViewComboBoxColumn
          With FrekwencjaColumn
            .HeaderText = Abs
            .Width = 50 '65
            .CellTemplate = New DataGridViewTextBoxCell
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            '.HeaderCell.Style.Font = New Font(.CellTemplate.Style.Font, FontStyle.Bold)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Name = "N" + Abs
            '.Frozen = True
            .ReadOnly = True
            .CellTemplate.Style.BackColor = Color.Ivory
            If Abs = "U" Then
              .CellTemplate.Style.ForeColor = Color.Green
              .ToolTipText = "Liczba nieobecnoœci usprawiedliwionych"
            ElseIf Abs = "N" Then
              .CellTemplate.Style.ForeColor = Color.Red
              .ToolTipText = "Liczba nieobecnoœci nieusprawiedliwionych"
            ElseIf Abs = "%" Then
              .CellTemplate.Style.BackColor = Color.LightYellow
              .CellTemplate.Style.ForeColor = Color.Red
              .ToolTipText = "Procent nieobecnoœci"
            Else
              .CellTemplate.Style.ForeColor = Color.Blue
              .ToolTipText = "Liczba spóŸnieñ"
            End If
            .SortMode = DataGridViewColumnSortMode.Programmatic
          End With
          .Columns.Add(FrekwencjaColumn)
        Next
        Dim BCL As BehaviourColProperty = GetKolumnaZachowania(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)
        Dim P As New DataGridViewComboBoxCell, W As New WynikiSQL

        Reader = DBA.GetReader(W.SelectBehaviorMarks)

        P.ValueMember = "ID"
        P.DisplayMember = "Nazwa"
        P.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        While Reader.Read
          P.Items.Add(New CbItem(CInt(Reader.Item(0)), Reader.Item(1).ToString))
        End While
        Dim EndColumn As New DataGridViewComboBoxColumn     'DataGridViewComboBoxColumn
        With EndColumn
          .HeaderText = "" 'IIf(nudSemestr.Value = 1, "Semestr I", "Ocena roczna").ToString
          .Tag = BCL 'IIf(nudSemestr.Value = 1, "Semestr I", "Ocena roczna").ToString
          .Width = 150 '65
          .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
          .CellTemplate = P
          .Name = "Zachowanie"
          .CellTemplate.Style.BackColor = Color.SeaShell
          .CellTemplate.Style.ForeColor = Color.Navy
          .CellTemplate.Style.Font = New Font(dgvZachowanie.Font, FontStyle.Bold)
          .ToolTipText = IIf(nudSemestr.Value = 1, "Ocena zachowania za I Semestr", "Ocena roczna zachowania").ToString
          .SortMode = DataGridViewColumnSortMode.Programmatic
          .ReadOnly = CType(.Tag, BehaviourColProperty).Lock
          '.Frozen = True
        End With
        .Columns.Add(EndColumn)

      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & "Nr b³êdu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      Reader.Close()
    End Try

  End Sub

  Private Function GetGroupMember() As List(Of GroupMember)
    Dim DBA As New DataBaseAction, Student As MySqlDataReader = Nothing '
    Dim T As New TematSQL
    Dim Students As List(Of GroupMember)
    Try
      Dim DT As DataTable = Nothing, W As New WynikiSQL
      DT = DBA.GetDataTable(W.SelectIndividualCourse(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, CbItem).ID.ToString))

      Students = New List(Of GroupMember)
      Student = DBA.GetReader(T.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, CbItem).ID.ToString))
      While Student.Read
        Dim IndividualCourse() As DataRow, NI As New IndividualCourse
        IndividualCourse = DT.Select("IdPrzydzial='" & Student.Item("IdPrzydzial").ToString & "' AND DataAktywacji<=#" & dtData.Value.ToShortDateString & "# AND (DataDeaktywacji>#" & dtData.Value.ToShortDateString & "# OR DataDeaktywacji is null)")
        If IndividualCourse.Length > 0 Then
          NI.VirtualClassID = CType(IndividualCourse(0).Item("Klasa"), Integer)
          Dim S As New List(Of IndividualStaff)
          For Each P As DataRow In IndividualCourse
            S.Add(New IndividualStaff With {.ObjectID = CType(P.Item("IdPrzedmiot"), Integer), .StartDate = CType(P.Item("DataAktywacji"), Date), .EndDate = If(IsDBNull(P.Item("DataDeaktywacji")), Nothing, CType(P.Item("DataDeaktywacji"), Date))})
          Next
          NI.SchoolObject = S
        Else
          NI = Nothing
        End If
        Students.Add(New GroupMember With {.Allocation = New StudentAllocation With {.ID = CInt(Student.Item("ID")), .AllocationID = CInt(Student.Item("IdPrzydzial")), .Status = CType(Student.Item("StatusAktywacji"), Boolean), .DataAktywacji = If(IsDBNull(Student.Item("DataAktywacji")), Nothing, CType(Student.Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(Student.Item("DataDeaktywacji")), Nothing, CType(Student.Item("DataDeaktywacji"), Date)), .NauczanieIndywidualne = NI}, .No = Student.Item("NrwDzienniku").ToString, .Name = Student.Item("Student").ToString})
      End While
      Return Students
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      Student.Close()
    End Try
    Return Nothing
  End Function
  Private Sub SetRows(ByVal dgvName As DataGridView)
    Try
      Dim Students As List(Of GroupMember) = GetGroupMember()
      With dgvName
        .Rows.Clear()
        For Each M As GroupMember In Students
          .Rows.Add(M.No, M.Name & If(M.Allocation.NauczanieIndywidualne IsNot Nothing, " (NI)", ""))
          .Rows(.Rows.Count - 1).Tag = M.Allocation
          '.Rows(.Rows.Count - 1).DefaultCellStyle.BackColor = GridColor(Convert.ToInt32(ShiftColor))
          If M.Allocation.Status = False Then
            .Rows(.Rows.Count - 1).Cells(0).Style.Font = New Font(dgvName.Font, FontStyle.Strikeout)
            .Rows(.Rows.Count - 1).Cells(1).Style.Font = New Font(dgvName.Font, FontStyle.Strikeout)
            .Rows(.Rows.Count - 1).Cells(1).ToolTipText = "Data skreœlenia: " & M.Allocation.DataDeaktywacji.ToString
            For i As Integer = 0 To .Columns.Count - 1
              .Rows(.Rows.Count - 1).Cells(i).Style.ForeColor = Color.SlateGray
              .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
            Next
          ElseIf M.Allocation.NauczanieIndywidualne IsNot Nothing Then
            .Rows(.Rows.Count - 1).Cells(0).ToolTipText = "Nauczanie indywidualne"
            .Rows(.Rows.Count - 1).Cells(1).ToolTipText = "Nauczanie indywidualne"
            For i As Integer = 0 To .ColumnCount - 1
              .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
            Next
          End If
        Next
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & "Nr b³êdu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try

  End Sub
  Private Sub GetNotes()
    InRefresh = True
    Try
      With dgvZachowanie
        For Each Row As DataGridViewRow In .Rows
          For Each Typ As String In "PN"
            If DS.Tables("Notes").Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND TypUwagi='" & Typ & "'").Length > 0 Then
              Row.Cells(.Columns("U" + Typ).Index).Value = CType(DS.Tables("Notes").Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND TypUwagi='" & Typ & "'")(0).Item("LiczbaUwag"), Integer)
            Else
              Row.Cells(.Columns("U" + Typ).Index).Value = "-"
            End If
          Next
        Next
      End With
      InRefresh = False
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub GetAbsence()
    Try
      InRefresh = True
      With dgvZachowanie
        For Each Row As DataGridViewRow In .Rows
          For Each Typ As String In "UNS"
            If DS.Tables("Absence").Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND Typ='" & Typ & "' AND Data>=#" & CType(Row.Tag, StudentAllocation).DataAktywacji & "# AND (Data<=#" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# OR Data IS NULL)").Length > 0 Then
              Row.Cells(.Columns("N" + Typ).Index).Value = CType(DS.Tables("Absence").Compute("Sum(Absencja)", "IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND Typ='" & Typ & "' AND Data>=#" & CType(Row.Tag, StudentAllocation).DataAktywacji & "# AND (Data<=#" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# OR Data IS NULL)"), Integer)
              'Row.Cells(.Columns("N" + Typ).Index).Value = CType(DS.Tables("Absence").Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND Typ='" & Typ & "' AND Data>=#" & CType(Row.Tag, StudentAllocation).DataAktywacji & "# AND (Data<=#" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# OR Data IS NULL)")(0).Item("Absencja"), Integer)
            Else
              Row.Cells(.Columns("N" + Typ).Index).Value = "-"
            End If
          Next
        Next
      End With
      InRefresh = False
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub ComputeAbsenceByPercent()
    Try
      InRefresh = True
      Dim Absencja As Single
      With dgvZachowanie
        For Each Row As DataGridViewRow In .Rows
          Dim LiczbaLekcji As Integer = 0, LiczbaNb As Integer = 0
          LiczbaLekcji = CType(DS.Tables("Lesson").Compute("COUNT(ID)", "Grupa=0 AND Data>=#" & CType(Row.Tag, StudentAllocation).DataAktywacji & "# AND (Data<=#" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# OR #" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# IS NULL)"), Integer)
          If dtGrupa.Select("IdPrzydzial=" & CType(Row.Tag, StudentAllocation).AllocationID).GetLength(0) > 0 Then
            Dim IdPrzedmiot As String = ""
            For Each Przedmiot As DataRow In dtGrupa.Select("IdPrzydzial=" & CType(Row.Tag, StudentAllocation).AllocationID)
              IdPrzedmiot += Przedmiot.Item("IdSzkolaPrzedmiot").ToString & ","
            Next
            IdPrzedmiot = IdPrzedmiot.TrimEnd(",".ToCharArray)
            If IdPrzedmiot.Length > 0 Then LiczbaLekcji += CType(DS.Tables("Lesson").Compute("COUNT(ID)", "Grupa>0 AND Przedmiot IN (" & IdPrzedmiot & ") AND Data>=#" & CType(Row.Tag, StudentAllocation).DataAktywacji & "# AND (Data<=#" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# OR Data IS NULL)"), Integer)
          End If

          If DS.Tables("Absence").Select("IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Typ IN ('U','N') AND Data>=#" & CType(Row.Tag, StudentAllocation).DataAktywacji & "# AND (Data<=#" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# OR Data IS NULL)").Length > 0 Then LiczbaNb = CType(DS.Tables("Absence").Compute("SUM(Absencja)", "IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Typ IN ('U','N') AND Data>=#" & CType(Row.Tag, StudentAllocation).DataAktywacji & "# AND (Data<=#" & CType(Row.Tag, StudentAllocation).DataDeaktywacji & "# OR Data IS NULL)"), Integer)
          If LiczbaLekcji > 0 Then
            Absencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
            Absencja = CType(IIf(Absencja > 100, 100, Absencja), Single)
            Row.Cells(.Columns("N%").Index).Value = Absencja.ToString & "%"
            Row.Cells(.Columns("N%").Index).ToolTipText = "Liczba godzin lekcyjnych: " & LiczbaLekcji & vbNewLine & "Liczba opuszczonych godzin: " & LiczbaNb
          Else
            Row.Cells(.Columns("N%").Index).Value = "-"
          End If
        Next
      End With
      InRefresh = False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub GetData()
    InRefresh = True
    ClearDetails()
    Try
      FetchResult()
      With dgvZachowanie
        For Each Row As DataGridViewRow In .Rows
          If CType(Row.Tag, StudentAllocation).Status = False Then Row.Cells(.Columns("Zachowanie").Index).ReadOnly = True
          If DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "'").Length > 0 Then
            Row.Cells(.Columns("Zachowanie").Index).Value = CType(DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "'")(0).Item("IdOcena"), Integer)
            Row.Cells(.Columns("Zachowanie").Index).Tag = CType(DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "'")(0).Item("ID"), String)
            Row.Cells(.Columns("Zachowanie").Index).ToolTipText = "Data wystawienia: " + CType(DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "'")(0).Item("Data"), DateTime).ToString
          Else
            Row.Cells(.Columns("Zachowanie").Index).Value = Nothing
            Row.Cells(.Columns("Zachowanie").Index).Tag = Nothing
            Row.Cells(.Columns("Zachowanie").Index).ToolTipText = Nothing
          End If
        Next
        .ClearSelection()
        If .RowCount > 0 Then .CurrentCell = .Rows(IntRowIndex).Cells(.Columns("Zachowanie").Index)
      End With

      InRefresh = False
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try
  End Sub

  Private Sub dgvZestawienieOcen_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZachowanie.CellEnter
    Try
      If e.ColumnIndex = dgvZachowanie.Columns.Count - 1 Then
        IntRowIndex = e.RowIndex
        IntColumnIndex = e.ColumnIndex
        Me.dgvZachowanie.Columns(e.ColumnIndex).HeaderCell.Style.Font = New Font(Me.dgvZachowanie.Font, FontStyle.Bold)
        If CType(dgvZachowanie.Columns(e.ColumnIndex).Tag, BehaviourColProperty).Lock = False Then
          If CType(dgvZachowanie.Rows(e.RowIndex).Tag, StudentAllocation).Status AndAlso (GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString) Then
            dgvZachowanie.CurrentCell.ReadOnly = False
          Else
            dgvZachowanie.CurrentCell.ReadOnly = True
          End If
        Else
          dgvZachowanie.CurrentCell.ReadOnly = True
        End If

        If Me.dgvZachowanie.CurrentCell.Tag IsNot Nothing Then
          If CType(dgvZachowanie.Columns(e.ColumnIndex).Tag, BehaviourColProperty).Lock = False AndAlso (GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString) Then cmdDelete.Enabled = True
          'If GlobalValues.gblEnableAdminCommands OrElse GlobalValues.AppUser.Login = TutorLogin Then cmdDelete.Enabled = True
          Dim FoundRow() As DataRow
          FoundRow = DTWyniki.Select("IdUczen='" & CType(dgvZachowanie.Rows(e.RowIndex).Tag, StudentAllocation).ID & "'")
          Dim User, Owner As String
          User = CType(FoundRow(0).Item("User"), String).ToLower.Trim
          Owner = CType(FoundRow(0).Item("Owner"), String).ToLower.Trim
          If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
            lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (W³: ", GlobalValues.Users.Item(Owner).ToString, ")")
          Else
            Me.lblUser.Text = User & " (W³: " & Owner & ")"
          End If
          Me.lblIP.Text = FoundRow(0).Item("ComputerIP").ToString
          Me.lblData.Text = FoundRow(0).Item("Version").ToString
          'Else
          '  cmdDelete.Enabled = False
          '  'dgvZachowanie.CurrentCell.ReadOnly = True
          '  Me.lblUser.Text = ""
          '  Me.lblIP.Text = ""
          '  Me.lblData.Text = ""
        End If
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub dgvZestawienieOcen_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZachowanie.CellLeave
    cmdDelete.Enabled = False
    Me.dgvZachowanie.Columns(e.ColumnIndex).HeaderCell.Style.Font = Me.dgvZachowanie.Font
    Me.lblUser.Text = ""
    Me.lblIP.Text = ""
    Me.lblData.Text = ""
  End Sub
  Private Sub dgvZestawienieOcen_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZachowanie.RowEnter
    If InRefresh Then Exit Sub
    For i As Integer = 0 To dgvZachowanie.Columns.Count - 1
      dgvZachowanie.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.White
      dgvZachowanie.Rows(e.RowIndex).Cells(i).Style.BackColor = Color.Orange
      dgvZachowanie.Rows(e.RowIndex).Cells(i).Style.Font = New Font(dgvZachowanie.Font, FontStyle.Bold)
    Next
    If CType(dgvZachowanie.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
      dgvZachowanie.Rows(e.RowIndex).Cells(0).Style.Font = New Font(dgvZachowanie.Font, FontStyle.Bold Or FontStyle.Strikeout)
      dgvZachowanie.Rows(e.RowIndex).Cells(1).Style.Font = New Font(dgvZachowanie.Font, FontStyle.Bold Or FontStyle.Strikeout)

    End If

  End Sub

  Private Sub dgvZestawienieOcen_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZachowanie.RowLeave
    Try
      With dgvZachowanie
        For i As Integer = 0 To .Columns.Count - 1
          .Rows(e.RowIndex).Cells(i).Style.BackColor = .Columns(i).CellTemplate.Style.BackColor 'Me.dgvZachowanie.Rows(e.RowIndex).Cells(i).Style.BackColor
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = .Columns(i).CellTemplate.Style.ForeColor
          .Rows(e.RowIndex).Cells(i).Style.Font = .Columns(i).CellTemplate.Style.Font '.Font
        Next
        If CType(.Rows(e.RowIndex).Tag, StudentAllocation).NauczanieIndywidualne IsNot Nothing Then
          For i As Integer = 0 To .Columns.Count - 1
            .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
          Next
        End If

        If CType(.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
          .Rows(e.RowIndex).Cells(0).Style.Font = New Font(.Font, FontStyle.Strikeout)
          .Rows(e.RowIndex).Cells(1).Style.Font = New Font(.Font, FontStyle.Strikeout)
          For i As Integer = 0 To .Columns.Count - 1
            .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.SlateGray
            .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
          Next
        End If
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZachowanie.CellValueChanged
    Try
      If e.RowIndex < 0 Or InRefresh Then Exit Sub
      Dim DBA As New DataBaseAction, W As New WynikiSQL, Data As DateTime
      'dtData.Value = dtData.Value.Date + Now.TimeOfDay
      Data = New DateTime(dtData.Value.Year, dtData.Value.Month, dtData.Value.Day, Now.Hour, Now.Minute, Now.Second)

      If Me.dgvZachowanie.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag IsNot Nothing Then
        Dim cmd As MySqlCommand = DBA.CreateCommand(W.UpdateResult)
        cmd.Parameters.AddWithValue("?IdOcena", Me.dgvZachowanie.CurrentCell.Value.ToString)
        cmd.Parameters.AddWithValue("?IdWynik", dgvZachowanie.CurrentCell.Tag)
        cmd.Parameters.AddWithValue("?Data", Data)
        cmd.ExecuteNonQuery()
      Else
        Dim cmd As MySqlCommand = DBA.CreateCommand(W.InsertResult)
        cmd.Parameters.AddWithValue("?IdUczen", CType(dgvZachowanie.CurrentRow.Tag, StudentAllocation).ID)
        cmd.Parameters.AddWithValue("?IdOcena", Me.dgvZachowanie.CurrentCell.Value.ToString)
        cmd.Parameters.AddWithValue("?IdKolumna", CType(dgvZachowanie.Columns("Zachowanie").Tag, BehaviourColProperty).ID.ToString)
        cmd.Parameters.AddWithValue("?Data", Data)
        cmd.ExecuteNonQuery()
        'End If
      End If
      GetData()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    DeleteScore()
  End Sub
  Private Sub DeleteScore()
    If MessageBox.Show("Czy na pewno chcesz usun¹æ zaznaczone pozycje?", "Belfer .NET", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, W As New WynikiSQL, MyTran As MySqlTransaction
      MyTran = GlobalValues.gblConn.BeginTransaction
      Try
        For Each Cell As DataGridViewCell In Me.dgvZachowanie.SelectedCells
          If Cell.Tag IsNot Nothing Then
            Dim cmd As MySqlCommand = DBA.CreateCommand(W.DeleteResult)
            cmd.Transaction = MyTran
            cmd.Parameters.AddWithValue("?IdWynik", Cell.Tag.ToString)
            cmd.ExecuteNonQuery()
          End If
          'If Cell.FormattedValue.ToString.Length > 0 Then DBA.ApplyChanges(CS.DeleteString("wyniki", "ID", Cell.Tag.ToString))
        Next
        MyTran.Commit()
        Me.GetData()

      Catch mex As MySqlException
        MyTran.Rollback()
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MyTran.Rollback()
        MessageBox.Show(ex.Message)

      End Try
    End If

  End Sub


  Private Sub nudSemestr_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSemestr.ValueChanged
    Try
      'If Me.nudSemestr.Created AndAlso Me.InRefresh = False Then
      '  IntRowIndex = 0
      '  IntColumnIndex = dgvZachowanie.Columns("Zachowanie").Index
      '  dgvZachowanie.Columns("Zachowanie").Tag = IIf(nudSemestr.Value = 1, "Semestr I", "Ocena Roczna").ToString
      '  RefreshData()
      'End If
      If Me.nudSemestr.Created Then 'AndAlso Me.InRefresh = False Then
        'Dim CurrentDate As Date
        'CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
        Dim CH As New CalcHelper
        'dtData.Tag = 1
        If CurrentDate > dtData.MaxDate Then
          If nudSemestr.Value = 1 Then
            dtData.MaxDate = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1)
            dtData.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
            'dtData.Tag = 0
            dtData.Value = If(CurrentDate > dtData.MaxDate, dtData.MaxDate, CurrentDate)
          Else
            'dtData.Tag = 1
            dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
            dtData.MinDate = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
            'dtData.Tag = 0
            dtData.Value = If(CurrentDate < dtData.MinDate, dtData.MinDate, CurrentDate)
          End If
        ElseIf CurrentDate < dtData.MinDate Then
          If nudSemestr.Value = 1 Then
            'dtData.Tag = 1
            dtData.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
            dtData.MaxDate = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1)
            'dtData.Tag = 0
            dtData.Value = If(CurrentDate > dtData.MaxDate, dtData.MaxDate, CurrentDate)
          Else
            'dtData.Tag = 1
            dtData.MinDate = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
            dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
            'dtData.Tag = 0
            dtData.Value = If(CurrentDate < dtData.MinDate, dtData.MinDate, CurrentDate)
          End If
        Else
          If nudSemestr.Value = 1 Then
            'dtData.Tag = 1
            dtData.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
            dtData.MaxDate = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1)
            'dtData.Tag = 0
            dtData.Value = If(CurrentDate > dtData.MaxDate, dtData.MaxDate, CurrentDate)
          Else
            'dtData.Tag = 1
            dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
            dtData.MinDate = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
            dtData.Value = If(CurrentDate < dtData.MinDate, dtData.MinDate, CurrentDate)
          End If
          'dtData.Value = CurrentDate
        End If
        'dtData.Tag = 0
        'dtData_ValueChanged(Nothing, Nothing)
        If cbKlasa.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  'Private Sub dtData_ValueChanged(sender As Object, e As EventArgs) Handles dtData.ValueChanged
  '  If dtData.Tag.ToString = "0" AndAlso cbKlasa.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)
  'End Sub

  Private Sub dgvZestawienieOcen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvZachowanie.KeyDown
    Try
      If dgvZachowanie.CurrentCell.ColumnIndex < dgvZachowanie.Columns("Zachowanie").Index Then Exit Sub

      If dgvZachowanie.CurrentCell.Tag IsNot Nothing AndAlso (e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back) Then
        If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString Then
          'If GlobalValues.gblEnableAdminCommands OrElse GlobalValues.AppUser.Login = TutorLogin Then
          Me.DeleteScore()
        Else
          MessageBox.Show("Nie mo¿esz usun¹æ tej wartoœci, poniewa¿ nie posiadasz wystarczaj¹cych uprawnieñ.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub


  Private Sub dgvZestawienieOcen_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvZachowanie.ColumnHeaderMouseClick

    With dgvZachowanie
      .CurrentCell = .Rows(IntRowIndex).Cells(e.ColumnIndex)
    End With

  End Sub

  Private Sub dgvZestawienieOcen_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvZachowanie.DataError
    MessageBox.Show(e.Exception.Message)
  End Sub

  Private Sub DelResultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelResultToolStripMenuItem.Click
    DeleteScore()
  End Sub


  Private Sub dgvZestawienieOcen_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvZachowanie.CellMouseUp
    Try
      With dgvZachowanie
        If e.RowIndex < 0 Then
          .CurrentCell = .Rows(IntRowIndex).Cells(e.ColumnIndex)
          DelResultToolStripMenuItem.Enabled = False
        Else
          .CurrentCell = .Rows(e.RowIndex).Cells(e.ColumnIndex)
          If e.Button = Windows.Forms.MouseButtons.Right AndAlso .Rows(e.RowIndex).Cells(e.ColumnIndex).Selected AndAlso .Rows(e.RowIndex).Cells(e.ColumnIndex).Tag IsNot Nothing Then
            If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString Then
              'If GlobalValues.gblEnableAdminCommands OrElse GlobalValues.AppUser.Login = TutorLogin Then
              DelResultToolStripMenuItem.Enabled = True
            Else
              DelResultToolStripMenuItem.Enabled = False
            End If
          Else
            DelResultToolStripMenuItem.Enabled = False
          End If
        End If
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub



  Private Sub dgvZestawienieOcen_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvZachowanie.EditingControlShowing
    Try
      e.CellStyle.BackColor = Color.White
      e.CellStyle.ForeColor = Color.Black
      If dgvZachowanie.CurrentCell.ColumnIndex = dgvZachowanie.Columns("Zachowanie").Index Then
        RemoveHandler e.Control.KeyDown, AddressOf InsertMark
        AddHandler e.Control.KeyDown, AddressOf InsertMark
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub InsertMark(sender As Object, e As KeyEventArgs)
    Try
      Dim SH As New SeekHelper

      Select Case e.KeyCode
        Case Keys.NumPad0
          SH.FindComboItem(CType(sender, ComboBox), EndMarks.Item(0).Nazwa)
        Case Keys.NumPad1
          SH.FindComboItem(CType(sender, ComboBox), EndMarks.Item(1).Nazwa)
        Case Keys.NumPad2
          SH.FindComboItem(CType(sender, ComboBox), EndMarks.Item(2).Nazwa)
        Case Keys.NumPad3
          SH.FindComboItem(CType(sender, ComboBox), EndMarks.Item(3).Nazwa)
        Case Keys.NumPad4
          SH.FindComboItem(CType(sender, ComboBox), EndMarks.Item(4).Nazwa)
        Case Keys.NumPad5
          SH.FindComboItem(CType(sender, ComboBox), EndMarks.Item(5).Nazwa)
          Exit Select
        Case Keys.NumPad6
          SH.FindComboItem(CType(sender, ComboBox), EndMarks.Item(6).Nazwa)

      End Select

    Catch ex As Exception

    End Try
  End Sub
  Private Sub GetEndMarks()
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing
    Dim W As New WynikiSQL
    Try
      R = DBA.GetReader(W.SelectBehaviorMarksByImportance())
      EndMarks = New List(Of EndMark)
      While R.Read
        EndMarks.Add(New EndMark With {.Waga = CInt(R.Item("Waga")), .Nazwa = R.Item("Nazwa").ToString})
      End While
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try
  End Sub

  'Private Sub dgvZestawienieOcen_Paint(sender As Object, e As PaintEventArgs) Handles dgvZachowanie.Paint
  '  Try
  '    MyBase.OnPaint(e)
  '  Catch ex As Exception
  '    Me.Invalidate()
  '  End Try
  'End Sub
  Private Sub dgvZestawienieOcen_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
    Try
      With dgvZachowanie
        Dim rtHeader As Rectangle = .DisplayRectangle
        rtHeader.Height = CType(.ColumnHeadersHeight / 2, Integer)
        .Invalidate(rtHeader)
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
    Try
      With dgvZachowanie
        If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then Exit Sub
        Dim rtHeader As Rectangle = .DisplayRectangle
        rtHeader.Height = CType(.ColumnHeadersHeight / 2, Integer)
        .Invalidate(rtHeader)
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
    Try
      With dgvZachowanie
        If .Columns.Count < 1 Then Exit Sub
        Dim StrFormat As New StringFormat()
        StrFormat.Alignment = StringAlignment.Center
        StrFormat.LineAlignment = StringAlignment.Center

        Dim NrCol As Rectangle = .GetCellDisplayRectangle(0, -1, True)
        NrCol.X += 1
        NrCol.Y += 1
        NrCol.Width = NrCol.Width - 2
        NrCol.Height = NrCol.Height - 2
        
        e.Graphics.FillRectangle(New SolidBrush(.ColumnHeadersDefaultCellStyle.BackColor), NrCol)
        e.Graphics.DrawString("Nr", New Font(.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(.ColumnHeadersDefaultCellStyle.ForeColor), NrCol, StrFormat)

        Dim Name As Rectangle = .GetCellDisplayRectangle(1, -1, True)
        e.Graphics.DrawLine(Pens.Black, Name.X, Name.Y, Name.X, Name.Height)
        Name.X += 1
        Name.Y += 1
        Name.Width = Name.Width - 2
        Name.Height = Name.Height - 2
        e.Graphics.FillRectangle(New SolidBrush(.ColumnHeadersDefaultCellStyle.BackColor), Name)
        e.Graphics.DrawString("Nazwisko i imiê", New Font(.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(.ColumnHeadersDefaultCellStyle.ForeColor), Name, StrFormat)

        Dim r1 As Rectangle = .GetCellDisplayRectangle(2, -1, True)
        e.Graphics.DrawLine(Pens.Black, r1.X, r1.Y, r1.X, r1.Height)
        'e.Graphics.DrawLine(Pens.Black, r1.X + r1.Width, r1.Y + CInt(r1.Height / 2), r1.X + r1.Width, CInt(r1.Height))
        r1.X += 1
        r1.Y += 1
        r1.Width = r1.Width * 2 - 2
        r1.Height = CType(r1.Height / 2 - 2, Integer)
        e.Graphics.FillRectangle(New SolidBrush(.ColumnHeadersDefaultCellStyle.BackColor), r1)
        e.Graphics.DrawString("Uwagi", New Font(.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.Black), r1, StrFormat)

        r1 = .GetCellDisplayRectangle(4, -1, True)
        e.Graphics.DrawLine(Pens.Black, r1.X, r1.Y, r1.X, r1.Height)
        r1.X += 1
        r1.Y += 1
        r1.Width = r1.Width * 4 - 2
        r1.Height = CType(r1.Height / 2 - 2, Integer)
        e.Graphics.FillRectangle(New SolidBrush(.ColumnHeadersDefaultCellStyle.BackColor), r1)
        e.Graphics.DrawString("Absencja", New Font(.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.Black), r1, StrFormat)

        Dim r2 As Rectangle = .GetCellDisplayRectangle(.ColumnCount - 1, -1, True)
        e.Graphics.DrawLine(Pens.Black, r2.X, r2.Y, r2.X, r2.Height)
        r2.X += 1
        r2.Y += 1
        r2.Width = r2.Width - 2
        r2.Height = CType(r2.Height - 2, Integer)

        e.Graphics.FillRectangle(New SolidBrush(.ColumnHeadersDefaultCellStyle.BackColor), r2)
        e.Graphics.DrawString(IIf(nudSemestr.Value = 1, "Semestr I", "Ocena roczna").ToString, New Font(.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.Black), r2, StrFormat)

      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs)
    Try
      If e.RowIndex = -1 AndAlso e.ColumnIndex > -1 Then
        Dim r2 As Rectangle = e.CellBounds
        r2.Y += CType(e.CellBounds.Height / 2, Integer)
        r2.Height = CType(e.CellBounds.Height / 2, Integer)
        e.PaintBackground(r2, True)
        e.PaintContent(r2)
        e.Handled = True
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub frmZachowanie_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    Me.Height += 1
    Me.Height -= 1
  End Sub

  Private Sub dgvZachowanie_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvZachowanie.RowPostPaint
    If (e.RowIndex + 1) Mod 10 = 0 Then
      e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 2, e.RowBounds.Right, e.RowBounds.Bottom - 2)
    End If
  End Sub
End Class

Public Class BehaviourColProperty
  Public Property ID As Integer
  Public Property Lock As Boolean
End Class