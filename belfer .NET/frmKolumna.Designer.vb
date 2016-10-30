<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKolumna
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKolumna))
    Me.Label1 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.cbPrzedmiot = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.lvKolumna = New System.Windows.Forms.ListView()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
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
    Me.Label9 = New System.Windows.Forms.Label()
    Me.nudSemestr = New System.Windows.Forms.NumericUpDown()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(10, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(33, 13)
    Me.Label1.TabIndex = 193
    Me.Label1.Text = "Klasa"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(49, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(145, 21)
    Me.cbKlasa.TabIndex = 192
    '
    'cbPrzedmiot
    '
    Me.cbPrzedmiot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPrzedmiot.Enabled = False
    Me.cbPrzedmiot.FormattingEnabled = True
    Me.cbPrzedmiot.Location = New System.Drawing.Point(259, 12)
    Me.cbPrzedmiot.Name = "cbPrzedmiot"
    Me.cbPrzedmiot.Size = New System.Drawing.Size(297, 21)
    Me.cbPrzedmiot.TabIndex = 195
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(200, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(53, 13)
    Me.Label2.TabIndex = 194
    Me.Label2.Text = "Przedmiot"
    '
    'lvKolumna
    '
    Me.lvKolumna.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvKolumna.Location = New System.Drawing.Point(12, 39)
    Me.lvKolumna.Name = "lvKolumna"
    Me.lvKolumna.Size = New System.Drawing.Size(656, 442)
    Me.lvKolumna.TabIndex = 196
    Me.lvKolumna.UseCompatibleStateImageBehavior = False
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.Location = New System.Drawing.Point(677, 80)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(85, 34)
    Me.cmdEdit.TabIndex = 200
    Me.cmdEdit.Text = "&Edytuj"
    Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
    Me.cmdClose.Location = New System.Drawing.Point(677, 446)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(85, 35)
    Me.cmdClose.TabIndex = 199
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
    Me.cmdDelete.Location = New System.Drawing.Point(677, 120)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(85, 35)
    Me.cmdDelete.TabIndex = 198
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = CType(resources.GetObject("cmdAddNew.Image"), System.Drawing.Image)
    Me.cmdAddNew.Location = New System.Drawing.Point(677, 39)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(85, 35)
    Me.cmdAddNew.TabIndex = 197
    Me.cmdAddNew.Text = "&Dodaj"
    Me.cmdAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(7, 485)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(755, 25)
    Me.Panel1.TabIndex = 201
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 3)
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
    Me.lblRecord.Location = New System.Drawing.Point(54, 3)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 144
    Me.lblRecord.Text = "lblRecord"
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.Controls.Add(Me.Label14)
    Me.Panel2.Controls.Add(Me.Label12)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Location = New System.Drawing.Point(7, 514)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(752, 29)
    Me.Panel2.TabIndex = 203
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(540, 8)
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
    Me.Label12.Location = New System.Drawing.Point(398, 8)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 146
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(631, 3)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(121, 23)
    Me.lblData.TabIndex = 145
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(435, 3)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(99, 23)
    Me.lblIP.TabIndex = 143
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 3)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(309, 23)
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
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(562, 15)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(45, 13)
    Me.Label9.TabIndex = 204
    Me.Label9.Text = "Semestr"
    '
    'nudSemestr
    '
    Me.nudSemestr.Enabled = False
    Me.nudSemestr.Location = New System.Drawing.Point(613, 13)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(55, 20)
    Me.nudSemestr.TabIndex = 205
    Me.nudSemestr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'frmKolumna
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(771, 550)
    Me.Controls.Add(Me.nudSemestr)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.lvKolumna)
    Me.Controls.Add(Me.cbPrzedmiot)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cbKlasa)
    Me.Name = "frmKolumna"
    Me.Text = "Kolumny wynikowe"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents cbPrzedmiot As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents lvKolumna As System.Windows.Forms.ListView
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
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
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
End Class
