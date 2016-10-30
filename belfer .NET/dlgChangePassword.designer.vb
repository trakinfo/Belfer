<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgChangePassword
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.txtPassword1 = New System.Windows.Forms.TextBox()
    Me.lblPassword2 = New System.Windows.Forms.Label()
    Me.txtPassword = New System.Windows.Forms.TextBox()
    Me.lblPassword = New System.Windows.Forms.Label()
    Me.txtLogin = New System.Windows.Forms.TextBox()
    Me.lblLogin = New System.Windows.Forms.Label()
    Me.tlpButtons.SuspendLayout()
    Me.SuspendLayout()
    '
    'tlpButtons
    '
    Me.tlpButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpButtons.ColumnCount = 2
    Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlpButtons.Controls.Add(Me.OK_Button, 0, 0)
    Me.tlpButtons.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.tlpButtons.Location = New System.Drawing.Point(277, 139)
    Me.tlpButtons.Name = "tlpButtons"
    Me.tlpButtons.RowCount = 1
    Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.tlpButtons.Size = New System.Drawing.Size(146, 29)
    Me.tlpButtons.TabIndex = 3
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Anuluj"
    '
    'txtPassword1
    '
    Me.txtPassword1.Location = New System.Drawing.Point(93, 77)
    Me.txtPassword1.Name = "txtPassword1"
    Me.txtPassword1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPassword1.Size = New System.Drawing.Size(318, 20)
    Me.txtPassword1.TabIndex = 2
    '
    'lblPassword2
    '
    Me.lblPassword2.AutoSize = True
    Me.lblPassword2.Location = New System.Drawing.Point(12, 80)
    Me.lblPassword2.Name = "lblPassword2"
    Me.lblPassword2.Size = New System.Drawing.Size(75, 13)
    Me.lblPassword2.TabIndex = 25
    Me.lblPassword2.Text = "Powtórz hasło"
    '
    'txtPassword
    '
    Me.txtPassword.Location = New System.Drawing.Point(93, 51)
    Me.txtPassword.Name = "txtPassword"
    Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPassword.Size = New System.Drawing.Size(318, 20)
    Me.txtPassword.TabIndex = 1
    '
    'lblPassword
    '
    Me.lblPassword.AutoSize = True
    Me.lblPassword.Location = New System.Drawing.Point(12, 54)
    Me.lblPassword.Name = "lblPassword"
    Me.lblPassword.Size = New System.Drawing.Size(65, 13)
    Me.lblPassword.TabIndex = 24
    Me.lblPassword.Text = "Nowe hasło"
    '
    'txtLogin
    '
    Me.txtLogin.Location = New System.Drawing.Point(93, 12)
    Me.txtLogin.Name = "txtLogin"
    Me.txtLogin.Size = New System.Drawing.Size(318, 20)
    Me.txtLogin.TabIndex = 0
    '
    'lblLogin
    '
    Me.lblLogin.AutoSize = True
    Me.lblLogin.Location = New System.Drawing.Point(12, 15)
    Me.lblLogin.Name = "lblLogin"
    Me.lblLogin.Size = New System.Drawing.Size(33, 13)
    Me.lblLogin.TabIndex = 23
    Me.lblLogin.Text = "Login"
    '
    'dlgChangePassword
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(435, 180)
    Me.Controls.Add(Me.txtPassword1)
    Me.Controls.Add(Me.lblPassword2)
    Me.Controls.Add(Me.txtPassword)
    Me.Controls.Add(Me.lblPassword)
    Me.Controls.Add(Me.txtLogin)
    Me.Controls.Add(Me.lblLogin)
    Me.Controls.Add(Me.tlpButtons)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgChangePassword"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Zmiana hasła"
    Me.tlpButtons.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents txtPassword1 As System.Windows.Forms.TextBox
  Friend WithEvents lblPassword2 As System.Windows.Forms.Label
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
  Friend WithEvents lblPassword As System.Windows.Forms.Label
  Friend WithEvents txtLogin As System.Windows.Forms.TextBox
  Friend WithEvents lblLogin As System.Windows.Forms.Label

End Class
