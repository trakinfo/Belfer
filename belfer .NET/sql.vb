Public Class AdminSQL
  Public Function SelectSslCipher() As String
    Return "SHOW STATUS LIKE 'Ssl_cipher';"
  End Function
  Public Function SelectProcessList() As String
    'Return "SHOW FULL PROCESSLiST;"
    Return "SELECT p.ID AS IdProcess,p.USER As SysUser,e.ID AS IdEvent,e.Login,e.ComputerIP,e.TimeIN,ADDDATE(SYSDATE(),INTERVAL -p.TIME SECOND) FROM information_schema.`PROCESSLIST` P INNER JOIN belfer.event e ON SUBSTRING_INDEX(p.HOST,':',1)=e.ComputerIP WHERE p.DB='belfer' AND e.TimeIN>=ADDDATE(SYSDATE(),INTERVAL -p.TIME SECOND);"
  End Function
  Public Function SelectFullProcessList() As String
    Return "SHOW FULL PROCESSLiST;"
  End Function
  Public Function KillProcess(ID As String) As String
    Return "Kill " & ID & ";"
  End Function
  Public Function SelectEvents(IP As String) As String
    Return "SELECT e.`ID`, e.`Login`,Concat_WS(' ',u.Nazwisko,u.Imie) AS Name, e.`ComputerIP`, e.`AppType`, e.`AppVer`, e.`TimeIn`, e.`TimeOut` FROM event e INNER JOIN user u ON u.Login=e.Login WHERE e.ComputerIP=SUBSTRING_INDEX('" & IP & "',':',1)  ORDER BY TimeIn DESC LIMIT 1;"
  End Function
  Public Function SelectEvents(StartDate As String, EndDate As String) As String
    Return "SELECT e.`ID`, e.`Login`,Concat_WS(' ',u.Nazwisko,u.Imie) AS Name, e.`ComputerIP`, Cast(e.`Status` AS Signed) AS StatusLogowania, e.`AppType`, e.`AppVer`, e.`TimeIn`, e.`TimeOut`,u.Role,u.Status FROM event e INNER JOIN user u ON u.Login=e.Login WHERE DATE(TimeIN) BETWEEN '" & StartDate & "' AND '" & EndDate & "' ORDER BY TimeIn DESC;"
  End Function
  Public Function SelectEvents(StartDate As String, EndDate As String, RokSzkolny As String, Klasa As String) As String
    Return "SELECT e.`ID`, e.`Login`,Concat_WS(' ',u.Nazwisko,u.Imie) AS Name, e.`ComputerIP`, e.`TimeIn`, e.`TimeOut`,up.IdUczen FROM event e INNER JOIN user u ON u.Login=e.Login  INNER JOIN uprawnienie up ON e.Login=up.UserLogin WHERE e.TimeIn Between '" & StartDate & "' AND '" & EndDate & "' AND u.Role=0 AND u.Status=1 AND up.IdUczen IN (Select IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND RokSzkolny='" & RokSzkolny & "' AND StatusAktywacji=1) ORDER BY TimeIn DESC;"
  End Function
  Public Function SelectOutofdatePrivilage(RokSzkolny As String) As String
    Return "Select Concat_WS(' ',u.UserLogin,' (',Concat_WS(' ',usr.Nazwisko,usr.Imie),')') AS Rodzic,Concat_WS(' ',ucn.Nazwisko,ucn.Imie) AS Student,u.UserLogin,u.IdUczen FROM uprawnienie u INNER JOIN uczen ucn ON u.IdUczen=ucn.ID INNER JOIN user usr ON u.UserLogin=usr.Login WHERE u.IdUczen NOT IN (Select p.IdUczen FROM przydzial p WHERE p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1) Order By u.UserLogin,ucn.Nazwisko,ucn.Imie;"
  End Function
  Public Function SelectNoPrivilageAccount() As String
    Return "SELECT Concat_WS(' ',u.Login,' (',Concat_WS(' ',u.Nazwisko,u.Imie),')') AS Rodzic,u.Status,u.Login FROM user u WHERE u.Role=0 AND u.Login NOT IN (SELECT DISTINCT UserLogin FROM uprawnienie) ORDER BY u.Login,u.Nazwisko,u.Imie;"
  End Function
  Public Function DeleteOutofdatePrivilage() As String
    Return "DELETE FROM uprawnienie WHERE UserLogin=?Login AND IdUczen=?IdUczen;"
  End Function
  Public Function DeleteNoPrivilageAccount() As String
    Return "DELETE FROM user WHERE Login=?Login;"
  End Function
  Public Function DeactivateNoPrivilageAccount() As String
    Return "UPDATE user SET Status=0 WHERE Login=?Login;"
  End Function
  Public Function UpdateColumnLock(Lock As String, Typ As String, RokSzkolny As String) As String
    Return "UPDATE kolumna k SET k.Lock=" & Lock & " WHERE k.Typ IN (" & Typ & ") AND k.IdObsada IN (SELECT ID FROM obsada WHERE RokSzkolny='" & RokSzkolny & "');"
  End Function
  Public Function CountColumnLock(Lock As String, Typ As String, RokSzkolny As String) As String
    Return "SELECT COUNT(k.ID) FROM kolumna k WHERE k.Lock=" & Lock & " AND k.Typ IN (" & Typ & ") AND k.IdObsada IN (SELECT ID FROM obsada WHERE RokSzkolny='" & RokSzkolny & "');"
  End Function
End Class

Public Class BelferSQL
  Public Function SelectBelfer() As String
    Return "SELECT ID,CONCAT_WS(' ',Nazwisko,Imie),Sex,User,ComputerIP,Version,Owner FROM nauczyciel ORDER BY Nazwisko,Imie;"
  End Function
  Public Function DeleteBelfer(ByVal ID As String) As String
    Return "DELETE FROM nauczyciel WHERE ID='" & ID & "';"
  End Function
  Public Function InsertBelfer() As String
    Return "INSERT INTO nauczyciel VALUES(NULL,?Nazwisko,?Imie,?Sex,NULL,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateBelfer(ByVal ID As String) As String
    Return "UPDATE nauczyciel SET Nazwisko=?Nazwisko,Imie=?Imie,Sex=?Sex,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID='" & ID & "';"
  End Function
End Class

Public Class KlasaSQL
  Public Function SelectKlasa() As String
    Return "SELECT Kod,User,ComputerIP,Version,Owner FROM klasa;"
  End Function
  Public Function DeleteKlasa(ByVal Klasa As String) As String
    Return "DELETE FROM klasa WHERE Kod='" & Klasa & "';"
  End Function
  Public Function InsertKlasa(ByVal Nazwa As String) As String
    Return "INSERT INTO klasa VALUES('" & Nazwa & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
End Class

Public Class UsersSQL
  Public Function SelectBelfer() As String
    Return "SELECT n.ID,CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel FROM nauczyciel n ORDER BY n.Nazwisko,n.Imie;"
  End Function
  Public Function SelectLogin() As String
    Return "SELECT u.`Role`,u.`Password`,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS FullName,IdNauczyciel FROM `user` u WHERE u.Role>0 AND u.Status=1 AND BINARY u.`Login`=?UserName;"
  End Function
  'Public Function SelectLogin(User As String) As String
  '  Return "SELECT u.`Role`,u.`Password`,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS FullName,IdNauczyciel FROM `user` u WHERE u.Role>0 AND u.Status=1 AND u.`Login`='" & User & "';"
  'End Function
  Public Function SelectSysUser() As String
    Return "SELECT USER();"
  End Function
  Public Function SelectUsers() As String
    'CONCAT(u.User,' (','Wł: ',u.Owner,')') As 
    Return "SELECT u.Login, Concat_WS(' ',u.Nazwisko, u.Imie) as UserName,u.Email, u.Role,u.Status,Concat_WS(' ',n.Nazwisko,n.Imie) as Belfer, u.User, u.ComputerIP, u.Version,u.Owner FROM `user` u LEFT Join nauczyciel n ON u.IdNauczyciel=n.ID ORDER BY u.Login,u.Role,u.Nazwisko,u.Imie;"
    'Return "SELECT u.Login, Concat_WS(' ',u.Nazwisko, u.Imie) as Belfer, u.Role,u.Status, CONCAT(User,' (','Wł: ',Owner,')') As User, u.ComputerIP, u.Version,Owner FROM `user` u ORDER BY u.Login,u.Role,u.Nazwisko,u.Imie;"
  End Function
  Public Function SelectUserNames() As String
    Return "SELECT u.Login,CONCAT_WS(' ',u.Nazwisko,u.Imie) as User FROM user u WHERE u.Role>0 ORDER BY u.Login;"
  End Function
  Public Function SelectSchoolTeacherID(IdSchool As String, IdTeacher As String) As String
    Return "SELECT sn.ID FROM szkola_nauczyciel sn WHERE IdSzkola='" & IdSchool & "' AND IdNauczyciel='" & IdTeacher & "';"
  End Function
  Public Function SelectTutorClass(IdSchool As String, IdSchoolTeacher As String, SchoolYear As String) As String
    Return "SELECT DISTINCT o.Klasa,sk.Nazwa_Klasy FROM obsada o INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa WHERE o.Nauczyciel='" & IdSchoolTeacher & "' AND o.RokSzkolny='" & SchoolYear & "' AND o.Przedmiot IN (SELECT sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE IdSzkola='" & IdSchool & "' AND Typ='Z') AND o.DataDeaktywacji IS NULL;"
  End Function
  Public Function CountUser(Login As String) As String
    Return "SELECT COUNT(Login) FROM user WHERE Login='" & Login & "';"
  End Function
  Public Function InsertUser() As String
    Return "INSERT INTO user (Login,Nazwisko,Imie,Email,Password,Role,Status,IdNauczyciel,Owner,User,ComputerIP,Version) VALUES (?Login,?Nazwisko,?Imie,?Email,?Password,?Role,?Status,?IdNauczyciel,'" + GlobalValues.AppUser.Login + "','" + GlobalValues.AppUser.Login + "','" + GlobalValues.gblIP + "',NULL);"
  End Function
  Public Function DeleteUser() As String
    Return "DELETE FROM user WHERE Login=?Login;"
  End Function
  Public Function UpdatePassword() As String
    Return "UPDATE user SET Password=?Password WHERE Login=?Login;"
  End Function
  Public Function UpdateUser() As String
    Return "UPDATE user SET Nazwisko=?Nazwisko,Imie=?Imie,Email=?Email,Role=?Role,Status=?Status,IdNauczyciel=?IdNauczyciel,User=?User,ComputerIP=?ComputerIP,Version=NULL WHERE Login=?Login;"
  End Function
End Class

Public Class PrivilagesSQL
  Public Function SelectPrivilege() As String
    'CONCAT(up.User,' (','Wł: ',up.Owner,')') As
    Return "SELECT u.ID, us.Login, Concat_WS(' ',us.Nazwisko, us.Imie) AS Opiekun,Concat_WS(' ',u.Nazwisko, u.Imie) AS Student, up.User, up.ComputerIP, up.Version,up.Owner FROM uczen u, user us,uprawnienie up WHERE up.IdUczen=u.ID AND up.UserLogin=us.Login Order by Login COLLATE UTF8_polish_ci,us.Nazwisko COLLATE UTF8_polish_ci,us.Imie COLLATE UTF8_polish_ci,u.Nazwisko COLLATE UTF8_polish_ci,u.Imie COLLATE UTF8_polish_ci,u.Imie2 COLLATE UTF8_polish_ci;"
  End Function
  Public Function InsertPrivilege() As String
    Return "INSERT INTO uprawnienie (UserLogin,IdUczen,Owner,User,ComputerIP,Version) VALUES (?Login,?IdUczen,'" + GlobalValues.AppUser.Login + "','" + GlobalValues.AppUser.Login + "','" + GlobalValues.gblIP + "',NULL);"
  End Function
  Public Function DeletePrivilege() As String
    Return "DELETE FROM uprawnienie WHERE UserLogin=?Login AND IdUczen=?IdUczen;"
  End Function
  Public Function SelectUsers() As String
    Return "SELECT u.Login, Concat_WS(' ',u.Nazwisko, u.Imie) as User FROM `user` u WHERE u.Status=1 ORDER BY u.Login COLLATE UTF8_polish_ci,u.Nazwisko COLLATE UTF8_polish_ci,u.Imie COLLATE UTF8_polish_ci;"
  End Function
  Public Function SelectStudent(RokSzkolny As String, IdSzkola As String) As String
    Return "SELECT  u.ID,Concat_WS(' ',u.Nazwisko, u.Imie,u.Imie2) as Student,sk.Nazwa_Klasy,NrArkusza,Pesel FROM uczen u, przydzial p,szkola_klasa sk WHERE u.ID=p.IdUczen AND p.Klasa=sk.ID AND p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa IN (SELECT ID FROM szkola_klasa WHERE IdSzkola='" & IdSzkola & "') AND p.StatusAktywacji=1 ORDER BY sk.Kod_Klasy, u.Nazwisko COLLATE UTF8_polish_ci,u.Imie COLLATE UTF8_polish_ci,u.Imie2 COLLATE UTF8_polish_ci;"
  End Function
End Class

Public Class SzkolaSQL
  Public Function SelectSchools(SchoolType As String) As String
    'CONCAT(s.User,' (','Wł: ',s.Owner,')') As 
    Return "SELECT s.ID,s.Alias,Concat_WS(' ',Concat_WS(', ',TRIM(CONCAT_WS(' ',IFNULL(TRIM(TRAILING ', ' FROM Concat_WS(', ',m.Nazwa,s.Ulica)),''),s.Nr)),IFNULL(Concat_WS('-',LEFT(m.KodPocztowy,2),RIGHT(m.KodPocztowy,3)),m.KodPocztowy)),m.Poczta) AS Adres,s.NIP, s.IsDefault, s.Nazwa, s.Telefon,s.Fax,s.Email,s.User,s.ComputerIP,s.Version,m.ID,s.Ulica,s.Nr,s.Owner FROM szkola s LEFT JOIN miejscowosc m ON s.IdMiejscowosc=m.ID INNER JOIN typy_szkol t ON s.IdTypSzkoly=t.ID WHERE t.ID='" & SchoolType & "' ORDER BY s.Nazwa COLLATE UTF8_polish_ci;"
  End Function
  Public Function SelectSchoolAlias(SchoolType As String) As String
    Return "Select ID,Alias FROM szkola WHERE IdTypSzkoly='" & SchoolType & "';"
  End Function
  Public Function SelectSchoolName(IdSchool As String) As String
    Return "SELECT Nazwa FROM szkola WHERE ID=" & IdSchool & ";"
  End Function
  Public Function UpdateSchool(ByVal IdSchool As String) As String
    Return "UPDATE szkola SET IdTypSzkoly=?IdTypSzkoly,Nazwa=?Nazwa,Alias=?Alias,Nip=?Nip,Ulica=?Ulica,Nr=?Nr,IdMiejscowosc=?IdMiejscowosc,Telefon=?Tel,Fax=?Fax,Email=?Email,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=" & IdSchool & ";"
  End Function
  Public Function InsertSchool() As String
    Return "INSERT INTO szkola VALUES(NULL,?IdTypSzkoly,?Nazwa,?Alias,?Nip,?Ulica,?Nr,?IdMiejscowosc,?Tel,?Fax,?Email,0,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function DeleteSchool(IdSchool As String) As String
    Return "Delete From szkola Where ID='" & IdSchool & "';"
  End Function
  Public Function ResetDefault() As String
    Return "UPDATE szkola SET IsDefault=false WHERE IdTypSzkoly=?TypSzkoly;"
  End Function
  Public Function SetDefault() As String
    Return "UPDATE szkola SET IsDefault=1 WHERE ID=?ID;"
  End Function
  Public Function SelectDefault(TypSzkoly As String) As String
    Return "Select ID From szkola Where IsDefault=1 AND IdTypSzkoly=" & TypSzkoly & ";"
  End Function
End Class

Public Class PrzydzialKlasSQL
  Public Function SelectSchoolClasses(ByVal IdSzkola As String) As String
    'CONCAT(User,' (','Wł: ',Owner,')') As
    Return "SELECT Kod_Klasy,Nazwa_Klasy,Virtual, User,ComputerIP,Version,Owner FROM szkola_klasa WHERE IdSzkola='" & IdSzkola & "' ORDER BY Kod_Klasy;"
  End Function
  Public Function SelectClasses(ByVal IdSzkola As String) As String
    Return "SELECT Kod FROM klasa WHERE Kod NOT IN (SELECT Kod_Klasy FROM szkola_klasa WHERE IdSzkola='" & IdSzkola & "') ORDER BY Kod;"
  End Function
  Public Function DeleteClass(ByVal IdSzkola As String, ByVal IdKlasa As String) As String
    Return "DELETE FROM szkola_klasa WHERE Kod_Klasy='" & IdKlasa & "' AND IdSzkola='" & IdSzkola & "';"
  End Function
  Public Function InsertSchoolClass() As String
    Return "INSERT INTO szkola_klasa VALUES(NULL,?IdSzkola,?Kod_Klasy,?Nazwa_Klasy,?Virtual,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  'Public Function ResetDefault() As String
  '  Return "UPDATE szkola_klasa SET IsDefault=false;"
  'End Function
  'Public Function SetVirtual() As String
  '  Return "UPDATE szkola_klasa SET Virtual=?Virtual WHERE IdSzkola=?IdSzkola AND Kod_Klasy=?Kod_Klasy;"
  'End Function
  Public Function UpdateSchoolClass() As String
    Return "UPDATE szkola_klasa SET Nazwa_Klasy=?Nazwa_Klasy,Virtual=?Virtual,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE IdSzkola=?IdSzkola AND Kod_Klasy=?Kod_Klasy;"
  End Function
End Class

Public Class PrzydzialNauczycieliSQL
  Public Overloads Function SelectSchoolTeachers(ByVal IdSzkola As String) As String
    'CONCAT(sn.User,' (','Wł: ',sn.Owner,')') As 
    Return "SELECT sn.ID,Concat_WS(' ',n.Nazwisko,n.Imie),sn.Status,sn.User,sn.ComputerIP,sn.Version,sn.Owner FROM szkola_nauczyciel sn,nauczyciel n WHERE sn.IdNauczyciel= n.ID AND sn.IdSzkola='" & IdSzkola & "' Order By n.Nazwisko,n.Imie;"
  End Function
  Public Overloads Function SelectTeachers(ByVal IdSzkola As String) As String
    Return "SELECT ID,Concat_WS(' ',Nazwisko,Imie) FROM nauczyciel WHERE ID NOT IN (SELECT IdNauczyciel FROM szkola_nauczyciel WHERE IdSzkola='" & IdSzkola & "') Order By Nazwisko,Imie;"
  End Function
  Public Function DeleteSchoolTeacher() As String
    Return "DELETE FROM szkola_nauczyciel WHERE ID=?IdPrzydzial;"
  End Function
  Public Function InsertSchoolTeacher() As String
    Return "INSERT INTO szkola_nauczyciel VALUES(NULL,?IdSzkola,?IdNauczyciel,1,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function Aktywacja() As String
    Return "UPDATE szkola_nauczyciel SET Status=?Status WHERE ID=?ID;"
  End Function
End Class

Public Class MiejscowoscSQL
  Public Function SelectMiejscowosc() As String
    'CONCAT(m.User,' (','Wł: ',m.Owner,')') As 
    Return "SELECT m.ID,m.Nazwa,m.NazwaMiejscownik,m.Poczta,m.KodPocztowy,w.Nazwa AS Woj,k.Nazwa AS Kraj, m.Miasto, m.User, m.ComputerIP, m.Version,m.Owner FROM miejscowosc m LEFT JOIN wojewodztwa w ON m.KodWoj=w.KodWoj LEFT JOIN kraj k ON m.IdKraj=k.ID ORDER BY m.Nazwa COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectMiejsce() As String
    Return "Select m.ID,concat_WS(' ',m.Nazwa,'{',w.Nazwa,'}') as Nazwa From miejscowosc m left Join wojewodztwa w on m.KodWoj=w.KodWoj Order by m.Nazwa Collate UTF8_polish_ci;"
  End Function

  Public Function SelectKraj() As String
    Return "SELECT ID,Nazwa FROM kraj Order by Nazwa Collate UTF8_polish_ci;"
  End Function
  Public Function SelectWoj() As String
    Return "SELECT KodWoj,Nazwa FROM wojewodztwa Order by Nazwa Collate UTF8_polish_ci;;"
  End Function
  Public Function DeleteMiejscowosc() As String
    Return "DELETE FROM miejscowosc WHERE ID=?ID;"
  End Function
  Public Function InsertMiejscowosc() As String
    Return "INSERT INTO miejscowosc VALUES(NULL,?Nazwa,?NazwaMiejscownik,?Poczta,?KodPocztowy,?KodWoj,?IdKraj,?Miasto,'" + GlobalValues.AppUser.Login + "','" + GlobalValues.AppUser.Login + "','" + GlobalValues.gblIP + "',NULL);"
  End Function
  Public Function UpdateMiejscowsc() As String
    Return "UPDATE miejscowosc SET Nazwa=?Nazwa,NazwaMiejscownik=?NazwaMiejscownik,Poczta=?Poczta,KodPocztowy=?KodPocztowy,KodWoj=?KodWoj,IdKraj=?IdKraj,Miasto=?Miasto,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
End Class

Public Class WojSQL
  Public Function SelectWoj() As String
    'CONCAT(User,' (','Wł: ',Owner,')') As 
    Return "SELECT KodWoj,Nazwa,IsDefault,User,ComputerIP,Version,Owner FROM wojewodztwa Order by Nazwa Collate UTF8_polish_ci;"
  End Function
  Public Function DeleteWoj(ByVal KodWoj As String) As String
    Return "DELETE FROM wojewodztwa WHERE KodWoj='" & KodWoj & "';"
  End Function
  Public Function InsertWoj() As String
    Return "INSERT INTO wojewodztwa(KodWoj,Nazwa,User,ComputerIP,Version) VALUES(?KodWoj,?Nazwa,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateWoj(ByVal KodWoj As String) As String
    Return "UPDATE wojewodztwa SET KodWoj=?KodWoj,Nazwa=?Nazwa,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE KodWoj='" & KodWoj & "';"
  End Function
  Public Function SetDefault(ByVal IsDefault As Integer, ByVal KodWoj As String) As String
    Return "UPDATE wojewodztwa SET IsDefault=" & IsDefault & " WHERE KodWoj='" & KodWoj & "';"
  End Function
  Public Function ResetDefault() As String
    Return "UPDATE wojewodztwa SET IsDefault=false;"
  End Function
End Class

Public Class KrajSQL
  Public Function SelectKraj() As String
    'CONCAT(User,' (','Wł: ',Owner,')') As 
    Return "SELECT ID,Nazwa,FullName,User,ComputerIP,Version,Owner FROM kraj Order by Nazwa Collate UTF8_polish_ci;"
  End Function
  Public Function DeleteKraj(ByVal ID As String) As String
    Return "DELETE FROM kraj WHERE ID='" & ID & "';"
  End Function
  Public Function InsertKraj() As String
    Return "INSERT INTO kraj VALUES(NULL,?Nazwa,?FullName,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateKraj(ByVal ID As String) As String
    Return "UPDATE kraj SET Nazwa=?Nazwa,FullName=?FullName,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID='" & ID & "';"
  End Function
End Class

Public Class PrzedmiotSQL
  Public Overloads Function SelectPrzedmiot() As String
    'CONCAT(User,' (','Wł: ',Owner,')') As
    Return "SELECT ID,Alias,Nazwa,Typ, User,ComputerIP,Version,Owner FROM przedmiot Order by Priorytet,Alias;"
  End Function
  Public Overloads Function SelectPrzedmiotAlias() As String
    Return "SELECT ID,Alias,Priorytet FROM przedmiot Order by Priorytet,Alias;"
  End Function
  Public Overloads Function SelectPrzedmiot(Szkola As String) As String
    'CONCAT(sp.User,' (','Wł: ',sp.Owner,')') As
    Return "SELECT sp.ID,p.Alias,sp.Grupa,p.ID AS IdPrzedmiot,sp.Priorytet, sp.User,sp.ComputerIP,sp.Version,sp.Owner FROM przedmiot p,szkola_przedmiot sp WHERE p.ID=sp.IdPrzedmiot AND sp.IdSzkola=" & Szkola & " Order by sp.Priorytet,p.Alias,sp.Grupa;"
  End Function
  Public Overloads Function SelectPrzedmiotAlias(ByVal IdSzkola As String) As String
    Return "SELECT ID,Alias,Priorytet FROM przedmiot WHERE ID NOT IN (SELECT IdPrzedmiot FROM szkola_przedmiot WHERE IdSzkola='" & IdSzkola & "') Order By Priorytet,Alias;"
  End Function
  Public Function DeletePrzedmiot() As String
    Return "DELETE FROM przedmiot WHERE ID=?ID;"
  End Function
  Public Function DeletePrzydzialPrzedmiot() As String
    Return "DELETE FROM szkola_przedmiot WHERE IdSzkola=?IdSzkola AND IdPrzedmiot=?IdPrzedmiot;"
  End Function
  Public Function InsertPrzedmiot() As String
    Return "INSERT INTO przedmiot VALUES(NULL,?Alias,?Nazwa,?Typ,null,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function InsertPrzydzialPrzedmiot() As String
    Return "INSERT INTO szkola_przedmiot(ID,IdSzkola,IdPrzedmiot,Grupa,Priorytet,Owner,User,ComputerIP,Version) VALUES(NULL,?IdSzkola,?IdPrzedmiot,?Grupa,?Priorytet,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdatePrzedmiot() As String
    Return "UPDATE przedmiot SET Alias=?Alias,Nazwa=?Nazwa,Typ=?Typ,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function UpdatePriorytet(ByVal Priorytet As String, ByVal ID As String) As String
    Return "UPDATE przedmiot SET Priorytet='" & Priorytet & "',User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "' where ID=" & ID
  End Function
  Public Function UpdatePriorytetBySchool(ByVal Priorytet As String, ByVal IdPrzedmiot As String) As String
    Return "UPDATE szkola_przedmiot sp SET Priorytet='" & Priorytet & "',User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "' where ID='" & IdPrzedmiot & "';"
  End Function

