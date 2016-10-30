Public Class frmWynikiPoprawkowe
  Private InRefresh As Boolean = True
  Dim EndMarks As List(Of EndMark)
  Private IntRowIndex As Integer = 0, IntColumnIndex As Integer = 2 ', IdKolumnaZachowania As String

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.WynikiPoprawkoweToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.WynikiPoprawkoweToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub

  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    GetEndMarks()
    DataGridConfig(dgvPoprawka)
    SetColumns(dgvPoprawka)
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    'lblOpis.Text = ""
    Dim CH As New CalcHelper
    InRefresh = True
    Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    InRefresh = False
    dgvPoprawka.Rows.Clear()
    FillKlasa()
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
    dgvPoprawka.Rows.Clear()
    IntRowIndex = 0
    cmdDelete.Enabled = False
    nudSemestr.Enabled = True
    SetRows(dgvPoprawka)
    Cursor = Cursors.Default
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
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 30
      '.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      'AddHandler .CellPainting, AddressOf dgvZestawienieOcen_CellPainting
      'AddHandler .Paint, AddressOf dgvZestawienieOcen_Paint
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
        Dim NameCol, NrCol, ObjectCol As New DataGridViewColumn
        NrCol.Name = "Nr"
        NrCol.HeaderText = "Nr"
        NrCol.Width = 30
        NrCol.CellTemplate = New DataGridViewTextBoxCell()
        NrCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        NrCol.ToolTipText = "Numer ucznia w dzienniku"
        NrCol.ReadOnly = True
        NrCol.Frozen = True
        NrCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NrCol)

        NameCol.Name = "Nazwisko"
        NameCol.HeaderText = "Nazwisko i imiê"
        NameCol.Width = 200
        NameCol.CellTemplate = New DataGridViewTextBoxCell()
        NameCol.ToolTipText = "Nazwisko i imiê ucznia"
        NameCol.ReadOnly = True
        NameCol.Frozen = True
        NameCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(NameCol)

        ObjectCol.Name = "Przedmiot"
        ObjectCol.HeaderText = "Przedmiot"
        ObjectCol.Width = 200
        ObjectCol.CellTemplate = New DataGridViewTextBoxCell()
        ObjectCol.ToolTipText = "Nazwa przedmiotu"
        ObjectCol.ReadOnly = True
        ObjectCol.Frozen = True
        ObjectCol.SortMode = DataGridViewColumnSortMode.Programmatic
        .Columns.Add(ObjectCol)
        
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
          .HeaderText = "Ocena"
          .Width = 150 '65
          .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
          .CellTemplate = P
          .Name = "Poprawka"
          .CellTemplate.Style.ForeColor = Color.Navy
          .CellTemplate.Style.Font = New Font(dgvPoprawka.Font, FontStyle.Bold)
          .ToolTipText = IIf(nudSemestr.Value = 1, "Ocena z egzaminu poprawkowego za I Semestr", "Ocena z egzaminu poprawkowego na koniec roku szkolnego").ToString
          .SortMode = DataGridViewColumnSortMode.NotSortable
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

  Private Function GetGroupMember() As List(Of MakeupMember)
    Dim DBA As New DataBaseAction, Student As MySqlDataReader = Nothing '
    Dim P As New PoprawkaSQL
    Dim Students As List(Of MakeupMember)
    Try
      Students = New List(Of MakeupMember)
      Student = DBA.GetReader(P.SelectStudent(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, CbItem).ID.ToString, If(nudSemestr.Value = 1, "S", "R")))
      While Student.Read
        Students.Add(New MakeupMember With {.No = Student.Item("NrwDzienniku").ToString, .StudentName = Student.Item("Student").ToString, .ObjectName = Student.Item("Alias").ToString, .AllocationID = CInt(Student.Item("IdPrzydzial")), .StaffID = Student.GetInt32("IdObsada"), .Type = Student.GetString("Typ"), .TeacherID = Student.GetInt32("Nauczyciel"), .ScoreID = Student.Item("IdOcena").ToString, .Lock = Student.GetBoolean("Lock"), .ExamDate = Student.Item("Data").ToString, .Owner = Student.GetString("Owner"), .User = Student.GetString("User"), .IP = Student.GetString("ComputerIP"), .Version = Student.GetString("Version")})
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
      InRefresh = True
      Dim Students As List(Of MakeupMember) = GetGroupMember()
      With dgvName
        .Rows.Clear()
        For Each M As MakeupMember In Students
          .Rows.Add(M.No, M.StudentName, M.ObjectName)
          .Rows(.Rows.Count - 1).ReadOnly = If(M.Lock, True, False)
          .Rows(.Rows.Count - 1).Tag = M
          If M.ScoreID.Length > 0 Then
            .Rows(.Rows.Count - 1).Cells("Poprawka").Value = CType(M.ScoreID, Integer)
            .Rows(.Rows.Count - 1).Cells("Poprawka").ToolTipText = M.ExamDate
          End If
        Next
        .ClearSelection()
        InRefresh = False
        If .RowCount > 0 Then
          .CurrentCell = .Rows(IntRowIndex).Cells(.Columns("Poprawka").Index)
          .Focus()
          .Enabled = True

        End If
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & "Nr b³êdu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try

  End Sub
  
  Private Sub dgvZestawienieOcen_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPoprawka.CellEnter
    Try
      'If InRefresh Then Exit Sub
      IntRowIndex = e.RowIndex
      IntColumnIndex = e.ColumnIndex
      If e.ColumnIndex = dgvPoprawka.Columns.Count - 1 Then
        Me.dgvPoprawka.Columns(e.ColumnIndex).HeaderCell.Style.Font = New Font(Me.dgvPoprawka.Font, FontStyle.Bold)
        If CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).Lock = False AndAlso CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).ScoreID.Length > 0 AndAlso (GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString OrElse GlobalValues.AppUser.SchoolTeacherID = CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).TeacherID.ToString) Then
          cmdDelete.Enabled = True
        Else
          cmdDelete.Enabled = False
        End If

        Dim User, Owner As String
        User = CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).User 'CType(FoundRow(0).Item("User"), String).ToLower.Trim
        Owner = CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).Owner 'CType(FoundRow(0).Item("Owner"), String).ToLower.Trim
        If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
          lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (W³: ", GlobalValues.Users.Item(Owner).ToString, ")")
        Else
          Me.lblUser.Text = User & " (W³: " & Owner & ")"
        End If
        Me.lblIP.Text = CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).IP 'FoundRow(0).Item("ComputerIP").ToString
        Me.lblData.Text = CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).Version  'FoundRow(0).Item("Version").ToString
      Else
        cmdDelete.Enabled = False
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub dgvZestawienieOcen_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPoprawka.CellLeave
    If InRefresh Then Exit Sub
    'cmdDelete.Enabled = False
    Me.dgvPoprawka.Columns(e.ColumnIndex).HeaderCell.Style.Font = Me.dgvPoprawka.Font
    Me.lblUser.Text = ""
    Me.lblIP.Text = ""
    Me.lblData.Text = ""
  End Sub
  Private Sub dgvZestawienieOcen_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPoprawka.RowEnter
    If InRefresh Then Exit Sub
    For i As Integer = 0 To dgvPoprawka.Columns.Count - 1
      dgvPoprawka.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.White
      dgvPoprawka.Rows(e.RowIndex).Cells(i).Style.BackColor = Color.Orange
      dgvPoprawka.Rows(e.RowIndex).Cells(i).Style.Font = New Font(dgvPoprawka.Font, FontStyle.Bold)
    Next
  End Sub

  Private Sub dgvZestawienieOcen_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPoprawka.RowLeave
    Try
      If InRefresh Then Exit Sub
      With dgvPoprawka
        For i As Integer = 0 To .Columns.Count - 1
          .Rows(e.RowIndex).Cells(i).Style.BackColor = .Columns(i).CellTemplate.Style.BackColor 'Me.dgvZachowanie.Rows(e.RowIndex).Cells(i).Style.BackColor
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = .Columns(i).CellTemplate.Style.ForeColor
          .Rows(e.RowIndex).Cells(i).Style.Font = .Columns(i).CellTemplate.Style.Font '.Font
        Next
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPoprawka.CellValueChanged
    Try
      If e.RowIndex < 0 Or InRefresh Then Exit Sub
      Dim DBA As New DataBaseAction, P As New PoprawkaSQL, Data As DateTime
      Data = New DateTime(dtData.Value.Year, dtData.Value.Month, dtData.Value.Day, Now.Hour, Now.Minute, Now.Second)

      Dim cmd As MySqlCommand = DBA.CreateCommand(P.UpdateResult)
      cmd.Parameters.AddWithValue("?IdOcena", Me.dgvPoprawka.CurrentCell.Value.ToString)
      cmd.Parameters.AddWithValue("?Data", Data)
      cmd.Parameters.AddWithValue("?IdPrzydzial", CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).AllocationID)
      cmd.Parameters.AddWithValue("?IdObsada", CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).StaffID)
      cmd.Parameters.AddWithValue("?Typ", CType(dgvPoprawka.Rows(e.RowIndex).Tag, MakeupMember).Type)

      cmd.ExecuteNonQuery()

      SetRows(dgvPoprawka)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub DeleteScore()
    If MessageBox.Show("Czy na pewno chcesz usun¹æ zaznaczone pozycje?", "Belfer .NET", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, P As New PoprawkaSQL, MyTran As MySqlTransaction
      MyTran = GlobalValues.gblConn.BeginTransaction
      Try
        For Each Cell As DataGridViewCell In Me.dgvPoprawka.SelectedCells
          'If Cell.Tag IsNot Nothing Then
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.DeleteResult)
          cmd.Transaction = MyTran
          cmd.Parameters.AddWithValue("?IdPrzydzial", CType(dgvPoprawka.Rows(Cell.RowIndex).Tag, MakeupMember).AllocationID)
          cmd.Parameters.AddWithValue("?IdObsada", CType(dgvPoprawka.Rows(Cell.RowIndex).Tag, MakeupMember).StaffID)
          cmd.Parameters.AddWithValue("?Typ", CType(dgvPoprawka.Rows(Cell.RowIndex).Tag, MakeupMember).Type)
          cmd.ExecuteNonQuery()
          'End If
          'If Cell.FormattedValue.ToString.Length > 0 Then DBA.ApplyChanges(CS.DeleteString("wyniki", "ID", Cell.Tag.ToString))
        Next
        MyTran.Commit()
        cmdDelete.Enabled = False
        SetRows(dgvPoprawka)
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
      If Me.nudSemestr.Created Then 'AndAlso Me.InRefresh = False Then
        Dim CurrentDate As Date
        CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
        Dim CH As New CalcHelper
        dtData.Tag = 1
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
            dtData.MinDate = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
            dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
            dtData.Value = If(CurrentDate < dtData.MinDate, dtData.MinDate, CurrentDate)
          End If
          'dtData.Value = CurrentDate
        End If
        dtData.Tag = 0
        cmdDelete.Enabled = False
        'IntRowIndex = 0
        If cbKlasa.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)
        'dtData_ValueChanged(Nothing, Nothing)
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  'Private Sub dtData_ValueChanged(sender As Object, e As EventArgs) Handles dtData.ValueChanged
  '  If dtData.Tag.ToString = "0" AndAlso cbKlasa.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)
  'End Sub

  Private Sub dgvZestawienieOcen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvPoprawka.KeyDown
    Try
      If dgvPoprawka.CurrentCell.ColumnIndex < dgvPoprawka.Columns("Poprawka").Index Then Exit Sub

      If CType(dgvPoprawka.Rows(dgvPoprawka.CurrentCell.RowIndex).Tag, MakeupMember).ScoreID.Length > 0 AndAlso (e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back) Then
        If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString OrElse GlobalValues.AppUser.SchoolTeacherID = CType(dgvPoprawka.Rows(dgvPoprawka.CurrentCell.RowIndex).Tag, MakeupMember).TeacherID.ToString Then
          Me.DeleteScore()
        Else
          MessageBox.Show("Nie mo¿esz usun¹æ tej wartoœci, poniewa¿ nie posiadasz wystarczaj¹cych uprawnieñ.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub


  Private Sub dgvZestawienieOcen_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPoprawka.ColumnHeaderMouseClick

    With dgvPoprawka
      .CurrentCell = .Rows(IntRowIndex).Cells(e.ColumnIndex)
    End With

  End Sub

  Private Sub dgvZestawienieOcen_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvPoprawka.DataError
    MessageBox.Show(e.Exception.Message)
  End Sub

  Private Sub DelResultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelResultToolStripMenuItem.Click
    DeleteScore()
  End Sub


  Private Sub dgvZestawienieOcen_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPoprawka.CellMouseUp
    Try
      With dgvPoprawka
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = .Columns("Poprawka").Index Then
          .CurrentCell = .Rows(e.RowIndex).Cells(e.ColumnIndex)
          If e.Button = Windows.Forms.MouseButtons.Right AndAlso .Rows(e.RowIndex).Cells(e.ColumnIndex).Selected AndAlso CType(.Rows(e.RowIndex).Tag, MakeupMember).ScoreID.Length > 0 Then
            If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString OrElse GlobalValues.AppUser.SchoolTeacherID = CType(.Rows(e.RowIndex).Tag, MakeupMember).TeacherID.ToString Then
              DelResultToolStripMenuItem.Enabled = True
            Else
              DelResultToolStripMenuItem.Enabled = False
            End If
          Else
            DelResultToolStripMenuItem.Enabled = False
          End If
        Else
          .CurrentCell = .Rows(IntRowIndex).Cells(e.ColumnIndex)
          DelResultToolStripMenuItem.Enabled = False

        End If
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub



  Private Sub dgvZestawienieOcen_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvPoprawka.EditingControlShowing
    Try
      e.CellStyle.BackColor = Color.White
      e.CellStyle.ForeColor = Color.Black
      If dgvPoprawka.CurrentCell.ColumnIndex = dgvPoprawka.Columns("Poprawka").Index Then
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

  Private Sub dgvZestawienieOcen_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
    Try
      With dgvPoprawka
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
      With dgvPoprawka
        If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then Exit Sub
        Dim rtHeader As Rectangle = .DisplayRectangle
        rtHeader.Height = CType(.ColumnHeadersHeight / 2, Integer)
        .Invalidate(rtHeader)
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  

  Private Sub frmZachowanie_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    Me.Height += 1
    Me.Height -= 1
  End Sub

  Private Sub dgvZachowanie_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvPoprawka.RowPostPaint
    If (e.RowIndex + 1) Mod 10 = 0 Then
      e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 2, e.RowBounds.Right, e.RowBounds.Bottom - 2)
    End If
  End Sub

  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    DeleteScore()
  End Sub
End Class

Public Class MakeupMember 'Lista uczniów danej klasy
  Public Property No As String 'nr ucznia w dzienniku
  Public Property StudentName As String 'nazwisko i imiê ucznia
  Public Property ObjectName As String 'Nazwa przedmiotu z tabeli przedmiot
  Public Property AllocationID As Integer 'ID przydzia³u
  Public Property StaffID As Integer 'ID obsada z tabeli obsada lub IdObsada z tabeli poprawka
  Public Property Type As String 'Typ poprawki - roczna czy semestralna (R, S)
  Public Property TeacherID As Integer 'ID nauczyciela z tabeli szkola_nauczyciel
  Public Property Lock As Boolean 'informacja o blokadzie rekordu
  Public Property ScoreID As String 'ID oceny w tabeli poprawka
  Public Property ExamDate As String  'Data wystawienia oceny poprawkowej
  Public Property Owner As String
  Public Property User As String
  Public Property IP As String
  Public Property Version As String
End Class