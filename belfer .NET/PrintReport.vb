Imports System.Drawing.Printing
Public Class PrintReport
  Inherits System.Drawing.Printing.PrintDocument

  Private FDS As DataSet
  Private FPageNumber As Integer
  Private FOffset(3) As Integer
  Private FReportHeader() As String
  Private FReportFooter() As String
  Private FPageHeader() As String
  Private FPageFooter() As String
  Private FGroupHeader() As String
  Private FGroupFooter() As String
  Private FTableHeader() As String
  Private FPodpis As Byte()
  'Public Property PrintArgs As System.Drawing.Printing.PrintPageEventArgs
  Private FLp As Integer = 0
  'Public Property FIsPreview As Boolean = True
  Public Sub New(ByVal DS As DataSet)
    Dim PrnVar As New PrintVariables
    FDS = DS
    Me.OriginAtMargins = False
    Me.DefaultPageSettings.Margins.Top = PrnVar.TopMargin
    Me.DefaultPageSettings.Margins.Left = PrnVar.LeftMargin
    Me.DefaultPageSettings.Margins.Bottom = PrnVar.BottomMargin
    Me.DefaultPageSettings.Margins.Right = PrnVar.RightMargin
    Me.DefaultPageSettings.Landscape = PrnVar.Landscape
    Me.DefaultPageSettings.Color = PrnVar.Color
  End Sub

  Public Property DS() As DataSet
    Get
      Return FDS
    End Get
    Set(ByVal value As DataSet)
      FDS = value
    End Set
  End Property
  'Public Property IsPreview() As Boolean
  '  Get
  '    Return FIsPreview
  '  End Get
  '  Set(ByVal value As Boolean)
  '    FIsPreview = value
  '  End Set
  'End Property
  Public Property PageNumber() As Integer
    Get
      Return FPageNumber
    End Get
    Set(ByVal value As Integer)
      FPageNumber = value
    End Set
  End Property
  Public Property Podpis() As Byte()
    Get
      Return FPodpis
    End Get
    Set(ByVal value As Byte())
      FPodpis = value
    End Set
  End Property
  Public Property Offset(ByVal index As Integer) As Integer
    Get
      Return FOffset(index)
    End Get
    Set(ByVal value As Integer)
      FOffset(index) = value
    End Set
  End Property

  Public Property ReportHeader() As String()
    Get
      Return FReportHeader
    End Get
    Set(ByVal value As String())
      FReportHeader = value
    End Set
  End Property
  Public Property ReportFooter() As String()
    Get
      Return FReportFooter
    End Get
    Set(ByVal value As String())
      FReportFooter = value
    End Set
  End Property
  Public Property PageHeader() As String()
    Get
      Return FPageHeader
    End Get
    Set(ByVal value As String())
      FPageHeader = value
    End Set
  End Property
  Public Property PageFooter() As String()
    Get
      Return FPageFooter
    End Get
    Set(ByVal value As String())
      FPageFooter = value
    End Set
  End Property
  Public Property GroupHeader() As String()
    Get
      Return FGroupHeader
    End Get
    Set(ByVal value As String())
      FGroupHeader = value
    End Set
  End Property
  Public Property GroupFooter() As String()
    Get
      Return FGroupFooter
    End Get
    Set(ByVal value As String())
      FGroupFooter = value
    End Set
  End Property
  Public Property TableHeader() As String()
    Get
      Return FTableHeader
    End Get
    Set(ByVal value As String())
      FTableHeader = value
    End Set
  End Property
  Public Property Lp() As Integer
    Get
      Return FLp
    End Get
    Set(ByVal value As Integer)
      FLp = value
    End Set
  End Property

  Public Overloads Sub DrawText(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal Text As String, ByVal Font As Font, ByVal x As Single, ByVal y As Single, ByVal Width As Single, ByVal Height As Single, ByVal Alignment As Byte, ByVal FontColor As Brush, Optional ByVal DrawLines As Boolean = True, Optional ByVal VerticalLayout As Boolean = False, Optional ByVal FillBackground As Boolean = False)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'Width += My.Settings.XCaliber
    'Height += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    Dim strFormat As New StringFormat
    strFormat.Alignment = CType(Alignment, StringAlignment)
    strFormat.LineAlignment = StringAlignment.Center
    If VerticalLayout Then strFormat.FormatFlags = StringFormatFlags.DirectionVertical
    If DrawLines Then e.Graphics.DrawRectangle(Pens.Black, x, y, Width, Height)
    If FillBackground Then e.Graphics.FillRectangle(Brushes.LightGray, New RectangleF(x, y, Width, Height))
    e.Graphics.DrawString(Text, Font, FontColor, New RectangleF(x, y, Width, Height), strFormat)
  End Sub
  Public Overloads Sub DrawText(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal Text As String, ByVal Font As Font, ByVal x As Single, ByVal y As Single, ByVal Width As Single, ByVal Height As Single, ByVal Alignment As Byte, ByVal FontColor As Brush, ByVal Angle As Integer, Optional ByVal DrawLines As Boolean = True)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If

    Dim strFormat As New StringFormat
    strFormat.Alignment = CType(Alignment, StringAlignment)
    strFormat.LineAlignment = StringAlignment.Center
    e.Graphics.RotateTransform(Angle)
    e.Graphics.TranslateTransform(x, y, Drawing2D.MatrixOrder.Append)
    If DrawLines Then e.Graphics.DrawRectangle(Pens.Black, 0, 0, Height, Width)
    'If DrawLines Then e.Graphics.DrawRectangle(Pens.Black, 0, 0 - Width / 2, Height, Width)
    'e.Graphics.DrawString(Text, Font, FontColor, 0, 0, strFormat)
    e.Graphics.DrawString(Text, Font, FontColor, New RectangleF(0, 0, Height, Width), strFormat)
    e.Graphics.ResetTransform()
  End Sub
  Public Sub DrawPageNumber(ByVal e As System.Drawing.Printing.PrintPageEventArgs, PageNumber As String, ByVal x As Single, ByVal y As Single, ByVal Width As Single, Optional HorizontalAlignment As Byte = 1)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    Dim DotPen As New Pen(Color.Black), CS As New CalcHelper
    Dim BaseFont As Font = New Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point)
    y -= CS.MMtoIn(5)
    'G.DrawLine(DotPen, x, y, x + Width, y)
    y -= BaseFont.GetHeight(e.Graphics)
    Dim strFormat As New StringFormat
    strFormat.LineAlignment = StringAlignment.Center
    strFormat.Alignment = CType(HorizontalAlignment, StringAlignment)
    e.Graphics.DrawString(PageNumber, BaseFont, Brushes.Black, New RectangleF(x, y, Width, BaseFont.GetHeight(e.Graphics)), strFormat)
  End Sub
  Public Sub DrawHeader(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal x As Single, ByVal y As Single, ByVal Width As Single)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    Dim DotPen As New Pen(Color.Black), CS As New CalcHelper, DBA As New DataBaseAction, S As New SzkolaSQL
    Dim FooterFont As Font = New Font("Arial", 8, FontStyle.Italic, GraphicsUnit.Point)
    y -= CS.MMtoIn(5)

    e.Graphics.DrawLine(DotPen, x, y, x + Width, y)
    y -= FooterFont.GetHeight(e.Graphics)
    Dim strFormat As New StringFormat
    strFormat.LineAlignment = StringAlignment.Center
    strFormat.Alignment = StringAlignment.Near
    e.Graphics.DrawString(DBA.GetSingleValue(S.SelectSchoolName(My.Settings.IdSchool)), FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(e.Graphics)), strFormat)
    strFormat.Alignment = StringAlignment.Far
    e.Graphics.DrawString("Rok szkolny: " & My.Settings.SchoolYear, FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(e.Graphics)), strFormat)
  End Sub
  Public Sub DrawFooter(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal x As Single, ByVal y As Single, ByVal Width As Single)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    Dim DotPen As New Pen(Color.Black), CS As New CalcHelper
    Dim FooterFont As Font = New Font("Arial", 8, FontStyle.Italic, GraphicsUnit.Point)
    y += CS.MMtoIn(5)
    e.Graphics.DrawLine(DotPen, x, y, x + Width, y)
    y += FooterFont.GetHeight(e.Graphics) / 2
    Dim strFormat As New StringFormat
    strFormat.LineAlignment = StringAlignment.Center
    strFormat.Alignment = StringAlignment.Near
    e.Graphics.DrawString("Belfer .NET (wersja " & My.Application.Info.Version.ToString & ")", FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(e.Graphics)), strFormat)
    strFormat.Alignment = StringAlignment.Far
    e.Graphics.DrawString(DateTime.Now.ToString, FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(e.Graphics)), strFormat)
  End Sub
  Public Sub DrawLine(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal x0 As Single, ByVal y0 As Single, ByVal x1 As Single, y1 As Single, Optional PenWidth As Single = 1)
    x0 += My.Settings.XCaliber
    y0 += My.Settings.YCaliber
    x1 += My.Settings.XCaliber
    y1 += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x0 -= e.PageSettings.PrintableArea.Left
    '  x1 -= e.PageSettings.PrintableArea.Left
    '  y0 -= e.PageSettings.PrintableArea.Top
    '  y1 -= e.PageSettings.PrintableArea.Top
    'End If
    'e.Graphics.PageUnit = GraphicsUnit.Pixel
    e.Graphics.DrawLine(New Pen(Brushes.Black, PenWidth), x0, y0, x1, y1)
    'e.Graphics.PageUnit = GraphicsUnit.Display

  End Sub
  Public Sub DrawRectangle(ByVal e As System.Drawing.Printing.PrintPageEventArgs, PenWidth As Single, ByVal x0 As Single, ByVal y0 As Single, ByVal Width As Single, Height As Single)
    x0 += My.Settings.XCaliber
    y0 += My.Settings.YCaliber
    'Width += My.Settings.XCaliber
    'Height += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x0 -= e.PageSettings.PrintableArea.Left
    '  'Width -= e.PageSettings.PrintableArea.Left
    '  y0 -= e.PageSettings.PrintableArea.Top
    '  'Height -= e.PageSettings.PrintableArea.Top
    'End If
    e.Graphics.DrawRectangle(New Pen(Brushes.Black, PenWidth), x0, y0, Width, Height)
  End Sub
  Public Sub DrawImage(ByVal e As System.Drawing.Printing.PrintPageEventArgs, img As System.Drawing.Image, ByVal x As Single, ByVal y As Single, ByVal Width As Single, Height As Single)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= e.PageSettings.PrintableArea.Left
    '  y -= e.PageSettings.PrintableArea.Top
    'End If
    e.Graphics.DrawImage(img, x, y, Width, Height)
  End Sub
 
