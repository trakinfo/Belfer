Public Class frmPrzydzial
  'przejrzec calosc
  Private DS As DataSet, NextSchoolYear, PreviousSchoolYear As String  ', MaxPion As Byte
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PrzydzialUczniowToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PrzydzialUczniowToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData()
    Dim P As New PrzydzialSQL, DBA As New DataBaseAction, O As New ObsadaSQL
    DS = DBA.GetDataSet(P.SelectStudent(My.Settings.SchoolYear, My.Settings.IdSchool) & P.SelectStudent(NextSchoolYear, My.Settings.IdSchool) & P.SelectStudent(PreviousSchoolYear, My.Settings.IdSchool) & P.SelectStudentNoAssign & O.SelectClasses(My.Settings.IdSchool, "0"))
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmPrzydzial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvKlasaNew)
    ListViewConfig(lvKlasaNow)
    lblRecord.Text = ""
    lblRecord1.Text = ""
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    NextSchoolYear = String.Concat((CType(My.Settings.SchoolYear.Substring(0, 4), Integer) + 1).ToString, "/", (CType(My.Settings.SchoolYear.Substring(0, 4), Integer) + 2).ToString)
    PreviousSchoolYear = String.Concat((CType(My.Settings.SchoolYear.Substring(0, 4), Integer) - 1).ToString, "/", My.Settings.SchoolYear.Substring(0, 4))
    lblCurrentYear.Text = "Rok szkolny: " & My.Settings.SchoolYear
    lblNewYear.Text = "Rok szkolny: " & NextSchoolYear
    CalendarConfig()
    FetchData()
    FillKlasa(cbKlasaNow)
    FillKlasa(cbKlasaNew)

    Me.cmdMove.Enabled = False
    gpbZakres.Enabled = True
    gpbStatus.Enabled = True
    gpbOpcje.Enabled = True
    RefreshData(cbKlasaNow)
    RefreshData(cbKlasaNew)

  End Sub
  Private Sub LvNewItem(ByVal LV As ListView, ByVal Klasa As String, ByVal Tablica As Integer)
    Dim Students() As DataRow
    Students = DS.Tables(Tablica).Select("Klasa='" & Klasa & "'")
    Dim NG As New ListViewGroup(DS.Tables(4).Select("ID='" & Klasa & "'")(0).Item("Nazwa_Klasy").ToString)
    NG.HeaderAlignment = HorizontalAlignment.Left
    If Students.Count > 0 Then
      LV.Groups.Add(NG)
      For j As Integer = 0 To Students.GetUpperBound(0)
        Dim NewItem As New ListViewItem(Students(j).Item(0).ToString, NG)
        NewItem.UseItemStyleForSubItems = True
        NewItem.ForeColor = CType(IIf(CType(Students(j).Item(7), Boolean), Color.Green, Color.LightCoral), Color)
        NewItem.SubItems.Add(Students(j).Item(1).ToString)
        NewItem.SubItems.Add(Students(j).Item(2).ToString)
        LV.Items.Add(NewItem)
      Next

    End If
  End Sub
  Private Overloads Sub GetData(ByVal cb As ComboBox)
    Try
      If cb.Name = "cbKlasaNow" Then
        lvKlasaNow.Items.Clear()
        Me.lvKlasaNow.Groups.Clear()
      Else
        lvKlasaNew.Items.Clear()
        Me.lvKlasaNew.Groups.Clear()
      End If
      If rbWszystkieKlasy.Checked Then
        If cb.Name = "cbKlasaNow" Then
          'lvKlasaNow.Items.Clear()
          'Me.lvKlasaNow.Groups.Clear()
          For Each Item As CbItem In cb.Items
            LvNewItem(lvKlasaNow, Item.ID.ToString, 0)
          Next
          'For i As Integer = 0 To cbKlasaNow.Items.Count - 1
          '  LvNewItem(lvKlasaNow, cbKlasaNow.Items(i).ToString, 0)
          'Next
        Else
          'lvKlasaNew.Items.Clear()
          'Me.lvKlasaNew.Groups.Clear()
          If rbAssignNewYear.Checked Then
            For Each Item As CbItem In cb.Items
              LvNewItem(lvKlasaNew, Item.ID.ToString, 1)
            Next
            'For i As Integer = 0 To cbKlasaNew.Items.Count - 1
            '  LvNewItem(lvKlasaNew, cbKlasaNew.Items(i).ToString, 1)
            'Next
          ElseIf rbReverseAssign.Checked Then
            For Each Item As CbItem In cb.Items
              LvNewItem(lvKlasaNew, Item.ID.ToString, 2)
            Next
            'For i As Integer = 0 To cbKlasaNew.Items.Count - 1
            '  LvNewItem(lvKlasaNew, cbKlasaNew.Items(i).ToString, 2)
            'Next
          End If

        End If
      ElseIf rbWybranaKlasa.Checked Then
        If cb.SelectedItem IsNot Nothing Then
          If cb.Name = "cbKlasaNow" Then
            'lvKlasaNow.Items.Clear()
            'Me.lvKlasaNow.Groups.Clear()
            LvNewItem(lvKlasaNow, CType(cb.SelectedItem, CbItem).ID.ToString, 0)
          Else
            'lvKlasaNew.Items.Clear()
            'Me.lvKlasaNew.Groups.Clear()
            If rbAssignNewYear.Checked Then
              LvNewItem(lvKlasaNew, CType(cb.SelectedItem, CbItem).ID.ToString, 1)
            ElseIf rbReverseAssign.Checked Then
              LvNewItem(lvKlasaNew, CType(cb.SelectedItem, CbItem).ID.ToString, 2)
            Else
              LvNewItem(lvKlasaNew, CType(cb.SelectedItem, CbItem).ID.ToString, 0)
            End If
          End If
        End If

      Else
        'lvKlasaNow.Items.Clear()
        'Me.lvKlasaNow.Groups.Clear()

        If cb.Name = "cbKlasaNow" Then
          For j As Integer = 0 To DS.Tables(3).Select().GetUpperBound(0)
            Dim NewItem As New ListViewItem(DS.Tables(3).Select()(j).Item(0).ToString)
            NewItem.SubItems.Add(DS.Tables(3).Select()(j).Item(1).ToString)
            NewItem.SubItems.Add(DS.Tables(3).Select()(j).Item(2).ToString)
            lvKlasaNow.Items.Add(NewItem)
          Next
        Else
          If cb.SelectedItem IsNot Nothing Then
            If rbAssignNewYear.Checked Then
              LvNewItem(lvKlasaNew, CType(cb.SelectedItem, CbItem).ID.ToString, 1)
            ElseIf rbReverseAssign.Checked Then
              LvNewItem(lvKlasaNew, CType(cb.SelectedItem, CbItem).ID.ToString, 2)
            Else
              LvNewItem(lvKlasaNew, CType(cb.SelectedItem, CbItem).ID.ToString, 0)
            End If
          End If
        End If
      End If

      If cb.Name = "cbKlasaNow" Then
        lblRecord.Text = "0 z " & lvKlasaNow.Items.Count
        lvKlasaNow.Columns(1).Width = CInt(IIf(lvKlasaNow.Items.Count > 20, 285, 305))
        lvKlasaNow.Enabled = CBool(IIf(lvKlasaNow.Items.Count > 0, True, False))
      Else
        lblRecord1.Text = "0 z " & lvKlasaNew.Items.Count
        lvKlasaNew.Columns(1).Width = CInt(IIf(lvKlasaNew.Items.Count > 20, 285, 305))
        lvKlasaNew.Enabled = CBool(IIf(lvKlasaNew.Items.Count > 0, True, False))
      End If
    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try
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
      .Columns.Add("Nazwisko i imię", 309, HorizontalAlignment.Left)
      .Columns.Add("Klasa", 0, HorizontalAlignment.Center)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Sub SetYearLabel()
    lblCurrentYear.Text = "Rok szkolny: " & My.Settings.SchoolYear
    If rbAssignNewYear.Checked Then
      lblNewYear.Text = "Rok szkolny: " & NextSchoolYear
    ElseIf rbReverseAssign.Checked Then
      lblNewYear.Text = "Rok szkolny: " & PreviousSchoolYear
    Else
      lblNewYear.Text = lblCurrentYear.Text
    End If
  End Sub

  Private Overloads Sub FillKlasa(ByVal cb As ComboBox)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, O As New ObsadaSQL 'P As New PrzydzialSQL
    'FCB.AddComboBoxComplexItems(cb, P.SelectClass(My.Settings.IdSchool))
    FCB.AddComboBoxComplexItems(cb, O.SelectClasses(My.Settings.IdSchool, "0"))
    'cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Overloads Sub FillKlasa(ByVal cb As ComboBox, ByVal Pion As String)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, P As New PrzydzialSQL
    FCB.AddComboBoxComplexItems(cb, P.SelectClass(My.Settings.IdSchool, Pion, "0"))
  End Sub
  Private Overloads Sub FillKlasa(ByVal cb As ComboBox, ByVal Pion1 As String, ByVal Pion2 As String)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, P As New PrzydzialSQL
    FCB.AddComboBoxComplexItems(cb, P.SelectClass(My.Settings.IdSchool, Pion1, Pion2, "0"))
  End Sub

  Private Sub RefreshData(ByVal cb As ComboBox)
    cmdMove.Enabled = False 'CType(IIf(lvKlasaNow.SelectedItems.Count > 0, True, False), Boolean)
    cmdDelete.Enabled = False 'CType(IIf(lvKlasaNow.SelectedItems.Count > 0, True, False), Boolean)
    ClearDetails()
    GetData(cb)
  End Sub
  Private Sub ClearDetails()
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ByVal ID As String, ByVal lv As ListView)
    Dim FoundRow() As DataRow
    Try
      If lv.Name = "lvKlasaNow" Then
        lblRecord.Text = lv.SelectedItems(0).Index + 1 & " z " & lv.Items.Count
        FoundRow = DS.Tables(CType(IIf(rbNoAssign.Checked, 3, 0), Integer)).Select("IdUczen=" & ID)

      Else
        lblRecord1.Text = lv.SelectedItems(0).Index + 1 & " z " & lv.Items.Count
        If rbAssignNewYear.Checked Then
          FoundRow = DS.Tables(1).Select("IdUczen=" & ID)
        ElseIf rbReverseAssign.Checked Then
          FoundRow = DS.Tables(2).Select("IdUczen=" & ID)
        Else
          FoundRow = DS.Tables(0).Select("IdUczen=" & ID)

        End If
      End If

      With FoundRow(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(3).ToString
        lblIP.Text = .Item(4).ToString
        lblData.Text = .Item(5).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub cbKlasaNow_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasaNow.SelectedIndexChanged, cbKlasaNew.SelectedIndexChanged
    If CType(sender, ComboBox).Name = "cbKlasaNow" Then
      If cbKlasaNew.Text.Length = 0 OrElse DS.Tables(4).Select("ID=" & CType(cbKlasaNow.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1) <> DS.Tables(4).Select("ID=" & CType(cbKlasaNew.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1) Then
        lvKlasaNew.Items.Clear()
        'Tu są błędy
        If rbChangeAssign.Checked Or rbReverseAssign.Checked Then
          FillKlasa(cbKlasaNew, DS.Tables(4).Select("ID=" & CType(cbKlasaNow.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1))
        Else
          FillKlasa(cbKlasaNew, DS.Tables(4).Select("ID=" & CType(cbKlasaNow.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1), (CType(DS.Tables(4).Select("ID=" & CType(cbKlasaNow.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer) + 1).ToString)

        End If

      End If
      cbKlasaNew.Enabled = True

    End If
    RefreshData(CType(sender, ComboBox))
  End Sub

  Private Sub lvKlasaObecna_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvKlasaNow.ItemSelectionChanged, lvKlasaNew.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(e.Item.Text, CType(sender, ListView))
      'If GlobalValues.gblEnableAdminCommands OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString Then
      If CType(sender, ListView).Name = "lvKlasaNow" Then
        If rbWszystkieKlasy.Checked Then
          Me.cmdMove.Enabled = True
        Else
          If rbChangeAssign.Checked Then
            Me.cmdMove.Enabled = CType(IIf(cbKlasaNew.SelectedItem IsNot Nothing, IIf(cbKlasaNow.Text = cbKlasaNew.Text, False, True), False), Boolean)
          Else
            Me.cmdMove.Enabled = CType(IIf(cbKlasaNew.SelectedItem IsNot Nothing, True, False), Boolean)

          End If
        End If
        cmdDelete.Enabled = True
      Else
        Me.cmdMove.Enabled = False
        cmdDelete.Enabled = False
      End If
      'End If

    Else
      ClearDetails()
      If CType(sender, ListView).Name = "lvKlasaNow" Then
        lblRecord.Text = "0 z " & CType(sender, ListView).Items.Count
      Else
        lblRecord1.Text = "0 z " & CType(sender, ListView).Items.Count
      End If
      Me.cmdMove.Enabled = False
      cmdDelete.Enabled = False
    End If
  End Sub

  Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMove.Click
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try

      If rbSelected.Checked Then
        If rbWszystkieKlasy.Checked Then
          For Each Item As ListViewItem In Me.lvKlasaNow.SelectedItems
            DoAssignment(Item, MySQLTrans)
          Next
        ElseIf rbWybranaKlasa.Checked Then
          For Each Item As ListViewItem In Me.lvKlasaNow.SelectedItems
            DoAssignmentForSelectedClass(Item, MySQLTrans)
          Next
        Else
          For Each Item As ListViewItem In Me.lvKlasaNow.SelectedItems
            DoAssignmentForNonAssign(Item, MySQLTrans)
          Next
        End If
      Else
        If rbWszystkieKlasy.Checked Then
          For Each Item As ListViewItem In Me.lvKlasaNow.Items
            If Item.Selected = False Then DoAssignment(Item, MySQLTrans)
          Next
        ElseIf rbWybranaKlasa.Checked Then
          For Each Item As ListViewItem In Me.lvKlasaNow.Items
            If Item.Selected = False Then DoAssignmentForSelectedClass(Item, MySQLTrans)
          Next
        Else
          For Each Item As ListViewItem In Me.lvKlasaNow.Items
            If Item.Selected = False Then DoAssignmentForNonAssign(Item, MySQLTrans)
          Next
        End If
      End If

      MySQLTrans.Commit()
      FetchData()

      RefreshData(cbKlasaNow)
      RefreshData(cbKlasaNew)

    Catch Myex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show("Rodzaj błędu:" & vbNewLine & Myex.Message & vbNewLine & "Wszystkie operacje na bazie danych zostały cofnięte.")
    End Try
  End Sub
  Private Sub DoAssignmentForNonAssign(ByVal Item As ListViewItem, ByVal MySQLTrans As MySqlTransaction)
    Dim DBA As New DataBaseAction, P As New PrzydzialSQL
    Dim cmd As MySqlCommand
    If rbAssignNewYear.Checked Then
      cmd = DBA.CreateCommand(P.InsertPrzydzial())
      cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
      cmd.Parameters.AddWithValue("?Klasa", CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString)
      cmd.Parameters.AddWithValue("?RokSzkolny", NextSchoolYear)
      cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)
      cmd.Transaction = MySQLTrans
      cmd.ExecuteNonQuery()
    ElseIf rbReverseAssign.Checked Then
      cmd = DBA.CreateCommand(P.InsertPrzydzial())
      cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
      cmd.Parameters.AddWithValue("?Klasa", CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString)
      cmd.Parameters.AddWithValue("?RokSzkolny", PreviousSchoolYear)
      cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

      cmd.Transaction = MySQLTrans
      cmd.ExecuteNonQuery()
    Else
      cmd = DBA.CreateCommand(P.InsertPrzydzial())
      cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
      cmd.Parameters.AddWithValue("?Klasa", CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString)
      cmd.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
      cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

      cmd.Transaction = MySQLTrans
      cmd.ExecuteNonQuery()
    End If
  End Sub
  Private Sub DoAssignmentForSelectedClass(ByVal Item As ListViewItem, ByVal MySQLTrans As MySqlTransaction)
    Dim DBA As New DataBaseAction, P As New PrzydzialSQL
    Dim cmd As MySqlCommand
    If rbAssignNewYear.Checked Then
      If Not CType(DS.Tables(0).Select("IdUczen=" & Item.Text)(0).Item(7), Boolean) Then
        If DS.Tables(4).Select("ID =" & CType(cbKlasaNow.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1) = DS.Tables(4).Select("ID =" & CType(cbKlasaNew.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1) Then
          'If cbKlasaNow.Text.Substring(0, 1) = cbKlasaNew.Text.Substring(0, 1) Then
          If Not DBA.ComputeRecords(P.CountAssignmentByPion(Item.Text, My.Settings.IdSchool, CType(DS.Tables(4).Select("ID =" & CType(cbKlasaNew.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer), NextSchoolYear)) > 0 Then
            cmd = DBA.CreateCommand(P.InsertPrzydzial())
            cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
            cmd.Parameters.AddWithValue("?Klasa", CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString)
            cmd.Parameters.AddWithValue("?RokSzkolny", NextSchoolYear)
            cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

            cmd.Transaction = MySQLTrans
            cmd.ExecuteNonQuery()
          Else
            MessageBox.Show("Uczeń " & Item.SubItems(1).Text & " posiada już przydział na rok szkolny " & NextSchoolYear)
          End If
        Else
          MessageBox.Show("Nie można przydzielić ucznia niepromowanego do klasy programowo wyższej.")
        End If

      Else
        If DS.Tables(4).Select("ID =" & CType(cbKlasaNow.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1) <> DS.Tables(4).Select("ID =" & CType(cbKlasaNew.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1) Then
          If Not DBA.ComputeRecords(P.CountAssignmentByPion(Item.Text, My.Settings.IdSchool, CType(DS.Tables(4).Select("ID =" & CType(cbKlasaNew.SelectedItem, CbItem).ID)(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer), NextSchoolYear)) > 0 Then
            cmd = DBA.CreateCommand(P.InsertPrzydzial())
            cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
            cmd.Parameters.AddWithValue("?Klasa", CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString)
            cmd.Parameters.AddWithValue("?RokSzkolny", NextSchoolYear)
            cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

            cmd.Transaction = MySQLTrans
            cmd.ExecuteNonQuery()
          Else
            MessageBox.Show("Uczeń " & Item.SubItems(1).Text & " posiada już przydział na rok szkolny " & NextSchoolYear)
          End If
        Else
          MessageBox.Show("Nie można przydzielić ucznia promowanego do klasy równoległej.")
        End If
      End If
    ElseIf rbReverseAssign.Checked Then
      If Not DBA.ComputeRecords(P.CountAssignmentBySchool(Item.Text, My.Settings.IdSchool, PreviousSchoolYear)) > 0 Then
        cmd = DBA.CreateCommand(P.InsertPrzydzial())
        cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
        cmd.Parameters.AddWithValue("?Klasa", CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString)
        cmd.Parameters.AddWithValue("?RokSzkolny", PreviousSchoolYear)
        cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

        cmd.Transaction = MySQLTrans
        cmd.ExecuteNonQuery()
      Else
        MessageBox.Show("Uczeń " & Item.SubItems(1).Text & " posiada już przydział na rok szkolny " & PreviousSchoolYear)
      End If
    Else
      If Not DBA.ComputeRecords(P.CountAssignmentBySchool(Item.Text, My.Settings.IdSchool, NextSchoolYear)) > 0 Then
        cmd = DBA.CreateCommand(P.UpdatePrzydzial(Item.Text, My.Settings.SchoolYear))
        cmd.Parameters.AddWithValue("?DataDeaktywacji", dtData.Value)
        cmd.Transaction = MySQLTrans
        cmd.ExecuteNonQuery()
        If DBA.ComputeRecords(P.CountAssignmentByClass(Item.Text, CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)) > 0 Then
          cmd.CommandText = P.UpdatePrzydzial(Item.Text, CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)
          'cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)
          'cmd.Parameters.AddWithValue("?DataReaktywacji", DBNull.Value)
        Else
          cmd.CommandText = P.InsertPrzydzial()
          cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
          cmd.Parameters.AddWithValue("?Klasa", CType(cbKlasaNew.SelectedItem, CbItem).ID.ToString)
          cmd.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
          cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)
        End If
        cmd.ExecuteNonQuery()
      Else
        MessageBox.Show("Nie można zmienić przydziału ucznia " & Item.SubItems(1).Text & ", ponieważ posiada on już przydział na rok szkolny " & NextSchoolYear)
      End If
    End If
  End Sub
  Private Sub DoAssignment(ByVal Item As ListViewItem, ByVal MySQLTrans As MySqlTransaction)
    Dim DBA As New DataBaseAction, P As New PrzydzialSQL
    Dim cmd As MySqlCommand = Nothing
    If rbAssignNewYear.Checked Then
      If Not CType(DS.Tables(0).Select("IdUczen=" & Item.Text)(0).Item(7), Boolean) Then
        If Not DBA.ComputeRecords(P.CountAssignmentByPion(Item.Text, My.Settings.IdSchool, CType(DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer), NextSchoolYear)) > 0 Then
          cmd = DBA.CreateCommand(P.InsertPrzydzial())
          cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
          cmd.Parameters.AddWithValue("?Klasa", DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("ID").ToString)
          cmd.Parameters.AddWithValue("?RokSzkolny", NextSchoolYear)
          cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Else
          MessageBox.Show("Uczeń " & Item.SubItems(1).Text & " posiada już przydział na rok szkolny " & NextSchoolYear)
        End If
      Else
        If Not DBA.ComputeRecords(P.CountAssignmentByPion(Item.Text, My.Settings.IdSchool, CType(DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer) + 1, NextSchoolYear)) > 0 Then
          cmd = DBA.CreateCommand(P.InsertPrzydzial())
          'cmd = DBA.CreateCommand(P.InsertPrzydzial(Item.Text, String.Concat((CType(Item.SubItems(2).Text.Substring(0, 1), Integer) + 1).ToString, Item.SubItems(2).Text.Substring(1, 1)), (Me.nudStartYear.Value + 1).ToString & "/" & (Me.nudEndYear.Value + 1).ToString))
          cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
          cmd.Parameters.AddWithValue("?Klasa", DS.Tables(4).Select("Kod_Klasy='" & String.Concat((CType(DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer) + 1).ToString, DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(1, 1)) & "'")(0).Item("ID").ToString)
          cmd.Parameters.AddWithValue("?RokSzkolny", NextSchoolYear)
          cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Else
          MessageBox.Show("Uczeń " & Item.SubItems(1).Text & " posiada już przydział na rok szkolny " & NextSchoolYear)
        End If
        'End If
      End If
    ElseIf rbReverseAssign.Checked Then
      If (Not DBA.ComputeRecords(P.CountAssignmentByPion(Item.Text, My.Settings.IdSchool, CType(DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer) - 1, PreviousSchoolYear)) > 0) Or (Not DBA.ComputeRecords(P.CountAssignmentByPion(Item.Text, My.Settings.IdSchool, CType(DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer), PreviousSchoolYear)) > 0) Then

        cmd = DBA.CreateCommand(P.InsertPrzydzial())
        cmd.Parameters.AddWithValue("?IdUczen", Item.Text)
        cmd.Parameters.AddWithValue("?Klasa", DS.Tables(4).Select("Kod_Klasy='" & String.Concat((CType(DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer) - 1).ToString, DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("Kod_Klasy").ToString.Substring(1, 1), "'"))(0).Item("ID").ToString)
        cmd.Parameters.AddWithValue("?RokSzkolny", PreviousSchoolYear)
        cmd.Parameters.AddWithValue("?DataAktywacji", dtData.Value)

        cmd.Transaction = MySQLTrans
        cmd.ExecuteNonQuery()
      Else
        MessageBox.Show("Uczeń " & Item.SubItems(1).Text & " posiada już przydział na rok szkolny " & PreviousSchoolYear)
      End If
    End If
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", "Belfer .NET", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, P As New PrzydzialSQL
      Try
        For Each Item As ListViewItem In lvKlasaNow.SelectedItems
          DBA.ApplyChanges(P.DeletePrzydzial(DS.Tables(0).Select("IdUczen=" & Item.Text & " AND Klasa=" & DS.Tables(4).Select("Nazwa_Klasy='" & Item.SubItems(2).Text & "'")(0).Item("ID").ToString)(0).Item("IdPrzydzial").ToString))
        Next
        FetchData()
        RefreshData(cbKlasaNow)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub
  Private Sub rbWybranaKlasa_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbWybranaKlasa.CheckedChanged, rbWszystkieKlasy.CheckedChanged, rbNoAssign.CheckedChanged
    If Not Me.Created Then Exit Sub
    If CType(sender, RadioButton).Checked Then
      cbKlasaNow.Enabled = CBool(IIf(cbKlasaNow.Items.Count > 0 AndAlso rbWybranaKlasa.Checked, True, False))
      If cbKlasaNew.Items.Count > 0 Then
        cbKlasaNew.Enabled = CBool(IIf(rbWszystkieKlasy.Checked, False, True))
      Else
        cbKlasaNew.Enabled = False
      End If
      rbChangeAssign.Enabled = CBool(IIf(rbWszystkieKlasy.Checked, False, True))
      If rbNoAssign.Checked Then FillKlasa(cbKlasaNew)
      If rbWszystkieKlasy.Checked AndAlso rbChangeAssign.Checked Then rbAssignNewYear.Checked = True
      RefreshData(cbKlasaNow)
      If rbNoAssign.Checked = False Then RefreshData(cbKlasaNew)
    End If
  End Sub

  Private Sub rbChangeAssign_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbChangeAssign.CheckedChanged, rbAssignNewYear.CheckedChanged, rbReverseAssign.CheckedChanged
    If Not Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    CalendarConfig()
    SetYearLabel()
    If CType(sender, RadioButton).Name = "rbChangeAssign" Or CType(sender, RadioButton).Name = "rbReverseAssign" Then
      If cbKlasaNow.SelectedItem IsNot Nothing Then FillKlasa(cbKlasaNew, DS.Tables(4).Select("ID=" & CType(cbKlasaNow.SelectedItem, CbItem).ID.ToString)(0).Item("Kod_Klasy").ToString.Substring(0, 1))
      'If cbKlasaNow.SelectedItem IsNot Nothing Then FillKlasa(cbKlasaNew, cbKlasaNow.Text.Substring(0, 1))
    Else
      If cbKlasaNow.SelectedItem IsNot Nothing Then FillKlasa(cbKlasaNew, DS.Tables(4).Select("ID=" & CType(cbKlasaNow.SelectedItem, CbItem).ID.ToString)(0).Item("Kod_Klasy").ToString.Substring(0, 1), (CType(DS.Tables(4).Select("ID=" & CType(cbKlasaNow.SelectedItem, CbItem).ID.ToString)(0).Item("Kod_Klasy").ToString.Substring(0, 1), Integer) + 1).ToString)
    End If
    RefreshData(cbKlasaNew)
  End Sub
  Private Sub CalendarConfig()
    Dim CH As New CalcHelper
    dtData.MaxDate = CH.EndDateOfSchoolYear(NextSchoolYear)
    dtData.MinDate = CH.StartDateOfSchoolYear(PreviousSchoolYear)
    If rbChangeAssign.Checked Then
      dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      dtData.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      dtData.Value = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
    ElseIf rbAssignNewYear.Checked Then
      dtData.MinDate = CH.StartDateOfSchoolYear(NextSchoolYear)
      dtData.MaxDate = CH.EndDateOfSchoolYear(NextSchoolYear)
      dtData.Value = dtData.MinDate
    Else
      dtData.MinDate = CH.StartDateOfSchoolYear(PreviousSchoolYear)
      dtData.MaxDate = CH.EndDateOfSchoolYear(PreviousSchoolYear)
      dtData.Value = dtData.MinDate
    End If
  End Sub
  Private Sub rbUnselected_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUnselected.CheckedChanged, rbSelected.CheckedChanged
    If Not Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    If CType(sender, RadioButton).Name = "rbUnselected" AndAlso lvKlasaNow.Items.Count > 0 Then
      cmdMove.Enabled = True
    Else
      If lvKlasaNow.SelectedItems.Count > 0 Then
        cmdMove.Enabled = True
      Else
        cmdMove.Enabled = False
      End If
    End If
  End Sub

End Class


