Public Class frmReklamacja
  Private DT As DataTable
  Private Status As String = "Status IN (0)"
  Public Filter As String = ""
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.ReklamacjaToolStripMenuItem.Enabled = True
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ReklamacjaToolStripMenuItem.Enabled = True
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, 1, Panel1.Width)
  End Sub

  Private Sub frmReklamacja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ListViewConfig(lvReklamacja)
    AddColumns(lvReklamacja)
    Dim SeekCriteria() As String = New String() {"Nazwisko i imię reklamującego", "Nazwisko i imię autora błędnego wpisu"}
    Me.cbSeek.Items.AddRange(SeekCriteria)
    Me.cbSeek.SelectedIndex = 0
    FetchData()
    GetData()
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub
  Private Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = True
      .AllowColumnReorder = False
      .AutoResizeColumns(0)
      .HideSelection = False
      '.HoverSelection = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("ID", 0, HorizontalAlignment.Left)
      .Columns.Add("Nazwisko i imię ucznia", 150, HorizontalAlignment.Left)
      .Columns.Add("Lekcja", 130, HorizontalAlignment.Left)
      .Columns.Add("Typ", 40, HorizontalAlignment.Center)
      .Columns.Add("Data zajęć", 80, HorizontalAlignment.Center)
      .Columns.Add("Autor --> Adresat", 230, HorizontalAlignment.Left)
      .Columns.Add("Status", 50, HorizontalAlignment.Center)
      .Columns.Add("Komentarz", 233, HorizontalAlignment.Left)
    End With
  End Sub
  Private Sub FetchData()
    Try
      Dim DBA As New DataBaseAction, A As New AbsencjaSQL
      DT = DBA.GetDataTable(A.SelectComplain(My.Settings.SchoolYear, My.Settings.IdSchool))
    Catch mex As MySqlException
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try

  End Sub
  Private Sub GetData()
    lvReklamacja.Items.Clear()
    lvReklamacja.Enabled = False
    cmdAccept.Enabled = False
    cmdReject.Enabled = False
    cmdDelete.Enabled = False
    For Each row As DataRow In DT.Select(Status & Filter)
      'Dim User As String
      'User = row.Item("User").ToString.ToLower.Trim
      Dim NewItem As New ListViewItem With {.UseItemStyleForSubItems = False, .Text = row.Item(0).ToString, .Tag = New UserSignature With {.User = If(GlobalValues.Users.ContainsKey(row.Item("User").ToString), GlobalValues.Users.Item(row.Item("User").ToString).ToString, row.Item("User").ToString), .ComputerIP = row.Item("ComputerIP").ToString, .Version = row.Item("Version").ToString}}

      NewItem.SubItems.Add(row.Item("Student").ToString).Tag = row.Item("IdUczen").ToString
      NewItem.SubItems.Add(row.Item("Przedmiot").ToString).Tag = row.Item("IdLekcja").ToString
      NewItem.SubItems.Add(row.Item("Typ").ToString.ToUpper)
      NewItem.SubItems.Add(CType(row.Item("DataLekcji"), Date).ToShortDateString)
      NewItem.SubItems.Add(String.Concat(row.Item("NotifierName").ToString, " --> ", row.Item("OwnerName").ToString)).Tag = New ComplainMemberRelation With {.AbsenceOwner = row.Item("AbsenceOwner").ToString, .ComplainUser = row.Item("AbsenceNotifier").ToString}

      Dim StatusMark As String = "", StatusColor As Color = Color.Black
      Select Case CType(row.Item("Status"), Byte)
        Case 0
          StatusMark = ChrW(&H4C)
        Case 1
          StatusMark = ChrW(&H43)
          StatusColor = Color.Green
        Case 2
          StatusMark = ChrW(&H44)
          StatusColor = Color.Red
      End Select
      NewItem.SubItems.Add(StatusMark).Font = New Font("Wingdings", 10, FontStyle.Bold)
      NewItem.SubItems(6).ForeColor = StatusColor
      NewItem.SubItems(6).Tag = row.Item("Status").ToString
      NewItem.SubItems.Add(row.Item("Komentarz").ToString)
      lvReklamacja.Items.Add(NewItem)
    Next
    lblRecord.Text = "0 z " & lvReklamacja.Items.Count
    lvReklamacja.Columns(7).Width = CInt(IIf(lvReklamacja.Items.Count > 21, 214, 233))
    Me.lvReklamacja.Enabled = CType(IIf(Me.lvReklamacja.Items.Count > 0, True, False), Boolean)

  End Sub
  Private Sub lvSubwniosek_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvReklamacja.ItemSelectionChanged
    If e.IsSelected Then
      If CType(e.Item.SubItems(6).Tag, Byte) = 0 Then
        If GlobalValues.AppUser.Role = GlobalValues.Role.Administrator Then
          cmdAccept.Enabled = True
          cmdReject.Enabled = True
          cmdDelete.Enabled = True
        ElseIf GlobalValues.AppUser.Login = CType(e.Item.SubItems(5).Tag, ComplainMemberRelation).AbsenceOwner Then
          cmdAccept.Enabled = True
          cmdReject.Enabled = True
        ElseIf GlobalValues.AppUser.Login = CType(e.Item.SubItems(5).Tag, ComplainMemberRelation).ComplainUser Then
          cmdDelete.Enabled = True
        End If
      End If

      GetDetails(CType(e.Item.Tag, UserSignature))
    Else
      cmdAccept.Enabled = False
      cmdReject.Enabled = False
      cmdDelete.Enabled = False
      ClearDetails()
    End If
  End Sub
  Sub GetDetails(Podpis As UserSignature)
    lblRecord.Text = lvReklamacja.SelectedItems(0).Index + 1 & " z " & lvReklamacja.Items.Count
    lblUser.Text = Podpis.User
    lblIP.Text = Podpis.ComputerIP
    lblData.Text = Podpis.Version
  End Sub
  Sub ClearDetails()
    lblRecord.Text = "0" & " z " & lvReklamacja.Items.Count
    lblData.Text = ""
    lblIP.Text = ""
    lblUser.Text = ""
  End Sub
  Private Sub txtSeek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeek.TextChanged
    Cursor = Cursors.WaitCursor
    Select Case Me.cbSeek.Text
      Case "Nazwisko i imię reklamującego"
        Filter = " AND NotifierName LIKE '" & Me.txtSeek.Text + "%'"
      Case "Nazwisko i imię autora błędnego wpisu"
        Filter = " AND OwnerName LIKE '" & Me.txtSeek.Text + "%'"
    End Select
    Me.GetData()
    Cursor = Cursors.Default
  End Sub

  Private Sub cbSeek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSeek.SelectedIndexChanged
    Me.txtSeek.Text = ""
    Me.txtSeek.Focus()
  End Sub

  Private Sub rbRegistered_CheckedChanged(sender As Object, e As EventArgs) Handles rbRegistered.CheckedChanged, rbAccepted.CheckedChanged, rbAllStatus.CheckedChanged, rbRejected.CheckedChanged
    If CType(sender, RadioButton).Created = False Then Exit Sub
    If CType(sender, RadioButton).Checked Then
      Status = CType(sender, RadioButton).Tag.ToString
      GetData()
    End If
  End Sub

  Private Sub cmdAccept_Click(sender As Object, e As EventArgs) Handles cmdAccept.Click, cmdReject.Click
    Dim SelectedPosition As String
    Dim MySQLTrans As MySqlTransaction
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      SelectedPosition = lvReklamacja.SelectedItems(0).Text
      For Each Item As ListViewItem In Me.lvReklamacja.SelectedItems
        UpdateComplainStatus(MySQLTrans, Item.Text, CType(sender, Button).Tag.ToString)
        If CType(CType(sender, Button).Tag, Byte) = 2 Then RestoreAbsence(MySQLTrans, Item.SubItems(1).Tag.ToString, Item.SubItems(2).Tag.ToString, Item.SubItems(3).Text, Item.SubItems(4).Text)
        MySQLTrans.Commit()
      Next
      FetchData()
      GetData()
      Dim SH As New SeekHelper
      SH.FindListViewItem(lvReklamacja, SelectedPosition)
    Catch mex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
    Catch ex As Exception
      MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
    End Try
  End Sub
  Private Sub UpdateComplainStatus(Tran As MySqlTransaction, IdReklamacja As String, Status As String)
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(A.UpdateComplainStatus)
    cmd.Transaction = Tran
    cmd.Parameters.AddWithValue("?IdReklamacja", IdReklamacja)
    cmd.Parameters.AddWithValue("?Status", Status)
    cmd.ExecuteNonQuery()
  End Sub
  Private Sub RestoreAbsence(Tran As MySqlTransaction, IdUczen As String, IdLekcja As String, Typ As String, Data As String)
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(A.InsertAbsence)
    cmd.Transaction = Tran
    cmd.Parameters.AddWithValue("?IdUczen", IdUczen)
    cmd.Parameters.AddWithValue("?IdLekcja", IdLekcja)
    cmd.Parameters.AddWithValue("?Typ", Typ)
    cmd.Parameters.AddWithValue("?Data", Data)
    cmd.ExecuteNonQuery()
    DeleteBackAbsence(Tran, IdUczen, IdLekcja, Typ, Data)
  End Sub
  Private Sub DeleteBackAbsence(Tran As MySqlTransaction, IdUczen As String, IdLekcja As String, Typ As String, Data As String)
    Dim DBA As New DataBaseAction, A As New AbsencjaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(A.DeleteBackAbsence)
    cmd.Transaction = Tran
    cmd.Parameters.AddWithValue("?IdUczen", IdUczen)
    cmd.Parameters.AddWithValue("?IdLekcja", IdLekcja)
    cmd.Parameters.AddWithValue("?Typ", Typ)
    cmd.Parameters.AddWithValue("?Data", Data)
    cmd.ExecuteNonQuery()
  End Sub
  Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
    If MessageBox.Show("Czy na pewno chcesz usunąć zaznaczone pozycje?", My.Application.Info.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
      Dim DBA As New DataBaseAction, A As New AbsencjaSQL, DeletedIndex As Integer
      Dim MySQLTrans As MySqlTransaction
      MySQLTrans = GlobalValues.gblConn.BeginTransaction()
      Try
        For Each Item As ListViewItem In Me.lvReklamacja.SelectedItems
          DeletedIndex = Item.Index
          Dim cmd As MySqlCommand = DBA.CreateCommand(A.DeleteComplain())
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?ID", Item.Text)
          cmd.ExecuteNonQuery()
          RestoreAbsence(MySQLTrans, Item.SubItems(1).Tag.ToString, Item.SubItems(2).Tag.ToString, Item.SubItems(3).Text, Item.SubItems(4).Text)
        Next
        MySQLTrans.Commit()
        FetchData()
        GetData()
        Dim SH As New SeekHelper
        SH.FindPostRemovedListViewItemIndex(Me.lvReklamacja, DeletedIndex)
      Catch mex As MySqlException
        MySQLTrans.Rollback()
        MessageBox.Show(mex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      Catch ex As Exception
        MessageBox.Show(ex.Message, My.Application.Info.ProductName, MessageBoxButtons.OK)
      End Try
    End If
  End Sub
End Class

Public Class UserSignature
  Public Property User As String
  Public Property Owner As String
  Public Property ComputerIP As String
  Public Property Version As String
End Class

Public Class ComplainMemberRelation
  Public Property ComplainUser As String
  Public Property AbsenceOwner As String
End Class