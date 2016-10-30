Imports belfer.NET.GlobalValues
Public Class frmZagrozenia
  Private DS As DataSet, InRefresh As Boolean
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ZagrozeniaToolStripMenuItem.Enabled = True
    MainForm.cmdZagrozenia.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ZagrozeniaToolStripMenuItem.Enabled = True
    MainForm.cmdZagrozenia.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  'nie dziala, collate nie dziala

  Private Sub FetchData()
    Dim DBA As New DataBaseAction, Z As New ZagrozeniaSQL
    DS = DBA.GetDataSet(Z.SelectPrzedmiot(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear) & Z.SelectPrzedmiot(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear, CType(nudSemestr.Value, Integer).ToString))
  End Sub
  Private Sub GetData()
    lvZagrozone.Items.Clear()
    lvNiezagrozone.Items.Clear()
    Me.cmdDelete.Enabled = False
    Me.cmdAdd.Enabled = False
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
    Me.lblUser.Text = ""
    If InRefresh Then Exit Sub
    Dim FoundPrzedmiot() As DataRow, FilterZagr As String = ""
    Try
      FoundPrzedmiot = DS.Tables(1).Select("IdPrzydzial=" + Me.lvUczen.SelectedItems(0).Text)
      For Each Item As DataRow In FoundPrzedmiot
        lvZagrozone.Items.Add(Item.Item(0).ToString)
        lvZagrozone.Items(lvZagrozone.Items.Count - 1).SubItems.Add(Item.Item(1).ToString)
        FilterZagr += Item.Item(5).ToString + ","
      Next
      FoundPrzedmiot = DS.Tables(0).Select(IIf(FilterZagr.Length > 0, "ID NOT IN (" & FilterZagr.TrimEnd(",".ToCharArray) & ")", "").ToString)
      For Each Item As DataRow In FoundPrzedmiot
        lvNiezagrozone.Items.Add(Item.Item(0).ToString)
        lvNiezagrozone.Items(lvNiezagrozone.Items.Count - 1).SubItems.Add(Item.Item(1).ToString)
      Next
      Me.lvZagrozone.Enabled = CBool(IIf(Me.lvZagrozone.Items.Count > 0, True, False))
      Me.lvNiezagrozone.Enabled = CBool(IIf(Me.lvNiezagrozone.Items.Count > 0, True, False))
      lvZagrozone.Columns(1).Width = CInt(IIf(lvUczen.Items.Count > 22, 226, 244))
      lvNiezagrozone.Columns(1).Width = CInt(IIf(lvUczen.Items.Count > 22, 226, 244))

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left, 2, Panel1.Width)
  End Sub
  Private Sub frmZagrozenia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    ListViewConfig(lvUczen)
    ListViewConfig(Me.lvNiezagrozone)
    ListViewConfig(Me.lvZagrozone)
    Dim CH As New CalcHelper
    Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    ApplyNewConfig()

  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      '.Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .HideSelection = False
      AddColumns(lv)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      If .Name = "lvUczen" Then
        .Columns.Add("Nr", 30, HorizontalAlignment.Center)
        .Columns.Add("Nazwisko i imiê", 138, HorizontalAlignment.Left)
      Else
        .Columns.Add("Przedmioty " + IIf(lv.Name = "lvNiezagrozone", "niezagro¿one", "zagro¿one").ToString, 244, HorizontalAlignment.Left)
      End If
    End With
  End Sub
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub ApplyNewConfig()
    'If Not My.Application.OpenForms.OfType(Of frmObsada)().Any() Then Exit Sub
    'RaiseEvent KonfiguracjaChanged()
    cmdAdd.Enabled = False
    'cmdEdit.Enabled = False
    cmdDelete.Enabled = False
    lvUczen.Items.Clear()
    lvNiezagrozone.Items.Clear()
    lvZagrozone.Items.Clear()
    lvUczen.Enabled = False
    lvZagrozone.Enabled = False
    lvNiezagrozone.Enabled = False
    'FetchData()
    'Dim CH As New CalcHelper
    'Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    FillKlasa()

  End Sub
  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, O As New ObsadaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, "0"))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
    Dim SH As New SeekHelper
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
  End Sub


  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    Me.lvNiezagrozone.Items.Clear()
    lvNiezagrozone.Enabled = False
    Me.lvZagrozone.Items.Clear()
    lvZagrozone.Enabled = False
    Me.GetStudent(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString)
    nudSemestr_ValueChanged(Nothing, Nothing)
  End Sub
  Private Sub GetStudent(Klasa As String)
    Me.lvUczen.Items.Clear()
    Me.InRefresh = True
    Dim Reader As MySqlDataReader, DBA As New DataBaseAction, Z As New ZagrozeniaSQL  ', CS As New CommonStrings
    Reader = DBA.GetReader(Z.SelectStudent(Klasa, My.Settings.SchoolYear))
    Try
      While Reader.Read
        lvUczen.Items.Add(Reader.Item(0).ToString)
        lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(Reader.Item(1).ToString)
        lvUczen.Items(lvUczen.Items.Count - 1).ToolTipText = Reader.Item(1).ToString
        lvUczen.Items(lvUczen.Items.Count - 1).SubItems.Add(Reader.Item(2).ToString)

      End While
      Me.lvUczen.Enabled = CBool(IIf(Me.lvUczen.Items.Count > 0, True, False))
      lvUczen.Columns(2).Width = CInt(IIf(lvUczen.Items.Count > 22, 119, 138))
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      Reader.Close()
    End Try
  End Sub

  Private Sub nudSemestr_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSemestr.ValueChanged
    If Not Me.Created OrElse cbKlasa.SelectedItem Is Nothing Then Exit Sub
    Me.FetchData()
    If lvUczen.SelectedItems.Count > 0 Then GetData()
  End Sub

  Private Sub lvUczen_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvUczen.ItemSelectionChanged
    If e.IsSelected Then
      Me.InRefresh = False
      Me.GetData()
    Else
      Me.InRefresh = True
    End If
  End Sub
  Private Sub AddNew()
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      Dim DBA As New DataBaseAction, Z As New ZagrozeniaSQL
      For Each Uczen As ListViewItem In Me.lvUczen.SelectedItems
        For Each Przedmiot As ListViewItem In Me.lvNiezagrozone.SelectedItems
          Dim cmd As MySqlCommand = DBA.CreateCommand(Z.InsertPrzedmiot)
          'DBA.ApplyChanges("")
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?IdPrzydzial", Uczen.Text)
          cmd.Parameters.AddWithValue("?IdPrzedmiot", Przedmiot.Text)
          cmd.Parameters.AddWithValue("?Semestr", CType(nudSemestr.Value, Integer))
          cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
          cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
          cmd.ExecuteNonQuery()
        Next
      Next
      MySQLTrans.Commit()
      Me.FetchData()
      Me.GetData()
    Catch mex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub Delete()
    Dim DBA As New DataBaseAction, Z As New ZagrozeniaSQL, DeletedIndex As Integer
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      For Each Przedmiot As ListViewItem In Me.lvZagrozone.SelectedItems
        DeletedIndex = Przedmiot.Index
        Dim cmd As MySqlCommand = DBA.CreateCommand(Z.DeletePrzedmiot)
        cmd.Parameters.AddWithValue("?ID", Przedmiot.Text)
        cmd.Transaction = MySQLTrans
        cmd.ExecuteNonQuery()
        'DBA.ApplyChanges("DELETE FROM zagrozenia WHERE ID=" + Przedmiot.Text + ";")
      Next
      MySQLTrans.Commit()
      FetchData()
      Me.GetData()
      Dim SH As New SeekHelper
      'cmdDelete.Enabled = False
      SH.FindPostRemovedListViewItemIndex(Me.lvZagrozone, DeletedIndex)
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      MySQLTrans.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
    Me.AddNew()
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    Me.Delete()
  End Sub

  Private Sub lvNiezagrozone_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvNiezagrozone.DoubleClick
    If AppUser.Role = Role.Administrator OrElse AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString Then Me.AddNew()
  End Sub

  Private Sub lvNiezagrozone_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvNiezagrozone.ItemSelectionChanged
    If AppUser.Role = Role.Administrator OrElse AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString Then
      Me.cmdAdd.Enabled = CBool(IIf(e.IsSelected, True, False))
    End If
  End Sub

  Private Sub lvZagrozone_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvZagrozone.DoubleClick
    If AppUser.Role = Role.Administrator OrElse AppUser.TutorClassID = CType(cbKlasa.SelectedItem, CbItem).ID.ToString Then Me.Delete()
  End Sub


  Private Sub lvZagrozone_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvZagrozone.ItemSelectionChanged
    If e.IsSelected Then
      Dim FoundPrzedmiot() As DataRow
      FoundPrzedmiot = DS.Tables(1).Select("ID=" + Me.lvZagrozone.SelectedItems(0).Text)
      Me.cmdDelete.Enabled = CType(IIf(AppUser.Role = Role.Administrator OrElse GlobalValues.AppUser.Login = FoundPrzedmiot(0).Item("Owner").ToString.Trim, True, False), Boolean)
      'Me.lblUser.Text = String.Concat(GlobalValues.Users.Item(FoundPrzedmiot(0).Item("User").ToString.ToLower).ToString, " (W³: ", GlobalValues.Users.Item(FoundPrzedmiot(0).Item("Owner").ToString.ToLower).ToString, ")")
      Dim User, Owner As String
      User = CType(FoundPrzedmiot(0).Item("User"), String).ToLower.Trim
      Owner = CType(FoundPrzedmiot(0).Item("Owner"), String).ToLower.Trim
      If Users.ContainsKey(User) AndAlso Users.ContainsKey(Owner) Then
        lblUser.Text = String.Concat(Users.Item(User).ToString, " (W³: ", Users.Item(Owner).ToString, ")")
      Else
        Me.lblUser.Text = User & " (W³: " & Owner & ")"
      End If
      Me.lblIP.Text = FoundPrzedmiot(0).Item("ComputerIP").ToString
      Me.lblData.Text = FoundPrzedmiot(0).Item("Version").ToString
    Else
      Me.cmdDelete.Enabled = False
      Me.lblUser.Text = ""
      Me.lblIP.Text = ""
      Me.lblData.Text = ""
    End If
  End Sub

  Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    'ShowMDIChild(prnZagrozenia, Me.ParentForm)
    Dim prn As New prnZagrozenia
    With prn
      .Icon = GlobalValues.gblAppIcon
      '.MdiParent = Me
      '.MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      '.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      cmdPrint.Enabled = False
      .ShowDialog()
      cmdPrint.Enabled = True
    End With
  End Sub

  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
  End Sub

End Class