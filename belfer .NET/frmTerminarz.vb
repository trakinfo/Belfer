Public Class frmTerminarz
  Private dtTerminarz As DataTable
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.TerminarzToolStripMenuItem.Enabled = True
    MainForm.cmdTerminarz.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.TerminarzToolStripMenuItem.Enabled = True
    MainForm.cmdTerminarz.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmTerminarz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    cmdTimeSpan.Text = mcData.SelectionStart.ToLongDateString & " (" & mcData.SelectionStart.ToString("dddd") & ") " & Chr(151) & " " & mcData.SelectionEnd.ToLongDateString & " (" & mcData.SelectionEnd.ToString("dddd") & ")"
    ListViewConfig(lvTerminarz)
    ApplyNewConfig()
  End Sub

  Private Sub ApplyNewConfig()
    Try
      ClearDetails()
      'Dim CH As New CalcHelper
      'If mcData.MaxDate < CH.StartDateOfSchoolYear(My.Settings.SchoolYear) Then
      '  mcData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      '  mcData.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      'Else
      '  mcData.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      '  mcData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      'End If
      Dim CH As New CalcHelper, StartDate, EndDate As Date
      StartDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      EndDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      If StartDate.DayOfWeek = 0 Then
        StartDate.AddDays(1)
      ElseIf StartDate.DayOfWeek = 6 Then
        StartDate.AddDays(2)
      End If
      If mcData.MaxDate < StartDate Then
        mcData.MaxDate = EndDate
        mcData.MinDate = StartDate
      Else
        mcData.MinDate = StartDate
        mcData.MaxDate = EndDate
      End If
      mcData.GetDisplayRange(True)
      'If Today > mcData.MaxDate Then
      '  mcData.SelectionStart = CH.StartDateOfWeek(mcData.MaxDate)
      '  mcData.SelectionEnd = CH.EndDateOfWeek(mcData.MaxDate)
      'Else
      '  mcData.SelectionStart = CType(IIf(Today >= mcData.MinDate, CH.StartDateOfWeek(Today), mcData.MinDate), Date)
      '  mcData.SelectionEnd = CType(IIf(Today >= mcData.MinDate, CH.EndDateOfWeek(Today), CH.EndDateOfWeek(mcData.MinDate)), Date)
      'End If
      Dim e As DateRangeEventArgs = Nothing
      If Today < mcData.MinDate Then
        e = New DateRangeEventArgs(mcData.MinDate, Nothing)
      ElseIf Today > mcData.MaxDate Then
        e = New DateRangeEventArgs(mcData.MaxDate, Nothing)
      Else
        e = New DateRangeEventArgs(Today, Nothing)
      End If
      mcData_DateChanged(mcData, e)
      FillKlasa(cbKlasa)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub SetDateSelection(SelectedDate As Date)
    Dim CH As New CalcHelper
    If CH.StartDateOfWeek(SelectedDate) < mcData.MinDate Then
      mcData.SelectionStart = mcData.MinDate
    Else
      mcData.SelectionStart = CH.StartDateOfWeek(SelectedDate)
    End If
    If CH.EndDateOfWeek(SelectedDate) <= mcData.MaxDate Then
      mcData.SelectionEnd = CH.EndDateOfWeek(SelectedDate)
    Else
      mcData.SelectionEnd = mcData.MaxDate
    End If
  End Sub
  Private Sub cmdTimeSpan_Click(sender As Object, e As EventArgs) Handles cmdTimeSpan.Click
    mcData.Visible = Not mcData.Visible
  End Sub

  Private Sub mcData_DateChanged(sender As Object, e As DateRangeEventArgs) Handles mcData.DateSelected
    Try
      'Dim CH As New CalcHelper
      'mcData.SelectionStart = CH.StartDateOfWeek(e.Start)
      'mcData.SelectionEnd = CH.EndDateOfWeek(e.Start)
      SetDateSelection(e.Start)
      cmdTimeSpan.Text = mcData.SelectionStart.ToLongDateString & " (" & mcData.SelectionStart.ToString("dddd") & ") " & Chr(151) & " " & mcData.SelectionEnd.ToLongDateString & " (" & mcData.SelectionEnd.ToString("dddd") & ")"
      mcData.Visible = False 'Not mcData.Visible
      If cbKlasa.SelectedItem IsNot Nothing Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub cmdForward_Click(sender As Object, e As EventArgs) Handles cmdForward.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim CH As New CalcHelper
      'If mcData.MaxDate >= mcData.SelectionEnd.AddDays(7) Then
      '  mcData.SelectionStart = CH.StartDateOfWeek(mcData.SelectionStart.AddDays(7))
      '  mcData.SelectionEnd = CH.EndDateOfWeek(mcData.SelectionStart)
      'End If
      If mcData.SelectionEnd.AddDays(7) > mcData.MaxDate Then
        mcData.SelectionEnd = mcData.MaxDate
      Else
        mcData.SelectionEnd = mcData.SelectionEnd.AddDays(7)
      End If
      mcData.SelectionStart = CH.StartDateOfWeek(mcData.SelectionEnd)
      Dim ev As New DateRangeEventArgs(mcData.SelectionStart, mcData.SelectionEnd)
      mcData_DateChanged(mcData, ev)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
    Cursor = Cursors.Default
  End Sub

  Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
    Try
      Cursor = Cursors.WaitCursor
      Dim CH As New CalcHelper
      'If mcData.SelectionStart.AddDays(-7) >= mcData.MinDate Then
      '  
      '  mcData.SelectionStart = CH.StartDateOfWeek(mcData.SelectionStart.AddDays(-7))
      '  mcData.SelectionEnd = CH.EndDateOfWeek(mcData.SelectionStart)
      'End If
      If mcData.SelectionStart.AddDays(-7) >= mcData.MinDate Then
        mcData.SelectionStart = mcData.SelectionStart.AddDays(-7) 'CH.StartDateOfWeek(mcData.SelectionStart.AddDays(-7))
      Else
        mcData.SelectionStart = mcData.MinDate
      End If
      mcData.SelectionEnd = CH.EndDateOfWeek(mcData.SelectionStart)
      Dim ev As New DateRangeEventArgs(mcData.SelectionStart, mcData.SelectionEnd)
      mcData_DateChanged(mcData, ev)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
    Cursor = Cursors.Default
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
      .Enabled = False
    End With
  End Sub
  Private Sub SetColumns(lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Lekcja", 60, HorizontalAlignment.Center)
      '.Columns.Add("L.p.", 30, HorizontalAlignment.Center)
      .Columns.Add("Przedmiot", 195, HorizontalAlignment.Left)
      .Columns.Add("Zdarzenie", 450, HorizontalAlignment.Left)
      .Columns.Add("Status", 100, HorizontalAlignment.Center)
      '.Columns.Add("Nb", 35, HorizontalAlignment.Center)
      .Columns.Add("DataLekcji", 0, HorizontalAlignment.Left)
      .Columns.Add("IdGodzina", 0, HorizontalAlignment.Left)
    End With
  End Sub


  Private Sub FillKlasa(ByVal cb As ComboBox)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, K As New KolumnaSQL
    FCB.AddComboBoxComplexItems(cb, K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
    'cb.Enabled = False
    'cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    If cb.Items.Count > 0 Then
      Dim SH As New SeekHelper
      If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(cb, CType(My.Settings.ClassName, Integer))
      cb.Enabled = True 'CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Else
      cb.Enabled = False
    End If
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    FetchData()
    GetData()
    ClearDetails()
    cmdAddNew.Enabled = True
    EnableButtons(False)
  End Sub
  Private Sub EnableButtons(Value As Boolean)
    cmdEdit.Enabled = Value
    Me.cmdDelete.Enabled = Value
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvTerminarz.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ID As Integer)
    Try
      lblRecord.Text = lvTerminarz.SelectedItems(0).Index + 1 & " z " & lvTerminarz.Items.Count
      With dtTerminarz.Select("ID=" & ID)(0)
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
  Private Sub GetData()
    Try
      lvTerminarz.Items.Clear()
      lvTerminarz.Groups.Clear()

      LvNewItem(lvTerminarz)
      lvTerminarz.Columns(3).Width = CInt(IIf(lvTerminarz.Items.Count > 6, 431, 450))
      lvTerminarz.Enabled = CType(IIf(lvTerminarz.Items.Count > 0, True, False), Boolean)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub FetchData()
    Dim T As New TerminarzSQL, DBA As New DataBaseAction ', 'CH As New CalcHelper
    dtTerminarz = DBA.GetDataTable(T.SelectEvent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, CbItem).ID.ToString, mcData.SelectionStart.ToShortDateString, mcData.SelectionEnd.ToShortDateString))
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView)
    Dim DayOfWeek As DataTable = dtTerminarz.DefaultView.ToTable(True, "DzienTygodnia", "Data") 'New DataView(dtTerminarz, "DzienTygodnia", DataViewRowState.CurrentRows)
    For Each R As DataRow In DayOfWeek.Rows
      Dim NG As New ListViewGroup(String.Concat(CType(R.Item("Data"), Date).ToShortDateString, " --> ", WeekdayName(CType(R.Item("DzienTygodnia"), Integer), False)))
      NG.HeaderAlignment = HorizontalAlignment.Center
      LV.Groups.Add(NG)
      For Each row As DataRow In dtTerminarz.Select("DzienTygodnia='" & R.Item("DzienTygodnia").ToString & "'")
        Dim NewItem As New ListViewItem(row.Item(0).ToString, NG)
        NewItem.UseItemStyleForSubItems = True
        If CType(row.Item("Status"), GlobalValues.EventStatus) = GlobalValues.EventStatus.Zaplanowane Then
          NewItem.ForeColor = Color.ForestGreen
        Else
          NewItem.ForeColor = Color.Blue
        End If
        NewItem.SubItems.Add(row.Item("NrLekcji").ToString)
        NewItem.SubItems.Add(row.Item("Przedmiot").ToString)
        NewItem.SubItems.Add(row.Item("Info").ToString)
        NewItem.SubItems.Add(CType(row.Item("Status"), GlobalValues.EventStatus).ToString)
        NewItem.SubItems.Add(row.Item("Data").ToString)
        NewItem.SubItems.Add(row.Item("IdGodzina").ToString)
        LV.Items.Add(NewItem)
      Next
    Next
  End Sub
  Private Sub lvTemat_DoubleClick(sender As Object, e As EventArgs) Handles lvTerminarz.DoubleClick
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTerminarz.Select("ID=" & lvTerminarz.SelectedItems(0).Text)(0).Item("Owner").ToString.ToLower.Trim Then EditEvent()
  End Sub

  Private Sub lvTemat_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvTerminarz.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(CType(e.Item.Text, Integer))
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTerminarz.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.ToLower.Trim Then EnableButtons(True)
    Else
      ClearDetails()
      EnableButtons(False)
    End If
  End Sub
  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgZdarzenie
    Try
      With dlgAddNew
        .Text = "Nowe zdarzenie"
        .IsNewMode = True
        .Klasa = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
        Dim CH As New CalcHelper
        If .dtpDataZajec.MaxDate < CH.StartDateOfSchoolYear(My.Settings.SchoolYear) Then
          .dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
          .dtpDataZajec.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
        Else
          .dtpDataZajec.MinDate = Date.Now 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
          .dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
        End If
        .FillGodzina()
        AddHandler dlgAddNew.NewAdded, AddressOf NewTematAdded
        Me.cmdAddNew.Enabled = False
        .ShowDialog()
        cmdAddNew.Enabled = True
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub NewTematAdded(ByVal InsertedID As String)
    FetchData()
    GetData()
    ClearDetails()
    Dim SH As New SeekHelper
    SH.FindListViewItem(lvTerminarz, InsertedID)
  End Sub
  Private Sub EditEvent()
    Dim MySQLTrans As MySqlTransaction = Nothing
    Try
      Dim dlgEdit As New dlgZdarzenie
      Dim txtGodzina As New TextBox

      With dlgEdit
        .IsRefreshMode = True
        .cbGodzina.Visible = False
        txtGodzina.Location = .cbGodzina.Location
        txtGodzina.Size = .cbGodzina.Size
        txtGodzina.Enabled = False
        .Controls.Add(txtGodzina)
        .Text = "Edycja zdarzenia"
        .IsNewMode = False

        Dim DBA As New DataBaseAction, T As New TerminarzSQL

        txtGodzina.Text = DBA.GetSingleValue(T.SelectGodzina(lvTerminarz.SelectedItems(0).SubItems(6).Text)) & " --> "
        txtGodzina.Text += lvTerminarz.SelectedItems(0).SubItems(2).Text
        .txtZdarzenie.Text = lvTerminarz.SelectedItems(0).SubItems(3).Text
        .txtZdarzenie.Enabled = True
        .dtpDataZajec.Value = CType(lvTerminarz.SelectedItems(0).SubItems(5).Text, Date)
        .dtpDataZajec.Enabled = False
        .chkStatus.Checked = CType(CType(System.Enum.Parse(GetType(GlobalValues.EventStatus), lvTerminarz.SelectedItems(0).SubItems(4).Text), GlobalValues.EventStatus), Boolean) 'CType(dtTerminarz.Select("ID=" & lvTerminarz.SelectedItems(0).Text)(0).Item("Status"), Boolean)
        .chkStatus.Enabled = True
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False
        .IsRefreshMode = False
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdEvent As String
          IdEvent = Me.lvTerminarz.SelectedItems(0).Text
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim cmd As MySqlCommand = DBA.CreateCommand(T.UpdateEvent)
          cmd.Transaction = MySQLTrans
          'cmd.Parameters.AddWithValue("?Nr", IIf(.chkStatus.Checked, 0, .nudNr.Value))
          cmd.Parameters.AddWithValue("?Info", .txtZdarzenie.Text.Trim)
          cmd.Parameters.AddWithValue("?ID", IdEvent)
          cmd.Parameters.AddWithValue("?Status", .chkStatus.Checked)
          cmd.ExecuteNonQuery()
          MySQLTrans.Commit()
          cbKlasa_SelectedIndexChanged(Nothing, Nothing)
          'FetchData()

          'ClearDetails()
          'Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvTerminarz, IdEvent)
        End If
      End With
    Catch myex As MySqlException
      MessageBox.Show(myex.Message & vbNewLine & myex.InnerException.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, T As New TerminarzSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvTerminarz.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(T.DeleteEvent)
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
        SH.FindPostRemovedListViewItemIndex(Me.lvTerminarz, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub
  
  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    EditEvent()
  End Sub

End Class