<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUser
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
    Me.lvUsers = New System.Windows.Forms.ListView()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.cbSeek = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdAddNewUser = New System.Windows.Forms.Button()
    Me.Panel2.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvUsers
    '
    Me.lvUsers.Location = New System.Drawing.Point(12, 12)
    Me.lvUsers.Name = "lvUsers"
    Me.lvUsers.ShowItemToolTips = True
    Me.lvUsers.Size = New System.Drawing.Size(779, 420)
    Me.lvUsers.TabIndex = 0
    Me.lvUsers.UseCompatibleStateImageBehavior = False
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
    Me.Panel2.Location = New System.Drawing.Point(12, 474)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(860, 34)
    Me.Panel2.TabIndex = 33
    '
    'Label14
    '
    Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(563, 11)
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
    Me.Label12.Location = New System.Drawing.Point(370, 11)
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
    Me.lblData.Location = New System.Drawing.Point(654, 6)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(203, 23)
    Me.lblData.TabIndex = 173
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(407, 5)
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
    Me.lblUser.Location = New System.Drawing.Point(83, 6)
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
    Me.Label3.Location = New System.Drawing.Point(3, 11)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 170
    Me.Label3.Text = "Zmodyfikował"
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.txtSeek)
    Me.Panel1.Controls.Add(Me.cbSeek)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(11, 435)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(868, 36)
    Me.Panel1.TabIndex = 32
    '
    'txtSeek
    '
    Me.txtSeek.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeek.Location = New System.Drawing.Point(345, 6)
    Me.txtSeek.Name = "txtSeek"
    Me.txtSeek.Size = New System.Drawing.Size(435, 20)
    Me.txtSeek.TabIndex = 149
    '
    'cbSeek
    '
    Me.cbSeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSeek.DropDownWidth = 200
    Me.cbSeek.FormattingEnabled = True
    Me.cbSeek.Location = New System.Drawing.Point(217, 5)
    Me.cbSeek.Name = "cbSeek"
    Me.cbSeek.Size = New System.Drawing.Size(122, 21)
    Me.cbSeek.TabIndex = 148
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(163, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(48, 13)
    Me.Label1.TabIndex = 147
    Me.Label1.Text = "Filtruj wg"
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 9)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 145
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 9)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 146
    Me.lblRecord.Text = "lblRecord"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Location = New System.Drawing.Point(797, 409)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(75, 23)
    Me.cmdClose.TabIndex = 37
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Location = New System.Drawing.Point(797, 41)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(75, 23)
    Me.cmdEdit.TabIndex = 36
    Me.cmdEdit.Text = "&Edytuj"
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Location = New System.Drawing.Point(797, 70)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(75, 23)
    Me.cmdDelete.TabIndex = 35
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdAddNewUser
    '
    Me.cmdAddNewUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNewUser.Location = New System.Drawing.Point(797, 12)
    Me.cmdAddNewUser.Name = "cmdAddNewUser"
    Me.cmdAddNewUser.Size = New System.Drawing.Size(75, 23)
    Me.cmdAddNewUser.TabIndex = 34
    Me.cmdAddNewUser.Text = "&Dodaj"
    Me.cmdAddNewUser.UseVisualStyleBackColor = True
    '
    'frmUser
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(884, 523)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdAddNewUser)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.lvUsers)
    Me.MaximizeBox = False
    Me.Name = "frmUser"
    Me.Text = "Zarządzanie użytkownikami"
    Me.Panel2.ResumeLayout(false)
    Me.Panel2.PerformLayout
    Me.Panel1.ResumeLayout(false)
    Me.Panel1.PerformLayout
    Me.ResumeLayout(false)

End Sub
  Friend WithEvents lvUsers As System.Windows.Forms.ListView
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdAddNewUser As System.Windows.Forms.Button
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents cbSeek As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
