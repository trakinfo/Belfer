<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKalibracjaWydruku
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
    Me.pvKaliber = New System.Windows.Forms.PrintPreviewControl()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.nudX = New System.Windows.Forms.NumericUpDown()
    Me.nudY = New System.Windows.Forms.NumericUpDown()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.nudLeft = New System.Windows.Forms.NumericUpDown()
    Me.nudTop = New System.Windows.Forms.NumericUpDown()
    CType(Me.nudX, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudY, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    CType(Me.nudLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pvKaliber
    '
    Me.pvKaliber.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pvKaliber.AutoZoom = False
    Me.pvKaliber.Location = New System.Drawing.Point(12, 12)
    Me.pvKaliber.Name = "pvKaliber"
    Me.pvKaliber.Size = New System.Drawing.Size(348, 471)
    Me.pvKaliber.TabIndex = 0
    Me.pvKaliber.UseAntiAlias = True
    Me.pvKaliber.Zoom = 0.4R
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(6, 29)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(81, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Współrzędna X"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(6, 55)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(81, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Współrzędna Y"
    '
    'nudX
    '
    Me.nudX.DecimalPlaces = 1
    Me.nudX.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
    Me.nudX.Location = New System.Drawing.Point(93, 27)
    Me.nudX.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
    Me.nudX.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
    Me.nudX.Name = "nudX"
    Me.nudX.Size = New System.Drawing.Size(68, 20)
    Me.nudX.TabIndex = 3
    '
    'nudY
    '
    Me.nudY.DecimalPlaces = 1
    Me.nudY.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
    Me.nudY.Location = New System.Drawing.Point(93, 53)
    Me.nudY.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
    Me.nudY.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
    Me.nudY.Name = "nudY"
    Me.nudY.Size = New System.Drawing.Size(68, 20)
    Me.nudY.TabIndex = 4
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Location = New System.Drawing.Point(366, 460)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(75, 23)
    Me.cmdPrint.TabIndex = 5
    Me.cmdPrint.Text = "Drukuj"
    Me.cmdPrint.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Location = New System.Drawing.Point(461, 460)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(75, 23)
    Me.cmdClose.TabIndex = 6
    Me.cmdClose.Text = "Zamknij"
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.nudX)
    Me.GroupBox1.Controls.Add(Me.nudY)
    Me.GroupBox1.Location = New System.Drawing.Point(366, 119)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(170, 86)
    Me.GroupBox1.TabIndex = 7
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Kalibracja (milimetry)"
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.Label3)
    Me.GroupBox2.Controls.Add(Me.Label4)
    Me.GroupBox2.Controls.Add(Me.nudLeft)
    Me.GroupBox2.Controls.Add(Me.nudTop)
    Me.GroupBox2.Location = New System.Drawing.Point(366, 12)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(170, 84)
    Me.GroupBox2.TabIndex = 8
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Marginesy (milimetry)"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(8, 29)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(32, 13)
    Me.Label3.TabIndex = 5
    Me.Label3.Text = "Lewy"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(8, 55)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(35, 13)
    Me.Label4.TabIndex = 6
    Me.Label4.Text = "Górny"
    '
    'nudLeft
    '
    Me.nudLeft.Location = New System.Drawing.Point(95, 27)
    Me.nudLeft.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
    Me.nudLeft.Name = "nudLeft"
    Me.nudLeft.Size = New System.Drawing.Size(68, 20)
    Me.nudLeft.TabIndex = 7
    Me.nudLeft.Value = New Decimal(New Integer() {20, 0, 0, 0})
    '
    'nudTop
    '
    Me.nudTop.Location = New System.Drawing.Point(95, 53)
    Me.nudTop.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
    Me.nudTop.Name = "nudTop"
    Me.nudTop.Size = New System.Drawing.Size(68, 20)
    Me.nudTop.TabIndex = 8
    Me.nudTop.Value = New Decimal(New Integer() {20, 0, 0, 0})
    '
    'frmKalibracjaWydruku
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(548, 495)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.pvKaliber)
    Me.Name = "frmKalibracjaWydruku"
    Me.Text = "Kalibracja wydruku"
    CType(Me.nudX, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudY, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    CType(Me.nudLeft, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents pvKaliber As System.Windows.Forms.PrintPreviewControl
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents nudX As System.Windows.Forms.NumericUpDown
  Friend WithEvents nudY As System.Windows.Forms.NumericUpDown
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents nudLeft As System.Windows.Forms.NumericUpDown
  Friend WithEvents nudTop As System.Windows.Forms.NumericUpDown
End Class
