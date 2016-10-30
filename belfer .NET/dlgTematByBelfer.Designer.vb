<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTematByBelfer
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTematByBelfer))
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.chkZastepstwo = New System.Windows.Forms.CheckBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txtTemat = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.lvPresent = New System.Windows.Forms.ListView()
    Me.lvAbsent = New System.Windows.Forms.ListView()
    Me.lvLate = New System.Windows.Forms.ListView()
    Me.nudNr = New System.Windows.Forms.NumericUpDown()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.cmdMoveOff = New System.Windows.Forms.Button()
    Me.cmdMoveDown = New System.Windows.Forms.Button()
    Me.cmdMoveBack = New System.Windows.Forms.Button()
    Me.cmdMove = New System.Windows.Forms.Button()
    Me.lblZastepstwo = New System.Windows.Forms.Label()
    Me.dtpDataZajec = New System.Windows.Forms.DateTimePicker()
    Me.chkStatus = New System.Windows.Forms.CheckBox()
    Me.cbGodzina = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudNr, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(460, 476)
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
    Me.Label1.Location = New System.Drawing.Point(42, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(58, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Data zajęć"
    '
    'chkZastepstwo
    '
    Me.chkZastepstwo.AutoSize = True
    Me.chkZastepstwo.Enabled = False
    Me.chkZastepstwo.Location = New System.Drawing.Point(473, 68)
    Me.chkZastepstwo.Name = "chkZastepstwo"
    Me.chkZastepstwo.Size = New System.Drawing.Size(133, 17)
    Me.chkZastepstwo.TabIndex = 2
    Me.chkZastepstwo.Text = "Potwierdzam realizację"
    Me.chkZastepstwo.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(38, 69)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(62, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Zastępstwo"
    '
    'txtTemat
    '
    Me.txtTemat.Enabled = False
    Me.txtTemat.Location = New System.Drawing.Point(106, 88)
    Me.txtTemat.Multiline = True
    Me.txtTemat.Name = "txtTemat"
    Me.txtTemat.Size = New System.Drawing.Size(500, 50)
    Me.txtTemat.TabIndex = 4
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(35, 91)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(65, 13)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Temat zajęć"
    '
    'lvPresent
    '
    Me.lvPresent.Enabled = False
    Me.lvPresent.Location = New System.Drawing.Point(15, 144)
    Me.lvPresent.Name = "lvPresent"
    Me.lvPresent.Size = New System.Drawing.Size(269, 322)
    Me.lvPresent.TabIndex = 5
    Me.lvPresent.UseCompatibleStateImageBehavior = False
    '
    'lvAbsent
    '
    Me.lvAbsent.Enabled = False
    Me.lvAbsent.Location = New System.Drawing.Point(336, 144)
    Me.lvAbsent.Name = "lvAbsent"
    Me.lvAbsent.Size = New System.Drawing.Size(270, 149)
    Me.lvAbsent.TabIndex = 6
    Me.lvAbsent.UseCompatibleStateImageBehavior = False
    '
    'lvLate
    '
    Me.lvLate.Enabled = False
    Me.lvLate.Location = New System.Drawing.Point(336, 338)
    Me.lvLate.Name = "lvLate"
    Me.lvLate.Size = New System.Drawing.Size(270, 128)
    Me.lvLate.TabIndex = 7
    Me.lvLate.UseCompatibleStateImageBehavior = False
    '
    'nudNr
    '
    Me.nudNr.Enabled = False
    Me.nudNr.Location = New System.Drawing.Point(55, 118)
    Me.nudNr.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
    Me.nudNr.Name = "nudNr"
    Me.nudNr.Size = New System.Drawing.Size(45, 20)
    Me.nudNr.TabIndex = 3
    Me.nudNr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 120)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(37, 13)
    Me.Label4.TabIndex = 150
    Me.Label4.Text = "Nr zaj."
    '
    'cmdMoveOff
    '
    Me.cmdMoveOff.Enabled = False
    Me.cmdMoveOff.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdMoveOff.Location = New System.Drawing.Point(290, 387)
    Me.cmdMoveOff.Name = "cmdMoveOff"
    Me.cmdMoveOff.Size = New System.Drawing.Size(40, 33)
    Me.cmdMoveOff.TabIndex = 148
    Me.cmdMoveOff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdMoveOff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdMoveOff.UseVisualStyleBackColor = True
    '
    'cmdMoveDown
    '
    Me.cmdMoveDown.Enabled = False
    Me.cmdMoveDown.Image = CType(resources.GetObject("cmdMoveDown.Image"), System.Drawing.Image)
    Me.cmdMoveDown.Location = New System.Drawing.Point(449, 299)
    Me.cmdMoveDown.Name = "cmdMoveDown"
    Me.cmdMoveDown.Size = New System.Drawing.Size(45, 33)
    Me.cmdMoveDown.TabIndex = 147
    Me.cmdMoveDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdMoveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdMoveDown.UseVisualStyleBackColor = True
    '
    'cmdMoveBack
    '
    Me.cmdMoveBack.Enabled = False
    Me.cmdMoveBack.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.cmdMoveBack.Image = Global.belfer.NET.My.Resources.Resources.arrow_back_24
    Me.cmdMoveBack.Location = New System.Drawing.Point(290, 221)
    Me.cmdMoveBack.Name = "cmdMoveBack"
    Me.cmdMoveBack.Size = New System.Drawing.Size(40, 33)
    Me.cmdMoveBack.TabIndex = 146
    Me.cmdMoveBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdMoveBack.UseVisualStyleBackColor = True
    '
    'cmdMove
    '
    Me.cmdMove.Enabled = False
    Me.cmdMove.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.cmdMove.Image = Global.belfer.NET.My.Resources.Resources.arrow_forward_24
    Me.cmdMove.Location = New System.Drawing.Point(290, 182)
    Me.cmdMove.Name = "cmdMove"
    Me.cmdMove.Size = New System.Drawing.Size(40, 33)
    Me.cmdMove.TabIndex = 145
    Me.cmdMove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdMove.UseVisualStyleBackColor = True
    '
    'lblZastepstwo
    '
    Me.lblZastepstwo.BackColor = System.Drawing.SystemColors.Control
    Me.lblZastepstwo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblZastepstwo.Enabled = False
    Me.lblZastepstwo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblZastepstwo.ForeColor = System.Drawing.Color.SaddleBrown
    Me.lblZastepstwo.Location = New System.Drawing.Point(106, 65)
    Me.lblZastepstwo.Name = "lblZastepstwo"
    Me.lblZastepstwo.Size = New System.Drawing.Size(361, 20)
    Me.lblZastepstwo.TabIndex = 151
    Me.lblZastepstwo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
    'chkStatus
    '
    Me.chkStatus.AutoSize = True
    Me.chkStatus.Enabled = False
    Me.chkStatus.Location = New System.Drawing.Point(330, 41)
    Me.chkStatus.Name = "chkStatus"
    Me.chkStatus.Size = New System.Drawing.Size(127, 17)
    Me.chkStatus.TabIndex = 153
    Me.chkStatus.Text = "Lekcja nie odbyła się"
    Me.chkStatus.UseVisualStyleBackColor = True
    '
    'cbGodzina
    '
    Me.cbGodzina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbGodzina.FormattingEnabled = True
    Me.cbGodzina.Location = New System.Drawing.Point(106, 39)
    Me.cbGodzina.Name = "cbGodzina"
    Me.cbGodzina.Size = New System.Drawing.Size(218, 21)
    Me.cbGodzina.TabIndex = 154
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(12, 42)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(88, 13)
    Me.Label5.TabIndex = 155
    Me.Label5.Text = "Godzina lekcyjna"
    '
    'dlgTematByBelfer
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(618, 517)
    Me.Controls.Add(Me.cbGodzina)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.chkStatus)
    Me.Controls.Add(Me.dtpDataZajec)
    Me.Controls.Add(Me.lblZastepstwo)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.nudNr)
    Me.Controls.Add(Me.cmdMoveOff)
    Me.Controls.Add(Me.cmdMoveDown)
    Me.Controls.Add(Me.cmdMoveBack)
    Me.Controls.Add(Me.cmdMove)
    Me.Controls.Add(Me.lvLate)
    Me.Controls.Add(Me.lvAbsent)
    Me.Controls.Add(Me.lvPresent)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtTemat)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.chkZastepstwo)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgTematByBelfer"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Nowy temat zajęć"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudNr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chkZastepstwo As System.Windows.Forms.CheckBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtTemat As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lvPresent As System.Windows.Forms.ListView
  Friend WithEvents lvAbsent As System.Windows.Forms.ListView
  Friend WithEvents lvLate As System.Windows.Forms.ListView
  Friend WithEvents cmdMove As System.Windows.Forms.Button
  Friend WithEvents cmdMoveBack As System.Windows.Forms.Button
  Friend WithEvents cmdMoveDown As System.Windows.Forms.Button
  Friend WithEvents cmdMoveOff As System.Windows.Forms.Button
  Friend WithEvents nudNr As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents lblZastepstwo As System.Windows.Forms.Label
  Friend WithEvents dtpDataZajec As System.Windows.Forms.DateTimePicker
  Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
  Friend WithEvents cbGodzina As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
