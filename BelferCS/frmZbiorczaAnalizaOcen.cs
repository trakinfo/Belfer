using System;
using System.Windows.Forms;
using System.ComponentModel;
using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Linq;
using BelferCS.Properties;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.Drawing;
namespace BelferCS
{
    public partial class frmZbiorczaAnalizaOcen : Form
    {
        public frmZbiorczaAnalizaOcen()
        {
            InitializeComponent();
        }
        public delegate void NewRowHandler();
        public event NewRowHandler NewRow;
        public event EventHandler TheEnd;
        private static MySqlConnection conn;
        private dlgWait Wait = new dlgWait { Tag = "Pobieranie danych ..." };
        private Timer tmRefresh;
        private RadioButton rbOkres, rbTyp;
        private int PageNumber = default(int);
        private List<string> ReportHeader;
        private int[] Offset=new int[3];
        private bool IsPreview;
        private PrintHelper PH = new PrintHelper();
        private DateTime EndOfSemester = default(DateTime), EndOfSchoolYear = default(DateTime);
        private List<SubjectStaff> lstObsada =new List<SubjectStaff>();
        private List<ScoreInfo> lstLiczbaOcen = new List<ScoreInfo>();
        private List<StudentCount> lstLiczbaUczniow = new List<StudentCount>();
        private List<VirtualStudentCount> lstLiczbaUczniowNI = new List<VirtualStudentCount>();
        private List<SubjectGroupCount> lstLiczbaUczniowGrupa = new List<SubjectGroupCount>();

