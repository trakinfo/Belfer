<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgWstawKolumna
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
    Me.lblPrzedmiot = New System.Windows.Forms.Label()
    Me.lblKlasa = New System.Windows.Forms.Label()
    Me.lblSemestr = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.nudLiczbaKolumn = New System.Windows.Forms.NumericUpDown()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.txtKolumnaOdniesienia = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.txtDostepnaLiczbaKolumn = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.cbPozycja = New System.Windows.Forms.ComboBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.chkPoprawa = New System.Windows.Forms.CheckBox()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudLiczbaKolumn, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(390, 136)
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
    Me.OK_Button.Text = "Wstaw"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Zamknij"
    '
    'lblPrzedmiot
    '
    Me.lblPrzedmiot.Enabled = False
    Me.lblPrzedmiot.ForeColor = System.Drawing.Color.Blue
    Me.lblPrzedmiot.Location = New System.Drawing.Point(138, 6)
    Me.lblPrzedmiot.Name = "lblPrzedmiot"
    Me.lblPrzedmiot.Size = New System.Drawing.Size(256, 13)
    Me.lblPrzedmiot.TabIndex = 218
    Me.lblPrzedmiot.Text = "Przedmiot"
    Me.lblPrzedmiot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblKlasa
    '
    Me.lblKlasa.Enabled = False
    Me.lblKlasa.ForeColor = System.Drawing.Color.Blue
    Me.lblKlasa.Location = New System.Drawing.Point(7, 6)
    Me.lblKlasa.Name = "lblKlasa"
    Me.lblKlasa.Size = New System.Drawing.Size(125, 13)
    Me.lblKlasa.TabIndex = 216
    Me.lblKlasa.Text = "Klasa"
    '
    'lblSemestr
    '
    Me.lblSemestr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblSemestr.Enabled = False
    Me.lblSemestr.ForeColor = System.Drawing.Color.Blue
    Me.lblSemestr.Location = New System.Drawing.Point(450, 6)
    Me.lblSemestr.Name = "lblSemestr"
    Me.lblSemestr.Size = New System.Drawing.Size(79, 13)
    Me.lblSemestr.TabIndex = 223
    Me.lblSemestr.Text = "Sem"
    Me.lblSemestr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(192, 80)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(145, 13)
    Me.Label6.TabIndex = 224
    Me.Label6.Text = "Liczba kolumn do wstawienia"
    '
    'nudLiczbaKolumn
    '
    Me.nudLiczbaKolumn.Location = New System.Drawing.Point(343, 78)
    Me.nudLiczbaKolumn.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudLiczbaKolumn.Name = "nudLiczbaKolumn"
    Me.nudLiczbaKolumn.Size = New System.Drawing.Size(62, 20)
    Me.nudLiczbaKolumn.TabIndex = 225
    Me.nudLiczbaKolumn.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(8, 54)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(104, 13)
    Me.Label8.TabIndex = 226
    Me.Label8.Text = "Kolumna odniesienia"
    '
    'txtKolumnaOdniesienia
    '
    Me.txtKolumnaOdniesienia.Enabled = False
    Me.txtKolumnaOdniesienia.Location = New System.Drawing.Point(134, 51)
    Me.txtKolumnaOdniesienia.Name = "txtKolumnaOdniesienia"
    Me.txtKolumnaOdniesienia.Size = New System.Drawing.Size(52, 20)
    Me.txtKolumnaOdniesienia.TabIndex = 227
    Me.txtKolumnaOdniesienia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(8, 80)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(120, 13)
    Me.Label9.TabIndex = 228
    Me.Label9.Text = "Dostępna liczba kolumn"
    '
    'txtDostepnaLiczbaKolumn
    '
    Me.txtDostepnaLiczbaKolumn.Enabled = False
    Me.txtDostepnaLiczbaKolumn.Location = New System.Drawing.Point(134, 77)
    Me.txtDostepnaLiczbaKolumn.Name = "txtDostepnaLiczbaKolumn"
    Me.txtDostepnaLiczbaKolumn.Size = New System.Drawing.Size(52, 20)
    Me.txtDostepnaLiczbaKolumn.TabIndex = 229
    Me.txtDostepnaLiczbaKolumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(192, 54)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(116, 13)
    Me.Label1.TabIndex = 230
    Me.Label1.Text = "pozycja nowej kolumny"
    '
    'cbPozycja
    '
    Me.cbPozycja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPozycja.FormattingEnabled = True
    Me.cbPozycja.Location = New System.Drawing.Point(343, 51)
    Me.cbPozycja.Name = "cbPozycja"
    Me.cbPozycja.Size = New System.Drawing.Size(193, 21)
    Me.cbPozycja.TabIndex = 231
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.lblKlasa)
    Me.Panel1.Controls.Add(Me.lblPrzedmiot)
    Me.Panel1.Controls.Add(Me.lblSemestr)
    Me.Panel1.Location = New System.Drawing.Point(1, 3)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(535, 32)
    Me.Panel1.TabIndex = 232
    '
    'chkPoprawa
    '
    Me.chkPoprawa.AutoSize = True
    Me.chkPoprawa.Location = New System.Drawing.Point(411, 79)
    Me.chkPoprawa.Name = "chkPoprawa"
    Me.chkPoprawa.Size = New System.Drawing.Size(131, 17)
    Me.chkPoprawa.TabIndex = 233
    Me.chkPoprawa.Text = "Kolumna poprawkowa"
    Me.chkPoprawa.UseVisualStyleBackColor = True
    '
    'dlgWstawKolumna
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(548, 177)
    Me.Controls.Add(Me.chkPoprawa)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cbPozycja)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.txtDostepnaLiczbaKolumn)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.txtKolumnaOdniesienia)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.nudLiczbaKolumn)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgWstawKolumna"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Wstawianie kolumn wynikowych"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudLiczbaKolumn, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents lblPrzedmiot As System.Windows.Forms.Label
  Friend WithEvents lblKlasa As System.Windows.Forms.Label
  Friend WithEvents lblSemestr As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents nudLiczbaKolumn As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents txtKolumnaOdniesienia As System.Windows.Forms.TextBox
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents txtDostepnaLiczbaKolumn As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents cbPozycja As System.Windows.Forms.ComboBox
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents chkPoprawa As System.Windows.Forms.CheckBox

End Class
