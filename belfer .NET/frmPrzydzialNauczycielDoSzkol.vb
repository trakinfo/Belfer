Public Class frmPrzydzialNauczycieliDoSzkol
  Private DT As DataTable ', IsOpen As Boolean
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PrzydzialNauczycieliDoSzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PrzydzialNauczycieliDoSzkolToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmPrzydzialKlasDoSzkol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    ListViewConfig(lvNauczyciel)
    AddColumns(lvNauczyciel)
    lblRecord.Text = ""
    ApplyNewConfig()

  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvNauczyciel.Items.Clear()
    FetchData()
    GetData()
    ClearDetails()
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, PN As New PrzydzialNauczycieliSQL
    DT = DBA.GetDataTable(PN.SelectSchoolTeachers(My.Settings.IdSchool))
  End Sub
  Private Sub GetData()
    lvNauczyciel.Items.Clear()
    For Each row As DataRow In DT.Rows
      Me.lvNauczyciel.Items.Add(row.Item(0).ToString)
      Me.lvNauczyciel.Items(Me.lvNauczyciel.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      'Me.lvNauczyciel.Items(Me.lvNauczyciel.Items.Count - 1).SubItems.Add([Enum].GetName(GetType(GlobalValues.Status), CType(row.Item(2), Integer)))
      Me.lvNauczyciel.Items(Me.lvNauczyciel.Items.Count - 1).SubItems.Add(CType(row.Item(2), GlobalValues.UserStatus).ToString)
    Next

    lvNauczyciel.Columns(1).Width = CInt(IIf(lvNauczyciel.Items.Count > 14, 321, 340))
    Me.lvNauczyciel.Enabled = CType(IIf(Me.lvNauczyciel.Items.Count > 0, True, False), Boolean)
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
      .Columns.Add("IdPrzydział", 0, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię", 340, HorizontalAlignment.Left)
      .Columns.Add("Status", 100, HorizontalAlignment.Center)
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub

  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvNauczyciel.SelectedItems(0).Index + 1 & " z " & lvNauczyciel.Items.Count
      With DT.Select("ID='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(3).ToString
        lblIP.Text = .Item(4).ToString
        lblData.Text = .Item(5).ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub


  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvNauczyciel.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      cmdEdit.Text = IIf(CType(DT.Select("ID=" & e.Item.Text)(0).Item("Status"), Integer) = 1, "Deaktywuj", "Aktywuj").ToString
      'EnableButtons(True)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = DT.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    cmdEdit.Enabled = Value
    Me.cmdDelete.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvNauczyciel.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, PN As New PrzydzialNauczycieliSQL, Trans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction

      Try
        For Each Item As ListViewItem In Me.lvNauczyciel.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(PN.DeleteSchoolTeacher)
          cmd.Transaction = Trans
          cmd.Parameters.AddWithValue("?IdPrzydzial", Item.Text)
          cmd.ExecuteNonQuery()
          'DBA.ApplyChanges(PN.DeleteSchoolTeacher(CType(cbSzkola.SelectedItem, CbItem).ID.ToString, Item.Text))
        Next
        Trans.Commit()

        Me.EnableButtons(False)
        'If My.Application.OpenForms.OfType(Of dlgPrzydzialNauczycieli)().Any() Then RaiseEvent NauczycielRemoved()
        FetchData()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvNauczyciel, DeletedIndex)
      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If
  End Sub
  Private Sub AddKlasa()
    Dim dlgAddNew As New dlgPrzydzialNauczycieli
    dlgAddNew.Text = "Przydział nauczyciela do szkoły"
    'dlgAddNew.IdSzkola = CType(cbSzkola.SelectedItem, CbItem).ID.ToString
    ListViewConfig(dlgAddNew.lvNauczyciel)
    'AddColumns(dlgAddNew.lvNauczyciel)
    With dlgAddNew.lvNauczyciel
      .Columns.Add("IdPrzydział", 0, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię", 325, HorizontalAlignment.Left)
    End With
    'dlgAddNew.MdiParent = Me.MdiParent
    dlgAddNew.MaximizeBox = False
    'dlgAddNew.StartPosition = FormStartPosition.CenterScreen
    'dlgAddNew.StartPosition = FormStartPosition.Manual
    'dlgAddNew.Location = New Point(Me.Location.X + Me.Width, Me.Location.Y)
    'dlgAddNew.Opacity = 0.7
    Me.cmdAddNew.Enabled = False
    AddHandler dlgAddNew.NewAdded, AddressOf NewAdded
    dlgAddNew.ShowDialog()
    cmdAddNew.Enabled = True
    'IsOpen = True
    'AddHandler SchoolChanged, AddressOf dlgAddNew.SchoolChanged
    'AddHandler NauczycielRemoved, AddressOf dlgAddNew.GetData

    'AddHandler dlgAddNew.Disposed, AddressOf EnableCmdAddNew
  End Sub

  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    FetchData()
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvNauczyciel, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddKlasa()
  End Sub

  'Private Sub cbTypSzkoly_SelectedIndexChanged(sender As Object, e As EventArgs)
  '  My.Settings.IdSchoolType = CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString
  '  My.Settings.IdSchool = ""
  '  My.Settings.Save()
  '  Dim FCB As New FillComboBox, S As New SzkolaSQL, SH As New SeekHelper
  '  FCB.AddComboBoxComplexItems(Me.cbSzkola, S.SelectSchoolAlias(CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString))
  '  If My.Settings.IdSchool.Length > 0 Then
  '    SH.FindComboItem(Me.cbSzkola, CType(My.Settings.IdSchool, Integer))
  '  Else
  '    SH.FindComboItem(Me.cbSzkola, CInt(SH.GetDefault(S.SelectDefault(My.Settings.IdSchoolType))))
  '  End If
  '  cbSzkola.Enabled = CType(IIf(cbSzkola.Items.Count > 0, True, False), Boolean)
  'End Sub

  Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
    If MessageBox.Show("Wybrany nauczyciel zostanie deaktywowany.", "Belfer .NET", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
      Dim DBA As New DataBaseAction, PN As New PrzydzialNauczycieliSQL, MySQLTrans As MySqlTransaction = Nothing, ID As String = ""
      Dim cmd As MySqlCommand = DBA.CreateCommand(PN.Aktywacja)
      Try
        For Each Item As ListViewItem In lvNauczyciel.SelectedItems
          ID = Item.Text
          MySQLTrans = GlobalValues.gblConn.BeginTransaction()
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.Parameters.AddWithValue("?Status", Not CType(DT.Select("ID=" & Item.Text)(0).Item("Status"), Boolean))
        Next
        cmd.ExecuteNonQuery()
        MySQLTrans.Commit()
      Catch ex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(ex.Message)
      End Try
      Me.FetchData()
      GetData()
      Me.EnableButtons(False)
      Dim SH As New SeekHelper
      SH.FindListViewItem(Me.lvNauczyciel, ID)
    End If
  End Sub
End Class

