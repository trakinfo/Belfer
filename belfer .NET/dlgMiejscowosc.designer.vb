<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgMiejscowosc
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtNazwa = New System.Windows.Forms.TextBox()
    Me.txtPoczta = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cbWoj = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.cbKraj = New System.Windows.Forms.ComboBox()
    Me.chkMiasto = New System.Windows.Forms.CheckBox()
    Me.mskKodPocztowy = New System.Windows.Forms.MaskedTextBox()
    Me.txtNazwaMiejscownik = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(514, 118)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 7
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 7
    Me.OK_Button.Text = "Zapisz"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 8
    Me.Cancel_Button.Text = "Zamknij"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(40, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Nazwa"
    '
    'txtNazwa
    '
    Me.txtNazwa.Location = New System.Drawing.Point(92, 12)
    Me.txtNazwa.Name = "txtNazwa"
    Me.txtNazwa.Size = New System.Drawing.Size(191, 20)
    Me.txtNazwa.TabIndex = 0
    '
    'txtPoczta
    '
    Me.txtPoczta.Location = New System.Drawing.Point(92, 38)
    Me.txtPoczta.Name = "txtPoczta"
    Me.txtPoczta.Size = New System.Drawing.Size(191, 20)
    Me.txtPoczta.TabIndex = 3
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 41)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(40, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Poczta"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(332, 41)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(74, 13)
    Me.Label3.TabIndex = 5
    Me.Label3.Text = "Kod pocztowy"
    '
    'cbWoj
    '
    Me.cbWoj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbWoj.FormattingEnabled = True
    Me.cbWoj.Location = New System.Drawing.Point(92, 64)
    Me.cbWoj.Name = "cbWoj"
    Me.cbWoj.Size = New System.Drawing.Size(191, 21)
    Me.cbWoj.TabIndex = 5
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 67)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(74, 13)
    Me.Label4.TabIndex = 8
    Me.Label4.Text = "Województwo"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(381, 67)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(25, 13)
    Me.Label5.TabIndex = 9
    Me.Label5.Text = "Kraj"
    '
    'cbKraj
    '
    Me.cbKraj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKraj.FormattingEnabled = True
    Me.cbKraj.Location = New System.Drawing.Point(412, 64)
    Me.cbKraj.Name = "cbKraj"
    Me.cbKraj.Size = New System.Drawing.Size(248, 21)
    Me.cbKraj.TabIndex = 6
    '
    'chkMiasto
    '
    Me.chkMiasto.AutoSize = True
    Me.chkMiasto.Location = New System.Drawing.Point(609, 14)
    Me.chkMiasto.Name = "chkMiasto"
    Me.chkMiasto.Size = New System.Drawing.Size(57, 17)
    Me.chkMiasto.TabIndex = 2
    Me.chkMiasto.Text = "Miasto"
    Me.chkMiasto.UseVisualStyleBackColor = True
    '
    'mskKodPocztowy
    '
    Me.mskKodPocztowy.Location = New System.Drawing.Point(412, 38)
    Me.mskKodPocztowy.Mask = "00-000"
    Me.mskKodPocztowy.Name = "mskKodPocztowy"
    Me.mskKodPocztowy.Size = New System.Drawing.Size(43, 20)
    Me.mskKodPocztowy.TabIndex = 4
    Me.mskKodPocztowy.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
    '
    'txtNazwaMiejscownik
    '
    Me.txtNazwaMiejscownik.Location = New System.Drawing.Point(412, 12)
    Me.txtNazwaMiejscownik.Name = "txtNazwaMiejscownik"
    Me.txtNazwaMiejscownik.Size = New System.Drawing.Size(191, 20)
    Me.txtNazwaMiejscownik.TabIndex = 1
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(289, 15)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(117, 13)
    Me.Label6.TabIndex = 29
    Me.Label6.Text = "Nazwa w miejscowniku"
    '
    'dlgMiejscowosc
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(672, 159)
    Me.Controls.Add(Me.txtNazwaMiejscownik)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.mskKodPocztowy)
    Me.Controls.Add(Me.chkMiasto)
    Me.Controls.Add(Me.cbKraj)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.cbWoj)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtPoczta)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtNazwa)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgMiejscowosc"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Dodawanie miejscowości"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
  Friend WithEvents txtPoczta As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cbWoj As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents cbKraj As System.Windows.Forms.ComboBox
  Friend WithEvents chkMiasto As System.Windows.Forms.CheckBox
  Friend WithEvents mskKodPocztowy As System.Windows.Forms.MaskedTextBox
  Friend WithEvents txtNazwaMiejscownik As System.Windows.Forms.TextBox
  Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
