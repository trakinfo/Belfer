Imports System.Drawing.Printing
Imports System.Net.Mail

Public Class frmKlasyfikacjaZbiorcza
  Public Event NewRow()
  Private Offset(1), PageNumber, ColFactor As Integer
  Private PH As PrintHelper, IsPreview As Boolean ', NewPage As Boolean = True
  Private CurrentDate As Date, DS As DataSet ', MaxPion As Byte
  'Private PasekParams, DyplomParams As OutstandingParams
  Private Pion As New Hashtable From {{1, "Klasy pierwsze"}, {2, "Klasy drugie"}, {3, "Klasy trzecie"}, {4, "Klasy czwarte"}, {5, "Klasy piąte"}, {6, "Klasy szóste"}, {7, "Klasy siódme"}, {8, "Klasy ósme"}, {9, "Klasy dziewiąte"}}
  Private TotalGS As ClasificationSummary, TotalBS(8) As Integer
  Private TableHeader As TableLayoutPanel

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ZestawienieKlasyfikacjiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ZestawienieKlasyfikacjiToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub FetchData(Okres As String)
    Dim DBA As New DataBaseAction, K As New KlasyfikacjaSQL, CH As New CalcHelper, S As New StatystykaSQL
    Dim StartDate, EndDate As Date
    StartDate = CH.StartDateOfSchoolYear(My.Settings.SchoolYear)
    EndDate = CType(IIf(Okres = "S", CH.StartDateOfSemester2(CType(My.Settings.SchoolYear.Substring(0, 4), Integer)).AddDays(-1), CH.EndDateOfSchoolYear(My.Settings.SchoolYear)), Date)
    DS = New DataSet
    Dim dt As DataTable
    dt = DBA.GetDataTable(K.CountStudentBySzkola(My.Settings.IdSchool, My.Settings.SchoolYear))
    dt.TableName = "CountStudentByKlasa"
    DS.Tables.Add(dt)
    dt = DBA.GetDataTable(K.CountStudentByNKL(My.Settings.IdSchool, My.Settings.SchoolYear, Okres))
    dt.TableName = "CountStudentByNkl"
    DS.Tables.Add(dt)
    dt = DBA.GetDataTable(K.SelectClasificationStatus(My.Settings.IdSchool, My.Settings.SchoolYear, Okres))
    dt.TableName = "StatusKlasyfikacjiByKlasa"
    DS.Tables.Add(dt)

    If rbPrzedmiot.Checked Then
      dt = DBA.GetDataTable(K.CountScoreByKlasaByStudentByWaga(My.Settings.IdSchool, My.Settings.SchoolYear, Okres))
      dt.TableName = "CountNdstByStudentByWaga"
      For Each Student As DataRow In DS.Tables("CountStudentByNkl").Rows
        For Each R As DataRow In dt.Select("IdUczen=" & Student.Item("IdUczen").ToString)
          R.Delete()
        Next
      Next
      dt.AcceptChanges()
      DS.Tables.Add(dt)
    Else
      dt = DBA.GetDataTable(K.CountZachowanieByKlasaByWaga(My.Settings.IdSchool, My.Settings.SchoolYear, Okres))
      dt.TableName = "CountZachowanie"
      DS.Tables.Add(dt)

    End If
  End Sub

  Private Sub frmKlasyfikacja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    lvKlasyfikacja.Height = 493
    ListViewConfig(lvKlasyfikacja)
    
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    CurrentDate = New Date(CType(If(Today.Month > 8, My.Settings.SchoolYear.Substring(0, 4), My.Settings.SchoolYear.Substring(5, 4)), Integer), Today.Month, Today.Day)
    'Dim OH As New OptionHolder
    'MaxPion = OH.GetMaxPion
    'MinPion = OH.GetMinPion
    EnableButton(False)
    lblRecord.Text = ""
    rbPrzedmiot.Checked = True
  End Sub

  Private Sub rbPrzedmiot_CheckedChanged(sender As Object, e As EventArgs) Handles rbPrzedmiot.CheckedChanged, rbZachowanie.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    Dim Okres As RadioButton
    Okres = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    If Okres Is Nothing Then
      Dim CH As New CalcHelper
      If CurrentDate < CH.StartDateOfSemester2(CH.StartDateOfSchoolYear(My.Settings.SchoolYear).Year) Then
        rbSemestr.Checked = True
      Else
        rbRokSzkolny.Checked = True
      End If
    Else
      rbSemestr_CheckedChanged(Okres, e)
    End If
    
  End Sub
  Function KR_Config() As List(Of ColumnHeader)
    Dim KlasyfikacjaCols As New List(Of ColumnHeader)
    With tlpKlasyfikacjaRoczna
      For i = 0 To .ColumnCount - 1
        Dim j As Integer
        Dim ColWidth As Integer

        If i < 2 OrElse i > 8 Then
          j = 0
          ColWidth = 53
        ElseIf (i > 1 AndAlso i < 5) OrElse i > 6 Then
          j = 1
          ColWidth = 103
        Else
          j = 2
          ColWidth = 103
        End If
        Dim ColName As String = .GetControlFromPosition(i, j).Name
        KlasyfikacjaCols.Add(New ColumnHeader With {.Name = ColName, .Text = ColName, .Width = ColWidth, .TextAlign = HorizontalAlignment.Center})
      Next
    End With
    Return KlasyfikacjaCols

  End Function
  Function KS_Config() As List(Of ColumnHeader)
    Dim KlasyfikacjaCols As New List(Of ColumnHeader)
    With tlpKlasyfikacja
      For i = 0 To .ColumnCount - 1
        Dim j As Integer
        Dim ColWidth As Integer
        If i < 2 OrElse i > 6 Then
          j = 0
          ColWidth = 53
        Else
          j = 1
          ColWidth = 143
        End If
        Dim ColName As String = .GetControlFromPosition(i, j).Name
        KlasyfikacjaCols.Add(New ColumnHeader With {.Name = ColName, .Text = ColName, .Width = ColWidth, .TextAlign = HorizontalAlignment.Center})
      Next
    End With
    Return KlasyfikacjaCols

  End Function
  Function Zachowanie_Config() As List(Of ColumnHeader)
    Dim KlasyfikacjaCols As New List(Of ColumnHeader)
    With tlpZachowanie
      For i = 0 To .ColumnCount - 1
        Dim j As Integer
        Dim ColWidth As Integer
        If i < 2 OrElse i > 8 Then
          j = 0
          ColWidth = 52
        ElseIf i = 3 Then
          j = 0
          ColWidth = 102
        Else
          j = 1
          ColWidth = 102
        End If
        Dim ColName As String = .GetControlFromPosition(i, j).Name
        KlasyfikacjaCols.Add(New ColumnHeader With {.Name = ColName, .Text = ColName, .Width = ColWidth, .TextAlign = HorizontalAlignment.Center})
      Next
    End With
    Return KlasyfikacjaCols

  End Function
  Private Sub rbSemestr_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemestr.CheckedChanged, rbRokSzkolny.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    Dim OH As New OptionHolder
    'DataRP = OH.DataRP(CType(sender, RadioButton).Tag.ToString, CurrentDate)
    Cursor = Cursors.WaitCursor
    Dim Wait As New dlgWait
    Wait.lblInfo.Text = "Trwa pobieranie danych ..."
    Wait.Show()
    Application.DoEvents()
    FetchData(CType(sender, RadioButton).Tag.ToString)
    SetVisibility(False)
    Dim KlasyfikacjaCols As New List(Of ColumnHeader)
    If rbPrzedmiot.Checked Then
      If rbSemestr.Checked Then
        KlasyfikacjaCols = KS_Config()
        tlpKlasyfikacja.Location = tlpKlasyfikacjaRoczna.Location
        tlpKlasyfikacja.Visible = True
      Else
        KlasyfikacjaCols = KR_Config()
        tlpKlasyfikacjaRoczna.Visible = True
      End If
    Else
      KlasyfikacjaCols = Zachowanie_Config()
      tlpZachowanie.Location = tlpKlasyfikacjaRoczna.Location
      tlpZachowanie.Visible = True
    End If
    AddListViewColumn(lvKlasyfikacja, KlasyfikacjaCols)
    Dim Status As RadioButton
    Status = gbStatus.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    If Status Is Nothing Then
      rbAll.Checked = True
    Else
      rbStatus_CheckedChanged(Status, Nothing)
    End If
    Wait.Hide()
    Cursor = Cursors.Default
  End Sub
  Private Sub rbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rbRejected.CheckedChanged, rbSubmitted.CheckedChanged, rbAccepted.CheckedChanged, rbAll.CheckedChanged, rbMissing.CheckedChanged
    If Not Me.Created OrElse CType(sender, RadioButton).Checked = False Then Exit Sub
    GetData(lvKlasyfikacja, CType(sender, RadioButton).Tag.ToString)
  End Sub

  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Dispose(True)
  End Sub
  Private Sub lvKlasyfikacja_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvKlasyfikacja.ItemSelectionChanged
    If e.IsSelected AndAlso GlobalValues.AppUser.Role = GlobalValues.Role.Administrator Then
      Dim Status As GlobalValues.ReasonStatus
      Status = CType(e.Item.SubItems(e.Item.SubItems.Count - 1).Tag, GlobalValues.ReasonStatus)
      If Status = GlobalValues.ReasonStatus.Przekazane Then
        EnableButton(True)
      Else
        EnableButton(False)
      End If
      lblRecord.Text = e.Item.ListView.SelectedItems(0).Index + 1 & " z " & e.Item.ListView.Items.Count
    Else
      lblRecord.Text = "0 z " & e.Item.ListView.Items.Count
      EnableButton(False)
    End If
  End Sub

  Private Sub ListViewConfig(LV As ListView)
    With LV
      .View = View.Details
      .FullRowSelect = True
      .GridLines = False
      .ShowItemToolTips = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .OwnerDraw = False
      .HeaderStyle = ColumnHeaderStyle.None
      .Items.Clear()
      .Enabled = True
      '.Visible = False
    End With
  End Sub
  Private Sub AddListViewColumn(lv As ListView, Cols As List(Of ColumnHeader))
    With lv
      .Items.Clear()
      .Columns.Clear()
      For Each Col As ColumnHeader In Cols
        .Columns.Add(Col)
      Next
    End With
  End Sub

  Private Sub GetData(lv As ListView, Status As String)
    Try
      EnableButton(False)
      lv.BeginUpdate()
      lv.Items.Clear()
      lv.Groups.Clear()
      TotalGS = New ClasificationSummary
      For i As Integer = 0 To 8
        TotalBS(i) = 0
      Next
      Dim dtGrupa As DataTable = Nothing
      Dim Klasa As String = "", Filter As String = ""
      If Status = "0" Then
        For Each R As DataRow In DS.Tables("StatusKlasyfikacjiByKlasa").Rows
          Klasa += R.Item("Klasa").ToString & ","
        Next
        Filter = If(Klasa.Length > 0, "Klasa NOT IN (" & Klasa.TrimEnd(",".ToCharArray) & ")", "")
        dtGrupa = New DataView(DS.Tables("CountStudentByKlasa"), Filter, "Pion ASC", DataViewRowState.CurrentRows).ToTable(True, "Pion")
      Else
        For Each R As DataRow In DS.Tables("StatusKlasyfikacjiByKlasa").Select(Status)
          Klasa += R.Item("Klasa").ToString & ","
        Next
        Filter = If(Klasa.Length > 0, "Klasa IN (" & Klasa.TrimEnd(",".ToCharArray) & ")", "Klasa=0")
        dtGrupa = New DataView(DS.Tables("CountStudentByKlasa"), Filter, "Pion ASC", DataViewRowState.CurrentRows).ToTable(True, "Pion")
      End If
      If Filter.Length > 0 Then Filter = " AND " & Filter
      For Each G As DataRow In dtGrupa.Rows
        LvNewGroupItems(lv, Filter, G.Item("Pion").ToString, Pion(CType(G.Item("Pion"), Integer)).ToString, Status)
      Next
      If Status <> "0" Then
        Dim NG As New ListViewGroup("Podsumowanie", "Wszystkie klasy")
        NG.HeaderAlignment = HorizontalAlignment.Center
        NG.Tag = "Podsumowanie"
        lv.Groups.Add(NG)
        If rbPrzedmiot.Checked Then
          LvSumGroupItem(lv, NG, TotalGS, Color.Firebrick, Color.LightYellow)
        Else
          LvSumGroupItem(lv, NG, TotalBS, Color.Firebrick, Color.LightYellow)
        End If
      End If
      If lv.Items.Count > 0 Then
        lv.Enabled = True
        cmdPrint.Enabled = True
      Else
        cmdPrint.Enabled = False
        lv.Enabled = False
      End If
      lv.EndUpdate()
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub
  Private Sub LvNewGroupItems(ByVal LV As ListView, Filter As String, ByVal IdGrupa As String, ByVal Grupa As String, Status As String)
    Dim GS As New ClasificationSummary, BS(8) As Integer
    Dim NG As New ListViewGroup(Grupa, Grupa)
    NG.HeaderAlignment = HorizontalAlignment.Center
    LV.Groups.Add(NG)
    LV.Groups(Grupa).Tag = IdGrupa
    For Each Klasa As DataRow In DS.Tables("CountStudentByKlasa").Select("Pion=" & IdGrupa & Filter)
      Dim NewItem As New ListViewItem(Klasa.Item("Nazwa_Klasy").ToString, NG)
      NewItem.UseItemStyleForSubItems = False
      NewItem.Tag = Klasa.Item("Klasa")
      Dim StanKlasy As Integer = CType(Klasa.Item("StanKlasy"), Integer)
      NewItem.SubItems.Add(StanKlasy.ToString)
      NewItem.SubItems(NewItem.SubItems.Count - 1).Name = "StanKlasy"
      GS.LiczbaUczniow += StanKlasy

      Dim StatusColor As Color = Color.Black
      If Status <> "0" Then
        Dim NKL As Integer
        If DS.Tables("CountStudentByNkl").Select("Klasa='" & Klasa.Item("Klasa").ToString & "'").GetLength(0) > 0 Then
          NKL = CType(DS.Tables("CountStudentByNkl").Select("Klasa='" & Klasa.Item("Klasa").ToString & "'")(0).Item("LiczbaNKL"), Integer)
        Else
          NKL = 0
        End If
        GS.LiczbaUczniowNieklasyfikowanych += NKL
        Dim KL As Integer = CType(Klasa.Item("StanKlasy"), Integer) - NKL
        GS.LiczbaUczniowKlasyfikowanych += KL
        NewItem.SubItems.Add(KL.ToString)
        If rbPrzedmiot.Checked Then
          NewItem.SubItems.Add(NKL.ToString)

          Dim NoNdst As Integer = KL - CType(New DataView(DS.Tables("CountNdstByStudentByWaga"), "Klasa='" & Klasa.Item("Klasa").ToString & "'", "IdUczen ASC", DataViewRowState.CurrentRows).ToTable(True, "IdUczen").Compute("Count(IdUczen)", ""), Integer)
          GS.LiczbaUczniowBezOcenNiedostatecznych += NoNdst
          NewItem.SubItems.Add(NoNdst.ToString)
          If rbRokSzkolny.Checked Then
            Dim Ndst1 As Integer = 0
            For Each S As DataRow In DS.Tables("CountNdstByStudentByWaga").Select("Klasa='" & Klasa.Item("Klasa").ToString & "' And Waga=1 AND LO=1")
              If CType(DS.Tables("CountNdstByStudentByWaga").Compute("Count(IdUczen)", "Klasa='" & Klasa.Item("Klasa").ToString & "' And Waga=0 AND IdUczen=" & S.Item("IdUczen").ToString), Byte) = 0 Then Ndst1 += 1
            Next
            GS.LiczbaUczniow_JednaPoprawka += Ndst1
            GS.LiczbaUczniowNiepromowanych -= Ndst1
            NewItem.SubItems.Add(Ndst1.ToString)

            Dim Ndst2 As Integer = 0
            For Each S As DataRow In DS.Tables("CountNdstByStudentByWaga").Select("Klasa='" & Klasa.Item("Klasa").ToString & "' And Waga=1 AND LO=2")
              If CType(DS.Tables("CountNdstByStudentByWaga").Compute("Count(IdUczen)", "Klasa='" & Klasa.Item("Klasa").ToString & "' And Waga=0 AND IdUczen=" & S.Item("IdUczen").ToString), Byte) = 0 Then Ndst2 += 1
            Next
            GS.LiczbaUczniow_DwiePoprawki += Ndst2
            GS.LiczbaUczniowNiepromowanych -= Ndst2
            NewItem.SubItems.Add(Ndst2.ToString)

            GS.LiczbaUczniowPromowanych += NoNdst
            NewItem.SubItems.Add(NoNdst.ToString)
            'GS.LiczbaUczniowNiepromowanych += KL - NoNdst
            GS.LiczbaUczniowNiepromowanych += StanKlasy - NoNdst
            'NewItem.SubItems.Add((KL - NoNdst).ToString)
            NewItem.SubItems.Add((StanKlasy - NoNdst - Ndst1 - Ndst2).ToString)
          Else
            Dim Ndst1 As Integer = 0
            For Each S As DataRow In New DataView(DS.Tables("CountNdstByStudentByWaga"), "Klasa='" & Klasa.Item("Klasa").ToString & "'", "IdUczen ASC", DataViewRowState.CurrentRows).ToTable(True, "IdUczen").Rows  'DS.Tables("CountNdstByStudentByWaga").Select("Klasa='" & Klasa.Item("Klasa").ToString & "'")
              If CType(DS.Tables("CountNdstByStudentByWaga").Compute("Sum(LO)", "Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdUczen=" & S.Item("IdUczen").ToString), Byte) <= 2 Then Ndst1 += 1
            Next
            GS.LiczbaUczniow_JednaDwieOcenyNiedostateczne += Ndst1
            NewItem.SubItems.Add(Ndst1.ToString)
            'Ndst = 0
            Dim Ndst2 As Integer = 0
            For Each S As DataRow In New DataView(DS.Tables("CountNdstByStudentByWaga"), "Klasa='" & Klasa.Item("Klasa").ToString & "'", "IdUczen ASC", DataViewRowState.CurrentRows).ToTable(True, "IdUczen").Rows 'DS.Tables("CountNdstByStudentByWaga").Select("Klasa='" & Klasa.Item("Klasa").ToString & "'")
              If CType(DS.Tables("CountNdstByStudentByWaga").Compute("Sum(LO)", "Klasa='" & Klasa.Item("Klasa").ToString & "' AND IdUczen=" & S.Item("IdUczen").ToString), Byte) > 2 Then Ndst2 += 1
            Next
            GS.LiczbaUczniow_TrzyPlusOcenNiedostatecznych += Ndst2
            NewItem.SubItems.Add(Ndst2.ToString)
          End If
        Else
          For i As Integer = 6 To 1 Step -1
            Dim LO As Integer = 0
            If DS.Tables("CountZachowanie").Select("Klasa='" & Klasa.Item("Klasa").ToString & "' AND Waga=" & i).GetLength(0) > 0 Then LO = CType(DS.Tables("CountZachowanie").Select("Klasa='" & Klasa.Item("Klasa").ToString & "' AND Waga=" & i)(0).Item("LO"), Integer)
            NewItem.SubItems.Add(LO.ToString)
            BS(i) += LO
          Next
          BS(7) += StanKlasy
          BS(8) += KL
        End If
        Dim ClasificationStatus As GlobalValues.ReasonStatus = CType(DS.Tables("StatusKlasyfikacjiByKlasa").Select("Klasa='" & Klasa.Item("Klasa").ToString & "'")(0).Item("Status"), GlobalValues.ReasonStatus)
        If ClasificationStatus = GlobalValues.ReasonStatus.Odrzucone Then
          StatusColor = Color.Red
          NewItem.SubItems.Add(ChrW(&HB4)).Font = New Font("Symbol", 11, FontStyle.Bold)
        ElseIf ClasificationStatus = GlobalValues.ReasonStatus.Zatwierdzone Then
          StatusColor = Color.Green
          NewItem.SubItems.Add(ChrW(&HFC)).Font = New Font("Wingdings", 10, FontStyle.Bold)
        Else
          StatusColor = Color.Black
          NewItem.SubItems.Add(ChrW(&H2A)).Font = New Font("Wingdings", 10, FontStyle.Bold)
        End If
        NewItem.SubItems(NewItem.SubItems.Count - 1).Tag = ClasificationStatus
        NewItem.SubItems(NewItem.SubItems.Count - 1).ForeColor = StatusColor
        NewItem.ToolTipText = ClasificationStatus.ToString
      Else
        For i = 2 To LV.Columns.Count - 2
          NewItem.SubItems.Add(Chr(151))
        Next
        NewItem.SubItems.Add(GlobalValues.ReasonStatus.Brak.ToString).Tag = GlobalValues.ReasonStatus.Brak
        NewItem.SubItems(NewItem.SubItems.Count - 1).Name = "Status"
        StatusColor = Color.Red
        NewItem.SubItems("Status").ForeColor = StatusColor
      End If

      LV.Items.Add(NewItem)
    Next
    If Status <> "0" Then
      If rbPrzedmiot.Checked Then
        With TotalGS
          .LiczbaUczniow += GS.LiczbaUczniow
          .LiczbaUczniow_DwiePoprawki += GS.LiczbaUczniow_DwiePoprawki
          .LiczbaUczniow_JednaDwieOcenyNiedostateczne += GS.LiczbaUczniow_JednaDwieOcenyNiedostateczne
          .LiczbaUczniow_JednaPoprawka += GS.LiczbaUczniow_JednaPoprawka
          .LiczbaUczniow_TrzyPlusOcenNiedostatecznych += GS.LiczbaUczniow_TrzyPlusOcenNiedostatecznych
          .LiczbaUczniowBezOcenNiedostatecznych += GS.LiczbaUczniowBezOcenNiedostatecznych
          .LiczbaUczniowKlasyfikowanych += GS.LiczbaUczniowKlasyfikowanych
          .LiczbaUczniowNieklasyfikowanych += GS.LiczbaUczniowNieklasyfikowanych
          .LiczbaUczniowNiepromowanych += GS.LiczbaUczniowNiepromowanych
          .LiczbaUczniowPromowanych += GS.LiczbaUczniowPromowanych
        End With
        LvSumGroupItem(LV, NG, GS, Color.Red, Color.AliceBlue)

      Else
        For i = 1 To 8
          TotalBS(i) += BS(i)
        Next
        LvSumGroupItem(LV, NG, BS, Color.Red, Color.AliceBlue)
      End If
    End If
    lblRecord.Text = "0 z " & LV.Items.Count
    'If LV.Groups(Grupa).Items.Count = 0 Then LV.Groups.Remove(NG)
  End Sub

  Private Sub LvSumGroupItem(LV As ListView, Grupa As ListViewGroup, Podsumowanie As ClasificationSummary, FontColor As Color, BgColor As Color)
    'Dim NG As New ListViewGroup(Grupa, "Podsumowanie - " & Grupa)
    'NG.HeaderAlignment = HorizontalAlignment.Left
    'NG.Tag = "Podsumowanie"
    'LV.Groups.Add(NG)
    'Dim NewItem As New ListViewItem("Razem", NG)

    Dim NewItem As New ListViewItem("Razem", Grupa)
    NewItem.Tag = "Podsumowanie"
    NewItem.UseItemStyleForSubItems = True
    NewItem.Font = New Font(Font, FontStyle.Bold)
    NewItem.BackColor = BgColor 'Color.AliceBlue
    NewItem.ForeColor = FontColor 'Color.Red
    NewItem.SubItems.Add(Podsumowanie.LiczbaUczniow.ToString)

    NewItem.SubItems.Add(Podsumowanie.LiczbaUczniowKlasyfikowanych.ToString)
    NewItem.SubItems.Add(Podsumowanie.LiczbaUczniowNieklasyfikowanych.ToString)

    NewItem.SubItems.Add(Podsumowanie.LiczbaUczniowBezOcenNiedostatecznych.ToString)
    If rbRokSzkolny.Checked Then
      NewItem.SubItems.Add(Podsumowanie.LiczbaUczniow_JednaPoprawka.ToString)
      NewItem.SubItems.Add(Podsumowanie.LiczbaUczniow_DwiePoprawki.ToString)
      NewItem.SubItems.Add(Podsumowanie.LiczbaUczniowPromowanych.ToString)
      NewItem.SubItems.Add(Podsumowanie.LiczbaUczniowNiepromowanych.ToString)
    Else
      NewItem.SubItems.Add(Podsumowanie.LiczbaUczniow_JednaDwieOcenyNiedostateczne.ToString)
      NewItem.SubItems.Add(Podsumowanie.LiczbaUczniow_TrzyPlusOcenNiedostatecznych.ToString)
    End If
    NewItem.SubItems.Add("")
    LV.Items.Add(NewItem)
    LvSumGroupItemByPercent(LV, Grupa, Podsumowanie, FontColor, BgColor)
  End Sub
  Private Sub LvSumGroupItem(LV As ListView, Grupa As ListViewGroup, Podsumowanie As Integer(), FontColor As Color, BgColor As Color)
    'Dim NG As New ListViewGroup(Grupa, "Podsumowanie - " & Grupa)
    'NG.HeaderAlignment = HorizontalAlignment.Left
    'NG.Tag = "Podsumowanie"
    'LV.Groups.Add(NG)
    Dim NewItem As New ListViewItem("Razem", Grupa)
    NewItem.Tag = "Podsumowanie"
    NewItem.UseItemStyleForSubItems = True
    NewItem.Font = New Font(Font, FontStyle.Bold)
    NewItem.BackColor = BgColor 'Color.AliceBlue
    NewItem.ForeColor = FontColor '.Red
    NewItem.SubItems.Add(Podsumowanie(7).ToString)

    NewItem.SubItems.Add(Podsumowanie(8).ToString)

    For i As Integer = 6 To 1 Step -1
      NewItem.SubItems.Add(Podsumowanie(i).ToString)
    Next
    NewItem.SubItems.Add("")
    LV.Items.Add(NewItem)
    LvSumGroupItemByPercent(LV, Grupa, Podsumowanie, FontColor, BgColor)

  End Sub
  Private Sub LvSumGroupItemByPercent(LV As ListView, Grupa As ListViewGroup, Podsumowanie As ClasificationSummary, FontColor As Color, BgColor As Color)
    Dim NewItem As New ListViewItem("%", Grupa)
    NewItem.Tag = "Podsumowanie"
    NewItem.UseItemStyleForSubItems = True
    NewItem.Font = New Font(Font, FontStyle.Bold)
    NewItem.BackColor = BgColor 'Color.AliceBlue
    NewItem.ForeColor = FontColor 'Color.Red
    NewItem.SubItems.Add(Chr(151))

    NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniowKlasyfikowanych / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
    NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniowNieklasyfikowanych / Podsumowanie.LiczbaUczniow * 100, 2).ToString)

    NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniowBezOcenNiedostatecznych / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
    If rbRokSzkolny.Checked Then
      NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniow_JednaPoprawka / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
      NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniow_DwiePoprawki / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
      NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniowPromowanych / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
      NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniowNiepromowanych / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
    Else
      NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniow_JednaDwieOcenyNiedostateczne / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
      NewItem.SubItems.Add(Math.Round(Podsumowanie.LiczbaUczniow_TrzyPlusOcenNiedostatecznych / Podsumowanie.LiczbaUczniow * 100, 2).ToString)
    End If

    NewItem.SubItems.Add("")
    LV.Items.Add(NewItem)
  End Sub
  Private Sub LvSumGroupItemByPercent(LV As ListView, Grupa As ListViewGroup, Podsumowanie As Integer(), FontColor As Color, BgColor As Color)
    Dim NewItem As New ListViewItem("%", Grupa)
    NewItem.Tag = "Podsumowanie"
    NewItem.UseItemStyleForSubItems = True
    NewItem.Font = New Font(Font, FontStyle.Bold)
    NewItem.BackColor = BgColor 'Color.AliceBlue
    NewItem.ForeColor = FontColor '.Red
    NewItem.SubItems.Add(Chr(151))

    NewItem.SubItems.Add(Math.Round(Podsumowanie(8) / Podsumowanie(7) * 100, 2).ToString)

    For i As Integer = 6 To 1 Step -1
      NewItem.SubItems.Add(Math.Round(Podsumowanie(i) / Podsumowanie(7) * 100, 2).ToString)
    Next
    NewItem.SubItems.Add("")
    LV.Items.Add(NewItem)

  End Sub
  Private Sub EnableButton(Value As Boolean)
    cmdAccept.Enabled = Value
    cmdReject.Enabled = Value
    'cmdPrint.Enabled = Value
    'cmdRefresh.Enabled = Value
  End Sub

  Private Sub SetVisibility(value As Boolean)
    tlpKlasyfikacja.Visible = value
    tlpZachowanie.Visible = value
    tlpKlasyfikacjaRoczna.Visible = value
  End Sub
  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    'Dim Semestr As RadioButton
    'Semestr = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    'rbSemestr_CheckedChanged(Semestr, Nothing)
    RefreshData()
  End Sub
 
  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    Try
      Dim PP As New dlgPrintPreview ', DSP As New DataSet
      PP.Doc = New PrintReport(Nothing)
      'My.Settings.Landscape = True
      PP.Doc.DefaultPageSettings.Landscape = My.Settings.Landscape
      PP.Doc.DefaultPageSettings.Margins.Left = My.Settings.LeftMargin
      PP.Doc.DefaultPageSettings.Margins.Top = My.Settings.TopMargin
      PP.Doc.DefaultPageSettings.Margins.Right = My.Settings.LeftMargin
      PP.Doc.DefaultPageSettings.Margins.Bottom = My.Settings.TopMargin
      RemoveHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
      AddHandler PP.PreviewModeChange, AddressOf PreviewModeChanged
      RemoveHandler NewRow, AddressOf PP.NewRow
      AddHandler NewRow, AddressOf PP.NewRow
      RemoveHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
      AddHandler PP.Doc.BeginPrint, AddressOf PrnDoc_BeginPrint
      RemoveHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage
      AddHandler PP.Doc.PrintPage, AddressOf PrnDoc_PrintPage
      If rbPrzedmiot.Checked Then
        If gbZakres.Controls.OfType(Of RadioButton).Where(Function(r) r.Checked = True).FirstOrDefault().Tag.ToString = "R" Then
          ColFactor = 7
          TableHeader = tlpKlasyfikacjaRoczna
          PP.Doc.ReportHeader = New String() {"Zestawienie wyników klasyfikacji rocznej"}
        Else
          PP.Doc.ReportHeader = New String() {"Zestawienie wyników klasyfikacji śródrocznej"}
          ColFactor = 5
          TableHeader = tlpKlasyfikacja
        End If
      Else
        PP.Doc.ReportHeader = New String() {"Zestawienie ocen zachowania"}
        ColFactor = 7
        TableHeader = tlpZachowanie

      End If

      PP.Width = 1000
      PP.ShowDialog()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub PrnDoc_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
    PH = New PrintHelper()
    If e.PrintAction = PrintAction.PrintToPrinter Then
      IsPreview = False
    Else
      IsPreview = True
    End If
  End Sub
  Private Sub PreviewModeChanged(PreviewMode As Boolean)
    IsPreview = PreviewMode
  End Sub
  Public Sub PrnDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    Dim Doc As PrintReport = CType(sender, PrintReport)
    PH.G = e.Graphics
    Dim x As Single = If(IsPreview, My.Settings.LeftMargin, My.Settings.LeftMargin - e.PageSettings.PrintableArea.Left)
    Dim y As Single = If(IsPreview, My.Settings.TopMargin, My.Settings.TopMargin - e.PageSettings.PrintableArea.Top)

    Dim TextFont As Font = My.Settings.TextFont 'PrnVars.BaseFont
    Dim HeaderFont As Font = My.Settings.HeaderFont 'PrnVars.HeaderFont
    Dim SubHeaderFont As Font = My.Settings.SubHeaderFont
    Dim SubHeaderLineHeight As Single = SubHeaderFont.GetHeight(e.Graphics)
    Dim LineHeight As Single = TextFont.GetHeight(e.Graphics)
    Dim HeaderLineHeight As Single = HeaderFont.GetHeight(e.Graphics)
    Dim PrintWidth As Integer = e.MarginBounds.Width
    Dim PrintHeight As Integer = e.MarginBounds.Bottom

    '---------------------------------------- Nagłówek i stopka ----------------------------
    PH.DrawHeader(x, y, PrintWidth)
    PH.DrawFooter(x, PrintHeight, PrintWidth)
    PageNumber += 1
    PH.DrawPageNumber("- " & PageNumber.ToString & " -", x, y, PrintWidth)
    'Dim ColFactor As Integer = 7
    If PageNumber = 1 Then
      y += LineHeight
      PH.DrawText(Doc.ReportHeader(0), HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, False)
      y += HeaderLineHeight * 2
    End If

    PrintHeader(TableHeader, ColFactor, x, y, LineHeight, PrintWidth, TextFont)
    If GetClasificationList(e, x, y, LineHeight, PrintWidth, PrintHeight, TextFont, ColFactor) = False Then Exit Sub

    PageNumber = 0
    Offset(0) = 0

  End Sub
 
  Private Sub PrintHeader(Tabela As TableLayoutPanel, ColFactor As Integer, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, TextFont As Font)
    Dim ColOffset As Integer = 0
    Dim TotalColSize As Integer = 60
    Dim DataColSize As Integer = CType((PrintWidth - TotalColSize * 2) / ColFactor, Integer)
    With Tabela
      ColOffset = 0
      PH.DrawText(CType(.GetControlFromPosition(0, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, TotalColSize, LineHeight * CSng(3), 1, Brushes.Black)
      ColOffset += TotalColSize
      PH.DrawText(CType(.GetControlFromPosition(1, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, TotalColSize, LineHeight * CSng(3), 1, Brushes.Black)
      ColOffset += TotalColSize
      Dim Start As Integer = 2
      If Tabela Is tlpZachowanie Then
        PH.DrawText(CType(.GetControlFromPosition(2, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize, LineHeight * CSng(3), 1, Brushes.Black)
        ColOffset += DataColSize
        PH.DrawText(CType(.GetControlFromPosition(3, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize * (ColFactor - 1), LineHeight, 1, Brushes.Black)
        Start = 3
      Else
        PH.DrawText(CType(.GetControlFromPosition(2, 0), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize * ColFactor, LineHeight, 1, Brushes.Black)
      End If
      y += LineHeight
      For i As Integer = Start To .ColumnCount - 2
        If .GetColumnSpan(CType(.GetControlFromPosition(i, 1), Label)) > 1 Then
          PH.DrawText(CType(.GetControlFromPosition(i, 1), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize * .GetColumnSpan(CType(.GetControlFromPosition(i, 1), Label)), LineHeight, 1, Brushes.Black)
          ColOffset += DataColSize * .GetColumnSpan(CType(.GetControlFromPosition(i, 1), Label))
          y += LineHeight
          ColOffset -= DataColSize * .GetColumnSpan(CType(.GetControlFromPosition(i, 1), Label))
          For j As Integer = i To i + .GetColumnSpan(CType(.GetControlFromPosition(i, 1), Label)) - 1
            PH.DrawText(CType(.GetControlFromPosition(j, 2), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize, LineHeight, 1, Brushes.Black)
            ColOffset += DataColSize
          Next
          y -= LineHeight
          i += 1
        Else
          PH.DrawText(CType(.GetControlFromPosition(i, 1), Label).Text, New Font(TextFont, FontStyle.Bold), x + ColOffset, y, DataColSize, LineHeight * 2, 1, Brushes.Black)
          ColOffset += DataColSize
        End If
      Next
    End With
    y += LineHeight * 2
  End Sub

  Private Function GetClasificationList(e As PrintPageEventArgs, ByRef x As Single, ByRef y As Single, LineHeight As Single, PrintWidth As Integer, PrintHeight As Integer, TextFont As Font, ColFactor As Integer) As Boolean
    Dim TotalColSize As Integer = 60
    Dim DataColSize As Integer = CType((PrintWidth - TotalColSize * 2) / ColFactor, Integer)
    Dim PrintFont As Font = TextFont
    Dim PrintLineHeight As Single = LineHeight

    With lvKlasyfikacja
      While Offset(0) < .Groups.Count - 1 AndAlso PrintHeight >= y + LineHeight * CSng(4.5)
        y += LineHeight * 2
        PH.DrawText(StrConv(.Groups(Offset(0)).Header, VbStrConv.Uppercase), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 1, Brushes.Black, False)
        y += LineHeight * CSng(1.5)
        While Offset(1) < .Groups(Offset(0)).Items.Count AndAlso PrintHeight >= y + LineHeight
          'For Each Item As ListViewItem In .Groups(Offset(0)).Items
          If .Groups(Offset(0)).Items(Offset(1)).Tag.ToString <> "Podsumowanie" Then
            PrintFont = TextFont
            PrintLineHeight = LineHeight
          Else
            PrintFont = New Font(TextFont, FontStyle.Bold)
            PrintLineHeight = LineHeight * CSng(1.5)
          End If
          Dim ColOffset As Integer = 0
          PH.DrawText(.Groups(Offset(0)).Items(Offset(1)).Text, PrintFont, x, y, TotalColSize, PrintLineHeight, 1, Brushes.Black)
          ColOffset += TotalColSize
          PH.DrawText(.Groups(Offset(0)).Items(Offset(1)).SubItems(1).Text, PrintFont, x + ColOffset, y, TotalColSize, PrintLineHeight, 1, Brushes.Black)
          ColOffset += TotalColSize
          For i As Integer = 2 To .Groups(Offset(0)).Items(Offset(1)).SubItems.Count - 2
            PH.DrawText(.Groups(Offset(0)).Items(Offset(1)).SubItems(i).Text, PrintFont, x + ColOffset, y, DataColSize, PrintLineHeight, 1, Brushes.Black)
            ColOffset += DataColSize
          Next
          y += PrintLineHeight
          Offset(1) += 1
          'Next
        End While

        If Offset(1) < .Groups(Offset(0)).Items.Count Then
          e.HasMorePages = True
          RaiseEvent NewRow()
          Return False
        Else
          Offset(0) += 1
          Offset(1) = 0
        End If
      End While
      If Offset(0) < .Groups.Count - 1 Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Return False
      End If
      While Offset(0) < .Groups.Count AndAlso PrintHeight >= y + LineHeight * CSng(4.5)
        y += LineHeight * 2
        PH.DrawText(StrConv(.Groups(Offset(0)).Header, VbStrConv.Uppercase), New Font(TextFont, FontStyle.Bold), x, y, PrintWidth, LineHeight, 1, Brushes.Black, False)
        y += LineHeight * CSng(1.5)
        For Each Item As ListViewItem In .Groups(Offset(0)).Items
          Dim ColOffset As Integer = 0
          PH.DrawText(Item.Text, PrintFont, x, y, TotalColSize, PrintLineHeight, 1, Brushes.Black)
          ColOffset += TotalColSize
          PH.DrawText(Item.SubItems(1).Text, PrintFont, x + ColOffset, y, TotalColSize, PrintLineHeight, 1, Brushes.Black)
          ColOffset += TotalColSize
          For i As Integer = 2 To Item.SubItems.Count - 2
            PH.DrawText(Item.SubItems(i).Text, PrintFont, x + ColOffset, y, DataColSize, PrintLineHeight, 1, Brushes.Black)
            ColOffset += DataColSize
          Next
          y += PrintLineHeight
        Next
        Offset(0) += 1
      End While

      If Offset(0) < .Groups.Count Then
        e.HasMorePages = True
        RaiseEvent NewRow()
        Return False
      End If
    End With
    Return True
  End Function

  Private Sub cmdReject_Click(sender As Object, e As EventArgs) Handles cmdReject.Click
    If MessageBox.Show("Odrzucenie wyników klasyfikacji zdejmie blokadę z ocen i pozwoli wprowadzić poprawki." & vbNewLine & "Czy na pewno chcesz odrzucić wyniki klasyfikacji dla wskazanej klasy?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Cursor = Cursors.WaitCursor
      Dim K As New KlasyfikacjaSQL, DBA As New DataBaseAction, cmd As MySqlCommand, T As MySqlTransaction = GlobalValues.gblConn.BeginTransaction, ObsadaToLock As String
      Try
        Dim Typ As RadioButton
        Typ = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
        Dim Zakres As String
        Zakres = If(Typ.Tag.ToString = "S", "'C1','S'", "'C2','R'")
        Dim dtObsada As DataTable = DBA.GetDataTable(K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, CurrentDate)).DefaultView.ToTable(True, "ID", "IdObsada")

        For Each Klasa As ListViewItem In lvKlasyfikacja.SelectedItems
          If Klasa.Tag.ToString <> "Podsumowanie" AndAlso CType(Klasa.SubItems(Klasa.SubItems.Count - 1).Tag, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Przekazane Then
            cmd = DBA.CreateCommand(K.SelectObsadaToLock(Klasa.Tag.ToString, My.Settings.SchoolYear))
            cmd.Transaction = T
            ObsadaToLock = cmd.ExecuteScalar().ToString

            cmd.CommandText = K.UpdateColumnLock("0", Zakres, ObsadaToLock)
            cmd.ExecuteNonQuery()

            cmd.CommandText = K.ChangeStatus()
            cmd.Parameters.AddWithValue("?Status", GlobalValues.ReasonStatus.Odrzucone)
            cmd.Parameters.AddWithValue("?Typ", Typ.Tag.ToString)
            cmd.Parameters.AddWithValue("?IdObsada", dtObsada.Select("ID=" & Klasa.Tag.ToString)(0).Item("IdObsada").ToString)
            cmd.ExecuteNonQuery()
          End If

        Next

        T.Commit()
        RefreshData()
      Catch mex As MySqlException
        T.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Catch ex As Exception
        T.Rollback()
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
      Cursor = Cursors.Default
    End If
  End Sub
  Private Sub RefreshData()
    Dim Semestr As RadioButton
    Semestr = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
    rbSemestr_CheckedChanged(Semestr, Nothing)
  End Sub

  Private Sub cmdAccept_Click(sender As Object, e As EventArgs) Handles cmdAccept.Click
    If MessageBox.Show("Po zatwierdzeniu wyników klasyfikacj nie będzie już możliwości cofnięcia operacji. Nastąpi też zmiana statusu uczniów promowanych, co umożliwi przydział na nowy rok szkolny." & vbNewLine & "Czy na pewno chcesz zatwierdzić wyniki klasyfikacji dla wskazanej klasy?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Cursor = Cursors.WaitCursor
      Dim K As New KlasyfikacjaSQL, DBA As New DataBaseAction, cmd As MySqlCommand, T As MySqlTransaction = GlobalValues.gblConn.BeginTransaction ', ObsadaToLock As String
      Try
        Dim Typ As RadioButton
        Typ = gbZakres.Controls.OfType(Of RadioButton).FirstOrDefault(Function(rb) rb.Checked = True)
        Dim dtObsada As DataTable = DBA.GetDataTable(K.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, CurrentDate)).DefaultView.ToTable(True, "ID", "IdObsada")

        For Each Klasa As ListViewItem In lvKlasyfikacja.SelectedItems
          If Klasa.Tag.ToString <> "Podsumowanie" AndAlso CType(Klasa.SubItems(Klasa.SubItems.Count - 1).Tag, GlobalValues.ReasonStatus) = GlobalValues.ReasonStatus.Przekazane Then
            cmd = DBA.CreateCommand(K.ChangeStatus())
            cmd.Transaction = T
            cmd.Parameters.AddWithValue("?Status", GlobalValues.ReasonStatus.Zatwierdzone)
            cmd.Parameters.AddWithValue("?Typ", Typ.Tag.ToString)
            cmd.Parameters.AddWithValue("?IdObsada", dtObsada.Select("ID=" & Klasa.Tag.ToString)(0).Item("IdObsada").ToString)
            cmd.ExecuteNonQuery()
            If Typ.Tag.ToString = "R" Then
              cmd.CommandText = K.UpdatePromotion(Klasa.Tag.ToString, My.Settings.SchoolYear, Typ.Tag.ToString)
              cmd.ExecuteNonQuery()
              Dim dtPoprawka As DataTable = DBA.GetDataTable(K.SelectStudentByNdstByObsada(Klasa.Tag.ToString, My.Settings.SchoolYear, Typ.Tag.ToString))
              For Each S As DataRow In dtPoprawka.DefaultView.ToTable(True, "IdPrzydzial").Rows
                If CType(dtPoprawka.Compute("Count(Waga)", "Waga=0 AND IdPrzydzial=" & S.Item("IdPrzydzial").ToString), Byte) = 0 AndAlso CType(dtPoprawka.Compute("Count(Waga)", "Waga=1 AND IdPrzydzial=" & S.Item("IdPrzydzial").ToString), Byte) < 3 Then
                  For Each O As DataRow In dtPoprawka.Select("IdPrzydzial=" & S.Item("IdPrzydzial").ToString)
                    cmd.CommandText = K.InsertPoprawka(S.Item("IdPrzydzial").ToString, O.Item("IdObsada").ToString, Typ.Tag.ToString)
                    cmd.ExecuteNonQuery()
                  Next
                End If
              Next
            End If
          End If

        Next

        T.Commit()
        RefreshData()
      Catch mex As MySqlException
        T.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Catch ex As Exception
        T.Rollback()
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
      Cursor = Cursors.Default
    End If
  End Sub
End Class

Public Class ClasificationSummary
  Property LiczbaUczniow As Integer = 0
  Property LiczbaUczniowKlasyfikowanych As Integer = 0
  Property LiczbaUczniowNieklasyfikowanych As Integer = 0
  Property LiczbaUczniowBezOcenNiedostatecznych As Integer = 0
  Property LiczbaUczniow_JednaPoprawka As Integer = 0
  Property LiczbaUczniow_DwiePoprawki As Integer = 0
  Property LiczbaUczniowPromowanych As Integer = 0
  Property LiczbaUczniowNiepromowanych As Integer = 0
  Property LiczbaUczniow_JednaDwieOcenyNiedostateczne As Integer = 0
  Property LiczbaUczniow_TrzyPlusOcenNiedostatecznych As Integer = 0
End Class
