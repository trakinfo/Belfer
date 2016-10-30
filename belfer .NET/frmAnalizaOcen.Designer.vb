<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnalizaOcen
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
    Me.lblObsadaFilter = New System.Windows.Forms.Label()
    Me.cbNauczyciel = New System.Windows.Forms.ComboBox()
    Me.nudSemestr = New System.Windows.Forms.NumericUpDown()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.lvAnaliza = New System.Windows.Forms.ListView()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'lblObsadaFilter
    '
    Me.lblObsadaFilter.Location = New System.Drawing.Point(12, 15)
    Me.lblObsadaFilter.Name = "lblObsadaFilter"
    Me.lblObsadaFilter.Size = New System.Drawing.Size(59, 13)
    Me.lblObsadaFilter.TabIndex = 185
    Me.lblObsadaFilter.Text = "Nauczyciel"
    Me.lblObsadaFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cbNauczyciel
    '
    Me.cbNauczyciel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbNauczyciel.Enabled = False
    Me.cbNauczyciel.FormattingEnabled = True
    Me.cbNauczyciel.Location = New System.Drawing.Point(77, 12)
    Me.cbNauczyciel.Name = "cbNauczyciel"
    Me.cbNauczyciel.Size = New System.Drawing.Size(336, 21)
    Me.cbNauczyciel.TabIndex = 184
    '
    'nudSemestr
    '
    Me.nudSemestr.Location = New System.Drawing.Point(490, 13)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(34, 20)
    Me.nudSemestr.TabIndex = 187
    Me.nudSemestr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(439, 15)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(45, 13)
    Me.Label8.TabIndex = 186
    Me.Label8.Text = "Semestr"
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(933, 39)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(93, 35)
    Me.cmdPrint.TabIndex = 189
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(933, 420)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(93, 35)
    Me.cmdClose.TabIndex = 188
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'lvAnaliza
    '
    Me.lvAnaliza.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvAnaliza.Location = New System.Drawing.Point(12, 39)
    Me.lvAnaliza.Name = "lvAnaliza"
    Me.lvAnaliza.Size = New System.Drawing.Size(915, 416)
    Me.lvAnaliza.TabIndex = 190
    Me.lvAnaliza.UseCompatibleStateImageBehavior = False
    '
    'frmAnalizaOcen
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1038, 467)
    Me.Controls.Add(Me.lvAnaliza)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.nudSemestr)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.lblObsadaFilter)
    Me.Controls.Add(Me.cbNauczyciel)
    Me.Name = "frmAnalizaOcen"
    Me.Text = "Analiza ocen"
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lblObsadaFilter As System.Windows.Forms.Label
  Friend WithEvents cbNauczyciel As System.Windows.Forms.ComboBox
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents lvAnaliza As System.Windows.Forms.ListView
End Class
