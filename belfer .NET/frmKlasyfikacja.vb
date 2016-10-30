Imports System.Drawing.Printing

Public Class frmKlasyfikacja
  Public Event NewRow()
  Private Offset(8), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean ', NewPage As Boolean = True
  Private CurrentDate, DataRP As Date, StanKlasy, MaxPion, MinPion As Byte, DS As DataSet, Avg As String, PrevYearObjectToMaxPionAvg As Boolean, DyplomFParam As Single
  Private PasekParams, DyplomParams As OutstandingParams

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.KlasyfikacjaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.KlasyfikacjaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData(Okres As String)
    Dim DBA As New DataBaseAction, K As New KlasyfikacjaSQL, CH As New CalcHelper, S As New StatystykaSQL
    Dim StartDate, EndDate As Date
    Dim Wait As New dlgWait
    Try
      Wait.lblInfo.Text = "Trwa pobieranie danych ..."
      Wait.Show()
      Application.DoEvents()
      StartDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      EndDate = CType(IIf(Okres = "S", CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)

      StanKlasy = CType(DBA.GetSingleValue(K.CountStudentByKlasa(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear)), Byte)

      Avg = DBA.GetSingleValue(K.CountAvgByKlasa(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)).ToString

      Dim ClasifyContent As New List(Of DataTableContent) From
      {
      New DataTableContent With {.TableName = "CountNdstByWaga", .SelectString = K.CountScoreByStudentByWaga(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)},
      New DataTableContent With {.TableName = "CountZachowanie", .SelectString = K.CountZachowanieByWaga(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)},
      New DataTableContent With {.TableName = "StudentByNkl", .SelectString = K.SelectStudentByNkl(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)},
      New DataTableContent With {.TableName = "CountAbsenceByStudent", .SelectString = K.CountAbsenceByStudent(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, StartDate, EndDate)},
      New DataTableContent With {.TableName = "Lekcja", .SelectString = S.SelectLekcja(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, StartDate, EndDate)},
      New DataTableContent With {.TableName = "GrupaPrzedmiotowa", .SelectString = K.SelectGrupaPrzedmiotowa(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)},
      New DataTableContent With {.TableName = "StudentByNdst", .SelectString = K.SelectStudentByNdst(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)},
      New DataTableContent With {.TableName = "StudentByNg", .SelectString = K.SelectStudentByNg(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)},
      New DataTableContent With {.TableName = "CountNdstByPrzedmiot", .SelectString = K.CountScoreByObject(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)}
      }
      'New DataTableContent With {.TableName = "CountNdstByStudent", .SelectString = K.CountScoreByStudent(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres)}
      DS = New DataSet
      For Each T In ClasifyContent
        Dim DT As DataTable = DBA.GetDataTable(T.SelectString)
        DT.TableName = T.TableName
        DS.Tables.Add(DT)
      Next
      For Each Student As DataRow In DS.Tables("StudentByNkl").Rows
        For Each R As DataRow In DS.Tables("CountNdstByWaga").Select("IdUczen=" & Student.Item("IdUczen").ToString)
          R.Delete()
        Next
      Next
      DS.Tables("CountNdstByWaga").AcceptChanges()

      If Okres = "R" Then
        Dim dtPoprawka As DataTable = DBA.GetDataTable(K.SelectRepeaterByNdst(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Okres))
        dtPoprawka.TableName = "RepeaterByNdst"
        DS.Tables.Add(dtPoprawka)

        Dim dtStudent As DataTable = DBA.GetDataTable(K.SelectStudentByKlasa(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, DataRP))
        dtStudent.TableName = "StudentByKlasa"
        DS.Tables.Add(dtStudent)

        'Dim dtAbsencja As DataTable = DBA.GetDataTable(K.CountAbsenceByStudent(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, StartDate, EndDate))
        'dtAbsencja.TableName = "CountAbsenceByStudent"
        'DS.Tables.Add(dtAbsencja)

        Dim dtWynik As DataTable = DBA.GetDataTable(K.SelectWynik(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, StartDate, EndDate, Okres))
        dtWynik.TableName = "Wynik"
        DS.Tables.Add(dtWynik)

        If CType(cbKlasa.SelectedItem, SchoolClassComboItem).PionKlas = MaxPion AndAlso PrevYearObjectToMaxPionAvg Then
          Dim Z As New ZestawienieOcenSQL
          Dim dtPrzedmiot As DataTable = DBA.GetDataTable(Z.SelectPrzedmiot(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, EndDate))
          dtPrzedmiot.TableName = "Przedmiot"
          DS.Tables.Add(dtPrzedmiot)

          For i As Integer = CType(cbKlasa.SelectedItem, SchoolClassComboItem).PionKlas - 1 To MinPion Step -1
            Dim Przedmiot As String = ""
            For Each R As DataRow In DS.Tables("Przedmiot").DefaultView.ToTable(True, "ID").Rows
              Przedmiot += R.Item("ID").ToString + ","
            Next
            If Przedmiot.Length > 0 Then
              Dim dtTEMP As DataTable, j As Integer = CType(cbKlasa.SelectedItem, SchoolClassComboItem).PionKlas - i, PreviousSchoolYear, PreviousClass As String, StartYear As Integer
              StartYear = CType(My.Settings.SchoolYear.Substring(0, 4), Integer) - j
              PreviousSchoolYear = String.Concat(StartYear, "/", StartYear + 1)
              PreviousClass = String.Concat(CType(cbKlasa.SelectedItem, SchoolClassComboItem).PionKlas - j, CType(cbKlasa.SelectedItem, SchoolClassComboItem).KodKlasy.Substring(1, 1))
              dtTEMP = DBA.GetDataTable(Z.SelectPrzedmiot(My.Settings.IdSchool, PreviousSchoolYear, PreviousClass, EndDate.AddYears(-j), Przedmiot.TrimEnd(",".ToCharArray)))
              'Else
              '  dtTEMP = DBA.GetDataTable(Z.SelectPrzedmiot(My.Settings.IdSchool, PreviousSchoolYear, PreviousClass, CType(B.Tag, SchoolPeriod).DataKoncowa.AddYears(-j), "''"))
              For Each R As DataRow In dtTEMP.Rows
                DS.Tables("Przedmiot").ImportRow(R)
              Next
              Przedmiot = ""
              For Each R As DataRow In dtTEMP.DefaultView.ToTable(True, "ID").Rows
                Przedmiot += R.Item("ID").ToString + ","
              Next
              Dim PreviousSchoolYearStartDate, PreviousSchoolYearEndDate As Date
              PreviousSchoolYearStartDate = CH.StartDateOfSchoolYear(PreviousSchoolYear)
              PreviousSchoolYearEndDate = CH.EndDateOfSchoolYear(PreviousSchoolYear)
              If Przedmiot.Length > 0 Then
                dtTEMP = DBA.GetDataTable(K.SelectWynik(My.Settings.IdSchool, PreviousSchoolYear, PreviousClass, PreviousSchoolYearStartDate, PreviousSchoolYearEndDate, Okres, Przedmiot.TrimEnd(",".ToCharArray)))
              End If
              For Each R As DataRow In dtTEMP.Rows
                DS.Tables("Wynik").ImportRow(R)
              Next
            End If
          Next
        End If
      End If
      Wait.Hide()

    Catch mex As MySqlException
      Wait.Hide()
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    Catch ex As Exception
      Wait.Hide()
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub

  Private Sub frmKlasyfikacja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    lblWychowawca.Text = ""
    Dim CH As New CalcHelper
    If Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year) Then
      rbSemestr.Checked = True
    Else
      rbRokSzkolny.Checked = True
    End If
    ListViewConfig(lvNkl)
    ListViewConfig(lvNdst)
    ListViewConfig(lvZachowanie)
    ListViewConfig(lvNdstByPrzedmiot)

    Dim NklCols As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Student", .Text = "Uczniowie nieklasyfikowani ze wszystkich przedmiotów", .Width = 280}}
    Dim NdstCols As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Student", .Text = "Uczniowie z ocenami ndst i nkl z jednego lub kilku przedmiotów", .Width = 264}}
    Dim ZachCols As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Student", .Text = "Uczniowie z oceną nieodpowiednią lub naganną", .Width = 280}}
    Dim LiczbaNdstCols As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Przedmiot", .Text = "Przedmiot", .Width = 200}, New ColumnHeader With {.Name = "LiczbaNdst", .Text = "Liczba uczniów z ocenami niedostatecznymi", .Width = 200, .TextAlign = HorizontalAlignment.Center}, New ColumnHeader With {.Name = "LiczbaNkl", .Text = "Liczba uczniów nieklasyfikowanych", .Width = 200, .TextAlign = HorizontalAlignment.Center}, New ColumnHeader With {.Name = "Nauczyciel", .Text = "Nauczyciel uczący", .Width = 242, .TextAlign = HorizontalAlignment.Left}}
    AddListViewColumn(lvNkl, NklCols)
    AddListViewColumn(lvNdst, NdstCols)
    AddListViewColumn(lvZachowanie, ZachCols)
    AddListViewColumn(lvNdstByPrzedmiot, LiczbaNdstCols)
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
    Dim OH As New OptionHolder
    MaxPion = OH.GetMaxPion
    MinPion = OH.GetMinPion
    PasekParams = New OutstandingParams With {.Avg = OH.GetPasekAvg(CurrentDate), .Behavior = OH.GetPasekBehaviorScore(CurrentDate)}
    DyplomParams = New OutstandingParams With {.Avg = OH.GetDyplomAvg(CurrentDate), .Behavior = OH.GetDyplomBehaviorScore(CurrentDate)}
    DyplomFParam = OH.GetDyplomFrekwencja(CurrentDate)
    PrevYearObjectToMaxPionAvg = OH.PrevYearObjectToMaxPionAvg(CurrentDate)

    FillKlasa()
  End Sub
  Private Sub FillKlasa()
    Dim K As New KlasyfikacjaSQL
    LoadClassItems(cbKlasa, K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, CurrentDate))
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
    Dim DBA As New DataBaseAction
    cb.Items.Clear()
    Try
      R = DBA.GetReader(SelectString)
      While R.Read()
        cb.Items.Add(New SchoolClassComboItem(R.GetInt32("ID"), R.GetString("Nazwa_Klasy"), CType(R.Item("Pion"), Byte), R.GetInt32("IdObsada"), R.GetString("Kod_Klasy")))
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
    Cursor = Cursors.WaitCursor
    GetWychowawca(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
    RefreshData()
    Cursor = Cursors.Default
  End Sub
  Private Sub RefreshData()
    Dim Semestr As RadioButton
    Semestr = Me.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)

    rbSemestr_CheckedChanged(Semestr, Nothing)
    'Wait.Close()
  End Sub
  Private Sub GetWychowawca(ByVal Klasa As String)
    Dim R As MySqlDataReader = Nothing, K As New KlasyfikacjaSQL, DBA As New DataBaseAction
    Try
      lblWychowawca.Text = ""

      R = DBA.GetReader(K.SelectWychowawca(Klasa, My.Settings.SchoolYear, CurrentDate))
      If R.HasRows Then
        R.Read()
        lblWychowawca.Text += R.Item("Wychowawca").ToString
      Else
        lblWychowawca.Text = "Nie udało się ustalić wychowawstwa"
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch err As Exception
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub rbSemestr_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemestr.CheckedChanged, rbRokSzkolny.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub

    Dim OH As New OptionHolder
    DataRP = OH.DataRP(CType(sender, RadioButton).Tag.ToString, CurrentDate) 'DBA.GetSingleValue(O.SelectOption(If(Okres = "S", "DataRPSemestr", "DataRPRokSzkolny"), "G", My.Settings.IdSchool, )).ToString

    If cbKlasa.SelectedItem IsNot Nothing Then GetClasification(CType(sender, RadioButton).Tag.ToString)
    'Wait.Close()
  End Sub
  Private Sub GetClasification(Okres As String)
    Try
      EnableButton(False)
      Dim Status As GlobalValues.ReasonStatus
      SetVisibility(False)
      Status = CheckClasificationStatus(CType(cbKlasa.SelectedItem, SchoolClassComboItem).IdObsadaWychowawstwa.ToString, Okres)
      Select Case Status
        Case GlobalValues.ReasonStatus.Brak, GlobalValues.ReasonStatus.Wprowadzone
          lblStatus.ForeColor = Color.Black
        Case GlobalValues.ReasonStatus.Odrzucone
          lblStatus.ForeColor = Color.Red
        Case GlobalValues.ReasonStatus.Przekazane
          lblStatus.ForeColor = Color.Green
        Case GlobalValues.ReasonStatus.Zatwierdzone
          lblStatus.ForeColor = Color.Blue

      End Select
      lblStatus.Text = Status.ToString

      If Status <> GlobalValues.ReasonStatus.Brak Then
        FetchData(Okres)
        ClasificationConfig(Okres)
        GetData(Okres)
        If (GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString) Then
          'cmdAddNew.Enabled = False
          cmdDelete.Enabled = If(Status = GlobalValues.ReasonStatus.Wprowadzone OrElse Status = GlobalValues.ReasonStatus.Odrzucone, True, False)
          cmdSend.Enabled = If(Status = GlobalValues.ReasonStatus.Wprowadzone OrElse Status = GlobalValues.ReasonStatus.Odrzucone, True, False)
          cmdRefresh.Enabled = True
          cmdPrint.Enabled = True
          'Else
          '  EnableButton(False)
        End If
      Else
        If (GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.TutorClassID = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString) Then
          'EnableButton(False)
          cmdAddNew.Enabled = True
          'Else
          '  EnableButton(False)
        End If
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub
  Private Sub ClasificationConfig(Okres As String)
    tcWykaz.TabPages.RemoveByKey("tp2")
    tcWykaz.TabPages.RemoveByKey("tp3")
    SetVisibility(True)
    If Okres = "S" Then
      lvNdst.Columns("Student").Text = "Uczniowie z ocenami niedostatecznymi i nieklasyfikowani z jednego lub kilku przedmiotów"
      lvNdst.Tag = "Wykaz uczniów z ocenami niedostatecznymi i nieklasyfikowanych z jednego lub kilku przedmiotów"
      tlpKlasyfikacjaRoczna.Visible = False
      tlpKlasyfikacja.Visible = True
    Else
      If CType(cbKlasa.SelectedItem, SchoolClassComboItem).PionKlas = MaxPion Then
        lvNdst.Columns("Student").Text = "Uczniowie, którzy nie ukończyli szkoły"
        lvNdst.Tag = "Wykaz uczniów, którzy nie ukończyli szkoły"
        lblPromocjaEtykieta.Text = "którzy ukończyli szkołę"
        lblNoPromocjaEtykieta.Text = "którzy nie ukończyli szkoły"
      Else
        lvNdst.Columns("Student").Text = "Uczniowie niepromowani"
        lvNdst.Tag = "Wykaz uczniów niepromowanych"
        lblPromocjaEtykieta.Text = "promowanych"
        lblNoPromocjaEtykieta.Text = "niepromowanych"
      End If
      tlpKlasyfikacja.Visible = False
      tlpKlasyfikacjaRoczna.Location = tlpKlasyfikacja.Location
      tlpKlasyfikacjaRoczna.Visible = True
      AddTabPages()
    End If
  End Sub

  Private Sub ListViewConfig(LV As ListView)
    With LV
      .View = View.Details
      .FullRowSelect = True
      .GridLines = False
      .ShowItemToolTips = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .OwnerDraw = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = True
      '.Visible = False
    End With
  End Sub
  Private Sub AddListViewColumn(lv As ListView, Cols As List(Of ColumnHeader))
    With lv
      .Items.Clear()
      .Columns.Clear()
      For Each Col As ColumnHeader In Cols
        .Columns.Add(Col)
      Next
    End With
  End Sub

  Private Sub GetData(Okres As String)
    lblDataRP.Text = DataRP.ToLongDateString
    lblAvg.Text = Avg
    If Okres = "S" Then
      lblStanKlasy.Text = StanKlasy.ToString
      lblNKL.Text = DS.Tables("StudentByNkl").Rows.Count.ToString
      lblKL.Text = (StanKlasy - CType(lblNKL.Text, Byte)).ToString
      lblNoNDST.Text = (CType(lblKL.Text, Integer) - DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Rows.Count).ToString
      'lblNoNDST.Text = (StanKlasy - DS.Tables("CountNdstByStudent").Rows.Count).ToString
      Dim StudentCount(1) As Integer
      For Each Student As DataRow In DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Rows
        Dim LO As Byte = CType(DS.Tables("CountNdstByWaga").Compute("Sum(LO)", "IdUczen=" & Student.Item("IdUczen").ToString), Byte)
        If LO = 1 Or LO = 2 Then StudentCount(0) += 1
        If LO > 2 Then StudentCount(1) += 1
      Next
      lbl12NDST.Text = StudentCount(0).ToString
      'lbl12NDST.Text = DS.Tables("CountNdstByStudent").Compute("Count(IdUczen)", "LO>=1 AND LO<=2").ToString
      'lbl3AndMoreNDST.Text = (CType(DS.Tables("CountNdstByStudent").Compute("Count(IdUczen)", "LO>2"), Byte) - DS.Tables("StudentByNkl").Rows.Count).ToString
      'StudentCount = 0
      'For Each Student As DataRow In DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Rows
      '  Dim LO As Byte = CType(DS.Tables("CountNdstByWaga").Compute("Sum(LO)", "IdUczen=" & Student.Item("IdUczen").ToString), Byte)
      '  If LO = 1 Or LO = 2 Then StudentCount += 1
      'Next
      lbl3AndMoreNDST.Text = StudentCount(1).ToString
      lvNdst.Groups.Clear()
      lvNdst.Items.Clear()
      For Each Student As DataRow In DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Rows
        Dim Grupa As String = DS.Tables("StudentByNdst").Select("IdUczen=" & Student.Item("IdUczen").ToString)(0).Item("Student").ToString
        Dim NG As New ListViewGroup(Grupa, Grupa.ToUpper)
        NG.HeaderAlignment = HorizontalAlignment.Left
        lvNdst.Groups.Add(NG)
        lvNdst.Groups(Grupa).Tag = Student.Item("IdUczen").ToString
        For Each P As DataRow In DS.Tables("StudentByNdst").Select("IdUczen=" & Student.Item("IdUczen").ToString)
          Dim NewItem As New ListViewItem(P.Item("Przedmiot").ToString, NG)
          NewItem.UseItemStyleForSubItems = False
          lvNdst.Items.Add(NewItem)
        Next

      Next
      lvNdst.Enabled = If(lvNdst.Items.Count > 0, True, False)
    Else
      lblStanKlasyRS.Text = StanKlasy.ToString
      lblNKLRS.Text = DS.Tables("StudentByNkl").Rows.Count.ToString
      lblKLRS.Text = (StanKlasy - CType(lblNKLRS.Text, Byte)).ToString
      'lblNoNDSTRS.Text = (StanKlasy - DS.Tables("CountNdstByStudent").Rows.Count).ToString
      lblNoNDSTRS.Text = (CType(lblKLRS.Text, Integer) - DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Rows.Count).ToString

      Dim Repeater As New List(Of String), RepeaterCount As Integer = 0, TotalRepeater As String = ""
      For Each S In DS.Tables("CountNdstByWaga").Select("Waga=1 AND LO=1")
        If CType(DS.Tables("CountNdstByWaga").Compute("Count(IdUczen)", "Waga=0 AND IdUczen=" & S.Item("IdUczen").ToString), Byte) = 0 Then
          Repeater.Add(S.Item("IdUczen").ToString)
          TotalRepeater += S.Item("IdUczen").ToString + ","
        End If
      Next

      lblEgzPopraw1Ndst.Text = Repeater.Count.ToString 'CountStudentByNdst.ToString
      RepeaterCount += Repeater.Count
      Dim lvPoprawka As ListView = CType(tcWykaz.TabPages("tp2").Controls("lvPoprawka1"), ListView)
      lvPoprawka.Groups.Clear()
      lvPoprawka.Items.Clear()
      For Each Rpt In Repeater
        Dim NewItem As New ListViewItem(DS.Tables("RepeaterByNdst").Select("IdUczen=" & Rpt.ToString)(0).Item("Student").ToString)
        NewItem.UseItemStyleForSubItems = False
        lvPoprawka.Items.Add(NewItem)
      Next
      lvPoprawka.Enabled = If(lvPoprawka.Items.Count > 0, True, False)
      Repeater.Clear()
      For Each S In DS.Tables("CountNdstByWaga").Select("Waga=1 AND LO=2")
        If CType(DS.Tables("CountNdstByWaga").Compute("Count(IdUczen)", "Waga=0 AND IdUczen=" & S.Item("IdUczen").ToString), Byte) = 0 Then
          Repeater.Add(S.Item("IdUczen").ToString)
          TotalRepeater += S.Item("IdUczen").ToString + ","
        End If
      Next
      lblEgzPopraw2Ndst.Text = Repeater.Count.ToString 'CountStudentByNdst.ToString
      RepeaterCount += Repeater.Count
      lvPoprawka = CType(tcWykaz.TabPages("tp2").Controls("lvPoprawka2"), ListView)
      lvPoprawka.Groups.Clear()
      lvPoprawka.Items.Clear()
      For Each Rpt In Repeater
        Dim NewItem As New ListViewItem(DS.Tables("RepeaterByNdst").Select("IdUczen=" & Rpt.ToString)(0).Item("Student").ToString)
        NewItem.UseItemStyleForSubItems = False
        lvPoprawka.Items.Add(NewItem)
      Next
      lvPoprawka.Enabled = If(lvPoprawka.Items.Count > 0, True, False)
      'lvPoprawka.Dispose()
      lblPromocja.Text = lblNoNDSTRS.Text
      lblNoPromocja.Text = (CType(lblStanKlasyRS.Text, Byte) - CType(lblPromocja.Text, Byte) - RepeaterCount).ToString
      'lblNoPromocja.Text = (CType(lblKLRS.Text, Byte) - CType(lblPromocja.Text, Byte)).ToString

      lvNdst.Groups.Clear()
      lvNdst.Items.Clear()
      Dim Filter As String = If(TotalRepeater.Length > 0, "IdUczen NOT IN (" & TotalRepeater.TrimEnd(",".ToCharArray) & ")", "")
      For Each Student As DataRow In DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Select(Filter)
        Dim Grupa As String = DS.Tables("StudentByNdst").Select("IdUczen=" & Student.Item("IdUczen").ToString)(0).Item("Student").ToString
        Dim NG As New ListViewGroup(Grupa, Grupa.ToUpper)
        NG.HeaderAlignment = HorizontalAlignment.Left
        lvNdst.Groups.Add(NG)
        lvNdst.Groups(Grupa).Tag = Student.Item("IdUczen").ToString
        For Each P As DataRow In DS.Tables("StudentByNdst").Select("IdUczen=" & Student.Item("IdUczen").ToString)
          Dim NewItem As New ListViewItem(P.Item("Przedmiot").ToString, NG)
          NewItem.UseItemStyleForSubItems = False
          lvNdst.Items.Add(NewItem)
        Next
      Next
      lvNdst.Enabled = If(lvNdst.Items.Count > 0, True, False)


      Dim Pasek, Dyplom As New List(Of OutstandingStudent)
      Dim Avg As Single = 0, Behavior As Byte = 0
      For Each S As DataRow In DS.Tables("Wynik").DefaultView.ToTable(True, "IdUczen").Rows
        Avg = CType(DS.Tables("Wynik").Compute("Avg(Waga)", "IdUczen=" & S.Item("IdUczen").ToString & " AND GetToAverage=1 AND Typ='P'"), Single)
        If DS.Tables("Wynik").Select("IdUczen=" & S.Item("IdUczen").ToString & " AND Typ='Z'").GetLength(0) > 0 Then
          Behavior = CType(DS.Tables("Wynik").Compute("Avg(Waga)", "IdUczen=" & S.Item("IdUczen").ToString & " AND Typ='Z'"), Byte)
        Else
          Behavior = 0
        End If
        If Avg >= PasekParams.Avg AndAlso Behavior >= PasekParams.Behavior Then
          Pasek.Add(New OutstandingStudent With {.StudentAvg = Math.Round(Avg, 2).ToString("0.00"),
                                                  .StudentBehavior = DS.Tables("Wynik").Select("IdUczen=" & S.Item("IdUczen").ToString)(0).Item("Wynik").ToString,
                                                  .StudentName = DS.Tables("Wynik").Select("IdUczen=" & S.Item("IdUczen").ToString)(0).Item("Student").ToString})
          DS.Tables("Wynik").Select("IdUczen=" & S.Item("IdUczen").ToString)(0).Delete()
        ElseIf Avg >= DyplomParams.Avg AndAlso Behavior >= DyplomParams.Behavior Then
          Dyplom.Add(New OutstandingStudent With {.StudentAvg = Math.Round(Avg, 2).ToString("0.00"),
                                                  .StudentBehavior = DS.Tables("Wynik").Select("IdUczen=" & S.Item("IdUczen").ToString)(0).Item("Wynik").ToString,
                                                  .StudentName = DS.Tables("Wynik").Select("IdUczen=" & S.Item("IdUczen").ToString)(0).Item("Student").ToString})
          DS.Tables("Wynik").Select("IdUczen=" & S.Item("IdUczen").ToString)(0).Delete()
        End If
      Next
      Dim lvPasek As ListView = CType(tcWykaz.TabPages("tp3").Controls("lvPasek"), ListView)
      lvPasek.Groups.Clear()
      lvPasek.Items.Clear()

      For Each Student As OutstandingStudent In Pasek
        Dim NewItem As New ListViewItem(Student.StudentName)
        NewItem.UseItemStyleForSubItems = False
        NewItem.SubItems.Add(Student.StudentBehavior)
        NewItem.SubItems.Add(Student.StudentAvg)
        lvPasek.Items.Add(NewItem)
      Next

      lvPasek.Enabled = If(lvPasek.Items.Count > 0, True, False)
      'lvPasek.Dispose()
      Dim lvDyplom As ListView = CType(tcWykaz.TabPages("tp3").Controls("lvDyplom"), ListView)
      lvDyplom.Groups.Clear()
      lvDyplom.Items.Clear()
      For Each Student As OutstandingStudent In Dyplom
        Dim NewItem As New ListViewItem(Student.StudentName)
        NewItem.UseItemStyleForSubItems = False
        NewItem.SubItems.Add(Student.StudentBehavior)
        NewItem.SubItems.Add(Student.StudentAvg)
        lvDyplom.Items.Add(NewItem)
      Next
      lvDyplom.Enabled = If(lvDyplom.Items.Count > 0, True, False)

      Dim lvDyplomF As ListView = CType(tcWykaz.TabPages("tp3").Controls("lvDyplomF"), ListView)
      lvDyplomF.Groups.Clear()
      lvDyplomF.Items.Clear()
      For Each Student As DataRow In DS.Tables("StudentByKlasa").Rows
        Dim Frekwencja As Single = ComputeAbsence(Student.Item("ID").ToString, CType(Student.Item("DataAktywacji"), Date), DS.Tables("CountAbsenceByStudent")).Frekwencja
        If Frekwencja >= DyplomFParam Then
          Dim NewItem As New ListViewItem(Student.Item("Student").ToString)
          NewItem.UseItemStyleForSubItems = False
          NewItem.SubItems.Add(Math.Round(Frekwencja, 2).ToString("0.00"))
          'NewItem.SubItems.Add(Student.StudentAvg)
          lvDyplomF.Items.Add(NewItem)

        End If

      Next
      lvDyplomF.Enabled = If(lvDyplomF.Items.Count > 0, True, False)
    End If

    For i As Integer = 0 To 5
      tlpZachowanie.GetControlFromPosition(i, 2).Text = If(DS.Tables("CountZachowanie").Select("Waga=" & tlpZachowanie.GetControlFromPosition(i, 2).Tag.ToString).GetLength(0) > 0, DS.Tables("CountZachowanie").Select("Waga=" & tlpZachowanie.GetControlFromPosition(i, 2).Tag.ToString)(0).Item("LO").ToString, "0")
    Next
    'Dim StudentNkl As String = ""
    'For Each S As DataRow In DS.Tables("StudentByNkl").Rows
    '  StudentNkl += S.Item("IdUczen").ToString & ","
    'Next

    lvNkl.Items.Clear()
    For Each Student As DataRow In DS.Tables("StudentByNkl").Rows
      Dim NewItem As New ListViewItem(Student.Item("Student").ToString)
      'NewItem.ForeColor = Color.Red
      Dim Absence As Absencja = ComputeAbsence(Student.Item("IdUczen").ToString, CType(Student.Item("DataAktywacji"), Date), DS.Tables("CountAbsenceByStudent"))
      NewItem.Text += " " & Chr(150) & " " & Absence.ProcentNieobecnosci & " nb"
      NewItem.ToolTipText = "Liczba lekcji: " & Absence.LiczbaLekcji & vbNewLine & "Liczba nieobecności: " & Absence.LiczbaNieobecnosci
      NewItem.UseItemStyleForSubItems = False
      lvNkl.Items.Add(NewItem)
    Next
    lvNkl.Enabled = If(lvNkl.Items.Count > 0, True, False)

    'lvNdst.Groups.Clear()
    'lvNdst.Items.Clear()
    'For Each Student As DataRow In DS.Tables("CountNdstByStudent").Select(If(StudentNkl.Length > 0, "IdUczen NOT IN (" & StudentNkl.TrimEnd(",".ToCharArray) & ")", ""))

    ''For Each Student As DataRow In DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Rows
    'For Each Student As DataRow In DS.Tables("CountNdstByWaga").DefaultView.ToTable(True, "IdUczen").Select("IdUczen NOT IN ("& tot
    '  'Dim LO As Byte = CType(DS.Tables("CountNdstByWaga").Compute("Sum(LO)", "IdUczen=" & Student.Item("IdUczen").ToString), Byte)
    '  tutaj blad
    '  If CType(DS.Tables("CountNdstByWaga").Compute("Sum(LO)", "IdUczen=" & Student.Item("IdUczen").ToString & " AND Waga=1"), Byte) > 2 OrElse CType(DS.Tables("CountNdstByWaga").Compute("Sum(LO)", "IdUczen=" & Student.Item("IdUczen").ToString & " AND Waga=0"), Byte) > 0 Then
    '    Dim Grupa As String = DS.Tables("StudentByNdst").Select("IdUczen=" & Student.Item("IdUczen").ToString)(0).Item("Student").ToString
    '    Dim NG As New ListViewGroup(Grupa, Grupa.ToUpper)
    '    NG.HeaderAlignment = HorizontalAlignment.Left
    '    lvNdst.Groups.Add(NG)
    '    lvNdst.Groups(Grupa).Tag = Student.Item("IdUczen").ToString
    '    For Each P As DataRow In DS.Tables("StudentByNdst").Select("IdUczen=" & Student.Item("IdUczen").ToString)
    '      Dim NewItem As New ListViewItem(P.Item("Przedmiot").ToString, NG)
    '      NewItem.UseItemStyleForSubItems = False
    '      lvNdst.Items.Add(NewItem)
    '    Next
    '  End If
    'Next
    'lvNdst.Enabled = If(lvNdst.Items.Count > 0, True, False)

    lvZachowanie.Items.Clear()
    For Each Student As DataRow In DS.Tables("StudentByNg").Rows
      'For Each Student As DataRow In DS.Tables("StudentByNg").Select(If(StudentNkl.Length > 0, "IdUczen NOT IN (" & StudentNkl.TrimEnd(",".ToCharArray) & ")", ""))
      Dim NewItem As New ListViewItem(Student.Item("Student").ToString)
      lvZachowanie.Items.Add(NewItem)
    Next
    lvZachowanie.Enabled = If(lvZachowanie.Items.Count > 0, True, False)

    lvNdstByPrzedmiot.Items.Clear()
    Dim dtPrzedmiot As DataTable = New DataView(DS.Tables("CountNdstByPrzedmiot")).ToTable(True, "Nazwa", "Belfer")
    For Each P As DataRow In dtPrzedmiot.Rows
      Dim NewItem As New ListViewItem(P.Item("Nazwa").ToString)
      NewItem.UseItemStyleForSubItems = False
      NewItem.SubItems.Add(If(CType(DS.Tables("CountNdstByPrzedmiot").Compute("Count(Waga)", "Nazwa='" & P.Item("Nazwa").ToString & "' AND Waga=1"), Byte) = 0, "0", DS.Tables("CountNdstByPrzedmiot").Select("Nazwa='" & P.Item("Nazwa").ToString & "' AND Waga=1")(0).Item("LO").ToString))

      NewItem.SubItems.Add(If(CType(DS.Tables("CountNdstByPrzedmiot").Compute("Count(Waga)", "Nazwa='" & P.Item("Nazwa").ToString & "' AND Waga=0"), Byte) = 0, "0", DS.Tables("CountNdstByPrzedmiot").Select("Nazwa='" & P.Item("Nazwa").ToString & "' AND Waga=0")(0).Item("LO").ToString))

      NewItem.SubItems.Add(P.Item("Belfer").ToString)
      lvNdstByPrzedmiot.Items.Add(NewItem)
    Next
    lvNdstByPrzedmiot.Enabled = If(lvNdstByPrzedmiot.Items.Count > 0, True, False)
    'If Okres = "R" Then
    'End If
  End Sub

  Private Overloads Function ComputeAbsence(IdUczen As String, DataAktywacji As Date, dtAbsencja As DataTable) As Absencja
    Dim Frekwencja, Absencja As Single
    Dim LiczbaNb, LiczbaLekcji As Integer

    LiczbaLekcji = CType(DS.Tables("Lekcja").Compute("Count(ID)", "Data>=#" & DataAktywacji & "# AND Grupa=0"), Integer)

    For Each G As DataRow In DS.Tables("GrupaPrzedmiotowa").Select("IdUczen=" & IdUczen)
      LiczbaLekcji += CType(DS.Tables("Lekcja").Compute("Count(ID)", "Data>=#" & DataAktywacji & "# AND Grupa>0 AND Przedmiot=" & G.Item("Przedmiot").ToString), Integer)
    Next
    If LiczbaLekcji = 0 Then Return New Absencja With {.ProcentNieobecnosci = "-", .LiczbaLekcji = "0", .LiczbaNieobecnosci = "?"}

    If dtAbsencja.Select("IdUczen=" & IdUczen).GetLength(0) > 0 Then LiczbaNb = CType(dtAbsencja.Compute("Sum(Abc)", "Data>=#" & DataAktywacji & "# AND IdUczen=" & IdUczen), Integer)
    Absencja = CType(Math.Round(LiczbaNb / LiczbaLekcji * 100, 2), Single)
    Absencja = CType(IIf(Absencja > 100, 100, Absencja), Single)
    Frekwencja = 100 - Absencja
    Return New Absencja With {.ProcentNieobecnosci = Absencja.ToString & "%", .LiczbaLekcji = LiczbaLekcji.ToString, .LiczbaNieobecnosci = LiczbaNb.ToString, .Frekwencja = Frekwencja}
  End Function
  Private Function CheckClasificationStatus(IdObsada As String, Okres As String) As GlobalValues.ReasonStatus
    Dim K As New KlasyfikacjaSQL, DBA As New DataBaseAction, R As MySqlDataReader = Nothing
    Try
      R = DBA.GetReader(K.SelectClasification(IdObsada, Okres))
      If R.Read Then
        'Status = R.GetByte("Status")
        Return CType(R.GetByte("Status"), GlobalValues.ReasonStatus)
      End If

      Return GlobalValues.ReasonStatus.Brak
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    Finally
      R.Close()
    End Try
  End Function
  Private Sub EnableButton(Value As Boolean)
    cmdAddNew.Enabled = Value
    cmdDelete.Enabled = Value
    cmdSend.Enabled = Value
    cmdPrint.Enabled = Value
    cmdRefresh.Enabled = Value
  End Sub

  Private Sub SetVisibility(value As Boolean)
    tlpKlasyfikacja.Visible = value
    tlpZachowanie.Visible = value
    tlpKlasyfikacjaRoczna.Visible = value
    tcWykaz.Visible = value
    lblRP.Visible = value
    lblDataRP.Visible = value
    lblAvg.Visible = value
    labelAvg.Visible = value
  End Sub
  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Try
      Dim K As New KlasyfikacjaSQL, DBA As New DataBaseAction, cmd As MySqlCommand
      Dim Semestr As RadioButton
      Semestr = Me.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
      cmd = DBA.CreateCommand(K.InsertClasification)
      cmd.Parameters.AddWithValue("?IdObsada", CType(cbKlasa.SelectedItem, SchoolClassComboItem).IdObsadaWychowawstwa)
      cmd.Parameters.AddWithValue("?Okres", Semestr.Tag.ToString)
      cmd.Parameters.AddWithValue("?Status", 0)
      cmd.ExecuteNonQuery()
      RefreshData()

    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć wyniki klasyfikacji dla wskazanej klasy?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Try
        Dim DBA As New DataBaseAction, K As New KlasyfikacjaSQL
        Dim Semestr As RadioButton
        Semestr = Me.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
        Dim cmd As MySqlCommand = DBA.CreateCommand(K.DeleteClasification)
        cmd.Parameters.AddWithValue("?IdObsada", CType(cbKlasa.SelectedItem, SchoolClassComboItem).IdObsadaWychowawstwa)
        cmd.Parameters.AddWithValue("?Okres", Semestr.Tag.ToString)
        cmd.ExecuteNonQuery()
        RefreshData()
      Catch mex As MySqlException
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub

  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    RefreshData()
  End Sub
  Private Sub AddTabPages()
    Dim tp2 As New TabPage With {.Name = "tp2", .Text = "Wykaz uczniów do egzaminów poprawkowych", .Tag = "Wykaz uczniów, którzy przystąpią do egzaminów poprawkowych"}
    Dim tp3 As New TabPage With {.Name = "tp3", .Text = "Wykaz uczniów do nagród i dyplomów"}
    Dim lvPoprawka1 As New ListView With {.Name = "lvPoprawka1", .Location = New Point(6, 6), .Size = New Size(428, 370), .Tag = "z jedną oceną niedostateczną"}
    tp2.Controls.Add(lvPoprawka1)
    Dim lvPoprawka2 As New ListView With {.Name = "lvPoprawka2", .Location = New Point(438, 6), .Size = New Size(428, 370), .Tag = "z dwiema ocenami niedostatecznymi"}
    tp2.Controls.Add(lvPoprawka2)
    Dim lvPasek As New ListView With {.Name = "lvPasek", .Location = New Point(3, 6), .Size = New Size(537, 183), .Tag = "Wykaz uczniów ze średnią ocen 4,75 i wyżej oraz co najmniej bardzo dobrą oceną zachowania"}
    tp3.Controls.Add(lvPasek)
    Dim lvDyplom As New ListView With {.Name = "lvDyplom", .Location = New Point(3, 193), .Size = New Size(537, 182), .Tag = "Wykaz uczniów ze średnią ocen 4,5 i wyżej oraz co najmniej bardzo dobrą oceną zachowania"}
    tp3.Controls.Add(lvDyplom)
    Dim lvDyplomF As New ListView With {.Name = "lvDyplomF", .Location = New Point(546, 6), .Size = New Size(320, 370), .Tag = "Wykaz uczniów do dyplomów za wzorową frekwencję"}
    tp3.Controls.Add(lvDyplomF)

    Dim PoprawkaCols1 As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Poprawka1", .Text = "Z jedną oceną niedostateczną", .Width = 408, .TextAlign = HorizontalAlignment.Left}}
    Dim PoprawkaCols2 As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Poprawka2", .Text = "Z dwiema ocenami niedostatecznymi", .Width = 408, .TextAlign = HorizontalAlignment.Left}}
    Dim PasekCols As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Student", .Text = "Uczeń", .Width = 200}, New ColumnHeader With {.Name = "Zachowanie", .Text = "Zachowanie", .Width = 200, .TextAlign = HorizontalAlignment.Center}, New ColumnHeader With {.Name = "Avg", .Text = "Średnia", .Width = 117, .TextAlign = HorizontalAlignment.Center}}
    Dim DyplomCols As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Student", .Text = "Uczeń", .Width = 200}, New ColumnHeader With {.Name = "Zachowanie", .Text = "Zachowanie", .Width = 200, .TextAlign = HorizontalAlignment.Center}, New ColumnHeader With {.Name = "Avg", .Text = "Średnia", .Width = 117, .TextAlign = HorizontalAlignment.Center}}
    Dim DyplomFCols As New List(Of ColumnHeader) From {New ColumnHeader With {.Name = "Student", .Text = "Uczeń", .Width = 200}, New ColumnHeader With {.Name = "Frekwencja", .Text = "Frekwencja", .Width = 100, .TextAlign = HorizontalAlignment.Center}}
    ListViewConfig(lvPoprawka1)
    ListViewConfig(lvPoprawka2)
    ListViewConfig(lvPasek)
    ListViewConfig(lvDyplom)
    ListViewConfig(lvDyplomF)

    'lvPoprawka1.OwnerDraw = False
    'lvPoprawka2.OwnerDraw = False
    'lvPasek.OwnerDraw = False
    'lvDyplom.OwnerDraw = False
    'lvDyplomF.OwnerDraw = False
    RemoveHandler lvPoprawka1.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    AddHandler lvPoprawka1.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    RemoveHandler lvPoprawka2.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    AddHandler lvPoprawka2.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    RemoveHandler lvPasek.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    AddHandler lvPasek.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    RemoveHandler lvDyplom.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    AddHandler lvDyplom.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    RemoveHandler lvDyplomF.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader
    AddHandler lvDyplomF.DrawColumnHeader, AddressOf lvNdstByPrzedmiot_DrawColumnHeader

    RemoveHandler lvPoprawka1.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    AddHandler lvPoprawka1.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    RemoveHandler lvPoprawka2.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    AddHandler lvPoprawka2.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    RemoveHandler lvPasek.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    AddHandler lvPasek.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    RemoveHandler lvDyplom.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    AddHandler lvDyplom.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    RemoveHandler lvDyplomF.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem
    AddHandler lvDyplomF.DrawSubItem, AddressOf lvNdstByPrzedmiot_DrawSubItem


    AddListViewColumn(lvPoprawka1, PoprawkaCols1)
    AddListViewColumn(lvPoprawka2, PoprawkaCols2)
    AddListViewColumn(lvPasek, PasekCols)
    AddListViewColumn(lvDyplom, DyplomCols)
    AddListViewColumn(lvDyplomF, DyplomFCols)
    tcWykaz.Controls.Add(tp2)
    tcWykaz.Controls.Add(tp3)
  End Sub

  Private Sub lvNdstByPrzedmiot_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles lvNdstByPrzedmiot.DrawColumnHeader, lvNdst.DrawColumnHeader, lvNkl.DrawColumnHeader, lvZachowanie.DrawColumnHeader
    'e.Graphics.FillRectangle(Brushes.LightGray, e.ColumnHeaderBounds.BoundsOuter)
    'e.Graphics.FillRectangle(New SolidBrush(SystemColors.ButtonFace), e.Bounds)
    e.Graphics.DrawLine(Pens.White, e.Bounds.Left, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom)
    'e.Graphics.DrawLine(Pens.Black, e.Bounds.X, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1)
    'e.DrawBackground()
    'e.DrawText()
    e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds)
    Dim sf As New StringFormat()
    sf.LineAlignment = StringAlignment.Center
    'Select Case e.Header.TextAlign
    '  Case HorizontalAlignment.Center
    '    sf.Alignment = StringAlignment.Center
    '  Case HorizontalAlignment.Right
    '    sf.Alignment = StringAlignment.Far
    'End Select
    sf.Alignment = StringAlignment.Center
    Dim headerFont As New Font(e.Font, FontStyle.Bold)
    Try
      e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Navy, e.Bounds, sf)
    Finally
      headerFont.Dispose()
    End Try
  End Sub


  Private Sub lvNdstByPrzedmiot_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles lvNdstByPrzedmiot.DrawSubItem, lvNdst.DrawSubItem, lvNkl.DrawSubItem, lvZachowanie.DrawSubItem
    e.DrawText()

  End Sub

  Private Sub cmdSend_Click(sender As Object, e As EventArgs) Handles cmdSend.Click
    If MessageBox.Show("Po przekazaniu wyników klasyfikacji do dyrekcji, nie będzie już możliwa edycja ocen." & vbNewLine & "Czy na pewno chcesz przekazać wyniki klasyfikacji dla wskazanej klasy?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim K As New KlasyfikacjaSQL, DBA As New DataBaseAction, cmd As MySqlCommand, T As MySqlTransaction = GlobalValues.gblConn.BeginTransaction, ObsadaToLock As String
      Try
        Dim Typ As RadioButton
        Typ = Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
        Dim Zakres As String
        Zakres = If(Typ.Tag.ToString = "S", "'C1','S'", "'C2','R'")
        cmd = DBA.CreateCommand(K.SelectObsadaToLock(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
        cmd.Transaction = T
        ObsadaToLock = cmd.ExecuteScalar().ToString

        cmd.CommandText = K.UpdateColumnLock("1", Zakres, ObsadaToLock)
        cmd.ExecuteNonQuery()

        cmd.CommandText = K.ChangeStatus()
        cmd.Parameters.AddWithValue("?Status", GlobalValues.ReasonStatus.Przekazane)
        cmd.Parameters.AddWithValue("?Typ", Typ.Tag.ToString)
        cmd.Parameters.AddWithValue("?IdObsada", CType(cbKlasa.SelectedItem, SchoolClassComboItem).IdObsadaWychowawstwa)
        cmd.ExecuteNonQuery()
        T.Commit()
        lblStatus.ForeColor = Color.Green
        lblStatus.Text = GlobalValues.ReasonStatus.Przekazane.ToString
        cmdSend.Enabled = False
        cmdDelete.Enabled = False
        'RefreshData()
      Catch mex As MySqlException
        T.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Catch ex As Exception
        T.Rollback()
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
    End If

  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Try
      Dim PP As New dlgPrintPreview ', DSP As New DataSet
      PP.Doc = New PrintReport(Nothing)
      My.Settings.Landscape = True
      PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
      PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
      PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
      PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
      PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin
      RemoveHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
      AddHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
      RemoveHandler NewRow, AddressOf PP.NewRow
      AddHandler NewRow, AddressOf PP.NewRow
      RemoveHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
      AddHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
      If Controls.OfType(Of RadioButton).Where(Function(r) r.Checked = True).FirstOrDefault().Tag.ToString = "R" Then
        RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage_RokSzkolny
        AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage_RokSzkolny
        PP.Doc.ReportHeader = New String() {"Wyniki klasyfikacji rocznej uczniów klasy " & cbKlasa.Text}
      Else
        RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage_Semestr
        AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage_Semestr
        PP.Doc.ReportHeader = New String() {"Wyniki klasyfikacji śródrocznej uczniów klasy " & cbKlasa.Text}
      End If
      'PP.Doc.ReportHeader = New String() {"Wyniki klasyfikacji " & If(Controls.OfType(Of RadioButton).Where(Function(r) r.Checked = True).FirstOrDefault().Tag.ToString = "S", "śródrocznej", "rocznej") & " uczniów klasy " & cbKlasa.Text}
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
  Public Sub PrnDoc_PrintPage_RokSzkolny(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    Dim Doc As PrintReport = CType(sender, PrintReport)
    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)

    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim SubHeaderFont As Font = My.Settings.SubHeaderFont
    Dim SubHeaderLineHeight As Single = SubHeaderFont.GetHeight(e.Graphics)
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
      PrintMeta(x, y, LineHeight, PrintWidth, TextFont)

      Dim ColFactor As Integer = 7
      PrintKlasyfikacja(tlpKlasyfikacjaRoczna, ColFactor, x, y, LineHeight, PrintWidth, TextFont)

      ColFactor = 6
      PrintBehaviour(ColFactor, x, y, LineHeight, PrintWidth, TextFont)

    End If
    If GetNklList(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont) = False Then Exit Sub

    If GetNdstList(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont) = False Then Exit Sub

    If GetBehaviourList(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont) = False Then Exit Sub

    Dim y1 As Single = y
    Dim x1 As Single = CType(x + PrintWidth / 2, Single)
    Dim lvPoprawka1 As ListView = CType(tcWykaz.TabPages("tp2").Controls("lvPoprawka1"), ListView)
    Dim lvPoprawka2 As ListView = CType(tcWykaz.TabPages("tp2").Controls("lvPoprawka2"), ListView)
    Dim PoprawkaMaxCount As Integer = If(lvPoprawka1.Items.Count > lvPoprawka2.Items.Count, lvPoprawka1.Items.Count, lvPoprawka2.Items.Count)
    While Offset(4) < PoprawkaMaxCount AndAlso PrintHeight >= y + LineHeight * (PoprawkaMaxCount + 2)
      If Offset(4) = 0 Then
        y += LineHeight
        PH.DrawText(lvPoprawka1.Parent.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
        PH.DrawLine(x, y + SubHeaderLineHeight, x + PrintWidth, y + SubHeaderLineHeight)
        y += SubHeaderLineHeight * CSng(1.5)
        y1 = y
      End If
      With lvPoprawka1
        If .Items.Count > 0 Then
          PH.DrawText(.Tag.ToString, New Font(TextFont, FontStyle.Bold Or FontStyle.Underline), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          y += LineHeight * CSng(1.5)
          For Each Item As ListViewItem In .Items
            PH.DrawText(Item.Text.Substring(0, Item.Text.IndexOf("-") - 1), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
            PH.DrawText(String.Concat(" ", Chr(150), " ", Item.Text.Substring(Item.Text.IndexOf("-") + 2)), TextFont, x + e.Graphics.MeasureString(Item.Text.Substring(0, Item.Text.IndexOf("-") - 1), New Font(TextFont, FontStyle.Bold)).Width, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
            y += LineHeight
            Offset(4) += 1
          Next
        Else
          x1 = x
        End If
      End With
      With lvPoprawka2
        If .Items.Count > 0 Then
          PH.DrawText(.Tag.ToString, New Font(TextFont, FontStyle.Bold Or FontStyle.Underline), x1, y1, PrintWidth, LineHeight, 0, Brushes.Black, False)
          y1 += LineHeight * CSng(1.5)
          For Each Item As ListViewItem In .Items
            PH.DrawText(Item.Text.Substring(0, Item.Text.IndexOf("-") - 1), New Font(TextFont, FontStyle.Bold), x1, y1, PrintWidth, LineHeight, 0, Brushes.Black, False)
            PH.DrawText(String.Concat(" ", Chr(150), " ", Item.Text.Substring(Item.Text.IndexOf("-") + 2)), TextFont, x1 + e.Graphics.MeasureString(Item.Text.Substring(0, Item.Text.IndexOf("-") - 1), New Font(TextFont, FontStyle.Bold)).Width, y1, PrintWidth, LineHeight, 0, Brushes.Black, False)
            y1 += LineHeight
            Offset(4) += 1
          Next
        End If
      End With
      If y1 > y Then y = y1
    End While
    If Offset(4) < PoprawkaMaxCount Then
      e.HasMorePages = True
      RaiseEvent NewRow()
      Exit Sub
    End If

    'If NewPage Then
    '  e.HasMorePages = True
    '  RaiseEvent NewRow()
    '  NewPage = False
    '  Exit Sub
    'End If

    Dim lvPasek As ListView = CType(tcWykaz.TabPages("tp3").Controls("lvPasek"), ListView)
    With lvPasek
      While Offset(5) < .Items.Count AndAlso PrintHeight >= y + LineHeight * (.Items.Count + 1)
        If Offset(5) = 0 Then
          y += LineHeight
          PH.DrawText(.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
          PH.DrawLine(x, y + SubHeaderLineHeight, x + PrintWidth, y + SubHeaderLineHeight)
          y += SubHeaderLineHeight * CSng(1.5)

        End If
        For Each Item As ListViewItem In .Items
          PH.DrawText(Item.Text, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          PH.DrawText(String.Concat(" ", Chr(150), " zachowanie: ", Item.SubItems(1).Text, "; średnia ocen: ", Item.SubItems(2).Text), TextFont, x + e.Graphics.MeasureString(Item.Text, New Font(TextFont, FontStyle.Bold)).Width, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          y += LineHeight
          Offset(5) += 1
        Next
        'y += LineHeight
      End While
      If Offset(5) < .Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      End If
    End With
    Dim lvDyplom As ListView = CType(tcWykaz.TabPages("tp3").Controls("lvDyplom"), ListView)
    With lvDyplom
      While Offset(6) < .Items.Count AndAlso PrintHeight >= y + LineHeight * (.Items.Count + 1)
        If Offset(6) = 0 Then
          y += LineHeight
          PH.DrawText(.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
          PH.DrawLine(x, y + SubHeaderLineHeight, x + PrintWidth, y + SubHeaderLineHeight)
          y += SubHeaderLineHeight * CSng(1.5)
        End If
        For Each Item As ListViewItem In .Items
          PH.DrawText(Item.Text, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          PH.DrawText(String.Concat(" ", Chr(150), " zachowanie: ", Item.SubItems(1).Text, "; średnia ocen: ", Item.SubItems(2).Text), TextFont, x + e.Graphics.MeasureString(Item.Text, New Font(TextFont, FontStyle.Bold)).Width, y, PrintWidth, LineHeight, 0, Brushes.Black, False)

          y += LineHeight
          Offset(6) += 1
        Next
      End While
      If Offset(6) < .Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      End If
    End With
    Dim lvDyplomF As ListView = CType(tcWykaz.TabPages("tp3").Controls("lvDyplomF"), ListView)
    With lvDyplomF
      While Offset(7) < .Items.Count AndAlso PrintHeight >= y + LineHeight * (.Items.Count + 1)
        If Offset(7) = 0 Then
          y += LineHeight
          PH.DrawText(.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
          PH.DrawLine(x, y + SubHeaderLineHeight, x + PrintWidth, y + SubHeaderLineHeight)
          y += SubHeaderLineHeight * CSng(1.5)
        End If
        For Each Item As ListViewItem In .Items
          PH.DrawText(Item.Text, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          PH.DrawText(String.Concat(" ", Chr(150), " frekwencja: ", Item.SubItems(1).Text), TextFont, x + e.Graphics.MeasureString(Item.Text, New Font(TextFont, FontStyle.Bold)).Width, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          y += LineHeight
          Offset(7) += 1
        Next
      End While
    End With
    If GetSubjectListByNdst(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont, Offset(8)) = False Then Exit Sub

    If Offset(8) < lvNdstByPrzedmiot.Items.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
      Exit Sub
    Else
      PageNumber = 0
      'NewPage = True
      For i = 0 To 8
        Offset(i) = 0
      Next
    End If
  End Sub
  Public Sub PrnDoc_PrintPage_Semestr(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    Dim Doc As PrintReport = CType(sender, PrintReport)
    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)

    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim SubHeaderFont As Font = My.Settings.SubHeaderFont
    Dim SubHeaderLineHeight As Single = SubHeaderFont.GetHeight(e.Graphics)
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    Dim PrintHeight As Integer = e.MarginBounds.Bottom

    '---------------------------------------- Nagłówek i stopka ----------------------------
    PH.DrawHeader(x, y, PrintWidth)
    PH.DrawFooter(x, PrintHeight, PrintWidth)
    PageNumber += 1
    PH.DrawPageNumber("- " & PageNumber.ToString & " -", x, y, PrintWidth)
    'Dim ColOffset As Integer = 0
    If PageNumber = 1 Then
      y += LineHeight
      PH.DrawText(Doc.ReportHeader(0), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      y += HeaderLineHeight * 2
      PrintMeta(x, y, LineHeight, PrintWidth, TextFont)

      Dim ColFactor As Integer = 5
      Dim Tabela As TableLayoutPanel = tlpKlasyfikacja
      PrintKlasyfikacja(Tabela, ColFactor, x, y, LineHeight, PrintWidth, TextFont)
      
      ColFactor = 6
      PrintBehaviour(ColFactor, x, y, LineHeight, PrintWidth, TextFont)

    End If
    If GetNklList(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont) = False Then Exit Sub

    If GetNdstList(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont) = False Then Exit Sub

    If GetBehaviourList(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont) = False Then Exit Sub

    'If NewPage Then
    '  e.HasMorePages = True
    '  RaiseEvent NewRow()
    '  NewPage = False
    '  Exit Sub
    'End If
    If GetSubjectListByNdst(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont, Offset(4)) = False Then Exit Sub

    If Offset(4) < lvNdstByPrzedmiot.Items.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
      Exit Sub
    Else
      PageNumber = 0
      'NewPage = True
      For i = 0 To 4
        Offset(i) = 0
      Next
    End If

  End Sub
  Private Sub PrintMeta(ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, TextFont As Font)
    Dim ColOffset As Integer = 0
    Dim Kolumna As New List(Of Pole) From
        {
          New Pole With {.Name = "DataRP", .Label = "Data klasyfikacyjnego posiedzenia Rady Pedagogicznej", .Size = CType(PrintWidth / 3, Integer)},
          New Pole With {.Name = "Wychowawca", .Label = "Wychowawca klasy", .Size = CType(PrintWidth / 3, Integer)},
          New Pole With {.Name = "Avg", .Label = "Średnia ocen klasy", .Size = CType(PrintWidth / 3, Integer)}
        }
    For Each Col In Kolumna
      PH.DrawText(Col.Label, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, Col.Size, LineHeight * CSng(2), 1, Brushes.Black)
      ColOffset += Col.Size
    Next
    y += LineHeight * CSng(2)
    ColOffset = 0
    PH.DrawText(DataRP.ToShortDateString, TextFont, x + ColOffset, y, Kolumna.Item(0).Size, LineHeight * CSng(1.5), 1, Brushes.Black)
    ColOffset += Kolumna.Item(0).Size
    PH.DrawText(lblWychowawca.Text, TextFont, x + ColOffset, y, Kolumna.Item(1).Size, LineHeight * CSng(1.5), 1, Brushes.Black)
    ColOffset += Kolumna.Item(1).Size
    PH.DrawText(lblAvg.Text, TextFont, x + ColOffset, y, Kolumna.Item(2).Size, LineHeight * CSng(1.5), 1, Brushes.Black)
    y += LineHeight * CSng(2.5)
  End Sub
  Private Sub PrintKlasyfikacja(Tabela As TableLayoutPanel, ColFactor As Integer, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, TextFont As Font)
    Dim ColOffset As Integer = 0
    Dim TotalColSize As Integer = 60
    Dim DataColSize As Integer = CType((PrintWidth - TotalColSize) / ColFactor, Integer)
    With Tabela
      ColOffset = 0
      PH.DrawText(CType(.GetControlFromPosition(0, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, TotalColSize, LineHeight * CSng(3), 1, Brushes.Black)
      ColOffset += TotalColSize
      PH.DrawText(CType(.GetControlFromPosition(1, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize * ColFactor, LineHeight, 1, Brushes.Black)
      y += LineHeight
      PH.DrawText(hKL.Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize, LineHeight * 2, 1, Brushes.Black)
      For i As Integer = 1 To .ColumnCount - 1
        If .GetColumnSpan(CType(.GetControlFromPosition(i, 1), Label)) > 1 Then
          PH.DrawText(CType(.GetControlFromPosition(i, 1), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize * 2, LineHeight, 1, Brushes.Black)
          ColOffset += DataColSize * 2
          y += LineHeight
          ColOffset -= DataColSize * 2
          For j As Integer = i To i + 1
            PH.DrawText(CType(.GetControlFromPosition(j, 2), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize, LineHeight, 1, Brushes.Black)
            ColOffset += DataColSize
          Next
          y -= LineHeight
          i += 1
        Else
          PH.DrawText(CType(.GetControlFromPosition(i, 1), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize, LineHeight * 2, 1, Brushes.Black)
          ColOffset += DataColSize
        End If
      Next
      y += LineHeight * CSng(2)
      ColOffset = 0
      PH.DrawText(CType(.GetControlFromPosition(0, .RowCount - 1), Label).Text, TextFont, x + ColOffset, y, TotalColSize, LineHeight, 1, Brushes.Black)
      ColOffset += TotalColSize
      For i As Integer = 1 To .ColumnCount - 1
        PH.DrawText(CType(.GetControlFromPosition(i, .RowCount - 1), Label).Text, TextFont, x + ColOffset, y, DataColSize, LineHeight, 1, Brushes.Black)
        ColOffset += DataColSize
      Next
    End With
    y += LineHeight * 2
  End Sub
  Private Sub PrintBehaviour(ColFactor As Integer, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, TextFont As Font)
    Dim ColOffset As Integer = 0

    Dim DataColSize As Integer = CType(PrintWidth / ColFactor, Integer)
    With tlpZachowanie
      PH.DrawText(CType(.GetControlFromPosition(0, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x, y, DataColSize * .GetColumnSpan(CType(.GetControlFromPosition(0, 0), Label)), LineHeight, 1, Brushes.Black)
      y += LineHeight
      ColOffset = 0
      For j As Integer = 0 To .ColumnCount - 1
        PH.DrawText(CType(.GetControlFromPosition(j, 1), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize, LineHeight * CSng(1.5), 1, Brushes.Black)
        ColOffset += DataColSize
      Next
      y += LineHeight * CSng(1.5)
      ColOffset = 0
      For j As Integer = 0 To .ColumnCount - 1
        PH.DrawText(CType(.GetControlFromPosition(j, 2), Label).Text, TextFont, x + ColOffset, y, DataColSize, LineHeight, 1, Brushes.Black)
        ColOffset += DataColSize
      Next
      y += LineHeight
    End With
  End Sub
  Private Function GetNklList(e As PrintPageEventArgs, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, PrintHeight As Integer, TextFont As Font) As Boolean
    Dim SubHeaderFont As Font = My.Settings.SubHeaderFont
    Dim SubHeaderLineHeight As Single = SubHeaderFont.GetHeight(e.Graphics)

    With lvNkl
      While Offset(0) < .Items.Count And PrintHeight >= y + LineHeight * (.Items.Count + 1)
        If Offset(0) = 0 Then
          y += LineHeight
          PH.DrawText(.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
          PH.DrawLine(x, y + SubHeaderLineHeight, x + PrintWidth, y + SubHeaderLineHeight)
          y += SubHeaderLineHeight * CSng(1.5)
        End If

        For Each Item As ListViewItem In .Items
          PH.DrawText(Item.Text.Substring(0, Item.Text.IndexOf(Chr(150)) - 1), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          PH.DrawText(Item.Text.Substring(Item.Text.IndexOf(Chr(150)) - 1), TextFont, x + e.Graphics.MeasureString(Item.Text.Substring(0, Item.Text.IndexOf(Chr(150)) - 1), New Font(TextFont, FontStyle.Bold)).Width, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          y += LineHeight
          Offset(0) += 1
        Next
        'y += LineHeight
      End While
      If Offset(0) < .Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Return False
        'Exit Function
      End If
    End With
    Return True
  End Function
  Private Function GetNdstList(e As PrintPageEventArgs, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, PrintHeight As Integer, TextFont As Font) As Boolean
    Dim SubHeaderFont As Font = My.Settings.SubHeaderFont
    Dim SubHeaderLineHeight As Single = SubHeaderFont.GetHeight(e.Graphics)

    With lvNdst
      While Offset(1) < .Groups.Count AndAlso PrintHeight >= y + LineHeight * 2
        If Offset(1) = 0 Then
          y += LineHeight
          PH.DrawText(.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
          PH.DrawLine(x, y + SubHeaderLineHeight, x + PrintWidth, y + SubHeaderLineHeight)
          y += SubHeaderLineHeight * CSng(1.5)
        End If
        PH.DrawText(StrConv(.Groups(Offset(1)).Header, VbStrConv.ProperCase), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
        PH.DrawText(" " & Chr(150) & " ", TextFont, x + e.Graphics.MeasureString(StrConv(.Groups(Offset(1)).Header, VbStrConv.ProperCase), New Font(TextFont, FontStyle.Bold)).Width, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
        Dim Przedmiot As String = ""
        For Each Item As ListViewItem In .Groups(Offset(1)).Items
          Przedmiot += Item.Text & "; ,"
        Next
        If PH.DrawWrappedText(Przedmiot.TrimEnd("; ,".ToCharArray).Split(",".ToCharArray), TextFont, Offset(2), x, y, PrintWidth, PrintHeight, LineHeight, CType(e.Graphics.MeasureString(StrConv(.Groups(Offset(1)).Header, VbStrConv.ProperCase), New Font(TextFont, FontStyle.Bold)).Width + e.Graphics.MeasureString(" " & Chr(150) & " ", TextFont).Width, Integer)) = False Then
          e.HasMorePages = True
          RaiseEvent NewRow()
          Return False
        Else
        End If
        Offset(1) += 1
      End While
      If Offset(1) < .Groups.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Return False
      End If
    End With
    Return True
  End Function
  Private Function GetBehaviourList(e As PrintPageEventArgs, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, PrintHeight As Integer, TextFont As Font) As Boolean
    Dim SubHeaderFont As Font = My.Settings.SubHeaderFont
    Dim SubHeaderLineHeight As Single = SubHeaderFont.GetHeight(e.Graphics)

       With lvZachowanie
      While Offset(3) < .Items.Count AndAlso PrintHeight >= y + LineHeight * (.Items.Count + 1)
        If Offset(3) = 0 Then
          y += LineHeight
          PH.DrawText(.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
          PH.DrawLine(x, y + SubHeaderLineHeight, x + PrintWidth, y + SubHeaderLineHeight)
          y += SubHeaderLineHeight * CSng(1.5)
        End If
        For Each Item As ListViewItem In .Items
          PH.DrawText(Item.Text.Substring(0, Item.Text.IndexOf("-") - 1), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          PH.DrawText(String.Concat(" ", Chr(150), " ", Item.Text.Substring(Item.Text.IndexOf("-") + 2)), TextFont, x + e.Graphics.MeasureString(Item.Text.Substring(0, Item.Text.IndexOf("-") - 1), New Font(TextFont, FontStyle.Bold)).Width, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          y += LineHeight
          Offset(3) += 1
        Next
      End While
      If Offset(3) < .Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Return False
      End If
    End With
    Return True
  End Function
  Private Function GetSubjectListByNdst(e As PrintPageEventArgs, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, PrintHeight As Integer, TextFont As Font, ByRef Offset As Integer) As Boolean
    Dim SubHeaderFont As Font = My.Settings.SubHeaderFont
    Dim SubHeaderLineHeight As Single = SubHeaderFont.GetHeight(e.Graphics)
    Dim LpSize As Integer = 40
    Dim ColWidth As Single = CType((PrintWidth - LpSize) / 6, Single)
    Dim Kolumna As New List(Of Pole) From
    {
      New Pole With {.Name = "Przedmiot", .Label = "Przedmiot", .Size = CType(ColWidth * 2, Integer)},
      New Pole With {.Name = "LiczbaNdst", .Label = "Liczba uczniów z ocenami niedostatecznymi", .Size = CType(ColWidth, Integer)},
      New Pole With {.Name = "LiczbaNkl", .Label = "Liczba uczniów nieklasyfikowanych", .Size = CType(ColWidth, Integer)},
     New Pole With {.Name = "Nauczyciel", .Label = "Nauczyciel uczący", .Size = CType(ColWidth * 2, Integer)}
    }
    Dim ColOffset As Integer = 0
    With lvNdstByPrzedmiot
      While Offset < .Items.Count AndAlso PrintHeight >= y + LineHeight * (.Items.Count + 5)
        If Offset = 0 Then
          y += LineHeight
          PH.DrawText(.Tag.ToString, SubHeaderFont, x, y, PrintWidth, SubHeaderLineHeight, 0, Brushes.Black, False)
          y += SubHeaderLineHeight * CSng(1.5)
          PH.DrawText("L.p", New Font(TextFont, FontStyle.Bold), x + ColOffset, y, LpSize, LineHeight * CSng(3), 1, Brushes.Black)
          ColOffset += LpSize
          For Each Col In Kolumna
            PH.DrawText(Col.Label, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, Col.Size, LineHeight * CSng(3), 1, Brushes.Black)
            ColOffset += Col.Size
          Next
        End If
        y += LineHeight * 3
        For Each Item As ListViewItem In .Items
          ColOffset = 0
          PH.DrawText((Item.Index + 1).ToString, TextFont, x + ColOffset, y, LpSize, LineHeight, 1, Brushes.Black)
          ColOffset += LpSize
          Dim StrAlign As Integer = 0, j As Integer = 2
          For i As Integer = 0 To Kolumna.Count - 1
            PH.DrawText(Item.SubItems(i).Text, TextFont, x + ColOffset, y, Kolumna.Item(i).Size, LineHeight, CType(StrAlign, Byte), Brushes.Black)
            ColOffset += Kolumna.Item(i).Size
            j -= 1
            StrAlign += j 'CType(j - 1, Byte) 'CType(If(StrAlign = 0, 1, 0), Byte)

          Next
          y += LineHeight
          Offset += 1
        Next
      End While
      If Offset < .Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Return False
      End If
    End With
    Return True
  End Function
End Class

Public Class OutstandingStudent
  Public Property StudentName As String
  Public Property StudentAvg As String
  Public Property StudentBehavior As String
End Class
Public Class OutstandingParams
  Public Property Avg As Single
  'Public Property StudentAvg As String
  Public Property Behavior As Byte
End Class
Public Class DataTableContent
  Public Property TableName As String
  Public Property SelectString As String
End Class