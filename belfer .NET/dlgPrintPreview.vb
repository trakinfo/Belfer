Imports System.Windows.Forms

Public Class dlgPrintPreview
  Public WithEvents Doc As New PrintReport(New DataSet)
  Public Event PreviewModeChange(ByVal IsPreview As Boolean)

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Dim prnDlg As New PrintDialog
    prnDlg.AllowSomePages = False
    prnDlg.PrinterSettings.FromPage = 1
    prnDlg.PrinterSettings.ToPage = 1
    If prnDlg.ShowDialog = Windows.Forms.DialogResult.OK Then
      'Doc.IsPreview = False
      RaiseEvent PreviewModeChange(False)
      Me.Doc.PageNumber = 0
      'Me.Doc.Offset(0) = 0
      'Me.Doc.Offset(1) = 0
      pvWydruk.Document.DefaultPageSettings.PrinterSettings.Copies = prnDlg.PrinterSettings.Copies
      Me.pvWydruk.Document.DefaultPageSettings.PrinterSettings.FromPage = prnDlg.PrinterSettings.FromPage
      Me.pvWydruk.Document.DefaultPageSettings.PrinterSettings.ToPage = prnDlg.PrinterSettings.ToPage
      Me.pvWydruk.Document.DefaultPageSettings.PrinterSettings.PrinterName = prnDlg.PrinterSettings.PrinterName
      Me.pvWydruk.Document.Print()
      'Doc.IsPreview = True
      RaiseEvent PreviewModeChange(True)
    End If
    'Me.Dispose(True)
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    'Me.Dispose(True)
  End Sub

  Private Sub nudZoom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudZoom.ValueChanged
    If Not Me.Created Then Exit Sub
    tbZoom.Value = CType(nudZoom.Value, Integer)
    'Me.pvWydruk.Rows = pvWydruk.Rows
    Me.pvWydruk.Zoom = nudZoom.Value * 0.01
    'Me.Doc.PageNumber = 0
    'Me.Doc.Offset(0) = 0
    'Me.Doc.Offset(1) = 0
    'Me.Doc.Lp = 0
    'Me.pvWydruk.InvalidatePreview()

  End Sub
  Private Sub tbZoom_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbZoom.Scroll
    'Zoom = tbZoom.Value * 0.01
    nudZoom.Value = tbZoom.Value
    'Me.pvWydruk.Rows = pvWydruk.Rows
    Me.pvWydruk.Zoom = tbZoom.Value * 0.01

  End Sub

  Private Sub dlgPrintPreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    pvWydruk.Document = Doc
    pvWydruk.Zoom = nudZoom.Value * 0.01
    pvWydruk.InvalidatePreview()
    'Me.Doc.Offset(0) = 0
    'Me.Doc.Offset(1) = 0
    'Me.Doc.Lp = 0
  End Sub
  Public Sub NewRow()
    pvWydruk.Rows += 1
  End Sub

  Private Sub pvWydruk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pvWydruk.Click
    If (Control.ModifierKeys And Keys.Shift) = 0 Then
      If nudZoom.Maximum >= pvWydruk.Zoom * 100 * 2.0 Then
        pvWydruk.Zoom = pvWydruk.Zoom * 2.0
        nudZoom.Value = CType(pvWydruk.Zoom * 100, Decimal)

      End If
    Else
      If nudZoom.Minimum <= pvWydruk.Zoom * 100 / 2.0 Then
        pvWydruk.Zoom = pvWydruk.Zoom / 2.0
        nudZoom.Value = CType(pvWydruk.Zoom * 100, Decimal)

      End If
    End If

  End Sub

  Private Sub cmdPageSetup_Click(sender As Object, e As EventArgs) Handles cmdPageSetup.Click
    Dim pg As New PageSetupDialog, prnVars As New PrintVariables
    pg.EnableMetric = True
    pg.Document = Doc
    pg.MinMargins.Left = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Left)
    pg.MinMargins.Top = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Top)
    pg.PageSettings.Margins.Top = My.Settings.TopMargin
    pg.PageSettings.Margins.Left = My.Settings.LeftMargin
    pg.PageSettings.Margins.Bottom = My.Settings.TopMargin
    pg.PageSettings.Margins.Right = My.Settings.LeftMargin
    pg.PageSettings.Landscape = My.Settings.Landscape
    pg.AllowPaper = False
    pg.AllowOrientation = True

    If pg.ShowDialog = Windows.Forms.DialogResult.OK Then
      'prnVars.Landscape = pg.PageSettings.Landscape
      'prnVars.LeftMargin = pg.PageSettings.Margins.Left
      'prnVars.TopMargin = pg.PageSettings.Margins.Top
      'prnVars.RightMargin = pg.PageSettings.Margins.Right
      'prnVars.BottomMargin = pg.PageSettings.Margins.Bottom
      My.Settings.TopMargin = pg.PageSettings.Margins.Top
      My.Settings.LeftMargin = pg.PageSettings.Margins.Left
      My.Settings.Landscape = pg.PageSettings.Landscape

      pvWydruk.Rows = 1
      Doc.PageNumber = 0
      'Me.Doc.Offset(0) = 0
      'Me.Doc.Offset(1) = 0
      'Me.Doc.Lp = 0
      pvWydruk.InvalidatePreview()
    End If
  End Sub

  Private Sub cmdFontSetup_Click(sender As Object, e As EventArgs) Handles cmdFontSetup.Click
    Dim fg As New FontDialog, prnVars As New PrintVariables
    fg.MinSize = 8
    fg.MaxSize = 20
    fg.FontMustExist = True
    fg.Font = My.Settings.TextFont
    If fg.ShowDialog = Windows.Forms.DialogResult.OK Then
      'TextFont = fg.Font
      My.Settings.TextFont = fg.Font
      My.Settings.SubHeaderFont = New Font(My.Settings.SubHeaderFont.FontFamily, My.Settings.TextFont.Size + 1, FontStyle.Bold)
      My.Settings.HeaderFont = New Font(My.Settings.HeaderFont.FontFamily, My.Settings.TextFont.Size + 2, FontStyle.Bold)
      pvWydruk.Rows = 1
      Doc.PageNumber = 0
      'Me.Doc.Offset(0) = 0
      'Me.Doc.Offset(1) = 0
      'Me.Doc.Lp = 0
      pvWydruk.InvalidatePreview()
    End If
  End Sub
End Class
