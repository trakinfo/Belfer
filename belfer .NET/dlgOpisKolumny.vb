Imports System.Windows.Forms

Public Class dlgOpisKolumny
  Private IdObsada As String, Waga As Decimal, IdPrzedmiot As String
  Private DT As DataTable, Filter As String = ""

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    'Me.Dispose(True)
    Close()
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub

  Public Sub ClassChanged(Klasa As String)
    lblKlasa.Text = "Klasa: " & Klasa
  End Sub

  Public Sub PrzedmiotChanged(Obsada As String, PrzedmiotNazwa As String, Przedmiot As String)
    IdObsada = Obsada
    IdPrzedmiot = Przedmiot
    lblPrzedmiot.Text = "Przedmiot: " & PrzedmiotNazwa
  End Sub
  Public Sub SetWaga(Value As Decimal)
    Waga = Value
    nudWaga.Value = Value
  End Sub

  Public Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      .Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = False
      .AllowColumnReorder = False
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nazwa", 485, HorizontalAlignment.Left)
      .Columns.Add("Kolor oceny", 80, HorizontalAlignment.Center)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, OW As New OpisWynikuSQL
    DT = DBA.GetDataTable(OW.SelectOpis(IdPrzedmiot))

  End Sub

  Public Sub GetData()
    Try
      FetchData()
      lvOpisWyniku.Items.Clear()
      Dim FilteredData() As DataRow
      FilteredData = DT.Select(Filter)

      For i As Integer = 0 To FilteredData.GetUpperBound(0)
        Dim NewItem As New ListViewItem(FilteredData(i).Item(0).ToString)
        NewItem.UseItemStyleForSubItems = False
        'Me.lvOpisWyniku.Items.Add(FilteredData(i).Item(0).ToString)
        'Me.lvOpisWyniku.Items(Me.lvOpisWyniku.Items.Count - 1).SubItems.Add(FilteredData(i).Item(1).ToString)
        NewItem.SubItems.Add(FilteredData(i).Item(1).ToString)
        NewItem.SubItems.Add(FilteredData(i).Item(2).ToString)
        NewItem.SubItems(2).BackColor = ColorTranslator.FromHtml(FilteredData(i).Item(2).ToString)
        NewItem.SubItems(2).ForeColor = NewItem.SubItems(2).BackColor 'CH.InvertColor(NewItem.SubItems(2).BackColor)
        lvOpisWyniku.Items.Add(NewItem)

      Next
      Me.lvOpisWyniku.Columns(1).Width = CInt(IIf(lvOpisWyniku.Items.Count > 21, 466, 485))
      Me.lvOpisWyniku.Enabled = CType(IIf(Me.lvOpisWyniku.Items.Count > 0, True, False), Boolean)
      lblRecord.Text = "0 z " & lvOpisWyniku.Items.Count
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub lvOpisWyniku_DoubleClick(sender As Object, e As EventArgs) Handles lvOpisWyniku.DoubleClick
    EditOpisKolumny()
  End Sub
  Private Sub EditOpisKolumny()
    Try
      Dim dlgEdit As New dlgOpisWyniku
      With dlgEdit
        .Text = "Edycja opisu wyniku cząstkowego"
        .OK_Button.Text = "Zapisz"
        .txtOpis.Text = lvOpisWyniku.SelectedItems(0).SubItems(1).Text
        .cmdKolor.BackColor = ColorTranslator.FromHtml(lvOpisWyniku.SelectedItems(0).SubItems(2).Text)
        .txtKolor.Text = lvOpisWyniku.SelectedItems(0).SubItems(2).Text
        .txtKolor.ForeColor = ColorTranslator.FromHtml(lvOpisWyniku.SelectedItems(0).SubItems(2).Text)
        .Icon = GlobalValues.gblAppIcon
        .MinimizeBox = False
        .MaximizeBox = False

        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, OW As New OpisWynikuSQL, IdOpis As String
          Dim MySQLTrans As MySqlTransaction

          IdOpis = Me.lvOpisWyniku.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(OW.UpdateOpis)
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          Try
            cmd.Parameters.AddWithValue("?Nazwa", .txtOpis.Text.Trim)
            cmd.Parameters.AddWithValue("?ID", IdOpis)
            cmd.Parameters.AddWithValue("?Kolor", .txtKolor.Text)

            cmd.ExecuteNonQuery()
            MySQLTrans.Commit()

          Catch ex As MySqlException
            MySQLTrans.Rollback()
            MessageBox.Show(ex.Message)
          End Try
          GetData()
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvOpisWyniku, IdOpis)
        End If
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub lvOpisWyniku_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvOpisWyniku.ItemSelectionChanged
    lblRecord.Text = "0 z " & lvOpisWyniku.Items.Count
    If e.IsSelected Then
      lblRecord.Text = lvOpisWyniku.SelectedItems(0).Index + 1 & " z " & lvOpisWyniku.Items.Count
      cmdReset.Enabled = True
      OK_Button.Enabled = True
      cmdEdit.Enabled = True
    Else
      cmdReset.Enabled = False
      cmdEdit.Enabled = False
      OK_Button.Enabled = CType(IIf(nudWaga.Value <> Waga, True, False), Boolean)
    End If
  End Sub
  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Filter = "Nazwa LIKE '" & Me.txtSeek.Text & "%'"
    GetData()

  End Sub

  Private Sub nudWaga_ValueChanged(sender As Object, e As EventArgs) Handles nudWaga.ValueChanged
    OK_Button.Enabled = CType(IIf(nudWaga.Value <> Waga, True, False), Boolean)
  End Sub


  Private Sub cmdReset_Click(sender As Object, e As EventArgs) Handles cmdReset.Click
    'lvOpisWyniku.SelectedItems(0).Selected = False
    lvOpisWyniku.SelectedItems.Clear()
    OK_Button.Enabled = True
  End Sub

  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgOpisWyniku
    dlgAddNew.Text = "Definiowanie opisu wyniku cząstkowego"
    dlgAddNew.IsNewMode = True
    dlgAddNew.IdPrzedmiot = IdPrzedmiot
    dlgAddNew.MaximizeBox = False
    
    Me.cmdAddNew.Enabled = False

    AddHandler dlgAddNew.NewAdded, AddressOf NewAdded
    dlgAddNew.ShowDialog()
    RemoveHandler dlgAddNew.NewAdded, AddressOf NewAdded
    cmdAddNew.Enabled = True
    'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
  End Sub
  Private Sub NewAdded(ByVal InsertedID As String)
    lvOpisWyniku.Items.Clear()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvOpisWyniku, InsertedID)
  End Sub

  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    EditOpisKolumny()
  End Sub

End Class
