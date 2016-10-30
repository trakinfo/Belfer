Imports System.Windows.Forms

Public Class dlgKlasa
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If Me.txtKlasa.Text.Length > 0 Then
      AddNew(txtKlasa, e)
      txtKlasa.Text = ""
      OK_Button.Enabled = False
    End If

  End Sub
  Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, K As New KlasaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(K.InsertKlasa(txtKlasa.Text.Trim))
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, txtKlasa.Text.Trim)
      txtKlasa.Focus()

    Catch myex As MySqlException
      MySQLTrans.Rollback()
      Select Case myex.Number
        Case 1062
          MessageBox.Show("Wartość wprowadzona w polu '" & CType(sender, TextBox).Name & "' już istnieje w bazie danych.")
        Case Else
          MessageBox.Show(myex.Message & vbCr & myex.Number)
      End Select
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub dlgKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'If Not Me.Modal Then AddHandler SharedKlasa.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub txtKlasa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKlasa.TextChanged
    OK_Button.Enabled = CType(IIf(txtKlasa.Text.Trim.Length < 2, False, True), Boolean)
  End Sub

  Private Sub txtKlasa_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtKlasa.Validating
    If txtKlasa.Text.Trim.Length < 2 Then Exit Sub
    If Char.IsDigit(txtKlasa.Text.Trim, 0) AndAlso Char.IsLetter(txtKlasa.Text.Trim, 1) Then
      OK_Button.Enabled = True
    Else
      OK_Button.Enabled = False
      MessageBox.Show("Nazwa oddziału może zawierać tylko dwa znaki. Pierwszy z nich musi być cyfrą a drugi literą!")
    End If

  End Sub
End Class
