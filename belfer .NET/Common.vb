Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
'Imports System.Text.RegularExpressions

Public Class CalcHelper
  
  Function CurrentSchoolYear() As String
    Dim Year As Integer
    Year = StartDateOfSchoolYear.Year
    Return Year & "/" & Year + 1
  End Function
  Public Function GetDateFromPesel(ByVal Pesel As String) As Date
    If CType(Pesel.Trim.Substring(2, 2), Byte) - 20 > 0 AndAlso CType(Pesel.Trim.Substring(2, 2), Byte) - 20 < 13 Then
      'Return DateSerial(2000 + CType(Pesel.Trim.Substring(0, 2), Integer), CType(Pesel.Trim.Substring(2, 2), Integer) - 20, CType(Pesel.Trim.Substring(4, 2), Integer))
      Return New Date(2000 + CType(Pesel.Trim.Substring(0, 2), Integer), CType(Pesel.Trim.Substring(2, 2), Integer) - 20, CType(Pesel.Trim.Substring(4, 2), Integer))
    ElseIf CType(Pesel.Trim.Substring(2, 2), Byte) - 80 > 0 Then
      'Return DateSerial(1800 + CType(Pesel.Trim.Substring(0, 2), Integer), CType(Pesel.Trim.Substring(2, 2), Integer) - 80, CType(Pesel.Trim.Substring(4, 2), Integer))
      Return New Date(1800 + CType(Pesel.Trim.Substring(0, 2), Integer), CType(Pesel.Trim.Substring(2, 2), Integer) - 80, CType(Pesel.Trim.Substring(4, 2), Integer))
    Else
      'Return DateSerial(1900 + CType(Pesel.Trim.Substring(0, 2), Integer), CType(Pesel.Trim.Substring(2, 2), Integer), CType(Pesel.Trim.Substring(4, 2), Integer))
      Return New Date(1900 + CType(Pesel.Trim.Substring(0, 2), Integer), CType(Pesel.Trim.Substring(2, 2), Integer), CType(Pesel.Trim.Substring(4, 2), Integer))
    End If
  End Function
  Public Function GetSexFromPesel(ByVal Pesel As String) As String
    Dim Sex() As String = New String() {"K", "M"}
    Return Sex(CType(Pesel.Trim.Substring(9, 1), Byte) Mod 2)
  End Function
  Public Function GetTextLength(ByVal Text As String, ByVal G As Graphics, ByVal TextFont As Font) As Single
    'Dim CH As New CalcHelper
    Return InToMM(G.MeasureString(Text, TextFont).Width)
  End Function
  Public Function InToMM(ByVal Inch As Single) As Single
    Return Inch / CSng(3.937)
  End Function
  Public Function InvertColor(K As Color) As Color
    Dim R, G, B As Byte
    R = CByte(255) - K.R
    G = CByte(255) - K.G
    B = CByte(255) - K.B
    Return Color.FromArgb(R, G, B)
  End Function
  Public Function MMtoIn(ByVal mm As Single) As Single
    Return mm * CSng(3.937)
  End Function

  Public Overloads Function StartDateOfSchoolYear() As Date
    If Date.Today.Month > 8 Then
      Return New Date(Date.Today.Year, 9, 1)
    Else
      Return New Date(Date.Today.Year - 1, 9, 1)
    End If
  End Function
  Public Overloads Function StartDateOfSchoolYear(SchoolYear As String) As Date
    Return New Date(CType(SchoolYear.Substring(0, 4), Integer), 9, 1)
  End Function
  Public Function EndDateOfSchoolYear(SchoolYear As String) As Date
    Return New Date(CType(SchoolYear.Substring(5, 4), Integer), 8, 31)
  End Function
  Public Function StartDateOfSemester2(ByVal StartYearOfSchoolYear As Integer) As Date
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("Semester2StartDate", "G", My.Settings.IdSchool))
      If R.HasRows Then
        R.Read()
        Return New Date(StartYearOfSchoolYear + 1, CType(R(0).ToString.Substring(0, 2), Integer), CType(R(0).ToString.Substring(2, 2), Integer))
        'Return New Date(StartYearOfSchoolYear + 1, CType(R(0), Date).Month, CType(R(0), Date).Day)

      End If
      Return New Date(StartYearOfSchoolYear + 1, 2, 1)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
    Return Today
  End Function
  Public Function StartDateOfWeek(DateValue As Date) As Date
    Dim Offset As Byte, LastDayOfWeek As Byte = 7
    Offset = LastDayOfWeek - CType(Weekday(DateValue, FirstDayOfWeek.Monday), Byte)
    Return DateValue.AddDays(Offset).AddDays((LastDayOfWeek - 1) * -1)
  End Function
  Public Function EndDateOfWeek(DateValue As Date) As Date
    Dim Offset As Byte, LastDayOfWeek As Byte = 7
    Offset = LastDayOfWeek - CType(Weekday(DateValue, FirstDayOfWeek.Monday), Byte)
    Return DateValue.AddDays(Offset)

  End Function

  Public Function ValidateHexColor(ByVal strInput As String) As Boolean
    'create Regular Expression Match pattern object
    Dim myRegex As New RegularExpressions.Regex("^\#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$")
    'boolean variable to hold the status
    Dim isValid As Boolean = False
    If strInput = "" Then
      isValid = False
    Else
      isValid = myRegex.IsMatch(strInput)
    End If
    'return the results
    Return isValid
  End Function
  Function ValidatePesel(ByVal strPesel As String) As Boolean
    Dim intTotal As Integer, i, bytTemp, bytRest, bytCtrlFigure As Byte

    Try
      If strPesel.Trim.Length <> 11 Then Return False
      If IsNumeric(strPesel.Trim) Then
        Const Waga As String = "1379137913"
        intTotal = 0
        For i = 0 To 9
          bytTemp = CType(Waga.Substring(i, 1), Byte) * CType(strPesel.Trim.Substring(i, 1), Byte)
          intTotal = intTotal + bytTemp
        Next
        bytRest = CType(intTotal Mod 10, Byte)
        bytCtrlFigure = CType(IIf(10 - bytRest = 10, 0, 10 - bytRest), Byte)
        Return CType(IIf(bytCtrlFigure = CType(strPesel.Trim.Substring(10, 1), Byte), True, False), Boolean)
      End If
      Return False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return False
    End Try
  End Function
  Function ValidateNIP(ByVal strNip As String) As Boolean
    Dim intTotal As Long, i, bytTemp, bytCtrlFigure As Byte
    Dim SH As New StringHelper
    If strNip.Trim.Length = 10 AndAlso SH.DigitOnly(strNip) Then
      Const Waga As String = "657234567"
      intTotal = 0
      For i = 1 To 9
        bytTemp = CByte(Mid(Waga, i, 1)) * CByte(Mid(strNip, i, 1))
        intTotal = intTotal + bytTemp
      Next
      bytCtrlFigure = CType(intTotal Mod 11, Byte)
      Return CType(IIf(bytCtrlFigure = CByte(Mid(strNip, 10, 1)), True, False), Boolean)
    End If
    Return False
  End Function
