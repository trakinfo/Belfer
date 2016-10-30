Public Class frmMiejscowosc

  Private DT As DataTable, Filter As String = ""
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.MiejscowoscToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.MiejscowoscToolStripMenuItem.Enabled = True
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
      .Columns.Add("Nazwa", 150, HorizontalAlignment.Left)
      .Columns.Add("Nazwa w miejscowniku", 150, HorizontalAlignment.Left)
      .Columns.Add("Poczta", 150, HorizontalAlignment.Left)
      .Columns.Add("KodPocztowy", 50, HorizontalAlignment.Center)
      .Columns.Add("Województwo", 150, HorizontalAlignment.Left)
      .Columns.Add("Kraj", 150, HorizontalAlignment.Left)
      .Columns.Add("Status", 50, HorizontalAlignment.Left)
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    Me.ListViewConfig(Me.lvMiejscowosc)
    Dim SeekCriteria() As String = New String() {"Nazwa", "Województwo", "Kraj"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    ApplyNewConfig()

    'FetchData()
    'Me.GetData()
  End Sub
  Private Sub ApplyNewConfig()
    'cmdAddNew.Enabled = False
    cmdEdit.Enabled = False
    cmdDelete.Enabled = False
    'lvMiejscowosc.Enabled = False
    GetData()
    'FetchData()
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, M As New MiejscowoscSQL
    DT = DBA.GetDataTable(M.SelectMiejscowosc)

  End Sub

  Private Sub GetData()
    Try
      FetchData()
      lvMiejscowosc.Items.Clear()
      Dim FilteredData() As DataRow
      FilteredData = DT.Select(Filter)

      For i As Integer = 0 To FilteredData.GetUpperBound(0) 'For Each row As DataRow In DT.Rows
        Me.lvMiejscowosc.Items.Add(FilteredData(i).Item(0).ToString)
        Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
        Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString)
        If CType(IsDBNull(FilteredData(i).Item(3)), Boolean) = True OrElse FilteredData(i).Item(3).ToString = "" Then
          Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add("")
        Else
          Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(FilteredData(i).Item(3).ToString)

        End If
        If FilteredData(i).Item(4).ToString = "" Then
          Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add("")
        Else
          Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(CType(FilteredData(i).Item(4), Integer).ToString("##-###"))

        End If

        'Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(FilteredData(i).Item(4).ToString)
        Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(FilteredData(i).Item(5).ToString)
        Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(FilteredData(i).Item(6).ToString)
        Me.lvMiejscowosc.Items(Me.lvMiejscowosc.Items.Count - 1).SubItems.Add(IIf(CType(FilteredData(i).Item(7), Boolean) = True, "Miasto", "Wieś").ToString)
      Next

      Me.lvMiejscowosc.Columns(1).Width = CInt(IIf(lvMiejscowosc.Items.Count > 23, 166, 187))

      Me.lvMiejscowosc.Enabled = CType(IIf(Me.lvMiejscowosc.Items.Count > 0, True, False), Boolean)
      lblRecord.Text = "0 z " & lvMiejscowosc.Items.Count
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvMiejscowosc.SelectedItems(0).Index + 1 & " z " & lvMiejscowosc.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        If GlobalValues.Users.ContainsKey(.Item("User").ToString.ToLower) Then
          lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(8).ToString
        End If
        lblIP.Text = .Item(9).ToString
        lblData.Text = .Item(10).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvMiejscowosc_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMiejscowosc.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvMiejscowosc.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
      'Else
      '  EnableButtons(False)
      'End If
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvMiejscowosc.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, M As New MiejscowoscSQL
      Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvMiejscowosc.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(M.DeleteMiejscowosc)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?ID", CType(Item.Text, Integer))
          cmd.ExecuteNonQuery()
          'DBA.ApplyChanges(M.DeleteMiejscowosc())
        Next
        MySQLTrans.Commit()
        Me.EnableButtons(False)
        'lvMiejscowosc.Items.Clear()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvMiejscowosc, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If
  End Sub
  Public Sub AddNewData()
    Dim dlgAddNew As New dlgMiejscowosc
    With dlgAddNew
      .IsNewMode = True
      .Text = "Dodawanie nowej miejscowości"
      Dim FCB As New FillComboBox, SH As New SeekHelper, M As New MiejscowoscSQL
      FCB.AddComboBoxComplexItems(.cbWoj, M.SelectWoj, True)
      .cbWoj.AutoCompleteSource = AutoCompleteSource.ListItems
      .cbWoj.AutoCompleteMode = AutoCompleteMode.Append
      'SH.FindComboItem(dlgAddNew.cbWoj, My.Settings.LastMiejsceUr)
      FCB.AddComboBoxComplexItems(.cbKraj, M.SelectKraj)
      .cbWoj.AutoCompleteSource = AutoCompleteSource.ListItems
      .cbWoj.AutoCompleteMode = AutoCompleteMode.Append

      '.MdiParent = Me.MdiParent
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .txtNazwa.Focus()
      AddHandler .NewAdded, AddressOf NewAdded
      'AddHandler .FormClosed, AddressOf EnableCmdAddNew
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      cmdAddNew.Enabled = True
    End With
  End Sub
  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    'lvMiejscowosc.Items.Clear()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvMiejscowosc, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddNewData()
  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgMiejscowosc


      With dlgEdit
        .IsNewMode = False
        .Text = "Edycja danych miejscowości"
        Dim FCB As New FillComboBox, SH As New SeekHelper
        FCB.AddComboBoxComplexItems(.cbWoj, "SELECT KodWoj,Nazwa FROM wojewodztwa;", True)
        .cbWoj.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbWoj.AutoCompleteMode = AutoCompleteMode.Append
        SH.FindComboItem(.cbWoj, lvMiejscowosc.SelectedItems(0).SubItems(5).Text)
        FCB.AddComboBoxComplexItems(.cbKraj, "SELECT ID,Nazwa FROM kraj;")
        .cbWoj.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbWoj.AutoCompleteMode = AutoCompleteMode.Append
        SH.FindComboItem(.cbKraj, lvMiejscowosc.SelectedItems(0).SubItems(6).Text)
        .txtNazwa.Text = lvMiejscowosc.SelectedItems(0).SubItems(1).Text
        .txtNazwaMiejscownik.Text = lvMiejscowosc.SelectedItems(0).SubItems(2).Text
        .txtPoczta.Text = lvMiejscowosc.SelectedItems(0).SubItems(3).Text
        .mskKodPocztowy.Text = lvMiejscowosc.SelectedItems(0).SubItems(4).Text
        '.txtKodPocztowy.Text = lvMiejscowosc.SelectedItems(0).SubItems(3).Text
        .chkMiasto.Checked = CType(IIf(lvMiejscowosc.SelectedItems(0).SubItems(7).Text = "Miasto", True, False), Boolean)

        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, M As New MiejscowoscSQL, Miejscowosc As String
          Dim MySQLTrans As MySqlTransaction

          Miejscowosc = Me.lvMiejscowosc.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(M.UpdateMiejscowsc())
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?Nazwa", .txtNazwa.Text.Trim)
            cmd.Parameters.AddWithValue("?NazwaMiejscownik", .txtNazwaMiejscownik.Text.Trim)
            cmd.Parameters.AddWithValue("?Poczta", .txtPoczta.Text.Trim)
            cmd.Parameters.AddWithValue("?KodPocztowy", .mskKodPocztowy.Text)

            'cmd.Parameters.AddWithValue("?KodPocztowy", .mskKodPocztowy.Text.Trim("-".ToCharArray))
            cmd.Parameters.AddWithValue("?KodWoj", CType(.cbWoj.SelectedItem, CbItem).Kod.ToString)
            cmd.Parameters.AddWithValue("?IdKraj", CType(.cbKraj.SelectedItem, CbItem).ID.ToString)
            cmd.Parameters.AddWithValue("?Miasto", CType(.chkMiasto.CheckState, Integer).ToString)
            cmd.Parameters.AddWithValue("?ID", Miejscowosc)

            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MySQLTrans.Rollback()
            MessageBox.Show(ex.Message)
          End Try
          'lvMiejscowosc.Items.Clear()
          'FetchData()
          GetData()
          Me.EnableButtons(False)
          'Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvMiejscowosc, Miejscowosc)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Select Case Me.cbSeek.Text
      Case "Województwo"
        Filter = "Woj LIKE '" & IIf(Me.txtSeek.Text.Trim.Length > 0, Me.txtSeek.Text & "%'", Me.txtSeek.Text & "%' OR Woj IS NULL").ToString 'Me.txtSeek.Text & "%'"
      Case "Nazwa"
        Filter = "Nazwa LIKE '" & Me.txtSeek.Text & "%'"
      Case "Kraj"
        Filter = "Kraj LIKE '" & IIf(Me.txtSeek.Text.Trim.Length > 0, Me.txtSeek.Text & "%'", Me.txtSeek.Text & "%' OR Kraj IS NULL").ToString
    End Select
    Me.EnableButtons(False)
    GetData()
    'Me.RefreshData()
  End Sub
  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub
End Class


