<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKlasyfikacja
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
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.rbRokSzkolny = New System.Windows.Forms.RadioButton()
    Me.rbSemestr = New System.Windows.Forms.RadioButton()
    Me.lblObsadaFilter = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.cmdSend = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.lblWychowawca = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(890, 165)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 35)
    Me.cmdPrint.TabIndex = 125
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(888, 512)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(119, 35)
    Me.cmdClose.TabIndex = 124
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'rbRokSzkolny
    '
    Me.rbRokSzkolny.AutoSize = True
    Me.rbRokSzkolny.Location = New System.Drawing.Point(798, 13)
    Me.rbRokSzkolny.Name = "rbRokSzkolny"
    Me.rbRokSzkolny.Size = New System.Drawing.Size(82, 17)
    Me.rbRokSzkolny.TabIndex = 206
    Me.rbRokSzkolny.TabStop = True
    Me.rbRokSzkolny.Tag = "R"
    Me.rbRokSzkolny.Text = "RokSzkolny"
    Me.rbRokSzkolny.UseVisualStyleBackColor = True
    '
    'rbSemestr
    '
    Me.rbSemestr.AutoSize = True
    Me.rbSemestr.Location = New System.Drawing.Point(723, 13)
    Me.rbSemestr.Name = "rbSemestr"
    Me.rbSemestr.Size = New System.Drawing.Size(69, 17)
    Me.rbSemestr.TabIndex = 205
    Me.rbSemestr.TabStop = True
    Me.rbSemestr.Tag = "S"
    Me.rbSemestr.Text = "Semestr I"
    Me.rbSemestr.UseVisualStyleBackColor = True
    '
    'lblObsadaFilter
    '
    Me.lblObsadaFilter.AutoSize = True
    Me.lblObsadaFilter.Location = New System.Drawing.Point(12, 15)
    Me.lblObsadaFilter.Name = "lblObsadaFilter"
    Me.lblObsadaFilter.Size = New System.Drawing.Size(33, 13)
    Me.lblObsadaFilter.TabIndex = 204
    Me.lblObsadaFilter.Text = "Klasa"
    Me.lblObsadaFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(51, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(238, 21)
    Me.cbKlasa.TabIndex = 203
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.Location = New System.Drawing.Point(890, 39)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddNew.TabIndex = 207
    Me.cmdAddNew.Text = "Dodaj"
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'cmdSend
    '
    Me.cmdSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSend.Enabled = False
    Me.cmdSend.Image = Global.belfer.NET.My.Resources.Resources.send_24
    Me.cmdSend.Location = New System.Drawing.Point(890, 81)
    Me.cmdSend.Name = "cmdSend"
    Me.cmdSend.Size = New System.Drawing.Size(117, 36)
    Me.cmdSend.TabIndex = 208
    Me.cmdSend.Text = "Przekaż"
    Me.cmdSend.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdSend.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(890, 123)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 209
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'lblWychowawca
    '
    Me.lblWychowawca.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblWychowawca.ForeColor = System.Drawing.Color.Green
    Me.lblWychowawca.Location = New System.Drawing.Point(295, 10)
    Me.lblWychowawca.Name = "lblWychowawca"
    Me.lblWychowawca.Size = New System.Drawing.Size(422, 23)
    Me.lblWychowawca.TabIndex = 212
    Me.lblWychowawca.Text = "lblWychowawca"
    Me.lblWychowawca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'frmKlasyfikacja
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1015, 559)
    Me.Controls.Add(Me.lblWychowawca)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdSend)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.rbRokSzkolny)
    Me.Controls.Add(Me.rbSemestr)
    Me.Controls.Add(Me.lblObsadaFilter)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Name = "frmKlasyfikacja"
    Me.Text = "Wyniki klasyfikacji"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents rbRokSzkolny As System.Windows.Forms.RadioButton
  Friend WithEvents rbSemestr As System.Windows.Forms.RadioButton
  Friend WithEvents lblObsadaFilter As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdSend As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents lblWychowawca As System.Windows.Forms.Label
End Class