End Class
Class CbItem
  Private FID As Integer, FNazwa As String, FKod As String
  Sub New(ByVal ID As Integer, ByVal Nazwa As String)
    FID = ID
    FNazwa = Nazwa
  End Sub
  Sub New(ByVal Kod As String, ByVal Nazwa As String)
    FKod = Kod
    FNazwa = Nazwa
  End Sub
  Sub New(ID As Integer, ByVal Kod As String, ByVal Nazwa As String)
    FID = ID
    FKod = Kod
    FNazwa = Nazwa
  End Sub
  Public Property Nazwa() As String
    Get
      Nazwa = FNazwa
    End Get
    Set(ByVal value As String)
      FNazwa = value
    End Set
  End Property
  Public Property Kod() As String
    Get
      Kod = FKod
    End Get
    Set(ByVal value As String)
      FKod = value
    End Set
  End Property

  Public Property ID() As Integer
    Get
      ID = FID
    End Get
    Set(ByVal value As Integer)
      FID = value
    End Set
  End Property

  Public Overrides Function ToString() As String
    ToString = FNazwa
  End Function
End Class

Public Class DataBaseAction
  Public Function Connection(ByVal Server$, ByVal Baza$, ByVal User$, ByVal Password$, SSL As GlobalValues.SslMode) As MySqlConnection
    'Dim N As New Net
    Dim Conn As New MySqlConnection
    Try
      Conn.ConnectionString = "server=" & Server & ";user id=" & User & ";password=" & Password & ";database=" & Baza & ";CharSet=utf8;Keepalive=60;SSL Mode=" & SSL.ToString & ";" 'CertificateFile=belfer.pfx;CertificatePassword=pass;"
      'Conn.ConnectionString = "server=" & Server & ";user id=" & User & ";password=" & Password & ";database=" & Baza & ";CharSet=utf8;Keepalive=60" 'ConnectionString(Server, Baza, User, Password)

      Conn.Open()

    Catch err As MySqlException
      Select Case err.Number
        Case 0
          MessageBox.Show("Nie można połączyć się z serwerem." & vbCr & "Skontaktuj się z administratorem serwera.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Case 1044
          'MessageBox.Show("Baza danych '" + Baza + "' nie istnieje w podanej lokalizacji - '" + Server + "'." + vbCr + "Skontaktuj się z administratorem serwera baz danych MySQL.")
          MessageBox.Show(err.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Case 1045
          MessageBox.Show("Błędna nazwa użytkownika i (lub) hasło." & vbCr & "Wpisz poprawną nazwę i spróbuj jeszcze raz.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
          'MessageBox.Show(err.Message)
        Case Else
          MessageBox.Show("Nie można uzyskać połączenia: " & vbCr & err.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Select
    End Try
    Return Conn
  End Function

  Public Overloads Sub ApplyChanges(ByVal SQLString As String)
    Dim cmd As MySqlCommand = CreateCommand(SQLString)
    Try
      cmd.ExecuteNonQuery()
    Catch err As MySqlException
      Select Case err.Number
        Case 1451
          MessageBox.Show("Nie można usunąć lub zaktualizować rekordu nadrzędnego" & vbCr & "z powodu istniejącej relacji między tabelami." & vbCr & "Musisz najpierw usunąć lub zaktualizować rekordy z nim powiązane.")
        Case 1062
          MessageBox.Show("Wprowadzona wartość już istnieje w bazie danych.")
        Case Else
          MessageBox.Show(err.Number & vbCr & err.Message)
      End Select
    End Try
  End Sub
  Public Overloads Sub ApplyChanges(ByVal SQLString As String, ByVal Transaction As MySqlTransaction)
    Dim cmd As MySqlCommand = CreateCommand(SQLString)
    cmd.Transaction = Transaction
    Try
      cmd.ExecuteNonQuery()
    Catch err As MySqlException
      SharedException.GenerateError(err.Number, err.Message)

    End Try
  End Sub
  Public Overloads Sub ApplyChanges(ByVal cmd As MySqlCommand)
    Try
      cmd.ExecuteNonQuery()
    Catch err As MySqlException
      SharedException.GenerateError(err.Number, err.Message)
    End Try
  End Sub
  Public Overloads Sub ApplyChanges(ByVal cmd As MySqlCommand, ByVal Import As Boolean)
    Try
      cmd.ExecuteNonQuery()
    Catch err As MySqlException
      If Import Then
        SharedException.GenerateError(err.Number, err.Message)
      Else
        MessageBox.Show(err.Message)
      End If
    End Try
  End Sub
  Public Function ComputeRecords(ByVal SQLString As String) As Integer
    Try
      Dim CMD As MySqlCommand = CreateCommand(SQLString)
      If CMD.ExecuteScalar IsNot DBNull.Value Then
        Return CType(CMD.ExecuteScalar(), Integer)
      Else
        Return 0
      End If
    Catch err As MySqlException
      MessageBox.Show(err.Number & vbCr & err.Message)
    End Try
    Return 0
  End Function
  Public Function CreateCommand(ByVal SQLString As String) As MySqlCommand
    Dim Cmd As New MySqlCommand(SQLString, GlobalValues.gblConn)
    Try
      Cmd.CommandType = CommandType.Text
    Catch err As MySqlException
      MessageBox.Show(err.Number & vbCr & err.Message)
    End Try
    Return Cmd
  End Function
  Public Function GetDataTable(ByVal SQLString As String) As DataTable
    Dim Adapter As MySqlDataAdapter = SetAdapter(SQLString)
    Dim DT As New DataTable
    Try
      Adapter.Fill(DT)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
    Return DT
  End Function
  Public Function GetDataSet(ByVal SQLString As String) As DataSet
    Dim Adapter As MySqlDataAdapter = SetAdapter(SQLString)
    Dim Ds As New DataSet
    Try
      Adapter.Fill(Ds)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
    Return Ds
  End Function
  Public Function GetReader(ByVal SQLString As String) As MySqlDataReader
    Dim Reader As MySqlDataReader = Nothing
    Dim Cmd As MySqlCommand = CreateCommand(SQLString)
    Try
      Reader = Cmd.ExecuteReader()
    Catch err As MySqlException
      MessageBox.Show(err.Number & vbCr & err.Message)
    End Try
    Return Reader
  End Function
  Public Function GetLastInsertedID() As String
    Try
      Dim cmd As MySqlCommand = CreateCommand("Select LAST_INSERT_ID();")
      Return cmd.ExecuteScalar().ToString
    Catch err As MySqlException
      MessageBox.Show(err.Number & vbCr & err.Message)
      Return ""
    End Try
  End Function

  Public Function GetMaxValue(ByVal SQLString As String) As Integer
    Try
      Dim cmd As MySqlCommand = CreateCommand(SQLString)
      If cmd.ExecuteScalar Is DBNull.Value Then Return 0
      Return CType(cmd.ExecuteScalar(), Integer)
    Catch err As MySqlException
      MessageBox.Show(err.Number & vbCr & err.Message)
    End Try
    Return 0
  End Function
  Public Function GetSingleValue(ByVal SQLString As String) As String
    'Executes the query, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored. 
    Try
      Dim cmd As MySqlCommand = CreateCommand(SQLString)
      If cmd.ExecuteScalar Is DBNull.Value Then Return ""
      If cmd.ExecuteScalar Is Nothing Then Return ""
      Return cmd.ExecuteScalar().ToString
    Catch err As MySqlException
      MessageBox.Show(err.Number & vbCr & err.Message)
      Return ""
    End Try
    'Return 0
  End Function

  Public Function SetAdapter(ByVal SQLString As String) As MySqlDataAdapter
    Dim Adapter As New MySqlDataAdapter
    Try
      Adapter.SelectCommand = CreateCommand(SQLString)
    Catch err As MySqlException
      MessageBox.Show(err.Number & vbCr & err.Message)
    End Try
    Return Adapter
  End Function
End Class


Class FillComboBox
  Public Overloads Sub AddComboBoxComplexItems(ByRef ctlName As ComboBox, ByVal SelectString As String, Optional ByVal Kod As Boolean = False)
    Dim Reader As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction
    ctlName.Items.Clear()
    Try
      Reader = DBA.GetReader(SelectString)
      While Reader.Read()
        If Kod = True Then
          ctlName.Items.Add(New CbItem(CStr(Reader.Item(0)), Reader.Item(1).ToString))
        Else
          ctlName.Items.Add(New CbItem(CInt(Reader.Item(0)), Reader.Item(1).ToString))
        End If
      End While
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      Reader.Close()
    End Try
  End Sub
  Public Overloads Sub AddComboBoxExtendedItems(ByRef ctlName As ComboBox, ByVal SelectString As String)
    Dim Reader As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction
    ctlName.Items.Clear()
    Try
      Reader = DBA.GetReader(SelectString)
      While Reader.Read()
        ctlName.Items.Add(New CbItem(CInt(Reader.Item(0)), Reader.Item(1).ToString, Reader.Item(2).ToString))
      End While
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      Reader.Close()
    End Try
  End Sub
  Public Sub AddComboBoxSimpleItems(ByRef ctlName As ComboBox, ByVal SelectString As String)
    Dim Reader As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction
    Try
      Reader = DBA.GetReader(SelectString)
      Dim i As Integer = 0
      While Reader.Read()
        ctlName.Items.Add(New CbItem(i, Reader.Item(0).ToString))
        i += 1
      End While
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      Reader.Close()
    End Try
  End Sub
  Public Sub AddListBoxComplexItems(ByRef ctlName As ListBox, ByVal SelectString As String)
    Dim Reader As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction
    Try
      Reader = DBA.GetReader(SelectString)
      While Reader.Read()
        ctlName.Items.Add(New CbItem(CInt(Reader.Item(0)), Reader.Item(1).ToString))
      End While
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      Reader.Close()
    End Try
  End Sub
End Class


Public Class HashHelper
  Private Const SaltLength As Integer = 4
  Public Function CreateDBPassword(ByVal Password As String) As Byte()
    Dim UnsaltedPassword() As Byte = CreatePasswordHash(Password)
    Dim SaltValue(SaltLength - 1) As Byte
    Dim Rng As New RNGCryptoServiceProvider
    Rng.GetBytes(SaltValue)
    Return CreateSaltedPassword(SaltValue, UnsaltedPassword)
  End Function
  Private Function CreatePasswordHash(ByVal Password As String) As Byte()
        Dim Sha1 As New SHA1Managed
        Dim b As Byte()
        b = System.Text.Encoding.UTF8.GetBytes(Password)
        Return Sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password))
  End Function
  Private Function CreateSaltedPassword(ByVal SaltValue As Byte(), ByVal UnsaltedPassword() As Byte) As Byte()
    Dim RawSalted(UnsaltedPassword.Length + SaltValue.Length - 1) As Byte
    UnsaltedPassword.CopyTo(RawSalted, 0)
    SaltValue.CopyTo(RawSalted, UnsaltedPassword.Length)
    Dim Sha1 As New SHA1Managed
    Dim SaltedPassword() As Byte = Sha1.ComputeHash(RawSalted)
    Dim DbPassword(SaltedPassword.Length + SaltValue.Length - 1) As Byte
    SaltedPassword.CopyTo(DbPassword, 0)
    SaltValue.CopyTo(DbPassword, SaltedPassword.Length)
    Return DbPassword
  End Function
  Public Function ComparePasswords(ByVal StoredPassword() As Byte, ByVal SuppliedPassword As String) As Boolean
    Try
      If StoredPassword.Length < SaltLength Then Return False
      Dim SaltValue(SaltLength - 1) As Byte
      Dim SaltOffset As Integer = StoredPassword.Length - SaltLength
      Dim i As Integer
      For i = 0 To SaltLength - 1
        SaltValue(i) = StoredPassword(SaltOffset + i)
      Next
      Dim HashedPassword As Byte() = CreatePasswordHash(SuppliedPassword)
      Dim SaltedPassword As Byte() = CreateSaltedPassword(SaltValue, HashedPassword)
      Return CompareByteArray(StoredPassword, SaltedPassword)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return False
    End Try

  End Function
  ' Compare the contents of two byte arrays.
  Private Function CompareByteArray(ByVal array1 As Byte(), ByVal array2 As Byte()) As Boolean
    If (array1.Length <> array2.Length) Then Return False
    Dim i As Integer
    For i = 0 To array1.Length - 1
      If Not array1(i).Equals(array2(i)) Then Return False
    Next
    Return True
  End Function
End Class

Public Class Net
  Public Function ComputerIP() As String
    Dim HostName, IPAddress As String
    HostName = System.Net.Dns.GetHostName
    IPAddress = System.Net.Dns.GetHostEntry(HostName).AddressList(0).ToString
    Return IPAddress
  End Function
  Public Function ComputerIPv4() As String
    Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)

    For Each IPaddress In IPList.AddressList
      'Only return IPv4 routable IPs
      'MessageBox.Show(IPaddress.AddressFamily.Unspecified.ToString)
      If (IPaddress.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork) Then
        Return IPaddress.ToString
      End If
    Next
    Return ""
  End Function
  Public Function ComputerName() As String
    Return System.Net.Dns.GetHostName()
  End Function
End Class

Public Class OptionHolder
  Public Function ColNumber() As Byte
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("ColNumber", "G", My.Settings.IdSchool))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Byte)
      End If
      Return 10
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
    Return 0
  End Function
  Public Function DataRP(Okres As String, Data As Date) As Date
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption(If(Okres = "S", "DataRPSemestr", "DataRPRokSzkolny"), "G", My.Settings.IdSchool, Data))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Date)
      End If
      Return Nothing
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return Nothing
    Finally
      R.Close()
    End Try
  End Function
  Public Function PrevYearObjectToMaxPionAvg(Data As Date) As Boolean
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("PrevYearMaxPionAvg", "G", My.Settings.IdSchool, Data))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Boolean)
      End If
      Return False
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return False
    Finally
      R.Close()
    End Try
  End Function
  Public Function DBVersion() As String
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("DBVersion", "G"))
      If R.HasRows Then
        R.Read()
        Return R(0).ToString
      End If
      Return "0.0"
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
    Return "0.0"
  End Function
  Public Function MaxColNumber() As Integer
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("MaxColNumber", "G", My.Settings.IdSchool))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Integer)
      End If
      Return 100
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
    Return 0
  End Function
  Public Function GetMaxPion() As Byte
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("MaxPion", "G", My.Settings.IdSchool))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Byte)
      End If
      Return 3
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
    Return 0
  End Function
  Public Function GetMinPion() As Byte
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("MinPion", "G", My.Settings.IdSchool))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Byte)
      End If
      Return 1
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
    Return 0
  End Function
  Public Function GetPasekAvg(Data As Date) As Single
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("PasekAvg", "G", My.Settings.IdSchool, Data))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Single)
      End If
      Return 4.75
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return 4.75
    Finally
      R.Close()
    End Try
  End Function
  Public Function GetDyplomAvg(Data As Date) As Single
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("DyplomAvg", "G", My.Settings.IdSchool, Data))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Single)
      End If
      Return 4.5
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return 4.5
    Finally
      R.Close()
    End Try
  End Function
  Public Function GetPasekBehaviorScore(Data As Date) As Byte
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("PasekBehavior", "G", My.Settings.IdSchool, Data))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Byte)
      End If
      Return 5
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return 5
    Finally
      R.Close()
    End Try
  End Function
  Public Function GetDyplomBehaviorScore(Data As Date) As Byte
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("DyplomBehavior", "G", My.Settings.IdSchool, Data))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Byte)
      End If
      Return 5
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return 5
    Finally
      R.Close()
    End Try
  End Function
  Public Function GetDyplomFrekwencja(Data As Date) As Single
    Dim DBA As New DataBaseAction, R As MySqlDataReader = Nothing, O As New OpcjeSQL
    Try
      R = DBA.GetReader(O.SelectOption("DyplomFrekwencja", "G", My.Settings.IdSchool, Data))
      If R.HasRows Then
        R.Read()
        Return CType(R(0), Single)
      End If
      Return 100
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return 100
    Finally
      R.Close()
    End Try
  End Function
