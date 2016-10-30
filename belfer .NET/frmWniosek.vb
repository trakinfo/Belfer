Imports System.Security.Cryptography
Imports System.Drawing.Printing
Public Class frmWniosek
  Public Event NewRow()
  Private Offset(1), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean

  Private Filter As String = "", dsWniosek As DataSet, Status As String = "Status IN (1)", Typ As String = " AND ApplyType IN ('NA','AP','RP')"
  Private ApplyType As New Hashtable From {{"NA", "Utworzenie nowego konta"}, {"AP", "Dodanie uprawnienia"}, {"RP", "Odzyskanie hasła"}}
  Private RegisterStatus As New Hashtable From {{"1", "Zarejestrowane"}, {"2", "Zrealizowane"}, {"3", "Zrealizowane, ale wystąpiły błędy"}, {"4", "Odrzucone - konto o podanych parametrach już istnieje"}, {"5", "Hasło wysłane pocztą e-mail"}, {"6", "Hasło przekazane wychowawcy"}, {"7", "Zweryfikowane pozytywnie"}, {"8", "Przygotowane do przekazania"}, {"9", "Odrzucone - nie znaleziono konta o podanych parametrach"}}
  Private ErrCode As New Hashtable From {{"0", "Zadanie nie zostało przetworzone"}, {"1", "Przetwarzanie zakończone powodzeniem"}, {"2", "Nieprawidłowy nr PESEL"}, {"3", "Niezgodne dane ucznia"}, {"4", "Nie znaleziono ucznia"}}

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ApplicationToolStripMenuItem.Enabled = True
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ApplicationToolStripMenuItem.Enabled = True
  End Sub
  Private Sub frmWniosek_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'RegisterStatus = New Hashtable From {{"NA", "Utwórz konto"}, {"AS", "Dopisz ucznia"}, {"RP", "Odzyskanie hasła"}}
    ListViewConfig(lvWniosek)
    AddColumns(lvWniosek)
    ListViewConfig(lvSubwniosek)
    AddColumns1(lvSubwniosek)
    Dim SeekCriteria() As String = New String() {"Nazwisko i imię zgłaszającego", "Login zgłaszającego", "Data zgłoszenia", "Nazwisko i imię ucznia", "Pesel ucznia"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    FetchData()
    GetData()
  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Close()
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
      .Columns.Add("ID", 0, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię", 200, HorizontalAlignment.Left)
      .Columns.Add("Typ zgłoszenia", 150, HorizontalAlignment.Center)
      .Columns.Add("Data wpływu", 120, HorizontalAlignment.Center)
      .Columns.Add("Status", 200, HorizontalAlignment.Center)
      .Columns.Add("Login", 80, HorizontalAlignment.Center)
      .Columns.Add("E-mail", 200, HorizontalAlignment.Left)
      .Columns.Add("IP", 150, HorizontalAlignment.Center)
    End With
  End Sub
  Private Sub AddColumns1(ByVal lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię", 200, HorizontalAlignment.Left)
      .Columns.Add("Pesel", 200, HorizontalAlignment.Center)
      .Columns.Add("Status", 448, HorizontalAlignment.Center)

    End With
  End Sub
  Private Sub FetchData()
    Try
      Dim DBA As New DataBaseAction, W As New WniosekSQL
      dsWniosek = DBA.GetDataSet(W.SelectWniosek & W.SelectSubwniosek)
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub
  Private Sub GetData()
    lvSubwniosek.Items.Clear()
    lvSubwniosek.Enabled = False
    lvWniosek.Items.Clear()
    lvWniosek.Enabled = False
    EnableCommands(False)

    For Each row As DataRow In dsWniosek.Tables(0).Select(Status & Typ & Filter)
      'Dim NewItem As New ListViewItem(row.Item(0).ToString)
      'NewItem.UseItemStyleForSubItems = True
      Me.lvWniosek.Items.Add(row.Item(0).ToString)
      'NewItem.SubItems.Add(row.Item(1).ToString)
      Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(ApplyType.Item(row.Item(2).ToString).ToString).Tag = row.Item(2).ToString
      Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(row.Item(3).ToString)
      Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(RegisterStatus.Item(row.Item(4).ToString).ToString).Tag = row.Item(4).ToString
      'Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add([Enum].GetName(GetType(RegisterStatus), CType(row.Item(4), Integer)))
      Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(row.Item(6).ToString)
      Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(row.Item(5).ToString)
      'Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(ChrW(&HFC) & ChrW(&H44)).Font = New Font("Wingdings", 12, FontStyle.Regular)
      Me.lvWniosek.Items(Me.lvWniosek.Items.Count - 1).SubItems.Add(row.Item(7).ToString)
    Next
    lblRecord.Text = "0 z " & lvWniosek.Items.Count
    'lvWniosek.Columns(1).Width = CInt(IIf(lvUsers.Items.Count > 23, 281, 300))
    Me.lvWniosek.Enabled = CType(IIf(Me.lvWniosek.Items.Count > 0, True, False), Boolean)

  End Sub

  Private Sub lvStudent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvWniosek.ItemSelectionChanged
    lvSubwniosek.Items.Clear()
    lvSubwniosek.Enabled = False
    EnableCommands(False)
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      GetContents(lvSubwniosek, e.Item.Text)
      cmdDelete.Enabled = True
      Select Case CType(e.Item.SubItems(4).Tag, Integer)
        Case 1
          cmdVerify.Enabled = True
        Case 7
          cmdExecute.Enabled = True
        Case 2, 8
          cmdPrint.Enabled = True
      End Select

    Else

      ClearDetails()
    End If

  End Sub

  Private Sub lvSubwniosek_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvSubwniosek.ItemSelectionChanged
    EnableCommands(False)
    If e.IsSelected Then
      GetSubDetails(e.Item.Text)
      cmdDelete.Enabled = True
    Else
      ClearDetails()
    End If
  End Sub
  Private Sub GetContents(lv As ListView, IdWniosek As String)
    Try
      For Each Wynik As DataRow In dsWniosek.Tables(1).Select("IdWniosek=" & IdWniosek)
        Dim NewItem As New ListViewItem(Wynik.Item("ID").ToString)
        NewItem.UseItemStyleForSubItems = True
        NewItem.SubItems.Add(Wynik.Item("Student").ToString)
        NewItem.SubItems.Add(Wynik.Item("StudentPesel").ToString)
        NewItem.SubItems.Add(ErrCode.Item(Wynik.Item("Status").ToString).ToString)
        lv.Items.Add(NewItem)
      Next
      lv.Columns(3).Width = CInt(IIf(lv.Items.Count > 4, 428, 448))
      lv.Enabled = If(lv.Items.Count > 0, True, False)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try

  End Sub
  Sub GetDetails(ID As String)
    lblRecord.Text = lvWniosek.SelectedItems(0).Index + 1 & " z " & lvWniosek.Items.Count

    With dsWniosek.Tables(0).Select("ID=" & ID)(0)
      'lblUser.Text = .Item("User").ToString
      Dim User As String
      User = .Item("User").ToString.ToLower.Trim
      If GlobalValues.Users.ContainsKey(User) Then
        lblUser.Text = GlobalValues.Users.Item(User).ToString
      Else
        Me.lblUser.Text = User
      End If
      lblIP.Text = .Item("ComputerIP").ToString
      lblData.Text = .Item("Version").ToString
    End With
  End Sub
  Sub GetSubDetails(ID As String)
    With dsWniosek.Tables(1).Select("ID=" & ID)(0)
      Dim User As String
      User = .Item("User").ToString.ToLower.Trim
      If GlobalValues.Users.ContainsKey(User) Then
        lblUser.Text = GlobalValues.Users.Item(User).ToString
      Else
        Me.lblUser.Text = User
      End If
      lblIP.Text = .Item("ComputerIP").ToString
      lblData.Text = .Item("Version").ToString
    End With
  End Sub
  Sub ClearDetails()
    lblData.Text = ""
    lblIP.Text = ""
    lblUser.Text = ""
  End Sub
  Sub EnableCommands(Value As Boolean)
    cmdDelete.Enabled = Value
    cmdExecute.Enabled = Value
    cmdPrint.Enabled = Value
    cmdVerify.Enabled = Value
  End Sub

  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Cursor = Cursors.WaitCursor
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię zgłaszającego"
        Filter = " AND ParentName LIKE '" & Me.txtSeek.Text + "%'"
      Case "Login zgłaszającego"
        Filter = " AND ParentLogin LIKE '" & Me.txtSeek.Text + "%'"
      Case "Data zgłoszenia"
        Filter = " AND ApplyDate LIKE '" & Me.txtSeek.Text + "%'"
      Case "Nazwisko i imię ucznia"
        Dim IdWniosek As String = ""
        For Each R As DataRow In dsWniosek.Tables(1).Select("Student LIKE '" & Me.txtSeek.Text + "%'")
          IdWniosek += R.Item("IdWniosek").ToString + ","
        Next
        Filter = " AND ID IN (" & IdWniosek.TrimEnd(",".ToCharArray) & ")"
      Case "Pesel ucznia"
        Dim IdWniosek As String = ""
        For Each R As DataRow In dsWniosek.Tables(1).Select("StudentPesel LIKE '" & Me.txtSeek.Text + "%'")
          IdWniosek += R.Item("IdWniosek").ToString + ","
        Next
        Filter = " AND ID IN (" & IdWniosek.TrimEnd(",".ToCharArray) & ")"

    End Select
    'Me.EnableButtons(False)
    Me.lvWniosek.Items.Clear()
    Me.GetData()
    Cursor = Cursors.Default
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub

  Private Sub rbRegistered_CheckedChanged(sender As Object, e As EventArgs) Handles rbRegistered.CheckedChanged, rbCompleted.CheckedChanged, rbAllStatus.CheckedChanged, rbInProgress.CheckedChanged, rbRejected.CheckedChanged, rbVerified.CheckedChanged
    If CType(sender, RadioButton).Created = False Then Exit Sub
    If CType(sender, RadioButton).Checked Then
      Status = CType(sender, RadioButton).Tag.ToString
      GetData()
    End If
  End Sub

  Private Sub rbAllType_CheckedChanged(sender As Object, e As EventArgs) Handles rbAllType.CheckedChanged, rbNewAccount.CheckedChanged, rbAddPrivilege.CheckedChanged, rbRestorePassword.CheckedChanged
    If CType(sender, RadioButton).Created = False Then Exit Sub
    If CType(sender, RadioButton).Checked Then
      Typ = CType(sender, RadioButton).Tag.ToString
      GetData()
    End If
  End Sub

  Function CheckUserExist(Name() As String) As Boolean
    Dim W As New WniosekSQL, DBA As New DataBaseAction
    If DBA.ComputeRecords(W.CountUser(Name(0), Name(1))) > 0 Then Return True
    Return False
  End Function
  Function CheckUserExist(Name() As String, Login As String) As Boolean
    Dim W As New WniosekSQL, DBA As New DataBaseAction
    If DBA.ComputeRecords(W.CountUser(Name(0), Name(1), Login)) > 0 Then Return True
    Return False
  End Function
  Sub ChangeRegisterStatus(Status As Integer, IdApplication As Integer, MyTransaction As MySqlTransaction)
    Try
      Dim W As New WniosekSQL, DBA As New DataBaseAction
      Dim cmd As MySqlCommand = DBA.CreateCommand(W.UpdateStatus)
      cmd.Transaction = MyTransaction
      cmd.Parameters.AddWithValue("?ID", IdApplication)
      cmd.Parameters.AddWithValue("?Status", Status)
      cmd.ExecuteNonQuery()

    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Sub SetErrorStatus(Status As Integer, IdApplication As Integer, MyTransaction As MySqlTransaction)
    Try
      Dim W As New WniosekSQL, DBA As New DataBaseAction
      Dim cmd As MySqlCommand = DBA.CreateCommand(W.UpdateErrCode)
      cmd.Transaction = MyTransaction
      cmd.Parameters.AddWithValue("?ID", IdApplication)
      cmd.Parameters.AddWithValue("?Status", Status)
      cmd.ExecuteNonQuery()

    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Function CreateAccount(MyTransaction As MySqlTransaction, ParentName As String, ParentGivenName As String, ParentEmail As String) As String
    Dim Login As String = Nothing
    Try
      'Dim Name() As String
      'Name = lvWniosek.SelectedItems(0).SubItems(1).Text.Split(" ".ToCharArray)
      Dim dlgNewAccount As New dlgUser
      With dlgNewAccount
        For Each i As Integer In [Enum].GetValues(GetType(GlobalValues.Role))
          .cbRola.Items.Add(New CbItem(i, [Enum].GetName(GetType(GlobalValues.Role), i)))
        Next
        '.txtLogin.Text = String.Concat(If(Name(0).Length > 4, Name(0).Substring(0, 5), Name(0)), If(Name(1).Length > 4, Name(1).Substring(0, 5), Name(1))).ToLower
        '.txtNazwisko.Text = Name(0)
        '.txtImie.Text = Name(1)
        '.txtEmail.Text = lvWniosek.SelectedItems(0).SubItems(6).Text
        .txtLogin.Text = String.Concat(If(ParentName.Length > 4, ParentName.Substring(0, 5), ParentName), If(ParentGivenName.Length > 4, ParentGivenName.Substring(0, 5), ParentGivenName)).ToLower
        .txtNazwisko.Text = ParentName
        .txtImie.Text = ParentGivenName
        .txtEmail.Text = ParentEmail

        .chkStatus.Checked = False
        .cbRola.Text = GlobalValues.Role.Rodzic.ToString
        .txtPassword.Enabled = False
        .txtPassword1.Enabled = False
        .chkStatus.Enabled = False
        .cbRola.Enabled = False
        If .ShowDialog = Windows.Forms.DialogResult.OK Then
          Dim HashHelper As New HashHelper, DBA As New DataBaseAction, U As New UsersSQL
          Dim SaltedPasswordHash As Byte()
          SaltedPasswordHash = HashHelper.CreateDBPassword(.txtPassword.Text)
          Dim cmd As MySqlCommand = DBA.CreateCommand(U.InsertUser())
          Login = .txtLogin.Text.Trim
          cmd.Transaction = MyTransaction
          cmd.Parameters.AddWithValue("?Password", SaltedPasswordHash)
          cmd.Parameters.AddWithValue("?Login", Login)
          cmd.Parameters.AddWithValue("?Nazwisko", .txtNazwisko.Text.Trim)
          cmd.Parameters.AddWithValue("?Imie", .txtImie.Text.Trim)
          cmd.Parameters.AddWithValue("?Email", .txtEmail.Text.Trim)
          cmd.Parameters.AddWithValue("?Role", CType(.cbRola.SelectedItem, CbItem).ID)
          cmd.Parameters.AddWithValue("?Status", CType(.chkStatus.Checked, Boolean))
          cmd.Parameters.AddWithValue("?IdNauczyciel", DBNull.Value)

          cmd.ExecuteNonQuery()
          Return Login
        End If
      End With
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
    Return Login
  End Function
  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    FetchData()
    GetData()
  End Sub
  Private Function AddPrivilage(MyTransaction As MySqlTransaction, Login As String, IdStudent As String) As Boolean
    Dim DBA As New DataBaseAction, P As New PrivilagesSQL
    Try
      Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertPrivilege)
      cmd.Transaction = MyTransaction
      cmd.Parameters.AddWithValue("?Login", Login)
      cmd.Parameters.AddWithValue("?IdUczen", IdStudent)
      cmd.ExecuteNonQuery()
      Return True
    Catch err As MySqlException
      Select Case err.Number
        Case 1062
          MessageBox.Show("Uprawnienie już istnieje.")
        Case Else
          MessageBox.Show(err.Message + vbNewLine + "Numer błędu: " + err.Number.ToString, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Select
      Return False
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return False
    End Try
  End Function
  Private Function CheckStudentExist(Name As String, GivenName As String, Pesel As String) As Integer
    Try
      Dim W As New WniosekSQL, DBA As New DataBaseAction, Answer As String
      Answer = DBA.GetSingleValue(W.SelectStudent(Name, GivenName, Pesel))
      If Answer.Length > 0 Then
        Return CType(Answer, Integer)
      Else
        Return 0
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
      Return 0
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return 0
    End Try
  End Function
  Private Function CheckStudentAllocation(IdStudent As String, SchoolYear As String) As Boolean
    Dim W As New WniosekSQL, DBA As New DataBaseAction
    If CType(DBA.GetSingleValue(W.CountStudent(IdStudent, SchoolYear)), Integer) > 0 Then Return True
    Return False
  End Function

  Private Sub cmdVerify_Click(sender As Object, e As EventArgs) Handles cmdVerify.Click
    For Each Item As ListViewItem In lvWniosek.SelectedItems
      Dim MyTransaction As MySqlTransaction = GlobalValues.gblConn.BeginTransaction
      Dim ApplyType As String = Item.SubItems(2).Tag.ToString 'lvWniosek.SelectedItems(0).SubItems(2).Tag.ToString
      Dim IdApplication As Integer = CType(Item.Text, Integer)
      Dim Name() As String
      Name = Item.SubItems(1).Text.Split(" ".ToCharArray)
      Select Case ApplyType
        Case "NA"
          If CheckUserExist(Name) Then
            ChangeRegisterStatus(4, IdApplication, MyTransaction)
          Else
            ChangeRegisterStatus(7, IdApplication, MyTransaction)
          End If
          'MyTransaction.Commit()
        Case "AP", "RP"
          Dim Login As String = Item.SubItems(5).Text
          If CheckUserExist(Name, Login) Then
            ChangeRegisterStatus(7, IdApplication, MyTransaction)
          Else
            ChangeRegisterStatus(9, IdApplication, MyTransaction)
          End If
      End Select
      MyTransaction.Commit()
    Next
    FetchData()
    GetData()
  End Sub

  Private Sub cmdExecute_Click(sender As Object, e As EventArgs) Handles cmdExecute.Click
    'Dim IdApplication As Integer = CType(lvWniosek.SelectedItems(0).Text, Integer)
    Dim MyTransaction As MySqlTransaction = Nothing
    Try
      For Each Item As ListViewItem In lvWniosek.SelectedItems
        MyTransaction = GlobalValues.gblConn.BeginTransaction()
        Dim ApplyType As String = Item.SubItems(2).Tag.ToString
        Dim IdApplication As Integer = CType(Item.Text, Integer)
        Dim Name() As String
        Name = Item.SubItems(1).Text.Split(" ".ToCharArray)
        Select Case ApplyType
          Case "NA"
            Dim Email As String = Item.SubItems(6).Text
            CreateNewUserPrivilage(MyTransaction, IdApplication, Name(0), Name(1), Email)
            ChangeRegisterStatus(8, IdApplication, MyTransaction)
          Case "AP"
            'CreateExistentUserPrivilage(MyTransaction)
            Dim Login As String = Item.SubItems(5).Text
            ChangeRegisterStatus(2, IdApplication, MyTransaction)
            CreatePrivilage(MyTransaction, Login, IdApplication)
          Case "RP"
            ChangeRegisterStatus(8, IdApplication, MyTransaction)
            'RestorePassword()
        End Select
        MyTransaction.Commit()
      Next
      FetchData()
      GetData()
    Catch mex As MySqlException
      MyTransaction.Rollback()
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      MyTransaction.Rollback()
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try

  End Sub
  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Try

      Dim PP As New dlgPrintPreview ', DSP As New DataSet
      PP.Doc = New PrintReport(dsWniosek)
      PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
      PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
      PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
      PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
      PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin
      RemoveHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
      RemoveHandler NewRow, AddressOf PP.NewRow
      RemoveHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
      AddHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage
      AddHandler NewRow, AddressOf PP.NewRow
      PP.Width = 1000

      PP.ShowDialog()
      'MyTransaction.Commit()
      FetchData()
      GetData()
    Catch mex As MySqlException
      'MyTransaction.Rollback()
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      'MyTransaction.Rollback()
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub
  Private Sub CreateNewUserPrivilage(MyTransaction As MySqlTransaction, IdApplication As Integer, ParentName As String, ParentGivenName As String, ParentEmail As String)
    
    Dim Login As String = CreateAccount(MyTransaction, ParentName, ParentGivenName, ParentEmail)
    'Dim Login As String = CreateAccount(MyTransaction, Name(0), Name(1), lvWniosek.SelectedItems(0).SubItems(6).Text)
    If Login IsNot Nothing Then
      CompleteApplication(MyTransaction, Login, IdApplication)
      CreatePrivilage(MyTransaction, Login, IdApplication)
    Else
      Throw New System.Exception("Procedura została przerwana. Wszystkie zmiany zostały wycofane.")
    End If
  End Sub
  Private Sub CompleteApplication(MyTransaction As MySqlTransaction, Login As String, IdWniosek As Integer)
    Dim DBA As New DataBaseAction, W As New WniosekSQL
    Try
      Dim cmd As MySqlCommand = DBA.CreateCommand(W.UpdateWniosek)
      cmd.Transaction = MyTransaction
      cmd.Parameters.AddWithValue("?ID", IdWniosek)
      cmd.Parameters.AddWithValue("?Login", Login)
      cmd.ExecuteNonQuery()
      'MessageBox.Show("Hasło zostało zmienione")
    Catch mex As MySqlException
      MessageBox.Show("Wystąpił błąd:" + vbNewLine + mex.Message)
    Catch ex As Exception
      MessageBox.Show("Wystąpił błąd:" + vbNewLine + ex.Message)
    End Try
  End Sub
  Private Sub PrnDoc_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
    PH = New PrintHelper()
    If e.PrintAction = PrintAction.PrintToPrinter Then
      IsPreview = False
    Else
      IsPreview = True
    End If
  End Sub
  Private Sub PreviewModeChanged(PreviewMode As Boolean)
    IsPreview = PreviewMode
  End Sub
  Public Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
    Dim Doc As PrintReport = CType(sender, PrintReport)
    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)

    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    Dim PrintHeight As Integer = e.MarginBounds.Bottom


    Dim GroupColSize As Integer = 80, NameColSize As Integer = 250, PeselColSize = 120
    Dim dx = x
    Dim DashPen As New Pen(Brushes.Black)
    DashPen.DashStyle = Drawing2D.DashStyle.Dash
    DashPen.DashPattern = New Single() {6.0F, 4.0F}
    Do Until (y + LineHeight * CSng(11)) > PrintHeight Or Offset(0) > lvWniosek.SelectedItems.Count - 1
      Dim MyTransaction As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
      Dim IdApplication As Integer = CType(lvWniosek.SelectedItems(Offset(0)).Text, Integer)
      'ChangeRegisterStatus(2, CType(lvWniosek.SelectedItems(0).Text, Integer), MyTransaction)
      Dim PwdString As String = RandomizePassword()
      Dim Login As String = lvWniosek.SelectedItems(Offset(0)).SubItems(5).Text
      'RestorePassword(MyTransaction, PwdString, Login)
      If Not IsPreview Then RestorePassword(MyTransaction, PwdString, Login)
      Dim StringLength As Single = e.Graphics.MeasureString("Nazwisko i imię zgłaszającego: ", TextFont).Width
      PH.DrawText("Nazwisko i imię zgłaszającego: ", TextFont, dx, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      PH.DrawText(StrConv(lvWniosek.SelectedItems(Offset(0)).SubItems(1).Text, vbProperCase), New Font(TextFont, FontStyle.Bold), dx + StringLength, y, PrintWidth - StringLength, LineHeight, 0, Brushes.Black, False)
      Dim ApplyType As String = lvWniosek.SelectedItems(Offset(0)).SubItems(2).Tag.ToString
      If ApplyType = "RP" Then
        y += LineHeight * CSng(1.5)
        Dim DBA As New DataBaseAction, W As New WniosekSQL, Pesel As String
        Pesel = DBA.GetSingleValue(W.SelectStudentPesel(Login))
        Dim StudentAllocation() As String = GetStudentAllocation(Pesel)
        If StudentAllocation IsNot Nothing Then
          PH.DrawText("Wychowawca klasy: ", TextFont, dx, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
          StringLength = e.Graphics.MeasureString("Wychowawca klasy: ", TextFont).Width
          'PH.DrawText(StudentAllocation(0), TextFont, dx, y, GroupColSize, LineHeight, 1, Brushes.Black)
          PH.DrawText(StudentAllocation(1), New Font(TextFont, FontStyle.Bold), dx + StringLength, y, PrintWidth - StringLength, LineHeight, 0, Brushes.Black, False)
        End If
      End If
      y += LineHeight * 2
      If ApplyType = "NA" Then
        PH.DrawText("Uczeń (uczennica)", New Font(TextFont, FontStyle.Bold), dx, y, NameColSize, LineHeight * CSng(1.5), 1, Brushes.Black)
        dx += NameColSize
        PH.DrawText("Pesel", New Font(TextFont, FontStyle.Bold), dx, y, PeselColSize, LineHeight * CSng(1.5), 1, Brushes.Black)
        dx += PeselColSize
        PH.DrawText("Klasa", New Font(TextFont, FontStyle.Bold), dx, y, GroupColSize, LineHeight * CSng(1.5), 1, Brushes.Black)
        dx += GroupColSize
        PH.DrawText("Wychowawca klasy", New Font(TextFont, FontStyle.Bold), dx, y, NameColSize, LineHeight * CSng(1.5), 1, Brushes.Black)
        y += LineHeight * CSng(1.5)
        Dim DR() As DataRow = Doc.DS.Tables(1).Select("IdWniosek=" & lvWniosek.SelectedItems(Offset(0)).Text)
        Do Until Offset(1) > DR.Count - 1
          dx = x
          PH.DrawText(DR(Offset(1)).Item(1).ToString, TextFont, dx, y, NameColSize, LineHeight, 0, Brushes.Black)
          dx += NameColSize
          PH.DrawText(DR(Offset(1)).Item(2).ToString, TextFont, dx, y, PeselColSize, LineHeight, 1, Brushes.Black)
          dx += PeselColSize
          Dim StudentAllocation() As String = GetStudentAllocation(DR(Offset(1)).Item(2).ToString)
          If StudentAllocation IsNot Nothing Then
            PH.DrawText(StudentAllocation(0), TextFont, dx, y, GroupColSize, LineHeight, 1, Brushes.Black)
            dx += GroupColSize
            PH.DrawText(StudentAllocation(1), TextFont, dx, y, NameColSize, LineHeight, 1, Brushes.Black)
          End If
          Offset(1) += 1
          y += LineHeight
        Loop
        dx = x
        y += LineHeight
      End If

      StringLength = e.Graphics.MeasureString("Adres aplikacji internetowej: ", TextFont).Width
      PH.DrawText("Adres aplikacji internetowej: ", TextFont, dx, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
      PH.DrawText("dziennik.gimsusz.idsl.pl", New Font(TextFont, FontStyle.Bold), dx + StringLength, y, PrintWidth - StringLength, LineHeight, 0, Brushes.Black, False)
      y += LineHeight
      StringLength = e.Graphics.MeasureString("Certyfikat uwierzytelniający: ", TextFont).Width
      PH.DrawText("Certyfikat uwierzytelniający: ", TextFont, dx, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
      PH.DrawText("pracownia.gimsusz.pl/e-dziennik", New Font(TextFont, FontStyle.Bold), dx + StringLength, y, PrintWidth - StringLength, LineHeight, 0, Brushes.Black, False)
      y += LineHeight * 2
      StringLength = e.Graphics.MeasureString("Login: ", TextFont).Width
      PH.DrawText("Login: ", TextFont, dx, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
      PH.DrawText(Login, New Font(TextFont, FontStyle.Bold), dx + StringLength, y, PrintWidth - StringLength, LineHeight, 0, Brushes.Black, False)
      y += LineHeight
      StringLength = e.Graphics.MeasureString("Hasło: ", TextFont).Width
      PH.DrawText("Hasło: ", TextFont, dx, y, PrintWidth, LineHeight, 0, Brushes.Black, False)
      PH.DrawText(PwdString, New Font(TextFont, FontStyle.Bold), dx + StringLength, y, PrintWidth - StringLength, LineHeight, 0, Brushes.Black, False)
      y += LineHeight * CSng(1.5)

      PH.DrawLine(x, y, x + PrintWidth, y, DashPen)
      Offset(1) = 0
      Offset(0) += 1
      If Not IsPreview Then
        If ApplyType = "NA" Then
          ChangeRegisterStatus(2, IdApplication, MyTransaction)
          ChangeAccountStatus(1, Login, MyTransaction)
        Else
          ChangeRegisterStatus(6, IdApplication, MyTransaction)
        End If
      End If
      MyTransaction.Commit()
      y += LineHeight * CSng(1.5)
    Loop

    If Offset(0) < lvWniosek.SelectedItems.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
    End If
  End Sub
  Private Function GetStudentAllocation(Pesel As String) As String()
    Dim DBA As New DataBaseAction, W As New WniosekSQL, R As MySqlDataReader = Nothing
    R = DBA.GetReader(W.SelectStudentAllocation(Pesel, My.Settings.SchoolYear, Date.Today.ToShortDateString))
    Try
      If R.Read Then
        Return New String() {R.GetString(0), R.GetString(1)}
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
      R.Close()
    End Try
    Return Nothing
  End Function
  Sub ChangeAccountStatus(Status As Integer, Login As String, MyTransaction As MySqlTransaction)
    Try
      Dim W As New WniosekSQL, DBA As New DataBaseAction
      Dim cmd As MySqlCommand = DBA.CreateCommand(W.UpdateAccountStatus)
      cmd.Transaction = MyTransaction
      cmd.Parameters.AddWithValue("?Login", Login)
      cmd.Parameters.AddWithValue("?Status", Status)
      cmd.ExecuteNonQuery()

    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub CreatePrivilage(MyTransaction As MySqlTransaction, Login As String, IdApplication As Integer)
    Dim CH As New CalcHelper
    For Each Item As ListViewItem In lvSubwniosek.Items
      If CH.ValidatePesel(Item.SubItems(2).Text) Then
        Dim Name() As String
        Name = Item.SubItems(1).Text.Split(" ".ToCharArray)
        Dim IdStudent As Integer = CheckStudentExist(Name(0), Name(1), Item.SubItems(2).Text)
        If IdStudent > 0 Then
          If CheckStudentAllocation(IdStudent.ToString, My.Settings.SchoolYear) Then
            If AddPrivilage(MyTransaction, Login, IdStudent.ToString) Then
              SetErrorStatus(1, CType(Item.Text, Integer), MyTransaction)
            Else
              Throw New System.Exception("Dodanie uprawnień nie powiodło się. Wszystkie zmiany zostały wycofane.")
            End If
          Else
            SetErrorStatus(4, CType(Item.Text, Integer), MyTransaction)
            ChangeRegisterStatus(3, IdApplication, MyTransaction)
          End If
        Else
          SetErrorStatus(3, CType(Item.Text, Integer), MyTransaction)
          ChangeRegisterStatus(3, IdApplication, MyTransaction)
        End If
      Else
        SetErrorStatus(2, CType(Item.Text, Integer), MyTransaction)
        ChangeRegisterStatus(3, IdApplication, MyTransaction)
      End If
    Next
  End Sub
  Private Sub RestorePassword(MyTransaction As MySqlTransaction, PwdString As String, Login As String)
    'Dim Login As String = lvWniosek.SelectedItems(0).SubItems(5).Text

    Dim HashHelper As New HashHelper, DBA As New DataBaseAction, U As New UsersSQL
    Try
      Dim SaltedPasswordHash As Byte()
      SaltedPasswordHash = HashHelper.CreateDBPassword(PwdString)
      Dim cmd As MySqlCommand = DBA.CreateCommand(U.UpdatePassword())
      cmd.Transaction = MyTransaction
      cmd.Parameters.AddWithValue("?Password", SaltedPasswordHash)
      cmd.Parameters.AddWithValue("?Login", Login)
      cmd.ExecuteNonQuery()
      'MessageBox.Show("Hasło zostało zmienione")
    Catch mex As MySqlException
      MessageBox.Show("Wystąpił błąd:" + vbNewLine + mex.Message)
    Catch ex As Exception
      MessageBox.Show("Wystąpił błąd:" + vbNewLine + ex.Message)
    End Try
  End Sub
  Private Function RandomizePassword() As String
    ' Define default min and max password lengths.
    Dim MinPwdLength As Integer = 9
    Dim MaxPwdLength As Integer = 9

    ' Define supported password characters divided into groups.
    ' You can add (or remove) characters to (from) these groups.
    Dim PASSWORD_CHARS_LCASE As String = "abcdefghijkmnopqrstwxyz"
    Dim PASSWORD_CHARS_UCASE As String = "ABCDEFGHJKLMNPQRSTWXYZ"
    Dim PASSWORD_CHARS_NUMERIC As String = "23456789"
    Dim PASSWORD_CHARS_SPECIAL As String = "$?&!%"

    Dim charGroups As Char()() = New Char()() {PASSWORD_CHARS_LCASE.ToCharArray(), PASSWORD_CHARS_UCASE.ToCharArray(), PASSWORD_CHARS_NUMERIC.ToCharArray(), PASSWORD_CHARS_SPECIAL.ToCharArray()}

    ' Use this array to track the number of unused characters in each
    ' character group.
    Dim charsLeftInGroup As Integer() = New Integer(charGroups.Length - 1) {}

    ' Initially, all characters in each group are not used.
    Dim I As Integer
    For I = 0 To charsLeftInGroup.Length - 1
      charsLeftInGroup(I) = charGroups(I).Length
    Next

    ' Use this array to track (iterate through) unused character groups.
    Dim leftGroupsOrder As Integer() = New Integer(charGroups.Length - 1) {}

    ' Initially, all character groups are not used.
    For I = 0 To leftGroupsOrder.Length - 1
      leftGroupsOrder(I) = I
    Next

    ' Because we cannot use the default randomizer, which is based on the
    ' current time (it will produce the same "random" number within a
    ' second), we will use a random number generator to seed the
    ' randomizer.

    ' Use a 4-byte array to fill it with random bytes and convert it then
    ' to an integer value.
    Dim randomBytes As Byte() = New Byte(3) {}

    ' Generate 4 random bytes.
    Dim rng As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()

    rng.GetBytes(randomBytes)

    ' Convert 4 bytes into a 32-bit integer value.
    Dim seed As Integer = BitConverter.ToInt32(randomBytes, 0)

    ' Now, this is real randomization.
    Dim random As Random = New Random(seed)

    ' This array will hold password characters.
    Dim password As Char() = Nothing

    ' Allocate appropriate memory for the password.
    If (MinPwdLength < MaxPwdLength) Then
      password = New Char(random.Next(MinPwdLength - 1, MaxPwdLength)) {}
    Else
      password = New Char(MinPwdLength - 1) {}
    End If

    ' Index of the next character to be added to password.
    Dim nextCharIdx As Integer

    ' Index of the next character group to be processed.
    Dim nextGroupIdx As Integer

    ' Index which will be used to track not processed character groups.
    Dim nextLeftGroupsOrderIdx As Integer

    ' Index of the last non-processed character in a group.
    Dim lastCharIdx As Integer

    ' Index of the last non-processed group.
    Dim lastLeftGroupsOrderIdx As Integer = leftGroupsOrder.Length - 1

    ' Generate password characters one at a time.
    For I = 0 To password.Length - 1

      ' If only one character group remained unprocessed, process it;
      ' otherwise, pick a random character group from the unprocessed
      ' group list. To allow a special character to appear in the
      ' first position, increment the second parameter of the Next
      ' function call by one, i.e. lastLeftGroupsOrderIdx + 1.
      If (lastLeftGroupsOrderIdx = 0) Then
        nextLeftGroupsOrderIdx = 0
      Else
        nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx)
      End If

      ' Get the actual index of the character group, from which we will
      ' pick the next character.
      nextGroupIdx = leftGroupsOrder(nextLeftGroupsOrderIdx)

      ' Get the index of the last unprocessed characters in this group.
      lastCharIdx = charsLeftInGroup(nextGroupIdx) - 1

      ' If only one unprocessed character is left, pick it; otherwise,
      ' get a random character from the unused character list.
      If (lastCharIdx = 0) Then
        nextCharIdx = 0
      Else
        nextCharIdx = random.Next(0, lastCharIdx + 1)
      End If

      ' Add this character to the password.
      password(I) = charGroups(nextGroupIdx)(nextCharIdx)

      ' If we processed the last character in this group, start over.
      If (lastCharIdx = 0) Then
        charsLeftInGroup(nextGroupIdx) = charGroups(nextGroupIdx).Length
        ' There are more unprocessed characters left.
      Else
        ' Swap processed character with the last unprocessed character
        ' so that we don't pick it until we process all characters in
        ' this group.
        If (lastCharIdx <> nextCharIdx) Then
          Dim temp As Char = charGroups(nextGroupIdx)(lastCharIdx)
          charGroups(nextGroupIdx)(lastCharIdx) = charGroups(nextGroupIdx)(nextCharIdx)
          charGroups(nextGroupIdx)(nextCharIdx) = temp
        End If

        ' Decrement the number of unprocessed characters in
        ' this group.
        charsLeftInGroup(nextGroupIdx) = charsLeftInGroup(nextGroupIdx) - 1
      End If

      ' If we processed the last group, start all over.
      If (lastLeftGroupsOrderIdx = 0) Then
        lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1
        ' There are more unprocessed groups left.
      Else
        ' Swap processed group with the last unprocessed group
        ' so that we don't pick it until we process all groups.
        If (lastLeftGroupsOrderIdx <> nextLeftGroupsOrderIdx) Then
          Dim temp As Integer = leftGroupsOrder(lastLeftGroupsOrderIdx)
          leftGroupsOrder(lastLeftGroupsOrderIdx) = leftGroupsOrder(nextLeftGroupsOrderIdx)
          leftGroupsOrder(nextLeftGroupsOrderIdx) = temp
        End If

        ' Decrement the number of unprocessed groups.
        lastLeftGroupsOrderIdx = lastLeftGroupsOrderIdx - 1
      End If
    Next

    ' Convert password characters into a string and return the result.
    Return New String(password)
  End Function


  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, W As New WniosekSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvWniosek.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(W.DeleteWniosek(Item.Text))
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        FetchData()
        GetData()
        Dim SH As New SeekHelper
        Me.EnableCommands(False)
        SH.FindPostRemovedListViewItemIndex(Me.lvWniosek, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub
End Class