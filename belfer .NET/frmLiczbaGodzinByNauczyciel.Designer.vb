<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLiczbaGodzinByNauczyciel
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
    Me.cbNauczyciel = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.lvLiczbaGodzin = New System.Windows.Forms.ListView()
    Me.SuspendLayout
    '
    'cbNauczyciel
    '
    Me.cbNauczyciel.DropDownHeight = 500
    Me.cbNauczyciel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbNauczyciel.Enabled = false
    Me.cbNauczyciel.FormattingEnabled = true
    Me.cbNauczyciel.IntegralHeight = false
    Me.cbNauczyciel.Location = New System.Drawing.Point(74, 12)
    Me.cbNauczyciel.Name = "cbNauczyciel"
    Me.cbNauczyciel.Size = New System.Drawing.Size(341, 21)
    Me.cbNauczyciel.TabIndex = 31
    '
    'Label2
    '
    Me.Label2.AutoSize = true
    Me.Label2.Location = New System.Drawing.Point(9, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(59, 13)
    Me.Label2.TabIndex = 30
    Me.Label2.Text = "Nauczyciel"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(692, 419)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(119, 35)
    Me.cmdClose.TabIndex = 34
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(692, 39)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(119, 35)
    Me.cmdPrint.TabIndex = 123
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'lvLiczbaGodzin
    '
    Me.lvLiczbaGodzin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvLiczbaGodzin.Location = New System.Drawing.Point(12, 39)
    Me.lvLiczbaGodzin.Name = "lvLiczbaGodzin"
    Me.lvLiczbaGodzin.Size = New System.Drawing.Size(670, 415)
    Me.lvLiczbaGodzin.TabIndex = 124
    Me.lvLiczbaGodzin.UseCompatibleStateImageBehavior = False
    '
    'frmLiczbaGodzinByNauczyciel
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(823, 466)
    Me.Controls.Add(Me.lvLiczbaGodzin)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cbNauczyciel)
    Me.Controls.Add(Me.Label2)
    Me.Name = "frmLiczbaGodzinByNauczyciel"
    Me.Text = "Godzinowa realizacja planu nauczania"
    Me.ResumeLayout(false)
    Me.PerformLayout

End Sub
  Friend WithEvents cbNauczyciel As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents lvLiczbaGodzin As System.Windows.Forms.ListView
End Class