End Class

Public Class Rijndael

  ' The key and initialization vector : change them for your application
  Private MyKey() As Byte = {12, 28, 36, 222, 45, 72, 10, 5, 97, 180, 52, 123, 37, 28, 255, 74}
  Private MyIV() As Byte = {2, 13, 28, 32, 20, 147, 252, 45, 63, 52, 96, 85, 42, 32, 159, 241}


  ' decrypts a string that was encrypted using the Encrypt method
  Public Function Decrypt(ByVal CipherText As String) As String 'Implements ICrypto.Decrypt
    Try
      Dim inBytes() As Byte = Convert.FromBase64String(CipherText)
      Dim mStream As New MemoryStream(inBytes, 0, inBytes.Length) ' instead of writing the decrypted text

      Dim aes As New RijndaelManaged()
      Dim cs As New CryptoStream(mStream, aes.CreateDecryptor(MyKey, MyIV), CryptoStreamMode.Read)
      Dim sr As New StreamReader(cs)

      Return sr.ReadToEnd()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return ""
      'Throw ex
    End Try
  End Function

  ' Encrypts a given string
  Public Function Encrypt(ByVal PlainText As String) As String
    Try

      Dim utf8 As New UTF8Encoding
      Dim inBytes() As Byte = utf8.GetBytes(PlainText) ' ascii encoding uses 7 
      'bytes for characters whereas the encryption uses 8 bytes, thus we use utf8
      Dim ms As New MemoryStream() 'instead of writing the encrypted 
      'string to a filestream, I will use a memorystream

      Dim aes As New RijndaelManaged()

      Dim cs As New CryptoStream(ms, aes.CreateEncryptor(MyKey, MyIV), CryptoStreamMode.Write)

      cs.Write(inBytes, 0, inBytes.Length) ' encrypt
      cs.FlushFinalBlock()

      Return Convert.ToBase64String(ms.GetBuffer(), 0, CType(ms.Length, Integer))
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return ""
    End Try
  End Function

