Imports System.Drawing.Printing
Public Class frmFrekwencja
  Public Event NewRow()
  Private Offset(1), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.FrekwencjaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.FrekwencjaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    DataGridConfig(dgvFrekwencja)
    SetColumns(dgvFrekwencja)
    SetRows(dgvFrekwencja)
    ApplyNewConfig()
  End Sub
  Private Sub DataGridConfig(ByVal dgvName As DataGridView)
    With dgvName
      .SelectionMode = DataGridViewSelectionMode.CellSelect
      .AutoGenerateColumns = False
      '.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 50
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
      ClearCellData()
      FillKlasa()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Function FetchStudent() As DataSet
    Dim DBA As New DataBaseAction, F As New FrekwencjaSQL, DS As DataSet
    Dim SelectString As String = ""
    SelectString = F.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
    SelectString += F.SelectIndividualCourse(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
    SelectString += F.SelectObjectGroup(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
    DS = DBA.GetDataSet(SelectString)
    DS.Tables(0).TableName = "StudentCount"
    DS.Tables(1).TableName = "IndividualCourse"
    DS.Tables(2).TableName = "ObjectGroup"
    Return DS
  End Function
  Private Function FetchAbsence() As DataTable
    Dim DBA As New DataBaseAction, S As New StatystykaSQL, dtAbsence As DataTable
    dtAbsence = DBA.GetDataTable(S.CountAbsence(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
    dtAbsence.TableName = "AbsenceCount"
    Return dtAbsence
  End Function
  Private Function FetchTopicCount() As DataTable
    Dim DBA As New DataBaseAction, F As New FrekwencjaSQL, DT As DataTable
    DT = DBA.GetDataTable(F.CountTopic(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
    DT.TableName = "TopicCount"
    Return DT
  End Function
  Private Sub FillKlasa()
    Dim W As New WynikiSQL
    LoadClassItems(cbKlasa, W.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
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
    RefreshData()
  End Sub
  Private Sub RefreshData()
    'InRefresh = True
    Cursor = Cursors.WaitCursor
    Me.GetData()
    Me.dgvFrekwencja.Focus()
    Me.dgvFrekwencja.Enabled = True
    Cursor = Cursors.Default
    'InRefresh = False
  End Sub

  Private Sub SetColumns(ByVal dgvName As DataGridView)
    Try
      Dim TableCols As New List(Of TableCell) From {New TableCell With {.Name = "MonthName", .Label = "", .Size = 120, .ToolTip = "Nazwa miesiąca"},
                                               New TableCell With {.Name = "StudentCount", .Label = "", .Size = 70, .ToolTip = "Liczba uczniów w klasie"},
                                               New TableCell With {.Name = "TotalCount", .Label = "razem", .Size = 100, .ToolTip = "Łączna liczba godzin lekcyjnych w miesiącu"},
                                               New TableCell With {.Name = "Presence", .Label = "obecności", .Size = 100, .ToolTip = "Obecności uczniów"},
                                               New TableCell With {.Name = "Absence", .Label = "nieobecności", .Size = 100, .ToolTip = "Nieobecności uczniów"},
                                               New TableCell With {.Name = "Percentage", .Label = "% obecności", .Size = 100, .ToolTip = "Procent obecności uczniów"},
                                               New TableCell With {.Name = "DaysCount", .Label = "odbyły się", .Size = 100, .ToolTip = "Liczba dni, w których lekcje się odbyły"},
                                               New TableCell With {.Name = "NoDaysCount", .Label = "nie odbyły się", .Size = 100, .ToolTip = "Liczba dni, w których lekcje się nie odbyły z innych powodów niż niedziele, święta i ferie"}}

      With dgvName
        .Columns.Clear()
        For Each Cell As TableCell In TableCols
          Dim NewCol As New DataGridViewColumn
          With NewCol
            .Name = Cell.Name
            .HeaderText = Cell.Label
            .Width = Cell.Size
            .HeaderCell.Style.Font = New Font(dgvName.Font, FontStyle.Bold)
            .CellTemplate = New DataGridViewTextBoxCell()
            '.CellTemplate.Style.BackColor = Cell.BgColor
            .CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ToolTipText = Cell.ToolTip
            .ReadOnly = True
            .Frozen = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
          End With
          .Columns.Add(NewCol)
        Next
        .Columns(0).Frozen = True
        .Columns(0).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(0).CellTemplate.Style.Font = New Font(.Font, FontStyle.Bold)

      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally

    End Try

  End Sub
  Private Sub ClearCellData()
    For Each R As DataGridViewRow In dgvFrekwencja.Rows
      For i As Integer = 2 To R.Cells.Count - 1
        R.Cells(i).Tag = Nothing
        R.Cells(i).Value = Nothing
      Next
    Next
  End Sub
  Private Sub GetData()
    'InRefresh = True
    Try
      Dim dsStudent As DataSet = FetchStudent()
      Dim dtAbsence As DataTable = FetchAbsence()
      Dim dtTopic As DataTable = FetchTopicCount()
      ClearCellData()
      With dgvFrekwencja
        For Each R As DataGridViewRow In .Rows
          Dim StudentTotal, IndividualTotal As Integer
          If R.Index < .Rows.Count - 1 Then
            Dim StudentCount As Byte, StartDate, EndDate As Date
            If CType(R.Tag, Byte) > 8 Then
              StartDate = New Date(CType(My.Settings.SchoolYear.Substring(0, 4), Integer), CType(R.Tag, Byte), 1)
              EndDate = New Date(CType(My.Settings.SchoolYear.Substring(0, 4), Integer), CType(R.Tag, Byte), Date.DaysInMonth(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), CType(R.Tag, Byte)))
            Else
              StartDate = New Date(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), CType(R.Tag, Byte), 1)
              EndDate = New Date(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), CType(R.Tag, Byte), Date.DaysInMonth(CType(My.Settings.SchoolYear.Substring(5, 4), Integer), CType(R.Tag, Byte)))

            End If
            StudentCount = CType(dsStudent.Tables("StudentCount").Compute("COUNT(ID)", "Aktywacja<=#" & EndDate & "# AND (Deaktywacja>#" & EndDate & "# OR Deaktywacja is null)"), Byte)
            Dim ObjectGroup As String = ""
            For Each P As DataRow In dsStudent.Tables("ObjectGroup").DefaultView.ToTable(True, "Przedmiot").Rows
              ObjectGroup += P.Item("Przedmiot").ToString & ","
            Next
            ObjectGroup = ObjectGroup.TrimEnd(",".ToCharArray)
            Dim TopicCount As Integer = 0
            If ObjectGroup.Length > 0 Then
              TopicCount = CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Przedmiot NOT IN (" & ObjectGroup & ")"), Integer) * StudentCount
              For Each P As DataRow In dsStudent.Tables("ObjectGroup").DefaultView.ToTable(True, "Przedmiot").Rows
                Dim GroupCount As Integer = CType(dsStudent.Tables("ObjectGroup").Compute("COUNT(IdPrzydzial)", "Przedmiot=" & P.Item("Przedmiot").ToString), Integer)
                TopicCount += CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Przedmiot=" & P.Item("Przedmiot").ToString), Integer) * GroupCount
              Next
            Else
              TopicCount = CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte)), Integer) * StudentCount

            End If


            'Uczniowie zapisani w danym miesiącu
            For Each S As DataRow In dsStudent.Tables("StudentCount").Select("Status=1 AND Aktywacja >#" & StartDate & "# AND Aktywacja<=#" & EndDate & "#")
              If ObjectGroup.Length > 0 Then
                TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Przedmiot NOT IN (" & ObjectGroup & ")" & " AND Data<#" & CType(S.Item("Aktywacja"), Date) & "#"), Integer)
                Dim dv As New DataView(dsStudent.Tables("ObjectGroup"), "IdPrzydzial=" & S.Item("ID").ToString, "Przedmiot", DataViewRowState.CurrentRows)
                For Each P As DataRow In dv.ToTable(True, "Przedmiot").Rows
                  TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Przedmiot=" & P.Item("Przedmiot").ToString & " AND Data<#" & CType(S.Item("Aktywacja"), Date) & "#"), Integer)
                Next
              Else
                TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Data<#" & CType(S.Item("Aktywacja"), Date) & "#"), Integer)
              End If
            Next

            'Uczniowie zapisani w danym miesiącu i skreśleni w tym samym miesiącu
            For Each S As DataRow In dsStudent.Tables("StudentCount").Select("Status=0 AND Aktywacja >=#" & StartDate & "# AND Deaktywacja<=#" & EndDate & "#")
              TopicCount += CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Data>=#" & CType(S.Item("Aktywacja"), Date) & "# OR Data<=#" & CType(S.Item("Deaktywacja"), Date) & "#"), Integer)
              Dim dv As New DataView(dsStudent.Tables("ObjectGroup"), "IdPrzydzial=" & S.Item("ID").ToString, "Przedmiot", DataViewRowState.CurrentRows)
              For Each P As DataRow In dv.ToTable(True, "Przedmiot").Rows
                TopicCount += CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Przedmiot=" & P.Item("Przedmiot").ToString & " AND Data>=#" & CType(S.Item("Aktywacja"), Date) & "# OR Data<=#" & CType(S.Item("Deaktywacja"), Date) & "#"), Integer)
              Next
            Next

            'Uczniowie skreśleni w danym miesiącu, ale zapisani wcześniej
            For Each S As DataRow In dsStudent.Tables("StudentCount").Select("Status=0 AND Deaktywacja <=#" & EndDate & "# AND Deaktywacja>=#" & StartDate & "# And Aktywacja<#" & StartDate & "#")
              TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Data>#" & CType(S.Item("Deaktywacja"), Date) & "#"), Integer)
              Dim dv As New DataView(dsStudent.Tables("ObjectGroup"), "IdPrzydzial=" & S.Item("ID").ToString, "Przedmiot", DataViewRowState.CurrentRows)
              For Each P As DataRow In dv.ToTable(True, "Przedmiot").Rows
                TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "MonthNumber=" & CType(R.Tag, Byte) & " AND Przedmiot=" & P.Item("Przedmiot").ToString & " AND Data>#" & CType(S.Item("Deaktywacja"), Date) & "#"), Integer)
              Next
            Next

            'Nauczanie indywidualne
            Dim IndividualCourseCount As Byte, dtIndividualCourse As DataTable
            dtIndividualCourse = New DataView(dsStudent.Tables("IndividualCourse"), "Aktywacja<#" & EndDate & "# AND (Deaktywacja>#" & EndDate & "# OR Deaktywacja is null)", "Aktywacja", DataViewRowState.CurrentRows).ToTable
            IndividualCourseCount = IndividualCount(dtIndividualCourse)

            If IndividualCourseCount > 0 Then
              StudentCount -= IndividualCourseCount
              IndividualTotal += IndividualCourseCount
              R.Cells(1).Value = String.Concat(StudentCount, " + ", IndividualCourseCount)

              'Wyłaczenie przedmiotow dzielonych na grupy
              If ObjectGroup.Length > 0 Then
                For Each P As DataRow In New DataView(dtIndividualCourse, "Przedmiot NOT IN (" & ObjectGroup & ")", "Przedmiot", DataViewRowState.CurrentRows).ToTable(True, "Przedmiot").Rows
                  'Uczniowie rozpoczynający nauczanie indywidualne w danym miesiącu
                  For Each S As DataRow In dtIndividualCourse.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja>=#" & StartDate & "#")
                    TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND Data>=#" & CType(S.Item("Aktywacja"), Date) & "#"), Integer)
                  Next

                  'Uczniowie rozpoczynający nauczanie indywidualne przed datą początkową danego miesiąca i kontunujący przez cały miesiąc
                  For Each S As DataRow In dtIndividualCourse.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja<#" & StartDate & "#")
                    TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte)), Integer)
                  Next
                Next

                'Przedmioty dzielone na grupy
                For Each P As DataRow In New DataView(dtIndividualCourse, "Przedmiot IN (" & ObjectGroup & ")", "Przedmiot", DataViewRowState.CurrentRows).ToTable(True, "Przedmiot").Rows
                  'Uczniowie rozpoczynający nauczanie indywidualne w danym miesiącu i kontynuujący przez cały miesiac
                  For Each S As DataRow In dtIndividualCourse.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja>=#" & StartDate & "#")
                    TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND Data>=#" & CType(S.Item("Aktywacja"), Date) & "#"), Integer)
                  Next

                  'Uczniowie rozpoczynający nauczanie indywidualne przed datą początkową danego miesiąca i kontunujący przez cały miesiąc
                  For Each S As DataRow In dtIndividualCourse.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja<#" & StartDate & "#")
                    TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte)), Integer)
                  Next
                Next
              Else
                For Each P As DataRow In dtIndividualCourse.DefaultView().ToTable(True, "Przedmiot").Rows 'New DataView(dtIndividualCourse, "Przedmiot NOT IN (" & ObjectGroup & ")", "Przedmiot", DataViewRowState.CurrentRows).ToTable(True, "Przedmiot").Rows
                  'Uczniowie rozpoczynający nauczanie indywidualne w danym miesiącu
                  For Each S As DataRow In dtIndividualCourse.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja>=#" & StartDate & "#")
                    TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND Data>=#" & CType(S.Item("Aktywacja"), Date) & "#"), Integer)
                  Next

                  'Uczniowie rozpoczynający nauczanie indywidualne przed datą początkową danego miesiąca i kontunujący przez cały miesiąc
                  For Each S As DataRow In dtIndividualCourse.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja<#" & StartDate & "#")
                    TopicCount -= CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte)), Integer)
                  Next
                Next
              End If

            Else
              R.Cells(1).Value = StudentCount.ToString
            End If
            StudentTotal += StudentCount
            Dim dtIndividualCourseEnd As DataTable
            dtIndividualCourseEnd = New DataView(dsStudent.Tables("IndividualCourse"), "Aktywacja<#" & EndDate & "# AND Deaktywacja<#" & EndDate & "#", "Aktywacja", DataViewRowState.CurrentRows).ToTable
            If ObjectGroup.Length > 0 Then
              'Wyłączenie przedmiotów dzielonych na grupy
              For Each P As DataRow In New DataView(dtIndividualCourseEnd, "Przedmiot NOT IN (" & ObjectGroup & ")", "Przedmiot", DataViewRowState.CurrentRows).ToTable(True, "Przedmiot").Rows 'dtIndividualCourseEnd.DefaultView().ToTable(True, "Przedmiot").Rows
                'Uczniowie rozpoczynający i kończący nauczanie indywidualne w danym miesiącu
                For Each S As DataRow In dtIndividualCourseEnd.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja>=#" & StartDate & "# AND Deaktywacja<#" & EndDate & "#")
                  TopicCount += CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND (Data<#" & CType(S.Item("Aktywacja"), Date) & "# OR Data>#" & CType(S.Item("Deaktywacja"), Date) & "#)"), Integer)
                Next

                'Uczniowie rozpoczynający nauczanie indywidualne przed datą początkową danego miesiąca i kontunujący przez cały miesiąc
                For Each S As DataRow In dtIndividualCourseEnd.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja<#" & StartDate & "# AND Deaktywacja<#" & EndDate & "#")
                  TopicCount += CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND Data>#" & CType(S.Item("Deaktywacja"), Date) & "#"), Integer)
                Next
              Next

              'Przedmioty dzielone na grupy
              For Each P As DataRow In New DataView(dtIndividualCourseEnd, "Przedmiot IN (" & ObjectGroup & ")", "Przedmiot", DataViewRowState.CurrentRows).ToTable(True, "Przedmiot").Rows 'dtIndividualCourseEnd.DefaultView().ToTable(True, "Przedmiot").Rows
                'Uczniowie rozpoczynający i kończący nauczanie indywidualne w danym miesiącu
                For Each S As DataRow In dtIndividualCourseEnd.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja>=#" & StartDate & "# AND Deaktywacja<#" & EndDate & "#")
                  TopicCount += CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND (Data<#" & CType(S.Item("Aktywacja"), Date) & "# OR Data>#" & CType(S.Item("Deaktywacja"), Date) & "#)"), Integer)
                Next

                'Uczniowie rozpoczynający nauczanie indywidualne przed datą początkową danego miesiąca i kontunujący przez cały miesiąc
                For Each S As DataRow In dtIndividualCourseEnd.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja<#" & StartDate & "# AND Deaktywacja<#" & EndDate & "#")
                  TopicCount += CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND Data>#" & CType(S.Item("Deaktywacja"), Date) & "#"), Integer)
                Next
              Next
            Else
              For Each P As DataRow In dtIndividualCourseEnd.DefaultView().ToTable(True, "Przedmiot").Rows 'New DataView(dtIndividualCourseEnd, "Przedmiot NOT IN (" & ObjectGroup & ")", "Przedmiot", DataViewRowState.CurrentRows).ToTable(True, "Przedmiot").Rows 'dtIndividualCourseEnd.DefaultView().ToTable(True, "Przedmiot").Rows
                'Uczniowie rozpoczynający i kończący nauczanie indywidualne w danym miesiącu
                For Each S As DataRow In dtIndividualCourseEnd.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja>=#" & StartDate & "# AND Deaktywacja<#" & EndDate & "#")
                  TopicCount += CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND (Data<#" & CType(S.Item("Aktywacja"), Date) & "# OR Data>#" & CType(S.Item("Deaktywacja"), Date) & "#)"), Integer)
                Next

                'Uczniowie rozpoczynający nauczanie indywidualne przed datą początkową danego miesiąca i kontunujący przez cały miesiąc
                For Each S As DataRow In dtIndividualCourseEnd.Select("Przedmiot=" & P.Item("Przedmiot").ToString & " AND Aktywacja<#" & StartDate & "# AND Deaktywacja<#" & EndDate & "#")
                  TopicCount += CType(dtTopic.Compute("COUNT(ID)", "Przedmiot=" & P.Item("Przedmiot").ToString & " AND MonthNumber=" & CType(R.Tag, Byte) & " AND Data>#" & CType(S.Item("Deaktywacja"), Date) & "#"), Integer)
                Next
              Next
            End If


            R.Cells(2).Value = If(TopicCount > 0, TopicCount, 0)
            R.Cells(4).Value = If(dtAbsence.Select("MonthNumber=" & CType(R.Tag, Byte)).Length > 0, dtAbsence.Select("MonthNumber=" & CType(R.Tag, Byte))(0).Item("AbsenceCount"), 0)
            R.Cells(3).Value = CType(R.Cells(2).Value, Integer) - CType(R.Cells(4).Value, Integer)
            R.Cells(5).Value = If(CType(R.Cells(2).Value, Integer) > 0, Math.Round(CType(R.Cells(3).Value, Integer) * 100 / CType(R.Cells(2).Value, Integer), 2), 0)
            R.Cells(6).Value = dtTopic.DefaultView().ToTable(True, "Data", "MonthNumber").Compute("COUNT(Data)", "MonthNumber=" & CType(R.Tag, Byte))

          Else
            R.Cells(1).Value = If(IndividualTotal > 0, String.Concat(StudentTotal, " + ", IndividualTotal), StudentTotal.ToString)

            For i As Integer = 2 To 4
              Dim Total As Integer = 0
              For j As Integer = 0 To dgvFrekwencja.Rows.Count - 2
                Total += CType(dgvFrekwencja.Rows(j).Cells(i).Value, Integer)
              Next
              R.Cells(i).Value = Total
            Next
            R.Cells(5).Value = If(CType(R.Cells(2).Value, Integer) > 0, Math.Round(CType(R.Cells(3).Value, Integer) * 100 / CType(R.Cells(2).Value, Integer), 2), 0)
            R.Cells(6).Value = dtTopic.DefaultView().ToTable(True, "Data").Compute("COUNT(Data)", "")
          End If
        Next

      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Function IndividualCount(T As DataTable) As Byte
    Dim dt As DataTable = T.DefaultView.ToTable(True, "IdPrzydzial")
    Return CType(dt.Compute("COUNT(IdPrzydzial)", ""), Byte)
  End Function

  Private Sub SetRows(ByVal dgvName As DataGridView)
    Try
      Dim SchoolYear = New List(Of SchoolMonth) From {New SchoolMonth With {.Nr = 9, .Name = "Wrzesień"},
                                                      New SchoolMonth With {.Nr = 10, .Name = "Październik"},
                                                      New SchoolMonth With {.Nr = 11, .Name = "Listopad"},
                                                      New SchoolMonth With {.Nr = 12, .Name = "Grudzień"},
                                                      New SchoolMonth With {.Nr = 1, .Name = "Styczeń"},
                                                      New SchoolMonth With {.Nr = 2, .Name = "Luty"},
                                                      New SchoolMonth With {.Nr = 3, .Name = "Marzec"},
                                                      New SchoolMonth With {.Nr = 4, .Name = "Kwiecień"},
                                                      New SchoolMonth With {.Nr = 5, .Name = "Maj"},
                                                      New SchoolMonth With {.Nr = 6, .Name = "Czerwiec"}}
      Dim ShiftColor As Boolean = False, GridColor() As Color = {Color.LightYellow, Color.AliceBlue}
      With dgvName
        .Rows.Clear()
        For Each M As SchoolMonth In SchoolYear
          .Rows.Add(M.Name)
          .Rows(.RowCount - 1).Tag = M.Nr
          .Rows(.RowCount - 1).DefaultCellStyle.BackColor = GridColor(Convert.ToInt32(ShiftColor))
          ShiftColor = Not ShiftColor
        Next
        .Rows.Add("Razem")
        .Rows(.RowCount - 1).Tag = 0
        .Rows(.RowCount - 1).DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFF99") 'Color.Yellow
        .Rows(.RowCount - 1).DefaultCellStyle.Font = New Font(.Font, FontStyle.Bold)
        .Rows(.RowCount - 1).DefaultCellStyle.ForeColor = Color.Firebrick
        .Rows(.RowCount - 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Rows(.RowCount - 1).Height = 40

        '.Rows(.RowCount - 1).Frozen = True

        .Enabled = True
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try

  End Sub


  Private Sub dgvZestawienieOcen_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
    Try
      Dim rtHeader As Rectangle = Me.dgvFrekwencja.DisplayRectangle
      rtHeader.Height = CType(Me.dgvFrekwencja.ColumnHeadersHeight / 2, Integer)
      Me.dgvFrekwencja.Invalidate(rtHeader)

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
    Try
      If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then Exit Sub
      Dim rtHeader As Rectangle = Me.dgvFrekwencja.DisplayRectangle
      rtHeader.Height = CType(Me.dgvFrekwencja.ColumnHeadersHeight / 2, Integer)
      Me.dgvFrekwencja.Invalidate(rtHeader)

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub dgvZestawienieOcen_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
    If dgvFrekwencja.Columns.Count < 1 Then Exit Sub
    Dim StrFormat As New StringFormat()
    StrFormat.Alignment = StringAlignment.Center
    StrFormat.LineAlignment = StringAlignment.Center
    Try

      Dim MonthCol As Rectangle = Me.dgvFrekwencja.GetCellDisplayRectangle(0, -1, True)
      e.Graphics.FillRectangle(New SolidBrush(SystemColors.Control), MonthCol.X, MonthCol.Y, MonthCol.Width, MonthCol.Height - 1)
      e.Graphics.DrawString("Miesiąc", New Font(Me.dgvFrekwencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Me.dgvFrekwencja.ColumnHeadersDefaultCellStyle.ForeColor), MonthCol, StrFormat)

      Dim StudentCount As Rectangle = Me.dgvFrekwencja.GetCellDisplayRectangle(1, -1, True)
      e.Graphics.FillRectangle(New SolidBrush(SystemColors.Control), StudentCount.X, StudentCount.Y, StudentCount.Width - 1, StudentCount.Height - 1)
      e.Graphics.DrawLine(Pens.Black, StudentCount.X, StudentCount.Y, StudentCount.X, StudentCount.Height)
      e.Graphics.DrawString("Liczba uczniów" & vbNewLine & "w klasie", New Font(Me.dgvFrekwencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Me.dgvFrekwencja.ColumnHeadersDefaultCellStyle.ForeColor), StudentCount, StrFormat)

      Dim r1 As Rectangle = Me.dgvFrekwencja.GetCellDisplayRectangle(2, -1, True)
      e.Graphics.DrawLine(Pens.Black, r1.X, r1.Y, r1.X, r1.Height)
      r1.X += 1
      r1.Y += 1
      r1.Width = r1.Width * 4
      r1.Height = CType(r1.Height / 2 - 2, Integer)
      e.Graphics.FillRectangle(New SolidBrush(SystemColors.Control), r1)
      e.Graphics.DrawString("Liczba godzin obowiązkowych zajeć lekcyjnych", New Font(Me.dgvFrekwencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.Black), r1, StrFormat)

      Dim r2 As Rectangle = Me.dgvFrekwencja.GetCellDisplayRectangle(6, -1, True)
      e.Graphics.DrawLine(Pens.Black, r2.X, r2.Y, r2.X, r2.Height)
      r2.X += 1
      r2.Y += 1
      r2.Width = r2.Width * 2
      r2.Height = CType(r2.Height / 2 - 2, Integer)
      e.Graphics.FillRectangle(New SolidBrush(SystemColors.Control), r2)
      e.Graphics.DrawString("Liczba dni, w których lekcje", New Font(Me.dgvFrekwencja.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), New SolidBrush(Color.Black), r2, StrFormat)
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
    'MessageBox.Show(Me.Height.ToString & vbNewLine & Me.Width.ToString)
    Me.Dispose(True)
  End Sub


  Private Sub dgvZestawienieOcen_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvFrekwencja.DataError
    MessageBox.Show(e.Exception.Message)
  End Sub


  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs)
    If Not Me.Created Then Exit Sub
    ApplyNewConfig()

  End Sub


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
      PP.Doc.ReportHeader = New String() {"Zestawienie frekwencji", "Klasa " & cbKlasa.Text}
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
      'PH.DrawText(Doc.ReportHeader(2), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
      y += LineHeight * 2
    End If
    With dgvFrekwencja
      Dim ColSize As Integer = 90, MonthColSize As Integer = 100
      Dim dx = x, y0 As Single = y
      PH.DrawText("Miesiąc", New Font(TextFont, FontStyle.Bold), dx, y, MonthColSize, LineHeight * 3, 1, Brushes.Black)
      dx += MonthColSize
      PH.DrawText("Liczba uczniów", New Font(TextFont, FontStyle.Bold), dx, y, ColSize, LineHeight * 3, 1, Brushes.Black)
      dx += ColSize
      PH.DrawText("Liczba godzin obowiązkowych zajęć lekcyjnych", New Font(TextFont, FontStyle.Bold), dx, y, ColSize * 4, LineHeight * CSng(1.5), 1, Brushes.Black)
      dx += ColSize * 4
      PH.DrawText("Liczba dni, w których lekcje", New Font(TextFont, FontStyle.Bold), dx, y, ColSize * 2, LineHeight * CSng(1.5), 1, Brushes.Black)

      'Dim dx1 = dx

      dx = x + MonthColSize + ColSize
      For i As Integer = 2 To .Columns.Count - 1
        PH.DrawText(.Columns(i).HeaderText, New Font(TextFont, FontStyle.Bold), dx, y + LineHeight * CSng(1.5), ColSize, LineHeight * CSng(1.5), 1, Brushes.Black)
        dx += ColSize
      Next
      y += LineHeight * 3
      PH.DrawLine(x, y, dx, y, 2)
      dx = x

      Do Until (y + LineHeight * CSng(1.5)) > PrintHeight Or Offset(0) > .Rows.Count - 1
        PH.DrawText(.Rows(Offset(0)).Cells("MonthName").FormattedValue.ToString, New Font(TextFont, FontStyle.Bold), dx, y, MonthColSize, If(Offset(0) < .Rows.Count - 1, LineHeight, LineHeight * 2), 0, Brushes.Black)
        dx += MonthColSize

        Offset(1) = 1
        Do Until Offset(1) > .Columns.Count - 1
          PH.DrawText(If(.Rows(Offset(0)).Cells(Offset(1)).FormattedValue.ToString.Length > 0, .Rows(Offset(0)).Cells(Offset(1)).FormattedValue.ToString, ""), If(Offset(0) < .Rows.Count - 1, TextFont, New Font(TextFont, FontStyle.Bold)), dx, y, ColSize, If(Offset(0) < .Rows.Count - 1, LineHeight, LineHeight * 2), 1, Brushes.Black)
          dx += ColSize
          Offset(1) += 1
        Loop
        y += LineHeight
        Offset(1) = 0
        Offset(0) += 1
        dx = x
      Loop
      PH.DrawLine(x, y - LineHeight, x + MonthColSize + ColSize * (.Columns.Count - 1), y - LineHeight, 2)
      y += LineHeight
      PH.DrawRectangle(2, x, y0, MonthColSize + ColSize * (.Columns.Count - 1), y - y0)
      PH.DrawLine(x + MonthColSize, y0, x + MonthColSize, y, 2)
      PH.DrawLine(x + ColSize + MonthColSize, y0, x + ColSize + MonthColSize, y, 2)
      PH.DrawLine(x + ColSize * (.Columns.Count - 3) + MonthColSize, y0, x + ColSize * (.Columns.Count - 3) + MonthColSize, y, 2)

      If Offset(0) < .Rows.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
      Else
        Offset(0) = 0
        PageNumber = 0
      End If
    End With

  End Sub


  Private Sub dgvFrekwencja_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvFrekwencja.RowPostPaint
    If e.IsLastVisibleRow Then
      Dim RowWidth As Integer = dgvFrekwencja.Columns.GetColumnsWidth(DataGridViewElementStates.Visible)
      e.Graphics.DrawLine(New Pen(Brushes.Black, 2), e.RowBounds.Left, e.RowBounds.Top - 1, RowWidth, e.RowBounds.Top - 1)
    End If
  End Sub
End Class
Public Class TableCell : Inherits Pole
  'Public Property BgColor As Color
  Public Property ToolTip As String

End Class