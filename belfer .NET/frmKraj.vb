Public Class frmKraj
  Private DT As DataTable
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.KrajeToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.KrajeToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
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
      AddColumns(lv)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Left)
      .Columns.Add("Nazwa", 100, HorizontalAlignment.Left)
      .Columns.Add("Pełna nazwa", 209, HorizontalAlignment.Left)
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub

  'Private Sub frmKlasa_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
  '  SharedKraj.CloseChildren(Me, e)
  'End Sub

  'Private Sub frmKlasa_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
  '  SharedKraj.CloseChildren(Me, e)
  'End Sub
  Private Sub EnableCmdAddNew(ByVal sender As Object, ByVal e As FormClosedEventArgs)
    Me.cmdAddNew.Enabled = True
  End Sub
  Private Sub frmKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    Me.ListViewConfig(Me.lvKraj)
    ApplyNewConfig()
    'Me.GetData()

  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvKraj.Items.Clear()
    ClearDetails()
    GetData()
  End Sub
  Private Sub GetData()
    Dim DBA As New DataBaseAction, K As New KrajSQL
    DT = DBA.GetDataTable(K.SelectKraj)
    For Each row As DataRow In DT.Rows
      Me.lvKraj.Items.Add(row.Item(0).ToString)
      Me.lvKraj.Items(Me.lvKraj.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      Me.lvKraj.Items(Me.lvKraj.Items.Count - 1).SubItems.Add(row.Item(2).ToString)
    Next

    lvKraj.Columns(2).Width = CInt(IIf(lvKraj.Items.Count > 17, 190, 209))
    Me.lvKraj.Enabled = CType(IIf(Me.lvKraj.Items.Count > 0, True, False), Boolean)
    lblRecord.Text = "0 z " & lvKraj.Items.Count
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvKraj.SelectedItems(0).Index + 1 & " z " & lvKraj.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(3).ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvWoj_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvKraj.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvKraj.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
      'EnableButtons(True)
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvKraj.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, K As New KrajSQL
      Try
        For Each Item As ListViewItem In Me.lvKraj.SelectedItems
          DeletedIndex = Item.Index
          DBA.ApplyChanges(K.DeleteKraj(Item.Text))
        Next
        Me.EnableButtons(False)
        lvKraj.Items.Clear()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvKraj, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)

      End Try
    End If

  End Sub
  Public Sub AddKlasa()
    Dim dlgAddNew As New dlgKraj
    With dlgAddNew
      .Text = "Dodawanie nowego kraju"
      .IsNewMode = True
      '.MdiParent = Me.MdiParent
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .txtNazwa.Focus()
      AddHandler dlgAddNew.NewAdded, AddressOf NewAdded
      cmdAddNew.Enabled = False
      .ShowDialog()
      'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
      Me.cmdAddNew.Enabled = True
    End With

  End Sub
  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    lvKraj.Items.Clear()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvKraj, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddKlasa()
  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgKraj

      With dlgEdit
        .IsNewMode = False
        .InRefresh = True
        .Text = "Edycja danych kraju"
        .OK_Button.Text = "Zapisz"
        '.MdiParent = Me.MdiParent
        '.txtKodWoj.Text = Me.lvKraj.SelectedItems(0).Text
        .txtNazwa.Text = Me.lvKraj.SelectedItems(0).SubItems(1).Text
        .txtFullName.Text = Me.lvKraj.SelectedItems(0).SubItems(2).Text
        .InRefresh = False
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, K As New KrajSQL, IdKraj As String
          Dim MySQLTrans As MySqlTransaction

          IdKraj = Me.lvKraj.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(K.UpdateKraj(IdKraj))
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?Nazwa", .txtNazwa.Text.Trim)
            cmd.Parameters.AddWithValue("?FullName", .txtFullName.Text.Trim)
            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MySQLTrans.Rollback()
          End Try
          lvKraj.Items.Clear()
          GetData()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvKraj, IdKraj)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub
End Class

