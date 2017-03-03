using System;
using System.Drawing;
using MySql.Data.MySqlClient;
using BelferCS.Properties;
using System.Linq;
namespace BelferCS
{
    public static class CustomType
    {
        public enum PageNumberLocation { Header = 0, Footer = 1 }
        public enum AnalysisOption { ByNumber, ByPercent, ByBoth }
    }

    /// <summary>
    /// Wyzwala zdarzenie informujące otwarte formularze o zmianie parametrów pracy (rok szkolny, szkoła, typ szkoły)
    /// </summary>
    public static class SharedConfiguration
    {
        public delegate void ConfigurationHandler();
        public static event ConfigurationHandler ConfigurationChanged;
        public static void OnConfigurationChanged()
        {
            ConfigurationChanged?.Invoke();
        }
    }
    public class MySQLserver
    {

    }
    /// <summary>
    /// Przechowuje paramtry komórki tabeli
    /// </summary>
    public class TableCell
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public int Size { get; set; }
    }
    /// <summary>
    /// Dostarcza metody umożliwiające uzyskiwanie informacji na podstawie różnorodnych obliczeń
    /// </summary>
    public class CalcHelper
    {
        public DateTime StartDateOfSchoolYear(string SchoolYear)
        {
            if (SchoolYear.Length < 4) { return default(DateTime); }
            int Year = default(int);
            if (int.TryParse(SchoolYear.Substring(0, 4), out Year)) {return new DateTime(Year, 9, 1); } else { return default(DateTime); }
        }
        public DateTime EndDateOfSchoolYear(string SchoolYear)
        {
            if (SchoolYear.Length < 9) { return default(DateTime); }
            int Year = default(int);
            if (int.TryParse(SchoolYear.Substring(5, 4), out Year)) { return new DateTime(Year, 8, 31); } else { return default(DateTime); }
        }
        
        public DateTime StartDateOfSemester2(string SchoolYear,DateTime CurrDate ,MySqlConnection MyConn)
        {
            try
            {
                if (SchoolYear.Length < 9) { return default(DateTime); }
                int Year = DateTime.Now.Year, Month = DateTime.Now.Month, Day = DateTime.Now.Day;
                int.TryParse(SchoolYear.Substring(5, 4), out Year);
                    
                var O = new OpcjeSQL();
                using (MySqlDataReader R = new MySqlCommand { Connection = MyConn, CommandText = O.SelectOption("Semester2StartDate","G",Settings.Default.IdSchool.ToString(),CurrDate)}.ExecuteReader())
                {   
                    if (R.Read())
                    {
                        int.TryParse(R.GetString("Value").Substring(0, 2), out Month);
                        int.TryParse(R.GetString("Value").Substring(2, 2), out Day);
                    }
                }
                return new DateTime(Year, Month, Day);
            }
            catch(MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        DateTime StartDateOfWeek(DateTime Date)
        {
            int Offset= (Date.DayOfWeek == 0 ? 7 : (int)Date.DayOfWeek) - 1;
            return Date.AddDays(-Offset);
        }
        DateTime EndDateOfWeek(DateTime Date)
        {
            int Offset = 7 - (Date.DayOfWeek == 0 ? 7 : (int)Date.DayOfWeek);
            return Date.AddDays(Offset);
        }
        public class Math
        {
            public double Mediana(System.Collections.Generic.IEnumerable<double> Numbers)
            {
                if (Numbers.Count() == 0)
                {
                    throw new InvalidOperationException("Nie można obliczyć mediany z pustego zbioru!");
                }
                var sortedNumbers = Numbers.OrderBy(x=> x).ToArray(); //from number in Numbers orderby number select number;
                int NumberIndex = sortedNumbers.Count() / 2;
                if (sortedNumbers.Count() % 2 == 0)
                {
                    return (float)(sortedNumbers[NumberIndex] + sortedNumbers[NumberIndex - 1]) / 2;
                }
                else
                {
                    return sortedNumbers[NumberIndex];
                }
                
            }
            public float INtoMM(float Inch) { return Inch / (float)3.937; }
            public float MMtoIN(float mm) { return mm * (float)3.937; }
        }
    }
    /// <summary>
    /// Dostarcza metody i właściwości umożliwiające definiowanie przedziału czasu
    /// </summary>
    public class DateRange
    {
        DateTime startdate, enddate;
        /// <summary>
        /// Dostarcza datę początkową aktualnie ustawionego roku szkolnego lub przedziału czasu
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return startdate;
            }
        }
        /// <summary>
        /// Dostarcza datę końcową aktualnie ustawionego roku szkolnego lub przedziału czasu
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return enddate;
            }
        }
        /// <summary>
        /// Ustala datę początkową i końcową bieżącego roku szkolnego
        /// </summary>
        public DateRange()
        {
            CalcHelper CH = new CalcHelper();
            startdate = CH.StartDateOfSchoolYear(Settings.Default.SchoolYear);
            enddate = CH.EndDateOfSchoolYear(Settings.Default.SchoolYear);
        }
        /// <summary>
        /// Ustala datę początkową i końcową danego roku szkolnego
        /// </summary>
        /// <param name="SchoolYear">Rok szkolny, którego granice zostaną ustawione</param>
        public DateRange(string SchoolYear)
        {
            if (SchoolYear.Length < 9) { return; }
            CalcHelper CH = new CalcHelper();
            startdate = CH.StartDateOfSchoolYear(SchoolYear);
            enddate = CH.EndDateOfSchoolYear(SchoolYear);
        }
        /// <summary>
        /// Ustawia datę początkową i końcową danego przedziału czasu
        /// </summary>
        /// <param name="CustomStartDate">Dolna granica przedziału czasu</param>
        /// <param name="CustomEndDate">Górna granica przedziału czasu</param>
        public DateRange(DateTime CustomStartDate,DateTime CustomEndDate)
        {
            startdate = CustomStartDate;
            enddate=CustomEndDate;
        }
    }
    /// <summary>
    /// Dostarcza metody umożliwiające wydruk tekstu
    /// </summary>
    public class PrintHelper
    {
        public Graphics G { get; set; }
        public void DrawText(string Text,Font TextFont, float x, float y, float PrintWidth, float PrintHeight,byte PrintAlignment,Brush FontColor,bool DrawLines=true,bool VerticalLayout=false ,bool FillBackgroud = false)
        {
            x += Settings.Default.XCaliber;
            y+= Settings.Default.YCaliber;
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = (StringAlignment)PrintAlignment;
            strFormat.LineAlignment = StringAlignment.Center;
            if (VerticalLayout) strFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            if (DrawLines) G.DrawRectangle(Pens.Black, x, y, PrintWidth, PrintHeight);
            if (FillBackgroud) G.FillRectangle(Brushes.LightGray, new RectangleF(x, y, PrintWidth, PrintHeight));
            G.DrawString(Text, TextFont, FontColor, new RectangleF(x, y, PrintWidth, PrintHeight), strFormat);
        }
        public void DrawText(string Text, Font TextFont, float x, float y, float PrintWidth, float PrintHeight, byte PrintAlignment, Brush FontColor, int PrintAngle, bool DrawLines = true)
        {
            x += Settings.Default.XCaliber;
            y += Settings.Default.YCaliber;
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = (StringAlignment)PrintAlignment;
            strFormat.LineAlignment = StringAlignment.Center;
            if (DrawLines) G.DrawRectangle(Pens.Black, x, y, PrintWidth, PrintHeight);
            G.TranslateTransform(x, y+PrintHeight, System.Drawing.Drawing2D.MatrixOrder.Append);
            G.RotateTransform(PrintAngle);
            G.DrawString(Text, TextFont, FontColor, new RectangleF(0,0, PrintHeight, PrintWidth), strFormat);
            G.ResetTransform();
        }
        /// <summary>
        /// Umożliwia wydruk tekstu zawijanego do następnego wiersza
        /// </summary>
        /// <param name="PrintText">Text do wydrukowania w formie tablicy znaków</param>
        /// <param name="PrintFont">Czcionka</param>
        /// <param name="WordOffset">Wskaźnik położenia ostatnio wydrukowanego słowa przekazany przez referencję</param>
        /// <param name="x">współrzędna x przekazana przez wartość</param>
        /// <param name="y">Współrzędna y przekazana przez referencję</param>
        /// <param name="PrintWidth">Szerokość obszaru wydruku</param>
        /// <param name="PrintHeight">Wysokość obszaru wydruku</param>
        /// <param name="TextLineHeight">Wysokość linii tekstu</param>
        /// <param name="TabIndent">Wielkość wysunięcia tekstu</param>
        /// <returns></returns>
        public bool DrawWrappedText(string[] PrintText,Font PrintFont,ref int WordOffset, float x, ref float y, float PrintWidth, float PrintHeight, float TextLineHeight,int TabIndent=0)
        {
            System.Text.StringBuilder TextLine = new System.Text.StringBuilder();
            do
            {
                TextLine.Append(String.Concat(PrintText[WordOffset], " "));
                if ((G.MeasureString(TextLine.ToString(),PrintFont).Width + TabIndent) > PrintWidth)
                {
                    TextLine.Remove(TextLine.ToString().Length - PrintText[WordOffset].Length - 1, PrintText[WordOffset].Length);
                    DrawText(TextLine.ToString(), PrintFont, x + TabIndent, y, PrintWidth, TextLineHeight, 0, Brushes.Black, false);
                    y += TextLineHeight;
                    WordOffset -= 1;
                    TextLine = new System.Text.StringBuilder();
                }
                WordOffset += 1;
            } while (PrintText.GetUpperBound(0) >= WordOffset && PrintHeight >= y + TextLineHeight);
            if (PrintHeight >= (y + TextLineHeight))
            {
                DrawText(TextLine.ToString(), PrintFont, x + TabIndent, y, PrintWidth, TextLineHeight, 0, Brushes.Black, false);
                y += TextLineHeight;
                WordOffset = 0;
                return true;
            }
            else return false;
        }
  /// <summary>
  /// Drukuje nr strony na górnym lub dolnym marginesie
  /// </summary>
  /// <param name="PageNumber">Nr strony</param>
  /// <param name="x">współrzędna x</param>
  /// <param name="y">współrzędna y</param>
  /// <param name="PrintWidth">Szerokość obszaru wydruku</param>
  /// <param name="Location">Lokalizacja nr strony (górny lub dolny margines)</param>
  /// <param name="HorizontalAlignment">Poziome położenie nr strony</param>
        public void DrawPageNumber(string PageNumber, float x, float y, float PrintWidth, CustomType.PageNumberLocation Location, byte HorizontalAlignment = 1)
        {
            x += Settings.Default.XCaliber;
            y += Settings.Default.YCaliber;
            var M = new CalcHelper.Math();
            var BaseFont = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point);
            if (Location==CustomType.PageNumberLocation.Header) { y -= M.MMtoIN(5) + BaseFont.GetHeight(G); } else { y += M.MMtoIN(5) + BaseFont.GetHeight(G); }
            var strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Alignment = (StringAlignment)HorizontalAlignment;
            G.DrawString(PageNumber, BaseFont, Brushes.Black, new Rectangle((int)x, (int)y, (int)PrintWidth, (int)BaseFont.GetHeight(G)), strFormat);
        }
        /// <summary>
        /// Drukuje nagłówek dokumentu na górnym marginesie strony
        /// </summary>
        /// <param name="x">Współrzędna początkowa pozioma</param>
        /// <param name="y">Współrzędna początkowa pionowa</param>
        /// <param name="PrintWidth">Szerokość obszaru wydruku</param>
        public void DrawHeader(float x, float y, float PrintWidth)
        {
            x += Settings.Default.XCaliber;
            y += Settings.Default.YCaliber;
            var DotPen = new Pen(Color.Black);
            var M = new CalcHelper.Math();
            var HeaderFont = new Font("Arial", 8, FontStyle.Italic, GraphicsUnit.Point);
            y -= M.MMtoIN(5);
            G.DrawLine(DotPen, x, y, x + PrintWidth, y);
            y -= HeaderFont.GetHeight(G);
            var strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Alignment = StringAlignment.Near;
            G.DrawString(Settings.Default.SchoolName, HeaderFont, Brushes.Black, new Rectangle((int)x, (int)y, (int)PrintWidth, (int)HeaderFont.GetHeight(G)), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            G.DrawString("Rok szkolny: " + Settings.Default.SchoolYear, HeaderFont, Brushes.Black, new Rectangle((int)x, (int)y, (int)PrintWidth, (int)HeaderFont.GetHeight(G)), strFormat);
        }
        /// <summary>
        /// Drukuje stopkę dokumentu na dolnym marginesie strony
        /// </summary>
        /// <param name="x">Współrzędna początkowa pozioma</param>
        /// <param name="y">Współrzędna początkowa pionowa</param>
        /// <param name="PrintWidth">Szerokość obszaru wydruku</param>
        public void DrawFooter(float x, float y, float PrintWidth)
        {
            x += Settings.Default.XCaliber;
            y += Settings.Default.YCaliber;
            var DotPen = new Pen(Color.Black);
            var M = new CalcHelper.Math();
            var FooterFont = new Font("Arial", 8, FontStyle.Italic, GraphicsUnit.Point);
            y += M.MMtoIN(5);
            G.DrawLine(DotPen, x, y, x + PrintWidth, y);
            y += FooterFont.GetHeight(G)/2;
            var strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Alignment = StringAlignment.Near;
            G.DrawString(System.Windows.Forms.Application.ProductName + " (wersja " + System.Windows.Forms.Application.ProductVersion + ")", FooterFont, Brushes.Black, new Rectangle((int)x, (int)y, (int)PrintWidth, (int)FooterFont.GetHeight(G)), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            G.DrawString(DateTime.Now.ToString() , FooterFont, Brushes.Black, new Rectangle((int)x, (int)y, (int)PrintWidth, (int)FooterFont.GetHeight(G)), strFormat);
        }
        public void DrawLine(float x0, float y0,float x1,float y1,float PenWidth=1)
        {
            x0 += Settings.Default.XCaliber;
            y0 += Settings.Default.YCaliber;
            x1 += Settings.Default.XCaliber;
            y1 += Settings.Default.YCaliber;
            G.DrawLine(new Pen(Brushes.Black, PenWidth),x0,y0,x1,y1);
        }
        public void DrawRectangle(float PenWidth,float x0, float y0,float Width,float Height)
        {
            x0 += Settings.Default.XCaliber;
            y0 += Settings.Default.YCaliber;
            G.DrawRectangle(new Pen(Brushes.Black, PenWidth), x0, y0, Width, Height);
        }
        public void DrawImage(System.Drawing.Image img, float x, float y, float Width, float Height)
        {
            x += Settings.Default.XCaliber;
            y += Settings.Default.YCaliber;
            G.DrawImage(img, x, y, Width, Height);
        }
    }

}
