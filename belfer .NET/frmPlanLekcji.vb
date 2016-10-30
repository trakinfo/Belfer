Imports System.Drawing.Printing
Public Class frmPlanLekcji
  Private dtLekcja As DataTable, Filter As String = "StartDate<=#" & Date.Today & "# AND EndDate>=#" & Date.Today & "#"
  Private InRefresh As Boolean = True
  Public LekcjaFilter As String = "Belfer"
  Public Event NewRow()
  Private Offset(1), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub frmPlanLekcji_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PlanLekcjiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PlanLekcjiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmPlanLekcji_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    InRefresh = True
    ListViewConfig(lvLekcja)
    ApplyNewConfig()

  End Sub
  Private Sub cbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPlan.SelectedIndexChanged
    rbKlasa.Enabled = True
    rbNauczyciel.Enabled = True
    rbKlasa_CheckedChanged(If(rbKlasa.Checked, rbKlasa, rbNauczyciel), e)
  End Sub
  Private Sub LoadPlanItems()
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, P As New PlanSQL, CH As New CalcHelper
    cbPlan.Items.Clear()
    Try
      R = DBA.GetReader(P.SelectPlan3(My.Settings.IdSchool, CH.StartDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString, CH.EndDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString))
      While R.Read()
        cbPlan.Items.Add(New PlanComboItem(R.GetInt32("ID"), R.GetString("Nazwa"), R.GetBoolean("Lock"), R.GetDateTime("StartDate"), R.GetDateTime("EndDate")))
      End While
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub ApplyNewConfig()
    lblRecord.Text = ""
    lvLekcja.Items.Clear()
    rbKlasa.Enabled = False
    rbNauczyciel.Enabled = False
    cbLekcjaFilter.Enabled = False
    'Dim FCB As New FillComboBox, P As New PlanSQL, CH As New CalcHelper
    'FCB.AddComboBoxExtendedItems(cbPlan, )
    LoadPlanItems()
    If cbPlan.Items.Count > 0 Then
      cbPlan.SelectedIndex = 0
      cbPlan.Enabled = True
    Else
      cbPlan.Enabled = False
    End If
    'cbPlan.Enabled = CType(IIf(cbPlan.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
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
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nr lekcji", 50, HorizontalAlignment.Center)
      .Columns.Add("Godzina lekcyjna", 100, HorizontalAlignment.Center)
      .Columns.Add("Lekcja", 493, HorizontalAlignment.Left)
      .Columns.Add("Sala", 150, HorizontalAlignment.Center)
      .Columns.Add("IdGodzina", 0, HorizontalAlignment.Left)
      .Columns.Add("IdObsada", 0, HorizontalAlignment.Left)
    End With
  End Sub

  Private Sub FillLekcjaFilter(ByVal cb As ComboBox)
    Try
      cb.Items.Clear()
      Dim FCB As New FillComboBox ', P As New PlanSQL
      Dim SelectString As String
      If LekcjaFilter = "Klasa" Then
        Dim K As New KolumnaSQL
        SelectString = K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear)
      Else
        Dim P As New PlanSQL
        SelectString = P.SelectBelfer(My.Settings.IdSchool, My.Settings.SchoolYear)
      End If

      FCB.AddComboBoxComplexItems(cb, SelectString)
      'cb.Enabled = False
      'If cbPlan.SelectedItem Is Nothing Then Exit Sub
      If cb.Items.Count > 0 Then
        cb.Enabled = True 'CType(IIf(cb.Items.Count > 0, True, False), Boolean)
        Dim SH As New SeekHelper
        If LekcjaFilter = "Klasa" Then
          If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbLekcjaFilter, CType(My.Settings.ClassName, Integer))
        Else
          If My.Settings.IdBelfer.Length > 0 Then SH.FindComboItem(Me.cbLekcjaFilter, CType(My.Settings.IdBelfer, Integer))
        End If
      Else
        cb.Enabled = False
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
    Cursor = Cursors.WaitCursor
    FetchLekcja()
    GetData(CType(cbPlan.SelectedItem, PlanComboItem).ID.ToString, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString)
    ClearDetails()
    EnableActivityButtons(False)
    If CType(cbPlan.SelectedItem, PlanComboItem).Lock Then
      cmdAddActivity.Enabled = False
    ElseIf GlobalValues.AppUser.Role = GlobalValues.Role.Administrator Then
      cmdAddActivity.Enabled = True
    Else
      If LekcjaFilter = "Klasa" Then
        cmdAddActivity.Enabled = If(GlobalValues.AppUser.TutorClassID = CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString, True, False)
      Else
        cmdAddActivity.Enabled = If(GlobalValues.AppUser.SchoolTeacherID = CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString, True, False)
      End If
    End If
    'cmdAddActivity.Enabled = Not CType(cbPlan.SelectedItem, PlanComboItem).Lock
    Cursor = Cursors.Default
  End Sub
  Private Sub rbKlasa_CheckedChanged(sender As Object, e As EventArgs) Handles rbKlasa.CheckedChanged, rbNauczyciel.CheckedChanged
    If Not Me.Created Then Exit Sub
    Cursor = Cursors.WaitCursor
    cmdAddActivity.Enabled = False
    If CType(sender, RadioButton).Checked Then
      LekcjaFilter = CType(sender, RadioButton).Tag.ToString
      FillLekcjaFilter(cbLekcjaFilter)
      'FetchLekcja()
      'ClearDetails()
    End If
    Cursor = Cursors.Default
  End Sub
  Private Sub EnableActivityButtons(Value As Boolean)
    'Me.cmdAddActivity.Enabled = Value 'CType(IIf(My.Application.OpenForms.OfType(Of dlgLekcja)().Any(), False, True), Boolean)
    cmdEditActivity.Enabled = Value
    Me.cmdDeleteActivity.Enabled = Value
  End Sub

  Private Sub FetchLekcja()
    Dim P As New PlanSQL, DBA As New DataBaseAction
    If chkScalLekcje.Checked Then
      dtLekcja = DBA.GetDataTable(If(LekcjaFilter = "Klasa", P.SelectLekcjaByKlasa(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString), P.SelectLekcjaByBelfer(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString)).ToString)
    Else
      dtLekcja = DBA.GetDataTable(If(LekcjaFilter = "Klasa", P.SelectLekcjaByKlasaNoGroupConcat(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString), P.SelectLekcjaByBelferNoGroupConcat(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString)).ToString)
    End If
  End Sub

  Private Sub GetData(Plan As String, Filter As String)
    'Dim DBA As New DataBaseAction
    Try
      With lvLekcja
        .Items.Clear()
        .Groups.Clear()
        For i As Integer = 1 To 7
          Dim NG As New ListViewGroup(WeekdayName(i, False, FirstDayOfWeek.Monday), WeekdayName(i, False, FirstDayOfWeek.Monday))
          NG.HeaderAlignment = HorizontalAlignment.Center
          NG.Tag = New CbItem(i, WeekdayName(i, False, FirstDayOfWeek.Monday))
          .Groups.Add(NG)
          For Each row As DataRow In dtLekcja.Select("IdPlan='" & Plan & "' AND " & IIf(LekcjaFilter = "Klasa", "Klasa='" & Filter & "'", "Nauczyciel='" & Filter & "'").ToString & " AND DzienTygodnia='" & i & "'")
            Dim NewItem As New ListViewItem(row.Item(0).ToString, NG)
            NewItem.UseItemStyleForSubItems = True
            NewItem.SubItems.Add(row.Item(1).ToString)
            NewItem.SubItems.Add(row.Item(2).ToString)
            NewItem.SubItems.Add(row.Item(3).ToString)
            NewItem.SubItems.Add(row.Item("Sala").ToString)
            NewItem.SubItems.Add(row.Item("IdGodzina").ToString)
            NewItem.SubItems.Add(row.Item("IdObsada").ToString)
            .Items.Add(NewItem)
          Next
        Next
        .Columns(3).Width = CInt(If(.Items.Count > 19, 474, 493))
        .Enabled = CType(If(.Items.Count > 0, True, False), Boolean)
        cmdPrint.Enabled = CType(If(.Items.Count > 0, True, False), Boolean)
      End With

    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvLekcja.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
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

  Private Sub lvLekcja_DoubleClick(sender As Object, e As EventArgs) Handles lvLekcja.DoubleClick
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse String.Compare(GlobalValues.AppUser.Login, CType(dtLekcja.Select("ID=" & lvLekcja.SelectedItems(0).Text)(0).Item("Owner"), String).Trim, True) = 0 Then EditLekcja()
  End Sub

  Private Sub lvLekcja_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvLekcja.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(CType(e.Item.Text, Integer))
      If CType(cbPlan.SelectedItem, PlanComboItem).Lock = False Then
        'If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtLekcja.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.ToLower Then EnableActivityButtons(True)
        'MessageBox.Show(dtLekcja.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim.Length.ToString)
        If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse String.Compare(GlobalValues.AppUser.Login.ToLower, CType(dtLekcja.Select("ID=" & e.Item.Text)(0).Item("Owner"), String).Trim, True) = 0 Then EnableActivityButtons(True)
      End If
    Else
      ClearDetails()
      EnableActivityButtons(False)
    End If
  End Sub

  Private Sub cmdAddActivity_Click(sender As Object, e As EventArgs) Handles cmdAddActivity.Click
    Dim dlgAddNew As New dlgLekcja
    With dlgAddNew
      .Text = "Nowa lekcja"
      .IsNewMode = True
      .IdPlan = CType(cbPlan.SelectedItem, PlanComboItem).ID
      .lblKlasa.Text = IIf(LekcjaFilter = "Klasa", "Klasa: ", "Nauczyciel: ").ToString + CType(cbLekcjaFilter.SelectedItem, CbItem).Nazwa
      '.WymiarGodzin = GetStaffAmount()
      .PlanEndDate = CType(cbPlan.SelectedItem, PlanComboItem).EndDate.ToShortDateString
      .Filter = LekcjaFilter
      .FilterID = CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString
      Dim FCB As New FillComboBox, P As New PlanSQL
      FCB.AddComboBoxComplexItems(.cbGodzina, P.SelectActivityTime(My.Settings.IdSchool))
      For i As Integer = 1 To 7
        .cbWeekDay.Items.Add(New CbItem(i, WeekdayName(i, False, FirstDayOfWeek.Monday)))
      Next
      If lvLekcja.SelectedItems.Count > 0 Then
        Dim SH As New SeekHelper
        SH.FindComboItem(.cbWeekDay, CType(lvLekcja.SelectedItems(0).Group.Tag, CbItem).ID)
        Dim NrLekcji As Integer = CType(lvLekcja.SelectedItems(0).SubItems(1).Text, Integer)
        .cbGodzina.SelectedIndex = If(NrLekcji = .cbGodzina.Items.Count, 0, NrLekcji) 'CType(If(NrLekcji > .cbGodzina.Items.Count, 0, NrLekcji), Integer)
      Else
        .cbWeekDay.SelectedIndex = 0
      End If
      .LessonNumberByStaff = .GetLessonNumberByStaff()
      .LoadLessonItems()
      If .cbObsada.Items.Count = 0 Then
        .cbGodzina.Enabled = False
        .cbWeekDay.Enabled = False
        .txtSala.Enabled = False
      End If
      AddHandler dlgAddNew.NewAdded, AddressOf NewActivityAdded
      Me.cmdAddActivity.Enabled = False
      .ShowDialog()
      cmdAddActivity.Enabled = True
    End With
  End Sub
  Private Sub NewActivityAdded(ByVal InsertedID As String)
    FetchLekcja()
    GetData(CType(cbPlan.SelectedItem, PlanComboItem).ID.ToString, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString)
    Dim SH As New SeekHelper
    SH.FindListViewItem(lvLekcja, InsertedID)
  End Sub

  Private Sub cmdDeleteActivity_Click(sender As Object, e As EventArgs) Handles cmdDeleteActivity.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, P As New PlanSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        Cursor = Cursors.WaitCursor
        For Each Item As ListViewItem In Me.lvLekcja.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.DeleteLekcja)
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        FetchLekcja()
        GetData(CType(cbPlan.SelectedItem, PlanComboItem).ID.ToString, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString)
        Dim SH As New SeekHelper
        Me.EnableActivityButtons(False)
        SH.FindPostRemovedListViewItemIndex(Me.lvLekcja, DeletedIndex)
        Cursor = Cursors.Default
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MySQLTrans.Rollback()
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub

  Private Sub cmdEditActivity_Click(sender As Object, e As EventArgs) Handles cmdEditActivity.Click
    EditLekcja()
  End Sub
  Private Sub EditLekcja()
    Dim MySQLTrans As MySqlTransaction = Nothing
    Try
      Dim dlgEdit As New dlgLekcja
      'sprawdzić edycję
      With dlgEdit
        .Text = "Edycja parametrów zajęć"
        .OK_Button.Text = "Zapisz"
        .IsNewMode = False
        .lblKlasa.Text = IIf(LekcjaFilter = "Klasa", "Klasa: ", "Nauczyciel: ").ToString + CType(cbLekcjaFilter.SelectedItem, CbItem).Nazwa
        .Sala = lvLekcja.SelectedItems(0).SubItems(4).Text
        .txtSala.Text = .Sala
        .cbObsada.Visible = False
        .cbGodzina.Visible = False
        .cbWeekDay.Visible = False
        .txtSala.Enabled = True

        Dim txtObsada, txtGodzina, txtWeekDay As New TextBox
        txtObsada.Text = lvLekcja.SelectedItems(0).SubItems(3).Text
        txtObsada.Size = .cbObsada.Size
        txtObsada.Location = .cbObsada.Location
        txtObsada.Enabled = False
        .Controls.Add(txtObsada)

        txtGodzina.Text = lvLekcja.SelectedItems(0).SubItems(2).Text
        txtGodzina.Size = .cbGodzina.Size
        txtGodzina.Location = .cbGodzina.Location
        txtGodzina.Enabled = False
        .Controls.Add(txtGodzina)

        txtWeekDay.Text = CType(lvLekcja.SelectedItems(0).Group.Tag, CbItem).Nazwa
        txtWeekDay.Size = .cbWeekDay.Size
        txtWeekDay.Location = .cbWeekDay.Location
        txtWeekDay.Enabled = False
        .Controls.Add(txtWeekDay)
        'Dim FCB As New FillComboBox, P As New PlanSQL, SH As New SeekHelper

        'FCB.AddComboBoxComplexItems(.cbGodzina, P.SelectActivityTime(My.Settings.IdSchool))
        'SH.FindComboItem(.cbGodzina, CType(lvLekcja.SelectedItems(0).SubItems(5).Text, Integer))
        '.cbGodzina.Enabled = False

        '.cbWeekDay.Items.Add(CType(lvLekcja.SelectedItems(0).Group.Tag, CbItem))

        '.cbWeekDay.SelectedIndex = 0
        '.cbWeekDay.Enabled = False

        '.LessonNumberByStaff = .GetLessonNumberByStaff()
        '.LoadLessonItems()
        'SH.FindComboItem(.cbObsada, CType(lvLekcja.SelectedItems(0).SubItems(6).Text, Integer))
        .OK_Button.Enabled = False
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Cursor = Cursors.WaitCursor
          Dim DBA As New DataBaseAction, IdLekcja As String, P As New PlanSQL, SH As New SeekHelper
          IdLekcja = Me.lvLekcja.SelectedItems(0).Text
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.UpdateLekcja())
          cmd.Transaction = MySQLTrans
          'cmd.Parameters.AddWithValue("?IdGodzina", CType(.cbGodzina.SelectedItem, CbItem).ID)
          'cmd.Parameters.AddWithValue("?IdObsada", CType(.cbObsada.SelectedItem, LessonComboItem).IdObsada)
          cmd.Parameters.AddWithValue("?IdLekcja", IdLekcja)
          cmd.Parameters.AddWithValue("?Sala", .txtSala.Text.Trim)
          cmd.ExecuteNonQuery()
          MySQLTrans.Commit()

          FetchLekcja()
          GetData(CType(cbPlan.SelectedItem, PlanComboItem).ID.ToString, CType(cbLekcjaFilter.SelectedItem, CbItem).ID.ToString)
          ClearDetails()
          SH.FindListViewItem(Me.lvLekcja, IdLekcja)
          Cursor = Cursors.Default
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

    If rbKlasa.Checked Then
      rbKlasa_CheckedChanged(rbKlasa, e)
    Else
      rbKlasa_CheckedChanged(rbNauczyciel, e)
    End If
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
    PP.Doc.ReportHeader = New String() {"Plan lekcji (ważny od " & CType(cbPlan.SelectedItem, PlanComboItem).StartDate.ToShortDateString & " do " & CType(cbPlan.SelectedItem, PlanComboItem).EndDate.ToShortDateString & ")", If(rbKlasa.Checked, rbKlasa.Text, rbNauczyciel.Text) & ": " & cbLekcjaFilter.Text}
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
  'Public Sub PrnDoc_SelectedPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
  '  Dim Doc As PrintReport = CType(sender, PrintReport)

  '  Dim x As Single = My.Settings.LeftMargin 'Doc.DefaultPageSettings.Margins.Left
  '  Dim y As Single = My.Settings.TopMargin 'Doc.DefaultPageSettings.Margins.Top
  '  Dim PrnVars As New PrintVariables
  '  Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
  '  Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
  '  Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
  '  Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)

  '  '---------------------------------------- Nagłówek i stopka ----------------------------
  '  Doc.DrawHeader(e, x, e.MarginBounds.Top, e.MarginBounds.Width)
  '  Doc.DrawFooter(e, x, e.MarginBounds.Bottom, e.MarginBounds.Width)
  '  Doc.PageNumber += 1
  '  Doc.DrawPageNumber(e, "- " & Doc.PageNumber.ToString & " -", x, y, e.MarginBounds.Width)
  '  If Doc.PageNumber = 1 Then
  '    y += LineHeight
  '    Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 1, Brushes.Black, False)
  '    y += HeaderLineHeight * 2
  '    Doc.DrawText(e, Doc.ReportHeader(1), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 0, Brushes.Black, False)
  '    y += LineHeight * 2
  '  End If
  '  'Dim ColSize As Integer = CInt((e.MarginBounds.Width - LekcjaColSize) / 3)
  '  Dim ColSize As Integer = 50, GodzinaColSize As Integer = 120

  '  Dim LekcjaColSize As Integer = CInt((e.MarginBounds.Width - GodzinaColSize - ColSize * 2)) '500

  '  Dim Kolumna As New List(Of Pole) From
  '  {
  '      New Pole With {.Name = "Nr", .Label = "Nr lekcji", .Size = ColSize},
  '      New Pole With {.Name = "Godzina", .Label = "Godzina lekcyjna", .Size = GodzinaColSize},
  '      New Pole With {.Name = "Lekcja", .Label = "Lekcja", .Size = LekcjaColSize},
  '      New Pole With {.Name = "Sala", .Label = "Sala", .Size = ColSize}
  '  }

  '  Do Until (y + LineHeight * CSng(1.5)) > e.MarginBounds.Bottom Or Doc.Offset(0) > lvLekcja.Groups.Count - 1
  '    If lvLekcja.Groups(Doc.Offset(0)).Items.Count > 0 Then
  '      Doc.DrawText(e, lvLekcja.Groups(Doc.Offset(0)).Name.ToUpper, New Font(TextFont, FontStyle.Bold), x, y, e.MarginBounds.Width, LineHeight * 2, 1, Brushes.Black, False)
  '      y += LineHeight * CSng(2)
  '      Dim ColOffset As Integer = 0
  '      For Each Col In Kolumna
  '        Doc.DrawText(e, Col.Label, PrnVars.BoldFont, x + ColOffset, y, Col.Size, LineHeight * CSng(2), 1, Brushes.Black)
  '        ColOffset += Col.Size
  '      Next
  '      y += LineHeight * CSng(2)
  '      Do Until (y + LineHeight * CSng(3)) > e.MarginBounds.Bottom Or Doc.Offset(1) > lvLekcja.Groups(Doc.Offset(0)).Items.Count - 1
  '        ColSize = 0
  '        Dim i As Integer = 1 '0
  '        For Each Col In Kolumna
  '          Doc.DrawText(e, lvLekcja.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, CType(If(Col.Name = "Lekcja", 0, 1), Byte), Brushes.Black)
  '          ColSize += Col.Size
  '          i += 1
  '        Next
  '        y += LineHeight
  '        Doc.Offset(1) += 1
  '      Loop
  '      If Doc.Offset(1) < lvLekcja.Groups(Doc.Offset(0)).Items.Count Then
  '        e.HasMorePages = True
  '        RaiseEvent NewRow()
  '        Exit Sub
  '      Else
  '        Doc.Offset(1) = 0
  '      End If
  '      y += LineHeight
  '    End If
  '    Doc.Offset(0) += 1

  '  Loop
  '  If Doc.Offset(0) < lvLekcja.Groups.Count Then
  '    e.HasMorePages = True
  '    RaiseEvent NewRow()
  '  Else
  '    Doc.Offset(0) = 0
  '  End If
  'End Sub
  Public Sub PrnDoc_SelectedPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
    Dim Doc As PrintReport = CType(sender, PrintReport)
    'Dim Doc As PrintDocument = CType(sender, PrintDocument)
    'Dim PH As New PrintHelper(e)
    PH.G = e.Graphics
    'PH.PS = e.PageSettings
    'PH.IsPreview = IsPreview
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)
    'Dim PrnVars As New PrintVariables
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
      y += LineHeight * 2
    End If
    'Dim ColSize As Integer = CInt((e.MarginBounds.Width - LekcjaColSize) / 3)
    Dim ColSize As Integer = 50, GodzinaColSize As Integer = 120

    Dim LekcjaColSize As Integer = CInt((PrintWidth - GodzinaColSize - ColSize * 2)) '500

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Nr", .Label = "Nr lekcji", .Size = ColSize},
        New Pole With {.Name = "Godzina", .Label = "Godzina lekcyjna", .Size = GodzinaColSize},
        New Pole With {.Name = "Lekcja", .Label = "Lekcja", .Size = LekcjaColSize},
        New Pole With {.Name = "Sala", .Label = "Sala", .Size = ColSize}
    }

    Do Until (y + LineHeight * CSng(1.5)) > PrintHeight Or Offset(0) > lvLekcja.Groups.Count - 1
      If lvLekcja.Groups(Offset(0)).Items.Count > 0 Then
        PH.DrawText(lvLekcja.Groups(Offset(0)).Name.ToUpper, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight * 2, 1, Brushes.Black, False)
        y += LineHeight * CSng(2)
        Dim ColOffset As Integer = 0
        For Each Col In Kolumna
          PH.DrawText(Col.Label, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, Col.Size, LineHeight * CSng(2), 1, Brushes.Black)
          ColOffset += Col.Size
        Next
        y += LineHeight * CSng(2)
        Do Until (y + LineHeight * CSng(3)) > PrintHeight Or Offset(1) > lvLekcja.Groups(Offset(0)).Items.Count - 1
          ColSize = 0
          Dim i As Integer = 1 '0
          For Each Col In Kolumna
            PH.DrawText(lvLekcja.Groups(Offset(0)).Items(Offset(1)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, CType(If(Col.Name = "Lekcja", 0, 1), Byte), Brushes.Black)
            ColSize += Col.Size
            i += 1
          Next
          y += LineHeight
          Offset(1) += 1
        Loop
        If Offset(1) < lvLekcja.Groups(Offset(0)).Items.Count Then
          e.HasMorePages = True
          RaiseEvent NewRow()
          Exit Sub
        Else
          Offset(1) = 0
        End If
        y += LineHeight
      End If
      Offset(0) += 1

    Loop
    If Offset(0) < lvLekcja.Groups.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
      PageNumber = 0
    End If
  End Sub
End Class

Public Class PlanComboItem
  Public Property ID As Integer
  Public Property Name As String = ""
  Public Property Lock As Boolean
  Public Property StartDate As Date
  Public Property EndDate As Date
  Public Overrides Function ToString() As String
    Return String.Concat(Name, " (Ważny: od ", StartDate.ToShortDateString, " do ", EndDate.ToShortDateString, ")")
  End Function
  Sub New(ByVal IdPlan As Integer, ByVal Nazwa As String, LockPlan As Boolean, DataPoczatkowa As Date, DataKoncowa As Date)
    ID = IdPlan
    Name = Nazwa
    Lock = LockPlan
    StartDate = DataPoczatkowa
    EndDate = DataKoncowa
  End Sub
End Class
Public Class LessonComboItem
  Public Property IdObsada As Integer
  Public Property Lekcja As String
  Public Property TygodniowyWymiarGodzin As Decimal
  Public Overrides Function ToString() As String
    Return Lekcja
  End Function
  Sub New(ByVal ID As Integer, ByVal Nazwa As String, LiczbaGodzin As Decimal)
    IdObsada = ID
    Lekcja = Nazwa
    TygodniowyWymiarGodzin = LiczbaGodzin
  End Sub
End Class