End Class

Public Class WychowawcaSQL
  Public Function SelectWychowawca(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    'CONCAT(o.User,' (','Wł: ',o.Owner,')') As
    Return "SELECT o.ID AS IdObsada,sk.Nazwa_Klasy,Concat_WS(' ',n.Nazwisko,n.Imie) AS Wychowawca,o.DataAktywacji,o.DataDeaktywacji, o.LiczbaGodzin,o.Klasa, o.User, o.ComputerIP, o.Version, o.Przedmiot,o.Nauczyciel as IdWychowawca,o.Owner FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON sn.IdNauczyciel=n.ID INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa WHERE o.Przedmiot IN (SELECT ID FROM szkola_przedmiot sp WHERE IdPrzedmiot IN (SELECT ID FROM przedmiot WHERE Typ='z')) AND o.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE sk.IdSzkola='" & IdSzkola & "') AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sk.Kod_Klasy,o.DataAktywacji;"
  End Function
  'Public Function SelectClass(ByVal School As String, ByVal RokSzkolny As String) As String
  '  Return "SELECT DISTINCT sk.ID, sk.Nazwa_Klasy FROM szkola_klasa sk INNER JOIN przydzial p ON sk.ID=p.Klasa WHERE sk.IdSzkola='" & School & "' AND LEFT(p.RokSzkolny,4)='" & RokSzkolny & "' AND sk.ID NOT IN (SELECT Klasa FROM obsada WHERE LEFT(RokSzkolny,4)='" & RokSzkolny & "' AND Przedmiot IN (SELECT sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE p.Typ='z' AND IdSzkola='" & School & "')) AND p.StatusAktywacji=1 ORDER BY sk.Kod_Klasy;"
  'End Function
  Public Function SelectClass(ByVal School As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT sk.ID, sk.Nazwa_Klasy FROM szkola_klasa sk WHERE sk.IdSzkola='" & School & "' AND sk.Virtual=0 AND sk.ID NOT IN (SELECT Klasa FROM obsada WHERE LEFT(RokSzkolny,4)='" & RokSzkolny & "' AND DataDeaktywacji IS NULL AND Przedmiot IN (SELECT sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE p.Typ='z' AND IdSzkola='" & School & "')) ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectBelfer(ByVal IdSzkola As String) As String
    Return "SELECT sn.ID,CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel FROM nauczyciel n,szkola_nauczyciel sn WHERE n.ID=sn.IdNauczyciel AND sn.IdSzkola='" & IdSzkola & "' ORDER BY n.Nazwisko,n.Imie;"
  End Function
  Public Function InsertWychowawca() As String
    Return "INSERT INTO obsada VALUES(NULL,?Klasa,?IdPrzedmiot,?IdNauczyciel,?RokSzkolny,'o',0,?DataAktywacji,NULL,?LiczbaGodzin,'" + GlobalValues.AppUser.Login + "','" + GlobalValues.AppUser.Login + "','" + GlobalValues.gblIP + "',NULL);"
  End Function
  Public Function UpdateWychowawca(IdObsada As String) As String
    Return "UPDATE obsada SET Nauczyciel=?IdBelfer,DataAktywacji=?DataAktywacji,LiczbaGodzin=?LiczbaGodzin,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID='" & IdObsada & "';"
  End Function

  Public Function DeleteWychowawca(ByVal IdObsada As String) As String
    Return "DELETE FROM obsada WHERE  ID='" & IdObsada & "';"
  End Function
  Public Function SelectPrzedmiotZachowanie(Szkola As String) As String
    Return "SELECT sp.ID FROM szkola_przedmiot sp,przedmiot p WHERE p.ID=sp.IdPrzedmiot AND p.Typ='z' AND sp.IdSzkola='" & Szkola & "';"
  End Function
End Class

Public Class TypySzkolSQL
  Public Function SelectSchoolTypes() As String
    'CONCAT(User,' (','Wł: ',Owner,')') As 
    Return "SELECT ID,Typ,Opis,User,ComputerIP,Version,Owner FROM typy_szkol ORDER BY Typ COLLATE utf8_polish_ci;"
  End Function

  Public Function InsertSchoolType() As String
    Return "INSERT INTO typy_szkol VALUES(NULL,?Typ,?Opis,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateSchoolType(ByVal ID As String) As String
    Return "UPDATE typy_szkol SET Typ=?Typ,Opis=?Opis,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=" & ID & ";"
  End Function
  Public Function DeleteSchoolType(ByVal ID As String) As String
    Return "DELETE FROM typy_szkol WHERE ID=" & ID & ";"
  End Function
End Class

Public Class PrzydzialSQL
  Public Function SelectStudent(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    'CONCAT(p.User,' (','Wł: ',p.Owner,')') As
    Return "SELECT u.ID AS IdUczen,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student,sk.Nazwa_Klasy, p.User,p.ComputerIP,p.Version,p.ID AS IdPrzydzial,p.Promocja,p.Klasa,sk.Kod_Klasy,p.Owner FROM uczen u,przydzial p,szkola_klasa sk WHERE p.Klasa=sk.ID AND u.ID=p.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1 AND sk.IdSzkola='" & IdSzkola & "' ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectStudentNoAssign() As String
    'CONCAT(u.User,' (','Wł: ',u.Owner,')') As
    Return "SELECT u.`ID` AS IdUczen,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student,null, u.User,u.ComputerIP,u.Version,u.Owner FROM uczen u WHERE ID NOT IN (SELECT DISTINCT IdUczen FROM przydzial p);"
  End Function

  'Public Overloads Function SelectClass(ByVal School As String) As String
  '  Return "SELECT ID,Nazwa_Klasy,Kod_klasy FROM szkola_klasa sk WHERE sk.IdSzkola='" & School & "' ORDER BY Kod_Klasy;"
  'End Function
  Public Overloads Function SelectClass(ByVal School As String, ByVal Pion As String, Virtual As String) As String
    'uwzględnić wirtualne klasy
    Return "SELECT ID,Nazwa_Klasy FROM szkola_klasa sk WHERE sk.IdSzkola='" & School & "'  AND LEFT(Kod_Klasy,1)='" & Pion & "' AND sk.Virtual='" & Virtual & "' ORDER BY Kod_Klasy;"
  End Function
  Public Overloads Function SelectClass(ByVal School As String, ByVal Pion1 As String, ByVal Pion2 As String, Virtual As String) As String
    Return "(SELECT ID,Nazwa_Klasy,Kod_Klasy FROM szkola_klasa sk WHERE sk.IdSzkola='" & School & "' AND LEFT(Kod_Klasy,1)='" & Pion1 & "' AND sk.Virtual='" & Virtual & "') UNION (SELECT ID,Nazwa_Klasy,Kod_Klasy FROM szkola_klasa sk WHERE sk.IdSzkola='" & School & "' AND LEFT(Kod_Klasy,1)='" & Pion2 & "' AND sk.Virtual='" & Virtual & "') ORDER BY Kod_Klasy;"

  End Function

  Public Overloads Function CountAssignmentByClass(ByVal IdUczen As String, ByVal Klasa As String, ByVal RokSzkolny As String) As String
    Return "SELECT COUNT(ID) FROM przydzial p WHERE IdUczen='" & IdUczen & "' AND Klasa='" & Klasa & "' AND RokSzkolny='" & RokSzkolny & "';"

  End Function
  Public Overloads Function CountAssignmentByPion(ByVal IdUczen As String, IdSzkola As String, ByVal Pion As Integer, ByVal RokSzkolny As String) As String

    Return "SELECT COUNT(p.ID) FROM przydzial p WHERE p.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE sk.IdSzkola='" & IdSzkola & "' AND LEFT(sk.Kod_Klasy,1)='" & Pion & "') AND IdUczen='" & IdUczen & "' AND RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Overloads Function CountAssignmentBySchool(ByVal IdUczen As String, IdSzkola As String, ByVal RokSzkolny As String) As String
    Return "SELECT COUNT(p.ID) FROM przydzial p WHERE IdUczen='" & IdUczen & "' AND RokSzkolny='" & RokSzkolny & "' AND p.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE IdSzkola='" & IdSzkola & "');"
  End Function
  Public Overloads Function UpdatePrzydzial(ByVal IdUczen As String, ByVal RokSzkolny As String) As String
    Return "UPDATE przydzial p SET p.StatusAktywacji='0',p.MasterRecord='0',DataDeaktywacji=?DataDeaktywacji,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE p.IdUczen='" & IdUczen & "' AND p.RokSzkolny='" & RokSzkolny & "' AND StatusAktywacji='1';"
  End Function
  Public Overloads Function UpdatePrzydzial() As String
    Return "UPDATE przydzial p SET DataDeaktywacji=?DataDeaktywacji,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE p.ID=?IdPrzydzial;"
  End Function
  Public Function UpdateAktywacja() As String
    Return "UPDATE przydzial p SET DataAktywacji=?DataAktywacji,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE p.ID=?IdPrzydzial;"
  End Function
  Public Overloads Function UpdatePrzydzial(ByVal IdUczen As String, ByVal Klasa As String, ByVal RokSzkolny As String) As String
    Return "UPDATE przydzial p SET p.StatusAktywacji='1',p.MasterRecord='1',DataDeaktywacji=NULL,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE p.IdUczen='" & IdUczen & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "';"
  End Function
  Public Function InsertPrzydzial() As String
    Return "INSERT INTO przydzial VALUES(NULL,?IdUczen,?Klasa,?RokSzkolny,NULL,0,1,?DataAktywacji,NULL,1,'" + GlobalValues.AppUser.Login + "','" + GlobalValues.AppUser.Login + "','" + GlobalValues.gblIP + "',NULL);"

  End Function

  Public Function DeletePrzydzial(ByVal IdPrzydzial As String) As String
    Return "DELETE FROM przydzial WHERE ID='" & IdPrzydzial & "';"
  End Function
End Class

Public Class OcenySQL

  Public Overloads Function SelectString() As String
    'CONCAT(User,' (','Wł: ',Owner,')') As
    Return "Select ID,Ocena,Nazwa,Alias,Waga,Typ,PodTyp,SortOrder, User,ComputerIP,Version,Owner From skala_ocen Order by SortOrder,Nazwa"
  End Function
  Public Overloads Function SelectString(ByVal Typ As String) As String
    Return "SELECT ID,Ocena FROM skala_ocen o WHERE Typ='" & Typ & "' ORDER BY SortOrder;"
  End Function

  Public Function UpdateString() As String
    Return "UPDATE skala_ocen SET Ocena=?Ocena,Nazwa=?Nazwa,Alias=?Alias,Waga=?Waga,Typ=?Typ,PodTyp=?PodTyp,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "' where ID=?ID"
  End Function
  Public Function InsertString() As String
    Return "INSERT INTO skala_ocen (Ocena,Nazwa,Alias,Waga,Typ,PodTyp,Owner,User,ComputerIP) VALUES(?Ocena,?Nazwa,?Alias,?Waga,?Typ,?PodTyp,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "')"
  End Function
  Public Function DeleteString() As String
    Return "Delete From skala_ocen where ID=?ID"
  End Function
End Class


Public Class OpisWynikuSQL
  Public Function SelectOpis(IdPrzedmiot As String) As String
    'CONCAT(User,' (','Wł: ',Owner,')') As 
    Return "SELECT ID,Nazwa,KolorHex,User,ComputerIP,Version,Owner FROM opis_wyniku WHERE IdPrzedmiot='" & IdPrzedmiot & "' ORDER BY Nazwa;"
  End Function
  Public Function SelectPrzedmiot(IdSchool As String) As String
    Return "SELECT DISTINCT p.ID,p.Alias FROM przedmiot p,szkola_przedmiot sp WHERE p.ID=sp.IdPrzedmiot AND sp.IdSzkola='" & IdSchool & "' ORDER BY sp.Priorytet, p.Alias;"
  End Function
  Public Function DeleteOpis(ByVal ID As String) As String
    Return "DELETE FROM opis_wyniku WHERE ID='" & ID & "';"
  End Function
  Public Function InsertOpis() As String
    Return "INSERT INTO opis_wyniku VALUES(NULL,?Nazwa,?IdPrzedmiot,?Kolor,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateOpis() As String
    Return "UPDATE opis_wyniku SET Nazwa=?Nazwa,KolorHex=?Kolor,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
End Class

Public Class ObsadaSQL
  Public Function SelectObsadaByKlasa(IdSzkola As String, RokSzkolny As String, Virtual As String) As String
    'CONCAT(o.User,' (','Wł: ',o.Owner,')') As 
    Return "SELECT o.ID AS IdObsada,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) as Przedmiot, o.Kategoria, o.GetToAverage, o.DataAktywacji, o.DataDeaktywacji, o.LiczbaGodzin, o.User, o.ComputerIP, o.Version, o.Klasa,o.Nauczyciel as IdNauczyciel,o.Przedmiot as IdPrzedmiot,o.Owner FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON sn.IdNauczyciel=n.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE o.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE sk.IdSzkola='" & IdSzkola & "' AND sk.Virtual='" & Virtual & "') AND o.RokSzkolny='" & RokSzkolny & "' AND p.Typ<>'z' ORDER BY sp.Priorytet,p.Alias,o.DataAktywacji,Nauczyciel;"
  End Function
  Public Function SelectObsadByPrzedmiot(IdSzkola As String, RokSzkolny As String, Virtual As String) As String
    'CONCAT(o.User,' (','Wł: ',o.Owner,')') As 
    Return "SELECT o.ID AS IdObsada,sk.Nazwa_Klasy,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel, o.Kategoria, o.GetToAverage,  o.DataAktywacji,o.DataDeaktywacji,o.LiczbaGodzin, o.User,o.ComputerIP, o.Version, o.Klasa,o.Nauczyciel as IdNauczyciel,o.Przedmiot as IdPrzedmiot,o.Owner FROM obsada o INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON sn.IdNauczyciel=n.ID WHERE o.Przedmiot IN (SELECT sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sp.IdSzkola='" & IdSzkola & "' AND p.Typ<>'z') AND o.RokSzkolny='" & RokSzkolny & "' AND sk.Virtual='" & Virtual & "' ORDER BY sk.Kod_Klasy,o.DataAktywacji,n.Nazwisko COLLATE UTF8_polish_ci,n.Imie COLLATE UTF8_polish_ci;"
  End Function
  Public Function SelectObsadByNauczyciel(IdSzkola As String, RokSzkolny As String, Virtual As String) As String
    'CONCAT(o.User,' (','Wł: ',o.Owner,')') As 
    Return "SELECT o.ID AS IdObsada,sk.Nazwa_Klasy,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) as Przedmiot,o.Kategoria,o.GetToAverage,o.DataAktywacji,o.DataDeaktywacji,o.LiczbaGodzin,o.User, o.ComputerIP, o.Version,o.Klasa,o.Nauczyciel as IdNauczyciel,o.Przedmiot as IdPrzedmiot,o.Owner FROM obsada o INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE o.Nauczyciel IN (SELECT ID FROM szkola_nauczyciel sn WHERE sn.IdSzkola='" & IdSzkola & "') AND o.RokSzkolny='" & RokSzkolny & "' AND p.Typ<>'z' AND sk.Virtual='" & Virtual & "' ORDER BY sk.Kod_Klasy,sp.Priorytet,p.Alias,o.DataAktywacji;"
  End Function
  Public Overloads Function SelectClasses(IdSzkola As String, RokSzkolny As String, Virtual As String) As String
    Return "SELECT DISTINCT sk.ID,sk.Nazwa_Klasy FROM szkola_klasa sk,przydzial p WHERE sk.ID=p.Klasa AND sk.IdSzkola='" & IdSzkola & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.Virtual='" & Virtual & "' ORDER BY sk.Kod_Klasy;"

  End Function

  Public Overloads Function SelectClasses(IdSzkola As String, Virtual As String) As String
    Return "SELECT DISTINCT sk.ID,sk.Nazwa_Klasy,Kod_klasy FROM szkola_klasa sk WHERE sk.IdSzkola='" & IdSzkola & "' AND sk.Virtual='" & Virtual & "' ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectBelfer(IdSzkola As String) As String
    Return "SELECT sn.ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel FROM szkola_nauczyciel sn,nauczyciel n WHERE sn.IdNauczyciel=n.ID AND sn.IdSzkola='" & IdSzkola & "' AND sn.Status=1 ORDER BY n.Nazwisko,n.Imie;"
  End Function
  Public Overloads Function SelectPrzedmiot(Szkola As String) As String
    Return "SELECT sp.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) as Przedmiot FROM przedmiot p,szkola_przedmiot sp WHERE p.ID=sp.IdPrzedmiot AND p.Typ<>'Z' AND sp.IdSzkola='" & Szkola & "' Order by sp.Priorytet,p.Alias;"
  End Function

  Public Function SelectStudentAllocation(Klasa As String, RokSzkolny As String) As String
    Return "SELECT p.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1 ORDER BY p.NrwDzienniku,u.Nazwisko,u.Imie;"
  End Function
  Public Function DeleteObsada(ByVal ID As String) As String
    Return "DELETE FROM obsada WHERE ID='" & ID & "';"
  End Function
  Public Function InsertObsada() As String
    Return "INSERT INTO obsada VALUES(NULL,?Klasa,?Przedmiot,?Nauczyciel,?RokSzkolny,?Kategoria,?GetToAverage,?DataAktywacji,NULL,?LiczbaGodzin,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function InsertVirtualAllocation() As String
    Return "INSERT INTO nauczanie_indywidualne VALUES(NULL,?Przydzial,?Obsada,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateObsada() As String
    Return "UPDATE obsada SET Klasa=?Klasa,Przedmiot=?Przedmiot,Nauczyciel=?Nauczyciel,RokSzkolny=?RokSzkolny,Kategoria=?Kategoria,GetToAverage=?GetToAverage,DataAktywacji=?DataAktywacji,LiczbaGodzin=?LiczbaGodzin,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function UpdateKolumna() As String
    Return "UPDATE kolumna SET IdObsada=?IdObsadaNew,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE IdObsada=?IdObsadaOld;"
  End Function
  Public Function CountActiveStaff(Klasa As String, Przedmiot As String, RokSzkolny As String) As String
    'Return "Select o.ID FROM obsada o WHERE o.Klasa='" & Klasa & "' AND o.Przedmiot='" & Przedmiot & "' AND o.DataDeaktywacji is null;"
    Return "Select o.ID FROM obsada o WHERE o.Klasa='" & Klasa & "' AND o.Przedmiot='" & Przedmiot & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.DataDeaktywacji is null;"
  End Function
  Public Function SelectClosingNotActiveStaff(Klasa As String, Przedmiot As String) As String
    Return "Select o.ID,o.DataDeaktywacji FROM obsada o WHERE o.Klasa='" & Klasa & "' AND o.Przedmiot='" & Przedmiot & "' AND o.DataDeaktywacji is not null ORDER BY o.DataDeaktywacji DESC LIMIT 1;"
  End Function
  Public Function DeactivateStaff() As String
    Return "UPDATE obsada SET DataDeaktywacji=?DataDeaktywacji,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
End Class

Public Class KolumnaSQL
  Public Function SelectKolumna(RokSzkolny As String, Klasa As String) As String
    'CONCAT(ok.User,' (','Wł: ',ok.Owner,')') As
    Return "SELECT ok.ID,ok.NrKolumny,ow.Nazwa,ow.KolorHex,ok.Waga,ok.Poprawa, ok.User,ok.ComputerIP,ok.Version,o.ID AS IdObsada,ow.ID as IdOpisWyniku,ok.Typ,ok.Owner FROM kolumna ok LEFT JOIN opis_wyniku ow ON ok.IdOpis=ow.ID, obsada o WHERE ok.IdObsada=o.ID AND ok.IdObsada IN (SELECT ID FROM obsada WHERE Klasa=" & Klasa & " AND RokSzkolny='" & RokSzkolny & "') ORDER BY NrKolumny;"
  End Function
  Public Overloads Function SelectClasses(IdSzkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT sk.ID,sk.Nazwa_Klasy,sk.Virtual FROM szkola_klasa sk INNER JOIN obsada o ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sk.Kod_Klasy;"
  End Function

  Public Function SelectPrzedmiot(Klasa As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT v1.ID AS IdObsada,v2.SchoolTeacherID,CONCAT_WS('',v1.Alias,' {',v2.Belfer,'}') as Przedmiot FROM (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias, sp.Priorytet FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND p.Typ<>'Z') AS v1, (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID,o.Nauczyciel AS SchoolTeacherID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v1.Priorytet;"
    'Return "SELECT sp.ID,p.Alias FROM przedmiot p,szkola_przedmiot sp WHERE p.ID=sp.IdPrzedmiot AND p.Typ<>'z' AND sp.ID IN (SELECT DISTINCT Przedmiot FROM obsada o WHERE Klasa='" & Klasa & "' AND RokSzkolny='" & RokSzkolny & "') Order by sp.Priorytet,p.Alias;"
  End Function
  Public Function SelectKolumnaID(IdObsada As String, Typ As String) As String
    Return "SELECT k.ID FROM kolumna k WHERE k.IdObsada=" & IdObsada & " AND k.Typ='" & Typ & "' ORDER BY k.NrKolumny;"
  End Function
  Public Function DeleteKolumna(ByVal ID As String) As String
    Return "DELETE FROM kolumna WHERE ID='" & ID & "';"
  End Function
  Public Function InsertKolumna() As String
    Return "INSERT INTO kolumna VALUES(NULL,?NrKolumny,?IdObsada,?IdOpis,?Typ,?Waga,?Poprawa,0,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateKolumna() As String
    Return "UPDATE kolumna SET IdOpis=?IdOpis,Waga=?Waga,Poprawa=?Poprawa,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Overloads Function UpdateNrKolumna() As String
    Return "UPDATE kolumna SET NrKolumny=?Nr,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Overloads Function UpdateNrKolumna(Offset As Byte) As String
    Return "UPDATE kolumna SET NrKolumny=NrKolumny+" & Offset & ",User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE IdObsada=?IdObsada AND Typ=?Typ AND NrKolumny=?NrKolumny;"
  End Function
End Class

Public Class OpcjeSQL
  Public Function SelectOption() As String
    Return "SELECT Name,Value,Type,Description,IdSchool,StartDate,EndDate,Owner,User,ComputerIP FROM opcje w WHERE IdSchool is null AND Name<>'DBVersion';"
  End Function
  Public Function SelectOption(OptionName As String, OptionType As String) As String
    Return "Select Value FROM opcje WHERE Name='" & OptionName & "' AND Type='" & OptionType & "' AND IdSchool is null;"
  End Function
  Public Function SelectOption(OptionName As String, OptionType As String, IdSchool As String) As String
    Return "Select Value FROM opcje WHERE Name='" & OptionName & "' AND Type='" & OptionType & "' AND IdSchool='" & IdSchool & "';"
  End Function
  Public Function SelectOption(OptionName As String, OptionType As String, IdSchool As String, CurrDate As Date) As String
    Return "Select NULLIF(Value,'') FROM opcje WHERE Name='" & OptionName & "' AND Type='" & OptionType & "' AND IdSchool='" & IdSchool & "' AND '" & CurrDate & "'>=StartDate AND ('" & CurrDate & "'<=EndDate OR EndDate IS NULL);"
  End Function
  Public Function InsertOption() As String
    Return "INSERT INTO opcje (ID,Name,Value,Type,Description,IdSchool,StartDate,EndDate,Owner,User,ComputerIP,Version) VALUES (NULL,?Name,?Value,?Type,?Description,?IdSchool,?StartDate,?EndDate,?Owner,?User,?ComputerIP,NULL);"
  End Function
End Class

Public Class WynikiSQL
  'Public Function SelectStudent(IdKlasa As String, RokSzkolny As String) As String
  '  Return "SELECT u.ID,p.NrwDzienniku,CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2) as Student,p.ID AS IdPrzydzial, p.StatusAktywacji, Cast(p.DataDeaktywacji as Date) AS DataDeaktywacji FROM uczen u,przydzial p WHERE u.ID=p.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.MasterRecord=1 AND p.Klasa='" & IdKlasa & "' ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  'End Function
  Public Function SelectGrupa(IdKlasa As String, RokSzkolny As String) As String
    Return "Select g.IdPrzydzial,g.IdSzkolaPrzedmiot,p.StatusAktywacji,Cast(p.DataDeaktywacji as Date) AS Deaktywacja,p.DataAktywacji From grupa g INNER JOIN przydzial p ON p.ID=g.IdPrzydzial WHERE p.Klasa='" & IdKlasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.MasterRecord=1 ORDER BY p.NrwDzienniku;"
    'Return "Select g.IdPrzydzial,g.IdSzkolaPrzedmiot From grupa g INNER JOIN przydzial p ON p.ID=g.IdPrzydzial WHERE p.Klasa='" & IdKlasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1;"
  End Function
  Public Function SelectPrzedmiot(Klasa As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT DISTINCT v1.ID AS IdObsada,CONCAT_WS('',v1.Alias,' {',v2.Belfer,'}') as Obsada, v2.SchoolTeacherID,v1.Przedmiot FROM (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias, sp.Priorytet,o.Przedmiot FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data.ToShortDateString & "' AND (DATE(o.DataDeaktywacji) >= '" & Data.ToShortDateString & "' OR o.DataDeaktywacji is null) AND p.Typ<>'Z') AS v1, (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID,o.Nauczyciel AS SchoolTeacherID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v1.Priorytet;"
  End Function
  'Public Function SelectObsada(Obsada As String) As String
  '  Return "Select Przedmiot From obsada WHERE ID='" & Obsada & "';"
  'End Function

  Public Function SelectKolumna(Obsada As String) As String
    Return "SELECT k.ID,k.NrKolumny,ow.Nazwa,k.Typ,k.Waga,ow.KolorHex,k.Poprawa,k.Lock FROM kolumna k LEFT JOIN opis_wyniku ow ON k.IdOpis=ow.ID WHERE k.IdObsada='" & Obsada & "' ORDER BY k.NrKolumny;"
  End Function

  Public Function SelectResult(Obsada As String) As String
    'CONCAT(w.User,' (','Wł: ',w.Owner,')') As 
    Return "Select w.ID,o.Ocena,w.Data,w.User, w.ComputerIP, w.Version, w.IdUczen, w.IdKolumna, k.Typ,w.IdOcena,o.Waga AS WagaOceny,k.Waga AS WagaKolumny,w.Owner From wyniki w Inner Join uczen u on w.IdUczen=u.ID Inner Join skala_ocen o on w.IdOcena=o.ID INNER Join kolumna k on w.IdKolumna=k.ID Where k.IdObsada='" & Obsada & "' Order by w.Data,w.Version;"
  End Function
  Public Function SelectBehaviorResult(IdKolumnaZachowania As String) As String
    Return "Select w.ID,w.IdOcena,w.Data,w.User, w.ComputerIP, w.Version,w.Owner,w.IdUczen From wyniki w Where w.IdKolumna='" & IdKolumnaZachowania & "';"
  End Function
  Public Function SelectIndividualCourse(RokSzkolny As String, Klasa As String) As String
    Return "SELECT o.Klasa,sp.IdPrzedmiot,o.DataAktywacji,o.DataDeaktywacji,n.IdPrzydzial FROM nauczanie_indywidualne n INNER JOIN obsada o ON o.ID=n.IdObsada INNER JOIN przydzial p ON p.ID=n.IdPrzydzial INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot WHERE p.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1;"
  End Function
  Public Function SelectCaution(ByVal Klasa As String, RokSzkolny As String, Semestr As String, Przedmiot As String) As String
    'Return "SELECT z.ID,z.IdPrzedmiot AS IdPrzedmiot,pdl.IdUczen,z.Semestr, z.User,z.ComputerIP,z.Version,z.IdPrzydzial,z.Owner FROM zagrozenia z,przydzial pdl WHERE pdl.ID=z.IdPrzydzial AND pdl.Klasa='" & Klasa & "' AND pdl.RokSzkolny='" & RokSzkolny & "' AND z.Semestr='" & Semestr & "' AND z.IdPrzedmiot='" & Przedmiot & "';"
    Return "SELECT DISTINCT z.ID,z.IdPrzydzial, z.User,z.ComputerIP,z.Version,z.Owner FROM zagrozenia z,przydzial pdl WHERE pdl.ID=z.IdPrzydzial AND pdl.Klasa='" & Klasa & "' AND pdl.RokSzkolny='" & RokSzkolny & "' AND z.Semestr='" & Semestr & "' AND z.IdPrzedmiot='" & Przedmiot & "';"
  End Function
  Public Function SelectVirtualCaution(ByVal Klasa As String, RokSzkolny As String, Semestr As String, Przedmiot As String) As String
    'Return "SELECT DISTINCT z.ID,z.IdPrzedmiot AS IdPrzedmiot,pdl.IdUczen, z.User,z.ComputerIP,z.Version,z.IdPrzydzial,z.Owner FROM zagrozenia z INNER JOIN przydzial pdl ON z.IdPrzydzial=pdl.ID INNER JOIN nauczanie_indywidualne ni ON ni.IdPrzydzial=pdl.ID INNER JOIN obsada o ON ni.IdObsada=o.ID WHERE o.Klasa='26' AND o.RokSzkolny='2014/2015' AND z.Semestr='2' AND z.IdPrzedmiot=9;"
    Return "SELECT DISTINCT z.ID,z.IdPrzydzial, z.User,z.ComputerIP,z.Version,z.Owner FROM zagrozenia z INNER JOIN nauczanie_indywidualne ni ON ni.IdPrzydzial=z.IdPrzydzial INNER JOIN obsada o ON ni.IdObsada=o.ID WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND z.Semestr='" & Semestr & "' AND z.IdPrzedmiot='" & Przedmiot & "';"
  End Function
  Public Function SelectPrzedmiotId(Obsada As String) As String
    Return "SELECT p.ID AS IdPrzedmiot FROM przedmiot p INNER JOIN szkola_przedmiot sp ON sp.IdPrzedmiot=p.ID INNER JOIN obsada o ON o.Przedmiot=sp.ID WHERE o.ID='" & Obsada & "';"
  End Function
  Public Function SelectKolumnaZachowanie(Klasa As String, RokSzkolny As String, Semestr As String) As String
    'Return "SELECT o.ID FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE p.Typ='Z' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "'"
    Return "SELECT k.ID,k.Lock FROM kolumna k WHERE k.Typ='" & Semestr & "' AND k.IdObsada IN (SELECT o.ID FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE p.Typ='Z' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "');"
  End Function
  Public Function SelectWychowawca(Klasa As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Wychowawca,u.Login FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON sn.IdNauczyciel=n.ID LEFT JOIN user u ON n.ID=u.IdNauczyciel WHERE o.Przedmiot IN (SELECT ID FROM szkola_przedmiot sp WHERE IdPrzedmiot IN (SELECT ID FROM przedmiot WHERE Typ='z')) AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data.ToShortDateString & "' AND (DATE(o.DataDeaktywacji) >= '" & Data.ToShortDateString & "' OR o.DataDeaktywacji is null);"
  End Function
  Public Function SelectEndMarks() As String
    Return "SELECT o.ID, o.Nazwa FROM skala_ocen o WHERE o.Typ IN ('P','PZ') AND o.PodTyp IN ('K','CK') ORDER BY o.SortOrder;"
  End Function
  Public Function SelectPartialMarks() As String
    Return "SELECT o.ID, o.Ocena FROM skala_ocen o WHERE o.Typ='P' AND o.PodTyp IN ('C','CK');"
  End Function
  Public Function SelectMarksByWaga() As String
    Return "SELECT CAST(Waga AS UNSIGNED) AS Waga,Nazwa FROM skala_ocen WHERE Typ IN ('P','PZ') AND PodTyp IN ('K','CK') AND Waga>=0 ORDER BY Waga;"
  End Function
  Public Function SelectBehaviorMarks() As String
    Return "SELECT o.ID, o.Nazwa FROM skala_ocen o WHERE o.Typ IN ('Z','PZ') AND o.PodTyp='K' ORDER BY o.SortOrder;"
  End Function
  Public Function SelectBehaviorMarksByImportance() As String
    Return "SELECT CAST(Waga AS UNSIGNED) AS Waga,Nazwa FROM skala_ocen WHERE Typ IN ('Z','PZ') AND PodTyp IN ('K') AND Waga>=0 ORDER BY Waga;"
  End Function
  Public Function SelectStudent(RokSzkolny As String, Klasa As String) As String
    Return "Select u.ID,p.NrwDzienniku,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,p.ID as IdPrzydzial, p.StatusAktywacji,p.DataAktywacji, p.DataDeaktywacji,p.MasterRecord FROM uczen u INNER JOIN przydzial p ON u.ID=p.IdUczen WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "'  ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectVirtualClasses(IdSzkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT o.Klasa As IdKlasa,sk.Nazwa_Klasy,sk.Virtual FROM szkola_klasa sk INNER JOIN obsada o ON o.Klasa=sk.ID INNER JOIN nauczanie_indywidualne ni ON ni.IdObsada=o.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectClasses(IdSzkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT sk.ID As IdKlasa,sk.Nazwa_Klasy,sk.Virtual FROM szkola_klasa sk INNER JOIN przydzial p ON p.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY sk.Kod_Klasy;"
  End Function
  Public Overloads Function InsertResult() As String
    Return "INSERT INTO wyniki (IdUczen,IdOcena,IdKolumna,Data,Owner,User,ComputerIP) VALUES(?IdUczen,?IdOcena,?IdKolumna,?Data,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function UpdateResult() As String
    Return "Update wyniki Set IdOcena=?IdOcena,Data=?Data,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL Where ID=?IdWynik;"
  End Function
  Public Function DeleteResult() As String
    Return "DELETE FROM wyniki Where ID=?IdWynik;"
  End Function
End Class

Public Class WorkingParamsSQL
  Public Function SelectSchoolName(IdSchool As String) As String
    Return "SELECT Nazwa FROM szkola WHERE ID='" & IdSchool & "';"
  End Function

End Class

Public Class HarmonogramSQL
  Public Function SelectActivityTime(IdSzkola As String) As String
    'CONCAT(User,' (','Wł: ',Owner,')') As
    Return "SELECT ID,NrLekcji,Time_Format(StartTime,'%H:%i'),Time_Format(EndTime,'%H:%i'), User,ComputerIP,Version,Owner FROM godzina_lekcyjna WHERE IdSzkola='" & IdSzkola & "' Order BY NrLekcji,StartTime;"
  End Function
  Public Function UpdateActivityTime() As String
    Return "Update godzina_lekcyjna Set NrLekcji=?NrLekcji,StartTime=?StartTime,EndTime=?EndTime,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function InsertActivityTime() As String
    Return "INSERT INTO godzina_lekcyjna (NrLekcji,StartTime,EndTime,IdSzkola,Owner,User,ComputerIP) VALUES(?NrLekcji,?StartTime,?EndTime,?IdSzkola,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function DeleteActivityTime() As String
    Return "DELETE FROM godzina_lekcyjna WHERE ID=?ID;"
  End Function
End Class


Public Class PlanSQL
  Public Function SelectPlan(IdSzkola As String, StartDate As String, EndDate As String) As String
    'CONCAT(User,' (','Wł: ',Owner,')') As
    Return "SELECT p.ID,p.Nazwa,p.StartDate,p.EndDate,p.Lock, p.User,p.ComputerIP,p.Version,p.Owner FROM plan_lekcji p WHERE p.IdSzkola='" & IdSzkola & "' AND p.StartDate>='" & StartDate & "' AND p.EndDate<='" & EndDate & "'  Order BY p.StartDate,p.EndDate;"
  End Function
  Public Function SelectPlan2(IdSzkola As String, StartDate As String, EndDate As String) As String
    'CONCAT(User,' (','Wł: ',Owner,')') As
    Return "SELECT p.ID,p.Lock,Concat(p.Nazwa,' (ważny od: ',p.StartDate,' do: ',p.EndDate,')') AS Nazwa FROM plan_lekcji p WHERE p.IdSzkola='" & IdSzkola & "' AND p.StartDate>='" & StartDate & "' AND p.EndDate<='" & EndDate & "'  Order BY p.StartDate desc,p.EndDate;"
  End Function
  Public Function SelectPlan3(IdSzkola As String, StartDate As String, EndDate As String) As String
    Return "SELECT p.ID,p.Nazwa,p.Lock,p.StartDate,p.EndDate FROM plan_lekcji p WHERE p.IdSzkola='" & IdSzkola & "' AND p.StartDate>='" & StartDate & "' AND p.EndDate<='" & EndDate & "'  Order BY p.StartDate desc,p.EndDate;"
  End Function
  Public Function UpdatePlan() As String
    Return "Update plan_lekcji pl Set pl.Nazwa=?Nazwa,pl.StartDate=?StartDate,pl.EndDate=?EndDate,pl.Lock=?Lock,pl.User='" & GlobalValues.AppUser.Login & "',pl.ComputerIP='" & GlobalValues.gblIP & "',pl.Version=NULL WHERE pl.ID=?ID;"
  End Function
  Public Function InsertPlan() As String
    Return "INSERT INTO plan_lekcji VALUES(NULL,?Nazwa,?IdSzkola,?StartDate,?EndDate,?Lock,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function DeletePlan() As String
    Return "DELETE FROM plan_lekcji WHERE ID=?ID;"
  End Function
  Public Function CountPlan(StartDate As Date, IdSzkola As String) As String
    Return "SELECT Count(ID) FROM plan_lekcji WHERE EndDate>='" & StartDate & "' AND IdSzkola='" & IdSzkola & "';"
  End Function
  Public Function CountLekcja(IdPlan As String) As String
    Return "SELECT Count(ID) FROM lekcja WHERE IdPlan='" & IdPlan & "';"
  End Function
  Public Function SelectLekcjaByKlasa(IdSzkola As String, RokSzkolny As String, Klasa As String) As String
    'CONCAT(l.User,' (','Wł: ',l.Owner,')') As
    Return "SELECT l.ID,g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,Group_Concat(DISTINCT Concat_WS('',v1.Alias,' {',v2.Belfer,'}') SEPARATOR ' ; ') as Przedmiot, l.User, l.ComputerIP, l.Version, l.IdPlan, l.DzienTygodnia, o.Klasa,l.IdGodzina AS IdGodzina,l.IdObsada,l.Sala,l.Owner,v1.Przedmiot AS Obsada FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.Priorytet,p.Alias as Przedmiot FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel) AS v2 ON l.IdObsada=v2.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' GROUP BY l.IdPlan,o.Klasa,l.DzienTygodnia,g.NrLekcji;"
  End Function
  Public Function SelectLekcjaByKlasa(IdSzkola As String, RokSzkolny As String) As String
    'CONCAT(l.User,' (','Wł: ',l.Owner,')') As
    Return "SELECT l.ID,g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,Group_Concat(DISTINCT Concat_WS('',v1.Alias,' {',v2.Belfer,'}') SEPARATOR ' ; ') as Przedmiot, l.User, l.ComputerIP, l.Version, l.IdPlan, l.DzienTygodnia, o.Klasa,l.IdGodzina AS IdGodzina,l.IdObsada,l.Sala,l.Owner,v1.Przedmiot AS Obsada FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.Priorytet,p.Alias as Przedmiot FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel) AS v2 ON l.IdObsada=v2.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' GROUP BY l.IdPlan,o.Klasa,l.DzienTygodnia,g.NrLekcji;"
  End Function
  Public Function SelectLekcjaByKlasaNoGroupConcat(IdSzkola As String, RokSzkolny As String, Klasa As String) As String
    'CONCAT(l.User,' (','Wł: ',l.Owner,')') As
    Return "SELECT l.ID,g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,Concat_WS('',v1.Alias,' {',v2.Belfer,'}') as Przedmiot, l.User, l.ComputerIP, l.Version, l.IdPlan,l.DzienTygodnia,o.Klasa,l.IdGodzina AS IdGodzina, l.IdObsada,l.Sala,l.Owner,v1.Przedmiot as Obsada FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.Priorytet,p.Alias as Przedmiot FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel) AS v2 ON l.IdObsada=v2.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' ORDER BY g.StartTime,v1.Priorytet,v1.Alias;"
  End Function

  Public Function SelectLekcjaByBelfer(IdSzkola As String, RokSzkolny As String, Nauczyciel As String) As String
    'CONCAT(l.User,' (','Wł: ',l.Owner,')') As
    Return "SELECT l.ID,g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,Group_Concat(DISTINCT Concat_WS('',v2.Klasa,' {',v1.Alias,'}') SEPARATOR ' ; ') as Klasa, l.User,l.ComputerIP,l.Version, l.IdPlan, l.DzienTygodnia, o.Nauczyciel,l.IdGodzina AS IdGodzina,l.IdObsada,l.Sala,l.Owner,Group_Concat(v2.Klasa SEPARATOR ';') AS Obsada FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT sk.Nazwa_Klasy AS Klasa,o.ID FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID) AS v2 ON l.IdObsada=v2.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Nauczyciel='" & Nauczyciel & "' GROUP BY l.IdPlan,o.Nauczyciel,l.DzienTygodnia,g.NrLekcji;"
  End Function
  Public Function SelectLekcjaByBelfer(IdSzkola As String, RokSzkolny As String) As String
    'CONCAT(l.User,' (','Wł: ',l.Owner,')') As
    Return "SELECT l.ID,g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,Group_Concat(DISTINCT Concat_WS('',v2.Klasa,' {',v1.Alias,'}') SEPARATOR ' ; ') as Klasa, l.User,l.ComputerIP,l.Version, l.IdPlan, l.DzienTygodnia, o.Nauczyciel,l.IdGodzina AS IdGodzina,l.IdObsada,l.Sala,l.Owner,Group_Concat(v2.Klasa SEPARATOR ';') AS Obsada FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT sk.Nazwa_Klasy AS Klasa,o.ID FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID) AS v2 ON l.IdObsada=v2.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' GROUP BY l.IdPlan,o.Nauczyciel,l.DzienTygodnia,g.NrLekcji;"
  End Function
  Public Function SelectLekcjaByBelferNoGroupConcat(IdSzkola As String, RokSzkolny As String, Nauczyciel As String) As String
    'CONCAT(l.User,' (','Wł: ',l.Owner,')') As 
    Return "SELECT l.ID,g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,Concat_WS('',v2.Klasa,' {',v1.Alias,'}') as Klasa, l.User,l.ComputerIP,l.Version, l.IdPlan,l.DzienTygodnia,o.Nauczyciel,l.IdGodzina AS IdGodzina,l.IdObsada,l.Sala,l.Owner,v2.Klasa as Obsada FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,CONCAT(p.Alias,' - gr ',sp.Grupa)) AS Alias FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT sk.Nazwa_Klasy AS Klasa,o.ID FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID) AS v2 ON l.IdObsada=v2.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Nauczyciel='" & Nauczyciel & "' ORDER BY g.StartTime;"
  End Function
  Public Function SelectLekcja(IdPlan As String) As String
    Return "SELECT IdObsada,DzienTygodnia,IdGodzina,Owner,Sala,User,ComputerIP FROM lekcja l WHERE IdPlan='" & IdPlan & "';"
  End Function
  Public Function SelectWeekDays(IdSzkola As String, RokSzkolny As String, IdPlan As String) As String
    Return "SELECT distinct l.DzienTygodnia FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE l.IdPlan='" & IdPlan & "' AND sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' Order BY l.DzienTygodnia; "
  End Function
  Public Function UpdateLekcja() As String
    Return "Update lekcja Set Sala=?Sala,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?IdLekcja;"
  End Function
  'Public Function UpdateLekcja() As String
  '  Return "Update lekcja Set IdGodzina=?IdGodzina,IdObsada=?IdObsada,Sala=?Sala,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?IdLekcja;"
  'End Function
  Public Overloads Function InsertLekcja() As String
    Return "INSERT INTO lekcja (IdPlan,IdObsada,DzienTygodnia,IdGodzina,Sala,Owner,User,ComputerIP) VALUES(?IdPlan,?IdObsada,?DzienTygodnia,?IdGodzina,?Sala,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Overloads Function CopyLesson() As String
    Return "INSERT INTO lekcja (IdPlan,IdObsada,DzienTygodnia,IdGodzina,Sala,Owner,User,ComputerIP) VALUES(?IdPlan,?IdObsada,?DzienTygodnia,?IdGodzina,?Sala,?Owner,?User,?IP);"
  End Function
  Public Function DeleteLekcja() As String
    Return "DELETE FROM lekcja WHERE ID=?ID;"
  End Function
  Public Function DeleteLekcja(IdPlan As String) As String
    Return "DELETE FROM lekcja WHERE IdPlan='" & IdPlan & "';"
  End Function
  Public Function SelectBelfer(IdSzkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT sn.ID,CONCAT_WS(' ',n.Nazwisko,n.Imie) as Belfer FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE sn.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY n.Nazwisko COLLATE utf8_polish_ci,n.Imie COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectBelfer(IdSzkola As String) As String
    Return "SELECT DISTINCT sn.ID,CONCAT_WS(' ',n.Nazwisko,n.Imie) as Belfer FROM szkola_nauczyciel sn INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE sn.IdSzkola='" & IdSzkola & "' AND sn.Status=1 ORDER BY n.Nazwisko COLLATE utf8_polish_ci,n.Imie COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectActivityTime(IdSzkola As String) As String
    Return "SELECT ID,Concat_WS(') ',NrLekcji,Concat_WS(' - ',Time_Format(StartTime,'%H:%i'),Time_Format(EndTime,'%H:%i'))) AS Godzina FROM godzina_lekcyjna WHERE IdSzkola='" & IdSzkola & "' Order BY NrLekcji,StartTime;"
  End Function
  Public Function SelectPrzedmiotByKlasa(Klasa As String, RokSzkolny As String, EndDate As String) As String
    Return "SELECT DISTINCT v1.ID AS IdObsada,CONCAT_WS('',v1.Alias,' {',v2.Belfer,'}') as Lekcja,v1.LiczbaGodzin FROM (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.Priorytet,o.LiczbaGodzin FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.DataAktywacji < '" & EndDate & "' AND (o.DataDeaktywacji >'" & EndDate & "' OR o.DataDeaktywacji is null)) AS v1, (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v1.Priorytet,v1.Alias;"
  End Function
  Public Function SelectPrzedmiotByBelfer(Belfer As String, RokSzkolny As String, EndDate As String) As String
    Return "SELECT DISTINCT v1.ID AS IdObsada,CONCAT_WS('',v2.Klasa,' {',v1.Alias,'}') as Lekcja,v1.LiczbaGodzin FROM (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.Priorytet,o.LiczbaGodzin FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Nauczyciel='" & Belfer & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.DataAktywacji < '" & EndDate & "' AND (o.DataDeaktywacji >'" & EndDate & "' OR o.DataDeaktywacji is null)) AS v1, (SELECT sk.Nazwa_Klasy AS Klasa,o.ID,sk.Kod_Klasy FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE o.Nauczyciel='" & Belfer & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v2.Kod_Klasy,v1.Priorytet;"
  End Function
  Public Function SelectClasses(IdSzkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT sk.ID,LEFT(sk.Kod_Klasy,1) AS Pion,sk.Nazwa_Klasy,sk.Kod_Klasy,sk.Virtual FROM szkola_klasa sk INNER JOIN obsada o ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function CountLessonByStaff(IdPlan As String, PlanEndDate As String) As String
    Return "SELECT l.IdObsada,COUNT(*) AS LiczbaGodzin FROM lekcja l WHERE l.IdPlan='" & IdPlan & "' AND l.IdObsada IN (SELECT o.ID FROM obsada o WHERE o.DataAktywacji < '" & PlanEndDate & "' AND (o.DataDeaktywacji > '" & PlanEndDate & "' OR o.DataDeaktywacji is null)) Group BY l.IdObsada;"
  End Function
  'Public Function SelectPion(IdSzkola As String, RokSzkolny As String) As String
  '  Return "SELECT DISTINCT LEFT(sk.Kod_Klasy,1) AS Pion FROM szkola_klasa sk INNER JOIN obsada o ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY Pion;"
  'End Function

End Class

Public Class TematSQL
  Public Overloads Function SelectTemat(RokSzkolny As String, Klasa As String, StartDate As String, EndDate As String) As String
    'CONCAT(t.User,' (','Wł: ',t.Owner,')') As
    Return "SELECT t.ID,g.NrLekcji,t.Nr,Concat_WS('',v1.Alias,' {',v2.Belfer,'}') as Przedmiot,t.Tresc,g.ID AS IdGodzina, v3.Zastepca,  t.User, t.ComputerIP,t.Version,l.DzienTygodnia,t.IdLekcja,v3.IdZastepstwo,v1.Przedmiot AS IdSzkolaPrzedmiot, v2.Nauczyciel, v3.Status, t.Data,t.Status AS StatusLekcji,t.Owner FROM temat t INNER JOIN lekcja l ON t.IdLekcja=l.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,o.Przedmiot FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID,o.Nauczyciel FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 ON l.IdObsada=v2.ID Left JOIN (SELECT sn.ID,Concat(n.Nazwisko,' ',n.Imie,' {',if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))),'}') as Zastepca,z.IdLekcja,z.Status,z.ID as IdZastepstwo FROM zastepstwo z INNER JOIN szkola_nauczyciel sn ON z.IdNauczycielSzkola=sn.ID INNER JOIN nauczyciel n on sn.IdNauczyciel=n.ID INNER JOIN szkola_przedmiot sp ON sp.ID=z.IdPrzedmiotSzkola INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE z.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "') as v3 ON v3.IdLekcja=l.ID WHERE t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' ORDER BY g.NrLekcji;"
  End Function
  Public Overloads Function SelectTemat(IdObsada As String, Nauczyciel As String, Przedmiot As String, Klasa As String, RokSzkolny As String) As String
    'CONCAT(t.User,' (','Wł: ',t.Owner,')') As
    Return "(SELECT t.ID,g.NrLekcji,t.Nr,t.Data,t.Tresc, t.User,t.ComputerIP,t.Version,MONTH(t.Data) AS Miesiac,g.ID AS IdGodzina,t.IdLekcja, o.Klasa,o.Przedmiot,t.Status,t.Owner FROM temat t INNER JOIN lekcja l ON t.IdLekcja=l.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE o.ID='" & IdObsada & "' AND t.ID NOT IN (SELECT t.ID FROM temat t INNER JOIN zastepstwo z ON t.IdLekcja=z.IdLekcja WHERE t.Data=z.Data)) UNION (SELECT t.ID,g.NrLekcji,t.Nr,t.Data,t.Tresc, t.User,t.ComputerIP,t.Version,MONTH(t.Data) AS Miesiac,g.ID AS IdGodzina,t.IdLekcja, o.Klasa,o.Przedmiot,t.Status,t.Owner FROM temat t INNER JOIN lekcja l ON t.IdLekcja=l.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE o.ID='" & IdObsada & "' AND t.ID IN (SELECT t.ID FROM temat t INNER JOIN zastepstwo z ON t.IdLekcja=z.IdLekcja WHERE t.Data=z.Data)) UNION (SELECT t.ID, g.NrLekcji, t.Nr, t.Data, t.Tresc, t.User, t.ComputerIP, t.Version,MONTH(t.Data) AS Miesiac,g.ID AS IdGodzina,t.IdLekcja,o.Klasa,o.Przedmiot,t.Status,t.Owner FROM temat t INNER JOIN lekcja l ON t.IdLekcja=l.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE l.ID IN (SELECT l.ID FROM lekcja l INNER JOIN zastepstwo z ON z.IdLekcja=l.ID INNER JOIN obsada o ON l.IdObsada=o.ID WHERE t.Data=z.Data AND z.Status=1 AND z.IdNauczycielSzkola='" & Nauczyciel & "' AND z.IdPrzedmiotSzkola='" & Przedmiot & "' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "')) ORDER BY Data,NrLekcji;"
  End Function
  'Public Function SelectKlasa(IdSzkola As String, RokSzkolny As String) As String
  '  Return "SELECT DISTINCT sk.ID,sk.Nazwa_Klasy FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "';"
  'End Function
  Public Function SelectMaxNr(Klasa As String, RokSzkolny As String) As String
    Return "SELECT IFNULL(MAX(t.Nr),0) AS Nr,l.IdObsada FROM temat t RIGHT JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON l.IdObsada=o.ID WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' GROUP BY l.IdObsada;"
  End Function
  Public Function SelectMaxNr(IdObsada As String) As String
    Return "SELECT IFNULL(MAX(t.Nr),0) AS Nr,l.IdObsada FROM temat t RIGHT JOIN lekcja l ON l.ID=t.IdLekcja WHERE l.IdObsada='" & IdObsada & "' GROUP BY l.IdObsada;"
  End Function
  Public Function SelectFrekwencja(IdObsada As String) As String
    Return "SELECT f.ID,f.IdLekcja,f.IdUczen,f.Data,f.Typ FROM frekwencja f INNER JOIN lekcja l ON l.ID=f.Idlekcja WHERE l.IdObsada='" & IdObsada & "';"
  End Function
  Public Function SelectFrekwencja(StartDate As String, EndDate As String) As String
    Return "SELECT f.ID,f.IdLekcja,f.IdUczen,f.Data,f.Typ FROM frekwencja f INNER JOIN lekcja l ON l.ID=f.Idlekcja WHERE f.Data Between '" & StartDate & "' AND '" & EndDate & "';"
  End Function
  Public Function SelectStudent(RokSzkolny As String, Klasa As String) As String
    Return "Select u.ID,p.NrwDzienniku,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,p.ID as IdPrzydzial, p.StatusAktywacji,p.DataAktywacji, p.DataDeaktywacji FROM uczen u INNER JOIN przydzial p ON u.ID=p.IdUczen WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND  p.MasterRecord=1 ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"

  End Function
  Public Function SelectStudent(RokSzkolny As String, Klasa As String, DataZajec As Date) As String
    'Return "Select u.ID,p.NrwDzienniku,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,p.ID as IdPrzydzial, p.StatusAktywacji,p.DataDeaktywacji FROM uczen u INNER JOIN przydzial p ON u.ID=p.IdUczen WHERE p.MasterRecord=1 AND p.ID IN (SELECT DISTINCT IdPrzydzial FROM nauczanie_indywidualne ni INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' AND  o.DataAktywacji < '" & DataZajec.ToShortDateString & "' AND (o.DataDeaktywacji > '" & DataZajec.ToShortDateString & "' OR o.DataDeaktywacji is null)) ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
    Return "Select u.ID,p.NrwDzienniku,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,p.ID as IdPrzydzial, p.StatusAktywacji,p.DataAktywacji, p.DataDeaktywacji, o.Przedmiot, p.MasterRecord FROM uczen u INNER JOIN przydzial p ON u.ID=p.IdUczen INNER JOIN nauczanie_indywidualne ni ON p.ID=ni.IdPrzydzial INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' AND  o.DataAktywacji < '" & DataZajec.ToShortDateString & "' AND (o.DataDeaktywacji > '" & DataZajec.ToShortDateString & "' OR o.DataDeaktywacji is null) AND p.MasterRecord=1 ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function

  Public Function SelectLekcja(RokSzkolny As String, Szkola As String, Klasa As String, Data As Date) As String
    Return "SELECT l.ID,Concat_WS(' ',v3.NrLekcji,') ',v3.Godzina,' --> ',v1.Alias,' {',v2.Belfer,'}') as Lekcja FROM lekcja l INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 ON l.IdObsada=v2.ID INNER JOIN (SELECT Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,g.ID,g.NrLekcji FROM godzina_lekcyjna g) as v3 ON l.IdGodzina=v3.ID WHERE l.IdPlan IN (SELECT ID FROM plan_lekcji p WHERE '" & Data.ToShortDateString & "' >= p.StartDate AND '" & Data.ToShortDateString & "' <= p.EndDate AND p.IdSzkola='" & Szkola & "') AND l.DzienTygodnia='" & Weekday(Data, FirstDayOfWeek.Monday) & "' AND l.ID NOT IN (SELECT IdLekcja FROM temat t WHERE Data='" & Data.ToShortDateString & "') ORDER BY v3.NrLekcji;"
  End Function
  Public Function SelectLekcja(IdObsada As String, Szkola As String, Data As Date) As String
    Return "SELECT l.ID,Concat(v1.NrLekcji,') ',v1.Godzina) as Lekcja FROM lekcja l INNER JOIN (SELECT Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina,g.ID,g.NrLekcji FROM godzina_lekcyjna g) as v1 ON l.IdGodzina=v1.ID WHERE l.IdObsada='" & IdObsada & "' AND l.IdPlan IN (SELECT ID FROM plan_lekcji p WHERE '" & Data.ToShortDateString & "' >= p.StartDate AND '" & Data.ToShortDateString & "' <= p.EndDate AND p.IdSzkola='" & Szkola & "') AND l.DzienTygodnia='" & Weekday(Data, FirstDayOfWeek.Monday) & "' AND l.ID NOT IN (SELECT IdLekcja FROM temat t WHERE Data='" & Data.ToShortDateString & "') ORDER BY v1.NrLekcji;"
  End Function
  Public Function SelectZastepca(Klasa As String, RokSzkolny As String, Data As String) As String
    Return "SELECT CONCAT_WS('',CONCAT_WS(' ',n.Nazwisko,n.Imie),' {',if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))),'}') As Zastepca,z.IdLekcja,z.ID AS IdZastepstwo,z.Status FROM zastepstwo z INNER JOIN szkola_nauczyciel sn ON z.IdNauczycielSzkola=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel INNER JOIN szkola_przedmiot sp ON z.IdPrzedmiotSzkola=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN lekcja l ON l.ID=z.IdLekcja INNER JOIN obsada o ON o.ID=l.IdObsada WHERE z.Data='" & Data & "' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function SelectZastepca(IdObsada As String, Data As String) As String
    Return "SELECT CONCAT_WS('',CONCAT_WS(' ',n.Nazwisko,n.Imie),' {',if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))),'}') As Zastepca,z.IdLekcja,z.ID AS IdZastepstwo,z.Status FROM zastepstwo z INNER JOIN szkola_nauczyciel sn ON z.IdNauczycielSzkola=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel INNER JOIN szkola_przedmiot sp ON z.IdPrzedmiotSzkola=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN lekcja l ON l.ID=z.IdLekcja WHERE z.Data='" & Data & "' AND z.IdLekcja IN (SELECT l.ID FROM lekcja l WHERE IdObsada='" & IdObsada & "');"
  End Function
  Public Function SelectZastepstwo(IdObsada As String) As String
    Return "SELECT Concat(n.Nazwisko,' ',n.Imie,' {',if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))),'}') as Zastepca,z.IdLekcja,z.Status,z.ID as IdZastepstwo,z.Data FROM zastepstwo z INNER JOIN szkola_nauczyciel sn ON z.IdNauczycielSzkola=sn.ID INNER JOIN nauczyciel n on sn.IdNauczyciel=n.ID INNER JOIN szkola_przedmiot sp ON sp.ID=z.IdPrzedmiotSzkola INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot INNER JOIN lekcja l ON z.Idlekcja=l.ID INNER JOIN temat t ON t.IdLekcja=l.ID WHERE t.Data=z.Data AND l.IdObsada='" & IdObsada & "';"
  End Function
  Public Function SelectZastepstwo(Nauczyciel As String, Przedmiot As String, Klasa As String, RokSzkolny As String) As String
    Return "SELECT Concat(if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))),' {',n.Nazwisko,' ',n.Imie,'}') as Zastepca,z.IdLekcja,z.Status,z.ID as IdZastepstwo,z.Data FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n on sn.IdNauczyciel=n.ID INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot INNER JOIN lekcja l ON l.IdObsada=o.ID INNER JOIN temat t ON t.IdLekcja=l.ID INNER JOIN zastepstwo z ON z.IdLekcja=l.ID WHERE t.Data=z.Data AND  z.IdNauczycielSzkola='" & Nauczyciel & "' AND z.IdPrzedmiotSzkola='" & Przedmiot & "' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function SelectGodzina(IdGodzina As String) As String
    Return "SELECT Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i')) as Godzina FROM godzina_lekcyjna g WHERE g.ID='" & IdGodzina & "';"
  End Function
  Public Overloads Function SelectBelfer(Szkola As String, NotId As Integer) As String
    Return "SELECT sn.ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer FROM szkola_nauczyciel sn INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE sn.IdSzkola='" & Szkola & "' AND sn.Status=1 AND sn.ID <> '" & NotId & "' ORDER BY n.Nazwisko COLLATE utf8_polish_ci,n.Imie COLLATE utf8_polish_ci;"
  End Function
  Public Overloads Function SelectBelfer(Szkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT o.Nauczyciel,Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE sn.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND Status=1 ORDER BY n.Nazwisko,n.Imie;"
  End Function

  Public Overloads Function SelectPrzedmiot(Klasa As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT o.Przedmiot,l.ID,o.Nauczyciel,l.IdObsada FROM obsada o INNER JOIN lekcja l ON o.ID=l.IdObsada INNER JOIN plan_lekcji pl ON pl.ID=l.IdPlan WHERE o.Klasa='" & Klasa & "' AND RokSzkolny='" & RokSzkolny & "' AND l.DzienTygodnia='" & Weekday(Data, FirstDayOfWeek.Monday) & "' AND '" & Data.ToShortDateString & "' >= pl.StartDate AND '" & Data.ToShortDateString & "' <= pl.EndDate;"

  End Function
  'Public Function SelectObsada(IdObsada As String, RokSzkolny As String) As String
  '  Return "SELECT o.Klasa,o.Przedmiot,o.Nauczyciel FROM obsada o WHERE o.RokSzkolny='" & RokSzkolny & "' AND o.ID='" & IdObsada & "';"
  'End Function
  Public Function SelectObsadaByBelfer(Nauczyciel As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT v1.ID AS IdObsada,CONCAT_WS('',v2.Klasa,' {',v1.Alias,'}') as Obsada,v1.Przedmiot,v1.Klasa,v2.Virtual FROM (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.Priorytet,o.Przedmiot,o.Klasa FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v1, (SELECT sk.Nazwa_Klasy AS Klasa,o.ID,sk.Kod_klasy,sk.Virtual FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v2.Kod_Klasy,v1.Priorytet;"
    'Return "SELECT DISTINCT v1.ID AS IdObsada,CONCAT_WS('',v2.Klasa,' {',v1.Alias,'}') as Obsada FROM (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.Priorytet FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "' AND p.Typ<>'Z') AS v1, (SELECT sk.Nazwa_Klasy AS Klasa,o.ID,sk.Kod_klasy FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v2.Kod_Klasy,v1.Priorytet;"
  End Function

  Public Function SelectIndividualStaff(RokSzkolny As String, Klasa As String, Data As Date) As String
    Return "SELECT ni.IdPrzydzial,o.Przedmiot,Date(o.DataAktywacji) AS DataAktywacji,Date(o.DataDeaktywacji) AS DataDeaktywacji FROM nauczanie_indywidualne ni INNER JOIN przydzial p ON p.ID=ni.IdPrzydzial INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE p.Klasa='" & Klasa & "' AND o.DataAktywacji < '" & Data & "' AND (o.DataDeaktywacji >'" & Data & "' OR o.DataDeaktywacji is null) AND p.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function SelectIndividualStaff(RokSzkolny As String, Klasa As String) As String
    Return "SELECT ni.IdPrzydzial,o.Przedmiot,Date(o.DataAktywacji) AS DataAktywacji,Date(o.DataDeaktywacji) AS DataDeaktywacji FROM nauczanie_indywidualne ni INNER JOIN przydzial p ON p.ID=ni.IdPrzydzial INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function SelectEvent(IdObsada As String) As String
    Return "SELECT t.Info,t.Data,t.Status,t.IdLekcja FROM terminarz t WHERE t.IdLekcja IN (SELECT ID FROM lekcja WHERE IdObsada='" & IdObsada & "');"
  End Function
  Public Function InsertTopic() As String
    Return "INSERT INTO temat (Nr,Tresc,IdLekcja,Data,Status,Owner,User,ComputerIP) VALUES(?Nr,?Tresc,?IdLekcja,?Data,?StatusLekcji,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function InsertAbsence() As String
    Return "INSERT INTO frekwencja (IdUczen,IdLekcja,Typ,Data,Owner,User,ComputerIP) VALUES(?IdUczen,?IdLekcja,?Typ,?Data,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function DeleteTopic() As String
    Return "DELETE FROM temat WHERE ID=?ID;"
  End Function
  Public Function DeleteAbsence() As String
    Return "DELETE FROM frekwencja WHERE ID=?ID;"
  End Function
  Public Function UpdateTopic() As String
    Return "UPDATE temat SET Nr=?Nr,Tresc=?Tresc,Status=?StatusLekcji,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function UpdateAbsence() As String
    Return "UPDATE frekwencja SET Typ=?Typ,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function UpdateSubstituteStatus() As String
    Return "UPDATE zastepstwo SET Status=?Status,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?IdZastepstwo;"
  End Function
End Class

Public Class StudentSQL
  Public Overloads Function SelectStudents(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    Return "SELECT u.ID,p.NrwDzienniku,CAST(u.NrArkusza AS CHAR) AS NrArkusza,Concat_WS(' ',u.Nazwisko,u.Imie,u.Imie2) as NazwiskoImie,sk.Nazwa_Klasy,CAST(u.DataUr AS CHAR) AS DataUr,u.Pesel,u.Nazwisko,u.Imie,u.Imie2,m.Nazwa as MiejsceZam,m1.Nazwa AS MiejsceUr,k.Nazwa as Kraj, p.StatusAktywacji,p.Klasa,u.Man,sk.Kod_Klasy,p.ID as IdPrzydzial,p.DataAktywacji,p.DataDeaktywacji FROM uczen u Left Join przydzial p on u.ID=p.IdUczen LEFT JOIN szkola_klasa sk on p.Klasa=sk.ID LEFT JOIN miejscowosc m ON u.IdMiejsceZam=m.ID LEFT JOIN miejscowosc m1 ON u.IdMiejsceUr=m1.ID LEFT JOIN kraj k ON m1.IdKraj=k.ID WHERE p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & IdSzkola & "' AND p.MasterRecord=1 ORDER BY sk.Kod_Klasy,p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Overloads Function SelectStudents(ByVal RokSzkolny As String, ByVal IdSzkola As String, PrintContent As String) As String
    Return "SELECT " & PrintContent & "p.Klasa FROM uczen u Left Join przydzial p on u.ID=p.IdUczen LEFT JOIN szkola_klasa sk on p.Klasa=sk.ID LEFT JOIN miejscowosc m ON u.IdMiejsceZam=m.ID LEFT JOIN miejscowosc m1 ON u.IdMiejsceUr=m1.ID WHERE p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & IdSzkola & "' AND p.StatusAktywacji=1 Order By p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectStudentsID(ByVal RokSzkolny As String, ByVal Klasa As String) As String
    Return "SELECT u.ID FROM uczen u, przydzial p WHERE u.ID=p.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "' AND p.StatusAktywacji='1' ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectDetails(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    Return "SELECT u.ID,m.Nazwa as MiejsceUr,w.Nazwa as WojUr,p.DataDeaktywacji,m1.Nazwa as MiejsceZam, u.Ulica, u.NrDomu, u.NrMieszkania, m1.Poczta, m1.KodPocztowy,w1.Nazwa as WojZam,m1.Miasto,u.Tel,k.Nazwa as Kraj, u.TelKom1, u.TelKom2, u.ImieOjca, u.NazwiskoOjca, u.ImieMatki, u.NazwiskoMatki, u.User,u.ComputerIP, u.Version,m.ID as IdMiejsceUr,m1.ID as IdMiejsceZam,u.Owner,p.DataAktywacji FROM uczen u LEFT JOIN miejscowosc m on u.IdMiejsceUr=m.ID LEFT JOIN wojewodztwa w on m.KodWoj=w.KodWoj LEFT JOIN kraj k ON m.IdKraj=k.ID LEFT JOIN miejscowosc m1 on u.IdMiejsceZam=m1.ID LEFT JOIN wojewodztwa w1 on m1.KodWoj=w1.KodWoj Left JOIN przydzial p on u.ID=p.IdUczen LEFT JOIN szkola_klasa sk on p.Klasa=sk.ID WHERE p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & IdSzkola & "' AND p.MasterRecord=1;"
  End Function
  Public Function SelectWychowawca(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    Return "SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Wychowawca,o.Klasa,u.Login FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON sn.IdNauczyciel=n.ID INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa LEFT JOIN user u ON n.ID=u.IdNauczyciel WHERE o.Przedmiot IN (SELECT ID FROM szkola_przedmiot sp WHERE IdPrzedmiot IN (SELECT ID FROM przedmiot WHERE Typ='z')) AND o.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE sk.IdSzkola='" & IdSzkola & "') AND o.RokSzkolny='" & RokSzkolny & "' AND o.DataDeaktywacji IS NULL;"
  End Function
  Public Function SelectRepeaters(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    Return "SELECT distinct p.IdUczen FROM przydzial p,szkola_klasa sk WHERE p.Klasa=sk.ID AND sk.IdSzkola='" & IdSzkola & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji='1' AND p.IdUczen IN (SELECT DISTINCT IdUczen FROM przydzial WHERE LEFT(RokSzkolny,4)='" & CType(Left(RokSzkolny, 4), Integer) - 1 & "' AND Promocja='0' AND StatusAktywacji='1');"
  End Function
  Public Function SelectStudent() As String
    Return "SELECT CONCAT_WS(' ',Nazwisko,Imie,Imie2) AS Student,DataUr FROM uczen;"
  End Function
  Public Function SelectKlasa(IdSzkola As String) As String
    Return "SELECT ID,Kod_Klasy FROM szkola_klasa WHERE IdSzkola='" & IdSzkola & "';"
  End Function
  Public Function SelectMaxNrDz(ByVal Klasa As String, ByVal RokSzkolny As String) As String
    Return "Select Max(p.NrwDzienniku) FROM przydzial p Where p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "';"
  End Function
  Public Function InsertKlasa() As String
    Return "INSERT INTO szkola_klasa (IdSzkola,Kod_Klasy,Nazwa_Klasy,Owner,User,ComputerIP) VALUES (?IdSzkola,?Kod_Klasy,?Nazwa_Klasy,?Owner,?User,?ComputerIP);"
    'Return "INSERT INTO szkola_klasa (IdSzkola,Kod_Klasy,Nazwa_Klasy,Owner,User,ComputerIP) VALUES (?IdSzkola,?Kod_Klasy,?Nazwa_Klasy,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function InsertStudent() As String
    Return "INSERT INTO uczen VALUES(null,?Nazwisko,?Imie,?Imie2,?NrArkusza,?ImieOjca,?NazwiskoOjca,?ImieMatki,?NazwiskoMatki,?DataUr,?Pesel,?IdMiejsceUr,?IdMiejsceZam,?Ulica,?NrDomu,?NrMieszkania,?Tel,?TelKom1,?TelKom2,?Man,'" + GlobalValues.AppUser.Login + "','" + GlobalValues.AppUser.Login + "','" + GlobalValues.gblIP + "',NULL);"
  End Function
  Public Function ImportStudent() As String
    Return "INSERT INTO uczen (Nazwisko,Imie,Imie2,NrArkusza,ImieOjca,ImieMatki,DataUr,Man,Pesel,Owner,User,ComputerIP) VALUES (?Nazwisko,?Imie,?Imie2,?NrArkusza,?ImieOjca,?ImieMatki,?DataUr,?Man,?Pesel,?Owner,?User,?ComputerIP);"
  End Function

  Public Function DeleteStudent(ByVal ID As String) As String
    Return "DELETE FROM uczen WHERE ID='" & ID & "';"
  End Function
  Public Function StrikeoutStudent(ByVal IdUczen As String, ByVal RokSzkolny As String, ByVal DataDeaktywacji As String) As String
    Return "UPDATE przydzial p SET p.StatusAktywacji=0,p.DataDeaktywacji='" & DataDeaktywacji & "',User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE p.IdUczen=" & IdUczen & " AND p.RokSzkolny='" & RokSzkolny & "' AND p.MasterRecord=1;"
  End Function
  Public Function UpdateNrDz(ByVal Nr As String, ByVal IdUczen As String, Klasa As String, ByVal RokSzkolny As String) As String
    Return "UPDATE przydzial SET NrwDzienniku='" & Nr & "' Where IdUczen='" & IdUczen & "' AND Klasa='" & Klasa & "' AND RokSzkolny='" & RokSzkolny & "' AND StatusAktywacji='1';"
  End Function
  Public Function UpdateStudent(ByVal IdUczen As String) As String
    Return "UPDATE uczen SET Nazwisko=?Nazwisko,Imie=?Imie,Imie2=?Imie2,NrArkusza=?NrArkusza,DataUr=?DataUr,IdMIejsceUr=?IdMiejsceUr,IdMIejsceZam=?IdMiejsceZam,Ulica=?Ulica,NrDomu=?NrDomu,NrMieszkania=?NrMieszkania,Tel=?Tel,TelKom1=?TelKom1,TelKom2=?TelKom2,ImieOjca=?ImieOjca,NazwiskoOjca=?NazwiskoOjca,ImieMatki=?ImieMatki,NazwiskoMatki=?NazwiskoMatki,Man=?Man,Pesel=?Pesel,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID='" & IdUczen & "';"
  End Function
End Class

Public Class GrupaSQL
  Public Function SelectGroupMember(Klasa As String, RokSzkolny As String) As String
    'CONCAT(g.User,' (','Wł: ',g.Owner,')') As
    Return "SELECT g.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2) AS Student,g.IdSzkolaPrzedmiot, g.User,g.ComputerIP,g.Version,g.Owner FROM uczen u INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN grupa g ON g.IdPrzydzial=p.ID WHERE p.Klasa=" & Klasa & " AND p.StatusAktywacji=1 AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectPrzedmiot(Klasa As String, RokSzkolny As String) As String
    Return "Select sp.ID,CONCAT(if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa)),' {',Concat_WS(' ',n.Nazwisko,n.Imie),'}') AS Przedmiot FROM szkola_przedmiot sp INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN obsada o ON sp.ID=o.Przedmiot INNER JOIN szkola_nauczyciel sn ON sn.ID=o.Nauczyciel INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa=" & Klasa & " AND o.RokSzkolny='" & RokSzkolny & "' AND sp.Grupa>0 Order BY sp.Priorytet,p.Alias;"
  End Function
  Public Function SelectStudent(Klasa As String, Przedmiot As String) As String
    Return "Select p.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID WHERE p.Klasa=" & Klasa & " AND p.StatusAktywacji=1 AND p.ID NOT IN (SELECT g.IdPrzydzial FROM grupa g INNER JOIN szkola_przedmiot sp ON sp.ID=g.IdSzkolaPrzedmiot INNER JOIN przydzial p ON g.IdPrzydzial=p.ID WHERE p.Klasa=" & Klasa & " AND sp.IdPrzedmiot IN (SELECT sp.IdPrzedmiot FROM szkola_przedmiot sp WHERE sp.ID=" & Przedmiot & ")) ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function DeleteGroupMember() As String
    Return "DELETE FROM grupa WHERE ID=?ID;"
  End Function
  Public Function InsertGroupMember() As String
    Return "INSERT INTO grupa VALUES(NULL,?IdSzkolaPrzedmiot,?IdPrzydzial,?User,?User,?ComputerIP,NULL);"
  End Function
End Class

Public Class ZagrozeniaSQL
  Public Overloads Function SelectStudent(Klasa As String, RokSzkolny As String) As String
    Return "Select p.ID,p.NrwDzienniku,CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID WHERE p.Klasa='" & Klasa & "' AND p.StatusAktywacji=1 AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY p.Nrwdzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Overloads Function SelectStudent(Klasa As String, RokSzkolny As String, Semestr As String) As String
    Return "Select DISTINCT p.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID INNER JOIN zagrozenia z ON p.ID=z.IdPrzydzial WHERE p.Klasa='" & Klasa & "' AND p.StatusAktywacji=1 AND p.RokSzkolny='" & RokSzkolny & "' AND z.Semestr='" & Semestr & "' ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Overloads Function SelectPrzedmiot(Klasa As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT p.ID,p.Alias FROM przedmiot p,obsada o,szkola_przedmiot sp WHERE sp.ID=o.Przedmiot AND sp.IdPrzedmiot=p.ID AND o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' AND p.Typ='p' Order By sp.Priorytet;"
  End Function
  Public Overloads Function SelectPrzedmiot(Klasa As String, RokSzkolny As String, Semestr As String) As String
    'CONCAT(z.User,' (','Wł: ',z.Owner,')') As 
    Return "SELECT DISTINCT z.ID,p.Alias,z.User, z.ComputerIP, z.Version, z.IdPrzedmiot, z.IdPrzydzial, z.Owner FROM przedmiot p,zagrozenia z,przydzial pdl,szkola_przedmiot sp WHERE p.ID=z.IdPrzedmiot AND pdl.ID=z.IdPrzydzial AND sp.IdPrzedmiot=p.ID AND pdl.Klasa='" & Klasa & "' AND pdl.RokSzkolny='" & RokSzkolny & "' AND z.Semestr='" & Semestr & "' ORDER BY sp.Priorytet;"
  End Function
  Public Function SelectZagrozenie(Klasa As String, RokSzkolny As String, Semestr As String) As String
    Return "Select Distinct Concat_WS(' ',u.Imie,u.Nazwisko) as Uczen,Concat_WS(' ',u.ImieMatki,u.NazwiskoMatki) as Matka, Concat_WS(' ',u.ImieOjca,u.NazwiskoOjca) as Ojciec,p.ID,u.Man FROM zagrozenia z,uczen u,przydzial p WHERE p.ID=z.IdPrzydzial AND u.ID=p.IdUczen AND p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND Semestr='" & Semestr & "' ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;SELECT DISTINCT p.Alias,z.IdPrzydzial FROM przedmiot p,zagrozenia z,przydzial pdl,szkola_przedmiot sp WHERE p.ID=z.IdPrzedmiot AND pdl.ID=z.IdPrzydzial AND pdl.Klasa='" & Klasa & "' AND pdl.RokSzkolny='" & RokSzkolny & "' AND z.Semestr='" & Semestr & "' ORDER BY sp.Priorytet;"
  End Function
  Public Function InsertPrzedmiot() As String
    Return "INSERT INTO zagrozenia VALUES(NULL,?IdPrzydzial,?IdPrzedmiot,?Semestr,?User,?User,?ComputerIP,NULL);"
  End Function
  Public Function DeletePrzedmiot() As String
    Return "DELETE FROM zagrozenia WHERE ID=?ID;"
  End Function
End Class

Public Class StatystykaSQL
  Public Function CountLekcja(Klasa As String, RokSzkolny As String) As String
    Return "SELECT MONTH(t.Data) AS Miesiac,Count(t.ID) AS LiczbaGodzin,l.IdObsada,o.Przedmiot FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON l.IdObsada=o.ID WHERE t.Status=1 AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND t.ID NOT IN (SELECT t.ID FROM temat t, zastepstwo z WHERE t.IdLekcja=z.IdLekcja AND t.Data=z.Data AND z.Status=1) Group By YEAR(t.Data),MONTH(t.Data),l.IdObsada;"
  End Function
  Public Function CountLekcjaByBelfer(Nauczyciel As String, RokSzkolny As String) As String
    Return "SELECT sk.Nazwa_Klasy,Count(t.ID) AS LiczbaGodzin,v1.Alias,o.Przedmiot,o.Klasa,v1.IdPrzedmiot FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN (SELECT DISTINCT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.ID,p.ID AS IdPrzedmiot FROM przedmiot p INNER JOIN szkola_przedmiot sp ON sp.IdPrzedmiot=p.ID INNER JOIN obsada o ON o.Przedmiot=sp.ID) as v1 ON o.Przedmiot=v1.ID WHERE t.Status=1 AND o.Nauczyciel='" & Nauczyciel & "' AND t.ID NOT IN (SELECT t.ID FROM temat t, zastepstwo z WHERE t.IdLekcja=z.IdLekcja AND t.Data=z.Data AND z.Status=1)  AND o.Klasa IN (SELECT DISTINCT p.Klasa FROM przydzial p WHERE p.RokSzkolny='" & RokSzkolny & "') AND o.RokSzkolny='" & RokSzkolny & "' Group By sk.Kod_Klasy,l.IdObsada;"
  End Function
  Public Overloads Function CountZastepstwo(Klasa As String, RokSzkolny As String) As String
    Return "SELECT MONTH(z.Data) AS Miesiac,COUNT(z.ID) AS LiczbaZastepstw,z.IdPrzedmiotSzkola AS Przedmiot FROM zastepstwo z INNER JOIN lekcja l ON z.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND z.Status=1 AND z.IdLekcja IN (SELECT t.IdLekcja FROM temat t,zastepstwo z WHERE t.IDLekcja=z.IdLekcja AND t.Data=z.Data) GROUP BY YEAR(z.Data),MONTH(z.Data),z.IdPrzedmiotSzkola;"
  End Function
  Public Function CountZastepstwoByBelfer(Nauczyciel As String, RokSzkolny As String) As String
    Return "SELECT sk.Nazwa_Klasy,COUNT(z.ID) AS LiczbaZastepstw,z.IdPrzedmiotSzkola AS Przedmiot,o.Klasa FROM zastepstwo z INNER JOIN lekcja l ON z.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE z.Status=1 AND o.RokSzkolny='" & RokSzkolny & "' AND z.IdLekcja IN (SELECT t.IdLekcja FROM temat t,zastepstwo z WHERE t.IDLekcja=z.IdLekcja AND t.Data=z.Data) GROUP BY sk.Nazwa_Klasy,z.IdPrzedmiotSzkola;"
    'Return "SELECT sk.Nazwa_Klasy,COUNT(z.ID) AS LiczbaZastepstw,z.IdPrzedmiotSzkola AS Przedmiot,o.Klasa FROM zastepstwo z INNER JOIN lekcja l ON z.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE o.Nauczyciel='" & Nauczyciel & "' AND z.Status=1 AND z.IdLekcja IN (SELECT t.IdLekcja FROM temat t,zastepstwo z WHERE t.IDLekcja=z.IdLekcja AND t.Data=z.Data) GROUP BY sk.Nazwa_Klasy,z.IdPrzedmiotSzkola;"
  End Function
  Public Overloads Function SelectRequiredNumberOfActivities(RokSzkolny As String) As String
    Return "SELECT m.Wartosc,m.Klasa,m.Przedmiot FROM min_lekcja m WHERE m.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Overloads Function SelectRequiredNumberOfActivities(RokSzkolny As String, Klasa As String) As String
    Return "SELECT m.Wartosc,m.Klasa,m.Przedmiot FROM min_lekcja m WHERE m.RokSzkolny='" & RokSzkolny & "' AND m.Klasa='" & Klasa & "';"
  End Function
  Public Function SelectMonth(Klasa As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT MONTH(t.Data) AS Miesiac FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON l.IdObsada=o.ID WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER By YEAR(t.Data),MONTH(t.Data);"
  End Function

  Public Function SelectLiczbaOcen(Szkola As String, RokSzkolny As String, Semestr As String) As String
    Return "SELECT COUNT(w.ID) AS LiczbaOcen,o.Klasa,p.ID AS IdPrzedmiot,o.Nauczyciel,so.Waga FROM wyniki w INNER JOIN kolumna k ON w.IdKolumna=k.ID INNER JOIN obsada o ON k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot INNER JOIN skala_ocen so ON so.ID=w.IdOcena WHERE sp.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND so.Typ!='Z' AND so.Waga>=0 GROUP BY o.Klasa, w.IdOcena, p.ID, o.Nauczyciel;"
  End Function
  Public Function SelectPrzedmiotByKlasa(Klasa As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT v1.Przedmiot,v1.IdPrzedmiot,CONCAT_WS('',v1.Alias,' {',v2.Belfer,'}') as Nazwa FROM (SELECT o.Przedmiot,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,p.ID AS IdPrzedmiot,o.ID,sp.Priorytet FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND p.Typ<>'Z') AS v1, (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v1.Priorytet;"
  End Function
  Public Overloads Function SelectPrzedmiot(Szkola As String, RokSzkolny As String, Nauczyciel As String) As String
    Return "SELECT DISTINCT p.ID,p.Nazwa FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sp.IdSzkola='" & Szkola & "' AND  o.RokSzkolny='" & RokSzkolny & "' AND p.Typ!='Z' AND o.Klasa IN (SELECT Klasa FROM przydzial p WHERE p.RokSzkolny='" & RokSzkolny & "') AND o.Nauczyciel='" & Nauczyciel & "' ORDER BY sp.Priorytet;"
  End Function
  Public Overloads Function SelectPrzedmiot(Szkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT p.ID,p.Nazwa As Alias,o.Nauczyciel FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sp.IdSzkola='" & Szkola & "' AND  o.RokSzkolny='" & RokSzkolny & "' AND p.Typ NOT IN ('Z','F') ORDER BY sp.Priorytet;"
    'Return "SELECT DISTINCT p.ID,p.Nazwa As Alias,o.Nauczyciel FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sp.IdSzkola='" & Szkola & "' AND  o.RokSzkolny='" & RokSzkolny & "' AND p.Typ!='Z' AND o.Klasa IN (SELECT DISTINCT Klasa FROM obsada o WHERE o.RokSzkolny='" & RokSzkolny & "') ORDER BY sp.Priorytet;"
  End Function
  Public Function SelectKlasa(Szkola As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT DISTINCT sk.Nazwa_Klasy,p.ID AS IdPrzedmiot,o.Nauczyciel,o.Klasa,sk.Virtual FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND p.Typ  NOT IN ('Z','F') AND o.ID NOT IN (SELECT o.ID FROM obsada o INNER JOIN nauczanie_indywidualne ni ON o.ID=ni.IdObsada WHERE o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) > '" & Data.ToShortDateString & "' AND (DATE(o.DataDeaktywacji) <= '" & Data.ToShortDateString & "' OR o.DataDeaktywacji is null)) ORDER BY sk.Kod_Klasy;"
    'Return "SELECT DISTINCT sk.Nazwa_Klasy,p.ID AS IdPrzedmiot,o.Nauczyciel,o.Klasa FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN przydzial pdl ON sk.ID=pdl.Klasa WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "'  AND p.Typ!='Z' ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectStanKlasy(Szkola As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT p.Klasa,COUNT(p.ID) As StanKlasy FROM przydzial p INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa WHERE sk.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' AND DATE(p.DataAktywacji) <= '" & Data.ToShortDateString & "' AND (DATE(p.DataDeaktywacji) >= '" & Data.ToShortDateString & "' OR p.DataDeaktywacji is null) Group BY p.Klasa;"
  End Function
  Public Function SelectStanKlasyWirtualnej(Szkola As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT o.Klasa AS KlasaWirtualna,p.Klasa,sp.IdPrzedmiot,COUNT(ni.IdPrzydzial) AS StanKlasy FROM obsada o INNER JOIN nauczanie_indywidualne ni ON o.ID=ni.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przydzial p ON p.ID = ni.IdPrzydzial WHERE sp.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data.ToShortDateString & "' AND (DATE(o.DataDeaktywacji) > '" & Data.ToShortDateString & "' OR o.DataDeaktywacji is null) GROUP BY o.Klasa,sp.IdPrzedmiot;"
  End Function
  Public Function SelectStudentNdst(Szkola As String, RokSzkolny As String, Semestr As String, Nauczyciel As String) As String
    Return "SELECT u.ID as IdStudent,Concat_ws(' ',u.Nazwisko,u.Imie) AS Student,sk.Nazwa_Klasy,so.Nazwa AS Ocena,pt.ID as IdPrzedmiot,o.Nauczyciel,o.Id as IdObsada,o.Klasa,o.Przedmiot,w.ID AS IdWynik,w.Owner,k.Lock,pt.Nazwa,p.DataAktywacji FROM wyniki w INNER JOIN uczen u On u.ID=w.IdUczen INNER JOIN skala_ocen so ON w.IdOcena=so.ID INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot WHERE so.Waga IN (0,1) AND k.Typ='" & Semestr & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sk.Kod_Klasy,u.Nazwisko,u.Imie;"
  End Function
  Public Function SelectStudentZ(Szkola As String, RokSzkolny As String, Semestr As String, Nauczyciel As String) As String
    Return "SELECT u.ID as IdStudent,Concat_ws(' ',u.Nazwisko,u.Imie) AS Student,sk.Nazwa_Klasy,so.Nazwa AS Ocena,o.Nauczyciel,o.Przedmiot,o.Klasa,w.ID AS IdWynik,w.Owner,pt.ID as IdPrzedmiot,p.DataAktywacji FROM wyniki w INNER JOIN uczen u On u.ID=w.IdUczen INNER JOIN skala_ocen so ON w.IdOcena=so.ID INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE pt.Typ='Z' AND so.Waga IN (0,1,2) AND k.Typ='" & Semestr & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Nauczyciel='" & Nauczyciel & "' ORDER BY u.Nazwisko,u.Imie,sk.Kod_Klasy;"
  End Function
  Public Function SelectAbsenceZ(Szkola As String, RokSzkolny As String, Semestr As String, Nauczyciel As String, StartDate As Date, EndDate As Date) As String
    'Return "SELECT f.IdUczen,f.ID,f.Data FROM frekwencja f WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND f.IdUczen IN (Select DISTINCT w.IdUczen FROM wyniki w INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot WHERE so.Waga IN (0,1,2) AND pt.Typ='Z' AND k.Typ='" & Semestr & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.Nauczyciel='" & Nauczyciel & "') Order BY f.IdUczen;"

    Return "SELECT f.IdUczen,f.ID,f.Data FROM frekwencja f INNER JOIN wyniki w ON f.IdUczen=w.IdUczen INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND so.Waga IN (0,1,2) AND pt.Typ='Z' AND k.Typ='" & Semestr & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.Nauczyciel='" & Nauczyciel & "' Order BY f.IdUczen;"
  End Function
  Public Function SelectLekcjaZ(Szkola As String, RokSzkolny As String, Semestr As String, Nauczyciel As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT t.ID,o.Klasa,t.Data FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot WHERE t.Status=1 AND sp.Grupa=0 AND t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa IN (SELECT DISTINCT o.Klasa FROM obsada o INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.Id=sp.IdPrzedmiot WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND so.Waga IN (0,1,2) AND p.Typ='Z' AND o.Nauczyciel='" & Nauczyciel & "') Order BY o.Klasa;"
  End Function
  Public Function SelectLekcjaZG(Szkola As String, RokSzkolny As String, Semestr As String, Nauczyciel As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT t.ID,o.Klasa,o.Przedmiot,t.Data FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot WHERE t.Status=1 AND sp.Grupa>0 AND t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa IN (SELECT DISTINCT o.Klasa FROM obsada o INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.Id=sp.IdPrzedmiot WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND so.Waga IN (0,1,2) AND p.Typ='Z' AND o.Nauczyciel='" & Nauczyciel & "') Order BY o.Klasa,o.Przedmiot;"
  End Function
  Public Function SelectGrupaPrzedmiot(Szkola As String, RokSzkolny As String, Semestr As String, Nauczyciel As String) As String
    Return "SELECT g.IdSzkolaPrzedmiot as Przedmiot,p.IdUczen FROM grupa g INNER JOIN przydzial p ON p.ID=g.IdPrzydzial WHERE p.RokSzkolny='" & RokSzkolny & "' AND p.IdUczen IN (SELECT DISTINCT w.IdUczen FROM obsada o INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.Id=sp.IdPrzedmiot WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND so.Waga IN (0,1,2) AND p.Typ='Z' AND o.Nauczyciel='" & Nauczyciel & "');"
  End Function

  Public Function SelectResult(Szkola As String, RokSzkolny As String, Semestr As String, TypKolumny As String, Nauczyciel As String) As String
    Return "Select w.ID as IdWynik,so.Ocena,ow.Nazwa AS OpisOceny,so.Waga AS WagaOceny,k.Waga AS WagaKolumny,k.Typ,sp.IdPrzedmiot,w.IdUczen From wyniki w Inner Join skala_ocen so on w.IdOcena=so.ID INNER Join kolumna k on w.IdKolumna=k.ID INNER JOIN obsada o ON k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID LEFT JOIN opis_wyniku ow ON ow.ID=k.IdOpis Where w.IdUczen IN (Select u.ID FROM uczen u INNER JOIN wyniki w On u.ID=w.IdUczen INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa WHERE so.Waga IN (0,1) AND k.Typ='" & Semestr & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.Nauczyciel='" & Nauczyciel & "') AND k.Typ='" & TypKolumny & "' AND o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "' Order by k.NrKolumny,w.Data,w.Version;"
  End Function

  Public Overloads Function SelectLekcjaBezZastepstw(Szkola As String, RokSzkolny As String, Semestr As String, StartDate As Date, EndDate As Date, Nauczyciel As String) As String
    Return "SELECT t.ID AS IdLekcja,o.ID AS IdObsada,t.Data FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON o.ID=l.IdObsada WHERE t.Status=1 AND t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.ID IN (SELECT DISTINCT o.ID FROM obsada o INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND so.Waga IN (0,1) AND o.Nauczyciel='" & Nauczyciel & "') AND t.ID NOT IN (SELECT t.ID FROM temat t, zastepstwo z WHERE t.IdLekcja=z.IdLekcja AND t.Data=z.Data) AND o.Nauczyciel='" & Nauczyciel & "' ORDER BY o.ID;"
  End Function
  Public Overloads Function SelectZastepstwo(Szkola As String, RokSzkolny As String, Semestr As String, StartDate As Date, EndDate As Date, Nauczyciel As String) As String
    Return "SELECT o.Klasa,z.IdPrzedmiotSzkola AS Przedmiot,z.IdNauczycielSzkola AS Nauczyciel,l.IdObsada,z.Data FROM zastepstwo z INNER JOIN lekcja l ON z.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE z.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa IN (SELECT DISTINCT o.Klasa FROM obsada o INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND so.Waga IN (0,1) AND o.Nauczyciel='" & Nauczyciel & "') AND z.Status=1;"
  End Function
  Public Overloads Function SelectAbsence(Szkola As String, RokSzkolny As String, Semestr As String, StartDate As Date, EndDate As Date, Nauczyciel As String) As String
    Return "SELECT f.IdUczen,o.ID AS IdObsada,f.Data FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND f.IdUczen IN (Select u.ID FROM uczen u INNER JOIN wyniki w On u.ID=w.IdUczen INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa WHERE so.Waga IN (0,1) AND k.Typ='" & Semestr & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.Nauczyciel='" & Nauczyciel & "') AND f.ID NOT IN (SELECT f.ID FROM temat t, zastepstwo z WHERE f.IdLekcja=z.IdLekcja AND f.Data=z.Data) AND o.Nauczyciel='" & Nauczyciel & "' ORDER BY f.IdUczen;"
  End Function
  Public Overloads Function SelectAbsenceZastepstwo(Szkola As String, RokSzkolny As String, Semestr As String, StartDate As Date, EndDate As Date, Nauczyciel As String) As String
    Return "SELECT f.IdUczen,o.ID AS IdObsada,f.Data FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN zastepstwo z ON l.ID=z.IdLekcja WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND f.IdUczen IN (Select u.ID FROM uczen u INNER JOIN wyniki w On u.ID=w.IdUczen INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa WHERE so.Waga IN (0,1) AND k.Typ='" & Semestr & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.Nauczyciel='" & Nauczyciel & "') AND z.Status=1 AND f.Data=z.Data ORDER BY f.IdUczen;"
  End Function

  Public Overloads Function SelectLekcjaBezZastepstw(Klasa As String, Przedmiot As String, RokSzkolny As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT t.ID,t.Data FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON l.IdObsada=o.ID WHERE t.Status=1 AND t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' AND o.Przedmiot='" & Przedmiot & "' AND o.RokSzkolny='" & RokSzkolny & "' AND t.ID NOT IN (SELECT t.ID FROM temat t, zastepstwo z WHERE t.IdLekcja=z.IdLekcja AND t.Data=z.Data) order BY t.ID;"
  End Function
  Public Overloads Function SelectAbsence(Klasa As String, Przedmiot As String, RokSzkolny As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT f.IdUczen,f.Data FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN obsada o On l.IdObsada=o.ID WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' AND o.Przedmiot='" & Przedmiot & "' AND o.RokSzkolny='" & RokSzkolny & "' AND f.ID NOT IN (SELECT f.ID FROM temat t, zastepstwo z WHERE f.IdLekcja=z.IdLekcja AND f.Data=z.Data) ORDER BY f.IdUczen;"
  End Function
  Public Overloads Function SelectZastepstwo(Klasa As String, Przedmiot As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT z.ID,z.Data FROM zastepstwo z INNER JOIN lekcja l ON z.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE z.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' AND z.IdPrzedmiotSzkola='" & Przedmiot & "' AND z.Status=1;"
  End Function
  'Public Overloads Function SelectZastepstwo(Klasa As String, Przedmiot As String, StartDate As Date, EndDate As Date) As String
  '  Return "SELECT DISTINCT l.IdObsada FROM zastepstwo z INNER JOIN lekcja l ON z.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE z.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' AND z.IdPrzedmiotSzkola='" & Przedmiot & "' AND z.Status=1;"
  'End Function
  Public Overloads Function SelectAbsenceZastepstwo(Klasa As String, Przedmiot As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT f.IdUczen,f.Data FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN zastepstwo z ON l.ID=z.IdLekcja WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' AND z.IdPrzedmiotSzkola='" & Przedmiot & "' AND z.Status=1 AND f.Data=z.Data ORDER BY f.IdUczen;"
  End Function
  Public Function SelectLekcja(Klasa As String, RokSzkolny As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT t.ID,t.Data,o.Przedmiot,sp.Grupa FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID WHERE t.Status=1 AND t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "';"
  End Function
  'Public Function SelectLekcjaGrupa(Klasa As String, RokSzkolny As String, StartDate As Date, EndDate As Date) As String
  '  Return "SELECT t.ID,o.Przedmiot,t.Data FROM temat t INNER JOIN lekcja l ON l.ID=t.IdLekcja INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID WHERE t.Status=1 AND t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sp.Grupa>0 ORDER BY t.ID;"
  'End Function
  Public Function CountStudentBySex(Szkola As String, RokSzkolny As String, Klasa As String) As String
    Return "SELECT Man,COUNT(u.ID) AS LiczbaUczniow FROM uczen u,przydzial p WHERE u.ID=p.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "' AND p.StatusAktywacji='1' GROUP BY Man ASC;"
  End Function
  Public Function CountStudentByTown(Szkola As String, RokSzkolny As String, Klasa As String) As String
    Return "SELECT Miasto,COUNT(u.ID) AS LiczbaUczniow FROM uczen u INNER JOIN przydzial p ON u.ID=p.IdUczen INNER JOIN miejscowosc m ON u.IdMiejsceZam=m.ID WHERE p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "' AND p.StatusAktywacji='1' GROUP BY Miasto ASC;"
  End Function
  Public Function CountNotes(Klasa As String, RokSzkolny As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT u.IdUczen,u.TypUwagi, COUNT(u.ID) AS LiczbaUwag FROM uwagi u WHERE  u.IdUczen IN (SELECT p.IdUczen FROM przydzial p WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND MasterRecord=1) AND u.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' GROUP BY u.IdUczen,u.TypUwagi;"
  End Function
  Public Overloads Function CountAbsence(Klasa As String, RokSzkolny As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT f.IdUczen,f.Typ, COUNT(f.ID) AS Absencja,f.Data FROM frekwencja f WHERE f.IdUczen IN (SELECT p.IdUczen FROM przydzial p WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND MasterRecord=1) AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' GROUP BY f.IdUczen,f.Data,f.Typ;"
  End Function
  Public Overloads Function CountAbsence(Klasa As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT f.IdUczen,Month(f.Data) AS MonthNumber,f.Typ,Count(f.ID) AS AbsenceCount FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' Group BY IdUczen,Month(f.Data),f.Typ;"
  End Function
  Public Overloads Function CountAbsence(Klasa As String, RokSzkolny As String) As String
    Return "SELECT Month(f.Data) AS MonthNumber,Count(f.ID) AS AbsenceCount FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE f.Typ<>'S' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' Group BY Month(f.Data);"
  End Function
  Public Function SelectStudent(ByVal RokSzkolny As String, ByVal IdKlasa As String) As String
    Return "SELECT DISTINCT u.ID AS IdUczen,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student FROM uczen u,przydzial p,uprawnienie up WHERE u.ID=p.IdUczen AND p.IdUczen=up.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1 AND p.Klasa='" & IdKlasa & "' ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectBelfer(IdSzkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT DISTINCT o.Nauczyciel AS ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nazwa FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID INNER JOIN obsada o ON o.Nauczyciel=sn.ID INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sn.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND so.Waga IN (0,1) AND p.Typ<>'Z' ORDER BY n.Nazwisko,n.Imie;"
  End Function
  Public Function SelectBelferZ(IdSzkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT DISTINCT o.Nauczyciel AS ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nazwa FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID INNER JOIN obsada o ON o.Nauczyciel=sn.ID INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sn.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND so.Waga IN (0,1,2) AND p.Typ='Z' ORDER BY n.Nazwisko,n.Imie;"
  End Function
End Class

Public Class ZastepstwoSQL
  Public Overloads Function SelectZastepstwo(Szkola As String, RokSzkolny As String, Data As Date) As String
    'Return "SELECT z.ID,CONCAT(v1.Lekcja,' --> ',v2.Klasa,' {',v5.Alias,'}') AS Lekcja,CONCAT(v3.Zastepca,' {',v4.Alias,'}') AS Zastepstwo, z.Status, z.Sala,z.User, z.ComputerIP, z.Version, v2.ZaNauczyciel,z.IdNauczycielSzkola AS InNauczyciel,z.IdPrzedmiotSzkola AS InPrzedmiot,z.IdLekcja,z.Data,CONCAT(v1.NrLekcji,') ',v2.Klasa,' {',v5.Alias,'}') AS LekcjaNoHour,z.Owner FROM zastepstwo z INNER JOIN (SELECT CONCAT_WS(') ',g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i'))) AS Lekcja,l.ID,g.NrLekcji FROM lekcja l INNER JOIN godzina_lekcyjna g ON g.ID=l.IdGodzina INNER JOIN zastepstwo z ON l.ID=z.IdLekcja WHERE z.Data='" & Data.ToShortDateString & "' AND g.IdSzkola='" & Szkola & "') AS v1 ON z.IdLekcja=v1.ID INNER JOIN (SELECT sk.Nazwa_klasy AS Klasa,o.Nauczyciel AS ZaNauczyciel,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa) AS v2 ON z.IDLekcja=v2.ID INNER JOIN (SELECT CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Zastepca,sn.ID FROM szkola_nauczyciel sn  INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel) AS v3 ON z.IdNauczycielSzkola=v3.ID INNER JOIN (SELECT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v4 ON z.IdPrzedmiotSzkola=v4.ID INNER JOIN (SELECT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v5 ON z.IdLekcja=v5.ID WHERE z.Data='" & Data.ToShortDateString & "' AND z.IdLekcja IN (SELECT l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON g.ID=l.IdGodzina WHERE o.RokSzkolny='" & RokSzkolny & "' AND g.IdSzkola='" & Szkola & "') ORDER BY z.Data,v1.NrLekcji;"
    Return "SELECT z.ID,CONCAT(v1.Lekcja,' --> ',v2.Klasa,' {',v5.Alias,'}') AS Lekcja,CONCAT(v3.Zastepca,' {',v4.Alias,'}') AS Zastepstwo, z.Status, z.Sala,z.User, z.ComputerIP, z.Version, v2.ZaNauczyciel,z.IdNauczycielSzkola AS InNauczyciel,z.IdPrzedmiotSzkola AS InPrzedmiot,z.IdLekcja,z.Data,CONCAT(v1.NrLekcji,') ',v2.Klasa,' {',v5.Alias,'}') AS LekcjaNoHour,z.Owner FROM zastepstwo z INNER JOIN (SELECT CONCAT_WS(') ',g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i'))) AS Lekcja,l.ID,g.NrLekcji FROM lekcja l INNER JOIN godzina_lekcyjna g ON g.ID=l.IdGodzina INNER JOIN zastepstwo z ON l.ID=z.IdLekcja WHERE z.Data='" & Data.ToShortDateString & "' AND g.IdSzkola='" & Szkola & "') AS v1 ON z.IdLekcja=v1.ID INNER JOIN (SELECT sk.Nazwa_klasy AS Klasa,o.Nauczyciel AS ZaNauczyciel,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa INNER JOIN zastepstwo z ON l.ID=z.IdLekcja WHERE o.RokSzkolny='" & RokSzkolny & "' AND z.Data='" & Data.ToShortDateString & "') AS v2 ON z.IDLekcja=v2.ID LEFT JOIN (SELECT CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Zastepca,sn.ID FROM szkola_nauczyciel sn INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE sn.IdSzkola='" & Szkola & "') AS v3 ON z.IdNauczycielSzkola=v3.ID LEFT JOIN (SELECT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE sp.IdSzkola='" & Szkola & "') AS v4 ON z.IdPrzedmiotSzkola=v4.ID INNER JOIN (SELECT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot INNER JOIN zastepstwo z ON l.ID=z.IdLekcja WHERE sp.IdSzkola='" & Szkola & "' AND z.Data='" & Data.ToShortDateString & "') AS v5 ON z.IdLekcja=v5.ID WHERE z.Data='" & Data.ToShortDateString & "' ORDER BY z.Data,v1.NrLekcji;"
  End Function
  Public Overloads Function SelectZastepstwo(Szkola As String, RokSzkolny As String, Nauczyciel As String) As String
    'CONCAT(z.User,' (','Wł: ',z.Owner,')') As 
    'Return "SELECT DISTINCT z.ID,CONCAT(v1.Lekcja,' --> ',v2.Klasa,' {',v5.Alias,'}') AS Lekcja,CONCAT(v3.Zastepca,' {',v4.Alias,'}') AS Zastepstwo, z.Status, z.Sala,z.User, z.ComputerIP, z.Version, v2.ZaNauczyciel,z.IdNauczycielSzkola AS InNauczyciel,z.IdPrzedmiotSzkola AS InPrzedmiot,z.IdLekcja,z.Data,CONCAT(v1.NrLekcji,') ',v2.Klasa,' {',v5.Alias,'}') AS LekcjaNoHour,z.Owner FROM zastepstwo z  INNER JOIN (SELECT CONCAT_WS(') ',g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i'))) AS Lekcja,l.ID,g.NrLekcji FROM lekcja l INNER JOIN godzina_lekcyjna g ON g.ID=l.IdGodzina) AS v1 ON z.IdLekcja=v1.ID INNER JOIN (SELECT sk.Nazwa_klasy AS Klasa,o.Nauczyciel AS ZaNauczyciel,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa WHERE o.Nauczyciel='" & Nauczyciel & "') AS v2 ON z.IDLekcja=v2.ID LEFT JOIN (SELECT CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Zastepca,sn.ID FROM szkola_nauczyciel sn  INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel) AS v3 ON z.IdNauczycielSzkola=v3.ID LEFT JOIN (SELECT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v4 ON z.IdPrzedmiotSzkola=v4.ID INNER JOIN (SELECT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) AS v5 ON z.IdLekcja=v5.ID WHERE z.IdLekcja IN (SELECT l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN godzina_lekcyjna g ON g.ID=l.IdGodzina WHERE o.RokSzkolny='" & RokSzkolny & "' AND g.IdSzkola='" & Szkola & "') ORDER BY z.Data,v1.NrLekcji;"
    Return "SELECT DISTINCT z.ID,CONCAT(v1.Lekcja,' --> ',v2.Klasa,' {',v5.Alias,'}') AS Lekcja,CONCAT(v3.Zastepca,' {',v4.Alias,'}') AS Zastepstwo, z.Status, z.Sala,z.User, z.ComputerIP, z.Version, v2.ZaNauczyciel,z.IdNauczycielSzkola AS InNauczyciel,z.IdPrzedmiotSzkola AS InPrzedmiot,z.IdLekcja,z.Data,CONCAT(v1.NrLekcji,') ',v2.Klasa,' {',v5.Alias,'}') AS LekcjaNoHour,z.Owner FROM zastepstwo z INNER JOIN (SELECT DISTINCT CONCAT_WS(') ',g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i'))) AS Lekcja,l.ID,g.NrLekcji FROM lekcja l INNER JOIN godzina_lekcyjna g ON g.ID=l.IdGodzina INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN zastepstwo z ON z.IdLekcja=l.ID WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "' AND g.IdSzkola='" & Szkola & "') AS v1 ON z.IdLekcja=v1.ID INNER JOIN (SELECT DISTINCT sk.Nazwa_klasy AS Klasa,o.Nauczyciel AS ZaNauczyciel,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "') AS v2 ON z.IDLekcja=v2.ID LEFT JOIN (SELECT DISTINCT CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Zastepca,sn.ID FROM szkola_nauczyciel sn INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE sn.IdSzkola='" & Szkola & "') AS v3 ON z.IdNauczycielSzkola=v3.ID LEFT JOIN (SELECT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE sp.IdSzkola='" & Szkola & "') AS v4 ON z.IdPrzedmiotSzkola=v4.ID INNER JOIN (SELECT DISTINCT if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,l.ID FROM lekcja l INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot INNER JOIN zastepstwo z ON z.IdLekcja=l.ID WHERE sp.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Nauczyciel='" & Nauczyciel & "') AS v5 ON z.IdLekcja=v5.ID ORDER BY z.Data,v1.NrLekcji;"
  End Function
  Public Function SelectLekcja(Szkola As String, Nauczyciel As String, Data As String, DzienTygodnia As String) As String
    Return "SELECT l.ID,CONCAT(v1.Godzina,' --> ',v2.Obsada) AS Lekcja FROM lekcja l INNER JOIN (SELECT CONCAT_WS(') ',g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i'))) AS Godzina,g.ID,g.NrLekcji FROM godzina_lekcyjna g) AS v1 ON v1.ID=l.IdGodzina INNER JOIN (SELECT CONCAT(sk.Nazwa_Klasy,' {',if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))),'}') AS Obsada,o.ID,o.Nauczyciel,o.RokSzkolny FROM obsada o INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN  przedmiot p ON p.ID=sp.IdPrzedmiot) AS v2 ON l.IdObsada=v2.ID INNER JOIN plan_lekcji pl ON l.IdPlan=pl.ID WHERE pl.IdSzkola='" & Szkola & "' AND v2.Nauczyciel='" & Nauczyciel & "' AND '" & Data & "' BETWEEN pl.StartDate AND pl.EndDate AND l.DzienTygodnia='" & DzienTygodnia & "' AND l.ID NOT IN (SELECT IdLekcja FROM zastepstwo WHERE Data='" & Data & "') ORDER BY v1.NrLekcji;"
  End Function
  Public Overloads Function SelectPrzedmiot(Szkola As String) As String
    Return "SELECT sp.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) as Przedmiot FROM przedmiot p,szkola_przedmiot sp WHERE p.ID=sp.IdPrzedmiot AND sp.IdSzkola='" & Szkola & "' Order by sp.Priorytet,p.Alias;"
  End Function
  Public Overloads Function SelectPrzedmiot(Szkola As String, IdLekcja As String) As String
    Return "SELECT sp.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) as Przedmiot FROM przedmiot p,szkola_przedmiot sp WHERE p.ID=sp.IdPrzedmiot AND sp.IdSzkola='" & Szkola & "' AND sp.ID NOT IN (SELECT o.Przedmiot FROM obsada o INNER JOIN lekcja l ON l.IdObsada=o.ID WHERE l.ID='" & IdLekcja & "') Order by sp.Priorytet,p.Alias;"
  End Function
  Public Function InsertZastepstwo() As String
    Return "INSERT INTO zastepstwo VALUES(NULL,?IdNauczyciel,?IdPrzedmiot,?IdLekcja,?Data,?Status,?Sala,?User,?User,?ComputerIP,NULL);"
  End Function
  Public Function UpdateZastepstwo() As String
    Return "UPDATE zastepstwo SET IdNauczycielSzkola=?IdNauczyciel,IdPrzedmiotSzkola=?IdPrzedmiot,Status=?Status,Sala=?Sala,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?IdZastepstwo;"
  End Function
  Public Function DeleteZastepstwo() As String
    Return "DELETE FROM zastepstwo WHERE ID=?ID;"
  End Function
End Class

Public Class AbsencjaSQL
  Public Function SelectNrLekcji(Data As Date, Klasa As String, RokSzkolny As String) As String
    Return "SELECT gl.NrLekcji,v1.Alias,l.ID,v1.Przedmiot,v1.Grupa FROM lekcja l INNER JOIN plan_lekcji pl ON pl.ID=l.IdPlan INNER JOIN godzina_lekcyjna gl ON l.IdGodzina=gl.ID INNER JOIN (SELECT p.Alias,o.ID,o.Przedmiot,sp.Grupa FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID) AS v1 ON l.IdObsada=v1.ID WHERE l.DzienTygodnia='" & Data.DayOfWeek & "' AND '" & Data.ToShortDateString & "' BETWEEN pl.StartDate AND pl.EndDate AND l.IdObsada IN (SELECT o.ID FROM obsada o WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') ORDER BY l.DzienTygodnia,gl.NrLekcji;"
  End Function
  Public Function SelectAbsence(Klasa As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT f.ID,f.IdUczen,l.DzienTygodnia,gl.NrLekcji,f.Typ, f.User,f.ComputerIP,f.Version, f.Owner FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN godzina_lekcyjna gl ON l.IdGodzina=gl.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' ORDER BY IdUczen,DzienTygodnia,NrLekcji;"
  End Function
  Public Function SelectObjectGroup(Klasa As String, RokSzkolny As String) As String
    Return "SELECT g.IdSzkolaPrzedmiot AS IdPrzedmiot,g.IdPrzydzial FROM grupa g INNER JOIN przydzial p ON p.ID=g.IdPrzydzial WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1;"
  End Function
  Public Function SelectWychowawca(Klasa As String) As String
    Return "SELECT u.Login FROM user u RIGHT JOIN nauczyciel n ON n.ID=u.IdNauczyciel INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID INNER JOIN obsada o ON o.Nauczyciel=sn.ID WHERE o.Klasa='" & Klasa & "' AND o.DataDeaktywacji IS NULL AND o.Przedmiot IN (SELECT sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE p.Typ='Z');"
  End Function
  Public Function SelectComplain(RokSzkolny As String, Szkola As String) As String
    Return "SELECT r.ID,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,p.Nazwa AS Przedmiot,r.Typ,r.DataLekcji,r.AbsenceOwner,r.Status,r.Komentarz,r.User,r.ComputerIP,r.Version,Concat_WS(' ',ur.Nazwisko,ur.Imie) AS OwnerName,Concat_WS(' ',usr.Nazwisko,usr.Imie) AS NotifierName,r.IdUczen,r.IdLekcja,r.AbsenceNotifier,r.NotifyDate FROM reklamacja r INNER JOIN uczen u ON u.ID=r.IdUczen INNER JOIN lekcja l ON l.ID=r.IdLekcja INNER JOIN obsada o ON l.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN user ur ON ur.Login=r.AbsenceOwner INNER JOIN user usr ON usr.Login=r.AbsenceNotifier WHERE o.RokSzkolny='" & RokSzkolny & "' AND sp.IdSzkola='" & Szkola & "' Order By r.NotifyDate DESC,r.DataLekcji DESC,u.Nazwisko,u.Imie;"
  End Function
  Public Function CountComplain(User As String) As String
    Return "SELECT Count(r.ID) FROM reklamacja r WHERE r.AbsenceOwner='" & User & "' AND Status=0;"
  End Function
  Public Function InsertAbsence() As String
    Return "INSERT INTO frekwencja VALUES(NULL,?IdUczen,?IdLekcja,?Typ,?Data,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function InsertComplain() As String
    Return "INSERT INTO reklamacja VALUES(NULL,?IdUczen,?IdLekcja,?Typ,?DataLekcji,?AbsenceOwner,?AbsenceNotifier,Curdate(),?Status,?Komentarz,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function
  Public Function UpdateAbsence() As String
    Return "UPDATE frekwencja SET Typ=?Typ,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?IdFrekwencja;"
  End Function
  Public Function UpdateComplainStatus() As String
    Return "UPDATE reklamacja SET Status=?Status,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?IdReklamacja;"
  End Function
  Public Function DeleteAbsence() As String
    Return "DELETE FROM frekwencja WHERE ID=?ID;"
  End Function
  Public Function DeleteBackAbsence() As String
    Return "DELETE FROM back_frekwencja WHERE IdUczen=?IdUczen AND IdLekcja=?IdLekcja AND Typ=?Typ AND Data=?Data;"
  End Function
  Public Function DeleteComplain() As String
    Return "DELETE FROM reklamacja WHERE ID=?ID;"
  End Function

End Class

Public Class WynikiRaportSQL
  Public Function CountAbsence(ByVal Klasa As String, ByVal StartDate As String, ByVal EndDate As String, RokSzkolny As String) As String
    Return "SELECT COUNT(f.ID),f.IdUczen,f.Typ FROM frekwencja f WHERE f.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' GROUP BY f.IdUczen,f.Typ;"
  End Function
  Public Function CountGradesByObjects(ByVal Klasa As String, ByVal Typ As String, RokSzkolny As String) As String
    Return "SELECT sp.IdPrzedmiot,so.Waga,COUNT(*) AS cnt FROM wyniki w,skala_ocen so,obsada o,kolumna k,szkola_przedmiot sp WHERE w.IdOcena=so.ID AND w.IdKolumna=k.ID AND k.IdObsada=o.ID AND o.Przedmiot=sp.ID AND w.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND k.Typ='" & Typ & "' AND w.IdKolumna IN (Select kn.ID FROM kolumna kn INNER JOIN obsada ob ON kn.IdObsada=ob.ID Where ob.RokSzkolny='" & RokSzkolny & "') GROUP BY sp.IdPrzedmiot,so.Waga;"
  End Function
  Public Function CountGradesByPupil(ByVal Klasa As String, ByVal Typ As String, RokSzkolny As String) As String
    Return "SELECT w.IdUczen,so.Waga,COUNT(*) As cnt FROM wyniki w, skala_ocen so,obsada o,kolumna k WHERE w.IdOcena=so.ID AND w.IdKolumna=k.ID AND k.IdObsada=o.ID AND w.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND k.Typ='" & Typ & "' And so.Typ!='z' AND w.IdKolumna IN (Select kn.ID FROM kolumna kn INNER JOIN obsada ob ON kn.IdObsada=ob.ID Where ob.RokSzkolny='" & RokSzkolny & "') GROUP BY w.IdUczen,so.Waga;"
  End Function
  Public Function CountNDSTByPupil(ByVal Klasa As String, ByVal Typ As String, RokSzkolny As String) As String
    Return "SELECT COUNT(*) As cnt FROM wyniki w,skala_ocen so,obsada o,kolumna k WHERE w.IdOcena=so.ID AND w.IdKolumna=k.ID AND k.IdObsada=o.ID AND w.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND k.Typ='" & Typ & "' And so.Typ!='z' AND o.Kategoria='o' AND so.Waga=1 AND w.IdKolumna IN (Select kn.ID FROM kolumna kn INNER JOIN obsada ob ON kn.IdObsada=ob.ID Where ob.RokSzkolny='" & RokSzkolny & "') GROUP BY w.IdUczen;"
  End Function
  Public Function CountNKLByPupil(ByVal Klasa As String, ByVal Typ As String, RokSzkolny As String) As String
    Return "SELECT COUNT(*) As cnt FROM wyniki w,skala_ocen so,obsada o,kolumna k WHERE w.IdOcena=so.ID AND w.IdKolumna=k.ID AND o.ID=k.IdObsada AND w.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND k.Typ='" & Typ & "' And so.Typ!='z' AND o.Kategoria='o' AND so.Waga=0 AND w.IdKolumna IN (Select kn.ID FROM kolumna kn INNER JOIN obsada ob ON kn.IdObsada=ob.ID Where ob.RokSzkolny='" & RokSzkolny & "') GROUP BY w.IdUczen;"
  End Function
  Public Function CountNotes(ByVal Klasa As String, ByVal StartDate As String, ByVal EndDate As String, RokSzkolny As String) As String
    Return "SELECT Count(uw.ID) luwg, uw.IdUczen, uw.TypUwagi FROM uwagi uw WHERE uw.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND uw.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' GROUP BY uw.IdUczen, uw.TypUwagi;"
  End Function
  Public Function SelectAvg(ByVal Klasa As String, ByVal Typ As String, RokSzkolny As String) As String
    Return "Select avg(so.Waga),w.IdUczen From wyniki w, skala_ocen so,obsada o,kolumna k WHERE w.IdOcena=so.ID AND w.IdKolumna=k.ID AND k.IdObsada=o.ID AND w.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND k.Typ='" & Typ & "' AND so.Waga>0 AND o.GetToAverage=1 AND w.IdKolumna IN (Select kn.ID FROM kolumna kn INNER JOIN obsada ob ON kn.IdObsada=ob.ID Where ob.RokSzkolny='" & RokSzkolny & "') GROUP BY w.IdUczen;"
  End Function
  Public Function SelectFaximile(ByVal Klasa As String) As String
    Return "SELECT n.Faximile FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON n.ID=sn.IdNauczyciel INNER JOIN obsada o ON o.Nauczyciel=sn.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID Where o.Klasa='" & Klasa & "' AND p.Typ='z';"
  End Function
  Public Function SelectPrzedmiot(ByVal IdKlasa As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT p.ID,if(p.Typ='z',p.Nazwa,p.Alias) AS Alias,o.Kategoria,p.Typ,sp.Priorytet,p.Nazwa FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE o.Klasa='" & IdKlasa & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sp.Priorytet;"
  End Function
  Public Function SelectResult(Klasa As String, Typ As String, RokSzkolny As String) As String
    Return "Select so.Ocena,so.Nazwa,so.Waga,k.Typ,sp.IdPrzedmiot,w.IdUczen,k.Poprawa From wyniki w Inner Join skala_ocen so on w.IdOcena=so.ID INNER Join kolumna k on w.IdKolumna=k.ID INNER JOIN obsada o ON k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID Where w.IdUczen IN (SELECT IdUczen FROM przydzial WHERE Klasa='" & Klasa & "' AND MasterRecord=1 AND RokSzkolny='" & RokSzkolny & "') AND k.Typ IN (" & Typ & ") AND w.IdKolumna IN (Select kn.ID FROM kolumna kn INNER JOIN obsada ob ON kn.IdObsada=ob.ID Where ob.RokSzkolny='" & RokSzkolny & "') Order by k.NrKolumny,w.Data,w.Version;"
  End Function
  'Public Function SelectConcatResult(Klasa As String) As String
  '  Return "Select k.Typ,Group_Concat(so.Ocena ORDER BY k.NrKolumny ASC SEPARATOR ', ') AS ResultString,sp.IdPrzedmiot,w.IdUczen From wyniki w Inner Join skala_ocen so on w.IdOcena=so.ID INNER Join kolumna k on w.IdKolumna=k.ID INNER JOIN obsada o ON k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID Where o.Klasa='" & Klasa & "' Group by Typ,IdUczen,IdPrzedmiot;"
  'End Function
  Public Function SelectSchoolPlace() As String
    Return "SELECT m.Nazwa FROM szkola s INNER JOIN miejscowosc m ON m.ID=s.IdMiejscowosc WHERE s.ID='" & My.Settings.IdSchool & "';"
  End Function
  Public Function SelectStudent(Klasa As String, RokSzkolny As String) As String
    Return "Select u.ID,p.NrwDzienniku,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID WHERE p.Klasa='" & Klasa & "' AND p.StatusAktywacji=1 AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectStudentList(Klasa As String, RokSzkolny As String) As String
    Return "Select u.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID WHERE p.Klasa='" & Klasa & "' AND p.MasterRecord=1  AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectClasses(IdSzkola As String, RokSzkolny As String, Virtual As String) As String
    Return "SELECT DISTINCT sk.ID,sk.Nazwa_Klasy FROM szkola_klasa sk INNER JOIN obsada o ON o.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.Virtual='" & Virtual & "' ORDER BY sk.Kod_Klasy;"
  End Function
End Class

Public Class UwagiSQL
  Public Function SelectStudent(Klasa As String, RokSzkolny As String) As String
    Return "Select u.ID,p.NrwDzienniku,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID WHERE p.Klasa='" & Klasa & "' AND p.StatusAktywacji=1 AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie Collate utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectStudent(Klasa As String, RokSzkolny As String, DataZajec As Date) As String
    Return "Select DISTINCT u.ID,p.NrwDzienniku,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student FROM uczen u INNER JOIN przydzial p ON u.ID=p.IdUczen INNER JOIN nauczanie_indywidualne ni ON p.ID=ni.IdPrzydzial INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' AND  o.DataAktywacji < '" & DataZajec.ToShortDateString & "' AND (o.DataDeaktywacji > '" & DataZajec.ToShortDateString & "' OR o.DataDeaktywacji is null) AND p.MasterRecord=1 ORDER BY p.NrwDzienniku,u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Overloads Function SelectNotes(ByVal Klasa As String, ByVal StartDate As String, ByVal EndDate As String) As String
    Return "SELECT uw.ID,uw.Autor,uw.TypUwagi,uw.Data,uw.TrescUwagi,uw.IdUczen, uw.User,uw.ComputerIP,uw.Version,uw.Owner FROM uwagi uw INNER JOIN uczen u ON u.ID=uw.IdUczen INNER JOIN przydzial p ON u.ID=p.IdUczen WHERE uw.Data between '" & StartDate & "' AND '" & EndDate & "' AND p.Klasa='" & Klasa & "' ORDER BY uw.Data,uw.Version,uw.ID;"
  End Function
  Public Overloads Function SelectNotes(ByVal KlasaWirtualna As String, RokSzkolny As String, ByVal StartDate As String, ByVal EndDate As String) As String
    Return "SELECT uw.ID,uw.Autor,uw.TypUwagi,uw.Data,uw.TrescUwagi,uw.IdUczen, uw.User,uw.ComputerIP,uw.Version,uw.Owner FROM uwagi uw INNER JOIN uczen u ON u.ID=uw.IdUczen INNER JOIN przydzial p ON u.ID=p.IdUczen WHERE uw.Data between '" & StartDate & "' AND '" & EndDate & "' AND u.ID IN (SELECT p.IdUczen FROM przydzial p INNER JOIN nauczanie_indywidualne ni ON p.ID=ni.IdPrzydzial INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE o.Klasa='" & KlasaWirtualna & "' AND o.RokSzkolny='" & RokSzkolny & "') ORDER BY uw.Data, uw.Version, uw.ID;"
  End Function
  Public Function InsertNote() As String
    Return "INSERT INTO uwagi VALUES(NULL,?IdUczen,?TrescUwagi,?TypUwagi,?Autor,?Data,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "',NULL);"
  End Function

  Public Function UpdateNote() As String
    Return "UPDATE uwagi SET TrescUwagi=?TrescUwagi,TypUwagi=?TypUwagi,Autor=?Autor,Data=?Data,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?IdUwaga;"
  End Function
  Public Function DeleteNote() As String
    Return "DELETE FROM uwagi WHERE ID=?ID;"
  End Function
End Class

Public Class AbsencjaRaportSQL
  Public Function SelectAbsence(Klasa As String, ByVal StartDate As String, ByVal EndDate As String) As String
    Return "SELECT f.IdUczen,CONCAT_WS(' ',u.Nazwisko,u.Imie) As Student,f.Data,CONCAT(gl.NrLekcji,') ',v1.Alias) as Lekcja,f.Typ AS Status,MONTH(f.Data) AS Miesiac FROM frekwencja f INNER JOIN lekcja l ON f.IdLekcja=l.ID INNER JOIN godzina_lekcyjna gl ON l.IdGodzina=gl.ID INNER JOIN obsada o ON o.ID=l.IdObsada INNER JOIN (SELECT o.ID,p.Alias FROM obsada o INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot) as v1 ON l.IdObsada=v1.ID INNER JOIN uczen u ON f.IdUczen=u.ID WHERE f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND o.Klasa='" & Klasa & "' ORDER BY IdUczen,Data,NrLekcji;"
  End Function
End Class

Public Class UwagiRaportSQL
  Public Function SelectNote(Klasa As String, ByVal StartDate As String, ByVal EndDate As String) As String
    Return "SELECT uw.IdUczen,CONCAT_WS(' ',u.Nazwisko,u.Imie) As Student,uw.Data,uw.TypUwagi,uw.Autor,uw.TrescUwagi FROM uwagi uw INNER JOIN uczen u ON uw.IdUczen=u.ID INNER JOIN przydzial p ON u.ID=p.IdUczen WHERE uw.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND p.Klasa='" & Klasa & "' ORDER BY IdUczen,Data,TrescUwagi;"
  End Function
End Class

Public Class MinLekcjaSQL
  Public Function SelectMinLekcja(Szkola As String, RokSzkolny As String) As String
    'CONCAT(ml.User,' (','Wł: ',ml.Owner,')') As 
    Return "SELECT ml.ID,sk.Nazwa_Klasy,ml.Wartosc,LEFT(sk.Kod_Klasy,1) AS Pion,ml.User, ml.ComputerIP, ml.Version,ml.Przedmiot,ml.Owner FROM min_lekcja ml INNER JOIN szkola_klasa sk ON sk.ID=ml.Klasa WHERE sk.IdSzkola='" & Szkola & "' AND ml.RokSzkolny='" & RokSzkolny & "' Order By sk.Kod_Klasy;"
  End Function

  Public Function InsertMinLekcja() As String
    Return "INSERT INTO min_lekcja VALUES(NULL,?Klasa,?Przedmiot,?RokSzkolny,?Wartosc,?User,?User,?ComputerIP,NULL);"
  End Function

  Public Function UpdateMinLekcja() As String
    Return "UPDATE min_lekcja SET Wartosc=?Wartosc,User='" + GlobalValues.AppUser.Login + "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function DeleteMinLekcja() As String
    Return "DELETE FROM min_lekcja WHERE ID=?ID;"
  End Function
  Public Function SelectClasses(IdSzkola As String, IdPrzedmiot As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT sk.ID,sk.Kod_klasy,sk.Nazwa_Klasy FROM szkola_klasa sk INNER JOIN przydzial p ON p.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.ID NOT IN (SELECT DISTINCT Klasa FROM min_lekcja m INNER JOIN szkola_klasa sk ON sk.ID=m.Klasa WHERE Przedmiot='" & IdPrzedmiot & "' AND RokSzkolny='" & RokSzkolny & "') ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectPrzedmiot(IdSzkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT p.ID,p.Alias FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sp.IdSzkola='" & IdSzkola & "' AND  o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa IN (SELECT Klasa FROM przydzial p WHERE p.RokSzkolny='" & RokSzkolny & "') ORDER BY sp.Priorytet;"
  End Function
End Class

Public Class TerminarzSQL
  Public Overloads Function SelectEvent(RokSzkolny As String, Klasa As String, StartDate As String, EndDate As String) As String
    Return "SELECT t.ID,g.NrLekcji,Concat_WS('',v1.Alias,' {',v2.Belfer,'}') as Przedmiot,t.Info,t.Status,t.Data,t.Owner, t.User, t.ComputerIP, t.Version,g.ID AS IdGodzina,l.DzienTygodnia,v1.Przedmiot AS IdSzkolaPrzedmiot, v2.Nauczyciel FROM terminarz t INNER JOIN lekcja l ON t.IdLekcja=l.ID INNER JOIN godzina_lekcyjna g ON l.IdGodzina=g.ID INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias,o.Przedmiot FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID,o.Nauczyciel FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 ON l.IdObsada=v2.ID WHERE t.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' ORDER BY g.NrLekcji;"
  End Function
  Public Function SelectLekcja(RokSzkolny As String, Szkola As String, Klasa As String, Nauczyciel As String, Data As Date) As String
    Return "SELECT l.ID,Concat_WS(' ',v3.Godzina,' --> ',v1.Alias,' {',v2.Belfer,'}') as Lekcja FROM lekcja l INNER JOIN (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v1 ON l.IdObsada=v1.ID INNER JOIN (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.Nauczyciel='" & Nauczyciel & "') AS v2 ON l.IdObsada=v2.ID INNER JOIN (SELECT Concat_WS(') ',g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i'))) as Godzina,g.ID,g.NrLekcji FROM godzina_lekcyjna g) as v3 ON l.IdGodzina=v3.ID WHERE l.IdPlan IN (SELECT ID FROM plan_lekcji p WHERE '" & Data.ToShortDateString & "' >= p.StartDate AND '" & Data.ToShortDateString & "' <= p.EndDate AND p.IdSzkola='" & Szkola & "') AND l.DzienTygodnia='" & Weekday(Data, FirstDayOfWeek.Monday) & "' AND l.ID NOT IN (SELECT IdLekcja FROM terminarz t WHERE Data='" & Data.ToShortDateString & "') ORDER BY v3.NrLekcji;"
  End Function
  Public Function SelectGodzina(IdGodzina As String) As String
    Return "SELECT Concat_WS(') ',g.NrLekcji,Concat_WS(' - ',Time_Format(g.StartTime,'%H:%i'),Time_Format(g.EndTime,'%H:%i'))) as Godzina FROM godzina_lekcyjna g WHERE g.ID='" & IdGodzina & "';"
  End Function
  Public Function InsertEvent() As String
    Return "INSERT INTO terminarz (IdLekcja,Info,Data,Status,Owner,User,ComputerIP) VALUES(?IdLekcja,?Info,?Data,?Status,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function DeleteEvent() As String
    Return "DELETE FROM terminarz WHERE ID=?ID;"
  End Function
  Public Function UpdateEvent() As String
    Return "UPDATE terminarz SET Info=?Info,Status=?Status,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?ID;"
  End Function
End Class

Public Class NauczanieIndywidualneSQL
  Public Function SelectStudent(RokSzkolny As String) As String
    Return "SELECT p.ID,CONCAT(CONCAT_WS(' ',u.Nazwisko,u.Imie,u.Imie2),' {Klasa: ',sk.Nazwa_Klasy,'}') AS Student FROM przydzial p INNER JOIN uczen u ON u.ID=p.IdUczen INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa WHERE p.ID IN (SELECT DISTINCT IdPrzydzial FROM nauczanie_indywidualne ni INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE o.RokSzkolny='" & RokSzkolny & "');"
  End Function
  Public Function SelectPrzedmiot(EndDate As Date) As String
    Return "SELECT CONCAT(v1.Przedmiot,' {',v2.Nauczyciel,'}') AS Przedmiot,o.DataAktywacji,o.DataDeaktywacji,ni.IdPrzydzial FROM obsada o INNER JOIN nauczanie_indywidualne ni ON o.ID=ni.IdObsada INNER JOIN (SELECT p.Alias AS Przedmiot,sp.ID,sp.Priorytet FROM przedmiot p INNER JOIN szkola_przedmiot sp ON sp.IdPrzedmiot=p.ID) AS v1 ON v1.ID=o.Przedmiot INNER JOIN (SELECT CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel,sn.ID FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID) AS v2 ON v2.ID=o.Nauczyciel WHERE o.DataAktywacji < '" & EndDate.ToShortDateString & "' AND (o.DataDeaktywacji >'" & EndDate.ToShortDateString & "' OR o.DataDeaktywacji is null) ORDER BY v1.Priorytet;"
  End Function
End Class

Public Class MoveResultSQL
  Public Function SelectStudent(ByVal RokSzkolny As String, Klasa As String) As String
    Return "SELECT u.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student,p.ID as IdPrzydzial FROM uczen u,przydzial p WHERE u.ID=p.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1 AND p.Klasa='" & Klasa & "' ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectVirtualStudent(ByVal RokSzkolny As String, Klasa As String) As String
    Return "SELECT DISTINCT u.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student,p.ID as IdPrzydzial FROM uczen u INNER JOIN przydzial p ON u.ID=p.IdUczen INNER JOIN nauczanie_indywidualne ni ON p.ID=ni.IdPrzydzial INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE o.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1 AND o.Klasa='" & Klasa & "' ORDER BY u.Nazwisko COLLATE utf8_polish_ci,u.Imie COLLATE utf8_polish_ci,u.Imie2 COLLATE utf8_polish_ci;"
  End Function
  Public Function SelectClass(ByVal RokSzkolny As String, Szkola As String, IdPrzydzial As String) As String
    Return "SELECT DISTINCT sk.ID As IdKlasa,sk.Nazwa_Klasy,sk.Virtual FROM szkola_klasa sk INNER JOIN przydzial p ON p.Klasa=sk.ID WHERE sk.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.ID='" & IdPrzydzial & "' ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectVirtualClass(ByVal RokSzkolny As String, Szkola As String, IdPrzydzial As String) As String
    Return "SELECT DISTINCT o.Klasa As IdKlasa,sk.Nazwa_Klasy,sk.Virtual FROM szkola_klasa sk INNER JOIN obsada o ON o.Klasa=sk.ID INNER JOIN nauczanie_indywidualne ni ON ni.IdObsada=o.ID WHERE sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND ni.IdPrzydzial='" & IdPrzydzial & "' ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectPrzedmiot(Przedmiot As String, Klasa As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT DISTINCT v1.ID AS IdObsada,CONCAT_WS('',v1.Alias,' {',v2.Belfer,'}') as Obsada, v2.SchoolTeacherID,v1.Przedmiot FROM (SELECT o.ID,if(sp.Grupa=0,p.ALias,if(LENGTH(sp.NazwaGrupy)>0,CONCAT(p.Alias,' - ',sp.NazwaGrupy),CONCAT(p.Alias,' - gr ',sp.Grupa))) AS Alias, sp.Priorytet,o.Przedmiot FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.Przedmiot='" & Przedmiot & "' AND o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data.ToShortDateString & "' AND (DATE(o.DataDeaktywacji) >= '" & Data.ToShortDateString & "' OR o.DataDeaktywacji is null) AND p.Typ<>'Z') AS v1, (SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,o.ID,o.Nauczyciel AS SchoolTeacherID FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "') AS v2 WHERE v1.ID=v2.ID ORDER BY v1.Priorytet;"
  End Function
  Public Function SelectSourceColumn(IdObsada As String, IdUczen As String) As String
    Return "SELECT k.ID,k.NrKolumny,k.Typ,w.ID AS IdWynik FROM kolumna k INNER JOIN wyniki w ON w.IdKolumna=k.ID WHERE k.IdObsada='" & IdObsada & "' AND w.IdUczen='" & IdUczen & "';"
  End Function
  Public Function SelectTargetColumn(IdObsada As String) As String
    Return "SELECT k.ID,k.NrKolumny,k.Typ FROM kolumna k WHERE k.IdObsada='" & IdObsada & "';"
  End Function
  Public Function UpdateWynik() As String
    Return "UPDATE wyniki SET IdKolumna=?IdKolumna,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?IdWynik;"
  End Function
End Class

Public Class FrekwencjaSQL
  Public Function SelectStudent(RokSzkolny As String, Klasa As String) As String
    Return "SELECT `ID`, Date(`DataAktywacji`) As Aktywacja, Date(`DataDeaktywacji`) As Deaktywacja,StatusAktywacji As Status FROM `przydzial` WHERE Klasa='" & Klasa & "' AND RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function SelectObjectGroup(RokSzkolny As String, Klasa As String) As String
    Return "SELECT g.IdPrzydzial,g.IdSzkolaPrzedmiot AS Przedmiot,p.StatusAktywacji,p.DataAktywacji,p.DataDeaktywacji FROM grupa g INNER JOIN przydzial p ON p.ID=g.IdPrzydzial WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function SelectIndividualCourse(RokSzkolny As String, Klasa As String) As String
    Return "SELECT ni.IdPrzydzial,o.Przedmiot,Date(o.DataAktywacji) AS Aktywacja,Date(o.DataDeaktywacji) AS Deaktywacja FROM nauczanie_indywidualne ni INNER JOIN przydzial p ON p.ID=ni.IdPrzydzial INNER JOIN obsada o ON o.ID=ni.IdObsada WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function CountTopic(Klasa As String, RokSzkolny As String) As String
    Return "SELECT Month(t.Data) AS MonthNumber,t.ID,t.Data,o.Przedmiot FROM temat t INNER JOIN lekcja l ON t.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE t.Status=1 AND o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' ORDER BY t.Data;"
    'Return "SELECT Month(t.Data) AS MonthNumber,Count(t.ID) AS TopicCount,t.Data FROM temat t INNER JOIN lekcja l ON t.IdLekcja=l.ID INNER JOIN obsada o ON o.ID=l.IdObsada WHERE t.Status=1 AND o.RokSzkolny='" & RokSzkolny & "' AND o.Klasa='" & Klasa & "' Group BY Month(t.Data);"
  End Function

End Class

Public Class ZestawienieOcenSQL
  Public Overloads Function SelectPrzedmiot(RokSzkolny As String, Klasa As String, Data As Date) As String
    Return "SELECT DISTINCT p.ID,if(p.Typ='z',p.Nazwa,p.ALias) AS Przedmiot,p.Nazwa,p.Typ FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data & "' AND (DATE(o.DataDeaktywacji) >= '" & Data & "' OR o.DataDeaktywacji is null) Order By sp.Priorytet;"
  End Function
  Public Overloads Function SelectPrzedmiot(Szkola As String, RokSzkolny As String, Klasa As String, Data As Date, Przedmiot As String) As String
    Return "SELECT DISTINCT p.ID,if(p.Typ='z',p.Nazwa,p.ALias) AS Przedmiot,p.Nazwa,p.Typ FROM obsada o INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE p.ID NOT IN (" & Przedmiot & ") AND o.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE sk.Kod_Klasy='" & Klasa & "' AND IdSzkola='" & Szkola & "') AND o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data & "' AND (DATE(o.DataDeaktywacji) >= '" & Data & "' OR o.DataDeaktywacji is null) Order By sp.Priorytet;"
  End Function
  Public Overloads Function SelectWynik(RokSzkolny As String, Klasa As String, StartDate As Date, EndDate As Date, Typ As String) As String
    Return "Select so.Ocena As Wynik,w.Data,w.User,w.Owner, w.ComputerIP, w.Version, w.IdUczen,so.Waga,sp.IdPrzedmiot,o.GetToAverage,pt.Typ From wyniki w Inner Join skala_ocen so on w.IdOcena=so.ID INNER Join kolumna k on w.IdKolumna=k.ID INNER JOIN obsada o On k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przydzial p ON p.IdUczen=w.IdUczen INNER JOIN przedmiot pt ON sp.IdPrzedmiot=pt.ID Where k.Typ='" & Typ & "' AND p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND DATE(w.Data) BETWEEN '" & StartDate.ToShortDateString & "' AND '" & EndDate.ToShortDateString & "';"
  End Function
  Public Overloads Function SelectWynik(Szkola As String, RokSzkolny As String, Klasa As String, StartDate As Date, EndDate As Date, Typ As String, Przedmiot As String) As String
    Return "Select so.Ocena As Wynik,w.Data,w.User,w.Owner, w.ComputerIP, w.Version, w.IdUczen,so.Waga,sp.IdPrzedmiot,o.GetToAverage,pt.Typ From wyniki w Inner Join skala_ocen so on w.IdOcena=so.ID INNER Join kolumna k on w.IdKolumna=k.ID INNER JOIN obsada o On k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przydzial p ON p.IdUczen=w.IdUczen INNER JOIN przedmiot pt ON sp.IdPrzedmiot=pt.ID Where k.Typ='" & Typ & "' AND p.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE sk.Kod_Klasy='" & Klasa & "' AND IdSzkola='" & Szkola & "') AND p.RokSzkolny='" & RokSzkolny & "' AND DATE(w.Data) BETWEEN '" & StartDate.ToShortDateString & "' AND '" & EndDate.ToShortDateString & "' AND sp.IdPrzedmiot IN (" & Przedmiot & ");"
  End Function
  Public Function SelectClasses(IdSzkola As String, RokSzkolny As String) As String
    Return "SELECT DISTINCT sk.ID As IdKlasa,sk.Nazwa_Klasy,sk.Kod_Klasy FROM szkola_klasa sk INNER JOIN przydzial p ON p.Klasa=sk.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY sk.Kod_Klasy;"
  End Function
End Class

Public Class PromocjaSQL
  Public Function SelectAllocatedStudent(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    Return "SELECT u.ID FROM uczen u,przydzial p,szkola_klasa sk WHERE p.Klasa=sk.ID AND u.ID=p.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1 AND sk.IdSzkola='" & IdSzkola & "';"
  End Function
  Public Function SelectStudent(ByVal RokSzkolny As String, ByVal IdSzkola As String) As String
    Return "SELECT u.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student,p.Klasa,p.Promocja,p.ID as IdPrzydzial,p.Owner,p.User,p.ComputerIP,p.Version FROM uczen u,przydzial p,szkola_klasa sk WHERE p.Klasa=sk.ID AND u.ID=p.IdUczen AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1 AND sk.IdSzkola='" & IdSzkola & "' ORDER BY p.Klasa,u.Nazwisko,u.Imie;"
  End Function

  Public Function UpdatePrzydzial(ByVal IdPrzydzial As String, ByVal RokSzkolny As String, ByVal Promocja As String) As String
    Return "UPDATE przydzial p SET p.Promocja='" & Promocja & "',User='" & GlobalValues.AppUser.Login & "',ComputerIP='" + GlobalValues.gblIP + "',Version=NULL WHERE p.ID='" & IdPrzydzial & "' AND p.RokSzkolny='" & RokSzkolny & "';"
  End Function

End Class

Public Class PoprawkaSQL
  Public Overloads Function SelectPrzedmiot(Szkola As String, RokSzkolny As String, Typ As String) As String
    Return "SELECT DISTINCT sp.IdPrzedmiot,p.Nazwa FROM obsada o INNER JOIN poprawka pk ON o.ID=pk.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE pk.Typ='" & Typ & "' AND sp.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sp.Priorytet;"
    'Return "SELECT DISTINCT o.ID,p.Nazwa,o.Klasa,sp.IdPrzedmiot,o.Nauczyciel FROM obsada o INNER JOIN poprawka pk ON o.ID=pk.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE pk.Typ='" & Typ & "' AND sp.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' ORDER BY sp.Priorytet;"
  End Function
  Public Overloads Function SelectPrzedmiot(RokSzkolny As String, Typ As String, IdPrzydzial As Integer) As String
    Return "SELECT DISTINCT o.ID,p.Nazwa FROM obsada o INNER JOIN kolumna k ON k.IdObsada=o.ID INNER JOIN wyniki w ON w.IdKolumna=k.ID INNER JOIN przydzial pl ON pl.IdUczen=w.IdUczen INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot INNER JOIN skala_ocen so ON so.ID=w.IdOcena WHERE k.Typ='" & Typ & "' AND so.Waga=1 AND so.Typ IN ('P','PZ') AND o.RokSzkolny='" & RokSzkolny & "' AND pl.ID=" & IdPrzydzial & " AND o.ID NOT IN (SELECT pw.IdObsada FROM poprawka pw WHERE pw.IdPrzydzial=" & IdPrzydzial & ");"
  End Function
  Public Overloads Function SelectStudent(Szkola As String, RokSzkolny As String, Typ As String) As String
    Return "SELECT DISTINCT p.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie,'{ klasa',sk.Nazwa_Klasy,'}') AS Student FROM przydzial p INNER JOIN poprawka pk ON p.ID=pk.IdPrzydzial INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN uczen u ON p.IdUczen=u.ID WHERE pk.Typ='" & Typ & "' AND sk.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY u.Nazwisko,u.Imie;"
    'Return "SELECT DISTINCT p.IdUczen,CONCAT_WS(' ',u.Nazwisko,u.Imie,'{ klasa',sk.Nazwa_Klasy,'}') AS Student,p.Klasa,p.ID,sk.Kod_Klasy FROM przydzial p INNER JOIN poprawka pk ON p.ID=pk.IdPrzydzial INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN uczen u ON p.IdUczen=u.ID WHERE pk.Typ='" & Typ & "' AND sk.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY u.Nazwisko,u.Imie;"
  End Function
  Public Overloads Function SelectStudent(Klasa As String, RokSzkolny As String, Typ As String, LiczbaNdst As Byte) As String
    Return "SELECT DISTINCT p.ID,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student FROM przydzial p INNER JOIN uczen u ON p.IdUczen=u.ID WHERE p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND u.ID IN (SELECT IdUczen FROM (SELECT w.IdUczen,Count(w.ID) AS Liczba FROM wyniki w INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena WHERE k.Typ='" & Typ & "' AND so.Waga BETWEEN 0 AND 1 AND so.Typ IN ('P','PZ') Group By IdUczen) AS v1 WHERE Liczba<" & LiczbaNdst & ");"
  End Function
  Public Overloads Function SelectStudent(Szkola As String, RokSzkolny As String, Klasa As String, Typ As String) As String
    Return "SELECT p.NrwDzienniku,CONCAT_WS(' ',u.Nazwisko,u.Imie) AS Student,pt.Alias,pk.IdPrzydzial,pk.IdObsada,pk.Typ,o.Nauczyciel,pk.IdOcena,pk.Data,pk.Lock,pk.Owner,pk.User,pk.ComputerIP,pk.Version FROM przydzial p INNER JOIN poprawka pk ON p.ID=pk.IdPrzydzial INNER JOIN obsada o ON o.ID=pk.IdObsada INNER JOIN uczen u ON p.IdUczen=u.ID INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot WHERE pk.Typ='" & Typ & "' AND sp.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "' ORDER BY u.Nazwisko,u.Imie,sp.Priorytet;"
  End Function
  Public Function SelectPoprawkaByObject(Szkola As String, RokSzkolny As String, Typ As String) As String
    Return "SELECT CONCAT_WS(' ',u.Nazwisko,u.Imie,'{ klasa',sk.Nazwa_Klasy,'}') AS Student,CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel,pt.ID AS FilterName,pt.Nazwa AS GroupName,pk.IdPrzydzial,pk.IdObsada,pk.Typ,pk.Owner,pk.User,pk.ComputerIP,pk.Version FROM przydzial p INNER JOIN poprawka pk ON p.ID=pk.IdPrzydzial INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN uczen u ON p.IdUczen=u.ID INNER JOIN obsada o ON pk.IdObsada=o.ID INNER JOIN szkola_nauczyciel sn On o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot WHERE pk.Typ='" & Typ & "' AND sk.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY sp.Priorytet,u.Nazwisko,u.Imie;"
  End Function
  Public Function SelectPoprawkaByStudent(Szkola As String, RokSzkolny As String, Typ As String) As String
    Return "SELECT pt.Nazwa AS Przedmiot,CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Nauczyciel,pk.IdPrzydzial AS FilterName, CONCAT_WS(' ',u.Nazwisko,u.Imie,'{ klasa',sk.Nazwa_Klasy,'}') AS GroupName,pk.IdPrzydzial, pk.IdObsada,pk.Typ,pk.Owner,pk.User,pk.ComputerIP,pk.Version FROM obsada o INNER JOIN poprawka pk ON o.ID=pk.IdObsada INNER JOIN szkola_klasa sk ON sk.ID=o.Klasa INNER JOIN przydzial p ON p.ID=pk.IdPrzydzial INNER JOIN uczen u ON p.IdUczen=u.ID INNER JOIN szkola_nauczyciel sn On o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot WHERE pk.Typ='" & Typ & "' AND sk.IdSzkola='" & Szkola & "' AND p.RokSzkolny='" & RokSzkolny & "' ORDER BY u.Nazwisko,u.Imie,sp.Priorytet;"
  End Function

  Public Function DeletePoprawka() As String
    Return "DELETE FROM poprawka WHERE IdPrzydzial=?IdPrzydzial AND IdObsada=?IdObsada AND Typ=?Typ;"
  End Function
  Public Function InsertPoprawka() As String
    Return "INSERT INTO poprawka (IdPrzydzial,IdObsada,Typ,Owner,User,ComputerIP) VALUES (?IdPrzydzial,?IdObsada,?Typ,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function UpdateResult() As String
    Return "Update poprawka Set IdOcena=?IdOcena,Data=?Data,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL Where IdPrzydzial=?IdPrzydzial AND IdObsada=?IdObsada AND Typ=?Typ;"
  End Function
  Public Function DeleteResult() As String
    Return "Update poprawka Set IdOcena=NULL,Data=NULL,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL Where IdPrzydzial=?IdPrzydzial AND IdObsada=?IdObsada AND Typ=?Typ;"
  End Function
End Class

Public Class WniosekSQL
  Public Function SelectWniosek() As String
    Return "SELECT w.ID,Concat_WS(' ',w.ParentName,w.ParentGivenName) AS ParentName,w.ApplyType,Cast(w.ApplyDate AS CHAR) AS ApplyDate,w.Status,w.ParentEmail,w.ParentLogin, w.ParentIP, w.User,w.ComputerIP, w.Version FROM wniosek w Order By w.ApplyDate DESC;"
  End Function
  Public Function SelectSubwniosek() As String
    Return "SELECT s.ID,Concat_WS(' ',s.StudentName,s.StudentGivenName) AS Student,s.StudentPesel,s.Status,s.User,s.ComputerIP,s.Version,s.IdWniosek FROM subwniosek s Order By s.IdWniosek,s.StudentName,s.StudentGivenName;"
  End Function
  Public Function SelectStudent(Name As String, GivenName As String, Pesel As String) As String
    Return "SELECT u.ID FROM uczen u WHERE u.Nazwisko='" & Name & "' AND u.Imie='" & GivenName & "' AND u.Pesel='" & Pesel & "';"
  End Function
  Public Function SelectStudentPesel(UserLogin As String) As String
    Return "SELECT u.Pesel FROM uczen u INNER JOIN uprawnienie up ON u.ID=up.IdUczen WHERE up.UserLogin='" & UserLogin & "';"
  End Function
  Public Function SelectStudentAllocation(Pesel As String, RokSzkolny As String, Data As String) As String
    Return "SELECT sk.Nazwa_Klasy AS Klasa,Concat_WS(' ',n.Nazwisko,n.Imie) AS Wychowawca FROM szkola_klasa sk INNER JOIN obsada o ON sk.ID=o.Klasa INNER JOIN szkola_nauczyciel sn ON sn.ID=o.Nauczyciel INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE o.Klasa IN (SELECT p.Klasa FROM przydzial p INNER JOIN uczen u ON u.ID=p.IdUczen WHERE u.Pesel='" & Pesel & "' AND p.RokSzkolny='" & RokSzkolny & "' AND p.StatusAktywacji=1) AND o.Przedmiot IN (SELECT sp.ID FROM szkola_przedmiot sp INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE p.Typ='Z') AND o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data & "' AND (DATE(o.DataDeaktywacji) >= '" & Data & "' OR o.DataDeaktywacji is null);"
  End Function
  Public Function CountUser(Name As String, GivenName As String, Login As String) As String
    Return "SELECT Count(Login) FROM user WHERE Nazwisko='" & Name & "' AND Imie='" & GivenName & "' AND Login='" & Login & "';"
  End Function
  Public Function CountUser(Name As String, GivenName As String) As String
    Return "SELECT Count(Login) FROM user WHERE Nazwisko='" & Name & "' AND Imie='" & GivenName & "';"
  End Function
  Public Function CountStudent(IdStudent As String, SchoolYear As String) As String
    Return "SELECT Count(p.ID) FROM przydzial p WHERE p.IdUczen='" & IdStudent & "' AND p.RokSzkolny='" & SchoolYear & "' AND p.StatusAktywacji=1;"
  End Function
  Public Function UpdateStatus() As String
    Return "UPDATE wniosek SET Status=?Status,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function UpdateErrCode() As String
    Return "UPDATE subwniosek SET Status=?Status,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function UpdateWniosek() As String
    Return "UPDATE wniosek SET ParentLogin=?Login,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE ID=?ID;"
  End Function
  Public Function UpdateAccountStatus() As String
    Return "UPDATE user SET Status=?Status,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "',Version=NULL WHERE Login=?Login;"
  End Function
  Public Function DeleteWniosek(IdWniosek As String) As String
    Return "Delete FROM wniosek WHERE ID=" & IdWniosek & ";"
  End Function
End Class

Public Class UzasadnienieSQL
  Public Function SelectUzasadnienie(Nauczyciel As String, RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID,u.IdWynik,u.Tresc,u.Status,u.Owner FROM uzasadnienie u INNER JOIN wyniki w ON u.IdWynik=w.ID INNER JOIN kolumna k ON w.IdKolumna=k.ID INNER JOIN obsada o ON o.ID=k.IdObsada WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "';"
  End Function
  Public Function SelectUzasadnienie(RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID,u.IdWynik,u.Tresc,u.Status,u.Owner FROM uzasadnienie u INNER JOIN wyniki w ON u.IdWynik=w.ID INNER JOIN kolumna k ON w.IdKolumna=k.ID INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.Id=sp.IdPrzedmiot WHERE u.Status>0 AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND p.Typ<>'Z';"
  End Function
  Public Function SelectUzasadnienieZ(RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID,u.IdWynik,u.Tresc,u.Status,u.Owner FROM uzasadnienie u INNER JOIN wyniki w ON u.IdWynik=w.ID INNER JOIN kolumna k ON w.IdKolumna=k.ID INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.Id=sp.IdPrzedmiot WHERE u.Status>0 AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND p.Typ='Z';"
  End Function
  Public Function SelectUzasadnienieZ(Nauczyciel As String, RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID,u.IdWynik,u.Tresc,u.Status,u.Owner FROM uzasadnienie u INNER JOIN wyniki w ON u.IdWynik=w.ID INNER JOIN kolumna k ON w.IdKolumna=k.ID INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.Id=sp.IdPrzedmiot WHERE o.Nauczyciel='" & Nauczyciel & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Semestr & "' AND p.Typ='Z';"
  End Function

  Public Function SelectGrupa(IdSzkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT DISTINCT o.Nauczyciel AS ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nazwa FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID INNER JOIN obsada o ON o.Nauczyciel=sn.ID WHERE sn.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND o.ID IN (SELECT DISTINCT o.ID FROM obsada o INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN uzasadnienie u ON w.ID=u.IdWynik WHERE u.Status>0 AND sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND so.Waga IN (0,1) AND so.Typ<>'Z') ORDER BY n.Nazwisko,n.Imie;"
  End Function
  Public Function SelectGrupaZ(IdSzkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT DISTINCT o.Nauczyciel AS ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nazwa FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID INNER JOIN obsada o ON o.Nauczyciel=sn.ID INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN uzasadnienie u ON w.ID=u.IdWynik INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sn.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND so.Waga IN (0,1,2) AND p.Typ='Z' ORDER BY n.Nazwisko,n.Imie;"
  End Function
  Public Function SelectGrupaB(IdSzkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT DISTINCT o.Nauczyciel AS ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nazwa FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID INNER JOIN obsada o ON o.Nauczyciel=sn.ID INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE sn.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND so.Waga IN (0,1) AND so.Typ<>'Z' AND w.ID NOT IN (SELECT IdWynik FROM uzasadnienie u WHERE Status>0) ORDER BY n.Nazwisko,n.Imie;"
  End Function
  Public Function SelectGrupaBZ(IdSzkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT DISTINCT o.Nauczyciel AS ID,Concat_WS(' ',n.Nazwisko,n.Imie) AS Nazwa FROM nauczyciel n INNER JOIN szkola_nauczyciel sn ON sn.IdNauczyciel=n.ID INNER JOIN obsada o ON o.Nauczyciel=sn.ID INNER JOIN kolumna k ON o.ID=k.IdObsada INNER JOIN wyniki w ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sn.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND so.Waga IN (0,1,2) AND p.Typ='Z' AND w.ID NOT IN (SELECT IdWynik FROM uzasadnienie u WHERE Status>0) ORDER BY n.Nazwisko,n.Imie;"
  End Function


  Public Function SelectStudent(Szkola As String, RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID as IdStudent,Concat(Concat_ws(' ',u.Nazwisko,u.Imie),' {',sk.Nazwa_Klasy,'}') AS Student,pt.Alias,so.Nazwa AS Ocena,o.Nauczyciel,o.Przedmiot,o.Klasa,w.ID AS IdWynik,sk.Nazwa_Klasy,Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,uz.Status FROM wyniki w INNER JOIN uczen u On u.ID=w.IdUczen INNER JOIN skala_ocen so ON w.IdOcena=so.ID INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot INNER JOIN uzasadnienie uz ON w.ID=uz.IdWynik INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE pt.Typ<>'Z' AND so.Waga IN (0,1) AND k.Typ='" & Semestr & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND uz.Status>0 ORDER BY u.Nazwisko,u.Imie,sk.Kod_Klasy;"
  End Function
  Public Function SelectStudentB(Szkola As String, RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID as IdStudent,Concat(Concat_ws(' ',u.Nazwisko,u.Imie),' {',sk.Nazwa_Klasy,'}') AS Student,pt.Alias,so.Nazwa AS Ocena,o.Nauczyciel,o.Przedmiot,o.Klasa,w.ID AS IdWynik,sk.Nazwa_Klasy,Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer FROM wyniki w INNER JOIN uczen u On u.ID=w.IdUczen INNER JOIN skala_ocen so ON w.IdOcena=so.ID INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE pt.Typ<>'Z' AND so.Waga IN (0,1) AND k.Typ='" & Semestr & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND w.ID NOT IN (SELECT IdWynik FROM uzasadnienie u WHERE Status>0) ORDER BY u.Nazwisko,u.Imie,sk.Kod_Klasy;"
  End Function
  Public Function SelectStudentZ(Szkola As String, RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID as IdStudent,Concat(Concat_ws(' ',u.Nazwisko,u.Imie),' {',sk.Nazwa_Klasy,'}') AS Student,pt.Alias,so.Nazwa AS Ocena,o.Nauczyciel,o.Przedmiot,o.Klasa,w.ID AS IdWynik,sk.Nazwa_Klasy,Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer,uz.Status FROM wyniki w INNER JOIN uczen u On u.ID=w.IdUczen INNER JOIN skala_ocen so ON w.IdOcena=so.ID INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot INNER JOIN uzasadnienie uz ON w.ID=uz.IdWynik INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE pt.Typ='Z' AND so.Waga IN (0,1,2) AND k.Typ='" & Semestr & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND uz.Status>0 ORDER BY u.Nazwisko,u.Imie,sk.Kod_Klasy;"
  End Function
  Public Function SelectStudentBZ(Szkola As String, RokSzkolny As String, Semestr As String) As String
    Return "SELECT u.ID as IdStudent,Concat(Concat_ws(' ',u.Nazwisko,u.Imie),' {',sk.Nazwa_Klasy,'}') AS Student,pt.Alias,so.Nazwa AS Ocena,o.Nauczyciel,o.Przedmiot,o.Klasa,w.ID AS IdWynik,sk.Nazwa_Klasy,Concat_WS(' ',n.Nazwisko,n.Imie) AS Belfer FROM wyniki w INNER JOIN uczen u On u.ID=w.IdUczen INNER JOIN skala_ocen so ON w.IdOcena=so.ID INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN szkola_klasa sk ON sk.ID=p.Klasa INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON n.ID=sn.IdNauczyciel WHERE pt.Typ='Z' AND so.Waga IN (0,1,2) AND k.Typ='" & Semestr & "' AND p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND  w.ID NOT IN (SELECT IdWynik FROM uzasadnienie u WHERE Status>0) ORDER BY u.Nazwisko,u.Imie,sk.Kod_Klasy;"
  End Function

  Public Function InsertUzasadnienie() As String
    Return "INSERT INTO uzasadnienie (IdWynik,Tresc,Status,Owner,User,ComputerIP) VALUES (?IdWynik,?Tresc,?Status,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function UpdateUzasadnienie() As String
    Return "UPDATE uzasadnienie SET Tresc=?Tresc,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "' WHERE IdWynik=?IdWynik;"
  End Function
  Public Function DeleteUzasadnienie() As String
    Return "DELETE FROM uzasadnienie WHERE IdWynik=?IdWynik;"
  End Function
  Public Function ChangeStatus() As String
    Return "UPDATE uzasadnienie SET Status=?Status,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "' WHERE IdWynik=?IdWynik;"
  End Function
End Class

Public Class KlasyfikacjaSQL
  Public Function SelectWychowawca(Klasa As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT Concat_WS(' ',n.Nazwisko,n.Imie) AS Wychowawca FROM obsada o INNER JOIN szkola_nauczyciel sn ON o.Nauczyciel=sn.ID INNER JOIN nauczyciel n ON sn.IdNauczyciel=n.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON p.ID=sp.IdPrzedmiot WHERE p.Typ='z' AND o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "' AND DATE(o.DataAktywacji) <= '" & Data.ToShortDateString & "' AND (DATE(o.DataDeaktywacji) >= '" & Data.ToShortDateString & "' OR o.DataDeaktywacji is null);"
  End Function
  Public Function SelectClasses(IdSzkola As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT sk.ID,sk.Nazwa_Klasy,LEFT(sk.Kod_Klasy,1) AS Pion,o.ID as IdObsada,sk.Kod_Klasy FROM szkola_klasa sk INNER JOIN obsada o ON o.Klasa=sk.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE sk.IdSzkola='" & IdSzkola & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sk.Virtual=0 AND p.Typ='Z' AND ('" & Data & "' BETWEEN DATE(o.DataAktywacji) AND DATE(o.DataDeaktywacji) OR o.DataDeaktywacji is null) ORDER BY sk.Kod_Klasy;"
  End Function
  Public Function SelectClasification(IdObsada As String, Okres As String) As String
    Return "SELECT Status FROM klasyfikacja WHERE IdObsada='" & IdObsada & "' AND Typ='" & Okres & "';"
  End Function
  Public Function SelectStudentByKlasa(Klasa As String, RokSzkolny As String, Data As Date) As String
    Return "SELECT Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,p.DataAktywacji,u.ID FROM przydzial p INNER JOIN uczen u ON u.ID=p.IdUczen WHERE p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "' AND p.MasterRecord=1 AND ('" & Data & "' BETWEEN DATE(p.DataAktywacji) AND DATE(p.DataDeaktywacji) OR p.DataDeaktywacji is null) Order BY u.Nazwisko,u.Imie;"
  End Function
  Public Function SelectStudentByNkl(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT w.IdUczen,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,pdl.DataAktywacji FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN uczen u ON u.ID=w.IdUczen INNER JOIN przydzial pdl ON pdl.IdUczen=u.ID WHERE so.Waga=0 AND p.Typ='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' Order BY u.Nazwisko,u.Imie;"
  End Function
  Public Function SelectStudentByNdst(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT Concat_WS(' ',u.Nazwisko,u.Imie) AS Student,Concat_WS(' - ',p.Alias,so.Nazwa) AS Przedmiot,w.IdUczen FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN uczen u ON u.ID=w.IdUczen WHERE so.Waga IN (0,1) AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' Order BY u.Nazwisko,u.Imie,sp.Priorytet;"
  End Function
  Public Function SelectStudentByNdstByObsada(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT pdl.ID AS IdPrzydzial,o.ID IdObsada,so.Waga FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN przydzial pdl ON pdl.IdUczen = w.IdUczen WHERE so.Waga BETWEEN 0 AND 1 AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' AND pdl.RokSzkolny='" & RokSzkolny & "';"
  End Function
  Public Function SelectRepeaterByNdst(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT Concat_WS(' - ',Concat_WS(' ',u.Nazwisko,u.Imie),Group_Concat(p.Alias Order By sp.Priorytet SEPARATOR ', ')) AS Student,w.IdUczen FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN uczen u ON u.ID=w.IdUczen WHERE so.Waga=1 AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' Group BY IdUczen;"
  End Function
  Public Function SelectStudentByNg(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT Concat(Concat_WS(' ',u.Nazwisko,u.Imie),' - ',so.Nazwa) AS Student,w.IdUczen FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN uczen u ON u.ID=w.IdUczen WHERE so.Waga IN (1,2) AND p.Typ='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' Order BY u.Nazwisko,u.Imie,sp.Priorytet;"
  End Function
  Public Overloads Function SelectWynik(RokSzkolny As String, Klasa As String, StartDate As Date, EndDate As Date, Typ As String) As String
    Return "Select so.Nazwa As Wynik,w.IdUczen,so.Waga,o.GetToAverage,pt.Typ,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student From wyniki w Inner Join skala_ocen so on w.IdOcena=so.ID INNER Join kolumna k on w.IdKolumna=k.ID INNER JOIN obsada o On k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN uczen u ON u.ID=w.IdUczen INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN przedmiot pt ON sp.IdPrzedmiot=pt.ID Where k.Typ='" & Typ & "' AND p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' AND DATE(w.Data) BETWEEN '" & StartDate.ToShortDateString & "' AND '" & EndDate.ToShortDateString & "' Order By Student;"
  End Function
  Public Overloads Function SelectWynik(Szkola As String, RokSzkolny As String, Klasa As String, StartDate As Date, EndDate As Date, Typ As String, Przedmiot As String) As String
    Return "Select so.Nazwa As Wynik,w.IdUczen,so.Waga,o.GetToAverage,pt.Typ,Concat_WS(' ',u.Nazwisko,u.Imie) AS Student From wyniki w Inner Join skala_ocen so on w.IdOcena=so.ID INNER Join kolumna k on w.IdKolumna=k.ID INNER JOIN obsada o On k.IdObsada=o.ID INNER JOIN szkola_przedmiot sp ON o.Przedmiot=sp.ID INNER JOIN uczen u ON u.ID=w.IdUczen INNER JOIN przydzial p ON p.IdUczen=u.ID INNER JOIN przedmiot pt ON sp.IdPrzedmiot=pt.ID Where k.Typ='" & Typ & "' AND p.Klasa IN (SELECT ID FROM szkola_klasa sk WHERE sk.Kod_Klasy='" & Klasa & "' AND IdSzkola='" & Szkola & "') AND p.RokSzkolny='" & RokSzkolny & "' AND DATE(w.Data) BETWEEN '" & StartDate.ToShortDateString & "' AND '" & EndDate.ToShortDateString & "' AND sp.IdPrzedmiot IN (" & Przedmiot & ") Order By Student;"
  End Function
  Public Function SelectGrupaPrzedmiotowa(RokSzkolny As String, Klasa As String) As String
    Return "SELECT g.IdSzkolaPrzedmiot as Przedmiot,p.IdUczen FROM grupa g INNER JOIN przydzial p ON p.ID=g.IdPrzydzial WHERE p.RokSzkolny='" & RokSzkolny & "' AND p.Klasa='" & Klasa & "' AND p.MasterRecord=1;"
  End Function
  Public Function SelectClasificationStatus(Szkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT o.Klasa,k.Status,k.IdObsada FROM klasyfikacja k INNER JOIN obsada o ON k.IdObsada=o.ID INNER JOIN szkola_klasa sk ON o.Klasa=sk.ID WHERE k.Status>0 AND o.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND k.Typ='" & Okres & "';"
  End Function
  'Public Function CountAbsenceByStudentNkl(Szkola As String, Klasa As String, RokSzkolny As String, Okres As String, StartDate As Date, EndDate As Date) As String
  '  Return "SELECT f.IdUczen,COUNT(f.ID) AS Abc,f.Data FROM frekwencja f WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND f.IdUczen IN (Select DISTINCT w.IdUczen FROM wyniki w INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot pt ON pt.ID=sp.IdPrzedmiot WHERE so.Waga=0 AND pt.Typ='Z' AND k.Typ='" & Okres & "' AND o.RokSzkolny='" & RokSzkolny & "' AND sp.IdSzkola='" & Szkola & "' AND o.Klasa='" & Klasa & "') GROUP BY f.IdUczen,f.Data;"
  'End Function
  Public Function CountAbsenceByStudent(Klasa As String, RokSzkolny As String, StartDate As Date, EndDate As Date) As String
    Return "SELECT f.IdUczen,COUNT(f.ID) AS Abc,f.Data FROM frekwencja f Right JOIN przydzial p ON f.IdUczen=p.IdUczen WHERE f.Typ<>'s' AND f.Data BETWEEN '" & StartDate & "' AND '" & EndDate & "' AND p.Klasa='" & Klasa & "' AND p.RokSzkolny='" & RokSzkolny & "' GROUP BY f.IdUczen,f.Data;"
  End Function
  Public Function CountStudentByKlasa(Klasa As String, RokSzkolny As String) As String
    Return "SELECT Count(p.ID) AS StanKlasy FROM przydzial p WHERE p.RokSzkolny='" & RokSzkolny & "' AND Klasa='" & Klasa & "' AND StatusAktywacji=1;"
  End Function
  Public Function CountStudentBySzkola(Szkola As String, RokSzkolny As String) As String
    Return "SELECT sk.Nazwa_Klasy, Count(p.ID) AS StanKlasy,p.Klasa,LEFT(sk.Kod_Klasy,1) AS Pion FROM przydzial p INNER JOIN szkola_klasa sk ON p.Klasa=sk.ID WHERE p.RokSzkolny='" & RokSzkolny & "' AND sk.IdSzkola='" & Szkola & "' AND StatusAktywacji = 1 Group By p.Klasa Order By sk.Kod_Klasy;"
  End Function
  Public Function CountStudentByNKL(Szkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT o.Klasa,w.IdUczen,Count(w.ID) AS LiczbaNKL FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga=0 AND p.Typ='z' AND o.RokSzkolny='" & RokSzkolny & "' AND sp.IdSzkola='" & Szkola & "' AND k.Typ='" & Okres & "' Group BY o.Klasa,w.IdUczen;"
  End Function
  Public Function CountScoreByStudentByWaga(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT w.IdUczen,so.Waga,Count(w.ID) AS LO FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga BETWEEN 0 AND 1 AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' GROUP BY w.IdUczen,so.Waga;"
  End Function
  'Public Function CountScoreByStudent(Klasa As String, RokSzkolny As String, Okres As String) As String
  '  Return "SELECT w.IdUczen,Count(w.ID) AS LO FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga BETWEEN 0 AND 1 AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' GROUP BY w.IdUczen;"
  'End Function
  Public Function CountScoreByKlasaByStudentByWaga(Szkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT o.Klasa,w.IdUczen,so.Waga,Count(w.ID) AS LO FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga BETWEEN 0 AND 1 AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND sp.IdSzkola='" & Szkola & "' GROUP BY o.Klasa,w.IdUczen,so.Waga;"
  End Function
  Public Function CountScoreByObject(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT so.Waga,Count(w.ID) AS LO,p.Nazwa,CONCAT_WS(' ',n.Nazwisko,n.Imie) AS Belfer FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID INNER JOIN szkola_nauczyciel sn ON sn.ID=o.Nauczyciel INNER JOIN nauczyciel n ON sn.IdNauczyciel=n.ID WHERE so.Waga IN (0,1) AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' AND w.IdUczen NOT IN (SELECT w.IdUczen FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga=0 AND p.Typ='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "') GROUP BY so.Waga,p.Nazwa Order BY sp.Priorytet;"
  End Function

  Public Function CountZachowanieByWaga(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT so.Waga,Count(w.ID) AS LO FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga>0 AND p.Typ='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "' GROUP BY so.Waga;"
  End Function
  Public Function CountZachowanieByKlasaByWaga(Szkola As String, RokSzkolny As String, Okres As String) As String
    Return "SELECT o.Klasa,so.Waga,Count(w.ID) AS LO FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga>0 AND p.Typ='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND sp.IdSzkola='" & Szkola & "' GROUP BY o.Klasa,so.Waga;"
  End Function
  Public Function CountAvgByKlasa(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "Select Round(avg(so.Waga),2) AS Avg From wyniki w INNER JOIN skala_ocen so ON w.IdOcena=so.ID INNER JOIN kolumna k ON w.IdKolumna=k.ID INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN przydzial p ON p.IdUczen=w.IdUczen WHERE o.Klasa='" & Klasa & "' AND p.MasterRecord=1 AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND so.Waga>0 AND o.GetToAverage=1;"
  End Function

  Public Function InsertClasification() As String
    Return "INSERT INTO klasyfikacja (IdObsada,Typ,Status,Owner,User,ComputerIP) VALUES(?IdObsada,?Okres,?Status,'" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function InsertPoprawka(IdPrzydzial As String, IdObsada As String, Typ As String) As String
    Return "INSERT INTO poprawka (IdPrzydzial,IdObsada,Typ,Owner,User,ComputerIP) VALUES ('" & IdPrzydzial & "','" & IdObsada & "','" & Typ & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.AppUser.Login & "','" & GlobalValues.gblIP & "');"
  End Function
  Public Function DeleteClasification() As String
    Return "DELETE FROM klasyfikacja WHERE IdObsada=?IdObsada AND Typ=?Okres;"
  End Function
  Public Function UpdateColumnLock(Lock As String, Zakres As String, Obsada As String) As String
    Return "UPDATE kolumna k SET k.Lock=" & Lock & " WHERE k.Typ IN (" & Zakres & ") AND k.IdObsada IN (" & Obsada & ");"
  End Function
  Public Function UpdatePromotion(Klasa As String, RokSzkolny As String, Okres As String) As String
    Return "UPDATE przydzial SET Promocja=1 WHERE Klasa='" & Klasa & "' AND RokSzkolny='" & RokSzkolny & "' AND StatusAktywacji=1 AND IdUczen NOT IN (SELECT DISTINCT w.IdUczen FROM wyniki w INNER JOIN skala_ocen so ON so.ID=w.IdOcena INNER JOIN kolumna k ON k.ID=w.IdKolumna INNER JOIN obsada o ON o.ID=k.IdObsada INNER JOIN szkola_przedmiot sp ON sp.ID=o.Przedmiot INNER JOIN przedmiot p ON sp.IdPrzedmiot=p.ID WHERE so.Waga IN (0,1) AND p.Typ!='z' AND o.RokSzkolny='" & RokSzkolny & "' AND k.Typ='" & Okres & "' AND o.Klasa='" & Klasa & "');"
  End Function
  Public Function ChangeStatus() As String
    Return "UPDATE klasyfikacja SET Status=?Status,User='" & GlobalValues.AppUser.Login & "',ComputerIP='" & GlobalValues.gblIP & "' WHERE IdObsada=?IdObsada AND Typ=?Typ;"
  End Function
  Public Function SelectObsadaToLock(Klasa As String, RokSzkolny As String) As String
    Return "SELECT Group_Concat(o.ID) FROM obsada o WHERE o.Klasa='" & Klasa & "' AND o.RokSzkolny='" & RokSzkolny & "';"
  End Function
End Class