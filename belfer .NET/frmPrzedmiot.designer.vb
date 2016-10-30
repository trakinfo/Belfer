<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrzedmiot
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrzedmiot))
    Me.lvPrzedmiot = New System.Windows.Forms.ListView()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.cbSeek = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.cmdStart = New System.Windows.Forms.Button()
    Me.cmdUp = New System.Windows.Forms.Button()
    Me.cmdDown = New System.Windows.Forms.Button()
    Me.cmdEnd = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvPrzedmiot
    '
    Me.lvPrzedmiot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvPrzedmiot.Location = New System.Drawing.Point(12, 12)
    Me.lvPrzedmiot.Name = "lvPrzedmiot"
    Me.lvPrzedmiot.Size = New System.Drawing.Size(706, 350)
    Me.lvPrzedmiot.TabIndex = 9
    Me.lvPrzedmiot.UseCompatibleStateImageBehavior = False
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.txtSeek)
    Me.Panel1.Controls.Add(Me.cbSeek)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(12, 368)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(813, 32)
    Me.Panel1.TabIndex = 13
    '
    'txtSeek
    '
    Me.txtSeek.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeek.Location = New System.Drawing.Point(530, 3)
    Me.txtSeek.Name = "txtSeek"
    Me.txtSeek.Size = New System.Drawing.Size(176, 20)
    Me.txtSeek.TabIndex = 149
    '
    'cbSeek
    '
    Me.cbSeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeek.DropDownWidth = 200
    Me.cbSeek.FormattingEnabled = True
    Me.cbSeek.Location = New System.Drawing.Point(402, 2)
    Me.cbSeek.Name = "cbSeek"
    Me.cbSeek.Size = New System.Drawing.Size(122, 21)
    Me.cbSeek.TabIndex = 148
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(348, 6)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(48, 13)
    Me.Label1.TabIndex = 147
    Me.Label1.Text = "Filtruj wg"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 6)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 145
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
    Me.lblRecord.TabIndex = 146
    Me.lblRecord.Text = "lblRecord"
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
    Me.Panel2.Location = New System.Drawing.Point(12, 403)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(813, 34)
    Me.Panel2.TabIndex = 15
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(599, 10)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 169
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(456, 10)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 168
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(690, 5)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(120, 23)
    Me.lblData.TabIndex = 167
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(493, 5)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 165
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 5)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(367, 23)
    Me.lblUser.TabIndex = 166
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(3, 10)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 164
    Me.Label3.Text = "Zmodyfikował"
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.cmdStart)
    Me.GroupBox1.Controls.Add(Me.cmdUp)
    Me.GroupBox1.Controls.Add(Me.cmdDown)
    Me.GroupBox1.Controls.Add(Me.cmdEnd)
    Me.GroupBox1.Location = New System.Drawing.Point(724, 143)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(97, 140)
    Me.GroupBox1.TabIndex = 27
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Priorytet"
    '
    'cmdStart
    '
    Me.cmdStart.Enabled = False
    Me.cmdStart.Location = New System.Drawing.Point(6, 19)
    Me.cmdStart.Name = "cmdStart"
    Me.cmdStart.Size = New System.Drawing.Size(85, 23)
    Me.cmdStart.TabIndex = 11
    Me.cmdStart.Text = "Na początek"
    '
    'cmdUp
    '
    Me.cmdUp.Enabled = False
    Me.cmdUp.Location = New System.Drawing.Point(6, 48)
    Me.cmdUp.Name = "cmdUp"
    Me.cmdUp.Size = New System.Drawing.Size(85, 23)
    Me.cmdUp.TabIndex = 10
    Me.cmdUp.Text = "W górę"
    '
    'cmdDown
    '
    Me.cmdDown.Enabled = False
    Me.cmdDown.Location = New System.Drawing.Point(6, 78)
    Me.cmdDown.Name = "cmdDown"
    Me.cmdDown.Size = New System.Drawing.Size(85, 23)
    Me.cmdDown.TabIndex = 12
    Me.cmdDown.Text = "W dół"
    '
    'cmdEnd
    '
    Me.cmdEnd.Enabled = False
    Me.cmdEnd.Location = New System.Drawing.Point(6, 108)
    Me.cmdEnd.Name = "cmdEnd"
    Me.cmdEnd.Size = New System.Drawing.Size(85, 23)
    Me.cmdEnd.TabIndex = 13
    Me.cmdEnd.Text = "Na koniec"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
    Me.cmdClose.Location = New System.Drawing.Point(730, 327)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(85, 35)
    Me.cmdClose.TabIndex = 183
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(730, 92)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(85, 34)
    Me.cmdDelete.TabIndex = 186
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.Location = New System.Drawing.Point(730, 52)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(85, 34)
    Me.cmdEdit.TabIndex = 185
    Me.cmdEdit.Text = "&Edytuj"
    Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.Location = New System.Drawing.Point(730, 12)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(85, 34)
    Me.cmdAddNew.TabIndex = 184
    Me.cmdAddNew.Text = "&Dodaj"
    Me.cmdAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'frmPrzedmiot
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(828, 446)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.lvPrzedmiot)
    Me.Name = "frmPrzedmiot"
    Me.Text = "Przedmioty nauczania"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents lvPrzedmiot As System.Windows.Forms.ListView
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
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents cbSeek As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents cmdStart As System.Windows.Forms.Button
  Friend WithEvents cmdUp As System.Windows.Forms.Button
  Friend WithEvents cmdDown As System.Windows.Forms.Button
  Friend WithEvents cmdEnd As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
End Class
