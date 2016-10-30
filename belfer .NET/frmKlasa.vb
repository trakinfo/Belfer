Public Class frmKlasa
  Private DT As DataTable
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.OddzialyToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.OddzialyToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
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
      AddColumns(lv)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub
  Private Sub AddColumns(ByVal lv As ListView)
    With lv
      .Columns.Add("Kod", 0, HorizontalAlignment.Center)
      .Columns.Add("Nazwa", 181, HorizontalAlignment.Center)
    End With
  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.Close()
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Private Sub frmKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
    Me.ListViewConfig(Me.lvKlasa)
    lblRecord.Text = ""
    ApplyNewConfig()
    'Me.GetData()
    '
  End Sub
  Private Sub ApplyNewConfig()
    EnableButtons(False)
    lvKlasa.Items.Clear()
    ClearDetails()
    GetData()
  End Sub
  Private Sub GetData()
    Dim DBA As New DataBaseAction, K As New KlasaSQL
    lvKlasa.Items.Clear()
    DT = DBA.GetDataTable(K.SelectKlasa)
    For Each row As DataRow In DT.Rows
      Me.lvKlasa.Items.Add(row.Item(0).ToString)
      Me.lvKlasa.Items(Me.lvKlasa.Items.Count - 1).SubItems.Add(row.Item(0).ToString)
    Next

    lvKlasa.Columns(1).Width = CInt(IIf(lvKlasa.Items.Count > 15, 162, 181))
    Me.lvKlasa.Enabled = CType(IIf(Me.lvKlasa.Items.Count > 0, True, False), Boolean)
    lblRecord.Text = "0 z " & lvKlasa.Items.Count
  End Sub
  Private Sub GetDetails(ByVal Name As String)
    Try
      lblRecord.Text = lvKlasa.SelectedItems(0).Index + 1 & " z " & lvKlasa.Items.Count
      With DT.Select("Kod='" & Name & "'")(0)
        lblUser.Text = String.Concat(GlobalValues.Users.Item(.Item("User").ToString.ToLower).ToString, " (Wł: ", GlobalValues.Users.Item(.Item("Owner").ToString.ToLower).ToString, ")") '.Item(1).ToString
        lblIP.Text = .Item("ComputerIP").ToString
        lblData.Text = .Item("Version").ToString
      End With
    Catch err As Exception
      MessageBox.Show(err.Message)
    End Try
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvKlasa.ItemSelectionChanged
    Me.ClearDetails()
    If e.IsSelected Then
      GetDetails(e.Item.Text)
      EnableButtons(True)
    Else
      EnableButtons(False)
    End If
  End Sub

  Private Sub EnableButtons(ByVal Value As Boolean)
    Me.cmdDelete.Enabled = Value
    'Me.cmdEdit.Enabled = Value
  End Sub
  Private Sub ClearDetails()
    lblRecord.Text = "0 z " & lvKlasa.Items.Count
    Me.lblUser.Text = ""
    Me.lblData.Text = ""
    Me.lblIP.Text = ""
  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    Dim DBA As New DataBaseAction, DeletedIndex As Integer, K As New KlasaSQL
    Try
      For Each Item As ListViewItem In Me.lvKlasa.SelectedItems
        DeletedIndex = Item.Index
        DBA.ApplyChanges(K.DeleteKlasa(Item.Text))
      Next
      Me.EnableButtons(False)
      lvKlasa.Items.Clear()
      Me.GetData()
      Dim SH As New SeekHelper
      SH.FindPostRemovedListViewItemIndex(Me.lvKlasa, DeletedIndex)
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)

    End Try
  End Sub
  Public Sub AddKlasa()
    Dim dlgAddNew As New dlgKlasa
    With dlgAddNew
      .Text = "Dodawanie nowego oddziału"

      '.MdiParent = Me.MdiParent
      .MaximizeBox = False
      .StartPosition = FormStartPosition.CenterScreen
      'dlgAddNew.Opacity = 0.7
      AddHandler .NewAdded, AddressOf NewAdded
      .txtKlasa.Focus()
      Me.cmdAddNew.Enabled = False
      .ShowDialog()
      'AddHandler dlgAddNew.FormClosed, AddressOf EnableCmdAddNew
      Me.cmdAddNew.Enabled = True
    End With

  End Sub
  Private Sub NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
    GetData()
    Dim SH As New SeekHelper
    SH.FindListViewItem(Me.lvKlasa, InsertedID)
  End Sub
  Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
    AddKlasa()
  End Sub

  
End Class

