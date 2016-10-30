<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgZdarzenie
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
    Me.txtZdarzenie = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.dtpDataZajec = New System.Windows.Forms.DateTimePicker()
    Me.cbGodzina = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.chkStatus = New System.Windows.Forms.CheckBox()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(460, 133)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 8
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Zapi&sz"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "&Zamknij"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(42, 16)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(58, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Data zajęć"
    '
    'txtZdarzenie
    '
    Me.txtZdarzenie.Enabled = False
    Me.txtZdarzenie.Location = New System.Drawing.Point(106, 65)
    Me.txtZdarzenie.Multiline = True
    Me.txtZdarzenie.Name = "txtZdarzenie"
    Me.txtZdarzenie.Size = New System.Drawing.Size(500, 50)
    Me.txtZdarzenie.TabIndex = 4
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(18, 68)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(82, 13)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Treść zdarzenia"
    '
    'dtpDataZajec
    '
    Me.dtpDataZajec.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtpDataZajec.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpDataZajec.Location = New System.Drawing.Point(106, 12)
    Me.dtpDataZajec.Name = "dtpDataZajec"
    Me.dtpDataZajec.Size = New System.Drawing.Size(218, 20)
    Me.dtpDataZajec.TabIndex = 152
    '
    'cbGodzina
    '
    Me.cbGodzina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbGodzina.FormattingEnabled = True
    Me.cbGodzina.Location = New System.Drawing.Point(106, 38)
    Me.cbGodzina.Name = "cbGodzina"
    Me.cbGodzina.Size = New System.Drawing.Size(322, 21)
    Me.cbGodzina.TabIndex = 154
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(12, 41)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(88, 13)
    Me.Label5.TabIndex = 155
    Me.Label5.Text = "Godzina lekcyjna"
    '
    'chkStatus
    '
    Me.chkStatus.AutoSize = True
    Me.chkStatus.Enabled = False
    Me.chkStatus.Location = New System.Drawing.Point(514, 40)
    Me.chkStatus.Name = "chkStatus"
    Me.chkStatus.Size = New System.Drawing.Size(89, 17)
    Me.chkStatus.TabIndex = 156
    Me.chkStatus.Text = "Zrealizowane"
    Me.chkStatus.UseVisualStyleBackColor = True
    '
    'dlgZdarzenie
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(618, 174)
    Me.Controls.Add(Me.chkStatus)
    Me.Controls.Add(Me.cbGodzina)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.dtpDataZajec)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtZdarzenie)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgZdarzenie"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Nowe zdarzenie"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtZdarzenie As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents dtpDataZajec As System.Windows.Forms.DateTimePicker
  Friend WithEvents cbGodzina As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents chkStatus As System.Windows.Forms.CheckBox

End Class
