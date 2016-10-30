Public Class frmParentLogging
  Private DT As DataTable
  Public Filter As String = "", InRefresh As Boolean, DateFilter As String = ""
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ParentLoggingToolStripMenuItem.Enabled = True
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ParentLoggingToolStripMenuItem.Enabled = True
  End Sub
  Private Sub frmZdarzenia_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, 0, 475, Me.Width)
  End Sub

  Private Sub frmReklamacja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    lblRecord.Text = ""
    ListViewConfig(lvEvents)
    AddColumns(lvEvents)
    Dim CH As New CalcHelper
    InRefresh = True
    dtDataOd.Value = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    SetDateFilter()
    InRefresh = False

    LoadClassItems(cbKlasa)
    If My.Settings.ClassName.Length > 0 Then
      For Each Item As SchoolClassComboItem In cbKlasa.Items
        If Item.ID = CType(My.Settings.ClassName, Integer) Then
          cbKlasa.SelectedIndex = cbKlasa.Items.IndexOf(Item)
          Exit For
        End If
      Next
    End If
  End Sub
  Private Sub LoadClassItems(cb As ComboBox)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, K As New KolumnaSQL
    cb.Items.Clear()
    Try
      R = DBA.GetReader(K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
      While R.Read()
        cb.Items.Add(New SchoolClassComboItem(R.GetInt32("ID"), R.GetString("Nazwa_Klasy"), R.GetBoolean("Virtual")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub
  Private Sub SetDateFilter()
    DateFilter = " AND (TimeIn >= #" & dtDataOd.Value & "# AND TimeIn <= #" & dtDataDo.Value & "#)"

  End Sub
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
      .Columns.Add("Data logowania", 120, HorizontalAlignment.Center)
      .Columns.Add("Data wylogowania", 120, HorizontalAlignment.Center)
    End With
  End Sub
  Private Sub dtpDataOd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    If Created = False Then Exit Sub
    'DateFilter = " AND TimeIn between #" & dtDataOd.Value & "# AND #" & dtDataDo.Value & "#"
    SetDateFilter()
    If Not InRefresh Then GetData()
  End Sub
  Private Sub FetchData(Klasa As String)
    Cursor = Cursors.WaitCursor
    Try
      Dim DBA As New DataBaseAction, A As New AdminSQL, CH As New CalcHelper
      DT = DBA.GetDataTable(A.SelectEvents(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).ToShortDateString, Date.Today.ToShortDateString, My.Settings.SchoolYear, Klasa))
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

    For Each row As DataRow In DT.Select(Filter & DateFilter, "TimeIN DESC")
      Dim NewItem As New ListViewItem With {.UseItemStyleForSubItems = False, .Text = row.Item(0).ToString}

      NewItem.SubItems.Add(row.Item("Login").ToString)
      NewItem.SubItems.Add(row.Item("Name").ToString)
      NewItem.SubItems.Add(row.Item("ComputerIP").ToString)
      NewItem.SubItems.Add(row.Item("TimeIn").ToString)
      NewItem.SubItems.Add(row.Item("TimeOut").ToString)
      lvEvents.Items.Add(NewItem)
    Next
    lblRecord.Text = "0 z " & lvEvents.Items.Count
    lvEvents.Columns(2).Width = CInt(IIf(lvEvents.Items.Count > 22, 181, 200))
    Me.lvEvents.Enabled = CType(IIf(Me.lvEvents.Items.Count > 0, True, False), Boolean)
    Cursor = Cursors.Default
  End Sub

  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    FetchData(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
    GetData()
  End Sub

  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    cmdRefresh.Enabled = False
    lvEvents.Items.Clear()
    lvEvents.Enabled = False
    FetchData(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
    Dim S As New StatystykaSQL, FCB As New FillComboBox
    FCB.AddComboBoxComplexItems(cbStudent, S.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString))
    cbStudent.Enabled = If(cbStudent.Items.Count > 0, True, False)
  End Sub

  Private Sub cbStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbStudent.SelectedIndexChanged
    cmdRefresh.Enabled = True
    Filter = "IdUczen=" & CType(cbStudent.SelectedItem, CbItem).ID
    GetData()
  End Sub

  Private Sub lvEvents_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvEvents.ItemSelectionChanged
    If e.IsSelected Then
      lblRecord.Text = lvEvents.SelectedItems(0).Index + 1 & " z " & lvEvents.Items.Count
    Else
      lblRecord.Text = "0 z " & lvEvents.Items.Count
    End If
  End Sub

End Class

