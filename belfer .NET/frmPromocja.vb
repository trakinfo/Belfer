Public Class frmPromocja
  Private DS As DataSet ', MaxPion As Byte  ', lvGroups() As ListViewGroup
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.PromocjaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.PromocjaToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub ApplyNewConfig()
    Try
      FillKlasa()
      RefreshData()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
  Private Sub FetchData()
    Dim P As New PromocjaSQL, DBA As New DataBaseAction, NextYear As Integer, NextSchoolYear As String
    NextYear = CType(My.Settings.SchoolYear.Substring(5, 4), Integer)
    NextSchoolYear = String.Concat(NextYear, "/", NextYear + 1)
    DS = DBA.GetDataSet(P.SelectStudent(My.Settings.SchoolYear, My.Settings.IdSchool) & P.SelectAllocatedStudent(NextSchoolYear, My.Settings.IdSchool))
  End Sub
  Private Sub FillKlasa()
    Dim Z As New ZestawienieOcenSQL
    LoadClassItems(cbKlasa, Z.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear))
  End Sub
  Private Sub LoadClassItems(cb As ComboBox, SelectString As String)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction ', W As New WynikiSQL 'K As New KolumnaSQL
    cb.Items.Clear()
    Try
      R = DBA.GetReader(SelectString)
      While R.Read()
        cb.Items.Add(New SchoolClassComboItem(R.GetInt32("IdKlasa"), R.GetString("Nazwa_Klasy"), False, R.GetString("Kod_Klasy")))
      End While
      cb.Enabled = False 'CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub

  Private Sub cbKlasa_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbKlasa.SelectionChangeCommitted
    My.Settings.ClassName = CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString
    My.Settings.Save()
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKlasa.SelectedIndexChanged
    RefreshData()
  End Sub
  Private Sub RefreshData()
    lvNiepromowani.Items.Clear()
    lvPromowani.Items.Clear()
    cmdAdd.Enabled = False
    cmdDelete.Enabled = False
    ClearDetails()
    'GetKlasa()
    FetchData()
    GetData()
  End Sub
  Private Sub GetData()
    Try
      Me.lvNiepromowani.Groups.Clear()
      Me.lvPromowani.Groups.Clear()
      If rbWszystkieKlasy.Checked Then
        For Each K As SchoolClassComboItem In cbKlasa.Items
          LvNewItem(lvNiepromowani, 0, K.ID.ToString, K.NazwaKlasy)
          LvNewItem(lvPromowani, 1, K.ID.ToString, K.NazwaKlasy)
        Next

      Else
        If cbKlasa.SelectedItem IsNot Nothing Then
          LvNewItem(lvNiepromowani, 0, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, CType(cbKlasa.SelectedItem, SchoolClassComboItem).NazwaKlasy)
          LvNewItem(lvPromowani, 1, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, CType(cbKlasa.SelectedItem, SchoolClassComboItem).NazwaKlasy)

        End If
      End If

      lblRecord.Text = "0 z " & lvNiepromowani.Items.Count
      lvNiepromowani.Columns(1).Width = CInt(IIf(lvNiepromowani.Items.Count > 20, 285, 305))
      lvNiepromowani.Enabled = CBool(IIf(lvNiepromowani.Items.Count > 0, True, False))

      lblRecord1.Text = "0 z " & lvPromowani.Items.Count
      lvPromowani.Columns(1).Width = CInt(IIf(lvPromowani.Items.Count > 20, 285, 305))
      lvPromowani.Enabled = CBool(IIf(lvPromowani.Items.Count > 0, True, False))

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      'R.Close()
    End Try

  End Sub

  'Private Sub GetMaxPion()
  '  Dim OH As New OptionHolder
  '  MaxPion = OH.MaxPion
  'End Sub
  Private Sub LvNewItem(ByVal LV As ListView, ByVal Promocja As Integer, ByVal Klasa As String, NazwaKlasy As String)
    Dim Students() As DataRow
    Students = DS.Tables(0).Select("Promocja=" & Promocja & " AND Klasa='" & Klasa & "'")
    Dim NG As New ListViewGroup(NazwaKlasy)
    LV.Groups.Add(NG)
    For j As Integer = 0 To Students.GetUpperBound(0)
      Dim NewItem As New ListViewItem(Students(j).Item(0).ToString, NG)
      NewItem.SubItems.Add(Students(j).Item(1).ToString)
      NewItem.SubItems.Add(Students(j).Item(2).ToString)
      NewItem.Tag = Students(j).Item("IdPrzydzial").ToString
      LV.Items.Add(NewItem)
    Next
  End Sub
  
  Private Sub frmPromocja_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    ListViewConfig(lvPromowani, "Uczniowie promowani", "0")
    ListViewConfig(lvNiepromowani, "Uczniowie niepromowani", "1")
    lblRecord.Text = ""
    lblRecord1.Text = ""
    ApplyNewConfig()
  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView, Nazwa As String, Promocja As String)
    With lv
      .View = View.Details
      .Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Tag = Promocja
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add(Nazwa, 309, HorizontalAlignment.Left)
      .Columns.Add("Klasa", 0, HorizontalAlignment.Center)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub lvNiepromowani_DoubleClick(sender As Object, e As EventArgs) Handles lvNiepromowani.DoubleClick, lvPromowani.DoubleClick
    Promotion(CType(sender, ListView), CType(sender, ListView).Tag.ToString)

  End Sub


  Private Sub lvNiepromowani_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvNiepromowani.ItemSelectionChanged, lvPromowani.ItemSelectionChanged
    If e.IsSelected Then
      ClearDetails()
      GetDetails(e.Item.Text, CType(sender, ListView))
    Else
      ClearDetails()
      If CType(sender, ListView).Name = "lvNiepromowani" Then
        lblRecord.Text = "0 z " & CType(sender, ListView).Items.Count
      Else
        lblRecord1.Text = "0 z " & CType(sender, ListView).Items.Count
      End If
    End If
    If rbSelected.Checked Then
      cmdAdd.Enabled = CBool(IIf(lvNiepromowani.SelectedItems.Count > 0, True, False))
      cmdDelete.Enabled = CBool(IIf(lvPromowani.SelectedItems.Count > 0, True, False))
    Else
      cmdAdd.Enabled = CBool(IIf(lvNiepromowani.SelectedItems.Count = lvNiepromowani.Items.Count, False, True))
      cmdDelete.Enabled = CBool(IIf(lvPromowani.SelectedItems.Count = lvNiepromowani.Items.Count, False, True))
    End If

  End Sub
  Private Sub ClearDetails()
    lblUser.Text = ""
    lblIP.Text = ""
    lblData.Text = ""
  End Sub
  Private Sub GetDetails(ByVal ID As String, ByVal lv As ListView)
    Dim FoundRow() As DataRow
    Try
      If lv.Name = "lvNiepromowani" Then
        lblRecord.Text = lv.SelectedItems(0).Index + 1 & " z " & lv.Items.Count
      Else
        lblRecord1.Text = lv.SelectedItems(0).Index + 1 & " z " & lv.Items.Count
      End If
      FoundRow = DS.Tables(0).Select("ID=" & ID)
      With FoundRow(0)
        lblUser.Text = String.Concat(.Item("User").ToString, " (Wł: ", .Item("Owner").ToString, ")") '.Item(3).ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub rbWybranaKlasa_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbWybranaKlasa.CheckedChanged, rbWszystkieKlasy.CheckedChanged
    If Not Me.Created Then Exit Sub
    If CType(sender, RadioButton).Checked Then
      If CType(sender, RadioButton).Name = "rbWybranaKlasa" Then
        cbKlasa.Enabled = True
      Else
        cbKlasa.Enabled = False
      End If
      RefreshData()

    End If
  End Sub

  Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
    Promotion(lvNiepromowani, "1")
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    Promotion(lvPromowani, "0")
  End Sub
  Private Sub Promotion(ByVal lv As ListView, ByVal Promocja As String)
    Dim DBA As New DataBaseAction, P As New PromocjaSQL
    Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
    Try
      If rbSelected.Checked Then
        For Each item As ListViewItem In lv.SelectedItems
          If DS.Tables(1).Select("ID=" & item.Text).GetLength(0) < 1 Then
            DBA.ApplyChanges(P.UpdatePrzydzial(item.Tag.ToString, My.Settings.SchoolYear, Promocja), MySQLTrans)
          Else
            MessageBox.Show("Operacja nie może być wykonana, ponieważ uczeń " & item.SubItems(1).Text & " ma już przydział na następny rok szkolny.")
          End If
        Next
      Else
        For Each item As ListViewItem In lv.Items
          If item.Selected = False AndAlso DS.Tables(1).Select("ID=" & item.Text).GetLength(0) < 1 Then
            DBA.ApplyChanges(P.UpdatePrzydzial(item.Tag.ToString, My.Settings.SchoolYear, Promocja), MySQLTrans)
          Else
            MessageBox.Show("Operacja nie może być wykonana, ponieważ uczeń " & item.SubItems(1).Text & " posiada przydział na następny rok szkolny.")
          End If
        Next
      End If
      MySQLTrans.Commit()
      'FetchData()
      RefreshData()
    Catch mex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub

  Private Sub rbPromuj_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSelected.CheckedChanged, rbUnselected.CheckedChanged
    If CType(sender, RadioButton).Name = "rbSelected" And CType(sender, RadioButton).Checked Then
      cmdAdd.Enabled = CBool(IIf(lvNiepromowani.SelectedItems.Count > 0, True, False))
      cmdDelete.Enabled = CBool(IIf(lvPromowani.SelectedItems.Count > 0, True, False))
    Else
      cmdAdd.Enabled = CBool(IIf(lvNiepromowani.SelectedItems.Count = lvNiepromowani.Items.Count, False, True))
      cmdDelete.Enabled = CBool(IIf(lvPromowani.SelectedItems.Count = lvNiepromowani.Items.Count, False, True))
    End If
  End Sub
End Class