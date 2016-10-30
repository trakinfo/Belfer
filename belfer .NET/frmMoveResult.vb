Public Class frmMoveResult

  Private Sub frmWynikiPartial_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    MainForm.MoveResultToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub

  Private Sub frmWynikiPartial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.MoveResultToolStripMenuItem.Enabled = True
    RemoveHandler SharedKonfiguracja.OnKonfiguracjaChanged, AddressOf ApplyNewConfig
  End Sub
  Private Sub frmWynikiTabela_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ApplyNewConfig()
  End Sub
  Private Sub ApplyNewConfig()
    cbKlasa.Items.Clear()
    cbKlasa.Enabled = False
    cbTargetClass.Items.Clear()
    cbTargetClass.Enabled = False
    cbPrzedmiot.Items.Clear()
    cbPrzedmiot.Enabled = False

    FillKlasa(cbKlasa, chkVirtual)

  End Sub
  Private Sub FillKlasa(cb As ComboBox, chk As CheckBox)
    Dim W As New WynikiSQL
    LoadClassItems(cb, If(chk.Checked, W.SelectVirtualClasses(My.Settings.IdSchool, My.Settings.SchoolYear), W.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear)))

  End Sub
  Private Sub LoadClassItems(cb As ComboBox, SelectString As String)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction ', W As New WynikiSQL 'K As New KolumnaSQL
    cb.Items.Clear()
    Try
      R = DBA.GetReader(SelectString)
      While R.Read()
        cb.Items.Add(New SchoolClassComboItem(R.GetInt32("IdKlasa"), R.GetString("Nazwa_Klasy"), R.GetBoolean("Virtual")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub chkVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkVirtual.CheckedChanged
    If Not Me.Created Then Exit Sub
    ApplyNewConfig()
  End Sub
  Private Sub chkTargetVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles chkTargetVirtual.CheckedChanged
    If Not Me.Created Then Exit Sub
    Dim MR As New MoveResultSQL
    LoadClassItems(cbTargetClass, If(chkTargetVirtual.Checked, MR.SelectVirtualClass(My.Settings.SchoolYear, My.Settings.IdSchool, CType(cbStudent.SelectedItem, StudentComboItem).AllocationID.ToString), MR.SelectClass(My.Settings.SchoolYear, My.Settings.IdSchool, CType(cbStudent.SelectedItem, StudentComboItem).AllocationID.ToString)))
  End Sub
  Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
    Me.Dispose(True)
  End Sub

  Private Sub cbStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbStudent.SelectedIndexChanged
    LoadObjectStaffItems(cbPrzedmiot)

  End Sub

  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    cbPrzedmiot.Items.Clear()
    cbPrzedmiot.Enabled = False
    cbTargetClass.Items.Clear()
    cbTargetClass.Enabled = False
    LoadStudentItems(cbStudent)
  End Sub
  Private Sub LoadObjectStaffItems(cb As ComboBox)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, W As New WynikiSQL
    cb.Items.Clear()
    Try

      R = DBA.GetReader(W.SelectPrzedmiot(CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Date.Today))

      While R.Read()
        cb.Items.Add(New ObjectStaffComboItem(R.GetInt32("IdObsada"), R.GetString("Obsada"), R.GetInt32("Przedmiot"), R.GetString("SchoolTeacherID")))
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub LoadObjectStaff(txt As TextBox)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, MR As New MoveResultSQL
    txt.Text = ""
    Try

      R = DBA.GetReader(MR.SelectPrzedmiot(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ObjectID.ToString, CType(cbTargetClass.SelectedItem, SchoolClassComboItem).ID.ToString, My.Settings.SchoolYear, Date.Today))
      If R.HasRows Then
        R.Read()
        txt.Tag = New ObjectStaffComboItem(R.GetInt32("IdObsada"), R.GetString("Obsada"), R.GetInt32("Przedmiot"), R.GetString("SchoolTeacherID"))
        txt.Text = CType(txt.Tag, ObjectStaffComboItem).ToString
      Else
        txt.Tag = Nothing
        txt.Text = "Nie znaleziono odpowiedniego przedmiotu"
      End If

    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub LoadStudentItems(cb As ComboBox)
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, MR As New MoveResultSQL, SelectString As String
    SelectString = If(chkVirtual.Checked, MR.SelectVirtualStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString), MR.SelectStudent(My.Settings.SchoolYear, CType(cbKlasa.SelectedItem, SchoolClassComboItem).ID.ToString))
    cb.Items.Clear()
    Try

      R = DBA.GetReader(SelectString)

      While R.Read()
        cb.Items.Add(New StudentComboItem With {.ID = R.GetInt32("ID"), .Student = R.GetString("Student"), .AllocationID = R.GetInt32("IdPrzydzial")})
      End While
      cb.Enabled = CType(IIf(cb.Items.Count > 0, True, False), Boolean)
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Private Sub cbPrzedmiot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPrzedmiot.SelectedIndexChanged
    chkTargetVirtual.Enabled = True
    Dim MR As New MoveResultSQL
    LoadClassItems(cbTargetClass, If(chkTargetVirtual.Checked, MR.SelectVirtualClass(My.Settings.SchoolYear, My.Settings.IdSchool, CType(cbStudent.SelectedItem, StudentComboItem).AllocationID.ToString), MR.SelectClass(My.Settings.SchoolYear, My.Settings.IdSchool, CType(cbStudent.SelectedItem, StudentComboItem).AllocationID.ToString)))
  End Sub

  Private Sub cbTargetClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTargetClass.SelectedIndexChanged
    LoadObjectStaff(txtPrzedmiot)
  End Sub

  Private Sub cmdExecute_Click(sender As Object, e As EventArgs) Handles cmdExecute.Click
    Dim MR As New MoveResultSQL, DS As DataSet, DBA As New DataBaseAction, MySQLTran As MySqlTransaction = Nothing
    Try
      DS = DBA.GetDataSet(MR.SelectSourceColumn(CType(cbPrzedmiot.SelectedItem, ObjectStaffComboItem).ID.ToString, CType(cbStudent.SelectedItem, StudentComboItem).ID.ToString) & MR.SelectTargetColumn(CType(txtPrzedmiot.Tag, ObjectStaffComboItem).ID.ToString))
      DS.Tables(0).TableName = "SourceColumn"
      DS.Tables(1).TableName = "TargetColumn"
      pbMoveResult.Minimum = 0
      pbMoveResult.Style = ProgressBarStyle.Continuous
      pbMoveResult.Maximum = DS.Tables("SourceColumn").Rows.Count
      MySQLTran = GlobalValues.gblConn.BeginTransaction()
      For Each R As DataRow In DS.Tables("SourceColumn").Rows
        Dim TargetColID As String, IdWynik As String
        IdWynik = R.Item("IdWynik").ToString
        TargetColID = DS.Tables("TargetColumn").Select("NrKolumny=" & R.Item("NrKolumny").ToString & " AND Typ='" & R.Item("Typ").ToString & "'")(0).Item("ID").ToString
        Dim cmd As MySqlCommand = DBA.CreateCommand(MR.UpdateWynik)
        cmd.Transaction = MySQLTran
        cmd.Parameters.AddWithValue("?IdKolumna", TargetColID)
        cmd.Parameters.AddWithValue("?IdWynik", IdWynik)
        cmd.ExecuteNonQuery()
        'pbMoveResult.Value += 1
        pbMoveResult.Increment(1)
      Next
      MySQLTran.Commit()
      MessageBox.Show("Operacja zakończona pomyślnie." & vbNewLine & "Przepięto " & pbMoveResult.Value.ToString & " ocen.")
    Catch myex As MySqlException
      MessageBox.Show(myex.Message & vbNewLine & myex.InnerException.Message)
      MySQLTran.Rollback()
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    End Try
  End Sub
End Class

Public Class StudentComboItem
  Public Property ID As Integer
  Public Property Student As String
  Public Property AllocationID As Integer
  Public Overrides Function ToString() As String
    Return Student
  End Function
End Class