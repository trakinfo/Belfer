Imports System.Drawing.Printing
Public Class frmAnalizaOcen
  Private DS As DataSet, DTLiczbaOcen As DataTable, EndDateOfSchoolYear As Date, StartOfSemester2 As Date
  'Private Kolumna As List(Of Pole)
  Private Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Klasa", .Size = 100},
        New Pole With {.Name = "Stan klasy", .Size = 80},
        New Pole With {.Name = "Liczba wyst. ocen", .Size = 100},
        New Pole With {.Name = "Liczba ucz. nkl", .Size = 90},
        New Pole With {.Name = "celujący", .Size = 100},
        New Pole With {.Name = "bardzo dobry", .Size = 100},
        New Pole With {.Name = "dobry", .Size = 100},
        New Pole With {.Name = "dostateczny", .Size = 100},
        New Pole With {.Name = "dopuszczający", .Size = 100},
        New Pole With {.Name = "niedostateczny", .Size = 100}
    }
  Public Event NewRow()
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.AnalizaOcenToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.AnalizaOcenToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData(Data As Date)
    Dim S As New StatystykaSQL, DBA As New DataBaseAction
    'Stanklasy nie uwzględnia skreślenia
    DS = DBA.GetDataSet(S.SelectPrzedmiot(My.Settings.IdSchool, My.Settings.SchoolYear) & S.SelectKlasa(My.Settings.IdSchool, My.Settings.SchoolYear, Data) & S.SelectStanKlasy(My.Settings.IdSchool, My.Settings.SchoolYear, Data) & S.SelectStanKlasyWirtualnej(My.Settings.IdSchool, My.Settings.SchoolYear, Data))
    DS.Tables(0).TableName = "Przedmiot"
    DS.Tables(1).TableName = "Klasa"
    DS.Tables(2).TableName = "StanKlasy"
    DS.Tables(3).TableName = "StanKlasyWirtualnej"
    FetchLiczbaOcen()
  End Sub
  Private Sub FetchLiczbaOcen()
    Dim S As New StatystykaSQL, DBA As New DataBaseAction
    DTLiczbaOcen = DBA.GetDataTable(S.SelectLiczbaOcen(My.Settings.IdSchool, My.Settings.SchoolYear, IIf(nudSemestr.Value = 1, "S", "R").ToString))
    DTLiczbaOcen.TableName = "LiczbaOcen"
  End Sub
  Private Sub frmAnalizaOcen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvAnaliza)
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    Dim CH As New CalcHelper, CurrentDate As Date
    CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
    StartOfSemester2 = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
    EndDateOfSchoolYear = CH.EndDateOfSchoolYear(My.Settings.SchoolYear)

    If CurrentDate < StartOfSemester2 Then
      Me.nudSemestr.Value = 1
      FetchData(StartOfSemester2.AddDays(-1))
    Else
      Me.nudSemestr.Value = 2
    End If
    'Me.nudSemestr.Value = CType(IIf(Today < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year), 1, 2), Integer)
    FillBelfer(cbNauczyciel)
    'If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
  End Sub
  Private Sub FillBelfer(ByVal cb As ComboBox)
    cb.Items.Clear()
    Dim FCB As New FillComboBox, P As New PlanSQL
    FCB.AddComboBoxComplexItems(cb, P.SelectBelfer(My.Settings.IdSchool, My.Settings.SchoolYear))
    Dim SH As New SeekHelper
    If My.Settings.IdBelfer.Length > 0 Then SH.FindComboItem(cb, CType(My.Settings.IdBelfer, Integer))
    cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub ListViewConfig(LV As ListView)
    With LV
      .View = View.Details
      .FullRowSelect = True
      .GridLines = True
      '.ColorGridLines = Color.Beige
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      'AddListViewColumn(LV)
      '.Columns.Add("Kod", 0, HorizontalAlignment.Center)
      .Columns.Add("Klasa", 100, HorizontalAlignment.Center)
      .Columns.Add("Stan klasy", 80, HorizontalAlignment.Center)
      .Columns.Add("Liczba wyst. ocen", 100, HorizontalAlignment.Center)
      .Columns.Add("Liczba ucz. nkl", 90, HorizontalAlignment.Center)
      .Columns.Add("Celujący", 90, HorizontalAlignment.Center)
      .Columns.Add("bardzo dobry", 90, HorizontalAlignment.Center)
      .Columns.Add("dobry", 90, HorizontalAlignment.Center)
      .Columns.Add("dostateczny", 90, HorizontalAlignment.Center)
      .Columns.Add("dopuszczający", 90, HorizontalAlignment.Center)
      .Columns.Add("niedostateczny", 90, HorizontalAlignment.Center)
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub GetData(lv As ListView, Belfer As String)
    Dim DBA As New DataBaseAction
    Try
      lv.Items.Clear()
      lv.Groups.Clear()

      For Each Przedmiot As DataRow In DS.Tables("Przedmiot").Select("Nauczyciel=" & Belfer)
        LvNewItem(lvAnaliza, Przedmiot.Item("ID").ToString, Belfer, Przedmiot.Item("Alias").ToString)
        'If lv.Items.Count > 0 Then LvSumItem(lv, Item.ID.ToString, CType(lv.Groups(Item.Nazwa), ListViewGroup))
      Next
      lv.Columns(0).Width = CInt(IIf(lv.Items.Count > 19, 80, 100))
      If lv.Items.Count > 0 Then
        lv.Enabled = True
        cmdPrint.Enabled = True
      Else
        lv.Enabled = False
        cmdPrint.Enabled = False
      End If
      'lv.Enabled = CType(IIf(lv.Items.Count > 0, True, False), Boolean)
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub LvNewItem(ByVal LV As ListView, ByVal Przedmiot As String, Belfer As String, ByVal Grupa As String)
    Dim NG As New ListViewGroup(Grupa, Grupa), TotalStudentCount As Integer = 0  ', Kl As String = ""
    NG.HeaderAlignment = HorizontalAlignment.Left
    'If DT.Select("IdObsada='" & Klasa & "'").Length > 0 Then
    LV.Groups.Add(NG)
    For Each Klasa As DataRow In DS.Tables("Klasa").Select("IdPrzedmiot=" & Przedmiot & " AND Nauczyciel=" & Belfer)
      Dim StudentCount As Integer = 0
      Dim NewItem As New ListViewItem(Klasa.Item("Nazwa_Klasy").ToString, NG)
      'Kl += String.Concat(Klasa.Item("Klasa").ToString, ",")
      NewItem.UseItemStyleForSubItems = True
      If CType(Klasa.Item("Virtual"), Boolean) Then
        NewItem.SubItems.Add(DS.Tables("StanKlasyWirtualnej").Select("KlasaWirtualna=" & Klasa.Item("Klasa").ToString & " AND IdPrzedmiot=" & Przedmiot)(0).Item("StanKlasy").ToString)
        TotalStudentCount += CType(DS.Tables("StanKlasyWirtualnej").Select("KlasaWirtualna=" & Klasa.Item("Klasa").ToString & " AND IdPrzedmiot=" & Przedmiot)(0).Item("StanKlasy"), Integer)
      Else
        StudentCount = CType(DS.Tables("StanKlasy").Select("Klasa=" & Klasa.Item("Klasa").ToString)(0).Item("StanKlasy"), Integer)
        If DS.Tables("StanKlasyWirtualnej").Select("Klasa=" & Klasa.Item("Klasa").ToString & " AND IdPrzedmiot=" & Przedmiot).Length > 0 Then StudentCount -= CType(DS.Tables("StanKlasyWirtualnej").Select("Klasa=" & Klasa.Item("Klasa").ToString & " AND IdPrzedmiot=" & Przedmiot)(0).Item("StanKlasy"), Integer)
        NewItem.SubItems.Add(StudentCount.ToString)
        TotalStudentCount += StudentCount 
      End If
      If DTLiczbaOcen.Select("Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga>0").GetLength(0) > 0 Then
        NewItem.SubItems.Add(DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga>0").ToString)
      Else
        NewItem.SubItems.Add(0.ToString)
      End If
      If DTLiczbaOcen.Select("Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=0").GetLength(0) > 0 Then
        NewItem.SubItems.Add(DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=0").ToString)
      Else
        NewItem.SubItems.Add("-")
      End If
      For i As Integer = 6 To 1 Step -1
        If DTLiczbaOcen.Select("Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=" & i).GetLength(0) > 0 Then
          NewItem.SubItems.Add(DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=" & i).ToString)
        Else
          NewItem.SubItems.Add(0.ToString)
        End If
      Next
      LV.Items.Add(NewItem)
    Next

    LvSumItem(LV, Przedmiot, Belfer, NG, TotalStudentCount)
    LvProcentItem(LV, Przedmiot, Belfer, NG, TotalStudentCount)
  End Sub
  Private Sub LvSumItem(ByVal LV As ListView, Przedmiot As String, Belfer As String, Grupa As ListViewGroup, StudentCount As Integer)
    Dim NewItem As New ListViewItem("Razem", Grupa)
    NewItem.UseItemStyleForSubItems = True
    NewItem.ForeColor = Color.Coral
    NewItem.Font = New Font(LV.Font, FontStyle.Bold)
    'uwzględnić wirtu
    'NewItem.SubItems.Add(DS.Tables("StanKlasy").Compute("SUM(StanKlasy)", "Klasa IN (" & Klasa & ")").ToString)
    NewItem.SubItems.Add(StudentCount.ToString)
    NewItem.SubItems.Add(IIf(DTLiczbaOcen.Select("IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "'").GetLength(0) > 0, DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga>0"), 0).ToString)
    'NewItem.SubItems.Add(DS.Tables("LiczbaOcen").Compute("SUM(LiczbaOcen)", "IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "'").ToString)
    If DTLiczbaOcen.Select("IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=0").GetLength(0) > 0 Then
      NewItem.SubItems.Add(DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=0").ToString)
    Else
      NewItem.SubItems.Add("-")
    End If
    For i As Integer = 6 To 1 Step -1
      If DTLiczbaOcen.Select("IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=" & i).GetLength(0) > 0 Then
        NewItem.SubItems.Add(DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=" & i).ToString)
      Else
        NewItem.SubItems.Add(0.ToString)
      End If
    Next
    LV.Items.Add(NewItem)
    'Next
  End Sub
  Private Sub LvProcentItem(ByVal LV As ListView, Przedmiot As String, Belfer As String, Grupa As ListViewGroup, StudentCount As Integer)
    Dim NewItem As New ListViewItem("% wyst. ocen", Grupa), LiczbaOcen, LiczbaNKL As Integer, ProcentOcen As String = ""
    NewItem.UseItemStyleForSubItems = True
    NewItem.ForeColor = Color.Firebrick
    NewItem.Font = New Font(LV.Font, FontStyle.Bold Or FontStyle.Italic)
    NewItem.SubItems.Add(Chr(151))
    LiczbaOcen = CType(IIf(DTLiczbaOcen.Select("IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga>0").GetLength(0) > 0, DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga>0"), 0), Integer)
    ProcentOcen = Math.Round(LiczbaOcen / StudentCount * 100, 2).ToString
    NewItem.SubItems.Add(String.Concat(ProcentOcen, "%"))
    Dim ProcentNKL As String = ""
    LiczbaNKL = CType(IIf(DTLiczbaOcen.Select("IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=0").GetLength(0) > 0, DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=0"), 0), Integer)
    ProcentNKL = Math.Round(LiczbaNKL / StudentCount * 100, 2).ToString
    NewItem.SubItems.Add(String.Concat(ProcentNKL, "%"))
    For i As Integer = 6 To 1 Step -1
      If DTLiczbaOcen.Select("IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=" & i).GetLength(0) > 0 Then
        ProcentOcen = Math.Round(CType(DTLiczbaOcen.Compute("SUM(LiczbaOcen)", "IdPrzedmiot='" & Przedmiot & "' AND Nauczyciel='" & Belfer & "' AND Waga=" & i), Integer) / LiczbaOcen * 100, 2).ToString
        NewItem.SubItems.Add(String.Concat(ProcentOcen, "%"))
      Else
        NewItem.SubItems.Add("0%")
      End If
    Next
    LV.Items.Add(NewItem)
    'Next
  End Sub
  Private Sub cbNauczyciel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbNauczyciel.SelectedIndexChanged
    GetData(lvAnaliza, CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString)
  End Sub

  Private Sub nudSemestr_ValueChanged(sender As Object, e As EventArgs) Handles nudSemestr.ValueChanged
    If Not Me.Created Then Exit Sub
    FetchData(If(nudSemestr.Value = 1, StartOfSemester2.AddDays(-1), EndDateOfSchoolYear))
    If cbNauczyciel.SelectedItem IsNot Nothing Then GetData(lvAnaliza, CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString)
  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Dim PP As New dlgPrintPreview, DSP As New DataSet ', DBA As New DataBaseAction, S As New SzkolaSQL
    For Each Table As DataTable In DS.Tables
      DSP.Tables.Add(Table.Copy)
    Next
    'DSP.Tables(0).TableName = "Przedmiot"
    DSP.Tables.Add(DTLiczbaOcen.Copy)
    'DS.Tables.Add(DBA.GetDataTable(S.SelectSchoolName(My.Settings.IdSchool)))

    PP.Doc = New PrintReport(DSP)
    AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage
    AddHandler NewRow, AddressOf PP.NewRow
    PP.Doc.ReportHeader = New String() {"Analiza ocen " & Chr(150) & IIf(nudSemestr.Value = 1, " klasyfikacja śródroczna", " klasyfikacja roczna").ToString, "Nauczyciel: " & cbNauczyciel.Text}
    'PP.Doc.GroupHeader = New String() {CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString}
    PP.Doc.DefaultPageSettings.Landscape = True

    PP.Width = 1000
    If PP.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
      DSP.Tables.Clear()
    End If
  End Sub

  
  Public Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
    Dim Doc As PrintReport = CType(sender, PrintReport)
    Dim x As Single = Doc.DefaultPageSettings.Margins.Left '- Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Left
    Dim y As Single = Doc.DefaultPageSettings.Margins.Top '- Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Top
    Dim PrnVars As New PrintVariables
    Dim TextFont As Font = PrnVars.BaseFont
    Dim HeaderFont As Font = PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    'Doc.DrawHeader(e.Graphics, x, e.MarginBounds.Top, CSng(e.PageBounds.Width) - 2 * x)
    'Doc.DrawFooter(e.Graphics, x, e.MarginBounds.Bottom, CSng(e.PageBounds.Width) - 2 * x)
    Doc.DrawHeader(e, x, e.MarginBounds.Top, e.MarginBounds.Width)
    Doc.DrawFooter(e, x, e.MarginBounds.Bottom, e.MarginBounds.Width)

    Doc.PageNumber += 1
    Doc.DrawPageNumber(e, "- " & Doc.PageNumber & " -", x, y, e.MarginBounds.Width)
    'Doc.DrawText(e, "- " & Doc.PageNumber & " -", TextFont, x,  Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Top + LineHeight, e.MarginBounds.Width, LineHeight, 1, Brushes.Black, False)
    If Doc.PageNumber = 1 Then
      y += LineHeight
      'Doc.DrawText(e, "Szkoła: " & Doc.ReportHeader(0), TextFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 0, Brushes.Black, False)
      'Doc.DrawText(e, "Rok szkolny: " & Doc.ReportHeader(1), TextFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 2, Brushes.Black, False)
      'y += LineHeight * 2
      Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 1, Brushes.Black, False)
      y += LineHeight * 3
      Doc.DrawText(e, Doc.ReportHeader(1), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 2, Brushes.Black, False)
      y += LineHeight * 2
    End If

    Dim TableSize As Integer = 0
    For Each Col In Kolumna
      With Col
        TableSize += .Size
      End With
    Next

    x += CType((e.MarginBounds.Width - TableSize) / 2, Single) '+ Doc.DefaultPageSettings.PrintableArea.Left
    Do Until (y + LineHeight * 6) > e.MarginBounds.Bottom Or Doc.Offset(0) > lvAnaliza.Groups.Count - 1
      Dim ColSize As Integer = 0
      If Doc.Offset(1) = 0 Then
        Doc.DrawText(e, "Przedmiot: " & lvAnaliza.Groups(Doc.Offset(0)).ToString, HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 0, Brushes.Black, False)
        y += LineHeight * 2
        For Each Col In Kolumna
          With Col
            Doc.DrawText(e, .Name, PrnVars.BoldFont, x + ColSize, y, .Size, LineHeight * 3, 1, Brushes.Black)
            ColSize += .Size
          End With
        Next
        y += LineHeight * 3
        ColSize = 0
      End If

      Do Until (y + LineHeight) > e.MarginBounds.Bottom Or Doc.Offset(1) > lvAnaliza.Groups(Doc.Offset(0)).Items.Count - 3
        For i As Integer = 0 To Kolumna.Count - 1
          Doc.DrawText(e, lvAnaliza.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(i).Text, TextFont, x + ColSize, y, Kolumna(i).Size, LineHeight, 1, Brushes.Black)
          ColSize += Kolumna(i).Size
        Next
        ColSize = 0
        y += LineHeight
        Doc.Offset(1) += 1
      Loop
      If Doc.Offset(1) <= lvAnaliza.Groups(Doc.Offset(0)).Items.Count - 3 Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      End If
      Do Until (y + LineHeight * 2) > e.MarginBounds.Bottom Or Doc.Offset(2) > 1
        For i As Integer = 0 To Kolumna.Count - 1
          Doc.DrawText(e, lvAnaliza.Groups(Doc.Offset(0)).Items(lvAnaliza.Groups(Doc.Offset(0)).Items.Count - (2 - Doc.Offset(2))).SubItems(i).Text, CType(IIf(Doc.Offset(2) = 0, PrnVars.BoldFont, New Font(TextFont, FontStyle.Bold Or FontStyle.Italic)), Font), x + ColSize, y, Kolumna(i).Size, LineHeight * 2, 1, Brushes.Black)
          ColSize += Kolumna(i).Size
        Next
        ColSize = 0
        y += LineHeight * 2
        Doc.Offset(2) += 1
      Loop
      If Doc.Offset(2) <= 1 Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      Else
        Doc.Offset(1) = 0
        Doc.Offset(2) = 0
        y += LineHeight * 2
        Doc.Offset(0) += 1
      End If
    Loop

    If Doc.Offset(0) <= lvAnaliza.Groups.Count - 1 Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Doc.Offset(0) = 0
    End If
  End Sub

  Private Sub cbNauczyciel_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbNauczyciel.SelectionChangeCommitted
    My.Settings.IdBelfer = CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
End Class