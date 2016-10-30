<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZdarzenia
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
    Me.lvEvents = New System.Windows.Forms.ListView()
    Me.txtSeek = New System.Windows.Forms.TextBox()
    Me.cbSeek = New System.Windows.Forms.ComboBox()
    Me.lblFilter = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.gbRola = New System.Windows.Forms.GroupBox()
    Me.rbAdmin = New System.Windows.Forms.RadioButton()
    Me.rbRodzic = New System.Windows.Forms.RadioButton()
    Me.rbAllUsers = New System.Windows.Forms.RadioButton()
    Me.rbNauczyciel = New System.Windows.Forms.RadioButton()
    Me.gbStatus = New System.Windows.Forms.GroupBox()
    Me.rbNieaktywny = New System.Windows.Forms.RadioButton()
    Me.rbAllStatus = New System.Windows.Forms.RadioButton()
    Me.rbAktywny = New System.Windows.Forms.RadioButton()
    Me.gbData = New System.Windows.Forms.GroupBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.dtDataOd = New System.Windows.Forms.DateTimePicker()
    Me.dtDataDo = New System.Windows.Forms.DateTimePicker()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.gbRola.SuspendLayout()
    Me.gbStatus.SuspendLayout()
    Me.gbData.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvEvents
    '
    Me.lvEvents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvEvents.Location = New System.Drawing.Point(12, 54)
    Me.lvEvents.Name = "lvEvents"
    Me.lvEvents.ShowItemToolTips = True
    Me.lvEvents.Size = New System.Drawing.Size(1041, 391)
    Me.lvEvents.TabIndex = 37
    Me.lvEvents.UseCompatibleStateImageBehavior = False
    '
    'txtSeek
    '
    Me.txtSeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSeek.Location = New System.Drawing.Point(734, 451)
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
    Me.cbSeek.Location = New System.Drawing.Point(483, 450)
    Me.cbSeek.Name = "cbSeek"
    Me.cbSeek.Size = New System.Drawing.Size(245, 21)
    Me.cbSeek.TabIndex = 158
    '
    'lblFilter
    '
    Me.lblFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFilter.AutoSize = True
    Me.lblFilter.Location = New System.Drawing.Point(429, 454)
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
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(936, 476)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 223
    Me.cmdClose.Text = " &Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'gbRola
    '
    Me.gbRola.Controls.Add(Me.rbAdmin)
    Me.gbRola.Controls.Add(Me.rbRodzic)
    Me.gbRola.Controls.Add(Me.rbAllUsers)
    Me.gbRola.Controls.Add(Me.rbNauczyciel)
    Me.gbRola.Location = New System.Drawing.Point(12, 5)
    Me.gbRola.Name = "gbRola"
    Me.gbRola.Size = New System.Drawing.Size(287, 43)
    Me.gbRola.TabIndex = 226
    Me.gbRola.TabStop = False
    Me.gbRola.Text = "Rola"
    '
    'rbAdmin
    '
    Me.rbAdmin.AutoSize = True
    Me.rbAdmin.Location = New System.Drawing.Point(153, 19)
    Me.rbAdmin.Name = "rbAdmin"
    Me.rbAdmin.Size = New System.Drawing.Size(54, 17)
    Me.rbAdmin.TabIndex = 6
    Me.rbAdmin.TabStop = True
    Me.rbAdmin.Tag = "AND Role IN (4)"
    Me.rbAdmin.Text = "Admin"
    Me.rbAdmin.UseVisualStyleBackColor = True
    '
    'rbRodzic
    '
    Me.rbRodzic.AutoSize = True
    Me.rbRodzic.Location = New System.Drawing.Point(89, 19)
    Me.rbRodzic.Name = "rbRodzic"
    Me.rbRodzic.Size = New System.Drawing.Size(58, 17)
    Me.rbRodzic.TabIndex = 5
    Me.rbRodzic.TabStop = True
    Me.rbRodzic.Tag = "AND Role IN (0)"
    Me.rbRodzic.Text = "Rodzic"
    Me.rbRodzic.UseVisualStyleBackColor = True
    '
    'rbAllUsers
    '
    Me.rbAllUsers.AutoSize = True
    Me.rbAllUsers.Checked = True
    Me.rbAllUsers.Location = New System.Drawing.Point(213, 19)
    Me.rbAllUsers.Name = "rbAllUsers"
    Me.rbAllUsers.Size = New System.Drawing.Size(67, 17)
    Me.rbAllUsers.TabIndex = 2
    Me.rbAllUsers.TabStop = True
    Me.rbAllUsers.Tag = "AND Role IN (0,1,2,4)"
    Me.rbAllUsers.Text = "Wszyscy"
    Me.rbAllUsers.UseVisualStyleBackColor = True
    '
    'rbNauczyciel
    '
    Me.rbNauczyciel.AutoSize = True
    Me.rbNauczyciel.Location = New System.Drawing.Point(6, 19)
    Me.rbNauczyciel.Name = "rbNauczyciel"
    Me.rbNauczyciel.Size = New System.Drawing.Size(77, 17)
    Me.rbNauczyciel.TabIndex = 0
    Me.rbNauczyciel.Tag = "AND Role IN (2)"
    Me.rbNauczyciel.Text = "Nauczyciel"
    Me.rbNauczyciel.UseVisualStyleBackColor = True
    '
    'gbStatus
    '
    Me.gbStatus.Controls.Add(Me.rbNieaktywny)
    Me.gbStatus.Controls.Add(Me.rbAllStatus)
    Me.gbStatus.Controls.Add(Me.rbAktywny)
    Me.gbStatus.Location = New System.Drawing.Point(319, 5)
    Me.gbStatus.Name = "gbStatus"
    Me.gbStatus.Size = New System.Drawing.Size(233, 43)
    Me.gbStatus.TabIndex = 227
    Me.gbStatus.TabStop = False
    Me.gbStatus.Text = "Status aktywacji"
    '
    'rbNieaktywny
    '
    Me.rbNieaktywny.AutoSize = True
    Me.rbNieaktywny.Location = New System.Drawing.Point(77, 19)
    Me.rbNieaktywny.Name = "rbNieaktywny"
    Me.rbNieaktywny.Size = New System.Drawing.Size(80, 17)
    Me.rbNieaktywny.TabIndex = 5
    Me.rbNieaktywny.TabStop = True
    Me.rbNieaktywny.Tag = "Status IN (0) "
    Me.rbNieaktywny.Text = "Nieaktywny"
    Me.rbNieaktywny.UseVisualStyleBackColor = True
    '
    'rbAllStatus
    '
    Me.rbAllStatus.AutoSize = True
    Me.rbAllStatus.Location = New System.Drawing.Point(163, 19)
    Me.rbAllStatus.Name = "rbAllStatus"
    Me.rbAllStatus.Size = New System.Drawing.Size(67, 17)
    Me.rbAllStatus.TabIndex = 2
    Me.rbAllStatus.Tag = "Status IN (0,1) "
    Me.rbAllStatus.Text = "Wszyscy"
    Me.rbAllStatus.UseVisualStyleBackColor = True
    '
    'rbAktywny
    '
    Me.rbAktywny.AutoSize = True
    Me.rbAktywny.Checked = True
    Me.rbAktywny.Location = New System.Drawing.Point(6, 19)
    Me.rbAktywny.Name = "rbAktywny"
    Me.rbAktywny.Size = New System.Drawing.Size(65, 17)
    Me.rbAktywny.TabIndex = 0
    Me.rbAktywny.TabStop = True
    Me.rbAktywny.Tag = "Status IN (1) "
    Me.rbAktywny.Text = "Aktywny"
    Me.rbAktywny.UseVisualStyleBackColor = True
    '
    'gbData
    '
    Me.gbData.Controls.Add(Me.Label4)
    Me.gbData.Controls.Add(Me.Label5)
    Me.gbData.Controls.Add(Me.dtDataOd)
    Me.gbData.Controls.Add(Me.dtDataDo)
    Me.gbData.Location = New System.Drawing.Point(558, 5)
    Me.gbData.Name = "gbData"
    Me.gbData.Size = New System.Drawing.Size(495, 43)
    Me.gbData.TabIndex = 228
    Me.gbData.TabStop = False
    Me.gbData.Text = "Przedział czasu"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(248, 21)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(21, 13)
    Me.Label4.TabIndex = 101
    Me.Label4.Text = "Do"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(6, 21)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(21, 13)
    Me.Label5.TabIndex = 100
    Me.Label5.Text = "Od"
    '
    'dtDataOd
    '
    Me.dtDataOd.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtDataOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtDataOd.Location = New System.Drawing.Point(33, 17)
    Me.dtDataOd.Name = "dtDataOd"
    Me.dtDataOd.Size = New System.Drawing.Size(209, 20)
    Me.dtDataOd.TabIndex = 99
    '
    'dtDataDo
    '
    Me.dtDataDo.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtDataDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtDataDo.Location = New System.Drawing.Point(275, 17)
    Me.dtDataDo.Name = "dtDataDo"
    Me.dtDataDo.Size = New System.Drawing.Size(209, 20)
    Me.dtDataDo.TabIndex = 98
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.Image = Global.belfer.NET.My.Resources.Resources.refresh_24
    Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdRefresh.Location = New System.Drawing.Point(813, 477)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(117, 36)
    Me.cmdRefresh.TabIndex = 229
    Me.cmdRefresh.Text = "Odśwież"
    Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'frmZdarzenia
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1065, 523)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.gbData)
    Me.Controls.Add(Me.gbStatus)
    Me.Controls.Add(Me.gbRola)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.txtSeek)
    Me.Controls.Add(Me.cbSeek)
    Me.Controls.Add(Me.lblFilter)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.lblRecord)
    Me.Controls.Add(Me.lvEvents)
    Me.Name = "frmZdarzenia"
    Me.Text = "Aktywność użytkowników systemu"
    Me.gbRola.ResumeLayout(False)
    Me.gbRola.PerformLayout()
    Me.gbStatus.ResumeLayout(False)
    Me.gbStatus.PerformLayout()
    Me.gbData.ResumeLayout(False)
    Me.gbData.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lvEvents As System.Windows.Forms.ListView
  Friend WithEvents txtSeek As System.Windows.Forms.TextBox
  Friend WithEvents cbSeek As System.Windows.Forms.ComboBox
  Friend WithEvents lblFilter As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents gbRola As System.Windows.Forms.GroupBox
  Friend WithEvents rbRodzic As System.Windows.Forms.RadioButton
  Friend WithEvents rbAllUsers As System.Windows.Forms.RadioButton
  Friend WithEvents rbNauczyciel As System.Windows.Forms.RadioButton
  Friend WithEvents gbStatus As System.Windows.Forms.GroupBox
  Friend WithEvents rbNieaktywny As System.Windows.Forms.RadioButton
  Friend WithEvents rbAllStatus As System.Windows.Forms.RadioButton
  Friend WithEvents rbAktywny As System.Windows.Forms.RadioButton
  Friend WithEvents gbData As System.Windows.Forms.GroupBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents dtDataOd As System.Windows.Forms.DateTimePicker
  Friend WithEvents dtDataDo As System.Windows.Forms.DateTimePicker
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents rbAdmin As System.Windows.Forms.RadioButton
End Class
