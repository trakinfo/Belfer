<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNauczanieIndywidualne
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
    Me.cbStudent = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.chkZakres = New System.Windows.Forms.CheckBox()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.lvPrzedmiot = New System.Windows.Forms.ListView()
    Me.SuspendLayout()
    '
    'cbStudent
    '
    Me.cbStudent.DropDownHeight = 500
    Me.cbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbStudent.Enabled = False
    Me.cbStudent.FormattingEnabled = True
    Me.cbStudent.IntegralHeight = False
    Me.cbStudent.Location = New System.Drawing.Point(56, 12)
    Me.cbStudent.Name = "cbStudent"
    Me.cbStudent.Size = New System.Drawing.Size(399, 21)
    Me.cbStudent.TabIndex = 212
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(38, 13)
    Me.Label2.TabIndex = 211
    Me.Label2.Text = "Uczeń"
    '
    'chkZakres
    '
    Me.chkZakres.AutoSize = True
    Me.chkZakres.Location = New System.Drawing.Point(461, 14)
    Me.chkZakres.Name = "chkZakres"
    Me.chkZakres.Size = New System.Drawing.Size(100, 17)
    Me.chkZakres.TabIndex = 213
    Me.chkZakres.Text = "Wybrany uczeń"
    Me.chkZakres.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(567, 39)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(119, 35)
    Me.cmdPrint.TabIndex = 215
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(567, 390)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(119, 35)
    Me.cmdClose.TabIndex = 214
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'lvPrzedmiot
    '
    Me.lvPrzedmiot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvPrzedmiot.Location = New System.Drawing.Point(12, 39)
    Me.lvPrzedmiot.Name = "lvPrzedmiot"
    Me.lvPrzedmiot.Size = New System.Drawing.Size(549, 388)
    Me.lvPrzedmiot.TabIndex = 216
    Me.lvPrzedmiot.UseCompatibleStateImageBehavior = False
    '
    'frmNauczanieIndywidualne
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(698, 437)
    Me.Controls.Add(Me.lvPrzedmiot)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.chkZakres)
    Me.Controls.Add(Me.cbStudent)
    Me.Controls.Add(Me.Label2)
    Me.Name = "frmNauczanieIndywidualne"
    Me.Text = "Wykaz uczniów nauczanych indywidualnie"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cbStudent As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents chkZakres As System.Windows.Forms.CheckBox
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents lvPrzedmiot As System.Windows.Forms.ListView
End Class
