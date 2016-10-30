Public Class frmAbsencja
  Private DTAbsence, DTGrupa, dtIndividualStaff As DataTable ', Wychowawca As String = ""
  Private IntRowIndex As Integer = 0, IntColumnIndex As Integer = 2 ', InRefresh As Boolean
  Private SchoolWeek As List(Of SchoolDay), Student As List(Of GroupMember), InRefresh As Boolean
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.AbsencjaToolStripMenuItem.Enabled = True
    MainForm.cmdAbsencja.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.AbsencjaToolStripMenuItem.Enabled = True
    MainForm.cmdAbsencja.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmZachowanie_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    Me.Height += 1
    Me.Height -= 1
  End Sub
  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    'Dim DBA As New DataBaseAction, W As New WynikiSQL
    DataGridConfig(dgvAbsencja)
    cmdTimeSpan.Text = mcData.SelectionStart.ToLongDateString & " (" & mcData.SelectionStart.ToString("dddd") & ") " & Chr(151) & " " & mcData.SelectionEnd.ToLongDateString & " (" & mcData.SelectionEnd.ToString("dddd") & ")"
    ApplyNewConfig()
  End Sub
  Private Sub DataGridConfig(ByVal dgvName As DataGridView)
    With dgvName
      .SelectionMode = DataGridViewSelectionMode.CellSelect
      .AutoGenerateColumns = False
      '.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 40
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
      RemoveHandler .CellPainting, AddressOf dgvZestawienieOcen_CellPainting
      RemoveHandler .Paint, AddressOf dgvZestawienieOcen_Paint
      RemoveHandler .Scroll, AddressOf dgvZestawienieOcen_Scroll
      RemoveHandler .ColumnWidthChanged, AddressOf dgvZestawienieOcen_ColumnWidthChanged

      AddHandler .CellPainting, AddressOf dgvZestawienieOcen_CellPainting
      AddHandler .Paint, AddressOf dgvZestawienieOcen_Paint
      AddHandler .Scroll, AddressOf dgvZestawienieOcen_Scroll
      AddHandler .ColumnWidthChanged, AddressOf dgvZestawienieOcen_ColumnWidthChanged
    End With
  End Sub
  Private Sub ApplyNewConfig()
    Try
      Dim CH As New CalcHelper, StartDate, EndDate As Date
      StartDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      EndDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      If StartDate.DayOfWeek = 0 Then
        StartDate.AddDays(1)
      ElseIf StartDate.DayOfWeek = 6 Then
        StartDate.AddDays(2)
      End If
      If mcData.MaxDate < StartDate Then
        mcData.MaxDate = EndDate
        mcData.MinDate = StartDate
      Else
        mcData.MinDate = StartDate
        mcData.MaxDate = EndDate
      End If
      mcData.GetDisplayRange(True)
      'Dim e As New DateRangeEventArgs(Today, Today)
      Dim e As DateRangeEventArgs = Nothing
      If Today < mcData.MinDate Then
        e = New DateRangeEventArgs(mcData.MinDate, Nothing)
      ElseIf Today > mcData.MaxDate Then
        e = New DateRangeEventArgs(mcData.MaxDate, Nothing)
      Else
        e = New DateRangeEventArgs(Today, Nothing)
      End If

      mcData_DateChanged(mcData, e)
      'SetDateSelection(Today)

      FillKlasa()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub SetDateSelection(SelectedDate As Date)
    Dim CH As New CalcHelper

    If CH.StartDateOfWeek(SelectedDate) < mcData.MinDate Then
      mcData.SelectionStart = mcData.MinDate
    Else
      mcData.SelectionStart = CH.StartDateOfWeek(SelectedDate)
    End If
    If CH.EndDateOfWeek(SelectedDate) <= mcData.MaxDate Then
      mcData.SelectionEnd = CH.EndDateOfWeek(SelectedDate)
    Else
      mcData.SelectionEnd = mcData.MaxDate
    End If
    'End If
  End Sub
  Private Function FetchStudent() As DataTable

    Dim DBA As New DataBaseAction, W As New WynikiSQL, T As New TematSQL
    Dim SelectString As String = ""
    If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
      SelectString = T.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, mcData.SelectionEnd)
      'dtGrupa = Nothing
      dtIndividualStaff = Nothing
    Else
      SelectString = W.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
      'dtGrupa = DBA.GetDataTable(W.SelectGrupa(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
      dtIndividualStaff = DBA.GetDataTable(T.SelectIndividualStaff(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString))
    End If
    'dtStudent = DBA.GetDataTable(SelectString)
    Return DBA.GetDataTable(SelectString)
  End Function
  Private Sub FetchAbsence(Klasa As String, StartDate As Date, EndDate As Date)
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL
    DTAbsence = DBA.GetDataTable(A.SelectAbsence(Klasa, StartDate, EndDate))
    DTAbsence.TableName = "Absencja"
  End Sub

  Private Sub FillKlasa()
    Dim W As New WynikiSQL
    LoadClassItems(cbKlasa, If(chkVirtual.Checked, W.SelectVirtualClasses(My.Settings.IdSchool, My.Settings.SchoolYear), W.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear)))
    If My.Settings.ClassName.Length > 0 Then
      For Each Item As SchoolClassComboItem In cbKlasa.Items
        If Item.ID = CType(My.Settings.ClassName, Integer) Then
          cbKlasa.SelectedIndex = cbKlasa.Items.IndexOf(Item)
          Exit For
        End If
      Next
    End If
  End Sub
  Private Sub LoadClassItems(cb As ComboBox, SelectString As String)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction ', W As New WynikiSQL 'K As New KolumnaSQL
    cb.Items.Clear()
    Try
      R = DBA.GetReader(SelectString)
      While R.Read()
        cb.Items.Add(New SchoolClassComboItem(R.GetInt32("IdKlasa"), R.GetString("Nazwa_Klasy"), R.GetBoolean("Virtual")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged

    dgvAbsencja.Rows.Clear()
    dgvAbsencja.Columns.Clear()
    'Me.dgvZestawienieOcen.Enabled = False
    IntRowIndex = 0
    IntColumnIndex = 2
    RefreshData()

  End Sub
  Private Sub RefreshData()
    Cursor = Cursors.WaitCursor
    InRefresh = True
    'GetTutor()
    GetObjectGroup()
    GetSchoolWeek()
    'GetGroupMember()
    SetColumns(dgvAbsencja)
    SetRows(dgvAbsencja)
    Me.GetData()
    Me.dgvAbsencja.Focus()
    Me.dgvAbsencja.Enabled = True
    InRefresh = False
    Cursor = Cursors.Default
  End Sub

  Private Sub ClearDetails()
    Me.lblUser.Text = ""
    Me.lblIP.Text = ""
    Me.lblData.Text = ""
  End Sub
  Private Sub SetColumns(ByVal dgvName As DataGridView)
    Try
      With dgvName
        .Columns.Clear()
        Dim NameCol, NrCol As New DataGridViewColumn
        With NrCol
          .Name = "Nr"
          .HeaderText = ""
          .Width = 30
          .CellTemplate = New DataGridViewTextBoxCell()
          .CellTemplate.Style.BackColor = Color.LightYellow
          .CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
          '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .ToolTipText = "Numer ucznia w dzienniku"
          .ReadOnly = True
          .Frozen = True
          .SortMode = DataGridViewColumnSortMode.NotSortable
        End With

        'NrCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NrCol)
        With NameCol
          .Name = "Nazwisko"
          .HeaderText = ""
          .Width = 200
          .CellTemplate = New DataGridViewTextBoxCell()
          .CellTemplate.Style.BackColor = Color.LightYellow
          '.DefaultCellStyle.BackColor = Color.LightYellow
          .ToolTipText = "Nazwisko i imię ucznia"
          .ReadOnly = True
          .Frozen = True
          .SortMode = DataGridViewColumnSortMode.NotSortable
        End With


        'NameCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NameCol)

        Dim ShiftColor As Boolean = False, GridColor() As Color = {Color.Khaki, Color.White}
        For i As Integer = 0 To SchoolWeek.Count - 1
          ShiftColor = Not ShiftColor
          For j As Integer = 0 To SchoolWeek.Item(i).Plan.Count - 1
            Dim NrLekcji As New DataGridViewColumn
            With NrLekcji
              .Name = String.Concat(CType(SchoolWeek.Item(i).Value.DayOfWeek, Byte), "#", SchoolWeek.Item(i).Plan(j).Nr.ToString)
              .HeaderText = SchoolWeek.Item(i).Plan(j).Nr.ToString 'Reader.GetString("NrLekcji")
              .Tag = New SchoolLessonByDay With {.DataZajec = SchoolWeek.Item(i).Value, .Lekcja = SchoolWeek.Item(i).Plan(j).Lekcja}
              .ToolTipText = String.Concat("Dzień tygodnia: ", SchoolWeek.Item(i).Value.ToString("dddd"), " {", SchoolWeek.Item(i).Value.ToShortDateString, "}") & vbCr & "Przedmiot: " & SchoolWeek.Item(i).Plan(j).Lekcja.Item(0).Przedmiot
              If SchoolWeek.Item(i).Plan(j).Lekcja.Count > 1 Then
                Dim Przedmiot As String = "Przedmiot: "
                For Each L As SchoolLesson In SchoolWeek.Item(i).Plan(j).Lekcja
                  Przedmiot += L.Przedmiot & "; "
                Next
                Przedmiot = Przedmiot.TrimEnd("; ".ToCharArray)
                .ToolTipText = String.Concat("Dzień tygodnia: ", SchoolWeek.Item(i).Value.ToString("dddd"), " {", SchoolWeek.Item(i).Value.ToShortDateString, "}") & vbCr & Przedmiot
              End If
              'dokończyć grupy przedmiottowe
              .Width = 20
              .CellTemplate = New DataGridViewTextBoxCell()
              .CellTemplate.Style.Font = New Font(dgvAbsencja.Font, FontStyle.Bold)
              .CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
              .CellTemplate.Style.BackColor = GridColor(Convert.ToInt32(ShiftColor))
              .ReadOnly = True
              .SortMode = DataGridViewColumnSortMode.NotSortable
              .ContextMenuStrip = cmsAbsencja
            End With
            .Columns.Add(NrLekcji)
          Next
        Next

        For Each S As String In New String() {"U", "N", "S"}
          Dim UColumn As New DataGridViewTextBoxColumn
          With UColumn
            .HeaderText = S
            .Width = 30 '65
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate = New DataGridViewTextBoxCell
            .Name = S
            '.Frozen = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            '.HeaderCell.Style.BackColor = Color.AliceBlue
            .CellTemplate.Style.BackColor = Color.MintCream   'Color.AliceBlue
            .CellTemplate.Style.ForeColor = Color.Blue
            '.CellTemplate.Style.Font = New Font(dgvAbsencja.Font, FontStyle.Bold)
            If S = "U" Then
              .ToolTipText = "Liczba godzin usprawiedliwionych"
            ElseIf S = "N" Then
              .ToolTipText = "Liczba godzin nieusprawiedliwionych"
            Else
              .ToolTipText = "Liczba spóźnień"
            End If
            '.SortMode = DataGridViewColumnSortMode.Programmatic
          End With
          .Columns.Add(UColumn)
        Next
      End With

      'Catch mex As MySqlException
      '  MessageBox.Show(mex.Message & vbNewLine & "Nr błędu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'Reader.Close()
    End Try

  End Sub
  Private Sub ClearCellData()
    For Each R As DataGridViewRow In dgvAbsencja.Rows
      If CType(R.Tag, StudentAllocation).Status AndAlso CType(R.Tag, StudentAllocation).NauczanieIndywidualne Is Nothing Then
        For i As Integer = 2 To R.Cells.Count - 1
          R.Cells(i).Tag = Nothing
          R.Cells(i).Value = Nothing
        Next
      Else
        'ElseIf CType(R.Tag, StudentAllocation).Status AndAlso CType(R.Tag, StudentAllocation).NauczanieIndywidualne IsNot Nothing Then
        For i As Integer = 2 To R.Cells.Count - 1
          If R.Cells(i).FormattedValue.ToString <> "X" Then
            R.Cells(i).Tag = Nothing
            R.Cells(i).Value = Nothing
          End If
        Next
      End If
    Next
  End Sub
  Private Sub GetData()
    'InRefresh = True
    Try
      ClearDetails()
      FetchAbsence(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, mcData.SelectionStart, mcData.SelectionEnd)
      'SetRows(dgvAbsencja)
      ClearCellData()
      With dgvAbsencja
        For Each R As DataGridViewRow In .Rows
          Dim U As Byte = 0, N As Byte = 0, S As Byte = 0
          For Each D As SchoolDay In SchoolWeek
            Dim dvAbsenceByDayOfWeek As DataView = New DataView(DTAbsence, "DzienTygodnia=" & D.Value.DayOfWeek & " AND IdUczen=" & CType(R.Tag, StudentAllocation).ID, "NrLekcji", DataViewRowState.CurrentRows)
            For Each NrLekcji As DataRow In dvAbsenceByDayOfWeek.ToTable(True, "NrLekcji").Rows
              Dim dvAbsenceByDayBySchoolHour As DataTable = New DataView(dvAbsenceByDayOfWeek.ToTable, "NrLekcji=" & NrLekcji.Item("NrLekcji").ToString, "NrLekcji", DataViewRowState.CurrentRows).ToTable
              SetCellContent(R, dvAbsenceByDayBySchoolHour.Rows(0).Item("Typ").ToString, String.Concat(CByte(D.Value.DayOfWeek), "#", NrLekcji.Item("NrLekcji").ToString), dvAbsenceByDayBySchoolHour.Rows)
            Next
            SetTotalAbsence(U, N, S, dvAbsenceByDayOfWeek.ToTable(True, "NrLekcji", "Typ"))
          Next
          R.Cells("U").Value = U
          R.Cells("N").Value = N
          R.Cells("S").Value = S
        Next
        .ClearSelection()
        .CurrentCell = .Rows(IntRowIndex).Cells(IntColumnIndex)
      End With

    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub SetTotalAbsence(ByRef U As Byte, ByRef N As Byte, ByRef S As Byte, DT As DataTable)
    U += CType(DT.Compute("Count(NrLekcji)", "Typ='u'"), Byte)
    N += CType(DT.Compute("Count(NrLekcji)", "Typ='n'"), Byte)
    S += CType(DT.Compute("Count(NrLekcji)", "Typ='s'"), Byte)
  End Sub
  Private Sub SetCellContent(Row As DataGridViewRow, AbsenceType As String, CellName As String, Absence As DataRowCollection)
    If AbsenceType = "n" Then
      Row.Cells(CellName).Style.ForeColor = Color.Red
    ElseIf AbsenceType = "u" Then
      Row.Cells(CellName).Style.ForeColor = Color.Green
    Else
      Row.Cells(CellName).Style.ForeColor = Color.SteelBlue
    End If
    Row.Cells(CellName).Value = AbsenceType.ToUpper
    Row.Cells(CellName).ToolTipText = dgvAbsencja.Columns(CellName).ToolTipText
    Dim Absencja As New List(Of AbsenceDetails)
    'Dim Absencja As New List(Of Integer)

    For Each DR As DataRow In Absence
      Absencja.Add(New AbsenceDetails With {.ID = CInt(DR.Item("ID")), .User = DR.Item("User").ToString, .Owner = DR.Item("Owner").ToString, .ComputerIP = DR.Item("ComputerIP").ToString, .Version = DR.Item("Version").ToString})
    Next
    Row.Cells(CellName).Tag = Absencja
  End Sub
  Private Sub cmdTimeSpan_Click(sender As Object, e As EventArgs) Handles cmdTimeSpan.Click
    mcData.Visible = Not mcData.Visible
  End Sub

  Private Sub mcData_DateChanged(sender As Object, e As DateRangeEventArgs) Handles mcData.DateSelected
    Try
      SetDateSelection(e.Start)
      cmdTimeSpan.Text = mcData.SelectionStart.ToLongDateString & " (" & mcData.SelectionStart.ToString("dddd") & ") " & Chr(151) & " " & mcData.SelectionEnd.ToLongDateString & " (" & mcData.SelectionEnd.ToString("dddd") & ")"

      mcData.Visible = False
      If cbKlasa.SelectedItem IsNot Nothing Then
        GetSchoolWeek()
        cbKlasa_SelectedIndexChanged(Nothing, Nothing)
      End If
      'mcData.GetDisplayRange(True)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Function GetIndividualCourse(IdPrzydzial As String, Data As Date) As IndividualCourse
    Dim IndividualStaff() As DataRow ', NI As New IndividualCourse
    IndividualStaff = dtIndividualStaff.Select("IdPrzydzial=" & IdPrzydzial & " AND DataAktywacji<=#" & Data.ToShortDateString & "# AND (DataDeaktywacji>#" & Data.ToShortDateString & "# OR DataDeaktywacji is null)")
    Dim S As New List(Of IndividualStaff)
    For Each P As DataRow In IndividualStaff
      S.Add(New IndividualStaff With {.ObjectID = CType(P.Item("Przedmiot"), Integer), .StartDate = CType(P.Item("DataAktywacji"), Date), .EndDate = If(IsDBNull(P.Item("DataDeaktywacji")), Nothing, CType(P.Item("DataDeaktywacji"), Date))})
    Next
    If S.Count > 0 Then
      Return New IndividualCourse With {.SchoolObject = S}
    Else
      Return Nothing
    End If
  End Function
 
  Private Function GetGroupMember() As List(Of GroupMember)
    Try
      Dim dtStudent As DataTable = FetchStudent()
      Dim Students = New List(Of GroupMember)
      If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
        For Each Student As DataRow In dtStudent.DefaultView().ToTable(True, "ID", "IdPrzydzial", "NrwDzienniku", "Student", "StatusAktywacji", "DataAktywacji", "DataDeaktywacji", "MasterRecord").Rows
          Students.Add(New GroupMember With {.Allocation = New StudentAllocation With {.ID = CInt(Student.Item("ID")), .AllocationID = CInt(Student.Item("IdPrzydzial")), .Status = CType(Student.Item("StatusAktywacji"), Boolean), .DataAktywacji = If(IsDBNull(Student.Item("DataAktywacji")), Nothing, CType(Student.Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(Student.Item("DataDeaktywacji")), Nothing, CType(Student.Item("DataDeaktywacji"), Date)), .MasterLocation = CBool(Student.Item("MasterRecord"))}, .No = Student.Item("NrwDzienniku").ToString, .Name = Student.Item("Student").ToString})
        Next
      Else
        For Each Student As DataRow In dtStudent.Rows
          Dim NI As IndividualCourse = GetIndividualCourse(Student.Item("IdPrzydzial").ToString, mcData.SelectionEnd)
          Students.Add(New GroupMember With {.Allocation = New StudentAllocation With {.ID = CInt(Student.Item("ID")), .AllocationID = CInt(Student.Item("IdPrzydzial")), .Status = CType(Student.Item("StatusAktywacji"), Boolean), .DataAktywacji = If(IsDBNull(Student.Item("DataAktywacji")), Nothing, CType(Student.Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(Student.Item("DataDeaktywacji")), Nothing, CType(Student.Item("DataDeaktywacji"), Date)), .MasterLocation = CBool(Student.Item("MasterRecord")), .NauczanieIndywidualne = NI}, .No = Student.Item("NrwDzienniku").ToString, .Name = Student.Item("Student").ToString})
        Next
      End If
      'End If

      Return Students
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try
    Return Nothing
  End Function
  Private Sub SetRows(ByVal dgvName As DataGridView)
    Try
      Dim Students As List(Of GroupMember) = GetGroupMember()
      Dim Idx As Integer = 0, ShiftColor As Boolean = False, GridColor() As Color = {Color.LightYellow, Color.MintCream}
      With dgvName
        .Rows.Clear()
        For Each M As GroupMember In Students
          If Idx < 10 Then
            Idx += 1
          Else
            Idx = 1
            ShiftColor = Not ShiftColor
          End If
          'If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
          .Rows.Add(M.No, M.Name)
          .Rows(.Rows.Count - 1).Tag = M.Allocation
          .Rows(.Rows.Count - 1).DefaultCellStyle.BackColor = GridColor(Convert.ToInt32(ShiftColor))
          .Rows(.Rows.Count - 1).Cells(0).ToolTipText = "Data zapisu: " & M.Allocation.DataAktywacji.ToShortDateString
          .Rows(.Rows.Count - 1).Cells(1).ToolTipText = "Data zapisu: " & M.Allocation.DataAktywacji.ToShortDateString

          If M.Allocation.Status Then
            If M.Allocation.NauczanieIndywidualne Is Nothing Then
              .Rows(.Rows.Count - 1).Cells(1).ToolTipText += vbNewLine & "Tryb nauki: normalny"
              For i As Integer = 2 To .ColumnCount - 1 'R.Cells.Count - 1
                .Rows(.Rows.Count - 1).Cells(i).Tag = Nothing
                .Rows(.Rows.Count - 1).Cells(i).Value = Nothing
                .Rows(.Rows.Count - 1).Cells(i).ToolTipText = .Rows(.Rows.Count - 1).Cells(i).OwningColumn.ToolTipText
              Next
            Else
              For i As Integer = 0 To 1
                .Rows(.Rows.Count - 1).Cells(i).ToolTipText += vbNewLine & "Tryb nauki: nauczanie indywidualne " & vbNewLine & vbTab & "Data początkowa: " & M.Allocation.NauczanieIndywidualne.SchoolObject(0).StartDate.ToShortDateString & vbNewLine & vbTab & "Data końcowa: " & If(M.Allocation.NauczanieIndywidualne.SchoolObject(0).EndDate = Nothing, "", M.Allocation.NauczanieIndywidualne.SchoolObject(0).EndDate.ToShortDateString)
                '.Rows(.Rows.Count - 1).Cells(i).Style.ForeColor = Color.SlateGray
                .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
              Next
              For Each D As SchoolDay In SchoolWeek
                For Each H As SchoolHour In D.Plan
                  For Each L As SchoolLesson In H.Lekcja
                    If IsIndividualCourse(M.Allocation.NauczanieIndywidualne.SchoolObject, L.IdPrzedmiot, D.Value) Then
                      .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Value = "X"
                      .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).ToolTipText = .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).OwningColumn.ToolTipText & vbNewLine & "Tryb nauki: nauczanie indywidualne"
                      '.Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Style.ForeColor = Color.SlateGray
                      '.Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Style.BackColor = Color.LightGray
                    Else
                      .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Value = Nothing
                      .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).ToolTipText = .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).OwningColumn.ToolTipText
                    End If
                  Next
                Next
              Next
            End If
          Else
            .Rows(.Rows.Count - 1).Cells(1).ToolTipText += vbNewLine & "Data skreślenia: " & M.Allocation.DataDeaktywacji.ToString
            For i As Integer = 0 To 1
              .Rows(.Rows.Count - 1).Cells(i).Style.Font = New Font(dgvName.Font, FontStyle.Strikeout)
              .Rows(.Rows.Count - 1).Cells(i).Style.ForeColor = Color.SlateGray
              .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
            Next
            If SchoolWeek.Count > 0 AndAlso SchoolWeek(0).Value > M.Allocation.DataDeaktywacji Then
              For i As Integer = .ColumnCount - 3 To .ColumnCount - 1
                .Rows(.Rows.Count - 1).Cells(i).Style.ForeColor = Color.SlateGray
                .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
              Next
            End If

            For Each D As SchoolDay In SchoolWeek
              If D.Value > M.Allocation.DataDeaktywacji AndAlso M.Allocation.MasterLocation Then
                For Each H As SchoolHour In D.Plan
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Style.ForeColor = Color.SlateGray
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Style.BackColor = Color.LightGray
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Value = "X"
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).ToolTipText = "Uczeń skreślony z listy" & vbNewLine & "Data skreślenia: " & M.Allocation.DataDeaktywacji.ToShortDateString
                Next
              ElseIf D.Value > M.Allocation.DataDeaktywacji AndAlso M.Allocation.MasterLocation = False Then
                For Each H As SchoolHour In D.Plan
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Style.ForeColor = Color.SlateGray
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Style.BackColor = Color.LightGray
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).Value = "X"
                  .Rows(.Rows.Count - 1).Cells(String.Concat(CByte(D.Value.DayOfWeek), "#", H.Nr.ToString)).ToolTipText = "Uczeń przeniesiony do innej klasy" & vbNewLine & "Data przeniesienia: " & M.Allocation.DataDeaktywacji.AddDays(1).ToShortDateString
                Next
              End If
            Next
          End If
        Next
        .Enabled = If(.RowCount > 0, True, False)
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & "Nr błędu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try

  End Sub
  Private Function IsIndividualCourse(SchoolObject As List(Of IndividualStaff), ObjectID As Integer, DataZajec As Date) As Boolean
    For Each P As IndividualStaff In SchoolObject
      If P.ObjectID = ObjectID AndAlso DataZajec >= P.StartDate AndAlso (DataZajec <= P.EndDate Or P.EndDate = Nothing) Then Return True
    Next
    Return False
  End Function
  Private Sub GetSchoolWeek()
    Dim DBA As New DataBaseAction
    Try
      Dim A As New AbsencjaSQL, Lekcja As String = "", DT As DataTable
      SchoolWeek = New List(Of SchoolDay)
      For i As Integer = 0 To 4
        Lekcja = A.SelectNrLekcji(mcData.SelectionStart.AddDays(i), CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear)
        DT = DBA.GetDataTable(Lekcja)
        If DT.Rows.Count > 0 Then
          Dim SH As New List(Of SchoolHour)
          For Each R As DataRow In DT.DefaultView.ToTable(True, "NrLekcji").Rows  'T.Select("NrLekcji=" & i)
            Dim SL As New List(Of SchoolLesson)
            For Each R1 As DataRow In DT.Select("NrLekcji=" & R.Item("NrLekcji").ToString)
              SL.Add(New SchoolLesson With {.IdLekcja = CInt(R1.Item("ID")), .IdPrzedmiot = CInt(R1.Item("Przedmiot")), .Przedmiot = R1.Item("Alias").ToString, .Grupa = CByte(R1.Item("Grupa"))})
            Next
            SH.Add(New SchoolHour With {.Nr = CByte(R.Item("NrLekcji")), .Lekcja = SL})
          Next
          SchoolWeek.Add(New SchoolDay With {.Value = mcData.SelectionStart.AddDays(i), .Plan = SH})
        End If
      Next
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try
  End Sub
  Private Sub GetObjectGroup()
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL
    DTGrupa = DBA.GetDataTable(A.SelectObjectGroup(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
  End Sub
  Private Sub cmdForward_Click(sender As Object, e As EventArgs) Handles cmdForward.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim CH As New CalcHelper
      'If mcData.MaxDate >= mcData.SelectionEnd.AddDays(7) Then
      '  mcData.SelectionStart = CH.StartDateOfWeek(mcData.SelectionStart.AddDays(7))
      '  mcData.SelectionEnd = CH.EndDateOfWeek(mcData.SelectionStart)
      'End If
      If mcData.SelectionEnd.AddDays(7) > mcData.MaxDate Then
        mcData.SelectionEnd = mcData.MaxDate
      Else
        mcData.SelectionEnd = mcData.SelectionEnd.AddDays(7)
      End If
      mcData.SelectionStart = CH.StartDateOfWeek(mcData.SelectionEnd)
      Dim ev As New DateRangeEventArgs(mcData.SelectionStart, mcData.SelectionEnd)
      mcData_DateChanged(mcData, ev)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
    Cursor = Cursors.Default
  End Sub

  Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim CH As New CalcHelper
      If mcData.SelectionStart.AddDays(-7) >= mcData.MinDate Then
        mcData.SelectionStart = mcData.SelectionStart.AddDays(-7) 'CH.StartDateOfWeek(mcData.SelectionStart.AddDays(-7))
      Else
        mcData.SelectionStart = mcData.MinDate
      End If
      mcData.SelectionEnd = CH.EndDateOfWeek(mcData.SelectionStart)
      Dim ev As New DateRangeEventArgs(mcData.SelectionStart, mcData.SelectionEnd)
      mcData_DateChanged(mcData, ev)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
    Cursor = Cursors.Default

  End Sub
  Private Sub dgvZestawienieOcen_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
    Try
      Dim rtHeader As Rectangle = Me.dgvAbsencja.DisplayRectangle
      rtHeader.Height = CType(Me.dgvAbsencja.ColumnHeadersHeight / 2, Integer)
      Me.dgvAbsencja.Invalidate(rtHeader)

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
    Try
      If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then Exit Sub
      Dim rtHeader As Rectangle = Me.dgvAbsencja.DisplayRectangle
      rtHeader.Height = CType(Me.dgvAbsencja.ColumnHeadersHeight / 2, Integer)
      Me.dgvAbsencja.Invalidate(rtHeader)

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
    If dgvAbsencja.Columns.Count < 1 Then Exit Sub
    Dim StrFormat As New StringFormat()
    StrFormat.Alignment = StringAlignment.Center
    StrFormat.LineAlignment = StringAlignment.Center
    Try

      Dim NrCol As Rectangle = Me.dgvAbsencja.GetCellDisplayRectangle(0, -1, True)

      e.Graphics.DrawString("Nr", New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.ForeColor), NrCol, StrFormat)

      Dim PlaceName As Rectangle = Me.dgvAbsencja.GetCellDisplayRectangle(1, -1, True)

      e.Graphics.DrawString("Nazwisko i imię", New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.ForeColor), PlaceName, StrFormat)

      'Dim y0 As Integer = dgvAbsencja.GetColumnDisplayRectangle(1, True).Height
      Dim ColIndex As Integer = 2
      For i As Integer = 0 To SchoolWeek.Count - 1
        Dim r1 As Rectangle = Me.dgvAbsencja.GetCellDisplayRectangle(ColIndex, -1, True)
        r1.X += 1
        r1.Y += 1
        r1.Width = r1.Width * SchoolWeek.Item(i).Plan.Count - 2 'DS.Tables(3).Rows.Count - 2
        r1.Height = CType(r1.Height / 2 - 2, Integer)
        e.Graphics.FillRectangle(New SolidBrush(Color.Salmon), r1)
        If r1.Width > e.Graphics.MeasureString(SchoolWeek.Item(i).Value.ToString, New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)).Width Then
          e.Graphics.DrawString(SchoolWeek.Item(i).Value.ToString("dddd"), New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.White), r1, StrFormat)

        Else
          e.Graphics.DrawString(SchoolWeek.Item(i).Value.ToString("ddd"), New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.White), r1, StrFormat)
        End If
        'e.Graphics.DrawLine(New Pen(Brushes.Black, 3), r1.X + r1.Width - 1, r1.Y - 1, r1.X + r1.Width - 1, y0)

        ColIndex += SchoolWeek.Item(i).Plan.Count
      Next
      Dim r2 As Rectangle = Me.dgvAbsencja.GetCellDisplayRectangle(dgvAbsencja.ColumnCount - 3, -1, True)
      r2.X += 1
      r2.Y += 1
      r2.Width = r2.Width * 3
      r2.Height = CType(r2.Height / 2 - 2, Integer)

      e.Graphics.FillRectangle(New SolidBrush(Color.Yellow), r2)
      e.Graphics.DrawString("Razem", New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.Red), r2, StrFormat)

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
      'Dim ColIndex As Integer = 1
      'For i As Integer = 0 To SchoolWeek.Count - 1
      '  Dim r As Rectangle = e.CellBounds  'Me.dgvAbsencja.GetCellDisplayRectangle(ColIndex, e.RowIndex, True)
      '  e.PaintBackground(e.ClipBounds, True)
      '  e.PaintContent(e.ClipBounds)
      '  e.Graphics.DrawLine(New Pen(Brushes.Black, 3), r.X + r.Width - 1, r.Y - 1, r.X + r.Width - 1, r.Y + r.Height - 1)
      '  e.Handled = True
      '  ColIndex += SchoolWeek.Item(i).Plan.Count
      'Next
      'If e.ColumnIndex = 1 Then
      '  'e.Graphics.FillRectangle(New SolidBrush(e.CellStyle.BackColor), e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 2, e.CellBounds.Height - 1)
      '  e.PaintBackground(e.ClipBounds, True)
      '  e.PaintContent(e.ClipBounds)
      '  e.Graphics.DrawLine(New Pen(Brushes.Black, 3), e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y - 1, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1)
      '  e.Handled = True
      'End If

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Resize(sender As Object, e As EventArgs) Handles dgvAbsencja.Resize
    Try
      With dgvAbsencja
        Dim rtHeader As Rectangle = .DisplayRectangle
        rtHeader.Height = .ColumnHeadersHeight
        .Invalidate(rtHeader)
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub dgvAbsencja_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAbsencja.CellEnter
    Try
      If e.ColumnIndex > 1 Then
        With dgvAbsencja
          .Columns(e.ColumnIndex).HeaderCell.Style.Font = New Font(.Font, FontStyle.Bold)

          If .CurrentCell.Tag IsNot Nothing Then
            Dim User, Owner As String
            'User = DTAbsence.Select("DzienTygodnia=" & .Columns(e.ColumnIndex).Name.Substring(0, 1) & " AND IdUczen=" & CType(.Rows(e.RowIndex).Tag, StudentAllocation).ID & " AND NrLekcji=" & .Columns(e.ColumnIndex).HeaderText)(0).Item("User").ToString.ToLower.Trim
            'Owner = DTAbsence.Select("DzienTygodnia=" & .Columns(e.ColumnIndex).Name.Substring(0, 1) & " AND IdUczen=" & CType(.Rows(e.RowIndex).Tag, StudentAllocation).ID & " AND NrLekcji=" & .Columns(e.ColumnIndex).HeaderText)(0).Item("Owner").ToString.ToLower.Trim
            User = CType(.CurrentCell.Tag, List(Of AbsenceDetails)).Item(0).User
            Owner = CType(.CurrentCell.Tag, List(Of AbsenceDetails)).Item(0).Owner
            If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
              lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (Wł: ", GlobalValues.Users.Item(Owner).ToString, ")")
            Else
              Me.lblUser.Text = User & " (Wł: " & Owner & ")"
            End If
            Me.lblIP.Text = CType(.CurrentCell.Tag, List(Of AbsenceDetails)).Item(0).ComputerIP
            Me.lblData.Text = CType(.CurrentCell.Tag, List(Of AbsenceDetails)).Item(0).Version
            'Me.lblIP.Text = DTAbsence.Select("DzienTygodnia=" & .Columns(e.ColumnIndex).Name.Substring(0, 1) & " AND IdUczen=" & CType(.Rows(e.RowIndex).Tag, StudentAllocation).ID & " AND NrLekcji=" & .Columns(e.ColumnIndex).HeaderText)(0).Item("ComputerIP").ToString
            'Me.lblData.Text = DTAbsence.Select("DzienTygodnia=" & .Columns(e.ColumnIndex).Name.Substring(0, 1) & " AND IdUczen=" & CType(.Rows(e.RowIndex).Tag, StudentAllocation).ID & " AND NrLekcji=" & .Columns(e.ColumnIndex).HeaderText)(0).Item("Version").ToString
          Else
            Me.lblUser.Text = ""
            Me.lblIP.Text = ""
            Me.lblData.Text = ""
          End If
          IntRowIndex = e.RowIndex
          IntColumnIndex = e.ColumnIndex
        End With
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAbsencja.CellLeave
    Me.dgvAbsencja.Columns(e.ColumnIndex).HeaderCell.Style.Font = Me.dgvAbsencja.Font

  End Sub
  Private Sub dgvAbsencja_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAbsencja.RowEnter
    If InRefresh Then Exit Sub
    With dgvAbsencja
      For i As Integer = 0 To .Columns.Count - 1
        .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.White
        .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.Orange
      Next
      'dgvAbsencja.Rows(e.RowIndex).Cells(0).Style.Font = New Font(dgvAbsencja.Font, FontStyle.Bold)
      'dgvAbsencja.Rows(e.RowIndex).Cells(1).Style.Font = New Font(dgvAbsencja.Font, FontStyle.Bold)
      If CType(.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
        .Rows(e.RowIndex).Cells(0).Style.Font = New Font(.Font, FontStyle.Bold Or FontStyle.Strikeout)
        .Rows(e.RowIndex).Cells(1).Style.Font = New Font(.Font, FontStyle.Bold Or FontStyle.Strikeout)
      End If
    End With

  End Sub

  Private Sub dgvAbsencja_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAbsencja.RowLeave
    Try
      With dgvAbsencja

        For i As Integer = 0 To .Columns.Count - 1
          .Rows(e.RowIndex).Cells(i).Style.BackColor = .Columns(i).CellTemplate.Style.BackColor
          'dgvAbsencja.Rows(e.RowIndex).Cells(i).Style.Font = dgvAbsencja.Font
        Next
        For i As Integer = 0 To 1
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = .Rows(e.RowIndex).DefaultCellStyle.ForeColor
          .Rows(e.RowIndex).Cells(i).Style.Font = .Font
        Next
        '.Rows(e.RowIndex).Cells(0).Style.ForeColor = .Rows(e.RowIndex).DefaultCellStyle.ForeColor
        '.Rows(e.RowIndex).Cells(1).Style.ForeColor = .Rows(e.RowIndex).DefaultCellStyle.ForeColor
        For i As Integer = 2 To .Columns.Count - 4
          If .Rows(e.RowIndex).Cells(i).FormattedValue.ToString = "N" Then
            .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Red
          ElseIf .Rows(e.RowIndex).Cells(i).FormattedValue.ToString = "U" Then
            .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Green
          ElseIf .Rows(e.RowIndex).Cells(i).FormattedValue.ToString = "S" Then
            .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.SteelBlue
          Else
            .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Black
          End If
        Next
        For i As Integer = .Columns.Count - 3 To .Columns.Count - 1
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Blue
        Next
        If CType(.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
          .Rows(e.RowIndex).Cells(1).Style.Font = New Font(.Font, FontStyle.Strikeout)
          .Rows(e.RowIndex).Cells(0).Style.Font = New Font(.Font, FontStyle.Strikeout)
          For i As Integer = 0 To .Columns.Count - 1
            .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.SlateGray
            .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
          Next
        ElseIf CType(.Rows(e.RowIndex).Tag, StudentAllocation).NauczanieIndywidualne IsNot Nothing Then
          For i As Integer = 0 To 1
            .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
          Next
        End If

      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvAbsencja.KeyDown
    If dgvAbsencja.CurrentCell.ColumnIndex < 2 OrElse (dgvAbsencja.CurrentCell.ColumnIndex > dgvAbsencja.Columns.Count - 4) Then Exit Sub
    Try
      If dgvAbsencja.CurrentCell.Tag IsNot Nothing Then
        If (e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back) Then
          If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = CType(dgvAbsencja.CurrentCell.Tag, List(Of AbsenceDetails)).Item(0).Owner Then
            'If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DTAbsence.Select("ID=" & CType(dgvAbsencja.CurrentCell.Tag, List(Of Integer)).Item(0).ToString)(0).Item("Owner").ToString.ToLower.Trim Then
            Me.DeleteAbsence()
          Else
            MessageBox.Show("Nie możesz usunąć tej wartości, ponieważ nie jesteś autorem tego wpisu.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
          End If
        End If
      Else
        If e.KeyCode = Keys.N Or e.KeyCode = Keys.U Or e.KeyCode = Keys.S Then
          If dgvAbsencja.CurrentCell.Value Is Nothing Then
            If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString Then 'GlobalValues.AppUser.Login = Wychowawca Then
              InsertAbsence(e.KeyCode.ToString)
            Else
              MessageBox.Show("Nie możesz wprowadzać nieobecności (spóźnień) w klasie, której nie jesteś wychowawcą!" & vbNewLine & "Skorzystaj z formularza 'Tematy zajęć lekcyjnych'.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
          Else
            MessageBox.Show("Nie można zmienić zawartości tej komórki! Uczeń został skreślony z listy lub realizuje zajęcia w trybie indywidualnym.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          End If
        End If

      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
    DeleteToolStripMenuItem.Enabled = False
    DeleteAbsence()
  End Sub
  Private Sub DeleteAbsence()
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, A As New AbsencjaSQL, MyTran As MySqlTransaction
      MyTran = GlobalValues.gblConn.BeginTransaction
      Try
        For Each Cell As DataGridViewCell In Me.dgvAbsencja.SelectedCells
          If Cell.Tag IsNot Nothing Then
            If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = CType(Cell.Tag, List(Of AbsenceDetails)).Item(0).Owner Then
              'If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DTAbsence.Select("ID=" & CType(Cell.Tag, List(Of Integer)).Item(0).ToString)(0).Item("Owner").ToString.ToLower.Trim Then
              For Each Nb As AbsenceDetails In CType(Cell.Tag, List(Of AbsenceDetails))

                Dim cmd As MySqlCommand = DBA.CreateCommand(A.DeleteAbsence)
                cmd.Transaction = MyTran
                cmd.Parameters.AddWithValue("?ID", Nb.ID)
                cmd.ExecuteNonQuery()
              Next

            Else
              MessageBox.Show("Nie możesz usunąć tej wartości, ponieważ nie jesteś autorem tego wpisu.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If

          End If
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
  Private Sub DeleteAndNotifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAndNotifyToolStripMenuItem.Click
    DeleteToolStripMenuItem.Enabled = False
    DeleteAbsenceAndNotify()
  End Sub
  Private Sub DeleteAbsenceAndNotify()
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, A As New AbsencjaSQL, MyTran As MySqlTransaction = Nothing
      Try
        For Each Cell As DataGridViewCell In Me.dgvAbsencja.SelectedCells
          If Cell.Tag IsNot Nothing AndAlso GlobalValues.AppUser.Login <> CType(Cell.Tag, List(Of AbsenceDetails)).Item(0).Owner Then
            MyTran = GlobalValues.gblConn.BeginTransaction
            'If GlobalValues.AppUser.Login <> CType(Cell.Tag, List(Of AbsenceDetails)).Item(0).Owner Then
            For Each Nb As AbsenceDetails In CType(Cell.Tag, List(Of AbsenceDetails))
              If NotifyOwner(MyTran, New String() {CType(Cell.OwningRow.Tag, StudentAllocation).ID.ToString, Cell.OwningRow.Cells(1).Value.ToString}, CType(Cell.OwningColumn.Tag, SchoolLessonByDay), Cell.Value.ToString, Nb.Owner) Then

                Dim cmd As MySqlCommand = DBA.CreateCommand(A.DeleteAbsence)
                cmd.Transaction = MyTran
                cmd.Parameters.AddWithValue("?ID", Nb.ID)
                cmd.ExecuteNonQuery()
              End If
            Next
            MyTran.Commit()
          End If
        Next

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

  Private Function NotifyOwner(MyTransaction As MySqlTransaction, Uczen As String(), Lekcja As SchoolLessonByDay, TypNb As String, AutorWpisu As String) As Boolean
    Dim dlg As New dlgReklamacja
    With dlg
      .txtUczen.Text = Uczen(1)
      .txtLekcja.Text = CType(Lekcja.Lekcja, List(Of SchoolLesson)).Item(0).Przedmiot
      .txtData.Text = Lekcja.DataZajec.ToShortDateString
      .txtTyp.Text = TypNb
      .txtOwner.Text = GlobalValues.Users.Item(AutorWpisu).ToString
      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        Dim DBA As New DataBaseAction, A As New AbsencjaSQL
        Dim cmd As MySqlCommand = DBA.CreateCommand(A.InsertComplain)
        cmd.Transaction = MyTransaction
        cmd.Parameters.AddWithValue("?IdUczen", Uczen(0))
        cmd.Parameters.AddWithValue("?DataLekcji", Lekcja.DataZajec)
        cmd.Parameters.AddWithValue("?Typ", TypNb)
        cmd.Parameters.AddWithValue("?IdLekcja", CType(Lekcja.Lekcja, List(Of SchoolLesson)).Item(0).IdLekcja)
        cmd.Parameters.AddWithValue("?AbsenceOwner", AutorWpisu)
        cmd.Parameters.AddWithValue("?AbsenceNotifier", GlobalValues.AppUser.Login)
        'cmd.Parameters.AddWithValue("?NotifyDate", Date.Today)

        cmd.Parameters.AddWithValue("?Status", 0)
        cmd.Parameters.AddWithValue("?Komentarz", .txtKomentarz.Text)
        cmd.ExecuteNonQuery()
        Return True
      End If
      Return False
    End With
  End Function


  Private Sub dgvZestawienieOcen_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvAbsencja.ColumnHeaderMouseClick

    With dgvAbsencja
      .CurrentCell = .Rows(IntRowIndex).Cells(e.ColumnIndex)
    End With

  End Sub
  Private Sub dgvZestawienieOcen_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvAbsencja.DataError
    MessageBox.Show(e.Exception.Message)
  End Sub

  Private Sub dgvZestawienieOcen_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvAbsencja.CellMouseUp
    With dgvAbsencja
      If e.RowIndex < 0 OrElse (e.ColumnIndex < 2 Or e.ColumnIndex > dgvAbsencja.Columns.Count - 4) Then Exit Sub

      .CurrentCell = .Rows(e.RowIndex).Cells(e.ColumnIndex)

      If e.Button = Windows.Forms.MouseButtons.Right AndAlso .Rows(e.RowIndex).Cells(e.ColumnIndex).Selected Then
        If .Rows(e.RowIndex).Cells(e.ColumnIndex).Tag Is Nothing Then
          If .Rows(e.RowIndex).Cells(e.ColumnIndex).Value Is Nothing Then
            InsertJustifiedToolStripMenuItem.Enabled = True
            InsertUnjustifiedToolStripMenuItem.Enabled = True
            InsertLatenessToolStripMenuItem.Enabled = True
          Else
            'cmsAbsencja.Enabled = False
            InsertJustifiedToolStripMenuItem.Enabled = False
            InsertUnjustifiedToolStripMenuItem.Enabled = False
            InsertLatenessToolStripMenuItem.Enabled = False
          End If
          ChangeToJustifiedToolStripMenuItem.Enabled = False
          ChangeToUnjustifiedToolStripMenuItem.Enabled = False
          ChangeToLatenessToolStripMenuItem.Enabled = False
          DeleteToolStripMenuItem.Enabled = False
          DeleteAndNotifyToolStripMenuItem.Enabled = False
        Else
          If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator Then
            DeleteToolStripMenuItem.Enabled = True
            DeleteAndNotifyToolStripMenuItem.Enabled = True
            ChangeToJustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "N", True, False), Boolean)
            ChangeToUnjustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "U", True, False), Boolean)

            ChangeToLatenessToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "N" OrElse .Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "U", True, False), Boolean)
            ChangeLatenessToJustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "S", True, False), Boolean)
            ChangeLatenessToUnjustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "S", True, False), Boolean)
          ElseIf GlobalValues.AppUser.Login = CType(.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, List(Of AbsenceDetails)).Item(0).Owner Then
            'ElseIf GlobalValues.AppUser.Login = DTAbsence.Select("ID=" & CType(.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, List(Of Integer)).Item(0).ToString)(0).Item("Owner").ToString.ToLower.Trim Then
            DeleteToolStripMenuItem.Enabled = True
            DeleteAndNotifyToolStripMenuItem.Enabled = False
            ChangeToJustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "N", True, False), Boolean)
            ChangeToUnjustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "U", True, False), Boolean)

            ChangeToLatenessToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "N" OrElse .Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "U", True, False), Boolean)
            ChangeLatenessToJustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "S", True, False), Boolean)
            ChangeLatenessToUnjustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "S", True, False), Boolean)
          ElseIf GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString Then
            DeleteAndNotifyToolStripMenuItem.Enabled = True
            DeleteToolStripMenuItem.Enabled = False
            ChangeLatenessToJustifiedToolStripMenuItem.Enabled = False
            ChangeLatenessToUnjustifiedToolStripMenuItem.Enabled = False
            ChangeToLatenessToolStripMenuItem.Enabled = False
            ChangeToJustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "N", True, False), Boolean)
            ChangeToUnjustifiedToolStripMenuItem.Enabled = CType(IIf(.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString = "U", True, False), Boolean)
          End If

          'If GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString Then DeleteAndNotifyToolStripMenuItem.Enabled = True
          InsertJustifiedToolStripMenuItem.Enabled = False
          InsertUnjustifiedToolStripMenuItem.Enabled = False
          InsertLatenessToolStripMenuItem.Enabled = False

        End If
      End If
    End With
  End Sub


  Private Sub ChangeToJustifiedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeToJustifiedToolStripMenuItem.Click
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL, MyTran As MySqlTransaction
    MyTran = GlobalValues.gblConn.BeginTransaction
    Try
      For Each Cell As DataGridViewCell In Me.dgvAbsencja.SelectedCells
        If Cell.Tag IsNot Nothing AndAlso Cell.FormattedValue.ToString = "N" Then
          If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = CType(Cell.Tag, List(Of AbsenceDetails)).Item(0).Owner OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString Then
            'If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DTAbsence.Select("ID=" & CType(Cell.Tag, List(Of Integer)).Item(0).ToString)(0).Item("Owner").ToString.ToLower.Trim OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString Then

            'GlobalValues.AppUser.Login = Wychowawca Then
            For Each AD As AbsenceDetails In CType(Cell.Tag, List(Of AbsenceDetails))
              Dim cmd As MySqlCommand = DBA.CreateCommand(A.UpdateAbsence)
              cmd.Transaction = MyTran
              cmd.Parameters.AddWithValue("?IdFrekwencja", AD.ID.ToString)
              cmd.Parameters.AddWithValue("?Typ", "u")
              cmd.ExecuteNonQuery()
            Next
          Else
            MessageBox.Show("Nie masz wystarczających uprawnień do edycji tego wpisu.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
          End If

        End If
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
  End Sub

  Private Sub ChangeToUnjustifiedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeToUnjustifiedToolStripMenuItem.Click
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL, MyTran As MySqlTransaction
    MyTran = GlobalValues.gblConn.BeginTransaction
    Try
      For Each Cell As DataGridViewCell In Me.dgvAbsencja.SelectedCells
        If Cell.Tag IsNot Nothing AndAlso Cell.FormattedValue.ToString = "U" Then
          If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = CType(Cell.Tag, List(Of AbsenceDetails)).Item(0).Owner OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString Then 'GlobalValues.AppUser.Login = Wychowawca Then
            For Each AD As AbsenceDetails In CType(Cell.Tag, List(Of AbsenceDetails))
              Dim cmd As MySqlCommand = DBA.CreateCommand(A.UpdateAbsence)
              cmd.Transaction = MyTran
              cmd.Parameters.AddWithValue("?IdFrekwencja", AD.ID.ToString)
              cmd.Parameters.AddWithValue("?Typ", "n")
              cmd.ExecuteNonQuery()
            Next
          Else
            MessageBox.Show("Nie masz wystarczających uprawnień do edycji tego wpisu.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
          End If

        End If
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
  End Sub

  Private Sub ChangeToLatenessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeToLatenessToolStripMenuItem.Click
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL, MyTran As MySqlTransaction
    MyTran = GlobalValues.gblConn.BeginTransaction
    Try
      For Each Cell As DataGridViewCell In Me.dgvAbsencja.SelectedCells
        If Cell.Tag IsNot Nothing AndAlso (Cell.FormattedValue.ToString = "N" OrElse Cell.FormattedValue.ToString = "U") Then
          If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = CType(Cell.Tag, List(Of AbsenceDetails)).Item(0).Owner Then
            For Each AD As AbsenceDetails In CType(Cell.Tag, List(Of AbsenceDetails))
              Dim cmd As MySqlCommand = DBA.CreateCommand(A.UpdateAbsence)
              cmd.Transaction = MyTran
              cmd.Parameters.AddWithValue("?IdFrekwencja", AD.ID.ToString)
              cmd.Parameters.AddWithValue("?Typ", "s")
              cmd.ExecuteNonQuery()
            Next
          Else
            MessageBox.Show("Nie masz wystarczających uprawnień do edycji tego wpisu.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
          End If
        End If
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
  End Sub

  Private Sub ChangeLatenessToJustifiedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeLatenessToJustifiedToolStripMenuItem.Click, ChangeLatenessToUnjustifiedToolStripMenuItem.Click
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL, MyTran As MySqlTransaction
    MyTran = GlobalValues.gblConn.BeginTransaction
    Try
      For Each Cell As DataGridViewCell In Me.dgvAbsencja.SelectedCells
        If Cell.Tag IsNot Nothing AndAlso Cell.FormattedValue.ToString = "S" Then
          If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = CType(Cell.Tag, List(Of AbsenceDetails)).Item(0).Owner Then
            For Each AD As AbsenceDetails In CType(Cell.Tag, List(Of AbsenceDetails))
              Dim cmd As MySqlCommand = DBA.CreateCommand(A.UpdateAbsence)
              cmd.Transaction = MyTran
              cmd.Parameters.AddWithValue("?IdFrekwencja", AD.ID.ToString)
              cmd.Parameters.AddWithValue("?Typ", CType(sender, ToolStripMenuItem).Tag.ToString)
              cmd.ExecuteNonQuery()
            Next
          Else
            MessageBox.Show("Nie masz wystarczających uprawnień do edycji tego wpisu.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
          End If

        End If
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
  End Sub

  Private Sub InsertUnjustifiedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertUnjustifiedToolStripMenuItem.Click, InsertJustifiedToolStripMenuItem.Click, InsertLatenessToolStripMenuItem.Click
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString Then 'GlobalValues.AppUser.Login = Wychowawca Then
      InsertAbsence(CType(sender, ToolStripMenuItem).Tag.ToString)
    Else
      MessageBox.Show("Nie możesz wprowadzać nieobecności (spóźnień) w klasie, której nie jesteś wychowawcą!" & vbNewLine & "Skorzystaj z formularza 'Tematy zajęć lekcyjnych'.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End If
  End Sub
  Private Sub InsertAbsence(AbsenceType As String)
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL, MyTran As MySqlTransaction
    MyTran = GlobalValues.gblConn.BeginTransaction
    Try
      For Each Cell As DataGridViewCell In Me.dgvAbsencja.SelectedCells
        If Cell.Tag Is Nothing Then

          If CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).Lekcja.Count > 1 Then
            Dim NotFound As Boolean = True, Przedmiot As String = ""
            'NotFound = True
            For Each L As SchoolLesson In CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).Lekcja
              If L.Grupa > 0 Then
                If DTGrupa.Select("IdPrzedmiot=" & L.IdPrzedmiot & " AND IdPrzydzial=" & CType(dgvAbsencja.Rows(Cell.RowIndex).Tag, StudentAllocation).AllocationID).Length > 0 Then
                  NotFound = False
                  Dim cmd As MySqlCommand = DBA.CreateCommand(A.InsertAbsence)
                  cmd.Transaction = MyTran
                  cmd.Parameters.AddWithValue("?IdUczen", CType(dgvAbsencja.Rows(Cell.RowIndex).Tag, StudentAllocation).ID)
                  cmd.Parameters.AddWithValue("?Data", CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).DataZajec)
                  cmd.Parameters.AddWithValue("?Typ", AbsenceType)
                  cmd.Parameters.AddWithValue("?IdLekcja", L.IdLekcja)
                  cmd.ExecuteNonQuery()
                Else
                  'NotFound = True
                  Przedmiot = L.Przedmiot
                End If

              Else
                NotFound = False
                Dim cmd As MySqlCommand = DBA.CreateCommand(A.InsertAbsence)
                cmd.Transaction = MyTran
                cmd.Parameters.AddWithValue("?IdUczen", CType(dgvAbsencja.Rows(Cell.RowIndex).Tag, StudentAllocation).ID)
                cmd.Parameters.AddWithValue("?Data", CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).DataZajec)
                cmd.Parameters.AddWithValue("?Typ", AbsenceType)
                cmd.Parameters.AddWithValue("?IdLekcja", L.IdLekcja)
                cmd.ExecuteNonQuery()
              End If
            Next
            If NotFound Then MessageBox.Show("Przedmiot '" & Przedmiot & "' jest dzielony na grupy!" & vbNewLine & "Jednak nie znaleziono wskazanego ucznia w grupie." & vbNewLine & "Przedmiot został pominięty." & vbNewLine & "Przejdź do menu 'Uczniowie->Przydział uczniów do grup przedmiotowych' i wprowadź odpowiednie zmiany.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          Else
            If CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).Lekcja.Item(0).Grupa > 0 Then
              If DTGrupa.Select("IdPrzedmiot=" & CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).Lekcja.Item(0).IdPrzedmiot & " AND IdPrzydzial=" & CType(dgvAbsencja.Rows(Cell.RowIndex).Tag, StudentAllocation).AllocationID).Length > 0 Then

                Dim cmd As MySqlCommand = DBA.CreateCommand(A.InsertAbsence)
                cmd.Transaction = MyTran
                cmd.Parameters.AddWithValue("?IdUczen", CType(dgvAbsencja.Rows(Cell.RowIndex).Tag, StudentAllocation).ID)
                cmd.Parameters.AddWithValue("?Data", CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).DataZajec)
                cmd.Parameters.AddWithValue("?Typ", AbsenceType)
                cmd.Parameters.AddWithValue("?IdLekcja", CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).Lekcja.Item(0).IdLekcja)
                cmd.ExecuteNonQuery()
              Else
                MessageBox.Show("Przedmiot '" & CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).Lekcja.Item(0).Przedmiot & "' jest dzielony na grupy!" & vbNewLine & "Jednak nie znaleziono wskazanego ucznia w grupie." & vbNewLine & "Przedmiot został pominięty." & vbNewLine & "Przejdź do menu 'Uczniowie->Przydział uczniów do grup przedmiotowych' i wprowadź odpowiednie zmiany.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
              End If
            Else
              Dim cmd As MySqlCommand = DBA.CreateCommand(A.InsertAbsence)
              cmd.Transaction = MyTran
              cmd.Parameters.AddWithValue("?IdUczen", CType(dgvAbsencja.Rows(Cell.RowIndex).Tag, StudentAllocation).ID)
              cmd.Parameters.AddWithValue("?Data", CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).DataZajec)
              cmd.Parameters.AddWithValue("?Typ", AbsenceType)
              cmd.Parameters.AddWithValue("?IdLekcja", CType(dgvAbsencja.Columns(Cell.ColumnIndex).Tag, SchoolLessonByDay).Lekcja.Item(0).IdLekcja)
              cmd.ExecuteNonQuery()
            End If
          End If
        End If
      Next
      MyTran.Commit()
      Me.GetData()
    Catch mex As MySqlException
      MyTran.Rollback()
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    Catch ex As Exception
      MyTran.Rollback()
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub

  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkVirtual.CheckedChanged
    If Not Me.Created Then Exit Sub
    'ApplyNewConfig()
    dgvAbsencja.Rows.Clear()
    dgvAbsencja.Columns.Clear()
    FillKlasa()
  End Sub

  Private Sub dgvAbsencja_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvAbsencja.RowPostPaint
    If (e.RowIndex + 1) Mod 10 = 0 Then
      Dim RowWidth As Integer = dgvAbsencja.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
      e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 1, RowWidth, e.RowBounds.Bottom - 1)
      'e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 2, e.RowBounds.Right, e.RowBounds.Bottom - 2)
    End If
  End Sub


