<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSkalaOcen
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
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtNazwa = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.txtNazwaPelna = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.txtNazwaSkrot = New System.Windows.Forms.TextBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.nudWaga = New System.Windows.Forms.NumericUpDown()
    Me.cbTyp = New System.Windows.Forms.ComboBox()
    Me.cbPodtyp = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudWaga, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(548, 84)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 6
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 6
    Me.OK_Button.Text = "&Dodaj"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 7
    Me.Cancel_Button.Text = "&Zamknij"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(100, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Nazwa symboliczna"
    '
    'txtNazwa
    '
    Me.txtNazwa.Location = New System.Drawing.Point(118, 12)
    Me.txtNazwa.Name = "txtNazwa"
    Me.txtNazwa.Size = New System.Drawing.Size(101, 20)
    Me.txtNazwa.TabIndex = 0
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(565, 41)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(68, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Waga oceny"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(12, 41)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(57, 13)
    Me.Label3.TabIndex = 5
    Me.Label3.Text = "Typ oceny"
    '
    'txtNazwaPelna
    '
    Me.txtNazwaPelna.Location = New System.Drawing.Point(302, 12)
    Me.txtNazwaPelna.Name = "txtNazwaPelna"
    Me.txtNazwaPelna.Size = New System.Drawing.Size(202, 20)
    Me.txtNazwaPelna.TabIndex = 1
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(225, 15)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(71, 13)
    Me.Label6.TabIndex = 29
    Me.Label6.Text = "Nazwa pełna"
    '
    'txtNazwaSkrot
    '
    Me.txtNazwaSkrot.Location = New System.Drawing.Point(603, 12)
    Me.txtNazwaSkrot.Name = "txtNazwaSkrot"
    Me.txtNazwaSkrot.Size = New System.Drawing.Size(93, 20)
    Me.txtNazwaSkrot.TabIndex = 2
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(510, 15)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(87, 13)
    Me.Label7.TabIndex = 31
    Me.Label7.Text = "Nazwa skrócona"
    '
    'nudWaga
    '
    Me.nudWaga.DecimalPlaces = 2
    Me.nudWaga.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
    Me.nudWaga.Location = New System.Drawing.Point(639, 39)
    Me.nudWaga.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
    Me.nudWaga.Name = "nudWaga"
    Me.nudWaga.Size = New System.Drawing.Size(57, 20)
    Me.nudWaga.TabIndex = 5
    '
    'cbTyp
    '
    Me.cbTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbTyp.DropDownWidth = 200
    Me.cbTyp.FormattingEnabled = True
    Me.cbTyp.Location = New System.Drawing.Point(75, 38)
    Me.cbTyp.Name = "cbTyp"
    Me.cbTyp.Size = New System.Drawing.Size(200, 21)
    Me.cbTyp.TabIndex = 3
    '
    'cbPodtyp
    '
    Me.cbPodtyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPodtyp.DropDownWidth = 217
    Me.cbPodtyp.FormattingEnabled = True
    Me.cbPodtyp.Location = New System.Drawing.Point(359, 38)
    Me.cbPodtyp.Name = "cbPodtyp"
    Me.cbPodtyp.Size = New System.Drawing.Size(200, 21)
    Me.cbPodtyp.TabIndex = 4
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(281, 41)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(72, 13)
    Me.Label4.TabIndex = 35
    Me.Label4.Text = "Podtyp oceny"
    '
    'dlgSkalaOcen
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(706, 125)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.cbPodtyp)
    Me.Controls.Add(Me.cbTyp)
    Me.Controls.Add(Me.nudWaga)
    Me.Controls.Add(Me.txtNazwaSkrot)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.txtNazwaPelna)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtNazwa)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgSkalaOcen"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Dodawanie nowej oceny"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudWaga, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtNazwaPelna As System.Windows.Forms.TextBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents txtNazwaSkrot As System.Windows.Forms.TextBox
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents nudWaga As System.Windows.Forms.NumericUpDown
  Friend WithEvents cbTyp As System.Windows.Forms.ComboBox
  Friend WithEvents cbPodtyp As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
