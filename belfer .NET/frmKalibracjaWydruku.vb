Imports System.Drawing.Printing
Public Class frmKalibracjaWydruku
  Private WithEvents Doc As New PrintDocument
  Private Preview As Boolean = True
  Private Sub frmTemat_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.KalibracjaWydrukuToolStripMenuItem.Enabled = True
  End Sub

  Private Sub frmTemat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.KalibracjaWydrukuToolStripMenuItem.Enabled = True
  End Sub
  Private Sub frmKalibracjaWydruku_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Dim CH As New CalcHelper
    Doc.DefaultPageSettings.Margins.Left = CInt(CH.MMtoIn(nudLeft.Value))
    Doc.DefaultPageSettings.Margins.Right = CInt(CH.MMtoIn(nudLeft.Value))
    Doc.DefaultPageSettings.Margins.Top = CInt(CH.MMtoIn(nudTop.Value))
    Doc.DefaultPageSettings.Margins.Bottom = CInt(CH.MMtoIn(nudTop.Value))
    nudX.Value = CDec(CH.InToMM(My.Settings.XCaliber))
    nudY.Value = CDec(CH.InToMM(My.Settings.YCaliber))

    pvKaliber.Document = Doc
    'pvKaliber.Document.OriginAtMargins = True

  End Sub
  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    pvKaliber.Document.OriginAtMargins = False
    Preview = False
    Me.pvKaliber.Document.Print()
  End Sub
  Public Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles Doc.PrintPage
    Dim CH As New CalcHelper
    Dim x As Single = Doc.DefaultPageSettings.Margins.Left '+ CH.MMtoIn(nudX.Value)
    Dim y As Single = Doc.DefaultPageSettings.Margins.Top '+ CH.MMtoIn(nudY.Value)
    If Not Preview Then
      x -= Doc.DefaultPageSettings.PrintableArea.Left
      y -= Doc.DefaultPageSettings.PrintableArea.Top
    End If
    Dim PrintFont As Font = New Font("Times", 10)
    Dim LineHeight As Single = PrintFont.GetHeight(e.Graphics)
    'Doc.DrawRectangle(e, x, y, e.MarginBounds.Width, e.MarginBounds.Height)
    e.Graphics.DrawRectangle(New Pen(Brushes.Black), x, y, e.MarginBounds.Width, e.MarginBounds.Height)
    x += 20 : y += 20
    Dim Komunikat As String = "Zmierz marginesy na wydruku i porównaj z marginesami zadanymi w sekcji Marginesy. Jeżeli wartości marginesów zadanych różnią się od wartości marginesów faktycznie drukowanych, to dokonaj korekty w sekcji Kalibracja, podając wartości przesunięcia współrzędnych X oraz Y."
    'Doc.DrawText(e, Komunikat, PrintFont, x, y, e.MarginBounds.Width - 40, e.MarginBounds.Height - 40, 0, Brushes.Black, 0, False)
    e.Graphics.DrawString(Komunikat, PrintFont, Brushes.Black, New RectangleF(x, y, e.MarginBounds.Width - 40, e.MarginBounds.Height - 40))
    y += LineHeight * 10
    e.Graphics.DrawString("Marginesy zadane (milimetry):", PrintFont, Brushes.Black, x, y)
    y += LineHeight
    e.Graphics.DrawString("Lewy: " & nudLeft.Value.ToString, PrintFont, Brushes.Black, x, y)
    e.Graphics.DrawString("Górny: " & nudTop.Value.ToString, PrintFont, Brushes.Black, x + e.Graphics.MeasureString(String.Concat("Lewy: ", nudLeft.Value.ToString), PrintFont).Width + 40, y)
    y += 40
    e.Graphics.DrawString("Wartości kalibracji (milimetry):", PrintFont, Brushes.Black, x, y)
    y += LineHeight
    e.Graphics.DrawString("Współrzędna X: " & nudX.Value.ToString, PrintFont, Brushes.Black, x, y)
    e.Graphics.DrawString("Współrzędna Y: " & nudY.Value.ToString, PrintFont, Brushes.Black, x + e.Graphics.MeasureString(String.Concat("Współrzędna X: ", nudX.Value.ToString), PrintFont).Width + 40, y)

  End Sub

  Private Sub nudX_ValueChanged(sender As Object, e As EventArgs) Handles nudX.ValueChanged, nudY.ValueChanged
    Dim CH As New CalcHelper
    If CType(sender, NumericUpDown).Name = "nudX" Then
      My.Settings.XCaliber = CH.MMtoIn(nudX.Value)
    Else
      My.Settings.YCaliber = CH.MMtoIn(nudY.Value)
    End If
    My.Settings.Save()
    pvKaliber.InvalidatePreview()
  End Sub

  Private Sub nudLeft_ValueChanged(sender As Object, e As EventArgs) Handles nudLeft.ValueChanged, nudTop.ValueChanged
    Dim CH As New CalcHelper
    Doc.DefaultPageSettings.Margins.Left = CInt(CH.MMtoIn(nudLeft.Value))
    Doc.DefaultPageSettings.Margins.Right = CInt(CH.MMtoIn(nudLeft.Value))
    Doc.DefaultPageSettings.Margins.Top = CInt(CH.MMtoIn(nudTop.Value))
    Doc.DefaultPageSettings.Margins.Bottom = CInt(CH.MMtoIn(nudTop.Value))

    pvKaliber.InvalidatePreview()
  End Sub
End Class