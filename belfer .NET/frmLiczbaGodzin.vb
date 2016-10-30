Imports System.Drawing.Printing
Public Class frmLiczbaGodzin
  Private DS As DataSet
  Public Event NewRow()
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.LiczbaGodzinWgKlasToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.LiczbaGodzinWgKlasToolStripMenuItem.Enabled = True
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
      .Columns.Add("Miesiąc", 200, HorizontalAlignment.Left)
      .Columns.Add("Liczba godzin", 127, HorizontalAlignment.Center)
      .Columns.Add("MonthNumber", 0, HorizontalAlignment.Center)
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  'Private Sub AddListViewColumn(LV As BetterListView)
  '  LV.Columns.AddRange(New BetterListViewColumnHeader() {
  '                      New BetterListViewColumnHeader() With {
  '                        .AlignVertical = TextAlignmentVertical.Middle,
  '                        .AlignHorizontal = TextAlignmentHorizontal.Center,
  '                        .Text = "Miesiąc",
  '                        .Width = 300},
  '                      New BetterListViewColumnHeader() With {
  '                        .AlignVertical = TextAlignmentVertical.Middle,
  '                        .AlignHorizontal = TextAlignmentHorizontal.Center,
  '                        .Text = "Zrealizowana liczba godzin",
  '                        .Width = 251}
  '                    })
  'End Sub
  Private Sub ApplyNewConfig()
    FillKlasa()
  End Sub
  Private Sub FetchData(Klasa As String)
    Dim S As New StatystykaSQL, DBA As New DataBaseAction
    DS = DBA.GetDataSet(S.CountLekcja(Klasa, My.Settings.SchoolYear) & S.CountZastepstwo(Klasa, My.Settings.SchoolYear) & S.SelectMonth(Klasa, My.Settings.SchoolYear) & S.SelectRequiredNumberOfActivities(My.Settings.SchoolYear, Klasa))
    DS.Tables(0).TableName = "LiczbaLekcji"
    DS.Tables(1).TableName = "LiczbaZastepstw"
    DS.Tables(2).TableName = "Miesiac"
    DS.Tables(3).TableName = "WymaganaLiczbaLekcji"
  End Sub
  Private Sub FillKlasa()
    cbKlasa.Items.Clear()
    cbPrzedmiot.Enabled = False
    Dim FCB As New FillComboBox, K As New KolumnaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
    Dim SH As New SeekHelper
    If My.Settings.ClassName.Length > 0 Then SH.FindComboItem(Me.cbKlasa, CType(My.Settings.ClassName, Integer))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    lvLiczbaGodzin.Items.Clear()
    lvLiczbaGodzin.Enabled = False
    FetchData(CType(cbKlasa.SelectedItem, CbItem).ID.ToString)
    FillPrzedmiot()
    If Not chkPrzedmiot.Checked Then GetData(lvLiczbaGodzin)
  End Sub
  Private Sub FillPrzedmiot()
    cbPrzedmiot.Items.Clear()
    Dim FCB As New FillComboBox, S As New StatystykaSQL
    FCB.AddComboBoxExtendedItems(cbPrzedmiot, S.SelectPrzedmiotByKlasa(My.Settings.ClassName, My.Settings.SchoolYear))
    Dim SH As New SeekHelper
    If My.Settings.ObjectName.Length > 0 Then SH.FindComboItem(Me.cbPrzedmiot, CType(My.Settings.ObjectName, Integer))
    cbPrzedmiot.Enabled = CType(IIf(chkPrzedmiot.Checked AndAlso cbPrzedmiot.Items.Count > 0, True, False), Boolean)
    chkPrzedmiot.Enabled = CType(IIf(cbPrzedmiot.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbPrzedmiot_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectionChangeCommitted
    My.Settings.ObjectName = CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    GetData(lvLiczbaGodzin)
    'chkPrzedmiot_CheckedChanged(Nothing, Nothing)
  End Sub
  Private Sub GetData(lv As ListView)
    Try
      lv.Items.Clear()
      lv.Groups.Clear()
      If chkPrzedmiot.Checked Then
        If cbPrzedmiot.SelectedItem IsNot Nothing Then LvNewItem(lv, CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
        'If lv.Items.Count > 0 Then LvSumItem(lv, CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
      Else
        For Each Item As CbItem In cbPrzedmiot.Items
          LvNewItem(lv, Item.ID.ToString, Item.Nazwa)
        Next
      End If
      lv.Columns(1).Width = CInt(IIf(lv.Items.Count > 21, 108, 127))
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
  'ciągle brakuje pewności czy dobrze działa (zliczanie zastepstw i godzin w pewnych warunkach) wydruk dostosować do zmian
  Private Function AddZastepstwo(Miesiac As String, Przedmiot As String) As Integer
    If DS.Tables("LiczbaZastepstw").Select("Miesiac=" & Miesiac & " AND Przedmiot=" & Przedmiot).Length > 0 Then
      Return CType(DS.Tables("LiczbaZastepstw").Select("Miesiac=" & Miesiac & " AND Przedmiot=" & Przedmiot)(0).Item("LiczbaZastepstw"), Integer)
    Else
      Return 0
    End If
  End Function
  Private Overloads Sub LvNewItem(ByVal LV As ListView, Przedmiot As String)
    For Each row As DataRow In DS.Tables("Miesiac").Select()
      Dim NewItem As New ListViewItem(MonthName(CType(row.Item("Miesiac"), Integer)).ToUpper)
      Dim LiczbaGodzin As Integer = 0
      NewItem.UseItemStyleForSubItems = True
      If DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "' AND Miesiac=" & row.Item("Miesiac").ToString).Length > 0 Then LiczbaGodzin = CType(DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "' AND Miesiac=" & row.Item("Miesiac").ToString)(0).Item("LiczbaGodzin"), Integer)
      LiczbaGodzin += AddZastepstwo(row.Item("Miesiac").ToString, Przedmiot)
      NewItem.SubItems.Add(LiczbaGodzin.ToString)
      NewItem.SubItems.Add(row.Item("Miesiac").ToString)
      If LiczbaGodzin > 0 Then LV.Items.Add(NewItem)
      'End If

    Next
    'If DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "'").Length > 0 Then
    LvSumItem(LV, Przedmiot)
  End Sub
  'Private Overloads Sub LvNewItem(ByVal LV As ListView, Obsada As String)
  '  For Each row As DataRow In DS.Tables("LiczbaLekcji").Select("IdObsada='" & Obsada & "'")
  '    Dim NewItem As New ListViewItem(MonthName(CType(row.Item(0), Integer)))
  '    Dim LiczbaGodzin As Integer = 0
  '    NewItem.UseItemStyleForSubItems = True
  '    'Nie pokazuje miesiaca gdzie było zastepstwo a nie było planowej lekcji
  '    LiczbaGodzin = CType(row.Item(1), Integer)
  '    LiczbaGodzin += AddZastepstwo(row.Item("Miesiac").ToString, row.Item("Przedmiot").ToString)
  '    NewItem.SubItems.Add(LiczbaGodzin.ToString)
  '    LV.Items.Add(NewItem)
  '  Next
  '  If DS.Tables("LiczbaLekcji").Select("IdObsada='" & Obsada & "'").Length > 0 Then LvSumItem(LV, Obsada)
  'End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView, ByVal Przedmiot As String, ByVal Grupa As String)
    Dim NG As New ListViewGroup(Grupa, Grupa)
    NG.HeaderAlignment = HorizontalAlignment.Left
    LV.Groups.Add(NG)
    For Each row As DataRow In DS.Tables("Miesiac").Select()
      Dim LiczbaGodzin As Integer = 0
      'If DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "'").Length > 0 Then
      Dim NewItem As New ListViewItem(MonthName(CType(row.Item("Miesiac"), Integer)).ToUpper, NG)
      NewItem.UseItemStyleForSubItems = True
      If DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "' AND Miesiac=" & row.Item("Miesiac").ToString).Length > 0 Then LiczbaGodzin = CType(DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "' AND Miesiac=" & row.Item("Miesiac").ToString)(0).Item("LiczbaGodzin"), Integer)
      LiczbaGodzin += AddZastepstwo(row.Item("Miesiac").ToString, Przedmiot)
      NewItem.SubItems.Add(LiczbaGodzin.ToString)
      NewItem.SubItems.Add(row.Item("Miesiac").ToString)
      If LiczbaGodzin > 0 Then
        LV.Items.Add(NewItem)
      End If
      'End If
    Next
    LvSumItem(LV, Przedmiot, NG)
  End Sub
  'Private Overloads Sub LvNewItem(ByVal LV As ListView, ByVal Obsada As String, ByVal Grupa As String)
  '  Dim NG As New ListViewGroup(Grupa, Grupa)
  '  NG.HeaderAlignment = HorizontalAlignment.Left
  '  If DS.Tables("LiczbaLekcji").Select("IdObsada='" & Obsada & "'").Length > 0 Then
  '    LV.Groups.Add(NG)
  '    For Each row As DataRow In DS.Tables("LiczbaLekcji").Select("IdObsada='" & Obsada & "'")
  '      Dim NewItem As New ListViewItem(MonthName(CType(row.Item(0), Integer)), NG)
  '      NewItem.UseItemStyleForSubItems = True
  '      NewItem.SubItems.Add(row.Item(1).ToString)
  '      LV.Items.Add(NewItem)
  '    Next
  '    LvSumItem(LV, Obsada, NG)
  '  End If
  'End Sub
  Private Overloads Sub LvSumItem(ByVal LV As ListView, Przedmiot As String)
    Dim NewItem As New ListViewItem("Razem")
    Dim Suma As Integer = 0
    NewItem.UseItemStyleForSubItems = True
    NewItem.ForeColor = Color.Coral
    NewItem.Font = New Font(LV.Font, FontStyle.Bold)
    If DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "'").Length > 0 Then Suma = CType(DS.Tables("LiczbaLekcji").Compute("SUM(LiczbaGodzin)", "Przedmiot='" & Przedmiot & "'"), Integer)
    If DS.Tables("LiczbaZastepstw").Select("Przedmiot='" & Przedmiot & "'").Length > 0 Then Suma += CType(DS.Tables("LiczbaZastepstw").Compute("SUM(LiczbaZastepstw)", "Przedmiot='" & Przedmiot & "'"), Integer)
    NewItem.SubItems.Add(Suma.ToString)
    If Suma > 0 Then LV.Items.Add(NewItem)
  End Sub
  Private Overloads Sub LvSumItem(ByVal LV As ListView, Przedmiot As String, Grupa As ListViewGroup)
    Dim NewItem As New ListViewItem("Razem", Grupa)
    Dim Suma As Integer = 0
    NewItem.UseItemStyleForSubItems = True
    NewItem.ForeColor = Color.Coral
    NewItem.Font = New Font(LV.Font, FontStyle.Bold)
    If DS.Tables("LiczbaLekcji").Select("Przedmiot='" & Przedmiot & "'").Length > 0 Then Suma = CType(DS.Tables("LiczbaLekcji").Compute("SUM(LiczbaGodzin)", "Przedmiot='" & Przedmiot & "'"), Integer)
    If DS.Tables("LiczbaZastepstw").Select("Przedmiot='" & Przedmiot & "'").Length > 0 Then Suma += CType(DS.Tables("LiczbaZastepstw").Compute("SUM(LiczbaZastepstw)", "Przedmiot='" & Przedmiot & "'"), Integer)
    NewItem.SubItems.Add(Suma.ToString)
    If Suma > 0 Then LV.Items.Add(NewItem)
    'Next

  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub chkPrzedmiot_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrzedmiot.CheckedChanged
    If Not chkPrzedmiot.Created Then Exit Sub
    If chkPrzedmiot.Checked Then
      cbPrzedmiot.Enabled = CType(IIf(cbPrzedmiot.Items.Count > 0, True, False), Boolean) 'True
    End If
    GetData(lvLiczbaGodzin)
  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Dim PP As New dlgPrintPreview, DSP As New DataSet
    'DSP.Tables.Add()
    PP.Doc = New PrintReport(DSP)
    If chkPrzedmiot.Checked Then
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_SelectedPrzedmiot_PrintPage
    Else
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllPrzedmiot_PrintPage
    End If
    AddHandler NewRow, AddressOf PP.NewRow
    PP.Doc.ReportHeader = New String() {"Godzinowa realizacja planu nauczania - Klasa " & cbKlasa.Text}
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
      'e.Graphics.DrawLine(New Pen(Brushes.Black), x + CSng((e.PageBounds.Width - e.MarginBounds.Width) / 2), y + HeaderLineHeight, e.MarginBounds.Width, y + HeaderLineHeight)
      Doc.DrawLine(e, x * 2, y + HeaderLineHeight, e.MarginBounds.Width, y + HeaderLineHeight)

      y += LineHeight * 2
    End If
    Dim TotalColSize As Integer = 79
    Dim ColSize As Integer = CInt((e.MarginBounds.Width - TotalColSize) / 12)
    Dim TwoCols As New List(Of Pole) From
{
    New Pole With {.Name = "Przedmiot", .Label = "Przedmiot: " & cbPrzedmiot.Text.ToUpper, .Size = ColSize * 12},
    New Pole With {.Name = "MinNumber", .Label = "Min. liczba godz. do realizacji", .Size = TotalColSize}
}
    Dim ThreeCols As New List(Of Pole) From
{
    New Pole With {.Name = "S1", .Label = "Okres I", .Size = ColSize * 6},
    New Pole With {.Name = "S2", .Label = "Okres II", .Size = ColSize * 6},
    New Pole With {.Name = "MinNumber", .Label = "", .Size = TotalColSize}
}
    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "9", .Label = "IX", .Size = ColSize},
        New Pole With {.Name = "10", .Label = "X", .Size = ColSize},
        New Pole With {.Name = "11", .Label = "XI", .Size = ColSize},
        New Pole With {.Name = "12", .Label = "XII", .Size = ColSize},
        New Pole With {.Name = "1", .Label = "I", .Size = ColSize},
        New Pole With {.Name = "0", .Label = "Suma", .Size = ColSize},
        New Pole With {.Name = "2", .Label = "II", .Size = ColSize},
        New Pole With {.Name = "3", .Label = "III", .Size = ColSize},
        New Pole With {.Name = "4", .Label = "IV", .Size = ColSize},
        New Pole With {.Name = "5", .Label = "V", .Size = ColSize},
        New Pole With {.Name = "6", .Label = "VI", .Size = ColSize},
        New Pole With {.Name = "0", .Label = "Suma", .Size = ColSize},
        New Pole With {.Name = "-1", .Label = "Suma roczna", .Size = TotalColSize}
    }
    ColSize = 0
    'Dim TotalSize As Integer = Kolumna.Sum(Function(Kol) Kol.Size)
    'x += CType((e.MarginBounds.Width - Kolumna.Sum(Function(Kol) Kol.Size)) / 2, Single) '+ Doc.DefaultPageSettings.PrintableArea.Left
    '---------------------------------------------------- Nagłówki kolumn ------------------------------------
    'dokończyć()
    For Each Col In TwoCols
      With Col
        Doc.DrawText(e, .Label, PrnVars.BoldFont, x + ColSize, y, .Size, LineHeight * 3, 1, Brushes.Black)
        ColSize += .Size
      End With
    Next
    y += LineHeight * 3
    ColSize = 0
    For Each Col In ThreeCols
      With Col
        Doc.DrawText(e, .Label, PrnVars.BoldFont, x + ColSize, y, .Size, LineHeight * CSng(1.5), 1, Brushes.Black)
        ColSize += .Size
      End With
    Next
    Dim WLiczbaGodzin As Single = 0
    If DS.Tables("WymaganaLiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.SelectedItem, CbItem).Kod.ToString & "'").Length > 0 Then
      WLiczbaGodzin = CType(DS.Tables("WymaganaLiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.SelectedItem, CbItem).Kod.ToString & "'")(0).Item("Wartosc"), Single)
      Doc.DrawText(e, WLiczbaGodzin.ToString, PrnVars.BoldFont, x + ColSize - ThreeCols.Item(2).Size, y, ThreeCols.Item(2).Size, LineHeight * CSng(1.5), 1, Brushes.Black, False)
    End If

    y += LineHeight * CSng(1.5)
    ColSize = 0
    For Each Col In Kolumna
      With Col
        Doc.DrawText(e, .Label, PrnVars.BoldFont, x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
        ColSize += .Size
      End With
    Next
    y += LineHeight * 2
    ColSize = 0
    Dim LiczbaGodzin As Integer = 0, Suma As Integer = 0, SumaRoczna As Integer = 0
    For Each col In Kolumna
      With col
        If CInt(.Name) > 0 AndAlso DS.Tables("LiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString & "' AND Miesiac=" & .Name).Length > 0 Then LiczbaGodzin = CType(DS.Tables("LiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString & "' AND Miesiac=" & col.Name)(0).Item("LiczbaGodzin"), Integer)
        LiczbaGodzin += AddZastepstwo(col.Name, CType(cbPrzedmiot.SelectedItem, CbItem).ID.ToString)
        Suma += LiczbaGodzin
        'End If
        If .Name = "0" AndAlso Suma > 0 Then
          Doc.DrawText(e, Suma.ToString, New Font(TextFont, FontStyle.Bold), x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
          SumaRoczna += Suma
          Suma = 0
        ElseIf col.Name = "-1" AndAlso SumaRoczna > 0 Then
          Doc.DrawText(e, SumaRoczna.ToString, New Font(TextFont, FontStyle.Bold), x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
          SumaRoczna = 0
        Else
          Doc.DrawText(e, IIf(LiczbaGodzin > 0, LiczbaGodzin, "").ToString, TextFont, x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
        End If
        LiczbaGodzin = 0
        ColSize += .Size
      End With
    Next
  End Sub
  Public Sub PrnDoc_AllPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
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
      y += LineHeight * 2
      Doc.DrawText(e, Doc.ReportHeader(0), HeaderFont, x, y, e.MarginBounds.Width, HeaderLineHeight, 1, Brushes.Black, False)
      e.Graphics.DrawLine(New Pen(Brushes.Black), x + CSng((e.PageBounds.Width - e.MarginBounds.Width) / 2), y + HeaderLineHeight, e.MarginBounds.Width, y + HeaderLineHeight)
      y += LineHeight * 5
    End If
    Dim TotalColSize As Integer = 79
    Dim ColSize As Integer = CInt((e.MarginBounds.Width - TotalColSize) / 12)
    Dim TwoCols As New List(Of Pole) From
{
    New Pole With {.Name = "Przedmiot", .Label = "Przedmiot: ", .Size = ColSize * 12},
    New Pole With {.Name = "MinNumber", .Label = "Min. liczba godz. do realizacji", .Size = TotalColSize}
}
    Dim ThreeCols As New List(Of Pole) From
{
    New Pole With {.Name = "S1", .Label = "Okres I", .Size = ColSize * 6},
    New Pole With {.Name = "S2", .Label = "Okres II", .Size = ColSize * 6},
    New Pole With {.Name = "MinNumber", .Label = "", .Size = TotalColSize}
}
    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "9", .Label = "IX", .Size = ColSize},
        New Pole With {.Name = "10", .Label = "X", .Size = ColSize},
        New Pole With {.Name = "11", .Label = "XI", .Size = ColSize},
        New Pole With {.Name = "12", .Label = "XII", .Size = ColSize},
        New Pole With {.Name = "1", .Label = "I", .Size = ColSize},
        New Pole With {.Name = "0", .Label = "Suma", .Size = ColSize},
        New Pole With {.Name = "2", .Label = "II", .Size = ColSize},
        New Pole With {.Name = "3", .Label = "III", .Size = ColSize},
        New Pole With {.Name = "4", .Label = "IV", .Size = ColSize},
        New Pole With {.Name = "5", .Label = "V", .Size = ColSize},
        New Pole With {.Name = "6", .Label = "VI", .Size = ColSize},
        New Pole With {.Name = "0", .Label = "Suma", .Size = ColSize},
        New Pole With {.Name = "-1", .Label = "Suma roczna", .Size = TotalColSize}
    }
    ColSize = 0
    'Dim TotalSize As Integer = Kolumna.Sum(Function(Kol) Kol.Size)
    'x += CType((e.MarginBounds.Width - Kolumna.Sum(Function(Kol) Kol.Size)) / 2, Single) '+ Doc.DefaultPageSettings.PrintableArea.Left
    '---------------------------------------------------- Nagłówki kolumn ------------------------------------

    'For Each Item As CbItem In cbPrzedmiot.Items
    Do Until (y + LineHeight * CSng(6.5)) > e.MarginBounds.Bottom Or Doc.Offset(0) > cbPrzedmiot.Items.Count - 1

      Doc.DrawText(e, TwoCols.Item(0).Label & CType(cbPrzedmiot.Items(Doc.Offset(0)), CbItem).Nazwa.ToUpper, PrnVars.BoldFont, x + ColSize, y, TwoCols.Item(0).Size, LineHeight * 3, 1, Brushes.Black)
      ColSize += TwoCols.Item(0).Size
      Doc.DrawText(e, TwoCols.Item(1).Label, PrnVars.BoldFont, x + ColSize, y, TwoCols.Item(1).Size, LineHeight * 3, 1, Brushes.Black)
      y += LineHeight * 3
      ColSize = 0
      For Each Col In ThreeCols
        With Col
          Doc.DrawText(e, .Label, PrnVars.BoldFont, x + ColSize, y, .Size, LineHeight * CSng(1.5), 1, Brushes.Black)
          ColSize += .Size
        End With
      Next
      Dim WLiczbaGodzin As Single = 0
      If DS.Tables("WymaganaLiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.Items(Doc.Offset(0)), CbItem).Kod.ToString & "'").Length > 0 Then
        WLiczbaGodzin = CType(DS.Tables("WymaganaLiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.Items(Doc.Offset(0)), CbItem).Kod.ToString & "'")(0).Item("Wartosc"), Single)
        Doc.DrawText(e, WLiczbaGodzin.ToString, PrnVars.BoldFont, x + ColSize - ThreeCols.Item(2).Size, y, ThreeCols.Item(2).Size, LineHeight * CSng(1.5), 1, Brushes.Black, False)
      End If
      y += LineHeight * CSng(1.5)
      ColSize = 0
      For Each Col In Kolumna
        With Col
          Doc.DrawText(e, .Label, PrnVars.BoldFont, x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
          ColSize += .Size
        End With
      Next
      y += LineHeight * 2
      ColSize = 0
      Dim LiczbaGodzin As Integer = 0, Suma As Integer = 0, SumaRoczna As Integer = 0
      For Each col In Kolumna
        With col
          If CInt(.Name) > 0 AndAlso DS.Tables("LiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.Items(Doc.Offset(0)), CbItem).ID.ToString & "' AND Miesiac=" & .Name).Length > 0 Then LiczbaGodzin = CType(DS.Tables("LiczbaLekcji").Select("Przedmiot='" & CType(cbPrzedmiot.Items(Doc.Offset(0)), CbItem).ID.ToString & "' AND Miesiac=" & col.Name)(0).Item("LiczbaGodzin"), Integer)
          LiczbaGodzin += AddZastepstwo(col.Name, CType(cbPrzedmiot.Items(Doc.Offset(0)), CbItem).ID.ToString)
          Suma += LiczbaGodzin
          'End If
          If .Name = "0" AndAlso Suma > 0 Then
            Doc.DrawText(e, Suma.ToString, New Font(TextFont, FontStyle.Bold), x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
            SumaRoczna += Suma
            Suma = 0
          ElseIf col.Name = "-1" AndAlso SumaRoczna > 0 Then
            Doc.DrawText(e, SumaRoczna.ToString, New Font(TextFont, FontStyle.Bold), x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
            SumaRoczna = 0
          Else
            Doc.DrawText(e, IIf(LiczbaGodzin > 0, LiczbaGodzin, "").ToString, TextFont, x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
          End If
          LiczbaGodzin = 0
          ColSize += .Size
        End With
      Next
      y += LineHeight * 2
      ColSize = 0
      Doc.Offset(0) += 1
    Loop

    If Doc.Offset(0) <= cbPrzedmiot.Items.Count - 1 Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Doc.Offset(0) = 0
    End If
    'Next
  End Sub
End Class