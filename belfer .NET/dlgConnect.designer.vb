<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgConnect
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
    Me.txtDBName = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtSerwerIP = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txtPassword = New System.Windows.Forms.TextBox()
    Me.txtUserName = New System.Windows.Forms.TextBox()
    Me.PasswordLabel = New System.Windows.Forms.Label()
    Me.UsernameLabel = New System.Windows.Forms.Label()
    Me.chkSSLMode = New System.Windows.Forms.CheckBox()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(243, 160)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
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
    'txtDBName
    '
    Me.txtDBName.Location = New System.Drawing.Point(168, 38)
    Me.txtDBName.Name = "txtDBName"
    Me.txtDBName.Size = New System.Drawing.Size(220, 20)
    Me.txtDBName.TabIndex = 12
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(6, 36)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(156, 23)
    Me.Label1.TabIndex = 16
    Me.Label1.Text = "Baza danych"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtSerwerIP
    '
    Me.txtSerwerIP.Location = New System.Drawing.Point(168, 12)
    Me.txtSerwerIP.Name = "txtSerwerIP"
    Me.txtSerwerIP.Size = New System.Drawing.Size(220, 20)
    Me.txtSerwerIP.TabIndex = 10
    '
    'Label2
    '
    Me.Label2.Location = New System.Drawing.Point(6, 10)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(156, 23)
    Me.Label2.TabIndex = 15
    Me.Label2.Text = "Serwer IP"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtPassword
    '
    Me.txtPassword.Location = New System.Drawing.Point(168, 90)
    Me.txtPassword.Name = "txtPassword"
    Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPassword.Size = New System.Drawing.Size(220, 20)
    Me.txtPassword.TabIndex = 14
    '
    'txtUserName
    '
    Me.txtUserName.Location = New System.Drawing.Point(168, 64)
    Me.txtUserName.Name = "txtUserName"
    Me.txtUserName.Size = New System.Drawing.Size(220, 20)
    Me.txtUserName.TabIndex = 13
    '
    'PasswordLabel
    '
    Me.PasswordLabel.Location = New System.Drawing.Point(6, 88)
    Me.PasswordLabel.Name = "PasswordLabel"
    Me.PasswordLabel.Size = New System.Drawing.Size(136, 23)
    Me.PasswordLabel.TabIndex = 11
    Me.PasswordLabel.Text = "Hasło do bazy danych"
    Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'UsernameLabel
    '
    Me.UsernameLabel.Location = New System.Drawing.Point(6, 62)
    Me.UsernameLabel.Name = "UsernameLabel"
    Me.UsernameLabel.Size = New System.Drawing.Size(156, 23)
    Me.UsernameLabel.TabIndex = 9
    Me.UsernameLabel.Text = "Użytkownik systemowy"
    Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'chkSSLMode
    '
    Me.chkSSLMode.AutoSize = True
    Me.chkSSLMode.Location = New System.Drawing.Point(221, 116)
    Me.chkSSLMode.Name = "chkSSLMode"
    Me.chkSSLMode.Size = New System.Drawing.Size(165, 17)
    Me.chkSSLMode.TabIndex = 17
    Me.chkSSLMode.Text = "Połączenie szyfrowane (SSL)"
    Me.chkSSLMode.UseVisualStyleBackColor = True
    '
    'dlgConnect
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(401, 201)
    Me.Controls.Add(Me.chkSSLMode)
    Me.Controls.Add(Me.txtDBName)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.txtSerwerIP)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtPassword)
    Me.Controls.Add(Me.txtUserName)
    Me.Controls.Add(Me.PasswordLabel)
    Me.Controls.Add(Me.UsernameLabel)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgConnect"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Parametry połączenia z bazą danych"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents txtDBName As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtSerwerIP As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
  Friend WithEvents txtUserName As System.Windows.Forms.TextBox
  Friend WithEvents PasswordLabel As System.Windows.Forms.Label
  Friend WithEvents UsernameLabel As System.Windows.Forms.Label
  Friend WithEvents chkSSLMode As System.Windows.Forms.CheckBox

End Class
