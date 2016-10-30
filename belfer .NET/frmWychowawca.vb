Imports System.Drawing.Printing
Public Class frmWychowawca
  Private DT As DataTable
  Public Event NewRow()
  'Public Event YearChanged(Year As String)
  'Public Event SchoolChanged(School As String)
  'Public Event WychowawcaRemoved()
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PrzydzialWychowawstwToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PrzydzialWychowawstwToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWychowawca_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvWychowawca)
    lblRecord.Text = ""
    ApplyNewConfig()
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvWychowawca.Items.Clear()
    ClearDetails()
    FetchData()
    GetData()
  End Sub
  Private Sub FetchData()
    Dim W As New WychowawcaSQL, DBA As New DataBaseAction
    DT = DBA.GetDataTable(W.SelectWychowawca(My.Settings.SchoolYear, My.Settings.IdSchool))
  End Sub
  Private Sub GetData()
    Try
      lvWychowawca.Items.Clear()
      ClearDetails()
      For Each R As DataRow In DT.Rows
        Dim NewItem As New ListViewItem(R.Item(0).ToString)
        NewItem.SubItems.Add(R.Item(1).ToString)
        NewItem.SubItems.Add(R.Item(2).ToString)
        NewItem.SubItems.Add(CType(R.Item("DataAktywacji"), Date).ToShortDateString)
        If IsDBNull(R.Item("DataDeaktywacji")) Then
          NewItem.SubItems.Add("")
        Else
          NewItem.SubItems.Add(CType(R.Item("DataDeaktywacji"), Date).ToShortDateString)
          NewItem.ForeColor = Color.DarkGray
        End If
        NewItem.SubItems.Add(R.Item("LiczbaGodzin").ToString)
        NewItem.SubItems.Add(R.Item("Klasa").ToString)
        lvWychowawca.Items.Add(NewItem)
      Next
      lblRecord.Text = "0 z " & lvWychowawca.Items.Count
      lvWychowawca.Columns(1).Width = CInt(IIf(lvWychowawca.Items.Count > 25, 181, 200))
      lvWychowawca.Enabled = CBool(IIf(lvWychowawca.Items.Count > 0, True, False))

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
      'poprawić wychowawcę
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Klasa", 200, HorizontalAlignment.Center)
      .Columns.Add("Nazwisko i imię", 330, HorizontalAlignment.Left)
      .Columns.Add("Data aktywacji", 100, HorizontalAlignment.Center)
      .Columns.Add("Data deaktywacji", 100, HorizontalAlignment.Center)
      .Columns.Add("Liczba godzin", 95, HorizontalAlignment.Center)
      .Columns.Add("IdKlasa", 0, HorizontalAlignment.Left)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub ClearDetails()
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ByVal IdObsada As String)
    'Dim FoundRow() As DataRow
    Try
      lblRecord.Text = lvWychowawca.SelectedItems(0).Index + 1 & " z " & lvWychowawca.Items.Count
      'FoundRow = DT.Select("ID=" & ID)

      With DT.Select("IdObsada='" & IdObsada & "'")(0) 'FoundRow(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(4).ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvWychowawca_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvWychowawca.DoubleClick
    If lvWychowawca.SelectedItems(0).SubItems(4).Text.Length = 0 Then EditData()
  End Sub

  Private Sub lvWychowawca_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvWychowawca.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      'Me.cmdAddNew.Enabled = True
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then
        Me.cmdDelete.Enabled = True
        If e.Item.SubItems(4).Text = "" Then
          cmdDeaktywacja.Enabled = True
          cmdEdit.Enabled = True
        End If
      End If
    Else
      ClearDetails()
      lblRecord.Text = "0 z " & CType(sender, ListView).Items.Count
      'Me.cmdAddNew.Enabled = True
      cmdEdit.Enabled = False
      Me.cmdDelete.Enabled = False
      cmdDeaktywacja.Enabled = False
    End If
  End Sub

  Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    Try
      Dim dlgAddNew As New dlgWychowawca
      dlgAddNew.IsNewMode = True

      If DT.Select().GetLength(0) > 0 Then
        dlgAddNew.IdPrzedmiot = DT.Select()(0).Item("Przedmiot").ToString
      Else
        Dim DBA As New DataBaseAction, W As New WychowawcaSQL
        Dim R As MySqlDataReader = DBA.GetReader(W.SelectPrzedmiotZachowanie(My.Settings.IdSchool))
        If R.HasRows Then
          R.Read()
          dlgAddNew.IdPrzedmiot = R.Item(0).ToString
          R.Close()
        Else
          R.Close()
          Throw New System.Exception("Nie można przydzielić wychowawstwa." & vbNewLine & "Brak zdefiniowanego typu przedmiotu, oznaczającego wychowawstwo." & vbNewLine & "Oznacz jeden z przedmiotów jako typ 'z' i spróbuj ponownie.")
        End If
      End If
      dlgAddNew.FillKlasa()
      dlgAddNew.FillBelfer()
      Dim CH As New CalcHelper
      dlgAddNew.dtDataAktywacji.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      dlgAddNew.dtDataAktywacji.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      If Date.Now >= dlgAddNew.dtDataAktywacji.MinDate AndAlso Date.Now <= dlgAddNew.dtDataAktywacji.MaxDate Then
        dlgAddNew.dtDataAktywacji.Value = Date.Now
      Else
        dlgAddNew.dtDataAktywacji.Value = dlgAddNew.dtDataAktywacji.MinDate
      End If
      dlgAddNew.Text = "Przydział wychowawstwa"
      'dlgAddNew.MdiParent = Me.MdiParent
      dlgAddNew.MaximizeBox = False
      dlgAddNew.StartPosition = FormStartPosition.CenterScreen

      AddHandler dlgAddNew.NewWychowawcaAdded, AddressOf NewWychowawcaAdded
  
      Me.cmdAddNew.Enabled = False
      dlgAddNew.ShowDialog()
      Me.cmdAddNew.Enabled = True

    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    Finally

    End Try
  End Sub
  'Private Sub EnableCmdAddNew(ByVal sender As Object, ByVal e As EventArgs)
  '  Me.cmdAdd.Enabled = True
  'End Sub
  Private Sub NewWychowawcaAdded(ByVal InsertedKlasa As String)
    Me.FetchData()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvWychowawca, InsertedKlasa)
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgWychowawca, W As New WychowawcaSQL
      Dim FCB As New FillComboBox
      With dlgEdit
        .IsNewMode = False
        .OK_Button.Text = "&Zapisz"
        'FCB.AddComboBoxSimpleItems(dlgEdit.cbKlasa, W.SelectAllClass(CType(cbSzkola.SelectedItem, CbItem).ID.ToString))
        .cbKlasa.Items.Add(New CbItem(CType(Me.lvWychowawca.SelectedItems(0).SubItems(6).Text, Integer), Me.lvWychowawca.SelectedItems(0).SubItems(1).Text))
        .cbKlasa.SelectedIndex = 0
        .cbKlasa.Enabled = False

        FCB.AddComboBoxComplexItems(dlgEdit.cbBelfer, W.SelectBelfer(My.Settings.IdSchool))
        .cbBelfer.AutoCompleteSource = AutoCompleteSource.ListItems
        .cbBelfer.AutoCompleteMode = AutoCompleteMode.Append

        .nudLiczbaGodzin.Value = CType(Me.lvWychowawca.SelectedItems(0).SubItems(5).Text, Integer)
        .dtDataAktywacji.Value = CType(Me.lvWychowawca.SelectedItems(0).SubItems(3).Text, Date)
        .Text = "Edycja przydziału wychowawstwa"
        Dim SH As New SeekHelper
        'SH.FindComboItem(.cbKlasa, Me.lvWychowawca.SelectedItems(0).SubItems(3).Text)
        SH.FindComboItem(.cbBelfer, CType(DT.Select("IdObsada='" & lvWychowawca.SelectedItems(0).Text & "'")(0).Item("IdWychowawca"), Integer))
        .CancelButton = .Cancel_Button
        .AcceptButton = .OK_Button

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, IdObsada As String
          Dim MySQLTrans As MySqlTransaction

          IdObsada = Me.lvWychowawca.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(W.UpdateWychowawca(Me.lvWychowawca.SelectedItems(0).Text))
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?IdBelfer", CType(.cbBelfer.SelectedItem, CbItem).ID)
            cmd.Parameters.AddWithValue("?DataAktywacji", .dtDataAktywacji.Value)
            cmd.Parameters.AddWithValue("?LiczbaGodzin", .nudLiczbaGodzin.Value)
            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MySQLTrans.Rollback()
          End Try
          Me.FetchData()
          GetData()
          Me.EnableButtons(False)
          SH.FindListViewItem(Me.lvWychowawca, IdObsada)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, W As New WychowawcaSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvWychowawca.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(W.DeleteWychowawca(Item.Text))
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()

          'DBA.ApplyChanges(W.DeleteWychowawca(Item.Text, Me.nudStartYear.Value.ToString & "/" & Me.nudEndYear.Value.ToString))
        Next
        MySQLTrans.Commit()
        Me.FetchData()
        GetData()
        'If My.Application.OpenForms.OfType(Of dlgWychowawca)().Any() Then RaiseEvent WychowawcaRemoved()
        Dim SH As New SeekHelper
        Me.EnableButtons(False)
        SH.FindPostRemovedListViewItemIndex(Me.lvWychowawca, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If

  End Sub

  Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    Dim PP As New dlgPrintPreview, DS As New DataSet, DBA As New DataBaseAction, S As New SzkolaSQL
    DS.Tables.Add(DT)
    DS.Tables.Add(DBA.GetDataTable(S.SelectSchoolName(My.Settings.IdSchool)))
    PP.Doc = New PrintReport(DS)
    RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage
    RemoveHandler NewRow, AddressOf PP.NewRow
    AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage
    AddHandler NewRow, AddressOf PP.NewRow
    PP.Doc.ReportHeader = New String() {"Przydział wychowawstw"}
    'PP.Doc.Offset(1) = 2
    If PP.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
      'DS.Tables.Remove(DT)
      'DS.Tables.Remove(DS.Tables(1))
      DS.Tables.Clear()
    End If
  End Sub
  Public Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
    Dim Doc As PrintReport = CType(sender, PrintReport)
    Dim x As Single = Doc.DefaultPageSettings.Margins.Left - Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Left
    Dim y As Single = Doc.DefaultPageSettings.Margins.Top - Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Top
    Dim PrnVars As New PrintVariables
    Dim TextFont As Font = PrnVars.BaseFont
    Dim HeaderFont As Font = PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)

    Doc.PageNumber += 1
    Doc.DrawText(e, "- " & Doc.PageNumber & " -", TextFont, x, Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Top + LineHeight, e.MarginBounds.Width, LineHeight, 1, Brushes.Black, False)
    If Doc.PageNumber = 1 Then
      y += LineHeight
      Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 1, Brushes.Black, False)
      y += LineHeight * 2
      Doc.DrawText(e, "Szkoła: " & Doc.DS.Tables(1).Rows(0).Item("Nazwa").ToString, TextFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 0, Brushes.Black, False)
      y += LineHeight
      Doc.DrawText(e, "Rok szkolny: " & My.Settings.SchoolYear, TextFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 0, Brushes.Black, False)
      y += LineHeight * 3
    End If

    'PrintColumnsHeaders(e, x, y, TextFont, LineHeight)
    Doc.DrawText(e, "L.p.", PrnVars.BoldFont, x, y, 40, LineHeight * 2, 1, Brushes.Black)
    Doc.DrawText(e, "Klasa", PrnVars.BoldFont, x + 40, y, 150, LineHeight * 2, 1, Brushes.Black)
    Doc.DrawText(e, "Wychowawca", PrnVars.BoldFont, x + 190, y, 400, LineHeight * 2, 1, Brushes.Black)
    y += LineHeight * 2

    Do Until (y + LineHeight) > e.MarginBounds.Bottom Or Doc.Offset(0) > Doc.DS.Tables(0).Select().GetUpperBound(0)
      Doc.Lp += 1
      Doc.DrawText(e, Doc.Lp.ToString, TextFont, x, y, 40, LineHeight, 1, Brushes.Black)
      Doc.DrawText(e, Doc.DS.Tables(0).Select()(Doc.Offset(0)).Item(1).ToString, TextFont, x + 40, y, 150, LineHeight, 1, Brushes.Black)
      Doc.DrawText(e, Doc.DS.Tables(0).Select()(Doc.Offset(0)).Item(2).ToString, TextFont, x + 190, y, 400, LineHeight, 0, Brushes.Black)
      y += LineHeight

      Doc.Offset(0) += 1
    Loop

    If Doc.Offset(0) <= Doc.DS.Tables(0).Select().GetLength(0) - 1 Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Doc.Offset(0) = 0
    End If

  End Sub

  Private Sub cmdDeaktywacja_Click(sender As Object, e As EventArgs) Handles cmdDeaktywacja.Click
    Dim dlgData As New dlgStrikeOut
    'cmdDeaktywacja.Enabled = False
    With dlgData
      .Text = "Data deaktywacji"
      .lblData.Text = "Data deaktywacji"
      Dim CH As New CalcHelper
      .dtData.MinDate = CType(lvWychowawca.SelectedItems(0).SubItems(3).Text, Date) 'CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
      .dtData.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
      .dtData.Value = CType(lvWychowawca.SelectedItems(0).SubItems(3).Text, Date)
      If .ShowDialog() = Windows.Forms.DialogResult.OK Then
        Dim MySQLTrans As MySqlTransaction = Nothing
        Try
          Dim DBA As New DataBaseAction, IdObsada As String, O As New ObsadaSQL
          IdObsada = Me.lvWychowawca.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(O.DeactivateStaff())
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?ID", IdObsada)
          cmd.Parameters.AddWithValue("?DataDeaktywacji", .dtData.Value)
          cmd.ExecuteNonQuery()
          MySQLTrans.Commit()
          Me.FetchData()
          GetData()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvWychowawca, IdObsada)
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



