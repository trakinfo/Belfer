Public Class frmObsada

  Public ObsadaFilter As String, Filter As String = ""
  Private DT As DataTable
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ObsadaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ObsadaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData()
    Dim O As New ObsadaSQL, DBA As New DataBaseAction
    If ObsadaFilter = "Klasa" Then
      DT = DBA.GetDataTable(O.SelectObsadaByKlasa(My.Settings.IdSchool, My.Settings.SchoolYear, CType(chkVirtual.CheckState, Byte).ToString))
    ElseIf ObsadaFilter = "Belfer" Then
      DT = DBA.GetDataTable(O.SelectObsadByNauczyciel(My.Settings.IdSchool, My.Settings.SchoolYear, CType(chkVirtual.CheckState, Byte).ToString))
    Else
      DT = DBA.GetDataTable(O.SelectObsadByPrzedmiot(My.Settings.IdSchool, My.Settings.SchoolYear, CType(chkVirtual.CheckState, Byte).ToString))
    End If
  End Sub
  Private Overloads Sub GetData(Filter As String)
    Try
      lvObsada.Items.Clear()
      ClearDetails()
      Dim Suma As Decimal
      'For j As Integer = 0 To DT.Select(Filter).GetUpperBound(0)
      For Each R As DataRow In DT.Select(Filter)
        Dim NewItem As New ListViewItem(R.Item(0).ToString)
        NewItem.UseItemStyleForSubItems = True
        NewItem.SubItems.Add(R.Item(1).ToString)
        NewItem.SubItems.Add(R.Item(2).ToString)
        NewItem.SubItems.Add(R.Item(3).ToString)
        NewItem.SubItems.Add(IIf(R.Item(4).ToString = "1", "Tak", "Nie").ToString)
        'NewItem.SubItems.Add(CType(DT.Select(Filter)(j).Item("Status"), GlobalValues.Status).ToString)
        If IsDBNull(R.Item("DataAktywacji")) Then
          NewItem.SubItems.Add("")
        Else
          NewItem.SubItems.Add(CType(R.Item("DataAktywacji"), Date).ToShortDateString)
        End If
        If IsDBNull(R.Item("DataDeaktywacji")) Then
          NewItem.SubItems.Add("")
        Else
          NewItem.SubItems.Add(CType(R.Item("DataDeaktywacji"), Date).ToShortDateString)
          NewItem.ForeColor = Color.DarkGray
        End If
        NewItem.SubItems.Add(R.Item("LiczbaGodzin").ToString)
        Suma += CType(R.Item("LiczbaGodzin"), Decimal)
        lvObsada.Items.Add(NewItem)
      Next
      'Next
      lblRecord.Text = "0 z " & lvObsada.Items.Count
      lblSuma.Text = Suma.ToString
      lvObsada.Columns(1).Width = CInt(IIf(lvObsada.Items.Count > 21, 181, 200))
      lvObsada.Enabled = CBool(IIf(lvObsada.Items.Count > 0, True, False))

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmObsadaWgKlas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvObsada)
    lblRecord.Text = ""
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    cmdAddNew.Enabled = False
    cmdEdit.Enabled = False
    cmdDelete.Enabled = False
    With lvObsada
      .Items.Clear()
      .Enabled = False
      If ObsadaFilter = "Klasa" Then
        .Columns(1).Text = "Nauczyciel"
        .Columns(2).Text = "Przedmiot"
      ElseIf ObsadaFilter = "Belfer" Then
        .Columns(1).Text = "Klasa"
        .Columns(2).Text = "Przedmiot"
      Else
        .Columns(1).Text = "Klasa"
        .Columns(2).Text = "Nauczyciel"
      End If
    End With

    FetchData()
    FillObsadaFilter(cbObsadaFilter)
    Dim SH As New SeekHelper
    If ObsadaFilter = "Klasa" Then
      If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbObsadaFilter, CType(My.Settings.ClassName, Integer))
    ElseIf ObsadaFilter = "Belfer" Then
      If My.Settings.IdBelfer.Length > 0 Then SH.FindComboItem(Me.cbObsadaFilter, CType(My.Settings.IdBelfer, Integer))
    Else
      If My.Settings.ObjectName.Length > 0 Then SH.FindComboItem(Me.cbObsadaFilter, CType(My.Settings.ObjectName, Integer))

    End If
  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      '.Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nauczyciel", 200, HorizontalAlignment.Left)
      .Columns.Add("Przedmiot", 205, HorizontalAlignment.Left)
      .Columns.Add("Typ", 50, HorizontalAlignment.Center)
      .Columns.Add("Do średniej", 80, HorizontalAlignment.Center)
      '.Columns.Add("Status", 70, HorizontalAlignment.Center)
      .Columns.Add("Data aktywacji", 100, HorizontalAlignment.Center)
      .Columns.Add("Data deaktywacji", 100, HorizontalAlignment.Center)
      .Columns.Add("Liczba godzin", 80, HorizontalAlignment.Center)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
 
  Private Sub FillObsadaFilter(ByVal cb As ComboBox)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, O As New ObsadaSQL
    'Tutaj zmienić na nauczyciela
    If ObsadaFilter = "Klasa" Then
      'FCB.AddComboBoxComplexItems(cb, O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, CType(chkVirtual.CheckState, Byte).ToString))
      FCB.AddComboBoxComplexItems(cb, O.SelectClasses(My.Settings.IdSchool, CType(chkVirtual.CheckState, Byte).ToString))
    ElseIf ObsadaFilter = "Belfer" Then
      FCB.AddComboBoxComplexItems(cb, O.SelectBelfer(My.Settings.IdSchool))
    Else
      FCB.AddComboBoxComplexItems(cb, O.SelectPrzedmiot(My.Settings.IdSchool))
    End If
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvObsada.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ByVal IdObsada As String)
    'Dim FoundRow() As DataRow
    Try
      lblRecord.Text = lvObsada.SelectedItems(0).Index + 1 & " z " & lvObsada.Items.Count
      With DT.Select("IdObsada='" & IdObsada & "'")(0) 'FoundRow(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(5).ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbObsadaFilter.SelectionChangeCommitted
    'RaiseEvent ClassChanged(CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString, CType(cbObsadaFilter.SelectedItem, CbItem).Nazwa)
    If ObsadaFilter = "Klasa" Then
      My.Settings.ClassName = CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString
    ElseIf ObsadaFilter = "Belfer" Then
      My.Settings.IdBelfer = CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString
    Else
      My.Settings.ObjectName = CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString
    End If
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbObsadaFilter.SelectedIndexChanged

    'Me.FetchData()

    If ObsadaFilter = "Klasa" Then
      Filter = "Klasa="
    ElseIf ObsadaFilter = "Belfer" Then
      Filter = "IdNauczyciel="
    Else
      Filter = "IdPrzedmiot="
    End If
    Filter += CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString
    GetData(Filter)
    cmdAddNew.Enabled = True
    EnableButtons(False)
    cmdDeaktywacja.Enabled = False
  End Sub
  Private Sub EnableButtons(Value As Boolean)
    'Me.cmdAddNew.Enabled = CType(IIf(My.Application.OpenForms.OfType(Of dlgObsada)().Any(), False, True), Boolean)
    cmdEdit.Enabled = Value
    Me.cmdDelete.Enabled = Value
  End Sub

  Private Sub lvObsada_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvObsada.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then
        EnableButtons(True)
        If e.Item.SubItems(6).Text = "" Then cmdDeaktywacja.Enabled = True
      End If
    Else
      ClearDetails()
      'lblRecord.Text = "0 z " & CType(sender, ListView).Items.Count
      EnableButtons(False)
      cmdDeaktywacja.Enabled = False
    End If
  End Sub
  Private Sub lvWychowawca_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvObsada.DoubleClick
    EditData()
  End Sub
  Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    'sprawdzić dodawanie i edycję wg przedmiotu
    Try
      Dim dlgAddNew As New dlgObsada
      With dlgAddNew
        .IsNewMode = True
        .ObsadaFilter = ObsadaFilter
        Dim OH As New OptionHolder
        .ColNumber = OH.ColNumber
        .Virtual = chkVirtual.Checked 'CType(chkVirtual.CheckState, Boolean)
        If chkVirtual.Checked Then
          .lblStudent.Visible = True
          .cmdStudent.Visible = True
          .pnlObsada.Enabled = False
          .pnlObsada.Location = New Point(.pnlObsada.Location.X, 39)
        Else
          .lblStudent.Visible = False
          .cmdStudent.Visible = False
          .pnlObsada.Enabled = True
          .pnlObsada.Location = New Point(.pnlObsada.Location.X, 0)
          .Height = .Height - 39
        End If
        If ObsadaFilter = "Klasa" Then
          '.lblObsadaFilterContent.Text = "Klasa: "
          '.chkKolumna.Visible = False
          .lbl2.Text = "Nauczyciel"
          .FillCombo(.cb1, "Przedmiot")
          .FillCombo(.cb2, "Belfer")
          .cb1.Name = "cbPrzedmiot"
          .cb2.Name = "cbBelfer"
        ElseIf ObsadaFilter = "Belfer" Then
          '.lblObsadaFilterContent.Text = "Nauczyciel: "
          '.chkKolumna.Visible = True
          .lbl2.Text = "Klasa"
          .FillCombo(.cb1, "Przedmiot")
          .FillCombo(.cb2, "Klasa")
          .cb1.Name = "cbPrzedmiot"
          .cb2.Name = "cbKlasa"
        Else
          '.lblObsadaFilterContent.Text = "Przedmiot: "
          '.chkKolumna.Visible = True
          .lbl1.Text = "Klasa"
          .lbl2.Text = "Nauczyciel"
          .FillCombo(.cb1, "Klasa")
          .FillCombo(.cb2, "Belfer")
          .cb1.Name = "cbKlasa"
          .cb2.Name = "cbBelfer"
        End If
        Dim CH As New CalcHelper
        .dtDataAktywacji.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
        .dtDataAktywacji.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
        If Date.Now >= .dtDataAktywacji.MinDate AndAlso Date.Now <= .dtDataAktywacji.MaxDate Then
          .dtDataAktywacji.Value = Date.Now
        Else
          .dtDataAktywacji.Value = .dtDataAktywacji.MinDate
        End If
        .FillKategoria()
        .IdObsadaFilter = CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString
        '.lblObsadaFilterContent.Text += CType(cbObsadaFilter.SelectedItem, CbItem).Nazwa
        .Text = "Dodawanie obsady przedmiotu"
        .MaximizeBox = False
        .StartPosition = FormStartPosition.CenterScreen

        AddHandler .NewObsadaAdded, AddressOf NewObsadaAdded
        Me.cmdAddNew.Enabled = False
        .ShowDialog()
        cmdAddNew.Enabled = True
      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
    Finally

    End Try
  End Sub
  'Private Sub EnableCmdAddNew(ByVal sender As Object, ByVal e As EventArgs)
  '  Me.cmdAddNew.Enabled = True
  'End Sub
  Private Sub NewObsadaAdded(ByVal InsertedObsada As String)
    Me.FetchData()
    GetData(Filter)
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvObsada, InsertedObsada)
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgObsada, O As New ObsadaSQL
      'Dim FCB As New FillComboBox
      With dlgEdit
        .OK_Button.Text = "&Zapisz"
        .IsNewMode = False
        .IdObsadaFilter = CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString
        '.lblObsadaFilterContent.Text = CType(cbObsadaFilter.SelectedItem, CbItem).Nazwa
        .Virtual = chkVirtual.Checked 'CType(chkVirtual.CheckState, Boolean)
        'If chkVirtual.Checked Then
        '  .lblStudent.Visible = True
        '  .cmdStudent.Visible = True
        '  .cmdStudent.Enabled = False
        '  .pnlObsada.Enabled = True
        '  .pnlObsada.Location = New Point(.pnlObsada.Location.X, 39)
        'Else
        .lblStudent.Visible = False
        .cmdStudent.Visible = False
        .pnlObsada.Enabled = True
        .pnlObsada.Location = New Point(.pnlObsada.Location.X, 0)
        .Height = .Height - 39
        'End If
        Dim SH As New SeekHelper
        If ObsadaFilter = "Klasa" Then
          '.lblObsadaFilter.Text = "Klasa"
          '.chkKolumna.Visible = False
          .lbl2.Text = "Nauczyciel"
          .FillCombo(.cb1, "Przedmiot")
          .FillCombo(.cb2, "Belfer")
          SH.FindComboItem(.cb1, CType(DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("IdPrzedmiot"), Integer))
          .cb1.Enabled = False
          SH.FindComboItem(.cb2, CType(DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("IdNauczyciel"), Integer))
        ElseIf ObsadaFilter = "Belfer" Then
          '.lblObsadaFilter.Text = "Nauczyciel"
          '.chkKolumna.Visible = True
          .lbl2.Text = "Klasa"
          .FillCombo(.cb1, "Przedmiot")
          .FillCombo(.cb2, "Klasa")
          SH.FindComboItem(.cb1, CType(DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("IdPrzedmiot"), Integer))
          SH.FindComboItem(.cb2, CType(DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("Klasa"), Integer))
          .cb1.Enabled = False
        Else
          '.lblObsadaFilter.Text = "Przedmiot"
          '.chkKolumna.Visible = True
          .lbl1.Text = "Klasa"
          .lbl2.Text = "Nauczyciel"
          .FillCombo(.cb1, "Klasa")
          .FillCombo(.cb2, "Belfer")
          SH.FindComboItem(.cb1, CType(DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("Klasa"), Integer))
          .cb1.Enabled = False
          SH.FindComboItem(.cb2, CType(DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("IdNauczyciel"), Integer))
        End If
        Dim CH As New CalcHelper
        .dtDataAktywacji.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
        .dtDataAktywacji.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
        .dtDataAktywacji.Value = CType(lvObsada.SelectedItems(0).SubItems(5).Text, Date)
        .nudLiczbaGodzin.Value = CType(lvObsada.SelectedItems(0).SubItems(7).Text, Integer)
        .FillKategoria()
        .Text = "Edycja obsady przedmiotu"
        SH.FindComboItem(.cbKategoria, DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("Kategoria").ToString)
        .chkGetToAvg.CheckState = CType(DT.Select("IdObsada='" & lvObsada.SelectedItems(0).Text & "'")(0).Item("GetToAverage"), CheckState)
        .CancelButton = .Cancel_Button
        .AcceptButton = .OK_Button

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, IdObsada As String
          Dim MySQLTrans As MySqlTransaction

          IdObsada = Me.lvObsada.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(O.UpdateObsada())
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?ID", IdObsada)
            If ObsadaFilter = "Klasa" Then
              cmd.Parameters.AddWithValue("?Klasa", CType(cbObsadaFilter.SelectedItem, CbItem).ID)
              cmd.Parameters.AddWithValue("?Przedmiot", CType(.cb1.SelectedItem, CbItem).ID)
              cmd.Parameters.AddWithValue("?Nauczyciel", CType(.cb2.SelectedItem, CbItem).ID)
            ElseIf ObsadaFilter = "Belfer" Then
              cmd.Parameters.AddWithValue("?Klasa", CType(.cb2.SelectedItem, CbItem).ID)
              cmd.Parameters.AddWithValue("?Przedmiot", CType(.cb1.SelectedItem, CbItem).ID)
              cmd.Parameters.AddWithValue("?Nauczyciel", CType(cbObsadaFilter.SelectedItem, CbItem).ID)
            Else
              cmd.Parameters.AddWithValue("?Klasa", CType(.cb1.SelectedItem, CbItem).ID)
              cmd.Parameters.AddWithValue("?Przedmiot", CType(cbObsadaFilter.SelectedItem, CbItem).ID)
              cmd.Parameters.AddWithValue("?Nauczyciel", CType(.cb2.SelectedItem, CbItem).ID)
            End If
            cmd.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
            cmd.Parameters.AddWithValue("?Kategoria", CType(.cbKategoria.SelectedItem, CbItem).Kod)
            cmd.Parameters.AddWithValue("?GetToAverage", CType(.chkGetToAvg.CheckState, Integer))
            cmd.Parameters.AddWithValue("?DataAktywacji", .dtDataAktywacji.Value)
            cmd.Parameters.AddWithValue("?LiczbaGodzin", .nudLiczbaGodzin.Value)

            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MySQLTrans.Rollback()
          End Try
          Me.FetchData()
          GetData(Filter) 'CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString)
          Me.EnableButtons(False)
          SH.FindListViewItem(Me.lvObsada, IdObsada)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", "Belfer .NET", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, O As New ObsadaSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvObsada.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(O.DeleteObsada(Item.Text))
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        Me.FetchData()
        GetData(Filter)
        'If My.Application.OpenForms.OfType(Of dlgObsada )().Any() Then RaiseEvent WychowawcaRemoved()
        Dim SH As New SeekHelper
        Me.EnableButtons(False)
        SH.FindPostRemovedListViewItemIndex(Me.lvObsada, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        Select Case mex.Number
          Case 1451
            MessageBox.Show("Nie można usunąć obsady z powodu istniejącej relacji między tabelami. Usuń powiązany rekord z tabeli podrzędnej i spróbuj jeszcze raz.", My.Application.Info.ProductName, MessageBoxButtons.OK)
          Case Else
            MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
        End Select
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If

  End Sub

  Private Sub rbPrzedmiot_CheckedChanged(sender As Object, e As EventArgs) Handles rbPrzedmiot.CheckedChanged, rbKlasa.CheckedChanged, rbNauczyciel.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    ObsadaFilter = CType(sender, RadioButton).Tag.ToString
    ApplyNewConfig()
  End Sub

  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkVirtual.CheckedChanged
    If Not Me.Created Then Exit Sub
    ApplyNewConfig()
  End Sub

  Private Sub cmdDeaktywacja_Click(sender As Object, e As EventArgs) Handles cmdDeaktywacja.Click
    Dim dlgData As New dlgStrikeOut
    'cmdDeaktywacja.Enabled = False
    With dlgData
      .Text = "Data deaktywacji"
      .lblData.Text = "Data deaktywacji"
      Dim CH As New CalcHelper
      .dtData.MinDate = CType(lvObsada.SelectedItems(0).SubItems(5).Text, Date) 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      .dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)

      .dtData.Value = CType(lvObsada.SelectedItems(0).SubItems(5).Text, Date)
      If .ShowDialog() = Windows.Forms.DialogResult.OK Then
        Dim MySQLTrans As MySqlTransaction = Nothing
        Try
          'If .dtData.Value < CType(lvObsada.SelectedItems(0).SubItems(5).Text, Date) Then Throw New Exception("Data deaktywacji nie może być mniejsza od daty aktywacji.")
          Dim DBA As New DataBaseAction, IdObsada As String, O As New ObsadaSQL
          IdObsada = Me.lvObsada.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(O.DeactivateStaff())
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?ID", IdObsada)
          cmd.Parameters.AddWithValue("?DataDeaktywacji", .dtData.Value)
          cmd.ExecuteNonQuery()
          MySQLTrans.Commit()
          Me.FetchData()
          GetData(Filter) 'CType(cbObsadaFilter.SelectedItem, CbItem).ID.ToString)
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvObsada, IdObsada)
        Catch mex As MySqlException
          MessageBox.Show(mex.Message)
          MySQLTrans.Rollback()
        Catch ex As Exception
          MessageBox.Show(ex.Message)
          MySQLTrans.Rollback()
        End Try
      End If
    End With
  End Sub
End Class