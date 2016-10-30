<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWniosek
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
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.lvWniosek = New System.Windows.Forms.ListView()
    Me.lvSubwniosek = New System.Windows.Forms.ListView()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.cbSeek = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdExecute = New System.Windows.Forms.Button()
    Me.cmdVerify = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.rbVerified = New System.Windows.Forms.RadioButton()
    Me.rbInProgress = New System.Windows.Forms.RadioButton()
    Me.rbRejected = New System.Windows.Forms.RadioButton()
    Me.rbAllStatus = New System.Windows.Forms.RadioButton()
    Me.rbCompleted = New System.Windows.Forms.RadioButton()
    Me.rbRegistered = New System.Windows.Forms.RadioButton()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.rbRestorePassword = New System.Windows.Forms.RadioButton()
    Me.rbAddPrivilege = New System.Windows.Forms.RadioButton()
    Me.rbNewAccount = New System.Windows.Forms.RadioButton()
    Me.rbAllType = New System.Windows.Forms.RadioButton()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.Panel2.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.SuspendLayout()
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.Controls.Add(Me.Label14)
    Me.Panel2.Controls.Add(Me.Label12)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Location = New System.Drawing.Point(5, 486)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(998, 34)
    Me.Panel2.TabIndex = 35
    '
    'Label14
    '
    Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(564, 11)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 175
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(371, 11)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 174
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(655, 6)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(220, 23)
    Me.lblData.TabIndex = 173
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(408, 6)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(150, 23)
    Me.lblIP.TabIndex = 171
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(84, 6)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(281, 23)
    Me.lblUser.TabIndex = 172
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(4, 11)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 170
    Me.Label3.Text = "Zmodyfikował"
    '
    'lvWniosek
    '
    Me.lvWniosek.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvWniosek.Location = New System.Drawing.Point(12, 54)
    Me.lvWniosek.Name = "lvWniosek"
    Me.lvWniosek.ShowItemToolTips = True
    Me.lvWniosek.Size = New System.Drawing.Size(868, 286)
    Me.lvWniosek.TabIndex = 36
    Me.lvWniosek.UseCompatibleStateImageBehavior = False
    '
    'lvSubwniosek
    '
    Me.lvSubwniosek.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvSubwniosek.Location = New System.Drawing.Point(12, 371)
    Me.lvSubwniosek.Name = "lvSubwniosek"
    Me.lvSubwniosek.ShowItemToolTips = True
    Me.lvSubwniosek.Size = New System.Drawing.Size(868, 109)
    Me.lvSubwniosek.TabIndex = 37
    Me.lvSubwniosek.UseCompatibleStateImageBehavior = False
    '
    'txtSeek
    '
    Me.txtSeek.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeek.Location = New System.Drawing.Point(558, 345)
    Me.txtSeek.Name = "txtSeek"
    Me.txtSeek.Size = New System.Drawing.Size(322, 20)
    Me.txtSeek.TabIndex = 154
    '
    'cbSeek
    '
    Me.cbSeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeek.DropDownWidth = 200
    Me.cbSeek.FormattingEnabled = True
    Me.cbSeek.Location = New System.Drawing.Point(357, 344)
    Me.cbSeek.Name = "cbSeek"
    Me.cbSeek.Size = New System.Drawing.Size(195, 21)
    Me.cbSeek.TabIndex = 153
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(303, 347)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(48, 13)
    Me.Label1.TabIndex = 152
    Me.Label1.Text = "Filtruj wg"
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(12, 349)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 150
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(56, 349)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 151
    Me.lblRecord.Text = "lblRecord"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(886, 445)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 222
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdExecute
    '
    Me.cmdExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdExecute.Enabled = False
    Me.cmdExecute.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdExecute.Location = New System.Drawing.Point(886, 96)
    Me.cmdExecute.Name = "cmdExecute"
    Me.cmdExecute.Size = New System.Drawing.Size(117, 36)
    Me.cmdExecute.TabIndex = 220
    Me.cmdExecute.Text = "Przygotuj"
    Me.cmdExecute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdExecute.UseVisualStyleBackColor = True
    '
    'cmdVerify
    '
    Me.cmdVerify.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdVerify.Enabled = False
    Me.cmdVerify.Image = Global.belfer.NET.My.Resources.Resources.register_24
    Me.cmdVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdVerify.Location = New System.Drawing.Point(886, 54)
    Me.cmdVerify.Name = "cmdVerify"
    Me.cmdVerify.Size = New System.Drawing.Size(117, 36)
    Me.cmdVerify.TabIndex = 221
    Me.cmdVerify.Text = "Weryfikuj"
    Me.cmdVerify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdVerify.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(886, 179)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 219
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.rbVerified)
    Me.GroupBox1.Controls.Add(Me.rbInProgress)
    Me.GroupBox1.Controls.Add(Me.rbRejected)
    Me.GroupBox1.Controls.Add(Me.rbAllStatus)
    Me.GroupBox1.Controls.Add(Me.rbCompleted)
    Me.GroupBox1.Controls.Add(Me.rbRegistered)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(594, 43)
    Me.GroupBox1.TabIndex = 223
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Status zgłoszenia"
    '
    'rbVerified
    '
    Me.rbVerified.AutoSize = True
    Me.rbVerified.Location = New System.Drawing.Point(110, 19)
    Me.rbVerified.Name = "rbVerified"
    Me.rbVerified.Size = New System.Drawing.Size(97, 17)
    Me.rbVerified.TabIndex = 5
    Me.rbVerified.TabStop = True
    Me.rbVerified.Tag = "Status IN (7)"
    Me.rbVerified.Text = "Zweryfikowane"
    Me.rbVerified.UseVisualStyleBackColor = True
    '
    'rbInProgress
    '
    Me.rbInProgress.AutoSize = True
    Me.rbInProgress.Location = New System.Drawing.Point(213, 19)
    Me.rbInProgress.Name = "rbInProgress"
    Me.rbInProgress.Size = New System.Drawing.Size(120, 17)
    Me.rbInProgress.TabIndex = 4
    Me.rbInProgress.TabStop = True
    Me.rbInProgress.Tag = "Status IN (8)"
    Me.rbInProgress.Text = "Gotowe do przekaz."
    Me.rbInProgress.UseVisualStyleBackColor = True
    '
    'rbRejected
    '
    Me.rbRejected.AutoSize = True
    Me.rbRejected.Location = New System.Drawing.Point(433, 19)
    Me.rbRejected.Name = "rbRejected"
    Me.rbRejected.Size = New System.Drawing.Size(77, 17)
    Me.rbRejected.TabIndex = 3
    Me.rbRejected.TabStop = True
    Me.rbRejected.Tag = "Status IN (4,9)"
    Me.rbRejected.Text = "Odrzucone"
    Me.rbRejected.UseVisualStyleBackColor = True
    '
    'rbAllStatus
    '
    Me.rbAllStatus.AutoSize = True
    Me.rbAllStatus.Location = New System.Drawing.Point(516, 19)
    Me.rbAllStatus.Name = "rbAllStatus"
    Me.rbAllStatus.Size = New System.Drawing.Size(73, 17)
    Me.rbAllStatus.TabIndex = 2
    Me.rbAllStatus.Tag = "Status IN (1,2,3,4,5,6,7,8,9)"
    Me.rbAllStatus.Text = "Wszystkie"
    Me.rbAllStatus.UseVisualStyleBackColor = True
    '
    'rbCompleted
    '
    Me.rbCompleted.AutoSize = True
    Me.rbCompleted.Location = New System.Drawing.Point(339, 19)
    Me.rbCompleted.Name = "rbCompleted"
    Me.rbCompleted.Size = New System.Drawing.Size(88, 17)
    Me.rbCompleted.TabIndex = 1
    Me.rbCompleted.Tag = "Status IN (2,3,5,6)"
    Me.rbCompleted.Text = "Zrealizowane"
    Me.rbCompleted.UseVisualStyleBackColor = True
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
    Me.rbRegistered.Tag = "Status IN (1)"
    Me.rbRegistered.Text = "Zarejestrowane"
    Me.rbRegistered.UseVisualStyleBackColor = True
    '
    'GroupBox2
    '
    Me.GroupBox2.Controls.Add(Me.rbRestorePassword)
    Me.GroupBox2.Controls.Add(Me.rbAddPrivilege)
    Me.GroupBox2.Controls.Add(Me.rbNewAccount)
    Me.GroupBox2.Controls.Add(Me.rbAllType)
    Me.GroupBox2.Location = New System.Drawing.Point(612, 5)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(391, 43)
    Me.GroupBox2.TabIndex = 224
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Typ zgłoszenia"
    '
    'rbRestorePassword
    '
    Me.rbRestorePassword.AutoSize = True
    Me.rbRestorePassword.Location = New System.Drawing.Point(288, 19)
    Me.rbRestorePassword.Name = "rbRestorePassword"
    Me.rbRestorePassword.Size = New System.Drawing.Size(93, 17)
    Me.rbRestorePassword.TabIndex = 3
    Me.rbRestorePassword.TabStop = True
    Me.rbRestorePassword.Tag = " AND ApplyType='RP'"
    Me.rbRestorePassword.Text = "Odzysk. hasła"
    Me.rbRestorePassword.UseVisualStyleBackColor = True
    '
    'rbAddPrivilege
    '
    Me.rbAddPrivilege.AutoSize = True
    Me.rbAddPrivilege.Location = New System.Drawing.Point(182, 19)
    Me.rbAddPrivilege.Name = "rbAddPrivilege"
    Me.rbAddPrivilege.Size = New System.Drawing.Size(100, 17)
    Me.rbAddPrivilege.TabIndex = 2
    Me.rbAddPrivilege.Tag = " AND ApplyType='AP'"
    Me.rbAddPrivilege.Text = "Dodanie upraw."
    Me.rbAddPrivilege.UseVisualStyleBackColor = True
    '
    'rbNewAccount
    '
    Me.rbNewAccount.AutoSize = True
    Me.rbNewAccount.Location = New System.Drawing.Point(85, 19)
    Me.rbNewAccount.Name = "rbNewAccount"
    Me.rbNewAccount.Size = New System.Drawing.Size(91, 17)
    Me.rbNewAccount.TabIndex = 1
    Me.rbNewAccount.Tag = " AND ApplyType='NA'"
    Me.rbNewAccount.Text = "Utworz. konta"
    Me.rbNewAccount.UseVisualStyleBackColor = True
    '
    'rbAllType
    '
    Me.rbAllType.AutoSize = True
    Me.rbAllType.Checked = True
    Me.rbAllType.Location = New System.Drawing.Point(6, 19)
    Me.rbAllType.Name = "rbAllType"
    Me.rbAllType.Size = New System.Drawing.Size(73, 17)
    Me.rbAllType.TabIndex = 0
    Me.rbAllType.TabStop = True
    Me.rbAllType.Tag = " AND ApplyType IN ('NA','AP','RP')"
    Me.rbAllType.Text = "Wszystkie"
    Me.rbAllType.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(886, 138)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 35)
    Me.cmdPrint.TabIndex = 225
    Me.cmdPrint.Text = "Zrealizuj"
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.Image = Global.belfer.NET.My.Resources.Resources.refresh_24
    Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdRefresh.Location = New System.Drawing.Point(886, 403)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(117, 36)
    Me.cmdRefresh.TabIndex = 226
    Me.cmdRefresh.Text = "Odśwież"
    Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'frmWniosek
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1015, 523)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdExecute)
    Me.Controls.Add(Me.cmdVerify)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.txtSeek)
    Me.Controls.Add(Me.cbSeek)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.lblRecord)
    Me.Controls.Add(Me.lvSubwniosek)
    Me.Controls.Add(Me.lvWniosek)
    Me.Controls.Add(Me.Panel2)
    Me.Name = "frmWniosek"
    Me.Tag = "1"
    Me.Text = "Zgłoszenia rodziców o uzyskanie prawa dostępu do ucznia"
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lvWniosek As System.Windows.Forms.ListView
  Friend WithEvents lvSubwniosek As System.Windows.Forms.ListView
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents cbSeek As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdExecute As System.Windows.Forms.Button
  Friend WithEvents cmdVerify As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents rbAllStatus As System.Windows.Forms.RadioButton
  Friend WithEvents rbCompleted As System.Windows.Forms.RadioButton
  Friend WithEvents rbRegistered As System.Windows.Forms.RadioButton
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents rbAddPrivilege As System.Windows.Forms.RadioButton
  Friend WithEvents rbNewAccount As System.Windows.Forms.RadioButton
  Friend WithEvents rbAllType As System.Windows.Forms.RadioButton
  Friend WithEvents rbRejected As System.Windows.Forms.RadioButton
  Friend WithEvents rbRestorePassword As System.Windows.Forms.RadioButton
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents rbInProgress As System.Windows.Forms.RadioButton
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents rbVerified As System.Windows.Forms.RadioButton
End Class
