Imports System.Windows.Forms

Public Class dlgUzasadnienie

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

  Private Sub txtUzasadnienie_TextChanged(sender As Object, e As EventArgs) Handles txtUzasadnienie.TextChanged
    OK_Button.Enabled = If(txtUzasadnienie.Text.Length > 0, True, False)
  End Sub

End Class
