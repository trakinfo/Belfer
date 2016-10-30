Public Class frmPrivilege
  Private dtUsers As DataTable, Filter As String = ""

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.UprawnieniaToolStripMenuItem.Enabled = True
    'RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.UprawnieniaToolStripMenuItem.Enabled = True
    'RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub cmdAddNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Me.AddNew()
  End Sub
  Private Sub AddNew()
    Dim dlgAddNew As New dlgPrivilege
    With dlgAddNew
      '.MdiParent = Me.MdiParent
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      RemoveHandler .NewPrivilegeAdded, AddressOf NewPrivilegeAdded
      AddHandler .NewPrivilegeAdded, AddressOf NewPrivilegeAdded
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      cmdAddNew.Enabled = True
      'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
    End With

  End Sub
  'Private Sub EnableCmdAddNew(ByVal sender As Object, ByVal e As FormClosedEventArgs)
  '  Me.cmdAddNew.Enabled = True
  'End Sub
  Private Sub NewPrivilegeAdded(ByVal sender As Object, ByVal UserLogin As String)
    Me.FetchData()
    RefreshData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvFamily, UserLogin)
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.ListViewConfig(Me.lvFamily)
    AddColumns(lvFamily)
    Dim SeekCriteria() As String = New String() {"Login opiekuna", "Nazwisko i imię ucznia", "Nazwisko i imię opiekuna"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    FetchData()
    RefreshData()

    'Me.GetData()
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
      'AddColumns(lv)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("IdUczen", 0, HorizontalAlignment.Left)
      .Columns.Add("Login", 114, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię opiekuna", 330, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię ucznia", 330, HorizontalAlignment.Left)
    End With
  End Sub

  Private Sub FetchData()
    Dim P As New PrivilagesSQL, DBA As New DataBaseAction
    dtUsers = DBA.GetDataTable(P.SelectPrivilege)
  End Sub

  Private Sub GetData()
    Dim FilteredData() As DataRow
    FilteredData = dtUsers.Select(Filter)
    For i As Integer = 0 To FilteredData.GetUpperBound(0)
      Me.lvFamily.Items.Add(FilteredData(i).Item(0).ToString)
      Me.lvFamily.Items(Me.lvFamily.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
      Me.lvFamily.Items(Me.lvFamily.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString)
      Me.lvFamily.Items(Me.lvFamily.Items.Count - 1).SubItems.Add(FilteredData(i).Item(3).ToString)
    Next

    lblRecord.Text = "0 z " & lvFamily.Items.Count
    lvFamily.Columns(1).Width = CInt(IIf(lvFamily.Items.Count > 23, 95, 100))
    Me.lvFamily.Enabled = CType(IIf(Me.lvFamily.Items.Count > 0, True, False), Boolean)

  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvFamily.SelectedItems(0).Index + 1 & " z " & lvFamily.Items.Count

      With dtUsers.Select("Login='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(4).ToString
        lblIP.Text = .Item(5).ToString
        lblData.Text = .Item(6).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvFamily.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub

  Private Sub lvUsers_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvFamily.ItemSelectionChanged
    If e.IsSelected Then
      'EnableButtons(True)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtUsers.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
      GetDetails(e.Item.SubItems(1).Text)
    Else
      EnableButtons(False)
      clearDetails()
    End If
  End Sub
  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, P As New PrivilagesSQL
      For Each Item As ListViewItem In Me.lvFamily.SelectedItems
        DeletedIndex = Item.Index
        Dim cmd As MySqlCommand = DBA.CreateCommand(P.DeletePrivilege)
        cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
        cmd.Parameters.AddWithValue("?Login", Item.SubItems(1).Text)
        cmd.ExecuteNonQuery()
      Next
      RefreshData()
      Dim SH As New SeekHelper
      SH.FindPostRemovedListViewItemIndex(Me.lvFamily, DeletedIndex)
    End If

  End Sub



  'Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
  '  Dim LinePen As New Pen(Color.White, 1)
  '  e.Graphics.DrawLine(LinePen, New Point(0, Me.Panel1.Height - 7), New Point(Me.Panel1.Width - 1, Me.Panel1.Height - 7))

  'End Sub

  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię ucznia"
        Filter = "Student LIKE '" & Me.txtSeek.Text + "%'"
      Case "Nazwisko i imię opiekuna"
        Filter = "Opiekun LIKE '" & Me.txtSeek.Text + "%'"
      Case "Login opiekuna"
        Filter = "Login LIKE '" & Me.txtSeek.Text + "%'"
    End Select
    Me.EnableButtons(False)
    Me.RefreshData()
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub
  Private Sub RefreshData()
    lvFamily.Items.Clear()
    ClearDetails()
    FetchData()
    GetData()
  End Sub
End Class