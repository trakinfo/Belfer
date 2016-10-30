<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpisWyniku
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
    Me.lvOpisWyniku = New System.Windows.Forms.ListView()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cbPrzedmiot = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvOpisWyniku
    '
    Me.lvOpisWyniku.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvOpisWyniku.Location = New System.Drawing.Point(12, 41)
    Me.lvOpisWyniku.Name = "lvOpisWyniku"
    Me.lvOpisWyniku.Size = New System.Drawing.Size(630, 242)
    Me.lvOpisWyniku.TabIndex = 0
    Me.lvOpisWyniku.UseCompatibleStateImageBehavior = False
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(12, 289)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(732, 28)
    Me.Panel1.TabIndex = 9
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 6)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 145
    Me.Label8.Text = "Rekord:"
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
    'Panel2
    '
    Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Panel2.Controls.Add(Me.Label14)
    Me.Panel2.Controls.Add(Me.Label12)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Location = New System.Drawing.Point(12, 322)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(732, 33)
    Me.Panel2.TabIndex = 10
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(511, 9)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 169
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(368, 9)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 168
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(602, 4)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(121, 23)
    Me.lblData.TabIndex = 167
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(405, 4)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 165
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 4)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(279, 23)
    Me.lblUser.TabIndex = 166
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(3, 9)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 164
    Me.Label3.Text = "Zmodyfikował"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(648, 249)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(90, 34)
    Me.cmdClose.TabIndex = 8
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
    Me.cmdDelete.Location = New System.Drawing.Point(648, 121)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(90, 34)
    Me.cmdDelete.TabIndex = 7
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.Location = New System.Drawing.Point(648, 41)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(90, 34)
    Me.cmdAddNew.TabIndex = 5
    Me.cmdAddNew.Text = "&Dodaj"
    Me.cmdAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.Location = New System.Drawing.Point(648, 81)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(90, 34)
    Me.cmdEdit.TabIndex = 23
    Me.cmdEdit.Text = "&Edytuj"
    Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cbPrzedmiot
    '
    Me.cbPrzedmiot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPrzedmiot.Enabled = False
    Me.cbPrzedmiot.FormattingEnabled = True
    Me.cbPrzedmiot.Location = New System.Drawing.Point(69, 12)
    Me.cbPrzedmiot.Name = "cbPrzedmiot"
    Me.cbPrzedmiot.Size = New System.Drawing.Size(297, 21)
    Me.cbPrzedmiot.TabIndex = 197
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(10, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(53, 13)
    Me.Label2.TabIndex = 196
    Me.Label2.Text = "Przedmiot"
    '
    'frmOpisWyniku
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(747, 367)
    Me.Controls.Add(Me.cbPrzedmiot)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.lvOpisWyniku)
    Me.Name = "frmOpisWyniku"
    Me.Text = "Opisy wyników cząstkowych"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lvOpisWyniku As System.Windows.Forms.ListView
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
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
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cbPrzedmiot As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
