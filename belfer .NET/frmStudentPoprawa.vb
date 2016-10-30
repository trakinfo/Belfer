Imports belfer.NET.GlobalValues
Imports System.Drawing.Printing
Public Class frmStudentPoprawa
  Private DT As DataTable
  Private InRefresh As Boolean = True
  Public Filter As String = "Przedmiot"
  Public Event NewRow()
  Private Offset(1), PageNumber As Integer
  Private PH As PrintHelper, IsPreview As Boolean
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.StudentMakeupAllowedToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.StudentMakeupAllowedToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmStudentPoprawa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    InRefresh = True
    rbPrzedmiot.Tag = New MakeupFilter With {.FilterName = "Przedmiot", .FilterText = "Wybrany przedmiot", .ListViewHeaderText = New String() {"Nazwisko i imię ucznia { klasa }", "Nazwisko i imię nauczyciela prowadzącego"}}
    rbStudent.Tag = New MakeupFilter With {.FilterName = "Student", .FilterText = "Wybrany uczeń", .ListViewHeaderText = New String() {"Nazwa przedmiotu", "Nazwisko i imię nauczyciela prowadzącego"}}
    ListViewConfig(lvPoprawa)
    'SetListViewColumn(CType(rbPrzedmiot.Tag, PoprawaFilter).ListViewHeaderText)
    Dim CH As New CalcHelper, CurrentDate As Date
    CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
    'InRefresh = True
    Dim Semester2 As Date = CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer))
    Me.nudSemestr.Value = CType(IIf(CurrentDate < Semester2, 1, 2), Integer)
    InRefresh = False
    ApplyNewConfig()
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
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
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub SetListViewColumn(HeaderText() As String)
    With lvPoprawa
      .Columns.Clear()
      '.Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add(HeaderText(0), 376, HorizontalAlignment.Left)
      '.Columns.Add("Klasa", 200, HorizontalAlignment.Center)
      .Columns.Add(HeaderText(1), 283, HorizontalAlignment.Left)
    End With

  End Sub
  Private Sub ApplyNewConfig()
    lblRecord.Text = ""
    'lvPoprawa.Items.Clear()
    Dim B As RadioButton
    B = Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    rbPrzedmiot_CheckedChanged(B, Nothing)
    'GetData()
  End Sub

  Private Sub rbPrzedmiot_CheckedChanged(sender As Object, e As EventArgs) Handles rbPrzedmiot.CheckedChanged, rbStudent.CheckedChanged
    If Not Created Or CType(sender, RadioButton).Checked = False Then Exit Sub
    'Dim B As RadioButton
    'B = CType(sender, RadioButton) 'Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    Dim MF As MakeupFilter = CType(CType(sender, RadioButton).Tag, MakeupFilter)
    chkZakres.Text = MF.FilterText 'CType(B.Tag, MakeupFilter).FilterText
    Filter = MF.FilterName 'CType(B.Tag, MakeupFilter).FilterName
    SetListViewColumn(MF.ListViewHeaderText) 'CType(B.Tag, MakeupFilter).ListViewHeaderText)
    FillFilterBox(cbFilter)
    nudSemestr_ValueChanged(Nothing, Nothing)
  End Sub

  Private Sub chkZakres_CheckedChanged(sender As Object, e As EventArgs) Handles chkZakres.CheckedChanged
    If Not Created Then Exit Sub
    If chkZakres.Checked Then
      lvPoprawa.Items.Clear()
      lvPoprawa.Enabled = False
      ClearDetails()
      cbFilter.Enabled = CType(IIf(cbFilter.Items.Count > 0, True, False), Boolean)
    Else
      cbFilter.SelectedItem = Nothing
      cbFilter.Enabled = False
      GetData()
    End If
  End Sub
  Private Sub FillFilterBox(ByVal cb As ComboBox)
    Try
      With cb
        .Items.Clear()
        Dim SelectString As String, P As New PoprawkaSQL
        If Filter = "Przedmiot" Then
          SelectString = P.SelectPrzedmiot(My.Settings.IdSchool, My.Settings.SchoolYear, If(nudSemestr.Value = 1, "S", "R"))
        Else
          SelectString = P.SelectStudent(My.Settings.IdSchool, My.Settings.SchoolYear, If(nudSemestr.Value = 1, "S", "R"))
        End If
        'LoadObjectStaffItems(cbFilter, SelectString)
        Dim FCB As New FillComboBox
        FCB.AddComboBoxComplexItems(cb, SelectString)
        'chkZakres_CheckedChanged(Nothing, Nothing)
        cbFilter.Enabled = CType(IIf(chkZakres.Checked AndAlso cbFilter.Items.Count > 0, True, False), Boolean)

      End With

    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try

  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub

  Private Sub nudSemestr_ValueChanged(sender As Object, e As EventArgs) Handles nudSemestr.ValueChanged
    If Not Created OrElse InRefresh Then Exit Sub
    'ApplyNewConfig()
    FillFilterBox(cbFilter)
    FetchData()
    GetData()
  End Sub
  Private Sub FetchData()
    Dim DBA As New DataBaseAction, P As New PoprawkaSQL, SelectString As String
    SelectString = If(Filter = "Przedmiot", P.SelectPoprawkaByObject(My.Settings.IdSchool, My.Settings.SchoolYear, If(nudSemestr.Value = 1, "S", "R")), P.SelectPoprawkaByStudent(My.Settings.IdSchool, My.Settings.SchoolYear, If(nudSemestr.Value = 1, "S", "R")))
    DT = DBA.GetDataTable(SelectString)
    DT.TableName = Filter
  End Sub
  Private Sub GetData()
    Try
      With lvPoprawa
        .Items.Clear()
        .Groups.Clear()
        cmdDelete.Enabled = False
        cmdPrint.Enabled = False
        If chkZakres.Checked = False Then
          For Each R As DataRow In DT.DefaultView.ToTable(True, "FilterName", "GroupName").Rows
            Dim FRows() As DataRow = DT.Select("FilterName=" & R.Item("FilterName").ToString)
            LvNewItem(lvPoprawa, FRows, R.Item("GroupName").ToString)
          Next
        Else
          If cbFilter.SelectedItem IsNot Nothing Then
            Dim FRows() As DataRow = DT.Select("FilterName=" & CType(cbFilter.SelectedItem, CbItem).ID)
            LvNewItem(lvPoprawa, FRows, CType(cbFilter.SelectedItem, CbItem).ToString)
          End If
        End If
        .Columns(0).Width = CInt(IIf(.Items.Count > 12, 357, 376))
        .Enabled = CType(IIf(.Items.Count > 0, True, False), Boolean)
      End With

      ClearDetails()
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Overloads Sub LvNewItem(ByVal LV As ListView, DR() As DataRow, NazwaGrupy As String)
    Dim NG As New ListViewGroup(NazwaGrupy.ToUpper, HorizontalAlignment.Center)
    LV.Groups.Add(NG)
    For Each Row As DataRow In DR
      Dim NewItem As New ListViewItem(Row.Item(0).ToString, NG)
      NewItem.UseItemStyleForSubItems = True
      'NewItem.SubItems.Add(Row.Item("Student").ToString)
      NewItem.SubItems.Add(Row.Item(1).ToString)
      NewItem.Tag = New MakeupDetails With {.AllocationID = CType(Row.Item("IdPrzydzial"), Integer), .StaffID = CType(Row.Item("IdObsada"), Integer), .Type = CType(Row.Item("Typ"), Char), .IP = Row.Item("ComputerIP").ToString, .Owner = Row.Item("Owner").ToString.ToLower.Trim, .User = Row.Item("User").ToString.ToLower.Trim, .Version = Row.Item("Version").ToString}
      LV.Items.Add(NewItem)
    Next
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvPoprawa.Items.Count
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(StudentDetails As MakeupDetails)
    Try
      lblRecord.Text = lvPoprawa.SelectedItems(0).Index + 1 & " z " & lvPoprawa.Items.Count
      If Users.ContainsKey(StudentDetails.User) AndAlso Users.ContainsKey(StudentDetails.Owner) Then
        lblUser.Text = String.Concat(Users.Item(StudentDetails.User).ToString, " (Wł: ", Users.Item(StudentDetails.Owner).ToString, ")")
      Else
        Me.lblUser.Text = StudentDetails.User & " (Wł: " & StudentDetails.Owner & ")"
      End If
      lblIP.Text = StudentDetails.IP
      lblData.Text = StudentDetails.Version
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub cbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
    GetData()
  End Sub

  Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
    Dim dlgAddNew As New dlgStudentPoprawka
    Try
      With dlgAddNew
        .Typ = If(nudSemestr.Value = 1, "S", "R")
        Dim FCB As New FillComboBox, O As New ObsadaSQL
        .cbKlasa.Items.Clear()
        FCB.AddComboBoxComplexItems(.cbKlasa, O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, "0"))
        .cbKlasa.Enabled = CType(IIf(.cbKlasa.Items.Count > 0, True, False), Boolean)
        AddHandler .NewAdded, AddressOf NewItemAdded
        .ShowDialog()
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub NewItemAdded()
    FetchData()
    GetData()
    'Dim SH As New SeekHelper
    'SH.FindListViewItem(lvTemat, InsertedID)
  End Sub
  Private Sub lvPoprawa_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvPoprawa.ItemSelectionChanged
    If e.IsSelected Then
      GetDetails(CType(e.Item.Tag, MakeupDetails))
      If AppUser.Role = Role.Administrator OrElse AppUser.Login = CType(e.Item.Tag, MakeupDetails).Owner Then cmdDelete.Enabled = True
      cmdPrint.Enabled = True
    Else
      ClearDetails()
      cmdDelete.Enabled = False
      cmdPrint.Enabled = False
    End If
  End Sub

  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, P As New PoprawkaSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvPoprawa.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.DeletePoprawka)
          cmd.Parameters.AddWithValue("?IdPrzydzial", CType(Item.Tag, MakeupDetails).AllocationID)
          cmd.Parameters.AddWithValue("?IdObsada", CType(Item.Tag, MakeupDetails).StaffID)
          cmd.Parameters.AddWithValue("?Typ", CType(Item.Tag, MakeupDetails).Type.ToString)
          cmd.Transaction = MySQLTrans
          cmd.ExecuteNonQuery()
        Next
        MySQLTrans.Commit()
        cmdDelete.Enabled = False
        'FetchData()
        'GetData()
        nudSemestr_ValueChanged(Nothing, Nothing)
        ClearDetails()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvPoprawa, DeletedIndex)

      Catch mex As MySqlException
        MessageBox.Show(mex.Message)
        MySQLTrans.Rollback()
      Catch ex As Exception
        MessageBox.Show(ex.Message)
      End Try
    End If
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

    PH.G = e.Graphics
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
      PH.DrawText("Uczniowie dopuszczeni do egzaminu poprawkowego", HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      'PH.DrawLine(x * 2, y + HeaderLineHeight, PrintWidth, y + HeaderLineHeight)
      y += LineHeight * 2
      PH.DrawText(cbFilter.Text, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, HeaderLineHeight, 0, Brushes.Black, False)
      y += LineHeight * CSng(1.5)
    End If
    'Dim DateColSize As Integer = 100 'CInt((e.MarginBounds.Width - TotalColSize) / 12)
    Dim PrintColSize As Integer = CInt(PrintWidth / 2)

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Przedmiot", .Label = lvPoprawa.Columns(0).Text, .Size = PrintColSize},
        New Pole With {.Name = "Nauczyciel", .Label = lvPoprawa.Columns(1).Text, .Size = PrintColSize}
    }

    Dim ColSize As Integer = 0
    For Each Col In Kolumna
      With Col
        PH.DrawText(.Label, New Font(TextFont, FontStyle.Bold), x + ColSize, y, .Size, LineHeight * 2, 1, Brushes.Black)
        ColSize += .Size
      End With
    Next
    y += LineHeight * 2
    Do Until (y + LineHeight * CSng(1)) > e.MarginBounds.Bottom Or Offset(0) > lvPoprawa.Items.Count - 1
      ColSize = 0
      Dim i As Integer = 0
      For Each Col In Kolumna
        PH.DrawText(lvPoprawa.Items(Offset(0)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, CType(If(Col.Name = "Przedmiot", 0, 1), Byte), Brushes.Black)
        ColSize += Col.Size
        i += 1
      Next
      y += LineHeight
      Offset(0) += 1
    Loop
    If Offset(0) < lvPoprawa.Items.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
    End If
  End Sub
  Public Sub PrnDoc_AllPrzedmiot_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)
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
      PH.DrawText("Uczniowie dopuszczeni do egzaminu poprawkowego", HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      y += LineHeight * 2
    End If

    Dim PrintColSize As Integer = CInt(PrintWidth / 2)

    Dim Kolumna As New List(Of Pole) From
    {
        New Pole With {.Name = "Przedmiot", .Label = lvPoprawa.Columns(0).Text, .Size = PrintColSize},
        New Pole With {.Name = "Nauczyciel", .Label = lvPoprawa.Columns(1).Text, .Size = PrintColSize}
    }

    Do Until (y + LineHeight * CSng(1.5)) > PrintHeight Or Offset(0) > lvPoprawa.Groups.Count - 1
      If lvPoprawa.Groups(Offset(0)).Items.Count > 0 Then
        PH.DrawText(lvPoprawa.Groups(Offset(0)).Header, New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight * 2, 0, Brushes.Black, False)
        y += LineHeight * CSng(2)
        Dim ColOffset As Integer = 0
        For Each Col In Kolumna
          PH.DrawText(Col.Label, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, Col.Size, LineHeight * CSng(2), 1, Brushes.Black)
          ColOffset += Col.Size
        Next
        y += LineHeight * CSng(2)
        Dim ColSize As Integer
        Do Until (y + LineHeight * CSng(3)) > PrintHeight Or Offset(1) > lvPoprawa.Groups(Offset(0)).Items.Count - 1
          ColSize = 0
          Dim i As Integer = 0
          For Each Col In Kolumna
            PH.DrawText(lvPoprawa.Groups(Offset(0)).Items(Offset(1)).SubItems(i).Text, TextFont, x + ColSize, y, Col.Size, LineHeight, CType(If(Col.Name = "Przedmiot", 0, 1), Byte), Brushes.Black)
            ColSize += Col.Size
            i += 1
          Next
          y += LineHeight
          Offset(1) += 1
        Loop
        If Offset(1) < lvPoprawa.Groups(Offset(0)).Items.Count Then
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
    If Offset(0) < lvPoprawa.Groups.Count Then
      e.HasMorePages = True
      RaiseEvent NewRow()
    Else
      Offset(0) = 0
      PageNumber = 0
    End If
  End Sub

End Class
Public Class MakeupFilter
  Public Property FilterName As String
  Public Property FilterText As String
  Public Property ListViewHeaderText As String()
End Class

Public Class MakeupDetails
  Public Property AllocationID As Integer
  Public Property StaffID As Integer
  Public Property Type As Char
  Public Property Owner As String
  Public Property User As String
  Public Property IP As String
  Public Property Version As String
  Public Overrides Function ToString() As String
    Return String.Concat(Users.Item(User).ToString, " (Wł: ", Users.Item(Owner).ToString, ")")
  End Function
End Class