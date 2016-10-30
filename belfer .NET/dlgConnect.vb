Imports System.Windows.Forms

Public Class dlgConnect
  Dim IsDirty(3) As Boolean

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

  Private Sub txtSerwerIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerwerIP.TextChanged, txtDBName.TextChanged, txtPassword.TextChanged, txtUserName.TextChanged
    Select Case CType(sender, TextBox).Name
      Case txtSerwerIP.Name
        If txtSerwerIP.Text <> My.Settings.ServerIP Then
          IsDirty(0) = True
        Else
          IsDirty(0) = False
        End If
      Case txtDBName.Name
        If txtDBName.Text <> My.Settings.DBName Then
          IsDirty(1) = True
        Else
          IsDirty(1) = False
        End If
      Case txtPassword.Name
        Dim R As New Rijndael
        If txtPassword.Text <> R.Decrypt(My.Settings.SysPassword) Then
          IsDirty(2) = True
        Else
          IsDirty(2) = False
        End If
      Case txtUserName.Name
        If txtUserName.Text <> My.Settings.SysUser Then
          IsDirty(3) = True
        Else
          IsDirty(3) = False
        End If

    End Select
    OK_Button.Enabled = CType(IIf(IsDirty(0) OrElse IsDirty(1) OrElse IsDirty(2) OrElse IsDirty(3), True, False), Boolean)
  End Sub

  Private Sub chkSSLMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkSSLMode.CheckedChanged
    'Dim R As New Rijndael
    'If Not chkSSLMode.Checked Then
    '  txtSerwerIP.Text = My.Settings.ServerIP
    '  txtUserName.Text = My.Settings.SysUser
    '  txtPassword.Text = R.Decrypt(My.Settings.SysPassword)
    'Else
    '  txtSerwerIP.Text = My.Settings.ServerSSLIP
    '  txtUserName.Text = My.Settings.SysSSLUser
    '  txtPassword.Text = R.Decrypt(My.Settings.SysSSLPassword)
    'End If
    'txtDBName.Text = My.Settings.DBName
    OK_Button.Enabled = CType(IIf(chkSSLMode.CheckState <> My.Settings.SSLMode, True, False), Boolean)

  End Sub

End Class
