Imports System.Drawing.Printing
Public Class frmNauczanieIndywidualne
  Private DT As DataTable
  Public Event NewRow()
  Public Event PreviewModeChange()
  Private Offset(1), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.NauczanieIndywidualneToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.NauczanieIndywidualneToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  'Private IdPrzedmiot As Integer

  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvPrzedmiot)
    ApplyNewConfig()
  End Sub
  Private Sub ListViewConfig(LV As ListView)
    With LV
      .View = View.Details
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = False
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Columns.Add("Przedmiot {nauczyciel}", 344, HorizontalAlignment.Left)
      .Columns.Add("Data początkowa", 100, HorizontalAlignment.Center)
      .Columns.Add("Data końcowa", 100, HorizontalAlignment.Center)
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub ApplyNewConfig()
    FillKlasa()
    FetchData()
    chkZakres_CheckedChanged(Nothing, Nothing)
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, NI As New NauczanieIndywidualneSQL, CH As New CalcHelper
    DT = DBA.GetDataTable(NI.SelectPrzedmiot(CH.EndDateOfSchoolYear(My.Settings.SchoolYear)))
  End Sub
  Private Sub FillKlasa()
    cbStudent.Items.Clear()
    Dim FCB As New FillComboBox, NI As New NauczanieIndywidualneSQL
    FCB.AddComboBoxComplexItems(cbStudent, NI.SelectStudent(My.Settings.SchoolYear))
  End Sub

  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStudent.SelectedIndexChanged
    lvPrzedmiot.Items.Clear()
    lvPrzedmiot.Enabled = False
    GetData(lvPrzedmiot)
  End Sub
  Private Sub GetData(lv As ListView)
    Try
      lv.Items.Clear()
      lv.Groups.Clear()

      If chkZakres.Checked Then
        If cbStudent.SelectedItem IsNot Nothing Then LvNewItem(lv, CType(cbStudent.SelectedItem, CbItem).ID.ToString)
      Else
        For Each Item As CbItem In cbStudent.Items
          LvNewItem(lv, Item.ID.ToString, Item.Nazwa)
        Next
      End If
      lv.Columns(0).Width = CInt(IIf(lv.Items.Count > 21, 325, 344))
      If lv.Items.Count > 0 Then
        lv.Enabled = True
        cmdPrint.Enabled = True
      Else
        lv.Enabled = False
        cmdPrint.Enabled = False
      End If
    Catch err As Exception
      MessageBox.Show(err.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
    End Try
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView, Przydzial As String)
    For Each row As DataRow In DT.Select("IdPrzydzial=" & Przydzial)
      Dim NewItem As New ListViewItem(row.Item("Przedmiot").ToString)
      NewItem.UseItemStyleForSubItems = True
      NewItem.SubItems.Add(CType(row.Item("DataAktywacji"), Date).ToShortDateString)
      NewItem.SubItems.Add(If(IsDBNull(row.Item("DataDeaktywacji")), "", CType(row.Item("DataDeaktywacji"), Date).ToShortDateString))
      LV.Items.Add(NewItem)
    Next
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView, ByVal Przydzial As String, ByVal Grupa As String)

    Try
      Dim NG As New ListViewGroup(Grupa, Grupa)
      NG.HeaderAlignment = HorizontalAlignment.Left
      LV.Groups.Add(NG)
      For Each Row As DataRow In DT.Select("IdPrzydzial=" & Przydzial)
        Dim NewItem As New ListViewItem(Row.Item("Przedmiot").ToString, NG)
        NewItem.UseItemStyleForSubItems = True
        NewItem.SubItems.Add(CType(Row.Item("DataAktywacji"), Date).ToShortDateString)
        NewItem.SubItems.Add(If(IsDBNull(Row.Item("DataDeaktywacji")), "", CType(Row.Item("DataDeaktywacji"), Date).ToShortDateString))
        LV.Items.Add(NewItem)
      Next
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
    Finally
    End Try
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub chkZakres_CheckedChanged(sender As Object, e As EventArgs) Handles chkZakres.CheckedChanged
    If Not chkZakres.Created Then Exit Sub
    cbStudent.Enabled = CType(IIf(chkZakres.Checked AndAlso cbStudent.Items.Count > 0, True, False), Boolean) 'True
    cbKlasa_SelectedIndexChanged(Nothing, Nothing)
  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Offset(0) = 0
    Offset(1) = 0
    PageNumber = 0
    Dim PP As New dlgPrintPreview ', DSP As New DataSet
    PP.Doc = New PrintReport(Nothing)
    PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
    PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
    PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
    PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin

    RemoveHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
    RemoveHandler NewRow, AddressOf PP.NewRow
    RemoveHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    If chkZakres.Checked Then
      RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_SelectedPrzedmiot_PrintPage
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_SelectedPrzedmiot_PrintPage
    Else
      RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllPrzedmiot_PrintPage
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_AllPrzedmiot_PrintPage
    End If
    AddHandler NewRow, AddressOf PP.NewRow
    AddHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
    AddHandler PP.PreviewModeChange, AddressOf PreviewModeChanged

    PP.Width = 1000
    PP.ShowDialog()
  End Sub
  Private Sub PreviewModeChanged(PreviewMode As Boolean)
    IsPreview = PreviewMode
  End Sub
  Private Sub PrnDoc_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
    PH = New PrintHelper()
    If e.PrintAction = PrintAction.PrintToPrinter Then
      IsPreview = False
    Else
      IsPreview = True
    End If
  End Sub
  Public Sub PrnDoc_SelectedPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) 'Handles Doc.PrintPage
    'Dim Doc As PrintReport = CType(sender, PrintReport)
    'Dim PH As New PrintHelper(e)
    PH.G = e.Graphics
    'PH.PS = e.PageSettings
    'PH.IsPreview = IsPreview
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left) 'Doc.DefaultPageSettings.Margins.Left
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top) 'Doc.DefaultPageSettings.Margins.Top
    'Dim PrnVars As New PrintVariables
    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    Dim PrintHeight As Integer = e.MarginBounds.Bottom

    '---------------------------------------- Nagłówek i stopka ----------------------------
    PH.DrawHeader(x, y, PrintWidth)
    PH.DrawFooter(x, PrintHeight, PrintWidth)
    PageNumber += 1
    PH.DrawPageNumber("- " & PageNumber.ToString & " -", x, y, PrintWidth)
    If PageNumber = 1 Then
      y += LineHeight
      PH.DrawText("Wykaz uczniów nauczanych indywidualnie", HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      'PH.DrawLine(x * 2, y + HeaderLineHeight, PrintWidth, y + HeaderLineHeight)
      y += LineHeight * 2
      PH.DrawText(cbStudent.Text, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      y += LineHeight * CSng(1.5)
    End If
    Dim DateColSize As Integer = 100 'CInt((e.MarginBounds.Width - TotalColSize) / 12)
    Dim ObjectColSize As Integer = CInt(PrintWidth - DateColSize * 2)

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Przedmiot", .Label = "Przedmiot {nauczyciel}", .Size = ObjectColSize},
        New Pole With {.Name = "StartDate", .Label = "Data początkowa", .Size = DateColSize},
        New Pole With {.Name = "EndDate", .Label = "Data końcowa", .Size = DateColSize}
    }

    Dim ColSize As Integer = 0
    For Each Col In Kolumna
      With Col
        PH.DrawText(.Label, New Font(TextFont, FontStyle.Bold), x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
        ColSize += .Size
      End With
    Next
    y += LineHeight * 2
    Do Until (y + LineHeight * CSng(1)) > e.MarginBounds.Bottom Or Offset(0) > lvPrzedmiot.Items.Count - 1
      ColSize = 0
      Dim i As Integer = 0
      For Each Col In Kolumna
        PH.DrawText(lvPrzedmiot.Items(Offset(0)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, CType(If(Col.Name = "Przedmiot", 0, 1), Byte), Brushes.Black)
        ColSize += Col.Size
        i += 1
      Next
      y += LineHeight
      Offset(0) += 1
    Loop
    If Offset(0) < lvPrzedmiot.Items.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
    End If
  End Sub
  Public Sub PrnDoc_AllPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    'Dim Doc As PrintReport = CType(sender, PrintReport)
    'Dim PH As New PrintHelper(e)
    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)
    'Dim x As Single = My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left
    'Dim y As Single = My.Settings.TopMargin - e.PageSettings.PrintableArea.Top  
    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    Dim PrintHeight As Integer = e.MarginBounds.Bottom

    '---------------------------------------- Nagłówek i stopka ----------------------------
    PH.DrawHeader(x, y, PrintWidth)
    PH.DrawFooter(x, PrintHeight, PrintWidth)
    PageNumber += 1
    PH.DrawPageNumber("- " & PageNumber.ToString & " -", x, y, PrintWidth)
    If PageNumber = 1 Then
      y += LineHeight
      PH.DrawText("Wykaz uczniów nauczanych indywidualnie", HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      'PH.DrawLine(x * 2, y + HeaderLineHeight, PrintWidth, y + HeaderLineHeight)
      y += LineHeight * 2
      'PH.DrawText(cbStudent.Text, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      'y += LineHeight * CSng(1.5)
    End If
    Dim DateColSize As Integer = 100 'CInt((e.MarginBounds.Width - TotalColSize) / 12)
    Dim ObjectColSize As Integer = CInt(PrintWidth - DateColSize * 2)

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Przedmiot", .Label = "Przedmiot {nauczyciel}", .Size = ObjectColSize},
        New Pole With {.Name = "StartDate", .Label = "Data początkowa", .Size = DateColSize},
        New Pole With {.Name = "EndDate", .Label = "Data końcowa", .Size = DateColSize}
    }

    Do Until (y + LineHeight * CSng(1.5)) > PrintHeight Or Offset(0) > lvPrzedmiot.Groups.Count - 1
      If lvPrzedmiot.Groups(Offset(0)).Items.Count > 0 Then
        PH.DrawText(lvPrzedmiot.Groups(Offset(0)).Name, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight * 2, 0, Brushes.Black, False)
        y += LineHeight * CSng(2)
        Dim ColOffset As Integer = 0
        For Each Col In Kolumna
          PH.DrawText(Col.Label, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, Col.Size, LineHeight * CSng(2), 1, Brushes.Black)
          ColOffset += Col.Size
        Next
        y += LineHeight * CSng(2)
        Dim ColSize As Integer
        Do Until (y + LineHeight * CSng(3)) > PrintHeight Or Offset(1) > lvPrzedmiot.Groups(Offset(0)).Items.Count - 1
          ColSize = 0
          Dim i As Integer = 0
          For Each Col In Kolumna
            PH.DrawText(lvPrzedmiot.Groups(Offset(0)).Items(Offset(1)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, CType(If(Col.Name = "Przedmiot", 0, 1), Byte), Brushes.Black)
            ColSize += Col.Size
            i += 1
          Next
          y += LineHeight
          Offset(1) += 1
        Loop
        If Offset(1) < lvPrzedmiot.Groups(Offset(0)).Items.Count Then
          e.HasMorePages = True
          RaiseEvent NewRow()
          Exit Sub
        Else
          Offset(1) = 0
        End If
        y += LineHeight
      End If
      Offset(0) += 1

    Loop
    If Offset(0) < lvPrzedmiot.Groups.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
      PageNumber = 0
    End If
  End Sub
End Class