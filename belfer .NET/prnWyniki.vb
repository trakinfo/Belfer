Imports System.Drawing.Printing
Public Class prnWyniki
  Private Zoom As Double = 1
  Private WithEvents Doc As New PrintReport(Nothing)
  'Private WithEvents Doc As New PrintReport(New DataSet)
  Private DS As DataSet
  Private Wait As New dlgWait
  'Private WithEvents Doc1 As New PrintDocument
  'Private PrnVar As New PrintVariables
  Private TextFont As Font = My.Settings.TextFont 'PrnVar.BaseFont
  Private HeaderFont As Font = My.Settings.HeaderFont 'PrnVar.HeaderFont
  Private LeftMargin As Single = My.Settings.LeftMargin, TopMargin As Single = My.Settings.TopMargin, Landscape As Boolean = False
  Private IsPreview As Boolean
  Private ObjectFieldWidth As Single = 0, ScoreOffset As Single = 20, OutOfResultWidth As Boolean

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.WynikiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.WynikiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData()
    'Dim dlg As New dlgWait
    'dlg.lblInfo.Text = "Pobieranie danych ..."
    'Wait.Show()
    'Application.DoEvents()
    Dim DBA As New DataBaseAction, CH As New CalcHelper, WR As New WynikiRaportSQL
    DS = New DataSet()
    DS.Tables.Add(DBA.GetDataTable(WR.SelectStudent(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)))
    DS.Tables(0).TableName = "Student"

    DS.Tables.Add(DBA.GetDataTable(WR.SelectPrzedmiot(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear)))
    DS.Tables(1).TableName = "Przedmiot"
    Dim ScoreType As String = If(nudSemestr.Value = 1, "'C1','S'", "'C2','R'")
    DS.Tables.Add(DBA.GetDataTable(WR.SelectResult(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, ScoreType, My.Settings.SchoolYear)))
    DS.Tables(2).TableName = "Wynik"

    DS.Tables.Add(DBA.GetDataTable(WR.CountAbsence(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, CType(If(Me.nudSemestr.Value = 1, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year)), Date).ToString("yyyy-MM-dd"), CType(If(Me.nudSemestr.Value = 1, CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year).Subtract(TimeSpan.FromDays(1)), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date).ToString("yyyy-MM-dd"), My.Settings.SchoolYear)))
    DS.Tables(3).TableName = "Frekwencja"

    DS.Tables.Add(DBA.GetDataTable(WR.CountNotes(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, CType(If(Me.nudSemestr.Value = 1, CH.StartDateOfSchoolYear(My.Settings.SchoolYear), CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year)), Date).ToString("yyyy-MM-dd"), CType(If(Me.nudSemestr.Value = 1, CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year).Subtract(TimeSpan.FromDays(1)), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date).ToString("yyyy-MM-dd"), My.Settings.SchoolYear)))
    DS.Tables(4).TableName = "Uwaga"

    DS.Tables.Add(DBA.GetDataTable(WR.SelectAvg(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString, My.Settings.SchoolYear)))
    DS.Tables(5).TableName = "Avg"

    DS.Tables.Add(DBA.GetDataTable(WR.CountGradesByPupil(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString, My.Settings.SchoolYear)))
    DS.Tables(6).TableName = "LiczbaOcenByStudent"

    DS.Tables.Add(DBA.GetDataTable(WR.CountGradesByObjects(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString, My.Settings.SchoolYear)))
    DS.Tables(7).TableName = "LiczbaOcenByPrzedmiot"

    DS.Tables.Add(DBA.GetDataTable(WR.CountNDSTByPupil(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString, My.Settings.SchoolYear)))
    DS.Tables(8).TableName = "LiczbaNdstByStudent"

    DS.Tables.Add(DBA.GetDataTable(WR.CountNKLByPupil(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString, My.Settings.SchoolYear)))
    DS.Tables(9).TableName = "LiczbaNKLByStudent"
    'tutaj()
    'DS.Tables.Add(DBA.GetDataTable(WR.SelectConcatResult(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString)))
    'DS.Tables(10).TableName = "ConcatResult"
    'Return DS
    'dlg.Hide()
  End Sub
  Private Sub prnListaUczniow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig

    dtData.Value = Date.Today
    rbZoom100.Checked = True

    ApplyNewConfig()
    pvOceny.Document = Doc
    pvOceny.AutoZoom = True
    pvOceny.Zoom = Zoom
  End Sub
  Private Sub ApplyNewConfig()
    Dim CH As New CalcHelper
    Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    EnableOptions(False)
    GetSchoolPlace()
    'rbAllStudents_CheckedChanged(rbAllStudents, Nothing)
    FillKlasa()
    Me.Doc.PageNumber = 0
    'pvOceny.InvalidatePreview()
  End Sub
  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, SH As New SeekHelper, W As New WynikiRaportSQL
    FCB.AddComboBoxComplexItems(cbKlasa, W.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, "0"))
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
  Private Sub GetSchoolPlace()
    Try
      Dim DBA As New DataBaseAction, WR As New WynikiRaportSQL
      Dim RH(0) As String
      RH(0) = DBA.GetSingleValue(WR.SelectSchoolPlace)
      Doc.ReportHeader = RH
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub nudSemestr_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSemestr.ValueChanged
    If Me.Created AndAlso Me.cbKlasa.Text.Length > 0 Then
      Cursor = Cursors.WaitCursor
      Me.FetchData()
      rbEndingScores_CheckedChanged(IIf(rbEndingScores.Checked, rbEndingScores, rbPartialScores), e)
      'Me.Doc.PageNumber = 0
      'pvOceny.InvalidatePreview()
      Cursor = Cursors.Default
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
    Cursor = Cursors.WaitCursor
    FillStudent()
    GetTutorFaximile()
    Me.FetchData()
    EnableOptions(True)
    rbEndingScores_CheckedChanged(IIf(rbEndingScores.Checked, rbEndingScores, rbPartialScores), e)
    Cursor = Cursors.Default
  End Sub
  Private Sub EnableOptions(Value As Boolean)
    nudSemestr.Enabled = Value
    dtData.Enabled = Value
    Me.gbPrintRange.Enabled = Value
    Me.gbScoreType.Enabled = Value
    Me.gbZoom.Enabled = Value
    gbOpcje.Enabled = Value
  End Sub
  Private Sub CheckResultLength(Table As DataTable)
    Try
      Dim ColWidth, ResultFieldWidth As Single
      Dim G As Graphics = pvOceny.CreateGraphics
      G.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
      G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
      Landscape = False
      If rbAllStudents.Checked Then
        ColWidth = CSng(Doc.DefaultPageSettings.PaperSize.Width / 2) - 2 * LeftMargin
        'ResultFieldWidth = ColWidth - ObjectFieldWidth - ScoreOffset
      Else
        ColWidth = CSng(Doc.DefaultPageSettings.PaperSize.Width) - 2 * LeftMargin
      End If
      ResultFieldWidth = ColWidth - ObjectFieldWidth - ScoreOffset

      '---------------------------------------------
      If rbEndingScores.Checked Then
        'If rbAllStudents.Checked Then
        For Each R As DataRow In Table.Rows
          If G.MeasureString(R.Item("Nazwa").ToString, TextFont).Width > ResultFieldWidth Then
            Landscape = True
            Exit Sub
          End If
        Next
        'Else

        'End If
      Else
      If rbAllStudents.Checked Then
        For Each Student As DataRow In New DataView(Table).ToTable(True, "IdUczen").Rows
          For Each Przedmiot As DataRow In New DataView(Table).ToTable(True, "IdPrzedmiot").Rows
            Dim Marks As String = ""
            For Each Ocena As DataRow In Table.Select("IdUczen=" & Student.Item("IdUczen").ToString & " AND IdPrzedmiot=" & Przedmiot.Item("IdPrzedmiot").ToString)
              If CType(Ocena.Item("Poprawa"), Boolean) Then
                Marks = Marks.TrimEnd(", ".ToCharArray)
                Marks += "{" + Ocena.Item("Ocena").ToString + "}, "
              Else
                Marks += Ocena.Item("Ocena").ToString + ", "
              End If
            Next
            If G.MeasureString(Marks.TrimEnd(", ".ToCharArray), TextFont).Width > ResultFieldWidth Then
              Landscape = True
              Exit Sub
            End If
          Next
        Next
      Else
        'ColWidth = CSng(Doc.DefaultPageSettings.PaperSize.Width) - 2 * LeftMargin
        'ResultFieldWidth = ColWidth - ObjectFieldWidth - ScoreOffset

        For Each Przedmiot As DataRow In New DataView(Table).ToTable(True, "IdPrzedmiot").Rows
          Dim Marks As String = ""
          For Each Ocena As DataRow In Table.Select("IdPrzedmiot=" & Przedmiot.Item("IdPrzedmiot").ToString)
            If CType(Ocena.Item("Poprawa"), Boolean) Then
              Marks = Marks.TrimEnd(", ".ToCharArray)
              Marks += "{" + Ocena.Item("Ocena").ToString + "}, "
            Else
              Marks += Ocena.Item("Ocena").ToString + ", "
            End If
          Next
          If G.MeasureString(Marks.TrimEnd(", ".ToCharArray), TextFont).Width > ResultFieldWidth Then
            'MessageBox.Show(G.MeasureString(Marks.TrimEnd(", ".ToCharArray), TextFont).Width.ToString)
            Landscape = True
            Exit Sub
          End If
        Next
      End If
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub GetTutorFaximile()
    Dim DBA As New DataBaseAction, WR As New WynikiRaportSQL
    Dim Reader As MySqlDataReader = DBA.GetReader(WR.SelectFaximile(CType(Me.cbKlasa.SelectedItem, CbItem).ID.ToString))
    Try
      If Reader.HasRows Then
        Reader.Read()
        If Not Reader.IsDBNull(0) Then Doc.Podpis = CType(Reader.Item(0), Byte())
        'Else
        'Doc.Podpis = Nothing
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      Reader.Close()
    End Try
  End Sub
  Private Sub cbUczen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUczen.SelectedIndexChanged
    'Dim DT As DataTable
    'If Me.rbEndingScores.Checked Then
    '  DT = New DataView(DS.Tables("Wynik"), "Typ='" & If(nudSemestr.Value = 1, "S", "R") & "' AND IdUczen=" & CType(cbUczen.SelectedItem, CbItem).ID, "IdPrzedmiot ASC", DataViewRowState.CurrentRows).ToTable(True, "Nazwa")
    'Else
    '  DT = New DataView(DS.Tables("Wynik"), "Typ='" & If(nudSemestr.Value = 1, "C1", "C2") & "' AND IdUczen=" & CType(cbUczen.SelectedItem, CbItem).ID, "IdPrzedmiot ASC", DataViewRowState.CurrentRows).ToTable(False, "Ocena", "IdPrzedmiot", "Poprawa")
    'End If
    'CheckResultLength(DT)
    'Me.Doc.DefaultPageSettings.Landscape = Landscape
    Me.Doc.PageNumber = 0
    'pvOceny.InvalidatePreview()
    rbEndingScores_CheckedChanged(IIf(rbEndingScores.Checked, rbEndingScores, rbPartialScores), e)
  End Sub

  Private Sub rbZoom100_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbZoom100.CheckedChanged, rbZoom200.CheckedChanged, rbZoom50.CheckedChanged
    If Not Me.Created Then Exit Sub
    Me.Zoom = CType(CType(sender, RadioButton).Tag, Double)
    Me.pvOceny.Zoom = Me.Zoom
    'Me.Doc.PageNumber = 0
    'Me.Doc.Offset(0) = 0
    'Me.Doc.Offset(1) = 0
    'Me.pvOceny.InvalidatePreview()
  End Sub
  Private Sub rbZoomCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbZoomCustom.CheckedChanged
    If rbZoomCustom.Checked Then
      tbZoom.Enabled = True
      nudZoom.Enabled = True
      Me.Zoom = tbZoom.Value * 0.01
      Me.pvOceny.Zoom = Me.Zoom
    Else
      tbZoom.Enabled = False
      nudZoom.Enabled = False
    End If
  End Sub

  Private Sub tbZoom_Scroll(sender As Object, e As EventArgs) Handles tbZoom.Scroll
    Me.Zoom = tbZoom.Value * 0.01
    Me.pvOceny.Zoom = Me.Zoom
    nudZoom.Value = tbZoom.Value
  End Sub
  Private Sub nudZoom_ValueChanged(sender As Object, e As EventArgs) Handles nudZoom.ValueChanged
    If Not Me.Created Then Exit Sub
    tbZoom.Value = CType(nudZoom.Value, Integer)
    Me.pvOceny.Zoom = nudZoom.Value * 0.01
  End Sub
  Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    Dim prnDlg As New PrintDialog
    'Preview = False
    prnDlg.AllowSomePages = False
    prnDlg.AllowCurrentPage = False
    prnDlg.AllowPrintToFile = False
    prnDlg.PrinterSettings.FromPage = 1
    prnDlg.PrinterSettings.ToPage = 1
    'pvOceny.Document.OriginAtMargins = False
    prnDlg.PrinterSettings.DefaultPageSettings.Landscape = Doc.DefaultPageSettings.Landscape
    If prnDlg.ShowDialog = Windows.Forms.DialogResult.OK Then
      'Doc.IsPreview = False
      Me.Doc.PageNumber = 0
      pvOceny.Document.PrinterSettings = prnDlg.PrinterSettings
      Me.pvOceny.Document.PrinterSettings.Copies = prnDlg.PrinterSettings.Copies
      Me.pvOceny.Document.Print()

      'Doc.IsPreview = True
    End If
    'Preview = True
  End Sub

  Private Sub rbEndingScores_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbEndingScores.CheckedChanged, rbPartialScores.CheckedChanged
    If Not Me.Created Then Exit Sub

    If Me.rbEndingScores.Checked Then
      SetObjectFieldWidth(New DataView(DS.Tables("Przedmiot")).ToTable(True, "Alias"))
      'ScoreType =
    Else
      SetObjectFieldWidth(New DataView(DS.Tables("Przedmiot"), "Typ<>'z' AND Typ<>'F'", "Priorytet ASC", DataViewRowState.CurrentRows).ToTable(True, "Alias"))
      'ScoreType = If(nudSemestr.Value = 1, "C1", "C2")
      'CheckResultLength("ConcatResult")
    End If
    rbAllStudents_CheckedChanged(sender, e)
  End Sub
  Private Sub SetObjectFieldWidth(ObjectTable As DataTable)
    Dim G As Graphics = pvOceny.CreateGraphics
    ObjectFieldWidth = 0
    For Each R As DataRow In ObjectTable.Rows  'DS.Tables("Przedmiot").Select("Typ<>'z' AND Typ<>'F'")
      If G.MeasureString(R.Item(0).ToString, TextFont).Width > ObjectFieldWidth Then ObjectFieldWidth = G.MeasureString(R.Item(0).ToString, TextFont).Width
    Next
  End Sub
  Private Sub chkTableSet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTableSet.CheckedChanged
    If Not Me.Created Then Exit Sub
    Me.Doc.DefaultPageSettings.Landscape = CType(Me.chkTableSet.Checked, Boolean)
    Doc.PageNumber = 0
    Me.pvOceny.InvalidatePreview()
  End Sub

  Private Sub rbAllStudents_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAllStudents.CheckedChanged, rbSelectedStudent.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    Try
      Dim DT As DataTable
      If Me.rbAllStudents.Checked Then
        Me.chkTableSet.Enabled = CType(Me.rbEndingScores.Checked, Boolean)
        Me.cbUczen.Enabled = False
        If Me.rbEndingScores.Checked Then
          If chkTableSet.Checked Then
            Me.Doc.DefaultPageSettings.Landscape = True 'CType(Me.chkTableSet.Checked, Boolean)
          Else
            ScoreOffset = 20
            DT = New DataView(DS.Tables("Wynik"), "Typ='" & If(nudSemestr.Value = 1, "S", "R") & "'", "IdPrzedmiot ASC", DataViewRowState.CurrentRows).ToTable(True, "Nazwa")
            CheckResultLength(DT)
            Me.Doc.DefaultPageSettings.Landscape = Landscape
          End If
        Else
          DT = New DataView(DS.Tables("Wynik"), "Typ='" & If(nudSemestr.Value = 1, "C1", "C2") & "'", "IdPrzedmiot ASC", DataViewRowState.CurrentRows).ToTable(False, "Ocena", "IdPrzedmiot", "IdUczen", "Poprawa")
          CheckResultLength(DT)
          Me.Doc.DefaultPageSettings.Landscape = Landscape
        End If
      Else
        ScoreOffset = 50
        Me.chkTableSet.Enabled = False
        Me.cbUczen.Enabled = True
        If cbUczen.SelectedItem Is Nothing Then
          Me.Doc.DefaultPageSettings.Landscape = False
          Me.Doc.PageNumber = 0
          Me.pvOceny.InvalidatePreview()
          Exit Sub
        End If
        If Me.rbEndingScores.Checked Then
          DT = New DataView(DS.Tables("Wynik"), "Typ='" & If(nudSemestr.Value = 1, "S", "R") & "' AND IdUczen=" & CType(cbUczen.SelectedItem, CbItem).ID, "IdPrzedmiot ASC", DataViewRowState.CurrentRows).ToTable(True, "Nazwa")
        Else
          DT = New DataView(DS.Tables("Wynik"), "Typ='" & If(nudSemestr.Value = 1, "C1", "C2") & "' AND IdUczen=" & CType(cbUczen.SelectedItem, CbItem).ID, "IdPrzedmiot ASC", DataViewRowState.CurrentRows).ToTable(False, "Ocena", "IdPrzedmiot", "Poprawa")
        End If
        CheckResultLength(DT)
        Me.Doc.DefaultPageSettings.Landscape = Landscape
      End If

      Me.Doc.PageNumber = 0
      Me.pvOceny.InvalidatePreview()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub PrnDoc_Begin(ByVal sender As Object, ByVal e As PrintEventArgs) Handles Doc.BeginPrint
    If e.PrintAction = PrintAction.PrintToPrinter Then
      IsPreview = False
    Else
      IsPreview = True
    End If
    Wait.Show()
    Application.DoEvents()
  End Sub
  Private Sub PrnDoc_End(ByVal sender As Object, ByVal e As PrintEventArgs) Handles Doc.EndPrint
    If OutOfResultWidth Then MessageBox.Show("Liczba ocen z co najmniej jednego przedmiotu jest zbyt duża," & Chr(13) & "żeby się zmieścić w wyznaczonym obszarze." & Chr(13) & "Zmniejsz wielkość czcionki lub mariginesy i spróbuj jeszcze raz.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    OutOfResultWidth = False
    Wait.Hide()
  End Sub

  Private Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles Doc.PrintPage
    If Me.cbKlasa.SelectedItem Is Nothing Then Exit Sub
    e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
    Doc.PageNumber += 1
    Me.pvOceny.Rows = Doc.PageNumber
    'If e.PageSettings.PrinterSettings.PrintRange = PrintRange.SomePages Then
    '  'MessageBox.Show(e.PageSettings.PrinterSettings.FromPage.ToString & vbNewLine & e.PageSettings.PrinterSettings.ToPage.ToString)
    '  Exit Sub
    'End If
    If Me.rbAllStudents.Checked Then
      If Me.rbPartialScores.Checked Then
        If Landscape Then
          PrintPartialScoresForAllStudents_x2(e)
        Else
          Me.PrintPartialScoresForAllStudents(e)
        End If
      Else
        If Me.chkTableSet.Checked Then
          PrintTableSet(e)
        Else
          PrintEndingScoresForAllStudents(e)
        End If
      End If
    Else
      If cbUczen.SelectedItem Is Nothing Then Exit Sub
      If Me.rbPartialScores.Checked Then
        PrintPartialScoresForSelectedStudent(e)
      Else
        PrintEndingScoresForSelectedStudent(e)
      End If
    End If
  End Sub

  Private Sub PrintPartialScoresForAllStudents(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth, ColHeight As Single
    ColWidth = CSng(e.PageBounds.Width / 2) - 2 * LeftMargin
    ColHeight = CSng(e.PageBounds.Height / 2) - 2 * TopMargin

    DrawDivisionLines(e)
    Dim x, y As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin
    '--------------------------------------- Nagłówek --------------------------------------
    Dim i As Integer = 1
    Do While (i < 5) And (Doc.Offset(0) < DS.Tables(0).Rows.Count)
      PrintHeader(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(2).ToString)
      y += HeaderLineHeight * 2

      '--------------------------------------- Wykaz ocen --------------------------------------
      PrintPartialScores(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString, IIf(Me.nudSemestr.Value = 1, "C1", "C2").ToString)
      y += CSng(TextLineHeight * 0.5)

      '------------------------------------------ linia oddzielająca ----------------------------------
      'e.Graphics.DrawLine(Pens.Black, x, y, x + 200, y)
      If chkAbsence.Checked OrElse chkNote.Checked Then
        Doc.DrawLine(e, x, y, x + 200, y)
        y += TextLineHeight
      End If


      '------------------------------------ Wykaz nieobecności -------------------------------------------
      If chkAbsence.Checked Then
        PrintAbsence(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString)
        y += TextLineHeight * 2

      End If

      '------------------------------------ Wykaz uwag -------------------------------------------
      If chkNote.Checked Then
        Me.PrintNotes(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString)
        y += TextLineHeight * 2
      End If

      '----------------------------------------- Wychowawca -----------------------------------------
      If Not chkAbsence.Checked And Not chkNote.Checked Then y += TextLineHeight * 2
      Me.PrintTutor(e, x, y, ColWidth)

      Doc.Offset(0) += 1
      y += TextLineHeight
      i += 1
      y = CSng(IIf(i > 2, TopMargin + ColHeight + TopMargin * 2, TopMargin))
      x = CSng(IIf(x = LeftMargin, x + ColWidth + LeftMargin * 2, LeftMargin))
      'y = CSng(IIf(i > 2, y0 + ColHeight + y0 * 2, y0))
      'x = CSng(IIf(x = x0, x + ColWidth + x0 * 2, x0))
    Loop
    If Doc.Offset(0) < DS.Tables(0).Rows.Count Then
      i = 1
      e.HasMorePages = True
    Else
      Doc.Offset(0) = 0
    End If
  End Sub
  Private Sub PrintPartialScoresForAllStudents_x2(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth As Single
    ColWidth = CSng(e.PageBounds.Width / 2) - 2 * LeftMargin
    'ColHeight = CSng(e.PageBounds.Height / 2) - 2 * TopMargin

    DrawDivisionLine(e)
    Dim x, y As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin
    '--------------------------------------- Nagłówek --------------------------------------
    Dim i As Integer = 1
    Do While (i < 3) And (Doc.Offset(0) < DS.Tables(0).Rows.Count)
      PrintHeader(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(2).ToString)
      y += HeaderLineHeight * 2

      '--------------------------------------- Wykaz ocen --------------------------------------
      PrintPartialScores(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString, IIf(Me.nudSemestr.Value = 1, "C1", "C2").ToString)
      y += CSng(TextLineHeight * 0.5)

      '------------------------------------------ linia oddzielająca ----------------------------------
      'e.Graphics.DrawLine(Pens.Black, x, y, x + 200, y)
      If chkAbsence.Checked OrElse chkNote.Checked Then
        Doc.DrawLine(e, x, y, x + 200, y)
        y += TextLineHeight
      End If


      '------------------------------------ Wykaz nieobecności -------------------------------------------
      If chkAbsence.Checked Then
        PrintAbsence(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString)
        y += TextLineHeight * 2

      End If

      '------------------------------------ Wykaz uwag -------------------------------------------
      If chkNote.Checked Then
        Me.PrintNotes(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString)
        y += TextLineHeight * 2
      End If

      '----------------------------------------- Wychowawca -----------------------------------------
      If Not chkAbsence.Checked And Not chkNote.Checked Then y += TextLineHeight * 2
      Me.PrintTutor(e, x, y, ColWidth)

      Doc.Offset(0) += 1
      y += TextLineHeight
      i += 1
      y = TopMargin 'CSng(IIf(i > 2, TopMargin + ColHeight + TopMargin * 2, TopMargin))
      x = CSng(IIf(x = LeftMargin, x + ColWidth + LeftMargin * 2, LeftMargin))
      'y = CSng(IIf(i > 2, y0 + ColHeight + y0 * 2, y0))
      'x = CSng(IIf(x = x0, x + ColWidth + x0 * 2, x0))
    Loop
    If Doc.Offset(0) < DS.Tables(0).Rows.Count Then
      i = 1
      e.HasMorePages = True
    Else
      Doc.Offset(0) = 0
    End If
  End Sub
  Private Sub PrintPartialScoresForSelectedStudent(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Doc.DrawHeader(e, LeftMargin, e.MarginBounds.Top, CSng(e.PageBounds.Width) - 2 * LeftMargin)
    Doc.DrawFooter(e, LeftMargin, e.MarginBounds.Bottom, CSng(e.PageBounds.Width) - 2 * LeftMargin)
    Dim x, y, ColWidth As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin

    ColWidth = CSng(e.PageBounds.Width) - 2 * LeftMargin
    '--------------------------------------- Nagłówek --------------------------------------
    y += HeaderLineHeight * 2
    PrintHeader(e, x, y, ColWidth, Me.cbUczen.Text)
    'PrintHeader(e, x, y, CSng(e.MarginBounds.Width), Me.cbUczen.Text)
    'PrintHeader(e, x, y, CSng(e.PageBounds.Width) - 2 * x0, Me.cbUczen.Text)
    y += HeaderLineHeight * 2

    '--------------------------------------- Wykaz ocen --------------------------------------
    PrintPartialScores(e, x, y, ColWidth, CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "C1", "C2").ToString, CSng(1.0))
    y += CSng(TextLineHeight * 0.5)

    '------------------------------------------ Linia oddzielająca -------------------------------------
    'e.Graphics.DrawLine(Pens.Black, x, y, x + 300, y)
    If chkAbsence.Checked OrElse chkNote.Checked Then
      Doc.DrawLine(e, x, y, x + 300, y)
      y += TextLineHeight * 2

    End If

    '------------------------------------ Wykaz nieobecności -------------------------------------------
    If chkAbsence.Checked Then
      PrintAbsence(e, x, y, ColWidth, CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString, CSng(1.0))
      y += TextLineHeight * 2

    End If

    '------------------------------------- Wykaz uwag -------------------------------------------------
    If chkNote.Checked Then
      PrintNotes(e, x, y, ColWidth, CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString, CSng(1.0))
      y += TextLineHeight * 3

    End If

    '------------------------------------- Wychowawca ------------------------------------------------
    If Not chkAbsence.Checked And Not chkNote.Checked Then y += TextLineHeight * 2
    PrintTutor(e, x, y, ColWidth)
  End Sub

  Private Sub PrintEndingScoresForAllStudents(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim ColWidth, ColHeight As Single
    ColWidth = CSng(e.PageBounds.Width / 2) - 2 * LeftMargin
    ColHeight = CSng(e.PageBounds.Height / 2) - 2 * TopMargin

    DrawDivisionLines(e)

    Dim x, y As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin

    Dim i As Integer = 1
    Do While (i < 5) And (Doc.Offset(0) < DS.Tables(0).Rows.Count)
      '--------------------------------------- Nagłówek --------------------------------------
      PrintHeader(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(2).ToString)
      y += HeaderLineHeight * 2

      '--------------------------------------- Wykaz ocen --------------------------------------
      PrintScores(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString)
      y += CSng(TextLineHeight * 0.5)

      '----------------------------------------- linia oddzielająca ----------------------------------
      'e.Graphics.DrawLine(Pens.Black, x, y, x + 200, y)
      If chkAbsence.Checked OrElse chkNote.Checked Then
        Doc.DrawLine(e, x, y, x + 200, y)
        y += TextLineHeight

      End If

      '------------------------------------ Wykaz nieobecności -------------------------------------------
      If chkAbsence.Checked Then
        PrintAbsence(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString)
        y += TextLineHeight * 2

      End If

      '--------------------------------- Wykaz uwag --------------------------------------------------
      If chkNote.Checked Then
        PrintNotes(e, x, y, ColWidth, DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString)
        y += TextLineHeight * 2

      End If

      '------------------------------------- Wychowawca ---------------------------------------
      If Not chkAbsence.Checked And Not chkNote.Checked Then y += TextLineHeight * 2
      PrintTutor(e, x, y, ColWidth)

      'Doc.Offset(1) = 0
      Doc.Offset(0) += 1
      'y += TextLineHeight
      i += 1
      y = CSng(IIf(i > 2, ColHeight + TopMargin * 3, TopMargin))
      x = CSng(IIf(x = LeftMargin, ColWidth + LeftMargin * 3, LeftMargin))
      'y = CSng(IIf(i > 2, y0 + ColHeight + TopMargin * 2, y0))
      'x = CSng(IIf(x = x0, x + ColWidth + LeftMargin * 2, x0))
    Loop
    If Doc.Offset(0) < DS.Tables(0).Rows.Count Then
      i = 1
      e.HasMorePages = True
    Else
      Doc.Offset(0) = 0
    End If
  End Sub
  'przejrzeć wszystko pod kątem x0 i y0 na leftmargin i topmargin
  Private Sub PrintEndingScoresForSelectedStudent(ByVal e As PrintPageEventArgs)
    Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Doc.DrawHeader(e, LeftMargin, e.MarginBounds.Top, CSng(e.PageBounds.Width) - 2 * LeftMargin)
    Doc.DrawFooter(e, LeftMargin, e.MarginBounds.Bottom, CSng(e.PageBounds.Width) - 2 * LeftMargin)
    Dim x, y, ColWidth As Single
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin

    ColWidth = CSng(e.PageBounds.Width) - 2 * LeftMargin
    '--------------------------------------- Nagłówek --------------------------------------
    y += HeaderLineHeight * 2
    PrintHeader(e, x, y, ColWidth, Me.cbUczen.Text)
    y += HeaderLineHeight * 2

    '--------------------------------------- Wykaz ocen --------------------------------------
    PrintScores(e, x, y, ColWidth, CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString, CSng(1.0))
    'PrintScores(e, x, y, ColWidth, CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString, IIf(Me.nudSemestr.Value = 1, "S", "R").ToString, CSng(1.5))
    y += CSng(TextLineHeight * 0.5)


    '---------------------------------------- Linia ---------------------------------------

    'e.Graphics.DrawLine(Pens.Black, x, y, x + 300, y)
    If chkAbsence.Checked OrElse chkNote.Checked Then
      Doc.DrawLine(e, x, y, x + 300, y)
      y += TextLineHeight

    End If

    '------------------------------------ Wykaz nieobecności -------------------------------------------
    If chkAbsence.Checked Then
      PrintAbsence(e, x, y, ColWidth, CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString, CSng(1.0))
      y += TextLineHeight * 2

    End If

    '--------------------------------- Wykaz uwag --------------------------------------------------
    If chkNote.Checked Then
      PrintNotes(e, x, y, ColWidth, CType(Me.cbUczen.SelectedItem, CbItem).ID.ToString, CSng(1.0))
      y += TextLineHeight * 3

    End If

    '------------------------------------- Wychowawca ---------------------------------------
    If Not chkAbsence.Checked And Not chkNote.Checked Then y += TextLineHeight * 2

    PrintTutor(e, x, y, ColWidth)

  End Sub

  Private Sub PrintTableSet(ByVal e As PrintPageEventArgs)
    'Dim Doc As PrintReport = CType(Sender, PrintReport)
    'Dim PrnVar As New PrintVariables
    Dim x, x1, y, y1, VerticalLinesOrigin(3) As Single
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    'x = x0 : y = y0
    x = LeftMargin : y = TopMargin

    Dim BoldPen As New Pen(Color.Black, 2)

    '------------------------------------------ Nagłówek raportu ------------------------------------------
    Doc.DrawText(e, "Tabelaryczne zestawienie ocen", New Font(TextFont, FontStyle.Bold), x, y, e.MarginBounds.Right, HeaderLineHeight, 1, Brushes.Black, False)
    y += HeaderLineHeight
    Doc.DrawText(e, "Klasa: " & Me.cbKlasa.Text, New Font(TextFont, FontStyle.Bold), x, y, e.MarginBounds.Right, HeaderLineHeight, 0, Brushes.Black, False)
    Dim CH As New CalcHelper
    Doc.DrawText(e, "Rok szkolny: " & CH.CurrentSchoolYear(), New Font(TextFont, FontStyle.Bold), x, y, e.MarginBounds.Right, HeaderLineHeight, 1, Brushes.Black, False)
    Doc.DrawText(e, IIf(Me.nudSemestr.Value = 1, "Semestr 1", "Rok szkolny").ToString, New Font(TextFont, FontStyle.Bold), x, y, e.MarginBounds.Right, HeaderLineHeight, 2, Brushes.Black, False)
    '----------------------------------------Koniec nagłówka raportu -----------------------------------
    y += HeaderLineHeight * CSng(1.5)
    Dim i As Integer = 0, CellWidth As Single = 32
    x1 = 200 + CellWidth : y1 = y
    VerticalLinesOrigin(0) = x1
    'Doc.DrawRotatedText(e, "", TextFont, x, y, CellWidth, 34, 0, Brushes.DarkBlue, 3)

    '--------------------------------------------Nagłówek tabeli ------------------------------------
    'e.Graphics.DrawRectangle(Pens.Black, x + CellWidth, y, x1 - LeftMargin, 85)
    'Doc.DrawRectangle(e, x + CellWidth, y, x1, 85)
    'e.Graphics.DrawLine(Pens.Black, x + CellWidth, y, x1, y + 85)
    Doc.DrawLine(e, x + CellWidth, y, x1, y)
    Doc.DrawLine(e, x + CellWidth, y, x1, y + 85)
    'Doc.DrawText(e, "Nr w dzien.", New Font(TextFont, FontStyle.Bold), x + CellWidth / 2, y + 85, CellWidth, 85, 0, Brushes.Black, 270, True)
    Doc.DrawText(e, "Nr w dzien.", New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth, 85, 0, Brushes.Black, 270, True)

    Doc.DrawText(e, "Przedmiot", New Font(TextFont, FontStyle.Bold), 130, y + 20, 100, HeaderLineHeight, 1, Brushes.Black, False)
    Doc.DrawText(e, "Nazwisko i imię", New Font(TextFont, FontStyle.Bold), x + CellWidth, y + CSng(85 / 2), 150 - LeftMargin, 85 / 2, 1, Brushes.Black, False)
    x = x1

    Dim DV As DataView = New DataView(DS.Tables("Przedmiot"))
    Dim Przedmiot As DataTable = DV.ToTable(True, "ID", "Alias", "Kategoria")

    Do Until i > Przedmiot.Rows.Count - 1
      If chkColor.Checked Then
        Doc.DrawText(e, Przedmiot.Rows(i).Item("Alias").ToString, New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth, 85, 0, CType(IIf(Przedmiot.Rows(i).Item("Kategoria").ToString = "o", Brushes.Black, Brushes.Green), Brush), 270, True)
      Else
        Doc.DrawText(e, Przedmiot.Rows(i).Item("Alias").ToString, New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth, 85, 0, Brushes.Black, 270, True)
      End If
      x += CellWidth
      i += 1
    Loop
    If chkColor.Checked Then
      Doc.DrawText(e, "Średnia ocen", New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth + 20, 85, 0, Brushes.DarkBlue, 270, True)
    Else
      Doc.DrawText(e, "Średnia ocen", New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth + 20, 85, 0, Brushes.Black, 270, True)
    End If
    'Doc.DrawText(e, "Średnia ocen", New Font(TextFont, FontStyle.Bold), x, y, CellWidth + 20, 85, 1, Brushes.DarkBlue, True, True)
    x += CellWidth + 20
    For i = 6 To 0 Step -1
      Doc.DrawText(e, i.ToString, New Font(TextFont, FontStyle.Bold), x, y, CellWidth, 85, 1, Brushes.Black, True)
      x += CellWidth
    Next
    If chkAbsence.Checked Then
      CellWidth += 10
      'Doc.DrawText(e, "uspr.", New Font(TextFont, FontStyle.Bold), x, y, CellWidth, 85, 1, Brushes.Black, True, True)
      Doc.DrawText(e, "Uspraw.", New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth, 85, 0, Brushes.Black, 270, True)

      x += CellWidth
      'Doc.DrawText(e, "nieuspr.", New Font(TextFont, FontStyle.Bold), x, y, CellWidth, 85, 1, Brushes.Black, True, True)
      Doc.DrawText(e, "Nieuspraw.", New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth, 85, 0, Brushes.Black, 270, True)
      x += CellWidth
      Doc.DrawText(e, "Spóźnień", New Font(TextFont, FontStyle.Bold), x, y + 85, CellWidth, 85, 0, Brushes.Black, 270, True)
      CellWidth -= 10
    End If

    '------------------------------------------Koniec nagłówka tabeli --------------------------------
    y += 85
    '--------------------------------------- Początek części głównej --------------------------------------
    Dim FoundRow() As DataRow
    Dim TotalAvg As Single, k As Integer, TotalMarkNumber(6) As Integer, TotalAbsence(3) As Integer
    Do While (Doc.Offset(0) < DS.Tables(0).Rows.Count)
      'x = x0
      x = LeftMargin
      Doc.DrawText(e, DS.Tables(0).Rows(Doc.Offset(0)).Item(1).ToString, New Font(TextFont, FontStyle.Bold), x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
      x += CellWidth
      Doc.DrawText(e, DS.Tables(0).Rows(Doc.Offset(0)).Item(2).ToString, New Font(TextFont, FontStyle.Bold), x, y, x1 - LeftMargin, LineHeight, 0, Brushes.Black, True)
      x = x1
      Do Until (Doc.Offset(1) > Przedmiot.Rows.Count - 1)
        FoundRow = DS.Tables(2).Select("IdUczen=" & DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString & " AND IdPrzedmiot=" & Przedmiot.Rows(Doc.Offset(1)).Item("ID").ToString + " AND Typ='" + IIf(Me.nudSemestr.Value = 1, "S", "R").ToString + "'")
        If FoundRow.Length > 0 Then
          If chkColor.Checked Then
            Doc.DrawText(e, FoundRow(0).Item(0).ToString, TextFont, x, y, CellWidth, LineHeight, 1, CType(IIf(CInt(FoundRow(0).Item(2)) > 1, CType(IIf(CInt(FoundRow(0).Item(2)) > 5, Brushes.Blue, IIf(Przedmiot.Rows(Doc.Offset(1)).Item("Kategoria").ToString = "o", Brushes.Black, Brushes.Green)), Brush), Brushes.Red), Brush), True)
          Else
            Doc.DrawText(e, FoundRow(0).Item(0).ToString, TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
          End If
        Else
          Doc.DrawText(e, "-", TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
        End If

        x += CellWidth
        Doc.Offset(1) += 1
      Loop
      VerticalLinesOrigin(1) = x
      '----------------------------------- Średnia ocen ucznia ----------------------------------------
      FoundRow = DS.Tables(5).Select("IdUczen=" & DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString)
      If FoundRow.Length > 0 Then
        If chkColor.Checked Then
          Doc.DrawText(e, Math.Round(CType(FoundRow(0).Item(0), Double), 2).ToString("0.00"), New Font(TextFont, FontStyle.Bold), x, y, CellWidth + 20, LineHeight, 1, Brushes.DarkBlue, True)
        Else
          Doc.DrawText(e, Math.Round(CType(FoundRow(0).Item(0), Double), 2).ToString("0.00"), New Font(TextFont, FontStyle.Bold), x, y, CellWidth + 20, LineHeight, 1, Brushes.Black, True)
        End If

        If CType(FoundRow(0).Item(0), Single) > 0 Then
          TotalAvg += CType(FoundRow(0).Item(0), Single)
          k += 1
        End If
      Else
        Doc.DrawText(e, "-", TextFont, x, y, CellWidth + 20, LineHeight, 1, Brushes.Blue, True)
      End If
      x += CellWidth + 20
      VerticalLinesOrigin(2) = x
      '----------------------------- Liczba ocena by uczen ----------------------------------------
      For i = 6 To 0 Step -1
        FoundRow = DS.Tables(6).Select("IdUczen=" & DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString & " AND Waga=" & i)
        If FoundRow.Length > 0 Then
          Doc.DrawText(e, FoundRow(0).Item(2).ToString, TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
          TotalMarkNumber(i) += CType(FoundRow(0).Item(2), Integer)
        Else
          Doc.DrawText(e, "-", TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
        End If
        x += CellWidth
      Next
      VerticalLinesOrigin(3) = x
      '------------------------------ Frekwencja ucznia --------------------------------------
      If chkAbsence.Checked Then
        Dim Typ() As String = {"u", "n", "s"}
        CellWidth += 10
        For i = 0 To 2
          FoundRow = DS.Tables(3).Select("IdUczen=" & DS.Tables(0).Rows(Doc.Offset(0)).Item(0).ToString & "AND Typ='" & Typ(i) & "'")
          If FoundRow.Length > 0 Then
            Doc.DrawText(e, FoundRow(0).Item(0).ToString, TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
            TotalAbsence(i) += CType(FoundRow(0).Item(0), Integer)
          Else
            Doc.DrawText(e, "-", TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
          End If
          x += CellWidth
        Next
        CellWidth -= 10
      End If

      If Doc.Offset(0) Mod 10 = 0 Then
        'e.Graphics.DrawLine(BoldPen, LeftMargin, y, x, y)
        Doc.DrawLine(e, LeftMargin, y, x, y)
      End If
      Doc.Offset(1) = 0
      Doc.Offset(0) += 1
      y += LineHeight
    Loop
    Doc.Offset(0) = 0
    '------------------------------------ Koniec części głównej -------------------------------------------
    For i = 0 To 3
      'e.Graphics.DrawLine(BoldPen, VerticalLinesOrigin(i), y1, VerticalLinesOrigin(i), y)
      Doc.DrawLine(e, VerticalLinesOrigin(i), y1, VerticalLinesOrigin(i), y)
    Next

    Dim y2 As Single
    y2 = y + LineHeight
    y = y2 : x = LeftMargin 'x0
    For i = 6 To 0 Step -1
      Doc.DrawText(e, i.ToString, New Font(TextFont, FontStyle.Bold), x, y, x1 - x, LineHeight, 1, Brushes.Black)
      y += LineHeight
    Next
    x = x1
    Dim j As Integer = 0
    '------------------------------------- Liczba ocen by przedmiot --------------------------------
    Do Until j > Przedmiot.Rows.Count - 1
      y = y2
      For i = 6 To 0 Step -1
        FoundRow = DS.Tables(7).Select("IdPrzedmiot=" & Przedmiot.Rows(j).Item("ID").ToString & " AND Waga=" & i)
        If FoundRow.Length > 0 Then
          Doc.DrawText(e, FoundRow(0).Item(2).ToString, TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
        Else
          Doc.DrawText(e, "-", TextFont, x, y, CellWidth, LineHeight, 1, Brushes.Black, True)
        End If
        y += LineHeight
      Next
      x += CellWidth
      j += 1
    Loop
    'e.Graphics.DrawLine(BoldPen, VerticalLinesOrigin(0), y2, VerticalLinesOrigin(0), y)
    Doc.DrawLine(e, VerticalLinesOrigin(0), y2, VerticalLinesOrigin(0), y)
    '-------------------------------------------- Średnia klasy -------------------------------------------
    y = y2

    TotalAvg = CType(IIf(k > 0, TotalAvg / k, 0), Single)
    If chkColor.Checked Then
      Doc.DrawText(e, Math.Round(TotalAvg, 2).ToString("0.00"), New Font(TextFont, FontStyle.Bold), x, y, CellWidth + 20, LineHeight, 1, Brushes.DarkBlue)
    Else
      Doc.DrawText(e, Math.Round(TotalAvg, 2).ToString("0.00"), New Font(TextFont, FontStyle.Bold), x, y, CellWidth + 20, LineHeight, 1, Brushes.Black)
    End If

    x += CellWidth + 20
    '----------------------------------------- Łączna liczba poszczególnych ocen --------------------------
    For i = 6 To 0 Step -1
      Doc.DrawText(e, TotalMarkNumber(i).ToString, New Font(TextFont, FontStyle.Bold), x, y, CellWidth, LineHeight, 1, Brushes.Black)
      x += CellWidth
    Next
    '-------------------------------------- Suma nieobecności ---------------------------------
    If chkAbsence.Checked Then
      CellWidth += 10
      For i = 0 To 2
        Doc.DrawText(e, TotalAbsence(i).ToString, New Font(TextFont, FontStyle.Bold), x, y, CellWidth, LineHeight, 1, Brushes.Black)
        x += CellWidth
      Next
      CellWidth -= 10
    End If

    For i = 1 To 3
      'e.Graphics.DrawLine(BoldPen, VerticalLinesOrigin(i), y, VerticalLinesOrigin(i), y + LineHeight)
      Doc.DrawLine(e, VerticalLinesOrigin(i), y, VerticalLinesOrigin(i), y + LineHeight)
    Next
    x = VerticalLinesOrigin(2)
    y += LineHeight * 2
    '------------------------------------- Liczba uczniów z ocenami ndst ------------------------------------
    Doc.DrawText(e, "Uczniowie z ocenami niedostatecznymi", New Font(TextFont, FontStyle.Bold), x, y, CellWidth * 8, LineHeight * 2, 1, Brushes.Black)
    y += LineHeight * 2
    'Dim TableHeader() As String = {"n=0", "1" + ChrW(242) + "n<=2", "n>=3", "nkl"}

    Dim TableHeader() As String = {"n=0", "1≥n≤2", "n≥3", "nkl"}
    For i = 0 To 3
      Doc.DrawText(e, TableHeader(i), New Font(TextFont, FontStyle.Bold), x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)
      x += CellWidth * 2
    Next
    x = VerticalLinesOrigin(2) : y += LineHeight
    'FoundRow = DS.Tables(6).Select("OcenaDoSredniej<2")
    'Doc.DrawText(e, CStr(DS.Tables(0).Rows.Count - FoundRow.GetLength(0)), TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)
    Dim ObligatObjects() As DataRow = Przedmiot.Select("Kategoria='o'")
    Doc.DrawText(e, CStr(DS.Tables(0).Rows.Count - DS.Tables(8).Rows.Count - DS.Tables(9).Select("cnt=" & ObligatObjects.GetLength(0)).Length), TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)

    x += CellWidth * 2
    'FoundRow = DS.Tables(6).Select("OcenaDoSredniej<2 And cnt<3")
    'Doc.DrawText(e, FoundRow.GetLength(0).ToString, TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)
    FoundRow = DS.Tables(8).Select("cnt<3")
    Doc.DrawText(e, FoundRow.GetLength(0).ToString, TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)
    x += CellWidth * 2
    'FoundRow = DS.Tables(6).Select("OcenaDoSredniej<2 And cnt>=3 And cnt<" & ObligatObjects.GetLength(0))
    'Doc.DrawText(e, FoundRow.GetLength(0).ToString, TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)
    FoundRow = DS.Tables(8).Select("cnt>=3 And cnt<" & ObligatObjects.GetLength(0))
    Doc.DrawText(e, FoundRow.GetLength(0).ToString, TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)
    x += CellWidth * 2
    'FoundRow = DS.Tables(6).Select("OcenaDoSredniej='0' And cnt=" & ObligatObjects.GetLength(0))
    'Doc.DrawText(e, FoundRow.GetLength(0).ToString, TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)
    FoundRow = DS.Tables(9).Select("cnt=" & ObligatObjects.GetLength(0))
    Doc.DrawText(e, FoundRow.GetLength(0).ToString, TextFont, x, y, CellWidth * 2, LineHeight, 1, Brushes.Black)

    x = VerticalLinesOrigin(2)
    y += LineHeight
    Doc.DrawText(e, "n - Liczba ocen niedostatecznych", New Font(TextFont, FontStyle.Italic), x, y, CellWidth * 10, LineHeight, 0, Brushes.Black, False)
  End Sub

  Private Sub PrintNotes(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, Optional ByVal k As Single = 1)
    Try
      Dim FoundNotes() As DataRow
      Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
      Doc.DrawText(e, "Liczba uwag:", New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
      y += TextLineHeight * k
      For Each NoteType As String In "pn"
        FoundNotes = DS.Tables(4).Select("IdUczen=" & IdUczen & " AND TypUwagi='" + NoteType + "'")
        If FoundNotes.Length > 0 Then
          Doc.DrawText(e, IIf(NoteType = "p", "pozytywnych", "negatywnych").ToString + " - " & FoundNotes(0).Item(0).ToString, TextFont, CSng(IIf(NoteType = "p", x, x + PrintWidth / 2)), y, PrintWidth / 2, TextLineHeight, 0, Brushes.Black, False)
        Else
          Doc.DrawText(e, IIf(NoteType = "p", "pozytywnych", "negatywnych").ToString + " - 0", TextFont, CSng(IIf(NoteType = "p", x, x + PrintWidth / 2)), y, PrintWidth / 2, TextLineHeight, 0, Brushes.Black, False)
        End If
      Next
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try


  End Sub

  Private Sub PrintAbsence(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, Optional ByVal k As Single = 1)
    Try
      Dim FoundAbsence() As DataRow
      Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
      Doc.DrawText(e, "Liczba opuszczonych godzin:", New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, TextLineHeight, 0, Brushes.Black, False)
      y += TextLineHeight * k
      For Each AbsenceType As String In "un"
        FoundAbsence = DS.Tables(3).Select("IdUczen=" & IdUczen & " AND Typ='" + AbsenceType + "'")
        If FoundAbsence.Length > 0 Then
          Doc.DrawText(e, IIf(AbsenceType = "u", "uspr.", "nieuspr.").ToString + " - " & FoundAbsence(0).Item(0).ToString, TextFont, CSng(IIf(AbsenceType = "u", x, x + PrintWidth / 2)), y, PrintWidth / 2, TextLineHeight, 0, Brushes.Black, False)
        Else
          Doc.DrawText(e, IIf(AbsenceType = "u", "uspr.", "nieuspr.").ToString + " - 0", TextFont, CSng(IIf(AbsenceType = "u", x, x + PrintWidth / 2)), y, PrintWidth / 2, TextLineHeight, 0, Brushes.Black, False)
        End If
      Next
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Overloads Sub PrintPartialScores(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, TypWyniku As String, Optional ByVal Interlinia As Single = 1)
    Try
      Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
      Dim DV As DataView = New DataView(DS.Tables("Przedmiot"), "Typ<>'z' AND Typ<>'F'", "Priorytet ASC", DataViewRowState.CurrentRows)
      Dim Przedmiot As DataTable = DV.ToTable(True, "ID", "Alias")
      Dim FoundScore() As DataRow

      'Dim G As Graphics = pvOceny.CreateGraphics
      Do Until (Doc.Offset(1) > Przedmiot.Rows.Count - 1) 'DS.Tables(1).Select("Typ<>'z' AND Typ<>'F'").Count - 1) 
        Doc.DrawText(e, Przedmiot.Rows(Doc.Offset(1)).Item("Alias").ToString, TextFont, x, y, ObjectFieldWidth + ScoreOffset, TextLineHeight, 0, Brushes.Black, False)
        'FoundScore = DS.Tables("ConcatResult").Select("IdUczen=" & IdUczen & " AND IdPrzedmiot=" & Przedmiot.Rows(Doc.Offset(1)).Item("ID").ToString + " AND Typ='" & TypWyniku & "'")
        FoundScore = DS.Tables("Wynik").Select("IdUczen=" & IdUczen & " AND IdPrzedmiot=" & Przedmiot.Rows(Doc.Offset(1)).Item("ID").ToString + " AND Typ='" & TypWyniku & "'")
        Dim MarkString As String = "" ',j As Integer
        'For j = 0 To FoundScore.GetUpperBound(0)
        '  MarkString += FoundScore(j).Item(0).ToString + ", "
        'Next
        For Each Score As DataRow In FoundScore
          If CType(Score.Item("Poprawa"), Boolean) Then
            MarkString = MarkString.TrimEnd(", ".ToCharArray)
            MarkString += "{" & Score.Item("Ocena").ToString & "}, "
          Else
            MarkString += Score.Item("Ocena").ToString + ", "
          End If
        Next
        MarkString = MarkString.TrimEnd(", ".ToCharArray)
        If MarkString.Length > 0 Then
          'If FoundScore.Length > 0 Then
          If e.Graphics.MeasureString(MarkString, TextFont).Width > (PrintWidth - ObjectFieldWidth - ScoreOffset) Then
            OutOfResultWidth = True
          End If
          Doc.DrawText(e, MarkString, TextFont, x + ObjectFieldWidth + ScoreOffset, y, PrintWidth - ObjectFieldWidth - ScoreOffset, TextLineHeight, 0, Brushes.Black, False)
        End If
        y += TextLineHeight * Interlinia  'CSng(1.5)
        Doc.Offset(1) += 1
      Loop
      Doc.Offset(1) = 0
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Doc.Offset(1) = 0
    End Try

  End Sub

  Private Overloads Sub PrintScores(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal IdUczen As String, ByVal Typ As String, Optional ByVal InterLinia As Single = 1)
    Try
      Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)

      Dim DV As DataView = New DataView(DS.Tables("Przedmiot"))
      Dim Przedmiot As DataTable = DV.ToTable(True, "ID", "Alias")
      Dim FoundScore() As DataRow
      Do Until (Doc.Offset(1) > Przedmiot.Rows.Count - 1) 'DS.Tables(1).Rows.Count - 1)
        Doc.DrawText(e, Przedmiot.Rows(Doc.Offset(1)).Item(1).ToString, TextFont, x, y, ObjectFieldWidth + ScoreOffset, TextLineHeight, 0, Brushes.Black, False)

        FoundScore = DS.Tables(2).Select("IdUczen=" & IdUczen & " AND IdPrzedmiot=" & Przedmiot.Rows(Doc.Offset(1)).Item("ID").ToString + " AND Typ='" + Typ + "'")
        If FoundScore.Length > 0 Then
          'If e.Graphics.MeasureString(FoundScore(0).Item(1).ToString, TextFont).Width > (PrintWidth - ObjectFieldWidth - ScoreOffset) Then MessageBox.Show("Ocena z przedmiotu '" & Przedmiot.Rows(Doc.Offset(1)).Item("Alias").ToString & "' nie mieści się w wyznaczonym obszarze." & Chr(13) & "Zmniejsz wielkość czcionki lub marginesy i spróbuj jeszcze raz.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
          If e.Graphics.MeasureString(FoundScore(0).Item(1).ToString, TextFont).Width > (PrintWidth - ObjectFieldWidth - ScoreOffset) Then OutOfResultWidth = True
          If chkColor.Checked Then
            Doc.DrawText(e, FoundScore(0).Item(1).ToString, TextFont, x + ObjectFieldWidth + ScoreOffset, y, PrintWidth - ObjectFieldWidth - ScoreOffset, TextLineHeight, 0, CType(IIf(CInt(FoundScore(0).Item("Waga").ToString) < 2 AndAlso CInt(FoundScore(0).Item("Waga").ToString) > -1, Brushes.Red, Brushes.Black), Brush), False)
          Else
            Doc.DrawText(e, FoundScore(0).Item(1).ToString, TextFont, x + ObjectFieldWidth + ScoreOffset, y, PrintWidth - ObjectFieldWidth - ScoreOffset, TextLineHeight, 0, Brushes.Black, False)
          End If

        End If

        y += TextLineHeight * InterLinia
        Doc.Offset(1) += 1
      Loop
      Doc.Offset(1) = 0
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Doc.Offset(1) = 0
    End Try

  End Sub

  Private Sub PrintTutor(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single)
    Try
      Dim TextLineHeight As Single = TextFont.GetHeight(e.Graphics)
      Doc.DrawText(e, "Wychowawca klasy", New Font(TextFont, FontStyle.Bold), x, y, PrintWidth - 20, TextLineHeight, 2, Brushes.Black, False)
      'Doc.DrawText(e, "Wychowawca klasy", New Font(TextFont, FontStyle.Bold), x + PrintWidth / 2, y, PrintWidth / 2, TextLineHeight, 1, Brushes.Black, False)
      y += TextLineHeight
      'If Doc.Podpis IsNot Nothing Then Doc.DrawImage(e, Image.FromStream(New System.IO.MemoryStream(Doc.Podpis)), x + PrintWidth / 2 + (PrintWidth / 2 - 168) / 2, y, 168, 39)
      If Doc.Podpis IsNot Nothing Then Doc.DrawImage(e, Image.FromStream(New System.IO.MemoryStream(Doc.Podpis)), x + PrintWidth - 168, y, 168, 39)
      'If Doc.Podpis IsNot Nothing Then e.Graphics.DrawImage(Image.FromStream(New System.IO.MemoryStream(Doc.Podpis)), x + PrintWidth / 2 + (PrintWidth / 2 - 168) / 2, y, 168, 39)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub PrintHeader(ByVal e As PrintPageEventArgs, ByVal x As Single, ByRef y As Single, ByVal PrintWidth As Single, ByVal StudentName As String)
    Try
      Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
      Doc.DrawText(e, Doc.ReportHeader(0) & ", dnia " + dtData.Value.ToLongDateString + " r.", TextFont, x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
      y += HeaderLineHeight * 2 'CSng(1.5)
      Doc.DrawText(e, StudentName, HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      y += HeaderLineHeight * CSng(1.5)
      Doc.DrawText(e, "Klasa " & Me.cbKlasa.Text, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      Doc.DrawText(e, "Semestr " & Me.nudSemestr.Value.ToString, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, HeaderLineHeight, 2, Brushes.Black, False)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try


  End Sub

  Private Sub DrawDivisionLines(ByVal e As PrintPageEventArgs)
    Try
      Dim DotPen As New Pen(Color.Black)
      DotPen.DashStyle = Drawing2D.DashStyle.Dot
      Dim x1, y1 As Single
      x1 = CSng(e.PageBounds.Width / 2) + My.Settings.XCaliber  ' - Doc.DefaultPageSettings.PrintableArea.Left
      y1 = CSng(e.PageBounds.Height / 2) + My.Settings.YCaliber ' - Doc.DefaultPageSettings.PrintableArea.Top
      'If Not Doc.IsPreview Then
      '  x1 -= Doc.DefaultPageSettings.PrintableArea.Left
      '  y1 -= Doc.DefaultPageSettings.PrintableArea.Top
      'End If
      e.Graphics.DrawLine(DotPen, x1, 0, x1, e.PageBounds.Height)
      e.Graphics.DrawLine(DotPen, e.PageBounds.X, y1, e.PageBounds.Width, y1)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub DrawDivisionLine(ByVal e As PrintPageEventArgs)
    Try
      Dim DotPen As New Pen(Color.Black)
      DotPen.DashStyle = Drawing2D.DashStyle.Dot
      Dim x1 As Single
      x1 = CSng(e.PageBounds.Width / 2) + My.Settings.XCaliber  ' - Doc.DefaultPageSettings.PrintableArea.Left
      'y1 = CSng(e.PageBounds.Height / 2) + My.Settings.YCaliber ' - Doc.DefaultPageSettings.PrintableArea.Top
      'If Not Doc.IsPreview Then
      '  x1 -= Doc.DefaultPageSettings.PrintableArea.Left
      '  'y1 -= Doc.DefaultPageSettings.PrintableArea.Top
      'End If
      e.Graphics.DrawLine(DotPen, x1, 0, x1, e.PageBounds.Height)
      'e.Graphics.DrawLine(DotPen, e.PageBounds.X, y1, e.PageBounds.Width, y1)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub
  Private Sub dtData_ValueChanged(sender As Object, e As EventArgs) Handles dtData.ValueChanged
    If Not Me.Created Then Exit Sub
    Me.Doc.PageNumber = 0
    pvOceny.InvalidatePreview()
  End Sub
  Private Sub cmdPageSetup_Click(sender As Object, e As EventArgs) Handles cmdPageSetup.Click
    Dim pg As New PageSetupDialog, prnVars As New PrintVariables
    pg.EnableMetric = True
    pg.Document = Doc
    pg.MinMargins.Left = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Left)
    pg.MinMargins.Top = CInt(Doc.PrinterSettings.DefaultPageSettings.PrintableArea.Top)

    pg.PageSettings.Margins.Top = CInt(TopMargin)
    pg.PageSettings.Margins.Left = CInt(LeftMargin)
    pg.PageSettings.Margins.Bottom = CInt(TopMargin)
    pg.PageSettings.Margins.Right = CInt(LeftMargin)
    pg.AllowPaper = False
    pg.AllowOrientation = False
    If pg.ShowDialog = Windows.Forms.DialogResult.OK Then
      My.Settings.TopMargin = pg.PageSettings.Margins.Top
      My.Settings.LeftMargin = pg.PageSettings.Margins.Left
      LeftMargin = My.Settings.LeftMargin
      TopMargin = My.Settings.TopMargin
      rbEndingScores_CheckedChanged(IIf(rbEndingScores.Checked, rbEndingScores, rbPartialScores), e)
    End If
  End Sub

  Private Sub cmdFontSetup_Click(sender As Object, e As EventArgs) Handles cmdFontSetup.Click
    Dim fg As New FontDialog, prnVars As New PrintVariables
    fg.MinSize = 8
    fg.MaxSize = 20
    fg.FontMustExist = True
    fg.Font = TextFont
    If fg.ShowDialog = Windows.Forms.DialogResult.OK Then
      'TextFont = fg.Font
      My.Settings.TextFont = fg.Font
      My.Settings.HeaderFont = New Font("Arial", My.Settings.TextFont.Size + 2, FontStyle.Bold)
      TextFont = My.Settings.TextFont
      HeaderFont = My.Settings.HeaderFont
      rbEndingScores_CheckedChanged(IIf(rbEndingScores.Checked, rbEndingScores, rbPartialScores), e)

    End If
  End Sub

  Private Sub chkAbsence_CheckedChanged(sender As Object, e As EventArgs) Handles chkAbsence.CheckedChanged, chkNote.CheckedChanged, chkColor.CheckedChanged
    If Not Me.Created Then Exit Sub
    pvOceny.InvalidatePreview()
  End Sub

End Class