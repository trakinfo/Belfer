<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgPrivilege
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgPrivilege))
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.lvOpiekun = New System.Windows.Forms.ListView()
    Me.lvStudent = New System.Windows.Forms.ListView()
    Me.txtSeekParent = New System.Windows.Forms.TextBox()
    Me.cbSeekParent = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtSeekStudent = New System.Windows.Forms.TextBox()
    Me.cbSeekStudent = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.lvOpiekun1 = New System.Windows.Forms.ListView()
    Me.lvStudent1 = New System.Windows.Forms.ListView()
    Me.cmdAddParent = New System.Windows.Forms.Button()
    Me.cmdDeleteParent = New System.Windows.Forms.Button()
    Me.cmdDeleteStudent = New System.Windows.Forms.Button()
    Me.cmdAddStudent = New System.Windows.Forms.Button()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.lblRecord1 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.OK_Button.Enabled = False
    Me.OK_Button.Image = Global.belfer.NET.My.Resources.Resources.save_24
    Me.OK_Button.Location = New System.Drawing.Point(724, 354)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(84, 40)
    Me.OK_Button.TabIndex = 14
    Me.OK_Button.Text = "Zapi&sz"
    Me.OK_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.OK_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.Cancel_Button.FlatAppearance.BorderSize = 3
    Me.Cancel_Button.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.Cancel_Button.Location = New System.Drawing.Point(724, 469)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(84, 34)
    Me.Cancel_Button.TabIndex = 15
    Me.Cancel_Button.Text = "&Zamknij"
    Me.Cancel_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.Cancel_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'lvOpiekun
    '
    Me.lvOpiekun.Location = New System.Drawing.Point(12, 39)
    Me.lvOpiekun.MultiSelect = False
    Me.lvOpiekun.Name = "lvOpiekun"
    Me.lvOpiekun.Size = New System.Drawing.Size(350, 270)
    Me.lvOpiekun.TabIndex = 6
    Me.lvOpiekun.UseCompatibleStateImageBehavior = False
    '
    'lvStudent
    '
    Me.lvStudent.Location = New System.Drawing.Point(368, 39)
    Me.lvStudent.Name = "lvStudent"
    Me.lvStudent.Size = New System.Drawing.Size(350, 270)
    Me.lvStudent.TabIndex = 7
    Me.lvStudent.UseCompatibleStateImageBehavior = False
    '
    'txtSeekParent
    '
    Me.txtSeekParent.Location = New System.Drawing.Point(194, 12)
    Me.txtSeekParent.Name = "txtSeekParent"
    Me.txtSeekParent.Size = New System.Drawing.Size(168, 20)
    Me.txtSeekParent.TabIndex = 3
    Me.txtSeekParent.Tag = "0"
    '
    'cbSeekParent
    '
    Me.cbSeekParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeekParent.DropDownWidth = 200
    Me.cbSeekParent.FormattingEnabled = True
    Me.cbSeekParent.Location = New System.Drawing.Point(66, 12)
    Me.cbSeekParent.Name = "cbSeekParent"
    Me.cbSeekParent.Size = New System.Drawing.Size(122, 21)
    Me.cbSeekParent.TabIndex = 2
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(48, 13)
    Me.Label1.TabIndex = 48
    Me.Label1.Text = "Filtruj wg"
    '
    'txtSeekStudent
    '
    Me.txtSeekStudent.Location = New System.Drawing.Point(549, 12)
    Me.txtSeekStudent.Name = "txtSeekStudent"
    Me.txtSeekStudent.Size = New System.Drawing.Size(169, 20)
    Me.txtSeekStudent.TabIndex = 5
    Me.txtSeekStudent.Tag = "1"
    '
    'cbSeekStudent
    '
    Me.cbSeekStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeekStudent.DropDownWidth = 200
    Me.cbSeekStudent.FormattingEnabled = True
    Me.cbSeekStudent.Location = New System.Drawing.Point(421, 12)
    Me.cbSeekStudent.Name = "cbSeekStudent"
    Me.cbSeekStudent.Size = New System.Drawing.Size(122, 21)
    Me.cbSeekStudent.TabIndex = 4
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(367, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(48, 13)
    Me.Label2.TabIndex = 51
    Me.Label2.Text = "Filtruj wg"
    '
    'lvOpiekun1
    '
    Me.lvOpiekun1.BackColor = System.Drawing.SystemColors.Window
    Me.lvOpiekun1.Location = New System.Drawing.Point(12, 354)
    Me.lvOpiekun1.Name = "lvOpiekun1"
    Me.lvOpiekun1.Size = New System.Drawing.Size(350, 149)
    Me.lvOpiekun1.TabIndex = 12
    Me.lvOpiekun1.UseCompatibleStateImageBehavior = False
    '
    'lvStudent1
    '
    Me.lvStudent1.BackColor = System.Drawing.SystemColors.Window
    Me.lvStudent1.Location = New System.Drawing.Point(368, 354)
    Me.lvStudent1.Name = "lvStudent1"
    Me.lvStudent1.Size = New System.Drawing.Size(350, 149)
    Me.lvStudent1.TabIndex = 13
    Me.lvStudent1.UseCompatibleStateImageBehavior = False
    '
    'cmdAddParent
    '
    Me.cmdAddParent.Enabled = False
    Me.cmdAddParent.Image = CType(resources.GetObject("cmdAddParent.Image"), System.Drawing.Image)
    Me.cmdAddParent.Location = New System.Drawing.Point(196, 315)
    Me.cmdAddParent.Name = "cmdAddParent"
    Me.cmdAddParent.Size = New System.Drawing.Size(75, 33)
    Me.cmdAddParent.TabIndex = 8
    Me.cmdAddParent.Text = "Dodaj"
    Me.cmdAddParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddParent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddParent.UseVisualStyleBackColor = True
    '
    'cmdDeleteParent
    '
    Me.cmdDeleteParent.Enabled = False
    Me.cmdDeleteParent.Image = CType(resources.GetObject("cmdDeleteParent.Image"), System.Drawing.Image)
    Me.cmdDeleteParent.Location = New System.Drawing.Point(277, 315)
    Me.cmdDeleteParent.Name = "cmdDeleteParent"
    Me.cmdDeleteParent.Size = New System.Drawing.Size(75, 33)
    Me.cmdDeleteParent.TabIndex = 9
    Me.cmdDeleteParent.Text = "Usuń"
    Me.cmdDeleteParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDeleteParent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDeleteParent.UseVisualStyleBackColor = True
    '
    'cmdDeleteStudent
    '
    Me.cmdDeleteStudent.Enabled = False
    Me.cmdDeleteStudent.Image = CType(resources.GetObject("cmdDeleteStudent.Image"), System.Drawing.Image)
    Me.cmdDeleteStudent.Location = New System.Drawing.Point(639, 315)
    Me.cmdDeleteStudent.Name = "cmdDeleteStudent"
    Me.cmdDeleteStudent.Size = New System.Drawing.Size(75, 33)
    Me.cmdDeleteStudent.TabIndex = 11
    Me.cmdDeleteStudent.Text = "Usuń"
    Me.cmdDeleteStudent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDeleteStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDeleteStudent.UseVisualStyleBackColor = True
    '
    'cmdAddStudent
    '
    Me.cmdAddStudent.Enabled = False
    Me.cmdAddStudent.Image = CType(resources.GetObject("cmdAddStudent.Image"), System.Drawing.Image)
    Me.cmdAddStudent.Location = New System.Drawing.Point(558, 315)
    Me.cmdAddStudent.Name = "cmdAddStudent"
    Me.cmdAddStudent.Size = New System.Drawing.Size(75, 33)
    Me.cmdAddStudent.TabIndex = 10
    Me.cmdAddStudent.Text = "Dodaj"
    Me.cmdAddStudent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddStudent.UseVisualStyleBackColor = True
    '
    'Label4
    '
    Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(367, 312)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(45, 13)
    Me.Label4.TabIndex = 147
    Me.Label4.Text = "Rekord:"
    '
    'lblRecord1
    '
    Me.lblRecord1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord1.AutoSize = True
    Me.lblRecord1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord1.ForeColor = System.Drawing.Color.Red
    Me.lblRecord1.Location = New System.Drawing.Point(418, 312)
    Me.lblRecord1.Name = "lblRecord1"
    Me.lblRecord1.Size = New System.Drawing.Size(68, 13)
    Me.lblRecord1.TabIndex = 148
    Me.lblRecord1.Text = "lblRecord1"
    '
    'Label6
    '
    Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(12, 312)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(45, 13)
    Me.Label6.TabIndex = 149
    Me.Label6.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(63, 312)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 150
    Me.lblRecord.Text = "lblRecord"
    '
    'dlgPrivilege
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(814, 515)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.lblRecord)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.lblRecord1)
    Me.Controls.Add(Me.Cancel_Button)
    Me.Controls.Add(Me.OK_Button)
    Me.Controls.Add(Me.cmdDeleteStudent)
    Me.Controls.Add(Me.cmdAddStudent)
    Me.Controls.Add(Me.cmdDeleteParent)
    Me.Controls.Add(Me.cmdAddParent)
    Me.Controls.Add(Me.lvStudent1)
    Me.Controls.Add(Me.lvOpiekun1)
    Me.Controls.Add(Me.txtSeekStudent)
    Me.Controls.Add(Me.cbSeekStudent)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtSeekParent)
    Me.Controls.Add(Me.cbSeekParent)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lvStudent)
    Me.Controls.Add(Me.lvOpiekun)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgPrivilege"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Nadawanie praw do odczytu wyników nauczania"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents lvOpiekun As System.Windows.Forms.ListView
  Friend WithEvents lvStudent As System.Windows.Forms.ListView
  Friend WithEvents txtSeekParent As System.Windows.Forms.TextBox
  Friend WithEvents cbSeekParent As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtSeekStudent As System.Windows.Forms.TextBox
  Friend WithEvents cbSeekStudent As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents lvOpiekun1 As System.Windows.Forms.ListView
  Friend WithEvents lvStudent1 As System.Windows.Forms.ListView
  Friend WithEvents cmdAddParent As System.Windows.Forms.Button
  Friend WithEvents cmdDeleteParent As System.Windows.Forms.Button
  Friend WithEvents cmdDeleteStudent As System.Windows.Forms.Button
  Friend WithEvents cmdAddStudent As System.Windows.Forms.Button
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents lblRecord1 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label

End Class
