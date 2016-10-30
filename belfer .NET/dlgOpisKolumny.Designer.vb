<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgOpisKolumny
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
    Me.lvOpisWyniku = New System.Windows.Forms.ListView()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.nudWaga = New System.Windows.Forms.NumericUpDown()
    Me.lblKlasa = New System.Windows.Forms.Label()
    Me.lblPrzedmiot = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.txtNrKolumny = New System.Windows.Forms.TextBox()
    Me.cmdReset = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.chkPoprawa = New System.Windows.Forms.CheckBox()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudWaga, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(438, 490)
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
    Me.OK_Button.Text = "Dodaj"
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
    'lvOpisWyniku
    '
    Me.lvOpisWyniku.Location = New System.Drawing.Point(11, 71)
    Me.lvOpisWyniku.Name = "lvOpisWyniku"
    Me.lvOpisWyniku.Size = New System.Drawing.Size(570, 385)
    Me.lvOpisWyniku.TabIndex = 197
    Me.lvOpisWyniku.UseCompatibleStateImageBehavior = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(8, 47)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(60, 13)
    Me.Label1.TabIndex = 198
    Me.Label1.Text = "Nr kolumny"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(120, 47)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(78, 13)
    Me.Label2.TabIndex = 199
    Me.Label2.Text = "Waga kolumny"
    '
    'nudWaga
    '
    Me.nudWaga.DecimalPlaces = 1
    Me.nudWaga.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
    Me.nudWaga.Location = New System.Drawing.Point(204, 45)
    Me.nudWaga.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudWaga.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
    Me.nudWaga.Name = "nudWaga"
    Me.nudWaga.Size = New System.Drawing.Size(51, 20)
    Me.nudWaga.TabIndex = 201
    Me.nudWaga.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'lblKlasa
    '
    Me.lblKlasa.Enabled = False
    Me.lblKlasa.Location = New System.Drawing.Point(8, 5)
    Me.lblKlasa.Name = "lblKlasa"
    Me.lblKlasa.Size = New System.Drawing.Size(200, 13)
    Me.lblKlasa.TabIndex = 205
    Me.lblKlasa.Text = "Klasa"
    '
    'lblPrzedmiot
    '
    Me.lblPrzedmiot.Enabled = False
    Me.lblPrzedmiot.Location = New System.Drawing.Point(214, 5)
    Me.lblPrzedmiot.Name = "lblPrzedmiot"
    Me.lblPrzedmiot.Size = New System.Drawing.Size(367, 13)
    Me.lblPrzedmiot.TabIndex = 208
    Me.lblPrzedmiot.Text = "Przedmiot"
    Me.lblPrzedmiot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.txtSeek)
    Me.Panel1.Controls.Add(Me.Label9)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(11, 458)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(573, 26)
    Me.Panel1.TabIndex = 213
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(239, 6)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(31, 13)
    Me.Label6.TabIndex = 212
    Me.Label6.Text = "Filtruj"
    '
    'txtSeek
    '
    Me.txtSeek.Location = New System.Drawing.Point(276, 3)
    Me.txtSeek.Name = "txtSeek"
    Me.txtSeek.Size = New System.Drawing.Size(294, 20)
    Me.txtSeek.TabIndex = 211
    '
    'Label9
    '
    Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(3, 6)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(45, 13)
    Me.Label9.TabIndex = 145
    Me.Label9.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 6)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 146
    Me.lblRecord.Text = "lblRecord"
    '
    'txtNrKolumny
    '
    Me.txtNrKolumny.Enabled = False
    Me.txtNrKolumny.Location = New System.Drawing.Point(74, 44)
    Me.txtNrKolumny.Name = "txtNrKolumny"
    Me.txtNrKolumny.Size = New System.Drawing.Size(40, 20)
    Me.txtNrKolumny.TabIndex = 213
    Me.txtNrKolumny.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'cmdReset
    '
    Me.cmdReset.Enabled = False
    Me.cmdReset.Location = New System.Drawing.Point(460, 42)
    Me.cmdReset.Name = "cmdReset"
    Me.cmdReset.Size = New System.Drawing.Size(121, 23)
    Me.cmdReset.TabIndex = 214
    Me.cmdReset.Text = "Resetuj opis kolumny"
    Me.cmdReset.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Location = New System.Drawing.Point(12, 493)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(102, 23)
    Me.cmdAddNew.TabIndex = 215
    Me.cmdAddNew.Text = "Nowy opis wyniku"
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.lblKlasa)
    Me.Panel2.Controls.Add(Me.lblPrzedmiot)
    Me.Panel2.Location = New System.Drawing.Point(2, 3)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(593, 32)
    Me.Panel2.TabIndex = 216
    '
    'cmdEdit
    '
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Location = New System.Drawing.Point(120, 493)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(102, 23)
    Me.cmdEdit.TabIndex = 217
    Me.cmdEdit.Text = "Edytuj opis wyniku"
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'chkPoprawa
    '
    Me.chkPoprawa.AutoSize = True
    Me.chkPoprawa.Location = New System.Drawing.Point(261, 46)
    Me.chkPoprawa.Name = "chkPoprawa"
    Me.chkPoprawa.Size = New System.Drawing.Size(131, 17)
    Me.chkPoprawa.TabIndex = 218
    Me.chkPoprawa.Text = "Kolumna poprawkowa"
    Me.chkPoprawa.UseVisualStyleBackColor = True
    '
    'dlgOpisKolumny
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(596, 531)
    Me.Controls.Add(Me.chkPoprawa)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.cmdReset)
    Me.Controls.Add(Me.txtNrKolumny)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.nudWaga)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lvOpisWyniku)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgOpisKolumny"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "dlgOpisKolumny"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudWaga, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents lvOpisWyniku As System.Windows.Forms.ListView
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents nudWaga As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblKlasa As System.Windows.Forms.Label
  Friend WithEvents lblPrzedmiot As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents txtNrKolumny As System.Windows.Forms.TextBox
  Friend WithEvents cmdReset As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents chkPoprawa As System.Windows.Forms.CheckBox

End Class
