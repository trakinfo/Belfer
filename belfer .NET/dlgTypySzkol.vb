Imports System.Windows.Forms

Public Class dlgTypySzkol
  Public Event NewSchoolTypeAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  Public IsNewMode As Boolean

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

    If IsNewMode Then
      If AddNewSchoolType(sender, e) Then
        txtOpis.Text = ""
        txtTyp.Text = ""
        txtTyp.Focus()
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.Close()
    End If
    End Sub

  Public Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Dispose(True)
  End Sub
  Private Function AddNewSchoolType(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean

    Try
      Dim DBA As New DataBaseAction, TS As New TypySzkolSQL
      Dim cmd As MySqlCommand = DBA.CreateCommand(TS.InsertSchoolType)
      cmd.Parameters.AddWithValue("?Typ", Trim(Me.txtTyp.Text))
      cmd.Parameters.AddWithValue("?Opis", Trim(Me.txtOpis.Text))
      cmd.ExecuteNonQuery()
      RaiseEvent NewSchoolTypeAdded(Me, e, Trim(Me.txtTyp.Text)) 'DBA.GetLastInsertedID)
      Return True
    Catch sqlex As MySqlException
      Select Case sqlex.Number
        Case 1062
          MessageBox.Show("Wartoœæ wprowadzona w polu '" & CType(sender, TextBox).Name & "' ju¿ istnieje w bazie danych.")
        Case Else
          MessageBox.Show(sqlex.Message & vbCr & sqlex.Number)
      End Select
      Return False
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return False
    End Try
  End Function


  Private Sub txtTyp_TextChanged(sender As Object, e As EventArgs) Handles txtTyp.TextChanged, txtOpis.TextChanged
    If txtTyp.Text.Trim.Length > 0 AndAlso txtOpis.Text.Trim.Length > 0 Then
      OK_Button.Enabled = True
    Else
      OK_Button.Enabled = False
    End If
  End Sub
End Class
