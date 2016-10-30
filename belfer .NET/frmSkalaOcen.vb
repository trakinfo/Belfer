Public Class frmSkalaOcen
  Private DT As DataTable, Filter As String = ""
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.SkalaOcenToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.SkalaOcenToolStripMenuItem.Enabled = True
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
      .Columns.Add("Ocena", 125, HorizontalAlignment.Center)
      .Columns.Add("Nazwa oceny", 250, HorizontalAlignment.Left)
      .Columns.Add("Alias", 125, HorizontalAlignment.Center)
      .Columns.Add("Waga oceny", 94, HorizontalAlignment.Center)
      .Columns.Add("Typ oceny", 100, HorizontalAlignment.Center)
      .Columns.Add("Podtyp oceny", 100, HorizontalAlignment.Center)
      .Columns.Add("Priorytet", 100, HorizontalAlignment.Center)
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
    Me.ListViewConfig(Me.lvSkalaOcen)
    Dim SeekCriteria() As String = New String() {"Typ oceny", "Podtyp oceny"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    lblRecord.Text = ""
    ApplyNewConfig()
    'FetchData()
    'Me.GetData()
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvSkalaOcen.Items.Clear()
    ClearDetails()
    GetData()
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, O As New OcenySQL
    DT = DBA.GetDataTable(O.SelectString)
  End Sub

  Private Sub GetData()
    Try
      FetchData()
      lvSkalaOcen.Items.Clear()
      Dim FilteredData() As DataRow
      FilteredData = DT.Select(Filter)

      For i As Integer = 0 To FilteredData.GetUpperBound(0)
        Me.lvSkalaOcen.Items.Add(FilteredData(i).Item(0).ToString)
        Me.lvSkalaOcen.Items(Me.lvSkalaOcen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
        Me.lvSkalaOcen.Items(Me.lvSkalaOcen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString)
        Me.lvSkalaOcen.Items(Me.lvSkalaOcen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(3).ToString)
        Me.lvSkalaOcen.Items(Me.lvSkalaOcen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(4).ToString)
        Me.lvSkalaOcen.Items(Me.lvSkalaOcen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(5).ToString)
        Me.lvSkalaOcen.Items(Me.lvSkalaOcen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(6).ToString)
        Me.lvSkalaOcen.Items(Me.lvSkalaOcen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(7).ToString)
      Next
      Me.lvSkalaOcen.Columns(2).Width = CInt(IIf(lvSkalaOcen.Items.Count > 19, 231, 250))
      Me.lvSkalaOcen.Enabled = CType(IIf(Me.lvSkalaOcen.Items.Count > 0, True, False), Boolean)
      lblRecord.Text = "0 z " & lvSkalaOcen.Items.Count
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvSkalaOcen.SelectedItems(0).Index + 1 & " z " & lvSkalaOcen.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(8).ToString
        lblIP.Text = .Item(9).ToString
        lblData.Text = .Item(10).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvMiejscowosc_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSkalaOcen.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvSkalaOcen.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString Then EnableButtons(True)
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
    'lblRecord.Text = "0 z " & lvSkalaOcen.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, O As New OcenySQL
      Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvSkalaOcen.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(O.DeleteString)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?ID", CType(Item.Text, Integer))
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        Me.EnableButtons(False)
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvSkalaOcen, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)

      End Try
    End If

  End Sub
  Public Sub AddNewData()
    Dim dlgAddNew As New dlgSkalaOcen
    With dlgAddNew
      .IsNewMode = True
      .Text = "Definiowanie nowej oceny"
      Dim Typ As String() = New String() {"Ocena przedmiotowa", "Ocena zachowania", "Ocena przedmiotowa i zachowania"}
      .cbTyp.Items.AddRange(Typ)
      .cbTyp.SelectedIndex = 0
      .cbTyp.AutoCompleteSource = AutoCompleteSource.ListItems
      .cbTyp.AutoCompleteMode = AutoCompleteMode.Append
      Dim PodTyp As String() = New String() {"Ocena cząstkowa", "Ocena końcowa", "Ocena cząstkowa i końcowa"}
      .cbPodtyp.Items.AddRange(PodTyp)
      .cbPodtyp.SelectedIndex = 0
      .cbPodtyp.AutoCompleteSource = AutoCompleteSource.ListItems
      .cbPodtyp.AutoCompleteMode = AutoCompleteMode.Append

      '.MdiParent = Me.MdiParent
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      '.StartPosition = FormStartPosition.Manual
      '.Location = New Point(Me.Location.X, Me.Location.Y - dlgAddNew.Height)
      .txtNazwa.Focus()
      AddHandler .NewAdded, AddressOf NewAdded
      'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      Me.cmdAddNew.Enabled = True

    End With

  End Sub
  Private Sub NewAdded(ByVal InsertedID As String)
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvSkalaOcen, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddNewData()
  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgSkalaOcen


      With dlgEdit
        .Text = "Edycja danych oceny"
        .OK_Button.Text = "Zapisz"
        'Dim SH As New SeekHelper
        Dim Typ As String() = New String() {"Ocena przedmiotowa", "Ocena zachowania", "Ocena przedmiotowa i zachowania"}
        .cbTyp.Items.AddRange(Typ)
        .cbTyp.SelectedIndex = 0
        .cbTyp.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbTyp.AutoCompleteMode = AutoCompleteMode.Append
        .cbTyp.SelectedIndex = CType(System.Enum.Parse(GetType(GlobalValues.GradeType), Me.lvSkalaOcen.SelectedItems(0).SubItems(5).Text), GlobalValues.GradeType)
        'MessageBox.Show(CType(System.Enum.Parse(GetType(GlobalValues.GradeType), Me.lvOceny.SelectedItems(0).SubItems(5).Text), GlobalValues.GradeType).ToString)
        'SH.FindComboItem(.cbTyp, CType(lvOceny.SelectedItems(0).SubItems(5).Text, GlobalValues.GradeType))
        Dim PodTyp As String() = New String() {"Ocena cząstkowa", "Ocena końcowa", "Ocena cząstkowa i końcowa"}
        .cbPodtyp.Items.AddRange(PodTyp)
        .cbPodtyp.SelectedIndex = 0
        .cbPodtyp.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbPodtyp.AutoCompleteMode = AutoCompleteMode.Append
        .cbPodtyp.SelectedIndex = CType(System.Enum.Parse(GetType(GlobalValues.GradeSubType), Me.lvSkalaOcen.SelectedItems(0).SubItems(6).Text), GlobalValues.GradeSubType)

        'SH.FindComboItem(.cbPodtyp, lvOceny.SelectedItems(0).SubItems(6).Text)
        .txtNazwa.Text = lvSkalaOcen.SelectedItems(0).SubItems(1).Text
        .txtNazwaPelna.Text = lvSkalaOcen.SelectedItems(0).SubItems(2).Text
        .txtNazwaSkrot.Text = lvSkalaOcen.SelectedItems(0).SubItems(3).Text
        .nudWaga.Value = CType(lvSkalaOcen.SelectedItems(0).SubItems(4).Text, Decimal)

        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, O As New OcenySQL, IdOcena As String
          Dim MySQLTrans As MySqlTransaction

          IdOcena = Me.lvSkalaOcen.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(O.UpdateString)
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?Ocena", .txtNazwa.Text.Trim)
            cmd.Parameters.AddWithValue("?Nazwa", .txtNazwaPelna.Text.Trim)
            cmd.Parameters.AddWithValue("?Alias", .txtNazwaSkrot.Text.Trim)
            cmd.Parameters.AddWithValue("?Waga", .nudWaga.Value)

            cmd.Parameters.AddWithValue("?Typ", CType(.cbTyp.SelectedIndex, GlobalValues.GradeType).ToString)
            cmd.Parameters.AddWithValue("?PodTyp", CType(.cbPodtyp.SelectedIndex, GlobalValues.GradeSubType).ToString)
            cmd.Parameters.AddWithValue("?ID", IdOcena)

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
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvSkalaOcen, IdOcena)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Select Case Me.cbSeek.Text
      Case "Typ oceny"
        Filter = "Typ LIKE '" & IIf(Me.txtSeek.Text.Trim.Length > 0, txtSeek.Text & "'", Me.txtSeek.Text & "%'").ToString
      Case "Podtyp oceny"
        Filter = "PodTyp LIKE '" & IIf(Me.txtSeek.Text.Trim.Length > 0, txtSeek.Text & "'", Me.txtSeek.Text & "%'").ToString
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