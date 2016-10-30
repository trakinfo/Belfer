<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStudentPoprawa
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
    Me.rbPrzedmiot = New System.Windows.Forms.RadioButton()
    Me.rbStudent = New System.Windows.Forms.RadioButton()
    Me.cbFilter = New System.Windows.Forms.ComboBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.nudSemestr = New System.Windows.Forms.NumericUpDown()
    Me.lvPoprawa = New System.Windows.Forms.ListView()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.chkZakres = New System.Windows.Forms.CheckBox()
    CType(Me.nudSemestr,System.ComponentModel.ISupportInitialize).BeginInit
    Me.Panel1.SuspendLayout
    Me.Panel2.SuspendLayout
    Me.SuspendLayout
    '
    'rbPrzedmiot
    '
    Me.rbPrzedmiot.AutoSize = true
    Me.rbPrzedmiot.Checked = true
    Me.rbPrzedmiot.Location = New System.Drawing.Point(12, 13)
    Me.rbPrzedmiot.Name = "rbPrzedmiot"
    Me.rbPrzedmiot.Size = New System.Drawing.Size(71, 17)
    Me.rbPrzedmiot.TabIndex = 0
    Me.rbPrzedmiot.TabStop = true
    Me.rbPrzedmiot.Tag = ""
    Me.rbPrzedmiot.Text = "Przedmiot"
    Me.rbPrzedmiot.UseVisualStyleBackColor = true
    '
    'rbStudent
    '
    Me.rbStudent.AutoSize = true
    Me.rbStudent.Location = New System.Drawing.Point(89, 13)
    Me.rbStudent.Name = "rbStudent"
    Me.rbStudent.Size = New System.Drawing.Size(56, 17)
    Me.rbStudent.TabIndex = 1
    Me.rbStudent.Tag = ""
    Me.rbStudent.Text = "Uczeń"
    Me.rbStudent.UseVisualStyleBackColor = true
    '
    'cbFilter
    '
    Me.cbFilter.DropDownHeight = 500
    Me.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbFilter.Enabled = false
    Me.cbFilter.FormattingEnabled = true
    Me.cbFilter.IntegralHeight = false
    Me.cbFilter.Location = New System.Drawing.Point(151, 12)
    Me.cbFilter.Name = "cbFilter"
    Me.cbFilter.Size = New System.Drawing.Size(311, 21)
    Me.cbFilter.TabIndex = 25
    '
    'Label8
    '
    Me.Label8.AutoSize = true
    Me.Label8.Location = New System.Drawing.Point(593, 15)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 38
    Me.Label8.Text = "Semestr"
    '
    'nudSemestr
    '
    Me.nudSemestr.Location = New System.Drawing.Point(644, 13)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(34, 20)
    Me.nudSemestr.TabIndex = 39
    Me.nudSemestr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'lvPoprawa
    '
    Me.lvPoprawa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.lvPoprawa.Location = New System.Drawing.Point(12, 39)
    Me.lvPoprawa.Name = "lvPoprawa"
    Me.lvPoprawa.Size = New System.Drawing.Size(666, 330)
    Me.lvPoprawa.TabIndex = 40
    Me.lvPoprawa.UseCompatibleStateImageBehavior = false
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(12, 375)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(788, 30)
    Me.Panel1.TabIndex = 204
    '
    'Label1
    '
    Me.Label1.AutoSize = true
    Me.Label1.Location = New System.Drawing.Point(3, 6)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(45, 13)
    Me.Label1.TabIndex = 143
    Me.Label1.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.AutoSize = true
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 6)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 144
    Me.lblRecord.Text = "lblRecord"
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
    Me.Panel2.Controls.Add(Me.Label14)
    Me.Panel2.Controls.Add(Me.Label12)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Location = New System.Drawing.Point(12, 411)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(789, 29)
    Me.Panel2.TabIndex = 206
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = true
    Me.Label14.Enabled = false
    Me.Label14.Location = New System.Drawing.Point(569, 8)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 147
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = true
    Me.Label12.Enabled = false
    Me.Label12.Location = New System.Drawing.Point(406, 8)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 146
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = false
    Me.lblData.Location = New System.Drawing.Point(660, 3)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(121, 23)
    Me.lblData.TabIndex = 145
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = false
    Me.lblIP.Location = New System.Drawing.Point(443, 3)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(120, 23)
    Me.lblIP.TabIndex = 143
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = false
    Me.lblUser.Location = New System.Drawing.Point(83, 3)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(317, 23)
    Me.lblUser.TabIndex = 144
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = true
    Me.Label3.Enabled = false
    Me.Label3.Location = New System.Drawing.Point(3, 8)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 142
    Me.Label3.Text = "Zmodyfikował"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(684, 334)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 222
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = true
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.Location = New System.Drawing.Point(684, 39)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddNew.TabIndex = 221
    Me.cmdAddNew.Text = "&Dodaj"
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = true
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = false
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(684, 81)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 219
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = true
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = false
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.Location = New System.Drawing.Point(684, 292)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 36)
    Me.cmdPrint.TabIndex = 223
    Me.cmdPrint.Text = "Drukuj"
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdPrint.UseVisualStyleBackColor = true
    '
    'chkZakres
    '
    Me.chkZakres.AutoSize = true
    Me.chkZakres.Location = New System.Drawing.Point(468, 14)
    Me.chkZakres.Name = "chkZakres"
    Me.chkZakres.Size = New System.Drawing.Size(116, 17)
    Me.chkZakres.TabIndex = 224
    Me.chkZakres.Tag = "Wybrany "
    Me.chkZakres.Text = "Wybrany przedmiot"
    Me.chkZakres.UseVisualStyleBackColor = true
    '
    'frmStudentPoprawa
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(812, 450)
    Me.Controls.Add(Me.chkZakres)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.lvPoprawa)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.nudSemestr)
    Me.Controls.Add(Me.cbFilter)
    Me.Controls.Add(Me.rbStudent)
    Me.Controls.Add(Me.rbPrzedmiot)
    Me.Name = "frmStudentPoprawa"
    Me.Text = "Uczniowie dopuszczeni do egzaminu poprawkowego"
    CType(Me.nudSemestr,System.ComponentModel.ISupportInitialize).EndInit
    Me.Panel1.ResumeLayout(false)
    Me.Panel1.PerformLayout
    Me.Panel2.ResumeLayout(false)
    Me.Panel2.PerformLayout
    Me.ResumeLayout(false)
    Me.PerformLayout

End Sub
  Friend WithEvents rbPrzedmiot As System.Windows.Forms.RadioButton
  Friend WithEvents rbStudent As System.Windows.Forms.RadioButton
  Friend WithEvents cbFilter As System.Windows.Forms.ComboBox
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
  Friend WithEvents lvPoprawa As System.Windows.Forms.ListView
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents chkZakres As System.Windows.Forms.CheckBox
End Class
