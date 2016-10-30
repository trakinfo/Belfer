Imports System.Xml
Imports System.IO
Public Class ImportStudent

  Public Sub ReadXmlData(xmlDoc As XmlDocument, DataAktywacji As Date)
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, S As New StudentSQL
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      SharedImport.pbValue = 0
      SharedImport.pbMaxValue = SharedImport.TotalValue
      SharedImport.pbMaxValueChange()

      Dim Node As XmlNode
      Node = xmlDoc.DocumentElement
      For Each Node In xmlDoc.DocumentElement.ChildNodes()
        SharedImport.ExtendedInfoValue = "Klasa " & Node.Attributes(0).Value
        SharedImport.InfoValue = "Klasa " & Node.Attributes(0).Value
        SharedImport.RoutineChange()
        Dim IdSzkolaKlasa As String, dtKlasa As DataTable = DBA.GetDataTable(S.SelectKlasa(My.Settings.IdSchool))

        If dtKlasa.Select("Kod_Klasy='" & Node.Attributes(0).Value & "'").GetLength(0) < 1 Then
          Dim cmd As MySqlCommand = DBA.CreateCommand(S.InsertKlasa)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
          cmd.Parameters.AddWithValue("?Kod_Klasy", Node.Attributes(0).Value)
          cmd.Parameters.AddWithValue("?Nazwa_Klasy", Node.Attributes(0).Value)
          cmd.Parameters.AddWithValue("?Owner", GlobalValues.AppUser.Login)
          cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
          cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
          cmd.ExecuteNonQuery()
          IdSzkolaKlasa = cmd.LastInsertedId.ToString
        Else
          IdSzkolaKlasa = dtKlasa.Select("Kod_Klasy='" & Node.Attributes(0).Value & "'")(0).Item("ID").ToString
        End If

        Dim DT As DataTable = DBA.GetDataTable(S.SelectStudent)
        For Each subNode As XmlNode In Node
          If DT.Select("Student='" & String.Concat(subNode.Attributes(0).Value, " ", subNode.Attributes(1).Value, " ", subNode.Attributes(2).Value).TrimEnd & "' AND DataUr='" & subNode.Attributes(6).Value & "'").GetLength(0) < 1 Then
            Dim cmdStudent As MySqlCommand = DBA.CreateCommand(S.ImportStudent)
            cmdStudent.Transaction = MySQLTrans
            cmdStudent.Parameters.AddWithValue("?Nazwisko", subNode.Attributes(0).Value)
            cmdStudent.Parameters.AddWithValue("?Imie", subNode.Attributes(1).Value)
            cmdStudent.Parameters.AddWithValue("?Imie2", subNode.Attributes(2).Value)
            If subNode.Attributes(3).Value.Length > 0 Then
              cmdStudent.Parameters.AddWithValue("?NrArkusza", subNode.Attributes(3).Value)
            Else
              cmdStudent.Parameters.AddWithValue("?NrArkusza", DBNull.Value)
            End If
            cmdStudent.Parameters.AddWithValue("?ImieOjca", subNode.Attributes(4).Value)
            cmdStudent.Parameters.AddWithValue("?ImieMatki", subNode.Attributes(5).Value)
            cmdStudent.Parameters.AddWithValue("?DataUr", subNode.Attributes(6).Value)
            cmdStudent.Parameters.AddWithValue("?Man", subNode.Attributes(7).Value)
            cmdStudent.Parameters.AddWithValue("?Pesel", subNode.Attributes(8).Value)
            cmdStudent.Parameters.AddWithValue("?Owner", GlobalValues.AppUser.Login)
            cmdStudent.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
            cmdStudent.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
            cmdStudent.ExecuteNonQuery()
            Dim IdUczen As String = cmdStudent.LastInsertedId.ToString
            Dim P As New PrzydzialSQL
            Dim cmdPrzydzial As MySqlCommand = DBA.CreateCommand(P.InsertPrzydzial)
            cmdPrzydzial.Transaction = MySQLTrans
            cmdPrzydzial.Parameters.AddWithValue("?IdUczen", IdUczen)
            cmdPrzydzial.Parameters.AddWithValue("?Klasa", IdSzkolaKlasa)
            cmdPrzydzial.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
            cmdPrzydzial.Parameters.AddWithValue("?DataAktywacji", DataAktywacji)
            'cmdPrzydzial.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
            cmdPrzydzial.ExecuteNonQuery()

            SharedImport.SuccessValue += 1
            SharedImport.ExtendedInfoValue = String.Concat(subNode.Attributes(0).Value, " ", subNode.Attributes(1).Value, " - zaimportowano")
          Else

            SharedImport.ExtendedInfoValue = String.Concat(subNode.Attributes(0).Value, " ", subNode.Attributes(1).Value, " - uczeń istnieje w bazie danych")
            SharedImport.ErrorValue += 1

          End If
          SharedImport.pbValue += 1
          SharedImport.RecordForward()
        Next
      Next
      MySQLTrans.Commit()
      'MySQLTrans.Rollback()
      SharedImport.SuccessValue = 0
      SharedImport.ErrorValue = 0
      SharedImport.TotalValue = 0
      SharedImport.pbValue = 0

      'End If
    Catch MySqlex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(MySqlex.Message)
      Exit Sub
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
End Class
