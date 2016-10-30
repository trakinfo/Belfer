
Public Class frmCurrentCommection

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.CurrentConnectionToolStripMenuItem.Enabled = True
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.CurrentConnectionToolStripMenuItem.Enabled = True
  End Sub
  Private Sub frmWniosek_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    lblRecord.Text = ""
    lblRecord1.Text = ""
    ListViewConfig(lvProcess)
    ListViewConfig(lvKonto)
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

      .AutoResizeColumns(0)
      .HideSelection = False
      '.HoverSelection = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable

      .Items.Clear()
      '.Enabled = False
    End With
  End Sub

  Private Sub GetData()
    lvKonto.Items.Clear()
    lvKonto.Enabled = False
    lvProcess.Items.Clear()
    lvProcess.Enabled = False

    GetProcessList()
    'GetAccount()
  End Sub
  Private Sub GetProcessList()
    Dim A As New AdminSQL, DBA As New DataBaseAction, DT As DataTable
    Try
      DT = DBA.GetDataTable(A.SelectFullProcessList)

      lvProcess.Columns.Clear()

      For Each DC As DataColumn In DT.Columns
        lvProcess.Columns.Add(DC.Caption)
      Next
      For Each DR As DataRow In DT.Rows
        lvProcess.Items.Add(DR.Item(0).ToString)
        For i As Integer = 1 To lvProcess.Columns.Count - 1
          lvProcess.Items(lvProcess.Items.Count - 1).SubItems.Add(DR.Item(i).ToString)
        Next
      Next

      lblRecord.Text = "0 z " & lvProcess.Items.Count
      Me.lvProcess.Enabled = CType(IIf(Me.lvProcess.Items.Count > 0, True, False), Boolean)
    Catch mex As Exception
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
 
    End Try

  End Sub
  Private Sub GetAccount(IP As String)
    Dim A As New AdminSQL, DBA As New DataBaseAction, DT As DataTable
    Try
      DT = DBA.GetDataTable(A.SelectEvents(IP))

      lvKonto.Columns.Clear()
      For Each DC As DataColumn In DT.Columns
        lvKonto.Columns.Add(DC.Caption)
      Next
      For Each DR As DataRow In DT.Rows
        lvKonto.Items.Add(DR.Item(0).ToString)
        For i As Integer = 1 To lvKonto.Columns.Count - 1
          lvKonto.Items(lvKonto.Items.Count - 1).SubItems.Add(DR.Item(i).ToString)
        Next
      Next

      lblRecord1.Text = "0 z " & lvKonto.Items.Count
      Me.lvKonto.Enabled = CType(IIf(Me.lvKonto.Items.Count > 0, True, False), Boolean)
    Catch mex As Exception
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Try

  End Sub
  Private Sub lvStudent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvProcess.ItemSelectionChanged

    If e.IsSelected Then
      cmdDelete.Enabled = True
      GetAccount(e.Item.SubItems(2).Text)
    Else
      cmdDelete.Enabled = False
      lvKonto.Items.Clear()
    End If

  End Sub


  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz zakończyć wskazane połączenie? ", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, A As New AdminSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvProcess.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(A.KillProcess(Item.Text))
          cmd.Transaction = MySQLTrans
          'cmd.Parameters.AddWithValue("?Login", Item.Tag.ToString)
          'cmd.Parameters.AddWithValue("?IdUczen", Item.SubItems(1).Tag.ToString)
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        GetData()
        'Dim SH As New SeekHelper
        'SH.FindPostRemovedListViewItemIndex(Me.lvProcess, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub

  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    GetData()
  End Sub
End Class