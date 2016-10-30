Public Class frmHarmonogram
  Private DT As DataTable
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.HarmonogramToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.HarmonogramToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmHarmonogram_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Me.ListViewConfig(Me.lvHarmonogram)
    GetData()
    ClearDetails()
    cmdAddNew.Enabled = True
    'lblRecord.Text = "0 z " & lvHarmonogram.Items.Count
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub ApplyNewConfig()
    GetData()
    ClearDetails()
  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      .Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nr lekcji", 50, HorizontalAlignment.Center)
      .Columns.Add("Czas rozpoczęcia", 141, HorizontalAlignment.Center)
      .Columns.Add("Czas zakończenia", 141, HorizontalAlignment.Center)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub GetData()

    Dim DBA As New DataBaseAction, H As New HarmonogramSQL

    DT = DBA.GetDataTable(H.SelectActivityTime(My.Settings.IdSchool))
    Try
      lvHarmonogram.Items.Clear()
      For Each row As DataRow In DT.Rows
        Me.lvHarmonogram.Items.Add(row.Item(0).ToString)
        Me.lvHarmonogram.Items(Me.lvHarmonogram.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
        Me.lvHarmonogram.Items(Me.lvHarmonogram.Items.Count - 1).SubItems.Add(row.Item(2).ToString)
        Me.lvHarmonogram.Items(Me.lvHarmonogram.Items.Count - 1).SubItems.Add(row.Item(3).ToString)
      Next

      lvHarmonogram.Columns(2).Width = CInt(IIf(lvHarmonogram.Items.Count > 15, 122, 141))
      Me.lvHarmonogram.Enabled = CType(IIf(Me.lvHarmonogram.Items.Count > 0, True, False), Boolean)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvHarmonogram.SelectedItems(0).Index + 1 & " z " & lvHarmonogram.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(4).ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvWoj_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvHarmonogram.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvHarmonogram.ItemSelectionChanged
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
    lblRecord.Text = "0 z " & lvHarmonogram.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgHarmonogram

      With dlgEdit
        '.InRefresh = True
        .Text = "Edycja parametrów godziny lekcyjnej"
        .OK_Button.Text = "Zapisz"
        .IsNewMode = False
        .nudNrLekcji.Value = CType(Me.lvHarmonogram.SelectedItems(0).SubItems(1).Text, Integer)
        .dtStartTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(2).Text, DateTime).Hour, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(2).Text, DateTime).Minute, 0)
        .dtEndTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(3).Text, DateTime).Hour, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(3).Text, DateTime).Minute, 0)
        '.InRefresh = False
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, H As New HarmonogramSQL, IdGodzina As String
          Dim MySQLTrans As MySqlTransaction

          IdGodzina = Me.lvHarmonogram.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(H.UpdateActivityTime())
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?NrLekcji", .nudNrLekcji.Value)
            cmd.Parameters.AddWithValue("?ID", IdGodzina)
            cmd.Parameters.AddWithValue("?StartTime", .dtStartTime.Value.ToShortTimeString)
            cmd.Parameters.AddWithValue("?EndTime", .dtEndTime.Value.ToShortTimeString)
            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MySQLTrans.Rollback()
          End Try
          'lvHarmonogram.Items.Clear
          GetData()
          ClearDetails()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvHarmonogram, IdGodzina)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub


  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgHarmonogram, H As New HarmonogramSQL
    With dlgAddNew
      .Text = "Dodawanie godziny lekcyjnej"
      .IsNewMode = True
      If lvHarmonogram.SelectedItems.Count > 0 Then
        .nudNrLekcji.Value = CType(Me.lvHarmonogram.SelectedItems(0).SubItems(1).Text, Integer)
        .dtStartTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(2).Text, DateTime).Hour, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(2).Text, DateTime).Minute, 0)
        .dtEndTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(2).Text, DateTime).Hour, CType(Me.lvHarmonogram.SelectedItems(0).SubItems(2).Text, DateTime).Minute, 0).AddMinutes(45)
      ElseIf lvHarmonogram.Items.Count > 0 Then
        .nudNrLekcji.Value = CType(Me.lvHarmonogram.Items(lvHarmonogram.Items.Count - 1).Text, Integer)
        .dtStartTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, CType(Me.lvHarmonogram.Items(lvHarmonogram.Items.Count - 1).SubItems(2).Text, DateTime).Hour, CType(Me.lvHarmonogram.Items(lvHarmonogram.Items.Count - 1).SubItems(2).Text, DateTime).Minute, 0)
        .dtEndTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, CType(Me.lvHarmonogram.Items(lvHarmonogram.Items.Count - 1).SubItems(2).Text, DateTime).Hour, CType(Me.lvHarmonogram.Items(lvHarmonogram.Items.Count - 1).SubItems(2).Text, DateTime).Minute, 0).AddMinutes(45)
      Else
        .nudNrLekcji.Value = 1
        .dtStartTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, 8, 0, 0)
        .dtEndTime.Value = New DateTime(Now.Year, Now.Month, Now.Day, .dtStartTime.Value.Hour, .dtStartTime.Value.Minute, 0).AddMinutes(45)

      End If

      AddHandler dlgAddNew.NewAdded, AddressOf NewIntervalAdded
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      cmdAddNew.Enabled = True

    End With
  End Sub
  Private Sub NewIntervalAdded(ByVal InsertedID As String)
    Me.GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(lvHarmonogram, InsertedID)
  End Sub

  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    Dim DBA As New DataBaseAction, H As New HarmonogramSQL, DeletedIndex As Integer
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      For Each Item As ListViewItem In Me.lvHarmonogram.SelectedItems
        DeletedIndex = Item.Index
        Dim cmd As MySqlCommand = DBA.CreateCommand(H.DeleteActivityTime)
        cmd.Parameters.AddWithValue("?ID", Item.Text)
        cmd.Transaction = MySQLTrans
        cmd.ExecuteNonQuery()
      Next
      MySQLTrans.Commit()
      GetData()
      Dim SH As New SeekHelper
      Me.EnableButtons(False)
      SH.FindPostRemovedListViewItemIndex(Me.lvHarmonogram, DeletedIndex)
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally

    End Try
  End Sub
End Class