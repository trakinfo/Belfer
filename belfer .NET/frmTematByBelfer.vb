Public Class frmTematByBelfer
  Private dtTemat, dtFrekwencja, dtStudent, dtGrupa, dtZastepstwo, dtZamiast, dtIndividualStaff, dtZdarzenie As DataTable ', Obsada As Hashtable

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
  Private Sub FetchData()
    Dim T As New TematSQL, DBA As New DataBaseAction, CH As New CalcHelper

    dtTemat = DBA.GetDataTable(T.SelectTemat(CType(cbObsada.SelectedItem, ObjectStaffComboItem).ID.ToString, CType(cbBelfer.SelectedItem, CbItem).ID.ToString, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString, My.Settings.SchoolYear))

    dtZastepstwo = DBA.GetDataTable(T.SelectZastepstwo(CType(cbObsada.SelectedItem, ObjectStaffComboItem).ID.ToString))
    dtZamiast = DBA.GetDataTable(T.SelectZastepstwo(CType(cbBelfer.SelectedItem, CbItem).ID.ToString, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString, My.Settings.SchoolYear))
    dtFrekwencja = DBA.GetDataTable(T.SelectFrekwencja(CType(cbObsada.SelectedItem, ObjectStaffComboItem).ID.ToString))
    dtZdarzenie = DBA.GetDataTable(T.SelectEvent(CType(cbObsada.SelectedItem, ObjectStaffComboItem).ID.ToString))
    'Dim W As New WynikiSQL
    'dtGrupa = DBA.GetDataTable(W.SelectGrupa(CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString, My.Settings.SchoolYear))
    'dtStudent = DBA.GetDataTable(T.SelectStudent(My.Settings.SchoolYear, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString))
    Dim SelectString As String = ""
    If CType(cbObsada.SelectedItem, ObjectStaffComboItem).IsVirtual Then
      SelectString = T.SelectStudent(My.Settings.SchoolYear, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString, CH.EndDateOfSchoolYear(My.Settings.SchoolYear))
    Else
      SelectString = T.SelectStudent(My.Settings.SchoolYear, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString)
      Dim W As New WynikiSQL
      dtGrupa = DBA.GetDataTable(W.SelectGrupa(CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString, My.Settings.SchoolYear))
      dtIndividualStaff = DBA.GetDataTable(T.SelectIndividualStaff(My.Settings.SchoolYear, CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString))
    End If
    dtStudent = DBA.GetDataTable(SelectString)
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView)
    For Each R As DataRow In dtTemat.DefaultView.ToTable(True, "Miesiac").Rows
      Dim NG As New ListViewGroup(MonthName(CType(R.Item("Miesiac"), Integer), False).ToUpper, HorizontalAlignment.Center)
      'NG.IsExpanded = False
      'NG.HeaderAlignment = HorizontalAlignment.Center
      LV.Groups.Add(NG)
      For Each Row As DataRow In dtTemat.Select("Miesiac='" & R.Item("Miesiac").ToString & "'")
        Dim NewItem As New ListViewItem(Row.Item(0).ToString, NG)
        NewItem.UseItemStyleForSubItems = True
        NewItem.SubItems.Add(Row.Item("NrLekcji").ToString)
        If dtZastepstwo.Select("Status=1 AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data='" & Row.Item("Data").ToString & "'").Length > 0 Then
          NewItem.SubItems.Add(Chr(151))
        Else
          NewItem.SubItems.Add(IIf(Row.Item("Nr").ToString = "0", Chr(151), Row.Item("Nr")).ToString)
        End If
        NewItem.SubItems.Add(CType(Row.Item("Data"), Date).ToString("yyyy-MM-dd --> dddd"))
        NewItem.SubItems.Add(Row.Item("Tresc").ToString)

        Dim StudentCount As Int32 = 0
        If CType(cbObsada.SelectedItem, ObjectStaffComboItem).IsVirtual Then
          For Each Student As DataRow In dtStudent.Select("Przedmiot=" & CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString)
            If CType(Student.Item("StatusAktywacji"), Boolean) = True Then
              StudentCount += 1
            Else
              If CType(Student.Item("DataDeaktywacji"), Date) > CType(Row.Item("Data"), Date) Then
                StudentCount += 1
              End If
            End If
          Next
        Else
          If dtGrupa.Select("IdSzkolaPrzedmiot=" & CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString).GetLength(0) > 0 Then
            For Each Student As DataRow In dtGrupa.Select("IdSzkolaPrzedmiot=" & CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString)
              If CType(Student.Item("StatusAktywacji"), Boolean) = True Then
                'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
                StudentCount += 1
              Else
                If CType(Student.Item("Deaktywacja"), Date) > CType(Row.Item("Data"), Date) Then
                  'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
                  StudentCount += 1
                End If
              End If
            Next
          Else
            For Each Student As DataRow In dtStudent.Select()
              If CType(Student.Item("StatusAktywacji"), Boolean) = True Then
                'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
                StudentCount += 1
              Else
                If CType(Student.Item("DataDeaktywacji"), Date) > CType(Row.Item("Data"), Date) Then
                  'IdPrzydzial += Student.Item("IdPrzydzial").ToString & ","
                  StudentCount += 1
                End If
              End If
            Next
          End If
          'tutaj()
          StudentCount -= CType(dtIndividualStaff.Compute("Count(Przedmiot)", "Przedmiot=" & Row.Item("Przedmiot").ToString & " AND DataAktywacji<=#" & CType(Row.Item("Data"), Date).ToShortDateString & "# AND (DataDeaktywacji>#" & CType(Row.Item("Data"), Date).ToShortDateString & "# OR DataDeaktywacji is null)"), Integer)
        End If

        'IdPrzydzial = IdPrzydzial.TrimEnd(",".ToCharArray)

        If CType(Row.Item("Status"), Boolean) Then
          NewItem.SubItems.Add((CType(StudentCount, Byte) - CType(dtFrekwencja.Compute("Count(IdLekcja)", "Typ IN ('u','n') AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data=#" & CType(Row.Item("Data"), Date).ToShortDateString & "#"), Byte)).ToString)
          'NewItem.SubItems.Add((CType(dtStudent.Compute("Count(ID)", "IdPrzydzial IN (" & IdPrzydzial & ")"), Byte) - CType(dtFrekwencja.Compute("Count(IdLekcja)", "Typ IN ('u','n') AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data=#" & CType(Row.Item("Data"), Date).ToShortDateString & "#"), Byte)).ToString)
          NewItem.SubItems.Add(dtFrekwencja.Compute("Count(IdLekcja)", "Typ IN ('u','n') AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data=#" & CType(Row.Item("Data"), Date).ToShortDateString & "#").ToString)
        Else
          NewItem.SubItems.Add(Chr(151))
          NewItem.SubItems.Add(Chr(151))
        End If

        NewItem.SubItems.Add(Row.Item("IdGodzina").ToString)
        If dtZastepstwo.Select("Status=1 AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data='" & Row.Item("Data").ToString & "'").Length > 0 Then
          NewItem.ForeColor = Color.DarkGray 'Color.FromArgb(128, 64, 0)
          NewItem.SubItems.Add(dtZastepstwo.Select("Status=1 AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data='" & Row.Item("Data").ToString & "'")(0).Item("IdZastepstwo").ToString)
        ElseIf dtZamiast.Select("Status=1 AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data='" & Row.Item("Data").ToString & "'").Length > 0 Then
          NewItem.ForeColor = Color.Blue   'Color.FromArgb(128, 64, 0)
          NewItem.SubItems.Add(dtZamiast.Select("Status=1 AND IdLekcja=" & Row.Item("IdLekcja").ToString & " AND Data='" & Row.Item("Data").ToString & "'")(0).Item("IdZastepstwo").ToString)
        ElseIf CType(Row.Item("Status"), Boolean) = False Then
          NewItem.ForeColor = Color.LightGray
          NewItem.SubItems.Add("0")
        Else
          NewItem.ForeColor = Color.Black
          NewItem.SubItems.Add("0")
        End If
        NewItem.SubItems.Add(Row.Item("IdLekcja").ToString)
        LV.Items.Add(NewItem)
      Next
    Next
  End Sub

  Private Sub GetData()
    Try
      lvTemat.BeginUpdate()
      lvTemat.Items.Clear()
      lvTemat.Groups.Clear()
      LvNewItem(lvTemat)
      lvTemat.Columns(4).Width = CInt(IIf(lvTemat.Items.Count > 16, 431, 450))
      lvTemat.Enabled = CType(IIf(lvTemat.Items.Count > 0, True, False), Boolean)
      'lvTemat.Groups(lvTemat.Groups.Count - 1).IsExpanded = True
      lvTemat.EndUpdate()
      ClearDetails()
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvTemat.Items.Clear()
    lvTemat.Enabled = False
    ClearDetails()
    FillBelfer()
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
        'lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item("User").ToString

        Dim User, Owner As String
        User = .Item("User").ToString.ToLower.Trim
        Owner = .Item("Owner").ToString.ToLower.Trim
        If GlobalValues.Users.ContainsKey(User) AndAlso GlobalValues.Users.ContainsKey(Owner) Then
          lblUser.Text = String.Concat(GlobalValues.Users.Item(User).ToString, " (Wł: ", GlobalValues.Users.Item(Owner).ToString, ")")
        Else
          Me.lblUser.Text = User & " (Wł: " & Owner & ")"
        End If

        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
        If lvTemat.SelectedItems(0).SubItems(8).Text = "0" Then
          If CType(dtTemat.Select("ID=" & lvTemat.SelectedItems(0).Text)(0).Item("Status"), Boolean) Then
            lblZastepstwo.Text = ""
          Else
            lblZastepstwo.ForeColor = Color.Gray
            lblZastepstwo.Text = "Lekcja nie odbyła się"
          End If
        Else
          If dtZastepstwo.Select("Status=1 AND IdLekcja=" & lvTemat.SelectedItems(0).SubItems(9).Text & " AND Data='" & lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10) & "'").Length > 0 Then
            lblZastepstwo.ForeColor = Color.DarkGray
            lblZastepstwo.Text = "Zastępstwo: " & dtZastepstwo.Select("IdZastepstwo=" & lvTemat.SelectedItems(0).SubItems(8).Text)(0).Item("Zastepca").ToString
          ElseIf dtZamiast.Select("Status=1 AND IdLekcja=" & lvTemat.SelectedItems(0).SubItems(9).Text & " AND Data='" & lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10) & "'").Length > 0 Then
            cmdEdit.Enabled = False
            cmdDelete.Enabled = False
            lblZastepstwo.ForeColor = Color.Blue
            lblZastepstwo.Text = "Zamiast: " & dtZamiast.Select("IdZastepstwo=" & lvTemat.SelectedItems(0).SubItems(8).Text)(0).Item("Zastepca").ToString
          Else
            'lblZastepstwo.ForeColor = Color.LightGray
            'lblZastepstwo.Text = "Lekcja nie odbyła się"
          End If
        End If

      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub FillBelfer()
    cbBelfer.Items.Clear()
    Dim FCB As New FillComboBox, T As New TematSQL

    FCB.AddComboBoxComplexItems(cbBelfer, T.SelectBelfer(My.Settings.IdSchool, My.Settings.SchoolYear))
    Dim SH As New SeekHelper
    If My.Settings.IdBelfer.Length > 0 Then SH.FindComboItem(Me.cbBelfer, CType(My.Settings.IdBelfer, Integer))
    cbBelfer.Enabled = CType(IIf(cbBelfer.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbPrzedmiot_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbBelfer.SelectionChangeCommitted
    My.Settings.IdBelfer = CType(cbBelfer.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBelfer.SelectedIndexChanged
    lvTemat.Items.Clear()
    cmdAddNew.Enabled = False
    cmdEvent.Enabled = False
    EnableButtons(False)
    'FillKlasa(cbObsada)
    LoadObjectStaffItems(cbObsada)
    'Dim SH As New SeekHelper
    'If My.Settings.IdObsada.Length > 0 Then SH.FindComboItem(Me.cbObsada, CType(My.Settings.IdObsada, Integer))
    If My.Settings.IdObsada.Length > 0 Then
      For Each Item As ObjectStaffComboItem In cbObsada.Items
        If Item.ID = CType(My.Settings.IdObsada, Integer) Then
          cbObsada.SelectedIndex = cbObsada.Items.IndexOf(Item)
          Exit For
        End If
      Next
    End If
  End Sub

  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbObsada.SelectionChangeCommitted
    My.Settings.IdObsada = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbObsada.SelectedIndexChanged
    Cursor = Cursors.WaitCursor
    Try
      My.Settings.ClassName = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID 'Obsada.Item("Klasa").ToString
      My.Settings.ObjectName = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString   'Obsada.Item("Przedmiot").ToString
      My.Settings.Save()
      FetchData()
      GetData()
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.SchoolTeacherID = CType(cbBelfer.SelectedItem, CbItem).ID.ToString Then 'Obsada.Item("Nauczyciel").ToString Then
        cmdAddNew.Enabled = True
        cmdEvent.Enabled = True
      Else
        cmdAddNew.Enabled = False
      End If
      EnableButtons(False)
      GetEvent()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try
    Cursor = Cursors.Default
  End Sub
  Private Sub GetEvent()
    ttEvent.Hide(lvTemat)
    ttEventOmitted.Hide(lvTemat)
    If dtZdarzenie.Select("Data=#" & Date.Now.ToShortDateString & "# AND Status=0").Length > 0 Then
      'Dim ttEvent As New ToolTip,
      Dim Content As String = ""
      ttEvent.ToolTipTitle = "Zaplanowane zadania"
      ttEvent.ToolTipIcon = ToolTipIcon.Info
      'ttEvent.OwnerDraw = True
      For Each E As DataRow In dtZdarzenie.Select("Data=#" & Date.Now.ToShortDateString & "# AND Status=0")
        Content += "Data: " & CType(E.Item("Data"), Date).ToShortDateString & vbNewLine
        Content += "Treść: " & E.Item("Info").ToString & vbNewLine
        Content += "Status: " & CType(E.Item("Status"), GlobalValues.EventStatus).ToString & vbNewLine
      Next
      ttEvent.Show(Content, lvTemat, New Point(lvTemat.Location.X + CType(lvTemat.Width / 2, Integer), lvTemat.Location.Y), 10000)
    End If
    If dtZdarzenie.Select("Data<#" & Date.Now.ToShortDateString & "# AND Status=0").Length > 0 Then
      'Dim ttEventOmitted As New ToolTip, 
      Dim Content As String = ""
      ttEventOmitted.ToolTipIcon = ToolTipIcon.Warning
      ttEventOmitted.ToolTipTitle = "Pominięte zadania"
      'ttEventOmitted.AutomaticDelay = 10000
      For Each E As DataRow In dtZdarzenie.Select("Data<#" & Date.Now.ToShortDateString & "# AND Status=0")
        Content += "Data: " & CType(E.Item("Data"), Date).ToShortDateString & vbNewLine
        Content += "Treść: " & E.Item("Info").ToString & vbNewLine
        Content += "Status: " & CType(E.Item("Status"), GlobalValues.EventStatus).ToString & vbNewLine
      Next
      ttEventOmitted.Show(Content, lvTemat, New Point(lvTemat.Location.X, lvTemat.Location.Y), 10000)
    End If
  End Sub
  Private Overloads Sub ListViewConfig(ByVal lv As ListView)
    With lv
      lv.ShowGroups = True
      .View = View.Details
      .Enabled = True
      .FullRowSelect = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      '.AllowAutoToolTips = True
      '.View = BetterListViewView.Details
      '.FullRowSelect = True
      '.GridLines = BetterListViewGridLines.None
      '.MultiSelect = True
      '.ColumnReorderMode = BetterListViewColumnReorderMode.Disabled
      '.AutoResizeColumns(BetterListViewColumnHeaderAutoResizeStyle.HeaderSize)
      '.HideSelection = False
      '.HeaderStyle = BetterListViewHeaderStyle.Nonclickable
      '.ShowDefaultGroupHeader = False
      '.ShowGroups = True
      '.ShowToolTips = True
      '.ShowToolTipsColumns = True
      '.ShowToolTipsGroups = True
      '.ShowToolTipsSubItems = True
      SetColumns(lv)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub SetColumns(lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Center) '0
      .Columns.Add("Lekcja", 79, HorizontalAlignment.Center) '1
      .Columns.Add("L.p.", 50, HorizontalAlignment.Center) '2
      .Columns.Add("Data", 150, HorizontalAlignment.Left) '3
      .Columns.Add("Treść tematu", 450, HorizontalAlignment.Left) '4
      .Columns.Add("Ob", 50, HorizontalAlignment.Center) '5
      .Columns.Add("Nb", 50, HorizontalAlignment.Center) '6
      .Columns.Add("IdGodzina", 0) '7
      .Columns.Add("IdZastepstwo", 0) '8
      .Columns.Add("IdLekcja", 0) '9
    End With
  End Sub

  Private Sub EnableButtons(Value As Boolean)
    'Me.cmdAddNew.Enabled = Value 'CType(IIf(My.Application.OpenForms.OfType(Of dlgLekcja)().Any(), False, True), Boolean)
    cmdEdit.Enabled = Value
    Me.cmdDelete.Enabled = Value
  End Sub


  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  'Private Sub lvStudent_DrawColumnHeaderBackground(sender As Object, e As BetterListViewDrawColumnHeaderBackgroundEventArgs) Handles lvTemat.DrawColumnHeaderBackground
  '  e.Graphics.FillRectangle(Brushes.LightGray, e.ColumnHeaderBounds.BoundsOuter)
  '  e.Graphics.FillRectangle(New SolidBrush(SystemColors.ButtonFace), e.ColumnHeaderBounds.BoundsOuter)
  '  e.Graphics.DrawLine(Pens.White, e.ColumnHeaderBounds.BoundsSpacing.Left, e.ColumnHeaderBounds.BoundsSpacing.Top, e.ColumnHeaderBounds.BoundsSpacing.Left, e.ColumnHeaderBounds.BoundsSpacing.Bottom)
  'End Sub
  'Private Sub lvTemat_DoubleClick(sender As Object, e As EventArgs)
  '  If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTemat.Select("ID=" & lvTemat.SelectedItems(0).Text)(0).Item("Owner").ToString.Trim Then EditTemat()
  '  'If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.SchoolTeacherID = Obsada.Item("Nauczyciel").ToString Then EditTemat()
  'End Sub
  Private Sub lvTemat_DoubleClick(sender As Object, e As EventArgs) Handles lvTemat.DoubleClick
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTemat.Select("ID=" & lvTemat.SelectedItems(0).Text)(0).Item("Owner").ToString.Trim Then EditTemat()
  End Sub
  Private Sub lvTemat_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvTemat.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(CType(e.Item.Text, Integer))

      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTemat.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)

      'If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.SchoolTeacherID = Obsada.Item("Nauczyciel").ToString Then EnableButtons(True)
    Else
      ClearDetails()
      EnableButtons(False)
    End If
  End Sub
  'Private Sub lvTemat_SelectedIndexChanged(sender As Object, e As EventArgs)
  '  If lvTemat.SelectedItems.Count > 0 Then
  '    GetDetails(CType(lvTemat.SelectedItems(0).Text, Integer))

  '    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtTemat.Select("ID=" & lvTemat.SelectedItems(0).Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
  '  Else
  '    ClearDetails()
  '    EnableButtons(False)
  '  End If
  'End Sub
  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgTematByBelfer
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
        .DtStudent = dtStudent.Copy
        .dtFrekwencja = dtFrekwencja.Copy
        .IdObsada = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ID.ToString
        If CType(cbObsada.SelectedItem, ObjectStaffComboItem).IsVirtual = False Then
          .IsVirtual = False
          .dtGrupa = dtGrupa.Copy
          .dtIndividualStaff = dtIndividualStaff.Copy
        Else
          .IsVirtual = True
        End If
        '.dtGrupa = dtGrupa.Copy
        Dim CH As New CalcHelper
        If .dtpDataZajec.MaxDate < CH.StartDateOfSchoolYear(My.Settings.SchoolYear) Then
          .dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
          .dtpDataZajec.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
        Else
          .dtpDataZajec.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
          .dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
        End If
        '.dtpDataZajec.Value = Date.Now
        If Today < .dtpDataZajec.MinDate Then
          .dtpDataZajec.Value = .dtpDataZajec.MinDate
        ElseIf Today > .dtpDataZajec.MaxDate Then
          .dtpDataZajec.Value = .dtpDataZajec.MaxDate
        Else
          .dtpDataZajec.Value = Date.Today
        End If
        .Przedmiot = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString 'Obsada.Item("Przedmiot").ToString
        '.FillGodzina()
        AddHandler dlgAddNew.NewAdded, AddressOf NewTematAdded
        'Me.cmdAddNew.Enabled = False
        .ShowDialog()
        'cmdAddNew.Enabled = True
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
      Dim dlgEdit As New dlgTematByBelfer
      Dim txtGodzina As New TextBox

      With dlgEdit
        .IsRefreshMode = True
        .IsNewMode = False
        .cbGodzina.Visible = False
        txtGodzina.Location = .cbGodzina.Location
        txtGodzina.Size = .cbGodzina.Size
        txtGodzina.Enabled = False
        .Controls.Add(txtGodzina)
        .Text = "Edycja tematu lekcji"
        .ListViewConfig(.lvPresent)
        .ListViewConfig(.lvAbsent)
        .ListViewConfig(.lvLate)
        .SetColumns(.lvPresent, "Obecni")
        .SetColumns(.lvAbsent, "Nieobecni")
        .SetColumns(.lvLate, "Spóźnieni")
        .DtStudent = dtStudent.Copy
        .dtFrekwencja = dtFrekwencja.Copy
        If CType(cbObsada.SelectedItem, ObjectStaffComboItem).IsVirtual = False Then
          .IsVirtual = False
          .dtGrupa = dtGrupa.Copy
          .dtIndividualStaff = dtIndividualStaff.Copy
        Else
          .IsVirtual = True
        End If
        Dim DBA As New DataBaseAction, T As New TematSQL

        '.dtGrupa = dtGrupa.Copy
        txtGodzina.Text = lvTemat.SelectedItems(0).SubItems(1).Text & ") " & DBA.GetSingleValue(T.SelectGodzina(lvTemat.SelectedItems(0).SubItems(7).Text))

        .txtTemat.Text = lvTemat.SelectedItems(0).SubItems(4).Text
        .txtTemat.Enabled = True

        .nudNr.Value = CType(IIf(IsNumeric(lvTemat.SelectedItems(0).SubItems(2).Text), lvTemat.SelectedItems(0).SubItems(2).Text, 0), Integer)
        .nudNr.Enabled = True
        .IdObsada = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ID.ToString
        .IdLekcja = CType(lvTemat.SelectedItems(0).SubItems(9).Text, Integer)
        .Przedmiot = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ObjectID.ToString 'Obsada.Item("Przedmiot").ToString
        .chkStatus.Checked = Not CType(dtTemat.Select("ID=" & lvTemat.SelectedItems(0).Text)(0).Item("Status"), Boolean)
        .chkStatus.Enabled = True
        .dtpDataZajec.Value = CType(lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10), Date) 'dtpDataZajec.Value
        .dtpDataZajec.Enabled = False

        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        .IsRefreshMode = False
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdTemat As String = Me.lvTemat.SelectedItems(0).Text
          'Dim IdZastepstwo As String = Me.lvTemat.SelectedItems(0).SubItems(8).Text
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim cmd As MySqlCommand = DBA.CreateCommand(T.UpdateTopic)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?Nr", IIf(.chkStatus.Checked, 0, .nudNr.Value))
          cmd.Parameters.AddWithValue("?Tresc", .txtTemat.Text.Trim)
          cmd.Parameters.AddWithValue("?ID", IdTemat)
          cmd.Parameters.AddWithValue("?StatusLekcji", Not .chkStatus.Checked)
          cmd.ExecuteNonQuery()
          If Not .chkStatus.Checked Then
            If dtZastepstwo.Select("IdLekcja=" & lvTemat.SelectedItems(0).SubItems(9).Text & " AND Data='" & lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10) & "'").Length > 0 Then
              cmd.CommandText = T.UpdateSubstituteStatus
              cmd.Parameters.AddWithValue("?IdZastepstwo", dtZastepstwo.Select("IdLekcja=" & lvTemat.SelectedItems(0).SubItems(9).Text & " AND Data='" & lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10) & "'")(0).Item("IdZastepstwo"))
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
                cmdAbsent.Parameters.AddWithValue("?Data", CType(lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10), Date)) 'dtpDataZajec.Value.ToShortDateString)
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
                cmdLate.Parameters.AddWithValue("?Data", CType(lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10), Date)) 'dtpDataZajec.Value.ToShortDateString)
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
            If dtZastepstwo.Select("IdLekcja=" & lvTemat.SelectedItems(0).SubItems(9).Text & " AND Data='" & lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10) & "'").Length > 0 Then
              cmd.CommandText = T.UpdateSubstituteStatus
              cmd.Parameters.AddWithValue("?IdZastepstwo", dtZastepstwo.Select("IdLekcja=" & lvTemat.SelectedItems(0).SubItems(9).Text & " AND Data='" & lvTemat.SelectedItems(0).SubItems(3).Text.Substring(0, 10) & "'")(0).Item("IdZastepstwo"))
              cmd.Parameters.AddWithValue("?Status", 0)
              cmd.ExecuteNonQuery()
            End If
          End If

          MySQLTrans.Commit()

          FetchData()
          GetData()
          'dtpDataZajec_ValueChanged(Nothing, Nothing)
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
  Private Sub LoadObjectStaffItems(cb As ComboBox)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, T As New TematSQL
    cb.Items.Clear()
    Try
      R = DBA.GetReader(T.SelectObsadaByBelfer(CType(cbBelfer.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear))
      While R.Read()
        cb.Items.Add(New ObjectStaffComboItem(R.GetInt32("IdObsada"), R.GetString("Obsada"), R.GetInt32("Przedmiot"), R.GetString("Klasa"), R.GetBoolean("Virtual")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub

  Private Sub cmdEvent_Click(sender As Object, e As EventArgs) Handles cmdEvent.Click
    Dim dlgEvent As New dlgZdarzenie
    With dlgEvent
      .IsNewMode = True
      .Klasa = CType(cbObsada.SelectedItem, ObjectStaffComboItem).ClassID.ToString
      Dim CH As New CalcHelper
      If .dtpDataZajec.MaxDate < CH.StartDateOfSchoolYear(My.Settings.SchoolYear) Then
        .dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
        .dtpDataZajec.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      Else
        .dtpDataZajec.MinDate = Date.Now 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
        .dtpDataZajec.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      End If
      .FillGodzina()
      .ShowDialog()
    End With
  End Sub

End Class
Public Class ObjectStaffComboItem
  Public Property ID As Integer
  Public Property ObjectStaff As String
  Public Property ObjectID As Integer
  Public Property ClassID As String
  Public Property SchoolTeacherID As String
  Public Property IsVirtual As Boolean
  Public Overrides Function ToString() As String
    Return ObjectStaff
  End Function
  Sub New(ByVal IdObsada As Integer, ByVal Obsada As String, IdPrzedmiot As Integer, IdNauczyciel As String)
    ID = IdObsada
    ObjectStaff = Obsada
    ObjectID = IdPrzedmiot
    SchoolTeacherID = IdNauczyciel
  End Sub
  Sub New(ByVal IdObsada As Integer, ByVal Obsada As String, IdPrzedmiot As Integer, IdKlasa As String, Virtual As Boolean)
    ID = IdObsada
    ObjectStaff = Obsada
    ObjectID = IdPrzedmiot
    ClassID = IdKlasa
    IsVirtual = Virtual
  End Sub
  'Sub New(ByVal IdObsada As Integer, ByVal NazwaPrzedmiotu As String, IdKlasa As String, IdPrzedmiot As Integer, IdNauczyciel As String)
  '  ID = IdObsada
  '  ObjectStaff = NazwaPrzedmiotu
  '  ObjectID = IdPrzedmiot
  '  ClassID = IdKlasa
  '  SchoolTeacherID = IdNauczyciel
  'End Sub
End Class