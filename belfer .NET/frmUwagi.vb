Public Class frmUwagi
  Dim DTNotes As DataTable, InRefresh As Boolean, Filter As String = "IdUczen=0"  ', ChildIsOpen As Boolean

  Private Sub frmTemat_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.UwagiToolStripMenuItem.Enabled = True
    MainForm.cmdUwaga.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmTemat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.UwagiToolStripMenuItem.Enabled = True
    MainForm.cmdUwaga.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, 1, Panel1.Width)
  End Sub
  Private Sub frmUwagi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    If OSFeature.Feature.IsPresent(OSFeature.Themes) Then Me.chkGrupa.Enabled = True
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvUczen)
    ListViewConfig(Me.lvUwagi)
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    'EnableButtons(False)
    
    'ClearDetails()
    Dim CH As New CalcHelper, StartDate, EndDate As Date
    StartDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    EndDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
    InRefresh = True
    If dtDataOd.MaxDate < StartDate Then
      dtDataOd.MaxDate = EndDate 'CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      dtDataOd.MinDate = StartDate 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      dtDataDo.MaxDate = EndDate 'CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      dtDataDo.MinDate = StartDate 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    Else
      dtDataOd.MinDate = StartDate 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      dtDataOd.MaxDate = EndDate 'CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      dtDataDo.MinDate = StartDate 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      dtDataDo.MaxDate = EndDate 'CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
    End If
    dtDataOd.Value = dtDataOd.MinDate
    dtDataDo.Value = CType(IIf(Today >= dtDataDo.MinDate AndAlso Today <= dtDataDo.MaxDate, Today, dtDataDo.MaxDate), Date)
    InRefresh = False
    ResetData()
  End Sub
  Private Sub FillKlasa(ByVal cb As ComboBox)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, W As New WynikiSQL
    FCB.AddComboBoxComplexItems(cb, If(chkVirtual.Checked, W.SelectVirtualClasses(My.Settings.IdSchool, My.Settings.SchoolYear), W.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear)))
    'FCB.AddComboBoxComplexItems(cb, K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Dim SH As New SeekHelper
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
  End Sub
  Private Sub ResetData()
    lvUczen.Items.Clear()
    lvUczen.Enabled = False
    lvUwagi.Items.Clear()
    lvUwagi.Enabled = False
    Filter = "IdUczen=0"
    ClearData()
    FillKlasa(cbKlasa)

  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      .Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .HideSelection = False
      AddColumns(lv)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      If .Name = "lvUczen" Then
        .Columns.Add("Nr", 30, HorizontalAlignment.Center)
        .Columns.Add("Nazwisko i imię", 138, HorizontalAlignment.Left)
      Else
        .Columns.Add("Autor uwagi", 290, HorizontalAlignment.Left)
        .Columns.Add("Typ uwagi", 65, HorizontalAlignment.Center)
        .Columns.Add("Data wystawienia", 140, HorizontalAlignment.Center)
      End If
    End With
  End Sub
  Private Sub FetchData()
    Dim U As New UwagiSQL, DBA As New DataBaseAction, CH As New CalcHelper, SelectString As String
    SelectString = If(chkVirtual.Checked, U.SelectNotes(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear, Me.dtDataOd.Value.ToString("yyyy-MM-dd"), Me.dtDataDo.Value.ToString("yyyy-MM-dd")), U.SelectNotes(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, Me.dtDataOd.Value.ToString("yyyy-MM-dd"), Me.dtDataDo.Value.ToString("yyyy-MM-dd")))
    Me.DTNotes = DBA.GetDataTable(SelectString)
  End Sub
  Private Sub RefreshData()
    Me.FetchData()
    ClearData()

    Me.GetNotes()
    'If Not Application.OpenForms.Item("dlgNewNote") Is Nothing Then Me.cmdAddNew.Enabled = True
  End Sub
  Private Sub ClearData()
    'Me.lvUwagi.Items.Clear()
    Me.txtTrescUwagi.Text = ""
    lblRekord.Text = "0 z 0"
    Me.lblNegatywne.Text = "0"
    Me.lblPozytywne.Text = "0"
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
    Me.lblUser.Text = ""
  End Sub
 
  Private Sub GetPupils()
    Me.lvUczen.Items.Clear()
    Dim Reader As MySqlDataReader, DBA As New DataBaseAction, U As New UwagiSQL, SelectString As String
    SelectString = If(chkVirtual.Checked, U.SelectStudent(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear, dtDataDo.Value), U.SelectStudent(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear))

    Reader = DBA.GetReader(SelectString)
    Try
      While Reader.Read
        lvUczen.Items.Add(Reader.Item(0).ToString)
        lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(Reader.Item(1).ToString)
        lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(Reader.Item(2).ToString)
      End While
      Me.lvUczen.Enabled = CBool(IIf(Me.lvUczen.Items.Count > 0, True, False))
      lvUczen.Columns(2).Width = CInt(IIf(lvUczen.Items.Count > 25, 120, 138))
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      Reader.Close()
    End Try
  End Sub
  Private Sub GetNotes()
    Try
      Me.lvUwagi.Items.Clear()
      Dim PupilNotes() As DataRow
      PupilNotes = Me.DTNotes.Select(Filter)
      For Each N As DataRow In DTNotes.Select(Filter)
        Me.lvUwagi.Items.Add(N.Item(0).ToString)
        lvUwagi.Items(lvUwagi.Items.Count - 1).SubItems.Add(N.Item(1).ToString)
        lvUwagi.Items(lvUwagi.Items.Count - 1).SubItems.Add(N.Item(2).ToString)
        lvUwagi.Items(lvUwagi.Items.Count - 1).SubItems.Add(CType(N.Item(3), Date).ToShortDateString)
      Next
      Dim NoteCount As New Hashtable
      For Each Typ As String In "np"
        Dim Notes() As DataRow
        Notes = Me.DTNotes.Select(Filter + " AND TypUwagi='" + Typ + "'")
        NoteCount.Add(Typ, Notes.GetLength(0).ToString)
      Next
      Me.lblPozytywne.Text = CType(NoteCount("p"), String)
      Me.lblNegatywne.Text = CType(NoteCount("n"), String)

      If Me.chkGrupa.Checked Then
        Me.lvUwagi.ShowGroups = True
        Me.SetGroups()
      Else
        Me.lvUwagi.ShowGroups = False
      End If

      lblRekord.Text = "0 z " & Me.lvUwagi.Items.Count
      Me.lvUwagi.Columns(3).Width = CInt(IIf(Me.lvUwagi.Items.Count > 13, 124, 140))
      Me.lvUwagi.Enabled = CType(IIf(lvUwagi.Items.Count > 0, True, False), Boolean)
      Me.cmdEdit.Enabled = False
      Me.cmdDelete.Enabled = False
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub lvUczen_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvUczen.ItemSelectionChanged
    ClearData()
    If e.IsSelected Then
      Filter = "IdUczen=" + e.Item.Text
      Me.GetNotes()
      cmdAddNew.Enabled = True
    Else
      Filter = "IdUczen=0"
      Me.cmdAddNew.Enabled = False
    End If
    Me.cmdEdit.Enabled = False
    Me.cmdDelete.Enabled = False
  End Sub

  Private Sub chkGrupa_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGrupa.CheckedChanged
    'If Not OSFeature.Feature.IsPresent(OSFeature.Themes) Then Exit Sub
    If Me.chkGrupa.Checked Then
      Me.lvUwagi.ShowGroups = True
      Me.SetGroups()
    Else
      Me.lvUwagi.ShowGroups = False
    End If
  End Sub
  Private Sub SetGroups()
    Me.lvUwagi.Groups.Clear()

    ' Retrieve the hash table corresponding to the column.
    Dim groups As New Hashtable '= CType(groupTables(column), Hashtable)
    groups.Add("p", New ListViewGroup("pozytywne", HorizontalAlignment.Left))
    groups.Add("n", New ListViewGroup("negatywne", HorizontalAlignment.Left))
    ' Copy the groups to an array.
    Dim groupsArray(groups.Count - 1) As ListViewGroup
    groups.Values.CopyTo(groupsArray, 0)

    Me.lvUwagi.Groups.AddRange(groupsArray)

    ' Iterate through the items in myListView, assigning each 
    ' one to the appropriate group.
    For Each item As ListViewItem In Me.lvUwagi.Items
      item.Group = CType(groups(item.SubItems(2).Text), ListViewGroup)
    Next item
  End Sub 'SetGroups
  Private Sub lvUwagi_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvUwagi.DoubleClick
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DTNotes.Select("ID=" + lvUwagi.SelectedItems(0).Text)(0).Item("Owner").ToString.Trim Then Me.EditNote()
  End Sub

  Private Sub lvUwagi_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvUwagi.ItemSelectionChanged
    If e.IsSelected Then
      Me.lblRekord.Text = (e.ItemIndex + 1).ToString + " z " + e.Item.ListView.Items.Count.ToString
      Dim Note() As DataRow
      Note = DTNotes.Select("ID=" + e.Item.Text)
      Me.txtTrescUwagi.Text = Note(0).Item(4).ToString
      'Me.lblUser.Text = String.Concat(GlobalValues.Users.Item(Note(0).Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(Note(0).Item("Owner").ToString.ToLower).ToString, ")")
      Dim User, Owner As String
      User = CType(Note(0).Item("User"), String).ToLower.Trim
      Owner = CType(Note(0).Item("Owner"), String).ToLower.Trim
      If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
        lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (Wł: ", GlobalValues.Users.Item(Owner).ToString, ")")
      Else
        Me.lblUser.Text = User & " (Wł: " & Owner & ")"
      End If
      Me.lblIP.Text = Note(0).Item("ComputerIP").ToString
      Me.lblData.Text = Note(0).Item("Version").ToString
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = Note(0).Item("Owner").ToString.Trim Then
        'EnableButtons(True)
        Me.cmdEdit.Enabled = True
        Me.cmdDelete.Enabled = True
        'Else
        '  If GlobalValues.AppUser.Login = Note(0).Item("Owner").ToString Then
        '    Me.cmdEdit.Enabled = True
        '    Me.cmdDelete.Enabled = True
        '  End If
      End If

    Else
      Me.lblRekord.Text = "0 z " + Me.lvUwagi.Items.Count.ToString
      Me.txtTrescUwagi.Text = ""
      Me.cmdEdit.Enabled = False
      Me.cmdDelete.Enabled = False
      lblUser.Text = ""
      lblData.Text = ""
      lblIP.Text = ""
    End If
  End Sub
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgNote
    With dlgAddNew
      .Name = "dlgNewNote"
      .Text = "Dodawanie nowej uwagi"
      .IsNewMode = True
      .InRefresh = True
      .IdUczen = New List(Of Long)
      .txtAutor.Text = GlobalValues.AppUser.Name
      For Each Item As ListViewItem In lvUczen.SelectedItems
        .IdUczen.Add(CType(Item.Text, Long))
      Next
      .cbTyp.Text = "n"
      .InRefresh = False
      AddHandler .NewAdded, AddressOf Me.NewNoteAdded
      .ShowDialog()
    End With
  End Sub
  Private Sub NewNoteAdded(ByVal InsertedID As String)
    Me.RefreshData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvUwagi, InsertedID)
  End Sub

  Private Sub dtpDataOd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtDataOd.ValueChanged, dtDataDo.ValueChanged
    If Not InRefresh Then Me.RefreshData()
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim U As New UwagiSQL, DeletedIndex As Integer
      Dim DBA As New DataBaseAction
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvUwagi.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(U.DeleteNote)
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        RefreshData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvUwagi, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub

  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    dtDataOd.Enabled = True
    dtDataDo.Enabled = True
    Me.GetPupils()
    Filter = "IdUczen=0"
    Me.RefreshData()
  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    Me.EditNote()
  End Sub
  Private Sub EditNote()
    Dim dlgEdit As New dlgNote
    Dim MySQLTrans As MySqlTransaction = Nothing
    Try
      With dlgEdit
        .Text = "Edycja uwagi"
        .cmdSave.Text = "Zapisz"
        .InRefresh = True
        .txtAutor.Text = Me.lvUwagi.SelectedItems(0).SubItems(1).Text
        .txtUwaga.Text = Me.txtTrescUwagi.Text
        .cbTyp.Text = Me.lvUwagi.SelectedItems(0).SubItems(2).Text
        .dtData.Value = CType(Me.lvUwagi.SelectedItems(0).SubItems(3).Text, Date)
        .InRefresh = False
        .IsNewMode = False
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdNote As String, DBA As New DataBaseAction, U As New UwagiSQL
          IdNote = Me.lvUwagi.SelectedItems(0).Text
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim cmd As MySqlCommand = DBA.CreateCommand(U.UpdateNote)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?TypUwagi", .cbTyp.Text)
          cmd.Parameters.AddWithValue("?TrescUwagi", .txtUwaga.Text.Trim)
          cmd.Parameters.AddWithValue("?Autor", .txtAutor.Text.Trim)
          cmd.Parameters.AddWithValue("?Data", .dtData.Value.ToShortDateString)
          cmd.Parameters.AddWithValue("?IdUwaga", IdNote)
          cmd.ExecuteNonQuery()
          MySQLTrans.Commit()
          Me.RefreshData()
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvUwagi, IdNote)
        End If
      End With
    Catch myex As MySqlException
      MessageBox.Show(myex.Message & vbNewLine & myex.InnerException.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkVirtual.CheckedChanged
    ResetData()
  End Sub
End Class