End Class

Public Class Pole
  Public Property Name As String
  Public Property Label As String
  Public Property Size As Integer
End Class

Public Class PrintHelper
  Public Property G As Graphics

  Public Overloads Sub DrawText(ByVal Text As String, ByVal Font As Font, ByVal x As Single, ByVal y As Single, ByVal Width As Single, ByVal Height As Single, ByVal Alignment As Byte, ByVal FontColor As Brush, Optional ByVal DrawLines As Boolean = True, Optional ByVal VerticalLayout As Boolean = False, Optional ByVal FillBackground As Boolean = False)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'Width += My.Settings.XCaliber
    'Height += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  y -= PS.PrintableArea.Top 'e.PageSettings.PrintableArea.Top
    'End If
    Dim strFormat As New StringFormat
    strFormat.Alignment = CType(Alignment, StringAlignment)
    strFormat.LineAlignment = StringAlignment.Center
    If VerticalLayout Then strFormat.FormatFlags = StringFormatFlags.DirectionVertical
    If DrawLines Then G.DrawRectangle(Pens.Black, x, y, Width, Height)
    If FillBackground Then G.FillRectangle(Brushes.LightGray, New RectangleF(x, y, Width, Height))
    G.DrawString(Text, Font, FontColor, New RectangleF(x, y, Width, Height), strFormat)
  End Sub
  Public Overloads Sub DrawText(ByVal Text As String, ByVal Font As Font, ByVal x As Single, ByVal y As Single, ByVal Width As Single, ByVal Height As Single, ByVal Alignment As Byte, ByVal FontColor As Brush, ByVal Angle As Integer, Optional ByVal DrawLines As Boolean = True)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  y -= PS.PrintableArea.Top 'e.PageSettings.PrintableArea.Top
    'End If

    Dim strFormat As New StringFormat
    strFormat.Alignment = CType(Alignment, StringAlignment)
    strFormat.LineAlignment = StringAlignment.Center
    G.RotateTransform(Angle)
    G.TranslateTransform(x, y, Drawing2D.MatrixOrder.Append)
    If DrawLines Then G.DrawRectangle(Pens.Black, 0, 0, Height, Width)
    'If DrawLines Then e.Graphics.DrawRectangle(Pens.Black, 0, 0 - Width / 2, Height, Width)
    'e.Graphics.DrawString(Text, Font, FontColor, 0, 0, strFormat)
    G.DrawString(Text, Font, FontColor, New RectangleF(0, 0, Height, Width), strFormat)
    G.ResetTransform()
  End Sub
  Public Function DrawWrappedText(ReasonContent As String(), TextFont As Font, ByRef WordOffset As Integer, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, PrintHeight As Single, TextLineHeight As Single, Optional TabIndent As Integer = 0) As Boolean
    'Dim TextLineHeight As Single = TextFont.GetHeight(G)
    Dim Line As New System.Text.StringBuilder

    Do
      Line.Append(String.Concat(ReasonContent(WordOffset), " "))
      If G.MeasureString(Line.ToString, TextFont).Width + TabIndent > PrintWidth Then
        Line.Remove(Line.ToString.Length - ReasonContent(WordOffset).Length - 1, ReasonContent(WordOffset).Length)
        DrawText(Line.ToString, TextFont, x + TabIndent, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
        y += TextLineHeight
        WordOffset -= 1
        Line = New System.Text.StringBuilder
      End If
      WordOffset += 1
    Loop While ReasonContent.GetUpperBound(0) >= WordOffset AndAlso PrintHeight >= y + TextLineHeight
    If PrintHeight >= y + TextLineHeight Then
      DrawText(Line.ToString, TextFont, x + TabIndent, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
      y += TextLineHeight
      WordOffset = 0
      Return True
    Else
      'WordOffset -= 1
      Return False
    End If
  End Function
  Public Sub DrawPageNumber(PageNumber As String, ByVal x As Single, ByVal y As Single, ByVal Width As Single, Optional HorizontalAlignment As Byte = 1)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  y -= PS.PrintableArea.Top 'e.PageSettings.PrintableArea.Top
    'End If
    Dim DotPen As New Pen(Color.Black), CS As New CalcHelper
    Dim BaseFont As Font = New Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point)
    y -= CS.MMtoIn(5)
    'G.DrawLine(DotPen, x, y, x + Width, y)
    y -= BaseFont.GetHeight(G)
    Dim strFormat As New StringFormat
    strFormat.LineAlignment = StringAlignment.Center
    strFormat.Alignment = CType(HorizontalAlignment, StringAlignment)
    G.DrawString(PageNumber, BaseFont, Brushes.Black, New RectangleF(x, y, Width, BaseFont.GetHeight(G)), strFormat)
  End Sub
  Public Sub DrawHeader(ByVal x As Single, ByVal y As Single, ByVal Width As Single)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  y -= PS.PrintableArea.Top 'e.PageSettings.PrintableArea.Top
    'End If
    Dim DotPen As New Pen(Color.Black), CS As New CalcHelper, DBA As New DataBaseAction, S As New SzkolaSQL
    Dim FooterFont As Font = New Font("Arial", 8, FontStyle.Italic, GraphicsUnit.Point)
    y -= CS.MMtoIn(5)

    G.DrawLine(DotPen, x, y, x + Width, y)
    y -= FooterFont.GetHeight(G)
    Dim strFormat As New StringFormat
    strFormat.LineAlignment = StringAlignment.Center
    strFormat.Alignment = StringAlignment.Near
    G.DrawString(DBA.GetSingleValue(S.SelectSchoolName(My.Settings.IdSchool)), FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(G)), strFormat)
    strFormat.Alignment = StringAlignment.Far
    G.DrawString("Rok szkolny: " & My.Settings.SchoolYear, FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(G)), strFormat)
  End Sub
  Public Sub DrawFooter(ByVal x As Single, ByVal y As Single, ByVal Width As Single)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  y -= PS.PrintableArea.Top 'e.PageSettings.PrintableArea.Top
    'End If
    Dim DotPen As New Pen(Color.Black), CS As New CalcHelper
    Dim FooterFont As Font = New Font("Arial", 8, FontStyle.Italic, GraphicsUnit.Point)
    y += CS.MMtoIn(5)
    G.DrawLine(DotPen, x, y, x + Width, y)
    y += FooterFont.GetHeight(G) / 2
    Dim strFormat As New StringFormat
    strFormat.LineAlignment = StringAlignment.Center
    strFormat.Alignment = StringAlignment.Near
    G.DrawString("Belfer .NET (wersja " & My.Application.Info.Version.ToString & ")", FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(G)), strFormat)
    strFormat.Alignment = StringAlignment.Far
    G.DrawString(DateTime.Now.ToString, FooterFont, Brushes.Black, New RectangleF(x, y, Width, FooterFont.GetHeight(G)), strFormat)
  End Sub
  Public Sub DrawLine(ByVal x0 As Single, ByVal y0 As Single, ByVal x1 As Single, y1 As Single, Optional PenWidth As Single = 1)
    x0 += My.Settings.XCaliber
    y0 += My.Settings.YCaliber
    x1 += My.Settings.XCaliber
    y1 += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x0 -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  x1 -= PS.PrintableArea.Left  'e.PageSettings.PrintableArea.Left
    '  y0 -= PS.PrintableArea.Top  'e.PageSettings.PrintableArea.Top
    '  y1 -= PS.PrintableArea.Top  'e.PageSettings.PrintableArea.Top
    'End If
    'e.Graphics.PageUnit = GraphicsUnit.Pixel
    G.DrawLine(New Pen(Brushes.Black, PenWidth), x0, y0, x1, y1)
    'e.Graphics.PageUnit = GraphicsUnit.Display

  End Sub
  Public Sub DrawLine(ByVal x0 As Single, ByVal y0 As Single, ByVal x1 As Single, y1 As Single, DrawPen As Pen)
    x0 += My.Settings.XCaliber
    y0 += My.Settings.YCaliber
    x1 += My.Settings.XCaliber
    y1 += My.Settings.YCaliber

    G.DrawLine(DrawPen, x0, y0, x1, y1)
    'e.Graphics.PageUnit = GraphicsUnit.Display

  End Sub
  Public Sub DrawRectangle(PenWidth As Single, ByVal x0 As Single, ByVal y0 As Single, ByVal Width As Single, Height As Single)
    x0 += My.Settings.XCaliber
    y0 += My.Settings.YCaliber
    'Width += My.Settings.XCaliber
    'Height += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x0 -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  'Width -= e.PageSettings.PrintableArea.Left
    '  y0 -= PS.PrintableArea.Top 'e.PageSettings.PrintableArea.Top
    '  'Height -= e.PageSettings.PrintableArea.Top
    'End If
    G.DrawRectangle(New Pen(Brushes.Black, PenWidth), x0, y0, Width, Height)
  End Sub
  Public Sub DrawImage(img As System.Drawing.Image, ByVal x As Single, ByVal y As Single, ByVal Width As Single, Height As Single)
    x += My.Settings.XCaliber
    y += My.Settings.YCaliber
    'If Not IsPreview Then
    '  x -= PS.PrintableArea.Left 'e.PageSettings.PrintableArea.Left
    '  y -= PS.PrintableArea.Top 'e.PageSettings.PrintableArea.Top

    'End If
    G.DrawImage(img, x, y, Width, Height)
  End Sub
End Class

