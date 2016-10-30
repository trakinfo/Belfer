Imports System.Windows.Forms

Public Class dlgPrivilege
  Public Event NewPrivilegeAdded(ByVal sender As Object, ByVal UserLogin As String)
  Private Filter(1) As String, dtStudent, dtUser As DataTable
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    'MainForm.ObsadaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    'MainForm.ObsadaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()
    If AddPrivilege() Then
      lvOpiekun1.Items.Clear()
      lvStudent1.Items.Clear()
      lvOpiekun1.Enabled = False
      lvStudent1.Enabled = False
      OK_Button.Enabled = False
    End If
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub
  Public Function AddPrivilege() As Boolean
    Dim DBA As New DataBaseAction, P As New PrivilagesSQL
    Try
      For Each Parent As ListViewItem In lvOpiekun1.Items
        For Each Student As ListViewItem In lvStudent1.Items
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertPrivilege)
          cmd.Parameters.AddWithValue("?Login", Parent.Text)
          cmd.Parameters.AddWithValue("?IdUczen", Student.Text)
          cmd.ExecuteNonQuery()
        Next
        RaiseEvent NewPrivilegeAdded(Me, Parent.Text)
      Next
      Return True
    Catch err As MySqlException
      Select Case err.Number
        Case 1062
          MessageBox.Show("Uprawnienie już istnieje.")
        Case Else
          MessageBox.Show(err.Message + vbNewLine + "Numer błędu: " + err.Number.ToString)
      End Select
      Return False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return False
    End Try
  End Function
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
      'AddColumns(lv)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      If lv.Name.Substring(0, lvOpiekun.Name.Length) = "lvOpiekun" Then
        .Columns.Add("Login", 114, HorizontalAlignment.Left)
        .Columns.Add("Nazwisko i imię opiekuna", 230, HorizontalAlignment.Left)
      Else
        .Columns.Add("IdUczen", 0, HorizontalAlignment.Left)
        .Columns.Add("Nazwisko i imię ucznia", 294, HorizontalAlignment.Left)
        .Columns.Add("Klasa", 50, HorizontalAlignment.Center)
      End If

    End With
  End Sub
  Private Sub dlgParents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvOpiekun)
    ListViewConfig(lvStudent)
    lvOpiekun.MultiSelect = False
    ListViewConfig(lvOpiekun1)
    ListViewConfig(lvStudent1)
    AddColumns(lvOpiekun)
    AddColumns(lvStudent)
    AddColumns(lvOpiekun1)
    AddColumns(lvStudent1)
    lblRecord.Text = "0 z 0"
    lblRecord1.Text = "0 z 0"
    'Dim FCB As New FillComboBox, TS As New TypySzkolSQL, SH As New SeekHelper
    'FCB.AddComboBoxComplexItems(cbTypSzkoly, TS.SelectSchoolTypes)
    'If My.Settings.IdSchoolType.Length > 0 Then SH.FindComboItem(Me.cbTypSzkoly, CType(My.Settings.IdSchoolType, Integer))
    'cbTypSzkoly.Enabled = CType(IIf(cbTypSzkoly.Items.Count > 0, True, False), Boolean)
    'Dim CH As New CalcHelper
    'Me.nudStartYear.Value = CH.StartDateOfSchoolYear.Year
    'FetchUserData()
    'GetParentData()
    ApplyNewConfig()

    Dim SeekStudentCriteria() As String = New String() {"Klasa", "Nazwisko i imię", "Nr ewidencyjny", "Pesel"}
    Dim SeekParentCriteria() As String = New String() {"Login", "Nazwisko i imię"}
    cbSeekParent.Items.AddRange(SeekParentCriteria)
    cbSeekParent.SelectedIndex = 0
    cbSeekStudent.Items.AddRange(SeekStudentCriteria)
    cbSeekStudent.SelectedIndex = 0
  End Sub
  Private Sub ApplyNewConfig()
    lvStudent.Enabled = False
    lvOpiekun.Enabled = False
    FetchUserData()
    FetchStudentData(My.Settings.SchoolYear, My.Settings.IdSchool)
    GetParentData()
    GetStudentData()
    'cbSeekParent.Enabled = True
    'cbSeekStudent.Enabled = True
    'txtSeekParent.Enabled = True
    'txtSeekStudent.Enabled = True
  End Sub
  'Private Sub nudStartYear_ValueChanged(sender As Object, e As EventArgs)
  '  If Not nudStartYear.Created Then Exit Sub
  '  Me.NudEndYear.Value = Me.nudStartYear.Value + 1
  '  If Not cbSzkola.SelectedItem Is Nothing Then
  '    Me.FetchStudentData(String.Concat(nudStartYear.Value.ToString, "/", NudEndYear.Value.ToString), CType(cbSzkola.SelectedItem, CbItem).ID.ToString)
  '    'RefreshData()
  '    GetStudentData()
  '  End If
  'End Sub
  'Private Sub cbSzkola_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  My.Settings.IdSchool = CType(cbSzkola.SelectedItem, CbItem).ID.ToString
  '  My.Settings.Save()
  '  Me.FetchStudentData(String.Concat(nudStartYear.Value.ToString, "/", NudEndYear.Value.ToString), CType(cbSzkola.SelectedItem, CbItem).ID.ToString)
  '  GetStudentData()
  '  nudStartYear.Enabled = True
  '  cbSeekParent.Enabled = True
  '  cbSeekStudent.Enabled = True
  '  txtSeekParent.Enabled = True
  '  txtSeekStudent.Enabled = True


  'End Sub
  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeekParent.TextChanged
    Select Case cbSeekParent.Text   'Me.cbSeek.Text
      Case "Nazwisko i imię"
        Filter(CType(CType(sender, TextBox).Tag, Byte)) = "User LIKE '" & CType(sender, TextBox).Text + "%'"
      Case "Login"
        Filter(CType(CType(sender, TextBox).Tag, Byte)) = "Login LIKE '" & CType(sender, TextBox).Text + "%'"
    End Select
    GetParentData()
  End Sub
  Private Sub txtSeekStudent_TextChanged(sender As Object, e As EventArgs) Handles txtSeekStudent.TextChanged
    Select Case cbSeekStudent.Text   'Me.cbSeek.Text
      Case "Nazwisko i imię"
        Filter(CType(CType(sender, TextBox).Tag, Byte)) = "Student LIKE '" & CType(sender, TextBox).Text + "%'"
      Case "Klasa"
        Filter(CType(CType(sender, TextBox).Tag, Byte)) = "Nazwa_Klasy LIKE '" & CType(sender, TextBox).Text + "%'"
      Case "Nr ewidencyjny"
        If CType(sender, TextBox).Text.Length > 0 Then
          Filter(CType(CType(sender, TextBox).Tag, Byte)) = "NrArkusza LIKE '" & CType(sender, TextBox).Text + "%'"
        Else
          Filter(CType(CType(sender, TextBox).Tag, Byte)) = "NrArkusza LIKE '" & CType(sender, TextBox).Text + "%' OR NrArkusza is null"
        End If
      Case "Pesel"
        If CType(sender, TextBox).Text = "" Then
          Filter(CType(CType(sender, TextBox).Tag, Byte)) = "Pesel is null or Pesel=''"
        Else
          Filter(CType(CType(sender, TextBox).Tag, Byte)) = "Pesel LIKE '" & CType(sender, TextBox).Text & "%'"
        End If
    End Select
    GetStudentData()
  End Sub

  Private Sub FetchUserData()
    Dim P As New PrivilagesSQL, DBA As New DataBaseAction
    dtUser = DBA.GetDataTable(P.SelectUsers)
  End Sub
  Private Sub FetchStudentData(RokSzkolny As String, IdSzkola As String)
    Dim P As New PrivilagesSQL, DBA As New DataBaseAction
    dtStudent = DBA.GetDataTable(P.SelectStudent(RokSzkolny, IdSzkola))
  End Sub
  Private Sub GetParentData()
    Dim FilteredData() As DataRow
    lvOpiekun.Items.Clear()
    FilteredData = dtUser.Select(Filter(0))
    For i As Integer = 0 To FilteredData.GetUpperBound(0)
      lvOpiekun.Items.Add(FilteredData(i).Item(0).ToString, FilteredData(i).Item(0).ToString, "")
      lvOpiekun.Items(lvOpiekun.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
    Next
    lblRecord.Text = "0 z " & lvOpiekun.Items.Count
    lvOpiekun.Columns(1).Width = CInt(IIf(lvOpiekun.Items.Count > 14, 211, 230))
    lvOpiekun.Enabled = CType(IIf(lvOpiekun.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub GetStudentData()
    Dim FilteredData() As DataRow
    lvStudent.Items.Clear()
    FilteredData = dtStudent.Select(Filter(1))
    For i As Integer = 0 To FilteredData.GetUpperBound(0)
      'Dim Item As New ListViewItem(FilteredData(i).Item(0).ToString)
      lvStudent.Items.Add(FilteredData(i).Item(0).ToString, FilteredData(i).Item(0).ToString, "")
      lvStudent.Items(lvStudent.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
      lvStudent.Items(lvStudent.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString)
    Next
    lblRecord1.Text = "0 z " & lvStudent.Items.Count
    lvStudent.Columns(1).Width = CInt(IIf(lvStudent.Items.Count > 14, 275, 294))
    lvStudent.Enabled = CType(IIf(lvStudent.Items.Count > 0, True, False), Boolean)
  End Sub

  Private Sub lvOpiekun_DoubleClick(sender As Object, e As EventArgs) Handles lvOpiekun.DoubleClick
    If cmdAddParent.Enabled Then cmdAddParent_Click(sender, e)
  End Sub

  Private Sub lvOpiekun_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvOpiekun.ItemSelectionChanged
    'MessageBox.Show(lvOpiekun1.Items.ContainsKey(e.Item.Name).ToString)
    If e.IsSelected Then
      lblRecord.Text = lvOpiekun.SelectedItems(0).Index + 1 & " z " & lvOpiekun.Items.Count
      cmdAddParent.Enabled = CType(IIf(Not lvOpiekun1.Items.ContainsKey(e.Item.Name), True, False), Boolean)
    Else
      lblRecord.Text = "0 z " & lvOpiekun.Items.Count

      cmdAddParent.Enabled = False
    End If
  End Sub

  Private Sub lvStudent_DoubleClick(sender As Object, e As EventArgs) Handles lvStudent.DoubleClick
    If cmdAddStudent.Enabled Then cmdAddStudent_Click(sender, e)
  End Sub
  Private Sub lvStudent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvStudent.ItemSelectionChanged
    If e.IsSelected Then
      lblRecord1.Text = lvStudent.SelectedItems(0).Index + 1 & " z " & lvStudent.Items.Count
      cmdAddStudent.Enabled = CType(IIf(Not lvStudent1.Items.ContainsKey(e.Item.Name), True, False), Boolean)
    Else
      lblRecord1.Text = "0 z " & lvStudent.Items.Count
      cmdAddStudent.Enabled = False
    End If
  End Sub

  Private Sub lvOpiekun1_DoubleClick(sender As Object, e As EventArgs) Handles lvOpiekun1.DoubleClick
    cmdDeleteParent_Click(sender, e)
  End Sub
  Private Sub lvOpiekun1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvOpiekun1.ItemSelectionChanged
    If e.IsSelected Then
      cmdDeleteParent.Enabled = True
    Else
      cmdDeleteParent.Enabled = False
    End If
  End Sub

  Private Sub lvStudent1_DoubleClick(sender As Object, e As EventArgs) Handles lvStudent1.DoubleClick
    cmdDeleteStudent_Click(sender, e)
  End Sub
  Private Sub lvStudent1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvStudent1.ItemSelectionChanged
    If e.IsSelected Then
      cmdDeleteStudent.Enabled = True
    Else
      cmdDeleteStudent.Enabled = False
    End If

  End Sub

  Private Sub cmdAddParent_Click(sender As Object, e As EventArgs) Handles cmdAddParent.Click
    For Each Item As ListViewItem In lvOpiekun.SelectedItems
      Dim NewItem As New ListViewItem(Item.Text)
      NewItem.Name = Item.Name
      NewItem.SubItems.Add(Item.SubItems(1).Text)
      lvOpiekun1.Items.Add(NewItem)
    Next
    lvOpiekun1.Columns(1).Width = CInt(IIf(lvOpiekun1.Items.Count > 4, 211, 230))

    cmdAddParent.Enabled = False
    OK_Button.Enabled = CType(IIf(lvStudent1.Items.Count > 0, True, False), Boolean)
    lvOpiekun1.Enabled = CType(IIf(lvOpiekun1.Items.Count > 0, True, False), Boolean)
  End Sub


  Private Sub cmdAddStudent_Click(sender As Object, e As EventArgs) Handles cmdAddStudent.Click
    For Each Item As ListViewItem In lvStudent.SelectedItems
      Dim NewItem As New ListViewItem(Item.Text)
      NewItem.Name = Item.Name
      NewItem.SubItems.Add(Item.SubItems(1).Text)
      NewItem.SubItems.Add(Item.SubItems(2).Text)
      lvStudent1.Items.Add(NewItem)
    Next
    lvStudent1.Columns(1).Width = CInt(IIf(lvStudent1.Items.Count > 4, 211, 230))
    cmdAddStudent.Enabled = False
    OK_Button.Enabled = CType(IIf(lvOpiekun1.Items.Count > 0, True, False), Boolean)
    lvStudent1.Enabled = CType(IIf(lvStudent1.Items.Count > 0, True, False), Boolean)
  End Sub

  Private Sub cbSeekStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSeekStudent.SelectedIndexChanged
    txtSeekStudent.Text = ""
    txtSeekStudent.Focus()
    GetStudentData()
  End Sub

  Private Sub cmdDeleteParent_Click(sender As Object, e As EventArgs) Handles cmdDeleteParent.Click
    For Each Item As ListViewItem In lvOpiekun1.SelectedItems
      lvOpiekun1.Items.Remove(Item)

    Next
    lvOpiekun1.Enabled = CType(IIf(lvOpiekun1.Items.Count > 0, True, False), Boolean)
    OK_Button.Enabled = CType(IIf(lvOpiekun1.Items.Count = 0 OrElse lvStudent1.Items.Count = 0, False, True), Boolean)
  End Sub

  Private Sub cmdDeleteStudent_Click(sender As Object, e As EventArgs) Handles cmdDeleteStudent.Click
    For Each Item As ListViewItem In lvStudent1.SelectedItems
      lvStudent1.Items.Remove(Item)
    Next
    lvStudent1.Enabled = CType(IIf(lvStudent1.Items.Count > 0, True, False), Boolean)
    OK_Button.Enabled = CType(IIf(lvOpiekun1.Items.Count = 0 OrElse lvStudent1.Items.Count = 0, False, True), Boolean)
  End Sub

  'Private Sub cbTypSzkoly_SelectedIndexChanged(sender As Object, e As EventArgs)
  '  My.Settings.IdSchoolType = CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString
  '  My.Settings.Save()
  '  lvStudent.Items.Clear()
  '  lvStudent1.Items.Clear()
  '  Dim FCB As New FillComboBox, S As New SzkolaSQL, SH As New SeekHelper
  '  FCB.AddComboBoxComplexItems(Me.cbSzkola, S.SelectSchoolAlias(CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString))
  '  If My.Settings.IdSchool.Length > 0 Then
  '    SH.FindComboItem(Me.cbSzkola, CType(My.Settings.IdSchool, Integer))
  '  Else
  '    SH.FindComboItem(Me.cbSzkola, CInt(SH.GetDefault(S.SelectDefault(My.Settings.IdSchoolType))))
  '  End If
  '  cbSzkola.Enabled = CType(IIf(cbSzkola.Items.Count > 0, True, False), Boolean)
  'End Sub
End Class
