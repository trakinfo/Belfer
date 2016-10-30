<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPromocja
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPromocja))
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.lblRecord1 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.gpbOpcje = New System.Windows.Forms.GroupBox()
    Me.rbUnselected = New System.Windows.Forms.RadioButton()
    Me.rbSelected = New System.Windows.Forms.RadioButton()
    Me.gpbZakres = New System.Windows.Forms.GroupBox()
    Me.rbWybranaKlasa = New System.Windows.Forms.RadioButton()
    Me.rbWszystkieKlasy = New System.Windows.Forms.RadioButton()
    Me.lvNiepromowani = New System.Windows.Forms.ListView()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.lvPromowani = New System.Windows.Forms.ListView()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.cmdAdd = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.Panel1.SuspendLayout()
    Me.gpbOpcje.SuspendLayout()
    Me.gpbZakres.SuspendLayout()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.lblRecord1)
    Me.Panel1.Controls.Add(Me.Label10)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.lblRecord)
    Me.Panel1.Location = New System.Drawing.Point(9, 447)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(696, 21)
    Me.Panel1.TabIndex = 166
    '
    'lblRecord1
    '
    Me.lblRecord1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord1.AutoSize = True
    Me.lblRecord1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord1.ForeColor = System.Drawing.Color.Green
    Me.lblRecord1.Location = New System.Drawing.Point(445, 3)
    Me.lblRecord1.Name = "lblRecord1"
    Me.lblRecord1.Size = New System.Drawing.Size(68, 13)
    Me.lblRecord1.TabIndex = 146
    Me.lblRecord1.Text = "lblRecord1"
    '
    'Label10
    '
    Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(394, 3)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(45, 13)
    Me.Label10.TabIndex = 145
    Me.Label10.Text = "Rekord:"
    '
    'Label8
    '
    Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(3, 3)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 143
    Me.Label8.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(54, 3)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 144
    Me.lblRecord.Text = "lblRecord"
    '
    'gpbOpcje
    '
    Me.gpbOpcje.Controls.Add(Me.rbUnselected)
    Me.gpbOpcje.Controls.Add(Me.rbSelected)
    Me.gpbOpcje.Location = New System.Drawing.Point(714, 174)
    Me.gpbOpcje.Name = "gpbOpcje"
    Me.gpbOpcje.Size = New System.Drawing.Size(121, 101)
    Me.gpbOpcje.TabIndex = 165
    Me.gpbOpcje.TabStop = False
    Me.gpbOpcje.Text = "Opcje"
    '
    'rbUnselected
    '
    Me.rbUnselected.Location = New System.Drawing.Point(6, 57)
    Me.rbUnselected.Name = "rbUnselected"
    Me.rbUnselected.Size = New System.Drawing.Size(109, 34)
    Me.rbUnselected.TabIndex = 1
    Me.rbUnselected.Text = "Wykonaj dla niezaznaczonych"
    Me.rbUnselected.UseVisualStyleBackColor = True
    '
    'rbSelected
    '
    Me.rbSelected.Checked = True
    Me.rbSelected.Location = New System.Drawing.Point(6, 19)
    Me.rbSelected.Name = "rbSelected"
    Me.rbSelected.Size = New System.Drawing.Size(104, 32)
    Me.rbSelected.TabIndex = 0
    Me.rbSelected.TabStop = True
    Me.rbSelected.Text = "Wykonaj dla zaznaczonych"
    Me.rbSelected.UseVisualStyleBackColor = True
    '
    'gpbZakres
    '
    Me.gpbZakres.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gpbZakres.Controls.Add(Me.rbWybranaKlasa)
    Me.gpbZakres.Controls.Add(Me.rbWszystkieKlasy)
    Me.gpbZakres.Location = New System.Drawing.Point(714, 68)
    Me.gpbZakres.Name = "gpbZakres"
    Me.gpbZakres.Size = New System.Drawing.Size(121, 71)
    Me.gpbZakres.TabIndex = 164
    Me.gpbZakres.TabStop = False
    Me.gpbZakres.Text = "Zakres wyświetania"
    '
    'rbWybranaKlasa
    '
    Me.rbWybranaKlasa.AutoSize = True
    Me.rbWybranaKlasa.Location = New System.Drawing.Point(6, 42)
    Me.rbWybranaKlasa.Name = "rbWybranaKlasa"
    Me.rbWybranaKlasa.Size = New System.Drawing.Size(96, 17)
    Me.rbWybranaKlasa.TabIndex = 1
    Me.rbWybranaKlasa.Text = "Wybrana klasa"
    Me.rbWybranaKlasa.UseVisualStyleBackColor = True
    '
    'rbWszystkieKlasy
    '
    Me.rbWszystkieKlasy.AutoSize = True
    Me.rbWszystkieKlasy.Checked = True
    Me.rbWszystkieKlasy.Location = New System.Drawing.Point(6, 19)
    Me.rbWszystkieKlasy.Name = "rbWszystkieKlasy"
    Me.rbWszystkieKlasy.Size = New System.Drawing.Size(100, 17)
    Me.rbWszystkieKlasy.TabIndex = 0
    Me.rbWszystkieKlasy.TabStop = True
    Me.rbWszystkieKlasy.Text = "Wszystkie klasy"
    Me.rbWszystkieKlasy.UseVisualStyleBackColor = True
    '
    'lvNiepromowani
    '
    Me.lvNiepromowani.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lvNiepromowani.BackColor = System.Drawing.SystemColors.Window
    Me.lvNiepromowani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.lvNiepromowani.HideSelection = False
    Me.lvNiepromowani.Location = New System.Drawing.Point(9, 39)
    Me.lvNiepromowani.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.lvNiepromowani.Name = "lvNiepromowani"
    Me.lvNiepromowani.Size = New System.Drawing.Size(309, 404)
    Me.lvNiepromowani.TabIndex = 163
    Me.lvNiepromowani.UseCompatibleStateImageBehavior = False
    '
    'Label14
    '
    Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label14.AutoSize = True
    Me.Label14.Enabled = False
    Me.Label14.Location = New System.Drawing.Point(596, 483)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(85, 13)
    Me.Label14.TabIndex = 160
    Me.Label14.Text = "Data modyfikacji"
    '
    'Label12
    '
    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label12.AutoSize = True
    Me.Label12.Enabled = False
    Me.Label12.Location = New System.Drawing.Point(433, 483)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(31, 13)
    Me.Label12.TabIndex = 159
    Me.Label12.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(687, 478)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(148, 23)
    Me.lblData.TabIndex = 158
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(470, 478)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(120, 23)
    Me.lblIP.TabIndex = 156
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(87, 478)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(340, 23)
    Me.lblUser.TabIndex = 157
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Enabled = False
    Me.Label3.Location = New System.Drawing.Point(7, 483)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 155
    Me.Label3.Text = "Zmodyfikował"
    '
    'lvPromowani
    '
    Me.lvPromowani.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lvPromowani.ForeColor = System.Drawing.Color.Green
    Me.lvPromowani.HideSelection = False
    Me.lvPromowani.Location = New System.Drawing.Point(396, 39)
    Me.lvPromowani.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
    Me.lvPromowani.Name = "lvPromowani"
    Me.lvPromowani.Size = New System.Drawing.Size(309, 404)
    Me.lvPromowani.TabIndex = 152
    Me.lvPromowani.UseCompatibleStateImageBehavior = False
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(6, 15)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(33, 13)
    Me.Label4.TabIndex = 168
    Me.Label4.Text = "Klasa"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(45, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(273, 21)
    Me.cbKlasa.TabIndex = 167
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
    Me.cmdDelete.Location = New System.Drawing.Point(323, 232)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(67, 45)
    Me.cmdDelete.TabIndex = 170
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'cmdAdd
    '
    Me.cmdAdd.Enabled = False
    Me.cmdAdd.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
    Me.cmdAdd.Location = New System.Drawing.Point(324, 181)
    Me.cmdAdd.Name = "cmdAdd"
    Me.cmdAdd.Size = New System.Drawing.Size(66, 45)
    Me.cmdAdd.TabIndex = 169
    Me.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAdd.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(714, 411)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(121, 30)
    Me.cmdClose.TabIndex = 171
    Me.cmdClose.Text = "   &Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'frmPromocja
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(842, 512)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdAdd)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.gpbOpcje)
    Me.Controls.Add(Me.gpbZakres)
    Me.Controls.Add(Me.lvNiepromowani)
    Me.Controls.Add(Me.Label14)
    Me.Controls.Add(Me.Label12)
    Me.Controls.Add(Me.lblData)
    Me.Controls.Add(Me.lblIP)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.lvPromowani)
    Me.Name = "frmPromocja"
    Me.Text = "Promocja uczniów"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.gpbOpcje.ResumeLayout(False)
    Me.gpbZakres.ResumeLayout(False)
    Me.gpbZakres.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents lblRecord1 As System.Windows.Forms.Label
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents gpbOpcje As System.Windows.Forms.GroupBox
  Friend WithEvents rbUnselected As System.Windows.Forms.RadioButton
  Friend WithEvents rbSelected As System.Windows.Forms.RadioButton
  Friend WithEvents gpbZakres As System.Windows.Forms.GroupBox
  Friend WithEvents rbWybranaKlasa As System.Windows.Forms.RadioButton
  Friend WithEvents rbWszystkieKlasy As System.Windows.Forms.RadioButton
  Friend WithEvents lvNiepromowani As System.Windows.Forms.ListView
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lvPromowani As System.Windows.Forms.ListView
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents cmdAdd As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
End Class