End Class

Public Class SeekHelper
  Public Overloads Sub FindListViewItem(ByVal LV As ListView, ByVal ItemText As String)
    If Not LV.FindItemWithText(ItemText) Is Nothing Then
      LV.Focus()
      LV.Items(LV.FindItemWithText(ItemText).Index).Selected = True
      LV.SelectedItems(0).EnsureVisible()
      LV.HideSelection = False
    End If
  End Sub
  'Public Overloads Sub FindListViewItem(ByVal LV As BetterListView, ByVal ItemText As String)
  '  If Not LV.FindItemWithText(ItemText) Is Nothing Then
  '    LV.Focus()
  '    LV.Items(LV.FindItemWithText(ItemText).Index).Selected = True
  '    LV.SelectedItems(0).EnsureVisible()
  '    LV.HideSelection = False
  '  End If
  'End Sub
  Public Overloads Sub FindPostRemovedListViewItemIndex(ByVal LV As ListView, ByVal DeletedIndex As Integer)
    If LV.Items.Count > 0 Then
      LV.SelectedItems.Clear()
      LV.Select()
      If DeletedIndex = LV.Items.Count Then
        LV.Items(DeletedIndex - 1).Selected = True
      ElseIf DeletedIndex > LV.Items.Count Then
        LV.Items(LV.Items.Count - 1).Selected = True
      Else
        LV.Items(DeletedIndex).Selected = True
      End If
      LV.SelectedItems(0).EnsureVisible()
    End If
  End Sub
  'Public Overloads Sub FindPostRemovedListViewItemIndex(ByVal LV As BetterListView, ByVal DeletedIndex As Integer)
  '  If LV.Items.Count > 0 Then
  '    LV.SelectedItems.Clear()
  '    LV.Select()
  '    If DeletedIndex = LV.Items.Count Then
  '      LV.Items(DeletedIndex - 1).Selected = True
  '    ElseIf DeletedIndex > LV.Items.Count Then
  '      LV.Items(LV.Items.Count - 1).Selected = True
  '    Else
  '      LV.Items(DeletedIndex).Selected = True
  '    End If
  '    LV.SelectedItems(0).EnsureVisible()
  '  End If
  'End Sub
  Public Overloads Sub FindComboItem(ByVal CB As ComboBox, ByVal ItemText As String)
    For Each Item As CbItem In CB.Items
      If Item.Nazwa = ItemText OrElse Item.Kod = ItemText Then
        CB.SelectedIndex = CB.Items.IndexOf(Item)
        Exit For
      End If
    Next
  End Sub
  Public Overloads Sub FindComboItem(ByVal CB As ComboBox, ByVal ItemID As Integer)
    Try
      For Each Item As CbItem In CB.Items
        If Item.ID = ItemID Then
          CB.SelectedIndex = CB.Items.IndexOf(Item)
          Exit For
        End If
      Next
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Public Function GetDefault(ByVal SelectString As String) As String
    Dim DBA As New DataBaseAction
    Try
      Dim cmd As MySqlCommand = DBA.CreateCommand(SelectString)
      If Len(CStr(cmd.ExecuteScalar())) = 0 Then
        Return CStr(-1)
      Else
        Return cmd.ExecuteScalar().ToString
      End If
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return ""
    End Try
  End Function
