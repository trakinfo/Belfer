<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParentLogging
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
    Me.Label8 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.cbStudent = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.dtDataOd = New System.Windows.Forms.DateTimePicker()
    Me.dtDataDo = New System.Windows.Forms.DateTimePicker()
    Me.SuspendLayout()
    '
    'lvEvents
    '
    Me.lvEvents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvEvents.Location = New System.Drawing.Point(12, 39)
    Me.lvEvents.Name = "lvEvents"
    Me.lvEvents.ShowItemToolTips = True
    Me.lvEvents.Size = New System.Drawing.Size(732, 406)
    Me.lvEvents.TabIndex = 37
    Me.lvEvents.UseCompatibleStateImageBehavior = False
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
    Me.cmdClose.Location = New System.Drawing.Point(627, 476)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 223
    Me.cmdClose.Text = " &Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.Enabled = False
    Me.cmdRefresh.Image = Global.belfer.NET.My.Resources.Resources.refresh_24
    Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdRefresh.Location = New System.Drawing.Point(504, 477)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(117, 36)
    Me.cmdRefresh.TabIndex = 229
    Me.cmdRefresh.Text = "Odśwież"
    Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'cbStudent
    '
    Me.cbStudent.DropDownHeight = 500
    Me.cbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbStudent.Enabled = False
    Me.cbStudent.FormattingEnabled = True
    Me.cbStudent.IntegralHeight = False
    Me.cbStudent.Location = New System.Drawing.Point(312, 12)
    Me.cbStudent.Name = "cbStudent"
    Me.cbStudent.Size = New System.Drawing.Size(240, 21)
    Me.cbStudent.TabIndex = 231
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(268, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(38, 13)
    Me.Label2.TabIndex = 230
    Me.Label2.Text = "Uczeń"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(51, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(211, 21)
    Me.cbKlasa.TabIndex = 233
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(33, 13)
    Me.Label1.TabIndex = 232
    Me.Label1.Text = "Klasa"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(257, 489)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(21, 13)
    Me.Label4.TabIndex = 237
    Me.Label4.Text = "Do"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(15, 489)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(21, 13)
    Me.Label5.TabIndex = 236
    Me.Label5.Text = "Od"
    '
    'dtDataOd
    '
    Me.dtDataOd.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtDataOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtDataOd.Location = New System.Drawing.Point(42, 485)
    Me.dtDataOd.Name = "dtDataOd"
    Me.dtDataOd.Size = New System.Drawing.Size(209, 20)
    Me.dtDataOd.TabIndex = 235
    '
    'dtDataDo
    '
    Me.dtDataDo.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtDataDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtDataDo.Location = New System.Drawing.Point(284, 485)
    Me.dtDataDo.Name = "dtDataDo"
    Me.dtDataDo.Size = New System.Drawing.Size(209, 20)
    Me.dtDataDo.TabIndex = 234
    '
    'frmParentLogging
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(756, 523)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.dtDataOd)
    Me.Controls.Add(Me.dtDataDo)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cbStudent)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.lblRecord)
    Me.Controls.Add(Me.lvEvents)
    Me.Name = "frmParentLogging"
    Me.Text = "Aktywność rodziców"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lvEvents As System.Windows.Forms.ListView
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents cbStudent As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents dtDataOd As System.Windows.Forms.DateTimePicker
  Friend WithEvents dtDataDo As System.Windows.Forms.DateTimePicker
End Class
