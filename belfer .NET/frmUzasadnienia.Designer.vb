<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUzasadnienia
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
    Me.rbSemestr = New System.Windows.Forms.RadioButton()
    Me.rbRokSzkolny = New System.Windows.Forms.RadioButton()
    Me.cmdReject = New System.Windows.Forms.Button()
    Me.cmdAccept = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.txtUzasadnienie = New System.Windows.Forms.TextBox()
    Me.rbSubmitted = New System.Windows.Forms.RadioButton()
    Me.rbAccepted = New System.Windows.Forms.RadioButton()
    Me.rbRejected = New System.Windows.Forms.RadioButton()
    Me.gbStatus = New System.Windows.Forms.GroupBox()
    Me.rbAll = New System.Windows.Forms.RadioButton()
    Me.rbMissing = New System.Windows.Forms.RadioButton()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.cbSeek = New System.Windows.Forms.ComboBox()
    Me.lblFilter = New System.Windows.Forms.Label()
    Me.gbZakres = New System.Windows.Forms.GroupBox()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.gbTyp = New System.Windows.Forms.GroupBox()
    Me.rbPrzedmiot = New System.Windows.Forms.RadioButton()
    Me.rbZachowanie = New System.Windows.Forms.RadioButton()
    Me.lvStudent = New System.Windows.Forms.ListView()
    Me.gbStatus.SuspendLayout()
    Me.gbZakres.SuspendLayout()
    Me.gbTyp.SuspendLayout()
    Me.SuspendLayout()
    '
    'rbSemestr
    '
    Me.rbSemestr.AutoSize = True
    Me.rbSemestr.Location = New System.Drawing.Point(6, 19)
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
    Me.rbRokSzkolny.Location = New System.Drawing.Point(81, 19)
    Me.rbRokSzkolny.Name = "rbRokSzkolny"
    Me.rbRokSzkolny.Size = New System.Drawing.Size(82, 17)
    Me.rbRokSzkolny.TabIndex = 202
    Me.rbRokSzkolny.TabStop = True
    Me.rbRokSzkolny.Tag = "R"
    Me.rbRokSzkolny.Text = "RokSzkolny"
    Me.rbRokSzkolny.UseVisualStyleBackColor = True
    '
    'cmdReject
    '
    Me.cmdReject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdReject.Enabled = False
    Me.cmdReject.Image = Global.belfer.NET.My.Resources.Resources.reject_24
    Me.cmdReject.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdReject.Location = New System.Drawing.Point(843, 95)
    Me.cmdReject.Name = "cmdReject"
    Me.cmdReject.Size = New System.Drawing.Size(117, 36)
    Me.cmdReject.TabIndex = 197
    Me.cmdReject.Tag = "2"
    Me.cmdReject.Text = "Odrzuć"
    Me.cmdReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdReject.UseVisualStyleBackColor = True
    '
    'cmdAccept
    '
    Me.cmdAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAccept.Enabled = False
    Me.cmdAccept.Image = Global.belfer.NET.My.Resources.Resources.accept_24
    Me.cmdAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAccept.Location = New System.Drawing.Point(843, 53)
    Me.cmdAccept.Name = "cmdAccept"
    Me.cmdAccept.Size = New System.Drawing.Size(117, 36)
    Me.cmdAccept.TabIndex = 198
    Me.cmdAccept.Tag = "3"
    Me.cmdAccept.Text = "Akceptuj"
    Me.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAccept.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(843, 220)
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
    'txtUzasadnienie
    '
    Me.txtUzasadnienie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.txtUzasadnienie.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.txtUzasadnienie.Location = New System.Drawing.Point(12, 430)
    Me.txtUzasadnienie.Multiline = True
    Me.txtUzasadnienie.Name = "txtUzasadnienie"
    Me.txtUzasadnienie.ReadOnly = True
    Me.txtUzasadnienie.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtUzasadnienie.Size = New System.Drawing.Size(825, 120)
    Me.txtUzasadnienie.TabIndex = 204
    '
    'rbSubmitted
    '
    Me.rbSubmitted.AutoSize = True
    Me.rbSubmitted.Location = New System.Drawing.Point(85, 17)
    Me.rbSubmitted.Name = "rbSubmitted"
    Me.rbSubmitted.Size = New System.Drawing.Size(81, 17)
    Me.rbSubmitted.TabIndex = 205
    Me.rbSubmitted.TabStop = True
    Me.rbSubmitted.Tag = "Status IN (1) "
    Me.rbSubmitted.Text = "Przekazane"
    Me.rbSubmitted.UseVisualStyleBackColor = True
    '
    'rbAccepted
    '
    Me.rbAccepted.AutoSize = True
    Me.rbAccepted.Location = New System.Drawing.Point(172, 17)
    Me.rbAccepted.Name = "rbAccepted"
    Me.rbAccepted.Size = New System.Drawing.Size(103, 17)
    Me.rbAccepted.TabIndex = 206
    Me.rbAccepted.TabStop = True
    Me.rbAccepted.Tag = "Status IN (3) "
    Me.rbAccepted.Text = "Zaakceptowane"
    Me.rbAccepted.UseVisualStyleBackColor = True
    '
    'rbRejected
    '
    Me.rbRejected.AutoSize = True
    Me.rbRejected.Location = New System.Drawing.Point(281, 17)
    Me.rbRejected.Name = "rbRejected"
    Me.rbRejected.Size = New System.Drawing.Size(77, 17)
    Me.rbRejected.TabIndex = 207
    Me.rbRejected.TabStop = True
    Me.rbRejected.Tag = "Status IN (2) "
    Me.rbRejected.Text = "Odrzucone"
    Me.rbRejected.UseVisualStyleBackColor = True
    '
    'gbStatus
    '
    Me.gbStatus.Controls.Add(Me.rbAll)
    Me.gbStatus.Controls.Add(Me.rbMissing)
    Me.gbStatus.Controls.Add(Me.rbSubmitted)
    Me.gbStatus.Controls.Add(Me.rbRejected)
    Me.gbStatus.Controls.Add(Me.rbAccepted)
    Me.gbStatus.Location = New System.Drawing.Point(208, 7)
    Me.gbStatus.Name = "gbStatus"
    Me.gbStatus.Size = New System.Drawing.Size(445, 40)
    Me.gbStatus.TabIndex = 208
    Me.gbStatus.TabStop = False
    Me.gbStatus.Text = "Status uzasadnienia"
    '
    'rbAll
    '
    Me.rbAll.AutoSize = True
    Me.rbAll.Location = New System.Drawing.Point(6, 17)
    Me.rbAll.Name = "rbAll"
    Me.rbAll.Size = New System.Drawing.Size(73, 17)
    Me.rbAll.TabIndex = 209
    Me.rbAll.TabStop = True
    Me.rbAll.Tag = "Status IN (1,2,3) "
    Me.rbAll.Text = "Wszystkie"
    Me.rbAll.UseVisualStyleBackColor = True
    '
    'rbMissing
    '
    Me.rbMissing.AutoSize = True
    Me.rbMissing.Location = New System.Drawing.Point(364, 17)
    Me.rbMissing.Name = "rbMissing"
    Me.rbMissing.Size = New System.Drawing.Size(73, 17)
    Me.rbMissing.TabIndex = 208
    Me.rbMissing.TabStop = True
    Me.rbMissing.Tag = "0"
    Me.rbMissing.Text = "Brakujące"
    Me.rbMissing.UseVisualStyleBackColor = True
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(12, 407)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 209
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(63, 407)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 210
    Me.lblRecord.Text = "lblRecord"
    '
    'txtSeek
    '
    Me.txtSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeek.Location = New System.Drawing.Point(518, 404)
    Me.txtSeek.Name = "txtSeek"
    Me.txtSeek.Size = New System.Drawing.Size(319, 20)
    Me.txtSeek.TabIndex = 213
    '
    'cbSeek
    '
    Me.cbSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbSeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeek.DropDownWidth = 200
    Me.cbSeek.FormattingEnabled = True
    Me.cbSeek.Location = New System.Drawing.Point(312, 403)
    Me.cbSeek.Name = "cbSeek"
    Me.cbSeek.Size = New System.Drawing.Size(200, 21)
    Me.cbSeek.TabIndex = 212
    '
    'lblFilter
    '
    Me.lblFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFilter.AutoSize = True
    Me.lblFilter.Location = New System.Drawing.Point(258, 407)
    Me.lblFilter.Name = "lblFilter"
    Me.lblFilter.Size = New System.Drawing.Size(48, 13)
    Me.lblFilter.TabIndex = 211
    Me.lblFilter.Text = "Filtruj wg"
    '
    'gbZakres
    '
    Me.gbZakres.Controls.Add(Me.rbSemestr)
    Me.gbZakres.Controls.Add(Me.rbRokSzkolny)
    Me.gbZakres.Location = New System.Drawing.Point(668, 7)
    Me.gbZakres.Name = "gbZakres"
    Me.gbZakres.Size = New System.Drawing.Size(169, 40)
    Me.gbZakres.TabIndex = 214
    Me.gbZakres.TabStop = False
    Me.gbZakres.Text = "Zakres"
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.Image = Global.belfer.NET.My.Resources.Resources.refresh_24
    Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdRefresh.Location = New System.Drawing.Point(843, 178)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(117, 36)
    Me.cmdRefresh.TabIndex = 230
    Me.cmdRefresh.Text = "Odśwież"
    Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'gbTyp
    '
    Me.gbTyp.Controls.Add(Me.rbPrzedmiot)
    Me.gbTyp.Controls.Add(Me.rbZachowanie)
    Me.gbTyp.Location = New System.Drawing.Point(12, 7)
    Me.gbTyp.Name = "gbTyp"
    Me.gbTyp.Size = New System.Drawing.Size(175, 40)
    Me.gbTyp.TabIndex = 231
    Me.gbTyp.TabStop = False
    Me.gbTyp.Text = "Typ"
    '
    'rbPrzedmiot
    '
    Me.rbPrzedmiot.AutoSize = True
    Me.rbPrzedmiot.Location = New System.Drawing.Point(6, 17)
    Me.rbPrzedmiot.Name = "rbPrzedmiot"
    Me.rbPrzedmiot.Size = New System.Drawing.Size(71, 17)
    Me.rbPrzedmiot.TabIndex = 201
    Me.rbPrzedmiot.TabStop = True
    Me.rbPrzedmiot.Tag = "P"
    Me.rbPrzedmiot.Text = "Przedmiot"
    Me.rbPrzedmiot.UseVisualStyleBackColor = True
    '
    'rbZachowanie
    '
    Me.rbZachowanie.AutoSize = True
    Me.rbZachowanie.Location = New System.Drawing.Point(85, 17)
    Me.rbZachowanie.Name = "rbZachowanie"
    Me.rbZachowanie.Size = New System.Drawing.Size(84, 17)
    Me.rbZachowanie.TabIndex = 202
    Me.rbZachowanie.TabStop = True
    Me.rbZachowanie.Tag = "Z"
    Me.rbZachowanie.Text = "Zachowanie"
    Me.rbZachowanie.UseVisualStyleBackColor = True
    '
    'lvStudent
    '
    Me.lvStudent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvStudent.Location = New System.Drawing.Point(12, 53)
    Me.lvStudent.Name = "lvStudent"
    Me.lvStudent.Size = New System.Drawing.Size(825, 345)
    Me.lvStudent.TabIndex = 232
    Me.lvStudent.UseCompatibleStateImageBehavior = False
    '
    'frmUzasadnienia
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(968, 562)
    Me.Controls.Add(Me.lvStudent)
    Me.Controls.Add(Me.gbTyp)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.gbZakres)
    Me.Controls.Add(Me.txtSeek)
    Me.Controls.Add(Me.cbSeek)
    Me.Controls.Add(Me.lblFilter)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.lblRecord)
    Me.Controls.Add(Me.txtUzasadnienie)
    Me.Controls.Add(Me.cmdReject)
    Me.Controls.Add(Me.cmdAccept)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.gbStatus)
    Me.Name = "frmUzasadnienia"
    Me.Text = "Uzasadnienia ocen"
    Me.gbStatus.ResumeLayout(False)
    Me.gbStatus.PerformLayout()
    Me.gbZakres.ResumeLayout(False)
    Me.gbZakres.PerformLayout()
    Me.gbTyp.ResumeLayout(False)
    Me.gbTyp.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdReject As System.Windows.Forms.Button
  Friend WithEvents cmdAccept As System.Windows.Forms.Button
  Friend WithEvents rbSemestr As System.Windows.Forms.RadioButton
  Friend WithEvents rbRokSzkolny As System.Windows.Forms.RadioButton
  Friend WithEvents txtUzasadnienie As System.Windows.Forms.TextBox
  Friend WithEvents rbSubmitted As System.Windows.Forms.RadioButton
  Friend WithEvents rbAccepted As System.Windows.Forms.RadioButton
  Friend WithEvents rbRejected As System.Windows.Forms.RadioButton
  Friend WithEvents gbStatus As System.Windows.Forms.GroupBox
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents cbSeek As System.Windows.Forms.ComboBox
  Friend WithEvents lblFilter As System.Windows.Forms.Label
  Friend WithEvents rbMissing As System.Windows.Forms.RadioButton
  Friend WithEvents rbAll As System.Windows.Forms.RadioButton
  Friend WithEvents gbZakres As System.Windows.Forms.GroupBox
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents gbTyp As System.Windows.Forms.GroupBox
  Friend WithEvents rbPrzedmiot As System.Windows.Forms.RadioButton
  Friend WithEvents rbZachowanie As System.Windows.Forms.RadioButton
  Friend WithEvents lvStudent As System.Windows.Forms.ListView
End Class
