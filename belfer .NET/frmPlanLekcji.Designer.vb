<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlanLekcji
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
    Me.lvLekcja = New System.Windows.Forms.ListView()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cbLekcjaFilter = New System.Windows.Forms.ComboBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.chkScalLekcje = New System.Windows.Forms.CheckBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdEditActivity = New System.Windows.Forms.Button()
    Me.cmdAddActivity = New System.Windows.Forms.Button()
    Me.cmdDeleteActivity = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.cbPlan = New System.Windows.Forms.ComboBox()
    Me.rbNauczyciel = New System.Windows.Forms.RadioButton()
    Me.rbKlasa = New System.Windows.Forms.RadioButton()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvLekcja
    '
    Me.lvLekcja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvLekcja.Enabled = False
    Me.lvLekcja.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
    Me.lvLekcja.Location = New System.Drawing.Point(12, 39)
    Me.lvLekcja.Name = "lvLekcja"
    Me.lvLekcja.Size = New System.Drawing.Size(798, 437)
    Me.lvLekcja.TabIndex = 1
    Me.lvLekcja.UseCompatibleStateImageBehavior = False
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(816, 441)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 110
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cbLekcjaFilter
    '
    Me.cbLekcjaFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbLekcjaFilter.Enabled = False
    Me.cbLekcjaFilter.FormattingEnabled = True
    Me.cbLekcjaFilter.Location = New System.Drawing.Point(554, 12)
    Me.cbLekcjaFilter.Name = "cbLekcjaFilter"
    Me.cbLekcjaFilter.Size = New System.Drawing.Size(256, 21)
    Me.cbLekcjaFilter.TabIndex = 116
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.chkScalLekcje)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(12, 478)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(921, 26)
    Me.Panel1.TabIndex = 202
    '
    'chkScalLekcje
    '
    Me.chkScalLekcje.AutoSize = True
    Me.chkScalLekcje.Checked = True
    Me.chkScalLekcje.CheckState = System.Windows.Forms.CheckState.Checked
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
    Me.Panel2.Size = New System.Drawing.Size(921, 29)
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
    'cmdEditActivity
    '
    Me.cmdEditActivity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEditActivity.Enabled = False
    Me.cmdEditActivity.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEditActivity.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEditActivity.Location = New System.Drawing.Point(816, 81)
    Me.cmdEditActivity.Name = "cmdEditActivity"
    Me.cmdEditActivity.Size = New System.Drawing.Size(117, 36)
    Me.cmdEditActivity.TabIndex = 207
    Me.cmdEditActivity.Text = "Edytuj lekcję"
    Me.cmdEditActivity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEditActivity.UseVisualStyleBackColor = True
    '
    'cmdAddActivity
    '
    Me.cmdAddActivity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddActivity.Enabled = False
    Me.cmdAddActivity.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddActivity.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddActivity.Location = New System.Drawing.Point(816, 39)
    Me.cmdAddActivity.Name = "cmdAddActivity"
    Me.cmdAddActivity.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddActivity.TabIndex = 208
    Me.cmdAddActivity.Text = "Nowa lekcja"
    Me.cmdAddActivity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddActivity.UseVisualStyleBackColor = True
    '
    'cmdDeleteActivity
    '
    Me.cmdDeleteActivity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDeleteActivity.Enabled = False
    Me.cmdDeleteActivity.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDeleteActivity.Location = New System.Drawing.Point(816, 123)
    Me.cmdDeleteActivity.Name = "cmdDeleteActivity"
    Me.cmdDeleteActivity.Size = New System.Drawing.Size(117, 36)
    Me.cmdDeleteActivity.TabIndex = 206
    Me.cmdDeleteActivity.Text = "&Usuń lekcję"
    Me.cmdDeleteActivity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDeleteActivity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDeleteActivity.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(9, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(28, 13)
    Me.Label1.TabIndex = 209
    Me.Label1.Text = "Plan"
    '
    'cbPlan
    '
    Me.cbPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPlan.FormattingEnabled = True
    Me.cbPlan.Location = New System.Drawing.Point(43, 12)
    Me.cbPlan.Name = "cbPlan"
    Me.cbPlan.Size = New System.Drawing.Size(365, 21)
    Me.cbPlan.TabIndex = 210
    '
    'rbNauczyciel
    '
    Me.rbNauczyciel.AutoSize = True
    Me.rbNauczyciel.Checked = True
    Me.rbNauczyciel.Enabled = False
    Me.rbNauczyciel.Location = New System.Drawing.Point(414, 13)
    Me.rbNauczyciel.Name = "rbNauczyciel"
    Me.rbNauczyciel.Size = New System.Drawing.Size(77, 17)
    Me.rbNauczyciel.TabIndex = 215
    Me.rbNauczyciel.TabStop = True
    Me.rbNauczyciel.Tag = "Belfer"
    Me.rbNauczyciel.Text = "Nauczyciel"
    Me.rbNauczyciel.UseVisualStyleBackColor = True
    '
    'rbKlasa
    '
    Me.rbKlasa.AutoSize = True
    Me.rbKlasa.Enabled = False
    Me.rbKlasa.Location = New System.Drawing.Point(497, 13)
    Me.rbKlasa.Name = "rbKlasa"
    Me.rbKlasa.Size = New System.Drawing.Size(51, 17)
    Me.rbKlasa.TabIndex = 214
    Me.rbKlasa.Tag = "Klasa"
    Me.rbKlasa.Text = "Klasa"
    Me.rbKlasa.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.Location = New System.Drawing.Point(816, 399)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 36)
    Me.cmdPrint.TabIndex = 216
    Me.cmdPrint.Text = "Drukuj"
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdPrint.UseVisualStyleBackColor = True
    '
    'frmPlanLekcji
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(945, 547)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.rbNauczyciel)
    Me.Controls.Add(Me.rbKlasa)
    Me.Controls.Add(Me.cbPlan)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cmdEditActivity)
    Me.Controls.Add(Me.cmdAddActivity)
    Me.Controls.Add(Me.cmdDeleteActivity)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cbLekcjaFilter)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.lvLekcja)
    Me.Name = "frmPlanLekcji"
    Me.Text = "Plan lekcji"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lvLekcja As System.Windows.Forms.ListView
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
  Friend WithEvents cmdEditActivity As System.Windows.Forms.Button
  Friend WithEvents cmdAddActivity As System.Windows.Forms.Button
  Friend WithEvents cmdDeleteActivity As System.Windows.Forms.Button
  Friend WithEvents chkScalLekcje As System.Windows.Forms.CheckBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents cbPlan As System.Windows.Forms.ComboBox
  Friend WithEvents rbNauczyciel As System.Windows.Forms.RadioButton
  Friend WithEvents rbKlasa As System.Windows.Forms.RadioButton
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
End Class