        public MySqlConnection Conn
        {
            set
            {
                conn = value;
            }
            get
            {
                return conn;
            }
        }
        private void frmZbiorczaAnalizaOcen_Load(object sender, EventArgs e)
        {
            SharedConfiguration.ConfigurationChanged += ApplyNewConfig;
            conn.Open();
            //string[] SeekCriteria = new string[] { "Klasa", "Nazwisko i imię","Przedmiot"};
            //cbSeek.Items.AddRange(SeekCriteria);
            //cbSeek.SelectedIndex = 0;
            ListViewConfig(tlvAnaliza);
            GenerateColumns(tlvAnaliza, SpecifyCols());

            ApplyNewConfig();
        }
        private void frmZbiorczaAnalizaOcen_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            TheEnd?.Invoke(sender,e);
            conn.Dispose();
            }
        }
        private void ListViewConfig(TreeListView olv)
        {
            olv.View = View.Details;
            olv.FullRowSelect = true;
            olv.GridLines = true;
            olv.AllowColumnReorder = false;
            olv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            olv.HideSelection = false;
            //olv.ShowGroups = true;
            olv.UseFiltering = true;
            olv.HeaderStyle = ColumnHeaderStyle.Clickable;
            olv.ShowItemToolTips = true;
            olv.HeaderWordWrap = true;
            olv.UseHotItem = true;
            olv.UseTranslucentHotItem = true;
            olv.HeaderMaximumHeight =80;
            olv.HeaderMinimumHeight = 50;
            HeaderFormatStyle HeaderStyle = new HeaderFormatStyle();
            HeaderStyle.SetFont(new System.Drawing.Font(olv.Font.FontFamily, olv.Font.Size, System.Drawing.FontStyle.Bold));
            olv.HeaderFormatStyle = HeaderStyle;
            //olv.SmallImageList = new ImageList { ImageSize = new System.Drawing.Size(16, 16) };
        }

        private void GenerateColumns(TreeListView olv, List<OLVColumn> Cols)
        {
            olv.AllColumns.Clear();
            olv.AllColumns.AddRange(Cols);
            olv.RebuildColumns();
        }

        private void ApplyNewConfig()
        {
            int Year=DateTime.Today.Year;
            int.TryParse(DateTime.Today.Month > 8 ? Settings.Default.SchoolYear.Substring(0, 4) : Settings.Default.SchoolYear.Substring(5, 4), out Year);
            DateTime CurrentDate = new DateTime(Year, DateTime.Today.Month, DateTime.Today.Day);
            var CH = new CalcHelper();
            //StartDateOfSchoolYear = CH.StartDateOfSchoolYear(Settings.Default.SchoolYear);
            EndOfSemester = CH.StartDateOfSemester2(Settings.Default.SchoolYear,CurrentDate, conn).AddDays(-1);
            EndOfSchoolYear = CH.EndDateOfSchoolYear(Settings.Default.SchoolYear);

            if (rbOkres == null)
            {
                if (CurrentDate <= EndOfSemester) {rbSemestr.Checked=true; }
                else { rbRokSzkolny.Checked = true; }
            }
            else
            {
                rbSemestr_CheckedChanged(rbOkres, null);
            }
        }

        private void rbSemestr_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked==false) return;
            rbOkres = (RadioButton)sender;
            RefreshData();
        }

        private void rbNauczyciel_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == false) return;
            rbTyp = (RadioButton)sender;
            GetData(tlvAnaliza);
        }
        private List<OLVColumn> SpecifyCols()
        {
            var Cols = new List<OLVColumn>();
            Cols.Add(new OLVColumn { Text = "Klasa", AspectName = "Label", MinimumWidth = 100, Width = 200, FillsFreeSpace = true, TextAlign=HorizontalAlignment.Left, HeaderTextAlign = HorizontalAlignment.Center });
            Cols.Add(new OLVColumn { Text = "Liczba\nocen", WordWrap=true, AspectName = "StudentCount", MinimumWidth = 50, Width = 60, FillsFreeSpace = false, HeaderTextAlign=HorizontalAlignment.Center,ToolTipText="Liczba uczniów w klasie",TextAlign=HorizontalAlignment.Center, IsHeaderVertical = false });
            Cols.Add(new OLVColumn { Text = "liczba \nwyst. ocen", WordWrap=true, AspectName = "TotalScoreCount", MinimumWidth = 50, Width = 80, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba wystawionych ocen", IsHeaderVertical = false });
            Cols.Add(new OLVColumn { Text = "nkl", WordWrap = true, AspectName = "UnclassifiedCount", MinimumWidth = 50, Width = 70, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba uczniów nieklasyfikowanych" });
            Cols.Add(new OLVColumn { Text = "cel", AspectName = "ExcelentCount", MinimumWidth = 50, Width = 70, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba ocen celujących"});
            Cols.Add(new OLVColumn { Text = "bdb", AspectName = "VeryGoodCount", MinimumWidth = 50, Width = 70, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba ocen bardzo dobrych" });
            Cols.Add(new OLVColumn { Text = "db", AspectName = "GoodCount", MinimumWidth = 50, Width = 70, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba ocen dobrych"});
            Cols.Add(new OLVColumn { Text = "dst", AspectName = "SufficientCount", MinimumWidth = 50, Width = 70, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba ocen dostatecznych"});
            Cols.Add(new OLVColumn { Text = "dps", AspectName = "PassedCount", MinimumWidth = 50, Width = 70, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba ocen dopuszczających" });
            Cols.Add(new OLVColumn { Text = "ndst", AspectName = "FailedCount", MinimumWidth = 50, Width = 70, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Liczba ocen niedostatecznych", IsHeaderVertical = false });
            Cols.Add(new OLVColumn { Text = "Średnia", AspectName = "AvgScore", MinimumWidth = 40, Width = 40, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Średnia ocen", IsHeaderVertical = true });
            Cols.Add(new OLVColumn { Text = "Mediana", AspectName = "MedianScore", MinimumWidth = 30, Width = 40, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Wartość środkowa", IsHeaderVertical = true });
            Cols.Add(new OLVColumn { Text = "Dominanta", AspectName = "DominantScore", MinimumWidth = 30, Width = 30, FillsFreeSpace = false, TextAlign = HorizontalAlignment.Center, HeaderTextAlign = HorizontalAlignment.Center, ToolTipText = "Ocena występująca najczęściej", IsHeaderVertical = true });


            return Cols;
    }

        private void SetAspectGetter(byte opcja)
        {
            switch (opcja)
            {
                case 0:
                    tlvAnaliza.GetColumn(2).AspectGetter = delegate (object x) { return ((StaffBranch)x).TotalScoreCount; };
                    tlvAnaliza.GetColumn(3).AspectGetter = delegate (object x) { return ((StaffBranch)x).UnclassifiedCount; };
                    tlvAnaliza.GetColumn(4).AspectGetter = delegate (object x) { return ((StaffBranch)x).ExcelentCount; };
                    tlvAnaliza.GetColumn(5).AspectGetter = delegate (object x) { return ((StaffBranch)x).VeryGoodCount; };
                    tlvAnaliza.GetColumn(6).AspectGetter = delegate (object x) { return ((StaffBranch)x).GoodCount; };
                    tlvAnaliza.GetColumn(7).AspectGetter = delegate (object x) { return ((StaffBranch)x).SufficientCount; };
                    tlvAnaliza.GetColumn(8).AspectGetter = delegate (object x) { return ((StaffBranch)x).PassedCount; };
                    tlvAnaliza.GetColumn(9).AspectGetter = delegate (object x) { return ((StaffBranch)x).FailedCount; };
                    break;
                case 1:
                    tlvAnaliza.GetColumn(2).AspectGetter = delegate (object x) { return ((StaffBranch)x).TotalScoreCountByPercent+"%"; };
                    tlvAnaliza.GetColumn(3).AspectGetter = delegate (object x) { return ((StaffBranch)x).UnclassifiedCountByPercent+"%"; };
                    tlvAnaliza.GetColumn(4).AspectGetter = delegate (object x) { return ((StaffBranch)x).ExcelentCountByPercent + "%"; };
                    tlvAnaliza.GetColumn(5).AspectGetter = delegate (object x) { return ((StaffBranch)x).VeryGoodCountByPercent + "%"; };
                    tlvAnaliza.GetColumn(6).AspectGetter = delegate (object x) { return ((StaffBranch)x).GoodCountByPercent + "%"; };
                    tlvAnaliza.GetColumn(7).AspectGetter = delegate (object x) { return ((StaffBranch)x).SufficientCountByPercent + "%"; };
                    tlvAnaliza.GetColumn(8).AspectGetter = delegate (object x) { return ((StaffBranch)x).PassedCountByPercent + "%"; };
                    tlvAnaliza.GetColumn(9).AspectGetter = delegate (object x) { return ((StaffBranch)x).FailedCountByPercent + "%"; };
                    break;
                case 2:
                    tlvAnaliza.GetColumn(2).AspectGetter = delegate (object x) { return ((StaffBranch)x).TotalScoreCount + " (" + ((StaffBranch)x).TotalScoreCountByPercent + "%)"; };
                    tlvAnaliza.GetColumn(3).AspectGetter = delegate (object x) { return ((StaffBranch)x).UnclassifiedCount + " (" + ((StaffBranch)x).UnclassifiedCountByPercent + "%)"; };
                    tlvAnaliza.GetColumn(4).AspectGetter = delegate (object x) { return ((StaffBranch)x).ExcelentCount + " (" + ((StaffBranch)x).ExcelentCountByPercent + "%)"; };
                    tlvAnaliza.GetColumn(5).AspectGetter = delegate (object x) { return ((StaffBranch)x).VeryGoodCount + " (" + ((StaffBranch)x).VeryGoodCountByPercent + "%)"; };
                    tlvAnaliza.GetColumn(6).AspectGetter = delegate (object x) { return ((StaffBranch)x).GoodCount + " (" + ((StaffBranch)x).GoodCountByPercent + "%)"; };
                    tlvAnaliza.GetColumn(7).AspectGetter = delegate (object x) { return ((StaffBranch)x).SufficientCount + " (" + ((StaffBranch)x).SufficientCountByPercent + "%)"; };
                    tlvAnaliza.GetColumn(8).AspectGetter = delegate (object x) { return ((StaffBranch)x).PassedCount + " (" + ((StaffBranch)x).PassedCountByPercent + "%)"; };
                    tlvAnaliza.GetColumn(9).AspectGetter = delegate (object x) { return ((StaffBranch)x).FailedCount + " (" + ((StaffBranch)x).FailedCountByPercent + "%)"; };
                    break;
            }
        }
        
        private void rbLiczbowo_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == false) return;
            byte opcja=0;
            byte.TryParse(((RadioButton)sender).Tag.ToString(), out opcja);
            SetAspectGetter(opcja);
            tlvAnaliza.Refresh();
        }

        private void RefreshData()
        {
            var bwFetchData = new BackgroundWorker();
            bwFetchData.DoWork -= bwFetchData_DoWork;
            bwFetchData.DoWork += bwFetchData_DoWork;
            bwFetchData.RunWorkerCompleted -= bwFetchData_RunWorkerCompleted;
            bwFetchData.RunWorkerCompleted += bwFetchData_RunWorkerCompleted;

            tmRefresh = new Timer { Interval = 500 };
            tmRefresh.Tick -= tmRefresh_tick;
            tmRefresh.Tick += tmRefresh_tick;

            tmRefresh.Start();
            bwFetchData.RunWorkerAsync();
            Wait.ShowDialog();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void bwFetchData_DoWork(Object sender, DoWorkEventArgs e)
        {
            try
            {
                FetchData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        private void FetchData()
        {
            try
            {
                var S = new StatystykaSQL();
                string SqlString;
                SqlString = S.SelectObsada(Settings.Default.IdSchool.ToString(), Settings.Default.SchoolYear, rbOkres.Tag.ToString()=="S"?EndOfSemester:EndOfSchoolYear);
                SqlString += S.SelectLiczbaOcen(Settings.Default.IdSchool.ToString(), Settings.Default.SchoolYear, rbOkres.Tag.ToString());
                SqlString += S.SelectStanKlasy(Settings.Default.IdSchool.ToString(), Settings.Default.SchoolYear, rbOkres.Tag.ToString() == "S" ? EndOfSemester : EndOfSchoolYear);
                SqlString += S.SelectStanKlasyWirtualnej(Settings.Default.IdSchool.ToString(), Settings.Default.SchoolYear, rbOkres.Tag.ToString() == "S" ? EndOfSemester : EndOfSchoolYear);
                SqlString += S.CountGroupMember(Settings.Default.IdSchool.ToString(), Settings.Default.SchoolYear);

                using (var R = new MySqlCommand(SqlString, conn).ExecuteReader())
                {
                    lstObsada.Clear();
                    while (R.Read())
                    {
                        lstObsada.Add(new SubjectStaff { Class = new StaffUnit { ID = R.GetInt32("Klasa"),Nazwa=R.GetString("Nazwa_Klasy") },
                                                        Subject=new SubjectUnit { ID = R.GetInt32("IdPrzedmiot"), Nazwa = R.GetString("Przedmiot"), IdSzkolaPrzedmiot=R.GetInt32("IdSzkolaPrzedmiot")},
                                                        Teacher = new StaffUnit { ID = R.GetInt32("IdNauczyciel"), Nazwa = R.GetString("Nauczyciel") }, IsVirtual=R.GetBoolean("Virtual")
                                                        }
                                    );
                    }
                    R.NextResult();
                    
                    lstLiczbaOcen.Clear();
                    while (R.Read())
                    {
                        lstLiczbaOcen.Add(new ScoreInfo { ScoreCount = R.GetInt32("LiczbaOcen"), ScoreWeight = (int)R.GetFloat("Waga"), ClassID = R.GetInt32("Klasa"), SubjectID = R.GetInt32("IdPrzedmiot"), TeacherID = R.GetInt32("Nauczyciel") });
                    }
                    R.NextResult();

                    lstLiczbaUczniow.Clear();
                    while (R.Read())
                    {
                        lstLiczbaUczniow.Add(new StudentCount { ClassID=R.GetInt32("Klasa"), Count=R.GetInt32("StanKlasy") });
                    }
                    R.NextResult();

                    lstLiczbaUczniowNI.Clear();
                    while (R.Read())
                    {
                        lstLiczbaUczniowNI.Add(new VirtualStudentCount { ClassID=R.GetInt32("Klasa"), Count=R.GetInt32("StanKlasy"), SubjectID=R.GetInt32("IdPrzedmiot"), VirtualClassID=R.GetInt32("KlasaWirtualna") });
                    }
                    R.NextResult();

                    lstLiczbaUczniowGrupa.Clear();
                    while (R.Read())
                    {
                        lstLiczbaUczniowGrupa.Add(new SubjectGroupCount { ClassID = R.GetInt32("Klasa"), Count = R.GetInt32("StanGrupy"), SubjectID = R.GetInt32("IdPrzedmiot"),  SubjectIdBySchool = R.GetInt32("IdSzkolaPrzedmiot") });
                    }
                }
                
            }
            catch (Exception)
            { 
                throw;
            }
        }

        private void bwFetchData_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
        {
            Wait.Close();
            tmRefresh.Stop();
            if (rbTyp == null)
            {
                rbNauczyciel.Checked = true;
            }
            else
            {
                rbNauczyciel_CheckedChanged(rbTyp, null);
            }
        }
        private void tmRefresh_tick(Object sender, EventArgs e)
        {
            Wait.Refresh();
        }
        private void GetData(TreeListView olv)
        {
            try
            {
                pbrProgress.Visible = true;
                olv.BeginUpdate();
                olv.Items.Clear();
                //var lstAnalysis = GetAnalysisTree();
                olv.CanExpandGetter = delegate (object SB) { return ((StaffBranch)SB).Children.Count > 0; };
                olv.ChildrenGetter = delegate (object SB) { return ((StaffBranch)SB).Children; };
                olv.Roots = GetAnalysisTree();
                olv.Expand(olv.TreeModel.GetNthObject(0));
                olv.EndUpdate();
                lblRecord.Text = "0 z " + olv.Items.Count;
                olv.Enabled = olv.Items.Count > 0 ? true : false;
                pbrProgress.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private List<StaffBranch> GetAnalysisTree()
        {
            List<StaffBranch> AnalysisTree = new List<StaffBranch>();
            var SchoolNode = new StaffBranch { Label = Settings.Default.SchoolName };
            SchoolNode.Children = GetAnalysis();
            SchoolNode.StudentCount = SchoolNode.Children.Sum(x => x.StudentCount);
            SchoolNode.ExcelentCount = SchoolNode.Children.Sum(x => x.ExcelentCount);
            SchoolNode.VeryGoodCount = SchoolNode.Children.Sum(x => x.VeryGoodCount);
            SchoolNode.GoodCount = SchoolNode.Children.Sum(x => x.GoodCount);
            SchoolNode.SufficientCount = SchoolNode.Children.Sum(x => x.SufficientCount);
            SchoolNode.PassedCount = SchoolNode.Children.Sum(x => x.PassedCount);
            SchoolNode.FailedCount = SchoolNode.Children.Sum(x => x.FailedCount);
            SchoolNode.UnclassifiedCount = SchoolNode.Children.Sum(x => x.UnclassifiedCount);
            AnalysisTree.Add(SchoolNode);
            
            return AnalysisTree;
        }
        private List<StaffBranch> GetAnalysis()
        {
            if (((RadioButton)rbTyp).Name == rbNauczyciel.Name)
            {
                return GetAnalysisByTeacher();
            }
            else
            {
                return GetAnalysisBySubject();
            }
        }
        /// <summary>
        /// Set of methods providing analysis by teacher
        /// </summary>
        /// <returns></returns>
        #region Get tree analysis by teacher
        /// <summary>
        /// Get analysis by teacher
        /// </summary>
        /// <returns>List of teacher's nodes </returns>
        private List<StaffBranch> GetAnalysisByTeacher()
        {
            List<StaffBranch> AnalysisTree=new List<StaffBranch>();
            foreach (var Belfer in lstObsada.Select(x => new { x.Teacher.ID, x.Teacher.Nazwa }).Distinct().OrderBy(x => x.Nazwa).ToList())
            {
                TeacherUnit Teacher = new TeacherUnit { ID = Belfer.ID, Nazwa = Belfer.Nazwa };
                var TeacherNode = new StaffBranch { Label = Teacher.Nazwa };
                TeacherNode.Children = GetAnalysisBySubject(Teacher);
                TeacherNode.StudentCount = TeacherNode.Children.Sum(x => x.StudentCount);
                TeacherNode.ExcelentCount = TeacherNode.Children.Sum(x => x.ExcelentCount);
                TeacherNode.VeryGoodCount = TeacherNode.Children.Sum(x => x.VeryGoodCount);
                TeacherNode.GoodCount = TeacherNode.Children.Sum(x => x.GoodCount);
                TeacherNode.SufficientCount = TeacherNode.Children.Sum(x => x.SufficientCount);
                TeacherNode.PassedCount = TeacherNode.Children.Sum(x => x.PassedCount);
                TeacherNode.FailedCount = TeacherNode.Children.Sum(x => x.FailedCount);
                TeacherNode.UnclassifiedCount = TeacherNode.Children.Sum(x => x.UnclassifiedCount);
                AnalysisTree.Add(TeacherNode);
            }
            return AnalysisTree;
        }
        /// <summary>
        /// Get analysis by subject for pointed teacher
        /// </summary>
        /// <param name="Teacher">Teacher's data consists of ID and Name</param>
        /// <returns>List of subject nodes for pointed teacher</returns>
        private List<StaffBranch> GetAnalysisBySubject(StaffUnit Teacher)
        {
            var lstSubject = lstObsada.Where(x => x.Teacher.ID == Teacher.ID).Select(x => new { x.Subject.ID, x.Subject.Nazwa }).Distinct().ToList();
            List<StaffBranch> AnalysisTree = new List<StaffBranch>();
            foreach (var Przedmiot in lstSubject)
            {
                StaffUnit Subject = new StaffUnit { ID = Przedmiot.ID, Nazwa = Przedmiot.Nazwa };
                var SubjectNode = new StaffBranch { Label = Przedmiot.Nazwa };
                SubjectNode.Children = GetAnalysisByClass(Teacher, Subject);
                SubjectNode.StudentCount = SubjectNode.Children.Sum(x => x.StudentCount);
                SubjectNode.ExcelentCount = SubjectNode.Children.Sum(x => x.ExcelentCount);
                SubjectNode.VeryGoodCount = SubjectNode.Children.Sum(x => x.VeryGoodCount);
                SubjectNode.GoodCount = SubjectNode.Children.Sum(x => x.GoodCount);
                SubjectNode.SufficientCount = SubjectNode.Children.Sum(x => x.SufficientCount);
                SubjectNode.PassedCount = SubjectNode.Children.Sum(x => x.PassedCount);
                SubjectNode.FailedCount = SubjectNode.Children.Sum(x => x.FailedCount);
                SubjectNode.UnclassifiedCount = SubjectNode.Children.Sum(x => x.UnclassifiedCount);
                AnalysisTree.Add(SubjectNode);
            }
            return AnalysisTree;
        }
        #endregion

        #region Get tree analysis by subject
        private List<StaffBranch> GetAnalysisBySubject()
        {
            List<StaffBranch> AnalysisTree = new List<StaffBranch>();
            foreach (var Przedmiot in lstObsada.Select(x => new { x.Subject.ID, x.Subject.Nazwa }).Distinct().ToList())
            {
                SubjectUnit Subject = new SubjectUnit { ID = Przedmiot.ID, Nazwa = Przedmiot.Nazwa };
                var SubjectNode = new StaffBranch { Label = Przedmiot.Nazwa };
                SubjectNode.Children = GetAnalysisByTeacher(Subject);
                SubjectNode.StudentCount = SubjectNode.Children.Sum(x => x.StudentCount);
                SubjectNode.ExcelentCount = SubjectNode.Children.Sum(x => x.ExcelentCount);
                SubjectNode.VeryGoodCount = SubjectNode.Children.Sum(x => x.VeryGoodCount);
                SubjectNode.GoodCount = SubjectNode.Children.Sum(x => x.GoodCount);
                SubjectNode.SufficientCount = SubjectNode.Children.Sum(x => x.SufficientCount);
                SubjectNode.PassedCount = SubjectNode.Children.Sum(x => x.PassedCount);
                SubjectNode.FailedCount = SubjectNode.Children.Sum(x => x.FailedCount);
                SubjectNode.UnclassifiedCount = SubjectNode.Children.Sum(x => x.UnclassifiedCount);
                AnalysisTree.Add(SubjectNode);
            }
            return AnalysisTree;
        }
        /// <summary>
        /// Get analysis by teacher for pointed subject
        /// </summary>
        /// <param name="Subject">Subject's data consists of ID and Name</param>
        /// <returns>List of teacher nodes for pointed subject</returns>
        private List<StaffBranch> GetAnalysisByTeacher(StaffUnit Subject)
        {
            var lstTeacher = lstObsada.Where(x => x.Subject.ID == Subject.ID).Select(x => new { x.Teacher.ID, x.Teacher.Nazwa }).Distinct().ToList();
            List<StaffBranch> AnalysisTree = new List<StaffBranch>();
            foreach (var Belfer in lstTeacher)
            {
                StaffUnit Teacher = new StaffUnit { ID = Belfer.ID, Nazwa = Belfer.Nazwa };
                var TeacherNode = new StaffBranch { Label = Belfer.Nazwa };
                TeacherNode.Children = GetAnalysisByClass(Teacher, Subject);
                TeacherNode.StudentCount = TeacherNode.Children.Sum(x => x.StudentCount);
                TeacherNode.ExcelentCount = TeacherNode.Children.Sum(x => x.ExcelentCount);
                TeacherNode.VeryGoodCount = TeacherNode.Children.Sum(x => x.VeryGoodCount);
                TeacherNode.GoodCount = TeacherNode.Children.Sum(x => x.GoodCount);
                TeacherNode.SufficientCount = TeacherNode.Children.Sum(x => x.SufficientCount);
                TeacherNode.PassedCount = TeacherNode.Children.Sum(x => x.PassedCount);
                TeacherNode.FailedCount = TeacherNode.Children.Sum(x => x.FailedCount);
                TeacherNode.UnclassifiedCount = TeacherNode.Children.Sum(x => x.UnclassifiedCount);
                AnalysisTree.Add(TeacherNode);
            }
            return AnalysisTree;
        }
        #endregion
        #region Common analysis for both teacher and subject
        private List<StaffBranch> GetAnalysisByClass(StaffUnit Teacher,StaffUnit Subject)
        {
            List<StaffBranch> ClassNode = new List<StaffBranch>();
            var lstClass = lstObsada.Where(T => T.Teacher.ID == Teacher.ID).Where(S => S.Subject.ID == Subject.ID).Select(x => new { x.Class.ID, x.Class.Nazwa, x.IsVirtual }).Distinct().OrderBy(K => K.Nazwa).ToList();
            foreach (var Klasa in lstClass)
            {
                ClassNode.Add(GetClassAnalysis(Teacher, Subject, new StaffUnit { ID = Klasa.ID, Nazwa = Klasa.Nazwa }, Klasa.IsVirtual));
            }
            return ClassNode;
        }
        private StaffBranch GetClassAnalysis(StaffUnit Teacher, StaffUnit Subject, StaffUnit Class, bool IsVirtual)
        {
            int StanKlasy, StanKlasyWirtualnej;
                if (!IsVirtual)
                {
                    StanKlasy = lstLiczbaUczniow.Where(x => x.ClassID == Class.ID).Select(x => x.Count).FirstOrDefault();
                    StanKlasyWirtualnej = lstLiczbaUczniowNI.Where(x => x.SubjectID == Subject.ID).Where(x => x.ClassID == Class.ID).Select(x => x.Count).FirstOrDefault();
                    StanKlasy -= StanKlasyWirtualnej;
                if (lstLiczbaUczniowGrupa.Where(x => x.ClassID == Class.ID).Where(x => x.SubjectID == Subject.ID).Count() > 0)
                {
                    var SubtractList = lstObsada.Where(T => T.Teacher.ID == Teacher.ID).Where(S => S.Subject.ID == Subject.ID).Where(C => C.Class.ID == Class.ID).Select(x => x.Subject.IdSzkolaPrzedmiot);
                    StanKlasy -= lstLiczbaUczniowGrupa.Where(x => x.ClassID == Class.ID).Where(x => x.SubjectID == Subject.ID).Where(x => !SubtractList.Contains(x.SubjectIdBySchool)).Sum(x => x.Count);                    
                }

                }
                else
                {
                    StanKlasy = lstLiczbaUczniowNI.Where(x => x.SubjectID == Subject.ID).Where(x => x.VirtualClassID == Class.ID).Select(x => x.Count).FirstOrDefault();
                }
            List<int> ScoreCount = ComputeScore(Teacher, Subject, Class);
            return new StaffBranch { Label = Class.Nazwa, ExcelentCount = ScoreCount[6], FailedCount = ScoreCount[1], GoodCount = ScoreCount[4], PassedCount = ScoreCount[2], StudentCount = StanKlasy, SufficientCount = ScoreCount[3], UnclassifiedCount = ScoreCount[0], VeryGoodCount = ScoreCount[5] };
        }
        private List<int> ComputeScore(StaffUnit Teacher,StaffUnit Subject, StaffUnit Class)
        {
            List<int> ScoreCount = new List<int>();
            List<ScoreInfo> lstScoreCount = lstLiczbaOcen.Where(SI => SI.TeacherID == Teacher.ID).Where(SI=>SI.SubjectID==Subject.ID).Where(SI=>SI.ClassID==Class.ID).ToList();
            foreach (var S in new List<int> { 0, 1, 2, 3, 4, 5, 6 })
            {
                int Count = lstScoreCount.Where(W => W.ScoreWeight == S).Sum(x => x.ScoreCount);
                ScoreCount.Add(Count);
            }
            return ScoreCount;
        }
        #endregion
               
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlvAnaliza_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                lblRecord.Text = e.ItemIndex + 1 + " z " + e.Item.ListView.Items.Count;
            }
            else
            {
                lblRecord.Text = "0 z " + e.Item.ListView.Items.Count;
            }
        }

        private void txtSeek_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tlvAnaliza.CollapseAll();
                tlvAnaliza.ExpandAll();
                tlvAnaliza.ModelFilter = new ModelFilter(x => ((StaffBranch)x).Label.StartsWith(txtSeek.Text, StringComparison.CurrentCultureIgnoreCase));

                lblRecord.Text = "0 z " + tlvAnaliza.GetItemCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }
        private void tlvAnaliza_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 2:
                    e.Text = ((StaffBranch)e.Model).TotalScoreCountByPercent.ToString() + "%";
                    break;
                case 3:
                    e.Text = ((StaffBranch)e.Model).UnclassifiedCountByPercent.ToString()+"%";
                    break;
                case 4:
                    e.Text = ((StaffBranch)e.Model).ExcelentCountByPercent.ToString() + "%";
                    break;
                case 5:
                    e.Text = ((StaffBranch)e.Model).VeryGoodCountByPercent.ToString() + "%";
                    break;
                case 6:
                    e.Text = ((StaffBranch)e.Model).GoodCountByPercent.ToString() + "%";
                    break;
                case 7:
                    e.Text = ((StaffBranch)e.Model).SufficientCountByPercent.ToString() + "%";
                    break;
                case 8:
                    e.Text = ((StaffBranch)e.Model).PassedCountByPercent.ToString() + "%";
                    break;
                case 9:
                    e.Text = ((StaffBranch)e.Model).FailedCountByPercent.ToString() + "%";
                    break;
                case 10:
                    e.Text = ((StaffBranch)e.Model).AvgScore.ToString();
                    break;
                default:
                    break;
            }
        }
        #region ---------------------------------- Printing ------------------------------------------
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            dlgPrintPreview dlgPrint = new dlgPrintPreview();
            dlgPrint.rbHorizontal.Checked = true;
            dlgPrint.PreviewModeChanged += PreviewModeChanged;
            NewRow += dlgPrint.NewRow;
            dlgPrint.Doc.BeginPrint += PrnDoc_BeginPrint;
            dlgPrint.Doc.PrintPage += tlvAnaliza_PrintPage;
            ReportHeader = new List<string> { "Zbiorcza analiza wyników nauczania" };
            dlgPrint.ShowDialog();
        }
        private void PrnDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            PageNumber = 0;
            if (e.PrintAction == PrintAction.PrintToPrinter) IsPreview = false; else IsPreview = true;
        }

        private void PreviewModeChanged(bool PreviewMode) { IsPreview = PreviewMode; }

        private void tlvAnaliza_PrintPage(object sender, PrintPageEventArgs e)
        {
            #region -------------------------- Print variables definitions -----------------------
            PH.G = e.Graphics;
            float x = IsPreview ? Settings.Default.LeftMargin : Settings.Default.LeftMargin - e.PageSettings.PrintableArea.Left;
            float y = IsPreview ? Settings.Default.TopMargin : Settings.Default.TopMargin - e.PageSettings.PrintableArea.Top;
            Font TextFont = Settings.Default.TextFont;
            Font SubHeaderFont = Settings.Default.SubHeaderFont;
            Font HeaderFont = Settings.Default.HeaderFont;
            float LineHeight = TextFont.GetHeight(e.Graphics);
            float SubHeaderLineHeight = SubHeaderFont.GetHeight(e.Graphics);
            float HeaderLineHeight = HeaderFont.GetHeight(e.Graphics);
            int PrintWidth = e.MarginBounds.Width;
            int PrintHeight = e.MarginBounds.Bottom;
            #endregion

            #region --------------------------- Document header and footer -----------------------
            PH.DrawHeader(x, y, PrintWidth);
            PH.DrawFooter(x, PrintHeight, PrintWidth);
            PageNumber += 1;

            PH.DrawPageNumber("- " + PageNumber.ToString() + " -", x, PrintHeight, PrintWidth, CustomType.PageNumberLocation.Footer);
            if (PageNumber == 1)
            {
                y += LineHeight;
                PH.DrawText(ReportHeader[0], HeaderFont, x, y, PrintWidth, HeaderLineHeight, 1, Brushes.Black, false);
                y += HeaderLineHeight * 2;
            }
            #endregion

            #region ------------------------------- Column Settings --------------------------
            List<TableCell> Kolumna = new List<TableCell>();
            foreach (OLVColumn Col in tlvAnaliza.Columns)
            {
                Kolumna.Add(new TableCell { Name = Col.AspectName, Label = Col.Text, Size = Col.Width });
            }
            int FirstColWidth = PrintWidth - Kolumna.Where(K => K.Name != "Label").Sum(K => K.Size);
            if (FirstColWidth > 0) Kolumna.Where(K => K.Name == "Label").First().Size = FirstColWidth;
            #endregion

            #region ------------------------------- Table header --------------------------------
            float MultiplyLine = 5;
            int ColOffset = 0;
            if (PageNumber == 1)
            {
                for (int i = 0; i < Kolumna.Count()-3; i++)
                {
                    PH.DrawText(Kolumna[i].Label, new Font(TextFont, FontStyle.Bold), x + ColOffset, y, Kolumna[i].Size, LineHeight * MultiplyLine, 1, Brushes.Black);
                    ColOffset += Kolumna[i].Size;
                }
                for (int i = Kolumna.Count() - 3; i < Kolumna.Count(); i++)
                {
                    PH.DrawText(Kolumna[i].Label, new Font(TextFont, FontStyle.Bold), x + ColOffset, y, Kolumna[i].Size, LineHeight * MultiplyLine, 0, Brushes.Black,270);
                    ColOffset += Kolumna[i].Size;
                }
                y += LineHeight * MultiplyLine;
            }
            ColOffset = 0;
            for (int i = 0; i < Kolumna.Count(); i++)
            {
                PH.DrawText((i+1).ToString(), new Font(TextFont, FontStyle.Bold | FontStyle.Italic), x + ColOffset, y, Kolumna[i].Size, LineHeight, 1, Brushes.Black);
                ColOffset += Kolumna[i].Size;
            }
            y += LineHeight * 1.5F;
            #endregion
            
            #region ------------------------ Table body --------------------------------
            MultiplyLine = 3;
            StaffBranch Level0 = (StaffBranch)tlvAnaliza.GetModelObject(0);
            if (Offset[0] == 0)
            {
                PrintModelObjectData(Level0, x, ref y, Kolumna, new Font(TextFont, FontStyle.Bold), LineHeight * MultiplyLine, 0, true);
                y += LineHeight * 0.5F;
            }
            int Indent = 20;

            MultiplyLine = rbLiczbowoProcentowo.Checked ? 2 : 0;
            while (y + LineHeight * (4.5f + MultiplyLine) < PrintHeight && Offset[0] < Level0.Children.Count)
            {
                //MultiplyLine = 2;
                StaffBranch Level1 = Level0.Children[Offset[0]];
                if (Offset[1] == 0 & Offset[2] == 0)
                {
                    PrintModelObjectData(Level1, x, ref y, Kolumna, new Font(TextFont, FontStyle.Bold), LineHeight * MultiplyLine, Indent, true);
                    y += LineHeight * 0.5F;
                }

                MultiplyLine = rbLiczbowoProcentowo.Checked ? 2 : 1;
                while (y + LineHeight * 2 * MultiplyLine < PrintHeight && Offset[1] < Level1.Children.Count)
                {
                    StaffBranch Level2 = Level1.Children[Offset[1]];
                    if (Offset[2] == 0) PrintModelObjectData(Level2, x, ref y, Kolumna, new Font(TextFont, FontStyle.Bold), LineHeight * MultiplyLine, Indent * 2, true);
                    
                    while (y + LineHeight * MultiplyLine < PrintHeight && Offset[2] < Level2.Children.Count)
                    {
                        StaffBranch Level3 = Level2.Children[Offset[2]];
                        PrintModelObjectData(Level3, x, ref y, Kolumna, TextFont, LineHeight * MultiplyLine, Indent * 3);
                        Offset[2] += 1;
                    }
                    if (Offset[2] < Level2.Children.Count)
                    {
                        e.HasMorePages = true;
                        NewRow?.Invoke();
                        return;
                    }
                    else
                    {
                        y += LineHeight * 0.5f;
                        Offset[2] = 0;
                        Offset[1] += 1;
                    }
                }
                if (Offset[1] < Level1.Children.Count)
                {
                    e.HasMorePages = true;
                    NewRow?.Invoke();
                    return;
                }
                else
                {
                    Offset[1] = 0;
                    Offset[0] += 1;
                }
            }
            if (Offset[0] < ((StaffBranch)tlvAnaliza.GetModelObject(0)).Children.Count)
            {
                e.HasMorePages = true;
                NewRow?.Invoke();
            }
            else
            {
                Offset[0] = 0;
            }
            #endregion
        }
        private void PrintModelObjectData(StaffBranch Node, float x, ref float y, List<TableCell> Kolumna, Font PrintFont, float LineHeight, int TabIndent=0, bool FillBackground=false)
        {
            List<string> AspectToPrint=new List<string>();
            AspectToPrint.Add(Node.Label);
            AspectToPrint.Add(Node.StudentCount.ToString());
            if (rbLiczbowo.Checked)
            {
                AspectToPrint.Add(Node.TotalScoreCount.ToString());
                AspectToPrint.Add(Node.UnclassifiedCount.ToString());
                AspectToPrint.Add(Node.ExcelentCount.ToString());
                AspectToPrint.Add(Node.VeryGoodCount.ToString());
                AspectToPrint.Add(Node.GoodCount.ToString());
                AspectToPrint.Add(Node.SufficientCount.ToString());
                AspectToPrint.Add(Node.PassedCount.ToString());
                AspectToPrint.Add(Node.FailedCount.ToString());
            }
            else if (rbProcentowo.Checked)
            {
                AspectToPrint.Add(Node.TotalScoreCountByPercent.ToString() + "%");
                AspectToPrint.Add(Node.UnclassifiedCountByPercent.ToString()+ "%") ;
                AspectToPrint.Add(Node.ExcelentCountByPercent.ToString() + "%");
                AspectToPrint.Add(Node.VeryGoodCountByPercent.ToString() + "%");
                AspectToPrint.Add(Node.GoodCountByPercent.ToString() + "%");
                AspectToPrint.Add(Node.SufficientCountByPercent.ToString() + "%");
                AspectToPrint.Add(Node.PassedCountByPercent.ToString() + "%");
                AspectToPrint.Add(Node.FailedCountByPercent.ToString() + "%");
            }
            else
            {
                AspectToPrint.Add(Node.TotalScoreCount.ToString() + "\n(" + Node.TotalScoreCountByPercent.ToString() + "%)");
                AspectToPrint.Add(Node.UnclassifiedCount.ToString() + "\n(" + Node.UnclassifiedCountByPercent.ToString() + "%)");
                AspectToPrint.Add(Node.ExcelentCount.ToString() + "\n(" + Node.ExcelentCountByPercent.ToString() + "%)");
                AspectToPrint.Add(Node.VeryGoodCount.ToString() + "\n(" + Node.VeryGoodCountByPercent.ToString() + "%)");
                AspectToPrint.Add(Node.GoodCount.ToString() + "\n(" + Node.GoodCountByPercent.ToString() + "%)");
                AspectToPrint.Add(Node.SufficientCount.ToString() + "\n(" + Node.SufficientCountByPercent.ToString() + "%)");
                AspectToPrint.Add(Node.PassedCount.ToString() + "\n(" + Node.PassedCountByPercent.ToString() + "%)");
                AspectToPrint.Add(Node.FailedCount.ToString() + "\n(" + Node.FailedCountByPercent.ToString() + "%)");
            }
            AspectToPrint.Add(Node.AvgScore.ToString());
            AspectToPrint.Add(Node.MedianScore.ToString());
            AspectToPrint.Add(Node.DominantScore.ToString());
            PH.DrawText(AspectToPrint[0], PrintFont, x + TabIndent, y, Kolumna[0].Size - TabIndent, LineHeight, 0, Brushes.Black, true, false, FillBackground);
            x += Kolumna[0].Size;

            for (int i = 1; i < AspectToPrint.Count; i++)
            {
                PH.DrawText(AspectToPrint[i], PrintFont, x, y, Kolumna[i].Size, LineHeight, 1, Brushes.Black, true, false, FillBackground);
                x += Kolumna[i].Size;
            }
            y += LineHeight;
        }

        #endregion
        /// <summary>
        /// Klasy prywatne do użytku wewnętrznego
        /// </summary>
        #region --------------------------------- Klasy prywatne na użytek analizy --------------------------
        private class StaffUnit
        {
            public int ID { get; set; }
            public string Nazwa { get; set; }
        }
        private class TeacherUnit : StaffUnit { }
        private class SubjectUnit : StaffUnit
        {
            public int IdSzkolaPrzedmiot { get; set; }
        }
        private class SubjectStaff
        {
            public StaffUnit Class { get; set; }
            public SubjectUnit Subject { get; set; }
            public StaffUnit Teacher { get; set; }
            public bool IsVirtual { get; set; }
        }
        private class ScoreInfo
        {
            public int ScoreWeight { get; set; }
            public int ScoreCount { get; set; }
            public int ClassID { get; set; }
            public int SubjectID { get; set; }
            public int TeacherID { get; set; }
        }

        private class StudentCount
        {
            public int ClassID { get; set; }
            public int Count { get; set; }
        }

        private class VirtualStudentCount : StudentCount
        {
            public int VirtualClassID { get; set; }
            public int SubjectID { get; set; }
        }
        private class SubjectGroupCount : StudentCount
        {
            public int SubjectID { get; set; }
            public int SubjectIdBySchool { get; set; }
        }
 
        private class StaffBranch
        {

            private int[] scorecount=new int[7];
            public List<StaffBranch> Children = new List<StaffBranch>();

            public string Label { get; set; }
            public int StudentCount { get; set; }
            public float AvgScore
            {
                get
                {
                    int Total=0;
                    for (int i = 1; i < scorecount.Length; i++)
                    {
                        Total += i * scorecount[i];
                    }
                    if (Total==0) return 0;
                    return (float)Math.Round((double)Total / TotalScoreCount, 2);
                }
            }
            public float MedianScore
            {
                get
                {
                    List<int> Numbers = new List<int>();
                    for (int i = 1; i < scorecount.Length; i++)
                    {
                        for (int j = 0; j < scorecount[i]; j++)
                        {
                            Numbers.Add(i);
                        }
                    }
                    if (Numbers.Count == 0) return 0;
                    int HalfIndex = Numbers.Count() / 2;
                    if (Numbers.Count() % 2 == 0)
                    {
                        return (float)(Numbers[HalfIndex] + Numbers[HalfIndex - 1]) / 2;
                    }
                    else
                    {
                        return Numbers[HalfIndex];
                    }
                }
            }
            public float DominantScore
            {
                get
                {
                    int modal = 1;
                    for (int i = 2; i < scorecount.Length; i++)
                    {
                        if (scorecount[i]>scorecount[modal])
                        {
                            modal = i;
                        }
                    }
                    return modal;
                }
            }
            public float TotalScoreCountByPercent
            {
                get
                {
                    return (float)Math.Round(TotalScoreCount * (double)100 / StudentCount, 2);
                }
            }
            public int TotalScoreCount
            {
                get
                {
                    return scorecount.Skip(1).Sum();
                }
            }            
            public float UnclassifiedCountByPercent
            {
                get
                {
                    return (float)Math.Round(scorecount[0] * (double)100 / TotalScoreCount, 2);
                }
            }

            public int UnclassifiedCount
            {
                get
                {
                    return scorecount[0];
                }
                set
                {
                    scorecount[0] = value;
                }
            }
            public float ExcelentCountByPercent
            {
                get
                {
                    return (float)Math.Round(scorecount[6] * (double)100 / TotalScoreCount, 2);
                }
            }
            public int ExcelentCount
            {
                get
                {
                    return scorecount[6];
                }
                set
                {
                    scorecount[6] = value;
                }
            }
            public float VeryGoodCountByPercent
            {
                get
                {
                    return (float)Math.Round(scorecount[5] * (double)100 / TotalScoreCount, 2);
                }
            }
            public int VeryGoodCount
            {
                get
                {
                    return scorecount[5];
                }
                set
                {
                    scorecount[5] = value;
                }
            }
            public float GoodCountByPercent
            {
                get
                {
                    return (float)Math.Round(scorecount[4] * (double)100 / TotalScoreCount, 2);
                }
            }
            public int GoodCount
            {
                get
                {
                    return scorecount[4];
                }
                set
                {
                    scorecount[4] = value;
                }
            }
            public float SufficientCountByPercent
            {
                get
                {
                    return (float)Math.Round(scorecount[3] * (double)100 / TotalScoreCount, 2);
                }
            }
            public int SufficientCount
            {
                get
                {
                    return scorecount[3];
                }
                set
                {
                    scorecount[3] = value;
                }
            }
            public float PassedCountByPercent
            {
                get
                {
                    return (float)Math.Round(scorecount[2] * (double)100 / TotalScoreCount, 2);
                }
            }
            public int PassedCount
            {
                get
                {
                    return scorecount[2];
                }
                set
                {
                    scorecount[2] = value;
                }
            }
            public float FailedCountByPercent
            {
                get
                {
                    return (float)Math.Round(scorecount[1] * (double)100 / TotalScoreCount, 2);
                }
            }
            public int FailedCount
            {
                get
                {
                    return scorecount[1];
                }
                set
                {
                    scorecount[1] = value;
                }
            }            
        }
        #endregion
        

    }

}
