Imports System.Windows.Forms

Public Class dlgNrDz

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
  Private Sub rbSetByHand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSetByHand.CheckedChanged
    Me.nudNr.Enabled = CBool(IIf(Me.rbSetByHand.Checked, True, False))
  End Sub

End Class
