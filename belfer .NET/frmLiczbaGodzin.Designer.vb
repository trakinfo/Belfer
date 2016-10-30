<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLiczbaGodzin
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
    Me.cbPrzedmiot = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.lvLiczbaGodzin = New System.Windows.Forms.ListView()
    Me.chkPrzedmiot = New System.Windows.Forms.CheckBox()
    Me.SuspendLayout()
    '
    'cbPrzedmiot
    '
    Me.cbPrzedmiot.DropDownHeight = 500
    Me.cbPrzedmiot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPrzedmiot.Enabled = False
    Me.cbPrzedmiot.FormattingEnabled = True
    Me.cbPrzedmiot.IntegralHeight = False
    Me.cbPrzedmiot.Location = New System.Drawing.Point(68, 39)
    Me.cbPrzedmiot.Name = "cbPrzedmiot"
    Me.cbPrzedmiot.Size = New System.Drawing.Size(277, 21)
    Me.cbPrzedmiot.TabIndex = 33
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(9, 42)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(53, 13)
    Me.Label4.TabIndex = 32
    Me.Label4.Text = "Przedmiot"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(68, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(155, 21)
    Me.cbKlasa.TabIndex = 31
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(9, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 30
    Me.Label2.Text = "Klasa"
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(355, 419)
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
    Me.cmdPrint.Location = New System.Drawing.Point(355, 66)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(119, 35)
    Me.cmdPrint.TabIndex = 123
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'lvLiczbaGodzin
    '
    Me.lvLiczbaGodzin.Location = New System.Drawing.Point(12, 66)
    Me.lvLiczbaGodzin.Name = "lvLiczbaGodzin"
    Me.lvLiczbaGodzin.Size = New System.Drawing.Size(333, 388)
    Me.lvLiczbaGodzin.TabIndex = 124
    Me.lvLiczbaGodzin.UseCompatibleStateImageBehavior = False
    '
    'chkPrzedmiot
    '
    Me.chkPrzedmiot.AutoSize = True
    Me.chkPrzedmiot.Checked = True
    Me.chkPrzedmiot.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkPrzedmiot.Location = New System.Drawing.Point(229, 15)
    Me.chkPrzedmiot.Name = "chkPrzedmiot"
    Me.chkPrzedmiot.Size = New System.Drawing.Size(116, 17)
    Me.chkPrzedmiot.TabIndex = 125
    Me.chkPrzedmiot.Text = "Wybrany przedmiot"
    Me.chkPrzedmiot.UseVisualStyleBackColor = True
    '
    'frmLiczbaGodzin
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(486, 466)
    Me.Controls.Add(Me.chkPrzedmiot)
    Me.Controls.Add(Me.lvLiczbaGodzin)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cbPrzedmiot)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label2)
    Me.Name = "frmLiczbaGodzin"
    Me.Text = "Godzinowa realizacja planu nauczania"
    Me.ResumeLayout(false)
    Me.PerformLayout

End Sub
  Friend WithEvents cbPrzedmiot As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents lvLiczbaGodzin As System.Windows.Forms.ListView
  Friend WithEvents chkPrzedmiot As System.Windows.Forms.CheckBox
End Class
