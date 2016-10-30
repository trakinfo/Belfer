<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReklamacja
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReklamacja))
    Me.lvReklamacja = New System.Windows.Forms.ListView()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.cbSeek = New System.Windows.Forms.ComboBox()
    Me.lblFilter = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdAccept = New System.Windows.Forms.Button()
    Me.cmdReject = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.gbStatus = New System.Windows.Forms.GroupBox()
    Me.rbAccepted = New System.Windows.Forms.RadioButton()
    Me.rbRejected = New System.Windows.Forms.RadioButton()
    Me.rbAllStatus = New System.Windows.Forms.RadioButton()
    Me.rbRegistered = New System.Windows.Forms.RadioButton()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.Panel1.SuspendLayout()
    Me.gbStatus.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvReklamacja
    '
    Me.lvReklamacja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvReklamacja.Location = New System.Drawing.Point(12, 54)
    Me.lvReklamacja.Name = "lvReklamacja"
    Me.lvReklamacja.ShowItemToolTips = True
    Me.lvReklamacja.Size = New System.Drawing.Size(918, 391)
    Me.lvReklamacja.TabIndex = 37
    Me.lvReklamacja.UseCompatibleStateImageBehavior = False
    '
    'txtSeek
    '
    Me.txtSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeek.Location = New System.Drawing.Point(611, 451)
    Me.txtSeek.Name = "txtSeek"
    Me.txtSeek.Size = New System.Drawing.Size(319, 20)
    Me.txtSeek.TabIndex = 159
    '
    'cbSeek
    '
    Me.cbSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbSeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeek.DropDownWidth = 200
    Me.cbSeek.FormattingEnabled = True
    Me.cbSeek.Location = New System.Drawing.Point(360, 451)
    Me.cbSeek.Name = "cbSeek"
    Me.cbSeek.Size = New System.Drawing.Size(245, 21)
    Me.cbSeek.TabIndex = 158
    '
    'lblFilter
    '
    Me.lblFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFilter.AutoSize = True
    Me.lblFilter.Location = New System.Drawing.Point(306, 454)
    Me.lblFilter.Name = "lblFilter"
    Me.lblFilter.Size = New System.Drawing.Size(48, 13)
    Me.lblFilter.TabIndex = 157
    Me.lblFilter.Text = "Filtruj wg"
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(15, 454)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 155
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(59, 454)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 156
    Me.lblRecord.Text = "lblRecord"
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label14)
    Me.Panel1.Controls.Add(Me.Label12)
    Me.Panel1.Controls.Add(Me.lblData)
    Me.Panel1.Controls.Add(Me.lblIP)
    Me.Panel1.Controls.Add(Me.lblUser)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Location = New System.Drawing.Point(12, 477)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(1041, 34)
    Me.Panel1.TabIndex = 160
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(607, 12)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 175
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(414, 12)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 174
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(698, 7)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(220, 23)
    Me.lblData.TabIndex = 173
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(451, 7)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(150, 23)
    Me.lblIP.TabIndex = 171
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 7)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(325, 23)
    Me.lblUser.TabIndex = 172
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(3, 12)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 170
    Me.Label3.Text = "Zmodyfikował"
    '
    'cmdAccept
    '
    Me.cmdAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAccept.Enabled = False
    Me.cmdAccept.Image = CType(resources.GetObject("cmdAccept.Image"), System.Drawing.Image)
    Me.cmdAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAccept.Location = New System.Drawing.Point(939, 54)
    Me.cmdAccept.Name = "cmdAccept"
    Me.cmdAccept.Size = New System.Drawing.Size(117, 36)
    Me.cmdAccept.TabIndex = 225
    Me.cmdAccept.Tag = "1"
    Me.cmdAccept.Text = " Akceptuj"
    Me.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAccept.UseVisualStyleBackColor = True
    '
    'cmdReject
    '
    Me.cmdReject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdReject.Enabled = False
    Me.cmdReject.Image = Global.belfer.NET.My.Resources.Resources.reject_24
    Me.cmdReject.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdReject.Location = New System.Drawing.Point(939, 96)
    Me.cmdReject.Name = "cmdReject"
    Me.cmdReject.Size = New System.Drawing.Size(117, 36)
    Me.cmdReject.TabIndex = 224
    Me.cmdReject.Tag = "2"
    Me.cmdReject.Text = " Odrzuć"
    Me.cmdReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdReject.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(939, 410)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 223
    Me.cmdClose.Text = " &Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'gbStatus
    '
    Me.gbStatus.Controls.Add(Me.rbAccepted)
    Me.gbStatus.Controls.Add(Me.rbRejected)
    Me.gbStatus.Controls.Add(Me.rbAllStatus)
    Me.gbStatus.Controls.Add(Me.rbRegistered)
    Me.gbStatus.Location = New System.Drawing.Point(12, 5)
    Me.gbStatus.Name = "gbStatus"
    Me.gbStatus.Size = New System.Drawing.Size(385, 43)
    Me.gbStatus.TabIndex = 226
    Me.gbStatus.TabStop = False
    Me.gbStatus.Text = "Status powiadomienia"
    '
    'rbAccepted
    '
    Me.rbAccepted.AutoSize = True
    Me.rbAccepted.Location = New System.Drawing.Point(110, 19)
    Me.rbAccepted.Name = "rbAccepted"
    Me.rbAccepted.Size = New System.Drawing.Size(103, 17)
    Me.rbAccepted.TabIndex = 5
    Me.rbAccepted.TabStop = True
    Me.rbAccepted.Tag = "Status IN (1)"
    Me.rbAccepted.Text = "Zaakceptowane"
    Me.rbAccepted.UseVisualStyleBackColor = True
    '
    'rbRejected
    '
    Me.rbRejected.AutoSize = True
    Me.rbRejected.Location = New System.Drawing.Point(219, 19)
    Me.rbRejected.Name = "rbRejected"
    Me.rbRejected.Size = New System.Drawing.Size(77, 17)
    Me.rbRejected.TabIndex = 3
    Me.rbRejected.TabStop = True
    Me.rbRejected.Tag = "Status IN (2)"
    Me.rbRejected.Text = "Odrzucone"
    Me.rbRejected.UseVisualStyleBackColor = True
    '
    'rbAllStatus
    '
    Me.rbAllStatus.AutoSize = True
    Me.rbAllStatus.Location = New System.Drawing.Point(302, 19)
    Me.rbAllStatus.Name = "rbAllStatus"
    Me.rbAllStatus.Size = New System.Drawing.Size(73, 17)
    Me.rbAllStatus.TabIndex = 2
    Me.rbAllStatus.Tag = "Status IN (0,1,2)"
    Me.rbAllStatus.Text = "Wszystkie"
    Me.rbAllStatus.UseVisualStyleBackColor = True
    '
    'rbRegistered
    '
    Me.rbRegistered.AutoSize = True
    Me.rbRegistered.Checked = True
    Me.rbRegistered.Location = New System.Drawing.Point(6, 19)
    Me.rbRegistered.Name = "rbRegistered"
    Me.rbRegistered.Size = New System.Drawing.Size(98, 17)
    Me.rbRegistered.TabIndex = 0
    Me.rbRegistered.TabStop = True
    Me.rbRegistered.Tag = "Status IN (0)"
    Me.rbRegistered.Text = "Zarejestrowane"
    Me.rbRegistered.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(936, 138)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 227
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'frmReklamacja
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1065, 523)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.gbStatus)
    Me.Controls.Add(Me.cmdAccept)
    Me.Controls.Add(Me.cmdReject)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.txtSeek)
    Me.Controls.Add(Me.cbSeek)
    Me.Controls.Add(Me.lblFilter)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.lblRecord)
    Me.Controls.Add(Me.lvReklamacja)
    Me.Name = "frmReklamacja"
    Me.Text = "Nieobecności wstawione pomyłkowo"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.gbStatus.ResumeLayout(False)
    Me.gbStatus.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lvReklamacja As System.Windows.Forms.ListView
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents cbSeek As System.Windows.Forms.ComboBox
  Friend WithEvents lblFilter As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdAccept As System.Windows.Forms.Button
  Friend WithEvents cmdReject As System.Windows.Forms.Button
  Friend WithEvents gbStatus As System.Windows.Forms.GroupBox
  Friend WithEvents rbAccepted As System.Windows.Forms.RadioButton
  Friend WithEvents rbRejected As System.Windows.Forms.RadioButton
  Friend WithEvents rbAllStatus As System.Windows.Forms.RadioButton
  Friend WithEvents rbRegistered As System.Windows.Forms.RadioButton
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
End Class
