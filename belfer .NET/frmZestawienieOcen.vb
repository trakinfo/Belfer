Imports belfer.NET.GlobalValues
Imports System.Drawing.Printing
Public Class frmZestawienieOcen
  Public Event NewRow()
  Private dtWynik, dtPrzedmiot As DataTable ', TotalAvg As String
  Private Offset(1), PageNumber, AggregateStartRowIndex, AggregateStartColIndex As Integer
  Private PH As PrintHelper, IsPreview, InRefresh As Boolean

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.TableSetToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.TableSetToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmZachowanie_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    Me.Height += 1
    Me.Height -= 1
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, 2, Panel1.Width)
  End Sub
  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    DataGridConfig(dgvZestawienieOcen)

    If Date.Today.Month < 2 OrElse Date.Today.Month > 8 Then
      rbSemestr.Checked = True
    Else
      rbSchoolYear.Checked = True
    End If
    ApplyNewConfig()
  End Sub
  Private Sub DataGridConfig(ByVal dgvName As DataGridView)
    With dgvName
      .SelectionMode = DataGridViewSelectionMode.CellSelect
      .AutoGenerateColumns = False

      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 80
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
      
    End With
  End Sub
  
  Private Sub ApplyNewConfig()
    Try
      EnableButton(False)
      dgvZestawienieOcen.Columns.Clear()
      Dim CH As New CalcHelper
      rbSemestr.Tag = New SchoolPeriod With {.Typ = "S", .DataKoncowa = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1)}
      rbSchoolYear.Tag = New SchoolPeriod With {.Typ = "R", .DataKoncowa = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)}
      rbAll.Tag = rbSchoolYear.Tag 'CH.EndDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString
      FillKlasa()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub FetchResult(Typ As String)
    Dim DBA As New DataBaseAction, Z As New ZestawienieOcenSQL, CH As New CalcHelper, StartDate, EndDate As Date, K As New KlasyfikacjaSQL
    StartDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    EndDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
    dtWynik = DBA.GetDataTable(Z.SelectWynik(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, StartDate, EndDate, Typ))
    'TotalAvg = DBA.GetSingleValue(K.CountAvgByKlasa(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Typ)).ToString

  End Sub
  Private Function FetchStudent() As DataSet
    Dim DBA As New DataBaseAction, W As New WynikiSQL, T As New TematSQL
    Dim SelectString As String = ""

    SelectString = W.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString) & T.SelectIndividualStaff(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)

    Return DBA.GetDataSet(SelectString)
  End Function
  Private Sub FillKlasa()
    Dim Z As New ZestawienieOcenSQL
    LoadClassItems(cbKlasa, Z.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
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
        cb.Items.Add(New SchoolClassComboItem(R.GetInt32("IdKlasa"), R.GetString("Nazwa_Klasy"), False, R.GetString("Kod_Klasy")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub EnableButton(Value As Boolean)
    For Each B As Control In Controls
      If TypeOf B Is RadioButton Then B.Enabled = Value
    Next
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    EnableButton(True)
    Dim B As RadioButton
    B = Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    RefreshData(CType(B.Tag, SchoolPeriod).Typ)
  End Sub
  Private Sub RefreshData(Typ As String)
    Try
      InRefresh = True
      Cursor = Cursors.WaitCursor
      FetchData(Typ)
      SetColumns(dgvZestawienieOcen)
      SetRows(dgvZestawienieOcen)
      Me.GetData()
      Me.dgvZestawienieOcen.Focus()
      Me.dgvZestawienieOcen.Enabled = True
      InRefresh = False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Sub FetchData(Typ As String)
    Dim Z As New ZestawienieOcenSQL, B As RadioButton, DBA As New DataBaseAction
    B = Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    dtPrzedmiot = DBA.GetDataTable(Z.SelectPrzedmiot(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, CType(B.Tag, SchoolPeriod).DataKoncowa))
    FetchResult(Typ)
    If B.Name = "rbAll" Then
      Dim OH As New OptionHolder
      Dim MinPion As Byte = OH.GetMinPion
      Dim Pion As Byte = CType(CType(cbKlasa.SelectedItem, SchoolClassComboItem).KodKlasy.Substring(0, 1), Byte)
      For i As Integer = Pion - 1 To MinPion Step -1
        Dim Przedmiot As String = ""
        For Each R As DataRow In dtPrzedmiot.DefaultView.ToTable(True, "ID").Rows
          Przedmiot += R.Item("ID").ToString + ","
        Next
        If Przedmiot.Length > 0 Then
          Dim dtTEMP As DataTable, j As Integer = Pion - i, PreviousSchoolYear, PreviousClass As String, StartYear As Integer
          StartYear = CType(My.Settings.SchoolYear.Substring(0, 4), Integer) - j
          PreviousSchoolYear = String.Concat(StartYear, "/", StartYear + 1)
          PreviousClass = String.Concat(Pion - j, CType(cbKlasa.SelectedItem, SchoolClassComboItem).KodKlasy.Substring(1, 1))
          dtTEMP = DBA.GetDataTable(Z.SelectPrzedmiot(My.Settings.IdSchool, PreviousSchoolYear, PreviousClass, CType(B.Tag, SchoolPeriod).DataKoncowa.AddYears(-j), Przedmiot.TrimEnd(",".ToCharArray)))
          'Else
          '  dtTEMP = DBA.GetDataTable(Z.SelectPrzedmiot(My.Settings.IdSchool, PreviousSchoolYear, PreviousClass, CType(B.Tag, SchoolPeriod).DataKoncowa.AddYears(-j), "''"))
          For Each R As DataRow In dtTEMP.Rows
            dtPrzedmiot.ImportRow(R)
          Next
          Przedmiot = ""
          For Each R As DataRow In dtTEMP.DefaultView.ToTable(True, "ID").Rows
            Przedmiot += R.Item("ID").ToString + ","
          Next
          Dim StartDate, EndDate As Date, CH As New CalcHelper
          StartDate = CH.StartDateOfSchoolYear(PreviousSchoolYear)
          EndDate = CH.EndDateOfSchoolYear(PreviousSchoolYear)
          If Przedmiot.Length > 0 Then
            dtTEMP = DBA.GetDataTable(Z.SelectWynik(My.Settings.IdSchool, PreviousSchoolYear, PreviousClass, StartDate, EndDate, Typ, Przedmiot.TrimEnd(",".ToCharArray)))
            'Else
            '  dtTEMP = DBA.GetDataTable(Z.SelectWynik(My.Settings.IdSchool, PreviousSchoolYear, PreviousClass, StartDate, EndDate, Typ, "''"))
          End If
          For Each R As DataRow In dtTEMP.Rows
            dtWynik.ImportRow(R)
          Next
        End If
      Next
    End If
  End Sub
  Private Overloads Sub SetColumns(ByVal dgvName As DataGridView)
    ', R As MySqlDataReader = Nothing
    Try
      Dim Header As New List(Of TableCell) From {
                                            New TableCell With {.Label = "Nr", .Name = "Nr", .ToolTip = "Nr ucznia w dzienniku", .Size = 30},
                                            New TableCell With {.Label = "Nazwisko i imię", .Name = "Student", .ToolTip = "Nazwisko i imię ucznia", .Size = 180}
                                            }
      With dgvName
        .Columns.Clear()
        AggregateStartColIndex = 0
        For i As Integer = 0 To 1
          Dim NewCol As New DataGridViewColumn
          With NewCol
            .Name = Header(i).Name
            .HeaderText = Header(i).Label
            .Width = Header(i).Size
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate = New DataGridViewTextBoxCell()
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.BackColor = Color.Ivory
            .ToolTipText = Header(i).ToolTip
            .Tag = New ColumnProperty With {.Blokada = True, .Typ = "RO"}
            .ReadOnly = True
            .Frozen = True
            .SortMode = DataGridViewColumnSortMode.Programmatic
          End With
          .Columns.Add(NewCol)
          AggregateStartColIndex += 1
        Next

        For Each P As DataRow In dtPrzedmiot.Select("Typ='Z'")
          Dim ObjectColumn As New DataGridViewTextBoxColumn
          With ObjectColumn
            .Tag = P.Item("ID").ToString
            .HeaderText = P.Item("Przedmiot").ToString
            .Name = .Tag.ToString
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate = New DataGridViewTextBoxCell
            .CellTemplate.Style.ForeColor = Color.BlueViolet
            .DefaultCellStyle.ForeColor = .CellTemplate.Style.ForeColor
            .DefaultCellStyle.BackColor = Color.WhiteSmoke
            .ToolTipText = P.Item("Nazwa").ToString
            .ReadOnly = True 'If(CType(.Tag, ColumnProperty).Blokada, True, False)
            .SortMode = DataGridViewColumnSortMode.NotSortable
          End With
          .Columns.Add(ObjectColumn)
          AggregateStartColIndex += 1
        Next

        For Each P As DataRow In dtPrzedmiot.Select("Typ='P'")
          Dim ObjectColumn As New DataGridViewTextBoxColumn
          With ObjectColumn
            .Tag = P.Item("ID").ToString
            .HeaderText = P.Item("Przedmiot").ToString
            .Name = .Tag.ToString
            .Width = 25 '65
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .CellTemplate = New DataGridViewTextBoxCell
            .CellTemplate.Style.ForeColor = Color.Black
            .DefaultCellStyle.ForeColor = .CellTemplate.Style.ForeColor
            .ToolTipText = P.Item("Nazwa").ToString
            .ReadOnly = True 'If(CType(.Tag, ColumnProperty).Blokada, True, False)
            .SortMode = DataGridViewColumnSortMode.NotSortable
          End With
          .Columns.Add(ObjectColumn)
          AggregateStartColIndex += 1
        Next
        If chkFunkcja.Checked Then
          Dim AggregateHeader As New List(Of TableCell) From {
                                         New TableCell With {.Label = "Średnia", .Name = "Avg", .ToolTip = "Średnia arytmetyczna ocen w roku szkolnym", .Size = 40},
                                         New TableCell With {.Label = "Mediana", .Name = "Mediana", .ToolTip = "Wartość środkowa", .Size = 40},
                                         New TableCell With {.Label = "Maksimum", .Name = "Max", .ToolTip = "Najlepsza ocena", .Size = 40},
                                         New TableCell With {.Label = "Minimum", .Name = "Min", .ToolTip = "Najgorsza ocena", .Size = 40}
                                         }
          For Each TC As TableCell In AggregateHeader
            If CType(gbFunkcja.Controls(String.Concat("chk", TC.Name)), CheckBox).Checked Then
              Dim NewCol As New DataGridViewColumn
              With NewCol
                .Name = TC.Name
                .HeaderText = TC.Label
                .Width = TC.Size
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .CellTemplate = New DataGridViewTextBoxCell()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.ForeColor = Color.Navy
                .ToolTipText = TC.ToolTip
                '.Tag = New ColumnProperty With {.Blokada = True, .Typ = "RO"}
                .ReadOnly = True
                .Frozen = False
                .SortMode = DataGridViewColumnSortMode.Programmatic
              End With
              .Columns.Add(NewCol)
            End If
          Next
        End If
        If chkAggregate.Checked Then
          Dim Ocena As New Hashtable From {{6, "cel"}, {5, "bdb"}, {4, "db"}, {3, "dst"}, {2, "dps"}, {1, "ndst"}, {0, "NKL"}}
          For Each O As DictionaryEntry In Ocena
            Dim NewCol As New DataGridViewColumn
            With NewCol
              .Name = "w" + O.Key.ToString
              .HeaderText = O.Value.ToString
              .Width = 30
              .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
              .CellTemplate = New DataGridViewTextBoxCell()
              .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
              .DefaultCellStyle.BackColor = Color.Azure
              .DefaultCellStyle.ForeColor = Color.MidnightBlue
              '.ToolTipText = TC.ToolTip
              .Tag = O.Key 'New ColumnProperty With {.Blokada = True, .Typ = "RO"}
              .ReadOnly = True
              .Frozen = False
              .SortMode = DataGridViewColumnSortMode.Programmatic
            End With
            .Columns.Add(NewCol)
          Next

        End If
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & "Nr błędu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try

  End Sub

  Private Function GetIndividualCourse(IdPrzydzial As String, Data As Date, dtIndividualStaff As DataTable) As IndividualCourse
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
      Dim DS As DataSet = FetchStudent()
      DS.Tables(0).TableName = "Student"
      DS.Tables(1).TableName = "NI"
      Dim Students = New List(Of GroupMember)

      For Each Student As DataRow In DS.Tables("Student").Rows
        Dim NI As IndividualCourse = GetIndividualCourse(Student.Item("IdPrzydzial").ToString, Date.Today, DS.Tables("NI"))
        Students.Add(New GroupMember With {.Allocation = New StudentAllocation With {.ID = CInt(Student.Item("ID")), .AllocationID = CInt(Student.Item("IdPrzydzial")), .Status = CType(Student.Item("StatusAktywacji"), Boolean), .DataAktywacji = If(IsDBNull(Student.Item("DataAktywacji")), Nothing, CType(Student.Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(Student.Item("DataDeaktywacji")), Nothing, CType(Student.Item("DataDeaktywacji"), Date)), .MasterLocation = CBool(Student.Item("MasterRecord")), .NauczanieIndywidualne = NI}, .No = Student.Item("NrwDzienniku").ToString, .Name = Student.Item("Student").ToString})
      Next

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
                For j As Integer = 0 To .Rows(.Rows.Count - 1).Cells.Count - 1
                  .Rows(.Rows.Count - 1).Cells(j).Style.BackColor = Color.LightGray
                Next
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
        If chkAggregate.Checked Then
          AggregateStartRowIndex = .Rows(.Rows.Count - 1).Index + 1
          Dim Ocena As New Hashtable From {{6, "wz / cel"}, {5, "bdb"}, {4, "db"}, {3, "pop / dst"}, {2, "ndp / dps"}, {1, "ng / ndst"}, {0, "NKL"}}
          For Each O As DictionaryEntry In Ocena
            .Rows.Add("", O.Value)
            .Rows(.Rows.Count - 1).Tag = O.Key
            .Rows(.Rows.Count - 1).Cells(1).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            For j As Integer = 0 To AggregateStartColIndex - 1
              .Rows(.Rows.Count - 1).Cells(j).Style.BackColor = Color.Azure
            Next
            For j As Integer = AggregateStartColIndex To .Rows(.Rows.Count - 1).Cells.Count - 1
              .Rows(.Rows.Count - 1).Cells(j).Style.BackColor = Color.Beige
              .Rows(.Rows.Count - 1).Cells(j).Style.ForeColor = Color.Firebrick
              .Rows(.Rows.Count - 1).Cells(j).Style.Font = New Font(.Font, FontStyle.Bold)
            Next
          Next

        End If
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & "Nr błędu: " & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub GetData()
    Try
      ClearDetails()
      With dgvZestawienieOcen
        For Each Row As DataGridViewRow In .Rows
          If Row.Index < AggregateStartRowIndex Then
            For Each DR As DataRow In dtWynik.Select("IdUczen=" & CType(Row.Tag, StudentAllocation).ID)
              Row.Cells(DR.Item("IdPrzedmiot").ToString).Value = DR.Item("Wynik")
              Row.Cells(DR.Item("IdPrzedmiot").ToString).Tag = DR.Item("Wynik")
              Row.Cells(DR.Item("IdPrzedmiot").ToString).ToolTipText = "Data wystawienia: " + CType(DR.Item("Data"), Date).ToString & vbNewLine & "Wystawił(a): " + Users.Item(DR.Item("Owner").ToString.ToLower.Trim).ToString & vbNewLine & "Zmodyfikował(a): " + Users.Item(DR.Item("User").ToString.ToLower.Trim).ToString
            Next
            If chkFunkcja.Checked Then
              If dtWynik.Select("IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Waga>0 AND GetToAverage=1").Length > 0 Then
                If chkAvg.Checked Then
                  Dim Avg As Double = CType(dtWynik.Compute("AVG(Waga)", "IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Waga>0 AND GetToAverage=1"), Double)
                  Row.Cells("Avg").Value = Math.Round(Avg, 2).ToString("0.00")
                  Row.Cells("Avg").ToolTipText = Avg.ToString
                End If
                If chkMediana.Checked Then
                  Row.Cells("Mediana").Value = Mediana((From R In dtWynik.Select("IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Waga>0 AND GetToAverage=1") Select colB = CType(R("Waga"), Double)))
                End If
                If chkMax.Checked Then
                  Dim Max As Double = CType(dtWynik.Compute("Max(Waga)", "IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Waga>0 AND GetToAverage=1"), Double)
                  Row.Cells("Max").Value = Max 'Math.Round(Avg, 2).ToString("0.00")
                End If
                If chkMin.Checked Then
                  Dim Min As Double = CType(dtWynik.Compute("Min(Waga)", "IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Waga>0 AND GetToAverage=1"), Double)
                  Row.Cells("Min").Value = Min 'Math.Round(Avg, 2).ToString("0.00")
                End If
              End If
            End If
            If chkAggregate.Checked Then
              For i As Integer = 6 To 0 Step -1
                Row.Cells("w" + i.ToString).Value = dtWynik.Compute("Count(Waga)", "IdUczen=" & CType(Row.Tag, StudentAllocation).ID & " AND Waga=" & i & " AND Typ<>'Z'")
              Next
            End If
          Else
            If chkAggregate.Checked Then
              For i As Integer = AggregateStartRowIndex To .Rows.Count - 1
                For j As Integer = 2 To AggregateStartColIndex - 1
                  Row.Cells(j).Value = dtWynik.Compute("Count(Waga)", "IdPrzedmiot=" & .Columns(j).Tag.ToString & " AND Waga=" & Row.Tag.ToString)
                Next
              Next
            End If
          End If
        Next
        If chkAggregate.Checked Then
          If chkFunkcja.Checked Then 'AndAlso dtWynik.Select("Waga>0 AND GetToAverage=1").Length > 0 Then
            If dtWynik.Select("Waga>0 AND GetToAverage=1").Length > 0 Then
              Dim CountStartColIndex As Integer = AggregateStartColIndex
              If chkAvg.Checked Then
                'Dim Avg As Double = CType(dtWynik.Compute("AVG(Waga)", "Waga>0 AND GetToAverage=1"), Double)
                .Rows(AggregateStartRowIndex).Cells("Avg").Value = Math.Round(CType(dtWynik.Compute("Avg(Waga)", "Waga>0 AND GetToAverage=1"), Double), 2).ToString("0.00") 'TotalAvg.ToString
                .Rows(AggregateStartRowIndex).Cells("Avg").ToolTipText = CType(dtWynik.Compute("Avg(Waga)", "Waga>0 AND GetToAverage=1"), Double).ToString
                '.Rows(AggregateStartRowIndex).Cells("Avg").Value = Math.Round(Avg, 2).ToString("0.00")
                '.Rows(AggregateStartRowIndex).Cells("Avg").ToolTipText = Avg.ToString
                CountStartColIndex += 1
              End If
              If chkMediana.Checked Then
                .Rows(AggregateStartRowIndex).Cells("Mediana").Value = Mediana((From R In dtWynik.Select("Waga>0 AND GetToAverage=1") Select colB = CType(R("Waga"), Double)))
                CountStartColIndex += 1
              End If
              If chkMax.Checked Then
                Dim Max As Double = CType(dtWynik.Compute("Max(Waga)", "Waga>0 AND GetToAverage=1"), Double)
                .Rows(AggregateStartRowIndex).Cells("Max").Value = Max 'Math.Round(Avg, 2).ToString("0.00")
                CountStartColIndex += 1
              End If
              If chkMin.Checked Then
                Dim Min As Double = CType(dtWynik.Compute("Min(Waga)", "Waga>0 AND GetToAverage=1"), Double)
                .Rows(AggregateStartRowIndex).Cells("Min").Value = Min 'Math.Round(Avg, 2).ToString("0.00")
                CountStartColIndex += 1
              End If
              For j As Integer = CountStartColIndex To .Columns.Count - 1
                .Rows(AggregateStartRowIndex).Cells(j).Value = dtWynik.Compute("Count(Waga)", "Waga=" & .Columns(j).Tag.ToString & " AND Typ<>'Z'")
              Next
            End If
          Else
            For j As Integer = AggregateStartColIndex To .Columns.Count - 1
              .Rows(AggregateStartRowIndex).Cells(j).Value = dtWynik.Compute("Count(Waga)", "Waga=" & .Columns(j).Tag.ToString & " AND Typ<>'Z'")
            Next
          End If
          End If
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try
  End Sub
  Private Sub ClearDetails()
    Me.lblUser.Text = ""
    Me.lblIP.Text = ""
    Me.lblData.Text = ""
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub dgvZestawienieOcen_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvZestawienieOcen.CellEnter
    Try
      If InRefresh OrElse e.RowIndex >= AggregateStartRowIndex Then Exit Sub
      If Me.dgvZestawienieOcen.CurrentCell.Tag IsNot Nothing Then
        Dim FoundRow() As DataRow
        FoundRow = dtWynik.Select("IdUczen='" & CType(dgvZestawienieOcen.Rows(e.RowIndex).Tag, StudentAllocation).ID & "' AND IdPrzedmiot='" & dgvZestawienieOcen.Columns(e.ColumnIndex).Tag.ToString & "'")
        Dim User, Owner As String
        User = CType(FoundRow(0).Item("User"), String).ToLower.Trim
        Owner = CType(FoundRow(0).Item("Owner"), String).ToLower.Trim
        If Users.ContainsKey(User) AndAlso Users.ContainsKey(Owner) Then
          lblUser.Text = String.Concat(Users.Item(User).ToString, " (Wł: ", Users.Item(Owner).ToString, ")")
        Else
          Me.lblUser.Text = User & " (Wł: " & Owner & ")"
        End If
        Me.lblIP.Text = FoundRow(0).Item("ComputerIP").ToString
        Me.lblData.Text = FoundRow(0).Item("Version").ToString
        'End If
      Else
        Me.lblUser.Text = ""
        Me.lblIP.Text = ""
        Me.lblData.Text = ""
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Paint(sender As Object, e As PaintEventArgs) Handles dgvZestawienieOcen.Paint
    'With dgvZestawienieOcen
    '  Dim y0 As Integer = .GetColumnDisplayRectangle(1, True).Y

    '  Dim y1 As Integer = .Rows.GetRowsHeight(DataGridViewElementStates.None) '.GetColumnDisplayRectangle(1, True).Height
    '  Dim x As Integer = .GetCellDisplayRectangle(1, -1, False).X + .GetCellDisplayRectangle(1, -1, False).Width - 1
    '  e.Graphics.DrawLine(New Pen(Brushes.Black, 3), x, y0, x, y1)

    'End With
  End Sub
  Private Sub dgvZestawienieOcen_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvZestawienieOcen.CellPainting
    If (e.RowIndex = -1 AndAlso e.ColumnIndex > 1) Then
      Dim HeaderTextHeight As Single = e.Graphics.MeasureString(e.Value.ToString.ToLower, e.CellStyle.Font).Height
      Dim HeaderTextWidth As Single = e.Graphics.MeasureString(e.Value.ToString, e.CellStyle.Font).Width
      e.PaintBackground(e.ClipBounds, True)
      If HeaderTextWidth > dgvZestawienieOcen.ColumnHeadersHeight Then
        dgvZestawienieOcen.ColumnHeadersHeight = CType(HeaderTextWidth, Integer)
      End If
      Dim r As Rectangle = dgvZestawienieOcen.GetCellDisplayRectangle(e.ColumnIndex, -1, True)
      e.Graphics.TranslateTransform(CType(r.Width / 2, Single), dgvZestawienieOcen.ColumnHeadersHeight)
      e.Graphics.RotateTransform(270.0F)
      e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, New PointF(r.Y, r.X - HeaderTextHeight / 2))
      e.Graphics.ResetTransform()
      e.Handled = True
    End If

  End Sub

  Private Sub rbSemestr_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemestr.CheckedChanged, rbAll.CheckedChanged, rbSchoolYear.CheckedChanged
    If CType(sender, RadioButton).Checked = False OrElse CType(sender, RadioButton).Tag Is Nothing Then Exit Sub
    RefreshData(CType(CType(sender, RadioButton).Tag, SchoolPeriod).Typ)
  End Sub
  Private Sub chkAggregate_CheckedChanged(sender As Object, e As EventArgs) Handles chkAggregate.CheckedChanged
    If Not Created Then Exit Sub
    Dim B As RadioButton
    B = Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    RefreshData(CType(B.Tag, SchoolPeriod).Typ)
  End Sub
  Private Sub chkFunkcja_CheckedChanged(sender As Object, e As EventArgs) Handles chkFunkcja.CheckedChanged
    If Not Created Then Exit Sub

    Dim B As RadioButton
    B = Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    RefreshData(CType(B.Tag, SchoolPeriod).Typ)

    gbFunkcja.Enabled = chkFunkcja.Checked
  End Sub
  Private Sub chkAvg_CheckedChanged(sender As Object, e As EventArgs) Handles chkAvg.CheckedChanged, chkMax.CheckedChanged, chkMediana.CheckedChanged, chkMin.CheckedChanged
    If Not Created Then Exit Sub
    Dim B As RadioButton
    B = Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    RefreshData(CType(B.Tag, SchoolPeriod).Typ)
  End Sub
  Private Sub dgvZestawienieOcen_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvZestawienieOcen.RowPostPaint
    If ((e.RowIndex + 1) Mod 10 = 0 AndAlso e.RowIndex < AggregateStartRowIndex) Or (chkAggregate.Checked And e.RowIndex = AggregateStartRowIndex - 1) Then
      Dim RowWidth As Integer = dgvZestawienieOcen.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
      e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Bottom - 1, RowWidth, e.RowBounds.Bottom - 1)
    End If
   
  End Sub
  Private Sub dgvAbsencja_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvZestawienieOcen.RowEnter
    If InRefresh OrElse e.RowIndex >= AggregateStartRowIndex Then Exit Sub
    With dgvZestawienieOcen
      For i As Integer = 0 To .Columns.Count - 1
        .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.White
        .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.Orange
        .Rows(e.RowIndex).Cells(i).Style.Font = New Font(.Font, FontStyle.Bold)
      Next
      If CType(.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
        .Rows(e.RowIndex).Cells(0).Style.Font = New Font(.Font, FontStyle.Bold Or FontStyle.Strikeout)
        .Rows(e.RowIndex).Cells(1).Style.Font = New Font(.Font, FontStyle.Bold Or FontStyle.Strikeout)
      End If
    End With
  End Sub

  Private Sub dgvAbsencja_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvZestawienieOcen.RowLeave
    Try
      With dgvZestawienieOcen
        If e.RowIndex >= AggregateStartRowIndex Then Exit Sub
        For i As Integer = 0 To .Columns.Count - 1
          .Rows(e.RowIndex).Cells(i).Style.BackColor = .Columns(i).CellTemplate.Style.BackColor
          .Rows(e.RowIndex).Cells(i).Style.Font = .Columns(i).CellTemplate.Style.Font
        Next
        For i As Integer = 0 To 1
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = .Columns(i).CellTemplate.Style.ForeColor
          .Rows(e.RowIndex).Cells(i).Style.Font = .Font
        Next

        For i As Integer = 2 To .Columns.Count - 1
          .Rows(e.RowIndex).Cells(i).Style.ForeColor = .Columns(i).CellTemplate.Style.ForeColor
        Next

        If CType(.Rows(e.RowIndex).Tag, StudentAllocation).Status = False Then
          .Rows(e.RowIndex).Cells(1).Style.Font = New Font(.Font, FontStyle.Strikeout)
          .Rows(e.RowIndex).Cells(0).Style.Font = New Font(.Font, FontStyle.Strikeout)
          For i As Integer = 0 To .Columns.Count - 1
            .Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.SlateGray
            .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
          Next
        ElseIf CType(.Rows(e.RowIndex).Tag, StudentAllocation).NauczanieIndywidualne IsNot Nothing Then
          For i As Integer = 0 To .Columns.Count - 1
            .Rows(e.RowIndex).Cells(i).Style.BackColor = Color.LightGray
          Next
        End If

      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_Scroll(sender As Object, e As ScrollEventArgs) Handles dgvZestawienieOcen.Scroll
    Try
      If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then Exit Sub
      With dgvZestawienieOcen
        Dim rtHeader As Rectangle = .DisplayRectangle
        rtHeader.Height = .ColumnHeadersHeight
        .Invalidate(rtHeader)
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles dgvZestawienieOcen.ColumnWidthChanged
    Try

      With dgvZestawienieOcen
        Dim rtHeader As Rectangle = .DisplayRectangle
        rtHeader.Height = .ColumnHeadersHeight
        .Invalidate(rtHeader)
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub dgvZestawienieOcen_Resize(sender As Object, e As EventArgs) Handles dgvZestawienieOcen.Resize
    Try
      With dgvZestawienieOcen
        Dim rtHeader As Rectangle = .DisplayRectangle
        rtHeader.Height = .ColumnHeadersHeight
        .Invalidate(rtHeader)
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Function Mediana(ByVal source As IEnumerable(Of Double)) As Double
    If source.Count = 0 Then
      Throw New InvalidOperationException("Nie można obliczyć mediany z pustego zbioru.")
    End If

    Dim sortedSource = From number In source
                       Order By number

    Dim itemIndex = sortedSource.Count \ 2

    If sortedSource.Count Mod 2 = 0 Then
      ' Even number of items in list.
      Return (sortedSource(itemIndex) + sortedSource(itemIndex - 1)) / 2
    Else
      ' Odd number of items in list.
      Return sortedSource(itemIndex)
    End If
  End Function
  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Try
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
      PP.Doc.ReportHeader = New String() {"Tabelaryczne zestawienie wyników nauczania", "Klasa " & cbKlasa.Text, Controls.OfType(Of RadioButton).Where(Function(r) r.Checked = True).FirstOrDefault().Text}
      PP.Width = 1000
      PP.ShowDialog()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

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
    With dgvZestawienieOcen
      Dim dx = x, y0 As Single = y
      For i As Integer = 0 To 1
        PH.DrawText(.Columns(i).HeaderText, New Font(TextFont, FontStyle.Bold), dx, y, .Columns(i).HeaderCell.Size.Width, .Columns(i).HeaderCell.Size.Height, 1, Brushes.Black)
        dx += .Columns(i).HeaderCell.Size.Width
      Next
      For i As Integer = 2 To .Columns.Count - 1
        PH.DrawText(.Columns(i).HeaderText, New Font(TextFont, FontStyle.Bold), dx, y + .Columns(i).HeaderCell.Size.Height, .Columns(i).HeaderCell.Size.Width, .Columns(i).HeaderCell.Size.Height, 0, Brushes.Black, 270)
        dx += .Columns(i).HeaderCell.Size.Width
      Next
      y += .Columns(Offset(1)).HeaderCell.Size.Height
      PH.DrawLine(x, y, dx, y, 2)
      dx = x
      Dim idx As Integer = 10
      Do Until (y + LineHeight * CSng(1.5)) > PrintHeight Or Offset(0) > .Rows.Count - 1
        If Offset(0) <= AggregateStartRowIndex Then
          Do Until Offset(1) > .Columns.Count - 1
            PH.DrawText(.Rows(Offset(0)).Cells(Offset(1)).FormattedValue.ToString, TextFont, dx, y, .Columns(Offset(1)).Width, LineHeight, CType(If(Offset(1) = 1, 0, 1), Byte), Brushes.Black)
            dx += .Columns(Offset(1)).Width
            Offset(1) += 1
          Loop
        Else
          Do Until Offset(1) >= AggregateStartColIndex
            PH.DrawText(.Rows(Offset(0)).Cells(Offset(1)).FormattedValue.ToString, TextFont, dx, y, .Columns(Offset(1)).Width, LineHeight, CType(If(Offset(1) = 1, 0, 1), Byte), Brushes.Black)
            dx += .Columns(Offset(1)).Width
            Offset(1) += 1
          Loop
        End If
        Offset(1) = 0
        Offset(0) += 1
        y += If(Offset(0) = AggregateStartRowIndex, LineHeight * 2, LineHeight)
        If Offset(0) = idx Then
          PH.DrawLine(x, y, dx, y, 2)
          idx += Offset(0)
        End If
        dx = x
      Loop
      Dim ObjectColsWidth As Integer = 0
      For i As Integer = 0 To AggregateStartColIndex - 1
        ObjectColsWidth += .Columns(i).Width
      Next
      PH.DrawRectangle(2, x, y0, ObjectColsWidth, y - (.Rows.Count - AggregateStartRowIndex + 1) * LineHeight - y0)
      '------------------------- Pionowe krawędzie --------------------------------
      PH.DrawLine(x + .Columns("Nr").Width, y0, x + .Columns("Nr").Width, y - (.Rows.Count - AggregateStartRowIndex + 1) * LineHeight, 2)
      PH.DrawLine(x + .Columns("Nr").Width + .Columns("Student").Width, y0, x + .Columns("Nr").Width + .Columns("Student").Width, y - (.Rows.Count - AggregateStartRowIndex + 1) * LineHeight, 2)
      '---------------------------- Podsumowanie
      If chkAggregate.Checked Then
        PH.DrawRectangle(2, x + ObjectColsWidth, y0, .Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - ObjectColsWidth, y - (.Rows.Count - AggregateStartRowIndex + 1) * LineHeight - y0)
        PH.DrawRectangle(2, x, y - (.Rows.Count - 1 - AggregateStartRowIndex + 1) * LineHeight, ObjectColsWidth, (.Rows.Count - 1 - AggregateStartRowIndex + 1) * LineHeight)
        PH.DrawLine(x + .Columns("Nr").Width + .Columns("Student").Width, y - (.Rows.Count - AggregateStartRowIndex) * LineHeight, x + .Columns("Nr").Width + .Columns("Student").Width, y, 2)
        PH.DrawRectangle(2, x + ObjectColsWidth, y - (.Rows.Count - 1 - AggregateStartRowIndex + 1) * LineHeight, .Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - ObjectColsWidth, LineHeight)
      End If
      If chkFunkcja.Checked Then
        Dim FuncFieldWidth As Integer = 0
        For Each chk As CheckBox In gbFunkcja.Controls.OfType(Of CheckBox).Where(Function(rb) rb.Checked = True)
          FuncFieldWidth += .Columns(chk.Name.Substring(3)).Width
        Next
        PH.DrawRectangle(2, x + ObjectColsWidth, y0, FuncFieldWidth, y - (.Rows.Count - AggregateStartRowIndex + 1) * LineHeight - y0)
        If chkAggregate.Checked Then PH.DrawRectangle(2, x + ObjectColsWidth, y - (.Rows.Count - 1 - AggregateStartRowIndex + 1) * LineHeight, FuncFieldWidth, LineHeight)
      End If

      If Offset(0) < .Rows.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
      Else
        Offset(0) = 0
        PageNumber = 0
      End If
    End With

  End Sub


End Class

Public Class SchoolPeriod
  Public Property Typ As String
  Public Property DataKoncowa As Date

End Class

