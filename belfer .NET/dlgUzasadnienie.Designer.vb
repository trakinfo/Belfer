<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgUzasadnienie
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
    Me.txtUzasadnienie = New System.Windows.Forms.TextBox()
    Me.lblStudent = New System.Windows.Forms.Label()
    Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
    Me.lblPrzedmiot = New System.Windows.Forms.Label()
    Me.lvWyniki = New System.Windows.Forms.ListView()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.TableLayoutPanel2.SuspendLayout()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(587, 439)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Zapisz"
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
    'txtUzasadnienie
    '
    Me.txtUzasadnienie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtUzasadnienie.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.txtUzasadnienie.Location = New System.Drawing.Point(12, 29)
    Me.txtUzasadnienie.Multiline = True
    Me.txtUzasadnienie.Name = "txtUzasadnienie"
    Me.txtUzasadnienie.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtUzasadnienie.Size = New System.Drawing.Size(718, 167)
    Me.txtUzasadnienie.TabIndex = 1
    '
    'lblStudent
    '
    Me.lblStudent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblStudent.AutoSize = True
    Me.lblStudent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblStudent.Location = New System.Drawing.Point(3, 0)
    Me.lblStudent.Name = "lblStudent"
    Me.lblStudent.Size = New System.Drawing.Size(353, 25)
    Me.lblStudent.TabIndex = 2
    Me.lblStudent.Text = "Student"
    Me.lblStudent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'TableLayoutPanel2
    '
    Me.TableLayoutPanel2.ColumnCount = 2
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel2.Controls.Add(Me.lblPrzedmiot, 1, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.lblStudent, 0, 0)
    Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 1)
    Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
    Me.TableLayoutPanel2.RowCount = 1
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel2.Size = New System.Drawing.Size(718, 25)
    Me.TableLayoutPanel2.TabIndex = 3
    '
    'lblPrzedmiot
    '
    Me.lblPrzedmiot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblPrzedmiot.AutoSize = True
    Me.lblPrzedmiot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblPrzedmiot.Location = New System.Drawing.Point(362, 0)
    Me.lblPrzedmiot.Name = "lblPrzedmiot"
    Me.lblPrzedmiot.Size = New System.Drawing.Size(353, 25)
    Me.lblPrzedmiot.TabIndex = 3
    Me.lblPrzedmiot.Text = "Przedmiot"
    Me.lblPrzedmiot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lvWyniki
    '
    Me.lvWyniki.Location = New System.Drawing.Point(12, 202)
    Me.lvWyniki.Name = "lvWyniki"
    Me.lvWyniki.Size = New System.Drawing.Size(718, 234)
    Me.lvWyniki.TabIndex = 196
    Me.lvWyniki.UseCompatibleStateImageBehavior = False
    '
    'dlgUzasadnienie
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(745, 480)
    Me.Controls.Add(Me.lvWyniki)
    Me.Controls.Add(Me.TableLayoutPanel2)
    Me.Controls.Add(Me.txtUzasadnienie)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgUzasadnienie"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Uzasadnienie"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel2.ResumeLayout(False)
    Me.TableLayoutPanel2.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents txtUzasadnienie As System.Windows.Forms.TextBox
  Friend WithEvents lblStudent As System.Windows.Forms.Label
  Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents lblPrzedmiot As System.Windows.Forms.Label
  Friend WithEvents lvWyniki As System.Windows.Forms.ListView

End Class
