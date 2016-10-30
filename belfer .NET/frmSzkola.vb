Public Class frmSzkola
  Private DT As DataTable
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.SzkolyToolStripMenuItem.Enabled = True
    'RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.SzkolyToolStripMenuItem.Enabled = True
    'RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
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
      AddColumns()
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns()
    With lvSzkoly
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Alias", 200, HorizontalAlignment.Left)
      .Columns.Add("Adres", 240, HorizontalAlignment.Left)
      .Columns.Add("NIP", 100, HorizontalAlignment.Center)
      .Columns.Add("Domyślna", 100, HorizontalAlignment.Center)
    End With
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel2.Left - 10, 68, Panel2.Width)
  End Sub
  Private Sub frmSzkola_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ListViewConfig(lvSzkoly)
    ClearDetails()
    Dim FCB As New FillComboBox, TS As New TypySzkolSQL, SH As New SeekHelper
    FCB.AddComboBoxComplexItems(cbTypSzkoly, TS.SelectSchoolTypes)
    If My.Settings.IdSchoolType.Length > 0 Then SH.FindComboItem(Me.cbTypSzkoly, CType(My.Settings.IdSchoolType, Integer))
    cbTypSzkoly.Enabled = CType(IIf(cbTypSzkoly.Items.Count > 0, True, False), Boolean)

    'GetData()
  End Sub
  Private Sub GetData(IdSchoolType As String)
    Dim DBA As New DataBaseAction, S As New SzkolaSQL
    'Dim Dr As DataRow
    DT = DBA.GetDataTable(S.SelectSchools(IdSchoolType))
    Try
      lvSzkoly.Items.Clear()
      For Each Dr As DataRow In DT.Select
        lvSzkoly.Items.Add(Dr.Item(0).ToString)
        For i As Byte = 1 To 3
          lvSzkoly.Items(lvSzkoly.Items.Count - 1).SubItems.Add(Dr.Item(i).ToString)
        Next
        lvSzkoly.Items(lvSzkoly.Items.Count - 1).SubItems.Add(IIf(CType(Dr.Item(4), Boolean) = True, "x", "").ToString)
      Next

      lvSzkoly.Enabled = CType(IIf(lvSzkoly.Items.Count > 0, True, False), Boolean)
      lvSzkoly.Columns(1).Width = CInt(IIf(lvSzkoly.Items.Count > 12, 184, 200))
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub GetDetails()
    Dim FoundRow() As DataRow
    Try
      lblRecord.Text = lvSzkoly.SelectedItems(0).Index + 1 & " z " & lvSzkoly.Items.Count
      FoundRow = DT.Select("ID=" & lvSzkoly.SelectedItems(0).Text)
      With FoundRow(0)
        Me.lblNazwa.Text = .Item(5).ToString
        'lblTypSzkoly.Text = .Item(6).ToString
        lblTel.Text = .Item(6).ToString
        lblFax.Text = .Item(7).ToString
        lblEmail.Text = .Item(8).ToString
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(9).ToString
        lblIP.Text = .Item(10).ToString
        lblData.Text = .Item(11).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvSzkoly.Items.Count
    lblNazwa.Text = ""
    'lblTypSzkoly.Text = ""
    lblTel.Text = ""
    lblFax.Text = ""
    lblEmail.Text = ""

    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub lvSzkoly_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Me.EditData()
  End Sub
  Private Sub lvSzkoly_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvSzkoly.ItemSelectionChanged
    'If e.IsSelected Then Me.GetDetails()
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails()
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
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub EditData()
    Dim dlgEdit As New dlgSzkola, FCB As New FillComboBox, TS As New TypySzkolSQL, M As New MiejscowoscSQL
    With dlgEdit
      .IsNewMode = False
      .InRefresh = True
      FCB.AddComboBoxComplexItems(.cbMiejscowosc, M.SelectMiejsce)
      .cbMiejscowosc.AutoCompleteSource = AutoCompleteSource.ListItems
      .cbMiejscowosc.AutoCompleteMode = AutoCompleteMode.Append

      FCB.AddComboBoxComplexItems(.cbTypSzkoly, TS.SelectSchoolTypes)
      .cbTypSzkoly.AutoCompleteSource = AutoCompleteSource.ListItems
      .cbTypSzkoly.AutoCompleteMode = AutoCompleteMode.Append

      .Text = "Edycja danych szkoły"
      .txtAlias.Text = Me.lvSzkoly.SelectedItems(0).SubItems(1).Text
      .chkIsDefault.Checked = CType(IIf(Me.lvSzkoly.SelectedItems(0).SubItems(4).Text = "x", True, False), Boolean)
      .txtNazwa.Text = Me.lblNazwa.Text
      .txtNip.Text = Me.lvSzkoly.SelectedItems(0).SubItems(3).Text
      .txtTel.Text = lblTel.Text
      .txtFax.Text = Me.lblFax.Text
      .txtEmail.Text = lblEmail.Text
      Dim SH As New SeekHelper
      SH.FindComboItem(.cbTypSzkoly, CType(cbTypSzkoly.SelectedItem, CbItem).ID) 'CType(DT.Select("ID=" & Me.lvSzkoly.SelectedItems(0).Text)(0).Item(13), Integer))

      If DT.Select("ID=" & Me.lvSzkoly.SelectedItems(0).Text)(0).Item(14).ToString.Length > 0 Then SH.FindComboItem(.cbMiejscowosc, CType(DT.Select("ID=" & Me.lvSzkoly.SelectedItems(0).Text)(0).Item(12), Integer))

      'If lblTypSzkoly.Text.Length > 0 Then SH.FindComboItem(.cbTypSzkoly, CType(DT.Select("ID=" & Me.lvSzkoly.SelectedItems(0).Text)(0).Item(18), Integer))

      .txtUlica.Text = DT.Select("ID=" & Me.lvSzkoly.SelectedItems(0).Text)(0).Item(13).ToString
      .txtNr.Text = DT.Select("ID=" & Me.lvSzkoly.SelectedItems(0).Text)(0).Item(14).ToString
      '.Icon = gblAppIcon
      .InRefresh = False
      If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
        Dim DBA As New DataBaseAction, S As New SzkolaSQL, IDSzkola As String
        Dim MySQLTrans As MySqlTransaction

        IDSzkola = Me.lvSzkoly.SelectedItems(0).Text
        Dim cmd As MySqlCommand = DBA.CreateCommand(S.UpdateSchool(IDSzkola))
        MySQLTrans = GlobalValues.gblConn.BeginTransaction()
        cmd.Transaction = MySQLTrans
        Try
          cmd.Parameters.AddWithValue("?Nazwa", .txtNazwa.Text.Trim)
          cmd.Parameters.AddWithValue("?Alias", .txtAlias.Text.Trim)
          cmd.Parameters.AddWithValue("?Nip", IIf(.txtNip.Text.Trim.Length > 0, .txtNip.Text.Trim, DBNull.Value))
          cmd.Parameters.AddWithValue("?Ulica", .txtUlica.Text.Trim)
          cmd.Parameters.AddWithValue("?Nr", .txtNr.Text.Trim)
          If .cbMiejscowosc.SelectedItem IsNot Nothing Then
            cmd.Parameters.AddWithValue("?IdMiejscowosc", CType(.cbMiejscowosc.SelectedItem, CbItem).ID.ToString)
          Else
            cmd.Parameters.AddWithValue("?IdMiejscowosc", DBNull.Value)
          End If
          If .cbTypSzkoly.SelectedItem IsNot Nothing Then
            cmd.Parameters.AddWithValue("?IdTypSzkoly", CType(.cbTypSzkoly.SelectedItem, CbItem).ID.ToString)
          Else
            cmd.Parameters.AddWithValue("?IdTypSzkoly", DBNull.Value)
          End If          'cmd.Parameters.AddWithValue("?IdMiejscowosc", CType(.cbMiejscowosc.SelectedItem, CbItem).ID.ToString)
          cmd.Parameters.AddWithValue("?Tel", .txtTel.Text.Trim)
          cmd.Parameters.AddWithValue("?Fax", .txtFax.Text.Trim)
          cmd.Parameters.AddWithValue("?Email", .txtEmail.Text.Trim)

          cmd.ExecuteNonQuery()
          If CType(IIf(lvSzkoly.SelectedItems(0).SubItems(4).Text = "x", True, False), Boolean) <> .chkIsDefault.Checked Then
            cmd.CommandText = S.ResetDefault
            cmd.Parameters.AddWithValue("?TypSzkoly", CType(.cbTypSzkoly.SelectedItem, CbItem).ID.ToString)

            cmd.ExecuteNonQuery()
            cmd.CommandText = S.SetDefault()
            cmd.Parameters.AddWithValue("?ID", IDSzkola)
            cmd.ExecuteNonQuery()
          End If
          MySQLTrans.Commit()

        Catch ex As MySqlException
          MessageBox.Show(ex.Message)
          MySQLTrans.Rollback()
        End Try
        RefreshData()
        'lvSzkoly.Items.Clear()
        'GetData()
        Me.EnableButtons(False)
        'Dim SH As New SeekHelper
        SH.FindListViewItem(Me.lvSzkoly, IDSzkola)
      End If
    End With
  End Sub

  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgSzkola, FCB As New FillComboBox, TS As New TypySzkolSQL, M As New MiejscowoscSQL
    dlgAddNew.IsNewMode = True
    FCB.AddComboBoxComplexItems(dlgAddNew.cbMiejscowosc, M.SelectMiejsce)
    dlgAddNew.cbMiejscowosc.AutoCompleteSource = AutoCompleteSource.ListItems
    dlgAddNew.cbMiejscowosc.AutoCompleteMode = AutoCompleteMode.Append

    FCB.AddComboBoxComplexItems(dlgAddNew.cbTypSzkoly, TS.SelectSchoolTypes)
    dlgAddNew.cbTypSzkoly.Enabled = CType(IIf(dlgAddNew.cbTypSzkoly.Items.Count > 0, True, False), Boolean)
    dlgAddNew.cbTypSzkoly.AutoCompleteSource = AutoCompleteSource.ListItems
    dlgAddNew.cbTypSzkoly.AutoCompleteMode = AutoCompleteMode.Append
    'If lvSzkoly.SelectedItems.Count > 0 Then
    Dim SH As New SeekHelper
    SH.FindComboItem(dlgAddNew.cbTypSzkoly, CType(cbTypSzkoly.SelectedItem, CbItem).ID) 'CType(DT.Select("ID=" & Me.lvSzkoly.SelectedItems(0).Text)(0).Item(13), Integer))

    'End If
    dlgAddNew.Text = "Dodawanie nowej szkoły"
    AddHandler dlgAddNew.NewAdded, AddressOf NewSchoolAdded
    AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
    'AddHandler SharedSchool.OnOwnerClose, AddressOf dlgAddNew.Cancel_Button_Click
    'dlgAddNew.MdiParent = Me.MdiParent
    dlgAddNew.cbTypSzkoly.Focus()
    Me.cmdAddNew.Enabled = False
    dlgAddNew.ShowDialog()
  End Sub
  Private Sub NewSchoolAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    Me.GetData(CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString)
    If Not lvSzkoly.FindItemWithText(InsertedID) Is Nothing Then
      lvSzkoly.Focus()
      lvSzkoly.Items(lvSzkoly.FindItemWithText(InsertedID).Index).Selected = True
    End If
  End Sub
  Private Sub EnableCmdAddNew(ByVal sender As Object, ByVal e As FormClosedEventArgs)
    Me.cmdAddNew.Enabled = True
  End Sub
  Private Sub RefreshData()
    lvSzkoly.Items.Clear()
    ClearDetails()
    GetData(CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString)
  End Sub
  Private Sub lvWoj_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSzkoly.DoubleClick
    EditData()
  End Sub
  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    Me.EditData()
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, S As New SzkolaSQL
      Try
        For Each Item As ListViewItem In Me.lvSzkoly.SelectedItems
          DeletedIndex = Item.Index
          DBA.ApplyChanges(S.DeleteSchool(Item.Text))
        Next
        Me.EnableButtons(False)
        RefreshData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvSzkoly, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)

      End Try
    End If

  End Sub

  Private Sub cbTypSzkoly_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTypSzkoly.SelectedIndexChanged
    My.Settings.IdSchoolType = CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
    GetData(CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString)
    ClearDetails()
    cmdAddNew.Enabled = True
  End Sub

End Class

