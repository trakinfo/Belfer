Imports System.Windows.Forms

Public Class dlgLekcja
  Public IsNewMode As Boolean
  Public Event NewAdded(ByVal InsertedID As String)
  Public WeekDay As Byte, IdPlan As Integer, LessonNumberByStaff As Hashtable, Filter, FilterID, PlanEndDate As String, Sala As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()
    If IsNewMode Then
      If AddNew() Then
        cbGodzina.SelectedIndex = CType(If(cbGodzina.SelectedIndex < cbGodzina.Items.Count - 1, cbGodzina.SelectedIndex + 1, 0), Integer)
        LessonNumberByStaff = GetLessonNumberByStaff()
        LoadLessonItems()
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
  Private Function AddNew() As Boolean
    AddNew = False
    Cursor = Cursors.WaitCursor
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, P As New PlanSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertLekcja)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?IdPlan", IdPlan)
      cmd.Parameters.AddWithValue("?IdObsada", CType(cbObsada.SelectedItem, LessonComboItem).IdObsada)
      cmd.Parameters.AddWithValue("?DzienTygodnia", WeekDay)
      cmd.Parameters.AddWithValue("?IdGodzina", CType(cbGodzina.SelectedItem, CbItem).ID)
      cmd.Parameters.AddWithValue("?Sala", txtSala.Text.Trim)
      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()
      RaiseEvent NewAdded(cmd.LastInsertedId.ToString)
      Cursor = Cursors.Default
      Return True
    Catch myex As MySqlException
      MySQLTrans.Rollback()
      Select Case myex.Number
        Case 1062
          MessageBox.Show("Nie można dwukrotnie wprowadzić tej samej lekcji na jednej godzinie lekcyjnej!", "Belfer .NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Case Else
          MessageBox.Show(myex.Message & vbCr & myex.Number)
      End Select
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      'Return False
    End Try
    'Return False
  End Function

  Private Sub cbGodzina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGodzina.SelectedIndexChanged, cbObsada.SelectedIndexChanged
    OK_Button.Enabled = If(cbGodzina.SelectedItem IsNot Nothing AndAlso cbObsada.SelectedItem IsNot Nothing, True, False)
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub

  Private Sub txtSala_TextChanged(sender As Object, e As EventArgs) Handles txtSala.TextChanged
    If IsNewMode Then
      cbGodzina_SelectedIndexChanged(sender, e)
    Else
      OK_Button.Enabled = If(String.Compare(txtSala.Text, Sala, False) <> 0, True, False)
      'If String.Compare(txtSala.Text, Sala, False) <> 0 Then OK_Button.Enabled = True
    End If
    'If Not IsNewMode AndAlso txtSala.Text.Length > 0 Then OK_Button.Enabled = True
  End Sub

  Private Sub cbWeekDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWeekDay.SelectedIndexChanged
    If IsNewMode Then
      txtSala.Enabled = True
      cbObsada.Enabled = If(cbObsada.Items.Count > 0, True, False)
      WeekDay = CType(CType(cbWeekDay.SelectedItem, CbItem).ID, Byte)
      cbGodzina.SelectedIndex = 0
      cbGodzina.Enabled = True
    End If
  End Sub
  Public Sub LoadLessonItems()
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, P As New PlanSQL, SelectString As String
    cbObsada.Items.Clear()
    SelectString = If(Filter = "Klasa", P.SelectPrzedmiotByKlasa(FilterID, My.Settings.SchoolYear, PlanEndDate), P.SelectPrzedmiotByBelfer(FilterID, My.Settings.SchoolYear, PlanEndDate))
    Try
      R = DBA.GetReader(SelectString)
      If R.HasRows Then
        While R.Read()
          If CType(LessonNumberByStaff.Item(R.GetInt32("IdObsada")), Integer) < R.GetDecimal("LiczbaGodzin") Then
            cbObsada.Items.Add(New LessonComboItem(R.GetInt32("IdObsada"), R.GetString("Lekcja"), R.GetDecimal("LiczbaGodzin")))
          End If
        End While
        'cbObsada.Enabled = If(, True, False)
        If cbObsada.Items.Count > 0 Then
          cbObsada.Enabled = True
          cbGodzina.Enabled = True
          cbWeekDay.Enabled = True 
          txtSala.Enabled = True
        Else
          cbObsada.Enabled = False
          cbGodzina.Enabled = False
          cbWeekDay.Enabled = False
          txtSala.Enabled = False
        End If
      Else
      End If
    Catch err As MySqlException
      MessageBox.Show(err.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Public Function GetLessonNumberByStaff() As Hashtable
    Dim R As MySqlDataReader = Nothing
    Dim DBA As New DataBaseAction, P As New PlanSQL
    Try
      R = DBA.GetReader(P.CountLessonByStaff(IdPlan.ToString, PlanEndDate))
      Dim LessonNumber As New Hashtable
      If R.HasRows Then
        While R.Read()
          LessonNumber.Add(R.GetInt32("IdObsada"), R.GetInt32("LiczbaGodzin"))
        End While
        'Return LessonNumber
      End If
      Return LessonNumber
    Catch err As MySqlException
      MessageBox.Show(err.Message)
      Return Nothing
    Finally
      R.Close()
    End Try
  End Function
End Class
