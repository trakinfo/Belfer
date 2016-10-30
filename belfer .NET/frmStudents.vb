Public Class frmStudents

  Private DS As DataSet, Filter As String = "", TutorLogin As String = ""
  'Dim sortColumn As Integer = -1
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.UczniowieToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.UczniowieToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData()
    Dim S As New StudentSQL, DBA As New DataBaseAction
    DS = DBA.GetDataSet(S.SelectStudents(My.Settings.SchoolYear, My.Settings.IdSchool) & S.SelectDetails(My.Settings.SchoolYear, My.Settings.IdSchool) & S.SelectWychowawca(My.Settings.SchoolYear, My.Settings.IdSchool) & S.SelectRepeaters(My.Settings.SchoolYear, My.Settings.IdSchool))
  End Sub
  Private Sub GetMainData()
    Try
      Dim FilteredData() As DataRow
      FilteredData = DS.Tables(0).Select(Filter, "Kod_Klasy ASC,NrwDzienniku ASC, NazwiskoImie ASC")

      For i As Integer = 0 To FilteredData.GetUpperBound(0)
        If CType(FilteredData(i).Item("StatusAktywacji"), Boolean) Then
          Dim Repeater() As DataRow
          Repeater = DS.Tables(3).Select("IdUczen=" & FilteredData(i).Item(0).ToString)
          If Repeater.GetLength(0) > 0 Then
            lvUczen.Items.Add(FilteredData(i).Item(0).ToString).UseItemStyleForSubItems = False
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString, Color.Red, lvUczen.BackColor, lvUczen.Font)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString, Color.Red, lvUczen.BackColor, lvUczen.Font)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(3).ToString, Color.Red, lvUczen.BackColor, lvUczen.Font)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(4).ToString, Color.Red, lvUczen.BackColor, lvUczen.Font)
            'lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(5).ToString, Color.Red, lvUczen.BackColor, lvUczen.Font)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(CType(FilteredData(i).Item(5), Date).ToString("yyyy-MM-dd"), Color.Red, lvUczen.BackColor, lvUczen.Font)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(6).ToString, Color.Red, lvUczen.BackColor, lvUczen.Font)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(IIf(CType(FilteredData(i).Item("Man"), Boolean) = True, "M", "K").ToString, Color.Red, lvUczen.BackColor, lvUczen.Font)
          Else
            lvUczen.Items.Add(FilteredData(i).Item(0).ToString)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(3).ToString)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(4).ToString)
            'lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(5).ToString)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(CType(FilteredData(i).Item(5), Date).ToString("yyyy-MM-dd"))
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(6).ToString)
            lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(IIf(CType(FilteredData(i).Item("Man"), Boolean) = True, "M", "K").ToString)
          End If
        Else
          lvUczen.Items.Add(FilteredData(i).Item(0).ToString).UseItemStyleForSubItems = False
          lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString, Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
          lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(2).ToString, Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
          lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(3).ToString, Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
          lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(4).ToString, Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
          'lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(5).ToString, Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
          lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(CType(FilteredData(i).Item(5), Date).ToString("yyyy-MM-dd"), Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
          lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(FilteredData(i).Item(6).ToString, Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
          lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(IIf(CType(FilteredData(i).Item("Man"), Boolean) = True, "M", "K").ToString, Color.Gray, lvUczen.BackColor, New Font(lvUczen.Font, FontStyle.Strikeout))
        End If
        lvUczen.Items(lvUczen.Items.Count - 1).Tag = New StudentAllocation With {.ID = CType(FilteredData(i).Item("ID"), Integer), .AllocationID = CType(FilteredData(i).Item("IdPrzydzial"), Integer), .DataAktywacji = If(IsDBNull(FilteredData(i).Item("DataAktywacji")), Nothing, CType(FilteredData(i).Item("DataAktywacji"), Date)), .DataDeaktywacji = If(IsDBNull(FilteredData(i).Item("DataDeaktywacji")), Nothing, CType(FilteredData(i).Item("DataDeaktywacji"), Date)), .Status = CType(FilteredData(i).Item("StatusAktywacji"), Boolean)} 'CType(FilteredData(i).Item("StatusAktywacji"), Boolean)

      Next
      lblRecord.Text = "0 z " & FilteredData.GetLength(0) & " (" & DS.Tables(0).Select("StatusAktywacji=1").GetLength(0) & " aktywnych)"
      lvUczen.Columns(3).Width = CInt(IIf(lvUczen.Items.Count > 16, 235, 254))
      lvUczen.Enabled = CBool(IIf(lvUczen.Items.Count > 0, True, False))
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub GetDetails(ByVal ID As String)
    Dim FoundRow() As DataRow
    Try
      lblRecord.Text = lvUczen.SelectedItems(0).Index + 1 & " z " & lvUczen.Items.Count & " (" & DS.Tables(0).Select("StatusAktywacji=1").GetLength(0) & " aktywnych)"
      FoundRow = DS.Tables(1).Select("ID=" & ID)
      With FoundRow(0)
        lblMiejsceUr.Text = .Item(1).ToString
        lblWojUr.Text = .Item(2).ToString
        lblDataAktywacji.Text = If(IsDBNull(.Item("DataAktywacji")), "", CType(.Item("DataAktywacji"), DateTime).ToShortDateString)
        lblDataDeaktywacji.Text = If(IsDBNull(.Item(3)), "", CType(.Item(3), DateTime).ToShortDateString)
        lblMiejsceZam.Text = .Item(4).ToString
        lblUlica.Text = .Item(5).ToString
        lblNrDomu.Text = .Item(6).ToString
        lblNrMieszkania.Text = .Item(7).ToString
        lblPoczta.Text = .Item(8).ToString
        'If .Item(10).ToString <> "" Then lblKodPocztowy.Text = Format(CType(.Item(10), Integer), "00-000")
        lblKodPocztowy.Text = .Item(9).ToString
        lblWojZam.Text = .Item(10).ToString
        chkMiasto.Checked = CType(IIf(CType(IsDBNull(.Item(11)), Boolean) = False, .Item(11), False), Boolean)
        lblTel1.Text = .Item(12).ToString
        lblKraj.Text = .Item(13).ToString
        lblTelKom1.Text = .Item(14).ToString
        lblTelKom2.Text = .Item(15).ToString
        lblImieOjca.Text = .Item(16).ToString
        lblNazwiskoOjca.Text = .Item(17).ToString
        lblImieMatki.Text = .Item(18).ToString
        lblNazwiskoMatki.Text = .Item(19).ToString
        'chkNiepelnosprawnosc.Checked = CType(IIf(CType(IsDBNull(.Item(21)), Boolean) = False, .Item(21), False), Boolean)


        'lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")")
        Dim User, Owner As String
        User = CType(.Item("User"), String).ToLower
        Owner = CType(.Item("Owner"), String).ToLower
        If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
          lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (Wł: ", GlobalValues.Users.Item(Owner).ToString, ")")
        Else
          Me.lblUser.Text = User & " (Wł: " & Owner & ")"
        End If
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub ClearDetails()
    lblMiejsceUr.Text = ""
    lblWojUr.Text = ""
    lblDataAktywacji.Text = ""
    lblDataDeaktywacji.Text = ""
    'chkNiepelnosprawnosc.Checked = False
    lblMiejsceZam.Text = ""
    lblUlica.Text = ""
    lblNrDomu.Text = ""
    lblNrMieszkania.Text = ""
    lblPoczta.Text = ""
    lblKodPocztowy.Text = ""
    lblWojZam.Text = ""
    chkMiasto.Checked = False
    lblTel1.Text = ""
    lblKraj.Text = ""
    lblTelKom1.Text = ""
    lblTelKom2.Text = ""
    lblImieOjca.Text = ""
    lblNazwiskoOjca.Text = ""
    lblImieMatki.Text = ""
    lblNazwiskoMatki.Text = ""
    'chkNiepelnosprawnosc.Checked = False
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
    lblWychowawca.Text = ""
    lblRecord.Text = "0 z " & lvUczen.Items.Count
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel2.Left - 10, Panel2.Height - 30, Panel2.Width)
  End Sub
  Private Sub frmStudents_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedException.OnErrorGenerate, AddressOf EditData
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    ClearDetails()
    ListViewConfig()

    Dim SeekCriteria() As String = New String() {"Klasa", "Nazwisko i imię", "Nr ewidencyjny", "Data urodzenia", "Pesel", "Miejsce urodzenia", "Kraj urodzenia", "Miejsce zamieszkania"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    Cursor = Cursors.WaitCursor
    EnableButtons(False)
    lvUczen.Enabled = False
    FetchData()
    RefreshData()
    Cursor = Cursors.Default
  End Sub
  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
    Me.cmdStrikeOut.Enabled = Value
    Me.cmdNrDz.Enabled = Value
  End Sub
  Private Sub ListViewConfig()
    With lvUczen
      .View = View.Details
      .Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nr kol.", 50, HorizontalAlignment.Center)
      .Columns.Add("Nr ewid.", 60, HorizontalAlignment.Center)
      '.Columns.Add("Nr leg. szk.", 70, HorizontalAlignment.Center)
      .Columns.Add("Nazwisko i imiona", 254, HorizontalAlignment.Left)
      .Columns.Add("Klasa", 120, HorizontalAlignment.Center)
      .Columns.Add("Data ur.", 80, HorizontalAlignment.Center)
      .Columns.Add("Pesel", 90, HorizontalAlignment.Center)
      .Columns.Add("Płeć", 80, HorizontalAlignment.Center)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub

  Private Sub lvUczen_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvUczen.ItemSelectionChanged
    If e.IsSelected Then
      ClearDetails()
      'GetTutorLogin(DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa").ToString)
      GetWychowawca(DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa").ToString)
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = TutorLogin OrElse GlobalValues.AppUser.Login = DS.Tables(1).Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.ToLower.Trim Then
        EnableButtons(True)
        cmdStrikeOut.Enabled = CType(IIf(e.Item.SubItems(1).Font.Strikeout, False, True), Boolean)
      End If
    Else
      ClearDetails()
      lblWychowawca.Text = ""
      EnableButtons(False)
    End If
  End Sub
  Private Sub GetWychowawca(ByVal Klasa As String)
    Try
      If DS.Tables(2).Select("Klasa='" & Klasa & "'").Length > 0 Then
        lblWychowawca.Text = DS.Tables(2).Select("Klasa='" & Klasa & "'")(0).Item("Wychowawca").ToString
        TutorLogin = DS.Tables(2).Select("Klasa='" & Klasa & "'")(0).Item("Login").ToString.ToLower
      Else
        lblWychowawca.Text = "Nie udało się ustalić wychowawstwa"
        TutorLogin = ""
      End If
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub RefreshData()
    'lvUczen.Sorting = SortOrder.None
    lvUczen.Items.Clear()
    ClearDetails()
    GetMainData()
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Zaznaczeni uczniowie zostaną bezpowrotnie usunięci z bazy danych." & vbNewLine & "Czy na pewno o to Ci chodzi? Jeśli tak, to naciśnij przycisk 'OK'. W przeciwnym razie wciśnij przycisk 'Anuluj'.", My.Application.Info.ProductName, MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
      Dim S As New StudentSQL, DBA As New DataBaseAction, DeletedIndex As Integer
      For Each Item As ListViewItem In Me.lvUczen.SelectedItems
        DeletedIndex = Item.Index
        DBA.ApplyChanges(S.DeleteStudent(Item.Text))
      Next
      ApplyNewConfig()
      'Me.FetchData()
      'Me.RefreshData()
      Dim SH As New SeekHelper
      'Me.EnableButtons(False)
      SH.FindPostRemovedListViewItemIndex(Me.lvUczen, DeletedIndex)
    End If
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgStudent
    dlgAddNew.IsNewMode = True
    Dim FCB As New FillComboBox, SH As New SeekHelper, M As New MiejscowoscSQL, O As New ObsadaSQL 'P As New PrzydzialSQL
    FCB.AddComboBoxComplexItems(dlgAddNew.cbMiejsceUr, M.SelectMiejsce)
    dlgAddNew.cbMiejsceUr.AutoCompleteSource = AutoCompleteSource.ListItems
    dlgAddNew.cbMiejsceUr.AutoCompleteMode = AutoCompleteMode.Append
    SH.FindComboItem(dlgAddNew.cbMiejsceUr, My.Settings.LastMiejsceUr)
    FCB.AddComboBoxComplexItems(dlgAddNew.cbMiejsceZam, M.SelectMiejsce)
    dlgAddNew.cbMiejsceZam.AutoCompleteSource = AutoCompleteSource.ListItems
    dlgAddNew.cbMiejsceZam.AutoCompleteMode = AutoCompleteMode.Append
    SH.FindComboItem(dlgAddNew.cbMiejsceZam, My.Settings.LastMiejsceZam)
    FCB.AddComboBoxComplexItems(dlgAddNew.cbKlasa, O.SelectClasses(My.Settings.IdSchool, "0"))
    dlgAddNew.cbKlasa.AutoCompleteSource = AutoCompleteSource.ListItems
    dlgAddNew.cbKlasa.AutoCompleteMode = AutoCompleteMode.Append
    dlgAddNew.cbKlasa.Enabled = CType(IIf(dlgAddNew.cbKlasa.Items.Count > 0, True, False), Boolean)
    dlgAddNew.TutorLogin = TutorLogin
    dlgAddNew.dtWychowawca = DS.Tables(2).Copy
    If Me.lvUczen.SelectedItems.Count > 0 Then SH.FindComboItem(dlgAddNew.cbKlasa, CType(DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa"), Integer)) ' = Me.lvUczen.SelectedItems(0).SubItems(4).Text
    dlgAddNew.Text = "Dodawanie nowego ucznia"
    dlgAddNew.cbPlec.Text = "M"
    dlgAddNew.MaximizeBox = False
    dlgAddNew.StartPosition = FormStartPosition.CenterScreen
    'dlgAddNew.Opacity = 0.7
    RemoveHandler dlgAddNew.NewStudentAdded, AddressOf NewStudentAdded
    AddHandler dlgAddNew.NewStudentAdded, AddressOf NewStudentAdded
    Me.cmdAddNew.Enabled = False
    dlgAddNew.ShowDialog()
    Me.cmdAddNew.Enabled = True
  End Sub

  Private Sub NewStudentAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    Me.FetchData()
    RefreshData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvUczen, InsertedID)
  End Sub
  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub lvUczen_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvUczen.DoubleClick
    EditData()
  End Sub

  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgStudent
      Dim FCB As New FillComboBox, M As New MiejscowoscSQL, O As New ObsadaSQL 'P As New PrzydzialSQL
      With dlgEdit
        .IsNewMode = False
        FCB.AddComboBoxComplexItems(.cbMiejsceUr, M.SelectMiejsce)
        .cbMiejsceUr.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbMiejsceUr.AutoCompleteMode = AutoCompleteMode.Append
        FCB.AddComboBoxComplexItems(.cbMiejsceZam, M.SelectMiejsce)
        .cbMiejsceZam.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbMiejsceZam.AutoCompleteMode = AutoCompleteMode.Append
        FCB.AddComboBoxComplexItems(.cbKlasa, O.SelectClasses(My.Settings.IdSchool, "0"))
        .cbKlasa.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbKlasa.AutoCompleteMode = AutoCompleteMode.Append
        .cbKlasa.Enabled = False
        .dtWychowawca = DS.Tables(2).Copy

        .Text = "Edycja danych ucznia"
        '.MdiParent = Me.MdiParent
        .txtNrArkusza.Text = Me.lvUczen.SelectedItems(0).SubItems(2).Text
        Dim SH As New SeekHelper, IdKlasa As Integer
        IdKlasa = CType(DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa"), Integer) 'CType(.cbKlasa.SelectedItem, CbItem).ID
        '.txtNrLegSzkol.Text = DS.Tables(0).Select("ID=" & lvUczen.SelectedItems(0).Text)(0).Item("NrLegSzkol").ToString
        SH.FindComboItem(.cbKlasa, IdKlasa)
        .txtNazwisko.Text = DS.Tables(0).Select("ID=" & lvUczen.SelectedItems(0).Text)(0).Item("Nazwisko").ToString
        .txtImie.Text = DS.Tables(0).Select("ID=" & lvUczen.SelectedItems(0).Text)(0).Item("Imie").ToString
        .txtImie2.Text = DS.Tables(0).Select("ID=" & lvUczen.SelectedItems(0).Text)(0).Item("Imie2").ToString
        .dtDataUr.Value = CType(Me.lvUczen.SelectedItems(0).SubItems(5).Text, Date)
        If Me.lblMiejsceUr.Text.Length > 0 Then SH.FindComboItem(.cbMiejsceUr, CType(DS.Tables(1).Select("ID=" & lvUczen.SelectedItems(0).Text)(0).Item("IdMiejsceUr"), Integer))
        If Me.lblMiejsceZam.Text.Length > 0 Then SH.FindComboItem(.cbMiejsceZam, CType(DS.Tables(1).Select("ID=" & lvUczen.SelectedItems(0).Text)(0).Item("IdMiejsceZam"), Integer))
        .txtUlica.Text = lblUlica.Text
        .txtNrDomu.Text = lblNrDomu.Text
        .txtNrMieszkania.Text = lblNrMieszkania.Text
        .txtTel1.Text = lblTel1.Text
        '.txtTel2.Text = lblKraj.Text
        .txtTelKom1.Text = lblTelKom1.Text
        .txtTelKom2.Text = lblTelKom2.Text
        .txtImieOjca.Text = Me.lblImieOjca.Text
        .txtNazwiskoOjca.Text = lblNazwiskoOjca.Text
        .txtImieMatki.Text = Me.lblImieMatki.Text
        .txtNazwiskoMatki.Text = lblNazwiskoMatki.Text
        .cbPlec.Text = Me.lvUczen.SelectedItems(0).SubItems(7).Text
        .txtPesel.Text = Me.lvUczen.SelectedItems(0).SubItems(6).Text
        
        .dtDataAktywacji.Value = CType(lblDataAktywacji.Text, Date)
        If lblDataDeaktywacji.Text.Length > 0 Then
          .dtDataDeaktywacji.MinDate = .dtDataAktywacji.Value
          .dtDataDeaktywacji.Value = CType(lblDataDeaktywacji.Text, Date)
          .dtDataDeaktywacji.Visible = True
          .lblDataDeaktywacji.Visible = True
        End If

        .CancelButton = .cmdClose
        .AcceptButton = .cmdSave
        '.Icon = gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, S As New StudentSQL, IdUczen As String ', Status As Boolean
          Dim MySQLTrans As MySqlTransaction
          'Status = CType(Me.lvUczen.SelectedItems(0).Tag, Boolean)
          IdUczen = Me.lvUczen.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(S.UpdateStudent(IdUczen))
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?Nazwisko", .txtNazwisko.Text.Trim)
            cmd.Parameters.AddWithValue("?Imie", .txtImie.Text.Trim)
            cmd.Parameters.AddWithValue("?Imie2", .txtImie2.Text.Trim)
            cmd.Parameters.AddWithValue("?NrArkusza", IIf(.txtNrArkusza.Text.Length > 0, .txtNrArkusza.Text.Trim, DBNull.Value))
            'cmd.Parameters.AddWithValue("?NrLegSzkol", IIf(.txtNrLegSzkol.Text.Length > 0, .txtNrLegSzkol.Text.Trim, DBNull.Value))
            cmd.Parameters.AddWithValue("?DataUr", .dtDataUr.Value.ToString("yyyy-MM-dd"))
            If .cbMiejsceUr.Text = "" Then
              cmd.Parameters.AddWithValue("?IdMiejsceUr", DBNull.Value)
            Else
              cmd.Parameters.AddWithValue("?IdMiejsceUr", CType(.cbMiejsceUr.SelectedItem, CbItem).ID)
            End If
            If .cbMiejsceZam.Text = "" Then
              cmd.Parameters.AddWithValue("?IdMiejsceZam", DBNull.Value)
            Else
              cmd.Parameters.AddWithValue("?IdMiejsceZam", CType(.cbMiejsceZam.SelectedItem, CbItem).ID)
            End If

            cmd.Parameters.AddWithValue("?Ulica", .txtUlica.Text)
            cmd.Parameters.AddWithValue("?NrDomu", .txtNrDomu.Text)
            cmd.Parameters.AddWithValue("?NrMieszkania", .txtNrMieszkania.Text)
            cmd.Parameters.AddWithValue("?Tel", .txtTel1.Text)
            'cmd.Parameters.AddWithValue("?Tel2", .txtTel2.Text)
            cmd.Parameters.AddWithValue("?TelKom1", .txtTelKom1.Text)
            cmd.Parameters.AddWithValue("?TelKom2", .txtTelKom2.Text)
            cmd.Parameters.AddWithValue("?ImieOjca", .txtImieOjca.Text)
            cmd.Parameters.AddWithValue("?NazwiskoOjca", .txtNazwiskoOjca.Text)
            cmd.Parameters.AddWithValue("?ImieMatki", .txtImieMatki.Text)
            cmd.Parameters.AddWithValue("?NazwiskoMatki", .txtNazwiskoMatki.Text)
            cmd.Parameters.AddWithValue("?Man", IIf(.cbPlec.Text = "M", 1, 0))
            cmd.Parameters.AddWithValue("?Pesel", .txtPesel.Text)
            cmd.ExecuteNonQuery()

            If lblDataDeaktywacji.Text.Length > 0 AndAlso .dtDataDeaktywacji.Value.ToShortDateString <> CType(lblDataDeaktywacji.Text, Date).ToShortDateString Then
              Dim P As New PrzydzialSQL
              cmd.CommandText = P.UpdatePrzydzial()
              cmd.Parameters.AddWithValue("?DataDeaktywacji", .dtDataDeaktywacji.Value)
              cmd.Parameters.AddWithValue("?IdPrzydzial", CType(lvUczen.SelectedItems(0).Tag, StudentAllocation).AllocationID)
              cmd.ExecuteNonQuery()
            End If
            If .dtDataAktywacji.Value.ToShortDateString <> CType(lblDataAktywacji.Text, Date).ToShortDateString Then
              Dim P As New PrzydzialSQL
              cmd.CommandText = P.UpdateAktywacja
              cmd.Parameters.AddWithValue("?DataAktywacji", .dtDataAktywacji.Value)
              cmd.Parameters.AddWithValue("?IdPrzydzial", CType(lvUczen.SelectedItems(0).Tag, StudentAllocation).AllocationID)
              cmd.ExecuteNonQuery()
            End If
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MySQLTrans.Rollback()
          End Try
          Me.FetchData()
          RefreshData()
          Me.EnableButtons(False)
          SH.FindListViewItem(Me.lvUczen, IdUczen)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub SetNrDzByAlphabet()
    Dim DT As DataTable, S As New StudentSQL, DBA As New DataBaseAction
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      DT = DBA.GetDataTable(S.SelectStudentsID(My.Settings.SchoolYear, DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa").ToString))
      Dim i As Integer = 0
      Do Until i > DT.Rows.Count - 1
        i += 1
        DBA.ApplyChanges(S.UpdateNrDz(CStr(i), DT.Rows(i - 1).Item(0).ToString, DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa").ToString, My.Settings.SchoolYear), MySQLTrans)
      Loop
      MySQLTrans.Commit()
    Catch ex As Exception
      MySQLTrans.Rollback()
    End Try
  End Sub
  Private Sub AddToEndOfList()
    Dim S As New StudentSQL, DBA As New DataBaseAction
    DBA.ApplyChanges(S.UpdateNrDz((DBA.ComputeRecords(S.SelectMaxNrDz(DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa").ToString, My.Settings.SchoolYear)) + 1).ToString, Me.lvUczen.SelectedItems(0).Text, DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa").ToString, My.Settings.SchoolYear))
  End Sub
  Private Sub SetNrByHand(ByVal Nr As String)
    Dim S As New StudentSQL, DBA As New DataBaseAction
    DBA.ApplyChanges(S.UpdateNrDz(Nr, Me.lvUczen.SelectedItems(0).Text, DS.Tables(0).Select("ID=" & Me.lvUczen.SelectedItems(0).Text)(0).Item("Klasa").ToString, My.Settings.SchoolYear))
  End Sub

  Private Sub cmdNrDz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNrDz.Click
    Cursor = Cursors.WaitCursor
    Dim dlgNrDz As New dlgNrDz, IdUczen As String = Me.lvUczen.SelectedItems(0).Text
    dlgNrDz.Text = "Numer w dzienniku"
    If dlgNrDz.ShowDialog = Windows.Forms.DialogResult.OK Then
      If dlgNrDz.rbAddToEnd.Checked Then
        AddToEndOfList()
      ElseIf dlgNrDz.rbByAlfabet.Checked Then
        SetNrDzByAlphabet()
      Else
        SetNrByHand(dlgNrDz.nudNr.Value.ToString)
      End If
      Me.FetchData()
      Me.RefreshData()
      Dim SH As New SeekHelper
      SH.FindListViewItem(Me.lvUczen, IdUczen)
    End If
    Cursor = Cursors.Default
  End Sub
  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Cursor = Cursors.WaitCursor
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię"
        Filter = "NazwiskoImie LIKE '" & Me.txtSeek.Text + "%'"
      Case "Klasa"
        Filter = "Nazwa_Klasy LIKE '" & Me.txtSeek.Text + "%'"
      Case "Nr ewidencyjny"
        If txtSeek.Text.Length > 0 Then
          Filter = "NrArkusza LIKE '" & Me.txtSeek.Text + "%'"
        Else
          Filter = "NrArkusza LIKE '" & Me.txtSeek.Text + "%' OR NrArkusza is null"
        End If
      Case "Data urodzenia"
        Filter = "DataUr LIKE '" & Me.txtSeek.Text + "%'"
      Case "Pesel"
        If txtSeek.Text = "brak" Then
          Filter = "Pesel is null or Pesel=''"
        Else
          Filter = "Pesel LIKE '" & Me.txtSeek.Text & "%'"
        End If
      Case "Miejsce urodzenia"
        Filter = "MiejsceUr LIKE '" & IIf(Me.txtSeek.Text.Trim.Length > 0, Me.txtSeek.Text & "%'", Me.txtSeek.Text & "%' OR Kraj IS NULL").ToString 'Me.txtSeek.Text & "%'"
      Case "Miejsce zamieszkania"
        Filter = "MiejsceZam LIKE '" & IIf(Me.txtSeek.Text.Trim.Length > 0, Me.txtSeek.Text & "%'", Me.txtSeek.Text & "%' OR Kraj IS NULL").ToString 'Me.txtSeek.Text & "%'"
      Case "Kraj urodzenia"
        Filter = "Kraj LIKE '" & IIf(Me.txtSeek.Text.Trim.Length > 0, Me.txtSeek.Text & "%'", Me.txtSeek.Text & "%' OR Kraj IS NULL").ToString
    End Select
    Me.EnableButtons(False)
    Me.RefreshData()
    Cursor = Cursors.Default
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub

  Private Sub cmdStrikeOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStrikeOut.Click
    If MessageBox.Show("Zaznaczeni uczniowie zostaną skreśleni z listy uczniów." & vbNewLine & "Czy kontynuować? Jeśli tak, to naciśnij przycisk 'OK'. W przeciwnym razie wciśnij przycisk 'Anuluj'.", My.Application.Info.ProductName, MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
      Dim dlgStrikeOut As New dlgStrikeOut
      With dlgStrikeOut
        .Text = "Data skreślenia z listy uczniów"
        Dim CH As New CalcHelper
        .dtData.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
        .dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
        If .ShowDialog = Windows.Forms.DialogResult.OK Then
          Cursor = Cursors.WaitCursor
          Dim S As New StudentSQL, DBA As New DataBaseAction, StrikeOutStudent As String = ""
          For Each Item As ListViewItem In Me.lvUczen.SelectedItems
            StrikeOutStudent = Item.Text
            DBA.ApplyChanges(S.StrikeoutStudent(Item.Text, My.Settings.SchoolYear, .dtData.Value.ToString("yyyy-MM-dd HH:mm:ss")))
          Next
          Me.FetchData()
          Me.RefreshData()
          Dim SH As New SeekHelper
          Me.EnableButtons(False)
          SH.FindListViewItem(Me.lvUczen, StrikeOutStudent)
          Cursor = Cursors.Default
        End If
      End With
    End If
  End Sub

  Private Sub frmStudents_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, 12, 348, 860)
  End Sub
End Class

