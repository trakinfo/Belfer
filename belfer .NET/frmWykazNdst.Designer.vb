<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWykazNdst
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
    Me.lblObsadaFilter = New System.Windows.Forms.Label()
    Me.cbNauczyciel = New System.Windows.Forms.ComboBox()
    Me.lvStudent = New System.Windows.Forms.ListView()
    Me.rbSemestr = New System.Windows.Forms.RadioButton()
    Me.rbRokSzkolny = New System.Windows.Forms.RadioButton()
    Me.cmdSend = New System.Windows.Forms.Button()
    Me.cmdExport = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.plFiltr = New System.Windows.Forms.Panel()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.cbSeek = New System.Windows.Forms.ComboBox()
    Me.lblFilter = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.plFiltr.SuspendLayout()
    Me.SuspendLayout()
    '
    'lblObsadaFilter
    '
    Me.lblObsadaFilter.Location = New System.Drawing.Point(12, 15)
    Me.lblObsadaFilter.Name = "lblObsadaFilter"
    Me.lblObsadaFilter.Size = New System.Drawing.Size(59, 13)
    Me.lblObsadaFilter.TabIndex = 189
    Me.lblObsadaFilter.Text = "Nauczyciel"
    Me.lblObsadaFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cbNauczyciel
    '
    Me.cbNauczyciel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbNauczyciel.Enabled = False
    Me.cbNauczyciel.FormattingEnabled = True
    Me.cbNauczyciel.Location = New System.Drawing.Point(77, 12)
    Me.cbNauczyciel.Name = "cbNauczyciel"
    Me.cbNauczyciel.Size = New System.Drawing.Size(336, 21)
    Me.cbNauczyciel.TabIndex = 188
    '
    'lvStudent
    '
    Me.lvStudent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lvStudent.Location = New System.Drawing.Point(12, 39)
    Me.lvStudent.Name = "lvStudent"
    Me.lvStudent.Size = New System.Drawing.Size(825, 478)
    Me.lvStudent.TabIndex = 192
    Me.lvStudent.UseCompatibleStateImageBehavior = False
    '
    'rbSemestr
    '
    Me.rbSemestr.AutoSize = True
    Me.rbSemestr.Location = New System.Drawing.Point(672, 13)
    Me.rbSemestr.Name = "rbSemestr"
    Me.rbSemestr.Size = New System.Drawing.Size(69, 17)
    Me.rbSemestr.TabIndex = 201
    Me.rbSemestr.TabStop = True
    Me.rbSemestr.Tag = "S"
    Me.rbSemestr.Text = "Semestr I"
    Me.rbSemestr.UseVisualStyleBackColor = True
    '
    'rbRokSzkolny
    '
    Me.rbRokSzkolny.AutoSize = True
    Me.rbRokSzkolny.Location = New System.Drawing.Point(747, 13)
    Me.rbRokSzkolny.Name = "rbRokSzkolny"
    Me.rbRokSzkolny.Size = New System.Drawing.Size(82, 17)
    Me.rbRokSzkolny.TabIndex = 202
    Me.rbRokSzkolny.TabStop = True
    Me.rbRokSzkolny.Tag = "R"
    Me.rbRokSzkolny.Text = "RokSzkolny"
    Me.rbRokSzkolny.UseVisualStyleBackColor = True
    '
    'cmdSend
    '
    Me.cmdSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSend.Enabled = False
    Me.cmdSend.Image = Global.belfer.NET.My.Resources.Resources.send_24
    Me.cmdSend.Location = New System.Drawing.Point(843, 123)
    Me.cmdSend.Name = "cmdSend"
    Me.cmdSend.Size = New System.Drawing.Size(117, 36)
    Me.cmdSend.TabIndex = 200
    Me.cmdSend.Text = "Przekaż uzasad."
    Me.cmdSend.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdSend.UseVisualStyleBackColor = True
    '
    'cmdExport
    '
    Me.cmdExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdExport.Enabled = False
    Me.cmdExport.Image = Global.belfer.NET.My.Resources.Resources.odt_24
    Me.cmdExport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdExport.Location = New System.Drawing.Point(843, 207)
    Me.cmdExport.Name = "cmdExport"
    Me.cmdExport.Size = New System.Drawing.Size(117, 36)
    Me.cmdExport.TabIndex = 199
    Me.cmdExport.Text = "Exportuj do .odt"
    Me.cmdExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdExport.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.Location = New System.Drawing.Point(843, 81)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(117, 36)
    Me.cmdEdit.TabIndex = 197
    Me.cmdEdit.Text = "Edytuj uzasad."
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.Location = New System.Drawing.Point(843, 39)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddNew.TabIndex = 198
    Me.cmdAddNew.Text = "Dodaj uzasad."
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(843, 165)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 196
    Me.cmdDelete.Text = "&Usuń uzasad."
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(843, 249)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 35)
    Me.cmdPrint.TabIndex = 194
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(843, 515)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 193
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'plFiltr
    '
    Me.plFiltr.Controls.Add(Me.txtSeek)
    Me.plFiltr.Controls.Add(Me.cbSeek)
    Me.plFiltr.Controls.Add(Me.lblFilter)
    Me.plFiltr.Controls.Add(Me.Label8)
    Me.plFiltr.Controls.Add(Me.lblRecord)
    Me.plFiltr.Location = New System.Drawing.Point(12, 519)
    Me.plFiltr.Name = "plFiltr"
    Me.plFiltr.Size = New System.Drawing.Size(825, 31)
    Me.plFiltr.TabIndex = 219
    '
    'txtSeek
    '
    Me.txtSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeek.Location = New System.Drawing.Point(503, 4)
    Me.txtSeek.Name = "txtSeek"
    Me.txtSeek.Size = New System.Drawing.Size(319, 20)
    Me.txtSeek.TabIndex = 223
    '
    'cbSeek
    '
    Me.cbSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbSeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeek.DropDownWidth = 200
    Me.cbSeek.FormattingEnabled = True
    Me.cbSeek.Location = New System.Drawing.Point(297, 4)
    Me.cbSeek.Name = "cbSeek"
    Me.cbSeek.Size = New System.Drawing.Size(200, 21)
    Me.cbSeek.TabIndex = 222
    '
    'lblFilter
    '
    Me.lblFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFilter.AutoSize = True
    Me.lblFilter.Location = New System.Drawing.Point(243, 7)
    Me.lblFilter.Name = "lblFilter"
    Me.lblFilter.Size = New System.Drawing.Size(48, 13)
    Me.lblFilter.TabIndex = 221
    Me.lblFilter.Text = "Filtruj wg"
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 7)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 219
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 7)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 220
    Me.lblRecord.Text = "lblRecord"
    '
    'frmWykazNdst
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(968, 562)
    Me.Controls.Add(Me.plFiltr)
    Me.Controls.Add(Me.rbRokSzkolny)
    Me.Controls.Add(Me.rbSemestr)
    Me.Controls.Add(Me.cmdSend)
    Me.Controls.Add(Me.cmdExport)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.lvStudent)
    Me.Controls.Add(Me.lblObsadaFilter)
    Me.Controls.Add(Me.cbNauczyciel)
    Me.Name = "frmWykazNdst"
    Me.Text = "Wykaz uczniów z ocenami niedostatecznymi"
    Me.plFiltr.ResumeLayout(False)
    Me.plFiltr.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lblObsadaFilter As System.Windows.Forms.Label
  Friend WithEvents cbNauczyciel As System.Windows.Forms.ComboBox
  Friend WithEvents lvStudent As System.Windows.Forms.ListView
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdExport As System.Windows.Forms.Button
  Friend WithEvents cmdSend As System.Windows.Forms.Button
  Friend WithEvents rbSemestr As System.Windows.Forms.RadioButton
  Friend WithEvents rbRokSzkolny As System.Windows.Forms.RadioButton
  Friend WithEvents plFiltr As System.Windows.Forms.Panel
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents cbSeek As System.Windows.Forms.ComboBox
  Friend WithEvents lblFilter As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
End Class
