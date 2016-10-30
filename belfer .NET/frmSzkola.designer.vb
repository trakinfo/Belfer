<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSzkola
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
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.lvSzkoly = New System.Windows.Forms.ListView()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.tlpDetails = New System.Windows.Forms.TableLayoutPanel()
    Me.Label21 = New System.Windows.Forms.Label()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.lblNazwa = New System.Windows.Forms.Label()
    Me.lblFax = New System.Windows.Forms.Label()
    Me.lblEmail = New System.Windows.Forms.Label()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.lblTel = New System.Windows.Forms.Label()
    Me.Label22 = New System.Windows.Forms.Label()
    Me.Label30 = New System.Windows.Forms.Label()
    Me.Label31 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label32 = New System.Windows.Forms.Label()
    Me.cbTypSzkoly = New System.Windows.Forms.ComboBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.tlpDetails.SuspendLayout()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label11)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(12, 252)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(728, 34)
    Me.Panel1.TabIndex = 15
    '
    'Label11
    '
    Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label11.AutoSize = True
    Me.Label11.Location = New System.Drawing.Point(3, 7)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(45, 13)
    Me.Label11.TabIndex = 145
    Me.Label11.Text = "Rekord:"
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
    Me.lblRecord.TabIndex = 146
    Me.lblRecord.Text = "lblRecord"
    '
    'lvSzkoly
    '
    Me.lvSzkoly.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvSzkoly.Enabled = False
    Me.lvSzkoly.Location = New System.Drawing.Point(12, 39)
    Me.lvSzkoly.Name = "lvSzkoly"
    Me.lvSzkoly.Size = New System.Drawing.Size(647, 212)
    Me.lvSzkoly.TabIndex = 16
    Me.lvSzkoly.UseCompatibleStateImageBehavior = False
    Me.lvSzkoly.View = System.Windows.Forms.View.Details
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Location = New System.Drawing.Point(665, 223)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(75, 23)
    Me.cmdClose.TabIndex = 20
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Location = New System.Drawing.Point(665, 97)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(75, 23)
    Me.cmdDelete.TabIndex = 19
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Location = New System.Drawing.Point(665, 68)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(75, 23)
    Me.cmdEdit.TabIndex = 18
    Me.cmdEdit.Text = "&Edytuj"
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Location = New System.Drawing.Point(665, 39)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(75, 23)
    Me.cmdAddNew.TabIndex = 17
    Me.cmdAddNew.Text = "&Dodaj"
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.BackColor = System.Drawing.SystemColors.Control
    Me.Panel2.Controls.Add(Me.tlpDetails)
    Me.Panel2.Controls.Add(Me.Label30)
    Me.Panel2.Controls.Add(Me.Label31)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label32)
    Me.Panel2.Location = New System.Drawing.Point(12, 286)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(728, 108)
    Me.Panel2.TabIndex = 21
    '
    'tlpDetails
    '
    Me.tlpDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpDetails.ColumnCount = 6
    Me.tlpDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.585055!))
    Me.tlpDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.80127!))
    Me.tlpDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.405406!))
    Me.tlpDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.64706!))
    Me.tlpDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.292649!))
    Me.tlpDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.95146!))
    Me.tlpDetails.Controls.Add(Me.Label21, 4, 1)
    Me.tlpDetails.Controls.Add(Me.Label13, 0, 0)
    Me.tlpDetails.Controls.Add(Me.lblNazwa, 1, 0)
    Me.tlpDetails.Controls.Add(Me.lblFax, 3, 1)
    Me.tlpDetails.Controls.Add(Me.lblEmail, 5, 1)
    Me.tlpDetails.Controls.Add(Me.Label16, 0, 1)
    Me.tlpDetails.Controls.Add(Me.lblTel, 1, 1)
    Me.tlpDetails.Controls.Add(Me.Label22, 2, 1)
    Me.tlpDetails.Location = New System.Drawing.Point(4, 6)
    Me.tlpDetails.Name = "tlpDetails"
    Me.tlpDetails.RowCount = 2
    Me.tlpDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.tlpDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.tlpDetails.Size = New System.Drawing.Size(721, 57)
    Me.tlpDetails.TabIndex = 56
    '
    'Label21
    '
    Me.Label21.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label21.AutoSize = True
    Me.Label21.Location = New System.Drawing.Point(405, 36)
    Me.Label21.Name = "Label21"
    Me.Label21.Size = New System.Drawing.Size(60, 13)
    Me.Label21.TabIndex = 87
    Me.Label21.Text = "E-mail"
    '
    'Label13
    '
    Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label13.AutoSize = True
    Me.Label13.Location = New System.Drawing.Point(3, 7)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(55, 13)
    Me.Label13.TabIndex = 71
    Me.Label13.Text = "Nazwa"
    '
    'lblNazwa
    '
    Me.lblNazwa.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblNazwa.AutoSize = True
    Me.tlpDetails.SetColumnSpan(Me.lblNazwa, 5)
    Me.lblNazwa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNazwa.ForeColor = System.Drawing.SystemColors.HotTrack
    Me.lblNazwa.Location = New System.Drawing.Point(64, 7)
    Me.lblNazwa.Name = "lblNazwa"
    Me.lblNazwa.Size = New System.Drawing.Size(654, 13)
    Me.lblNazwa.TabIndex = 50
    Me.lblNazwa.Text = "Alias"
    '
    'lblFax
    '
    Me.lblFax.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFax.AutoSize = True
    Me.lblFax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblFax.ForeColor = System.Drawing.SystemColors.HotTrack
    Me.lblFax.Location = New System.Drawing.Point(279, 36)
    Me.lblFax.Name = "lblFax"
    Me.lblFax.Size = New System.Drawing.Size(120, 13)
    Me.lblFax.TabIndex = 55
    Me.lblFax.Text = "Fax"
    '
    'lblEmail
    '
    Me.lblEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblEmail.AutoSize = True
    Me.lblEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblEmail.ForeColor = System.Drawing.SystemColors.HotTrack
    Me.lblEmail.Location = New System.Drawing.Point(471, 36)
    Me.lblEmail.Name = "lblEmail"
    Me.lblEmail.Size = New System.Drawing.Size(247, 13)
    Me.lblEmail.TabIndex = 56
    Me.lblEmail.Text = "email"
    '
    'Label16
    '
    Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label16.AutoSize = True
    Me.Label16.Location = New System.Drawing.Point(3, 36)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(55, 13)
    Me.Label16.TabIndex = 74
    Me.Label16.Text = "Telefon"
    '
    'lblTel
    '
    Me.lblTel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblTel.AutoSize = True
    Me.lblTel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblTel.ForeColor = System.Drawing.SystemColors.HotTrack
    Me.lblTel.Location = New System.Drawing.Point(64, 36)
    Me.lblTel.Name = "lblTel"
    Me.lblTel.Size = New System.Drawing.Size(171, 13)
    Me.lblTel.TabIndex = 61
    Me.lblTel.Text = "Tel. "
    '
    'Label22
    '
    Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label22.AutoSize = True
    Me.Label22.Location = New System.Drawing.Point(241, 36)
    Me.Label22.Name = "Label22"
    Me.Label22.Size = New System.Drawing.Size(32, 13)
    Me.Label22.TabIndex = 84
    Me.Label22.Text = "Fax"
    '
    'Label30
    '
    Me.Label30.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label30.AutoSize = True
    Me.Label30.Enabled = False
    Me.Label30.Location = New System.Drawing.Point(512, 86)
    Me.Label30.Name = "Label30"
    Me.Label30.Size = New System.Drawing.Size(85, 13)
    Me.Label30.TabIndex = 54
    Me.Label30.Text = "Data modyfikacji"
    '
    'Label31
    '
    Me.Label31.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label31.AutoSize = True
    Me.Label31.Enabled = False
    Me.Label31.Location = New System.Drawing.Point(369, 86)
    Me.Label31.Name = "Label31"
    Me.Label31.Size = New System.Drawing.Size(31, 13)
    Me.Label31.TabIndex = 53
    Me.Label31.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(602, 81)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(120, 23)
    Me.lblData.TabIndex = 52
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(406, 81)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 50
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 81)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(280, 23)
    Me.lblUser.TabIndex = 51
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label32
    '
    Me.Label32.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label32.AutoSize = True
    Me.Label32.Enabled = False
    Me.Label32.Location = New System.Drawing.Point(3, 86)
    Me.Label32.Name = "Label32"
    Me.Label32.Size = New System.Drawing.Size(74, 13)
    Me.Label32.TabIndex = 49
    Me.Label32.Text = "Zmodyfikował"
    '
    'cbTypSzkoly
    '
    Me.cbTypSzkoly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbTypSzkoly.FormattingEnabled = True
    Me.cbTypSzkoly.Location = New System.Drawing.Point(77, 12)
    Me.cbTypSzkoly.Name = "cbTypSzkoly"
    Me.cbTypSzkoly.Size = New System.Drawing.Size(299, 21)
    Me.cbTypSzkoly.TabIndex = 84
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(12, 15)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(59, 13)
    Me.Label7.TabIndex = 83
    Me.Label7.Text = "Typ szkoły"
    '
    'frmSzkola
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(752, 399)
    Me.Controls.Add(Me.cbTypSzkoly)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.lvSzkoly)
    Me.Controls.Add(Me.Panel1)
    Me.Name = "frmSzkola"
    Me.Text = "Szkoły"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.tlpDetails.ResumeLayout(False)
    Me.tlpDetails.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label

  Friend WithEvents lvSzkoly As New System.Windows.Forms.ListView()
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents tlpDetails As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents Label21 As System.Windows.Forms.Label
  Friend WithEvents Label13 As System.Windows.Forms.Label
  Friend WithEvents lblNazwa As System.Windows.Forms.Label
  Friend WithEvents lblTel As System.Windows.Forms.Label
  Friend WithEvents Label16 As System.Windows.Forms.Label
  Friend WithEvents lblFax As System.Windows.Forms.Label
  Friend WithEvents lblEmail As System.Windows.Forms.Label
  Friend WithEvents Label22 As System.Windows.Forms.Label
  Friend WithEvents Label30 As System.Windows.Forms.Label
  Friend WithEvents Label31 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label32 As System.Windows.Forms.Label
  Friend WithEvents cbTypSzkoly As System.Windows.Forms.ComboBox
  Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
