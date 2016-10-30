Public Class frmGrupaPrzedmiotowa
  Private DT As DataTable
  'Public Filter As String = ""
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.GrupyPrzedmiotoweToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.GrupyPrzedmiotoweToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData(Klasa As String)
    Dim DBA As New DataBaseAction, G As New GrupaSQL
    DT = DBA.GetDataTable(G.SelectGroupMember(Klasa, My.Settings.SchoolYear))
  End Sub
  Private Sub frmGrupaPrzedmiotowa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvStudent)
    ApplyNewConfig()
    'FillKlasa()
    'Dim SH As New SeekHelper
    'If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub ApplyNewConfig()
    cbPrzedmiot.Items.Clear()
    cbPrzedmiot.Enabled = False
    cmdDelete.Enabled = False
    lvStudent.Items.Clear()
    lvStudent.Enabled = False
    lblRecord.Text = ""
    FillKlasa()
    'Dim SH As New SeekHelper
    'If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
  End Sub
  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, O As New ObsadaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, "0"))
    Dim SH As New SeekHelper
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    FetchData(CType(cbKlasa.SelectedItem, CbItem).ID.ToString)
    cmdDelete.Enabled = False
    FillPrzedmiot()
  End Sub
  Private Sub FillPrzedmiot()
    cbPrzedmiot.Items.Clear()
    Dim FCB As New FillComboBox, G As New GrupaSQL
    FCB.AddComboBoxComplexItems(cbPrzedmiot, G.SelectPrzedmiot(My.Settings.ClassName, My.Settings.SchoolYear))
    Dim SH As New SeekHelper
    If My.Settings.ObjectName.Length > 0 Then SH.FindComboItem(Me.cbPrzedmiot, CType(My.Settings.ObjectName, Integer))
    cbPrzedmiot.Enabled = CType(IIf(cbPrzedmiot.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbPrzedmiot_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectionChangeCommitted
    My.Settings.ObjectName = CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    'Me.GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
    'Filter = "IdSzkolaPrzedmiot=" & CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString
    cmdAddNew.Enabled = True
    GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
  End Sub
  Private Sub GetData(Filter As String)
    Try
      lvStudent.Items.Clear()
      ClearDetails()

      For j As Integer = 0 To DT.Select("IdSzkolaPrzedmiot=" & Filter).GetUpperBound(0)
        Dim NewItem As New ListViewItem(DT.Select("IdSzkolaPrzedmiot=" & Filter)(j).Item(0).ToString)
        NewItem.SubItems.Add(DT.Select("IdSzkolaPrzedmiot=" & Filter)(j).Item(1).ToString)
        lvStudent.Items.Add(NewItem)
      Next

      lblRecord.Text = "0 z " & lvStudent.Items.Count
      lvStudent.Columns(1).Width = CInt(IIf(lvStudent.Items.Count > 21, 381, 400))
      lvStudent.Enabled = CBool(IIf(lvStudent.Items.Count > 0, True, False))

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try
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
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nazwisko i imiona ucznia", 400, HorizontalAlignment.Left)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvStudent.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ByVal IdObsada As String)
    'Dim FoundRow() As DataRow
    Try
      lblRecord.Text = lvStudent.SelectedItems(0).Index + 1 & " z " & lvStudent.Items.Count
      With DT.Select("ID='" & IdObsada & "'")(0) 'FoundRow(0)
        'Dim User As String = .Item("User").ToString.ToLower
        'Dim Owner As String = .Item("Owner").ToString.ToLower
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")")
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub lvObsada_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvStudent.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      cmdDelete.Enabled = CType(IIf(GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString, True, False), Boolean)
    Else
      ClearDetails()
      'lblRecord.Text = "0 z " & CType(sender, ListView).Items.Count
      cmdDelete.Enabled = False
    End If
  End Sub

  Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Try
      Dim dlgAddNew As New dlgGrupaPrzedmiotowa
      With dlgAddNew
        .Text = "Dodawanie uczniów do grupy przedmiotowej"
        .Klasa = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
        .Przedmiot = CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString
        ListViewConfig(dlgAddNew.lvStudent)

        'With .lvStudent
        '  .Columns.Add("ID", 0, HorizontalAlignment.Left)
        '  .Columns.Add("Nazwisko i imiona ucznia", 316, HorizontalAlignment.Left)
        'End With
        .MaximizeBox = False
        .StartPosition = FormStartPosition.CenterScreen
        AddHandler .NewGroupMemberAdded, AddressOf NewGroupMemberAdded
        .ShowDialog()
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally

    End Try
  End Sub

  Private Sub NewGroupMemberAdded(ByVal InsertedGroupMember As String)
    Me.FetchData(CType(cbKlasa.SelectedItem, CbItem).ID.ToString)
    GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvStudent, InsertedGroupMember)
  End Sub
  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    Dim DBA As New DataBaseAction, G As New GrupaSQL, DeletedIndex As Integer
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      For Each Item As ListViewItem In Me.lvStudent.SelectedItems
        DeletedIndex = Item.Index
        Dim cmd As MySqlCommand = DBA.CreateCommand(G.DeleteGroupMember())
        cmd.Parameters.AddWithValue("ID", Item.Text)
        cmd.Transaction = MySQLTrans
        cmd.ExecuteNonQuery()
      Next
      MySQLTrans.Commit()
      Me.FetchData(CType(cbKlasa.SelectedItem, CbItem).ID.ToString)
      GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
      'If My.Application.OpenForms.OfType(Of dlgObsada )().Any() Then RaiseEvent WychowawcaRemoved()
      Dim SH As New SeekHelper
      cmdDelete.Enabled = False
      SH.FindPostRemovedListViewItemIndex(Me.lvStudent, DeletedIndex)
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
End Class