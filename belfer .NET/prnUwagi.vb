Imports System.Drawing.Printing
Public Class prnUwagi
  Private Zoom As Double = 1
  Private WithEvents Doc As New PrintReport(New DataSet)
  Private PrnVar As New PrintVariables
  Private TextFont As Font = PrnVar.BaseFont
  Private HeaderFont As Font = PrnVar.HeaderFont
  Private LeftMargin As Single = PrnVar.LeftMargin, TopMargin As Single = PrnVar.TopMargin

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.UwagiRaportToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.UwagiRaportToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Function FetchData() As DataSet
    Dim DBA As New DataBaseAction, UR As New UwagiRaportSQL
    Dim DS As New DataSet()
    DS.Tables.Add(DBA.GetDataTable(UR.SelectNote(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, dtStartDate.Value.ToString("yyyy-MM-dd"), dtEndDate.Value.ToString("yyyy-MM-dd"))))
    DS.Tables(0).TableName = "Uwagi"
    Return DS
  End Function
  Private Sub prnListaUczniow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    'dtStartDate.Value = Date.Today
    Try
      rbZoom100.Checked = True
      ApplyNewConfig()
      pvAbsencja.Document = Doc
      pvAbsencja.AutoZoom = True
      pvAbsencja.Zoom = Zoom
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub ApplyNewConfig()
    EnableOptions(False)
    Dim CH As New CalcHelper
    dtStartDate.MinDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    dtStartDate.MaxDate = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)
    dtStartDate.Value = CType(IIf(Today < CH.StartDateOfSemester2(dtStartDate.MinDate.Year), dtStartDate.MinDate, CH.StartDateOfSemester2(dtStartDate.MinDate.Year)), Date)
    dtEndDate.MinDate = dtStartDate.MinDate
    dtEndDate.MaxDate = dtStartDate.MaxDate
    dtEndDate.Value = Today
    'GetSchoolPlace()
    FillKlasa()
  End Sub
  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, SH As New SeekHelper, K As New KolumnaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub FillStudent()
    cbUczen.Items.Clear()
    Dim FCB As New FillComboBox, WR As New WynikiRaportSQL
    FCB.AddComboBoxComplexItems(Me.cbUczen, WR.SelectStudentList(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear))
    If rbSelectedStudent.Checked AndAlso Me.cbUczen.Items.Count > 0 Then
      cbUczen.Enabled = True
      Me.cbUczen.SelectedIndex = 0
    Else
      cbUczen.Enabled = False
    End If
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    If Not Me.Created Then Exit Sub
    FillStudent()
    Me.Doc.DS = Me.FetchData

    Me.Doc.PageNumber = 0
    EnableOptions(True)
    pvAbsencja.InvalidatePreview()
  End Sub
  Private Sub EnableOptions(Value As Boolean)
    dtStartDate.Enabled = Value
    dtEndDate.Enabled = Value
    Me.gbPrintRange.Enabled = Value
    Me.gbZoom.Enabled = Value
  End Sub

  Private Sub cbUczen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUczen.SelectedIndexChanged
    Me.Doc.PageNumber = 0
    pvAbsencja.InvalidatePreview()
  End Sub

  Private Sub rbZoom100_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbZoom100.CheckedChanged, rbZoom200.CheckedChanged, rbZoom50.CheckedChanged
    If Not Me.Created Then Exit Sub
    Me.Zoom = CType(CType(sender, RadioButton).Tag, Double)
    Me.pvAbsencja.Zoom = Me.Zoom
  End Sub
  Private Sub rbZoomCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbZoomCustom.CheckedChanged
    If rbZoomCustom.Checked Then
      tbZoom.Enabled = True
      nudZoom.Enabled = True
      Me.Zoom = tbZoom.Value * 0.01
      Me.pvAbsencja.Zoom = Me.Zoom
    Else
      tbZoom.Enabled = False
      nudZoom.Enabled = False
    End If
  End Sub

  Private Sub tbZoom_Scroll(sender As Object, e As EventArgs) Handles tbZoom.Scroll
    Me.Zoom = tbZoom.Value * 0.01
    Me.pvAbsencja.Zoom = Me.Zoom
    nudZoom.Value = tbZoom.Value
  End Sub
  Private Sub nudZoom_ValueChanged(sender As Object, e As EventArgs) Handles nudZoom.ValueChanged
    If Not Me.Created Then Exit Sub
    tbZoom.Value = CType(nudZoom.Value, Integer)
    Me.pvAbsencja.Zoom = nudZoom.Value * 0.01
  End Sub
  Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    Dim prnDlg As New PrintDialog
    'Preview = False
    prnDlg.AllowSomePages = True
    prnDlg.AllowCurrentPage = True
    prnDlg.PrinterSettings.FromPage = 1
    prnDlg.PrinterSettings.ToPage = 1
    If prnDlg.ShowDialog = Windows.Forms.DialogResult.OK Then
      'Doc.IsPreview = False
      Me.Doc.PageNumber = 0
      pvAbsencja.Document.DefaultPageSettings.PrinterSettings.Copies = prnDlg.PrinterSettings.Copies
      Me.pvAbsencja.Document.DefaultPageSettings.PrinterSettings.FromPage = prnDlg.PrinterSettings.FromPage
      Me.pvAbsencja.Document.DefaultPageSettings.PrinterSettings.ToPage = prnDlg.PrinterSettings.ToPage
      Me.pvAbsencja.Document.DefaultPageSettings.PrinterSettings.PrinterName = prnDlg.PrinterSettings.PrinterName

      Me.pvAbsencja.Document.Print()

      'Doc.IsPreview = True
    End If
    'Preview = True
  End Sub


  Private Sub rbAllStudents_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAllStudents.CheckedChanged, rbSelectedStudent.CheckedChanged
    If Me.rbAllStudents.Checked Then
      Me.cbUczen.Enabled = False
    Else
      Me.cbUczen.Enabled = True
    End If
    Me.Doc.PageNumber = 0
    Me.pvAbsencja.InvalidatePreview()
  End Sub

  Private Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles Doc.PrintPage
    Try
      If Me.cbKlasa.SelectedItem Is Nothing Then Exit Sub

      Doc.PageNumber += 1
      Me.pvAbsencja.Rows = Doc.PageNumber
      LeftMargin = PrnVar.LeftMargin
      TopMargin = PrnVar.TopMargin
      Doc.DrawHeader(e, LeftMargin, e.MarginBounds.Top, CSng(e.MarginBounds.Width))
      Doc.DrawFooter(e, LeftMargin, e.MarginBounds.Bottom, CSng(e.MarginBounds.Width))
      Doc.DrawPageNumber(e, Doc.PageNumber.ToString, LeftMargin, TopMargin, e.MarginBounds.Width)

      If Me.rbAllStudents.Checked Then
        Me.PrintNoteForAllStudents(e)
      Else
        If cbUczen.SelectedItem Is Nothing Then Exit Sub
        PrintNoteForSelectedStudent(e)
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub PrintNoteForAllStudents(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth As Single ', ColHeight As Single
    ColWidth = e.MarginBounds.Width 'CSng(e.PageBounds.Width / 2) - 2 * LeftMargin

    Dim x, y As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin
    If Doc.PageNumber = 1 Then
      PrintHeader(e, x, y, ColWidth, "Uwagi o zachowaniu uczniów kl. " & cbKlasa.Text, 1)
      y += HeaderLineHeight * CSng(1.2)
      PrintHeader(e, x, y, ColWidth, "w okresie od " & dtStartDate.Value.ToString("dd-MM-yyyy") & " do " & dtEndDate.Value.ToString("dd-MM-yyyy"), 1)
      y += HeaderLineHeight * 3
    End If
    Dim Student As DataTable = Doc.DS.Tables("Uwagi").DefaultView.ToTable(True, "IdUczen", "Student")
    Do While ((y + HeaderLineHeight * 5) < e.MarginBounds.Bottom) And (Doc.Offset(0) < Student.Rows.Count)
      '--------------------------------------- Nagłówek --------------------------------------
      If Doc.Offset(1) = 0 AndAlso Doc.Offset(2) = 0 AndAlso Doc.Offset(3) = 0 Then
        PrintHeader(e, x, y, ColWidth, Student.Rows(Doc.Offset(0)).Item("Student").ToString, 1, True)
        y += CSng(TextLineHeight * 1.5)
      End If

      '--------------------------------------- Wykaz nieobecności --------------------------------------
      If Not PrintNote(e, x, y, ColWidth, Student.Rows(Doc.Offset(0)).Item("IdUczen").ToString, 1.2) Then Exit Do
      Doc.Offset(0) += 1
    Loop
    If Doc.Offset(0) < Student.Rows.Count Then
      e.HasMorePages = True
    Else
      Doc.Offset(0) = 0
    End If
  End Sub

  Private Sub PrintNoteForSelectedStudent(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth As Single ', ColHeight As Single
    ColWidth = e.MarginBounds.Width 'CSng(e.PageBounds.Width / 2) - 2 * LeftMargin

    Dim x, y As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin
    If Doc.PageNumber = 1 Then
      PrintHeader(e, x, y, ColWidth, "Uwagi o zachowaniu uczniów kl. " & cbKlasa.Text, 1)
      y += HeaderLineHeight * CSng(1.2)
      PrintHeader(e, x, y, ColWidth, "w okresie od " & dtStartDate.Value.ToString("dd-MM-yyyy") & " do " & dtEndDate.Value.ToString("dd-MM-yyyy"), 1)
      y += HeaderLineHeight * 3
    End If
    '--------------------------------------- Nagłówek --------------------------------------
    If Doc.Offset(1) = 0 AndAlso Doc.Offset(2) = 0 AndAlso Doc.Offset(3) = 0 Then
      PrintHeader(e, x, y, ColWidth, cbUczen.Text, 1)
      y += CSng(TextLineHeight * 1.5)
    End If
    '--------------------------------------- Wykaz nieobecności --------------------------------------
    If Not PrintNote(e, x, y, ColWidth, CType(cbUczen.SelectedItem, CbItem).ID.ToString, 1.2) Then e.HasMorePages = True

  End Sub


  Private Sub PrintAbsenceAmount(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, Optional ByVal k As Single = 1)
    Dim DV As DataView = New DataView(Doc.DS.Tables("Uwagi"), "IdUczen=" & IdUczen, "Data ASC", DataViewRowState.CurrentRows)
    Dim Note As DataTable = DV.ToTable(False, "IdUczen", "TypUwagi")
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Doc.DrawText(e, "Łączna liczba uwag:", New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
    y += TextLineHeight * k

    For Each NoteType As String In "np"
      If Note.Select("TypUwagi='" & NoteType & "'").GetLength(0) > 0 Then
        Doc.DrawText(e, String.Concat(IIf(NoteType = "n", "negatywnych", "pozytywnych").ToString, " ", Chr(150), " ", Note.Select("TypUwagi='" & NoteType & "'").GetLength(0)), TextFont, x, y, PrintWidth / 2, TextLineHeight, 0, Brushes.Black, False)
      Else
        Doc.DrawText(e, String.Concat(IIf(NoteType = "n", "negatywnych", "pozytywnych").ToString, " ", Chr(150), " 0"), TextFont, x, y, PrintWidth / 2, TextLineHeight, 0, Brushes.Black, False)
      End If
      x += PrintWidth / 2
    Next
  End Sub

  Private Function PrintNote(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, Optional ByVal Interlinia As Single = 1) As Boolean
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim DV As DataView = New DataView(Doc.DS.Tables("Uwagi"), "IdUczen=" & IdUczen, "Data ASC", DataViewRowState.CurrentRows)
    Dim TypUwagi As DataTable = DV.ToTable(True, "TypUwagi")
    'Dim FoundScore() As DataRow
    Dim Kolumna As New List(Of Pole) From
    {
      New Pole With {.Name = "Data", .Size = CInt(PrintWidth / 2)},
      New Pole With {.Name = "Autor", .Size = CInt(PrintWidth / 2)}
    }
    Do Until ((y + TextLineHeight * CSng(3.5)) > e.MarginBounds.Bottom) Or (Doc.Offset(1) > TypUwagi.Rows.Count - 1)
      If Doc.Offset(2) = 0 AndAlso Doc.Offset(3) = 0 Then
        Doc.DrawText(e, IIf(TypUwagi.Rows(Doc.Offset(1)).Item("TypUwagi").ToString = "p", "pozytywne", "negatywne").ToString.ToUpper, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
        y += TextLineHeight '* Interlinia  'CSng(1.5)
        Doc.DrawLine(e, x, y, x + Kolumna(0).Size, y)
        y += TextLineHeight   'CSng(1.5)
        'PrintTableHeader(e, x, y, Kolumna)
      End If

      Dim Uwaga As DataRow() = DV.ToTable(False, "Data", "Autor", "TrescUwagi", "TypUwagi").Select("TypUwagi='" & TypUwagi.Rows(Doc.Offset(1)).Item("TypUwagi").ToString & "'")
      Do Until ((y + TextLineHeight) > e.MarginBounds.Bottom) Or (Doc.Offset(2) > Uwaga.GetLength(0) - 1)
        If Doc.Offset(3) = 0 Then
          Dim ColSize As Integer = 0
          Doc.DrawText(e, String.Concat("Data: ", CType(Uwaga(Doc.Offset(2)).Item("Data"), Date).ToShortDateString, " ", Chr(150), " ", CType(Uwaga(Doc.Offset(2)).Item("Data"), Date).ToString("dddd")), New Font(TextFont, FontStyle.Bold), x + ColSize, y, Kolumna.Item(0).Size, TextLineHeight, 0, Brushes.Black, False)
          ColSize += Kolumna.Item(0).Size
          Doc.DrawText(e, "Autor: " & Uwaga(Doc.Offset(2)).Item("Autor").ToString, New Font(TextFont, FontStyle.Bold), x + ColSize, y, Kolumna.Item(1).Size, TextLineHeight, 0, Brushes.Black, False)
          y += TextLineHeight * Interlinia
        End If
        If Not PrintBody(e, Uwaga(Doc.Offset(2)).Item("TrescUwagi").ToString.Split(" ".ToCharArray), x, y, PrintWidth) Then Return False
        y += TextLineHeight * 2
        Doc.Offset(2) += 1
      Loop
      If Doc.Offset(2) < Uwaga.GetLength(0) Then
        e.HasMorePages = True
        Return False
      Else
        'y += TextLineHeight
        Doc.Offset(2) = 0
        Doc.Offset(1) += 1
      End If
      'ulepszyć, zrobić dla jednego ucznia
      'y += TextLineHeight
    Loop
    If Doc.Offset(1) < TypUwagi.Rows.Count Then
      e.HasMorePages = True
      Return False
    Else
      '------------------------------------ Podsumowanie nieobecności -------------------------------------------
      If y + TextLineHeight * 2.2 < e.MarginBounds.Bottom Then
        PrintAbsenceAmount(e, x, y, PrintWidth, IdUczen, 1.2)
        y += TextLineHeight * 3
      Else
        Return False
      End If
      Doc.Offset(1) = 0
    End If
    Return True
    'Doc.Offset(1) = 0
  End Function
  Private Function PrintBody(ByVal e As PrintPageEventArgs, NoteContent As String(), ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, Optional ByVal k As Single = 1) As Boolean
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim Line As New System.Text.StringBuilder, TabIndent As Integer = 20

    Do
      Line.Append(String.Concat(NoteContent(Doc.Offset(3)), " "))
      If e.Graphics.MeasureString(Line.ToString, TextFont).Width + TabIndent > PrintWidth Then
        Line.Remove(Line.ToString.Length - NoteContent(Doc.Offset(3)).Length - 1, NoteContent(Doc.Offset(3)).Length)
        Doc.DrawText(e, Line.ToString, TextFont, x + TabIndent, y, PrintWidth, TextLineHeight * k, 0, Brushes.Black, False)
        y += TextLineHeight
        'TabIndent = 0
        Doc.Offset(3) -= 1
        Line = New System.Text.StringBuilder
      End If
      Doc.Offset(3) += 1
    Loop While NoteContent.GetUpperBound(0) >= Doc.Offset(3) AndAlso e.MarginBounds.Bottom >= y + TextLineHeight
    If e.MarginBounds.Bottom >= y + TextLineHeight Then
      Doc.DrawText(e, Line.ToString, TextFont, x + TabIndent, y, PrintWidth, TextLineHeight * k, 0, Brushes.Black, False)
      Doc.Offset(3) = 0
      Return True
    Else
      Doc.Offset(3) -= 1
      Return False
    End If
  End Function
 
  'Private Sub PrintTableHeader(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal Kolumna As List(Of Pole))
  '  Dim TableHeaderFont As Font = New Font(TextFont, FontStyle.Bold)
  '  Dim TextLineHeight As Single = TableHeaderFont.GetHeight(e.Graphics) * CSng(1.5)

  '  Dim ColSize As Integer = 0
  '  For Each Col In Kolumna
  '    With Col
  '      Doc.DrawText(e, .Name, TableHeaderFont, x + ColSize, y, .Size, TextLineHeight, 1, Brushes.Black, True)
  '      ColSize += .Size
  '    End With
  '  Next
  '  y += TextLineHeight
  'End Sub
  Private Sub PrintHeader(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal HeaderText As String, Optional Alignment As Byte = 0, Optional FillBackground As Boolean = False)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Doc.DrawText(e, HeaderText, HeaderFont, x, y, PrintWidth, HeaderLineHeight, Alignment, Brushes.Black, False, , FillBackground)
  End Sub

  Private Sub dtData_ValueChanged(sender As Object, e As EventArgs) Handles dtStartDate.ValueChanged, dtEndDate.ValueChanged
    'coś tu nie gra
    If cbKlasa.SelectedItem Is Nothing Then Exit Sub
    Me.Doc.DS = Me.FetchData
    Me.Doc.PageNumber = 0
    pvAbsencja.InvalidatePreview()
  End Sub

  Private Sub cmdPageSetup_Click(sender As Object, e As EventArgs) Handles cmdPageSetup.Click
    Dim pg As New PageSetupDialog, prnVars As New PrintVariables
    pg.EnableMetric = True
    pg.Document = Doc
    pg.MinMargins.Left = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Left)
    pg.MinMargins.Top = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Top)
    pg.AllowPaper = False
    If pg.ShowDialog = Windows.Forms.DialogResult.OK Then
      prnVars.Landscape = pg.PageSettings.Landscape
      prnVars.LeftMargin = pg.PageSettings.Margins.Left
      prnVars.TopMargin = pg.PageSettings.Margins.Top
      prnVars.RightMargin = pg.PageSettings.Margins.Right
      prnVars.BottomMargin = pg.PageSettings.Margins.Bottom
      pvAbsencja.Rows = 1
      Doc.PageNumber = 0
      Me.Doc.Offset(0) = 0
      Me.Doc.Offset(1) = 0
      Me.Doc.Lp = 0
      pvAbsencja.InvalidatePreview()
    End If
  End Sub


End Class