Public Class frmKolumna
  Public Event ColumnNumberChanged(ColNumber As Byte)
  Private DT As DataTable
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.KolumnyToolStripMenuItem.Enabled = True
    MainForm.OcenyCzastkoweToolStripMenuItem.Enabled = True
    MainForm.cmdWynikiCzastkowe.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.KolumnyToolStripMenuItem.Enabled = True
    MainForm.OcenyCzastkoweToolStripMenuItem.Enabled = True
    MainForm.cmdWynikiCzastkowe.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData()
    Dim O As New KolumnaSQL, DBA As New DataBaseAction
    DT = DBA.GetDataTable(O.SelectKolumna(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, CbItem).ID.ToString))

  End Sub
  Private Overloads Sub GetData(IdObsada As String, Typ As String)
    Try
      lvKolumna.Items.Clear()
      ClearDetails()
      For j As Integer = 0 To DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'").GetUpperBound(0)
        Dim NewItem As New ListViewItem(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(0).ToString)
        NewItem.UseItemStyleForSubItems = False
        NewItem.SubItems.Add(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(1).ToString)
        NewItem.SubItems(1).BackColor = If(CType(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(5), Boolean), Color.Aqua, lvKolumna.BackColor)
        NewItem.SubItems.Add(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(2).ToString)
        NewItem.SubItems.Add(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(3).ToString)
        NewItem.SubItems.Add(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(4).ToString)
        NewItem.SubItems(3).BackColor = ColorTranslator.FromHtml(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(3).ToString)
        NewItem.SubItems(3).ForeColor = NewItem.SubItems(3).BackColor 'CH.InvertColor(NewItem.SubItems(2).BackColor)
        'NewItem.SubItems.Add(DT.Select("IdObsada=" & IdObsada & " AND Typ='" & Typ & "'")(j).Item(5).ToString)

        lvKolumna.Items.Add(NewItem)
      Next
      RaiseEvent ColumnNumberChanged(CType(lvKolumna.Items.Count, Byte))
      lblRecord.Text = "0 z " & lvKolumna.Items.Count
      lvKolumna.Columns(2).Width = CInt(IIf(lvKolumna.Items.Count > 24, 462, 481))
      lvKolumna.Enabled = CBool(IIf(lvKolumna.Items.Count > 0, True, False))

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try
  End Sub
  Private Sub ApplyNewConfig()

    cmdAddNew.Enabled = False
    cmdEdit.Enabled = False
    cmdDelete.Enabled = False
    lvKolumna.Items.Clear()
    lvKolumna.Enabled = False
    cbPrzedmiot.Items.Clear()
    cbPrzedmiot.Enabled = False
    nudSemestr.Enabled = False
    FillKlasa(cbKlasa)
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmObsadaWgKlas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvKolumna)
    lblRecord.Text = ""
    Dim CH As New CalcHelper
    'MaxColNumber = CH.MaxColNumber
    'Me.nudStartYear.Value = CH.StartDateOfSchoolYear.Year
    nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    ApplyNewConfig()
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
      .Columns.Add("Nr", 40, HorizontalAlignment.Center)
      .Columns.Add("Opis", 481, HorizontalAlignment.Left)
      .Columns.Add("Kolor oceny", 80, HorizontalAlignment.Center)
      .Columns.Add("Waga", 50, HorizontalAlignment.Center)
      '.Columns.Add("Poprawa", 50, HorizontalAlignment.Center)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub FillKlasa(ByVal cb As ComboBox)
    cb.Items.Clear()
    'RaiseEvent ClassChanged(Nothing)
    Dim FCB As New FillComboBox, OK As New KolumnaSQL
    FCB.AddComboBoxComplexItems(cb, OK.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Dim SH As New SeekHelper
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvKolumna.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ByVal IdOpis As String)
    'Dim FoundRow() As DataRow
    Try
      lblRecord.Text = lvKolumna.SelectedItems(0).Index + 1 & " z " & lvKolumna.Items.Count
      With DT.Select("ID='" & IdOpis & "'")(0) 'FoundRow(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")")
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    'RaiseEvent ClassChanged(CType(cbKlasa.SelectedItem, CbItem).Nazwa)
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    Me.FetchData()
    lvKolumna.Items.Clear()
    lvKolumna.Enabled = False
    EnableButtons(False)
    FillPrzedmiot(cbPrzedmiot)
  End Sub
  Private Sub FillPrzedmiot(ByVal cb As ComboBox)
    cmdAddNew.Enabled = False
    cb.Items.Clear()
    Dim FCB As New FillComboBox, O As New KolumnaSQL
    FCB.AddComboBoxExtendedItems(cb, O.SelectPrzedmiot(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear))
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Dim SH As New SeekHelper
    If My.Settings.IdObsada.Length > 0 Then SH.FindComboItem(Me.cbPrzedmiot, CType(My.Settings.IdObsada, Integer))
  End Sub

  Private Sub cbPrzedmiot_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectionChangeCommitted
    'RaiseEvent PrzedmiotChanged(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, CType(cbPrzedmiot.SelectedItem, CbItem).Nazwa)
    My.Settings.IdObsada = CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectedIndexChanged

    GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
    EnableButtons(False)
    'Me.cmdAddNew.Enabled = CType(IIf(My.Application.OpenForms.OfType(Of dlgKolumna)().Any(), False, True), Boolean)
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.SchoolTeacherID = CType(cbPrzedmiot.SelectedItem, CbItem).Kod Then
      cmdAddNew.Enabled = True
    Else
      cmdAddNew.Enabled = False
    End If
    nudSemestr.Enabled = True
  End Sub
  Private Sub EnableButtons(Value As Boolean)
    'Me.cmdAddNew.Enabled = Value
    cmdEdit.Enabled = Value
    Me.cmdDelete.Enabled = Value
  End Sub

  Private Sub lvObsada_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvKolumna.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.ToLower Then
        EnableButtons(True)
      Else
        EnableButtons(False)
        If GlobalValues.AppUser.SchoolTeacherID = CType(cbPrzedmiot.SelectedItem, CbItem).Kod Then
          cmdEdit.Enabled = True
        End If
      End If
    Else
      ClearDetails()
      EnableButtons(False)
    End If
  End Sub
  Private Sub lvWychowawca_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvKolumna.DoubleClick
    If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.SchoolTeacherID = CType(cbPrzedmiot.SelectedItem, CbItem).Kod Then
      EditData()
      'Else
      '  If GlobalValues.AppUser.SchoolTeacherID = CType(cbPrzedmiot.SelectedItem, CbItem).Kod Then
      '    EditData()
      '  End If
    End If
  End Sub
  Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    'Dim dlgAddNew As New dlgKolumna
    Dim dlgAddNew As New dlgWstawKolumna
    Dim MySQLTrans As MySqlTransaction = Nothing
    With dlgAddNew
      Try
        .ClassChanged(CType(cbKlasa.SelectedItem, CbItem).Nazwa)
        .PrzedmiotChanged(CType(cbPrzedmiot.SelectedItem, CbItem).Nazwa)
        .SemestrChanged(nudSemestr.Value.ToString)
        .SetMaxColNumber()
        .SetColNumber(lvKolumna.Items.Count)
        Dim CurrentColumn As Byte = CType(lvKolumna.SelectedItems(0).Index + 1, Byte)
        .cbPozycja.Items.Add(New CbItem(CurrentColumn, "Przed kolumną odniesienia"))
        .cbPozycja.Items.Add(New CbItem(CurrentColumn + 1, "Po kolumnie odniesienia"))
        .cbPozycja.SelectedIndex = 1
        .UpdateColData(CurrentColumn)
        '.Text = "Dodawanie nowych kolumn bez opisu"
        .MaximizeBox = False
        .StartPosition = FormStartPosition.CenterScreen
        If .ShowDialog = Windows.Forms.DialogResult.OK Then
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          Dim TotalColNumber As Byte = CType(lvKolumna.Items.Count, Byte)
          While TotalColNumber >= CType(CType(.cbPozycja.SelectedItem, CbItem).ID, Byte)
            .MoveColumn(TotalColNumber, CType(.nudLiczbaKolumn.Value, Byte), CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString, MySQLTrans)
            TotalColNumber -= CType(1, Byte)
          End While
          Dim ColID As String
          ColID = .AddNew(CType(CType(.cbPozycja.SelectedItem, CbItem).ID, Byte), CType(.nudLiczbaKolumn.Value, Byte), CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString, MySQLTrans)
          MySQLTrans.Commit()
          NewColumnAdded(ColID)
        End If
      Catch myex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(myex.Message)
      Catch ex As Exception
        MySQLTrans.Rollback()
        MessageBox.Show(ex.Message)
      Finally

      End Try
    End With
  End Sub

  Private Sub NewColumnAdded(ByVal InsertedColumn As String)
    Me.FetchData()
    GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvKolumna, InsertedColumn)
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgOpisKolumny, DBA As New DataBaseAction
      'Dim FCB As New FillComboBox
      With dlgEdit
        .OK_Button.Text = "&Zapisz"
        .ClassChanged(CType(cbKlasa.SelectedItem, CbItem).Nazwa)
        Dim W As New WynikiSQL
        .PrzedmiotChanged(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, CType(cbPrzedmiot.SelectedItem, CbItem).Nazwa, DBA.GetSingleValue(W.SelectPrzedmiotId(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)))
        .txtNrKolumny.Text = lvKolumna.SelectedItems(0).SubItems(1).Text
        .SetWaga(CType(lvKolumna.SelectedItems(0).SubItems(4).Text, Decimal))
        .chkPoprawa.Checked = If(lvKolumna.SelectedItems(0).SubItems(1).BackColor = Color.Aqua, True, False) 'CType(lvKolumna.SelectedItems(0).SubItems(1).Text, Boolean)
        .Text = "Edycja parametrów kolumny"
        .ListViewConfig(.lvOpisWyniku)
        .GetData()
        Dim SH As New SeekHelper
        SH.FindListViewItem(.lvOpisWyniku, DT.Select("ID='" & lvKolumna.SelectedItems(0).Text & "'")(0).Item("IdOpisWyniku").ToString)

        .CancelButton = .Cancel_Button
        .AcceptButton = .OK_Button

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim IdOpisKolumny As String, OK As New KolumnaSQL
          Dim MySQLTrans As MySqlTransaction = Nothing

          IdOpisKolumny = Me.lvKolumna.SelectedItems(0).Text
          Try
            Dim cmd As MySqlCommand = DBA.CreateCommand(OK.UpdateKolumna)
            MySQLTrans = GlobalValues.gblConn.BeginTransaction()
            cmd.Transaction = MySQLTrans

            cmd.Parameters.AddWithValue("?ID", IdOpisKolumny)
            'cmd.Parameters.AddWithValue("?IdObsada", CType(cbPrzedmiot.SelectedItem, CbItem).ID)
            If .lvOpisWyniku.SelectedItems.Count > 0 Then
              cmd.Parameters.AddWithValue("?IdOpis", .lvOpisWyniku.SelectedItems(0).Text)
            Else
              cmd.Parameters.AddWithValue("?IdOpis", DBNull.Value)
            End If
            cmd.Parameters.AddWithValue("?Waga", .nudWaga.Value)
            cmd.Parameters.AddWithValue("?Poprawa", .chkPoprawa.CheckState)

            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch mex As MySqlException
            MessageBox.Show(mex.Message)
            MySQLTrans.Rollback()
          Catch ex As Exception
            MySQLTrans.Rollback()
            MessageBox.Show(ex.Message)
          End Try

          Me.FetchData()
          GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
          Me.EnableButtons(False)
          SH.FindListViewItem(Me.lvKolumna, IdOpisKolumny)
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
    If MessageBox.Show("Zaznaczone pozycje zostaną bezpowrotnie usunięte z bazy danych." & vbNewLine & "Czy na pewno o to Ci chodzi? Jeśli tak, to naciśnij przycisk 'OK'. W przeciwnym razie wciśnij przycisk 'Anuluj'.", My.Application.Info.ProductName, MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
      Dim DBA As New DataBaseAction, K As New KolumnaSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvKolumna.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(K.DeleteKolumna(Item.Text))
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        Dim DT As DataTable
        DT = DBA.GetDataTable(K.SelectKolumnaID(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString))
        Dim i As Integer = 0
        Do Until i > DT.Rows.Count - 1
          i += 1
          Dim cmd1 As MySqlCommand = DBA.CreateCommand(K.UpdateNrKolumna)
          cmd1.Transaction = MySQLTrans
          cmd1.Parameters.AddWithValue("?ID", DT.Rows(i - 1).Item(0).ToString)
          cmd1.Parameters.AddWithValue("?Nr", i)
          cmd1.ExecuteNonQuery()
        Loop

        MySQLTrans.Commit()
        Me.FetchData()
        GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
        'If My.Application.OpenForms.OfType(Of dlgkolumna )().Any() Then RaiseEvent WychowawcaRemoved()
        Dim SH As New SeekHelper
        Me.EnableButtons(False)
        SH.FindPostRemovedListViewItemIndex(Me.lvKolumna, DeletedIndex)
      Catch mex As MySqlException
        Select Case mex.Number
          Case 1451
            MessageBox.Show("Nie można usunąć kolumny wynikowej zawierającej oceny! Usuń oceny z tej kolumny i spróbuj jeszcze raz.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          Case Else
            MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Select
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      Finally

      End Try
    End If

  End Sub

  Private Sub nudSemestr_ValueChanged(sender As Object, e As EventArgs) Handles nudSemestr.ValueChanged
    If Not nudSemestr.Created Then Exit Sub
    EnableButtons(False)

    If cbPrzedmiot.SelectedItem IsNot Nothing Then GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString, IIf(nudSemestr.Value = 1, "C1", "C2").ToString)
  End Sub


End Class