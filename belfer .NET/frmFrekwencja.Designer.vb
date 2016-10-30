<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFrekwencja
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
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dgvFrekwencja = New System.Windows.Forms.DataGridView()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    CType(Me.dgvFrekwencja, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(51, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(261, 21)
    Me.cbKlasa.TabIndex = 26
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 25
    Me.Label2.Text = "Klasa"
    '
    'dgvFrekwencja
    '
    Me.dgvFrekwencja.AllowUserToAddRows = False
    Me.dgvFrekwencja.AllowUserToDeleteRows = False
    Me.dgvFrekwencja.AllowUserToResizeColumns = False
    Me.dgvFrekwencja.AllowUserToResizeRows = False
    Me.dgvFrekwencja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dgvFrekwencja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvFrekwencja.Enabled = False
    Me.dgvFrekwencja.Location = New System.Drawing.Point(12, 39)
    Me.dgvFrekwencja.Name = "dgvFrekwencja"
    Me.dgvFrekwencja.RowHeadersVisible = False
    Me.dgvFrekwencja.RowTemplate.Height = 20
    Me.dgvFrekwencja.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvFrekwencja.Size = New System.Drawing.Size(795, 295)
    Me.dgvFrekwencja.TabIndex = 27
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(714, 338)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(93, 35)
    Me.cmdClose.TabIndex = 208
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(615, 338)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(93, 35)
    Me.cmdPrint.TabIndex = 217
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'frmFrekwencja
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(819, 382)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.dgvFrekwencja)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label2)
    Me.Name = "frmFrekwencja"
    Me.Text = "Frekwencja na zajęciach"
    CType(Me.dgvFrekwencja, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents dgvFrekwencja As System.Windows.Forms.DataGridView
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
End Class
