Partial Public Class frmUwagi
  Inherits System.Windows.Forms.Form

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Me.Label7 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblNegatywne = New System.Windows.Forms.Label()
    Me.lblPozytywne = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.lblRekord = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.txtTrescUwagi = New System.Windows.Forms.TextBox()
    Me.lvUwagi = New System.Windows.Forms.ListView()
    Me.lvUczen = New System.Windows.Forms.ListView()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.chkGrupa = New System.Windows.Forms.CheckBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.dtDataOd = New System.Windows.Forms.DateTimePicker()
    Me.dtDataDo = New System.Windows.Forms.DateTimePicker()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.chkVirtual = New System.Windows.Forms.CheckBox()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(9, 15)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(33, 13)
    Me.Label7.TabIndex = 24
    Me.Label7.Text = "Klasa"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(48, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(300, 21)
    Me.cbKlasa.TabIndex = 25
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.Label12.Location = New System.Drawing.Point(548, 320)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(83, 13)
    Me.Label12.TabIndex = 86
    Me.Label12.Text = "Negatywnych"
    '
    'lblNegatywne
    '
    Me.lblNegatywne.BackColor = System.Drawing.SystemColors.Info
    Me.lblNegatywne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblNegatywne.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNegatywne.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.lblNegatywne.Location = New System.Drawing.Point(637, 320)
    Me.lblNegatywne.Name = "lblNegatywne"
    Me.lblNegatywne.Size = New System.Drawing.Size(54, 14)
    Me.lblNegatywne.TabIndex = 85
    Me.lblNegatywne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblPozytywne
    '
    Me.lblPozytywne.BackColor = System.Drawing.SystemColors.Info
    Me.lblPozytywne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblPozytywne.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblPozytywne.ForeColor = System.Drawing.Color.Green
    Me.lblPozytywne.Location = New System.Drawing.Point(470, 320)
    Me.lblPozytywne.Name = "lblPozytywne"
    Me.lblPozytywne.Size = New System.Drawing.Size(46, 14)
    Me.lblPozytywne.TabIndex = 84
    Me.lblPozytywne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label11.ForeColor = System.Drawing.Color.Green
    Me.Label11.Location = New System.Drawing.Point(384, 320)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(80, 13)
    Me.Label11.TabIndex = 83
    Me.Label11.Text = "Pozytywnych"
    '
    'lblRekord
    '
    Me.lblRekord.AutoSize = True
    Me.lblRekord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRekord.ForeColor = System.Drawing.Color.Blue
    Me.lblRekord.Location = New System.Drawing.Point(239, 320)
    Me.lblRekord.Name = "lblRekord"
    Me.lblRekord.Size = New System.Drawing.Size(0, 13)
    Me.lblRekord.TabIndex = 82
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(191, 320)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(42, 13)
    Me.Label3.TabIndex = 81
    Me.Label3.Text = "Rekord"
    '
    'txtTrescUwagi
    '
    Me.txtTrescUwagi.BackColor = System.Drawing.SystemColors.Info
    Me.txtTrescUwagi.Location = New System.Drawing.Point(191, 351)
    Me.txtTrescUwagi.Multiline = True
    Me.txtTrescUwagi.Name = "txtTrescUwagi"
    Me.txtTrescUwagi.ReadOnly = True
    Me.txtTrescUwagi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtTrescUwagi.Size = New System.Drawing.Size(500, 135)
    Me.txtTrescUwagi.TabIndex = 80
    '
    'lvUwagi
    '
    Me.lvUwagi.Location = New System.Drawing.Point(191, 63)
    Me.lvUwagi.Name = "lvUwagi"
    Me.lvUwagi.Size = New System.Drawing.Size(500, 254)
    Me.lvUwagi.TabIndex = 79
    Me.lvUwagi.UseCompatibleStateImageBehavior = False
    '
    'lvUczen
    '
    Me.lvUczen.Location = New System.Drawing.Point(12, 39)
    Me.lvUczen.Name = "lvUczen"
    Me.lvUczen.Size = New System.Drawing.Size(173, 447)
    Me.lvUczen.TabIndex = 78
    Me.lvUczen.UseCompatibleStateImageBehavior = False
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label9.ForeColor = System.Drawing.Color.Blue
    Me.Label9.Location = New System.Drawing.Point(239, 320)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(0, 13)
    Me.Label9.TabIndex = 82
    '
    'chkGrupa
    '
    Me.chkGrupa.AutoSize = True
    Me.chkGrupa.Enabled = False
    Me.chkGrupa.Location = New System.Drawing.Point(563, 14)
    Me.chkGrupa.Name = "chkGrupa"
    Me.chkGrupa.Size = New System.Drawing.Size(128, 17)
    Me.chkGrupa.TabIndex = 87
    Me.chkGrupa.Text = "Grupuj wg typu uwagi"
    Me.chkGrupa.UseVisualStyleBackColor = True
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(444, 41)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(21, 13)
    Me.Label4.TabIndex = 97
    Me.Label4.Text = "Do"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(191, 41)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(21, 13)
    Me.Label5.TabIndex = 96
    Me.Label5.Text = "Od"
    '
    'dtDataOd
    '
    Me.dtDataOd.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtDataOd.Enabled = False
    Me.dtDataOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtDataOd.Location = New System.Drawing.Point(218, 37)
    Me.dtDataOd.Name = "dtDataOd"
    Me.dtDataOd.Size = New System.Drawing.Size(220, 20)
    Me.dtDataOd.TabIndex = 95
    '
    'dtDataDo
    '
    Me.dtDataDo.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtDataDo.Enabled = False
    Me.dtDataDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtDataDo.Location = New System.Drawing.Point(471, 37)
    Me.dtDataDo.Name = "dtDataDo"
    Me.dtDataDo.Size = New System.Drawing.Size(220, 20)
    Me.dtDataDo.TabIndex = 94
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label14)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.lblData)
    Me.Panel1.Controls.Add(Me.lblIP)
    Me.Panel1.Controls.Add(Me.lblUser)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Location = New System.Drawing.Point(12, 492)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(802, 37)
    Me.Panel1.TabIndex = 211
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(587, 16)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 147
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Enabled = False
    Me.Label1.Location = New System.Drawing.Point(424, 16)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(31, 13)
    Me.Label1.TabIndex = 146
    Me.Label1.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(678, 11)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(121, 23)
    Me.lblData.TabIndex = 145
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(461, 11)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(120, 23)
    Me.lblIP.TabIndex = 143
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 11)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(335, 23)
    Me.lblUser.TabIndex = 144
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Enabled = False
    Me.Label2.Location = New System.Drawing.Point(3, 16)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(74, 13)
    Me.Label2.TabIndex = 142
    Me.Label2.Text = "Zmodyfikował"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(697, 451)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 210
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.Location = New System.Drawing.Point(697, 105)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(117, 36)
    Me.cmdEdit.TabIndex = 208
    Me.cmdEdit.Text = "&Edytuj uwagę"
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.Location = New System.Drawing.Point(697, 63)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddNew.TabIndex = 209
    Me.cmdAddNew.Text = "&Nowa uwaga"
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(697, 147)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 207
    Me.cmdDelete.Text = "&Usuń uwagę"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'chkVirtual
    '
    Me.chkVirtual.AutoSize = True
    Me.chkVirtual.Location = New System.Drawing.Point(354, 14)
    Me.chkVirtual.Name = "chkVirtual"
    Me.chkVirtual.Size = New System.Drawing.Size(97, 17)
    Me.chkVirtual.TabIndex = 215
    Me.chkVirtual.Text = "Klasa wirtualna"
    Me.chkVirtual.UseVisualStyleBackColor = True
    '
    'frmUwagi
    '
    Me.ClientSize = New System.Drawing.Size(825, 541)
    Me.Controls.Add(Me.chkVirtual)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.dtDataOd)
    Me.Controls.Add(Me.dtDataDo)
    Me.Controls.Add(Me.chkGrupa)
    Me.Controls.Add(Me.Label12)
    Me.Controls.Add(Me.lblNegatywne)
    Me.Controls.Add(Me.lblPozytywne)
    Me.Controls.Add(Me.Label11)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.lblRekord)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtTrescUwagi)
    Me.Controls.Add(Me.lvUwagi)
    Me.Controls.Add(Me.lvUczen)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label7)
    Me.Name = "frmUwagi"
    Me.Text = "Uwagi o zachowaniu"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblNegatywne As System.Windows.Forms.Label
  Friend WithEvents lblPozytywne As System.Windows.Forms.Label
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents lblRekord As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtTrescUwagi As System.Windows.Forms.TextBox
  Friend WithEvents lvUwagi As System.Windows.Forms.ListView
  Friend WithEvents lvUczen As System.Windows.Forms.ListView
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents chkGrupa As System.Windows.Forms.CheckBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents dtDataOd As System.Windows.Forms.DateTimePicker
  Friend WithEvents dtDataDo As System.Windows.Forms.DateTimePicker
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents chkVirtual As System.Windows.Forms.CheckBox
End Class
