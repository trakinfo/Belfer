Public Class frmPrzydzialKlasDoSzkol
  Private DT As DataTable 
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PrzydzialKlasDoSzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PrzydzialKlasDoSzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, PK As New PrzydzialKlasSQL
    DT = DBA.GetDataTable(PK.SelectSchoolClasses(My.Settings.IdSchool))
  End Sub
  Private Sub GetData()
    lvKlasa.Items.Clear()
    For Each row As DataRow In DT.Rows
      Me.lvKlasa.Items.Add(row.Item(0).ToString)
      Me.lvKlasa.Items(Me.lvKlasa.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      lvKlasa.Items(lvKlasa.Items.Count - 1).SubItems.Add(IIf(CType(row.Item(2), Boolean) = True, "x", "").ToString)
    Next

    lvKlasa.Columns(1).Width = CInt(IIf(lvKlasa.Items.Count > 14, 181, 200))
    Me.lvKlasa.Enabled = CType(IIf(Me.lvKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmPrzydzialKlasDoSzkol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvKlasa)
    AddColumns(lvKlasa)
    lblRecord.Text = ""
    ApplyNewConfig()

  End Sub
  Private Sub ApplyNewConfig()
    cmdAddNew.Enabled = True
    cmdEdit.Enabled = False
    cmdDelete.Enabled = False
    lvKlasa.Items.Clear()
    lvKlasa.Enabled = False
    FetchData()
    GetData()

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
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("Kod", 50, HorizontalAlignment.Left)
      .Columns.Add("Nazwa", 200, HorizontalAlignment.Center)
      .Columns.Add("Wirtualna", 90, HorizontalAlignment.Center)

    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub

  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvKlasa.SelectedItems(0).Index + 1 & " z " & lvKlasa.Items.Count
      With DT.Select("Kod_Klasy='" & Name & "'")(0)
        'MessageBox.Show(GlobalValues.Users.Item(.Item("User").ToString.Trim).ToString & vbNewLine & GlobalValues.Users.Item(.Item("Owner").ToString.Trim).ToString)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.Trim).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.Trim).ToString, ")") '.Item(3).ToString
        lblIP.Text = .Item(4).ToString
        lblData.Text = .Item(5).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvKlasa_DoubleClick(sender As Object, e As EventArgs) Handles lvKlasa.DoubleClick
    EditKlasa()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvKlasa.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("Kod_Klasy=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
      GetDetails(e.Item.Text)
      'EnableButtons(True)
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    cmdEdit.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvKlasa.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, PK As New PrzydzialKlasSQL
      Try
        For Each Item As ListViewItem In Me.lvKlasa.SelectedItems
          DeletedIndex = Item.Index
          DBA.ApplyChanges(PK.DeleteClass(My.Settings.IdSchool, Item.Text))
        Next
        Me.EnableButtons(False)
        'If My.Application.OpenForms.OfType(Of dlgPrzydzialKlas)().Any() Then RaiseEvent KlasaRemoved()
        FetchData()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvKlasa, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)

      End Try
    End If

  End Sub
  Private Sub AddKlasa()
    Dim dlgAddNew As New dlgPrzydzialKlas
    dlgAddNew.Text = "Przydział oddziału do szkoły"
    'dlgAddNew.IdSzkola = My.Settings.IdSchool
    ListViewConfig(dlgAddNew.lvKlasa)
    dlgAddNew.lvKlasa.Columns.Add("Kod", 0, HorizontalAlignment.Left)
    dlgAddNew.lvKlasa.Columns.Add("Kod oddziału", 160, HorizontalAlignment.Center)
    dlgAddNew.Name = "dlgAddNew"
    'dlgAddNew.MdiParent = Me.MdiParent
    dlgAddNew.MaximizeBox = False

    AddHandler dlgAddNew.NewAdded, AddressOf NewAdded

    Me.cmdAddNew.Enabled = False
    dlgAddNew.ShowDialog()
    Me.cmdAddNew.Enabled = True

  End Sub
  Private Sub EditKlasa()
    Dim dlgEdit As New dlgPrzydzialKlas1
    With dlgEdit
      '.InRefresh = True
      .txtKod.Text = Me.lvKlasa.SelectedItems(0).Text
      .chkVirtual.Checked = CType(IIf(Me.lvKlasa.SelectedItems(0).SubItems(2).Text = "x", True, False), Boolean)
      .txtNazwa.Text = lvKlasa.SelectedItems(0).SubItems(1).Text
      '.InRefresh = False
      If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
        Dim DBA As New DataBaseAction, PK As New PrzydzialKlasSQL

        Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()

        Try
          Dim cmd As MySqlCommand = DBA.CreateCommand(PK.UpdateSchoolClass)
          cmd.Transaction = MySQLTrans

          cmd.Parameters.AddWithValue("?Nazwa_Klasy", .txtNazwa.Text.Trim)
          cmd.Parameters.AddWithValue("?Kod_Klasy", .txtKod.Text)
          cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
          cmd.Parameters.AddWithValue("?Virtual", .chkVirtual.Checked)
          cmd.ExecuteNonQuery()
          MySQLTrans.Commit()
          FetchData()
          GetData()
          Me.EnableButtons(False)
          Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvKlasa, .txtKod.Text)
        Catch ex As MySqlException
          MessageBox.Show(ex.Message)
          MySQLTrans.Rollback()
        Catch ex1 As Exception
          MessageBox.Show(ex1.Message)
        End Try
      End If
    End With
  End Sub
  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    FetchData()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvKlasa, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    'If Not My.Application.OpenForms.OfType(Of dlgPrzydzialKlas)().Any() Then 
    AddKlasa()
  End Sub

  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    EditKlasa()
  End Sub

End Class

