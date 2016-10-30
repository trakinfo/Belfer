Imports System.Windows.Forms
Imports System.Text.RegularExpressions

Public Class dlgUser
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If System.String.Compare(Me.txtPassword.Text, Me.txtPassword1.Text, False) <> 0 Then
      MessageBox.Show("Wartości pól 'Hasło' i 'Powtórz hasło' muszą być takie same!")
      Me.txtPassword1.Focus()
    Else
      Me.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub txtLogin_Validated(sender As Object, e As EventArgs) Handles txtLogin.Validated
    If cbRola.Text.Length > 0 Then OK_Button.Enabled = True
  End Sub

  Private Sub txtNazwa_TextChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtLogin.Validating
    'If Me.txtLogin.Text.Length < 1 Then
    '  Me.OK_Button.Enabled = False
    'Else
    If CheckLoginExist(txtLogin.Text) Then
      'OK_Button.Enabled = False
      MessageBox.Show("Wprowadzony login jest zajęty. Spróbuj wprowadzić inny.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
      e.Cancel = True
      'Else
      '  Me.OK_Button.Enabled = True
    End If
    'End If

  End Sub
  Private Function CheckLoginExist(Login As String) As Boolean
    Dim U As New UsersSQL, DBA As New DataBaseAction
    If DBA.ComputeRecords(U.CountUser(Login)) > 0 Then Return True
    Return False
  End Function
  Private Function CheckEmail(Email As String) As Boolean
    Dim Pattern As String = "[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"
    Dim emailAddressMatch As Match = Regex.Match(Email, Pattern)
    If emailAddressMatch.Success Then
      Return True
    Else
      MessageBox.Show("Nieprawidłowy adres e-mail. Popraw adres lub pozostaw pole puste.", My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
    End If
    Return False
  End Function

  Private Sub cbRola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRola.SelectedIndexChanged

    If CType(cbRola.SelectedItem, CbItem).ID > 0 Then
      cbNauczyciel.Enabled = True
    Else
      cbNauczyciel.Enabled = False
      cbNauczyciel.SelectedItem = Nothing
    End If
    If txtLogin.Text.Length > 0 Then OK_Button.Enabled = True
  End Sub


  Private Sub txtEmail_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtEmail.Validating
    If txtEmail.Text.Length = 0 Then Exit Sub
    If CheckEmail(txtEmail.Text) = False Then e.Cancel = True
  End Sub

  Private Sub txtNazwisko_Validated(sender As Object, e As EventArgs) Handles txtNazwisko.Validated, txtImie.Validated
    Dim SH As New StringHelper
    CType(sender, TextBox).Text = SH.CapitalizeFirst(CType(sender, TextBox).Text)
  End Sub
End Class
