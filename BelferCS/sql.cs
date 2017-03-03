using System;
namespace BelferCS
{
    public class OpcjeSQL
    {
        public string SelectOption(string OptionName,string OptionType,string IdSchool,DateTime CurrDate)
        {
            return "Select Value FROM opcje WHERE Name='" + OptionName + "' AND Type='" + OptionType + "' AND IdSchool='" + IdSchool + "' AND '"+ CurrDate.ToShortDateString() +"' Between StartDate AND EndDate;";
        }
    }
    public class StatystykaSQL
    {
        /// <summary>
        /// Kwerenda zlicza oceny w poszczególnych klasach z poszczególnych przedmiotów i dla poszczególnych nauczycieli
        /// </summary>
        /// <param name="Szkola">Identyfikator szkoły, której kwerenda dotyczy</param>
        /// <param name="RokSzkolny">Rok szkolny, którego kwerenda dotyczy</param>
        /// <param name="Okres">Okres roku szkolnego ( semestr I lub rok szkolny)</param>
        /// <returns></returns>
        public String SelectLiczbaOcen(String Szkola ,String RokSzkolny ,String Okres )
        {
            return "SELECT COUNT(w.ID) AS LiczbaOcen,o.Klasa,sp.IdPrzedmiot,o.Nauczyciel,so.Waga FROM wyniki w INNER JOIN kolumna k ON w.IdKolumna=k.ID INNER JOIN obsada o ON k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN skala_ocen so ON so.ID=w.IdOcena WHERE sp.IdSzkola='" + Szkola + "' AND o.RokSzkolny='" + RokSzkolny + "' AND k.Typ='" + Okres + "'  AND sp.IdPrzedmiot NOT IN (SELECT ID FROM przedmiot WHERE Typ='Z') AND so.Waga>=0 GROUP BY o.Klasa, w.IdOcena, sp.IdPrzedmiot, o.Nauczyciel;";
        }
        /// <summary>
        /// Kwerenda pobiera aktywną obsadę wszystkich przedmiotów w szkole w danym roku szkolnym dla wskazanego okresu
        /// </summary>
        /// <param name="Szkola">Identyfikator szkoły</param>
        /// <param name="RokSzkolny">Rok szkolny</param>
        /// <param name="DataKoncowaOkresu">Data końca semestru lub roku szkolnego</param>
        /// <returns></returns>
        public string SelectObsada(String Szkola, String RokSzkolny,DateTime DataKoncowaOkresu)
        {
            return "SELECT DISTINCT sp.IdPrzedmiot,o.Przedmiot As IdSzkolaPrzedmiot,p.Nazwa As Przedmiot,o.Nauczyciel AS IdNauczyciel,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel,o.Klasa,sk.Nazwa_Klasy,sk.Virtual FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID = sn.IdNauczyciel INNER JOIN szkola_klasa sk ON o.Klasa = sk.ID WHERE sp.IdSzkola = '" + Szkola + "' AND o.RokSzkolny = '" + RokSzkolny + "' AND p.Typ NOT IN('Z', 'F') AND Date(o.DataAktywacji) <= '" + DataKoncowaOkresu.ToShortDateString() + "' AND(Date(o.DataDeaktywacji) > '" + DataKoncowaOkresu.ToShortDateString() + "'  OR o.DataDeaktywacji IS NULL) ORDER BY sp.Priorytet, n.Nazwisko, n.Imie, sk.Nazwa_Klasy;";
        }
        /// <summary>
        /// Kweredna pobiera liczbę uczniów w klasach, łącznie z uczniami nauczanymi indywidualnie
        /// </summary>
        /// <param name="Szkola">Identyfikator szkoły</param>
        /// <param name="RokSzkolny">Rok szkolny</param>
        /// <param name="DataKoncowaOkresu">Końcowa data okresu (semestru lub roku szkolnego)</param>
        /// <returns></returns>
        public string SelectStanKlasy(String Szkola, String RokSzkolny,DateTime DataKoncowaOkresu)
        {
            return "SELECT p.Klasa,COUNT(p.ID) As StanKlasy FROM przydzial p INNER JOIN szkola_klasa sk ON sk.ID = p.Klasa WHERE sk.IdSzkola = '" + Szkola + "' AND p.RokSzkolny = '" + RokSzkolny + "' AND DATE(p.DataAktywacji) <= '" + DataKoncowaOkresu.ToShortDateString() + "' AND(DATE(p.DataDeaktywacji) > '" + DataKoncowaOkresu.ToShortDateString() + "' OR p.DataDeaktywacji is null) Group BY p.Klasa;";
        }
        /// <summary>
        /// Kwerenda pobiera liczbę uczniów w klasach wirtualnych z uwzględnieniem przedmiotów nauczanych indywidualnie
        /// </summary>
        /// <param name="Szkola">Identyfikator szkoły</param>
        /// <param name="RokSzkolny">Rok szkolny</param>
        /// <param name="DataKoncowaOkresu">Końcowa data okresu (semestru lub roku szkolnego)</param>
        /// <returns></returns>
        public string SelectStanKlasyWirtualnej(String Szkola, String RokSzkolny, DateTime DataKoncowaOkresu)
        {
            return "SELECT o.Klasa AS KlasaWirtualna,p.Klasa,sp.IdPrzedmiot,COUNT(ni.IdPrzydzial) AS StanKlasy FROM obsada o INNER JOIN nauczanie_indywidualne ni ON o.ID=ni.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przydzial p ON p.ID = ni.IdPrzydzial WHERE sp.IdSzkola='" + Szkola + "' AND p.RokSzkolny='" + RokSzkolny + "' AND DATE(o.DataAktywacji) <= '" + DataKoncowaOkresu.ToShortDateString() + "' AND (DATE(o.DataDeaktywacji) > '" + DataKoncowaOkresu.ToShortDateString() + "' OR o.DataDeaktywacji is null) GROUP BY o.Klasa,sp.IdPrzedmiot;";
        }
        /// <summary>
        /// Kwerenda zlicza uczniów należących do grup przedmiotowych wg klas i przedmiotów 
        /// </summary>
        /// <param name="Szkola">Identyfikator szkoły</param>
        /// <param name="RokSzkolny">Rok szkolny</param>
        /// <returns></returns>
        public string CountGroupMember(string Szkola, string RokSzkolny)
        {
            return "SELECT Count(g.IdSzkolaPrzedmiot) AS StanGrupy,p.Klasa,sp.IdPrzedmiot,g.IdSzkolaPrzedmiot FROM grupa g INNER JOIN przydzial p ON p.ID=g.IdPrzydzial INNER JOIN szkola_przedmiot sp ON sp.ID=g.IdSzkolaPrzedmiot WHERE p.RokSzkolny = '" + RokSzkolny + "' AND sp.IdSzkola = '" + Szkola + "' AND p.StatusAktywacji = 1 GROUP BY Klasa,IdPrzedmiot,IdSzkolaPrzedmiot;";
        }
    }  
    public class PoprawkaSQL
    {
        /// <summary>
        /// Kwerenda pobiera listę uczniów dopuszczonych do egzaminu poprawkowego, wraz z przedmiotami i nauczycielami prowadzącymi 
        /// </summary>
        /// <param name="Szkola">Identyfikator szkoły, ogranicza zakres danych do uczniów wskazanej szkoły</param>
        /// <param name="RokSzkolny">Ogranicza zakres danych do wskazanego roku szkolnego</param>
        /// <returns></returns>
        public string SelectStudent(string Szkola, string RokSzkolny)
        {
            return "SELECT u.ID AS IdStudent,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student,sk.Nazwa_Klasy AS Klasa,CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel,pt.Nazwa AS Przedmiot FROM przydzial p INNER JOIN poprawka pk ON p.ID = pk.IdPrzydzial INNER JOIN szkola_klasa sk ON sk.ID = p.Klasa INNER JOIN uczen u ON p.IdUczen = u.ID INNER JOIN obsada o ON pk.IdObsada = o.ID INNER JOIN szkola_nauczyciel sn On o.Nauczyciel = sn.ID INNER JOIN nauczyciel n ON n.ID = sn.IdNauczyciel INNER JOIN szkola_przedmiot sp ON sp.ID = o.Przedmiot INNER JOIN przedmiot pt ON pt.ID = sp.IdPrzedmiot WHERE pk.Typ = 'R' AND sk.IdSzkola = '" + Szkola + "' AND p.RokSzkolny = '" + RokSzkolny + "' ORDER BY sp.Priorytet,u.Nazwisko,u.Imie;";
        }
    }
}