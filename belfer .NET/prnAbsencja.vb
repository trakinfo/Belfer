Imports System.Drawing.Printing
Public Class prnAbsencja
  Private Zoom As Double = 1
  Private WithEvents Doc As New PrintReport(New DataSet)
  Private PrnVar As New PrintVariables
  Private TextFont As Font = PrnVar.BaseFont
  Private HeaderFont As Font = PrnVar.HeaderFont
  Private LeftMargin As Single = PrnVar.LeftMargin, TopMargin As Single = PrnVar.TopMargin

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.AbsencjaRaportToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.AbsencjaRaportToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Function FetchData() As DataSet
    Dim DBA As New DataBaseAction, AR As New AbsencjaRaportSQL
    Dim DS As New DataSet()
    DS.Tables.Add(DBA.GetDataTable(AR.SelectAbsence(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, dtStartDate.Value.ToString("yyyy-MM-dd"), dtEndDate.Value.ToString("yyyy-MM-dd"))))
    DS.Tables(0).TableName = "Absencja"
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
    Dim Semestr2 As Date = CH.StartDateOfSemester2(dtStartDate.MinDate.Year)
    dtStartDate.Value = CType(IIf(Today < Semestr2, dtStartDate.MinDate, Semestr2), Date)
    'dtStartDate.Value = CType(IIf(Today < CH.StartDateOfSemester2(dtStartDate.MinDate.Year), dtStartDate.MinDate, CH.StartDateOfSemester2(dtStartDate.MinDate.Year)), Date)
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
  'Private Sub GetSchoolPlace()
  '  Try
  '    Dim DBA As New DataBaseAction, WR As New WynikiRaportSQL
  '    Dim RH(0) As String
  '    RH(0) = DBA.GetSingleValue(WR.SelectSchoolPlace)
  '    Doc.ReportHeader = RH
  '  Catch ex As Exception
  '    MessageBox.Show(ex.Message)
  '  End Try
  'End Sub

  'Private Sub nudSemestr_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  If Me.Created AndAlso Me.cbKlasa.Text.Length > 0 Then
  '    Me.Doc.DS = Me.FetchData()
  '    Me.Doc.PageNumber = 0
  '    pvAbsencja.InvalidatePreview()
  '  End If
  'End Sub

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
  'Private Sub GetTutorFaximile()
  '  Dim DBA As New DataBaseAction, WR As New WynikiRaportSQL
  '  Dim Reader As MySqlDataReader = DBA.GetReader(WR.SelectFaximile(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString))
  '  Try
  '    If Reader.HasRows Then
  '      Reader.Read()
  '      If Not Reader.IsDBNull(0) Then Doc.Podpis = CType(Reader.Item(0), Byte())
  '      'Else
  '      'Doc.Podpis = Nothing
  '    End If
  '  Catch ex As Exception
  '    MessageBox.Show(ex.Message)
  '  Finally
  '    Reader.Close()
  '  End Try
  'End Sub
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

  'Private Sub rbEndingScores_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  If Not Me.Created Then Exit Sub
  '  If Me.rbEndingScores.Checked Then
  '    SetObjectFieldWidth(New DataView(Doc.DS.Tables("Przedmiot")).ToTable(True, "Nazwa"))
  '    Me.chkTableSet.Enabled = CType(Me.rbAllStudents.Checked, Boolean)
  '    If Me.rbAllStudents.Checked Then Me.Doc.DefaultPageSettings.Landscape = CType(Me.chkTableSet.Checked, Boolean)
  '  Else
  '    SetObjectFieldWidth(New DataView(Doc.DS.Tables("Przedmiot"), "Typ<>'z' AND Typ<>'F'", "Priorytet ASC", DataViewRowState.CurrentRows).ToTable(True, "Alias"))
  '    'ObjectFieldWidth += 20
  '    Me.chkTableSet.Enabled = False
  '    Me.Doc.DefaultPageSettings.Landscape = False
  '  End If
  '  Me.Doc.PageNumber = 0
  '  Me.pvAbsencja.InvalidatePreview()
  'End Sub
  'Private Sub SetObjectFieldWidth(ObjectTable As DataTable)
  '  Dim G As Graphics = pvAbsencja.CreateGraphics
  '  ObjectFieldWidth = 0
  '  For Each R As DataRow In ObjectTable.Rows  'Doc.DS.Tables("Przedmiot").Select("Typ<>'z' AND Typ<>'F'")
  '    If G.MeasureString(R.Item(0).ToString, TextFont).Width > ObjectFieldWidth Then ObjectFieldWidth = G.MeasureString(R.Item(0).ToString, TextFont).Width
  '  Next
  'End Sub
  'Private Sub chkTableSet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  If Not Me.Created Then Exit Sub
  '  Me.Doc.DefaultPageSettings.Landscape = CType(Me.chkTableSet.Checked, Boolean)
  '  Doc.PageNumber = 0
  '  Me.pvAbsencja.InvalidatePreview()
  'End Sub

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
    If Me.cbKlasa.SelectedItem Is Nothing Then Exit Sub

    Doc.PageNumber += 1
    Me.pvAbsencja.Rows = Doc.PageNumber
    LeftMargin = PrnVar.LeftMargin
    TopMargin = PrnVar.TopMargin
    Doc.DrawHeader(e, LeftMargin, e.MarginBounds.Top, CSng(e.MarginBounds.Width))
    Doc.DrawFooter(e, LeftMargin, e.MarginBounds.Bottom, CSng(e.MarginBounds.Width))
    Doc.DrawPageNumber(e, Doc.PageNumber.ToString, LeftMargin, TopMargin, e.MarginBounds.Width)

    If Me.rbAllStudents.Checked Then
      Me.PrintAbsenceForAllStudents(e)
    Else
      If cbUczen.SelectedItem Is Nothing Then Exit Sub
      PrintAbsenceForSelectedStudent(e)
    End If
  End Sub

  Private Sub PrintAbsenceForAllStudents(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth As Single ', ColHeight As Single
    ColWidth = e.MarginBounds.Width 'CSng(e.PageBounds.Width / 2) - 2 * LeftMargin

    Dim x, y As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin
    If Doc.PageNumber = 1 Then
      PrintHeader(e, x, y, ColWidth, "Wykaz nieobecności uczniów kl. " & cbKlasa.Text, 1)
      y += HeaderLineHeight * CSng(1.2)
      PrintHeader(e, x, y, ColWidth, "w okresie od " & dtStartDate.Value.ToString("dd-MM-yyyy") & " do " & dtEndDate.Value.ToString("dd-MM-yyyy"), 1)
      y += HeaderLineHeight * 3
    End If
    Dim Student As DataTable = Doc.DS.Tables("Absencja").DefaultView.ToTable(True, "IdUczen", "Student")
    Do While ((y + HeaderLineHeight * 5) < e.MarginBounds.Bottom) And (Doc.Offset(0) < Student.Rows.Count)
      '--------------------------------------- Nagłówek --------------------------------------
      'y += HeaderLineHeight * 2
      'usunac duplikaty nazwisk i miesiecy na nastepnej stronie
      If Doc.Offset(1) = 0 AndAlso Doc.Offset(2) = 0 Then
        PrintHeader(e, x, y, ColWidth, Student.Rows(Doc.Offset(0)).Item("Student").ToString, 1)
        y += CSng(TextLineHeight * 1.5)
      End If


      '--------------------------------------- Wykaz nieobecności --------------------------------------
      If Not PrintAbsence(e, x, y, ColWidth, Student.Rows(Doc.Offset(0)).Item("IdUczen").ToString, 1.2) Then Exit Do
      Doc.Offset(0) += 1
    Loop
    If Doc.Offset(0) < Student.Rows.Count Then
      e.HasMorePages = True
    Else
      Doc.Offset(0) = 0
    End If
  End Sub

  Private Sub PrintAbsenceForSelectedStudent(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth As Single ', ColHeight As Single
    ColWidth = e.MarginBounds.Width 'CSng(e.PageBounds.Width / 2) - 2 * LeftMargin

    Dim x, y As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin
    If Doc.PageNumber = 1 Then
      PrintHeader(e, x, y, ColWidth, "Wykaz nieobecności ucznia kl. " & cbKlasa.Text, 1)
      y += HeaderLineHeight * CSng(1.2)
      PrintHeader(e, x, y, ColWidth, "w okresie od " & dtStartDate.Value.ToString("dd-MM-yyyy") & " do " & dtEndDate.Value.ToString("dd-MM-yyyy"), 1)
      y += HeaderLineHeight * 3
    End If
    '--------------------------------------- Nagłówek --------------------------------------
    If Doc.Offset(1) = 0 AndAlso Doc.Offset(2) = 0 Then
      PrintHeader(e, x, y, ColWidth, cbUczen.Text, 1)
      y += CSng(TextLineHeight * 1.5)
    End If
    '--------------------------------------- Wykaz nieobecności --------------------------------------
    If Not PrintAbsence(e, x, y, ColWidth, CType(cbUczen.SelectedItem, CbItem).ID.ToString, 1.2) Then e.HasMorePages = True

  End Sub

  
  Private Sub PrintAbsenceAmount(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, Optional ByVal k As Single = 1)
    Dim DV As DataView = New DataView(Doc.DS.Tables("Absencja"), "IdUczen=" & IdUczen, "Data ASC", DataViewRowState.CurrentRows)
    Dim Absence As DataTable = DV.ToTable(False, "IdUczen", "Status")
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Doc.DrawText(e, "Łączna liczba opuszczonych godzin:", New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
    y += TextLineHeight * k
    Dim Status As New Hashtable
    Status.Add("u", "usprawiedliwionych")
    Status.Add("n", "nieusprawiedliwionych")
    Status.Add("s", "spóźnień")
    For Each AbsenceType As String In "uns"
      If Absence.Select("Status='" & AbsenceType & "'").GetLength(0) > 0 Then
        Doc.DrawText(e, String.Concat(Status(AbsenceType), " ", Chr(150), " ", Absence.Select("Status='" & AbsenceType & "'").GetLength(0)), TextFont, x, y, PrintWidth / 3, TextLineHeight, 0, Brushes.Black, False)
      Else
        Doc.DrawText(e, String.Concat(Status(AbsenceType), " ", Chr(150), " 0"), TextFont, x, y, PrintWidth / 3, TextLineHeight, 0, Brushes.Black, False)
      End If
      x += PrintWidth / 3
    Next
  End Sub

  Private Function PrintAbsence(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, Optional ByVal Interlinia As Single = 1) As Boolean
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim DV As DataView = New DataView(Doc.DS.Tables("Absencja"), "IdUczen=" & IdUczen, "Data ASC", DataViewRowState.CurrentRows)
    Dim Miesiac As DataTable = DV.ToTable(True, "Miesiac")
    'Dim FoundScore() As DataRow
    Dim Kolumna As New List(Of Pole) From
    {
      New Pole With {.Name = "Data", .Size = CInt(PrintWidth / 7 * 2)},
      New Pole With {.Name = "Lekcja", .Size = CInt(PrintWidth / 7 * 4)},
      New Pole With {.Name = "Status", .Size = CInt(PrintWidth / 7)}
    }
    Do Until ((y + TextLineHeight * CSng(3.5)) > e.MarginBounds.Bottom) Or (Doc.Offset(1) > Miesiac.Rows.Count - 1)
      If Doc.Offset(2) = 0 Then
        Doc.DrawText(e, MonthName(CType(Miesiac.Rows(Doc.Offset(1)).Item("Miesiac"), Integer)), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
        y += TextLineHeight * Interlinia  'CSng(1.5)
        PrintTableHeader(e, x, y, Kolumna)
      End If

      Dim Absence As DataRow() = DV.ToTable(False, "Data", "Lekcja", "Status", "Miesiac").Select("Miesiac=" & Miesiac.Rows(Doc.Offset(1)).Item("Miesiac").ToString)
      Do Until ((y + TextLineHeight) > e.MarginBounds.Bottom) Or (Doc.Offset(2) > Absence.GetLength(0) - 1)
        Dim ColSize As Integer = 0
        Doc.DrawText(e, String.Concat(CType(Absence(Doc.Offset(2)).Item("Data"), Date).ToShortDateString, " ", Chr(150), " ", CType(Absence(Doc.Offset(2)).Item("Data"), Date).ToString("dddd")), TextFont, x + ColSize, y, Kolumna.Item(0).Size, TextLineHeight, 0, Brushes.Black)
        ColSize += Kolumna.Item(0).Size
        Doc.DrawText(e, Absence(Doc.Offset(2)).Item("Lekcja").ToString, TextFont, x + ColSize, y, Kolumna.Item(1).Size, TextLineHeight, 0, Brushes.Black)
        ColSize += Kolumna.Item(1).Size
        Doc.DrawText(e, Absence(Doc.Offset(2)).Item("Status").ToString.ToUpper, TextFont, x + ColSize, y, Kolumna.Item(2).Size, TextLineHeight, 1, Brushes.Black)
        y += TextLineHeight
        Doc.Offset(2) += 1
      Loop
      If Doc.Offset(2) < Absence.GetLength(0) Then
        e.HasMorePages = True
        Return False
      Else
        y += TextLineHeight
        Doc.Offset(2) = 0
        Doc.Offset(1) += 1
      End If
      'y += TextLineHeight
    Loop
    If Doc.Offset(1) < Miesiac.Rows.Count Then
      e.HasMorePages = True
      Return False
    Else
      '------------------------------------ Podsumowanie nieobecności -------------------------------------------
      If y + TextLineHeight * 2.2 < e.MarginBounds.Bottom Then
        PrintAbsenceAmount(e, x, y, PrintWidth, IdUczen, 1.2)
        y += TextLineHeight * 2
      Else
        Return False
      End If
      Doc.Offset(1) = 0
    End If
    Return True
    'Doc.Offset(1) = 0
  End Function

  'Private Overloads Sub PrintScores(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, ByVal Typ As String, Optional ByVal InterLinia As Single = 1)
  '  Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
  '  'Dim ObjectFieldWidth As Single = e.Graphics.MeasureString("Informatyka", TextFont).Width + 50
  '  'ObjectFieldWidth += 30
  '  'tutaj poprawka uwzględniająca offset oceny oraz duplicat nazwy przedmiotu zrobić dataview
  '  Dim DV As DataView = New DataView(Doc.DS.Tables("Przedmiot"))
  '  Dim Przedmiot As DataTable = DV.ToTable(True, "ID", "Nazwa")
  '  Dim FoundScore() As DataRow
  '  Do Until (Doc.Offset(1) > Przedmiot.Rows.Count - 1) 'Doc.DS.Tables(1).Rows.Count - 1)
  '    Doc.DrawText(e, Przedmiot.Rows(Doc.Offset(1)).Item(1).ToString, TextFont, x, y, ObjectFieldWidth + ScoreOffset, TextLineHeight, 0, Brushes.Black, False)
  '    'Doc.DrawText(e, Doc.DS.Tables(1).Rows(Doc.Offset(1)).Item(1).ToString, TextFont, x, y, ObjectFieldWidth, TextLineHeight, 0, Brushes.Black, False)
  '    FoundScore = Doc.DS.Tables(2).Select("IdUczen=" & IdUczen & " AND IdPrzedmiot=" & Przedmiot.Rows(Doc.Offset(1)).Item("ID").ToString + " AND Typ='" + Typ + "'")
  '    'FoundScore = Doc.DS.Tables(2).Select("IdUczen=" & IdUczen & " AND IdPrzedmiot=" & Doc.DS.Tables(1).Rows(Doc.Offset(1)).Item(0).ToString + " AND Typ='" + Typ + "'")
  '    If FoundScore.Length > 0 Then Doc.DrawText(e, FoundScore(0).Item(1).ToString, TextFont, x + ObjectFieldWidth + ScoreOffset, y, PrintWidth - ObjectFieldWidth - ScoreOffset, TextLineHeight, 0, CType(IIf(CInt(FoundScore(0).Item("Waga").ToString) < 2 AndAlso CInt(FoundScore(0).Item("Waga").ToString) > -1, Brushes.Red, Brushes.Black), Brush), False)

  '    y += TextLineHeight * InterLinia
  '    Doc.Offset(1) += 1
  '  Loop
  '  Doc.Offset(1) = 0
  'End Sub

  'Private Sub PrintTutor(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single)
  '  Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
  '  Doc.DrawText(e, "Wychowawca klasy", New Font(TextFont, FontStyle.Bold), x + PrintWidth / 2, y, PrintWidth / 2, TextLineHeight, 1, Brushes.Black, False)
  '  y += TextLineHeight
  '  If Doc.Podpis IsNot Nothing Then Doc.DrawImage(e, Image.FromStream(New System.IO.MemoryStream(Doc.Podpis)), x + PrintWidth / 2 + (PrintWidth / 2 - 168) / 2, y, 168, 39)
  '  'If Doc.Podpis IsNot Nothing Then e.Graphics.DrawImage(Image.FromStream(New System.IO.MemoryStream(Doc.Podpis)), x + PrintWidth / 2 + (PrintWidth / 2 - 168) / 2, y, 168, 39)
  'End Sub
  Private Sub PrintTableHeader(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal Kolumna As List(Of Pole))
    Dim TableHeaderFont As Font = New Font(TextFont, FontStyle.Bold)
    Dim TextLineHeight As Single = TableHeaderFont.GetHeight(e.Graphics) * CSng(1.5)

    Dim ColSize As Integer = 0
    For Each Col In Kolumna
      With Col
        Doc.DrawText(e, .Name, TableHeaderFont, x + ColSize, y, .Size, TextLineHeight, 1, Brushes.Black, True)
        ColSize += .Size
      End With
    Next
    y += TextLineHeight
  End Sub
  Private Sub PrintHeader(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal HeaderText As String, Optional Alignment As Byte = 0)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Doc.DrawText(e, HeaderText, HeaderFont, x, y, PrintWidth, HeaderLineHeight, Alignment, Brushes.Black, False)
  End Sub
  'Private Sub PrintHeader(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal StudentName As String)
  '  Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
  '  'y += HeaderLineHeight * 2 'CSng(1.5)
  '  Doc.DrawText(e, StudentName, HeaderFont, x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
  'End Sub

  'Private Sub DrawDivisionLines(ByVal e As PrintPageEventArgs)
  '  Dim DotPen As New Pen(Color.Black)
  '  DotPen.DashStyle = Drawing2D.DashStyle.Dot
  '  Dim x1, y1 As Single
  '  x1 = CSng(e.PageBounds.Width / 2) + My.Settings.XCaliber  ' - Doc.DefaultPageSettings.PrintableArea.Left
  '  y1 = CSng(e.PageBounds.Height / 2) + My.Settings.YCaliber ' - Doc.DefaultPageSettings.PrintableArea.Top
  '  If Not Doc.IsPreview Then
  '    x1 -= Doc.DefaultPageSettings.PrintableArea.Left
  '    y1 -= Doc.DefaultPageSettings.PrintableArea.Top
  '  End If
  '  e.Graphics.DrawLine(DotPen, x1, 0, x1, e.PageBounds.Height)
  '  e.Graphics.DrawLine(DotPen, e.PageBounds.X, y1, e.PageBounds.Width, y1)
  'End Sub


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
    'pg.MinMargins.Right = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Right)
    'pg.MinMargins.Bottom = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Bottom)
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