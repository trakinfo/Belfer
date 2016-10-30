Public Class frmTypySzkol
  Private DT As DataTable
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.TypySzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.TypySzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmTypySzkol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(Me.lvTypySzkol)
    ApplyNewConfig()
    'Me.GetData()
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub ApplyNewConfig()
    lvTypySzkol.Items.Clear()
    'ClearDetails()

    GetData()
  End Sub
  Private Sub FetchData()
    Dim W As New WychowawcaSQL, DBA As New DataBaseAction

  End Sub
  Private Sub GetData()
    Dim DBA As New DataBaseAction, TS As New TypySzkolSQL
    Try
      DT = DBA.GetDataTable(TS.SelectSchoolTypes)
      lvTypySzkol.Items.Clear()
      ClearDetails()
      For j As Integer = 0 To DT.Select().GetUpperBound(0)
        Dim NewItem As New ListViewItem(DT.Select()(j).Item(0).ToString)
        NewItem.SubItems.Add(DT.Select()(j).Item(1).ToString)
        NewItem.SubItems.Add(DT.Select()(j).Item(2).ToString)
        'NewItem.SubItems.Add(DT.Select()(j).Item(3).ToString)

        lvTypySzkol.Items.Add(NewItem)
      Next

      lblRecord.Text = "0 z " & lvTypySzkol.Items.Count
      'lvTypySzkol.Columns(1).Width = CInt(IIf(lvTypySzkol.Items.Count > 26, 181, 200))
      lvTypySzkol.Columns(2).Width = CInt(IIf(lvTypySzkol.Items.Count > 12, 321, 340))
      lvTypySzkol.Enabled = CBool(IIf(lvTypySzkol.Items.Count > 0, True, False))

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
      .FullRowSelect = True
      .GridLines = True
      .CheckBoxes = False
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(0)
      .HideSelection = False
      AddColumns()
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns()
    With lvTypySzkol
      .Columns.Add("ID", 0, HorizontalAlignment.Left)
      .Columns.Add("Typ", 290, HorizontalAlignment.Left)
      .Columns.Add("Opis", 340, HorizontalAlignment.Left)
    End With
  End Sub
  Private Sub NewSchoolTypeAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    Me.RefreshData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(lvTypySzkol, InsertedID)
  End Sub

  Private Sub ClearDetails()
    lblRecord.Text = ""
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ByVal IdObsada As String)
    'Dim FoundRow() As DataRow
    Try
      lblRecord.Text = lvTypySzkol.SelectedItems(0).Index + 1 & " z " & lvTypySzkol.Items.Count
      'FoundRow = DT.Select("ID=" & ID)

      With DT.Select("ID='" & IdObsada & "'")(0) 'FoundRow(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (W³: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(3).ToString
        lblIP.Text = .Item(4).ToString
        lblData.Text = .Item(5).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub RefreshData()
    'Me.lvTypySzkol.Items.Clear()
    GetData()
  End Sub
  Private Sub lvWychowawca_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvTypySzkol.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      'Me.cmdAdd.Enabled = True
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then
        cmdEdit.Enabled = True
        Me.cmdDelete.Enabled = True
      End If
    Else
      ClearDetails()
      lblRecord.Text = "0 z " & CType(sender, ListView).Items.Count
      'Me.cmdAdd.Enabled = True
      cmdEdit.Enabled = False
      Me.cmdDelete.Enabled = False
    End If
  End Sub
  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usun¹æ zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, TS As New TypySzkolSQL, LastItem As Integer
      For Each Item As ListViewItem In Me.lvTypySzkol.SelectedItems
        LastItem = Item.Index
        DBA.ApplyChanges(TS.DeleteSchoolType(Item.Text))
      Next
      Me.RefreshData()
      Dim SH As New SeekHelper
      SH.FindPostRemovedListViewItemIndex(lvTypySzkol, LastItem)

    End If
  End Sub

  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgTypySzkol
    With dlgAddNew
      .Text = "Dodawanie nowego typu szko³y"
      .IsNewMode = True
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen

      AddHandler .NewSchoolTypeAdded, AddressOf NewSchoolTypeAdded
      cmdAddNew.Enabled = False
      .ShowDialog()
      Me.cmdAddNew.Enabled = True
    End With


  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    Me.EditData()
  End Sub
  Private Sub EditData()
    Dim dlgEdit As New dlgTypySzkol
    'Dim ico As New System.Drawing.Icon(Application.StartupPath & "\owl.ico")
    With dlgEdit
      .Text = "Edycja danych typu szko³y"
      .txtTyp.Text = Me.lvTypySzkol.SelectedItems(0).SubItems(1).Text
      .txtOpis.Text = Me.lvTypySzkol.SelectedItems(0).SubItems(2).Text
      '.Icon = gblAppIcon
      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        Dim TS As New TypySzkolSQL, DBA As New DataBaseAction, IdTyp As String
        IdTyp = Me.lvTypySzkol.SelectedItems(0).Text
        Dim cmd As MySqlCommand = DBA.CreateCommand(TS.UpdateSchoolType(Me.lvTypySzkol.SelectedItems(0).Text))
        cmd.Parameters.AddWithValue("?Typ", .txtTyp.Text.Trim)
        cmd.Parameters.AddWithValue("?Opis", .txtOpis.Text.Trim)
        cmd.ExecuteNonQuery()
        'DBA.ApplyChanges(TS.UpdateSchoolType(Typ, Opis, Me.lvTypySzkol.SelectedItems(0).Tag.ToString))
        RefreshData()
        Dim SH As New SeekHelper
        SH.FindListViewItem(lvTypySzkol, IdTyp)
      End If
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub lvTypySzkol_DoubleClick(sender As Object, e As EventArgs) Handles lvTypySzkol.DoubleClick
    EditData()
  End Sub

  'Private Sub lvTypySzkol_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvTypySzkol.ItemSelectionChanged

  '  Me.cmdDelete.Enabled = CType(IIf(e.IsSelected, True, False), Boolean)
  '  Me.cmdEdit.Enabled = CType(IIf(e.IsSelected, True, False), Boolean)

  'End Sub

End Class

