Public Class frmZdarzenia
  Private DT As DataTable
  Private Status As String = "Status IN (1) ", Rola As String = " AND Role IN (0,1,2,4)"
  Public Filter As String = "", InRefresh As Boolean ', DateFilter As String = ""
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.LoggingToolStripMenuItem.Enabled = True
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.LoggingToolStripMenuItem.Enabled = True
  End Sub
  Private Sub frmZdarzenia_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, 0, 475, Me.Width)
  End Sub

  Private Sub frmReklamacja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    lblRecord.Text = ""
    ListViewConfig(lvEvents)
    AddColumns(lvEvents)
    Dim SeekCriteria() As String = New String() {"Nazwisko i imię użytkownika", "Login użytkownika"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    Dim CH As New CalcHelper
    InRefresh = True
    'dtDataOd.Value = Date.Today.Subtract(TimeSpan.FromDays(31)) 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    dtDataOd.Value = Date.Today.AddMonths(-1)
    'SetDateFilter()
    InRefresh = False
    FetchData()
    GetData()

  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub
  'Private Sub SetDateFilter()
  '  DateFilter = " AND (TimeIn >= #" & dtDataOd.Value & "# AND TimeIn <= #" & dtDataDo.Value & "#)"

  'End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(0)
      .HideSelection = False
      '.HoverSelection = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Left)
      .Columns.Add("Login", 100, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię użytkownika", 200, HorizontalAlignment.Left)
      .Columns.Add("Nr IP", 100, HorizontalAlignment.Center)
      .Columns.Add("Status logowania", 100, HorizontalAlignment.Center)
      .Columns.Add("Interfejs", 100, HorizontalAlignment.Center)
      .Columns.Add("Wersja aplikacji", 100, HorizontalAlignment.Center)
      .Columns.Add("Data logowania", 120, HorizontalAlignment.Center)
      .Columns.Add("Data wylogowania", 120, HorizontalAlignment.Center)
    End With
  End Sub
  Private Sub dtpDataOd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtDataOd.ValueChanged, dtDataDo.ValueChanged
    If Created = False Then Exit Sub
    'DateFilter = " AND TimeIn between #" & dtDataOd.Value & "# AND #" & dtDataDo.Value & "#"
    'SetDateFilter()
    'If Not InRefresh Then GetData()
  End Sub
  Private Sub FetchData()
    Cursor = Cursors.WaitCursor
    Try
      Dim DBA As New DataBaseAction, A As New AdminSQL
      DT = DBA.GetDataTable(A.SelectEvents(dtDataOd.Value.ToShortDateString, dtDataDo.Value.ToShortDateString))
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
    Cursor = Cursors.Default
  End Sub
  Private Sub GetData()
    Cursor = Cursors.WaitCursor
    lvEvents.Items.Clear()
    lvEvents.Enabled = False

    For Each row As DataRow In DT.Select(Status & Rola & Filter, "TimeIN DESC")
      Dim NewItem As New ListViewItem With {.UseItemStyleForSubItems = False, .Text = row.Item(0).ToString}

      NewItem.SubItems.Add(row.Item("Login").ToString)
      NewItem.SubItems.Add(row.Item("Name").ToString)
      NewItem.SubItems.Add(row.Item("ComputerIP").ToString)
      Dim StatusMark As String = "", StatusColor As Color = Color.Black
      Select Case CType(row.Item("StatusLogowania"), Byte)
        Case 0
          StatusMark = ChrW(&H4C) 'ChrW(&H44)
          StatusColor = Color.Red
        Case 1
          StatusMark = ChrW(&H4A) 'ChrW(&H43)
          StatusColor = Color.Green
      End Select
      NewItem.SubItems.Add(StatusMark).Font = New Font("Wingdings", 10, FontStyle.Bold)
      NewItem.SubItems(4).ForeColor = StatusColor
      'NewItem.SubItems.Add(row.Item("AppType").ToString)
      Dim AppTypeFont As Font = Nothing
      Select Case row.Item("AppType").ToString
        Case "W"
          StatusMark = ChrW(&HFC) 'ChrW(&H44)
          StatusColor = Color.Blue
          AppTypeFont = New Font("Webdings", 10, FontStyle.Bold)
        Case "D"
          StatusMark = ChrW(&H3A) 'ChrW(&H43)
          StatusColor = Color.DarkOrange
          AppTypeFont = New Font("Wingdings", 10, FontStyle.Bold)
      End Select
      NewItem.SubItems.Add(StatusMark).Font = AppTypeFont
      NewItem.SubItems(5).ForeColor = StatusColor
      NewItem.SubItems.Add(row.Item("AppVer").ToString)
      NewItem.SubItems.Add(row.Item("TimeIn").ToString)
      NewItem.SubItems.Add(row.Item("TimeOut").ToString)
      lvEvents.Items.Add(NewItem)
    Next
    lblRecord.Text = "0 z " & lvEvents.Items.Count
    lvEvents.Columns(2).Width = CInt(IIf(lvEvents.Items.Count > 21, 181, 200))
    Me.lvEvents.Enabled = CType(IIf(Me.lvEvents.Items.Count > 0, True, False), Boolean)
    Cursor = Cursors.Default
  End Sub
  
  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię użytkownika"
        Filter = " AND Name LIKE '" & Me.txtSeek.Text + "%'"
      Case "Login użytkownika"
        Filter = " AND Login LIKE '" & Me.txtSeek.Text + "%'"
    End Select
    Me.GetData()
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub

  Private Sub rbRegistered_CheckedChanged(sender As Object, e As EventArgs) Handles rbNauczyciel.CheckedChanged, rbRodzic.CheckedChanged, rbAllUsers.CheckedChanged, rbAdmin.CheckedChanged
    If CType(sender, RadioButton).Created = False Then Exit Sub
    If CType(sender, RadioButton).Checked Then
      Rola = CType(sender, RadioButton).Tag.ToString
      GetData()
    End If
  End Sub

  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    FetchData()
    GetData()
  End Sub

  Private Sub rbAktywny_CheckedChanged(sender As Object, e As EventArgs) Handles rbAktywny.CheckedChanged, rbNieaktywny.CheckedChanged, rbAllStatus.CheckedChanged
    If CType(sender, RadioButton).Created = False Then Exit Sub
    If CType(sender, RadioButton).Checked Then
      Status = CType(sender, RadioButton).Tag.ToString
      GetData()
    End If
  End Sub

  Private Sub lvEvents_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvEvents.ItemSelectionChanged
    If e.IsSelected Then
      lblRecord.Text = lvEvents.SelectedItems(0).Index + 1 & " z " & lvEvents.Items.Count
    Else
      lblRecord.Text = "0 z " & lvEvents.Items.Count
    End If
  End Sub
End Class

