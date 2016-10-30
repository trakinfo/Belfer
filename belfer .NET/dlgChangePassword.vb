Imports System.Windows.Forms

Public Class dlgChangePassword

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
  Public Sub cbNazwa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Dim cbNazwa As ComboBox = CType(sender, ComboBox)
    Dim dlgPassword As Form = cbNazwa.FindForm()
    dlgPassword.Controls("tlpButtons").Controls("Ok_button").Enabled = True
  End Sub
End Class
