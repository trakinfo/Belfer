Public Class frmUser
  Private dtUsers As DataTable, Filter As String = ""
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ZarzadzanieuzytkownikamiToolStripMenuItem.Enabled = True

  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ZarzadzanieuzytkownikamiToolStripMenuItem.Enabled = True

  End Sub
  Public Sub AddUser()
    Dim HashHelper As New HashHelper, DBA As New DataBaseAction, U As New UsersSQL, dlgAddNew As New dlgUser
    Try
      For Each i As Integer In [Enum].GetValues(GetType(GlobalValues.Role))
        dlgAddNew.cbRola.Items.Add(New CbItem(i, [Enum].GetName(GetType(GlobalValues.Role), i)))
      Next
      Dim FCB As New FillComboBox
      FCB.AddComboBoxComplexItems(dlgAddNew.cbNauczyciel, U.SelectBelfer)
      If dlgAddNew.ShowDialog = Windows.Forms.DialogResult.OK Then
        Dim SaltedPasswordHash As Byte()
        SaltedPasswordHash = HashHelper.CreateDBPassword(dlgAddNew.txtPassword.Text)
        Dim cmd As MySqlCommand = DBA.CreateCommand(U.InsertUser())

        cmd.Parameters.AddWithValue("?Password", SaltedPasswordHash)
        cmd.Parameters.AddWithValue("?Login", dlgAddNew.txtLogin.Text.Trim)
        cmd.Parameters.AddWithValue("?Nazwisko", dlgAddNew.txtNazwisko.Text.Trim)
        cmd.Parameters.AddWithValue("?Imie", dlgAddNew.txtImie.Text.Trim)
        cmd.Parameters.AddWithValue("?Email", dlgAddNew.txtEmail.Text.Trim)
        cmd.Parameters.AddWithValue("?Role", CType(dlgAddNew.cbRola.SelectedItem, CbItem).ID)
        cmd.Parameters.AddWithValue("?Status", CType(dlgAddNew.chkStatus.Checked, Boolean))
        If dlgAddNew.cbNauczyciel.SelectedItem IsNot Nothing Then
          cmd.Parameters.AddWithValue("?IdNauczyciel", CType(dlgAddNew.cbNauczyciel.SelectedItem, CbItem).ID)
        Else
          cmd.Parameters.AddWithValue("?IdNauczyciel", DBNull.Value)
        End If
        cmd.ExecuteNonQuery()
        Me.lvUsers.Items.Clear()
        Me.GetData()
        Dim SH As New SeekHelper
        SH.FindListViewItem(Me.lvUsers, DBA.GetLastInsertedID)
      End If
    Catch err As MySqlException
      Select Case err.Number
        Case 1062
          MessageBox.Show("Podany użytkownik już istnieje." + vbNewLine + "Spróbuj innej nazwy.")
        Case Else
          MessageBox.Show(err.Message + vbNewLine + "Numer błędu: " + err.Number.ToString)
      End Select
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub

  Private Sub cmdAddNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNewUser.Click
    Me.AddUser()
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.ListViewConfig(Me.lvUsers)
    Dim SeekCriteria() As String = New String() {"Login", "Nazwisko i imię", "Rola", "Status"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    Me.GetData()
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
      .Columns.Add("Login", 100, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię", 200, HorizontalAlignment.Left)
      .Columns.Add("Adres e-mail", 125, HorizontalAlignment.Left)
      .Columns.Add("Rola", 80, HorizontalAlignment.Left)
      .Columns.Add("Status", 70, HorizontalAlignment.Left)
      .Columns.Add("Nauczyciel", 200, HorizontalAlignment.Left)
    End With
  End Sub


  Private Sub GetData()
    Dim DBA As New DataBaseAction, U As New UsersSQL
    dtUsers = DBA.GetDataTable(U.SelectUsers)
    For Each row As DataRow In dtUsers.Select(Filter)
      'For Each row As DataRow In dtUsers.Rows
      Me.lvUsers.Items.Add(row.Item(0).ToString)
      Me.lvUsers.Items(Me.lvUsers.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      Me.lvUsers.Items(Me.lvUsers.Items.Count - 1).SubItems.Add(row.Item(2).ToString)
      Me.lvUsers.Items(Me.lvUsers.Items.Count - 1).SubItems.Add([Enum].GetName(GetType(GlobalValues.Role), CType(row.Item(3), Integer)))
      Me.lvUsers.Items(Me.lvUsers.Items.Count - 1).SubItems.Add([Enum].GetName(GetType(GlobalValues.UserStatus), CType(row.Item(4), Integer)))
      Me.lvUsers.Items(Me.lvUsers.Items.Count - 1).SubItems.Add(row.Item("Belfer").ToString)
    Next
    lblRecord.Text = "0 z " & lvUsers.Items.Count
    lvUsers.Columns(1).Width = CInt(IIf(lvUsers.Items.Count > 23, 181, 200))
    Me.lvUsers.Enabled = CType(IIf(Me.lvUsers.Items.Count > 0, True, False), Boolean)

  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvUsers.SelectedItems(0).Index + 1 & " z " & lvUsers.Items.Count

      With dtUsers.Select("Login='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item("User").ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvUsers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvUsers.DoubleClick
    Me.EditData()
  End Sub

  Private Sub lvUsers_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvUsers.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator OrElse GlobalValues.AppUser.Login = dtUsers.Select("ID=" & e.Item.Text)(0).Item("Owner").ToString.Trim Then EnableButtons(True)
      'EnableButtons(True)
    Else
      EnableButtons(False)
    End If
  End Sub
  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    Me.cmdEdit.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvUsers.Items.Count

    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub
  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Zaznaczeni użytkownicy zostaną bezpowrotnie usunięci z bazy danych." & vbNewLine & "Czy na pewno chcesz to zrobić? Jeśli tak, to naciśnij przycisk 'OK'. W przeciwnym razie wciśnij przycisk 'Anuluj'.", My.Application.Info.ProductName, MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
      Dim DBA As New DataBaseAction, DeletedIndex As Integer, U As New UsersSQL
      For Each Item As ListViewItem In Me.lvUsers.SelectedItems
        DeletedIndex = Item.Index
        Dim cmd As MySqlCommand = DBA.CreateCommand(U.DeleteUser)
        cmd.Parameters.AddWithValue("?Login", Item.Text)
        cmd.ExecuteNonQuery()
        'DBA.ApplyChanges(U.DeleteUser(Item.Text))
      Next
      Me.EnableButtons(False)
      lvUsers.Items.Clear()
      Me.GetData()
      Dim SH As New SeekHelper
      SH.FindPostRemovedListViewItemIndex(Me.lvUsers, DeletedIndex)
    End If

  End Sub

  Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
    Me.EditData()
  End Sub
  Private Sub EditData()
    Dim dlgEdit As New dlgUser
    Try
      With dlgEdit
        .Text = "Edycja danych użytkownika"
        .txtLogin.Text = Me.lvUsers.SelectedItems(0).Text
        .txtNazwisko.Text = Me.lvUsers.SelectedItems(0).SubItems(1).Text.Split(" ".ToCharArray)(0).ToString
        .txtImie.Text = Me.lvUsers.SelectedItems(0).SubItems(1).Text.Split(" ".ToCharArray)(1).ToString
        .txtEmail.Text = Me.lvUsers.SelectedItems(0).SubItems(2).Text
        For Each i As Integer In [Enum].GetValues(GetType(GlobalValues.Role))
          .cbRola.Items.Add(New CbItem(i, [Enum].GetName(GetType(GlobalValues.Role), i)))
        Next

        .chkStatus.Checked = CType(CType(System.Enum.Parse(GetType(GlobalValues.UserStatus), Me.lvUsers.SelectedItems(0).SubItems(4).Text), GlobalValues.UserStatus), Boolean)
        Dim SH As New SeekHelper, U As New UsersSQL
        SH.FindComboItem(.cbRola, Me.lvUsers.SelectedItems(0).SubItems(3).Text)
        Dim FCB As New FillComboBox
        FCB.AddComboBoxComplexItems(.cbNauczyciel, U.SelectBelfer)

        If CType(.cbRola.SelectedItem, CbItem).ID > 0 Then
          If Me.lvUsers.SelectedItems(0).SubItems(5).Text.Length > 0 Then SH.FindComboItem(.cbNauczyciel, Me.lvUsers.SelectedItems(0).SubItems(5).Text)
          .cbNauczyciel.Enabled = True
        Else
          .cbNauczyciel.Enabled = False
        End If
        .txtLogin.Enabled = False
        .lblPassword.Enabled = False
        .lblPassword2.Enabled = False
        .txtPassword.Enabled = False
        .txtPassword1.Enabled = False
        .OK_Button.Text = "Zapisz"
        '.Icon = gblAppIcon
        If dlgEdit.ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim DBA As New DataBaseAction, Login As String ', U As New UsersSQL
          Login = Me.lvUsers.SelectedItems(0).Text
          Dim cmd As MySqlCommand = DBA.CreateCommand(U.UpdateUser())

          cmd.Parameters.AddWithValue("?Nazwisko", .txtNazwisko.Text)
          cmd.Parameters.AddWithValue("?Imie", .txtImie.Text)
          cmd.Parameters.AddWithValue("?Email", .txtEmail.Text)
          cmd.Parameters.AddWithValue("?Role", CType(.cbRola.SelectedItem, CbItem).ID)
          cmd.Parameters.AddWithValue("?Status", CType(.chkStatus.Checked, Boolean))
          cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
          cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
          cmd.Parameters.AddWithValue("?Login", Login)

          If .cbNauczyciel.SelectedItem IsNot Nothing Then
            cmd.Parameters.AddWithValue("?IdNauczyciel", CType(.cbNauczyciel.SelectedItem, CbItem).ID)
          Else
            cmd.Parameters.AddWithValue("?IdNauczyciel", DBNull.Value)
          End If

          cmd.ExecuteNonQuery()
          Me.lvUsers.Items.Clear()
          Me.GetData()

          'Dim SH As New SeekHelper
          SH.FindListViewItem(Me.lvUsers, Login)
        End If
      End With
    Catch err As MySqlException
      Select Case err.Number
        Case 1062
          MessageBox.Show("Podany użytkownik już istnieje." + vbNewLine + "Spróbuj innej nazwy.")
        Case Else
          MessageBox.Show(err.Message + vbNewLine + "Numer błędu: " + err.Number.ToString)
      End Select
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Cursor = Cursors.WaitCursor
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię"
        Filter = "UserName LIKE '" & Me.txtSeek.Text + "%'"
      Case "Login"
        Filter = "Login LIKE '" & Me.txtSeek.Text + "%'"
      Case "Rola"
        Dim Rola As GlobalValues.Role
        If [Enum].TryParse(Me.txtSeek.Text, True, Rola) Then
          Filter = "Role=" & Rola
        Else
          Filter = ""
        End If
      Case "Status"
        Dim Status As GlobalValues.UserStatus
        If [Enum].TryParse(Me.txtSeek.Text, True, Status) Then
          Filter = "Status=" & Status
        Else
          Filter = ""
        End If
        'Filter = "Status LIKE '" & Me.txtSeek.Text + "%'"
    End Select
    Me.EnableButtons(False)
    Me.lvUsers.Items.Clear()
    Me.GetData()
    Cursor = Cursors.Default
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub
End Class