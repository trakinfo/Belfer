Imports System.Drawing.Printing
Public Class frmZastepstwo
  Public Event NewRow()
  Private dtZastepstwo As DataTable, IsRefresh As Boolean
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub FetchData()
    Dim Z As New ZastepstwoSQL, DBA As New DataBaseAction, CH As New CalcHelper
    If chkNauczyciel.Checked Then
      dtZastepstwo = DBA.GetDataTable(Z.SelectZastepstwo(My.Settings.IdSchool, My.Settings.SchoolYear, CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString))
    Else
      dtZastepstwo = DBA.GetDataTable(Z.SelectZastepstwo(My.Settings.IdSchool, My.Settings.SchoolYear, dtpData.Value))
    End If
  End Sub
  Private Sub frmTemat_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ZastepstwoToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmTemat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ZastepstwoToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmTemat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvZastepstwo)
    ApplyNewConfig()
  End Sub

  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvZastepstwo.Items.Clear()
    lvZastepstwo.Enabled = False
    ClearDetails()
    Dim CH As New CalcHelper
    Dim StartDate As Date = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    Dim EndDate As Date = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)

    If dtpData.MaxDate < StartDate Then 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear) Then
      dtpData.MaxDate = EndDate 'CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      dtpData.MinDate = StartDate 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    Else
      dtpData.MinDate = StartDate 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      dtpData.MaxDate = EndDate 'CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
    End If
    IsRefresh = True
    dtpData.Value = CType(IIf(Today >= dtpData.MinDate AndAlso Today <= dtpData.MaxDate, Today, dtpData.MinDate), Date)
    IsRefresh = False
    FillBelfer(cbNauczyciel, My.Settings.IdSchool, My.Settings.SchoolYear)
    chkNauczyciel_CheckedChanged(Nothing, Nothing)
  End Sub
  Private Overloads Sub FillBelfer(ByVal cb As ComboBox, IdSchool As String, SchoolYear As String)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, P As New PlanSQL
    FCB.AddComboBoxComplexItems(cb, P.SelectBelfer(IdSchool, SchoolYear))
  End Sub
  Private Overloads Sub FillBelfer(ByVal cb As ComboBox, IdSchool As String)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, P As New PlanSQL
    FCB.AddComboBoxComplexItems(cb, P.SelectBelfer(IdSchool))
  End Sub
  Private Overloads Sub ListViewConfig(ByVal lv As ListView)
    With lv
      lv.ShowGroups = True
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
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("lekcja", 257, HorizontalAlignment.Left)
      .Columns.Add("Zastępstwo", 280, HorizontalAlignment.Left)
      .Columns.Add("Komentarz", 150, HorizontalAlignment.Center)
      .Columns.Add("Status", 130, HorizontalAlignment.Center)
      .Columns.Add("ZaNauczyciel", 0, HorizontalAlignment.Center)
      .Columns.Add("InNauczyciel", 0, HorizontalAlignment.Center)
      .Columns.Add("IdLekcja", 0, HorizontalAlignment.Center)
      .Columns.Add("DataZastepstwa", 0, HorizontalAlignment.Center)
      .Columns.Add("InPrzedmiot", 0, HorizontalAlignment.Center)
    End With
  End Sub
  Private Sub GetData()
    'Dim DBA As New DataBaseAction
    Try
      lvZastepstwo.Items.Clear()
      lvZastepstwo.Groups.Clear()
      If chkNauczyciel.Checked Then
        If cbNauczyciel.SelectedItem IsNot Nothing Then LvNewItem(lvZastepstwo, CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString)
      Else
        LvNewItem(lvZastepstwo)
      End If
      lvZastepstwo.Columns(2).Width = CInt(IIf(lvZastepstwo.Items.Count > 17, 261, 280))
      If lvZastepstwo.Items.Count > 0 Then
        lvZastepstwo.Enabled = True 'CType(IIf(lvZastepstwo.Items.Count > 0, True, False), Boolean)
        cmdPrint.Enabled = True
      Else
        lvZastepstwo.Enabled = False  'CType(IIf(lvZastepstwo.Items.Count > 0, True, False), Boolean)
        cmdPrint.Enabled = False
      End If
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView)
    For Each Item As CbItem In cbNauczyciel.Items
      If dtZastepstwo.Select("ZaNauczyciel='" & Item.ID.ToString & "' AND Data=#" & dtpData.Value.ToShortDateString & "#").Length > 0 Then
        Dim NG As New ListViewGroup(Item.Nazwa.ToUpper)
        NG.HeaderAlignment = HorizontalAlignment.Center
        LV.Groups.Add(NG)
        For Each R As DataRow In dtZastepstwo.Select("ZaNauczyciel='" & Item.ID.ToString & "' AND Data=#" & dtpData.Value.ToShortDateString & "#")
          Dim NewItem As New ListViewItem(R.Item("ID").ToString, NG)
          NewItem.UseItemStyleForSubItems = True
          NewItem.ForeColor = CType(IIf(CType(R.Item("Status"), Boolean) = True, Color.Black, Color.Firebrick), Color)
          NewItem.SubItems.Add(R.Item("Lekcja").ToString)
          NewItem.SubItems.Add(R.Item("Zastepstwo").ToString)
          NewItem.SubItems.Add(R.Item("Sala").ToString)
          NewItem.SubItems.Add(CType(R.Item("Status"), GlobalValues.EventStatus).ToString)
          NewItem.SubItems(4).Tag = CType(R.Item("Status"), GlobalValues.EventStatus)
          'NewItem.SubItems.Add(IIf(CType(R.Item("Status"), Boolean) = True, "Zrealizowane", "Niezrealizowane").ToString)
          NewItem.SubItems.Add(R.Item("ZaNauczyciel").ToString)
          NewItem.SubItems.Add(R.Item("InNauczyciel").ToString)
          NewItem.SubItems.Add(R.Item("IdLekcja").ToString)
          NewItem.SubItems.Add(R.Item("Data").ToString)
          NewItem.SubItems.Add(R.Item("InPrzedmiot").ToString)
          LV.Items.Add(NewItem)
        Next
      End If
    Next
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView, IdNauczyciel As String)
    If dtZastepstwo.Select("ZaNauczyciel='" & IdNauczyciel & "'").Length > 0 Then
      'zrobić tylko dla unikalnych dat
      Dim DV As DataView = New DataView(dtZastepstwo)
      DV.RowFilter = "ZaNauczyciel='" & IdNauczyciel & "'"
      DV.Sort = "Data ASC"
      For Each DR As DataRow In DV.ToTable(True, "Data").Rows  'dtZastepstwo.Select("ZaNauczyciel='" & IdNauczyciel & "'")
        'Dim NG As New ListViewGroup(CType(DR.Item("Data"), Date).ToShortDateString & " - " & WeekdayName(CType(DR.Item("Data"), Date).DayOfWeek).ToUpper)
        Dim NG As New ListViewGroup(CType(DR.Item("Data"), Date).ToShortDateString & " - " & CType(DR.Item("Data"), Date).ToString("dddd").ToUpper)
        NG.HeaderAlignment = HorizontalAlignment.Center
        LV.Groups.Add(NG)
        For Each R As DataRow In dtZastepstwo.Select("ZaNauczyciel='" & IdNauczyciel & "' AND Data=#" & CType(DR.Item("Data"), Date).ToShortDateString & "#")
          Dim NewItem As New ListViewItem(R.Item("ID").ToString, NG)
          NewItem.UseItemStyleForSubItems = True
          NewItem.ForeColor = CType(IIf(CType(R.Item("Status"), Boolean) = True, Color.Black, Color.Firebrick), Color)
          NewItem.SubItems.Add(R.Item("Lekcja").ToString)
          NewItem.SubItems.Add(R.Item("Zastepstwo").ToString)
          NewItem.SubItems.Add(R.Item("Sala").ToString)
          NewItem.SubItems.Add(CType(R.Item("Status"), GlobalValues.EventStatus).ToString)
          NewItem.SubItems(4).Tag = CType(R.Item("Status"), GlobalValues.EventStatus)
          'NewItem.SubItems.Add(IIf(CType(R.Item("Status"), Boolean) = True, "Zrealizowane", "Niezrealizowane").ToString)
          NewItem.SubItems.Add(R.Item("ZaNauczyciel").ToString)
          NewItem.SubItems.Add(R.Item("InNauczyciel").ToString)
          NewItem.SubItems.Add(R.Item("IdLekcja").ToString)
          NewItem.SubItems.Add(R.Item("Data").ToString)
          NewItem.SubItems.Add(R.Item("InPrzedmiot").ToString)
          LV.Items.Add(NewItem)
        Next
      Next
    End If
  End Sub
  Private Sub GetDetails(ID As Integer)
    Try
      lblRecord.Text = lvZastepstwo.SelectedItems(0).Index + 1 & " z " & lvZastepstwo.Items.Count
      With dtZastepstwo.Select("ID=" & ID)(0)
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
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvZastepstwo.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub chkNauczyciel_CheckedChanged(sender As Object, e As EventArgs) Handles chkNauczyciel.CheckedChanged
    If Not chkNauczyciel.Created Then Exit Sub
    EnableButtons(False)
    lvZastepstwo.Items.Clear()
    If chkNauczyciel.Checked Then
      cbNauczyciel.Enabled = CType(IIf(cbNauczyciel.Items.Count > 0, True, False), Boolean)
      dtpData.Enabled = False
      If cbNauczyciel.SelectedItem IsNot Nothing Then
        cbNauczyciel_SelectedIndexChanged(Nothing, Nothing)
        'FetchData()
        'GetData()
      End If
    Else
      cbNauczyciel.Enabled = False
      dtpData.Enabled = True
      dtData_ValueChanged(Nothing, Nothing)
      'FetchData()
      'GetData()
    End If
    'dtData_ValueChanged(Nothing, Nothing)
    'ClearDetails()
  End Sub

  Private Sub dtData_ValueChanged(sender As Object, e As EventArgs) Handles dtpData.ValueChanged
    If Not dtpData.Created OrElse IsRefresh = True Then Exit Sub
    Cursor = Cursors.WaitCursor
    FetchData()
    GetData()
    ClearDetails()
    Cursor = Cursors.Default
  End Sub

  Private Sub cbNauczyciel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbNauczyciel.SelectedIndexChanged
    'Cursor = Cursors.WaitCursor
    'FetchData()
    'GetData()
    'ClearDetails()
    'Cursor = Cursors.Default
    dtData_ValueChanged(Nothing, Nothing)
  End Sub

  Private Sub EnableButtons(Value As Boolean)
    'Me.cmdAddNew.Enabled = Value 'CType(IIf(My.Application.OpenForms.OfType(Of dlgLekcja)().Any(), False, True), Boolean)
    cmdEdit.Enabled = Value
    Me.cmdDelete.Enabled = Value
  End Sub
  Private Sub lvZastepstwo_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvZastepstwo.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(CType(e.Item.Text, Integer))
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtZastepstwo.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
      'EnableButtons(True)
    Else
      ClearDetails()
      EnableButtons(False)
    End If
  End Sub
  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, Z As New ZastepstwoSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvZastepstwo.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(Z.DeleteZastepstwo)
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        EnableButtons(False)
        FetchData()
        GetData()
        ClearDetails()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvZastepstwo, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub
  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgZastepstwo
    With dlgAddNew
      .Text = "Nowe zastępstwo"
      .IsNewMode = True
      .dtpData.MinDate = dtpData.MinDate
      .dtpData.MaxDate = dtpData.MaxDate
      .dtpData.Value = dtpData.Value
      FillBelfer(.cbNauczyciel, My.Settings.IdSchool, My.Settings.SchoolYear)
      FillBelfer(.cbNauczyciel2, My.Settings.IdSchool)
      .cbNauczyciel.Enabled = CType(IIf(cbNauczyciel.Items.Count > 0, True, False), Boolean)
      Dim SH As New SeekHelper
      If cbNauczyciel.SelectedItem IsNot Nothing Then SH.FindComboItem(.cbNauczyciel, CType(cbNauczyciel.SelectedItem, CbItem).ID)
      RemoveHandler dlgAddNew.NewAdded, AddressOf NewZastepstwoAdded
      AddHandler dlgAddNew.NewAdded, AddressOf NewZastepstwoAdded
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      cmdAddNew.Enabled = True
    End With
  End Sub
  Private Sub NewZastepstwoAdded(ByVal InsertedID As String)
    FetchData()
    GetData()
    ClearDetails()
    Dim SH As New SeekHelper
    SH.FindListViewItem(lvZastepstwo, InsertedID)
  End Sub
  Private Sub lvTemat_DoubleClick(sender As Object, e As EventArgs) Handles lvZastepstwo.DoubleClick
    EditZastepstwo()
  End Sub
  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    EditZastepstwo()
  End Sub
  Private Sub EditZastepstwo()
    Dim MySQLTrans As MySqlTransaction = Nothing
    Try
      Dim dlgEdit As New dlgZastepstwo
      Dim txtLekcja As New TextBox

      With dlgEdit
        .IsRefreshMode = True
        .Text = "Edycja zastępstwa"
        .IsNewMode = False
        .IdLekcja = lvZastepstwo.SelectedItems(0).SubItems(7).Text
        .cbLekcja.Visible = False
        txtLekcja.Text = lvZastepstwo.SelectedItems(0).SubItems(1).Text
        txtLekcja.Location = .cbLekcja.Location
        txtLekcja.Size = .cbLekcja.Size
        txtLekcja.Enabled = False
        .Panel1.Controls.Add(txtLekcja)
        .dtpData.Value = CType(lvZastepstwo.SelectedItems(0).SubItems(8).Text, Date)
        .dtpData.Enabled = False

        FillBelfer(.cbNauczyciel, My.Settings.IdSchool, My.Settings.SchoolYear)
        Dim SH As New SeekHelper
        SH.FindComboItem(.cbNauczyciel, CType(lvZastepstwo.SelectedItems(0).SubItems(5).Text, Integer))
        .cbNauczyciel.Enabled = False
        FillBelfer(.cbNauczyciel2, My.Settings.IdSchool)
        If lvZastepstwo.SelectedItems(0).SubItems(2).Text.Length > 0 Then
          SH.FindComboItem(.cbNauczyciel2, CType(lvZastepstwo.SelectedItems(0).SubItems(6).Text, Integer))
          .cbNauczyciel2.Enabled = True
          SH.FindComboItem(.cbPrzedmiot, CType(lvZastepstwo.SelectedItems(0).SubItems(9).Text, Integer))
        End If

        .txtSala.Text = lvZastepstwo.SelectedItems(0).SubItems(3).Text
        .chkStatus.Checked = CType(CType(lvZastepstwo.SelectedItems(0).SubItems(4).Tag, GlobalValues.EventStatus), Boolean)
        .chkStatusLekcji.Checked = If(lvZastepstwo.SelectedItems(0).SubItems(2).Text.Length > 0, False, True)
        .chkStatusLekcji.Enabled = True
        .txtSala.Enabled = True
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        .IsRefreshMode = False
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdZastepstwo As String
          IdZastepstwo = Me.lvZastepstwo.SelectedItems(0).Text
          Dim DBA As New DataBaseAction, Z As New ZastepstwoSQL
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim cmd As MySqlCommand = DBA.CreateCommand(Z.UpdateZastepstwo)
          cmd.Transaction = MySQLTrans
          If .chkStatusLekcji.Checked Then
            cmd.Parameters.AddWithValue("?IdNauczyciel", DBNull.Value)
            cmd.Parameters.AddWithValue("?IdPrzedmiot", DBNull.Value)
          Else
            cmd.Parameters.AddWithValue("?IdNauczyciel", CType(.cbNauczyciel2.SelectedItem, CbItem).ID)
            cmd.Parameters.AddWithValue("?IdPrzedmiot", CType(.cbPrzedmiot.SelectedItem, CbItem).ID)
          End If
          cmd.Parameters.AddWithValue("?Sala", .txtSala.Text.Trim)
          cmd.Parameters.AddWithValue("?Status", .chkStatus.CheckState)
          cmd.Parameters.AddWithValue("?IdZastepstwo", IdZastepstwo)
          cmd.ExecuteNonQuery()

          MySQLTrans.Commit()

          Me.EnableButtons(False)
          FetchData()
          GetData()
          ClearDetails()
          'Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvZastepstwo, IdZastepstwo)
        End If
        .Panel1.Controls.Remove(txtLekcja)
      End With
    Catch myex As MySqlException
      MessageBox.Show(myex.Message & vbNewLine & myex.InnerException.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Dim PP As New dlgPrintPreview, DSP As New DataSet
    'DSP.Tables.Add(dtZastepstwo.Copy)
    PP.Doc = New PrintReport(DSP)
    PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
    PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
    PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin
    'PP.Doc.IsPreview = True
    'PP.Doc.DefaultPageSettings.Landscape = False
    AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage
    AddHandler NewRow, AddressOf PP.NewRow
    If chkNauczyciel.Checked Then
      PP.Doc.ReportHeader = New String() {"Przydział zastępstw", cbNauczyciel.Text.ToUpper}
    Else
      PP.Doc.ReportHeader = New String() {"Przydział zastępstw", dtpData.Value.ToShortDateString & " - " & dtpData.Value.ToString("dddd").ToUpper}
    End If
    PP.Width = 1000
    PP.ShowDialog()
    'If PP.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
    '  DSP.Tables.Clear()
    'End If
  End Sub
  Public Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
    Dim Doc As PrintReport = CType(sender, PrintReport)
    Dim x As Single = My.Settings.LeftMargin
    Dim y As Single = My.Settings.TopMargin
    Dim PrnVars As New PrintVariables
    Dim TextFont As Font = My.Settings.TextFont 'New Font(PrnVars.BaseFont.FontFamily, 12)
    Dim HeaderFont As Font = New Font(My.Settings.HeaderFont.FontFamily, 12, FontStyle.Bold)
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    '---------------------------------------- Nagłówek i stopka ----------------------------
    Doc.DrawHeader(e, x, y, PrintWidth)
    Doc.DrawFooter(e, x, e.MarginBounds.Bottom, PrintWidth)
    Doc.PageNumber += 1
    Doc.DrawPageNumber(e, "- " & Doc.PageNumber.ToString & " -", x, y, PrintWidth)
    If Doc.PageNumber = 1 Then
      y += LineHeight
      Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      y += LineHeight * 2
      Doc.DrawText(e, Doc.ReportHeader(1), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      'e.Graphics.DrawLine(New Pen(Brushes.Black), x + CSng((e.PageBounds.Width - e.MarginBounds.Width) / 2), y + HeaderLineHeight, e.MarginBounds.Width, y + HeaderLineHeight)
      Doc.DrawLine(e, x * 2, y + HeaderLineHeight, PrintWidth, y + HeaderLineHeight)
      y += LineHeight * 2
    End If
    Dim CH As New CalcHelper
    Dim Tab As Integer = CInt(CH.MMtoIn(7))
    Dim Kolumna As New List(Of Pole) From
{
    New Pole With {.Name = "Lekcja wg planu", .Size = CInt(PrintWidth * 2 / 7)},
    New Pole With {.Name = "", .Size = Tab},
    New Pole With {.Name = "Zastępstwo", .Size = CInt(PrintWidth * (5 / 7)) - Tab}
}

    Dim ColSize As Integer = 0
    'Dim TotalSize As Integer = Kolumna.Sum(Function(Kol) Kol.Size)
    'x += CType((e.MarginBounds.Width - Kolumna.Sum(Function(Kol) Kol.Size)) / 2, Single) '+ Doc.DefaultPageSettings.PrintableArea.Left
    '---------------------------------------------------- Nagłówki kolumn ------------------------------------

    For Each Col In Kolumna
      With Col
        Doc.DrawText(e, .Name, PrnVars.BoldFont, x + ColSize, y, .Size, LineHeight, 0, Brushes.Black, False, , True)
        ColSize += .Size
      End With
    Next
    y += LineHeight * 2
    ColSize = 0

    '--------------------------------------------- Treść --------------------------------------------------

    Do Until (y + HeaderLineHeight + LineHeight * CSng(2.5)) > e.MarginBounds.Bottom Or Doc.Offset(0) > lvZastepstwo.Groups.Count - 1
      'If Doc.Offset(1) = 0 Then
      Doc.DrawText(e, lvZastepstwo.Groups(Doc.Offset(0)).ToString, HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      'e.Graphics.DrawLine(New Pen(Color.Black), x, y + HeaderLineHeight, CSng(e.MarginBounds.Width * 3 / 5), y + HeaderLineHeight)
      Doc.DrawLine(e, x, y + HeaderLineHeight, CSng(e.MarginBounds.Width * 3 / 5), y + HeaderLineHeight)

      y += LineHeight * CSng(1.5)

      'End If

      Do Until (y + LineHeight) > e.MarginBounds.Bottom Or Doc.Offset(1) > lvZastepstwo.Groups(Doc.Offset(0)).Items.Count - 1
        Doc.DrawText(e, dtZastepstwo.Select("ID=" & lvZastepstwo.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).Text)(0).Item("LekcjaNoHour").ToString, TextFont, x + ColSize, y, Kolumna(0).Size, LineHeight, 0, Brushes.Black, False)
        ColSize += Kolumna(0).Size        'Else
        Dim SD As New ShapeDrawer
        SD.DrawEndArrow(e.Graphics, x + ColSize, y + LineHeight / 2, Kolumna(1).Size - CH.MMtoIn(4), CH.MMtoIn(2), CH.MMtoIn(1.5), False)
        ColSize += Kolumna(1).Size        'Else
        Dim PrintContent As String = ""
        If lvZastepstwo.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(2).Text.Length > 0 Then
          PrintContent = lvZastepstwo.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(2).Text
          PrintContent += If(lvZastepstwo.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(3).Text.Length > 0, " (" & lvZastepstwo.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(3).Text & ")", "")
        Else
          PrintContent = lvZastepstwo.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(3).Text
        End If
        Doc.DrawText(e, PrintContent, TextFont, x + ColSize, y, Kolumna(2).Size, LineHeight, 0, Brushes.Black, False)
        'Doc.DrawText(e, lvZastepstwo.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(2).Text, TextFont, x + ColSize, y, Kolumna(2).Size, LineHeight, 0, Brushes.Black, False)
        ColSize = 0
        y += LineHeight
        Doc.Offset(1) += 1
      Loop

      If Doc.Offset(1) <= lvZastepstwo.Groups(Doc.Offset(0)).Items.Count - 1 Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      End If
      y += LineHeight * 1
      Doc.Offset(1) = 0
      Doc.Offset(0) += 1
    Loop

    If Doc.Offset(0) <= lvZastepstwo.Groups.Count - 1 Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      '  Doc.Offset(1) = 0
      Doc.Offset(0) = 0
      '  Doc.PageNumber = 0
    End If
  End Sub
End Class