Public Class frmPrzydzialPrzedmiotowDoSzkol
  Private DT As DataTable ', IsOpen As Boolean

  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PrzydzialPrzedmiotowDoSzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PrzydzialPrzedmiotowDoSzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmPrzydzialKlasDoSzkol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    ListViewConfig(lvPrzedmiot)
    AddColumns(lvPrzedmiot)
    lblRecord.Text = ""
    ApplyNewConfig()

  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvPrzedmiot.Items.Clear()
    FetchData()
    GetData()
    ClearDetails()
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, P As New PrzedmiotSQL
    DT = DBA.GetDataTable(P.SelectPrzedmiot(My.Settings.IdSchool))
  End Sub
  Private Sub GetData()
    lvPrzedmiot.Items.Clear()

    For Each row As DataRow In DT.Rows
      Me.lvPrzedmiot.Items.Add(row.Item(0).ToString)
      Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(row.Item(2).ToString)
      Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(row.Item(3).ToString)
      Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(row.Item(4).ToString)
    Next
    ClearDetails()

    lvPrzedmiot.Columns(1).Width = CInt(IIf(lvPrzedmiot.Items.Count > 14, 321, 340))
    Me.lvPrzedmiot.Enabled = CType(IIf(Me.lvPrzedmiot.Items.Count > 0, True, False), Boolean)
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
      .Columns.Add("Przedmiot nauczania", 340, HorizontalAlignment.Left)
      .Columns.Add("Grupa", 50, HorizontalAlignment.Center)
      .Columns.Add("IdPrzedmiot", 0, HorizontalAlignment.Center)
      .Columns.Add("Priorytet", 0, HorizontalAlignment.Center)
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub

  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvPrzedmiot.SelectedItems(0).Index + 1 & " z " & lvPrzedmiot.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(5).ToString
        lblIP.Text = .Item(6).ToString
        lblData.Text = .Item(7).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub


  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvPrzedmiot.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then
        EnableButtons(True)
        If e.ItemIndex = 0 And e.Item.ListView.Items.Count > 1 Then
          cmdStart.Enabled = False
          cmdUp.Enabled = False
        ElseIf e.ItemIndex = e.Item.ListView.Items.Count - 1 And e.Item.ListView.Items.Count > 1 Then
          cmdEnd.Enabled = False
          cmdDown.Enabled = False
        ElseIf e.ItemIndex = 0 And e.ItemIndex = e.Item.ListView.Items.Count - 1 Then
          cmdStart.Enabled = False
          cmdUp.Enabled = False
          cmdEnd.Enabled = False
          cmdDown.Enabled = False
        End If
      End If

    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    cmdStart.Enabled = Value
    cmdUp.Enabled = Value
    cmdEnd.Enabled = Value
    cmdDown.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvPrzedmiot.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub
  'Private Sub frmKlasa_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
  '  'SharedPrzydzialPrzedmiotow.CloseChildren(Me, e)
  'End Sub


  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, P As New PrzedmiotSQL, Trans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction

      Try
        For Each Item As ListViewItem In Me.lvPrzedmiot.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.DeletePrzydzialPrzedmiot())
          cmd.Transaction = Trans

          cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
          cmd.Parameters.AddWithValue("?IdPrzedmiot", Item.SubItems(3).Text)

          cmd.ExecuteNonQuery()
        Next
        Trans.Commit()
        Me.EnableButtons(False)
        'If My.Application.OpenForms.OfType(Of dlgPrzydzialPrzedmiotow)().Any() Then RaiseEvent PrzedmiotRemoved()
        FetchData()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvPrzedmiot, DeletedIndex)
      Catch mex As MySqlException
        Trans.Rollback()
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)

      End Try
    End If

  End Sub
  Private Sub AddPrzedmiot()
    Dim dlgAddNew As New dlgPrzydzialPrzedmiotow
    dlgAddNew.Text = "Przydział Przydmiotów do szkoły"
    ListViewConfig(dlgAddNew.lvPrzedmiot)

    With dlgAddNew.lvPrzedmiot
      .Columns.Add("ID", 0, HorizontalAlignment.Left)
      .Columns.Add("Przedmiot nauczania", 283, HorizontalAlignment.Left)
      .Columns.Add("Priorytet", 0, HorizontalAlignment.Center)
    End With

    dlgAddNew.MaximizeBox = False
    dlgAddNew.StartPosition = FormStartPosition.CenterScreen

    AddHandler dlgAddNew.NewAdded, AddressOf NewAdded
    dlgAddNew.ShowDialog()

  End Sub

  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    FetchData()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvPrzedmiot, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddPrzedmiot()
  End Sub

  ' -------------------------------------- Priorytet przedmiotów -------------------------------------

  Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click, cmdStart.Click, cmdDown.Click, cmdEnd.Click

    Dim NewIndex As New ArrayList, i As Integer = 0, ItemIndex As Integer

    For Each item As ListViewItem In lvPrzedmiot.SelectedItems
      i += 1
      ItemIndex = item.Index
      Select Case CType(sender, Button).Name
        Case "cmdStart"
          'If item.Index > 0 Then
          lvPrzedmiot.Items.Remove(item)
          lvPrzedmiot.Items.Insert(i - 1, item)
          NewIndex.Add(i - 1)
          'End If
        Case "cmdEnd"
          'If item.Index < lvPrzedmiot.Items.Count - 1 Then
          lvPrzedmiot.Items.Remove(item)
          lvPrzedmiot.Items.Insert(lvPrzedmiot.Items.Count, item)
          NewIndex.Add(lvPrzedmiot.Items.Count - i)
          'End If
        Case "cmdUp"
          'If item.Index > 0 Then
          lvPrzedmiot.Items.Remove(item)
          lvPrzedmiot.Items.Insert(ItemIndex - 1, item)
          NewIndex.Add(ItemIndex - 1)
          'End If
        Case "cmdDown"
          'If item.Index < lvPrzedmiot.Items.Count - 1 Then
          lvPrzedmiot.Items.Remove(item)
          lvPrzedmiot.Items.Insert(ItemIndex + 1, item)
          NewIndex.Add(ItemIndex + 1)
          'End If
      End Select
    Next
    SetPriorytet()

    Dim Emr As IEnumerator = NewIndex.GetEnumerator
    While (Emr.MoveNext)
      lvPrzedmiot.Items(CType(Emr.Current, Integer)).Selected = True
    End While
  End Sub
  Private Sub SetPriorytet()
    Dim Emr As IEnumerator, Index As Integer = 0
    Dim Transaction As MySqlTransaction = GlobalValues.gblConn.BeginTransaction
    Emr = lvPrzedmiot.Items.GetEnumerator
    While (Emr.MoveNext)
      Index += 1
      SavePriority(Index.ToString, CType(Emr.Current, ListViewItem).Text, My.Settings.IdSchool, Transaction)
    End While
    Transaction.Commit()
  End Sub
  Private Sub SavePriority(ByVal Priorytet As String, ByVal ID As String, Szkola As String, MysqlTrans As MySqlTransaction)
    Dim DBA As New DataBaseAction, P As New PrzedmiotSQL
    Try
      DBA.ApplyChanges(P.UpdatePriorytetBySchool(Priorytet, ID), MysqlTrans)
    Catch err As MySqlException
      MysqlTrans.Rollback()
      MessageBox.Show(err.Number & err.Message)
    End Try
  End Sub

  Private Sub cmtEdit_Click(sender As Object, e As EventArgs) Handles cmtEdit.Click
    'zrobić edycję przydziału przedmiotu, możliwość zmiany liczby grup, dodawanie i usuwanie grup
  End Sub
End Class

