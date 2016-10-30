<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbsencja
    Inherits System.Windows.Forms.Form

    'Formularz zastępuje metodę dispose, aby wyczyścić listę składników.
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

    'Wymagane przez Projektanta formularzy systemu Windows
    Private components As System.ComponentModel.IContainer

    'UWAGA: Następująca procedura jest wymagana przez Projektanta formularzy systemu Windows
    'Można to modyfikować, używając Projektanta formularzy systemu Windows.  
    'Nie należy modyfikować za pomocą edytora kodu.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dgvAbsencja = New System.Windows.Forms.DataGridView()
    Me.cmsAbsencja = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.InsertUnjustifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.InsertJustifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.InsertLatenessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
    Me.ChangeToJustifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ChangeToUnjustifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ChangeToLatenessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
    Me.ChangeLatenessToJustifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ChangeLatenessToUnjustifiedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
    Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.DeleteAndNotifyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdTimeSpan = New System.Windows.Forms.Button()
    Me.mcData = New System.Windows.Forms.MonthCalendar()
    Me.cmdForward = New System.Windows.Forms.Button()
    Me.cmdBack = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.chkVirtual = New System.Windows.Forms.CheckBox()
    CType(Me.dgvAbsencja, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.cmsAbsencja.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(51, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(261, 21)
    Me.cbKlasa.TabIndex = 26
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 25
    Me.Label2.Text = "Klasa"
    '
    'dgvAbsencja
    '
    Me.dgvAbsencja.AllowUserToAddRows = False
    Me.dgvAbsencja.AllowUserToDeleteRows = False
    Me.dgvAbsencja.AllowUserToResizeColumns = False
    Me.dgvAbsencja.AllowUserToResizeRows = False
    Me.dgvAbsencja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dgvAbsencja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvAbsencja.Enabled = False
    Me.dgvAbsencja.Location = New System.Drawing.Point(12, 39)
    Me.dgvAbsencja.Name = "dgvAbsencja"
    Me.dgvAbsencja.RowHeadersVisible = False
    Me.dgvAbsencja.RowTemplate.Height = 20
    Me.dgvAbsencja.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvAbsencja.Size = New System.Drawing.Size(1041, 480)
    Me.dgvAbsencja.TabIndex = 27
    '
    'cmsAbsencja
    '
    Me.cmsAbsencja.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InsertUnjustifiedToolStripMenuItem, Me.InsertJustifiedToolStripMenuItem, Me.InsertLatenessToolStripMenuItem, Me.ToolStripSeparator1, Me.ChangeToJustifiedToolStripMenuItem, Me.ChangeToUnjustifiedToolStripMenuItem, Me.ChangeToLatenessToolStripMenuItem, Me.ToolStripSeparator2, Me.ChangeLatenessToJustifiedToolStripMenuItem, Me.ChangeLatenessToUnjustifiedToolStripMenuItem, Me.ToolStripSeparator3, Me.DeleteToolStripMenuItem, Me.DeleteAndNotifyToolStripMenuItem})
    Me.cmsAbsencja.Name = "cmsAbsencja"
    Me.cmsAbsencja.ShowImageMargin = False
    Me.cmsAbsencja.Size = New System.Drawing.Size(345, 242)
    '
    'InsertUnjustifiedToolStripMenuItem
    '
    Me.InsertUnjustifiedToolStripMenuItem.Enabled = False
    Me.InsertUnjustifiedToolStripMenuItem.Name = "InsertUnjustifiedToolStripMenuItem"
    Me.InsertUnjustifiedToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.InsertUnjustifiedToolStripMenuItem.Tag = "n"
    Me.InsertUnjustifiedToolStripMenuItem.Text = "Wstaw nieobecność nieusprawiedliwioną"
    '
    'InsertJustifiedToolStripMenuItem
    '
    Me.InsertJustifiedToolStripMenuItem.Enabled = False
    Me.InsertJustifiedToolStripMenuItem.Name = "InsertJustifiedToolStripMenuItem"
    Me.InsertJustifiedToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.InsertJustifiedToolStripMenuItem.Tag = "u"
    Me.InsertJustifiedToolStripMenuItem.Text = "Wstaw nieobecność usprawiedliwioną"
    '
    'InsertLatenessToolStripMenuItem
    '
    Me.InsertLatenessToolStripMenuItem.Enabled = False
    Me.InsertLatenessToolStripMenuItem.Name = "InsertLatenessToolStripMenuItem"
    Me.InsertLatenessToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.InsertLatenessToolStripMenuItem.Tag = "s"
    Me.InsertLatenessToolStripMenuItem.Text = "Wstaw spóźnienie"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(341, 6)
    '
    'ChangeToJustifiedToolStripMenuItem
    '
    Me.ChangeToJustifiedToolStripMenuItem.Enabled = False
    Me.ChangeToJustifiedToolStripMenuItem.Name = "ChangeToJustifiedToolStripMenuItem"
    Me.ChangeToJustifiedToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.ChangeToJustifiedToolStripMenuItem.Text = "Zmień nieobecność na usprawiedliwioną"
    '
    'ChangeToUnjustifiedToolStripMenuItem
    '
    Me.ChangeToUnjustifiedToolStripMenuItem.Enabled = False
    Me.ChangeToUnjustifiedToolStripMenuItem.Name = "ChangeToUnjustifiedToolStripMenuItem"
    Me.ChangeToUnjustifiedToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.ChangeToUnjustifiedToolStripMenuItem.Text = "Zmień nieobecność na nieusprawiedliwioną"
    '
    'ChangeToLatenessToolStripMenuItem
    '
    Me.ChangeToLatenessToolStripMenuItem.Enabled = False
    Me.ChangeToLatenessToolStripMenuItem.Name = "ChangeToLatenessToolStripMenuItem"
    Me.ChangeToLatenessToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.ChangeToLatenessToolStripMenuItem.Text = "Zmień nieobecność na spóźnienie"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(341, 6)
    '
    'ChangeLatenessToJustifiedToolStripMenuItem
    '
    Me.ChangeLatenessToJustifiedToolStripMenuItem.Enabled = False
    Me.ChangeLatenessToJustifiedToolStripMenuItem.Name = "ChangeLatenessToJustifiedToolStripMenuItem"
    Me.ChangeLatenessToJustifiedToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.ChangeLatenessToJustifiedToolStripMenuItem.Tag = "u"
    Me.ChangeLatenessToJustifiedToolStripMenuItem.Text = "Zmień spóźnienie na nieobecność usprawiedliwioną"
    '
    'ChangeLatenessToUnjustifiedToolStripMenuItem
    '
    Me.ChangeLatenessToUnjustifiedToolStripMenuItem.Enabled = False
    Me.ChangeLatenessToUnjustifiedToolStripMenuItem.Name = "ChangeLatenessToUnjustifiedToolStripMenuItem"
    Me.ChangeLatenessToUnjustifiedToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.ChangeLatenessToUnjustifiedToolStripMenuItem.Tag = "n"
    Me.ChangeLatenessToUnjustifiedToolStripMenuItem.Text = "Zmień spóźnienie na nieobecność nieusprawiedliwioną"
    '
    'ToolStripSeparator3
    '
    Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    Me.ToolStripSeparator3.Size = New System.Drawing.Size(341, 6)
    '
    'DeleteToolStripMenuItem
    '
    Me.DeleteToolStripMenuItem.Enabled = False
    Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
    Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.DeleteToolStripMenuItem.Text = "Usuń nieobecność/spóźnienie"
    '
    'DeleteAndNotifyToolStripMenuItem
    '
    Me.DeleteAndNotifyToolStripMenuItem.Enabled = False
    Me.DeleteAndNotifyToolStripMenuItem.Name = "DeleteAndNotifyToolStripMenuItem"
    Me.DeleteAndNotifyToolStripMenuItem.Size = New System.Drawing.Size(344, 22)
    Me.DeleteAndNotifyToolStripMenuItem.Text = "Usuń nieobecność/spóźnienie i powiadom autora wpisu"
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.Controls.Add(Me.Label14)
    Me.Panel2.Controls.Add(Me.Label12)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Location = New System.Drawing.Point(12, 526)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(906, 29)
    Me.Panel2.TabIndex = 207
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(691, 8)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 147
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(548, 8)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 146
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(782, 3)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(120, 23)
    Me.lblData.TabIndex = 145
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(585, 3)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 143
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 3)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(459, 23)
    Me.lblUser.TabIndex = 144
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(3, 8)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 142
    Me.Label3.Text = "Zmodyfikował"
    '
    'cmdTimeSpan
    '
    Me.cmdTimeSpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdTimeSpan.BackColor = System.Drawing.SystemColors.Info
    Me.cmdTimeSpan.FlatStyle = System.Windows.Forms.FlatStyle.Popup
    Me.cmdTimeSpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.cmdTimeSpan.ForeColor = System.Drawing.Color.Blue
    Me.cmdTimeSpan.Location = New System.Drawing.Point(545, 10)
    Me.cmdTimeSpan.Margin = New System.Windows.Forms.Padding(0)
    Me.cmdTimeSpan.Name = "cmdTimeSpan"
    Me.cmdTimeSpan.Size = New System.Drawing.Size(479, 23)
    Me.cmdTimeSpan.TabIndex = 210
    Me.cmdTimeSpan.UseVisualStyleBackColor = False
    '
    'mcData
    '
    Me.mcData.CalendarDimensions = New System.Drawing.Size(3, 1)
    Me.mcData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.mcData.Location = New System.Drawing.Point(238, 39)
    Me.mcData.Name = "mcData"
    Me.mcData.TabIndex = 211
    Me.mcData.Visible = False
    '
    'cmdForward
    '
    Me.cmdForward.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdForward.Image = Global.belfer.NET.My.Resources.Resources.arrow_forward_16
    Me.cmdForward.Location = New System.Drawing.Point(1030, 10)
    Me.cmdForward.Name = "cmdForward"
    Me.cmdForward.Size = New System.Drawing.Size(23, 23)
    Me.cmdForward.TabIndex = 213
    Me.cmdForward.UseVisualStyleBackColor = True
    '
    'cmdBack
    '
    Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdBack.Image = Global.belfer.NET.My.Resources.Resources.arrow_back_16
    Me.cmdBack.Location = New System.Drawing.Point(516, 10)
    Me.cmdBack.Name = "cmdBack"
    Me.cmdBack.Size = New System.Drawing.Size(23, 23)
    Me.cmdBack.TabIndex = 212
    Me.cmdBack.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(960, 523)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(93, 35)
    Me.cmdClose.TabIndex = 208
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'chkVirtual
    '
    Me.chkVirtual.AutoSize = True
    Me.chkVirtual.Location = New System.Drawing.Point(318, 14)
    Me.chkVirtual.Name = "chkVirtual"
    Me.chkVirtual.Size = New System.Drawing.Size(97, 17)
    Me.chkVirtual.TabIndex = 214
    Me.chkVirtual.Text = "Klasa wirtualna"
    Me.chkVirtual.UseVisualStyleBackColor = True
    '
    'frmAbsencja
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1065, 567)
    Me.Controls.Add(Me.chkVirtual)
    Me.Controls.Add(Me.cmdForward)
    Me.Controls.Add(Me.cmdBack)
    Me.Controls.Add(Me.mcData)
    Me.Controls.Add(Me.cmdTimeSpan)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.dgvAbsencja)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label2)
    Me.MinimumSize = New System.Drawing.Size(1000, 605)
    Me.Name = "frmAbsencja"
    Me.Text = "Absencja na zajęciach lekcyjnych"
    CType(Me.dgvAbsencja, System.ComponentModel.ISupportInitialize).EndInit()
    Me.cmsAbsencja.ResumeLayout(False)
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout

End Sub
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents dgvAbsencja As System.Windows.Forms.DataGridView
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdTimeSpan As System.Windows.Forms.Button
  Friend WithEvents mcData As System.Windows.Forms.MonthCalendar
  Friend WithEvents cmdBack As System.Windows.Forms.Button
  Friend WithEvents cmdForward As System.Windows.Forms.Button
  Friend WithEvents cmsAbsencja As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents InsertUnjustifiedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents InsertJustifiedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents InsertLatenessToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ChangeToJustifiedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ChangeToUnjustifiedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ChangeToLatenessToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ChangeLatenessToJustifiedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ChangeLatenessToUnjustifiedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents chkVirtual As System.Windows.Forms.CheckBox
  Friend WithEvents DeleteAndNotifyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
