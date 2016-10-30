
Public Class GlobalValues
  Public Enum Role As Byte
    Administrator = 4
    Nauczyciel = 2
    Czytelnik = 1
    Rodzic = 0
  End Enum
  Public Enum UserStatus
    Aktywny = 1
    Nieaktywny = 0
  End Enum
  Public Enum EventStatus 'Status zdarzenia w terminarzu zdarzeń
    Zrealizowane = 1
    Zaplanowane = 0
  End Enum
  Public Enum ReasonStatus 'Status uzasadnienia oceny
    Brak = -1
    Wprowadzone = 0
    Przekazane = 1
    Odrzucone = 2
    Zatwierdzone = 3
  End Enum

  Public Enum GradeType
    P = 0
    Z = 1
    PZ = 2
  End Enum
  Public Enum GradeSubType
    C = 0
    K = 1
    CK = 2
  End Enum
  Public Enum SslMode
    None = 0
    Required = 1
  End Enum
  Public Shared gblConn As MySqlConnection = Nothing
  Public Shared gblIP, gblHostName, gblSysUser As String
  Public Shared gblAppIcon As New System.Drawing.Icon(Application.StartupPath & "\belfer.ico")
  Public Shared gblIdEvent As Long
  Public Shared Users As Hashtable
  Public Shared AppUser As User
End Class
Public Class User
  Public Property Login As String = ""
  Public Property Name As String = ""
  Public Property Role As GlobalValues.Role = Nothing
  Public Property TeacherID As String = ""
  Public Property SchoolTeacherID As String = ""
  Public Property TutorClassID As String = ""
  Public Property TutorClassName As String = ""
  Public Overrides Function ToString() As String
    Dim Info As String
    Info = String.Concat(Login, " (", Name, ")")
    Return Info.ToString()
  End Function
  Public Sub Reset()
    Login = ""
    Name = ""
    Role = Nothing
    TeacherID = ""
    SchoolTeacherID = ""
    TutorClassID = ""
    TutorClassName = ""
  End Sub
End Class

Public Class SharedImport
  Public Shared pbValue, pbMaxValue, SuccessValue, ErrorValue, TotalValue As Integer, InfoValue, ExtendedInfoValue As String
  Public Shared Event OnRecordForward()
  Public Shared Event OnpbMaxValueChange()
  Public Shared Event OnRoutineChange()
  Shared Sub RoutineChange()
    RaiseEvent OnRoutineChange()
  End Sub
  Shared Sub pbMaxValueChange()
    RaiseEvent OnpbMaxValueChange()
  End Sub
  Shared Sub RecordForward()
    RaiseEvent OnRecordForward()
  End Sub
End Class


Public Class SharedExport
  Public Shared pbValue, pbMaxValue, SuccessValue, ErrorValue, TotalValue As Integer, InfoValue, ExtendedInfoValue As String
  Public Shared Event OnRecordForward()
  Public Shared Event OnpbMaxValueChange()
  Public Shared Event OnRoutineChange()
  Public Shared Event OnDetailedRoutineChange()
  Shared Sub DetailedRoutineChange()
    RaiseEvent OnDetailedRoutineChange()
  End Sub
  Shared Sub RoutineChange()
    RaiseEvent OnRoutineChange()
  End Sub
  Shared Sub pbMaxValueChange()
    RaiseEvent OnpbMaxValueChange()
  End Sub
  Shared Sub RecordForward()
    RaiseEvent OnRecordForward()
  End Sub
End Class


Public Class SharedException
  Public Shared ErrorMessage As String, ErrorNumber As Integer
  Public Shared Event OnErrorGenerate()

  Shared Sub GenerateError(ByVal errNumber As Integer, ByVal errMessage As String)
    ErrorMessage = errMessage
    ErrorNumber = errNumber
    RaiseEvent OnErrorGenerate()
  End Sub

End Class


Public Class SharedKonfiguracja
  Public Shared Event OnKonfiguracjaChanged()
  Shared Sub KonfiguracjaChanged()
    RaiseEvent OnKonfiguracjaChanged()
  End Sub
End Class

'Public Class SharedPreviewMode
'  Public Shared Event OnPreviewModeChanged(ByVal IsPreview As Boolean)
'  Shared Sub PreviewModeChanged(ByVal IsPreview As Boolean)
'    RaiseEvent OnPreviewModeChanged(IsPreview)
'  End Sub
'End Class