Public Class frmTemat
  Private dtTemat, dtFrekwencja, dtStudent, dtGrupa, dtIndividualStaff As DataTable

  Private Sub frmTemat_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.TematToolStripMenuItem.Enabled = True
    MainForm.cmdTemat.Enabled = True
    MainForm.cmdTematByNauczyciel.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmTemat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.TematToolStripMenuItem.Enabled = True
    MainForm.cmdTemat.Enabled = True
    MainForm.cmdTematByNauczyciel.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

  End Sub ', Filter As String = ""
  Private Sub frmTemat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvTemat)
    ApplyNewConfig()

  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub LoadClassItems(cb As ComboBox)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, K As New KolumnaSQL
    cb.Items.Clear()
    Try
      R = DBA.GetReader(K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
      While R.Read()
        cb.Items.Add(New SchoolClassComboItem(R.GetInt32("ID"), R.GetString("Nazwa_Klasy"), R.GetBoolean("Virtual")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub FetchData()
    Dim T As New TematSQL, DBA As New DataBaseAction, CH As New CalcHelper
    dtTemat = DBA.GetDataTable(T.SelectTemat(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, CH.StartDateOfWeek(dtpDataZajec.Value).ToShortDateString, CH.EndDateOfWeek(dtpDataZajec.Value).ToShortDateString))
    dtFrekwencja = DBA.GetDataTable(T.SelectFrekwencja(CH.StartDateOfWeek(dtpDataZajec.Value).ToShortDateString, CH.EndDateOfWeek(dtpDataZajec.Value).ToShortDateString))
    Dim SelectString As String = ""
    If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
      SelectString = T.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, CH.EndDateOfWeek(dtpDataZajec.Value))
    Else
      SelectString = T.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString)
      Dim W As New WynikiSQL
      dtGrupa = DBA.GetDataTable(W.SelectGrupa(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
      dtIndividualStaff = DBA.GetDataTable(T.SelectIndividualStaff(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, CH.EndDateOfWeek(dtpDataZajec.Value)))
    End If
    dtStudent = DBA.GetDataTable(SelectString)
  End Sub
  
  Private Overloads Sub LvNewItem(ByVal LV As ListView, DataZajec As Date)
    Dim NG As New ListViewGroup(DataZajec.ToString("dddd --> d MMMM yyyy"))
    'Dim NG As New ListViewGroup(WeekdayName(Weekday(DataZajec, FirstDayOfWeek.Monday), False, FirstDayOfWeek.Monday) & " --> " & DataZajec.ToShortDateString)
    NG.HeaderAlignment = HorizontalAlignment.Center
    LV.Groups.Add(NG)
    For Each row As DataRow In dtTemat.Select("DzienTygodnia='" & Weekday(DataZajec, FirstDayOfWeek.Monday) & "'")
      Dim NewItem As New ListViewItem(row.Item(0).ToString, NG)
      NewItem.UseItemStyleForSubItems = True
      If CType(row.Item("StatusLekcji"), Boolean) Then
        NewItem.ForeColor = CType(IIf(IsDBNull(row.Item("Zastepca")) OrElse CType(row.Item("Status"), Boolean) = False, Color.Black, Color.FromArgb(128, 64, 0)), Color)
      Else
        NewItem.ForeColor = Color.LightGray
        'NewItem.ToolTipText = "Lekcja nie odbyła się"
      End If
      NewItem.SubItems.Add(row.Item(1).ToString)
      NewItem.SubItems.Add(IIf(row.Item("Nr").ToString = "0", Chr(151), row.Item("Nr")).ToString) 'row.Item(2).ToString)
      NewItem.SubItems.Add(row.Item(3).ToString)
      NewItem.SubItems.Add(row.Item(4).ToString)
      'Dim IdPrzydzial As String = ""
      Dim StudentCount As Int32 = 0
      If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual Then
        For Each Student As DataRow In dtStudent.Select("Przedmiot=" & row.Item("IdSzkolaPrzedmiot").ToString)
          If CType(Student.Item("StatusAktywacji"), Boolean) = True Then
            'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
            StudentCount += 1
          Else
            If CType(Student.Item("DataDeaktywacji"), Date) > CType(row.Item("Data"), Date) Then
              'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
              StudentCount += 1
            End If
          End If
        Next
      Else
        If dtGrupa.Select("IdSzkolaPrzedmiot=" & row.Item("IdSzkolaPrzedmiot").ToString).GetLength(0) > 0 Then
          For Each Student As DataRow In dtGrupa.Select("IdSzkolaPrzedmiot=" & row.Item("IdSzkolaPrzedmiot").ToString)
            If CType(Student.Item("StatusAktywacji"), Boolean) = True Then
              'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
              StudentCount += 1
            Else
              If CType(Student.Item("Deaktywacja"), Date) > CType(row.Item("Data"), Date) Then
                'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
                StudentCount += 1
              End If
            End If
          Next
        Else
          For Each Student As DataRow In dtStudent.Rows
            If CType(Student.Item("StatusAktywacji"), Boolean) = True Then
              'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
              StudentCount += 1
            Else
              If CType(Student.Item("DataDeaktywacji"), Date) > CType(row.Item("Data"), Date) Then
                'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
                StudentCount += 1
              End If
            End If
          Next
        End If
        StudentCount -= CType(dtIndividualStaff.Compute("Count(Przedmiot)", "Przedmiot=" & row.Item("IdSzkolaPrzedmiot").ToString & " AND DataAktywacji<=#" & CType(row.Item("Data"), Date).ToShortDateString & "# AND (DataDeaktywacji>#" & CType(row.Item("Data"), Date).ToShortDateString & "# OR DataDeaktywacji is null)"), Integer)
      End If

      'IdPrzydzial = IdPrzydzial.TrimEnd(",".ToCharArray)
      If CType(row.Item("StatusLekcji"), Boolean) Then 'AndAlso IdPrzydzial.Length > 0 Then
        NewItem.SubItems.Add((CType(StudentCount, Byte) - CType(dtFrekwencja.Compute("Count(IdLekcja)", "Typ IN ('u','n') AND IdLekcja=" & row.Item("IdLekcja").ToString & " AND Data=#" & DataZajec.ToShortDateString & "#"), Byte)).ToString)
        'NewItem.SubItems.Add((CType(dtStudent.Compute("Count(ID)", "IdPrzydzial IN (" & IdPrzydzial & ")"), Byte) - CType(dtFrekwencja.Compute("Count(IdLekcja)", "Typ IN ('u','n') AND IdLekcja=" & row.Item("IdLekcja").ToString & " AND Data=#" & dtpDataZajec.Value.ToShortDateString & "#"), Byte)).ToString)
        NewItem.SubItems.Add(dtFrekwencja.Compute("Count(IdLekcja)", "Typ IN ('u','n') AND IdLekcja=" & row.Item("IdLekcja").ToString & " AND Data=#" & DataZajec.ToShortDateString & "#").ToString)
      Else
        NewItem.SubItems.Add(Chr(151))
        NewItem.SubItems.Add(Chr(151))
      End If
      NewItem.SubItems.Add(row.Item(5).ToString)
      NewItem.SubItems.Add(row.Item("Data").ToString)

      NewItem.SubItems.Add(row.Item("IdZastepstwo").ToString)

      LV.Items.Add(NewItem)
    Next
  End Sub

 

  Private Sub GetData()
    Try
      lvTemat.Items.Clear()
      lvTemat.Groups.Clear()
      If chkAllWeek.Checked Then
        Dim CH As New CalcHelper, StartDate As Date
        StartDate = CH.StartDateOfWeek(dtpDataZajec.Value)
        For i As Integer = 0 To 6
          LvNewItem(lvTemat, StartDate.AddDays(i))
        Next
      Else
        LvNewItem(lvTemat, dtpDataZajec.Value)
        'LvNewItem(lvTemat, Weekday(dtpDataZajec.Value, FirstDayOfWeek.Monday))
      End If
      lvTemat.Columns(4).Width = CInt(IIf(lvTemat.Items.Count > 6, 431, 450))
      lvTemat.Enabled = CType(IIf(lvTemat.Items.Count > 0, True, False), Boolean)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvTemat.Items.Clear()
    lvTemat.Enabled = False
    ClearDetails()
    Dim CH As New CalcHelper
    If dtpDataZajec.MaxDate < CH.StartDateOfSchoolYear(My.Settings.SchoolYear) Then
      dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      dtpDataZajec.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    Else
      dtpDataZajec.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
    End If
    dtpDataZajec.Value = CType(IIf(Today >= dtpDataZajec.MinDate AndAlso Today <= dtpDataZajec.MaxDate, Today, dtpDataZajec.MinDate), Date)
    chkAllWeek_CheckedChanged(Nothing, Nothing)
    'FillKlasa(cbKlasa)
    LoadClassItems(cbKlasa)
    If My.Settings.ClassName.Length > 0 Then
      For Each Item As SchoolClassComboItem In cbKlasa.Items
        If Item.ID = CType(My.Settings.ClassName, Integer) Then
          cbKlasa.SelectedIndex = cbKlasa.Items.IndexOf(Item)
          Exit For
        End If
      Next
    End If

    'Dim SH As New SeekHelper
    'If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
    'If My.Settings.ClassName.Length > 0 Then cbKlasa.SelectedIndex = cbKlasa.FindStringExact(My.Settings.ClassName)
  End Sub
 
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvTemat.Items.Count
    lblZastepstwo.Text = ""
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ID As Integer)
    Try
      lblRecord.Text = lvTemat.SelectedItems(0).Index + 1 & " z " & lvTemat.Items.Count
      With dtTemat.Select("ID=" & ID)(0)
        'lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")")
        Dim User, Owner As String

        User = CType(.Item("User"), String).ToLower.Trim
        Owner = CType(.Item("Owner"), String).ToLower.Trim
        If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
          lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (Wł: ", GlobalValues.Users.Item(Owner).ToString, ")")
        Else
          Me.lblUser.Text = User & " (Wł: " & Owner & ")"
        End If
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
        If CType(.Item("StatusLekcji"), Boolean) Then
          lblZastepstwo.Text = IIf(Not IsDBNull(.Item("Zastepca")) AndAlso CType(.Item("Status"), Boolean), "Zastępstwo: " & .Item("Zastepca").ToString, "").ToString
        Else
          lblZastepstwo.Text = "Lekcja nie odbyła się"
        End If
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Overloads Sub ListViewConfig(ByVal lv As ListView)
    With lv
      lv.ShowGroups = True
      .View = View.Details
      '.Enabled = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      SetColumns(lv)
      .Items.Clear()
      '.Enabled = False
    End With
  End Sub
  Private Sub SetColumns(lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Lekcja", 60, HorizontalAlignment.Center)
      .Columns.Add("L.p.", 30, HorizontalAlignment.Center)
      .Columns.Add("Przedmiot", 195, HorizontalAlignment.Left)
      .Columns.Add("Treść tematu", 450, HorizontalAlignment.Left)
      .Columns.Add("Ob", 35, HorizontalAlignment.Center)
      .Columns.Add("Nb", 35, HorizontalAlignment.Center)
      .Columns.Add("IdGodzina", 0, HorizontalAlignment.Left)
      .Columns.Add("DataLekcji", 0, HorizontalAlignment.Left)
      .Columns.Add("IdZastepstwo", 0)
      '.Columns.Add("IdObsada", 0, HorizontalAlignment.Left)
    End With
  End Sub


  'Private Sub FillKlasa(ByVal cb As ComboBox)
  '  cb.Items.Clear()
  '  Dim FCB As New FillComboBox, K As New KolumnaSQL
  '  FCB.AddComboBoxComplexItems(cb, K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
  '  cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
  'End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    'InRefresh = False
    dtpDataZajec_ValueChanged(Nothing, Nothing)
    'GetData(CType(cbKlasa.SelectedItem, CbItem).ID.ToString)
    cmdAddNew.Enabled = True
    EnableButtons(False)
  End Sub
  Private Sub EnableButtons(Value As Boolean)
    'Me.cmdAddNew.Enabled = Value 'CType(IIf(My.Application.OpenForms.OfType(Of dlgLekcja)().Any(), False, True), Boolean)
    cmdEdit.Enabled = Value
    Me.cmdDelete.Enabled = Value
  End Sub

  Private Sub dtpDataZajec_ValueChanged(sender As Object, e As EventArgs) Handles dtpDataZajec.ValueChanged
    Cursor = Cursors.WaitCursor
    If cbKlasa.SelectedItem Is Nothing Then Exit Sub
    EnableButtons(False)
    FetchData()
    'Filter = "DzienTygodnia='" & dtpDataZajec.Value.DayOfWeek & "'" '"Data=#" & dtpDataZajec.Value & "#"
    GetData()
    ClearDetails()
    'chkAllWeek_CheckedChanged(Nothing, Nothing)
    Cursor = Cursors.Default
  End Sub

  Private Sub chkAllWeek_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllWeek.CheckedChanged
    Cursor = Cursors.WaitCursor
    If cbKlasa.SelectedItem IsNot Nothing Then GetData()
    Cursor = Cursors.Default
  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub lvTemat_DoubleClick(sender As Object, e As EventArgs) Handles lvTemat.DoubleClick
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTemat.Select("ID=" & lvTemat.SelectedItems(0).Text)(0).Item("Owner").ToString.ToLower.Trim Then EditTemat()
  End Sub

  Private Sub lvTemat_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvTemat.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(CType(e.Item.Text, Integer))
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTemat.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.ToLower.Trim Then EnableButtons(True)
    Else
      ClearDetails()
      EnableButtons(False)
    End If
  End Sub

  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgTemat
    Try
      With dlgAddNew
        .Text = "Nowy temat lekcji"
        .IsNewMode = True
        .ListViewConfig(.lvPresent)
        .ListViewConfig(.lvAbsent)
        .ListViewConfig(.lvLate)
        .SetColumns(.lvPresent, "Obecni")
        .SetColumns(.lvAbsent, "Nieobecni")
        .SetColumns(.lvLate, "Spóźnieni")
        'poprawka()
        .DtStudent = dtStudent.Copy
        .dtFrekwencja = dtFrekwencja.Copy

        Dim DBA As New DataBaseAction, T As New TematSQL
        .dtObsada = DBA.GetDataTable(T.SelectPrzedmiot(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, dtpDataZajec.Value))
        .dtNrTemat = DBA.GetDataTable(T.SelectMaxNr(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear))
        .dtZastepstwo = DBA.GetDataTable(T.SelectZastepca(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, dtpDataZajec.Value.ToShortDateString))
        If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual = False Then
          .IsVirtual = False
          .dtGrupa = dtGrupa.Copy
          .dtIndividualStaff = dtIndividualStaff.Copy
        Else
          .IsVirtual = True
        End If
        '.GetData()
        .DataLekcji = dtpDataZajec.Value
        .Klasa = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString
        .FillGodzina()
        '.FillBelfer()
        AddHandler dlgAddNew.NewAdded, AddressOf NewTematAdded
        Me.cmdAddNew.Enabled = False
        'Indeks wykracza poza granice tabeli
        .ShowDialog()
        cmdAddNew.Enabled = True
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub NewTematAdded(ByVal InsertedID As String)

    FetchData()
    GetData()
    ClearDetails()
    Dim SH As New SeekHelper
    SH.FindListViewItem(lvTemat, InsertedID)
  End Sub

  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    EditTemat()
  End Sub
  Private Sub EditTemat()
    Dim MySQLTrans As MySqlTransaction = Nothing
    Try
      Dim dlgEdit As New dlgTemat
      Dim txtGodzina As New TextBox

      With dlgEdit
        .IsRefreshMode = True
        .cbGodzina.Visible = False
        txtGodzina.Location = .cbGodzina.Location
        txtGodzina.Size = .cbGodzina.Size
        txtGodzina.Enabled = False
        .Controls.Add(txtGodzina)
        .Text = "Edycja tematu lekcji"
        .IsNewMode = False
        .ListViewConfig(.lvPresent)
        .ListViewConfig(.lvAbsent)
        .ListViewConfig(.lvLate)
        .SetColumns(.lvPresent, "Obecni")
        .SetColumns(.lvAbsent, "Nieobecni")
        .SetColumns(.lvLate, "Spóźnieni")
        .DtStudent = dtStudent.Copy
        .dtFrekwencja = dtFrekwencja.Copy
        If CType(cbKlasa.SelectedItem, SchoolClassComboItem).IsVirtual = False Then
          .IsVirtual = False
          .dtGrupa = dtGrupa.Copy
          .dtIndividualStaff = dtIndividualStaff.Copy
        Else
          .IsVirtual = True
        End If
        Dim DBA As New DataBaseAction, T As New TematSQL

        .dtZastepstwo = DBA.GetDataTable(T.SelectZastepca(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, lvTemat.SelectedItems(0).SubItems(8).Text))
        .dtObsada = DBA.GetDataTable(T.SelectPrzedmiot(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, CType(lvTemat.SelectedItems(0).SubItems(8).Text, Date)))
        '.dtGrupa = dtGrupa.Copy
        txtGodzina.Text = DBA.GetSingleValue(T.SelectGodzina(lvTemat.SelectedItems(0).SubItems(7).Text)) & " --> "
        txtGodzina.Text += lvTemat.SelectedItems(0).SubItems(3).Text
        .txtTemat.Text = lvTemat.SelectedItems(0).SubItems(4).Text
        .txtTemat.Enabled = True
        .Lp = CType(IIf(IsNumeric(lvTemat.SelectedItems(0).SubItems(2).Text), lvTemat.SelectedItems(0).SubItems(2).Text, 0), Integer)
        .nudNr.Value = .Lp
        .nudNr.Enabled = True
        .DataLekcji = CType(lvTemat.SelectedItems(0).SubItems(8).Text, Date) 'dtpDataZajec.Value
        .Klasa = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString
        .IdLekcja = CType(dtTemat.Select("ID=" & CType(lvTemat.SelectedItems(0).Text, Integer))(0).Item("IdLekcja"), Integer)

        .chkStatus.Checked = Not CType(dtTemat.Select("ID=" & lvTemat.SelectedItems(0).Text)(0).Item("StatusLekcji"), Boolean)
        .chkStatus.Enabled = True
        .chkStatus_CheckedChanged(Nothing, Nothing)
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False
        .IsRefreshMode = False
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdTemat As String
          IdTemat = Me.lvTemat.SelectedItems(0).Text
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim cmd As MySqlCommand = DBA.CreateCommand(T.UpdateTopic)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?Nr", IIf(.chkStatus.Checked, 0, .nudNr.Value))
          cmd.Parameters.AddWithValue("?Tresc", .txtTemat.Text.Trim)
          cmd.Parameters.AddWithValue("?ID", IdTemat)
          cmd.Parameters.AddWithValue("?StatusLekcji", Not .chkStatus.Checked)
          cmd.ExecuteNonQuery()
          If Not .chkStatus.Checked Then
            'Dim cmdZastepstwo As MySqlCommand = DBA.CreateCommand(T.UpdateSubstituteStatus)
            If Not IsDBNull(dtTemat.Select("ID=" & IdTemat)(0).Item("IdZastepstwo")) Then
              cmd.CommandText = T.UpdateSubstituteStatus
              cmd.Parameters.AddWithValue("?IdZastepstwo", dtTemat.Select("ID=" & IdTemat)(0).Item("IdZastepstwo").ToString)
              cmd.Parameters.AddWithValue("?Status", .chkZastepstwo.Checked)
              cmd.ExecuteNonQuery()
            End If

            For Each Student As ListViewItem In .lvPresent.Items
              If Student.SubItems(3).Text.Length > 0 Then
                Dim cmdPresent As MySqlCommand = DBA.CreateCommand(T.DeleteAbsence)
                cmdPresent.Transaction = MySQLTrans
                cmdPresent.Parameters.AddWithValue("?ID", Student.SubItems(3).Text)
                cmdPresent.ExecuteNonQuery()
              End If
            Next
            For Each Student As ListViewItem In .lvAbsent.Items
              If Student.SubItems(3).Text.Length = 0 Then
                Dim cmdAbsent As MySqlCommand = DBA.CreateCommand(T.InsertAbsence)
                cmdAbsent.Transaction = MySQLTrans
                cmdAbsent.Parameters.AddWithValue("?IdUczen", Student.Text)
                cmdAbsent.Parameters.AddWithValue("?IdLekcja", CType(dtTemat.Select("ID=" & CType(IdTemat, Integer))(0).Item("IdLekcja"), Integer))
                cmdAbsent.Parameters.AddWithValue("?Typ", "n")
                cmdAbsent.Parameters.AddWithValue("?Data", CType(lvTemat.SelectedItems(0).SubItems(8).Text, Date)) 'dtpDataZajec.Value.ToShortDateString)
                cmdAbsent.ExecuteNonQuery()
              ElseIf Student.SubItems(3).Text.Length > 0 AndAlso Student.SubItems(4).Text = "s" Then
                Dim cmdAbsent As MySqlCommand = DBA.CreateCommand(T.UpdateAbsence)
                cmdAbsent.Transaction = MySQLTrans
                cmdAbsent.Parameters.AddWithValue("?ID", Student.SubItems(3).Text)
                cmdAbsent.Parameters.AddWithValue("?Typ", "n")
                cmdAbsent.ExecuteNonQuery()
              End If

            Next
            For Each Student As ListViewItem In .lvLate.Items
              If Student.SubItems(3).Text.Length = 0 Then
                Dim cmdLate As MySqlCommand = DBA.CreateCommand(T.InsertAbsence)
                cmdLate.Transaction = MySQLTrans
                cmdLate.Parameters.AddWithValue("?IdUczen", Student.Text)
                cmdLate.Parameters.AddWithValue("?IdLekcja", CType(dtTemat.Select("ID=" & CType(IdTemat, Integer))(0).Item("IdLekcja"), Integer))
                cmdLate.Parameters.AddWithValue("?Typ", "s")
                cmdLate.Parameters.AddWithValue("?Data", CType(lvTemat.SelectedItems(0).SubItems(8).Text, Date)) 'dtpDataZajec.Value.ToShortDateString)
                cmdLate.ExecuteNonQuery()
              ElseIf Student.SubItems(3).Text.Length > 0 AndAlso Student.SubItems(4).Text <> "s" Then
                Dim cmdAbsent As MySqlCommand = DBA.CreateCommand(T.UpdateAbsence)
                cmdAbsent.Transaction = MySQLTrans
                cmdAbsent.Parameters.AddWithValue("?ID", Student.SubItems(3).Text)
                cmdAbsent.Parameters.AddWithValue("?Typ", "s")
                cmdAbsent.ExecuteNonQuery()
              End If
            Next
          Else
            If Not IsDBNull(dtTemat.Select("ID=" & IdTemat)(0).Item("IdZastepstwo")) Then
              cmd.CommandText = T.UpdateSubstituteStatus
              cmd.Parameters.AddWithValue("?IdZastepstwo", dtTemat.Select("ID=" & IdTemat)(0).Item("IdZastepstwo").ToString)
              cmd.Parameters.AddWithValue("?Status", 0)
              cmd.ExecuteNonQuery()
            End If
          End If

          MySQLTrans.Commit()

          FetchData()
          dtpDataZajec_ValueChanged(Nothing, Nothing)
          ClearDetails()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvTemat, IdTemat)
        End If
      End With
    Catch myex As MySqlException
      MessageBox.Show(myex.Message & vbNewLine & myex.InnerException.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, T As New TematSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvTemat.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(T.DeleteTopic)
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        EnableButtons(False)
        FetchData()
        GetData()
        ClearDetails()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvTemat, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub

End Class
Public Class SchoolClassComboItem
  Public Property ID As Integer
  Public Property NazwaKlasy As String
  Public Property IsVirtual As Boolean
  Public Property KodKlasy As String
  Public Property PionKlas As Byte
  Public Property IdObsadaWychowawstwa As Integer

  Public Overrides Function ToString() As String
    Return NazwaKlasy
  End Function
  Sub New(ByVal IdKlasa As Integer, ByVal Nazwa As String, Optional Virtual As Boolean = False, Optional Kod As String = "")
    ID = IdKlasa
    NazwaKlasy = Nazwa
    IsVirtual = Virtual
    KodKlasy = Kod
  End Sub
  Sub New(ByVal IdKlasa As Integer, ByVal Nazwa As String, Pion As Byte, IdObsada As Integer, Kod As String)
    ID = IdKlasa
    NazwaKlasy = Nazwa
    PionKlas = Pion
    IdObsadaWychowawstwa = IdObsada
    KodKlasy = Kod
  End Sub
End Class