End Class

Public Class SchoolHour
  Public Property Nr As Byte
  Public Property Lekcja As List(Of SchoolLesson)
End Class
Public Class SchoolDay
  Public Property Value As Date 'data określająca dzień miesiąca
  Public Property Plan As List(Of SchoolHour) 'plan lekcji w danym dniu tygodnia
End Class

Public Class GroupMember 'Lista uczniów danej klasy
  Public Property Allocation As StudentAllocation 'identyfikacja ucznia (ID oraz IdPrzydzial)
  Public Property No As String 'nr ucznia w dzienniku
  Public Property Name As String 'nazwisko i imię ucznia
End Class
Public Class IndividualCourse
  Public Property SchoolObject As List(Of IndividualStaff)
  Public Property VirtualClassID As Integer ' ID wirtualnej klasy
End Class
Public Class IndividualStaff
  Public Property ObjectID As Integer 'Id przedmiotu nauczanego indywidualnie
  Public Property StartDate As Date 'Data początkowa nauczania indywidualnego
  Public Property EndDate As Date 'Data zakończenia nauczania indywidualnego
End Class
Public Class StudentAllocation
  Public Property ID As Integer 'identyfikator ucznia w tabeli uczen
  Public Property AllocationID As Integer 'identyfikator przydziału ucznia do klasy w danym roku szkolnym w tabeli przydzial
  Public Property Status As Boolean 'Status aktywacji ucznia
  Public Property MasterLocation As Boolean 'Status przydziału ucznia określający stan bieżący
  Public Property DataAktywacji As Date 'Data zapisu
  Public Property DataDeaktywacji As Date 'Data skreślenia
  Public Property NauczanieIndywidualne As IndividualCourse
End Class
Public Class SchoolLesson
  Public Property IdLekcja As Integer
  Public Property IdPrzedmiot As Integer 'Przedmiot w tabeli obsada czyli ID w tabeli szkola_przedmiot
  Public Property Przedmiot As String 'Nazwa przedmiotu
  Public Property Grupa As Byte 'Informacja o podziale przedmiotu na grupy; Grupa=0 brak podziału na grupy. Grupa>0 podział na grupy
End Class
Public Class SchoolLessonByDay
  Public Property DataZajec As Date
  Public Property Lekcja As List(Of SchoolLesson)
End Class
'Public Class ObjectGroup
'  Public Property IdPrzedmiot As Integer
'  Public Property Student As List(Of Integer)
'End Class
Public Class AbsenceDetails
  Public Property ID As Integer
  Public Property User As String
  Public Property Owner As String
  Public Property ComputerIP As String
  Public Property Version As String
End Class