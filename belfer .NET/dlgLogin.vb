Imports System.Windows.Forms

Public Class dlgLogin

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
  End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
  End Sub
  Private Sub dlgLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    txtUserName.Text = My.Settings.UserName
  End Sub

  Private Sub txtUserName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserName.TextChanged, txtPassword.TextChanged
    Me.OK_Button.Enabled = CType(IIf(Me.txtUserName.Text.Length < 1 OrElse Me.txtPassword.Text.Length < 1, False, True), Boolean)
  End Sub
End Class
