Public Class frmPrzedmiot

  Private DT As DataTable, Filter As String = ""
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PrzedmiotyToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PrzedmiotyToolStripMenuItem.Enabled = True
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
      .Columns.Add("Alias", 275, HorizontalAlignment.Left)
      .Columns.Add("Nazwa", 375, HorizontalAlignment.Left)
      .Columns.Add("Typ", 50, HorizontalAlignment.Center)
      '.Columns.Add("Priorytet", 50, HorizontalAlignment.Center)
 
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  'Private Sub EnableCmdAddNew(ByVal sender As Object, ByVal e As FormClosedEventArgs)
  '  Me.cmdAddNew.Enabled = True
  'End Sub
  Private Sub frmKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Me.ListViewConfig(Me.lvPrzedmiot)
    Dim SeekCriteria() As String = New String() {"Nazwa", "Alias", "Typ"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    lblRecord.Text = ""
    ApplyNewConfig()
    'FetchData()
    'Me.GetData()
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvPrzedmiot.Items.Clear()
    ClearDetails()
    GetData()
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, P As New PrzedmiotSQL
    DT = DBA.GetDataTable(P.SelectPrzedmiot)
  End Sub

  Private Sub GetData()
    Try
      FetchData()
      lvPrzedmiot.Items.Clear()
      Dim FilteredData() As DataRow
      FilteredData = DT.Select(Filter)

      For i As Integer = 0 To FilteredData.GetUpperBound(0) 'For Each row As DataRow In DT.Rows
        Me.lvPrzedmiot.Items.Add(FilteredData(i).Item(0).ToString)
        Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
        Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString)
        Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(FilteredData(i).Item(3).ToString)
        'Me.lvPrzedmiot.Items(Me.lvPrzedmiot.Items.Count - 1).SubItems.Add(FilteredData(i).Item(4).ToString)
      Next
      Me.lvPrzedmiot.Columns(1).Width = CInt(IIf(lvPrzedmiot.Items.Count > 19, 256, 275))
      Me.lvPrzedmiot.Enabled = CType(IIf(Me.lvPrzedmiot.Items.Count > 0, True, False), Boolean)
      lblRecord.Text = "0 z " & lvPrzedmiot.Items.Count
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvPrzedmiot.SelectedItems(0).Index + 1 & " z " & lvPrzedmiot.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(4).ToString
        lblIP.Text = .Item(5).ToString
        lblData.Text = .Item(6).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvMiejscowosc_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvPrzedmiot.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvPrzedmiot.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
      'EnableButtons(True)
      If e.ItemIndex = 0 Then
        cmdStart.Enabled = False
        cmdUp.Enabled = False
      ElseIf e.ItemIndex = e.Item.ListView.Items.Count - 1 Then
        cmdEnd.Enabled = False
        cmdDown.Enabled = False
      End If
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
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

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, p As New PrzedmiotSQL
      Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvPrzedmiot.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(p.DeletePrzedmiot)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        Me.EnableButtons(False)
        'lvPrzedmiot.Items.Clear()
        'FetchData()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvPrzedmiot, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If
  End Sub
  Public Sub AddNewData()
    Dim dlgAddNew As New dlgPrzedmiot
    With dlgAddNew
      .IsNewMode = True
      .Text = "Dodawanie nowego przedmiotu nauczania"
      .cbTyp.Items.AddRange(New String() {"P", "Z", "F"})
      .cbTyp.SelectedIndex = 0
      '.MdiParent = Me.MdiParent
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      'dlgAddNew.Opacity = 0.7
      .txtAlias.Focus()
      AddHandler .NewAdded, AddressOf NewAdded
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
      Me.cmdAddNew.Enabled = True
    End With

  End Sub
  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    'lvPrzedmiot.Items.Clear()
    'FetchData()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvPrzedmiot, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddNewData()
  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgPrzedmiot
      With dlgEdit
        .IsNewMode = False
        .Text = "Edycja danych przedmiotu nauczania"
        .cbTyp.Items.AddRange(New String() {"P", "Z", "F"})
        .txtAlias.Text = lvPrzedmiot.SelectedItems(0).SubItems(1).Text
        .txtNazwa.Text = lvPrzedmiot.SelectedItems(0).SubItems(2).Text
        .cbTyp.Text = lvPrzedmiot.SelectedItems(0).SubItems(3).Text
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, P As New PrzedmiotSQL, IdPrzedmiot As String
          Dim MySQLTrans As MySqlTransaction

          IdPrzedmiot = Me.lvPrzedmiot.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.UpdatePrzedmiot)
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?Nazwa", .txtNazwa.Text.Trim)
            cmd.Parameters.AddWithValue("?Alias", .txtAlias.Text.Trim)
            cmd.Parameters.AddWithValue("?Typ", .cbTyp.Text)
            cmd.Parameters.AddWithValue("?ID", IdPrzedmiot)

            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MySQLTrans.Rollback()
            MessageBox.Show(ex.Message)
          End Try
          GetData()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvPrzedmiot, IdPrzedmiot)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Select Case Me.cbSeek.Text
      Case "Alias"
        Filter = "Alias LIKE '" & Me.txtSeek.Text & "%'"
      Case "Nazwa"
        Filter = "Nazwa LIKE '" & Me.txtSeek.Text & "%'"
      Case "Typ"
        Filter = "Typ LIKE '" & txtSeek.Text & "%"
    End Select
    Me.EnableButtons(False)
    GetData()
    'Me.RefreshData()
  End Sub
  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
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
          NewIndex.Add(ItemIndex + i)
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
    Dim Enr As IEnumerator, Index As Integer = 0
    Dim Transaction As MySqlTransaction = GlobalValues.gblConn.BeginTransaction
    Enr = lvPrzedmiot.Items.GetEnumerator
    While (Enr.MoveNext)
      Index += 1
      SavePriority(Index.ToString, CType(Enr.Current, ListViewItem).Text, Transaction)
    End While
    Transaction.Commit()
  End Sub
  Private Sub SavePriority(ByVal Priorytet As String, ByVal ID As String, MysqlTrans As MySqlTransaction)
    Dim DBA As New DataBaseAction, P As New PrzedmiotSQL
    Try
      DBA.ApplyChanges(P.UpdatePriorytet(Priorytet, ID), MysqlTrans)
    Catch err As MySqlException
      MysqlTrans.Rollback()
      MessageBox.Show(err.Number & err.Message)
    End Try
  End Sub
End Class


