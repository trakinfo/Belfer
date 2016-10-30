Public Class frmOpisWyniku
  Private DT As DataTable
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.OpisyOcenToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.OpisyOcenToolStripMenuItem.Enabled = True
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
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Opis wyniku cząstkowego", 545, HorizontalAlignment.Left)
      .Columns.Add("Kolor oceny", 80, HorizontalAlignment.Center)
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Me.ListViewConfig(Me.lvOpisWyniku)
    lblRecord.Text = ""
    ApplyNewConfig()
  End Sub
  Private Sub FillPrzedmiot(ByVal cb As ComboBox)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, OW As New OpisWynikuSQL
    FCB.AddComboBoxComplexItems(cb, OW.SelectPrzedmiot(My.Settings.IdSchool))
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Dim SH As New SeekHelper
    If My.Settings.IdObsada.Length > 0 Then SH.FindComboItem(Me.cbPrzedmiot, CType(My.Settings.IdObsada, Integer))
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvOpisWyniku.Items.Clear()
    ClearDetails()
    FillPrzedmiot(cbPrzedmiot)
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
    cmdAddNew.Enabled = True
  End Sub
  Private Sub GetData(IdPrzedmiot As String)
    lvOpisWyniku.Items.Clear()
    Dim DBA As New DataBaseAction, OW As New OpisWynikuSQL
    DT = DBA.GetDataTable(OW.SelectOpis(IdPrzedmiot))
    Dim CH As New CalcHelper
    For Each row As DataRow In DT.Rows
      Dim NewItem As New ListViewItem(row.Item(0).ToString)
      NewItem.UseItemStyleForSubItems = False
      NewItem.SubItems.Add(row.Item(1).ToString)
      NewItem.SubItems.Add(row.Item(2).ToString)
      NewItem.SubItems(2).BackColor = ColorTranslator.FromHtml(row.Item(2).ToString)
      NewItem.SubItems(2).ForeColor = NewItem.SubItems(2).BackColor 'CH.InvertColor(NewItem.SubItems(2).BackColor)
      lvOpisWyniku.Items.Add(NewItem)
    Next

    lvOpisWyniku.Columns(1).Width = CInt(IIf(lvOpisWyniku.Items.Count > 15, 526, 545))
    Me.lvOpisWyniku.Enabled = CType(IIf(Me.lvOpisWyniku.Items.Count > 0, True, False), Boolean)
    lblRecord.Text = "0 z " & lvOpisWyniku.Items.Count
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvOpisWyniku.SelectedItems(0).Index + 1 & " z " & lvOpisWyniku.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item("User").ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvOpisWyniku_DoubleClick(sender As Object, e As EventArgs) Handles lvOpisWyniku.DoubleClick
    EditData()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvOpisWyniku.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.ToLower Then EnableButtons(True)
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvOpisWyniku.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, OW As New OpisWynikuSQL
      Try
        For Each Item As ListViewItem In Me.lvOpisWyniku.SelectedItems
          DeletedIndex = Item.Index
          DBA.ApplyChanges(OW.DeleteOpis(Item.Text))
        Next
        Me.EnableButtons(False)
        'lvOpisWyniku.Items.Clear()
        Me.GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
        ClearDetails()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvOpisWyniku, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)

      End Try
    End If
  End Sub
  Public Sub AddKlasa()
    Dim dlgAddNew As New dlgOpisWyniku
    With dlgAddNew
      .Text = "Definiowanie opisu wyniku cząstkowego"
      .IdPrzedmiot = CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString
      .IsNewMode = True
      .MaximizeBox = False

      Me.cmdAddNew.Enabled = False
      AddHandler .NewAdded, AddressOf NewAdded
      .ShowDialog()
      cmdAddNew.Enabled = True
    End With
  End Sub
  Private Sub NewAdded(ByVal InsertedID As String)
    lvOpisWyniku.Items.Clear()
    GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvOpisWyniku, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddKlasa()
  End Sub

  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    EditData()
  End Sub
  Private Sub EditData()
    Try
      Dim dlgEdit As New dlgOpisWyniku
      With dlgEdit
        .Text = "Edycja opisu wyniku cząstkowego"
        .OK_Button.Text = "Zapisz"
        .txtOpis.Text = lvOpisWyniku.SelectedItems(0).SubItems(1).Text
        .cmdKolor.BackColor = ColorTranslator.FromHtml(lvOpisWyniku.SelectedItems(0).SubItems(2).Text)
        '.lblKolor.Text = lvOpisWyniku.SelectedItems(0).SubItems(2).Text
        '.lblKolor.ForeColor = ColorTranslator.FromHtml(lvOpisWyniku.SelectedItems(0).SubItems(2).Text)
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
          'lvMiejscowosc.Items.Clear()
          'FetchData()
          GetData(CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvOpisWyniku, IdOpis)
        End If
      End With
    Catch ex As Exception

      MessageBox.Show(ex.Message)
    End Try

  End Sub

End Class

