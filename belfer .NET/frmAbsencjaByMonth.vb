Imports System.Drawing.Printing
Public Class frmAbsencjaByMonth
  Private DTAbsence, dtIndividualStaff As DataTable
  'Private IntRowIndex As Integer = 0, IntColumnIndex As Integer = 2 '
  Private Student As List(Of GroupMember), SchoolSemester As List(Of SchoolMonth)
  Private StartDate, EndDate As Date, InRefresh As Boolean
  Public Event NewRow()
  Private Offset(1), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.AbsencjaByMonthToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.AbsencjaByMonthToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmZachowanie_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    Me.Height += 1
    Me.Height -= 1
  End Sub
  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    DataGridConfig(dgvAbsencja)
    rbSemestr1.Tag = New SchoolMonth With {.Nr = 1, .Name = "9,10,11,12,1"}
    rbSemestr2.Tag = New SchoolMonth With {.Nr = 2, .Name = "2,3,4,5,6"}
    If Date.Today.Month < 2 OrElse Date.Today.Month > 8 Then
      rbSemestr1.Checked = True
    Else
      rbSemestr2.Checked = True
    End If
    'nudSemestr.Value = If(Date.Today.Month < 2 OrElse Date.Today.Month > 8, 1, 2)
    'nudSemestr.Minimum = 1
    'ApplyNewConfig()
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
      SetColumns(dgvAbsencja)
      FillKlasa()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Function FetchStudent() As DataTable
    Dim DBA As New DataBaseAction, W As New WynikiSQL, T As New TematSQL
    Dim SelectString As String = ""
    If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
      SelectString = T.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, Date.Today)
      dtIndividualStaff = Nothing
    Else
      SelectString = W.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
      dtIndividualStaff = DBA.GetDataTable(T.SelectIndividualStaff(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString))
    End If
    Return DBA.GetDataTable(SelectString)
  End Function
  Private Sub FetchAbsence(Klasa As String, StartDate As Date, EndDate As Date)
    Dim DBA As New DataBaseAction, S As New StatystykaSQL
    DTAbsence = DBA.GetDataTable(S.CountAbsence(Klasa, StartDate, EndDate))
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

    RefreshData()

  End Sub
  Private Sub RefreshData()
    InRefresh = True
    Cursor = Cursors.WaitCursor
    SetRows(dgvAbsencja)
    Me.GetData()
    Me.dgvAbsencja.Focus()
    Me.dgvAbsencja.Enabled = True
    Cursor = Cursors.Default
    InRefresh = False
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
          .ToolTipText = "Numer ucznia w dzienniku"
          .ReadOnly = True
          .Frozen = True
          .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        .Columns.Add(NrCol)

        With NameCol
          .Name = "Nazwisko"
          .HeaderText = ""
          .Width = 200
          .CellTemplate = New DataGridViewTextBoxCell()
          .CellTemplate.Style.BackColor = Color.LightYellow
          .ToolTipText = "Nazwisko i imię ucznia"
          .ReadOnly = True
          .Frozen = True
          .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        .Columns.Add(NameCol)

        Dim ShiftColor As Boolean = False, GridColor() As Color = {Color.Khaki, Color.White}
        For Each M As SchoolMonth In SchoolSemester
          ShiftColor = Not ShiftColor
          For Each Nb As String In "UNS"
            Dim AbsenceType As New DataGridViewColumn
            With AbsenceType
              .Name = String.Concat(M.Nr, "#", Nb)
              .HeaderText = Nb
              .Tag = M
              .Width = 40
              .CellTemplate = New DataGridViewTextBoxCell()
              '.CellTemplate.Style.Font = New Font(dgvAbsencja.Font, FontStyle.Bold)
              .CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
              .CellTemplate.Style.BackColor = GridColor(Convert.ToInt32(ShiftColor))
              If Nb = "U" Then
                .CellTemplate.Style.ForeColor = Color.Green
              ElseIf Nb = "N" Then
                .CellTemplate.Style.ForeColor = Color.Red
              Else
                .CellTemplate.Style.ForeColor = Color.SteelBlue
              End If
              .ReadOnly = True
              .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(AbsenceType)
          Next
        Next

        For Each S As String In New String() {"U", "N", "S"}
          Dim UColumn As New DataGridViewTextBoxColumn
          With UColumn
            .HeaderText = S
            .Width = 40 '65
            '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate = New DataGridViewTextBoxCell
            .Name = S
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate.Style.BackColor = Color.MintCream
            .CellTemplate.Style.ForeColor = Color.Blue
            .CellTemplate.Style.Font = New Font(dgvAbsencja.Font, FontStyle.Bold)
            If S = "U" Then
              .ToolTipText = "Liczba godzin usprawiedliwionych"
            ElseIf S = "N" Then
              .ToolTipText = "Liczba godzin nieusprawiedliwionych"
            Else
              .ToolTipText = "Liczba spóźnień"
            End If
          End With
          .Columns.Add(UColumn)
        Next
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally

    End Try

  End Sub
  Private Sub ClearCellData()
    For Each R As DataGridViewRow In dgvAbsencja.Rows
      For i As Integer = 2 To R.Cells.Count - 1
        R.Cells(i).Tag = Nothing
        R.Cells(i).Value = Nothing
      Next
    Next
  End Sub
  Private Sub GetData()
    'InRefresh = True
    Try
      FetchAbsence(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, StartDate, EndDate)
      ClearCellData()
      With dgvAbsencja
        For Each R As DataGridViewRow In .Rows
          Dim dvAbsenceMonth As DataView = New DataView(DTAbsence, "IdUczen=" & CType(R.Tag, StudentAllocation).ID, "Typ", DataViewRowState.CurrentRows)
          If rbRokSzkolny.Checked Then
            For Each B As RadioButton In Controls.OfType(Of RadioButton).Where(Function(rb) rb.Checked = False)
              Dim dvAbsenceBySemestr As DataTable = New DataView(dvAbsenceMonth.ToTable, "MonthNumber IN (" & CType(B.Tag, SchoolMonth).Name & ")", "Typ", DataViewRowState.CurrentRows).ToTable(False, "Typ", "AbsenceCount")
              'SetCellContent(R, dvAbsenceByMonth.Rows, CType(B.Tag, SchoolMonth).Nr.ToString)
              SetTotalAbsence(R, dvAbsenceBySemestr, CType(B.Tag, SchoolMonth).Nr)
            Next
          Else
            For Each M As DataRow In dvAbsenceMonth.ToTable(True, "MonthNumber").Rows
              Dim dvAbsenceByMonth As DataTable = New DataView(dvAbsenceMonth.ToTable, "MonthNumber=" & M.Item("MonthNumber").ToString, "Typ", DataViewRowState.CurrentRows).ToTable(False, "Typ", "AbsenceCount")
              SetCellContent(R, dvAbsenceByMonth.Rows, M.Item("MonthNumber").ToString)
            Next
          End If
          SetTotalAbsence(R, dvAbsenceMonth.ToTable(False, "AbsenceCount", "Typ"))
        Next
        .ClearSelection()
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub SetTotalAbsence(Row As DataGridViewRow, DT As DataTable)
    Row.Cells("U").Value = If(DT.Select("Typ='u'").Length > 0, DT.Compute("SUM(AbsenceCount)", "Typ='u'").ToString, Chr(151))
    Row.Cells("N").Value = If(DT.Select("Typ='n'").Length > 0, DT.Compute("SUM(AbsenceCount)", "Typ='n'").ToString, Chr(151))
    Row.Cells("S").Value = If(DT.Select("Typ='s'").Length > 0, DT.Compute("Sum(AbsenceCount)", "Typ='s'").ToString, Chr(151))
  End Sub
  Private Sub SetTotalAbsence(Row As DataGridViewRow, DT As DataTable, Semestr As Byte)
    Row.Cells(String.Concat(Semestr, "#", "U")).Value = If(DT.Select("Typ='u'").Length > 0, DT.Compute("SUM(AbsenceCount)", "Typ='u'").ToString, Chr(151))
    Row.Cells(String.Concat(Semestr, "#", "N")).Value = If(DT.Select("Typ='n'").Length > 0, DT.Compute("SUM(AbsenceCount)", "Typ='n'").ToString, Chr(151))
    Row.Cells(String.Concat(Semestr, "#", "S")).Value = If(DT.Select("Typ='s'").Length > 0, DT.Compute("Sum(AbsenceCount)", "Typ='s'").ToString, Chr(151))
  End Sub
  Private Sub SetCellContent(Row As DataGridViewRow, Absence As DataRowCollection, MonthNumber As String)
    For Each R As DataRow In Absence
      Dim CellName = String.Concat(MonthNumber, "#", R.Item("Typ").ToString)
      Row.Cells(CellName).Value = R.Item("AbsenceCount").ToString
    Next
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
          Dim NI As IndividualCourse = GetIndividualCourse(Student.Item("IdPrzydzial").ToString, Date.Today)
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
      'Dim Idx As Integer = 0, ShiftColor As Boolean = False, GridColor() As Color = {Color.LightYellow, Color.MintCream}
      With dgvName
        .Rows.Clear()
        For Each M As GroupMember In Students
          .Rows.Add(M.No, M.Name)
          .Rows(.Rows.Count - 1).Tag = M.Allocation
          .Rows(.Rows.Count - 1).Cells(0).ToolTipText = "Data zapisu: " & M.Allocation.DataAktywacji.ToShortDateString
          .Rows(.Rows.Count - 1).Cells(1).ToolTipText = "Data zapisu: " & M.Allocation.DataAktywacji.ToShortDateString

          If M.Allocation.Status Then
            If M.Allocation.NauczanieIndywidualne Is Nothing Then
              .Rows(.Rows.Count - 1).Cells(1).ToolTipText += vbNewLine & "Tryb nauki: normalny"
            Else
              For i As Integer = 0 To 1
                .Rows(.Rows.Count - 1).Cells(i).ToolTipText += vbNewLine & "Tryb nauki: nauczanie indywidualne " & vbNewLine & vbTab & "Data początkowa: " & M.Allocation.NauczanieIndywidualne.SchoolObject(0).StartDate.ToShortDateString & vbNewLine & vbTab & "Data końcowa: " & If(M.Allocation.NauczanieIndywidualne.SchoolObject(0).EndDate = Nothing, "", M.Allocation.NauczanieIndywidualne.SchoolObject(0).EndDate.ToShortDateString)
                '.Rows(.Rows.Count - 1).Cells(i).Style.ForeColor = Color.SlateGray
                .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
              Next
            End If
          Else
            .Rows(.Rows.Count - 1).Cells(1).ToolTipText += vbNewLine & "Data skreślenia: " & M.Allocation.DataDeaktywacji.ToString
            .Rows(.Rows.Count - 1).Cells(0).Style.Font = New Font(dgvName.Font, FontStyle.Strikeout)
            .Rows(.Rows.Count - 1).Cells(1).Style.Font = New Font(dgvName.Font, FontStyle.Strikeout)
            For i As Integer = 0 To .Rows(.Rows.Count - 1).Cells.Count - 1
              .Rows(.Rows.Count - 1).Cells(i).Style.ForeColor = Color.SlateGray
              .Rows(.Rows.Count - 1).Cells(i).Style.BackColor = Color.LightGray
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


      'Dim ColIndex As Integer = 2
      For i As Integer = 0 To SchoolSemester.Count - 1
        Dim r1 As Rectangle = Me.dgvAbsencja.GetCellDisplayRectangle(3 * i + 2, -1, True)
        e.Graphics.DrawLine(Pens.Black, r1.X, r1.Y, r1.X, r1.Height)
        r1.X += 1
        r1.Y += 1
        r1.Width = r1.Width * 3
        r1.Height = CType(r1.Height / 2 - 2, Integer)
        e.Graphics.FillRectangle(New SolidBrush(Color.Salmon), r1)
        'If r1.Width > e.Graphics.MeasureString(SchoolWeek.Item(i).Value.ToString, New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)).Width Then
        e.Graphics.DrawString(SchoolSemester.Item(i).Name, New Font(Me.dgvAbsencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.White), r1, StrFormat)
      Next
      Dim r2 As Rectangle = Me.dgvAbsencja.GetCellDisplayRectangle(dgvAbsencja.ColumnCount - 3, -1, True)
      e.Graphics.DrawLine(Pens.Black, r2.X, r2.Y, r2.X, r2.Height)
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
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub dgvAbsencja_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAbsencja.RowEnter
    If InRefresh Then Exit Sub
    With dgvAbsencja
      For i As Integer = 0 To .Columns.Count - 1
        .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.White
        .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.Orange
        .Rows(e.RowIndex).Cells(i).Style.Font = New Font(.Font, FontStyle.Bold)
      Next
      '.Rows(e.RowIndex).Cells(0).Style.Font = New Font(.Font, FontStyle.Bold)
      '.Rows(e.RowIndex).Cells(1).Style.Font = New Font(.Font, FontStyle.Bold)
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
          .Rows(e.RowIndex).Cells(i).Style.Font = .Columns(i).CellTemplate.Style.Font
        Next
        For i As Integer = 0 To 1
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = .Columns(i).CellTemplate.Style.ForeColor  '.Rows(e.RowIndex).DefaultCellStyle.ForeColor
          .Rows(e.RowIndex).Cells(i).Style.Font = .Font
        Next

        For i As Integer = 2 To .Columns.Count - 4
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = .Columns(i).CellTemplate.Style.ForeColor
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

  Private Sub dgvZestawienieOcen_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvAbsencja.DataError
    MessageBox.Show(e.Exception.Message)
  End Sub


  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkVirtual.CheckedChanged
    If Not Me.Created Then Exit Sub
    'ApplyNewConfig()
    dgvAbsencja.Rows.Clear()
    dgvAbsencja.Columns.Clear()
    ApplyNewConfig()
    'FillKlasa()
  End Sub
  Private Sub rbSemestr1_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemestr1.CheckedChanged, rbSemestr2.CheckedChanged, rbRokSzkolny.CheckedChanged
    If rbSemestr1.Checked Then

      StartDate = New Date(CType(My.Settings.SchoolYear.Substring(0, 4), Integer), 9, 1)
      EndDate = New Date(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), 1, 31)
      SchoolSemester = New List(Of SchoolMonth) From {New SchoolMonth With {.Nr = 9, .Name = "wrzesień"},
                                                      New SchoolMonth With {.Nr = 10, .Name = "październik"},
                                                      New SchoolMonth With {.Nr = 11, .Name = "listopad"},
                                                      New SchoolMonth With {.Nr = 12, .Name = "grudzień"},
                                                      New SchoolMonth With {.Nr = 1, .Name = "styczeń"}}

    ElseIf rbSemestr2.Checked Then
      StartDate = New Date(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), 2, 1)
      EndDate = New Date(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), 6, 30)
      SchoolSemester = New List(Of SchoolMonth) From {New SchoolMonth With {.Nr = 2, .Name = "luty"},
                                                      New SchoolMonth With {.Nr = 3, .Name = "marzec"},
                                                      New SchoolMonth With {.Nr = 4, .Name = "kwiecień"},
                                                      New SchoolMonth With {.Nr = 5, .Name = "maj"},
                                                      New SchoolMonth With {.Nr = 6, .Name = "czerwiec"}}
    Else
      StartDate = New Date(CType(My.Settings.SchoolYear.Substring(0, 4), Integer), 9, 1)
      EndDate = New Date(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), 6, 30)
      SchoolSemester = New List(Of SchoolMonth) From {New SchoolMonth With {.Nr = 1, .Name = "Semestr 1"},
                                                      New SchoolMonth With {.Nr = 2, .Name = "Semestr 2"}}

    End If
    ApplyNewConfig()
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
    PP.Doc.ReportHeader = New String() {"Zestawienie nieobecności uczniów", "Klasa " & cbKlasa.Text, Controls.OfType(Of RadioButton).Where(Function(r) r.Checked = True).FirstOrDefault().Text}
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
  Public Sub PrnDoc_SelectedPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
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
    Dim ColSize As Integer = 25, NameColSize As Integer = 150, AbsenceColSize As Integer = 30
    Dim dx = x, y0 As Single = y
    PH.DrawText("Nr", New Font(TextFont, FontStyle.Bold), dx, y, ColSize, LineHeight * 2, 1, Brushes.Black)
    dx += ColSize
    PH.DrawText("Nazwisko i imię", New Font(TextFont, FontStyle.Bold), dx, y, NameColSize, LineHeight * 2, 1, Brushes.Black)
    dx += NameColSize
    Dim dx1 = dx
    With dgvAbsencja
      For Each M As SchoolMonth In SchoolSemester
        PH.DrawText(M.Name, New Font(TextFont, FontStyle.Bold), dx1, y, AbsenceColSize * 3, LineHeight, 1, Brushes.Black)
        dx1 += AbsenceColSize * 3
      Next
      PH.DrawText("Razem", New Font(TextFont, FontStyle.Bold), dx1, y, AbsenceColSize * 3, LineHeight, 1, Brushes.Black)
      For i As Integer = 2 To .Columns.Count - 1
        PH.DrawText(.Columns(i).HeaderText, New Font(TextFont, FontStyle.Bold), dx, y + LineHeight, AbsenceColSize, LineHeight, 1, Brushes.Black)
        dx += AbsenceColSize
      Next
      y += LineHeight * 2
      PH.DrawLine(x, y, dx, y, 2)
      dx = x
      Dim idx As Integer = 10
      Do Until (y + LineHeight * CSng(1.5)) > PrintHeight Or Offset(0) > .Rows.Count - 1
        PH.DrawText(.Rows(Offset(0)).Cells("Nr").FormattedValue.ToString, TextFont, dx, y, ColSize, LineHeight, 1, Brushes.Black)
        dx += ColSize
        PH.DrawText(.Rows(Offset(0)).Cells("Nazwisko").FormattedValue.ToString, TextFont, dx, y, NameColSize, LineHeight, 0, Brushes.Black)
        dx += NameColSize
        Offset(1) = 2
        Do Until Offset(1) > .Columns.Count - 1
          PH.DrawText(If(.Rows(Offset(0)).Cells(Offset(1)).FormattedValue.ToString.Length > 0, .Rows(Offset(0)).Cells(Offset(1)).FormattedValue.ToString, "-"), TextFont, dx, y, AbsenceColSize, LineHeight, 1, Brushes.Black)
          dx += AbsenceColSize
          Offset(1) += 1
        Loop
        y += LineHeight
        Offset(1) = 0
        Offset(0) += 1
        If Offset(0) = idx Then
          PH.DrawLine(x, y, dx, y, 2)
          idx += Offset(0)
        End If
        dx = x
      Loop
      PH.DrawRectangle(2, x, y0, ColSize + NameColSize + AbsenceColSize * (.Columns.Count - 2), y - y0)
      PH.DrawLine(x + ColSize, y0, x + ColSize, y, 2)
      PH.DrawLine(x + ColSize + NameColSize, y0, x + ColSize + NameColSize, y, 2)
      For i As Integer = 3 To .Columns.Count - 5 Step 3
        PH.DrawLine(x + ColSize + NameColSize + AbsenceColSize * i, y0, x + ColSize + NameColSize + AbsenceColSize * i, y, 2)
      Next
      If Offset(0) < .Rows.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
      Else
        Offset(0) = 0
        PageNumber = 0
      End If
    End With

  End Sub


  Private Sub dgvAbsencja_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvAbsencja.RowPostPaint
    If (e.RowIndex + 1) Mod 10 = 0 Then
      Dim RowWidth As Integer = dgvAbsencja.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
      e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 1, RowWidth, e.RowBounds.Bottom - 1)
      'e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 2, e.RowBounds.Right, e.RowBounds.Bottom - 2)
    End If
  End Sub
End Class

Public Class SchoolMonth
  Public Property Nr As Byte
  Public Property Name As String
End Class