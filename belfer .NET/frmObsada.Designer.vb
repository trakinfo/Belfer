<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmObsada
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmObsada))
    Me.cbObsadaFilter = New System.Windows.Forms.ComboBox()
    Me.lvObsada = New System.Windows.Forms.ListView()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.chkVirtual = New System.Windows.Forms.CheckBox()
    Me.rbKlasa = New System.Windows.Forms.RadioButton()
    Me.rbPrzedmiot = New System.Windows.Forms.RadioButton()
    Me.rbNauczyciel = New System.Windows.Forms.RadioButton()
    Me.cmdDeaktywacja = New System.Windows.Forms.Button()
    Me.cmdEdit = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lblSuma = New System.Windows.Forms.Label()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'cbObsadaFilter
    '
    Me.cbObsadaFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbObsadaFilter.Enabled = False
    Me.cbObsadaFilter.FormattingEnabled = True
    Me.cbObsadaFilter.Location = New System.Drawing.Point(286, 10)
    Me.cbObsadaFilter.Name = "cbObsadaFilter"
    Me.cbObsadaFilter.Size = New System.Drawing.Size(369, 21)
    Me.cbObsadaFilter.TabIndex = 182
    '
    'lvObsada
    '
    Me.lvObsada.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvObsada.Location = New System.Drawing.Point(12, 39)
    Me.lvObsada.Name = "lvObsada"
    Me.lvObsada.Size = New System.Drawing.Size(820, 385)
    Me.lvObsada.TabIndex = 184
    Me.lvObsada.UseCompatibleStateImageBehavior = False
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.lblSuma)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(12, 426)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(916, 25)
    Me.Panel1.TabIndex = 185
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 3)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 143
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 3)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 144
    Me.lblRecord.Text = "lblRecord"
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
    Me.Panel2.Location = New System.Drawing.Point(12, 455)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(916, 29)
    Me.Panel2.TabIndex = 191
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(648, 8)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 147
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(505, 8)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 146
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(739, 3)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(121, 23)
    Me.lblData.TabIndex = 145
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(542, 3)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 143
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(83, 3)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(416, 23)
    Me.lblUser.TabIndex = 144
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(3, 8)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 142
    Me.Label3.Text = "Zmodyfikował"
    '
    'chkVirtual
    '
    Me.chkVirtual.AutoSize = True
    Me.chkVirtual.Location = New System.Drawing.Point(661, 13)
    Me.chkVirtual.Name = "chkVirtual"
    Me.chkVirtual.Size = New System.Drawing.Size(141, 17)
    Me.chkVirtual.TabIndex = 198
    Me.chkVirtual.Text = "Nauczanie indywidualne"
    Me.chkVirtual.UseVisualStyleBackColor = True
    '
    'rbKlasa
    '
    Me.rbKlasa.AutoSize = True
    Me.rbKlasa.Checked = True
    Me.rbKlasa.Location = New System.Drawing.Point(12, 12)
    Me.rbKlasa.Name = "rbKlasa"
    Me.rbKlasa.Size = New System.Drawing.Size(61, 17)
    Me.rbKlasa.TabIndex = 199
    Me.rbKlasa.TabStop = True
    Me.rbKlasa.Tag = "Klasa"
    Me.rbKlasa.Text = "wg klas"
    Me.rbKlasa.UseVisualStyleBackColor = True
    '
    'rbPrzedmiot
    '
    Me.rbPrzedmiot.AutoSize = True
    Me.rbPrzedmiot.Location = New System.Drawing.Point(79, 12)
    Me.rbPrzedmiot.Name = "rbPrzedmiot"
    Me.rbPrzedmiot.Size = New System.Drawing.Size(101, 17)
    Me.rbPrzedmiot.TabIndex = 200
    Me.rbPrzedmiot.Tag = "Przedmiot"
    Me.rbPrzedmiot.Text = "wg przedmiotów"
    Me.rbPrzedmiot.UseVisualStyleBackColor = True
    '
    'rbNauczyciel
    '
    Me.rbNauczyciel.AutoSize = True
    Me.rbNauczyciel.Location = New System.Drawing.Point(186, 12)
    Me.rbNauczyciel.Name = "rbNauczyciel"
    Me.rbNauczyciel.Size = New System.Drawing.Size(94, 17)
    Me.rbNauczyciel.TabIndex = 201
    Me.rbNauczyciel.Tag = "Belfer"
    Me.rbNauczyciel.Text = "wg nauczycieli"
    Me.rbNauczyciel.UseVisualStyleBackColor = True
    '
    'cmdDeaktywacja
    '
    Me.cmdDeaktywacja.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDeaktywacja.Enabled = False
    Me.cmdDeaktywacja.Image = CType(resources.GetObject("cmdDeaktywacja.Image"), System.Drawing.Image)
    Me.cmdDeaktywacja.Location = New System.Drawing.Point(836, 120)
    Me.cmdDeaktywacja.Name = "cmdDeaktywacja"
    Me.cmdDeaktywacja.Size = New System.Drawing.Size(92, 34)
    Me.cmdDeaktywacja.TabIndex = 202
    Me.cmdDeaktywacja.Text = "Deaktywu&j"
    Me.cmdDeaktywacja.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDeaktywacja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDeaktywacja.UseVisualStyleBackColor = True
    '
    'cmdEdit
    '
    Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEdit.Enabled = False
    Me.cmdEdit.Image = Global.belfer.NET.My.Resources.Resources.edit
    Me.cmdEdit.Location = New System.Drawing.Point(836, 80)
    Me.cmdEdit.Name = "cmdEdit"
    Me.cmdEdit.Size = New System.Drawing.Size(92, 34)
    Me.cmdEdit.TabIndex = 190
    Me.cmdEdit.Text = "&Edytuj"
    Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdEdit.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
    Me.cmdClose.Location = New System.Drawing.Point(837, 389)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(91, 35)
    Me.cmdClose.TabIndex = 189
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
    Me.cmdDelete.Location = New System.Drawing.Point(836, 160)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(92, 35)
    Me.cmdDelete.TabIndex = 188
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = CType(resources.GetObject("cmdAddNew.Image"), System.Drawing.Image)
    Me.cmdAddNew.Location = New System.Drawing.Point(837, 39)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(91, 35)
    Me.cmdAddNew.TabIndex = 187
    Me.cmdAddNew.Text = "&Dodaj"
    Me.cmdAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(654, 3)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(110, 13)
    Me.Label1.TabIndex = 145
    Me.Label1.Text = "Łączna liczba godzin:"
    '
    'lblSuma
    '
    Me.lblSuma.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblSuma.ForeColor = System.Drawing.Color.Green
    Me.lblSuma.Location = New System.Drawing.Point(770, 3)
    Me.lblSuma.Name = "lblSuma"
    Me.lblSuma.Size = New System.Drawing.Size(50, 13)
    Me.lblSuma.TabIndex = 146
    Me.lblSuma.Text = "Suma"
    Me.lblSuma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'frmObsada
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(934, 491)
    Me.Controls.Add(Me.cmdDeaktywacja)
    Me.Controls.Add(Me.rbNauczyciel)
    Me.Controls.Add(Me.rbPrzedmiot)
    Me.Controls.Add(Me.rbKlasa)
    Me.Controls.Add(Me.chkVirtual)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.cmdEdit)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.lvObsada)
    Me.Controls.Add(Me.cbObsadaFilter)
    Me.Name = "frmObsada"
    Me.Text = "Obsada przedmiotów"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cbObsadaFilter As System.Windows.Forms.ComboBox
  Friend WithEvents lvObsada As System.Windows.Forms.ListView
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdEdit As System.Windows.Forms.Button
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents chkVirtual As System.Windows.Forms.CheckBox
  Friend WithEvents rbKlasa As System.Windows.Forms.RadioButton
  Friend WithEvents rbPrzedmiot As System.Windows.Forms.RadioButton
  Friend WithEvents rbNauczyciel As System.Windows.Forms.RadioButton
  Friend WithEvents cmdDeaktywacja As System.Windows.Forms.Button
  Friend WithEvents lblSuma As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
