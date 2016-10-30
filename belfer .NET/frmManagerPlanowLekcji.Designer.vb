<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManagerPlanowLekcji
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
    Me.lvPlan = New System.Windows.Forms.ListView()
    Me.lvLekcja = New System.Windows.Forms.ListView()
    Me.cbLekcjaFilter = New System.Windows.Forms.ComboBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.chkScalLekcje = New System.Windows.Forms.CheckBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.chkActive = New System.Windows.Forms.CheckBox()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.lblRecord0 = New System.Windows.Forms.Label()
    Me.rbKlasa = New System.Windows.Forms.RadioButton()
    Me.rbNauczyciel = New System.Windows.Forms.RadioButton()
    Me.cmdKopiuj = New System.Windows.Forms.Button()
    Me.cmdExport = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdEditPlan = New System.Windows.Forms.Button()
    Me.cmdAddPlan = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvPlan
    '
    Me.lvPlan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvPlan.Location = New System.Drawing.Point(12, 12)
    Me.lvPlan.Name = "lvPlan"
    Me.lvPlan.Size = New System.Drawing.Size(798, 130)
    Me.lvPlan.TabIndex = 0
    Me.lvPlan.UseCompatibleStateImageBehavior = False
    '
    'lvLekcja
    '
    Me.lvLekcja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvLekcja.Location = New System.Drawing.Point(12, 175)
    Me.lvLekcja.Name = "lvLekcja"
    Me.lvLekcja.Size = New System.Drawing.Size(798, 301)
    Me.lvLekcja.TabIndex = 1
    Me.lvLekcja.UseCompatibleStateImageBehavior = False
    '
    'cbLekcjaFilter
    '
    Me.cbLekcjaFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbLekcjaFilter.FormattingEnabled = True
    Me.cbLekcjaFilter.Location = New System.Drawing.Point(529, 148)
    Me.cbLekcjaFilter.Name = "cbLekcjaFilter"
    Me.cbLekcjaFilter.Size = New System.Drawing.Size(281, 21)
    Me.cbLekcjaFilter.TabIndex = 116
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.chkScalLekcje)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Controls.Add(Me.chkActive)
    Me.Panel1.Location = New System.Drawing.Point(12, 478)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(927, 26)
    Me.Panel1.TabIndex = 202
    '
    'chkScalLekcje
    '
    Me.chkScalLekcje.AutoSize = True
    Me.chkScalLekcje.Checked = True
    Me.chkScalLekcje.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkScalLekcje.Enabled = False
    Me.chkScalLekcje.Location = New System.Drawing.Point(619, 4)
    Me.chkScalLekcje.Name = "chkScalLekcje"
    Me.chkScalLekcje.Size = New System.Drawing.Size(179, 17)
    Me.chkScalLekcje.TabIndex = 212
    Me.chkScalLekcje.Text = "Scal lekcje na tej samej godzinie"
    Me.chkScalLekcje.UseVisualStyleBackColor = True
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 5)
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
    Me.lblRecord.Location = New System.Drawing.Point(54, 5)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 144
    Me.lblRecord.Text = "lblRecord"
    '
    'chkActive
    '
    Me.chkActive.AutoSize = True
    Me.chkActive.Checked = True
    Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkActive.Location = New System.Drawing.Point(490, 4)
    Me.chkActive.Name = "chkActive"
    Me.chkActive.Size = New System.Drawing.Size(123, 17)
    Me.chkActive.TabIndex = 209
    Me.chkActive.Text = "Pokaż tylko aktywny"
    Me.chkActive.UseVisualStyleBackColor = True
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
    Me.Panel2.Location = New System.Drawing.Point(12, 510)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(927, 29)
    Me.Panel2.TabIndex = 204
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(703, 8)
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
    Me.Label12.Location = New System.Drawing.Point(560, 8)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 146
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(794, 3)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(121, 23)
    Me.lblData.TabIndex = 145
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(597, 3)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 143
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 3)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(471, 23)
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
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 151)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(45, 13)
    Me.Label4.TabIndex = 210
    Me.Label4.Text = "Rekord:"
    '
    'lblRecord0
    '
    Me.lblRecord0.AutoSize = True
    Me.lblRecord0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord0.ForeColor = System.Drawing.Color.Red
    Me.lblRecord0.Location = New System.Drawing.Point(59, 151)
    Me.lblRecord0.Name = "lblRecord0"
    Me.lblRecord0.Size = New System.Drawing.Size(68, 13)
    Me.lblRecord0.TabIndex = 211
    Me.lblRecord0.Text = "lblRecord0"
    '
    'rbKlasa
    '
    Me.rbKlasa.AutoSize = True
    Me.rbKlasa.Checked = True
    Me.rbKlasa.Location = New System.Drawing.Point(472, 149)
    Me.rbKlasa.Name = "rbKlasa"
    Me.rbKlasa.Size = New System.Drawing.Size(51, 17)
    Me.rbKlasa.TabIndex = 212
    Me.rbKlasa.TabStop = True
    Me.rbKlasa.Tag = "Klasa"
    Me.rbKlasa.Text = "Klasa"
    Me.rbKlasa.UseVisualStyleBackColor = True
    '
    'rbNauczyciel
    '
    Me.rbNauczyciel.AutoSize = True
    Me.rbNauczyciel.Location = New System.Drawing.Point(389, 149)
    Me.rbNauczyciel.Name = "rbNauczyciel"
    Me.rbNauczyciel.Size = New System.Drawing.Size(77, 17)
    Me.rbNauczyciel.TabIndex = 213
    Me.rbNauczyciel.Tag = "Nauczyciel"
    Me.rbNauczyciel.Text = "Nauczyciel"
    Me.rbNauczyciel.UseVisualStyleBackColor = True
    '
    'cmdKopiuj
    '
    Me.cmdKopiuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdKopiuj.Enabled = False
    Me.cmdKopiuj.Image = Global.belfer.NET.My.Resources.Resources.copy24
    Me.cmdKopiuj.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdKopiuj.Location = New System.Drawing.Point(822, 259)
    Me.cmdKopiuj.Name = "cmdKopiuj"
    Me.cmdKopiuj.Size = New System.Drawing.Size(117, 36)
    Me.cmdKopiuj.TabIndex = 214
    Me.cmdKopiuj.Text = "Kopiuj plan"
    Me.cmdKopiuj.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdKopiuj.UseVisualStyleBackColor = True
    '
    'cmdExport
    '
    Me.cmdExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdExport.Enabled = False
    Me.cmdExport.Image = Global.belfer.NET.My.Resources.Resources.html
    Me.cmdExport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdExport.Location = New System.Drawing.Point(822, 217)
    Me.cmdExport.Name = "cmdExport"
    Me.cmdExport.Size = New System.Drawing.Size(117, 36)
    Me.cmdExport.TabIndex = 207
    Me.cmdExport.Text = "Export do HTML"
    Me.cmdExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdExport.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.Location = New System.Drawing.Point(822, 175)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 36)
    Me.cmdPrint.TabIndex = 208
    Me.cmdPrint.Text = "Drukuj"
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdPrint.UseVisualStyleBackColor = True
    '
    'cmdEditPlan
    '
    Me.cmdEditPlan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEditPlan.Enabled = False
    Me.cmdEditPlan.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEditPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEditPlan.Location = New System.Drawing.Point(822, 54)
    Me.cmdEditPlan.Name = "cmdEditPlan"
    Me.cmdEditPlan.Size = New System.Drawing.Size(117, 36)
    Me.cmdEditPlan.TabIndex = 112
    Me.cmdEditPlan.Text = "Edytuj parametry planu"
    Me.cmdEditPlan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEditPlan.UseVisualStyleBackColor = True
    '
    'cmdAddPlan
    '
    Me.cmdAddPlan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddPlan.Enabled = False
    Me.cmdAddPlan.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddPlan.Location = New System.Drawing.Point(822, 12)
    Me.cmdAddPlan.Name = "cmdAddPlan"
    Me.cmdAddPlan.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddPlan.TabIndex = 113
    Me.cmdAddPlan.Text = "Nowy plan"
    Me.cmdAddPlan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddPlan.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.Location = New System.Drawing.Point(822, 96)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 111
    Me.cmdDelete.Text = "&Usuń plan"
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(822, 441)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 110
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'frmManagerPlanowLekcji
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(951, 547)
    Me.Controls.Add(Me.cmdKopiuj)
    Me.Controls.Add(Me.rbNauczyciel)
    Me.Controls.Add(Me.rbKlasa)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.lblRecord0)
    Me.Controls.Add(Me.cmdExport)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cbLekcjaFilter)
    Me.Controls.Add(Me.cmdEditPlan)
    Me.Controls.Add(Me.cmdAddPlan)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.lvLekcja)
    Me.Controls.Add(Me.lvPlan)
    Me.Name = "frmManagerPlanowLekcji"
    Me.Text = "Plan lekcji"
    Me.Panel1.ResumeLayout(false)
    Me.Panel1.PerformLayout
    Me.Panel2.ResumeLayout(false)
    Me.Panel2.PerformLayout
    Me.ResumeLayout(false)
    Me.PerformLayout

End Sub
  Friend WithEvents lvPlan As System.Windows.Forms.ListView
  Friend WithEvents lvLekcja As System.Windows.Forms.ListView
  Friend WithEvents cmdEditPlan As System.Windows.Forms.Button
  Friend WithEvents cmdAddPlan As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cbLekcjaFilter As System.Windows.Forms.ComboBox
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
  Friend WithEvents cmdExport As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents chkActive As System.Windows.Forms.CheckBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents lblRecord0 As System.Windows.Forms.Label
  Friend WithEvents chkScalLekcje As System.Windows.Forms.CheckBox
  Friend WithEvents rbKlasa As System.Windows.Forms.RadioButton
  Friend WithEvents rbNauczyciel As System.Windows.Forms.RadioButton
  Friend WithEvents cmdKopiuj As System.Windows.Forms.Button
End Class
