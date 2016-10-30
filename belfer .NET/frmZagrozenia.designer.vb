<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZagrozenia
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZagrozenia))
    Me.nudSemestr = New System.Windows.Forms.NumericUpDown()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.lvUczen = New System.Windows.Forms.ListView()
    Me.lvNiezagrozone = New System.Windows.Forms.ListView()
    Me.lvZagrozone = New System.Windows.Forms.ListView()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdAdd = New System.Windows.Forms.Button()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label18 = New System.Windows.Forms.Label()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'nudSemestr
    '
    Me.nudSemestr.Location = New System.Drawing.Point(411, 13)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(34, 20)
    Me.nudSemestr.TabIndex = 47
    Me.nudSemestr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(360, 15)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 46
    Me.Label8.Text = "Semestr"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(51, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(303, 21)
    Me.cbKlasa.TabIndex = 41
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 39
    Me.Label2.Text = "Klasa"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(777, 406)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(85, 35)
    Me.cmdClose.TabIndex = 114
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'lvUczen
    '
    Me.lvUczen.HideSelection = False
    Me.lvUczen.Location = New System.Drawing.Point(15, 39)
    Me.lvUczen.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.lvUczen.Name = "lvUczen"
    Me.lvUczen.ShowItemToolTips = True
    Me.lvUczen.Size = New System.Drawing.Size(173, 402)
    Me.lvUczen.TabIndex = 115
    Me.lvUczen.UseCompatibleStateImageBehavior = False
    '
    'lvNiezagrozone
    '
    Me.lvNiezagrozone.HideSelection = False
    Me.lvNiezagrozone.Location = New System.Drawing.Point(196, 39)
    Me.lvNiezagrozone.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.lvNiezagrozone.Name = "lvNiezagrozone"
    Me.lvNiezagrozone.Size = New System.Drawing.Size(249, 402)
    Me.lvNiezagrozone.TabIndex = 116
    Me.lvNiezagrozone.UseCompatibleStateImageBehavior = False
    '
    'lvZagrozone
    '
    Me.lvZagrozone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvZagrozone.BackColor = System.Drawing.SystemColors.Info
    Me.lvZagrozone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.lvZagrozone.HideSelection = False
    Me.lvZagrozone.Location = New System.Drawing.Point(523, 39)
    Me.lvZagrozone.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.lvZagrozone.Name = "lvZagrozone"
    Me.lvZagrozone.Size = New System.Drawing.Size(249, 402)
    Me.lvZagrozone.TabIndex = 117
    Me.lvZagrozone.UseCompatibleStateImageBehavior = False
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(777, 39)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(85, 35)
    Me.cmdPrint.TabIndex = 122
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
    Me.cmdDelete.Location = New System.Drawing.Point(450, 237)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(67, 45)
    Me.cmdDelete.TabIndex = 170
    Me.cmdDelete.Text = "&Usuñ"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdAdd
    '
    Me.cmdAdd.Enabled = False
    Me.cmdAdd.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
    Me.cmdAdd.Location = New System.Drawing.Point(451, 186)
    Me.cmdAdd.Name = "cmdAdd"
    Me.cmdAdd.Size = New System.Drawing.Size(66, 45)
    Me.cmdAdd.TabIndex = 169
    Me.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAdd.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label16)
    Me.Panel1.Controls.Add(Me.Label17)
    Me.Panel1.Controls.Add(Me.lblData)
    Me.Panel1.Controls.Add(Me.lblIP)
    Me.Panel1.Controls.Add(Me.lblUser)
    Me.Panel1.Controls.Add(Me.Label18)
    Me.Panel1.Location = New System.Drawing.Point(1, 446)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(872, 41)
    Me.Panel1.TabIndex = 171
    '
    'Label16
    '
    Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label16.AutoSize = True
    Me.Label16.Enabled = False
    Me.Label16.Location = New System.Drawing.Point(645, 14)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(85, 13)
    Me.Label16.TabIndex = 117
    Me.Label16.Text = "Data modyfikacji"
    '
    'Label17
    '
    Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label17.AutoSize = True
    Me.Label17.Enabled = False
    Me.Label17.Location = New System.Drawing.Point(502, 14)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(31, 13)
    Me.Label17.TabIndex = 116
    Me.Label17.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(736, 9)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(120, 23)
    Me.lblData.TabIndex = 115
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(539, 9)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 113
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(95, 9)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(401, 23)
    Me.lblUser.TabIndex = 114
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label18
    '
    Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label18.AutoSize = True
    Me.Label18.Enabled = False
    Me.Label18.Location = New System.Drawing.Point(11, 14)
    Me.Label18.Name = "Label18"
    Me.Label18.Size = New System.Drawing.Size(74, 13)
    Me.Label18.TabIndex = 112
    Me.Label18.Text = "Zmodyfikowa³"
    '
    'frmZagrozenia
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(869, 490)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdAdd)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.lvZagrozone)
    Me.Controls.Add(Me.lvNiezagrozone)
    Me.Controls.Add(Me.lvUczen)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.nudSemestr)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label2)
    Me.Name = "frmZagrozenia"
    Me.Text = "Zagro¿enia ocenami niedostatecznymi"
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents lvUczen As System.Windows.Forms.ListView
  Friend WithEvents lvNiezagrozone As System.Windows.Forms.ListView
  Friend WithEvents lvZagrozone As System.Windows.Forms.ListView
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdAdd As System.Windows.Forms.Button
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label16 As System.Windows.Forms.Label
  Friend WithEvents Label17 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label18 As System.Windows.Forms.Label
End Class
