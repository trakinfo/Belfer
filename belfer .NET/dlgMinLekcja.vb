Imports System.Windows.Forms

Public Class dlgMinLekcja
  Public IsNewMode, InRefresh As Boolean, Przedmiot As Integer, Liczba As Decimal
  Public Event NewAdded(ByVal InsertedID As String)
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If IsNewMode Then
      If chkCalyPion.Checked Then
        If AddNew() Then
          FillKlasa()
          OK_Button.Enabled = False
        End If
      Else
        If AddNew(CType(cbKlasa.SelectedItem, CbItem).ID) Then
          FillKlasa()
          OK_Button.Enabled = False
        End If
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub
  Private Overloads Function AddNew(Klasa As Integer) As Boolean
    AddNew = False
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, ML As New MinLekcjaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(ML.InsertMinLekcja)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Klasa", Klasa)
      cmd.Parameters.AddWithValue("?Przedmiot", Przedmiot)
      cmd.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
      cmd.Parameters.AddWithValue("?Wartosc", nudWartosc.Value)
      cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
      cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()
      RaiseEvent NewAdded(cmd.LastInsertedId.ToString)
      Return True
      'dtStartTime.Focus()

    Catch myex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(myex.Message & vbCr & myex.Number)
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      'Return False
    End Try
    'Return False
  End Function

  Private Overloads Function AddNew() As Boolean
    AddNew = False
    Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
    Dim DBA As New DataBaseAction, ML As New MinLekcjaSQL
    Dim LastItemID As String = ""
    Dim Pion As String = CType(cbKlasa.SelectedItem, CbItem).Kod.Substring(0, 1)
    Try
      For Each Klasa As CbItem In cbKlasa.Items
        'kwestia pionu klas
        If Klasa.Kod.Substring(0, 1) = Pion Then
          Dim cmd As MySqlCommand = DBA.CreateCommand(ML.InsertMinLekcja)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?Klasa", Klasa.ID)
          cmd.Parameters.AddWithValue("?Przedmiot", Przedmiot)
          cmd.Parameters.AddWithValue("?RokSzkolny", My.Settings.SchoolYear)
          cmd.Parameters.AddWithValue("?Wartosc", nudWartosc.Value)
          cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
          cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
          LastItemID = cmd.LastInsertedId.ToString
          cmd.ExecuteNonQuery()
        End If
      Next

      MySQLTrans.Commit()
      RaiseEvent NewAdded(LastItemID)
      Return True

    Catch myex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(myex.Message & vbCr & myex.Number)
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      'Return False
    End Try
    'Return False
  End Function
  Private Sub cbGodzina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    If InRefresh Then Exit Sub
    OK_Button.Enabled = CType(IIf(cbKlasa.SelectedItem IsNot Nothing, True, False), Boolean)
  End Sub
  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, ML As New MinLekcjaSQL
    FCB.AddComboBoxExtendedItems(cbKlasa, ML.SelectClasses(My.Settings.IdSchool, Przedmiot.ToString, My.Settings.SchoolYear))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub dlgMinLekcja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    If IsNewMode Then FillKlasa()
  End Sub

  Private Sub nudWartosc_ValueChanged(sender As Object, e As EventArgs) Handles nudWartosc.ValueChanged
    If IsNewMode Then Exit Sub
    'Liczba = nudWartosc.Value
    OK_Button.Enabled = CType(IIf(nudWartosc.Value <> Liczba, True, False), Boolean)
  End Sub
End Class
