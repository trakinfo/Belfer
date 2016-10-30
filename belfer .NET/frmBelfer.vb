Public Class frmBelfer
  Private DT As DataTable
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.NauczycieleToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.NauczycieleToolStripMenuItem.Enabled = True
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
      .Columns.Add("Nazwisko i imię", 339, HorizontalAlignment.Left)
      .Columns.Add("Płeć", 70, HorizontalAlignment.Center)
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub

  'Private Sub EnableCmdAddNew(ByVal sender As Object, ByVal e As FormClosedEventArgs)
  '  Me.cmdAddNew.Enabled = True
  'End Sub
  Private Sub frmKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Me.ListViewConfig(Me.lvBelfer)
    lblRecord.Text = ""
    ApplyNewConfig()
    'Me.GetData()
    '
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    ClearDetails()
    GetData()
  End Sub
  Private Sub GetData()
    Dim DBA As New DataBaseAction, B As New BelferSQL
    lvBelfer.Items.Clear()
    DT = DBA.GetDataTable(B.SelectBelfer)
    For Each row As DataRow In DT.Rows
      Me.lvBelfer.Items.Add(row.Item(0).ToString)
      Me.lvBelfer.Items(Me.lvBelfer.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      Me.lvBelfer.Items(Me.lvBelfer.Items.Count - 1).SubItems.Add(row.Item(2).ToString)
      'Me.lvBelfer.Items(Me.lvBelfer.Items.Count - 1).SubItems.Add(IIf(CType(row.Item(3), Boolean) = True, "M", "K").ToString)
    Next

    lvBelfer.Columns(1).Width = CInt(IIf(lvBelfer.Items.Count > 16, 320, 339))
    Me.lvBelfer.Enabled = CType(IIf(Me.lvBelfer.Items.Count > 0, True, False), Boolean)
    lblRecord.Text = "0 z " & lvBelfer.Items.Count
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvBelfer.SelectedItems(0).Index + 1 & " z " & lvBelfer.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item("User").ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvWoj_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvBelfer.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvBelfer.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      EnableButtons(True)
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    'lblRecord.Text = "0 z " & lvBelfer.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    Dim DBA As New DataBaseAction, DeletedIndex As Integer, B As New BelferSQL
    Try
      For Each Item As ListViewItem In Me.lvBelfer.SelectedItems
        DeletedIndex = Item.Index
        DBA.ApplyChanges(B.DeleteBelfer(Item.Text))
      Next
      Me.EnableButtons(False)
      lvBelfer.Items.Clear()
      Me.GetData()
      Dim SH As New SeekHelper
      SH.FindPostRemovedListViewItemIndex(Me.lvBelfer, DeletedIndex)
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try
  End Sub
  Public Sub AddBelfer()
    Dim dlgAddNew As New dlgBelfer
    With dlgAddNew
      .IsNewMode = True
      .Text = "Dodawanie nowego nauczyciela"
      .cbPlec.Text = "K"
      '.MdiParent = Me.MdiParent
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .txtNazwisko.Focus()
      AddHandler dlgAddNew.NewAdded, AddressOf NewAdded
      'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      cmdAddNew.Enabled = True
    End With
  End Sub
  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    lvBelfer.Items.Clear()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvBelfer, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddBelfer()
  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgBelfer

      With dlgEdit
        .IsNewMode = False
        .InRefresh = True
        .Text = "Edycja danych nauczyciela"
        .OK_Button.Text = "Zapisz"
        '.MdiParent = Me.MdiParent
        '.txtKodWoj.Text = Me.lvKraj.SelectedItems(0).Text
        .txtNazwisko.Text = Me.lvBelfer.SelectedItems(0).SubItems(1).Text.Split(" ".ToCharArray)(0).ToString
        .txtImie.Text = Me.lvBelfer.SelectedItems(0).SubItems(1).Text.Split(" ".ToCharArray)(1).ToString
        .cbPlec.Text = Me.lvBelfer.SelectedItems(0).SubItems(2).Text
        .InRefresh = False
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, B As New BelferSQL, IdBelfer As String
          Dim MySQLTrans As MySqlTransaction

          IdBelfer = Me.lvBelfer.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(B.UpdateBelfer(IdBelfer))
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?Nazwisko", .txtNazwisko.Text.Trim)
            cmd.Parameters.AddWithValue("?Imie", .txtImie.Text.Trim)
            cmd.Parameters.AddWithValue("?Sex", .cbPlec.Text)
            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MySQLTrans.Rollback()
          End Try
          lvBelfer.Items.Clear()
          GetData()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvBelfer, IdBelfer)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub
End Class

