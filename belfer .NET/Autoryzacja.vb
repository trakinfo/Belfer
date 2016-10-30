Public Class Autoryzacja
  Public Function Connect() As Boolean
    Try
      Dim DBA As New DataBaseAction, R As New Rijndael
      Connect = False
      'R.Encrypt("user")
      If CType(My.Settings.SSLMode, GlobalValues.SslMode) = GlobalValues.SslMode.None Then
        GlobalValues.gblConn = DBA.Connection(My.Settings.ServerIP, My.Settings.DBName, My.Settings.SysUser, R.Decrypt(My.Settings.SysPassword), CType(My.Settings.SSLMode, GlobalValues.SslMode))
      Else
        GlobalValues.gblConn = DBA.Connection(My.Settings.ServerSSLIP, My.Settings.DBName, My.Settings.SysSSLUser, R.Decrypt(My.Settings.SysSSLPassword), CType(My.Settings.SSLMode, GlobalValues.SslMode))

      End If

      If GlobalValues.gblConn.State = ConnectionState.Open Then
        'Dim DR As MySqlDataReader
        'DR = DBA.GetReader("SHOW STATUS LIKE 'Ssl_cipher';")
        'DR.Read()
        'MessageBox.Show(DR.Item(0).ToString & vbNewLine & DR.Item(1).ToString)
        'DR.Close()
        Dim DBVersion As String, OH As New OptionHolder
        DBVersion = OH.DBVersion
        If String.Compare(String.Concat(My.Application.Info.Version.Major.ToString, ".", My.Application.Info.Version.Minor.ToString), DBVersion) <> 0 Then
          Throw New System.Exception("Wersja programu jest niezgodna z wersją bazy danych!" & vbNewLine & "Praca nie może być kontynuowana, aplikacja zostanie zamknięta." & vbNewLine & "Przy ponownym uruchomieniu program zaktualizuje się automatycznie. Jeśli automatyczna aktualizacja zawiedzie, to pobierz i zainstaluj nową wersję programu dostępną pod adresem http://trakinfo.idsl.pl/belfer." & vbNewLine & "Wersja programu: " & My.Application.Info.Version.ToString & vbNewLine & "Wersja bazy danych: " & DBVersion)
        End If
        Dim SysUser As String, U As New UsersSQL
        SysUser = DBA.GetSingleValue(U.SelectSysUser)
        GlobalValues.gblSysUser = SysUser.Substring(0, SysUser.IndexOf("@")).ToLower
        GlobalValues.gblIP = SysUser.Substring(SysUser.IndexOf("@") + 1)
        Dim N As New Net
        'GlobalValues.gblIP = N.ComputerIPv4 'N.ComputerIP
        GlobalValues.gblHostName = N.ComputerName
        Return True
      Else
        If SetConnectParams() Then
          If Connect() Then Return True
        Else
          Return False
        End If
      End If
      'Return False
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      Return False
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
      Return False
    End Try
  End Function
  Public Sub CloseConnection()
    If Not IsNothing(GlobalValues.gblConn) Then
      If GlobalValues.gblConn.State <> ConnectionState.Closed Then
        GlobalValues.gblConn.Close()
      End If
    End If
  End Sub

  Public Function SetConnectParams() As Boolean
    Dim dlgConnect As New dlgConnect, R As New Rijndael
    With dlgConnect
      .txtSerwerIP.Text = If(CType(My.Settings.SSLMode, GlobalValues.SslMode) = GlobalValues.SslMode.None, My.Settings.ServerIP, My.Settings.ServerSSLIP)
      .txtDBName.Text = My.Settings.DBName
      .txtUserName.Text = If(CType(My.Settings.SSLMode, GlobalValues.SslMode) = GlobalValues.SslMode.None, My.Settings.SysUser, My.Settings.SysSSLUser)
      .txtPassword.Text = If(CType(My.Settings.SSLMode, GlobalValues.SslMode) = GlobalValues.SslMode.None, R.Decrypt(My.Settings.SysPassword), R.Decrypt(My.Settings.SysSSLPassword))
      .chkSSLMode.Checked = CType(My.Settings.SSLMode, Boolean)
    End With
    If dlgConnect.ShowDialog = Windows.Forms.DialogResult.OK Then
      With dlgConnect
        If .chkSSLMode.Checked Then
          My.Settings.ServerSSLIP = .txtSerwerIP.Text
          My.Settings.SysSSLUser = .txtUserName.Text.ToLower
          My.Settings.SysSSLPassword = R.Encrypt(.txtPassword.Text)
        Else
          My.Settings.ServerIP = .txtSerwerIP.Text
          'My.Settings.DBName = .txtDBName.Text
          My.Settings.SysUser = .txtUserName.Text.ToLower
          My.Settings.SysPassword = R.Encrypt(.txtPassword.Text)
        End If
        My.Settings.DBName = .txtDBName.Text
        My.Settings.SSLMode = CType(.chkSSLMode.CheckState, Byte)
        My.Settings.Save()
        Return True
      End With
    Else

    End If
    Return False
  End Function

  Public Function CheckUser(ByVal UserName As String, ByVal Password As String) As Boolean
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, HH As New HashHelper, U As New UsersSQL
    Try
      Dim cmd As MySqlCommand = DBA.CreateCommand(U.SelectLogin)
      cmd.Parameters.AddWithValue("?UserName", UserName)
      R = cmd.ExecuteReader()
      Dim spCmd As New MySqlCommand
      spCmd.Connection = GlobalValues.gblConn
      spCmd.CommandText = "LogIn"
      spCmd.CommandType = CommandType.StoredProcedure
      spCmd.Parameters.AddWithValue("?Nick", UserName)
      spCmd.Parameters("?Nick").Direction = ParameterDirection.Input
      spCmd.Parameters.AddWithValue("?IP", GlobalValues.gblIP)
      spCmd.Parameters("?IP").Direction = ParameterDirection.Input
      spCmd.Parameters.AddWithValue("?AppType", "D")
      spCmd.Parameters("?AppType").Direction = ParameterDirection.Input
      spCmd.Parameters.AddWithValue("?AppVer", My.Application.Info.Version.ToString)
      spCmd.Parameters("?AppVer").Direction = ParameterDirection.Input

      spCmd.Parameters.Add("?IdEvent", MySqlDbType.Int32)
      spCmd.Parameters("?IdEvent").Direction = ParameterDirection.Output
      If R.HasRows Then
        R.Read()
        If HH.ComparePasswords(CType(R.Item(1), Byte()), Password) Then
          GlobalValues.AppUser = New User
          GlobalValues.AppUser.Login = UserName
          GlobalValues.AppUser.Name = R.Item("FullName").ToString
          GlobalValues.AppUser.Role = CType(R.Item("Role"), GlobalValues.Role)
          GlobalValues.AppUser.TeacherID = R.Item("IdNauczyciel").ToString
          'GlobalValues.gblEnableCommands = CType(IIf(GlobalValues.AppUser.Role > 1, True, False), Boolean)
          R.Close()
          spCmd.Parameters.AddWithValue("?LoginStatus", 1)
          spCmd.Parameters("?LoginStatus").Direction = ParameterDirection.Input
          spCmd.ExecuteNonQuery()
          GlobalValues.gblIdEvent = CType(spCmd.Parameters("?IdEvent").Value, Long)
          Return True
        Else
          MessageBox.Show("Podane hasło jest nieprawidłowe lub użytkownik nie istnieje!", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          R.Close()
          spCmd.Parameters.AddWithValue("?LoginStatus", 0)
          spCmd.Parameters("?LoginStatus").Direction = ParameterDirection.Input
          spCmd.ExecuteNonQuery()
          Return False
        End If
      Else
        R.Close()
        MessageBox.Show("Podane hasło jest nieprawidłowe lub użytkownik nie istnieje!", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        spCmd.Parameters.AddWithValue("?LoginStatus", 0)
        spCmd.Parameters("?LoginStatus").Direction = ParameterDirection.Input
        spCmd.ExecuteNonQuery()
        Return False
      End If

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
      Return False
    Finally
      R.Close()
    End Try

  End Function
  Public Overloads Sub ChangePassword(ByVal Name As String)
    Dim HashHelper As New HashHelper, DBA As New DataBaseAction, U As New UsersSQL, dlgPassword As New dlgChangePassword
    With dlgPassword
      .Icon = GlobalValues.gblAppIcon
      .Text = "Zmiana hasła - tryb użytkownika"
      .txtLogin.Text = Name
      .txtLogin.Enabled = False
      .lblLogin.Enabled = False
      .OK_Button.Text = "Zmień"

      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        Try
          Dim SaltedPasswordHash As Byte()
          SaltedPasswordHash = HashHelper.CreateDBPassword(dlgPassword.txtPassword.Text)
          Dim cmd As MySqlCommand = DBA.CreateCommand(U.UpdatePassword())
          cmd.Parameters.AddWithValue("?Password", SaltedPasswordHash)
          cmd.Parameters.AddWithValue("?Login", Name)
          cmd.ExecuteNonQuery()
          MessageBox.Show("Hasło zostało zmienione")
        Catch mex As MySqlException
          MessageBox.Show("Wystąpił błąd:" + vbNewLine + mex.Message)
        Catch ex As Exception
          MessageBox.Show("Wystąpił błąd:" + vbNewLine + ex.Message)
        End Try
      End If
    End With
  End Sub
  Public Overloads Sub ChangePassword()
    Dim HashHelper As New HashHelper, DBA As New DataBaseAction, U As New UsersSQL, dlgPassword As New dlgChangePassword
    With dlgPassword
      .Text = "Zmiana hasła - tryb administratora"
      .txtLogin.Enabled = False
      .txtLogin.Visible = False
      .OK_Button.Text = "Zmień"
      Dim cbNazwa As New ComboBox
      cbNazwa.AutoCompleteSource = AutoCompleteSource.ListItems
      cbNazwa.AutoCompleteMode = AutoCompleteMode.Append
      cbNazwa.Name = "cbNazwa"
      cbNazwa.Location = .txtLogin.Location
      cbNazwa.Width = .txtLogin.Width
      cbNazwa.DropDownStyle = ComboBoxStyle.DropDownList
      cbNazwa.Parent = dlgPassword
      Dim FCB As New FillComboBox
      FCB.AddComboBoxSimpleItems(cbNazwa, U.SelectUsers)
      AddHandler cbNazwa.SelectedIndexChanged, AddressOf dlgPassword.cbNazwa_SelectedIndexChanged

      .Controls.Add(cbNazwa)

      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        Try
          Dim SaltedPasswordHash As Byte()
          SaltedPasswordHash = HashHelper.CreateDBPassword(.txtPassword.Text)
          Dim cmd As MySqlCommand = DBA.CreateCommand(U.UpdatePassword())
          cmd.Parameters.AddWithValue("?Password", SaltedPasswordHash)
          cmd.Parameters.AddWithValue("?Login", cbNazwa.Text)
          cmd.ExecuteNonQuery()
          MessageBox.Show("Hasło zostało zmienione")

        Catch mex As MySqlException
          MessageBox.Show("Wystąpił błąd:" + vbNewLine + mex.Message)
        Catch ex As Exception
          MessageBox.Show("Wystąpił błąd:" + vbNewLine + ex.Message)

        End Try

      End If
    End With
  End Sub
  Public Function GetSSLCipher() As String
    Dim R As MySqlDataReader = Nothing, DBA As New DataBaseAction, A As New AdminSQL
    Try
      R = DBA.GetReader(A.SelectSslCipher)
      R.Read()
      Return String.Concat(R.Item(0).ToString, ": ", R.Item(1).ToString)
    Catch mex As Exception
      Return mex.Message
    Finally
      If R IsNot Nothing Then R.Close()
    End Try

  End Function
End Class
