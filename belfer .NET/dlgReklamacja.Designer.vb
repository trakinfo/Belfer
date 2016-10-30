<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgReklamacja
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.lblUczen = New System.Windows.Forms.Label()
    Me.lblLekcja = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblTyp = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtUczen = New System.Windows.Forms.TextBox()
    Me.txtData = New System.Windows.Forms.TextBox()
    Me.txtLekcja = New System.Windows.Forms.TextBox()
    Me.txtTyp = New System.Windows.Forms.TextBox()
    Me.txtKomentarz = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.txtOwner = New System.Windows.Forms.TextBox()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(179, 211)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Anuluj"
    '
    'lblUczen
    '
    Me.lblUczen.AutoSize = True
    Me.lblUczen.Location = New System.Drawing.Point(12, 121)
    Me.lblUczen.Name = "lblUczen"
    Me.lblUczen.Size = New System.Drawing.Size(38, 13)
    Me.lblUczen.TabIndex = 1
    Me.lblUczen.Text = "Uczeń"
    '
    'lblLekcja
    '
    Me.lblLekcja.AutoSize = True
    Me.lblLekcja.Location = New System.Drawing.Point(12, 147)
    Me.lblLekcja.Name = "lblLekcja"
    Me.lblLekcja.Size = New System.Drawing.Size(39, 13)
    Me.lblLekcja.TabIndex = 2
    Me.lblLekcja.Text = "Lekcja"
    '
    'lblData
    '
    Me.lblData.AutoSize = True
    Me.lblData.Location = New System.Drawing.Point(12, 173)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(30, 13)
    Me.lblData.TabIndex = 3
    Me.lblData.Text = "Data"
    '
    'lblTyp
    '
    Me.lblTyp.AutoSize = True
    Me.lblTyp.Location = New System.Drawing.Point(181, 173)
    Me.lblTyp.Name = "lblTyp"
    Me.lblTyp.Size = New System.Drawing.Size(91, 13)
    Me.lblTyp.TabIndex = 4
    Me.lblTyp.Text = "Typ nieobecności"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 41)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(57, 13)
    Me.Label1.TabIndex = 5
    Me.Label1.Text = "Komentarz"
    '
    'txtUczen
    '
    Me.txtUczen.Enabled = False
    Me.txtUczen.Location = New System.Drawing.Point(75, 118)
    Me.txtUczen.Name = "txtUczen"
    Me.txtUczen.ReadOnly = True
    Me.txtUczen.Size = New System.Drawing.Size(248, 20)
    Me.txtUczen.TabIndex = 6
    '
    'txtData
    '
    Me.txtData.Enabled = False
    Me.txtData.Location = New System.Drawing.Point(75, 170)
    Me.txtData.Name = "txtData"
    Me.txtData.ReadOnly = True
    Me.txtData.Size = New System.Drawing.Size(100, 20)
    Me.txtData.TabIndex = 7
    '
    'txtLekcja
    '
    Me.txtLekcja.Enabled = False
    Me.txtLekcja.Location = New System.Drawing.Point(75, 144)
    Me.txtLekcja.Name = "txtLekcja"
    Me.txtLekcja.ReadOnly = True
    Me.txtLekcja.Size = New System.Drawing.Size(248, 20)
    Me.txtLekcja.TabIndex = 8
    '
    'txtTyp
    '
    Me.txtTyp.Enabled = False
    Me.txtTyp.Location = New System.Drawing.Point(278, 170)
    Me.txtTyp.Name = "txtTyp"
    Me.txtTyp.ReadOnly = True
    Me.txtTyp.Size = New System.Drawing.Size(45, 20)
    Me.txtTyp.TabIndex = 9
    '
    'txtKomentarz
    '
    Me.txtKomentarz.Location = New System.Drawing.Point(75, 38)
    Me.txtKomentarz.MaxLength = 45
    Me.txtKomentarz.Multiline = True
    Me.txtKomentarz.Name = "txtKomentarz"
    Me.txtKomentarz.Size = New System.Drawing.Size(248, 62)
    Me.txtKomentarz.TabIndex = 10
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(43, 13)
    Me.Label2.TabIndex = 11
    Me.Label2.Text = "Adresat"
    '
    'txtOwner
    '
    Me.txtOwner.Enabled = False
    Me.txtOwner.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.txtOwner.ForeColor = System.Drawing.Color.Firebrick
    Me.txtOwner.Location = New System.Drawing.Point(75, 12)
    Me.txtOwner.Name = "txtOwner"
    Me.txtOwner.ReadOnly = True
    Me.txtOwner.Size = New System.Drawing.Size(248, 20)
    Me.txtOwner.TabIndex = 12
    '
    'dlgReklamacja
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(337, 252)
    Me.Controls.Add(Me.txtOwner)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtKomentarz)
    Me.Controls.Add(Me.txtTyp)
    Me.Controls.Add(Me.txtLekcja)
    Me.Controls.Add(Me.txtData)
    Me.Controls.Add(Me.txtUczen)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lblTyp)
    Me.Controls.Add(Me.lblData)
    Me.Controls.Add(Me.lblLekcja)
    Me.Controls.Add(Me.lblUczen)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgReklamacja"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Powiadomienie autora wpisu"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents lblUczen As System.Windows.Forms.Label
  Friend WithEvents lblLekcja As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblTyp As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtUczen As System.Windows.Forms.TextBox
  Friend WithEvents txtData As System.Windows.Forms.TextBox
  Friend WithEvents txtLekcja As System.Windows.Forms.TextBox
  Friend WithEvents txtTyp As System.Windows.Forms.TextBox
  Friend WithEvents txtKomentarz As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtOwner As System.Windows.Forms.TextBox

End Class
