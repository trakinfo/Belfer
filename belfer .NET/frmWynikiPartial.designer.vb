<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWynikiPartial
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
    Me.cbPrzedmiot = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dgvZestawienieOcen = New System.Windows.Forms.DataGridView()
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
    Me.lblOpis = New System.Windows.Forms.Label()
    Me.cmdEditOpis = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdInsertColumn = New System.Windows.Forms.Button()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.chkVirtual = New System.Windows.Forms.CheckBox()
    CType(Me.dgvZestawienieOcen, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.cmsDgvWyniki.SuspendLayout()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'dtData
    '
    Me.dtData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtData.CustomFormat = ""
    Me.dtData.Location = New System.Drawing.Point(737, 3)
    Me.dtData.Name = "dtData"
    Me.dtData.Size = New System.Drawing.Size(155, 20)
    Me.dtData.TabIndex = 29
    '
    'cbPrzedmiot
    '
    Me.cbPrzedmiot.DropDownHeight = 500
    Me.cbPrzedmiot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPrzedmiot.Enabled = False
    Me.cbPrzedmiot.FormattingEnabled = True
    Me.cbPrzedmiot.IntegralHeight = False
    Me.cbPrzedmiot.Location = New System.Drawing.Point(456, 10)
    Me.cbPrzedmiot.Name = "cbPrzedmiot"
    Me.cbPrzedmiot.Size = New System.Drawing.Size(339, 21)
    Me.cbPrzedmiot.TabIndex = 28
    '
    'Label5
    '
    Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label5.AutoSize = True
    Me.Label5.ForeColor = System.Drawing.Color.Green
    Me.Label5.Location = New System.Drawing.Point(578, 7)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(153, 13)
    Me.Label5.TabIndex = 27
    Me.Label5.Text = "Data wystawienia nowej oceny"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(397, 13)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(53, 13)
    Me.Label4.TabIndex = 26
    Me.Label4.Text = "Przedmiot"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(48, 10)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(240, 21)
    Me.cbKlasa.TabIndex = 24
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(9, 13)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 22
    Me.Label2.Text = "Klasa"
    '
    'dgvZestawienieOcen
    '
    Me.dgvZestawienieOcen.AllowUserToAddRows = False
    Me.dgvZestawienieOcen.AllowUserToDeleteRows = False
    Me.dgvZestawienieOcen.AllowUserToResizeColumns = False
    Me.dgvZestawienieOcen.AllowUserToResizeRows = False
    Me.dgvZestawienieOcen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dgvZestawienieOcen.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
    Me.dgvZestawienieOcen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvZestawienieOcen.ContextMenuStrip = Me.cmsDgvWyniki
    Me.dgvZestawienieOcen.Enabled = False
    Me.dgvZestawienieOcen.EnableHeadersVisualStyles = False
    Me.dgvZestawienieOcen.Location = New System.Drawing.Point(11, 37)
    Me.dgvZestawienieOcen.Name = "dgvZestawienieOcen"
    Me.dgvZestawienieOcen.RowHeadersVisible = False
    Me.dgvZestawienieOcen.RowTemplate.Height = 20
    Me.dgvZestawienieOcen.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvZestawienieOcen.Size = New System.Drawing.Size(875, 447)
    Me.dgvZestawienieOcen.TabIndex = 17
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
    Me.Label8.Location = New System.Drawing.Point(801, 12)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 36
    Me.Label8.Text = "Semestr"
    '
    'nudSemestr
    '
    Me.nudSemestr.Location = New System.Drawing.Point(852, 11)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(34, 20)
    Me.nudSemestr.TabIndex = 37
    '
    'Label16
    '
    Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label16.AutoSize = True
    Me.Label16.Enabled = False
    Me.Label16.Location = New System.Drawing.Point(792, 532)
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
    Me.Label17.Location = New System.Drawing.Point(649, 532)
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
    Me.lblData.Location = New System.Drawing.Point(883, 527)
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
    Me.lblIP.Location = New System.Drawing.Point(686, 527)
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
    Me.lblUser.Size = New System.Drawing.Size(554, 23)
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
    'lblOpis
    '
    Me.lblOpis.ForeColor = System.Drawing.Color.Blue
    Me.lblOpis.Location = New System.Drawing.Point(14, 7)
    Me.lblOpis.Name = "lblOpis"
    Me.lblOpis.Size = New System.Drawing.Size(554, 13)
    Me.lblOpis.TabIndex = 107
    Me.lblOpis.Text = "OpisKolumny"
    '
    'cmdEditOpis
    '
    Me.cmdEditOpis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEditOpis.Enabled = False
    Me.cmdEditOpis.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEditOpis.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEditOpis.Location = New System.Drawing.Point(892, 79)
    Me.cmdEditOpis.Name = "cmdEditOpis"
    Me.cmdEditOpis.Size = New System.Drawing.Size(117, 36)
    Me.cmdEditOpis.TabIndex = 108
    Me.cmdEditOpis.Text = "Edytuj parametry kolumny"
    Me.cmdEditOpis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEditOpis.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(892, 121)
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
    Me.cmdClose.Location = New System.Drawing.Point(892, 449)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(119, 35)
    Me.cmdClose.TabIndex = 20
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdInsertColumn
    '
    Me.cmdInsertColumn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdInsertColumn.Enabled = False
    Me.cmdInsertColumn.Image = Global.belfer.NET.My.Resources.Resources.insert_column_icon
    Me.cmdInsertColumn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdInsertColumn.Location = New System.Drawing.Point(892, 37)
    Me.cmdInsertColumn.Name = "cmdInsertColumn"
    Me.cmdInsertColumn.Size = New System.Drawing.Size(117, 36)
    Me.cmdInsertColumn.TabIndex = 109
    Me.cmdInsertColumn.Text = "Wstaw kolumny"
    Me.cmdInsertColumn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdInsertColumn.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.lblOpis)
    Me.Panel1.Controls.Add(Me.dtData)
    Me.Panel1.Location = New System.Drawing.Point(-6, 487)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(1017, 34)
    Me.Panel1.TabIndex = 110
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(892, 408)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 35)
    Me.cmdPrint.TabIndex = 123
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'chkVirtual
    '
    Me.chkVirtual.AutoSize = True
    Me.chkVirtual.Location = New System.Drawing.Point(294, 12)
    Me.chkVirtual.Name = "chkVirtual"
    Me.chkVirtual.Size = New System.Drawing.Size(97, 17)
    Me.chkVirtual.TabIndex = 200
    Me.chkVirtual.Text = "Klasa wirtualna"
    Me.chkVirtual.UseVisualStyleBackColor = True
    '
    'frmWynikiPartial
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(1015, 559)
    Me.Controls.Add(Me.chkVirtual)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdEditOpis)
    Me.Controls.Add(Me.cmdInsertColumn)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.Label16)
    Me.Controls.Add(Me.Label17)
    Me.Controls.Add(Me.lblData)
    Me.Controls.Add(Me.lblIP)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.Label18)
    Me.Controls.Add(Me.nudSemestr)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.cbPrzedmiot)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.dgvZestawienieOcen)
    Me.Name = "frmWynikiPartial"
    Me.Text = "Wprowadzanie ocen cz¹stkowych"
    CType(Me.dgvZestawienieOcen, System.ComponentModel.ISupportInitialize).EndInit()
    Me.cmsDgvWyniki.ResumeLayout(False)
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents dtData As System.Windows.Forms.DateTimePicker
  Friend WithEvents cbPrzedmiot As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents dgvZestawienieOcen As System.Windows.Forms.DataGridView
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label16 As System.Windows.Forms.Label
  Friend WithEvents Label17 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label18 As System.Windows.Forms.Label
  Friend WithEvents lblOpis As System.Windows.Forms.Label
  Friend WithEvents cmdEditOpis As System.Windows.Forms.Button
  Friend WithEvents cmsDgvWyniki As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents DelResultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents cmdInsertColumn As System.Windows.Forms.Button
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents chkVirtual As System.Windows.Forms.CheckBox
End Class
