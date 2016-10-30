Imports System.Drawing.Printing
Public Class prnZagrozenia
  Private Zoom As Double = 0.5
  Private WithEvents Doc As New PrintReport(New DataSet)
  Private PrnVar As New PrintVariables
  Private TextFont As Font = PrnVar.BaseFont
  Private HeaderFont As Font = PrnVar.HeaderFont
  Private x0 As Single = PrnVar.LeftMargin - Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Left
  Private y0 As Single = PrnVar.TopMargin - Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Top

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ZagrozeniaRaportToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf FillKlasa
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ZagrozeniaRaportToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf FillKlasa
  End Sub
  Private Sub prnListaUczniow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf FillKlasa
    Dim CH As New CalcHelper
    Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    'ApplyNewConfig()
    FillKlasa()
    pvZagrozenia.Document = Doc
    pvZagrozenia.AutoZoom = True
    pvZagrozenia.Zoom = Zoom

  End Sub
  'Private Sub ApplyNewConfig()
  '  'Dim CH As New CalcHelper
  '  'Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
  '  'cbUczen.Items.Clear()
  '  FillKlasa()
  '  'nudSemestr_ValueChanged(Nothing, Nothing)
  'End Sub
  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    cbUczen.Items.Clear()
    cbUczen.Enabled = False
    Dim FCB As New FillComboBox, SH As New SeekHelper, O As New ObsadaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, CType(chkVirtual.CheckState, Byte).ToString))
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub

  Private Sub nudSemestr_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSemestr.ValueChanged
    If Me.Created AndAlso Me.cbKlasa.Text.Length > 0 Then cbKlasa_SelectedIndexChanged(Nothing, Nothing)
    '  Me.RefreshPupils()
    '  Me.Doc.DS = Me.FetchData()
    '  Me.Doc.PageNumber = 0
    '  pvZagrozenia.InvalidatePreview()
    'End If
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    If Me.Created Then
      Me.RefreshPupils()
      Me.Doc.DS = Me.FetchData
      Me.Doc.PageNumber = 0
      Me.gbPrintRange.Enabled = True
      Me.gbZoom.Enabled = True
      pvZagrozenia.InvalidatePreview()
    End If
  End Sub
  Private Sub RefreshPupils()
    'Me.cbUczen.Items.Clear()
    Dim FCB As New FillComboBox, Z As New ZagrozeniaSQL  ', CS As New CommonStrings
    FCB.AddComboBoxComplexItems(Me.cbUczen, Z.SelectStudent(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear, CType(nudSemestr.Value, Integer).ToString))
    rbAllStudents_CheckedChanged(Nothing, Nothing)
    'If Me.cbUczen.Items.Count > 0 Then
    '  'Me.cbUczen.SelectedIndex = 0
    '  Me.cbUczen.Enabled = True
    'Else
    '  Me.cbUczen.Enabled = False
    'End If
  End Sub

  Private Sub cbUczen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUczen.SelectedIndexChanged
    Me.Doc.PageNumber = 0
    pvZagrozenia.InvalidatePreview()
  End Sub

  Private Sub rbZoom100_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbZoom100.Click, rbZoom150.Click, rbZoom200.Click, rbZoom25.Click, rbZoom50.Click, rbZoom75.Click
    Me.Zoom = CType(CType(sender, RadioButton).Tag, Double)
    Me.pvZagrozenia.Zoom = Me.Zoom
    Me.Doc.PageNumber = 0
    Me.pvZagrozenia.InvalidatePreview()
  End Sub
  Private Sub rbAllStudents_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAllStudents.CheckedChanged, rbSelectedStudent.CheckedChanged
    If Me.rbAllStudents.Checked Then
      Me.cbUczen.Enabled = False
    Else
      Me.cbUczen.Enabled = CBool(IIf(Me.cbUczen.Items.Count > 0, True, False))
    End If
    Me.Doc.PageNumber = 0
    Me.pvZagrozenia.InvalidatePreview()
  End Sub

  Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    Dim prnDlg As New PrintDialog
    prnDlg.AllowSomePages = True
    prnDlg.AllowCurrentPage = True
    prnDlg.PrinterSettings.FromPage = 1
    prnDlg.PrinterSettings.ToPage = 1
    If prnDlg.ShowDialog = Windows.Forms.DialogResult.OK Then
      Me.Doc.PageNumber = 0
      Me.pvZagrozenia.Document.PrinterSettings.PrinterName = prnDlg.PrinterSettings.PrinterName
      Me.pvZagrozenia.Document.PrinterSettings.PrintRange = prnDlg.PrinterSettings.PrintRange
      Me.pvZagrozenia.Document.PrinterSettings.Copies = prnDlg.PrinterSettings.Copies
      Me.pvZagrozenia.Document.Print()
    End If
  End Sub
  Public Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles Doc.PrintPage
    If Me.cbKlasa.SelectedItem Is Nothing Then Exit Sub
    Doc.PageNumber += 1
    Me.pvZagrozenia.Rows = Doc.PageNumber

    If Me.rbAllStudents.Checked Then
      Me.PrintZagrozeniaForAllStudents(e)
    Else
      If Not Me.cbUczen.SelectedItem Is Nothing Then Me.PrintZagrozeniaForSelectedStudent(e)
    End If
  End Sub
  Private Function FetchData() As DataSet
    Dim DBA As New DataBaseAction, Z As New ZagrozeniaSQL
    Return DBA.GetDataSet(Z.SelectZagrozenie(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear, CType(nudSemestr.Value, Integer).ToString))
  End Function

  Private Sub PrintZagrozeniaForAllStudents(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth, ColHeight As Single
    ColWidth = CSng(e.PageBounds.Width) - 2 * x0
    ColHeight = CSng(e.PageBounds.Height / 2)

    DrawDivisionLines(e)

    Dim x, y As Single
    x = x0 : y = y0
    Dim i As Integer = 1
    Do While (i < 3) And (Doc.Offset(0) < Doc.DS.Tables(0).Rows.Count)
      '--------------------------------------- Nag³ówek --------------------------------------
      Dim Adresat(1) As String
      Adresat(0) = IIf(chkMatka.Checked, Doc.DS.Tables(0).Rows(Doc.Offset(0)).Item(1).ToString, "").ToString

      Adresat(1) = IIf(chkOjciec.Checked, Doc.DS.Tables(0).Rows(Doc.Offset(0)).Item(2).ToString, "").ToString

      PrintHeader(e, x + (ColWidth * 2 / 3), y, ColWidth / 3, Adresat)
      y += HeaderLineHeight * 4

      '--------------------------------------- Treœæ --------------------------------------

      PrintBody(e, x, y, ColWidth, New String() {Doc.DS.Tables(0).Rows(Doc.Offset(0)).Item(3).ToString, Doc.DS.Tables(0).Rows(Doc.Offset(0)).Item(4).ToString, Doc.DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString})
      y += CSng(TextLineHeight * 5)

      '------------------------------------- Stopka ---------------------------------------
      PrintFooter(e, x, y, ColWidth)

      Doc.Offset(0) += 1
      i += 1
      y = CSng(IIf(i > 1, y0 + ColHeight, y0))
      x = x0
    Loop
    If Doc.Offset(0) < Doc.DS.Tables(0).Rows.Count Then
      i = 1
      e.HasMorePages = True
    Else
      Doc.Offset(0) = 0
    End If
  End Sub
  Private Sub PrintZagrozeniaForSelectedStudent(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth, ColHeight As Single
    ColWidth = CSng(e.PageBounds.Width) - 2 * x0
    ColHeight = CSng(e.PageBounds.Height / 2)
    DrawDivisionLines(e)

    Dim x, y As Single
    x = x0 : y = y0
    Dim FoundItem() As DataRow
    FoundItem = Doc.DS.Tables(0).Select("ID=" + CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString)

    '--------------------------------------- Nag³ówek --------------------------------------
    Dim Adresat(1) As String
    Adresat(0) = IIf(chkMatka.Checked, FoundItem(0).Item(1).ToString, "").ToString

    Adresat(1) = IIf(chkOjciec.Checked, FoundItem(0).Item(2).ToString, "").ToString

    'PrintHeader(e, x + (ColWidth * 2 / 3), y, ColWidth / 3, New String() {FoundItem(0).Item(1).ToString, FoundItem(0).Item(2).ToString})
    PrintHeader(e, x + (ColWidth * 2 / 3), y, ColWidth / 3, Adresat)
    y += HeaderLineHeight * 4

    '--------------------------------------- Treœæ --------------------------------------

    PrintBody(e, x, y, ColWidth, New String() {FoundItem(0).Item(3).ToString, FoundItem(0).Item(4).ToString, FoundItem(0).Item(0).ToString})
    y += CSng(TextLineHeight * 5)

    '------------------------------------- Stopka ---------------------------------------
    PrintFooter(e, x, y, ColWidth)

  End Sub
  Private Sub PrintBody(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal Uczen() As String, Optional ByVal k As Single = 1)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim FoundObject() As DataRow
    Dim InformationContents As New ArrayList
    InformationContents.AddRange(New String() {"Informujê, ¿e ", IIf(Uczen(1) = "1", "syn ", "córka ").ToString, Uczen(2) + ", ", IIf(Uczen(1) = "1", "uczeñ kl. ", "uczennica kl. ").ToString + Me.cbKlasa.Text + ", ", "w klasyfikacji za ", IIf(Me.nudSemestr.Value = 1, "I semestr ", "rok szkolny ").ToString, "mo¿e ", "otrzymaæ ", "oceny ", "niedostateczne ", "z nastêpuj¹cych ", "przedmiotów: "})
    FoundObject = Doc.DS.Tables(1).Select("IdPrzydzial=" & Uczen(0))
    Do Until (Doc.Offset(1) > FoundObject.GetUpperBound(0))
      InformationContents.Add(FoundObject(Doc.Offset(1)).Item(0).ToString + ", ")
      Doc.Offset(1) += 1
    Loop
    InformationContents.Item(InformationContents.Count - 1) = InformationContents.Item(InformationContents.Count - 1).ToString.TrimEnd(", ".ToCharArray) + "."
    Dim TextToPrint() As String = CType(InformationContents.ToArray(GetType(String)), String())

    Dim Line As New System.Text.StringBuilder, idx As Integer, TabIndent As Integer = 39
    Do
      Line.Append(TextToPrint(idx))
      If e.Graphics.MeasureString(Line.ToString, TextFont).Width + TabIndent > PrintWidth Then
        Line.Remove(Line.ToString.Length - TextToPrint(idx).Length - 1, TextToPrint(idx).Length)
        Doc.DrawText(e, Line.ToString, TextFont, x + TabIndent, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
        y += TextLineHeight
        TabIndent = 0
        idx -= 1
        Line = New System.Text.StringBuilder
      End If
      idx += 1
    Loop While TextToPrint.GetUpperBound(0) >= idx
    Doc.DrawText(e, Line.ToString, TextFont, x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)

    y += TextLineHeight '* LineNumber
    Doc.DrawText(e, "Proszê o jak najszybszy kontakt z nauczycielami wymienionych przedmiotów.", TextFont, x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
    Doc.Offset(1) = 0
  End Sub
  Private Sub DrawDivisionLines(ByVal e As PrintPageEventArgs)
    Dim DotPen As New Pen(Color.Black)
    DotPen.DashStyle = Drawing2D.DashStyle.Dot
    'e.Graphics.DrawLine(DotPen,  CSng(e.PageBounds.Width / 2), 0, CSng(e.PageBounds.Width / 2), e.PageBounds.Height)
    e.Graphics.DrawLine(DotPen, e.PageBounds.X, CSng(e.PageBounds.Height / 2), e.PageBounds.Width, CSng(e.PageBounds.Height / 2))
  End Sub
  Private Sub PrintHeader(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal ParentsName As String())
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Doc.DrawText(e, "Susz, dnia " + Me.dtData.Value.ToLongDateString + " r.", TextFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
    y += HeaderLineHeight * 4
    Doc.DrawText(e, "Sz. P.", HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
    y += HeaderLineHeight
    If ParentsName(0).Length > 0 Then
      Doc.DrawText(e, ParentsName(0), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      y += HeaderLineHeight
    End If
    Doc.DrawText(e, ParentsName(1), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
  End Sub
  Private Sub PrintFooter(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Doc.DrawText(e, "Wychowawca klasy", New Font(TextFont, FontStyle.Bold), x, y, PrintWidth / 3, TextLineHeight, 1, Brushes.Black, False)
    Doc.DrawText(e, "Data i podpis rodziców", New Font(TextFont, FontStyle.Bold), x + PrintWidth * 2 / 3, y, PrintWidth / 3, TextLineHeight, 1, Brushes.Black, False)
  End Sub
  Private Sub dtData_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtData.ValueChanged
    Me.Doc.PageNumber = 0
    Me.pvZagrozenia.InvalidatePreview()
  End Sub

  Private Sub chkMatka_CheckedChanged(sender As Object, e As EventArgs) Handles chkMatka.CheckedChanged, chkOjciec.CheckedChanged
    Me.Doc.PageNumber = 0
    Me.pvZagrozenia.InvalidatePreview()
  End Sub

  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkVirtual.CheckedChanged
    If Not Me.Created Then Exit Sub
    FillKlasa()
  End Sub
End Class