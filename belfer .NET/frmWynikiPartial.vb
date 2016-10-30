Imports belfer.NET.GlobalValues
Imports System.Drawing.Printing
Public Class frmWynikiPartial
  Private InRefresh As Boolean = True, DTAllowedMarks, DTWarning, DTWyniki, dtGrupa, dtIndividualStaff As DataTable, DS As DataSet
  Dim EndMarks As List(Of EndMark)
  Private IntRowIndex As Integer = 0, IntColumnIndex As Integer = 2, IdPrzedmiot As String, CurrentDate As Date
  Public Event NewRow()
  Private Offset(1), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.OcenyCzastkoweToolStripMenuItem.Enabled = True
    MainForm.cmdWynikiCzastkowe.Enabled = True
    MainForm.KolumnyToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.OcenyCzastkoweToolStripMenuItem.Enabled = True
    MainForm.cmdWynikiCzastkowe.Enabled = True
    MainForm.KolumnyToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  'Private IdPrzedmiot As Integer
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub

  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Dim DBA As New DataBaseAction, W As New WynikiSQL
    DTAllowedMarks = DBA.GetDataTable(W.SelectPartialMarks)
    GetEndMarks()
    DataGridConfig(dgvZestawienieOcen)
    'Dim CH As New CalcHelper
    'Dim Semester2 As Date = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
    'Me.nudSemestr.Value = CType(IIf(CurrentDate < Semester2, 1, 2), Integer)
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()

    cbKlasa.Items.Clear()
    cbKlasa.Enabled = False
    cbPrzedmiot.Items.Clear()
    cbPrzedmiot.Enabled = False
    dgvZestawienieOcen.Rows.Clear()
    dgvZestawienieOcen.Columns.Clear()
    lblOpis.Text = ""
    Me.dgvZestawienieOcen.Enabled = False
    cmdInsertColumn.Enabled = False
    cmdEditOpis.Enabled = False
    cmdDelete.Enabled = False
    ClearDetails()
    ', CurrentDate As Date
    CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
    Dim CH As New CalcHelper
    Dim Semester2 As Date = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
    Me.nudSemestr.Value = CType(IIf(CurrentDate < Semester2, 1, 2), Integer)
    nudSemestr.Minimum = 1
    'nudSemestr_ValueChanged(Nothing, Nothing)
    FillKlasa()
  End Sub

  Private Sub FetchData(Obsada As ObjectStaffComboItem)
    Dim DBA As New DataBaseAction, W As New WynikiSQL, S As New StatystykaSQL, CH As New CalcHelper

    DS = DBA.GetDataSet(W.SelectKolumna(Obsada.ID.ToString) & S.SelectAbsence(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, Obsada.ObjectID.ToString, My.Settings.SchoolYear, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)) & S.SelectAbsenceZastepstwo(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, Obsada.ObjectID.ToString, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)) & S.SelectLekcjaBezZastepstw(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, Obsada.ObjectID.ToString, My.Settings.SchoolYear, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)) & S.SelectZastepstwo(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, Obsada.ObjectID.ToString, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CType(If(nudSemestr.Value = 1, CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)))
    DS.Tables(0).TableName = "Kolumna"
    DS.Tables(1).TableName = "Absence"
    DS.Tables(2).TableName = "AbsenceZastepstwo"
    DS.Tables(3).TableName = "LekcjaBezZastepstw"
    DS.Tables(4).TableName = "Zastepstwo"
  End Sub
  Private Sub FetchResult(Obsada As String)
    Dim DBA As New DataBaseAction, W As New WynikiSQL
    DTWyniki = DBA.GetDataTable(W.SelectResult(Obsada))
    DTWyniki.TableName = "Wyniki"
    'DSWarning = DBA.GetDataSet(W.SelectCaution(Klasa, RokSzkolny) & W.SelectPrzedmiotId(Klasa, RokSzkolny))
  End Sub
  Private Sub FetchWarning(Klasa As SchoolClassComboItem, RokSzkolny As String, Semestr As String)
    Dim DBA As New DataBaseAction, W As New WynikiSQL
    IdPrzedmiot = DBA.GetSingleValue(W.SelectPrzedmiotId(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID.ToString))
    Dim SelectString As String = If(Klasa.IsVirtual, W.SelectVirtualCaution(Klasa.ID.ToString, RokSzkolny, Semestr, IdPrzedmiot), W.SelectCaution(Klasa.ID.ToString, RokSzkolny, Semestr, IdPrzedmiot))
    DTWarning = DBA.GetDataTable(SelectString)
    DTWarning.TableName = "Warning"
  End Sub
  Private Function FetchStudent(Klasa As String, RokSzkolny As String) As DataTable
    Dim DBA As New DataBaseAction, W As New WynikiSQL, CH As New CalcHelper, T As New TematSQL
    Dim SelectString As String = ""
    If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
      SelectString = T.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, dtData.Value)
      dtGrupa = Nothing
      dtIndividualStaff = Nothing
    Else
      SelectString = W.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
      dtGrupa = DBA.GetDataTable(W.SelectGrupa(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
      dtIndividualStaff = DBA.GetDataTable(T.SelectIndividualStaff(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString))
    End If
    'dtStudent = DBA.GetDataTable(SelectString)
    Return DBA.GetDataTable(SelectString)
  End Function
  Private Sub FillKlasa()
    cbPrzedmiot.Enabled = False
    cbPrzedmiot.Items.Clear()
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
    dgvZestawienieOcen.Rows.Clear()
    dgvZestawienieOcen.Columns.Clear()
    lblOpis.Text = ""
    'nudSemestr.Enabled = False
    Me.dgvZestawienieOcen.Enabled = False
    'cmdAddColumn.Enabled = False
    cmdInsertColumn.Enabled = False
    cmdEditOpis.Enabled = False
    cmdDelete.Enabled = False
    ClearDetails()
    'FillPrzedmiot()
    'FetchStudent(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear)
    LoadObjectStaffItems(cbPrzedmiot)
    If My.Settings.IdObsada.Length > 0 Then
      For Each Item As ObjectStaffComboItem In cbPrzedmiot.Items
        If Item.ID = CType(My.Settings.IdObsada, Integer) Then
          cbPrzedmiot.SelectedIndex = cbPrzedmiot.Items.IndexOf(Item)
          Exit For
        End If
      Next
    End If
  End Sub
  Private Sub LoadObjectStaffItems(cb As ComboBox)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, W As New WynikiSQL
    cb.Items.Clear()
    Try

      R = DBA.GetReader(W.SelectPrzedmiot(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, dtData.Value))

      While R.Read()
        cb.Items.Add(New ObjectStaffComboItem(R.GetInt32("IdObsada"), R.GetString("Obsada"), R.GetInt32("Przedmiot"), R.GetString("SchoolTeacherID")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub

  Private Sub cbPrzedmiot_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectionChangeCommitted
    My.Settings.IdObsada = CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    IntRowIndex = 0
    IntColumnIndex = 2
    RefreshData()
  End Sub
  Private Sub RefreshData()
    Try
      InRefresh = True
      Cursor = Cursors.WaitCursor
      FetchData(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem))
      'GetGroupMember(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem))
      SetColumns(dgvZestawienieOcen, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
      SetRows(dgvZestawienieOcen)
      'nudSemestr.Enabled = True
      Me.GetData(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
      Me.dgvZestawienieOcen.Focus()
      Me.dgvZestawienieOcen.Enabled = True
      InRefresh = False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      Cursor = Cursors.Default
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
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End With
  End Sub

  Private Overloads Sub SetColumns(ByVal dgvName As DataGridView, Typ As String)
    Dim DBA As New DataBaseAction, Reader As MySqlDataReader = Nothing
    Try
      With dgvName
        'While Reader.Read
        .Columns.Clear()
        Dim NameCol, NrCol As New DataGridViewColumn
        NrCol.Name = "Nr"
        NrCol.HeaderText = "Nr"
        NrCol.Width = 30
        NrCol.CellTemplate = New DataGridViewTextBoxCell()
        NrCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        NrCol.ToolTipText = "Numer ucznia w dzienniku"
        NrCol.Tag = New ColumnProperty With {.Blokada = True, .Typ = "RO"}
        NrCol.ReadOnly = True
        NrCol.Frozen = True
        NrCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NrCol)

        NameCol.Name = "Nazwisko"
        NameCol.HeaderText = "Nazwisko i imiê"
        NameCol.Width = 200
        NameCol.CellTemplate = New DataGridViewTextBoxCell()
        NameCol.ToolTipText = "Nazwisko i imiê ucznia"
        NameCol.Tag = New ColumnProperty With {.Blokada = True, .Typ = "RO"}
        NameCol.ReadOnly = True
        NameCol.Frozen = True
        NameCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NameCol)

        For i As Integer = 0 To DS.Tables("Kolumna").Select("Typ='" & Typ & "'").GetUpperBound(0)
          Dim ObjectColumn As New DataGridViewTextBoxColumn   'DataGridViewComboBoxColumn
          With ObjectColumn
            .Tag = New ColumnProperty With {.ID = CType(DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item(0), Integer), .Nr = CType(DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item("NrKolumny"), Byte), .Poprawa = CType(DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item("Poprawa"), Boolean), .Typ = Typ, .Waga = CType(DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item("Waga"), Decimal), .Opis = DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item("Nazwa").ToString, .Blokada = CType(DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item("Lock"), Boolean)}
            .HeaderText = CType(.Tag, ColumnProperty).Nr.ToString
            .Name = "Kolumna " + .HeaderText
            .HeaderCell.Style.BackColor = If(CType(DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item("Poprawa"), Boolean), Color.Aqua, .HeaderCell.Style.BackColor)
            .Width = 25 '65
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate = New DataGridViewTextBoxCell
            .CellTemplate.Style.ForeColor = ColorTranslator.FromHtml(DS.Tables("Kolumna").Select("Typ='" & Typ & "'")(i).Item("KolorHex").ToString)
            .DefaultCellStyle.ForeColor = .CellTemplate.Style.ForeColor
            .ToolTipText = "Waga kolumny: " & CType(.Tag, ColumnProperty).Waga.ToString & vbNewLine & "Kolumna poprawkowa: " & If(CType(.Tag, ColumnProperty).Poprawa, "Tak", "Nie") & vbNewLine & "Opis: " & CType(.Tag, ColumnProperty).Opis.ToString
            .ReadOnly = If(CType(.Tag, ColumnProperty).Blokada, True, False)
            .SortMode = DataGridViewColumnSortMode.NotSortable
          End With
          .Columns.Add(ObjectColumn)
        Next

        Dim AvgColumn As New DataGridViewTextBoxColumn      'DataGridViewComboBoxColumn
        With AvgColumn
          .HeaderText = "Œrednia"
          .Width = 85 '65
          .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .CellTemplate = New DataGridViewTextBoxCell
          .Name = "Avg"
          '.Frozen = True
          .Tag = New ColumnProperty With {.Blokada = True, .Typ = "RO"}
          .ReadOnly = True
          '.HeaderCell.Style.BackColor = Color.AliceBlue
          '.CellTemplate.Style.BackColor = Color.AliceBlue
          .CellTemplate.Style.ForeColor = Color.Blue
          .ToolTipText = "Œrednia wa¿ona = Suma(ocena*waga)/Suma(waga)"
          .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        .Columns.Add(AvgColumn)
        'End While
        Dim FrekwencjaColumn As New DataGridViewTextBoxColumn      'DataGridViewComboBoxColumn
        With FrekwencjaColumn
          .HeaderText = "Absencja"
          .Width = 100 '65
          .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .CellTemplate = New DataGridViewTextBoxCell
          .Name = "Frekwencja"
          '.Frozen = True
          .Tag = New ColumnProperty With {.Blokada = True, .Typ = "RO"}
          .ReadOnly = True
          .CellTemplate.Style.ForeColor = Color.SteelBlue
          .ToolTipText = "Nieobecnoœci na zajêciach wyra¿one procentowo"
          .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        .Columns.Add(FrekwencjaColumn)
        'dokoñczyæ kolumnê zagro¿enia
        Dim WarningColumn As New DataGridViewCheckBoxColumn
        With WarningColumn
          .HeaderText = "!!!"
          .HeaderCell.Style.ForeColor = Color.Red
          .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
          '.HeaderCell.Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
          .Name = "Warning"
          .Width = 40 '65
          .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .CellTemplate = New DataGridViewCheckBoxCell
          .ToolTipText = "Zagro¿enie ocen¹ niedostateczn¹ lub nieklasyfikowaniem"
          .Tag = New ColumnProperty With {.Blokada = CType(DS.Tables("Kolumna").Select("Typ='" & IIf(Typ = "C1", "S", "R").ToString & "'")(0).Item("Lock"), Boolean), .Typ = "Z"}
          .ReadOnly = If(CType(.Tag, ColumnProperty).Blokada, True, False)
          .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        .Columns.Add(WarningColumn)

        Dim P As New DataGridViewComboBoxCell, W As New WynikiSQL

        Reader = DBA.GetReader(W.SelectEndMarks)

        P.ValueMember = "ID"
        P.DisplayMember = "Nazwa"
        P.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        While Reader.Read
          P.Items.Add(New CbItem(CInt(Reader.Item(0)), Reader.Item(1).ToString))
        End While
        Dim EndColumn As New DataGridViewComboBoxColumn     'DataGridViewComboBoxColumn
        With EndColumn
          .HeaderText = IIf(nudSemestr.Value = 1, "Semestr I", "Ocena roczna").ToString

          .Width = 150 '65
          .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
          .CellTemplate = P
          .Name = "Semestr"
          '.CellTemplate.Style.BackColor = Color.SeaShell
          .CellTemplate.Style.ForeColor = Color.Navy
          .CellTemplate.Style.Font = New Font(dgvZestawienieOcen.Font, FontStyle.Bold)
          .Tag = New ColumnProperty With {.ID = CType(DS.Tables("Kolumna").Select("Typ='" & IIf(Typ = "C1", "S", "R").ToString & "'")(0).Item("ID"), Integer), .Blokada = CType(DS.Tables("Kolumna").Select("Typ='" & IIf(Typ = "C1", "S", "R").ToString & "'")(0).Item("Lock"), Boolean), .Typ = If(Typ = "C1", "S", "R")}
          '.Tag = DS.Tables("Kolumna").Select("Typ='" & IIf(Typ = "C1", "S", "R").ToString & "'")(0).Item("ID").ToString
          .ReadOnly = If(CType(.Tag, ColumnProperty).Blokada, True, False)
          .ToolTipText = IIf(Typ = "C1", "Ocena za I Semestr", "Ocena roczna").ToString
          .SortMode = DataGridViewColumnSortMode.NotSortable
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
  Private Function GetIndividualCourse(IdPrzydzial As String, IdPrzedmiot As String, Data As Date) As IndividualCourse
    Dim IndividualStaff() As DataRow ', NI As New IndividualCourse
    IndividualStaff = dtIndividualStaff.Select("IdPrzydzial=" & IdPrzydzial & " AND Przedmiot=" & IdPrzedmiot & " AND DataAktywacji<=#" & Data.ToShortDateString & "# AND (DataDeaktywacji>#" & Data.ToShortDateString & "# OR DataDeaktywacji is null)")
    If IndividualStaff.Length > 0 Then
      Dim S As New List(Of IndividualStaff)
      S.Add(New IndividualStaff With {.ObjectID = CType(IndividualStaff(0).Item("Przedmiot"), Integer), .StartDate = CType(IndividualStaff(0).Item("DataAktywacji"), Date), .EndDate = If(IsDBNull(IndividualStaff(0).Item("DataDeaktywacji")), Nothing, CType(IndividualStaff(0).Item("DataDeaktywacji"), Date))})
      'NI.SchoolObject = S
      Return New IndividualCourse With {.SchoolObject = S}
    Else
      Return Nothing
    End If
  End Function
  Private Function GetGroupMember(Obsada As ObjectStaffComboItem) As List(Of GroupMember)
    Try
      Dim dtStudent As DataTable = FetchStudent(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear)
      Dim Students = New List(Of GroupMember)
      If dtGrupa IsNot Nothing AndAlso dtGrupa.Select("IdSzkolaPrzedmiot=" & Obsada.ObjectID.ToString).GetLength(0) > 0 Then
        For Each Student As DataRow In dtGrupa.Select("IdSzkolaPrzedmiot=" & Obsada.ObjectID.ToString)
          Dim NI As IndividualCourse = GetIndividualCourse(Student.Item("IdPrzydzial").ToString, Obsada.ObjectID.ToString, dtData.Value)
          Dim GroupMember As DataRow
          GroupMember = dtStudent.Select("IdPrzydzial=" & Student.Item("IdPrzydzial").ToString)(0)
          Students.Add(New GroupMember With {.Allocation = New StudentAllocation With {.ID = CInt(GroupMember.Item("ID")), .AllocationID = CInt(GroupMember.Item("IdPrzydzial")), .Status = CType(GroupMember.Item("StatusAktywacji"), Boolean), .DataAktywacji = If(IsDBNull(GroupMember.Item("DataAktywacji")), Nothing, CType(GroupMember.Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(GroupMember.Item("DataDeaktywacji")), Nothing, CType(GroupMember.Item("DataDeaktywacji"), Date)), .NauczanieIndywidualne = NI}, .No = GroupMember.Item("NrwDzienniku").ToString, .Name = GroupMember.Item("Student").ToString})
        Next
      Else
        If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
          For Each Student As DataRow In dtStudent.Select("Przedmiot=" & Obsada.ObjectID.ToString)
            Students.Add(New GroupMember With {.Allocation = New StudentAllocation With {.ID = CInt(Student.Item("ID")), .AllocationID = CInt(Student.Item("IdPrzydzial")), .Status = CType(Student.Item("StatusAktywacji"), Boolean), .DataAktywacji = If(IsDBNull(Student.Item("DataAktywacji")), Nothing, CType(Student.Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(Student.Item("DataDeaktywacji")), Nothing, CType(Student.Item("DataDeaktywacji"), Date))}, .No = Student.Item("NrwDzienniku").ToString, .Name = Student.Item("Student").ToString})
          Next
        Else
          For Each Student As DataRow In dtStudent.Rows
            Dim NI As IndividualCourse = GetIndividualCourse(Student.Item("IdPrzydzial").ToString, Obsada.ObjectID.ToString, dtData.Value)
            Students.Add(New GroupMember With {.Allocation = New StudentAllocation With {.ID = CInt(Student.Item("ID")), .AllocationID = CInt(Student.Item("IdPrzydzial")), .Status = CType(Student.Item("StatusAktywacji"), Boolean), .DataAktywacji = If(IsDBNull(Student.Item("DataAktywacji")), Nothing, CType(Student.Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(Student.Item("DataDeaktywacji")), Nothing, CType(Student.Item("DataDeaktywacji"), Date)), .NauczanieIndywidualne = NI}, .No = Student.Item("NrwDzienniku").ToString, .Name = Student.Item("Student").ToString})
          Next
        End If
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
      Dim Students As List(Of GroupMember) = GetGroupMember(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem))
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
          .Rows(.Rows.Count - 1).Cells(1).ToolTipText = "Data zapisu: " & M.Allocation.DataAktywacji.ToString
          If M.Allocation.Status = False Then
            .Rows(.Rows.Count - 1).Cells(0).Style.Font = New Font(dgvName.Font, FontStyle.Strikeout)
            .Rows(.Rows.Count - 1).Cells(1).Style.Font = New Font(dgvName.Font, FontStyle.Strikeout)
            .Rows(.Rows.Count - 1).Cells(1).ToolTipText += vbNewLine & "Data skreœlenia: " & M.Allocation.DataDeaktywacji.ToString
            For i As Integer = 0 To .Columns.Count - 1
              .Rows(.Rows.Count - 1).Cells(i).Style.ForeColor = Color.SlateGray
              .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
            Next
            .Rows(.Rows.Count - 1).ReadOnly = True
          ElseIf M.Allocation.NauczanieIndywidualne IsNot Nothing Then
            .Rows(.Rows.Count - 1).Cells(1).ToolTipText += vbNewLine & "Nauczanie indywidualne: " & vbNewLine & "Data pocz¹tkowa: " & M.Allocation.NauczanieIndywidualne.SchoolObject(0).StartDate.ToString & vbNewLine & "Data koñcowa: " & If(M.Allocation.NauczanieIndywidualne.SchoolObject(0).EndDate = Nothing, "", M.Allocation.NauczanieIndywidualne.SchoolObject(0).EndDate.ToString)
            For i As Integer = 0 To dgvZestawienieOcen.ColumnCount - 1
              .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
            Next
            .Rows(.Rows.Count - 1).ReadOnly = True
          End If
        Next
        .Enabled = If(.RowCount > 0, True, False)
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & "Nr b³êdu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try

  End Sub


  Private Sub GetData(IdObsada As Integer, Typ As String)
    'DTWyniki = Me.FetchData
    InRefresh = True
    ClearDetails()
    Try
      FetchResult(IdObsada.ToString)
      FetchWarning(CType(cbKlasa.SelectedItem, SchoolClassComboItem), My.Settings.SchoolYear, nudSemestr.Value.ToString)
      With dgvZestawienieOcen
        For Each Row As DataGridViewRow In .Rows
          Dim Suma As Single = 0, SumaWag As Single = 0
          For i As Integer = 0 To DS.Tables("Kolumna").Select("Typ='" & Typ & "'").GetUpperBound(0)  '.Columns.Count - 3
            'Row.Cells(i + 2).Style.ForeColor = Color.Black
            'Row.Cells(i + 2).Style.BackColor = Row.DefaultCellStyle.BackColor
            Dim FoundRow() As DataRow
            FoundRow = DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdKolumna='" & CType(.Columns(i + 2).Tag, ColumnProperty).ID.ToString & "'") ' & "' AND Typ='" & Typ & "'")
            If FoundRow.Length > 0 Then
              If CType(FoundRow(0).Item("WagaOceny"), Single) > 0 Then
                Suma += CType(FoundRow(0).Item("WagaOceny"), Single) * CType(FoundRow(0).Item("WagaKolumny"), Single)
                'Suma += CType(FoundRow(0).Item(10), Single) * CType(FoundRow(0).Item(11), Single)
                SumaWag += CType(FoundRow(0).Item("WagaKolumny"), Single)
              End If
              Row.Cells(i + 2).Value = FoundRow(0).Item("Ocena").ToString
              Row.Cells(i + 2).Tag = FoundRow(0).Item("ID").ToString
              Row.Cells(i + 2).ToolTipText = "Opis: " & DS.Tables("Kolumna").Select("ID=" & CType(.Columns(i + 2).Tag, ColumnProperty).ID.ToString)(0).Item("Nazwa").ToString & vbNewLine & "Waga kolumny: " & CType(.Columns(i + 2).Tag, ColumnProperty).Waga.ToString & vbNewLine & "Data wystawienia: " + CType(FoundRow(0).Item("Data"), Date).ToString & vbNewLine & "Wystawi³(a): " + Users.Item(FoundRow(0).Item("Owner").ToString.Trim).ToString & vbNewLine & "Zmodyfikowa³(a): " + Users.Item(FoundRow(0).Item("User").ToString.Trim).ToString
              'Row.Cells(i + 2).Style.ForeColor = ColorTranslator.FromHtml(FoundRow(0).Item("KolorHex").ToString)
            Else
              Row.Cells(i + 2).Value = Nothing
              Row.Cells(i + 2).Tag = Nothing
              Row.Cells(i + 2).ToolTipText = Nothing
            End If
            If Row.ReadOnly = False AndAlso (AppUser.Role <> Role.Administrator AndAlso AppUser.SchoolTeacherID <> CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).SchoolTeacherID) Then Row.Cells(i + 2).ReadOnly = True
            'If CType(Row.Tag, StudentAllocation).Status = False OrElse (AppUser.Role <> Role.Administrator AndAlso AppUser.SchoolTeacherID <> CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).SchoolTeacherID) Then Row.Cells(i + 2).ReadOnly = True
          Next
          Row.Cells("Avg").Value = IIf(Suma > 0, Math.Round(Suma / SumaWag, 2), 0).ToString
          Dim Absencja As Absencja = ComputeAbsence(CType(Row.Tag, StudentAllocation))

          Row.Cells("Frekwencja").Value = Absencja.ProcentNieobecnosci
          Row.Cells("Frekwencja").ToolTipText = "Liczba godzin lekcyjnych: " & Absencja.LiczbaLekcji & vbNewLine & "Liczba opuszczonych godzin: " & Absencja.LiczbaNieobecnosci
          'If DTWarning.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdPrzedmiot='" & IdPrzedmiot & "' AND Semestr=" & CType(nudSemestr.Value, Integer)).Length > 0 Then
          If DTWarning.Select("IdPrzydzial=" & CType(Row.Tag, StudentAllocation).AllocationID.ToString).Length > 0 Then
            Row.Cells(.Columns("Warning").Index).Value = True

            Row.Cells(.Columns("Warning").Index).Tag = DTWarning.Select("IdPrzydzial=" & CType(Row.Tag, StudentAllocation).AllocationID.ToString)(0).Item("ID").ToString
            'Row.Cells(.Columns("Warning").Index).Tag = DTWarning.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdPrzedmiot='" & IdPrzedmiot & "' AND Semestr=" & CType(nudSemestr.Value, Integer))(0).Item("ID").ToString
            Row.Cells(.Columns("Warning").Index).ToolTipText = "Uczeñ jest zagro¿ony ocen¹ niedostateczn¹ lub nieklasyfikowaniem" & vbCr & "Wystawi³(a): " & Users.Item(DTWarning.Select("IdPrzydzial=" & CType(Row.Tag, StudentAllocation).AllocationID.ToString)(0).Item("User").ToString.Trim).ToString
            'Row.Cells(.Columns("Warning").Index).ToolTipText = "Uczeñ jest zagro¿ony ocen¹ niedostateczn¹ lub nieklasyfikowaniem" & vbCr & "Wystawi³(a): " & Users.Item(DTWarning.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdPrzedmiot='" & IdPrzedmiot & "' AND Semestr=" & CType(nudSemestr.Value, Integer))(0).Item("User").ToString.Trim).ToString
          Else
            Row.Cells(.Columns("Warning").Index).Value = False
            Row.Cells(.Columns("Warning").Index).Tag = Nothing
            Row.Cells(.Columns("Warning").Index).ToolTipText = "Uczeñ nie jest zagro¿ony ocen¹ niedostateczn¹"
          End If
          If Row.ReadOnly = False AndAlso AppUser.Role <> Role.Administrator AndAlso AppUser.SchoolTeacherID <> CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).SchoolTeacherID Then Row.Cells(.Columns("Warning").Index).ReadOnly = True
          'If CType(Row.Tag, StudentAllocation).Status = False Then
          '  Row.Cells(.Columns("Warning").Index).ReadOnly = True
          'Else
          '  If AppUser.Role <> Role.Administrator AndAlso AppUser.SchoolTeacherID <> CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).SchoolTeacherID Then Row.Cells(.Columns("Warning").Index).ReadOnly = True

          'End If
          If DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdKolumna='" & CType(.Columns("Semestr").Tag, ColumnProperty).ID.ToString & "'").Length > 0 Then
            Row.Cells(.Columns("Semestr").Index).Value = CType(DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdKolumna='" & CType(.Columns("Semestr").Tag, ColumnProperty).ID.ToString & "'")(0).Item("IdOcena"), Integer)
            Row.Cells(.Columns("Semestr").Index).Tag = CType(DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdKolumna='" & CType(.Columns("Semestr").Tag, ColumnProperty).ID.ToString & "'")(0).Item("ID"), String)
            Row.Cells(.Columns("Semestr").Index).ToolTipText = "Data wystawienia: " + CType(DTWyniki.Select("IdUczen='" & CType(Row.Tag, StudentAllocation).ID & "' AND IdKolumna='" & CType(.Columns("Semestr").Tag, ColumnProperty).ID.ToString & "'")(0).Item("Data"), Date).ToString
          Else
            Row.Cells(.Columns("Semestr").Index).Value = Nothing
            Row.Cells(.Columns("Semestr").Index).Tag = Nothing
            Row.Cells(.Columns("Semestr").Index).ToolTipText = Nothing
          End If
          'If CType(Row.Tag, StudentAllocation).Status = False OrElse (AppUser.Role <> Role.Administrator AndAlso AppUser.SchoolTeacherID <> CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).SchoolTeacherID) Then Row.Cells(.Columns("Semestr").Index).ReadOnly = True
          If Row.ReadOnly = False AndAlso AppUser.Role <> Role.Administrator AndAlso AppUser.SchoolTeacherID <> CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).SchoolTeacherID Then Row.Cells(.Columns("Semestr").Index).ReadOnly = True
        Next
        If dgvZestawienieOcen.Rows.Count > 0 Then
          .ClearSelection()
          .CurrentCell = .Rows(IntRowIndex).Cells(IntColumnIndex)
          .Rows(.CurrentCell.RowIndex).Cells(.CurrentCell.ColumnIndex).Selected = True
        End If
        '.Rows(RowIndex).Cells(ColumnIndex).IsInEditMode
      End With

      InRefresh = False
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try
  End Sub

  Private Sub dgvZestawienieOcen_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvZestawienieOcen.CellContentClick
    Try
      If dgvZestawienieOcen.Columns(e.ColumnIndex).Name <> "Warning" OrElse e.RowIndex < 0 OrElse dgvZestawienieOcen.CurrentCell.ReadOnly Then Exit Sub
      'If AppUser.Role = Role.Administrator OrElse AppUser.SchoolTeacherID = CType(cbPrzedmiot.SelectedItem, CbItem).Kod Then
      If CType(dgvZestawienieOcen.CurrentCell.EditedFormattedValue, Boolean) Then
        AddWarning(CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).AllocationID.ToString)
      Else
        DeleteWarning(dgvZestawienieOcen.CurrentCell.Tag.ToString)
      End If
      GetData(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
      'Else
      'MessageBox.Show("Brak wystarczaj¹cych uprawnieñ", Application.ProductName.ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      'End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZestawienieOcen.CellEnter
    Try
      If InRefresh Then Exit Sub
      If e.ColumnIndex > 1 Then
        Me.dgvZestawienieOcen.Columns(e.ColumnIndex).HeaderCell.Style.Font = New Font(Me.dgvZestawienieOcen.Font, FontStyle.Bold)
        If Me.dgvZestawienieOcen.CurrentCell.Tag IsNot Nothing Then
          If e.ColumnIndex <> dgvZestawienieOcen.Columns("Warning").Index Then
            Dim FoundRow() As DataRow
            FoundRow = DTWyniki.Select("IdUczen='" & CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).ID & "' AND IdKolumna='" & CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).ID.ToString & "'") ' AND Typ='" & IIf(nudSemestr.Value = 1, "C1", "C2").ToString & "'")

            Dim User, Owner As String
            User = CType(FoundRow(0).Item("User"), String).ToLower.Trim
            Owner = CType(FoundRow(0).Item("Owner"), String).ToLower.Trim
            'MessageBox.Show(Owner.Length.ToString & vbNewLine & User.Length.ToString)
            If Users.ContainsKey(User) AndAlso Users.ContainsKey(Owner) Then
              lblUser.Text = String.Concat(Users.Item(User).ToString, " (W³: ", Users.Item(Owner).ToString, ")") 'FoundRow(0).Item(3).ToString
            Else
              Me.lblUser.Text = User & " (W³: " & Owner & ")"
            End If
            Me.lblIP.Text = FoundRow(0).Item("ComputerIP").ToString
            Me.lblData.Text = FoundRow(0).Item("Version").ToString
            'cmdDelete.Enabled = True
          Else
            Dim User, Owner As String
            User = DTWarning.Select("IdPrzydzial=" & CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).AllocationID)(0).Item("User").ToString.ToLower
            Owner = DTWarning.Select("IdPrzydzial=" & CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).AllocationID)(0).Item("Owner").ToString.ToLower
            Me.lblUser.Text = String.Concat(Users.Item(User).ToString, " (W³: ", Users.Item(Owner).ToString, ")")
            Me.lblIP.Text = DTWarning.Select("IdPrzydzial=" & CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).AllocationID)(0).Item("ComputerIP").ToString
            Me.lblData.Text = DTWarning.Select("IdPrzydzial=" & CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).AllocationID)(0).Item("Version").ToString
            'cmdDelete.Enabled = False
          End If
        Else
          Me.lblUser.Text = ""
          Me.lblIP.Text = ""
          Me.lblData.Text = ""
        End If
        IntRowIndex = e.RowIndex
        IntColumnIndex = e.ColumnIndex
        'lblOpis.Text = dgvZestawienieOcen.Columns(e.ColumnIndex).ToolTipText
      End If

      If CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).Typ.Substring(0, 1) <> "C" Then
        lblOpis.Text = dgvZestawienieOcen.Columns(e.ColumnIndex).ToolTipText
      Else
        lblOpis.Text = DS.Tables("Kolumna").Select("ID=" & CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).ID.ToString)(0).Item("Nazwa").ToString
      End If

      If dgvZestawienieOcen.Rows(e.RowIndex).ReadOnly = False AndAlso CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).Blokada = False Then
        If AppUser.Role = Role.Administrator OrElse AppUser.SchoolTeacherID = CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).SchoolTeacherID Then
          cmdInsertColumn.Enabled = CType(IIf(CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).Typ.Substring(0, 1) = "C", True, False), Boolean)
          cmdEditOpis.Enabled = CType(IIf(CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).Typ.Substring(0, 1) = "C", True, False), Boolean)
        Else
          cmdInsertColumn.Enabled = False
          cmdEditOpis.Enabled = False
        End If

        'dgvZestawienieOcen.CurrentCell.ReadOnly = False
        Dim ColType As New List(Of String)
        ColType.AddRange(New String() {"C1", "C2", "S", "R"})
        If ColType.Contains(CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).Typ) AndAlso Me.dgvZestawienieOcen.CurrentCell.Tag IsNot Nothing Then
          cmdDelete.Enabled = If(AppUser.Role = Role.Administrator OrElse AppUser.Login = DTWyniki.Select("ID=" & dgvZestawienieOcen.CurrentCell.Tag.ToString)(0).Item("Owner").ToString, True, False)
        Else
          cmdDelete.Enabled = False
        End If
      Else
        cmdDelete.Enabled = False
        cmdInsertColumn.Enabled = False
        cmdEditOpis.Enabled = False
        'dgvZestawienieOcen.CurrentCell.ReadOnly = True

      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub dgvZestawienieOcen_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZestawienieOcen.CellLeave
    Me.dgvZestawienieOcen.Columns(e.ColumnIndex).HeaderCell.Style.Font = Me.dgvZestawienieOcen.Font

  End Sub
  Private Sub dgvZestawienieOcen_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZestawienieOcen.RowEnter
    If InRefresh Then Exit Sub
    For i As Integer = 0 To dgvZestawienieOcen.Columns.Count - 1
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.White
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.BackColor = Color.Orange
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.Font = New Font(dgvZestawienieOcen.Font, FontStyle.Bold)
    Next
    If CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
      'dgvZestawienieOcen.Rows(e.RowIndex).ReadOnly = True
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(0).Style.Font = New Font(dgvZestawienieOcen.Font, FontStyle.Bold Or FontStyle.Strikeout)
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(1).Style.Font = New Font(dgvZestawienieOcen.Font, FontStyle.Bold Or FontStyle.Strikeout)
      'ElseIf CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).NauczanieIndywidualne IsNot Nothing Then
      '  dgvZestawienieOcen.Rows(e.RowIndex).ReadOnly = True
    End If

  End Sub

  Private Sub dgvZestawienieOcen_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZestawienieOcen.RowLeave
    Try

      For i As Integer = 0 To dgvZestawienieOcen.Columns.Count - 1
        dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.BackColor = Me.dgvZestawienieOcen.Rows(e.RowIndex).DefaultCellStyle.BackColor
      Next

      For i As Integer = 0 To dgvZestawienieOcen.Columns.Count - 2
        dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.Font = dgvZestawienieOcen.Font
      Next
      For i As Integer = 0 To dgvZestawienieOcen.Columns("Avg").Index - 1
        dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.ForeColor = Me.dgvZestawienieOcen.Rows(e.RowIndex).DefaultCellStyle.ForeColor
      Next
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(dgvZestawienieOcen.Columns("Avg").Index).Style.ForeColor = Color.Blue
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(dgvZestawienieOcen.Columns("Frekwencja").Index).Style.ForeColor = Color.SteelBlue
      dgvZestawienieOcen.Rows(e.RowIndex).Cells(dgvZestawienieOcen.Columns("Semestr").Index).Style.ForeColor = Color.Navy
      If CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
        dgvZestawienieOcen.Rows(e.RowIndex).Cells(0).Style.Font = New Font(dgvZestawienieOcen.Font, FontStyle.Strikeout)
        dgvZestawienieOcen.Rows(e.RowIndex).Cells(1).Style.Font = New Font(dgvZestawienieOcen.Font, FontStyle.Strikeout)
        For i As Integer = 0 To dgvZestawienieOcen.Columns.Count - 1
          dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.SlateGray
          dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
        Next
      ElseIf CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).NauczanieIndywidualne IsNot Nothing Then
        For i As Integer = 0 To dgvZestawienieOcen.Columns.Count - 1
          dgvZestawienieOcen.Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
        Next
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZestawienieOcen.CellValueChanged
    Try
      If e.RowIndex < 0 Or InRefresh Or dgvZestawienieOcen.Columns(e.ColumnIndex).Name = "Warning" Then Exit Sub
      Dim DBA As New DataBaseAction, W As New WynikiSQL, Data As DateTime
      Data = New DateTime(dtData.Value.Year, dtData.Value.Month, dtData.Value.Day, Now.Hour, Now.Minute, Now.Second)
      'dtData.Value = Now
      If Me.dgvZestawienieOcen.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag IsNot Nothing Then
        If Me.dgvZestawienieOcen.CurrentCell.FormattedValue.ToString.Length = 0 Then
          'DeleteScore()

          Dim cmd As MySqlCommand = DBA.CreateCommand(W.DeleteResult)
          cmd.Parameters.AddWithValue("?IdWynik", dgvZestawienieOcen.CurrentCell.Tag)
          cmd.ExecuteNonQuery()
        Else
          Dim cmd As MySqlCommand = DBA.CreateCommand(W.UpdateResult)
          If dgvZestawienieOcen.Columns(e.ColumnIndex).Name = "Semestr" Then
            cmd.Parameters.AddWithValue("?IdOcena", Me.dgvZestawienieOcen.CurrentCell.Value.ToString)
          Else
            cmd.Parameters.AddWithValue("?IdOcena", Me.DTAllowedMarks.Select("Ocena='" + Me.dgvZestawienieOcen.CurrentCell.FormattedValue.ToString + "'")(0).Item(0).ToString)
          End If
          cmd.Parameters.AddWithValue("?IdWynik", dgvZestawienieOcen.CurrentCell.Tag)
          cmd.Parameters.AddWithValue("?Data", Data)
          cmd.ExecuteNonQuery()
        End If

      Else
        Dim cmd As MySqlCommand = DBA.CreateCommand(W.InsertResult)
        cmd.Parameters.AddWithValue("?IdUczen", CType(dgvZestawienieOcen.CurrentRow.Tag, StudentAllocation).ID)
        If dgvZestawienieOcen.Columns(e.ColumnIndex).Name = "Semestr" Then
          cmd.Parameters.AddWithValue("?IdOcena", Me.dgvZestawienieOcen.CurrentCell.Value.ToString)
          cmd.Parameters.AddWithValue("?IdKolumna", CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).ID.ToString)
        Else
          cmd.Parameters.AddWithValue("?IdOcena", Me.DTAllowedMarks.Select("Ocena='" + Me.dgvZestawienieOcen.CurrentCell.FormattedValue.ToString + "'")(0).Item(0).ToString)
          cmd.Parameters.AddWithValue("?IdKolumna", CType(dgvZestawienieOcen.Columns(e.ColumnIndex).Tag, ColumnProperty).ID.ToString)
        End If
        cmd.Parameters.AddWithValue("?Data", Data)
        cmd.ExecuteNonQuery()
        'End If
      End If

      GetData(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
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
        For Each Cell As DataGridViewCell In Me.dgvZestawienieOcen.SelectedCells
          If Cell.Tag IsNot Nothing Then
            If AppUser.Role = Role.Administrator OrElse AppUser.Login = DTWyniki.Select("ID=" & Cell.Tag.ToString)(0).Item("Owner").ToString Then
              Dim cmd As MySqlCommand = DBA.CreateCommand(W.DeleteResult)
              cmd.Transaction = MyTran
              cmd.Parameters.AddWithValue("?IdWynik", Cell.Tag.ToString)
              cmd.ExecuteNonQuery()
            End If
          End If
          'If Cell.FormattedValue.ToString.Length > 0 Then DBA.ApplyChanges(CS.DeleteString("wyniki", "ID", Cell.Tag.ToString))
        Next
        MyTran.Commit()
        cmdDelete.Enabled = False
        Me.GetData(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)

      Catch mex As MySqlException
        MyTran.Rollback()
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MyTran.Rollback()
        MessageBox.Show(ex.Message)

      End Try
    End If

  End Sub


  Private Sub dgvZestawienieOcen_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvZestawienieOcen.CellValidating
    Try
      If InRefresh Or e.ColumnIndex >= dgvZestawienieOcen.Columns.Count - 2 Or dgvZestawienieOcen.Columns(e.ColumnIndex).ReadOnly Then Exit Sub
      If e.FormattedValue.ToString.Length > 0 Then
        Dim FoundRow() As DataRow
        FoundRow = Me.DTAllowedMarks.Select("Ocena='" + e.FormattedValue.ToString + "'")
        If FoundRow.Length = 0 Then
          MessageBox.Show("Wprowadzona wartoœæ '" + e.FormattedValue.ToString + "' jest nieprawid³owa." & vbNewLine & "Skala ocen nie zawiera tego znaku!")
          e.Cancel = True
        End If
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub nudSemestr_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSemestr.ValueChanged
    Try
      If Me.nudSemestr.Created Then 'AndAlso Me.InRefresh = False Then
        'If Not Me.cbPrzedmiot.SelectedItem Is Nothing Then Me.GetData()
        'IntRowIndex = 0
        'IntColumnIndex = 2
        'dgvZestawienieOcen.Columns("Semestr").HeaderText = IIf(nudSemestr.Value = 1, "Semestr I", "Ocena Roczna").ToString
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
        'dtData.Value = If(CurrentDate > dtData.MaxDate, dtData.MaxDate, CurrentDate)
        If cbPrzedmiot.SelectedItem IsNot Nothing Then cbPrzedmiot_SelectedIndexChanged(sender, Nothing)
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  'Private Sub dtData_ValueChanged(sender As Object, e As EventArgs) Handles dtData.ValueChanged
  '  If dtData.Tag.ToString = "0" AndAlso cbKlasa.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)

  'End Sub
  Private Sub dgvZestawienieOcen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvZestawienieOcen.KeyDown
    Try

      'If dgvZestawienieOcen.CurrentCell.ColumnIndex < 2 OrElse (dgvZestawienieOcen.CurrentCell.ColumnIndex > dgvZestawienieOcen.Columns("Avg").Index - 1 AndAlso dgvZestawienieOcen.CurrentCell.ColumnIndex < dgvZestawienieOcen.Columns("Semestr").Index) Then Exit Sub
      If CType(dgvZestawienieOcen.Columns(dgvZestawienieOcen.CurrentCell.ColumnIndex).Tag, ColumnProperty).Blokada Then Exit Sub
      Dim ColType As New List(Of String)
      ColType.AddRange(New String() {"C1", "C2", "S", "R"})
      If ColType.Contains(CType(dgvZestawienieOcen.Columns(dgvZestawienieOcen.CurrentCell.ColumnIndex).Tag, ColumnProperty).Typ) Then
        If (e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back) AndAlso dgvZestawienieOcen.CurrentCell.Tag IsNot Nothing Then
          Me.DeleteScore()
        End If
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

    'Me.DeleteScore()
  End Sub


  Private Sub dgvZestawienieOcen_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvZestawienieOcen.ColumnHeaderMouseClick

    With dgvZestawienieOcen
      If .RowCount > 0 Then .CurrentCell = .Rows(IntRowIndex).Cells(e.ColumnIndex)
    End With

  End Sub


  Private Sub dgvZestawienieOcen_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvZestawienieOcen.DataError
    MessageBox.Show(e.Exception.Message)
  End Sub

  Private Sub DelResultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelResultToolStripMenuItem.Click
    DeleteScore()
  End Sub


  Private Sub dgvZestawienieOcen_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvZestawienieOcen.CellMouseUp
    Try
      With dgvZestawienieOcen
        If CType(.Columns(e.ColumnIndex).Tag, ColumnProperty).Blokada Then
          DelResultToolStripMenuItem.Enabled = False
          Exit Sub
        End If
        If e.RowIndex < 0 Then
          .CurrentCell = .Rows(IntRowIndex).Cells(e.ColumnIndex)
          DelResultToolStripMenuItem.Enabled = False
        Else
          .CurrentCell = .Rows(e.RowIndex).Cells(e.ColumnIndex)
          If e.Button = Windows.Forms.MouseButtons.Right AndAlso .Rows(e.RowIndex).Cells(e.ColumnIndex).Selected AndAlso .Rows(e.RowIndex).Cells(e.ColumnIndex).Tag IsNot Nothing Then
            If AppUser.Role = Role.Administrator OrElse AppUser.Login = DTWyniki.Select("ID=" & .Rows(e.RowIndex).Cells(e.ColumnIndex).Tag.ToString)(0).Item("Owner").ToString Then
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

  Private Sub EditDescription(ColumnIndex As Integer)
    Try
      Dim dlgEdit As New dlgOpisKolumny
      With dlgEdit
        .OK_Button.Text = "&Zapisz"
        .ClassChanged(CType(cbKlasa.SelectedItem, SchoolClassComboItem).NazwaKlasy)

        .PrzedmiotChanged(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID.ToString, CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ObjectStaff, IdPrzedmiot)
        .txtNrKolumny.Text = dgvZestawienieOcen.Columns(ColumnIndex).HeaderText

        .SetWaga(CType(dgvZestawienieOcen.Columns(ColumnIndex).Tag, ColumnProperty).Waga)
        .chkPoprawa.Checked = If(CType(dgvZestawienieOcen.Columns(ColumnIndex).Tag, ColumnProperty).Poprawa, True, False)
        .Text = "Edycja parametrów kolumny"
        .ListViewConfig(.lvOpisWyniku)
        .GetData()
        If lblOpis.Text.Length > 0 Then
          Dim SH As New SeekHelper
          SH.FindListViewItem(.lvOpisWyniku, lblOpis.Text)
        End If

        .CancelButton = .Cancel_Button
        .AcceptButton = .OK_Button

        If .ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdKolumna As String, DBA As New DataBaseAction, OK As New KolumnaSQL
          Dim MySQLTrans As MySqlTransaction = Nothing

          IdKolumna = CType(dgvZestawienieOcen.Columns(ColumnIndex).Tag, ColumnProperty).ID.ToString
          Try
            Dim cmd As MySqlCommand = DBA.CreateCommand(OK.UpdateKolumna)
            MySQLTrans = GlobalValues.gblConn.BeginTransaction()
            cmd.Transaction = MySQLTrans

            cmd.Parameters.AddWithValue("?ID", IdKolumna)
            'cmd.Parameters.AddWithValue("?IdObsada", CType(cbPrzedmiot.SelectedItem, CbItem).ID)
            If .lvOpisWyniku.SelectedItems.Count > 0 Then
              cmd.Parameters.AddWithValue("?IdOpis", .lvOpisWyniku.SelectedItems(0).Text)
            Else
              cmd.Parameters.AddWithValue("?IdOpis", DBNull.Value)
            End If
            cmd.Parameters.AddWithValue("?Waga", .nudWaga.Value)
            cmd.Parameters.AddWithValue("?Poprawa", .chkPoprawa.CheckState)

            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch mex As MySqlException
            MessageBox.Show(mex.Message)
            MySQLTrans.Rollback()
          Catch ex As Exception
            MySQLTrans.Rollback()
            MessageBox.Show(ex.Message)
          End Try
          RefreshData()
        End If
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  'Private Sub EditDescriptionToolStripMenuItem_Click(sender As Object, e As EventArgs)
  '  EditDescription(dgvZestawienieOcen.CurrentCell.ColumnIndex)
  'End Sub
  
  Private Sub cmdInsertColumn_Click(sender As Object, e As EventArgs) Handles cmdInsertColumn.Click
    InsertColumn(CType(dgvZestawienieOcen.Columns(dgvZestawienieOcen.CurrentCell.ColumnIndex).HeaderText, Byte))
  End Sub

  Private Sub cmdEditOpis_Click(sender As Object, e As EventArgs) Handles cmdEditOpis.Click
    EditDescription(dgvZestawienieOcen.CurrentCell.ColumnIndex)
  End Sub

  'Private Sub InsertColumnToolStripMenuItem_Click(sender As Object, e As EventArgs)
  '  InsertColumn(CType(dgvZestawienieOcen.Columns(dgvZestawienieOcen.CurrentCell.ColumnIndex).HeaderText, Byte))
  'End Sub
  Private Sub AddWarning(IdPrzydzial As String)
    Dim DBA As New DataBaseAction, Z As New ZagrozeniaSQL
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      Dim cmd As MySqlCommand = DBA.CreateCommand(Z.InsertPrzedmiot)

      cmd.Transaction = MySQLTrans
      cmd.Parameters.AddWithValue("?IdPrzedmiot", IdPrzedmiot)
      cmd.Parameters.AddWithValue("?IdPrzydzial", IdPrzydzial) 'dsStudent.Tables("Student").Select("ID=" & IdUczen)(0).Item("IdPrzydzial").ToString)
      cmd.Parameters.AddWithValue("?Semestr", CType(nudSemestr.Value, Integer))
      cmd.Parameters.AddWithValue("?User", AppUser.Login)
      cmd.Parameters.AddWithValue("?ComputerIP", gblIP)
      cmd.ExecuteNonQuery()

      MySQLTrans.Commit()
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      MySQLTrans.Rollback()
    End Try

  End Sub
  Private Sub DeleteWarning(ID As String)
    Dim DBA As New DataBaseAction, Z As New ZagrozeniaSQL ', DeletedIndex As Integer
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = gblConn.BeginTransaction()
    Try
      Dim cmd As MySqlCommand = DBA.CreateCommand(Z.DeletePrzedmiot)
      cmd.Parameters.AddWithValue("?ID", ID)
      cmd.Transaction = MySQLTrans
      cmd.ExecuteNonQuery()
      'Next
      MySQLTrans.Commit()

    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Function ComputeAbsence(Przydzial As StudentAllocation) As Absencja
    Try
      Dim IdUczen As Integer = Przydzial.ID
      Dim DataAktywacji, DataDeaktywacji As Date
      DataAktywacji = Przydzial.DataAktywacji
      DataDeaktywacji = Przydzial.DataDeaktywacji
      If Przydzial.NauczanieIndywidualne IsNot Nothing Then
        Dim IdPrzedmiot As Integer = CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ObjectID
        For Each P As IndividualStaff In Przydzial.NauczanieIndywidualne.SchoolObject
          If P.ObjectID = IdPrzedmiot Then DataDeaktywacji = P.StartDate.AddDays(-1)
        Next
      End If
      Dim Frekwencja As Single, LiczbaLekcji, LiczbaNb As Integer
      LiczbaLekcji = CType(DS.Tables("LekcjaBezZastepstw").Compute("COUNT(ID)", "Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# is null)"), Integer)
      LiczbaLekcji += CType(DS.Tables("Zastepstwo").Compute("COUNT(ID)", "Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# IS NULL)"), Integer)
      If LiczbaLekcji = 0 Then Return New Absencja With {.ProcentNieobecnosci = "-", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = "?"}
      LiczbaNb = CType(DS.Tables("Absence").Compute("COUNT(IdUczen)", "IdUczen=" & IdUczen & " AND Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# is null)"), Integer)
      LiczbaNb += CType(DS.Tables("AbsenceZastepstwo").Compute("COUNT(IdUczen)", "IdUczen=" & IdUczen & " AND Data>=#" & DataAktywacji & "# AND (Data<=#" & DataDeaktywacji & "# OR #" & DataDeaktywacji & "# IS NULL)"), Integer)
      Frekwencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
      Frekwencja = CType(IIf(Frekwencja > 100, 100, Frekwencja), Single)
      Return New Absencja With {.ProcentNieobecnosci = Frekwencja.ToString & "%", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = LiczbaNb.ToString}
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return Nothing
    End Try
  End Function

  Private Sub dgvZestawienieOcen_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvZestawienieOcen.EditingControlShowing
    Try
      e.CellStyle.BackColor = Color.White
      e.CellStyle.ForeColor = Color.Black
      If dgvZestawienieOcen.CurrentCell.ColumnIndex = dgvZestawienieOcen.Columns("Semestr").Index Then
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
      R = DBA.GetReader(W.SelectMarksByWaga())
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




  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Dim PP As New dlgPrintPreview ', DSP As New DataSet
    PP.Doc = New PrintReport(Nothing)
    PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
    PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
    PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin
    RemoveHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
    RemoveHandler NewRow, AddressOf PP.NewRow
    RemoveHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_SelectedPrzedmiot_PrintPage
    AddHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    AddHandler NewRow, AddressOf PP.NewRow
    PP.Doc.ReportHeader = New String() {"Wyniki nauczania - semestr " & nudSemestr.Value.ToString, "Klasa " & cbKlasa.Text, "Przedmiot: " & cbPrzedmiot.Text}
    PP.Width = 1000
    PP.ShowDialog()
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
  Public Sub PrnDoc_SelectedPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
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

    '---------------------------------------- Nag³ówek i stopka ----------------------------
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
    Dim ColSize As Integer = 25, NameColSize As Integer = 150, SemestrColSize = 120
    Dim dx = x
    PH.DrawText("Nr", New Font(TextFont, FontStyle.Bold), dx, y, ColSize, LineHeight * 2, 1, Brushes.Black)
    dx += ColSize
    PH.DrawText("Nazwisko i imiê", New Font(TextFont, FontStyle.Bold), dx, y, NameColSize, LineHeight * 2, 1, Brushes.Black)
    dx += NameColSize

    For i As Integer = 2 To dgvZestawienieOcen.Columns.Count - 5
      PH.DrawText(dgvZestawienieOcen.Columns(i).HeaderText, New Font(TextFont, FontStyle.Bold), dx, y, ColSize, LineHeight * 2, 1, Brushes.Black)
      dx += ColSize
    Next
    PH.DrawText("Semestr " & nudSemestr.Value.ToString, New Font(TextFont, FontStyle.Bold), dx, y, SemestrColSize, LineHeight * 2, 1, Brushes.Black)
    y += LineHeight * 2
    dx = x
    Do Until (y + LineHeight * CSng(1.5)) > PrintHeight Or Offset(0) > dgvZestawienieOcen.Rows.Count - 1
      PH.DrawText(dgvZestawienieOcen.Rows(Offset(0)).Cells("Nr").FormattedValue.ToString, TextFont, dx, y, ColSize, LineHeight, 1, Brushes.Black)
      dx += ColSize
      PH.DrawText(dgvZestawienieOcen.Rows(Offset(0)).Cells("Nazwisko").FormattedValue.ToString, TextFont, dx, y, NameColSize, LineHeight, 0, Brushes.Black)
      dx += NameColSize
      Offset(1) = 2
      Do Until Offset(1) > dgvZestawienieOcen.Columns.Count - 5
        PH.DrawText(dgvZestawienieOcen.Rows(Offset(0)).Cells(Offset(1)).FormattedValue.ToString, TextFont, dx, y, ColSize, LineHeight, 1, Brushes.Black)
        dx += ColSize
        Offset(1) += 1
      Loop
      PH.DrawText(dgvZestawienieOcen.Rows(Offset(0)).Cells("Semestr").FormattedValue.ToString, TextFont, dx, y, SemestrColSize, LineHeight, 0, Brushes.Black)
      dx = x
      y += LineHeight
      Offset(1) = 0
      Offset(0) += 1
    Loop
    If Offset(0) < dgvZestawienieOcen.Rows.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
      PageNumber = 0
    End If
  End Sub
  Private Sub InsertColumn(CurrentColumn As Byte)
    Dim dlg As New dlgWstawKolumna
    Dim MySQLTrans As MySqlTransaction = Nothing
    With dlg
      Try
        .ClassChanged(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ToString)
        .PrzedmiotChanged(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ToString)
        .SemestrChanged(nudSemestr.Value.ToString)
        .SetMaxColNumber()
        .SetColNumber(dgvZestawienieOcen.Columns.Count - dgvZestawienieOcen.Columns.GetColumnCount(DataGridViewElementStates.ReadOnly) - 2)
        .UpdateColData(CurrentColumn)

        .cbPozycja.Items.Add(New CbItem(CurrentColumn, "Przed kolumn¹ odniesienia"))
        .cbPozycja.Items.Add(New CbItem(CurrentColumn + 1, "Po kolumnie odniesienia"))
        .cbPozycja.SelectedIndex = 1
        .MaximizeBox = False
        .StartPosition = FormStartPosition.CenterScreen
        'cmdInsertColumn.Enabled = False
        If .ShowDialog = Windows.Forms.DialogResult.OK Then
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim TotalColNumber As Byte = CType((dgvZestawienieOcen.Columns.Count - dgvZestawienieOcen.Columns.GetColumnCount(DataGridViewElementStates.ReadOnly) - 2), Byte)
          While TotalColNumber >= CType(CType(.cbPozycja.SelectedItem, CbItem).ID, Byte) 'ColPos
            .MoveColumn(TotalColNumber, CType(.nudLiczbaKolumn.Value, Byte), CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString, MySQLTrans)
            TotalColNumber -= CType(1, Byte)
          End While
          .AddNew(CType(CType(.cbPozycja.SelectedItem, CbItem).ID, Byte), CType(.nudLiczbaKolumn.Value, Byte), CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString, MySQLTrans)
          MySQLTrans.Commit()
          RefreshData()
          'cbPrzedmiot_SelectedIndexChanged(Nothing, Nothing)
        End If

      Catch myex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(myex.Message)
      Catch ex As Exception
        MySQLTrans.Rollback()
        MessageBox.Show(ex.Message)
      Finally

      End Try

    End With
  End Sub
  
  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkVirtual.CheckedChanged
    If Not Me.Created Then Exit Sub
    ApplyNewConfig()
  End Sub

  Private Sub dgvZestawienieOcen_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvZestawienieOcen.RowPostPaint
    If (e.RowIndex + 1) Mod 10 = 0 Then
      Dim RowWidth As Integer = dgvZestawienieOcen.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
      e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 1, RowWidth, e.RowBounds.Bottom - 1)
      'e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 2, e.RowBounds.Right, e.RowBounds.Bottom - 2)
    End If
  End Sub

End Class

Public Class EndMark
  Public Property Waga As Integer
  Public Property Nazwa As String
End Class
Public Class ColumnProperty
  Public Property ID As Integer
  Public Property Waga As Decimal
  Public Property Poprawa As Boolean
  Public Property Typ As String
  Public Property Nr As Byte
  Public Property Opis As String
  Public Property Blokada As Boolean
End Class

Public Class Absencja
  Public Property ProcentNieobecnosci As String
  Public Property LiczbaLekcji As String
  Public Property LiczbaNieobecnosci As String
  Public Property Frekwencja As Single
End Class