End Class

Public Class ShapeDrawer
  Public Sub DrawSeparator(G As Graphics, x As Single, y As Single, x1 As Single)
    Dim LinePen As New Pen(Color.White)
    G.DrawLine(LinePen, x, y, x1, y)
  End Sub
  Public Sub DrawEndArrow(G As Graphics, x As Single, y As Single, LineLength As Single, ArrowLength As Single, ArrowHeight As Single, FillArrow As Boolean)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    Dim P As New Pen(Brushes.Black)
    P.CustomEndCap = New System.Drawing.Drawing2D.AdjustableArrowCap(ArrowHeight, ArrowLength, FillArrow)
    G.DrawLine(P, x, y, x + LineLength, y)
  End Sub
  Public Sub DrawStartArrow(G As Graphics, x As Single, y As Single, LineLength As Single, ArrowLength As Single, ArrowHeight As Single, FillArrow As Boolean)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    Dim P As New Pen(Brushes.Black)
    P.CustomStartCap = New System.Drawing.Drawing2D.AdjustableArrowCap(ArrowHeight, ArrowLength, FillArrow)
    G.DrawLine(P, x, y, x + LineLength, y)
  End Sub
  Public Sub DrawBothArrow(G As Graphics, x As Single, y As Single, LineLength As Single, ArrowLength As Single, ArrowHeight As Single, FillArrow As Boolean)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    Dim P As New Pen(Brushes.Black)
    P.CustomStartCap = New System.Drawing.Drawing2D.AdjustableArrowCap(ArrowHeight, ArrowLength, FillArrow)
    P.CustomEndCap = New System.Drawing.Drawing2D.AdjustableArrowCap(ArrowHeight, ArrowLength, FillArrow)
    G.DrawLine(P, x, y, x + LineLength, y)
  End Sub
