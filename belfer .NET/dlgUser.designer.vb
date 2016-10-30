<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgUser
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
    Me.txtImie = New System.Windows.Forms.TextBox()
    Me.txtNazwisko = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtLogin = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.txtPassword1 = New System.Windows.Forms.TextBox()
    Me.lblPassword2 = New System.Windows.Forms.Label()
    Me.txtPassword = New System.Windows.Forms.TextBox()
    Me.lblPassword = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.cbRola = New System.Windows.Forms.ComboBox()
    Me.chkStatus = New System.Windows.Forms.CheckBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cbNauczyciel = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.txtEmail = New System.Windows.Forms.TextBox()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(267, 229)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 9
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 9
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 10
    Me.Cancel_Button.Text = "Anuluj"
    '
    'txtImie
    '
    Me.txtImie.Location = New System.Drawing.Point(93, 64)
    Me.txtImie.Name = "txtImie"
    Me.txtImie.Size = New System.Drawing.Size(318, 20)
    Me.txtImie.TabIndex = 2
    '
    'txtNazwisko
    '
    Me.txtNazwisko.Location = New System.Drawing.Point(93, 38)
    Me.txtNazwisko.Name = "txtNazwisko"
    Me.txtNazwisko.Size = New System.Drawing.Size(318, 20)
    Me.txtNazwisko.TabIndex = 1
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 67)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(26, 13)
    Me.Label2.TabIndex = 10
    Me.Label2.Text = "Imię"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 41)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(53, 13)
    Me.Label1.TabIndex = 9
    Me.Label1.Text = "Nazwisko"
    '
    'txtLogin
    '
    Me.txtLogin.Location = New System.Drawing.Point(93, 12)
    Me.txtLogin.MaxLength = 10
    Me.txtLogin.Name = "txtLogin"
    Me.txtLogin.Size = New System.Drawing.Size(318, 20)
    Me.txtLogin.TabIndex = 0
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 15)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(33, 13)
    Me.Label4.TabIndex = 13
    Me.Label4.Text = "Login"
    '
    'txtPassword1
    '
    Me.txtPassword1.Location = New System.Drawing.Point(93, 142)
    Me.txtPassword1.Name = "txtPassword1"
    Me.txtPassword1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPassword1.Size = New System.Drawing.Size(318, 20)
    Me.txtPassword1.TabIndex = 5
    '
    'lblPassword2
    '
    Me.lblPassword2.AutoSize = True
    Me.lblPassword2.Location = New System.Drawing.Point(12, 145)
    Me.lblPassword2.Name = "lblPassword2"
    Me.lblPassword2.Size = New System.Drawing.Size(75, 13)
    Me.lblPassword2.TabIndex = 19
    Me.lblPassword2.Text = "Powtórz hasło"
    '
    'txtPassword
    '
    Me.txtPassword.Location = New System.Drawing.Point(93, 116)
    Me.txtPassword.Name = "txtPassword"
    Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.txtPassword.Size = New System.Drawing.Size(318, 20)
    Me.txtPassword.TabIndex = 4
    '
    'lblPassword
    '
    Me.lblPassword.AutoSize = True
    Me.lblPassword.Location = New System.Drawing.Point(12, 119)
    Me.lblPassword.Name = "lblPassword"
    Me.lblPassword.Size = New System.Drawing.Size(36, 13)
    Me.lblPassword.TabIndex = 17
    Me.lblPassword.Text = "Hasło"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(12, 171)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(29, 13)
    Me.Label7.TabIndex = 58
    Me.Label7.Text = "Rola"
    '
    'cbRola
    '
    Me.cbRola.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbRola.FormattingEnabled = True
    Me.cbRola.Location = New System.Drawing.Point(93, 168)
    Me.cbRola.Name = "cbRola"
    Me.cbRola.Size = New System.Drawing.Size(205, 21)
    Me.cbRola.Sorted = True
    Me.cbRola.TabIndex = 6
    '
    'chkStatus
    '
    Me.chkStatus.AutoSize = True
    Me.chkStatus.Checked = True
    Me.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkStatus.Location = New System.Drawing.Point(343, 172)
    Me.chkStatus.Name = "chkStatus"
    Me.chkStatus.Size = New System.Drawing.Size(67, 17)
    Me.chkStatus.TabIndex = 7
    Me.chkStatus.Text = "Aktywne"
    Me.chkStatus.UseVisualStyleBackColor = True
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(12, 198)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(59, 13)
    Me.Label3.TabIndex = 61
    Me.Label3.Text = "Nauczyciel"
    '
    'cbNauczyciel
    '
    Me.cbNauczyciel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbNauczyciel.Enabled = False
    Me.cbNauczyciel.FormattingEnabled = True
    Me.cbNauczyciel.Location = New System.Drawing.Point(93, 195)
    Me.cbNauczyciel.Name = "cbNauczyciel"
    Me.cbNauczyciel.Size = New System.Drawing.Size(317, 21)
    Me.cbNauczyciel.Sorted = True
    Me.cbNauczyciel.TabIndex = 8
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(12, 93)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(35, 13)
    Me.Label5.TabIndex = 62
    Me.Label5.Text = "E-mail"
    '
    'txtEmail
    '
    Me.txtEmail.Location = New System.Drawing.Point(93, 90)
    Me.txtEmail.Name = "txtEmail"
    Me.txtEmail.Size = New System.Drawing.Size(317, 20)
    Me.txtEmail.TabIndex = 3
    '
    'dlgUser
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(425, 270)
    Me.Controls.Add(Me.txtEmail)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.cbNauczyciel)
    Me.Controls.Add(Me.chkStatus)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.cbRola)
    Me.Controls.Add(Me.txtPassword1)
    Me.Controls.Add(Me.lblPassword2)
    Me.Controls.Add(Me.txtPassword)
    Me.Controls.Add(Me.lblPassword)
    Me.Controls.Add(Me.txtLogin)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.txtImie)
    Me.Controls.Add(Me.txtNazwisko)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgUser"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Dodawanie nowego użytkownika"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents txtImie As System.Windows.Forms.TextBox
  Friend WithEvents txtNazwisko As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtLogin As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents txtPassword1 As System.Windows.Forms.TextBox
  Friend WithEvents lblPassword2 As System.Windows.Forms.Label
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
  Friend WithEvents lblPassword As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents cbRola As System.Windows.Forms.ComboBox
  Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cbNauczyciel As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents txtEmail As System.Windows.Forms.TextBox

End Class
