
Public Class frmWeryfikacjaKont

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.OutofdatePrivilageToolStripMenuItem.Enabled = True
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.OutofdatePrivilageToolStripMenuItem.Enabled = True
  End Sub
  Private Sub frmWniosek_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ListViewConfig(lvUprawnienie)
    AddColumns(lvUprawnienie)
    ListViewConfig(lvKonto)
    AddColumns1(lvKonto)
    'FetchData()
    GetData()
  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Close()
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
      '.OwnerDraw = True
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("Użytkownik", 400, HorizontalAlignment.Left)
      .Columns.Add("Uczeń", 315, HorizontalAlignment.Center)
    End With
  End Sub
  Private Sub AddColumns1(ByVal lv As ListView)
    With lv
      .Columns.Add("Konto bez uprawnień do odczytu wyników nauczania", 600, HorizontalAlignment.Left)
      .Columns.Add("Status", 115, HorizontalAlignment.Center)
    End With
  End Sub
  Private Sub GetData()
    lvKonto.Items.Clear()
    lvKonto.Enabled = False
    lvUprawnienie.Items.Clear()
    lvUprawnienie.Enabled = False
    cmdDelete.Enabled = False
    cmdDeleteKonto.Enabled = False
    cmdDeactivate.Enabled = False
    GetPrivilage()
    GetAccount()
  End Sub
  Private Sub GetPrivilage()
    Dim R As MySqlDataReader = Nothing, A As New AdminSQL, DBA As New DataBaseAction
    Try
      R = DBA.GetReader(A.SelectOutofdatePrivilage(My.Settings.SchoolYear))
      If R.HasRows Then
        While R.Read
          lvUprawnienie.Items.Add(R.GetString("Rodzic")).Tag = R.GetString("UserLogin")
          lvUprawnienie.Items(lvUprawnienie.Items.Count - 1).SubItems.Add(R.GetString("Student")).Tag = R.GetString("IdUczen")
        End While
      End If
      lblRecord.Text = "0 z " & lvUprawnienie.Items.Count
      Me.lvUprawnienie.Enabled = CType(IIf(Me.lvUprawnienie.Items.Count > 0, True, False), Boolean)
    Catch mex As Exception
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
      If R IsNot Nothing Then R.Close()
    End Try

  End Sub
  Private Sub GetAccount()
    Dim R As MySqlDataReader = Nothing, A As New AdminSQL, DBA As New DataBaseAction
    Try
      R = DBA.GetReader(A.SelectNoPrivilageAccount)
      If R.HasRows Then
        While R.Read
          lvKonto.Items.Add(R.GetString("Rodzic")).Tag = R.GetString("Login")
          lvKonto.Items(lvKonto.Items.Count - 1).SubItems.Add(R.GetString("Status"))
        End While
      End If
      lblRecord1.Text = "0 z " & lvKonto.Items.Count
      Me.lvKonto.Enabled = CType(IIf(Me.lvKonto.Items.Count > 0, True, False), Boolean)
    Catch mex As Exception
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
      If R IsNot Nothing Then R.Close()
    End Try
  End Sub

  'Private Sub lvKonto_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles lvKonto.DrawColumnHeader
  '  e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds)
  '  e.DrawText()
  'End Sub
  Private Sub lvStudent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvUprawnienie.ItemSelectionChanged

    If e.IsSelected Then
      cmdDelete.Enabled = True
    Else
      cmdDelete.Enabled = False
    End If

  End Sub
  Private Sub lvKonto_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvKonto.ItemSelectionChanged

    If e.IsSelected Then
      cmdDeleteKonto.Enabled = True
      cmdDeactivate.Enabled = True
    Else
      cmdDeleteKonto.Enabled = False
      cmdDeactivate.Enabled = False
    End If

  End Sub

  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, A As New AdminSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvUprawnienie.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(A.DeleteOutofdatePrivilage)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?Login", Item.Tag.ToString)
          cmd.Parameters.AddWithValue("?IdUczen", Item.SubItems(1).Tag.ToString)
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvUprawnienie, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub

  Private Sub cmdDeleteKonto_Click(sender As Object, e As EventArgs) Handles cmdDeleteKonto.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, A As New AdminSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvKonto.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(A.DeleteNoPrivilageAccount)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?Login", Item.Tag.ToString)

          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvKonto, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub

  Private Sub cmdDeactivate_Click(sender As Object, e As EventArgs) Handles cmdDeactivate.Click
    If MessageBox.Show("Czy na pewno chcesz deaktywować wskazanych użytkowników?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, A As New AdminSQL, ListItem As String = ""
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvKonto.SelectedItems
          ListItem = Item.Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(A.DeactivateNoPrivilageAccount)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?Login", Item.Tag.ToString)

          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        GetData()
        Dim SH As New SeekHelper
        SH.FindListViewItem(Me.lvKonto, ListItem)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub
End Class