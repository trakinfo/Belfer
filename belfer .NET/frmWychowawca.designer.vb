<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWychowawca
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWychowawca))
    Me.lvWychowawca = New System.Windows.Forms.ListView()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdDeaktywacja = New System.Windows.Forms.Button()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvWychowawca
    '
    Me.lvWychowawca.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvWychowawca.BackColor = System.Drawing.SystemColors.Window
    Me.lvWychowawca.ForeColor = System.Drawing.Color.Black
    Me.lvWychowawca.HideSelection = False
    Me.lvWychowawca.Location = New System.Drawing.Point(18, 11)
    Me.lvWychowawca.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.lvWychowawca.Name = "lvWychowawca"
    Me.lvWychowawca.Size = New System.Drawing.Size(850, 449)
    Me.lvWychowawca.TabIndex = 143
    Me.lvWychowawca.UseCompatibleStateImageBehavior = False
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Location = New System.Drawing.Point(12, 465)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(849, 28)
    Me.Panel1.TabIndex = 178
    '
    'lblRecord
    '
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 6)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 163
    Me.lblRecord.Text = "lblRecord"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 6)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 162
    Me.Label8.Text = "Rekord:"
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(874, 385)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(92, 34)
    Me.cmdPrint.TabIndex = 151
    Me.cmdPrint.Text = "D&rukuj"
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdPrint.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(874, 132)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(92, 34)
    Me.cmdDelete.TabIndex = 150
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.Location = New System.Drawing.Point(874, 52)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(92, 34)
    Me.cmdEdit.TabIndex = 149
    Me.cmdEdit.Text = "&Edytuj"
    Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.Location = New System.Drawing.Point(874, 12)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(92, 34)
    Me.cmdAddNew.TabIndex = 148
    Me.cmdAddNew.Text = "&Dodaj"
    Me.cmdAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.AutoScroll = True
    Me.Panel2.Controls.Add(Me.Label14)
    Me.Panel2.Controls.Add(Me.lblData)
    Me.Panel2.Controls.Add(Me.Label12)
    Me.Panel2.Controls.Add(Me.lblIP)
    Me.Panel2.Controls.Add(Me.lblUser)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Location = New System.Drawing.Point(12, 496)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(945, 31)
    Me.Panel2.TabIndex = 179
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(44, 38)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 168
    Me.Label12.Text = "Nr IP"
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(146, 33)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(274, 23)
    Me.lblIP.TabIndex = 165
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(146, 2)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(779, 23)
    Me.lblUser.TabIndex = 166
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(3, 7)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(137, 13)
    Me.Label3.TabIndex = 164
    Me.Label3.Text = "Zmodyfikował (wprowadził)"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
    Me.cmdClose.Location = New System.Drawing.Point(874, 425)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(92, 35)
    Me.cmdClose.TabIndex = 181
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdDeaktywacja
    '
    Me.cmdDeaktywacja.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDeaktywacja.Enabled = False
    Me.cmdDeaktywacja.Image = CType(resources.GetObject("cmdDeaktywacja.Image"), System.Drawing.Image)
    Me.cmdDeaktywacja.Location = New System.Drawing.Point(874, 92)
    Me.cmdDeaktywacja.Name = "cmdDeaktywacja"
    Me.cmdDeaktywacja.Size = New System.Drawing.Size(92, 34)
    Me.cmdDeaktywacja.TabIndex = 203
    Me.cmdDeaktywacja.Text = "Deaktywu&j"
    Me.cmdDeaktywacja.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDeaktywacja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDeaktywacja.UseVisualStyleBackColor = True
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(426, 38)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 171
    Me.Label14.Text = "Data modyfikacji"
    '
    'lblData
    '
    Me.lblData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(517, 33)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(339, 23)
    Me.lblData.TabIndex = 170
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'frmWychowawca
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(974, 532)
    Me.Controls.Add(Me.cmdDeaktywacja)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.lvWychowawca)
    Me.MaximumSize = New System.Drawing.Size(990, 700)
    Me.MinimumSize = New System.Drawing.Size(990, 570)
    Me.Name = "frmWychowawca"
    Me.Text = "Przydział wychowawstw"
    Me.Panel1.ResumeLayout(false)
    Me.Panel1.PerformLayout
    Me.Panel2.ResumeLayout(false)
    Me.Panel2.PerformLayout
    Me.ResumeLayout(false)

End Sub
  Friend WithEvents lvWychowawca As System.Windows.Forms.ListView
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdDeaktywacja As System.Windows.Forms.Button
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
End Class