End Class

'Poniższa klasa zawiera funkcje pomocne w sprawdzaniu poprawności wprowadzanych danych
Public Class StringHelper
  Public Function AllowedChars(ByVal str As String, Optional ByVal SpaceAllowed As Boolean = False) As Boolean
    Dim i As Integer
    Dim ZnakiDozwolone As String = "AĄBCĆDEĘFGHIJKLŁMNŃOÓQPRSŚTUWVXYZŻŹ.- "
    If Not SpaceAllowed Then ZnakiDozwolone = ZnakiDozwolone.TrimEnd
    For i = 0 To str.Trim.Length - 1
      If InStr(1, ZnakiDozwolone, str.Trim.Chars(i), CompareMethod.Text) < 1 Then Return False
    Next
    Return True
  End Function
  Function CapitalizeFirst(ByVal str As String) As String
    'Zamiana pierwszej litery na wielka; pozostałe litery bez zmian.
    Return StrConv(str.Trim, vbProperCase)
  End Function
  Public Function DigitOnly(ByVal Text As String) As Boolean
    If Text.Length = 0 Then Return False
    For Each T As Char In Text
      If Not Char.IsDigit(T) Then Return False
    Next
    Return True
  End Function
  Public Function LetterOrDigit(ByVal Text As String) As Boolean
    For Each T As Char In Text
      If Not Char.IsLetterOrDigit(T) Then Return False
    Next
    Return True
  End Function
End Class