<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTematByBelfer
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
    Me.components = New System.ComponentModel.Container()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cbObsada = New System.Windows.Forms.ComboBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.lblZastepstwo = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cbBelfer = New System.Windows.Forms.ComboBox()
    Me.cmdEvent = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.ttEvent = New System.Windows.Forms.ToolTip(Me.components)
    Me.ttEventOmitted = New System.Windows.Forms.ToolTip(Me.components)
    Me.lvTemat = New System.Windows.Forms.ListView()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(59, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Nauczyciel"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(390, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(89, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Klasa {przedmiot}"
    '
    'cbObsada
    '
    Me.cbObsada.DropDownHeight = 500
    Me.cbObsada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbObsada.Enabled = False
    Me.cbObsada.FormattingEnabled = True
    Me.cbObsada.IntegralHeight = False
    Me.cbObsada.Location = New System.Drawing.Point(483, 12)
    Me.cbObsada.Name = "cbObsada"
    Me.cbObsada.Size = New System.Drawing.Size(324, 21)
    Me.cbObsada.TabIndex = 2
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.lblZastepstwo)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(12, 471)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(960, 30)
    Me.Panel1.TabIndex = 203
    '
    'lblZastepstwo
    '
    Me.lblZastepstwo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblZastepstwo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblZastepstwo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.lblZastepstwo.Location = New System.Drawing.Point(350, 6)
    Me.lblZastepstwo.Name = "lblZastepstwo"
    Me.lblZastepstwo.Size = New System.Drawing.Size(473, 13)
    Me.lblZastepstwo.TabIndex = 145
    Me.lblZastepstwo.Text = "Zastępstwo"
    Me.lblZastepstwo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 6)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 143
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 6)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 144
    Me.lblRecord.Text = "lblRecord"
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Panel2.Controls.Add(Me.Label14)
    Me.Panel2.Controls.Add(Me.Label12)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Location = New System.Drawing.Point(12, 504)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(960, 29)
    Me.Panel2.TabIndex = 205
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(745, 8)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 147
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(582, 8)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 146
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(836, 3)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(121, 23)
    Me.lblData.TabIndex = 145
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(619, 3)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(120, 23)
    Me.lblIP.TabIndex = 143
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 3)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(493, 23)
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
    'cbBelfer
    '
    Me.cbBelfer.DropDownHeight = 500
    Me.cbBelfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbBelfer.Enabled = False
    Me.cbBelfer.FormattingEnabled = True
    Me.cbBelfer.IntegralHeight = False
    Me.cbBelfer.Location = New System.Drawing.Point(77, 10)
    Me.cbBelfer.Name = "cbBelfer"
    Me.cbBelfer.Size = New System.Drawing.Size(296, 21)
    Me.cbBelfer.TabIndex = 208
    '
    'cmdEvent
    '
    Me.cmdEvent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEvent.Enabled = False
    Me.cmdEvent.Image = Global.belfer.NET.My.Resources.Resources.event_24
    Me.cmdEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEvent.Location = New System.Drawing.Point(855, 191)
    Me.cmdEvent.Name = "cmdEvent"
    Me.cmdEvent.Size = New System.Drawing.Size(117, 36)
    Me.cmdEvent.TabIndex = 209
    Me.cmdEvent.Text = "Zdarzenie"
    Me.cmdEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEvent.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(855, 435)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 206
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.Location = New System.Drawing.Point(855, 80)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(117, 36)
    Me.cmdEdit.TabIndex = 115
    Me.cmdEdit.Text = "Edytuj temat"
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.Location = New System.Drawing.Point(855, 38)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddNew.TabIndex = 116
    Me.cmdAddNew.Text = "Nowy temat"
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(855, 122)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 114
    Me.cmdDelete.Text = "&Usuń temat"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'lvTemat
    '
    Me.lvTemat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvTemat.Location = New System.Drawing.Point(12, 37)
    Me.lvTemat.Name = "lvTemat"
    Me.lvTemat.Size = New System.Drawing.Size(834, 430)
    Me.lvTemat.TabIndex = 211
    Me.lvTemat.UseCompatibleStateImageBehavior = False
    '
    'frmTematByBelfer
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(984, 541)
    Me.Controls.Add(Me.lvTemat)
    Me.Controls.Add(Me.cmdEvent)
    Me.Controls.Add(Me.cbBelfer)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cbObsada)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Name = "frmTematByBelfer"
    Me.Text = "Tematy zajęć"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cbObsada As System.Windows.Forms.ComboBox
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents lblZastepstwo As System.Windows.Forms.Label
  Friend WithEvents cbBelfer As System.Windows.Forms.ComboBox
  Friend WithEvents cmdEvent As System.Windows.Forms.Button
  Friend WithEvents ttEvent As System.Windows.Forms.ToolTip
  Friend WithEvents ttEventOmitted As System.Windows.Forms.ToolTip
  Friend WithEvents lvTemat As System.Windows.Forms.ListView
End Class
