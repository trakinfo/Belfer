Imports System.Drawing.Printing
Imports System.Xml
Public Class frmManagerPlanowLekcji
  Private dtPlan, dtLekcja As DataTable, Filter As String = "StartDate<=#" & Date.Today & "# AND EndDate>=#" & Date.Today & "#"
  Private InRefresh As Boolean = True
  Public LekcjaFilter As String = "Klasa"
  Private dx As Single = 0, dy As Single = 0
  Public Event NewRow()
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub frmPlanLekcji_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ManagePlanLekcjiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ManagePlanLekcjiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmPlanLekcji_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    InRefresh = True
    ListViewConfig(lvPlan)
    ListViewConfig(lvLekcja)
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator Then cmdAddPlan.Enabled = True
    EnableButtons(False)
    cmdPrint.Enabled = False
    lvPlan.Items.Clear()
    lvPlan.Enabled = False
    lvLekcja.Items.Clear()
    lvLekcja.Enabled = False
    FetchData()
    GetMetaData()
    ClearDetails()
    FillLekcjaFilter(cbLekcjaFilter)

  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      '.Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      SetColumns(lv)
      .Items.Clear()
      '.Enabled = False
    End With
  End Sub
  Private Sub SetColumns(lv As ListView)
    With lv
      If lv.Name = lvPlan.Name Then
        .Columns.Add("ID", 0, HorizontalAlignment.Center)
        .Columns.Add("Nazwa", 393, HorizontalAlignment.Left)
        .Columns.Add("Obowiązuje od", 125, HorizontalAlignment.Center)
        .Columns.Add("Obowiązuje do", 125, HorizontalAlignment.Center)
        .Columns.Add("Status", 150, HorizontalAlignment.Center)
      Else
        .Columns.Add("ID", 0, HorizontalAlignment.Center)
        .Columns.Add("Nr lekcji", 50, HorizontalAlignment.Center)
        .Columns.Add("Godz. lekcyjna", 100, HorizontalAlignment.Center)
        '.Columns.Add(IIf(LekcjaFilter = "Klasa", "Przedmiot {Nauczyciel}", "Klasa {Przedmiot}").ToString, 493, HorizontalAlignment.Left)
        .Columns.Add("Lekcja", 493, HorizontalAlignment.Left)
        .Columns.Add("Sala", 150, HorizontalAlignment.Center)
        .Columns.Add("IdGodzina", 0, HorizontalAlignment.Left)
        .Columns.Add("IdObsada", 0, HorizontalAlignment.Left)
      End If
    End With
  End Sub

  Private Sub FillLekcjaFilter(ByVal cb As ComboBox)
    Try
      cb.Items.Clear()
      Dim FCB As New FillComboBox, P As New PlanSQL
      Dim SelectString As String
      If LekcjaFilter = "Klasa" Then
        'Dim K As New KolumnaSQL
        SelectString = P.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear)
        FCB.AddComboBoxExtendedItems(cb, SelectString)
      Else
        'Dim P As New PlanSQL
        SelectString = P.SelectBelfer(My.Settings.IdSchool, My.Settings.SchoolYear)
        FCB.AddComboBoxComplexItems(cb, SelectString)
      End If

      cb.Enabled = CType(IIf(lvPlan.SelectedItems.Count > 0, True, False), Boolean)
      If cb.Items.Count > 0 Then
        Dim SH As New SeekHelper
        If LekcjaFilter = "Klasa" Then
          If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbLekcjaFilter, CType(My.Settings.ClassName, Integer))
        Else
          If My.Settings.IdBelfer.Length > 0 Then SH.FindComboItem(Me.cbLekcjaFilter, CType(My.Settings.IdBelfer, Integer))
        End If
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbLekcjaFilter.SelectionChangeCommitted
    If LekcjaFilter = "Klasa" Then
      My.Settings.ClassName = CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString
    Else
      My.Settings.IdBelfer = CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString
    End If
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLekcjaFilter.SelectedIndexChanged
    InRefresh = False
    ClearDetails()
    If lvPlan.SelectedItems.Count > 0 Then
      FetchLekcja()
      GetData(lvPlan.SelectedItems(0).Text, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString)
    End If
  End Sub
  Private Sub EnableButtons(Value As Boolean)
    cmdEditPlan.Enabled = Value
    cmdDelete.Enabled = Value
    cmdPrint.Enabled = Value
    cmdExport.Enabled = Value
    cmdKopiuj.Enabled = Value
  End Sub

  Private Sub FetchData()
    FetchPlan()
    'FetchLekcja()
  End Sub
  Private Sub FetchPlan()
    Dim P As New PlanSQL, DBA As New DataBaseAction, CH As New CalcHelper
    'CH.StartDateOfSchoolYear()
    dtPlan = DBA.GetDataTable(P.SelectPlan(My.Settings.IdSchool, CH.StartDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString, CH.EndDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString))
  End Sub
  Private Sub FetchLekcja()
    Dim P As New PlanSQL, DBA As New DataBaseAction
    If chkScalLekcje.Checked Then
      If LekcjaFilter = "Klasa" Then
        dtLekcja = DBA.GetDataTable(P.SelectLekcjaByKlasa(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString))
      Else
        dtLekcja = DBA.GetDataTable(P.SelectLekcjaByBelfer(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString))
      End If
      'dtLekcja = DBA.GetDataTable(IIf(LekcjaFilter = "Klasa", P.SelectLekcjaByKlasa(My.Settings.IdSchool, My.Settings.SchoolYear), P.SelectLekcjaByBelfer(My.Settings.IdSchool, My.Settings.SchoolYear)).ToString)
    Else
      If LekcjaFilter = "Klasa" Then
        dtLekcja = DBA.GetDataTable(P.SelectLekcjaByKlasaNoGroupConcat(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString))
      Else
        dtLekcja = DBA.GetDataTable(P.SelectLekcjaByBelferNoGroupConcat(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString))
      End If
      'dtLekcja = DBA.GetDataTable(IIf(LekcjaFilter = "Klasa", P.SelectLekcjaByKlasaNoGroupConcat(My.Settings.IdSchool, My.Settings.SchoolYear), P.SelectLekcjaByBelferNoGroupConcat(My.Settings.IdSchool, My.Settings.SchoolYear)).ToString)
    End If
  End Sub
  Private Sub GetMetaData()
    'Dim DBA As New DataBaseAction
    Try
      lvPlan.Items.Clear()
      For Each row As DataRow In dtPlan.Select(Filter)
        lvPlan.Items.Add(row.Item(0).ToString)
        lvPlan.Items(Me.lvPlan.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
        lvPlan.Items(Me.lvPlan.Items.Count - 1).SubItems.Add(CType(row.Item(2), Date).ToShortDateString)
        lvPlan.Items(Me.lvPlan.Items.Count - 1).SubItems.Add(CType(row.Item(3), Date).ToShortDateString)
        lvPlan.Items(Me.lvPlan.Items.Count - 1).SubItems.Add(IIf(row.Item(4).ToString = "0", "Otwarty", "Zamknięty").ToString)
      Next

      lvPlan.Columns(1).Width = CInt(IIf(lvPlan.Items.Count > 6, 374, 393))
      Me.lvPlan.Enabled = CType(IIf(Me.lvPlan.Items.Count > 0, True, False), Boolean)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub GetData(Plan As String, Filter As String)
    'Dim DBA As New DataBaseAction
    Try
      With lvLekcja
        .Items.Clear()
        .Groups.Clear()
        For i As Integer = 1 To 7
          Dim NG As New ListViewGroup(WeekdayName(i, False, FirstDayOfWeek.Monday))
          NG.HeaderAlignment = HorizontalAlignment.Center
          NG.Name = WeekdayName(i, False, FirstDayOfWeek.Monday)
          .Groups.Add(NG)
          For Each row As DataRow In dtLekcja.Select("IdPlan='" & Plan & "' AND " & If(LekcjaFilter = "Klasa", "Klasa='" & Filter & "'", "Nauczyciel='" & Filter & "'").ToString & " AND DzienTygodnia='" & i & "'")
            Dim NewItem As New ListViewItem(row.Item(0).ToString, NG)
            NewItem.UseItemStyleForSubItems = True
            '.Items.Add(row.Item(0).ToString)
            NewItem.SubItems.Add(row.Item(1).ToString) 'Nr lekcji
            NewItem.SubItems.Add(row.Item(2).ToString) 'Godzina lekcyjna
            NewItem.SubItems.Add(row.Item(3).ToString) 'Lekcja
            NewItem.SubItems.Add(row.Item("Sala").ToString)
            NewItem.SubItems.Add(row.Item("IdGodzina").ToString)
            NewItem.SubItems.Add(row.Item("IdObsada").ToString)
            .Items.Add(NewItem)
          Next
          If .Groups(NG.Name).Items.Count = 0 Then .Groups.Remove(NG)

        Next
        .Columns(3).Width = CInt(IIf(.Items.Count > 14, 474, 493))
        .Enabled = CType(IIf(.Items.Count > 0, True, False), Boolean)
      End With

    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub ClearDetails()
    'lblRecord0.Text = "0 z " & lvPlan.Items.Count
    lblRecord.Text = "0 z " & lvLekcja.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetPlanDetails(ID As Integer)
    Try
      lblRecord0.Text = lvPlan.SelectedItems(0).Index + 1 & " z " & lvPlan.Items.Count
      With dtPlan.Select("ID=" & ID)(0)
        'lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item("User").ToString
        Dim User, Owner As String
        User = CType(.Item("User"), String).ToLower.Trim
        Owner = CType(.Item("Owner"), String).ToLower.Trim
        If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
          lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (Wł: ", GlobalValues.Users.Item(Owner).ToString, ")")
        Else
          Me.lblUser.Text = User & " (Wł: " & Owner & ")"
        End If
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub GetDetails(ID As Integer)
    Try
      lblRecord.Text = lvLekcja.SelectedItems(0).Index + 1 & " z " & lvLekcja.Items.Count
      With dtLekcja.Select("ID=" & ID)(0)
        'lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item("User").ToString
        Dim User, Owner As String
        User = CType(.Item("User"), String).ToLower.Trim
        Owner = CType(.Item("Owner"), String).ToLower.Trim
        If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
          lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (Wł: ", GlobalValues.Users.Item(Owner).ToString, ")")
        Else
          Me.lblUser.Text = User & " (Wł: " & Owner & ")"
        End If
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged
    If dtPlan Is Nothing Then Exit Sub
    Filter = IIf(chkActive.Checked, "StartDate<=#" & Date.Today & "# AND EndDate>=#" & Date.Today & "#", "").ToString
    EnableButtons(False)
    cmdPrint.Enabled = False
    chkScalLekcje.Enabled = False
    GetMetaData()
    ClearDetails()
    lvLekcja.Items.Clear()
    lvLekcja.Enabled = False
    cbLekcjaFilter.Enabled = False
  End Sub

  Private Sub lvPlan_DoubleClick(sender As Object, e As EventArgs) Handles lvPlan.DoubleClick
    EditPlan()
  End Sub

  Private Sub lvPlan_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvPlan.ItemSelectionChanged
    If e.IsSelected Then
      cbLekcjaFilter.Enabled = CType(IIf(cbLekcjaFilter.Items.Count > 0, True, False), Boolean)
      chkScalLekcje.Enabled = True
      GetPlanDetails(CType(e.Item.Text, Integer))
      If cbLekcjaFilter.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator Then EnableButtons(True)

      If rbNauczyciel.Checked Then cmdExport.Enabled = False
    Else
      cbLekcjaFilter.Enabled = False
      chkScalLekcje.Enabled = False
      lvLekcja.Items.Clear()
      lvLekcja.Enabled = False
      ClearDetails()
      EnableButtons(False)
      cmdPrint.Enabled = False
    End If

  End Sub


  Private Sub cmdAddPlan_Click(sender As Object, e As EventArgs) Handles cmdAddPlan.Click
    Dim dlgAddNew As New dlgPlan
    With dlgAddNew
      .Text = "Nowy plan lekcji"
      .IsNewMode = True
      Dim CH As New CalcHelper
      .dtpStartDate.Value = Today
      .dtpEndDate.Value = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      AddHandler dlgAddNew.NewAdded, AddressOf NewPlanAdded
      Me.cmdAddPlan.Enabled = False
      .ShowDialog()
      cmdAddPlan.Enabled = True

    End With
  End Sub
  Private Sub NewPlanAdded(ByVal InsertedID As String)
    FetchPlan()
    lvLekcja.Items.Clear()
    ClearDetails()
    GetMetaData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(lvPlan, InsertedID)
  End Sub

  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, P As New PlanSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvPlan.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.DeletePlan)
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        FetchData()
        GetMetaData()
        ClearDetails()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvPlan, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub

  Private Sub cmdEditPlan_Click(sender As Object, e As EventArgs) Handles cmdEditPlan.Click
    EditPlan()
  End Sub
  Private Sub EditPlan()
    Dim MySQLTrans As MySqlTransaction = Nothing
    Try
      Dim dlgEdit As New dlgPlan

      With dlgEdit
        .Text = "Edycja parametrów planu lekcji"
        .OK_Button.Text = "Zapisz"
        .IsNewMode = False
        .txtPlan.Text = lvPlan.SelectedItems(0).SubItems(1).Text
        .dtpStartDate.Value = CType(lvPlan.SelectedItems(0).SubItems(2).Text, Date)
        .dtpEndDate.Value = CType(lvPlan.SelectedItems(0).SubItems(3).Text, Date)
        .chkLock.Checked = CType(IIf(lvPlan.SelectedItems(0).SubItems(4).Text = "Zamknięty", True, False), Boolean)
        .OK_Button.Enabled = False
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, IdPlan As String, P As New PlanSQL
          IdPlan = Me.lvPlan.SelectedItems(0).Text
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.UpdatePlan())
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?Nazwa", .txtPlan.Text.Trim)
          cmd.Parameters.AddWithValue("?StartDate", .dtpStartDate.Value.ToShortDateString)
          cmd.Parameters.AddWithValue("?EndDate", .dtpEndDate.Value.ToShortDateString)
          cmd.Parameters.AddWithValue("?Lock", .chkLock.CheckState)
          cmd.Parameters.AddWithValue("?ID", IdPlan)
          cmd.ExecuteNonQuery()
          MySQLTrans.Commit()
          'lvHarmonogram.Items.Clear
          FetchPlan()
          GetMetaData()
          lvLekcja.Items.Clear()
          ClearDetails()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvPlan, IdPlan)
        End If
      End With
    Catch myex As MySqlException
      MessageBox.Show(myex.Message & vbNewLine & myex.InnerException.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub


  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub

  Private Sub chkScalLekcje_CheckedChanged(sender As Object, e As EventArgs) Handles chkScalLekcje.CheckedChanged
    'FetchLekcja()
    If cbLekcjaFilter.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(cbLekcjaFilter, e)
  End Sub


  Private Sub rbKlasa_CheckedChanged(sender As Object, e As EventArgs) Handles rbKlasa.CheckedChanged, rbNauczyciel.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    LekcjaFilter = CType(sender, RadioButton).Tag.ToString
    cmdExport.Enabled = If(rbKlasa.Checked, True, False)
    lvLekcja.Items.Clear()
    ClearDetails()
    'FetchLekcja()
    FillLekcjaFilter(cbLekcjaFilter)
  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    'If dtLekcja Is Nothing Then Exit Sub
    Dim PP As New dlgPrintPreview, DSP As New DataSet
    Dim DBA As New DataBaseAction, P As New PlanSQL
    DSP.Tables.Add(DBA.GetDataTable(P.SelectWeekDays(My.Settings.IdSchool, My.Settings.SchoolYear, lvPlan.SelectedItems(0).Text)))
    DSP.Tables(0).TableName = "Tydzien"
    'DSP.Tables.Add(New DataView(dtLekcja, "IdPlan='" & lvPlan.SelectedItems(0).Text & "'", "DzienTygodnia ASC", DataViewRowState.CurrentRows).ToTable("Tydzien", True, "DzienTygodnia"))
    PP.Doc = New PrintReport(DSP)
    PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
    PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
    PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin
    If rbNauczyciel.Checked Then

      PP.Doc.DS.Tables.Add(DBA.GetDataTable(P.SelectLekcjaByBelfer(My.Settings.IdSchool, My.Settings.SchoolYear)))
      PP.Doc.DS.Tables(1).TableName = "Lekcja"

      RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllTeachers_PrintPage
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllTeachers_PrintPage
      PP.Doc.ReportHeader = New String() {"Plan lekcji - nauczyciele"}
    Else
      Dim S As New StudentSQL ', DBA As New DataBaseAction, P As New PlanSQL
      PP.Doc.DS.Tables.Add(DBA.GetDataTable(S.SelectWychowawca(My.Settings.SchoolYear, My.Settings.IdSchool)))
      PP.Doc.DS.Tables(1).TableName = "Tutor"
      PP.Doc.DS.Tables.Add(DBA.GetDataTable(P.SelectLekcjaByKlasa(My.Settings.IdSchool, My.Settings.SchoolYear)))
      PP.Doc.DS.Tables(2).TableName = "Lekcja"

      RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllClasses_PrintPage
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllClasses_PrintPage
      PP.Doc.ReportHeader = New String() {"Plan lekcji - oddziały klasowe"}
      'End If
    End If
    AddHandler NewRow, AddressOf PP.NewRow
    PP.Width = 1000
    PP.ShowDialog()
  End Sub

  Public Sub PrnDoc_AllTeachers_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    Dim Doc As PrintReport = CType(sender, PrintReport)
    Dim x As Single = My.Settings.LeftMargin
    Dim y As Single = My.Settings.TopMargin
    Dim TextFont As Font = My.Settings.TextFont
    Dim HeaderFont As Font = My.Settings.HeaderFont
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim TeacherColWidth As Integer = 80 '59
    Dim LessonHourColWidth As Integer = 39
    Dim WeekDayColWidth As Integer = 39
    Dim HeaderColHeight = 197
    Dim ColHeight As Integer = 35
    Dim ColsNumber, MaxColNumber As Integer
    Dim TheEnd As Boolean = False
    If Doc.Offset(1) = 0 Then
      MaxColNumber = CInt(Math.Floor(CType((e.MarginBounds.Width - LessonHourColWidth - WeekDayColWidth) / TeacherColWidth, Double)))
    Else
      MaxColNumber = CInt(Math.Floor(CType(e.MarginBounds.Width / TeacherColWidth, Double)))
    End If
    If (MaxColNumber + Doc.Offset(1)) > cbLekcjaFilter.Items.Count Then
      ColsNumber = cbLekcjaFilter.Items.Count - Doc.Offset(1)
      TheEnd = True
    Else
      ColsNumber = MaxColNumber
    End If
    Doc.PageNumber += 1
    If Doc.PageNumber = 1 Then
      If Doc.Offset(1) = 0 Then
        Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 1, Brushes.Black, False)
        y += HeaderLineHeight * 2
        'Doc.DrawLine(e, x, y, x+, y, 3)
        y += HeaderColHeight
        Doc.DrawText(e, "Dzień tygodnia", New Font(TextFont, FontStyle.Bold), x, y, WeekDayColWidth, HeaderColHeight, 0, Brushes.Black, 270, True)
        x += WeekDayColWidth
        Doc.DrawText(e, "Godzina lekcyjna", New Font(TextFont, FontStyle.Bold), x, y, LessonHourColWidth, HeaderColHeight, 0, Brushes.Black, 270, True)
        x += LessonHourColWidth
      Else
        y += HeaderLineHeight * 2
        y += HeaderColHeight
      End If

      For i As Integer = Doc.Offset(1) To ColsNumber + Doc.Offset(1) - 1
        Doc.DrawText(e, CType(cbLekcjaFilter.Items(i), CbItem).ToString, New Font(TextFont, FontStyle.Bold), x, y, TeacherColWidth, HeaderColHeight, 0, Brushes.Black, 270, True)
        x += TeacherColWidth
      Next
      Doc.DrawLine(e, My.Settings.LeftMargin, y - HeaderColHeight, x, y - HeaderColHeight, 3)

    End If

    Doc.DrawLine(e, My.Settings.LeftMargin, y, x, y, 3)
    x = My.Settings.LeftMargin

    Do While ((y + ColHeight * 7) < e.MarginBounds.Bottom) And (Doc.Offset(0) <= Doc.DS.Tables("Tydzien").Rows.Count - 1)
      For i As Integer = 1 To 7
        If Doc.Offset(1) = 0 Then
          x += WeekDayColWidth
          Doc.DrawText(e, i.ToString, TextFont, x, y, LessonHourColWidth, ColHeight, 1, Brushes.Black)
          x += LessonHourColWidth
        End If

        For j As Integer = Doc.Offset(1) To ColsNumber + Doc.Offset(1) - 1
          Dim Lekcja As DataRow()
          'Lekcja = dtLekcja.Select("IdPlan='" & lvPlan.SelectedItems(0).Text & "' AND Nauczyciel='" & CType(cbLekcjaFilter.Items(j), CbItem).ID.ToString & "' AND DzienTygodnia='" & Doc.DS.Tables("Tydzien").Rows(Doc.Offset(0)).Item("DzienTygodnia").ToString & "' AND NrLekcji='" & i & "'")
          Lekcja = Doc.DS.Tables("Lekcja").Select("IdPlan='" & lvPlan.SelectedItems(0).Text & "' AND Nauczyciel='" & CType(cbLekcjaFilter.Items(j), CbItem).ID.ToString & "' AND DzienTygodnia='" & Doc.DS.Tables("Tydzien").Rows(Doc.Offset(0)).Item("DzienTygodnia").ToString & "' AND NrLekcji='" & i & "'")
          If Lekcja.Length > 0 Then
            Doc.DrawText(e, String.Concat(Lekcja(0).Item("Obsada").ToString, " [", Lekcja(0).Item("Sala").ToString, "]"), TextFont, x, y, TeacherColWidth, ColHeight, 1, Brushes.Black)
            'Doc.DrawText(e, Lekcja(0).Item("Obsada").ToString, TextFont, x, y, TeacherColWidth, ColHeight, 1, Brushes.Black)
          Else
            Doc.DrawText(e, "", TextFont, x, y, TeacherColWidth, ColHeight, 0, Brushes.Black)
          End If
          x += TeacherColWidth
        Next
        x = My.Settings.LeftMargin
        y += ColHeight
      Next
      If Doc.Offset(1) = 0 Then
        If Doc.PageNumber = 1 Then
          Doc.DrawLine(e, x, My.Settings.TopMargin + HeaderLineHeight * 2, x, y, 3)
          Doc.DrawLine(e, x + WeekDayColWidth + LessonHourColWidth, My.Settings.TopMargin + HeaderLineHeight * 2, x + WeekDayColWidth + LessonHourColWidth, y, 3)
        Else
          Doc.DrawLine(e, x, My.Settings.TopMargin, x, y, 3)
          Doc.DrawLine(e, x + WeekDayColWidth + LessonHourColWidth, My.Settings.TopMargin, x + WeekDayColWidth + LessonHourColWidth, y, 3)
        End If
        Doc.DrawLine(e, x, y, x + WeekDayColWidth + LessonHourColWidth + TeacherColWidth * ColsNumber, y, 3)
        Doc.DrawText(e, WeekdayName(CType(Doc.DS.Tables("Tydzien").Rows(Doc.Offset(0)).Item("DzienTygodnia"), Integer), False, FirstDayOfWeek.Monday).ToUpper, New Font(TextFont, FontStyle.Bold), x, y, LessonHourColWidth, ColHeight * 7, 1, Brushes.Black, 270, True)
      Else
        'Doc.DrawLine(e, x, My.Settings.TopMargin, x, y, 3)
        Doc.DrawLine(e, x, y, x + TeacherColWidth * ColsNumber, y, 3)
        If TheEnd Then
          If Doc.PageNumber = 1 Then
            Doc.DrawLine(e, x + TeacherColWidth * ColsNumber, My.Settings.TopMargin + HeaderLineHeight * 2, x + TeacherColWidth * ColsNumber, y, 3)
          Else
            Doc.DrawLine(e, x + TeacherColWidth * ColsNumber, My.Settings.TopMargin, x + TeacherColWidth * ColsNumber, y, 3)
          End If

        End If
      End If

      Doc.Offset(0) += 1
    Loop
    If Doc.Offset(0) <= Doc.DS.Tables("Tydzien").Rows.Count - 1 Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Doc.Offset(0) = 0
      Doc.PageNumber = 0
      If (Doc.Offset(1) + ColsNumber) < cbLekcjaFilter.Items.Count Then
        Doc.Offset(1) += ColsNumber
        e.HasMorePages = True
        RaiseEvent NewRow()
      Else
        Doc.Offset(1) = 0
      End If
    End If
  End Sub
  Public Sub PrnDoc_AllClasses_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    Dim Doc As PrintReport = CType(sender, PrintReport)
    Dim x As Single = My.Settings.LeftMargin
    Dim y As Single = My.Settings.TopMargin
    Dim TextFont As Font = My.Settings.TextFont
    Dim HeaderFont As Font = My.Settings.HeaderFont
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim TeacherColWidth As Integer = 122
    Dim LessonHourColWidth As Integer = 39
    Dim WeekDayColWidth As Integer = 39
    Dim HeaderColHeight = 80
    Dim ColHeight As Integer = 35
    Dim ColsNumber, MaxColNumber As Integer
    Dim TheEnd As Boolean = False
    If Doc.Offset(1) = 0 Then
      MaxColNumber = CInt(Math.Floor(CType((e.MarginBounds.Width - LessonHourColWidth - WeekDayColWidth) / TeacherColWidth, Double)))
    Else
      MaxColNumber = CInt(Math.Floor(CType(e.MarginBounds.Width / TeacherColWidth, Double)))
    End If
    If (MaxColNumber + Doc.Offset(1)) > cbLekcjaFilter.Items.Count Then
      ColsNumber = cbLekcjaFilter.Items.Count - Doc.Offset(1)
      TheEnd = True
    Else
      ColsNumber = MaxColNumber
    End If

    Doc.PageNumber += 1
    dy = y
    If Doc.PageNumber = 1 Then
      dx = 0
      dy += HeaderLineHeight * 2
      If Doc.Offset(1) = 0 Then
        Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 1, Brushes.Black, False)
        y += HeaderLineHeight * 2
        y += HeaderColHeight
        Doc.DrawText(e, "Dzień tygodnia", New Font(TextFont, FontStyle.Bold), x, y, WeekDayColWidth, HeaderColHeight, 1, Brushes.Black, 270, True)
        x += WeekDayColWidth
        Doc.DrawText(e, "Nr lekcji", New Font(TextFont, FontStyle.Bold), x, y, LessonHourColWidth, HeaderColHeight, 1, Brushes.Black, 270, True)
        x += LessonHourColWidth
      Else

        y += HeaderLineHeight * 2
        y += HeaderColHeight
      End If

      For i As Integer = Doc.Offset(1) To ColsNumber + Doc.Offset(1) - 1
        Dim Tutor As String = ""
        If Doc.DS.Tables("Tutor").Select("Klasa='" & CType(cbLekcjaFilter.Items(i), CbItem).ID.ToString & "'").Length > 0 Then
          Tutor = Doc.DS.Tables("Tutor").Select("Klasa='" & CType(cbLekcjaFilter.Items(i), CbItem).ID.ToString & "'")(0).Item("Wychowawca").ToString
          Doc.DrawText(e, CType(cbLekcjaFilter.Items(i), CbItem).ToString, New Font(TextFont, FontStyle.Bold), x, y - HeaderColHeight, TeacherColWidth, CInt(HeaderColHeight / 2), 1, Brushes.Black)
          Doc.DrawText(e, Tutor, New Font(TextFont.FontFamily, TextFont.Size - 2, FontStyle.Italic), x, y - CInt(HeaderColHeight / 2), TeacherColWidth, CInt(HeaderColHeight / 2), 1, Brushes.Black)
        Else
          Doc.DrawText(e, CType(cbLekcjaFilter.Items(i), CbItem).ToString & vbNewLine & Tutor, New Font(TextFont, FontStyle.Bold), x, y - HeaderColHeight, TeacherColWidth, HeaderColHeight, 1, Brushes.Black)
        End If
        If CType(CType(cbLekcjaFilter.Items(i), CbItem).Kod, Integer) > Doc.Offset(2) Then
          Doc.Offset(2) = CType(CType(cbLekcjaFilter.Items(i), CbItem).Kod, Integer)
          dx = x
        End If
        x += TeacherColWidth
      Next

      Doc.DrawLine(e, My.Settings.LeftMargin, y - HeaderColHeight, x, y - HeaderColHeight, 3)

    End If

    Doc.DrawLine(e, My.Settings.LeftMargin, y, x, y, 3)
    x = My.Settings.LeftMargin

    Do While ((y + ColHeight * 7) < e.MarginBounds.Bottom) And (Doc.Offset(0) <= Doc.DS.Tables("Tydzien").Rows.Count - 1)
      For i As Integer = 1 To 7
        If Doc.Offset(1) = 0 Then
          x += WeekDayColWidth
          Doc.DrawText(e, i.ToString, TextFont, x, y, LessonHourColWidth, ColHeight, 1, Brushes.Black)
          x += LessonHourColWidth
        End If

        For j As Integer = Doc.Offset(1) To ColsNumber + Doc.Offset(1) - 1
          Dim Lekcja As DataRow()
          Lekcja = Doc.DS.Tables("Lekcja").Select("IdPlan='" & lvPlan.SelectedItems(0).Text & "' AND Klasa='" & CType(cbLekcjaFilter.Items(j), CbItem).ID.ToString & "' AND DzienTygodnia='" & Doc.DS.Tables("Tydzien").Rows(Doc.Offset(0)).Item("DzienTygodnia").ToString & "' AND NrLekcji='" & i & "'")
          If Lekcja.Length > 0 Then
            Doc.DrawText(e, String.Concat(Lekcja(0).Item("Obsada").ToString, " [", Lekcja(0).Item("Sala").ToString, "]"), TextFont, x, y, TeacherColWidth, ColHeight, 0, Brushes.Black)
          Else
            Doc.DrawText(e, "", TextFont, x, y, TeacherColWidth, ColHeight, 0, Brushes.Black)
          End If
          x += TeacherColWidth
        Next
        x = My.Settings.LeftMargin
        y += ColHeight
      Next
      If Doc.Offset(1) = 0 Then
        If Doc.PageNumber = 1 Then
          Doc.DrawLine(e, x, My.Settings.TopMargin + HeaderLineHeight * 2, x, y, 3)
          Doc.DrawLine(e, x + WeekDayColWidth + LessonHourColWidth, My.Settings.TopMargin + HeaderLineHeight * 2, x + WeekDayColWidth + LessonHourColWidth, y, 3)
        Else
          Doc.DrawLine(e, x, My.Settings.TopMargin, x, y, 3)
          Doc.DrawLine(e, x + WeekDayColWidth + LessonHourColWidth, My.Settings.TopMargin, x + WeekDayColWidth + LessonHourColWidth, y, 3)
        End If
        Doc.DrawLine(e, x, y, x + WeekDayColWidth + LessonHourColWidth + TeacherColWidth * ColsNumber, y, 3)
        Doc.DrawText(e, WeekdayName(CType(Doc.DS.Tables("Tydzien").Rows(Doc.Offset(0)).Item("DzienTygodnia"), Integer), False, FirstDayOfWeek.Monday).ToUpper, New Font(TextFont, FontStyle.Bold), x, y, LessonHourColWidth, ColHeight * 7, 1, Brushes.Black, 270, True)
      Else
        Doc.DrawLine(e, x, y, x + TeacherColWidth * ColsNumber, y, 3)
        If TheEnd Then
          If Doc.PageNumber = 1 Then
            Doc.DrawLine(e, x + TeacherColWidth * ColsNumber, My.Settings.TopMargin + HeaderLineHeight * 2, x + TeacherColWidth * ColsNumber, y, 3)
          Else
            Doc.DrawLine(e, x + TeacherColWidth * ColsNumber, My.Settings.TopMargin, x + TeacherColWidth * ColsNumber, y, 3)
          End If

        End If
      End If

      Doc.Offset(0) += 1
    Loop
    If dx > 0 Then Doc.DrawLine(e, dx, dy, dx, y, 3)
    If Doc.Offset(0) <= Doc.DS.Tables("Tydzien").Rows.Count - 1 Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Doc.Offset(0) = 0
      Doc.PageNumber = 0
      If (Doc.Offset(1) + ColsNumber) < cbLekcjaFilter.Items.Count Then
        Doc.Offset(1) += ColsNumber
        e.HasMorePages = True
        RaiseEvent NewRow()
      Else
        Doc.Offset(1) = 0
        Doc.Offset(2) = 0
      End If
    End If
  End Sub

  Private Sub lvLekcja_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvLekcja.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(CType(e.Item.Text, Integer))
    Else
      ClearDetails()
    End If
  End Sub


  Private Sub cmdKopiuj_Click(sender As Object, e As EventArgs) Handles cmdKopiuj.Click
    Dim dlgAddNew As New dlgKopiujPlan
    With dlgAddNew
      .FillPlan(.cbSource)
      .FillPlan(.cbTarget)
      .ShowDialog()
    End With
  End Sub

  Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
    Dim dlgSave As New SaveFileDialog

    With dlgSave
      .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

      .AddExtension = True
      .CheckFileExists = False
      .DefaultExt = "html"
      .Filter = "Pliki html (*.html;*.htm)|*.html;*.htm|Wszystkie pliki (*.*)|*.*"

      If .ShowDialog() = Windows.Forms.DialogResult.OK Then
        If ExportToHTML(.FileName) Then MessageBox.Show("Export dokumentu zakończony powodzeniem.")

      End If
    End With
  End Sub
  Public Function ExportToHTML(FileName As String) As Boolean
    Cursor = Cursors.WaitCursor
    Dim Wait As New dlgWait
    Wait.Show()
    Application.DoEvents()
    Dim CH As New CalcHelper
    Dim DocumentHeader() As String = New String() {"Plan lekcji", "obowiązuje od " & lvPlan.SelectedItems(0).SubItems(2).Text & If(CType(lvPlan.SelectedItems(0).SubItems(3).Text, Date) < CH.EndDateOfSchoolYear(My.Settings.SchoolYear), " do " & lvPlan.SelectedItems(0).SubItems(3).ToString, " do odwołania")}
    Try
      Dim XmlDoc As New Xml.XmlDocument()
      XmlDoc.Load(Application.StartupPath & "\Szablony\PlanTemplate.html")
      'Dim Root As XmlElement = XmlDoc.DocumentElement
      Dim Body As XmlNode = XmlDoc.DocumentElement.LastChild
      InsertAnchor(XmlDoc, Body, "h1", "PageEntry")

      For Each Line As String In DocumentHeader
        InsertTag(XmlDoc, Body, "h2", "text-align:center;line-height:100%;color:blue;", Line)
      Next
      Dim Pion As New Hashtable From {{1, "Klasy pierwsze"}, {2, "Klasy drugie"}, {3, "Klasy trzecie"}, {4, "Klasy czwarte"}, {5, "Klasy piąte"}, {6, "Klasy szóste"}, {7, "Klasy siódme"}, {8, "Klasy ósme"}, {9, "Klasy dziewiąte"}}
      Dim OH As New OptionHolder, MinPion, MaxPion As Byte
      MinPion = OH.GetMinPion
      MaxPion = OH.GetMaxPion
      Dim DBA As New DataBaseAction, P As New PlanSQL
      Dim dtKlasa As DataTable = DBA.GetDataTable(P.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
      For i As Byte = MinPion To MaxPion
        InsertTag(XmlDoc, Body, "p", "text-align:center;font-weight:bold;color:#990000", Pion(CInt(i)).ToString.ToUpper)
        InsertEmptyTag(XmlDoc, Body, "table", "Link", "width:60%;border:1px solid;margin:0 auto;background-color:yellow;")
        Dim LinkTable As XmlNode = Body.LastChild
        InsertEmptyTag(XmlDoc, LinkTable, "tr")
        Dim LinkTableRow As XmlNode = LinkTable.LastChild
        For Each R As DataRow In dtKlasa.Select("Pion=" & i & " AND Virtual=0")
          InsertEmptyTag(XmlDoc, LinkTableRow, "td", "border:1px solid;")
          Dim LinkTableRowCell As XmlNode = LinkTableRow.LastChild
          InsertLink(XmlDoc, LinkTableRowCell, "#" + R.Item("Kod_Klasy").ToString, "Klasa " & R.Item("Nazwa_Klasy").ToString)
        Next
      Next
      InsertEmptyTag(XmlDoc, Body, "p")
      InsertEmptyTag(XmlDoc, Body, "hr")
      InsertEmptyTag(XmlDoc, Body, "p")
      Dim PlanColumnHeader() As String = New String() {"Nr lekcji", "Godzina lekcyjna", "Lekcja", "Sala"}
      Dim dtLekcja As DataTable = DBA.GetDataTable(P.SelectLekcjaByKlasa(My.Settings.IdSchool, My.Settings.SchoolYear))
      For Each Klasa As DataRow In dtKlasa.Select("Virtual=0")
        InsertEmptyTag(XmlDoc, Body, "p")
        InsertEmptyTag(XmlDoc, Body, "p")
        InsertEmptyTag(XmlDoc, Body, "table", Klasa.Item("Kod_Klasy").ToString, "width:100%;border:none;margin:0 auto;border-collapse: collapse;")
        InsertEmptyTag(XmlDoc, Body.LastChild, "tr")
        InsertTag(XmlDoc, Body.LastChild.LastChild, "td", "text-align:left;border:none;width:25%;background-color:black;", "")
        InsertTag(XmlDoc, Body.LastChild.LastChild, "td", "background-color:black;color:white;text-align:center;font-weight:bold;text-transform: uppercase;", "Klasa " & Klasa.Item("Nazwa_Klasy").ToString)
        InsertEmptyTag(XmlDoc, Body.LastChild.LastChild, "td", "text-align:right;border:none;width:25%;background-color:black;")
        InsertLink(XmlDoc, Body.LastChild.LastChild.LastChild, "text-decoration:none;color:white;", "#PageEntry", "Powrót do menu")
        InsertEmptyTag(XmlDoc, Body, "p")
        For i As Integer = 1 To 7
          If dtLekcja.Select("IdPlan='" & lvPlan.SelectedItems(0).Text & "' AND Klasa='" & Klasa.Item("ID").ToString & "' AND DzienTygodnia='" & i & "'").GetLength(0) > 0 Then
            Dim SchoolDay As String = WeekdayName(i, False, FirstDayOfWeek.Monday)
            InsertEmptyTag(XmlDoc, Body, "p")
            InsertTag(XmlDoc, Body, "p", "text-align:center;width:900px;font-weight:bold;margin:0 auto;line-height:1.5;", SchoolDay.ToUpper)
            InsertEmptyTag(XmlDoc, Body, "table", "width:900px;border:1px solid;margin:0 auto;background-color:white;border-collapse: collapse;")
            Dim PlanTable As XmlNode = Body.LastChild
            InsertEmptyTag(XmlDoc, PlanTable, "tr", "height:40px")
            Dim PlanTableRow As XmlNode = PlanTable.LastChild
            For Each ColumnHeader As String In PlanColumnHeader
              InsertTag(XmlDoc, PlanTableRow, "th", "border:1px solid;text-align:center;font-weight:bold;color:#990000;background-color:#fbfbfb", ColumnHeader)
            Next
            For Each Lekcja As DataRow In dtLekcja.Select("IdPlan='" & lvPlan.SelectedItems(0).Text & "' AND Klasa='" & Klasa.Item("ID").ToString & "' AND DzienTygodnia='" & i & "'")
              InsertEmptyTag(XmlDoc, PlanTable, "tr")
              PlanTableRow = PlanTable.LastChild
              InsertTag(XmlDoc, PlanTableRow, "td", "text-align:center;border:1px solid;width:60px;	padding: 5px;", Lekcja.Item("NrLekcji").ToString)
              InsertTag(XmlDoc, PlanTableRow, "td", "text-align:center;border:1px solid;width:120px;padding: 5px;", Lekcja.Item("Godzina").ToString)
              InsertTag(XmlDoc, PlanTableRow, "td", "text-align:left;border:1px solid;padding: 5px;", Lekcja.Item("Przedmiot").ToString)
              InsertTag(XmlDoc, PlanTableRow, "td", "text-align:center;border:1px solid;width:60px;padding: 5px;", Lekcja.Item("Sala").ToString)
            Next
          End If
        Next
      Next
      Dim writer As XmlTextWriter = New XmlTextWriter(FileName, System.Text.Encoding.GetEncoding("UTF-8"))
      writer.Formatting = Formatting.Indented
      XmlDoc.Save(writer)
      writer.Close()
      Wait.Hide()
      Cursor = Cursors.Default
      Return True
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return False
    Finally
    End Try
  End Function
  Private Function AddAttrib(ByVal Nazwa As String, ByVal Value As String, ByVal XmlDoc As XmlDocument) As XmlAttribute
    Dim Attr As XmlAttribute
    Attr = XmlDoc.CreateAttribute(Nazwa)
    Attr.Value = Value
    Return Attr
  End Function

  Private Sub InsertTag(Doc As XmlDocument, DocNode As XmlNode, NodeName As String, StyleProperty As String, ParagraphText As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName)
    P.InnerText = ParagraphText
    P.Attributes.Append(AddAttrib("style", StyleProperty, Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertTag(Doc As XmlDocument, DocNode As XmlNode, NodeName As String, ParagraphText As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName)
    P.InnerText = ParagraphText
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertTag(Doc As XmlDocument, DocNode As XmlNode, NodeName As String, NodeID As String, StyleProperty As String, ParagraphText As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName)
    P.InnerText = ParagraphText
    P.Attributes.Append(AddAttrib("style", StyleProperty, Doc))
    P.Attributes.Append(AddAttrib("ID", NodeID, Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertEmptyTag(Doc As XmlDocument, DocNode As XmlNode, NodeName As String, StyleProperty As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName)
    P.Attributes.Append(AddAttrib("style", StyleProperty, Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertEmptyTag(Doc As XmlDocument, DocNode As XmlNode, NodeName As String, NodeID As String, StyleProperty As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName)
    P.Attributes.Append(AddAttrib("style", StyleProperty, Doc))
    P.Attributes.Append(AddAttrib("ID", NodeID, Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertEmptyTag(Doc As XmlDocument, DocNode As XmlNode, NodeName As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName)
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertAnchor(Doc As XmlDocument, DocNode As XmlNode, NodeName As String, AnchorName As String)
    Dim P As XmlElement
    P = Doc.CreateElement(NodeName)
    'P.InnerText = ParagraphText
    P.Attributes.Append(AddAttrib("ID", AnchorName, Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertLink(Doc As XmlDocument, DocNode As XmlNode, LinkValue As String, LinkText As String)
    Dim P As XmlElement
    P = Doc.CreateElement("a")
    P.InnerText = LinkText
    P.Attributes.Append(AddAttrib("href", LinkValue, Doc))
    P.Attributes.Append(AddAttrib("style", "text-decoration:none;text-align:center;display:block;background-color:#FFFFCC;", Doc))
    DocNode.AppendChild(P)
  End Sub
  Private Sub InsertLink(Doc As XmlDocument, DocNode As XmlNode, LinkStyle As String, LinkValue As String, LinkText As String)
    Dim P As XmlElement
    P = Doc.CreateElement("a")
    P.InnerText = LinkText
    P.Attributes.Append(AddAttrib("href", LinkValue, Doc))
    P.Attributes.Append(AddAttrib("style", LinkStyle, Doc))
    DocNode.AppendChild(P)
  End Sub
End Class