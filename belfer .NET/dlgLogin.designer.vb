<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgLogin
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.txtPassword = New System.Windows.Forms.TextBox()
    Me.txtUserName = New System.Windows.Forms.TextBox()
    Me.PasswordLabel = New System.Windows.Forms.Label()
    Me.UsernameLabel = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(200, 79)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 2
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
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
    Me.Cancel_Button.Text = "&Anuluj"
    '
    'txtPassword
    '
    Me.txtPassword.Location = New System.Drawing.Point(127, 38)
    Me.txtPassword.Name = "txtPassword"
    Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPassword.Size = New System.Drawing.Size(220, 20)
    Me.txtPassword.TabIndex = 1
    '
    'txtUserName
    '
    Me.txtUserName.Location = New System.Drawing.Point(127, 12)
    Me.txtUserName.Name = "txtUserName"
    Me.txtUserName.Size = New System.Drawing.Size(220, 20)
    Me.txtUserName.TabIndex = 0
    '
    'PasswordLabel
    '
    Me.PasswordLabel.Location = New System.Drawing.Point(12, 36)
    Me.PasswordLabel.Name = "PasswordLabel"
    Me.PasswordLabel.Size = New System.Drawing.Size(111, 23)
    Me.PasswordLabel.TabIndex = 6
    Me.PasswordLabel.Text = "&Hasło"
    Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'UsernameLabel
    '
    Me.UsernameLabel.Location = New System.Drawing.Point(10, 10)
    Me.UsernameLabel.Name = "UsernameLabel"
    Me.UsernameLabel.Size = New System.Drawing.Size(111, 23)
    Me.UsernameLabel.TabIndex = 4
    Me.UsernameLabel.Text = "Nazwa &użytkownika"
    Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'dlgLogin
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(358, 120)
    Me.Controls.Add(Me.txtPassword)
    Me.Controls.Add(Me.txtUserName)
    Me.Controls.Add(Me.PasswordLabel)
    Me.Controls.Add(Me.UsernameLabel)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgLogin"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Logowanie do programu Belfer.NET"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
  Friend WithEvents txtUserName As System.Windows.Forms.TextBox
  Friend WithEvents PasswordLabel As System.Windows.Forms.Label
  Friend WithEvents UsernameLabel As System.Windows.Forms.Label

End Class
