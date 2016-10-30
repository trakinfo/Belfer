Imports belfer.NET.GlobalValues

Public Class MainForm

  Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    Try
      If GlobalValues.gblConn.State = ConnectionState.Closed Then Exit Sub

      Dim spCmd As New MySqlCommand
      spCmd.Connection = GlobalValues.gblConn
      spCmd.CommandText = "LogOut"
      spCmd.CommandType = CommandType.StoredProcedure
      spCmd.Parameters.AddWithValue("?IdRecord", GlobalValues.gblIdEvent)
      spCmd.Parameters("?IdRecord").Direction = ParameterDirection.Input
      spCmd.ExecuteNonQuery()
      Dim auth As New Autoryzacja
      auth.CloseConnection()
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try

  End Sub

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
      System.Windows.Forms.Application.EnableVisualStyles()

      Me.Text = Me.Text + " - ver. " + My.Application.Info.Version.ToString
      lblRokSzkolny.Text = ""
      lblSchoolName.Text = ""
      ResetMainStatus()
      Dim auth As New Autoryzacja
      'auth.SetConnectParams()

      If auth.Connect() Then
        StatConn.ForeColor = Color.Green
        Me.StatConn.Text = "Połączony"
        statBaza.Text = My.Settings.DBName
        statSSLMode.Font = New Font(statSSLMode.Font, FontStyle.Bold)
        If My.Settings.SSLMode = 1 Then
          statServer.Text = My.Settings.ServerSSLIP
          statSSLMode.ForeColor = Color.Green
          statSSLMode.BackColor = Color.Ivory
          statSSLMode.Image = My.Resources.lock
          statSSLMode.Text = "SSL"
        Else
          statServer.Text = My.Settings.ServerIP
          'statSSLMode.Font = New Font(statSSLMode.Font, FontStyle.Bold)
          statSSLMode.ForeColor = Color.FromArgb(192, 0, 0) 'Color.Red
          statSSLMode.BackColor = Color.Yellow
          statSSLMode.Image = My.Resources.unlock
          statSSLMode.Text = "No SSL"
        End If
        statSSLMode.ToolTipText = auth.GetSSLCipher
        Login()
      Else
        Application.Exit()
        'Me.Dispose(True)
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try

  End Sub
  Private Sub LogOut()
    Try
      If gblConn.State = ConnectionState.Closed Then Exit Sub
      Dim spCmd As New MySqlCommand
      spCmd.Connection = gblConn
      spCmd.CommandText = "LogOut"
      spCmd.CommandType = CommandType.StoredProcedure
      spCmd.Parameters.AddWithValue("?IdRecord", gblIdEvent)
      spCmd.Parameters("?IdRecord").Direction = ParameterDirection.Input
      spCmd.ExecuteNonQuery()
      'CloseOpenedForms()
      AppUser.Reset()
      'GlobalValues.AppUser.Login = ""
      'GlobalValues.gblRole = Nothing
      Users = Nothing
      ResetUserStatus()
      EnableSuperAdminMenu(False)
      EnableAdminMenu(False)
      'tlpCommonData.Visible = False
      'QuickAccessToolStrip.Visible = False
      'tscQuickAccess.Visible = False
      WylogujToolStripMenuItem.Enabled = False
      Login()
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub
  'Private Sub CloseOpenedForms()
  '  For Each F As Form In Application.OpenForms
  '    If Not F.IsMdiContainer Then F.Close()
  '  Next
  'End Sub
  Private Sub ResetMainStatus()
    StatConn.ForeColor = Color.Firebrick
    StatConn.Text = "Rozłączony"
    statServer.Text = "0.0.0.0"
    statBaza.Text = ""
    statSSLMode.Text = ""
    statSSLMode.Image = Nothing
    ResetUserStatus()
  End Sub
  Private Sub ResetUserStatus()
    StatUser.Text = ""
    statRole.Text = ""
  End Sub

  Public Sub Login()
    Dim dlgLogin As New dlgLogin
    Try
      If dlgLogin.ShowDialog = Windows.Forms.DialogResult.OK Then
        With dlgLogin
          My.Settings.UserName = .txtUserName.Text.ToLower
          My.Settings.Save()
          Dim auth As New Autoryzacja
          If auth.CheckUser(.txtUserName.Text.ToLower, .txtPassword.Text) Then
            SetProfile()
            GetUsers()
            If AbsenceComplain(AppUser.Login) > 0 Then
              Dim dlg As New frmReklamacja With {.Filter = " AND AbsenceOwner='" & AppUser.Login & "'"}
              With dlg
                .gbStatus.Enabled = False
                .gbStatus.Visible = False
                .lblFilter.Visible = False
                .cbSeek.Enabled = False
                .cbSeek.Visible = False
                .txtSeek.Enabled = False
                .txtSeek.Visible = False
                .StartPosition = FormStartPosition.CenterScreen
                .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
                .MaximizeBox = False
                .MinimizeBox = False
                .Text += " przez aktualnie zalogowanego użytkownika (" & AppUser.Name & ")"

                .ShowDialog()
              End With
            End If
          Else
            Login()
          End If
        End With
      Else
        Application.Exit()
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    Finally
    End Try

  End Sub
  Private Sub GetUsers()
    Dim R As MySqlDataReader = Nothing
    Try
      Users = New Hashtable
      Dim DBA As New DataBaseAction, U As New UsersSQL
      R = DBA.GetReader(U.SelectUserNames)
      If R.HasRows Then
        While R.Read
          Users.Add(R.Item("Login").ToString.ToLower, R.Item("User").ToString)
        End While
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    Finally
      If R IsNot Nothing Then R.Close()
    End Try
  End Sub
  Private Function AbsenceComplain(User As String) As Integer
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL
    Return DBA.ComputeRecords(A.CountComplain(User))
  End Function
  Private Sub SetProfile()
    StatUser.Text = AppUser.ToString 'GlobalValues.AppUser.Login & " (" & GlobalValues.gblFullName & ")"
    statRole.Text = [Enum].GetName(GetType(Role), AppUser.Role)
    'StatUser.Text = GlobalValues.AppUser.Login & " (" & GlobalValues.gblFullName & ")"
    'statRole.Text = [Enum].GetName(GetType(GlobalValues.Role), GlobalValues.gblRole)
    If SetWorkingParams() Then
      GetSchoolTeacherID()
      CompleteUserObject()
      If AppUser.Role = 4 Then 'GlobalValues.gblRole = 4 Then
        'gblEnableAdminCommands = True
        EnableSuperAdminMenu(True)
        EnableAdminMenu(True)
      ElseIf AppUser.Role = 2 Then 'GlobalValues.gblRole = 2 Then
        'gblEnableAdminCommands = False
        EnableEditorMenu(True)
        tlpCommonData.Enabled = False
      Else
        'gblEnableAdminCommands = False
        EnableMenu(True)
        tlpCommonData.Enabled = False
      End If
    Else

      If AppUser.Role = 4 Then 'GlobalValues.gblRole = 4 Then
        MessageBox.Show("Do prawidłowego działania większości funkcji programu niezbędny jest wybór szkoły i roku szkolnego.", My.Application.Info.ProductName)
        EnableSuperAdminMenu(True)
        If EditWorkingParams() Then EnableAdminMenu(True)
      Else
        MessageBox.Show("Do prawidłowego działania większości funkcji programu niezbędny jest wybór szkoły i roku szkolnego." & vbNewLine & "Zaloguj się na konto z uprawnieniami administratora i ustaw parametry pracy programu.", My.Application.Info.ProductName)
        LogOut()
      End If
      'EnableAdminMenu(False)
    End If
  End Sub
  Private Sub CompleteUserObject()
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, U As New UsersSQL
    Try
      R = DBA.GetReader(U.SelectTutorClass(My.Settings.IdSchool, AppUser.SchoolTeacherID, My.Settings.SchoolYear))
      If R.Read Then
        AppUser.TutorClassID = R.GetString("Klasa")
        AppUser.TutorClassName = R.GetString("Nazwa_Klasy")
      End If
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub GetSchoolTeacherID()
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, U As New UsersSQL
    Try
      R = DBA.GetReader(U.SelectSchoolTeacherID(My.Settings.IdSchool, AppUser.TeacherID))
      If R.Read() Then AppUser.SchoolTeacherID = R.GetString("ID")
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Function SetWorkingParams() As Boolean
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, W As New WorkingParamsSQL
    Try
      lblRokSzkolny.Text = My.Settings.SchoolYear
      R = DBA.GetReader(W.SelectSchoolName(My.Settings.IdSchool))
      If R.Read() Then
        lblSchoolName.Text = R.GetString(0)
      End If
      If My.Settings.SchoolYear.Length > 0 AndAlso My.Settings.IdSchool.Length > 0 Then Return True
    Catch mex As MySqlException
      MessageBox.Show(mex.Message & vbNewLine & mex.ErrorCode)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try
    Return False
  End Function
  Private Function EditWorkingParams() As Boolean
    Try
      Dim dlgEdit As New dlgKonfiguracja
      With dlgEdit
        If My.Settings.SchoolYear.Length >= 4 Then
          .nudStartYear.Value = CType(My.Settings.SchoolYear.Substring(0, 4), Integer)
        Else
          Dim CH As New CalcHelper
          .nudStartYear.Value = CType(CH.CurrentSchoolYear.Substring(0, 4), Integer)

        End If
        .nudEndYear.Value = .nudStartYear.Value + 1
        Dim FCB As New FillComboBox, S As New SzkolaSQL, SH As New SeekHelper, T As New TypySzkolSQL
        FCB.AddComboBoxComplexItems(.cbTypSzkoly, T.SelectSchoolTypes)
        If .cbTypSzkoly.Items.Count > 0 Then
          If My.Settings.IdSchoolType.Length > 0 Then SH.FindComboItem(.cbTypSzkoly, CType(My.Settings.IdSchoolType, Integer))
          .cbTypSzkoly.Enabled = True

        Else
          .cbTypSzkoly.Enabled = False
        End If

        FCB.AddComboBoxComplexItems(.cbSzkola, S.SelectSchoolAlias(My.Settings.IdSchoolType))
        If .cbSzkola.Items.Count > 0 Then
          If My.Settings.IdSchool.Length > 0 Then SH.FindComboItem(.cbSzkola, CType(My.Settings.IdSchool, Integer))
          .cbSzkola.Enabled = True
        Else
          .cbSzkola.Enabled = False
        End If
        AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf SetWorkingParams

        .OK_Button.Text = "&Zapisz"
        .OK_Button.Enabled = False
        .Text = "Edycja parametrów pracy"
        .CancelButton = .Cancel_Button
        .AcceptButton = .OK_Button
        '.ShowDialog()
        If .ShowDialog() = Windows.Forms.DialogResult.OK Then Return True
      End With
      'Return False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
    Return False
  End Function
  Private Sub EnableSuperAdminMenu(ByVal Value As Boolean)
    ProgramToolStripMenuItem.Enabled = Value
    UstawieniaToolStripMenuItem.Enabled = Value
    PlikToolStripMenuItem.Enabled = Value
    'KonfiguracjaToolStripMenuItem.Enabled = Value
    'KonfiguracjaToolStripButton.Enabled = Value
    ZmianahasłaToolStripMenuItem.Enabled = Value
    ZarzadzanieuzytkownikamiToolStripMenuItem.Enabled = Value
    UprawnieniaToolStripMenuItem.Enabled = Value
    TypySzkolToolStripMenuItem.Enabled = Value
    SzkolyToolStripMenuItem.Enabled = Value
    SlownikToolStripMenuItem.Enabled = Value
    MiejscowoscToolStripMenuItem.Enabled = Value
    WojewodztwaToolStripMenuItem.Enabled = Value
    KrajeToolStripMenuItem.Enabled = Value
    OddzialyToolStripMenuItem.Enabled = Value
    PrzedmiotyToolStripMenuItem.Enabled = Value
    NauczycieleToolStripMenuItem.Enabled = Value
    SkalaOcenToolStripMenuItem.Enabled = Value
    WylogujToolStripMenuItem.Enabled = Value
    AdminToolStripMenuItem.Enabled = Value
    'EnableAdminMenu(Value)
  End Sub
  Private Sub EnableAdminMenu(ByVal Value As Boolean)
    PrzydzialToolStripMenuItem.Enabled = Value
    PrzydzialUczniowToolStripMenuItem.Enabled = Value
    PromocjaToolStripMenuItem.Enabled = Value
    ObsadaToolStripMenuItem.Enabled = Value
    HarmonogramToolStripMenuItem.Enabled = Value
    ImportToolStripMenuItem.Enabled = Value
    NadzorToolStripMenuItem.Enabled = Value
    ZastepstwoToolStripMenuItem.Enabled = Value
    MinLekcjaToolStripMenuItem.Enabled = Value
    ManagePlanLekcjiToolStripMenuItem.Enabled = Value
    UzasadnieniaToolStripMenuItem.Enabled = Value
    ZestawienieKlasyfikacjiToolStripMenuItem.Enabled = Value
    UserActivityToolStripMenuItem.Enabled = Value
    EnableEditorMenu(Value)
  End Sub
  Private Sub EnableEditorMenu(ByVal Value As Boolean)
    UczniowieToolStripMenuItem.Enabled = Value
    DziennikToolStripMenuItem.Enabled = Value
    KonfiguracjaToolStripMenuItem.Enabled = Value
    KonfiguracjaToolStripButton.Enabled = Value
    SlownikToolStripMenuItem.Enabled = Value
    KolumnyToolStripMenuItem.Enabled = Value
    OpisyOcenToolStripMenuItem.Enabled = Value
    MiejscowoscToolStripMenuItem.Enabled = Value
    GrupyPrzedmiotoweToolStripMenuItem.Enabled = Value
    QuickAccessToolStrip.Visible = Value
    cmdTemat.Enabled = Value
    cmdTematByNauczyciel.Enabled = Value
    cmdWynikiCzastkowe.Enabled = Value
    cmdOcenyZachowania.Enabled = Value
    cmdAbsencja.Enabled = Value
    cmdUwaga.Enabled = Value
    'ZagrozeniaToolStripMenuItem.Enabled = Value
    cmdZagrozenia.Enabled = Value
    cmdTerminarz.Enabled = Value
    TerminarzToolStripMenuItem.Enabled = Value
    EnableMenu(Value)
  End Sub
  Private Sub EnableMenu(ByVal value As Boolean)
    ZmianahasłaToolStripMenuItem.Enabled = value
    PlikToolStripMenuItem.Enabled = value
    WylogujToolStripMenuItem.Enabled = value
    RaportyToolStripMenuItem.Enabled = value
    StatystykiToolStripMenuItem.Enabled = value
    'ProgramToolStripMenuItem.Enabled = value
    UstawieniaToolStripMenuItem.Enabled = value
    'NauczanieIndywidualneToolStripMenuItem.Enabled = value
    'AbsencjaByMonthToolStripMenuItem.Enabled = value
    'FrekwencjaToolStripMenuItem.Enabled = value
    'ReklamacjaToolStripMenuItem.Enabled = value
    'ParentLoggingToolStripMenuItem.Enabled = value
    tlpCommonData.Visible = value
    tlpCommonData.Enabled = value
  End Sub

  Private Sub ZamknijToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZamknijToolStripMenuItem.Click
    Application.Exit()
  End Sub

  Private Sub WylogujToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WylogujToolStripMenuItem.Click, cmdWyloguj.Click
    For Each F As Form In MdiChildren
      F.Close()
    Next
    LogOut()
  End Sub
  Private Sub ZmianaHasla2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZmianaHasla2ToolStripMenuItem.Click
    Try
      Dim auth As New Autoryzacja

      'If AppUser.Role = Role.Administrator Then 'GlobalValues.gblRole = GlobalValues.Role.Administrator Then
      auth.ChangePassword()
      'Else
      '  auth.ChangePassword(AppUser.Login) 'GlobalValues.AppUser.Login)
      'End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub ZmianahasłaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZmianahasłaToolStripMenuItem.Click
    Try
      Dim auth As New Autoryzacja

      'If AppUser.Role = Role.Administrator Then 'GlobalValues.gblRole = GlobalValues.Role.Administrator Then
      '  auth.ChangePassword()
      'Else
      auth.ChangePassword(AppUser.Login) 'GlobalValues.AppUser.Login)
      'End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub ZarządzanieużytkownikamiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZarzadzanieuzytkownikamiToolStripMenuItem.Click
    Dim frmUsers As New frmUser
    With frmUsers
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      ZarzadzanieuzytkownikamiToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub UprawnieniaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UprawnieniaToolStripMenuItem.Click
    Dim frmUsers As New frmPrivilege
    With frmUsers
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      UprawnieniaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub SzkolyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SzkolyToolStripMenuItem.Click
    Dim frmSchool As New frmSzkola
    With frmSchool
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      SzkolyToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub OddzialyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OddzialyToolStripMenuItem.Click
    Dim frm As New frmKlasa
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      OddzialyToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PrzydzialKlasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzydzialKlasDoSzkolToolStripMenuItem.Click
    Dim frm As New frmPrzydzialKlasDoSzkol
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      PrzydzialKlasDoSzkolToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub MiejscowoscToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MiejscowoscToolStripMenuItem.Click
    Dim frm As New frmMiejscowosc
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      MiejscowoscToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub WojewodztwaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WojewodztwaToolStripMenuItem.Click
    Dim frm As New frmWoj
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      WojewodztwaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub KrajeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KrajeToolStripMenuItem.Click
    Dim frm As New frmKraj
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      KrajeToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub NauczycieleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NauczycieleToolStripMenuItem.Click
    Dim frm As New frmBelfer
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      NauczycieleToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PrzydzialNauczycieliDoSzkolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzydzialNauczycieliDoSzkolToolStripMenuItem.Click
    Dim frm As New frmPrzydzialNauczycieliDoSzkol
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      PrzydzialNauczycieliDoSzkolToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PrzydzialWychowawstwToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzydzialWychowawstwToolStripMenuItem.Click
    Dim frm As New frmWychowawca
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      PrzydzialWychowawstwToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PrzedmiotyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzedmiotyToolStripMenuItem.Click
    Dim frm As New frmPrzedmiot
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      PrzedmiotyToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PrzydzialPrzedmiotowDoSzkolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzydzialPrzedmiotowDoSzkolToolStripMenuItem.Click
    Dim frm As New frmPrzydzialPrzedmiotowDoSzkol
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      PrzydzialPrzedmiotowDoSzkolToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub


  Private Sub TypySzkolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TypySzkolToolStripMenuItem.Click
    Dim frm As New frmTypySzkol
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      TypySzkolToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PrzydzialUczniowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrzydzialUczniowToolStripMenuItem.Click
    Dim frm As New frmPrzydzial
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      PrzydzialUczniowToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub OcenyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SkalaOcenToolStripMenuItem.Click
    Dim frm As New frmSkalaOcen
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      SkalaOcenToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub OpisyOcenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpisyOcenToolStripMenuItem.Click
    Dim frm As New frmOpisWyniku
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      OpisyOcenToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub


  Private Sub ObsadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObsadaToolStripMenuItem.Click
    Dim frm As New frmObsada
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      .ObsadaFilter = "Klasa"
      .Text = "Obsada przedmiotów w klasach"
      ObsadaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub KolumnyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KolumnyToolStripMenuItem.Click
    Dim frm As New frmKolumna
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      KolumnyToolStripMenuItem.Enabled = False
      OcenyCzastkoweToolStripMenuItem.Enabled = False
      cmdWynikiCzastkowe.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub OcenyCzastkoweToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OcenyCzastkoweToolStripMenuItem.Click
    Dim frm As New frmWynikiPartial
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      OcenyCzastkoweToolStripMenuItem.Enabled = False
      cmdWynikiCzastkowe.Enabled = False
      KolumnyToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub cmdWynikiCzastkowe_Click(sender As Object, e As EventArgs) Handles cmdWynikiCzastkowe.Click
    OcenyCzastkoweToolStripMenuItem_Click(sender, e)
  End Sub


  Private Sub KonfiguracjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KonfiguracjaToolStripMenuItem.Click
    If EditWorkingParams() Then EnableAdminMenu(True)
  End Sub

  Private Sub KonfiguracjaToolStripButton_Click(sender As Object, e As EventArgs) Handles KonfiguracjaToolStripButton.Click
    If EditWorkingParams() Then EnableAdminMenu(True)
  End Sub

  Private Sub lblSchoolName_Click(sender As Object, e As EventArgs) Handles lblSchoolName.DoubleClick, lblRokSzkolny.DoubleClick
    EditWorkingParams()
  End Sub

  Private Sub HarmonogramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HarmonogramToolStripMenuItem.Click
    Dim frm As New frmHarmonogram
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      HarmonogramToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub


  Private Sub cmdTemat_Click(sender As Object, e As EventArgs) Handles cmdTemat.Click
    TematByDataToolStripMenuItem_Click(sender, e)
  End Sub

  Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click

    'Dim Import As New ImportStudent
    'Import.ReadXmlData()
    Dim dlgImport As New dlgImportStudent
    With dlgImport
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      ImportToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub



  Private Sub cmdCloseProgram_Click(sender As Object, e As EventArgs) Handles cmdCloseProgram.Click
    Application.Exit()
  End Sub

  Private Sub GrupyPrzedmiotoweToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GrupyPrzedmiotoweToolStripMenuItem.Click
    Dim frm As New frmGrupaPrzedmiotowa
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      GrupyPrzedmiotoweToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub


  Private Sub DanePersonalneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DanePersonalneToolStripMenuItem.Click
    Dim frm As New frmStudents
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      UczniowieToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub ZagrozeniaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZagrozeniaToolStripMenuItem.Click
    Dim frm As New frmZagrozenia
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      ZagrozeniaToolStripMenuItem.Enabled = False
      cmdZagrozenia.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub cmdZagrozenia_Click(sender As Object, e As EventArgs) Handles cmdZagrozenia.Click
    ZagrozeniaToolStripMenuItem_Click(sender, e)
  End Sub

  Private Sub ZagrozeniaRaportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZagrozeniaRaportToolStripMenuItem.Click
    Dim prn As New prnZagrozenia
    With prn
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      '.MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      '.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      'cmdPrint.Enabled = False
      .Show()
      'cmdPrint.Enabled = True
    End With
  End Sub

  'Private Sub LiczbaGodzinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiczbaGodzinToolStripMenuItem.Click
  '  Dim frm As New frmLiczbaGodzin
  '  With frm
  '    .Icon = GlobalValues.gblAppIcon
  '    .MdiParent = Me
  '    .MaximizeBox = False
  '    .StartPosition = FormStartPosition.CenterScreen
  '    .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
  '    .WindowState = FormWindowState.Normal
  '    LiczbaGodzinToolStripMenuItem.Enabled = False
  '    .Show()
  '  End With
  'End Sub

  Private Sub WynikiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WynikiToolStripMenuItem.Click
    Dim prn As New prnWyniki
    With prn
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      '.MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      '.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      'cmdPrint.Enabled = False
      WynikiToolStripMenuItem.Enabled = False
      .Show()
      'cmdPrint.Enabled = True
    End With
  End Sub

  Private Sub AnalizaOcenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnalizaOcenToolStripMenuItem.Click
    Dim frm As New frmAnalizaOcen
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      AnalizaOcenToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub WykazNdstToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WykazNdstToolStripMenuItem.Click
    Dim frm As New frmWykazNdst
    With frm
      .Typ = "P"
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      WykazNdstToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub ProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProgramToolStripMenuItem.Click
    Dim dlg As New AboutBelfer
    With dlg
      .Icon = GlobalValues.gblAppIcon
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
      .WindowState = FormWindowState.Normal
      .ShowDialog()
    End With
  End Sub

  Private Sub ZastepstwoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZastepstwoToolStripMenuItem.Click
    Dim frm As New frmZastepstwo
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      ZastepstwoToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub AbsencjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbsencjaToolStripMenuItem.Click
    Dim frm As New frmAbsencja
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      AbsencjaToolStripMenuItem.Enabled = False
      cmdAbsencja.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub cmdAbsencja_Click(sender As Object, e As EventArgs) Handles cmdAbsencja.Click
    AbsencjaToolStripMenuItem_Click(sender, e)
  End Sub

  Private Sub KalibracjaWydrukuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KalibracjaWydrukuToolStripMenuItem.Click
    Dim frm As New frmKalibracjaWydruku
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      KalibracjaWydrukuToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub UwagiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UwagiToolStripMenuItem.Click
    Dim frm As New frmUwagi
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      UwagiToolStripMenuItem.Enabled = False
      cmdUwaga.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub cmdUwaga_Click(sender As Object, e As EventArgs) Handles cmdUwaga.Click
    UwagiToolStripMenuItem_Click(sender, e)
  End Sub

  Private Sub ListaUczniowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListaUczniowToolStripMenuItem.Click
    Dim prn As New prnStudents
    With prn
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      ListaUczniowToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub AbsencjaRaportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbsencjaRaportToolStripMenuItem.Click
    Dim prn As New prnAbsencja
    With prn
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      AbsencjaRaportToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub UwagiRaportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UwagiRaportToolStripMenuItem.Click
    Dim prn As New prnUwagi
    With prn
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      UwagiRaportToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub WgKlasToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LiczbaGodzinWgKlasToolStripMenuItem.Click
    Dim frm As New frmLiczbaGodzin
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      LiczbaGodzinWgKlasToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub LiczbaGodzinWgNauczycielaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiczbaGodzinWgNauczycielaToolStripMenuItem.Click
    Dim frm As New frmLiczbaGodzinByNauczyciel
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      LiczbaGodzinWgNauczycielaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub MinLekcjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MinLekcjaToolStripMenuItem.Click
    Dim frm As New frmMinLekcja
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      MinLekcjaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub TematByDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TematByDataToolStripMenuItem.Click
    Dim frm As New frmTemat
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      TematToolStripMenuItem.Enabled = False
      cmdTemat.Enabled = False
      cmdTematByNauczyciel.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub TematByPrzedmiotToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TematByBelferToolStripMenuItem.Click
    Dim frm As New frmTematByBelfer
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      TematToolStripMenuItem.Enabled = False
      cmdTemat.Enabled = False
      cmdTematByNauczyciel.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub cmdTematByPrzedmiot_Click(sender As Object, e As EventArgs) Handles cmdTematByNauczyciel.Click
    TematByPrzedmiotToolStripMenuItem_Click(sender, e)
  End Sub


  Private Sub ManagePlanLekcjiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManagePlanLekcjiToolStripMenuItem.Click
    Dim frm As New frmManagerPlanowLekcji
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      .LekcjaFilter = "Klasa"
      ManagePlanLekcjiToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PlanLekcjiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlanLekcjiToolStripMenuItem.Click
    Dim frm As New frmPlanLekcji
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = True
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      '.LekcjaFilter = "Klasa"
      PlanLekcjiToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub OcenyZachowanieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OcenyZachowanieToolStripMenuItem.Click
    Dim frm As New frmZachowanie
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      OcenyZachowanieToolStripMenuItem.Enabled = False
      cmdOcenyZachowania.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub cmdOcenyZachowania_Click(sender As Object, e As EventArgs) Handles cmdOcenyZachowania.Click
    OcenyZachowanieToolStripMenuItem_Click(sender, e)
  End Sub

  Private Sub KlasyfikacjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KlasyfikacjaToolStripMenuItem.Click
    Dim frm As New frmKlasyfikacja
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      KlasyfikacjaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub TerminarzToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TerminarzToolStripMenuItem.Click
    Dim frm As New frmTerminarz
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      TerminarzToolStripMenuItem.Enabled = False
      cmdTerminarz.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub cmdTerminarz_Click(sender As Object, e As EventArgs) Handles cmdTerminarz.Click
    TerminarzToolStripMenuItem_Click(sender, e)
  End Sub


  Private Sub NauczanieIndywidualneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NauczanieIndywidualneToolStripMenuItem.Click
    Dim frm As New frmNauczanieIndywidualne
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      NauczanieIndywidualneToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub MoveResultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoveResultToolStripMenuItem.Click
    Dim frm As New frmMoveResult
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      MoveResultToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub AbsencjaByMonthToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbsencjaByMonthToolStripMenuItem.Click
    Dim frm As New frmAbsencjaByMonth
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      AbsencjaByMonthToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub FrekwencjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrekwencjaToolStripMenuItem.Click
    Dim frm As New frmFrekwencja
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      FrekwencjaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub TableSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TableSetToolStripMenuItem.Click
    Dim frm As New frmZestawienieOcen
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      TableSetToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub PromocjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PromocjaToolStripMenuItem.Click
    Dim frm As New frmPromocja
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      PromocjaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub WynikiPoprawkoweToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WynikiPoprawkoweToolStripMenuItem.Click
    Dim frm As New frmWynikiPoprawkowe
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      WynikiPoprawkoweToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub StudentMakeupAllowedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StudentMakeupAllowedToolStripMenuItem.Click
    Dim frm As New frmStudentPoprawa
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      StudentMakeupAllowedToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub


  Private Sub ApplicationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApplicationToolStripMenuItem.Click
    Dim frm As New frmWniosek
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      ApplicationToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub ReklamacjaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReklamacjaToolStripMenuItem.Click
    Dim frm As New frmReklamacja
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      ReklamacjaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub LoggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoggingToolStripMenuItem.Click
    Dim frm As New frmZdarzenia
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      LoggingToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub ParentLoggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParentLoggingToolStripMenuItem.Click
    Dim frm As New frmParentLogging
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      ParentLoggingToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub OutofdatePrivilageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutofdatePrivilageToolStripMenuItem.Click
    Dim frm As New frmWeryfikacjaKont
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      OutofdatePrivilageToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub CurrentConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CurrentConnectionToolStripMenuItem.Click
    Dim frm As New frmCurrentCommection
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      CurrentConnectionToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub LockResultColumnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LockResultColumnToolStripMenuItem.Click
    Dim frm As New frmBlokada
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
      .WindowState = FormWindowState.Normal
      LockResultColumnToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub UserActivityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserActivityToolStripMenuItem.Click
    Dim frm As New frmZdarzenia
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      .WindowState = FormWindowState.Normal
      'UserActivityToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub ReasonContentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UzasadnieniaToolStripMenuItem.Click
    Dim frm As New frmUzasadnienia
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      UzasadnieniaToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub WykazNdpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WykazNdpToolStripMenuItem.Click
    Dim frm As New frmWykazNdst
    With frm
      .Typ = "Z"
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      WykazNdpToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub

  Private Sub ZestawienieKlasyfikacjiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZestawienieKlasyfikacjiToolStripMenuItem.Click
    Dim frm As New frmKlasyfikacjaZbiorcza
    With frm
      .Icon = GlobalValues.gblAppIcon
      .MdiParent = Me
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
      .WindowState = FormWindowState.Normal
      ZestawienieKlasyfikacjiToolStripMenuItem.Enabled = False
      .Show()
    End With
  End Sub
End Class
