Public Class frmMinLekcja

  Private DT As DataTable
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.MinLekcjaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.MinLekcjaToolStripMenuItem.Enabled = True
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
      .Columns.Add("Klasa", 239, HorizontalAlignment.Left)
      .Columns.Add("Wartość", 170, HorizontalAlignment.Center)
      .Columns.Add("Kod", 0, HorizontalAlignment.Center)
      .Columns.Add("IdKlasa", 0, HorizontalAlignment.Center)
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
    Me.ListViewConfig(Me.lvLiczbaGodzin)
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
    FillPrzedmiot()
    'GetData()
  End Sub
  Private Sub FillPrzedmiot()
    cbPrzedmiot.Items.Clear()
    Dim FCB As New FillComboBox, ML As New MinLekcjaSQL
    FCB.AddComboBoxComplexItems(cbPrzedmiot, ML.SelectPrzedmiot(My.Settings.IdSchool, My.Settings.SchoolYear))
    Dim SH As New SeekHelper
    If My.Settings.ObjectName.Length > 0 Then SH.FindComboItem(Me.cbPrzedmiot, CType(My.Settings.ObjectName, Integer))
    cbPrzedmiot.Enabled = CType(IIf(cbPrzedmiot.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    GetData()
    cmdAddNew.Enabled = True
  End Sub
  Private Sub GetData()
    Dim DBA As New DataBaseAction, ML As New MinLekcjaSQL
    lvLiczbaGodzin.Items.Clear()
    DT = DBA.GetDataTable(ML.SelectMinLekcja(My.Settings.IdSchool, My.Settings.SchoolYear))
    For Each row As DataRow In DT.Select("Przedmiot=" & CType(cbPrzedmiot.SelectedItem, CbItem).ID)
      Me.lvLiczbaGodzin.Items.Add(row.Item(0).ToString)
      Me.lvLiczbaGodzin.Items(Me.lvLiczbaGodzin.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      Me.lvLiczbaGodzin.Items(Me.lvLiczbaGodzin.Items.Count - 1).SubItems.Add(row.Item(2).ToString)
      Me.lvLiczbaGodzin.Items(Me.lvLiczbaGodzin.Items.Count - 1).SubItems.Add(row.Item(3).ToString)
      'Me.lvLiczbaGodzin.Items(Me.lvLiczbaGodzin.Items.Count - 1).SubItems.Add(row.Item(4).ToString)
    Next
    lvLiczbaGodzin.Columns(1).Width = CInt(IIf(lvLiczbaGodzin.Items.Count > 16, 220, 239))
    Me.lvLiczbaGodzin.Enabled = CType(IIf(Me.lvLiczbaGodzin.Items.Count > 0, True, False), Boolean)
    lblRecord.Text = "0 z " & lvLiczbaGodzin.Items.Count
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvLiczbaGodzin.SelectedItems(0).Index + 1 & " z " & lvLiczbaGodzin.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item("User").ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvWoj_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvLiczbaGodzin.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvLiczbaGodzin.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then
        EnableButtons(True)
      Else
        EnableButtons(False)
      End If
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
    'lblRecord.Text = "0 z " & lvBelfer.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, ML As New MinLekcjaSQL
      'Try
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvLiczbaGodzin.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(ML.DeleteMinLekcja)
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        Me.EnableButtons(False)
        lvLiczbaGodzin.Items.Clear()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvLiczbaGodzin, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub
  Public Sub AddNew()
    Dim dlgAddNew As New dlgMinLekcja
    With dlgAddNew
      .IsNewMode = True
      .Text = "Dodawanie nowej wartości"
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .Przedmiot = CType(cbPrzedmiot.SelectedItem, CbItem).ID
      AddHandler dlgAddNew.NewAdded, AddressOf NewAdded
      'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      cmdAddNew.Enabled = True
    End With
  End Sub
  Private Sub NewAdded(ByVal InsertedID As String)
    lvLiczbaGodzin.Items.Clear()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvLiczbaGodzin, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddNew()
  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgMinLekcja

      With dlgEdit
        .IsNewMode = False
        .InRefresh = True
        .Text = "Edycja wartości godzin"
        .OK_Button.Text = "Zapisz"
        .cbKlasa.Items.Add(lvLiczbaGodzin.SelectedItems(0).SubItems(1).Text)
        .cbKlasa.SelectedIndex = 0
        .cbKlasa.Enabled = False
        .Liczba = CType(lvLiczbaGodzin.SelectedItems(0).SubItems(2).Text, Decimal)
        .nudWartosc.Value = .Liczba
        .InRefresh = False
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdRecord As String = Me.lvLiczbaGodzin.SelectedItems(0).Text
          If .chkCalyPion.Checked Then
            UpdatePion(lvLiczbaGodzin.SelectedItems(0).SubItems(3).Text, .nudWartosc.Value)
          Else
            UpdateSelectedClass(lvLiczbaGodzin.SelectedItems(0).Text, .nudWartosc.Value)
          End If

          lvLiczbaGodzin.Items.Clear()
          GetData()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvLiczbaGodzin, IdRecord)
        End If
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub UpdatePion(Pion As String, Wartosc As Decimal)
    Dim DBA As New DataBaseAction, ML As New MinLekcjaSQL ', IdRecord As String
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      For Each row As DataRow In DT.Select("Przedmiot=" & CType(cbPrzedmiot.SelectedItem, CbItem).ID & " AND Pion=" & Pion)
        Dim cmd As MySqlCommand = DBA.CreateCommand(ML.UpdateMinLekcja)
        cmd.Transaction = MySQLTrans
        cmd.Parameters.AddWithValue("?ID", row("ID"))
        cmd.Parameters.AddWithValue("?Wartosc", Wartosc)
        cmd.ExecuteNonQuery()
      Next
      MySQLTrans.Commit()

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
      MySQLTrans.Rollback()
    End Try
  End Sub
  Private Sub UpdateSelectedClass(ID As String, Wartosc As Decimal)
    Dim DBA As New DataBaseAction, ML As New MinLekcjaSQL ', IdRecord As String
    Dim MySQLTrans As MySqlTransaction
    Dim cmd As MySqlCommand = DBA.CreateCommand(ML.UpdateMinLekcja)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?ID", ID)
      cmd.Parameters.AddWithValue("?Wartosc", Wartosc)
      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
      MySQLTrans.Rollback()
    End Try
  End Sub
End Class

