<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZachowanie
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Me.dtData = New System.Windows.Forms.DateTimePicker()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dgvZachowanie = New System.Windows.Forms.DataGridView()
    Me.cmsDgvWyniki = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.DelResultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.nudSemestr = New System.Windows.Forms.NumericUpDown()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.lblWychowawca = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    CType(Me.dgvZachowanie, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.cmsDgvWyniki.SuspendLayout()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'dtData
    '
    Me.dtData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtData.CustomFormat = ""
    Me.dtData.Location = New System.Drawing.Point(562, 3)
    Me.dtData.Name = "dtData"
    Me.dtData.Size = New System.Drawing.Size(155, 20)
    Me.dtData.TabIndex = 29
    '
    'Label5
    '
    Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label5.AutoSize = True
    Me.Label5.ForeColor = System.Drawing.Color.Green
    Me.Label5.Location = New System.Drawing.Point(434, 7)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(122, 13)
    Me.Label5.TabIndex = 27
    Me.Label5.Text = "Data wystawienia oceny"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(50, 6)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(261, 21)
    Me.cbKlasa.TabIndex = 24
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(11, 9)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 22
    Me.Label2.Text = "Klasa"
    '
    'dgvZachowanie
    '
    Me.dgvZachowanie.AllowUserToAddRows = False
    Me.dgvZachowanie.AllowUserToDeleteRows = False
    Me.dgvZachowanie.AllowUserToResizeColumns = False
    Me.dgvZachowanie.AllowUserToResizeRows = False
    Me.dgvZachowanie.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dgvZachowanie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvZachowanie.ContextMenuStrip = Me.cmsDgvWyniki
    Me.dgvZachowanie.Enabled = False
    Me.dgvZachowanie.Location = New System.Drawing.Point(11, 37)
    Me.dgvZachowanie.Name = "dgvZachowanie"
    Me.dgvZachowanie.RowHeadersVisible = False
    Me.dgvZachowanie.RowTemplate.Height = 20
    Me.dgvZachowanie.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvZachowanie.Size = New System.Drawing.Size(700, 447)
    Me.dgvZachowanie.TabIndex = 17
    '
    'cmsDgvWyniki
    '
    Me.cmsDgvWyniki.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DelResultToolStripMenuItem})
    Me.cmsDgvWyniki.Name = "cmsDgvWyniki"
    Me.cmsDgvWyniki.Size = New System.Drawing.Size(137, 26)
    '
    'DelResultToolStripMenuItem
    '
    Me.DelResultToolStripMenuItem.Enabled = False
    Me.DelResultToolStripMenuItem.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.DelResultToolStripMenuItem.Name = "DelResultToolStripMenuItem"
    Me.DelResultToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
    Me.DelResultToolStripMenuItem.Text = "Usuñ ocenê"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(317, 9)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 36
    Me.Label8.Text = "Semestr"
    '
    'nudSemestr
    '
    Me.nudSemestr.Enabled = False
    Me.nudSemestr.Location = New System.Drawing.Point(368, 7)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(34, 20)
    Me.nudSemestr.TabIndex = 37
    Me.nudSemestr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label16
    '
    Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label16.AutoSize = True
    Me.Label16.Enabled = False
    Me.Label16.Location = New System.Drawing.Point(625, 532)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(85, 13)
    Me.Label16.TabIndex = 105
    Me.Label16.Text = "Data modyfikacji"
    '
    'Label17
    '
    Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label17.AutoSize = True
    Me.Label17.Enabled = False
    Me.Label17.Location = New System.Drawing.Point(482, 532)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(31, 13)
    Me.Label17.TabIndex = 104
    Me.Label17.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(716, 527)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(120, 23)
    Me.lblData.TabIndex = 103
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(519, 527)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 101
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(89, 527)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(387, 23)
    Me.lblUser.TabIndex = 102
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label18
    '
    Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label18.AutoSize = True
    Me.Label18.Enabled = False
    Me.Label18.Location = New System.Drawing.Point(9, 532)
    Me.Label18.Name = "Label18"
    Me.Label18.Size = New System.Drawing.Size(74, 13)
    Me.Label18.TabIndex = 100
    Me.Label18.Text = "Zmodyfikowa³"
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(719, 37)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 33
    Me.cmdDelete.Text = "&Usuñ oceny"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(717, 449)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(119, 35)
    Me.cmdClose.TabIndex = 20
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.dtData)
    Me.Panel1.Location = New System.Drawing.Point(-6, 487)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(842, 34)
    Me.Panel1.TabIndex = 110
    '
    'lblWychowawca
    '
    Me.lblWychowawca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblWychowawca.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblWychowawca.ForeColor = System.Drawing.Color.Green
    Me.lblWychowawca.Location = New System.Drawing.Point(410, 9)
    Me.lblWychowawca.Name = "lblWychowawca"
    Me.lblWychowawca.Size = New System.Drawing.Size(300, 13)
    Me.lblWychowawca.TabIndex = 53
    Me.lblWychowawca.Text = "lblWychowawca"
    Me.lblWychowawca.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.Controls.Add(Me.lblWychowawca)
    Me.Panel2.Controls.Add(Me.cbKlasa)
    Me.Panel2.Controls.Add(Me.Label2)
    Me.Panel2.Controls.Add(Me.Label8)
    Me.Panel2.Controls.Add(Me.nudSemestr)
    Me.Panel2.Location = New System.Drawing.Point(-2, 3)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(713, 31)
    Me.Panel2.TabIndex = 111
    '
    'frmZachowanie
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(840, 559)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.Label16)
    Me.Controls.Add(Me.Label17)
    Me.Controls.Add(Me.lblData)
    Me.Controls.Add(Me.lblIP)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.Label18)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.dgvZachowanie)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(856, 700)
    Me.MinimumSize = New System.Drawing.Size(856, 597)
    Me.Name = "frmZachowanie"
    Me.Text = "Wprowadzanie ocen zachowania"
    CType(Me.dgvZachowanie, System.ComponentModel.ISupportInitialize).EndInit()
    Me.cmsDgvWyniki.ResumeLayout(False)
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents dtData As System.Windows.Forms.DateTimePicker
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents dgvZachowanie As System.Windows.Forms.DataGridView
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label16 As System.Windows.Forms.Label
  Friend WithEvents Label17 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label18 As System.Windows.Forms.Label
  Friend WithEvents cmsDgvWyniki As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents DelResultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents lblWychowawca As System.Windows.Forms.Label
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
