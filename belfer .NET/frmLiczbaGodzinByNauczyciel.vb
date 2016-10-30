Imports System.Drawing.Printing
Public Class frmLiczbaGodzinByNauczyciel
  Private DS As DataSet
  Public Event NewRow()
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.LiczbaGodzinWgNauczycielaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.LiczbaGodzinWgNauczycielaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  'Private IdPrzedmiot As Integer

  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvLiczbaGodzin)
    ApplyNewConfig()
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
      .Columns.Add("Klasa", 215, HorizontalAlignment.Left)
      .Columns.Add("Wymagana liczba godzin", 150, HorizontalAlignment.Center)
      .Columns.Add("Zrealizowana liczba godzin", 150, HorizontalAlignment.Center)
      .Columns.Add("Różnica", 150, HorizontalAlignment.Center)

      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub ApplyNewConfig()
    FillBelfer()
  End Sub
  Private Sub FetchData(Nauczyciel As String)
    Dim S As New StatystykaSQL, DBA As New DataBaseAction
    DS = DBA.GetDataSet(S.CountLekcjaByBelfer(Nauczyciel, My.Settings.SchoolYear) & S.CountZastepstwoByBelfer(Nauczyciel, My.Settings.SchoolYear) & S.SelectRequiredNumberOfActivities(My.Settings.SchoolYear))
    DS.Tables(0).TableName = "LiczbaLekcji"
    DS.Tables(1).TableName = "LiczbaZastepstw"
    DS.Tables(2).TableName = "WymaganaLiczbaLekcji"
  End Sub
  Private Sub FillBelfer()
    cbNauczyciel.Items.Clear()
    Dim FCB As New FillComboBox, P As New PlanSQL
    FCB.AddComboBoxComplexItems(cbNauczyciel, P.SelectBelfer(My.Settings.IdSchool, My.Settings.SchoolYear))
    Dim SH As New SeekHelper
    If My.Settings.IdBelfer.Length > 0 Then SH.FindComboItem(Me.cbNauczyciel, CType(My.Settings.IdBelfer, Integer))
    cbNauczyciel.Enabled = CType(IIf(cbNauczyciel.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbNauczyciel.SelectionChangeCommitted
    My.Settings.IdBelfer = CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbNauczyciel.SelectedIndexChanged
    lvLiczbaGodzin.Items.Clear()
    lvLiczbaGodzin.Enabled = False
    FetchData(CType(cbNauczyciel.SelectedItem, CbItem).ID.ToString)
    GetData(lvLiczbaGodzin)
  End Sub
  
  Private Sub GetData(lv As ListView)
    Try
      lv.Items.Clear()
      lv.Groups.Clear()
      For Each Przedmiot As DataRow In DS.Tables("LiczbaLekcji").DefaultView.ToTable(True, "Przedmiot", "Alias").Rows 'DV.ToTable(True, "Przedmiot,Alias").Rows
        LvNewItem(lv, Przedmiot.Item("Przedmiot").ToString, Przedmiot.Item("Alias").ToString)
      Next

      lv.Columns(0).Width = CInt(IIf(lv.Items.Count > 20, 196, 215))
      If lv.Items.Count > 0 Then
        lv.Enabled = True
        cmdPrint.Enabled = True
      Else
        lv.Enabled = False
        cmdPrint.Enabled = False
      End If
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  'ciągle brakuje pewności czy dobrze działa (zliczanie zastepstw i godzin w pewnych warunkach) wydruk dostosować do zmian
  Private Function AddZastepstwo(Klasa As String, Przedmiot As String) As Integer
    If DS.Tables("LiczbaZastepstw").Select("Klasa=" & Klasa & " AND Przedmiot=" & Przedmiot).Length > 0 Then
      Return CType(DS.Tables("LiczbaZastepstw").Select("Klasa=" & Klasa & " AND Przedmiot=" & Przedmiot)(0).Item("LiczbaZastepstw"), Integer)
    Else
      Return 0
    End If
  End Function
 
  Private Overloads Sub LvNewItem(ByVal LV As ListView, ByVal Przedmiot As String, ByVal Grupa As String)
    Dim NG As New ListViewGroup(Grupa, Grupa)
    NG.HeaderAlignment = HorizontalAlignment.Left
    NG.Header.ToUpper()
    LV.Groups.Add(NG)
    For Each row As DataRow In DS.Tables("LiczbaLekcji").Select("Przedmiot=" & Przedmiot)
      Dim NewItem As New ListViewItem(row.Item("Nazwa_Klasy").ToString, NG)
      NewItem.UseItemStyleForSubItems = False
      Dim WLiczbaGodzin As Single = 0
      If DS.Tables("WymaganaLiczbaLekcji").Select("Przedmiot='" & row.Item("IdPrzedmiot").ToString & "' AND Klasa=" & row.Item("Klasa").ToString).Length > 0 Then
        WLiczbaGodzin = CType(DS.Tables("WymaganaLiczbaLekcji").Select("Przedmiot='" & row.Item("IdPrzedmiot").ToString & "' AND Klasa=" & row.Item("Klasa").ToString)(0).Item("Wartosc"), Single)
      End If
      NewItem.SubItems.Add(WLiczbaGodzin.ToString)

      Dim LiczbaGodzin As Integer = 0
      LiczbaGodzin = CType(row.Item("LiczbaGodzin"), Integer)
      LiczbaGodzin += AddZastepstwo(row.Item("Klasa").ToString, Przedmiot)
      NewItem.SubItems.Add(LiczbaGodzin.ToString)
      Dim Subtract As Single
      Subtract = LiczbaGodzin - WLiczbaGodzin
      If Subtract < 0 Then
        NewItem.SubItems.Add((Subtract).ToString, Color.Red, NewItem.BackColor, NewItem.Font)
      Else
        NewItem.SubItems.Add((Subtract).ToString)
      End If
      'If LiczbaGodzin > 0 Then
      LV.Items.Add(NewItem)
      'End If
      'End If
    Next
    'LvSumItem(LV, Przedmiot, NG)
  End Sub
 
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Dim PP As New dlgPrintPreview, DSP As New DataSet
    'DSP.Tables.Add()
    PP.Doc = New PrintReport(DSP)
    'If chkPrzedmiot.Checked Then
    AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_SelectedPrzedmiot_PrintPage
    'Else
    'AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllPrzedmiot_PrintPage
    'End If
    AddHandler NewRow, AddressOf PP.NewRow
    PP.Doc.ReportHeader = New String() {"Godzinowa realizacja planu nauczania w roku szkolnym " & My.Settings.SchoolYear, "Nauczyciel: " & cbNauczyciel.Text}
    PP.Width = 1000
    PP.ShowDialog()
    'If PP.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
    '  DSP.Tables.Clear()
    'End If
  End Sub
  Public Sub PrnDoc_SelectedPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
    Dim Doc As PrintReport = CType(sender, PrintReport)
    Dim x As Single = Doc.DefaultPageSettings.Margins.Left '- Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Left
    Dim y As Single = Doc.DefaultPageSettings.Margins.Top '- Doc.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Top
    Dim PrnVars As New PrintVariables
    Dim TextFont As Font = PrnVars.BaseFont
    Dim HeaderFont As Font = PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)

    '---------------------------------------- Nagłówek i stopka ----------------------------
    Doc.DrawHeader(e, x, e.MarginBounds.Top, e.MarginBounds.Width)
    Doc.DrawFooter(e, x, e.MarginBounds.Bottom, e.MarginBounds.Width)
    Doc.PageNumber += 1
    Doc.DrawPageNumber(e, "- " & Doc.PageNumber.ToString & " -", x, y, e.MarginBounds.Width)
    If Doc.PageNumber = 1 Then
      y += LineHeight
      Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 1, Brushes.Black, False)
      y += HeaderLineHeight * 2
      Doc.DrawText(e, Doc.ReportHeader(1), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 0, Brushes.Black, False)
      y += LineHeight * 2
    End If
    Dim KlasaColSize As Integer = 300
    Dim ColSize As Integer = CInt((e.MarginBounds.Width - KlasaColSize) / 3)

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "KL", .Label = "Klasa", .Size = KlasaColSize},
        New Pole With {.Name = "WL", .Label = "Wymagana", .Size = ColSize},
        New Pole With {.Name = "ZRL", .Label = "Zrealizowana", .Size = ColSize},
        New Pole With {.Name = "Diff", .Label = "Różnica", .Size = ColSize}
    }

    Doc.DrawText(e, "Klasa", PrnVars.BoldFont, x, y, KlasaColSize, LineHeight * CSng(3), 1, Brushes.Black)
    Doc.DrawText(e, "Liczba godzin", PrnVars.BoldFont, x + KlasaColSize, y, ColSize * 2, LineHeight * CSng(1.5), 1, Brushes.Black)
    Doc.DrawText(e, "Różnica", PrnVars.BoldFont, x + KlasaColSize + ColSize * 2, y, ColSize, LineHeight * CSng(3), 1, Brushes.Black)

    y += LineHeight * CSng(1.5)

    ColSize = KlasaColSize
    Doc.DrawText(e, Kolumna.Item(1).Label, PrnVars.BoldFont, x + ColSize, y, Kolumna.Item(1).Size, LineHeight * CSng(1.5), 1, Brushes.Black)
    ColSize += Kolumna.Item(1).Size
    Doc.DrawText(e, Kolumna.Item(2).Label, PrnVars.BoldFont, x + ColSize, y, Kolumna.Item(2).Size, LineHeight * CSng(1.5), 1, Brushes.Black)

    y += LineHeight * CSng(2)

    Do Until (y + LineHeight * CSng(3)) > e.MarginBounds.Bottom Or Doc.Offset(0) > lvLiczbaGodzin.Groups.Count - 1
      Doc.DrawText(e, lvLiczbaGodzin.Groups(Doc.Offset(0)).Name.ToUpper, New Font(TextFont, FontStyle.Bold), x, y, e.MarginBounds.Width, LineHeight * 2, 0, Brushes.Black, False)
      y += LineHeight * CSng(1.5)
      Do Until (y + LineHeight * CSng(3)) > e.MarginBounds.Bottom Or Doc.Offset(1) > lvLiczbaGodzin.Groups(Doc.Offset(0)).Items.Count - 1
        ColSize = 0
        Dim i As Integer = 0
        For Each Col In Kolumna
          Doc.DrawText(e, lvLiczbaGodzin.Groups(Doc.Offset(0)).Items(Doc.Offset(1)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, 1, Brushes.Black)
          ColSize += Col.Size
          i += 1
        Next
        y += LineHeight
        Doc.Offset(1) += 1
      Loop
      If Doc.Offset(1) < lvLiczbaGodzin.Groups(Doc.Offset(0)).Items.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Exit Sub
      Else
        Doc.Offset(1) = 0
      End If
      Doc.Offset(0) += 1
      y += LineHeight
    Loop
    If Doc.Offset(0) < lvLiczbaGodzin.Groups.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Doc.Offset(0) = 0
    End If
  End Sub
  
End Class